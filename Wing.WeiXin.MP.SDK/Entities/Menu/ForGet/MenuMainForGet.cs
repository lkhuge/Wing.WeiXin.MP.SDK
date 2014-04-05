using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.Menu.ForGet
{
    /// <summary>
    /// 目录主要内容（适配查询目录）
    /// </summary>
    public class MenuMainForGet : IMenu
    {
        /// <summary>
        /// 一级菜单数组，个数应为1~3个
        /// </summary>
        public List<MenuButtonForGet> button { get; set; }
    }
}
