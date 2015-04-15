using System;
using System.Collections.Generic;
using System.IO;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 菜单工具类
    /// </summary>
    public class MenuController : WXController
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
            return Action<ErrorMsg>(UrlCreateMenu, menu, account);
        } 
        #endregion

        #region 创建菜单 public ErrorMsg CreateMenu(WXAccount account, MenuForGet menu)
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="menu">菜单对象（适配查询目录）</param>
        /// <returns>错误码</returns>
        public ErrorMsg CreateMenu(WXAccount account, MenuForGet menu)
        {
            return CreateMenu(account, GetMenu(menu));
        }
        #endregion

        #region 获取菜单 public Menu GetMenu(WXAccount account)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>菜单</returns>
        public Menu GetMenu(WXAccount account)
        {
            return GetMenu(Action<MenuForGet>(UrlGetMenu, account));
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
            return Action<ErrorMsg>(UrlDeleteMenu, null, account);
        } 
        #endregion

        #region 将用于适配查询的目录对象转换为目录对象 private Menu GetMenu(MenuForGet menu)
        /// <summary>
        /// 将用于适配查询的目录对象转换为目录对象
        /// </summary>
        /// <param name="menu">目录对象（适配查询目录）</param>
        /// <returns>目录对象</returns>
        private Menu GetMenu(MenuForGet menu)
        {
            return new Menu { button = GetListMenuItem(menu.menu.button) };
        }
        #endregion

        #region 将用于适配查询目录的项目列表转换为项目列表 private List<AMenuItem> GetListMenuItem(List<MenuButtonForGet> listMenuForGet)
        /// <summary>
        /// 将用于适配查询目录的项目列表转换为项目列表
        /// </summary>
        /// <param name="listMenuForGet">用于适配查询目录的项目列表</param>
        /// <returns>项目列表</returns>
        private List<AMenuItem> GetListMenuItem(List<MenuButtonForGet> listMenuForGet)
        {
            List<AMenuItem> returnMenu = new List<AMenuItem>();
            foreach (MenuButtonForGet menuButton in listMenuForGet)
            {
                if ((menuButton.sub_button == null || menuButton.sub_button.Count == 0) && String.IsNullOrEmpty(menuButton.type))
                    throw WXException.GetInstance("子菜单不能不能为空", Settings.Default.SystemUsername);
                if (menuButton.sub_button != null && menuButton.sub_button.Count > 0)
                {
                    returnMenu.Add(new MenuList
                    {
                        name = menuButton.name,
                        sub_button = GetListMenuItem(menuButton.sub_button)
                    });
                    continue;
                }
                if (menuButton.type.Equals("view"))
                {
                    returnMenu.Add(new MenuButtonView { name = menuButton.name, url = menuButton.url });
                }
                if (menuButton.type.Equals("click"))
                {
                    returnMenu.Add(new MenuButtonClick { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("location_select"))
                {
                    returnMenu.Add(new MenuButtonLocationSelect { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("pic_photo_or_album"))
                {
                    returnMenu.Add(new MenuButtonPicPhotoOrAlbum { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("pic_sysphoto"))
                {
                    returnMenu.Add(new MenuButtonPicSysPhoto { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("pic_weixin"))
                {
                    returnMenu.Add(new MenuButtonPicWeixin { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("scancode_push"))
                {
                    returnMenu.Add(new MenuButtonScanCodePush { name = menuButton.name, key = menuButton.key });
                }
                if (menuButton.type.Equals("scancode_waitmsg"))
                {
                    returnMenu.Add(new MenuButtonScanCodeWaitMsg { name = menuButton.name, key = menuButton.key });
                }
            }

            return returnMenu;
        }
        #endregion
    }
}
