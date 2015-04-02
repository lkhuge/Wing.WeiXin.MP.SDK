using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig
{
    /// <summary>
    /// 公共平台账号项目列表配置节点
    /// </summary>
    public class AccountItemListConfigSection : ConfigurationElementCollection
    {
        #region 创建新的公共平台账号项目配置节点 protected override ConfigurationElement CreateNewElement()
        /// <summary>
        /// 创建新的公共平台账号项目配置节点
        /// </summary>
        /// <returns>公共平台账号项目配置节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AccountItemConfigSection();
        }
        #endregion

        #region 获取公共平台账号项目配置节点名称 protected override object GetElementKey(ConfigurationElement element)
        /// <summary>
        /// 获取公共平台账号项目配置节点名称
        /// </summary>
        /// <param name="element">公共平台账号项目配置节点</param>
        /// <returns>公共平台账号项目配置节点名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            AccountItemConfigSection config = element as AccountItemConfigSection;
            if (config == null) throw WXException.GetInstance("无法获取公共平台账号项目列表配置节点", Settings.Default.SystemUsername);

            return config.WeixinMPID;
        }
        #endregion
    }
}
