using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

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

        /// <summary>
        /// AccessToken管理类
        /// </summary>
        private readonly IAccessTokenManager accessTokenManager;

        #region 根据AccessToken管理类实例化 public AccessTokenContainer(IAccessTokenManager accessTokenManager)
        /// <summary>
        /// 根据AccessToken管理类实例化
        /// </summary>
        /// <param name="accessTokenManager">AccessToken管理类</param>
        public AccessTokenContainer(IAccessTokenManager accessTokenManager)
        {
            if (accessTokenManager == null) throw new ArgumentNullException("accessTokenManager");
            this.accessTokenManager = accessTokenManager;
        } 
        #endregion

        #region 获取AccessToken public AccessToken GetAccessToken(WXAccount account)
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>AccessToken</returns>
        public AccessToken GetAccessToken(WXAccount account)
        {
            AccessToken accessToken = accessTokenManager.GetAccessToken(account);
            if (accessToken != null
                && DateTime.Now < accessTokenManager.GetExpiresDateTime(account)) return accessToken;

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
            string result = LibManager.HTTPHelper.Get(String.Format(Url, account.AppID, account.AppSecret));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }
            AccessToken accessTokenNew = LibManager.JSONHelper.JSONDeserialize<AccessToken>(result);
            accessTokenManager.SetAccessToken(account, accessTokenNew);
            accessTokenManager.SetExpiresDateTime(account, DateTime.Now + new TimeSpan(0, 0, accessTokenNew.expires_in));

            return accessTokenNew;
        } 
        #endregion
    }
}
