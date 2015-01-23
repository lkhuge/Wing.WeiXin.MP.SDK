using System;
using System.Configuration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件配置节点
    /// </summary>
    public class EventConfigSection : ConfigurationSection
    {
        #region 事件列表 public EventItemListConfigSection EventList
        /// <summary>
        /// 事件列表
        /// </summary>
        [ConfigurationProperty("EventList")]
        public EventItemListConfigSection EventList
        {
            get { return this["EventList"] as EventItemListConfigSection; }
        }
        #endregion
    }
}
