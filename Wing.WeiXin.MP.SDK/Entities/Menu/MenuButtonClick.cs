namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// Click类型菜单按钮
    /// </summary>
    public class MenuButtonClick : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据Click类型菜单按钮 public MenuButtonClick()
        /// <summary>
        /// 实例化空数据Click类型菜单按钮
        /// </summary>
        public MenuButtonClick()
        {
            type = "click";
        } 
        #endregion
    }
}
