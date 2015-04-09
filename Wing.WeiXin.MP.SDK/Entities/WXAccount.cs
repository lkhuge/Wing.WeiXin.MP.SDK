using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// 是否需要加密
        /// </summary>
        public bool NeedEncoding { get; private set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; private set; }

        /// <summary>
        /// 微信加解密工具
        /// </summary>
        internal WXBizMsgCrypt WXBizMsgCrypt;

        #region 根据参数实例化 public WXAccount(string token, string id, string appID, string appSecret, bool needEncoding = false, string encodingAESKey = null)
        /// <summary>
        /// 根据参数实例化
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="id">微信公共平台ID</param>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="needEncoding">是否需要加密</param>
        /// <param name="encodingAESKey">加密密钥</param>
        public WXAccount(string token, string id, string appID, string appSecret, bool needEncoding = false, string encodingAESKey = null)
        {
            Token = token;
            ID = id;
            AppID = appID;
            AppSecret = appSecret;
            NeedEncoding = needEncoding;
            if (!needEncoding) return;
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
            return String.Format("ID:{1}{0}{0}{0}AppID:{2}{0}AppSecret:{3}{0}是否需要加密:{4}{0}加密密钥:{5}",
                Environment.NewLine,
                ID,
                AppID,
                AppSecret,
                NeedEncoding,
                EncodingAESKey);
        } 
        #endregion
    }
}
