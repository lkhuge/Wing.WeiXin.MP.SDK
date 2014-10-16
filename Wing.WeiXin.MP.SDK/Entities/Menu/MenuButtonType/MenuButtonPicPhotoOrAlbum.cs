namespace Wing.WeiXin.MP.SDK.Entities.Menu.MenuButtonType
{
    /// <summary>
    /// 弹出拍照或者相册发图类型菜单按钮
    /// </summary>
    public class MenuButtonPicPhotoOrAlbum : AMenuButton
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        #region 实例化空数据弹出拍照或者相册发图类型菜单按钮 public MenuButtonPicPhotoOrAlbum()
        /// <summary>
        /// 实例化空数据弹出拍照或者相册发图类型菜单按钮
        /// </summary>
        public MenuButtonPicPhotoOrAlbum()
        {
            type = "pic_photo_or_album";
        } 
        #endregion
    }
}
