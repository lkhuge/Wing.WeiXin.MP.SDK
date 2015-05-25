using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 安全控制器
    /// </summary>
    public class SecurityController : WXController
    {
        /// <summary>
        /// 获取微信服务器IP列表的URL
        /// </summary>
        private const string UrlGetWXServerIPList = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=[AT]";

        #region 根据AccessToken容器初始化 public SecurityController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public SecurityController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 获取微信服务器IP列表 public WXServerIPList GetWXServerIPList(WXAccount account)
        /// <summary>
        /// 获取微信服务器IP列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>微信服务器IP列表</returns>
        public WXServerIPList GetWXServerIPList(WXAccount account)
        {
            return Action<WXServerIPList>(UrlGetWXServerIPList, account);
        }
        #endregion
    }
}
