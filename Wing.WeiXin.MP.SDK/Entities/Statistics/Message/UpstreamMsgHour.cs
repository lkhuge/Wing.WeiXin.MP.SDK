using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Statistics.Message
{
    /// <summary>
    /// 消息分送分时数据
    /// </summary>
    public class UpstreamMsgHour
    {
        /// <summary>
        /// 消息分送分时数据信息列表
        /// </summary>
        public List<UpstreamMsgHourItem> list { get; set; }

        /// <summary>
        /// 消息分送分时数据信息
        /// </summary>
        public class UpstreamMsgHourItem
        {
            /// <summary>
            /// 数据的日期，需在begin_date和end_date之间
            /// </summary>
            public string ref_date { get; set; }

            /// <summary>
            /// 数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，
            /// 即每日的第1小时和最后1小时
            /// </summary>
            public int ref_hour { get; set; }

            /// <summary>
            /// 消息类型，代表含义如下：
            /// 
            /// 1    代表文字 
            /// 2    代表图片 
            /// 3    代表语音 
            /// 4    代表视频 
            /// 6    代表第三方应用消息（链接消息）
            /// </summary>
            public int msg_type { get; set; }

            /// <summary>
            /// 上行发送了（向公众号发送了）消息的用户数
            /// </summary>
            public int msg_user { get; set; }

            /// <summary>
            /// 上行发送了消息的消息总数
            /// </summary>
            public int msg_count { get; set; }
        }
    }
}
