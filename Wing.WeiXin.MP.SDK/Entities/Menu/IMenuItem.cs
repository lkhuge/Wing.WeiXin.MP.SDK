namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// 菜单项目标志接口
    /// </summary>
    public interface IMenuItem : IMenu
    {
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        string name { get; set; }
    }
}
