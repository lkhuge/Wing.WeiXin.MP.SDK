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

        #region 快速配置回复消息列表 public QuickConfigReturnMessageItemListConfigSection QuickConfigReturnMessageList
        /// <summary>
        /// 快速配置回复消息列表
        /// </summary>
        [ConfigurationProperty("QuickConfigReturnMessageList")]
        public QuickConfigReturnMessageItemListConfigSection QuickConfigReturnMessageList
        {
            get { return this["QuickConfigReturnMessageList"] as QuickConfigReturnMessageItemListConfigSection; }
        }
        #endregion

        #region 事件模板列表 public EventTemplateListConfigSection EventTemplateList
        /// <summary>
        /// 事件模板列表
        /// </summary>
        [ConfigurationProperty("EventTemplateList")]
        public EventTemplateListConfigSection EventTemplateList
        {
            get { return this["EventTemplateList"] as EventTemplateListConfigSection; }
        }
        #endregion
    }
}
