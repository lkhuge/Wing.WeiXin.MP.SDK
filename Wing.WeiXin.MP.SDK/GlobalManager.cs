using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Wing.WeiXin.MP.SDK.Common.AccessTokenManager;
using Wing.WeiXin.MP.SDK.Common.MsgCrypt;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;
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
        /// AccessToken容器
        /// </summary>
        public static AccessTokenContainer AccessTokenContainer { get; private set; }

        /// <summary>
        /// 微信用户会话管理类
        /// </summary>
        public static WXSessionManager WXSessionManager { get; private set; }

        /// <summary>
        /// 微信加解密工具类列表
        /// </summary>
        public static Dictionary<string, WXBizMsgCrypt> CryptList;

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        internal static bool IsInit;

        #region 初始化 public static void Init()
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            InitBase();
            LibManager.InitLibByDefault();
            InitWXSessionManager(new StaticWXSession());
            InitAccessTokenContainer(new WXSessionAccessTokenManager());

            IsInit = true;
        }
        #endregion

        #region 初始化基础配置 public static void InitBase()
        /// <summary>
        /// 初始化基础配置
        /// </summary>
        public static void InitBase()
        {
            InitConfig(new ConfigManager());
            InitCryptList();
            InitEvent(new EventManager());
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

        #region 初始化AccessToken容器 public static void InitAccessTokenContainer(IAccessTokenManager accessTokenManager)
        /// <summary>
        /// 初始化AccessToken容器
        /// </summary>
        /// <param name="accessTokenManager">AccessToken管理类</param>
        public static void InitAccessTokenContainer(IAccessTokenManager accessTokenManager)
        {
            AccessTokenContainer = new AccessTokenContainer(accessTokenManager);
        }
        #endregion

        #region 初始化微信用户会话管理类 public static void InitWXSessionManager(IWXSession wxSession)
        /// <summary>
        /// 初始化微信用户会话管理类
        /// </summary>
        /// <param name="wxSession">微信用户会话管理类</param>
        public static void InitWXSessionManager(IWXSession wxSession)
        {
            WXSessionManager = new WXSessionManager(wxSession);
        }
        #endregion

        #region 初始化微信加解密工具类列表 public static void InitCryptList()
        /// <summary>
        /// 初始化微信加解密工具类列表
        /// </summary>
        public static void InitCryptList()
        {
            string token = ConfigManager.BaseConfig.Token;
            CryptList = ConfigManager.BaseConfig.AccountList.Cast<AccountItemConfigSection>()
                .Where(w => w.NeedEncoding)
                .ToDictionary(
                    k => k.WeixinMPID,
                    v => new WXBizMsgCrypt
                    {
                        token = token,
                        encodingAESKey = v.EncodingAESKey,
                        appID = v.AppID
                    });
        }
        #endregion

        #region 获取首个账号 public static WXAccount GetFirstAccount()
        /// <summary>
        /// 获取首个账号
        /// </summary>
        /// <returns>首个账号</returns>
        public static WXAccount GetFirstAccount()
        {
            return ConfigManager.BaseConfig.AccountList.GetWXAccountList().FirstOrDefault();
        }
        #endregion

        #region 获取首个服务号账号 public static WXAccount GetFirstServiceAccount()
        /// <summary>
        /// 获取首个服务号账号
        /// </summary>
        /// <returns>首个服务号账号</returns>
        public static WXAccount GetFirstServiceAccount()
        {
            return ConfigManager.BaseConfig.AccountList.GetWXAccountFirst(WeixinMPType.Service);
        }
        #endregion

        #region 获取首个订阅号账号 public static WXAccount GetFirstSubscriptionAccount()
        /// <summary>
        /// 获取首个订阅号账号
        /// </summary>
        /// <returns>首个订阅号账号</returns>
        public static WXAccount GetFirstSubscriptionAccount()
        {
            return ConfigManager.BaseConfig.AccountList.GetWXAccountFirst(WeixinMPType.Subscription);
        }
        #endregion

        #region 检测事件是否可以执行 public static bool CheckEventAction(string actionKey)
        /// <summary>
        /// 检测事件是否可以执行
        /// </summary>
        /// <param name="actionKey">事件配置Key</param>
        /// <returns>事件是否可以执行</returns>
        public static bool CheckEventAction(string actionKey)
        {
            LogManager.WriteSystem(String.Format("检测事件({0})是否可以执行", actionKey));
            bool result = ConfigManager.EventConfig.EventList.CheckEvent(actionKey);
            LogManager.WriteSystem(String.Format("事件({0}){1}可以执行", actionKey, result ? "" : "不"));

            return result;
        }
        #endregion

        #region 检测是否初始化 public static void CheckInit()
        /// <summary>
        /// 检测是否初始化
        /// </summary>
        public static void CheckInit()
        {
            LogManager.WriteSystem("检测是否初始化");
            if (!IsInit) throw WXException.GetInstance("微信公共平台未初始化", Settings.Default.SystemUsername);
            LogManager.WriteSystem("确认已初始化");
        }
        #endregion
    }
}
