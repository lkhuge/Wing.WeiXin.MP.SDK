using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Group
{
    /// <summary>
    /// 微信小店分组全部列表
    /// </summary>
    public class WXGoodsGroupAllList : ErrorMsg
    {
        /// <summary>
        /// 分组集合
        /// </summary>
        public List<WXGoodsGroupAllListItem> groups_detail { get; set; }

        /// <summary>
        /// 微信小店分组
        /// </summary>
        public class WXGoodsGroupAllListItem
        {
            /// <summary>
            /// 分组ID
            /// </summary>
            public int group_id { get; set; }

            /// <summary>
            /// 分组名称
            /// </summary>
            public String group_name { get; set; }
        }
    }
}