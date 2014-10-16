using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.GoodsShelf
{
    /// <summary>
    /// 微信小店货架响应
    /// </summary>
    public class WXGoodsShelfResponse : ErrorMsg
    {
        /// <summary>
        /// 货架ID
        /// </summary>
        public int shelf_id { get; set; }
    }
}