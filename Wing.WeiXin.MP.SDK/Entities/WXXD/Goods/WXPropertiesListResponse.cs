using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 微信小店属性列表响应
    /// </summary>
    public class WXPropertiesListResponse : ErrorMsg
    {
        /// <summary>
        /// 属性列表
        /// </summary>
        public List<WXProperties> properties { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public class WXProperties
        {
            /// <summary>
            /// 属性id
            /// </summary>
            public String id { get; set; }

            /// <summary>
            /// 属性名称
            /// </summary>
            public String name { get; set; }

            /// <summary>
            /// 属性值
            /// </summary>
            public List<WXPropertiesValue> property_value { get; set; }

            /// <summary>
            /// 属性值
            /// </summary>
            public class WXPropertiesValue
            {
                /// <summary>
                /// 属性值id
                /// </summary>
                public String id { get; set; }
            }
        }
    }
}