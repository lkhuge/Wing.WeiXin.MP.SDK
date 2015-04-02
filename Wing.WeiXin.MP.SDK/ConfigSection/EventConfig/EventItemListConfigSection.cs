using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件项目列表配置节点
    /// </summary>
    public class EventItemListConfigSection : ConfigurationElementCollection
    {
        #region 创建新的事件项目配置节点 protected override ConfigurationElement CreateNewElement()
        /// <summary>
        /// 创建新的事件项目配置节点
        /// </summary>
        /// <returns>事件项目配置节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EventItemConfigSection();
        } 
        #endregion

        #region 获取事件项目配置节点名称 protected override object GetElementKey(ConfigurationElement element)
        /// <summary>
        /// 获取事件项目配置节点名称
        /// </summary>
        /// <param name="element">事件项目配置节点</param>
        /// <returns>事件项目配置节点名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            EventItemConfigSection config = element as EventItemConfigSection;
            if (config == null) throw WXException.GetInstance("无法获取事件项目列表配置节点", Settings.Default.SystemUsername);

            return config.Name;
        } 
        #endregion
    }
}
