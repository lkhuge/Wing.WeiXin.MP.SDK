using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.User.Group
{
    /// <summary>
    /// 微信用户组列表
    /// </summary>
    public class WXUserGroupList : IJSON
    {
        /// <summary>
        /// 公众平台分组信息列表
        /// </summary>
        public List<WXGroup> groups { get; set; }
    }
}
