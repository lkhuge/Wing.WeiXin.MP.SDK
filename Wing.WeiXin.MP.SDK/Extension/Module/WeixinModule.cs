using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities.Config;
using Wing.WeiXin.MP.SDK.Entities.Config.Handler;

namespace Wing.WeiXin.MP.SDK.Extension.Module
{
    /// <summary>
    /// 微信模块
    /// </summary>
    public class WeixinModule : IHttpModule
    {
        /// <summary>
        /// 微信标记
        /// </summary>
        private string sign;

        /// <summary>
        /// 默认入口名称
        /// </summary>
        private string defaultName;

        /// <summary>
        /// Handler命名空间
        /// </summary>
        private const string HandlerNamespace = "Wing.WeiXin.MP.SDK.Extension.Module.Handler";

        /// <summary>
        /// Handler列表
        /// </summary>
        public static Dictionary<string, IHttpHandler> HandlerList = new Dictionary<string,IHttpHandler>(); 

        #region 注册事件 public void Init(HttpApplication context)
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="context">HTTP应用</param>
        public void Init(HttpApplication context)
        {
            GlobalManager.CheckInit();
            context.BeginRequest += BeginRequest;
            if (HandlerList == null) HandlerList = new Dictionary<string, IHttpHandler>();
            foreach (KeyValuePair<string, IHttpHandler> kv in LoadHandlerList())
            {
                HandlerList.Add(kv.Key, kv.Value);
            }
        }
        #endregion

        #region 载入Handler列表 private Dictionary<string, IHttpHandler> LoadHandlerList()
        /// <summary>
        /// 载入Handler列表
        /// </summary>
        /// <returns>Handler列表</returns>
        private Dictionary<string, IHttpHandler> LoadHandlerList()
        {
            ConfigInfo configInfo = GlobalManager.ConfigManager.Config;
            if (configInfo == null) return null;
            HandlerConfigInfo handlerConfig = configInfo.Handler;
            if (handlerConfig.HandlerInfoList == null || handlerConfig.HandlerInfoList.Count == 0) return null;
            sign = handlerConfig.Sign;
            defaultName = handlerConfig.Default;
            Dictionary<string, IHttpHandler> allHandlerList = GetAllHandlerList();

            return handlerConfig.HandlerInfoList
                .Where(h => allHandlerList.ContainsKey(h.Name) && h.IsAction)
                .ToDictionary(
                    k => String.IsNullOrEmpty(k.Alias) ? k.Name : k.Alias,
                    v => allHandlerList[v.Name]);
        }
        #endregion

        #region 获取全部Handler列表 private Dictionary<string, IHttpHandler> GetAllHandlerList()
        /// <summary>
        /// 获取全部Handler列表
        /// </summary>
        /// <returns>全部Handler列表</returns>
        private Dictionary<string, IHttpHandler> GetAllHandlerList()
        {
            IEnumerable<Type> allHandlerType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(HandlerNamespace) &&
                        typeof(IHttpHandler).IsAssignableFrom(t));
            Dictionary<string, IHttpHandler> allHandlerList = new Dictionary<string, IHttpHandler>();
            foreach (Type t in allHandlerType)
            {
                ConstructorInfo constructorInfo = t.GetConstructors().FirstOrDefault(c => c.IsPublic && !c.GetParameters().Any());
                if (constructorInfo == null) continue;
                allHandlerList.Add(t.Name, (IHttpHandler)constructorInfo.Invoke(null));
            }

            return allHandlerList;
        }
        #endregion

        #region 开始请求 private void BeginRequest(object sender, EventArgs e)
        /// <summary>
        /// 开始请求
        /// </summary>
        /// <param name="sender">HTTP应用</param>
        /// <param name="e">参数</param>
        private void BeginRequest(object sender, EventArgs e)
        {
            if (HandlerList == null) return;
            HttpApplication app = (HttpApplication) sender;
            string[] pathList = app.Request.Path.Split('/');
            string handlerName;
            if (String.IsNullOrEmpty(pathList[1]))
            {
                if (!HandlerList.ContainsKey(defaultName)) return;
                handlerName = defaultName;
            }
            else
            {
                if (String.IsNullOrEmpty(sign))
                {
                    if (!HandlerList.ContainsKey(pathList[1])) return;
                    handlerName = pathList[1];
                }
                else
                {
                    if (pathList.Length < 3) return;
                    if (!HandlerList.ContainsKey(pathList[2])) return;
                    handlerName = pathList[2];
                }
            }
            app.Context.RemapHandler(HandlerList[handlerName]);
        }  
        #endregion

        #region 释放资源 public void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { } 
        #endregion
    }
}
