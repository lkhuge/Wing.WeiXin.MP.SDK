using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 群发回复消息
    /// </summary>
    public class SendAllReturnMessage
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public int msg_id { get; set; }
    }
}
