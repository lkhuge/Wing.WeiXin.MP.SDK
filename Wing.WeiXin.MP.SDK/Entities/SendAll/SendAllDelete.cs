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
    public class SendAllDelete
    {
        /// <summary>
        /// 发送出去的消息ID
        /// </summary>
        public long msgid { get; set; }

        #region 根据发送出去的消息ID实例化 public SendAllDelete(long msgid)
        /// <summary>
        /// 根据发送出去的消息ID实例化
        /// </summary>
        /// <param name="msgid">发送出去的消息ID</param>
        public SendAllDelete(long msgid)
        {
            this.msgid = msgid;
        } 
        #endregion
    }
}
