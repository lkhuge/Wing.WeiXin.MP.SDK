using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.ConfigSection.EventConfig;
using Wing.WeiXin.MP.SDK.ConfigSection.HandlerConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Config;
using Wing.WeiXin.MP.SDK.Entities.Config.Base;
using Wing.WeiXin.MP.SDK.Entities.Config.Event;
using Wing.WeiXin.MP.SDK.Entities.Config.Handler;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public ConfigInfo Config { get; protected set; }

        #region 根据本地配置节点实例化配置管理类 public ConfigManager()
        /// <summary>
        /// 根据本地配置节点实例化配置管理类
        /// </summary>
        public ConfigManager()
        {
            Config = LoadFromConfigSection();
        }
        #endregion

        #region 根据配置信息实例化配置管理类 public ConfigManager(ConfigInfo config)
        /// <summary>
        /// 根据配置信息实例化配置管理类
        /// </summary>
        /// <param name="config">配置信息</param>
        public ConfigManager(ConfigInfo config)
        {
            if (config == null)
                throw WXException.GetInstance("配置信息不能为空", Settings.Default.SystemUsername);
            if (config.Base == null)
                throw WXException.GetInstance("基本配置信息（ConfigInfo.Base）不能为空", Settings.Default.SystemUsername);
            if (String.IsNullOrEmpty(config.Base.Token))
                throw WXException.GetInstance("Token（ConfigInfo.Base.Token）不能为空", Settings.Default.SystemUsername);
            if (config.Base.AccountList == null)
                config.Base.AccountList = new List<WXAccount>();
            if (config.Event == null)
                config.Event = new EventConfigInfo { EventInfoList = new List<EventInfoConfigInfo>() };
            if (config.Event.EventInfoList == null)
                config.Event.EventInfoList = new List<EventInfoConfigInfo>();
            Config = config;
        }
        #endregion

        #region 是否存在账户 public bool HasAccount()
        /// <summary>
        /// 是否存在账户
        /// </summary>
        /// <returns>是否存在账户</returns>
        public bool HasAccount()
        {
            return Config.Base != null
                && Config.Base.AccountList != null
                && Config.Base.AccountList.Count > 0;
        }
        #endregion

        #region 根据账号ID获取账号信息 public WXAccount GetWXAccountByID(string id)
        /// <summary>
        /// 根据账号ID获取账号信息
        /// </summary>
        /// <param name="id">账号ID</param>
        /// <returns>账号信息</returns>
        public WXAccount GetWXAccountByID(string id)
        {
            return Config.Base.AccountList.FirstOrDefault(a => a.ID.Equals(id));
        }
        #endregion

        #region 检测事件是否生效 prublic bool CheckEvent(string eventKey)
        /// <summary>
        /// 检测事件是否生效
        /// </summary>
        /// <param name="eventKey">事件ID</param>
        /// <returns>是否生效</returns>
        public bool CheckEvent(string eventKey)
        {
            return Config.Event.EventInfoList.
                Any(c =>
                    c.Name.Equals(eventKey)
                    && c.IsAction);
        }
        #endregion

        #region 通过本地配置节点载入配置 private ConfigInfo LoadFromConfigSection()
        /// <summary>
        /// 通过本地配置节点载入配置
        /// </summary>
        /// <returns>配置信息</returns>
        private ConfigInfo LoadFromConfigSection()
        {
            return new ConfigInfo
            {
                Base = GetBaseConfigSection(),
                Event = GetEventConfigSection(),
                Handler = GetHandlerConfigSection()
            };
        }
        #endregion

        #region 获取基础配置节点 private BaseConfigInfo GetBaseConfigSection()
        /// <summary>
        /// 获取基础配置节点
        /// </summary>
        /// <returns>基础配置节点</returns>
        private BaseConfigInfo GetBaseConfigSection()
        {
            BaseConfigSection baseConfig = ConfigurationManager.GetSection(Settings.Default.ConfigRootNameGroup + "/Base") as BaseConfigSection;
            if (baseConfig == null) throw WXException.GetInstance("未发现基础配置", Settings.Default.SystemUsername);

            return new BaseConfigInfo
            {
                Token = baseConfig.Token,
                AccountList = baseConfig.AccountList
                    .Cast<AccountItemConfigSection>()
                    .Select(a => new WXAccount(
                        baseConfig.Token,
                        a.WeixinMPID,
                        a.AppID,
                        a.AppSecret,
                        a.EncodingAESKey))
                    .ToList()
            };
        }
        #endregion

        #region 获取事件配置节点 private EventConfigInfo GetEventConfigSection()
        /// <summary>
        /// 获取事件配置节点
        /// </summary>
        /// <returns>事件配置节点</returns>
        private EventConfigInfo GetEventConfigSection()
        {
            EventConfigSection eventConfig = ConfigurationManager.GetSection(Settings.Default.ConfigRootNameGroup + "/Event") as EventConfigSection;
            if (eventConfig == null) throw WXException.GetInstance("未发现事件配置", Settings.Default.SystemUsername);

            return new EventConfigInfo
            {
                EventInfoList = eventConfig.EventList
                    .Cast<EventItemConfigSection>()
                    .Select(e => new EventInfoConfigInfo
                    {
                        Name = e.Name,
                        IsAction = e.IsAction
                    })
                    .ToList()
            };
        }
        #endregion

        #region 获取Handler配置节点 private HandlerConfigInfo GetHandlerConfigSection()
        /// <summary>
        /// 获取Handler配置节点
        /// </summary>
        /// <returns>Handler配置节点</returns>
        private HandlerConfigInfo GetHandlerConfigSection()
        {
            HandlerConfigSection handlerConfig = ConfigurationManager.GetSection(Settings.Default.ConfigRootNameGroup + "/Handler") as HandlerConfigSection;
            if (handlerConfig == null) throw WXException.GetInstance("未发现Handler配置", Settings.Default.SystemUsername);

            return new HandlerConfigInfo
            {
                Sign = handlerConfig.Sign,
                Default = handlerConfig.Default,
                HandlerInfoList = handlerConfig.HandlerList
                    .Cast<HandlerItemConfigSection>()
                    .Select(e => new HandlerInfoConfigInfo
                    {
                        Name = e.Name,
                        Alias = e.Alias,
                        IsAction = e.IsAction
                    })
                    .ToList()
            };
        }
        #endregion
    }
}
