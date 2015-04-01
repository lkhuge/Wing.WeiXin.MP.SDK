using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.AccessTokenManager
{
    /// <summary>
    /// 微信会话管理器AccessToken管理类
    /// </summary>
    public class WXSessionAccessTokenManager : IAccessTokenManager
    {
        #region 根据微信公共平台账号获取AccessToken public AccessToken GetAccessToken(WXAccount account)
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public AccessToken GetAccessToken(WXAccount account)
        {
            return GlobalManager.WXSessionManager.Get<AccessToken>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID);
        } 
        #endregion

        #region 根据微信公共平台账号设置AccessToken public void SetAccessToken(WXAccount account, AccessToken accessToken)
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        public void SetAccessToken(WXAccount account, AccessToken accessToken)
        {
            GlobalManager.WXSessionManager.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenHead + account.ID,
                accessToken);
        } 
        #endregion

        #region 根据微信公共平台账号获取AccessToken的截止日期 public DateTime GetExpiresDateTime(WXAccount account)
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken的截止日期</returns>
        public DateTime GetExpiresDateTime(WXAccount account)
        {
            return GlobalManager.WXSessionManager.Get<DateTime>(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID);
        } 
        #endregion

        #region 根据微信公共平台账号设置AccessToken的截止日期 public void SetExpiresDateTime(WXAccount account, DateTime expires)
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="expires">AccessToken的截止日期</param>
        public void SetExpiresDateTime(WXAccount account, DateTime expires)
        {
            GlobalManager.WXSessionManager.Set(
                Settings.Default.SystemUsername,
                Settings.Default.AccessTokenTimeHead + account.ID,
                expires);
        } 
        #endregion
    }
}
