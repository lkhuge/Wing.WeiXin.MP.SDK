using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.AccessTokenManager
{
    /// <summary>
    /// AccessToken容器
    /// </summary>
    public class AccessTokenContainer
    {
        /// <summary>
        /// 获取AccessToken的URL
        /// </summary>
        private const string Url =
            "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 获取新的AccessToken事件
        /// </summary>
        public event Action<WXAccount> NewAccessToken;

        #region 获取AccessToken public AccessToken GetAccessToken(WXAccount account)
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public AccessToken GetAccessToken(WXAccount account)
        {
            AccessToken accessToken = GlobalManager.WXSessionManager.Get<AccessToken>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID);
            DateTime accessTokenExpDT = GlobalManager.WXSessionManager.Get<DateTime>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID);
            if (accessToken != null && accessTokenExpDT != default(DateTime)
                && DateTime.Now < accessTokenExpDT) return accessToken;

            return GetNewAccessToken(account);
        } 
        #endregion

        #region 获取新的AccessToken private AccessToken GetNewAccessToken(WXAccount account)
        /// <summary>
        /// 获取新的AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        private AccessToken GetNewAccessToken(WXAccount account)
        {
            if (NewAccessToken != null) NewAccessToken(account);
            string result = HTTPHelper.Get(String.Format(Url, account.AppID, account.AppSecret));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce(), account.ID, account);
            }
            AccessToken accessTokenNew = JSONHelper.JSONDeserialize<AccessToken>(result);
            GlobalManager.WXSessionManager.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID,
                accessTokenNew);
            GlobalManager.WXSessionManager.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID,
                DateTime.Now + new TimeSpan(0, 0, accessTokenNew.expires_in));

            return accessTokenNew;
        } 
        #endregion
    }
}
