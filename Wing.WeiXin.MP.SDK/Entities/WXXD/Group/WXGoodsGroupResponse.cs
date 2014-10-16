using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Group
{
    /// <summary>
    /// 微信小店分组响应
    /// </summary>
    public class WXGoodsGroupResponse : ErrorMsg
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }
    }
}