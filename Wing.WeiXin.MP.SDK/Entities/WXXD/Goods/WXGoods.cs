using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.Goods
{
    /// <summary>
    /// 微信小店商品
    /// </summary>
    public class WXGoods
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public String product_id { get; set; }

        /// <summary>
        /// 基本属性
        /// </summary>
        public BaseAttr base_attr { get; set; }

        /// <summary>
        /// sku信息列表(可为多个)，每个sku信息串即为一个确定的商品，比如白色的37码的鞋子
        /// </summary>
        public List<SKUList> sku_list { get; set; }

        /// <summary>
        /// 商品其他属性
        /// </summary>
        public Attrext attrext { get; set; }

        /// <summary>
        /// 运费信息
        /// </summary>
        public DeliveryInfo delivery_info { get; set; }

        /// <summary>
        /// 基本属性
        /// </summary>
        public class BaseAttr
        {
            /// <summary>
            /// （必选）商品名称
            /// </summary>
            public String name { get; set; }

            /// <summary>
            /// （必选）商品分类id，商品分类列表请通过《获取指定分类的所有子分类》获取
            /// </summary>
            public String category { get; set; }

            /// <summary>
            /// （必选）商品主图
            /// (图片需调用图片上传接口获得图片Url填写至此，否则无法添加商品。图片分辨率推荐尺寸为640×600)
            /// </summary>
            public String main_img { get; set; }

            /// <summary>
            /// （必选）商品图片列表
            /// (图片需调用图片上传接口获得图片Url填写至此，否则无法添加商品。图片分辨率推荐尺寸为640×600)
            /// </summary>
            public List<String> img { get; set; }

            /// <summary>
            /// （必选）商品详情列表，显示在客户端的商品详情页内
            /// </summary>
            public List<BaseAttrDetail> detail { get; set; }

            /// <summary>
            /// 商品属性列表，属性列表请通过《获取指定分类的所有属性》获取
            /// </summary>
            public List<BaseAttrProperty> property { get; set; }

            /// <summary>
            /// 商品sku定义，SKU列表请通过《获取指定子分类的所有SKU》获取
            /// </summary>
            public List<BaseAttrSKUInfo> sku_info { get; set; }

            /// <summary>
            /// 用户商品限购数量
            /// </summary>
            public String buy_limit { get; set; }

            /// <summary>
            /// 商品详情
            /// </summary>
            public class BaseAttrDetail
            {
                /// <summary>
                /// 文字描述
                /// </summary>
                public String text { get; set; }

                /// <summary>
                /// 图片
                /// (图片需调用图片上传接口获得图片Url填写至此，否则无法添加商品)
                /// </summary>
                public String img { get; set; }
            }

            /// <summary>
            /// 商品属性
            /// </summary>
            public class BaseAttrProperty
            {
                /// <summary>
                /// 属性id
                /// </summary>
                public String id { get; set; }

                /// <summary>
                /// 属性值id
                /// </summary>
                public String vid { get; set; }
            }

            /// <summary>
            /// 商品sku
            /// </summary>
            public class BaseAttrSKUInfo
            {
                /// <summary>
                /// sku属性
                /// (SKU列表中id, 支持自定义SKU，格式为"$xxx"，xxx即为显示在客户端中的字符串)
                /// </summary>
                public String id { get; set; }

                /// <summary>
                /// sku值
                /// (SKU列表中vid, 如需自定义SKU，格式为"$xxx"，xxx即为显示在客户端中的字符串)
                /// </summary>
                public String vid { get; set; }
            }
        }

        /// <summary>
        /// sku信息
        /// </summary>
        public class SKUList
        {
            /// <summary>
            /// sku信息, 参照上述sku_table的定义; 
            /// 格式 : "id1:vid1;id2:vid2"
            /// 规则 : id_info的组合个数必须与sku_table个数一致
            /// (若商品无sku信息, 即商品为统一规格，则此处赋值为空字符串即可)
            /// </summary>
            public String sku_id { get; set; }

            /// <summary>
            /// sku原价(单位 : 分)
            /// </summary>
            public int ori_price { get; set; }

            /// <summary>
            /// sku微信价(单位 : 分, 微信价必须比原价小, 否则添加商品失败)
            /// </summary>
            public int price { get; set; }

            /// <summary>
            /// sku iconurl(图片需调用图片上传接口获得图片Url)
            /// </summary>
            public String icon_url { get; set; }

            /// <summary>
            /// sku库存
            /// </summary>
            public int quantity { get; set; }

            /// <summary>
            /// 商家商品编码
            /// </summary>
            public String product_code { get; set; }
        }

        /// <summary>
        /// 商品其他属性
        /// </summary>
        public class Attrext
        {
            /// <summary>
            /// 是否包邮
            /// (0-否, 1-是), 如果包邮delivery_info字段可省略
            /// </summary>
            public int isPostFree { get; set; }

            /// <summary>
            /// 是否提供发票
            /// (0-否, 1-是)
            /// </summary>
            public int isHasReceipt { get; set; }

            /// <summary>
            /// 是否保修
            /// (0-否, 1-是)
            /// </summary>
            public int isUnderGuaranty { get; set; }

            /// <summary>
            /// 是否支持退换货
            /// (0-否, 1-是)
            /// </summary>
            public int isSupportReplace { get; set; }

            /// <summary>
            /// 商品所在地地址
            /// </summary>
            public AttrextLocation location { get; set; }

            /// <summary>
            /// 商品所在地地址
            /// </summary>
            public class AttrextLocation
            {
                /// <summary>
                /// 国家
                /// (详见《地区列表》说明)
                /// </summary>
                public String country { get; set; }

                /// <summary>
                /// 省份
                /// (详见《地区列表》说明)
                /// </summary>
                public String province { get; set; }

                /// <summary>
                /// 城市
                /// (详见《地区列表》说明)
                /// </summary>
                public String city { get; set; }

                /// <summary>
                /// 地址
                /// </summary>
                public String address { get; set; }
            }
        }

        /// <summary>
        /// 运费信息
        /// </summary>
        public class DeliveryInfo
        {
            /// <summary>
            /// 运费类型
            /// (
            ///     0-使用下面express字段的默认模板, 
            ///     1-使用template_id代表的邮费模板,详见邮费模板相关API
            /// )
            /// </summary>
            public int delivery_type { get; set; }

            /// <summary>
            /// 邮费模板ID
            /// </summary>
            public int template_id { get; set; }

            /// <summary>
            /// 默认模板
            /// </summary>
            public List<DeliveryInfoExpress> express { get; set; }

            /// <summary>
            /// 默认模板
            /// </summary>
            public class DeliveryInfoExpress
            {
                /// <summary>
                /// 快递ID
                /// </summary>
                public int id { get; set; }

                /// <summary>
                /// 运费(单位 : 分)
                /// </summary>
                public int price { get; set; }
            }
        }
    }
}