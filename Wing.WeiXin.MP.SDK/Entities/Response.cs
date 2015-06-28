using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        /// <summary>
        /// 请求对象
        /// </summary>
        public Request Request { get; private set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; }

        #region 类型
        /// <summary>
        /// JSON类型
        /// </summary>
        public const string JSON = "application/json";

        /// <summary>
        /// XML类型
        /// </summary>
        public const string XML = "application/xml";

        /// <summary>
        /// 文本类型
        /// </summary>
        public const string TEXT = "text/plain";
        #endregion

        #region 响应数据索引器 public object this[string key]
        /// <summary>
        /// 响应数据索引器
        /// </summary>
        /// <param name="key">索引Key</param>
        /// <returns>索引Value（不存在则返回null）</returns>
        public object this[string key]
        {
            get { return Data.ContainsKey(key) ? Data[key] : null; }
        }
        #endregion

        #region 根据异常实例化文本类型对象 internal Response(Exception e)
        /// <summary>
        /// 根据异常实例化文本类型对象
        /// </summary>
        /// <param name="e">异常</param>
        internal Response(Exception e)
        {
            Text = e.Message;
            ContentType = TEXT;
        }
        #endregion

        #region 根据响应内容请求对象和响应类型实例化 internal Response(string text, Request request, string type)
        /// <summary>
        /// 根据响应内容请求对象和响应类型实例化
        /// </summary>
        /// <param name="text">响应内容</param>
        /// <param name="request">请求对象</param>
        /// <param name="type">响应类型</param>
        internal Response(string text, Request request, string type)
        {
            Request = request;
            Text = GetCryptMessage(text, request);
            ContentType = type;
            RunTime = request.GetRunTime();
        }
        #endregion

        #region 根据响应内容响应数据请求对象和响应类型实例化 internal Response(string text, Dictionary<string, object> data, Request request, string type)
        /// <summary>
        /// 根据响应内容响应数据请求对象和响应类型实例化
        /// </summary>
        /// <param name="text">响应内容</param>
        /// <param name="data">响应数据</param>
        /// <param name="request">请求对象</param>
        /// <param name="type">响应类型</param>
        internal Response(string text, Dictionary<string, object> data, Request request, string type) 
            : this(text, request, type)
        {
            Data = data;
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
            return String.Format("[ContentType]:{0}{2}{3}[Text]:{1}",
                ContentType, Text,
                String.IsNullOrEmpty(ActionEventName) ? "" : String.Format("[ActionEventName]:{0}", ActionEventName),
                RunTime == 0 ? "" : String.Format("[RunTime]:{0}", RunTime));
        }
        #endregion

        #region 获取响应数据介绍 public string GetDataIntroduce(Dictionary<string, object> data = null)
        /// <summary>
        /// 获取响应数据介绍
        /// </summary>
        /// <returns>响应数据介绍</returns>
        public string GetDataIntroduce(Dictionary<string, object> data = null)
        {
            data = data ?? Data;
            string sp = Environment.NewLine + "#######" + Environment.NewLine;
            return String.Join(Environment.NewLine,
                data.Select(d => String.Format("{0}：{1}", 
                    d.Key, 
                    (d.Value is string || d.Value is int)
                        ? d.Value
                        : (sp + String.Join(sp, ((Dictionary<string, object>[])d.Value)
                            .Select(GetDataIntroduce)) + sp))));
        } 
        #endregion
    }
}
