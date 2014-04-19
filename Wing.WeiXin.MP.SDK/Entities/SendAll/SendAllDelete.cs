using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 删除群发对象
    /// </summary>
    public class SendAllDelete : IJSON
    {
        /// <summary>
        /// 发送出去的消息ID
        /// </summary>
        public string msgid { get; set; }
    }
}
