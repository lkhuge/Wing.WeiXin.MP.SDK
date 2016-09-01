namespace Wing.WeiXin.MP.SDK.Entities.Card.Type
{
    /// <summary>
    /// 折扣券
    /// </summary>
    public class CardDiscount : CardTypeBaseInfo
    {
        /// <summary>
        /// 实例化折扣券
        /// </summary>
        public CardDiscount()
        {
            card_type = "DISCOUNT";
        }

        /// <summary>
        /// 折扣券详细信息
        /// </summary>
        public Detail discount { get; set; }

        /// <summary>
        /// 折扣券详细信息
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 基本的卡券数据
            /// </summary>
            public CardBaseInfo base_info { get; set; }

            /// <summary>
            /// 折扣券专用 表示打折额度（百分比） 填30就是七折
            /// </summary>
            public int discount { get; set; }
        }
    }
}
