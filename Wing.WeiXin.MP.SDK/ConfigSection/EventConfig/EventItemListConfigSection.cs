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

        #region 检测全局事件是否生效 public bool CheckEventForGlobal(string weixinMPID, string eventID)
        /// <summary>
        /// 检测全局事件是否生效
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="eventID">全局事件ID</param>
        /// <returns>是否生效</returns>
        public bool CheckEventForGlobal(string weixinMPID, string eventID)
        {
            return CheckEvent(weixinMPID, "Global:" + eventID);
        }
        #endregion

        #region 检测自定义事件是否生效 public bool CheckEventForCustom(string weixinMPID, string eventID)
        /// <summary>
        /// 检测自定义事件是否生效
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="eventID">自定义事件ID</param>
        /// <returns>是否生效</returns>
        public bool CheckEventForCustom(string weixinMPID, string eventID)
        {
            return CheckEvent(weixinMPID, "Custom:" + eventID);
        }
        #endregion

        #region 检测事件是否生效 private bool CheckEvent(string weixinMPID, string eventKey)
        /// <summary>
        /// 检测事件是否生效
        /// </summary>
        /// <param name="weixinMPID">微信公共平台ID</param>
        /// <param name="eventKey">事件ID</param>
        /// <returns>是否生效</returns>
        private bool CheckEvent(string weixinMPID, string eventKey)
        {
            return this.OfType<EventItemConfigSection>().
                Any(config =>
                    config.Name.Equals(eventKey)
                    && config.WeixinMPID.Equals(weixinMPID)
                    && config.IsAction);
        } 
        #endregion
    }
}
