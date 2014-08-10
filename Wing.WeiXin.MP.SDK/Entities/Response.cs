using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 响应对象
    /// </summary>
    public class Response
    {
        #region 类型
        /// <summary>
        /// JSON类型
        /// </summary>
        public static string JSON = "application/json";

        /// <summary>
        /// XML类型
        /// </summary>
        public static string XML = "application/xml";

        /// <summary>
        /// 文本类型
        /// </summary>
        public static string TEXT = "text/plain";
        #endregion

        #region 成员变量
        /// <summary>
        /// 响应类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string Text { get; private set; }
        #endregion

        #region 根据响应内容实例化文本类型对象 public Response(string text)
        /// <summary>
        /// 根据响应内容实例化文本类型对象
        /// </summary>
        /// <param name="text">响应内容</param>
        public Response(string text)
        {
            Text = text;
            ContentType = TEXT;
        }
        #endregion

        #region 根据微信异常实例化文本类型对象 public Response(WXException e)
        /// <summary>
        /// 根据微信异常实例化文本类型对象
        /// </summary>
        /// <param name="e">消息对象</param>
        public Response(Exception e)
        {
            Text = e.Message;
            ContentType = TEXT;
        }
        #endregion

        #region 根据响应内容和响应类型实例化 public Response(string text, string type)
        /// <summary>
        /// 根据响应内容和响应类型实例化
        /// </summary>
        /// <param name="text">响应内容</param>
        /// <param name="type">响应类型</param>
        public Response(string text, string type)
        {
            Text = text;
            ContentType = type;
        }
        #endregion

        #region 输出响应 public void ResponseOutput(HttpResponse response)
        /// <summary>
        /// 输出响应
        /// </summary>
        /// <param name="response">HTTP响应</param>
        public void ResponseOutput(HttpResponse response)
        {
            response.ContentType = ContentType;
            response.Write(Text);
        }
        #endregion

        #region 获取完整响应信息 public override string ToString()
        /// <summary>
        /// 获取完整响应信息
        /// </summary>
        /// <returns>完整响应信息</returns>
        public override string ToString()
        {
            return String.Format("[ContentType]:{0}[Text]:{1}",
                ContentType, Text);
        }
        #endregion
    }
}
