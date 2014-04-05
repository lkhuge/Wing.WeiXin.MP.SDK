using System;
using System.Configuration;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件配置节点
    /// </summary>
    public class EventConfigSection : ConfigurationSection
    {
        #region 是否需要使用全局事件处理 public bool UseGlobalEventHandler
        /// <summary>
        /// 是否需要使用全局事件处理
        /// </summary>
        [ConfigurationProperty("UseGlobalEventHandler", DefaultValue = true)]
        public bool UseGlobalEventHandler
        {
            get { return Convert.ToBoolean(this["UseGlobalEventHandler"]); }
        }
        #endregion

        #region 是否需要使用基于微信用户事件处理 public bool UseWXUserBaseEventHandler
        /// <summary>
        /// 是否需要使用基于微信用户事件处理
        /// </summary>
        [ConfigurationProperty("UseWXUserBaseEventHandler", DefaultValue = true)]
        public bool UseWXUserBaseEventHandler
        {
            get { return Convert.ToBoolean(this["UseWXUserBaseEventHandler"]); }
        }
        #endregion

        #region 是否需要使用基于微信用户分组事件处理 public bool UseWXUserGroupBaseEventHandler
        /// <summary>
        /// 是否需要使用基于微信用户分组事件处理
        /// </summary>
        [ConfigurationProperty("UseWXUserGroupBaseEventHandler", DefaultValue = true)]
        public bool UseWXUserGroupBaseEventHandler
        {
            get { return Convert.ToBoolean(this["UseWXUserGroupBaseEventHandler"]); }
        }
        #endregion

        #region 是否需要自定义基础事件处理 public bool UseCustomEventHandler
        /// <summary>
        /// 是否需要自定义基础事件处理
        /// </summary>
        [ConfigurationProperty("UseCustomEventHandler", DefaultValue = true)]
        public bool UseCustomEventHandler
        {
            get { return Convert.ToBoolean(this["UseCustomEventHandler"]); }
        }
        #endregion

        #region 是否需要使用基础事件处理 public bool UseBaseEventHandler
        /// <summary>
        /// 是否需要使用基础事件处理
        /// </summary>
        [ConfigurationProperty("UseBaseEventHandler", DefaultValue = true)]
        public bool UseBaseEventHandler
        {
            get { return Convert.ToBoolean(this["UseBaseEventHandler"]); }
        }
        #endregion

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
