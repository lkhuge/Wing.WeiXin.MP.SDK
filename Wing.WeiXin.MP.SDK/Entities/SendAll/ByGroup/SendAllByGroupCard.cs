namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发卡券消息（根据微信用户分组）
    /// </summary>
    public class SendAllByGroupCard : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的卡券消息
        /// </summary>
        public MPCard wxcard { get; set; }

        #region 实例化空的高级群发卡券消息 public SendAllByGroupCard()
        /// <summary>
        /// 实例化空的高级群发卡券消息
        /// </summary>
        public SendAllByGroupCard()
        {
            msgtype = "wxcard";
        } 
        #endregion

        #region 根据用于群发的消息的card_id和微信用户分组实例化高级群发卡券消息 public SendAllByGroupCard(string card_id, string group_id)
        /// <summary>
        /// 根据用于群发的消息的card_id和微信用户分组实例化高级群发卡券消息
        /// </summary>
        /// <param name="card_id">用于群发的消息的card_id</param>
        /// <param name="group_id">微信用户分组</param>
        public SendAllByGroupCard(string card_id, string group_id)
        {
            msgtype = "wxcard";
            filter = new Filter
            {
                group_id = group_id
            };
            wxcard = new MPCard
            {
                card_id = card_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的卡券消息
        /// </summary>
        public class MPCard
        {
            /// <summary>
            /// 用于群发的消息的card_id
            /// </summary>
            public string card_id { get; set; }
        }
    }
}
