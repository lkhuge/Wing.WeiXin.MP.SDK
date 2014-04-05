using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

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
            if (config == null) throw new ArgumentException();

            return config.Name;
        } 
        #endregion

        #region 检测基于微信用户事件是否生效 public bool CheckEventForWXUserBase(string OpenID)
        /// <summary>
        /// 检测基于微信用户事件是否生效
        /// </summary>
        /// <param name="OpenID">公共账号ID</param>
        /// <returns>是否生效</returns>
        public bool CheckEventForWXUserBase(string OpenID)
        {
            return this.OfType<EventItemConfigSection>().
                All(config => !config.Name.Equals("WXUserBase" + OpenID) || config.IsAction);
        } 
        #endregion

        #region 检测基于微信用户分组事件是否生效 public bool CheckEventForWXUserGroupBase(int GroupID)
        /// <summary>
        /// 检测基于微信用户分组事件是否生效
        /// </summary>
        /// <param name="GroupID">微信用户分组ID</param>
        /// <returns>是否生效</returns>
        public bool CheckEventForWXUserGroupBase(int GroupID)
        {
            return this.OfType<EventItemConfigSection>().
                All(config => !config.Name.Equals("WXUserGroupBase" + GroupID) || config.IsAction);
        }
        #endregion
    }
}
