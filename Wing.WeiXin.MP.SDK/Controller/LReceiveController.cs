using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;

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

        /// <summary>
        /// 功能管理器
        /// </summary>
        public static FunctionManager FunctionManager { get; private set; }

        #region 根据微信账号实例化 public LReceiveController(string token, string id, string appID, string appSecret, string encodingAESKey = null, IWXSession wxSession = null)
        /// <summary>
        /// 根据微信账号实例化
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="id">微信公共平台ID</param>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="encodingAESKey">加密密钥</param>
        /// <param name="wxSession">微信会话接口</param>
        public LReceiveController(string token, string id, string appID, string appSecret, string encodingAESKey = null, IWXSession wxSession = null)
        {
            this.token = token;
            WXAccount = new WXAccount(
                token,
                id,
                appID,
                appSecret,
                encodingAESKey);
            EventManager = new EventManager();
            EventManager.IsCheckEventName = false;
            EventManager.IsCheckToUserName = false;
            WXSession = wxSession ?? new StaticWXSession();
            FunctionManager = new FunctionManager(new AccessTokenContainer(WXSession));
        } 
        #endregion

        #region 执行事件 public Response Action(string postData, string encryptType, string msgSignature)
        /// <summary>
        /// 执行事件
        /// 不需要检查请求
        /// </summary>
        /// <param name="postData">POST数据</param>
        /// <param name="encryptType">加密类型</param>
        /// <param name="msgSignature">消息体的签名</param>
        /// <returns>响应对象</returns>
        public Response Action(string postData, string encryptType, string msgSignature)
        {
            Request request = new Request(postData, encryptType, msgSignature)
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

        #region 执行事件 public Response Action(string signature, string timestamp, string nonce, string echostr, string postData, string encryptType, string msgSignature)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="postData">POST数据</param>
        /// <param name="encryptType">加密类型</param>
        /// <param name="msgSignature">消息体的签名</param>
        /// <returns>响应对象</returns>
        public Response Action(string signature, string timestamp, string nonce, string echostr, string postData, string encryptType, string msgSignature)
        {
            Request request = new Request(token, signature, timestamp, nonce, echostr, postData, encryptType, msgSignature)
            {
                WXAccount = WXAccount
            };
            try
            {
                request.Check();
                request.ParsePostData();
                return EventManager.ActionEvent(request);
            }
            catch (WXException e)
            {
                return new Response(e);
            }
        } 
        #endregion
    }
}
