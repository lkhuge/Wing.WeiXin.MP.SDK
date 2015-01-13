using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.ReceiveHandler.Ashx
{
    /// <summary>
    /// 调试事件处理
    /// 
    /// 用法：通过GET方式请求，通过Mode来区分调试模式
    /// 目前支持两种调试内容
    /// 1."MessageTest" 显示一个简易的网页界面，可以直接模拟发送一个消息请求
    ///     PS: 由于发送的消息为XML格式，会触发数据安全的异常，如果要使用这个功能可以把这个数据验证关闭
    ///         关闭方法：
    ///             在Web.config中：configuration -> system.web
    ///             1.添加节点<pages validateRequest="false" /> 
    ///             2.httpRuntime 中 添加属性 requestValidationMode="2.0"
    /// 2."TextMessageTest" 直接模拟发送一个文本消息
    ///     参数：
    ///         "Message" : 文本消息内容
    ///         "Account" : 账号ID，如果为空，则为账号列表的第一个账号
    ///         "User"    : 用户OpenID，如果为空，则为"TestUser"
    ///     文本消息内容在"Param"参数中设置
    /// </summary>
    public class AshxDebugHandler : IHttpHandler
    {
        /// <summary>
        /// 文本消息模板
        /// </summary>
        private const string textMessageFormat = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content><MsgId>1234567890</MsgId></xml>";

        /// <summary>
        /// 输入提示HTML模板
        /// </summary>
        private const string inputMessageHTML = "<form method='POST' action='?Mode=MessageTestPost'><label>Message:<textarea name='Param' rows='10' cols='80'></textarea></label><input type='submit' text='Submit'/></form>";

        /// <summary>
        /// 消息调试字符串
        /// </summary>
        private const string MessageTest = "MessageTest";

        /// <summary>
        /// 接收消息控制器
        /// </summary>
        private readonly ReceiveController receiveController = new ReceiveController();

        #region 执行方法列表 private readonly Dictionary<string, Func<string, string>> actionList = new Dictionary<string, Func<string, string>>
        /// <summary>
        /// 执行方法列表
        /// </summary>
        private readonly Dictionary<string, Func<HttpRequest, string>> actionList = new Dictionary<string, Func<HttpRequest, string>>
        {
            {MessageTest, p => inputMessageHTML },
            {"MessageTestPost", p => MessageRequest(p.Form["Param"]) },
            {"TextMessageTest", p => MessageRequest(String.Format(textMessageFormat,
                String.IsNullOrEmpty(p.QueryString["Account"]) ? GlobalManager.GetFirstAccount().ID : p.QueryString["Account"],
                String.IsNullOrEmpty(p.QueryString["User"]) ? "TestUser" : p.QueryString["User"],
                LibManager.DateTimeHelper.GetLongTimeByDateTime(DateTime.Now),
                p.QueryString["Message"]))
            }
        };
        #endregion

        #region 模拟执行操作 private static string MessageRequest(string message)
        /// <summary>
        /// 模拟执行操作
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>响应</returns>
        private static string MessageRequest(string message)
        {
            Response response = new ReceiveController().Action(
                new Request(message),
                false);
            return response == null ? "" : FormatXMLToHTML(response.Text);
        }
        #endregion

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            string mode = context.Request.QueryString["Mode"];
            mode = String.IsNullOrEmpty(mode) ? MessageTest : mode;
            if (!actionList.ContainsKey(mode))
            {
                Response response = receiveController.Action(
                    new Request(LibManager.HTTPHelper.GetPostStream(context)),
                    false);
                context.Response.Write(response == null ? "" : response.Text);
                return;
            }
            context.Response.Write(actionList[mode](context.Request));
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
        public bool IsReusable { get; private set; }
    }
}
