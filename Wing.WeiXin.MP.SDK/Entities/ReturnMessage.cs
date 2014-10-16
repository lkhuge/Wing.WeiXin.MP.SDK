using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 返回信息
    /// </summary>
    public class ReturnMessage : ErrorMsg
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long msg_id { get; set; }
    }
}
