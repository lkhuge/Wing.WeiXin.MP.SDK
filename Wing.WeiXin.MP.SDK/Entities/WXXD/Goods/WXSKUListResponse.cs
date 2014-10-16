using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 微信小店SKU列表响应
    /// </summary>
    public class WXSKUListResponse : ErrorMsg
    {
        /// <summary>
        /// sku列表
        /// </summary>
        public List<WXSKU> sku_table { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public class WXSKU
        {
            /// <summary>
            /// sku id
            /// </summary>
            public String id { get; set; }

            /// <summary>
            /// sku 名称
            /// </summary>
            public String name { get; set; }

            /// <summary>
            /// sku vid列表
            /// </summary>
            public List<WXSKUValue> value_list { get; set; }

            /// <summary>
            /// SKU值
            /// </summary>
            public class WXSKUValue
            {
                /// <summary>
                /// vid
                /// </summary>
                public String id { get; set; }

                /// <summary>
                /// vid名称
                /// </summary>
                public String name { get; set; }
            }
        }
    }
}