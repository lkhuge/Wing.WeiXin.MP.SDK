using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 自定义菜单事件（点击菜单跳转链接时的事件推送）
    /// </summary>
    [XmlRoot("xml")]
    public class EventView : BaseEvent
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        #region 实例化空数据自定义菜单事件（点击菜单跳转链接时的事件推送） public EventView()
        /// <summary>
        /// 实例化空数据自定义菜单事件（点击菜单跳转链接时的事件推送）
        /// </summary>
        public EventView()
        {
            Event = "VIEW";
        }
        #endregion
    }
}
