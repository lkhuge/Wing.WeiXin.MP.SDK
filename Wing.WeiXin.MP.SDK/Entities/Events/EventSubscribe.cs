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
    /// 关注事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventSubscribe : AEvent
    {
        #region 实例化空数据关注事件 public EventSubscribe()
        /// <summary>
        /// 实例化空数据关注事件
        /// </summary>
        public EventSubscribe()
        {
            Event = "subscribe";
        }
        #endregion
    }
}
