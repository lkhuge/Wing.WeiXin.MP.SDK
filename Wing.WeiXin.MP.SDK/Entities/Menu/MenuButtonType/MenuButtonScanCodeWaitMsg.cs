namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框类型菜单按钮
    /// </summary>
    public class MenuButtonScanCodeWaitMsg : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据扫码推事件且弹出“消息接收中”提示框类型菜单按钮 public MenuButtonScanCodeWaitMsg()
        /// <summary>
        /// 实例化空数据扫码推事件且弹出“消息接收中”提示框类型菜单按钮
        /// </summary>
        public MenuButtonScanCodeWaitMsg()
        {
            type = "scancode_waitmsg";
        } 
        #endregion
    }
}
