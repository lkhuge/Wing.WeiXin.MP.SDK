using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.MessageFilter;
using Wing.WeiXin.MP.SDK.Common.MsgCrypt;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 微信账号
    /// </summary>
    public class WXAccount
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// 微信公共平台ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; private set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; private set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; private set; }

        /// <summary>
        /// 微信加解密工具
        /// </summary>
        internal WXBizMsgCrypt WXBizMsgCrypt;

        /// <summary>
        /// 消息过滤器列表
        /// </summary>
        private List<IMessageFilter> MessageFilterList;

        #region 根据参数实例化 public WXAccount(string token, string id, string appID, string appSecret, string encodingAESKey = null)
        /// <summary>
        /// 根据参数实例化
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="id">微信公共平台ID</param>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="encodingAESKey">加密密钥</param>
        public WXAccount(string token, string id, string appID, string appSecret, string encodingAESKey = null)
        {
            Token = token;
            ID = id;
            AppID = appID;
            AppSecret = appSecret;
            if (String.IsNullOrEmpty(encodingAESKey)) return;
            EncodingAESKey = encodingAESKey;
            WXBizMsgCrypt = new WXBizMsgCrypt
            {
                token = token,
                encodingAESKey = encodingAESKey,
                appID = appID
            };
        } 
        #endregion

        #region 获取账户信息 public override string ToString()
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns>账户信息</returns>
        public override string ToString()
        {
            return String.Format("ID:{1}{0}{0}{0}AppID:{2}{0}AppSecret:{3}{0}加密密钥:{4}",
                Environment.NewLine,
                ID,
                AppID,
                AppSecret,
                EncodingAESKey);
        } 
        #endregion

        #region 添加消息过滤器 public void AddMessageFilter(IMessageFilter messageFilter)
        /// <summary>
        /// 添加消息过滤器
        /// </summary>
        /// <param name="messageFilter">消息过滤器</param>
        public void AddMessageFilter(IMessageFilter messageFilter)
        {
            if (MessageFilterList == null) MessageFilterList = new List<IMessageFilter>
            {
                new CheckMsgIDMessageFilter()
            };
            MessageFilterList.Add(messageFilter);
        } 
        #endregion

        #region 消息过滤 internal Response MessageFilter(Request request)
        /// <summary>
        /// 消息过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象（如果为空则跳过过滤）</returns>
        internal Response MessageFilter(Request request)
        {
            if (MessageFilterList == null) return null;

            return MessageFilterList.Select(t => t.Action(request)).FirstOrDefault(response => response != null);
        } 
        #endregion
    }
}
