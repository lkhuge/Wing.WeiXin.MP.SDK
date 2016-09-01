namespace Wing.WeiXin.MP.SDK.Entities.Card.Type
{
    /// <summary>
    /// 团购券
    /// </summary>
    public class CardGroupon : CardTypeBaseInfo
    {
        /// <summary>
        /// 实例化团购券
        /// </summary>
        public CardGroupon()
        {
            card_type = "GROUPON";
        }

        /// <summary>
        /// 团购券详细信息
        /// </summary>
        public Detail groupon { get; set; }

        /// <summary>
        /// 团购券详细信息
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 基本的卡券数据
            /// </summary>
            public CardBaseInfo base_info { get; set; }

            /// <summary>
            /// 团购详情
            /// </summary>
            public string deal_detail { get; set; }
        }
    }
}
