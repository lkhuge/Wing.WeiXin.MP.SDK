namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 弹出微信相册发图器类型菜单按钮
    /// </summary>
    public class MenuButtonPicWeixin : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据弹出微信相册发图器类型菜单按钮 public MenuButtonPicWeixin()
        /// <summary>
        /// 实例化空数据弹出微信相册发图器类型菜单按钮
        /// </summary>
        public MenuButtonPicWeixin()
        {
            type = "pic_weixin";
        } 
        #endregion
    }
}
