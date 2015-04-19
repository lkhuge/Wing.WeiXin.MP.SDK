﻿using System;
using System.Linq;
using System.Reflection;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.MessageFilter;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Config;
using Wing.WeiXin.MP.SDK.Extension.Module;
using Wing.WeiXin.MP.SDK.Properties;

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
        /// 功能管理器
        /// </summary>
        public static FunctionManager FunctionManager { get; private set; }

        /// <summary>
        /// 微信用户会话接口
        /// </summary>
        public static IWXSession WXSession { get; private set; }

        /// <summary>
        /// 外部调用的程序集
        /// </summary>
        public static Assembly CallingAssembly { get; set; }

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        internal static bool IsInit;

        #region 初始化 public static void Init(ConfigInfo config = null, IWXSession wxSession = null)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <param name="wxSession">微信会话接口</param>
        public static void Init(ConfigInfo config = null, IWXSession wxSession = null)
        {
            CallingAssembly = Assembly.GetCallingAssembly();
            InitConfig(config == null ? new ConfigManager() : new ConfigManager(config));
            WXSession = wxSession ?? new StaticWXSession();
            InitFunction(WXSession);
            InitEvent(new EventManager());

            IsInit = true;
        }
        #endregion

        #region 初始化配置 private static void InitConfig(ConfigManager configManager)
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configManager">配置管理类</param>
        private static void InitConfig(ConfigManager configManager)
        {
            if (ConfigLoad != null) ConfigLoad();
            ConfigManager = configManager;
        }
        #endregion

        #region 初始化事件 private static void InitEvent(EventManager eventManager)
        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="eventManager">事件管理类</param>
        private static void InitEvent(EventManager eventManager)
        {
            if (EventLoad != null) EventLoad();
            EventManager = eventManager;
        }
        #endregion

        #region 初始化功能管理器 public static void InitFunction(IWXSession wxSession)
        /// <summary>
        /// 初始化功能管理器
        /// </summary>
        /// <param name="wxSession">微信用户会话接口</param>
        public static void InitFunction(IWXSession wxSession)
        {
            FunctionManager = new FunctionManager(new AccessTokenContainer(wxSession));
        }
        #endregion

        #region 初始化基于Module的入口管理类 public static void InitWeixinModule()
        /// <summary>
        /// 初始化基于Module的入口管理类
        /// </summary>
        public static void InitWeixinModule()
        {
            WeixinModule.LoadHandlerList();
        } 
        #endregion

        #region 获取首个账号 public static WXAccount GetFirstAccount()
        /// <summary>
        /// 获取首个账号
        /// </summary>
        /// <returns>首个账号</returns>
        public static WXAccount GetFirstAccount()
        {
            return ConfigManager.Config.Base.AccountList.FirstOrDefault();
        }
        #endregion

        #region 检测是否初始化 public static void CheckInit()
        /// <summary>
        /// 检测是否初始化
        /// </summary>
        public static void CheckInit()
        {
            if (!IsInit) throw WXException.GetInstance("微信公共平台未初始化", Settings.Default.SystemUsername);
        }
        #endregion

        #region 添加重复消息过滤器 public static void AddRepetitionMessageFilter(string toUserName)
        /// <summary>
        /// 添加重复消息过滤器
        /// </summary>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        public static void AddRepetitionMessageFilter(string toUserName)
        {
            RepetitionMessageFilter.AddFilter(toUserName);
        } 
        #endregion
    }
}
