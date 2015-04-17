using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.ConfigSection.HandlerConfig
{
    /// <summary>
    /// 事件项目列表配置节点
    /// </summary>
    public class HandlerItemListConfigSection : ConfigurationElementCollection
    {
        #region 创建新的Handler项目配置节点 protected override ConfigurationElement CreateNewElement()
        /// <summary>
        /// 创建新的Handler项目配置节点
        /// </summary>
        /// <returns>Handler项目配置节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new HandlerItemConfigSection();
        } 
        #endregion

        #region 获取Handler项目配置节点名称 protected override object GetElementKey(ConfigurationElement element)
        /// <summary>
        /// 获取Handler项目配置节点名称
        /// </summary>
        /// <param name="element">Handler项目配置节点</param>
        /// <returns>Handler项目配置节点名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            HandlerItemConfigSection config = element as HandlerItemConfigSection;
            if (config == null) throw WXException.GetInstance("无法获取Handler项目列表配置节点", Settings.Default.SystemUsername);

            return config.Name;
        } 
        #endregion
    }
}
