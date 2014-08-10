using System;
using System.Collections.Generic;
using System.Configuration;
using Wing.WeiXin.MP.SDK.ConfigSection;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.ConfigSection.EventConfig;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 基础配置节点
        /// </summary>
        public BaseConfigSection BaseConfig { get; private set; }

        /// <summary>
        /// 事件配置节点
        /// </summary>
        public EventConfigSection EventConfig { get; private set; }

        #region 实例化配置管理类 public ConfigManager()
        /// <summary>
        /// 实例化配置管理类
        /// </summary>
        public ConfigManager()
        {
            LoadBaseConfigSection();
            LoadEventConfigSection();
        }
        #endregion

        #region 加载基础配置 private void LoadBaseConfigSection()
        /// <summary>
        /// 加载基础配置
        /// </summary>
        private void LoadBaseConfigSection()
        {
            BaseConfig = ConfigurationManager.GetSection("WeiXinMPSDKConfigGroup/Base") as BaseConfigSection;
            if (BaseConfig == null) throw new Exception("未发现基础配置");
        }
        #endregion

        #region 加载事件配置 private void LoadEventConfigSection()
        /// <summary>
        /// 加载事件配置
        /// </summary>
        private void LoadEventConfigSection()
        {
            EventConfig = ConfigurationManager.GetSection("WeiXinMPSDKConfigGroup/Event") as EventConfigSection;
            if (EventConfig == null) throw new Exception("未发现事件配置");
        }
        #endregion
    }
}
