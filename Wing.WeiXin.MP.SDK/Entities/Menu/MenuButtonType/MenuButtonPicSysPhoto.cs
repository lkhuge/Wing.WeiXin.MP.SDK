namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 弹出系统拍照发图类型菜单按钮
    /// </summary>
    public class MenuButtonPicSysPhoto : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据弹出系统拍照发图类型菜单按钮 public MenuButtonPicSysPhoto()
        /// <summary>
        /// 实例化空数据弹出系统拍照发图类型菜单按钮
        /// </summary>
        public MenuButtonPicSysPhoto()
        {
            type = "pic_sysphoto";
        } 
        #endregion
    }
}
