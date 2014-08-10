using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 全局管理
    /// </summary>
    public static class GlobalManager
    {
        /// <summary>
        /// 载入配置事件
        /// </summary>
        public static event Action ConfigLoad;

        /// <summary>
        /// 载入事件事件
        /// </summary>
        public static event Action EventLoad;

        /// <summary>
        /// 配置管理类
        /// </summary>
        public static ConfigManager ConfigManager { get; private set; }

        /// <summary>
        /// 事件管理类
        /// </summary>
        public static EventManager EventManager { get; private set; }

        #region 初始化配置 public static void InitConfig(ConfigManager configManager)
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configManager">配置管理类</param>
        public static void InitConfig(ConfigManager configManager)
        {
            if (ConfigLoad != null) ConfigLoad();
            ConfigManager = configManager;
        } 
        #endregion

        #region 初始化事件 public static void InitEvent(EventManager eventManager)
        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="eventManager">事件管理类</param>
        public static void InitEvent(EventManager eventManager)
        {
            if (EventLoad != null) EventLoad();
            if (ConfigManager == null) throw new Exception("配置未加载");
            EventManager = eventManager;
        } 
        #endregion

        #region 检测是否已经加装 public static void CheckInit()
        /// <summary>
        /// 检测是否已经加装
        /// </summary>
        public static void CheckInit()
        {
            if (EventManager != null) return;
            throw new Exception("应用未加载");
        } 
        #endregion
    }
}
