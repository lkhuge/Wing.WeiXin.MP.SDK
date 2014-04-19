using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities.OAuth;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Entities.User.User;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// URL管理类
    /// </summary>
    public static class URLManager
    {
        #region 获取获取AccessToken的URL public static string GetURLForGetAccessToken(string appID, string appSecret)
        /// <summary>
        /// 获取获取AccessToken的URL
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <returns>获取AccessToken的URL</returns>
        public static string GetURLForGetAccessToken(string appID, string appSecret)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetAccessToken,
                appID, appSecret);
        } 
        #endregion

        #region 获取发送客服消息的URL public static string GetURLForSendCSMessage(string weixinMPID)
        /// <summary>
        /// 获取发送客服消息的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>获取发送客服消息的URL</returns>
        public static string GetURLForSendCSMessage(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForSendCSMessage,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        }
        #endregion

        #region 获取上传多媒体的URL public static string GetURLForUploadMedia(string weixinMPID, UploadMediaType type)
        /// <summary>
        /// 获取上传多媒体的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="type">多媒体类型</param>
        /// <returns>获取上传多媒体的URL</returns>
        public static string GetURLForUploadMedia(string weixinMPID, UploadMediaType type)
        {
            return String.Format(
                Properties.Settings.Default.URLForUploadMedia,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token, 
                type);
        }
        #endregion

        #region 获取下载多媒体的URL public static string GetURLForDownloadMedia(string weixinMPID, string media_id)
        /// <summary>
        /// 获取下载多媒体的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="media_id">多媒体编号</param>
        /// <returns>获取上传多媒体的URL</returns>
        public static string GetURLForDownloadMedia(string weixinMPID, string media_id)
        {
            return String.Format(
                Properties.Settings.Default.URLForDownloadMedia,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token, 
                media_id);
        }
        #endregion

        #region 获取创建菜单的URL public static string GetURLForCreateMenu(string weixinMPID)
        /// <summary>
        /// 获取创建菜单的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>创建菜单的URL</returns>
        public static string GetURLForCreateMenu(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateMenu,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取获取菜单的URL public static string GetURLForGetMenu(string weixinMPID)
        /// <summary>
        /// 获取获取菜单的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>获取菜单的URL</returns>
        public static string GetURLForGetMenu(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetMenu,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取删除菜单的URL public static string GetURLForDeleteMenu(string weixinMPID)
        /// <summary>
        /// 获取删除菜单的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>删除菜单的URL</returns>
        public static string GetURLForDeleteMenu(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForDeleteMenu,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取取得Code的URL public static string GetURLForOAuthGetCode(string appID, string redirectURL, OAuthScope scope, string state)
        /// <summary>
        /// 获取取得Code的URL
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="redirectURL">授权后重定向的回调链接地址</param>
        /// <param name="scope">应用授权作用域</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <returns>取得Code的URL</returns>
        public static string GetURLForOAuthGetCode(string appID, string redirectURL, OAuthScope scope, string state)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthGetCode,
                appID, HttpUtility.UrlEncode(redirectURL), scope, state);
        } 
        #endregion

        #region 获取根据Code获取AccessToken的URL public static string GetURLForOAuthGetAccessToken(string appID, string appSecret, string code)
        /// <summary>
        /// 获取根据Code获取AccessToken的URL
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="code">Code值</param>
        /// <returns>根据Code获取AccessToken的URL</returns>
        public static string GetURLForOAuthGetAccessToken(string appID, string appSecret, string code)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthGetAccessToken,
                appID, appSecret, code);
        } 
        #endregion

        #region 获取根据RefreshToken刷新AccessToken的URL public static string GetURLForOAuthRefreshAccessToken(string appID, string refresh_token)
        /// <summary>
        /// 获取根据RefreshToken刷新AccessToken的URL
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="refresh_token">用户刷新AccessToken值</param>
        /// <returns>根据RefreshToken刷新AccessToken的URL</returns>
        public static string GetURLForOAuthRefreshAccessToken(string appID, string refresh_token)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthRefreshAccessToken,
                appID, refresh_token);
        } 
        #endregion

        #region 获取获取认证用户信息的URL public static string GetURLForOAuthGetUserInfo(OAuthAccessToken accessToken)
        /// <summary>
        /// 获取获取认证用户信息的URL
        /// </summary>
        /// <param name="accessToken">OAuth使用的AccessToken</param>
        /// <returns>获取认证用户信息的URL</returns>
        public static string GetURLForOAuthGetUserInfo(OAuthAccessToken accessToken)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthGetUserInfo,
                accessToken.access_token,
                accessToken.openid,
                Properties.Settings.Default.Language);
        } 
        #endregion

        #region 获取创建二维码ticket的URL public static string GetURLForCreateQRCodeTicket(string weixinMPID)
        /// <summary>
        /// 获取创建二维码ticket的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>创建二维码ticket的URL</returns>
        public static string GetURLForCreateQRCodeTicket(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateQRCodeTicket,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取通过ticket换取二维码的URL public static string GetURLForGetQRCode(QRCodeTicket qrCodeTicket)
        /// <summary>
        /// 获取通过ticket换取二维码的URL
        /// </summary>
        /// <param name="qrCodeTicket">二维码ticket</param>
        /// <returns>通过ticket换取二维码的URL</returns>
        public static string GetURLForGetQRCode(QRCodeTicket qrCodeTicket)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetQRCode,
                HttpUtility.UrlEncode(qrCodeTicket.ticket));
        } 
        #endregion

        #region 获取获取用户基本信息的URL public static string GetURLForGetWXUser(string weixinMPID, string openID)
        /// <summary>
        /// 获取获取用户基本信息的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="openID">普通用户的标识</param>
        /// <returns>获取用户基本信息的URL</returns>
        public static string GetURLForGetWXUser(string weixinMPID, string openID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUser,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token,
                openID,
                Properties.Settings.Default.Language);
        } 
        #endregion

        #region 获取获取用户列表的URL public static string GetURLForGetWXUserList(string weixinMPID)
        /// <summary>
        /// 获取获取用户列表的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>获取用户列表的URL</returns>
        public static string GetURLForGetWXUserList(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserList,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取获取后续用户列表的URL public static string GetURLForGetWXUserListNext(string weixinMPID, WXUserList userList)
        /// <summary>
        /// 获取获取后续用户列表的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="userList">用户列表</param>
        /// <returns>获取后续用户列表的URL</returns>
        public static string GetURLForGetWXUserListNext(string weixinMPID, WXUserList userList)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserListNext,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token,
                userList.next_openid);
        } 
        #endregion

        #region 获取添加组的URL public static string GetURLForCreateWXUserGroup(string weixinMPID)
        /// <summary>
        /// 获取添加组的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>添加组的URL</returns>
        public static string GetURLForCreateWXUserGroup(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateWXUserGroup,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取获取用户组列表的URL public static string GetURLForGetWXUserGroupList(string weixinMPID)
        /// <summary>
        /// 获取获取用户组列表的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>获取用户组列表的URL</returns>
        public static string GetURLForGetWXUserGroupList(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserGroupList,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取根据用户获取组的URL public static string GetURLForGetWXUserGroupByWXUser(string weixinMPID)
        /// <summary>
        /// 获取根据用户获取组的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>根据用户获取组的URL</returns>
        public static string GetURLForGetWXUserGroupByWXUser(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserGroupByWXUser,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取修改组名的URL public static string GetURLForModityWXUserGroup(string weixinMPID)
        /// <summary>
        /// 获取修改组名的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>修改组名的URL</returns>
        public static string GetURLForModityWXUserGroup(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForModityWXUserGroup,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion

        #region 获取移动用户分组的URL public static string GetURLForMoveWXUserGroup(string weixinMPID)
        /// <summary>
        /// 获取移动用户分组的URL
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <returns>移动用户分组的URL</returns>
        public static string GetURLForMoveWXUserGroup(string weixinMPID)
        {
            return String.Format(
                Properties.Settings.Default.URLForMoveWXUserGroup,
                AccessTokenContainer.GetAccessToken(weixinMPID).access_token);
        } 
        #endregion
    }
}
