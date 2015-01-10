using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Statistics.Message
{
    /// <summary>
    /// 消息发送分布周数据
    /// </summary>
    public class UpstreamMsgDistWeek
    {
        /// <summary>
        /// 消息发送分布周数据信息列表
        /// </summary>
        public List<UpstreamMsgDistWeekItem> list { get; set; }

        /// <summary>
        /// 消息发送分布周数据信息
        /// </summary>
        public class UpstreamMsgDistWeekItem
        {
            /// <summary>
            /// 数据的日期，需在begin_date和end_date之间
            /// </summary>
            public string ref_date { get; set; }

            /// <summary>
            /// 当日发送消息量分布的区间，
            /// 
            /// 0代表 “0”，
            /// 1代表“1-5”，
            /// 2代表“6-10”，
            /// 3代表“10次以上”
            /// </summary>
            public int count_interval { get; set; }

            /// <summary>
            /// 上行发送了（向公众号发送了）消息的用户数
            /// </summary>
            public int msg_user { get; set; }
        }
    }
}
