namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// 目录项目抽象类
    /// </summary>
    public class AMenuItem : IMenuItem
    {
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }
    }
}
