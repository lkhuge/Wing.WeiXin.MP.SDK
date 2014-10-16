namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 弹出地理位置选择器类型菜单按钮
    /// </summary>
    public class MenuButtonLocationSelect : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据弹出地理位置选择器类型菜单按钮 public MenuButtonLocationSelect()
        /// <summary>
        /// 实例化空数据弹出地理位置选择器类型菜单按钮
        /// </summary>
        public MenuButtonLocationSelect()
        {
            type = "location_select";
        } 
        #endregion
    }
}
