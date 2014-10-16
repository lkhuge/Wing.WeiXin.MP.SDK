using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.GoodsShelf
{
    /// <summary>
    /// 微信小店获取全部货架响应
    /// </summary>
    public class WXGoodsShelfAllListResponse : ErrorMsg
    {
        /// <summary>
        /// 所有货架集合
        /// </summary>
        public List<WXGoodsShelf> shelves { get; set; }
    }
}