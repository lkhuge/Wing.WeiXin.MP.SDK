using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

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

        #region 获取公共平台账号项目配置节点 public AccountItemConfigSection GetAccountItemConfigSection(string weixinMPID)
        /// <summary>
        /// 获取公共平台账号项目配置节点
        /// </summary>
        /// <param name="weixinMPID">公共平台账号ID</param>
        /// <returns>公共平台账号项目配置节点</returns>
        public AccountItemConfigSection GetAccountItemConfigSection(string weixinMPID)
        {
            AccountItemConfigSection config =
                this.Cast<AccountItemConfigSection>().FirstOrDefault(a => a.WeixinMPID.Equals(weixinMPID));
            if (config == null) throw WXException.GetInstance(String.Format("未注册该账号（{0}）", weixinMPID), weixinMPID);

            return config;
        }
        #endregion

        #region 获取账号列表 public List<WXAccount> GetWXAccountList()
        /// <summary>
        /// 获取账号列表
        /// </summary>
        /// <returns>账号列表</returns>
        public List<WXAccount> GetWXAccountList()
        {
            return this.Cast<AccountItemConfigSection>()
                .Select(a => new WXAccount(a))
                .ToList();
        }
        #endregion
    }
}
