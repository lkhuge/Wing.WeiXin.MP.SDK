using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class Note : IEntity
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
