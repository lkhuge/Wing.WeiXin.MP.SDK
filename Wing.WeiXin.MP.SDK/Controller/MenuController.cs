using System;
using System.IO;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 菜单工具类
    /// </summary>
    public class MenuController
    {
        /// <summary>
        /// 创建菜单的URL
        /// </summary>
        private const string UrlCreateMenu = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";

        /// <summary>
        /// 获取菜单的URL
        /// </summary>
        private const string UrlGetMenu = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";

        /// <summary>
        /// 删除菜单的URL
        /// </summary>
        private const string UrlDeleteMenu = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";

        #region 创建菜单 public ErrorMsg CreateMenu(WXAccount account, Menu menu)
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>错误码</returns>
        public ErrorMsg CreateMenu(WXAccount account, Menu menu)
        {
            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(
                LibManager.HTTPHelper.Post(String.Format(
                    UrlCreateMenu,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token), LibManager.JSONHelper.JSONSerialize(menu)));
        } 
        #endregion

        #region 获取菜单 public MenuForGet GetMenu(WXAccount account)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>菜单</returns>
        public MenuForGet GetMenu(WXAccount account)
        {
            string result = LibManager.HTTPHelper.Get(String.Format(
                    UrlGetMenu,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw WXException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result), account.ID);
            }

            return LibManager.JSONHelper.JSONDeserialize<MenuForGet>(result);
        } 
        #endregion

        #region 删除菜单 public ErrorMsg DeleteMenu(WXAccount account)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>错误码</returns>
        public ErrorMsg DeleteMenu(WXAccount account)
        {
            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(LibManager.HTTPHelper.Get(String.Format(
                UrlDeleteMenu,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token)));
        } 
        #endregion
    }
}
