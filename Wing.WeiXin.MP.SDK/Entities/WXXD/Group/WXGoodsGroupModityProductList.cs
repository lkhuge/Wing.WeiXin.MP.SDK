using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Group
{
    /// <summary>
    /// 微信小店分组商品列表
    /// </summary>
    public class WXGoodsGroupModityProductList
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int group_id { get; set; }

        /// <summary>
        /// 分组的商品集合
        /// </summary>
        public List<WXGoodsGroupModityProduct> product { get; set; }

        /// <summary>
        /// 微信小店分组商品
        /// </summary>
        public class WXGoodsGroupModityProduct
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public String product_id { get; set; }

            /// <summary>
            /// 修改操作
            /// (0-删除, 1-增加)
            /// </summary>
            public int mod_action { get; set; }
        }
    }
}