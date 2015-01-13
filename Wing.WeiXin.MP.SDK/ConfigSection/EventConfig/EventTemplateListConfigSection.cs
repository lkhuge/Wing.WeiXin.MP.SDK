using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.ConfigSection.EventConfig
{
    /// <summary>
    /// 事件模板列表配置节点
    /// </summary>
    public class EventTemplateListConfigSection : ConfigurationElementCollection
    {
        #region 创建新的事件项目配置节点 protected override ConfigurationElement CreateNewElement()
        /// <summary>
        /// 创建新的事件模板配置节点
        /// </summary>
        /// <returns>事件模板配置节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EventTemplateConfigSection();
        }
        #endregion

        #region 获取事件项目配置节点名称 protected override object GetElementKey(ConfigurationElement element)
        /// <summary>
        /// 获取事件模板配置节点名称
        /// </summary>
        /// <param name="element">事件模板配置节点</param>
        /// <returns>事件模板配置节点名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            EventTemplateConfigSection config = element as EventTemplateConfigSection;
            if (config == null) throw new ArgumentException();

            return config.Name;
        }
        #endregion

        #region 获取事件模板列表 public IEnumerable<EventTemplateConfigSection> GetEventTemplateList()
        /// <summary>
        /// 获取事件模板列表
        /// </summary>
        /// <returns>事件模板列表</returns>
        public IEnumerable<EventTemplateConfigSection> GetEventTemplateList()
        {
            return this.Cast<EventTemplateConfigSection>();
        }
        #endregion
    }
}
