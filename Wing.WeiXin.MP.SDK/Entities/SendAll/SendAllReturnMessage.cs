using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 群发回复消息
    /// </summary>
    public class SendAllReturnMessage : ErrorMsg
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long msg_id { get; set; }
    }
}
