using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event
{
    /// <summary>
    /// 发送模板消息事件请求
    /// </summary>
    public class RequestTemplateSendJobFinish : RequestAMessage
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgID
        {
            get { return GetPostData("MsgID"); }
        }

        /// <summary>
        /// 发送状态
        /// 
        /// success                 => 成功
        /// failed:user block       => 用户拒绝接收
        /// failed: system failed   => 发送失败（非用户拒绝）
        /// </summary>
        public string Status
        {
            get { return GetPostData("Status"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType {
            get { return ReceiveEntityType.CLICK; } 
        }
    }
}
