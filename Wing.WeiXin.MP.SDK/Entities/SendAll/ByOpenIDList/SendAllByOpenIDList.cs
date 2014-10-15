using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList
{
    /// <summary>
    /// 根据OpenID列表群发
    /// </summary>
    public abstract class SendAllByOpenIDList
    {
        /// <summary>
        /// Open列表
        /// </summary>
        public List<string> touser { get; set; }

        /// <summary>
        /// 群发的消息类型
        /// </summary>
        public string msgtype { get; set; }
    }
}
