using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            /// 微信用户分组
            /// </summary>
            public string group_id { get; set; }
        }
    }
}
