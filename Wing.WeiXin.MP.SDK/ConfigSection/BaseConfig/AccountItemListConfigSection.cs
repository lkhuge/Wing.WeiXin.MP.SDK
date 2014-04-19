using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

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
            if (config == null) throw new ArgumentException();

            return config.WeixinMPID;
        }
        #endregion

        #region 获取公共平台账号项目配置节点 public AccountItemConfigSection GetAccountItemConfigSection(string weixinPMID)
        /// <summary>
        /// 获取公共平台账号项目配置节点
        /// </summary>
        /// <param name="weixinPMID">公共平台账号ID</param>
        /// <returns>公共平台账号项目配置节点</returns>
        public AccountItemConfigSection GetAccountItemConfigSection(string weixinPMID)
        {
            List<AccountItemConfigSection> list =
                this.OfType<AccountItemConfigSection>().Where(a => a.WeixinMPID.Equals(weixinPMID)).ToList();

            return list.Count != 1 ? null : list[0];
        }
        #endregion
    }
}
