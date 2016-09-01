namespace Wing.WeiXin.MP.SDK.Entities.Card.Type
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public class CardGeneralCoupon : CardTypeBaseInfo
    {
        /// <summary>
        /// 实例化优惠券
        /// </summary>
        public CardGeneralCoupon()
        {
            card_type = "GENERAL_COUPON";
        }

        /// <summary>
        /// 优惠券详细信息
        /// </summary>
        public Detail general_coupon { get; set; }

        /// <summary>
        /// 优惠券详细信息
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 基本的卡券数据
            /// </summary>
            public CardBaseInfo base_info { get; set; }

            /// <summary>
            /// 优惠券专用 填写优惠详情
            /// </summary>
            public string default_detail { get; set; }
        }
    }
}
