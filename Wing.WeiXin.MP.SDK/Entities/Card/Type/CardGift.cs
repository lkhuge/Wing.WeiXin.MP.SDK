namespace Wing.WeiXin.MP.SDK.Entities.Card.Type
{
    /// <summary>
    /// 礼品券
    /// </summary>
    public class CardGift : CardTypeBaseInfo
    {
        /// <summary>
        /// 实例化折扣券
        /// </summary>
        public CardGift()
        {
            card_type = "GIFT";
        }

        /// <summary>
        /// 礼品券详细信息
        /// </summary>
        public Detail gift { get; set; }

        /// <summary>
        /// 礼品券详细信息
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 基本的卡券数据
            /// </summary>
            public CardBaseInfo base_info { get; set; }

            /// <summary>
            /// 礼品券专用 填写礼品的名称
            /// </summary>
            public string gift { get; set; }
        }
    }
}
