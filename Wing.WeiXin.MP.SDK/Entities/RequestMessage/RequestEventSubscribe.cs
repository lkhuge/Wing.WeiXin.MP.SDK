using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage
{
    /// <summary>
    /// 关注事件请求
    /// </summary>
    public class RequestEventSubscribe : RequestAMessage
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.subscribe; }
        }
    }
}
