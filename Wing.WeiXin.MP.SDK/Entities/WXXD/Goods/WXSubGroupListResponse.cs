using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 微信小店分类列表响应
    /// </summary>
    public class WXSubGroupListResponse : ErrorMsg
    {
        /// <summary>
        /// 子分类列表
        /// </summary>
        public List<WXSubGroup> cate_list { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public class WXSubGroup
        {
            /// <summary>
            /// 子分类ID
            /// </summary>
            public String id { get; set; }

            /// <summary>
            /// 子分类名称
            /// </summary>
            public String name { get; set; }
        }
    }
}