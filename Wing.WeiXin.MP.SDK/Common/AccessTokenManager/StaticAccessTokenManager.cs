using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.AccessTokenManager
{
    /// <summary>
    /// 静态AccessToken管理类
    /// </summary>
    public class StaticAccessTokenManager : IAccessTokenManager
    {
        /// <summary>
        /// AccessToken列表
        /// </summary>
        private static readonly ConcurrentDictionary<string, AccessToken> accessTokenList
            = new ConcurrentDictionary<string, AccessToken>();

        /// <summary>
        /// 截止日期列表
        /// </summary>
        private static readonly ConcurrentDictionary<string, DateTime> expiresDateTimeList
            = new ConcurrentDictionary<string, DateTime>();

        #region 根据微信公共平台账号获取AccessToken public AccessToken GetAccessToken(WXAccount account);
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public AccessToken GetAccessToken(WXAccount account)
        {
            AccessToken temp;

            return !accessTokenList.TryGetValue(account.ID, out temp) ? null : temp;
        }

        #endregion

        #region 根据微信公共平台账号设置AccessToken public void SetAccessToken(WXAccount account, AccessToken accessToken);
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        public void SetAccessToken(WXAccount account, AccessToken accessToken)
        {
            accessTokenList.GetOrAdd(account.ID, accessToken);
        }
        #endregion

        #region 根据微信公共平台账号获取AccessToken的截止日期 public DateTime GetExpiresDateTime(WXAccount account);
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken的截止日期</returns>
        public DateTime GetExpiresDateTime(WXAccount account)
        {
            DateTime temp;
            if (!expiresDateTimeList.TryGetValue(account.ID, out temp)) 
                throw new Exception("没有该账号的截止日期信息");

            return temp;
        }
        #endregion

        #region 根据微信公共平台账号设置AccessToken的截止日期 public void SetExpiresDateTime(WXAccount account, DateTime expires);
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="expires">AccessToken的截止日期</param>
        public void SetExpiresDateTime(WXAccount account, DateTime expires)
        {
            expiresDateTimeList.GetOrAdd(account.ID, expires);
        }
        #endregion
    }
}
