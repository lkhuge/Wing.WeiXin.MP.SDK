using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 响应对象
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 运行时长
        /// </summary>
        public long RunTime { get; private set; }

        /// <summary>
        /// 执行的事件名称
        /// </summary>
        public string ActionEventName { get; internal set; }

        /// <summary>
        /// 响应类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string Text { get; private set; }

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

        #region 根据异常实例化文本类型对象 public Response(Exception e)
        /// <summary>
        /// 根据异常实例化文本类型对象
        /// </summary>
        /// <param name="e">异常</param>
        public Response(Exception e)
        {
            Text = e.Message;
            ContentType = TEXT;
        }
        #endregion

        #region 根据响应内容和响应类型实例化 public Response(string text, Request request, string type)
        /// <summary>
        /// 根据响应内容和响应类型实例化
        /// </summary>
        /// <param name="text">响应内容</param>
        /// <param name="request">请求对象</param>
        /// <param name="type">响应类型</param>
        public Response(string text, Request request, string type)
        {
            Text = GetCryptMessage(text, request);
            ContentType = type;
            RunTime = request.GetRunTime();
        }
        #endregion

        #region 获取加密消息 private string GetCryptMessage(string text, Request request)
        /// <summary>
        /// 获取加密消息
        /// </summary>
        /// <param name="text">原文</param>
        /// <param name="request">请求对象</param>
        /// <returns>加密后的消息</returns>
        private string GetCryptMessage(string text, Request request)
        {
            if (request.WXAccount.WXBizMsgCrypt == null || !"aes".Equals(request.EncryptType)) return text;
            string encryptMsg = null;
            if (request.WXAccount.WXBizMsgCrypt.EncryptMsg(
                text,
                DateTimeHelper.GetLongTimeByDateTime(DateTime.Now).ToString(CultureInfo.InvariantCulture),
                request.Nonce,
                ref encryptMsg) != 0)
                throw WXException.GetInstance(String.Format("消息加密失败，原文：{0}", text), "[CryptMessage]Account:" + request.ToUserName);

            return encryptMsg;
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
