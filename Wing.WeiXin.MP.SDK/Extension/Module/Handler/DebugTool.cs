using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.Module.Handler
{
    /// <summary>
    /// 调试工具
    /// </summary>
    public class DebugTool : IHttpHandler
    {
        /// <summary>
        /// 接收消息控制器
        /// </summary>
        private readonly ReceiveController receiveController = new ReceiveController();

        /// <summary>
        /// 模式参数名称
        /// </summary>
        public static string ModeName = "Mode";

        /// <summary>
        /// 提交信息参数名称
        /// </summary>
        public static string SubmitMessageName = "SubmitMessage";

        /// <summary>
        /// 提交信息数据参数名称
        /// </summary>
        public static string SubmitMessageDataName = "Data";

        /// <summary>
        /// 刷新服务器参数名称
        /// </summary>
        public static string RefreshServerName = "RefreshServer";

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            string mode = context.Request.QueryString[ModeName];
            object result = new { msg = "未知操作" };
            if (SubmitMessageName.Equals(mode)) result = SubmitMessage(context.Request.Form[SubmitMessageDataName]);
            if (RefreshServerName.Equals(mode)) result = RefreshServer();

            context.Response.Write(JSONHelper.JSONSerialize(result));
        }
        #endregion

        #region 提交消息 private object SubmitMessage(string data)
        /// <summary>
        /// 提交消息
        /// </summary>
        /// <param name="data">消息数据</param>
        /// <returns>结果</returns>
        private object SubmitMessage(string data)
        {
            try
            {
                Response response = receiveController.Action(
                    new Request(HttpUtility.UrlDecode(data), null, null),
                    false);
                return new
                {
                    data = response == null ? "" : FormatXMLToHTML(response.Text)
                };
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        }
        #endregion

        #region 刷新服务器 private object RefreshServer()
        /// <summary>
        /// 刷新服务器
        /// </summary>
        /// <returns>结果</returns>
        private object RefreshServer()
        {
            try
            {
                HttpRuntime.UnloadAppDomain();
                return new
                {
                    msg = "操作成功！下次请求将会重启服务器程序"
                };
            }
            catch (Exception e)
            {
                return new
                {
                    msg = e.Message
                };
            }
        }
        #endregion

        #region 将XML文本格式化并转换为HTML文本 private static string FormatXMLToHTML(string xmlStr)
        /// <summary>
        /// 将XML文本格式化并转换为HTML文本
        /// </summary>
        /// <param name="xmlStr">XML文本</param>
        /// <returns>HTML文本</returns>
        private static string FormatXMLToHTML(string xmlStr)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlStr);
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (XmlTextWriter xtw = new XmlTextWriter(sw)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 1,
                        IndentChar = '\t'
                    })
                    {
                        xd.WriteTo(xtw);
                        return HttpUtility.HtmlEncode(sb.ToString())
                                    .Replace(Environment.NewLine, "</br>")
                                    .Replace(Convert.ToString('\t'), "&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
