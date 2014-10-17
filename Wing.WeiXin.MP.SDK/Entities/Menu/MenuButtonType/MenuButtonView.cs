﻿namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// View类型菜单按钮
    /// </summary>
    public class MenuButtonView : AMenuButton
    {
        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }

        #region 实例化空数据View类型菜单按钮 public MenuButtonView()
        /// <summary>
        /// 实例化空数据View类型菜单按钮
        /// </summary>
        public MenuButtonView()
        {
            type = "view";
        } 
        #endregion
    }
}