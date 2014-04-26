using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Exception;
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
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("开始获取AccessToken", typeof(AccessTokenContainer));
            if (AccessToken.ContainsKey(account.ID) && DateTime.Now < ExpiresDateTime[account.ID]) return AccessToken[account.ID];
            GetNewAccessToken(account);
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("成功开始获取AccessToken", typeof(AccessTokenContainer));

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
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("开始获取新的AccessToken", typeof(AccessTokenContainer));
            string result = HTTPHelper.Get(URLManager.GetURLForGetAccessToken(account.AppID, account.AppSecret));
            ErrorMsg errorMsg = Authentication.CheckHaveErrorMsg(result);
            if (errorMsg != null) throw new FailGetAccessToken(errorMsg.GetIntroduce());
            AccessToken[account.ID] = JSONHelper.JSONDeserialize<AccessToken>(result);
            ExpiresDateTime[account.ID] = DateTime.Now + new TimeSpan(0, 0, AccessToken[account.ID].expires_in);
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info(
                    String.Format("成功获取新的AccessToken（AccessTokenID:{0}, 截止日期：{1}）",
                    AccessToken[account.ID].access_token, ExpiresDateTime), typeof(AccessTokenContainer));
        } 
        #endregion
    }
}
