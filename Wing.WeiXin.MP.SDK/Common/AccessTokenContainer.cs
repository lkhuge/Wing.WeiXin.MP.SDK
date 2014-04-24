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

        #region 获取AccessToken public static AccessToken GetAccessToken(string weixinMPID)
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>AccessToken</returns>
        public static AccessToken GetAccessToken(string weixinMPID)
        {
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("开始获取AccessToken", typeof(AccessTokenContainer));
            if (AccessToken.ContainsKey(weixinMPID) && DateTime.Now < ExpiresDateTime[weixinMPID]) return AccessToken[weixinMPID];
            GetNewAccessToken(weixinMPID);
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("成功开始获取AccessToken", typeof(AccessTokenContainer));

            return AccessToken[weixinMPID];
        } 
        #endregion

        #region 获取新的AccessToken private static void GetNewAccessToken(string weixinMPID)
        /// <summary>
        /// 获取新的AccessToken
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        private static void GetNewAccessToken(string weixinMPID)
        {
            AccountItemConfigSection account =
                ConfigManager.BaseConfig.AccountList.GetAccountItemConfigSection(weixinMPID);
            if (account == null) throw new FailGetAccountException(weixinMPID);
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info("开始获取新的AccessToken", typeof(AccessTokenContainer));
            string result = HTTPHelper.Get(URLManager.GetURLForGetAccessToken(account.AppID, account.AppSecret));
            ErrorMsg errorMsg = Authentication.CheckHaveErrorMsg(result);
            if (errorMsg != null) throw new FailGetAccessToken(errorMsg.GetIntroduce());
            AccessToken[weixinMPID] = JSONHelper.JSONDeserialize<AccessToken>(result);
            ExpiresDateTime[weixinMPID] = DateTime.Now + new TimeSpan(0, 0, AccessToken[weixinMPID].expires_in);
            if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info(
                    String.Format("成功获取新的AccessToken（AccessTokenID:{0}, 截止日期：{1}）",
                    AccessToken[weixinMPID].access_token, ExpiresDateTime), typeof(AccessTokenContainer));
        } 
        #endregion
    }
}
