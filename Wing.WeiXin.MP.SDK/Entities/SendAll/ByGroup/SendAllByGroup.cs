using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 群发组
    /// </summary>
    public abstract class SendAllByGroup
    {
        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public Filter filter { get; set; }

        /// <summary>
        /// 群发的消息类型，图文消息为mpnews
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public class Filter
        {
            /// <summary>
            /// 群发到的分组的group_id
            /// </summary>
            public string group_id { get; set; }
        }
    }
}
