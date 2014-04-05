using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.Interface;

namespace Wing.WeiXin.MP.SDK.Entities.User.Group
{
    /// <summary>
    /// 组
    /// </summary>
    public class WXGroup : IJSON
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 分组内用户数量
        /// </summary>
        public int count { get; set; }
    }
}
