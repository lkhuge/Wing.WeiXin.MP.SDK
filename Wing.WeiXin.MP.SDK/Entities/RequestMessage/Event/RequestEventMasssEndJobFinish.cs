using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event
{
    /// <summary>
    /// 推送群发结果事件请求
    /// </summary>
    public class RequestEventMasssEndJobFinish : RequestAMessage
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.MASSSENDJOBFINISH; }
        }
    }
}
