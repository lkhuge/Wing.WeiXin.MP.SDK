using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Group
{
    /// <summary>
    /// 微信小店分组信息
    /// </summary>
    public class WXGoodsGroupInfo
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public WXGoodsGroupByIDItem group_detail { get; set; }

        /// <summary>
        /// 分组信息
        /// </summary>
        public class WXGoodsGroupByIDItem
        {
            /// <summary>
            /// 分组ID
            /// </summary>
            public int group_id { get; set; }

            /// <summary>
            /// 分组名称
            /// </summary>
            public String group_name { get; set; }

            /// <summary>
            /// 商品ID集合
            /// </summary>
            public List<String> product_list { get; set; }
        }
    }
}