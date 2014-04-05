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
    /// 自定义菜单事件（点击菜单拉取消息时的事件推送）
    /// </summary>
    [XmlRoot("xml")]
    public class EventClick : AEvent
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        #region 实例化空数据自定义菜单事件（点击菜单拉取消息时的事件推送） public EventClick()
        /// <summary>
        /// 实例化空数据自定义菜单事件（点击菜单拉取消息时的事件推送）
        /// </summary>
        public EventClick()
        {
            Event = "CLICK";
        }
        #endregion
    }
}
