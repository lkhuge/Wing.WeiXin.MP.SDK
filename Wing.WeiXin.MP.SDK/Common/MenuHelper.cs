using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 目录对象工具类（适配查询目录）
    /// </summary>
    public static class MenuHelper
    {
        #region 将用于适配查询的目录对象转换为目录对象 public static Menu GetMenu(MenuForGet menu)
        /// <summary>
        /// 将用于适配查询的目录对象转换为目录对象
        /// </summary>
        /// <param name="menu">目录对象（适配查询目录）</param>
        /// <returns>目录对象</returns>
        public static Menu GetMenu(MenuForGet menu)
        {
            return new Menu { button = GetListMenuItem(menu.menu.button) };
        } 
        #endregion

        #region 将用于适配查询目录的项目列表转换为项目列表 private static List<AMenuItem> GetListMenuItem(List<MenuButtonForGet> listMenuForGet)
        /// <summary>
        /// 将用于适配查询目录的项目列表转换为项目列表
        /// </summary>
        /// <param name="listMenuForGet">用于适配查询目录的项目列表</param>
        /// <returns>项目列表</returns>
        private static List<AMenuItem> GetListMenuItem(List<MenuButtonForGet> listMenuForGet)
        {
            List<AMenuItem> returnMenu = new List<AMenuItem>();
            foreach (MenuButtonForGet menuButton in listMenuForGet)
            {
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
            }

            return returnMenu;
        } 
        #endregion
    }
}
