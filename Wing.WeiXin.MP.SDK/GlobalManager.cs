using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Wing.WeiXin.MP.SDK.Common.AccessTokenManager;
using Wing.WeiXin.MP.SDK.Common.WXSession;

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

        /// <summary>
        /// AccessToken容器
        /// </summary>
        public static AccessTokenContainer AccessTokenContainer { get; private set; }

        /// <summary>
        /// 微信用户会话管理类
        /// </summary>
        public static WXSessionManager WXSessionManager { get; private set; }

        #region 初始化 public static void Init()
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            InitConfig(new ConfigManager());
            InitEvent(new EventManager());
            InitAccessTokenContainer(new AccessTokenContainer());
        } 
        #endregion

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
            EventManager = eventManager;
        } 
        #endregion

        #region 初始化AccessToken容器 public static void InitAccessTokenContainer(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 初始化AccessToken容器
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public static void InitAccessTokenContainer(AccessTokenContainer accessTokenContainer)
        {
            AccessTokenContainer = accessTokenContainer;
        }
        #endregion

        #region 初始化微信用户会话管理类 public static void InitWXSessionManager(WXSessionManager wxSessionManager)
        /// <summary>
        /// 初始化微信用户会话管理类
        /// </summary>
        /// <param name="wxSessionManager">微信用户会话管理类</param>
        public static void InitWXSessionManager(WXSessionManager wxSessionManager)
        {
            WXSessionManager = wxSessionManager;
        }
        #endregion
    }
}
