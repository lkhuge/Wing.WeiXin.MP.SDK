using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.DKF
{
    /// <summary>
    /// 会话记录列表
    /// </summary>
    public class DKFrecordList
    {
        /// <summary>
        /// 会话记录列表
        /// </summary>
        public List<DKFrecord> recordlist { get; set; }

        /// <summary>
        /// 会话记录
        /// </summary>
        public class DKFrecord
        {
            /// <summary>
            /// 客服账号
            /// </summary>
            public string worker { get; set; }

            /// <summary>
            /// 普通用户openid
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 操作ID（会话状态）
            /// 
            /// 1000	创建未接入会话
            /// 1001	接入会话
            /// 1002	主动发起会话
            /// 1004	关闭会话
            /// 1005	抢接会话
            /// 2001	公众号收到消息
            /// 2002	客服发送消息
            /// 2003	客服收到消息
            /// </summary>
            public int opercode { get; set; }

            /// <summary>
            /// 操作时间，UNIX时间戳(秒级别)
            /// </summary>
            public long time { get; set; }

            /// <summary>
            /// 聊天记录
            /// </summary>
            public string text { get; set; }
        }
    }
}
