using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 查询商品响应
    /// </summary>
    public class WXGoodsQueryResponse : ErrorMsg
    {
        /// <summary>
        /// 商品详情
        /// </summary>
        public WXGoods product_info { get; set; }
    }
}