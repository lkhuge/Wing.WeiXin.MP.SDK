using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 微信小店货物响应
    /// </summary>
    public class WXGoodsResponse : ErrorMsg
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public String product_id { get; set; }
    }
}