using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessage
{
    /// <summary>
    /// 客服消息类
    /// </summary>
    public class CSMessage :IEntity, IJSON
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }
}
