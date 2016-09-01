using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.Card
{
    /// <summary>
    /// 卡券基础信息
    /// </summary>
    public class CardBaseInfo
    {
        /// <summary>
        /// 卡券的商户logo 建议像素为300*300
        /// </summary>
        public string logo_url { get; set; }

        /// <summary>
        /// Code展示类型
        ///     "CODE_TYPE_TEXT"，文本
        ///     "CODE_TYPE_BARCODE"，一维码
        ///     "CODE_TYPE_QRCODE"，二维码
        ///     "CODE_TYPE_ONLY_QRCODE",二维码无code显示
        ///     "CODE_TYPE_ONLY_BARCODE",一维码无code显示
        /// </summary>
        public string code_type { get; set; }

        /// <summary>
        /// 商户名字
        /// 字数上限为12个汉字
        /// </summary>
        public string brand_name { get; set; }

        /// <summary>
        /// 卡券名
        /// 字数上限为9个汉字 (建议涵盖卡券属性、服务及金额)
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 券名
        /// 字数上限为18个汉字
        /// </summary>
        public string sub_title { get; set; }

        /// <summary>
        /// 券颜色
        /// 按色彩规范标注填写Color010-Color100
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 卡券使用提醒
        /// 字数上限为16个汉字
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 卡券使用说明
        /// 字数上限为1024个汉字
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public SKU sku { get; set; }

        /// <summary>
        /// 使用日期
        /// 有效期的信息
        /// </summary>
        public DataInfo date_info { get; set; }

        /// <summary>
        /// type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天内有效，不支持填写0。
        /// </summary>
        public int fixed_term { get; set; }

        /// <summary>
        /// type为DATE_TYPE_FIX_TERM时专用，
        /// 表示自领取后多少天开始生效，领取后当天生效填写0。（单位为天）
        /// </summary>
        public int fixed_begin_term { get; set; }

        /// <summary>
        /// 是否自定义Code码
        /// 填写true或false，默认为false
        /// 通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码
        /// </summary>
        public bool use_custom_code { get; set; }

        /// <summary>
        /// 是否指定用户领取
        /// 填写true或false。默认为false
        /// 通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取
        /// </summary>
        public bool bind_openid { get; set; }

        /// <summary>
        /// 客服电话
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 门店位置poiid
        /// 调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填
        /// </summary>
        public List<int> location_id_list { get; set; }

        /// <summary>
        /// 第三方来源名，例如同程旅游、大众点评
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 自定义跳转外链的入口名字
        /// </summary>
        public string custom_url_name { get; set; }

        /// <summary>
        /// 自定义跳转的URL
        /// </summary>  
        public string custom_url { get; set; }

        /// <summary>
        /// 显示在入口右侧的提示语
        /// </summary>
        public string custom_url_sub_title { get; set; }

        /// <summary>
        /// 营销场景的自定义入口名称
        /// </summary>
        public string promotion_url_name { get; set; }

        /// <summary>
        /// 入口跳转外链的地址链接
        /// </summary>
        public string promotion_url { get; set; }

        /// <summary>
        /// 显示在营销入口右侧的提示语
        /// </summary>
        public string promotion_url_sub_title { get; set; }

        /// <summary>
        /// 每人可领券的数量限制
        /// 不填写默认为50
        /// </summary>
        public int get_limit { get; set; }

        /// <summary>
        /// 卡券领取页面是否可分享
        /// </summary>
        public bool can_share { get; set; }

        /// <summary>
        /// 卡券是否可转赠
        /// </summary>
        public bool can_give_friend { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public class SKU
        {
            /// <summary>
            /// 卡券库存的数量
            /// 上限为100000000
            /// </summary>
            public int quantity { get; set; }
        }

        /// <summary>
        /// 使用日期 有效期的信息
        /// </summary>
        public class DataInfo
        {
            /// <summary>
            /// DATE_TYPE_FIX_TIME_RANGE 表示固定日期区间，
            /// DATE_TYPE_FIX_TERM表示固定时长（自领取后按天算)
            /// 使用时间的类型，旧文档采用的1和2依然生效。
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// type为DATE_TYPE_FIX_TIME_RANGE时专用，
            /// 表示起用时间。从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入
            /// （东八区时间，单位为秒）
            /// </summary>
            public long begin_timestamp { get; set; }

            /// <summary>
            /// type为DATE_TYPE_FIX_TIME_RANGE时专用，
            /// 表示结束时间，建议设置为截止日期的23:59:59过期。（东八区时间，单位为秒）
            /// </summary>
            public long end_timestamp { get; set; }
        }
    }
}
