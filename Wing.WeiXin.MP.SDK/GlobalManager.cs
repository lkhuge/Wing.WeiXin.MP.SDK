﻿using System;
using System.IO;
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
            ConfigManager = config == null ? new ConfigManager() : new ConfigManager(config);
            WXSession = wxSession ?? new StaticWXSession();
            FunctionManager = new FunctionManager(new AccessTokenContainer(WXSession));
            EventManager = new EventManager();
            InitLog();

            IsInit = true;
        }
        #endregion

        #region 初始化日志 private static void InitLog()
        /// <summary>
        /// 初始化日志
        /// </summary>
        private static void InitLog()
        {
            string log = ConfigManager.Config.Base.Log;
            if (String.IsNullOrEmpty(log)) return;
            LogManager.AddWriteCallback(msg => File.AppendAllLines(log, new[] { msg }));
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

        #region 获取默认账号账号 public static WXAccount GetDefaultAccount()
        /// <summary>
        /// 获取默认账号账号
        /// 如果未设置或者不存在则返回第一个账号
        /// </summary>
        /// <returns>默认账号账号</returns>
        public static WXAccount GetDefaultAccount()
        {
            string id = ConfigManager.Config.Base.DefaultAccount;
            if (String.IsNullOrEmpty(id)) return GetFirstAccount();

            return ConfigManager.Config.Base.AccountList.FirstOrDefault(a => a.ID.Equals(id)) ?? GetFirstAccount();
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

        #region 当前是否为Debug模式 public static bool IsDebug()
        /// <summary>
        /// 当前是否为Debug模式
        /// </summary>
        /// <returns>是否为Debug模式</returns>
        public static bool IsDebug()
        {
            return ConfigManager.Config.Base.Debug;
        } 
        #endregion
    }
}
