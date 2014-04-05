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
        #region 获取获取AccessToken的URL public static string GetURLForGetAccessToken()
        /// <summary>
        /// 获取获取AccessToken的URL
        /// </summary>
        /// <returns>获取AccessToken的URL</returns>
        public static string GetURLForGetAccessToken()
        {
            return String.Format(
                Properties.Settings.Default.URLForGetAccessToken,
                ConfigManager.GetAppID(), 
                ConfigManager.GetAppSecret());
        } 
        #endregion

        #region 获取发送客服消息的URL public static string GetURLForSendCSMessage()
        /// <summary>
        /// 获取发送客服消息的URL
        /// </summary>
        /// <returns>获取发送客服消息的URL</returns>
        public static string GetURLForSendCSMessage()
        {
            return String.Format(
                Properties.Settings.Default.URLForSendCSMessage,
                AccessTokenContainer.GetAccessToken().access_token);
        }
        #endregion

        #region 获取上传多媒体的URL public static string GetURLForUploadMedia(UploadMediaType type)
        /// <summary>
        /// 获取上传多媒体的URL
        /// </summary>
        /// <param name="type">多媒体类型</param>
        /// <returns>获取上传多媒体的URL</returns>
        public static string GetURLForUploadMedia(UploadMediaType type)
        {
            return String.Format(
                Properties.Settings.Default.URLForUploadMedia,
                AccessTokenContainer.GetAccessToken().access_token, 
                type);
        }
        #endregion

        #region 获取下载多媒体的URL public static string GetURLForDownloadMedia(string media_id)
        /// <summary>
        /// 获取下载多媒体的URL
        /// </summary>
        /// <param name="media_id">多媒体编号</param>
        /// <returns>获取上传多媒体的URL</returns>
        public static string GetURLForDownloadMedia(string media_id)
        {
            return String.Format(
                Properties.Settings.Default.URLForDownloadMedia,
                AccessTokenContainer.GetAccessToken().access_token, 
                media_id);
        }
        #endregion

        #region 获取创建菜单的URL public static string GetURLForCreateMenu()
        /// <summary>
        /// 获取创建菜单的URL
        /// </summary>
        /// <returns>创建菜单的URL</returns>
        public static string GetURLForCreateMenu()
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateMenu,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取获取菜单的URL public static string GetURLForGetMenu()
        /// <summary>
        /// 获取获取菜单的URL
        /// </summary>
        /// <returns>获取菜单的URL</returns>
        public static string GetURLForGetMenu()
        {
            return String.Format(
                Properties.Settings.Default.URLForGetMenu,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取删除菜单的URL public static string GetURLForDeleteMenu()
        /// <summary>
        /// 获取删除菜单的URL
        /// </summary>
        /// <returns>删除菜单的URL</returns>
        public static string GetURLForDeleteMenu()
        {
            return String.Format(
                Properties.Settings.Default.URLForDeleteMenu,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取取得Code的URL public static string GetURLForOAuthGetCode(string redirectURL, OAuthScope scope, string state)
        /// <summary>
        /// 获取取得Code的URL
        /// </summary>
        /// <param name="redirectURL">授权后重定向的回调链接地址</param>
        /// <param name="scope">应用授权作用域</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值</param>
        /// <returns>取得Code的URL</returns>
        public static string GetURLForOAuthGetCode(string redirectURL, OAuthScope scope, string state)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthGetCode,
                ConfigManager.BaseConfig.AppID, 
                HttpUtility.UrlEncode(redirectURL), 
                scope, 
                state);
        } 
        #endregion

        #region 获取根据Code获取AccessToken的URL public static string GetURLForOAuthGetAccessToken(string code)
        /// <summary>
        /// 获取根据Code获取AccessToken的URL
        /// </summary>
        /// <param name="code">Code值</param>
        /// <returns>根据Code获取AccessToken的URL</returns>
        public static string GetURLForOAuthGetAccessToken(string code)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthGetAccessToken,
                ConfigManager.BaseConfig.AppID,
                ConfigManager.BaseConfig.AppSecret,
                code);
        } 
        #endregion

        #region 获取根据RefreshToken刷新AccessToken的URL public static string GetURLForOAuthRefreshAccessToken(string refresh_token)
        /// <summary>
        /// 获取根据RefreshToken刷新AccessToken的URL
        /// </summary>
        /// <param name="refresh_token">用户刷新AccessToken值</param>
        /// <returns>根据RefreshToken刷新AccessToken的URL</returns>
        public static string GetURLForOAuthRefreshAccessToken(string refresh_token)
        {
            return String.Format(
                Properties.Settings.Default.URLForOAuthRefreshAccessToken,
                ConfigManager.BaseConfig.AppID,
                refresh_token);
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

        #region 获取创建二维码ticket的URL public static string GetURLForCreateQRCodeTicket()
        /// <summary>
        /// 获取创建二维码ticket的URL
        /// </summary>
        /// <returns>创建二维码ticket的URL</returns>
        public static string GetURLForCreateQRCodeTicket()
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateQRCodeTicket,
                AccessTokenContainer.GetAccessToken().access_token);
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

        #region 获取获取用户基本信息的URL public static string GetURLForGetWXUser(string openID)
        /// <summary>
        /// 获取获取用户基本信息的URL
        /// </summary>
        /// <param name="openID">普通用户的标识</param>
        /// <returns>获取用户基本信息的URL</returns>
        public static string GetURLForGetWXUser(string openID)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUser,
                AccessTokenContainer.GetAccessToken().access_token,
                openID,
                Properties.Settings.Default.Language);
        } 
        #endregion

        #region 获取获取用户列表的URL public static string GetURLForGetWXUserList()
        /// <summary>
        /// 获取获取用户列表的URL
        /// </summary>
        /// <returns>获取用户列表的URL</returns>
        public static string GetURLForGetWXUserList()
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserList,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取获取后续用户列表的URL public static string GetURLForGetWXUserListNext(WXUserList userList)
        /// <summary>
        /// 获取获取后续用户列表的URL
        /// </summary>
        /// <param name="userList">用户列表</param>
        /// <returns>获取后续用户列表的URL</returns>
        public static string GetURLForGetWXUserListNext(WXUserList userList)
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserListNext,
                AccessTokenContainer.GetAccessToken().access_token,
                userList.next_openid);
        } 
        #endregion

        #region 获取添加组的URL public static string GetURLForCreateWXUserGroup()
        /// <summary>
        /// 获取添加组的URL
        /// </summary>
        /// <returns>添加组的URL</returns>
        public static string GetURLForCreateWXUserGroup()
        {
            return String.Format(
                Properties.Settings.Default.URLForCreateWXUserGroup,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取获取用户组列表的URL public static string GetURLForGetWXUserGroupList()
        /// <summary>
        /// 获取获取用户组列表的URL
        /// </summary>
        /// <returns>获取用户组列表的URL</returns>
        public static string GetURLForGetWXUserGroupList()
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserGroupList,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取根据用户获取组的URL public static string GetURLForGetWXUserGroupByWXUser()
        /// <summary>
        /// 获取根据用户获取组的URL
        /// </summary>
        /// <returns>根据用户获取组的URL</returns>
        public static string GetURLForGetWXUserGroupByWXUser()
        {
            return String.Format(
                Properties.Settings.Default.URLForGetWXUserGroupByWXUser,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取修改组名的URL public static string GetURLForModityWXUserGroup()
        /// <summary>
        /// 获取修改组名的URL
        /// </summary>
        /// <returns>修改组名的URL</returns>
        public static string GetURLForModityWXUserGroup()
        {
            return String.Format(
                Properties.Settings.Default.URLForModityWXUserGroup,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion

        #region 获取移动用户分组的URL public static string GetURLForMoveWXUserGroup()
        /// <summary>
        /// 获取移动用户分组的URL
        /// </summary>
        /// <returns>移动用户分组的URL</returns>
        public static string GetURLForMoveWXUserGroup()
        {
            return String.Format(
                Properties.Settings.Default.URLForMoveWXUserGroup,
                AccessTokenContainer.GetAccessToken().access_token);
        } 
        #endregion
    }
}
