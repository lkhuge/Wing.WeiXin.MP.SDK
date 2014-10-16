using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Group
{
    /// <summary>
    /// 微信小店分组
    /// </summary>
    public class WXGoodsGroup
    {
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