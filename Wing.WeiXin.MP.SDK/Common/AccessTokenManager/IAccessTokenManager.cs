using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.AccessTokenManager
{
    /// <summary>
    /// AccessToken管理接口
    /// </summary>
    public interface IAccessTokenManager
    {
        #region 根据微信公共平台账号获取AccessToken AccessToken GetAccessToken(WXAccount account); 
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        AccessToken GetAccessToken(WXAccount account); 
        #endregion

        #region 根据微信公共平台账号设置AccessToken void SetAccessToken(WXAccount account, AccessToken accessToken);
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        void SetAccessToken(WXAccount account, AccessToken accessToken);
        #endregion

        #region 根据微信公共平台账号获取AccessToken的截止日期 DateTime GetExpiresDateTime(WXAccount account); 
        /// <summary>
        /// 根据微信公共平台账号获取AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken的截止日期</returns>
        DateTime GetExpiresDateTime(WXAccount account); 
        #endregion

        #region 根据微信公共平台账号设置AccessToken的截止日期 void SetExpiresDateTime(WXAccount account, DateTime expires);
        /// <summary>
        /// 根据微信公共平台账号设置AccessToken的截止日期
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="expires">AccessToken的截止日期</param>
        void SetExpiresDateTime(WXAccount account, DateTime expires);
        #endregion
    }
}
