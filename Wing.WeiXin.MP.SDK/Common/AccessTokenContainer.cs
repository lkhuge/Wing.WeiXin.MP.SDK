using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// AccessToken容器
    /// </summary>
    public static class AccessTokenContainer
    {
        /// <summary>
        /// 获取新的AccessToken事件
        /// </summary>
        public static event Action<WXAccount> NewAccessToken;

        /// <summary>
        /// 截止日期
        /// </summary>
        private static readonly Dictionary<string, DateTime> ExpiresDateTime = new Dictionary<string, DateTime>();

        /// <summary>
        /// AccessToken
        /// </summary>
        private static readonly Dictionary<string, AccessToken> AccessToken = new Dictionary<string, AccessToken>();

        #region 获取AccessToken public static AccessToken GetAccessToken(WXAccount account)
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public static AccessToken GetAccessToken(WXAccount account)
        {
            if (AccessToken.ContainsKey(account.ID) && DateTime.Now < ExpiresDateTime[account.ID]) return AccessToken[account.ID];
            GetNewAccessToken(account);

            return AccessToken[account.ID];
        } 
        #endregion

        #region 获取新的AccessToken private static void GetNewAccessToken(WXAccount account)
        /// <summary>
        /// 获取新的AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        private static void GetNewAccessToken(WXAccount account)
        {
            if (NewAccessToken != null) NewAccessToken(account);
            string result = HTTPHelper.Get(URLManager.GetURLForGetAccessToken(account.AppID, account.AppSecret));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }
            AccessToken[account.ID] = JSONHelper.JSONDeserialize<AccessToken>(result);
            ExpiresDateTime[account.ID] = DateTime.Now + new TimeSpan(0, 0, AccessToken[account.ID].expires_in);
        } 
        #endregion
    }
}
