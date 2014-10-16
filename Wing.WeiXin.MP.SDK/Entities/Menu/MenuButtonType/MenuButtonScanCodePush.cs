namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 扫码推事件类型菜单按钮
    /// </summary>
    public class MenuButtonScanCodePush : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据扫码推事件类型菜单按钮 public MenuButtonScanCodePush()
        /// <summary>
        /// 实例化空数据扫码推事件类型菜单按钮
        /// </summary>
        public MenuButtonScanCodePush()
        {
            type = "scancode_push";
        } 
        #endregion
    }
}
