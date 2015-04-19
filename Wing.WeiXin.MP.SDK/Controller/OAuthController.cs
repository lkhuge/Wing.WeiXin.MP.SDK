using System;
using System.Web;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.OAuth;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// OAuth控制器
    /// </summary>
    public class OAuthController : WXController
    {
        /// <summary>
        /// 获取取得Code的URL的URL
        /// </summary>
        private const string UrlGetURLForOAuthGetCode = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";

        /// <summary>
        /// 根据Code获取AccessToken的URL
        /// </summary>
        private const string UrlGetAccessTokenByCode = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";

        /// <summary>
        /// 根据RefreshToken刷新AccessToken的URL
        /// </summary>
        private const string UrlRefreshAccessTokenByRefreshToken = "https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}";

        /// <summary>
        /// 获取认证用户信息的URL
        /// </summary>
        private const string UrlGetOAuthUser = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}";

        #region 根据AccessToken容器初始化 public OAuthController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public OAuthController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 获取取得Code的URL public string GetURLForOAuthGetCode(WXAccount account, string redirectURL, OAuthScope scope, string state)
        /// <summary>
        /// 获取取得Code的URL
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="redirectURL">授权后重定向的回调链接地址</param>
        /// <param name="scope">应用授权作用域</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <returns>取得Code的URL</returns>
        public string GetURLForOAuthGetCode(WXAccount account, string redirectURL, OAuthScope scope, string state)
        {
            return String.Format(
                UrlGetURLForOAuthGetCode,
                account.AppID, HttpUtility.UrlEncode(redirectURL), scope, state);
        } 
        #endregion

        #region 根据Code获取AccessToken public OAuthAccessToken GetAccessTokenByCode(WXAccount account, string code)
        /// <summary>
        /// 根据Code获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="code">Code值</param>
        /// <returns>AccessToken</returns>
        public OAuthAccessToken GetAccessTokenByCode(WXAccount account, string code)
        {
            return ActionWithoutAccessToken<OAuthAccessToken>(
                String.Format(UrlGetAccessTokenByCode, account.AppID, account.AppSecret, code),
                account);
        } 
        #endregion

        #region 根据RefreshToken刷新AccessToken public OAuthAccessToken RefreshAccessTokenByRefreshToken(WXAccount account, string refresh_token)
        /// <summary>
        /// 根据RefreshToken刷新AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="refresh_token">用户刷新AccessToken值</param>
        /// <returns>AccessToken</returns>
        public OAuthAccessToken RefreshAccessTokenByRefreshToken(WXAccount account, string refresh_token)
        {
            return ActionWithoutAccessToken<OAuthAccessToken>(
                String.Format(UrlRefreshAccessTokenByRefreshToken, account.AppID, refresh_token),
                account);
        }
        #endregion

        #region 获取认证用户信息 public OAuthUser GetOAuthUser(WXAccount account, OAuthAccessToken accessToken, OAuthUserInfoLanguage language = OAuthUserInfoLanguage.zh_CN)
        /// <summary>
        /// 获取认证用户信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">OAuth使用的AccessToken</param>
        /// <param name="language">返回国家地区语言版本</param>
        /// <returns>AccessToken</returns>
        public OAuthUser GetOAuthUser(WXAccount account, OAuthAccessToken accessToken, WXLanguageType language = WXLanguageType.zh_CN)
        {
            return ActionWithoutAccessToken<OAuthUser>(
                String.Format(UrlGetOAuthUser, accessToken.access_token, accessToken.openid, language), 
                account);
        } 
        #endregion
    }
}
