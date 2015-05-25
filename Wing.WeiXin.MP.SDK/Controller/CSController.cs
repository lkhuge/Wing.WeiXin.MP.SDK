using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.CSMessages;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 客服控制器
    /// </summary>
    public class CSController : WXController
    {
        /// <summary>
        /// 发送客服消息的URL
        /// </summary>
        private const string Url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=[AT]";

        #region 根据AccessToken容器初始化 public CSController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public CSController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 发送客服信息 public ErrorMsg SendCSMessage(WXAccount account, CSMessage csmessage)
        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="csmessage">客服信息</param>
        /// <returns>错误码</returns>
        public ErrorMsg SendCSMessage(WXAccount account, CSMessage csmessage)
        {
            return Action<ErrorMsg>(Url, csmessage, account);
        } 
        #endregion
    }
}
