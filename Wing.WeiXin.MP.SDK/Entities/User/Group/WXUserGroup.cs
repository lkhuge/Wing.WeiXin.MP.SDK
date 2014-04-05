using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.Interface;

namespace Wing.WeiXin.MP.SDK.Entities.User.Group
{
    /// <summary>
    /// 微信用户组
    /// </summary>
    public class WXUserGroup : IJSON
    {
        /// <summary>
        /// 微信用户组
        /// </summary>
        public WXGroup group { get; set; }
    }
}
