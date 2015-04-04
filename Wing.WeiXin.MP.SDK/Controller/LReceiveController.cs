using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.MsgCrypt;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 轻量级接收消息控制器
    /// 只支持单账号
    /// </summary>
    public class LReceiveController
    {
        /// <summary>
        /// 微信账号
        /// </summary>
        public WXAccount WXAccount { get; private set; }

        /// <summary>
        /// Token
        /// </summary>
        private readonly string token;

        /// <summary>
        /// 事件管理类
        /// </summary>
        public EventManager EventManager { get; private set; }

        /// <summary>
        /// 微信会话接口
        /// </summary>
        public IWXSession WXSession { get; private set; }

        #region 根据微信账号实例化 public LReceiveController(string token, string id, string appID, string appSecret, bool needEncoding, string encodingAESKey = null, IWXSession wxSession = null)
        /// <summary>
        /// 根据微信账号实例化
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="id">微信公共平台ID</param>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="needEncoding">是否需要加密</param>
        /// <param name="encodingAESKey">加密密钥</param>
        /// <param name="wxSession">微信会话接口</param>
        public LReceiveController(string token, string id, string appID, string appSecret, bool needEncoding, string encodingAESKey = null, IWXSession wxSession = null)
        {
            this.token = token;
            WXAccount = new WXAccount
            {
                ID = id,
                AppID = appID,
                AppSecret = appSecret,
                NeedEncoding = needEncoding,
                EncodingAESKey = encodingAESKey
            };
            if (needEncoding)
                WXAccount.WXBizMsgCrypt = new WXBizMsgCrypt
                {
                    token = token,
                    encodingAESKey = encodingAESKey,
                    appID = appID
                };
            EventManager = new EventManager();
            EventManager.IsCheckEventName = false;
            EventManager.IsCheckToUserName = false;
            WXSession = wxSession ?? new StaticWXSession();
            WXController.AccessTokenContainer = new AccessTokenContainer(WXSession);
        } 
        #endregion

        #region 执行事件 public Response Action(string postData)
        /// <summary>
        /// 执行事件
        /// 不需要检查请求
        /// </summary>
        /// <param name="postData">POST数据</param>
        /// <returns>响应对象</returns>
        public Response Action(string postData)
        {
            Request request = new Request(postData)
            {
                WXAccount = WXAccount
            };
            try
            {
                request.ParsePostData();
                return EventManager.ActionEvent(request);
            }
            catch (WXException e)
            {
                return new Response(e);
            }
        }
        #endregion

        #region 执行事件 public Response Action(string signature, string timestamp, string nonce, string echostr, string postData)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="postData">POST数据</param>
        /// <returns>响应对象</returns>
        public Response Action(string signature, string timestamp, string nonce, string echostr, string postData)
        {
            Request request = new Request(token, signature, timestamp, nonce, echostr, postData)
            {
                WXAccount = WXAccount
            };
            try
            {
                request.Check();
                request.ParsePostData();
                return CheckMsgID(request) ? null : EventManager.ActionEvent(request);
            }
            catch (WXException e)
            {
                return new Response(e);
            }
        } 
        #endregion

        #region 检测MsgID防止消息重复 private bool CheckMsgID(Request request)
        /// <summary>
        /// 检测MsgID防止消息重复
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否息重复</returns>
        private bool CheckMsgID(Request request)
        {
            LogManager.WriteSystem("检测MsgID");
            if (!request.HasPostData("MsgId")) return false;
            if (WXSession == null) return false;
            string msgID = request.GetMsgId();
            string msgIDTemp = WXSession.Get<string>(request.FromUserName, Settings.Default.LastMsgIDKey);
            if (msgIDTemp != null)
            {
                LogManager.WriteSystem("检测MsgID通过");
                string lastMsgID = msgIDTemp;
                if (msgID.Equals(lastMsgID)) return true;
            }
            WXSession.Set(request.FromUserName, Settings.Default.LastMsgIDKey, msgID);
            LogManager.WriteSystem("检测MsgID未通过");
            return false;
        }
        #endregion
    }
}
