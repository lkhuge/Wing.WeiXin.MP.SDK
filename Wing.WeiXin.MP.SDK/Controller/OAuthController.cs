using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Entities.OAuth;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// OAuth控制器
    /// </summary>
    public class OAuthController
    {
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
            account.CheckIsService();
            return URLManager.GetURLForOAuthGetCode(account.AppID, redirectURL, scope, state);
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
            account.CheckIsService();
            string result = HTTPHelper.Get(URLManager.GetURLForOAuthGetAccessToken(account.AppID, account.AppSecret, code));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }
            return JSONHelper.JSONDeserialize<OAuthAccessToken>(result);
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
            account.CheckIsService();
            string result = HTTPHelper.Get(URLManager.GetURLForOAuthRefreshAccessToken(account.AppID, refresh_token));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<OAuthAccessToken>(result);
        }
        #endregion

        #region 获取认证用户信息 public OAuthUser GetOAuthUser(OAuthAccessToken accessToken)
        /// <summary>
        /// 获取认证用户信息
        /// </summary>
        /// <param name="accessToken">OAuth使用的AccessToken</param>
        /// <returns>AccessToken</returns>
        public OAuthUser GetOAuthUser(OAuthAccessToken accessToken)
        {
            string result = HTTPHelper.Get(URLManager.GetURLForOAuthGetUserInfo(accessToken));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<OAuthUser>(result);
        } 
        #endregion
    }
}
