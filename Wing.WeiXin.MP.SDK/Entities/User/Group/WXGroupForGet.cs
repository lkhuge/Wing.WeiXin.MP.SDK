using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.User.Group
{
    /// <summary>
    /// 适配获取分组
    /// </summary>
    public class WXGroupForGet
    {
        /// <summary>
        /// 用户所属的groupid
        /// </summary>
        public int groupid { get; set; }
    }
}
