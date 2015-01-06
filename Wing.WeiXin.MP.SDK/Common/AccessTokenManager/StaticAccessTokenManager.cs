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
        /// AccessToken锁标示
        /// </summary>
        private static readonly object accessTokenLockSign = new object();
        
        /// <summary>
        /// AccessToken列表
        /// </summary>
        private static readonly Dictionary<string, AccessToken> accessTokenList
            = new Dictionary<string, AccessToken>();

        /// <summary>
        /// 截止日期列表
        /// </summary>
        private static readonly Dictionary<string, DateTime> expiresDateTimeList
            = new Dictionary<string, DateTime>();

        #region 根据微信公共平台账号获取AccessToken public AccessToken GetAccessToken(WXAccount account);
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public AccessToken GetAccessToken(WXAccount account)
        {
            lock (accessTokenLockSign)
            {
                return accessTokenList.ContainsKey(account.ID) ? accessTokenList[account.ID] : null;
            }
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
            lock (accessTokenLockSign)
            {
                accessTokenList[account.ID] = accessToken;
            }
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
            lock (accessTokenLockSign)
            {
                if (expiresDateTimeList.ContainsKey(account.ID)) return expiresDateTimeList[account.ID];
                throw WXException.GetInstance("没有该账号的截止日期信息", account.ID);
            }
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
            lock (accessTokenLockSign)
            {
                expiresDateTimeList[account.ID] = expires;
            }
        }
        #endregion
    }
}
