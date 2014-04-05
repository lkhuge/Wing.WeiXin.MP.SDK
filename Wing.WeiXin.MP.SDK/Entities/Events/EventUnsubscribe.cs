using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.EventHandle;

namespace Wing.WeiXin.MP.SDK.Entities.Events
{
    /// <summary>
    /// 取消关注事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventUnsubscribe : AEvent
    {
        #region 实例化空数据取消关注事件 public EventUnsubscribe()
        /// <summary>
        /// 实例化空数据取消关注事件
        /// </summary>
        public EventUnsubscribe()
        {
            Event = "unsubscribe";
        }
        #endregion
    }
}
