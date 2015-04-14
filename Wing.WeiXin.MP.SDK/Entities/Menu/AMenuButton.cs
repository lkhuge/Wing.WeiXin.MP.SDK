
namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// 菜单按钮抽象类
    /// </summary>
    public abstract class AMenuButton : AMenuItem
    {
        /// <summary>
        /// 菜单的响应动作类型
        /// </summary>
        public string type { get; set; }
    }
}
