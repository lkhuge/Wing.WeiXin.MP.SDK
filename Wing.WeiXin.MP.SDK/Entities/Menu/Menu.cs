using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 一级菜单数组，个数应为1~3个
        /// </summary>
        public List<AMenuItem> button { get; set; }
    }
}
