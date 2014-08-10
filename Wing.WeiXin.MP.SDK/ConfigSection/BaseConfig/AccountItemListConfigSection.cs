using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

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
            if (config == null) throw new Exception(String.Format("未注册该账号（{0}）", weixinMPID));

            return config;
        }
        #endregion

        #region 根据账号类型获取账号列表 public List<WXAccount> GetWXAccountList(WeixinMPType type)
        /// <summary>
        /// 根据账号类型获取账号列表
        /// </summary>
        /// <param name="type">账号类型</param>
        /// <returns>账号列表</returns>
        public List<WXAccount> GetWXAccountList(WeixinMPType type)
        {
            return this.Cast<AccountItemConfigSection>()
                .Where(a => a.WeixinMPType == type)
                .Select(a => new WXAccount(a))
                .ToList();
        }
        #endregion

        #region 根据账号类型获取账号列表中的第一个账号 public WXAccount GetWXAccountFirst(WeixinMPType type)
        /// <summary>
        /// 根据账号类型获取账号列表中的第一个账号
        /// </summary>
        /// <param name="type">账号类型</param>
        /// <returns>第一个账号</returns>
        public WXAccount GetWXAccountFirst(WeixinMPType type)
        {
            return this.Cast<AccountItemConfigSection>()
                .Where(a => a.WeixinMPType == type)
                .Select(a => new WXAccount(a))
                .FirstOrDefault();
        }
        #endregion
    }
}
