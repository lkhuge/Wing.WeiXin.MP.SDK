using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 接收到的实体接口
    /// </summary>
    public interface IReceive
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        ReceiveEntityType entityType { get; }
    }
}
