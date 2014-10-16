using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Order
{
    /// <summary>
    /// 微信小店订单响应
    /// </summary>
    public class WXOrderResponse : ErrorMsg
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        public WXOrderResponseInfo order { get; set; }

        /// <summary>
        /// 订单详情
        /// </summary>
        public class WXOrderResponseInfo
        {
            /// <summary>
            /// 订单ID
            /// </summary>
            public String order_id { get; set; }

            /// <summary>
            /// 订单状态
            /// </summary>
            public int order_status { get; set; }

            /// <summary>
            /// 订单总价格(单位 : 分)
            /// </summary>
            public int order_total_price { get; set; }

            /// <summary>
            /// 订单创建时间
            /// </summary>
            public long order_create_time { get; set; }

            /// <summary>
            /// 订单运费价格(单位 : 分)
            /// </summary>
            public int order_express_price { get; set; }

            /// <summary>
            /// 买家微信OPENID
            /// </summary>
            public String buyer_openid { get; set; }

            /// <summary>
            /// 买家微信昵称
            /// </summary>
            public String buyer_nick { get; set; }

            /// <summary>
            /// 收货人姓名
            /// </summary>
            public String receiver_name { get; set; }

            /// <summary>
            /// 收货地址省份
            /// </summary>
            public String receiver_province { get; set; }

            /// <summary>
            /// 收货地址城市
            /// </summary>
            public String receiver_city { get; set; }

            /// <summary>
            /// 收货详细地址
            /// </summary>
            public String receiver_address { get; set; }

            /// <summary>
            /// 收货人移动电话
            /// </summary>
            public String receiver_mobile { get; set; }

            /// <summary>
            /// 收货人固定电话
            /// </summary>
            public String receiver_phone { get; set; }

            /// <summary>
            /// 商品ID
            /// </summary>
            public String product_id { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public String product_name { get; set; }

            /// <summary>
            /// 商品价格(单位 : 分)
            /// </summary>
            public int product_price { get; set; }

            /// <summary>
            /// 商品SKU
            /// </summary>
            public String product_sku { get; set; }

            /// <summary>
            /// 商品个数
            /// </summary>
            public int product_count { get; set; }

            /// <summary>
            /// 商品图片
            /// </summary>
            public String product_img { get; set; }

            /// <summary>
            /// 运单ID
            /// </summary>
            public String delivery_id { get; set; }

            /// <summary>
            /// 物流公司编码
            /// </summary>
            public String delivery_company { get; set; }

            /// <summary>
            /// 交易ID
            /// </summary>
            public String trans_id { get; set; }
        }
    }
}