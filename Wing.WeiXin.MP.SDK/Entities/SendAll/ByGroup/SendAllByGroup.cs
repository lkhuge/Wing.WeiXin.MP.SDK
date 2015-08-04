namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 根据微信用户分组群发
    /// </summary>
    public abstract class SendAllByGroup
    {
        /// <summary>
        /// 微信用户分组
        /// </summary>
        public Filter filter { get; set; }

        /// <summary>
        /// 群发的消息类型
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 微信用户分组
        /// </summary>
        public class Filter
        {
            /// <summary>
            /// 用于设定是否向全部用户发送，值为true或false，
            /// 选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户
            /// </summary>
            public bool is_to_all { get; set; }

            /// <summary>
            /// 微信用户分组
            /// </summary>
            public string group_id { get; set; }
        }
    }
}
