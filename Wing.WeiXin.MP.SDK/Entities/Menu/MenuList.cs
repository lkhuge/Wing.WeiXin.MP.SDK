using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.Menu
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public class MenuList : AMenuItem
    {
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        public List<AMenuItem> sub_button { get; set; }
    }
}
