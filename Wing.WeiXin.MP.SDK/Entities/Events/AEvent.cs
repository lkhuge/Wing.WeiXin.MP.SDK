using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Events
{
    /// <summary>
    /// 事件抽象类
    /// </summary>
    public abstract class AEvent :AMessage
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        #region 实例化空数据事件抽象类 public AEvent()
        /// <summary>
        /// 实例化空数据事件抽象类
        /// </summary>
        protected AEvent()
        {
            MsgType = "event";
        }
        #endregion
    }
}
