namespace Wing.WeiXin.MP.SDK.Entities.Card.Type
{
    /// <summary>
    /// 代金券
    /// </summary>
    public class CardCash : CardTypeBaseInfo
    {
        /// <summary>
        /// 实例化代金券
        /// </summary>
        public CardCash()
        {
            card_type = "CASH";
        }

        /// <summary>
        /// 代金券详细信息
        /// </summary>
        public Detail cash { get; set; }

        /// <summary>
        /// 代金券详细信息
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 基本的卡券数据
            /// </summary>
            public CardBaseInfo base_info { get; set; }

            /// <summary>
            /// 代金券专用 表示起用金额（单位为分） 如果无起用门槛则填0
            /// </summary>
            public int least_cost { get; set; }

            /// <summary>
            /// 代金券专用 表示减免金额 （单位为分）
            /// </summary>
            public int reduce_cost { get; set; }
        }
    }
}
