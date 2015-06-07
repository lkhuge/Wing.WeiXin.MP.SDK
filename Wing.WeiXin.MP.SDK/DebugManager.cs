using System;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 调试管理器
    /// </summary>
    public static class DebugManager
    {
        #region 框架
        #region 框架初始化之前
        /// <summary>
        /// 框架初始化之前
        /// </summary>
        public static event Action Init;

        /// <summary>
        /// 框架初始化之前
        /// </summary>
        internal static void OnInit()
        {
            if (Init != null) Init();
        }
        #endregion

        #region 框架初始化之后
        /// <summary>
        /// 框架初始化之后
        /// </summary>
        public static event Action InitD;

        /// <summary>
        /// 框架初始化之后
        /// </summary>
        internal static void OnInitD()
        {
            if (InitD != null) InitD();
        }
        #endregion 

        #region 基于Module的入口管理类初始化之前
        /// <summary>
        /// 基于Module的入口管理类初始化之前
        /// </summary>
        public static event Action InitWeixinModule;

        /// <summary>
        /// 基于Module的入口管理类初始化之前
        /// </summary>
        internal static void OnInitWeixinModule()
        {
            if (InitWeixinModule != null) InitWeixinModule();
        }
        #endregion

        #region 基于Module的入口管理类初始化之后
        /// <summary>
        /// 基于Module的入口管理类初始化之后
        /// </summary>
        public static event Action InitWeixinModuleD;

        /// <summary>
        /// 基于Module的入口管理类初始化之后
        /// </summary>
        internal static void OnInitWeixinModuleD()
        {
            if (InitWeixinModuleD != null) InitWeixinModuleD();
        }
        #endregion 
        #endregion

        #region AccessToken
        #region 获取AccessToken之前
        /// <summary>
        /// 获取AccessToken之前
        /// </summary>
        public static event Action<WXAccount> GetAccessToken;

        /// <summary>
        /// 获取AccessToken之前
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        internal static void OnGetAccessToken(WXAccount account)
        {
            if (GetAccessToken != null) GetAccessToken(account);
        }
        #endregion

        #region 获取AccessToken之后
        /// <summary>
        /// 获取AccessToken之后
        /// </summary>
        public static event Func<WXAccount, AccessToken, AccessToken> GetAccessTokenD;

        /// <summary>
        /// 获取AccessToken之后
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        /// <returns>AccessToken</returns>
        internal static AccessToken OnGetAccessTokenD(WXAccount account, AccessToken accessToken)
        {
            return GetAccessTokenD != null 
                ? GetAccessTokenD(account, accessToken) 
                : accessToken;
        }
        #endregion

        #region 获取缓存的AccessToken之后
        /// <summary>
        /// 获取缓存的AccessToken之后
        /// </summary>
        public static event Func<WXAccount, AccessToken, AccessToken> GetCacheAccessTokenD;

        /// <summary>
        /// 获取缓存的AccessToken之后
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        internal static AccessToken OnGetCacheAccessTokenD(WXAccount account, AccessToken accessToken)
        {
            return GetCacheAccessTokenD != null
                ? GetCacheAccessTokenD(account, accessToken)
                : accessToken;
        }
        #endregion

        #region 发现失效的AccessToken之后
        /// <summary>
        /// 发现失效的AccessToken之后
        /// </summary>
        public static event Action<WXAccount, AccessToken> FindFailureAccessTokenD;

        /// <summary>
        /// 发现失效的AccessToken之后
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="accessToken">AccessToken</param>
        internal static void OnFindFailureAccessTokenD(WXAccount account, AccessToken accessToken)
        {
            if (FindFailureAccessTokenD != null) FindFailureAccessTokenD(account, accessToken);
        }
        #endregion
        #endregion

        #region 事件
        #region 注册
        #region 添加临时接收事件之前
        /// <summary>
        /// 添加临时接收事件之前
        /// </summary>
        public static event Action<string, string, Func<Request, Response>> AddTempReceiveEvent;

        /// <summary>
        /// 添加临时接收事件之前
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="fromUserName">微信用户</param>
        /// <param name="receiveEvent">事件</param>
        internal static void OnAddTempReceiveEvent(string toUserName, string fromUserName, Func<Request, Response> receiveEvent)
        {
            if (AddTempReceiveEvent != null) AddTempReceiveEvent(toUserName, fromUserName, receiveEvent);
        }
        #endregion

        #region 添加临时接收事件之后
        /// <summary>
        /// 添加临时接收事件之后
        /// </summary>
        public static event Action<string, string, Func<Request, Response>> AddTempReceiveEventD;

        /// <summary>
        /// 添加临时接收事件之后
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="fromUserName">微信用户</param>
        /// <param name="receiveEvent">事件</param>
        internal static void OnAddTempReceiveEventD(string toUserName, string fromUserName, Func<Request, Response> receiveEvent)
        {
            if (AddTempReceiveEventD != null) AddTempReceiveEventD(toUserName, fromUserName, receiveEvent);
        }
        #endregion

        #region 添加系统接收事件之前
        /// <summary>
        /// 添加系统接收事件之前
        /// </summary>
        public static event Action<string, Func<Request, Response>> AddSystemReceiveEvent;

        /// <summary>
        /// 添加系统接收事件之前
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        internal static void OnAddSystemReceiveEvent(string toUserName, Func<Request, Response> receiveEvent)
        {
            if (AddSystemReceiveEvent != null) AddSystemReceiveEvent(toUserName, receiveEvent);
        }
        #endregion

        #region 添加系统接收事件之后
        /// <summary>
        /// 添加系统接收事件之后
        /// </summary>
        public static event Action<string, Func<Request, Response>> AddSystemReceiveEventD;

        /// <summary>
        /// 添加系统接收事件之后
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        internal static void OnAddSystemReceiveEventD(string toUserName, Func<Request, Response> receiveEvent)
        {
            if (AddSystemReceiveEventD != null) AddSystemReceiveEventD(toUserName, receiveEvent);
        }
        #endregion

        #region 添加全局接收事件之前
        /// <summary>
        /// 添加全局接收事件之前
        /// </summary>
        public static event Action<string, string, Func<Request, Response>, int> AddGloablReceiveEvent;

        /// <summary>
        /// 添加全局接收事件之前
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddGloablReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority)
        {
            if (AddGloablReceiveEvent != null) AddGloablReceiveEvent(eventName, toUserName, receiveEvent, priority);
        }
        #endregion

        #region 添加全局接收事件之后
        /// <summary>
        /// 添加全局接收事件之后
        /// </summary>
        public static event Action<string, string, Func<Request, Response>, int> AddGloablReceiveEventD;

        /// <summary>
        /// 添加全局接收事件之后
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddGloablReceiveEventD(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority)
        {
            if (AddGloablReceiveEventD != null) AddGloablReceiveEventD(eventName, toUserName, receiveEvent, priority);
        }
        #endregion

        #region 添加普通接收事件(IEventBuilder)之前
        /// <summary>
        /// 添加普通接收事件(IEventBuilder)之前
        /// </summary>
        public static event Action<string, string, object, int> AddReceiveEBEvent;

        /// <summary>
        /// 添加普通接收事件(IEventBuilder)之前
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="eventBuilder">事件生成器</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddReceiveEBEvent(string eventName, string toUserName, object eventBuilder, int priority)
        {
            if (AddReceiveEBEvent != null) AddReceiveEBEvent(eventName, toUserName, eventBuilder, priority);
        }
        #endregion

        #region 添加普通接收事件(IEventBuilder)之后
        /// <summary>
        /// 添加普通接收事件(IEventBuilder)之后
        /// </summary>
        public static event Action<string, string, object, int> AddReceiveEBEventD;

        /// <summary>
        /// 添加普通接收事件(IEventBuilder)之后
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="eventBuilder">事件生成器</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddReceiveEBEventD(string eventName, string toUserName, object eventBuilder, int priority)
        {
            if (AddReceiveEBEventD != null) AddReceiveEBEventD(eventName, toUserName, eventBuilder, priority);
        }
        #endregion

        #region 添加普通接收事件(委托)之前
        /// <summary>
        /// 添加普通接收事件(委托)之前
        /// </summary>
        public static event Action<string, string, object, int> AddReceiveEvent;

        /// <summary>
        /// 添加普通接收事件(委托)之前
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority)
        {
            if (AddReceiveEvent != null) AddReceiveEvent(eventName, toUserName, receiveEvent, priority);
        }
        #endregion

        #region 添加普通接收事件(委托)之后
        /// <summary>
        /// 添加普通接收事件(委托)之后
        /// </summary>
        public static event Action<string, string, object, int> AddReceiveEventD;

        /// <summary>
        /// 添加普通接收事件(委托)之后
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal static void OnAddReceiveEventD(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority)
        {
            if (AddReceiveEventD != null) AddReceiveEventD(eventName, toUserName, receiveEvent, priority);
        }
        #endregion   
        #endregion

        #region 执行
        #region 执行请求之前
        /// <summary>
        /// 执行请求之前
        /// </summary>
        public static event Action<Request> ActionRequest;

        /// <summary>
        /// 执行请求之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionRequest(Request request)
        {
            if (ActionRequest != null) ActionRequest(request);
        }
        #endregion

        #region 执行请求之后
        /// <summary>
        /// 执行请求之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionRequestD;

        /// <summary>
        /// 执行请求之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionRequestD(Request request, Response response)
        {
            return ActionRequestD != null
                ? ActionRequestD(request, response)
                : response;
        }
        #endregion 

        #region 执行事件之前
        /// <summary>
        /// 执行事件之前
        /// </summary>
        public static event Action<Request> ActionEvent;

        /// <summary>
        /// 执行事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionEvent(Request request)
        {
            if (ActionEvent != null) ActionEvent(request);
        }
        #endregion

        #region 执行事件之后
        /// <summary>
        /// 执行事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionEventD;

        /// <summary>
        /// 执行事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionEventD(Request request, Response response)
        {
            return ActionEventD != null
                ? ActionEventD(request, response)
                : response;
        }
        #endregion 

        #region 执行消息过滤事件之前
        /// <summary>
        /// 执行消息过滤事件之前
        /// </summary>
        public static event Action<Request> ActionMessageFilterEvent;

        /// <summary>
        /// 执行消息过滤事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionMessageFilterEvent(Request request)
        {
            if (ActionMessageFilterEvent != null) ActionMessageFilterEvent(request);
        }
        #endregion

        #region 执行消息过滤事件之后
        /// <summary>
        /// 执行消息过滤事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionMessageFilterEventD;

        /// <summary>
        /// 执行消息过滤事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionMessageFilterEventD(Request request, Response response)
        {
            return ActionMessageFilterEventD != null
                ? ActionMessageFilterEventD(request, response)
                : response;
        }
        #endregion 

        #region 执行临时事件之前
        /// <summary>
        /// 执行临时事件之前
        /// </summary>
        public static event Action<Request> ActionTempEvent;

        /// <summary>
        /// 执行临时事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionTempEvent(Request request)
        {
            if (ActionTempEvent != null) ActionTempEvent(request);
        }
        #endregion

        #region 执行临时事件之后
        /// <summary>
        /// 执行临时事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionTempEventD;

        /// <summary>
        /// 执行临时事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionTempEventD(Request request, Response response)
        {
            return ActionTempEventD != null
                ? ActionTempEventD(request, response)
                : response;
        }
        #endregion 

        #region 执行临时事件之后并保留
        /// <summary>
        /// 执行临时事件之后并保留
        /// </summary>
        public static event Func<Request, Response> ActionTempEventKeepD;

        /// <summary>
        /// 执行临时事件之后并保留
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionTempEventKeepD(Request request)
        {
            return ActionTempEventKeepD != null
                ? ActionTempEventKeepD(request)
                : null;
        }
        #endregion 

        #region 执行系统事件之前
        /// <summary>
        /// 执行系统事件之前
        /// </summary>
        public static event Action<Request> ActionSystemEvent;

        /// <summary>
        /// 执行系统事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionSystemEvent(Request request)
        {
            if (ActionSystemEvent != null) ActionSystemEvent(request);
        }
        #endregion

        #region 执行系统事件之后
        /// <summary>
        /// 执行系统事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionSystemEventD;

        /// <summary>
        /// 执行系统事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionSystemEventD(Request request, Response response)
        {
            return ActionSystemEventD != null
                ? ActionSystemEventD(request, response)
                : response;
        }
        #endregion 

        #region 执行系统事件没有响应
        /// <summary>
        /// 执行系统事件没有响应
        /// </summary>
        public static event Func<Request, Response> ActionSystemEventOver;

        /// <summary>
        /// 执行系统事件没有响应
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionSystemEventOver(Request request)
        {
            return ActionSystemEventOver != null
                ? ActionSystemEventOver(request)
                : null;
        }
        #endregion 

        #region 执行全局事件之前
        /// <summary>
        /// 执行全局事件之前
        /// </summary>
        public static event Action<Request> ActionGlobalEvent;

        /// <summary>
        /// 执行全局事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionGlobalEvent(Request request)
        {
            if (ActionGlobalEvent != null) ActionGlobalEvent(request);
        }
        #endregion

        #region 执行全局事件之后
        /// <summary>
        /// 执行全局事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionGlobalEventD;

        /// <summary>
        /// 执行全局事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionGlobalEventD(Request request, Response response)
        {
            return ActionGlobalEventD != null
                ? ActionGlobalEventD(request, response)
                : response;
        }
        #endregion

        #region 执行全局事件没有响应
        /// <summary>
        /// 执行全局事件没有响应
        /// </summary>
        public static event Func<Request, Response> ActionGlobalEventOver;

        /// <summary>
        /// 执行全局事件没有响应
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionGlobalEventOver(Request request)
        {
            return ActionGlobalEventOver != null
                ? ActionGlobalEventOver(request)
                : null;
        }
        #endregion 

        #region 执行普通事件之前
        /// <summary>
        /// 执行普通事件之前
        /// </summary>
        public static event Action<Request> ActionReceiveEvent;

        /// <summary>
        /// 执行普通事件之前
        /// </summary>
        /// <param name="request">请求对象</param>
        internal static void OnActionReceiveEvent(Request request)
        {
            if (ActionReceiveEvent != null) ActionReceiveEvent(request);
        }
        #endregion

        #region 执行普通事件之后
        /// <summary>
        /// 执行普通事件之后
        /// </summary>
        public static event Func<Request, Response, Response> ActionReceiveEventD;

        /// <summary>
        /// 执行普通事件之后
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="response">响应对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionReceiveEventD(Request request, Response response)
        {
            return ActionReceiveEventD != null
                ? ActionReceiveEventD(request, response)
                : response;
        }
        #endregion

        #region 执行普通事件没有响应
        /// <summary>
        /// 执行普通事件没有响应
        /// </summary>
        public static event Func<Request, Response> ActionReceiveEventOver;

        /// <summary>
        /// 执行普通事件没有响应
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        internal static Response OnActionReceiveEventOver(Request request)
        {
            return ActionReceiveEventOver != null
                ? ActionReceiveEventOver(request)
                : null;
        }
        #endregion 
        #endregion
        #endregion

        #region HTTP
        #region HTTPGET请求之前
        /// <summary>
        /// HTTPGET请求之前
        /// </summary>
        public static event Action<string> HttpGet;

        /// <summary>
        /// HTTPGET请求之前
        /// </summary>
        /// <param name="url">地址</param>
        internal static void OnHttpGet(string url)
        {
            if (HttpGet != null) HttpGet(url);
        }
        #endregion

        #region HTTPGET请求之后
        /// <summary>
        /// HTTPGET请求之后
        /// </summary>
        public static event Func<string, string, string> HttpGetD;

        /// <summary>
        /// HTTPGET请求之后
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="result">结果</param>
        /// <returns>结果</returns>
        internal static string OnHttpGetD(string url, string result)
        {
            return HttpGetD != null
                ? HttpGetD(url, result)
                : result;
        }
        #endregion

        #region HTTPPOST请求之前
        /// <summary>
        /// HTTPPOST请求之前
        /// </summary>
        public static event Action<string, string> HttpPost;

        /// <summary>
        /// HTTPPOST请求之前
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">参数</param>
        internal static void OnHttpPost(string url, string data)
        {
            if (HttpPost != null) HttpPost(url, data);
        }
        #endregion

        #region HTTPPOST请求之后
        /// <summary>
        /// HTTPPOST请求之后
        /// </summary>
        public static event Func<string, string, string, string> HttpPostD;

        /// <summary>
        /// HTTPPOST请求之后
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">参数</param>
        /// <param name="result">结果</param>
        /// <returns>结果</returns>
        internal static string OnHttpPostD(string url, string data, string result)
        {
            return HttpPostD != null
                ? HttpPostD(url, data, result)
                : result;
        }
        #endregion

        #region HTTP下载请求之前
        /// <summary>
        /// HTTP下载请求之前
        /// </summary>
        public static event Action<string, string, string> HttpDownload;

        /// <summary>
        /// HTTP下载请求之前
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <param name="data">POST参数（如果该参数不为空则使用POST方式下载）</param>
        internal static void OnHttpDownload(string url, string pathname, string data)
        {
            if (HttpDownload != null) HttpDownload(url, pathname, data);
        }
        #endregion

        #region HTTP下载请求有错误信息
        /// <summary>
        /// HTTP下载请求有错误信息
        /// </summary>
        public static event Func<string, string, string, string, string> HttpDownloadHasError;

        /// <summary>
        /// HTTP下载请求有错误信息
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <param name="data">POST参数（如果该参数不为空则使用POST方式下载）</param>
        /// <param name="result">结果</param>
        /// <returns>结果</returns>
        internal static string OnHttpDownloadHasError(string url, string pathname, string data, string result)
        {
            return HttpDownloadHasError != null
                ? HttpDownloadHasError(url, pathname, data, result)
                : result;
        }
        #endregion

        #region HTTP下载请求之后
        /// <summary>
        /// HTTP下载请求之后
        /// </summary>
        public static event Action<string, string, string> HttpDownloadD;

        /// <summary>
        /// HTTP下载请求之后
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <param name="data">POST参数（如果该参数不为空则使用POST方式下载）</param>
        internal static void OnHttpDownloadD(string url, string pathname, string data)
        {
            if (HttpDownloadD != null) HttpDownloadD(url, pathname, data);
        }
        #endregion

        #region HTTP上传请求之前
        /// <summary>
        /// HTTP上传请求之前
        /// </summary>
        public static event Action<string, string, string> HttpUpload;

        /// <summary>
        /// HTTP上传请求之前
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="path">要上传的本地文件路径</param>
        /// <param name="name">文件上传后的名称</param>
        internal static void OnHttpUpload(string url, string path, string name)
        {
            if (HttpUpload != null) HttpUpload(url, path, name);
        }
        #endregion

        #region HTTP上传请求之后
        /// <summary>
        /// HTTP上传请求之后
        /// </summary>
        public static event Func<string, string, string, string, string> HttpUploadD;

        /// <summary>
        /// HTTP上传请求之后
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="path">要上传的本地文件路径</param>
        /// <param name="name">文件上传后的名称</param>
        /// <param name="result">结果</param>
        /// <returns>结果</returns>
        internal static string OnHttpUploadD(string url, string path, string name, string result)
        {
            return HttpUploadD != null
                ? HttpUploadD(url, path, name, result)
                : result;
        }
        #endregion 
        #endregion

        #region 异常
        #region 捕获到异常
        /// <summary>
        /// 捕获到异常
        /// </summary>
        public static event Action<Exception> CatchException;

        /// <summary>
        /// 捕获到异常
        /// </summary>
        /// <param name="e">异常</param>
        internal static void OnCatchException(Exception e)
        {
            if (CatchException != null) CatchException(e);
        }
        #endregion 

        #region 捕获到微信异常
        /// <summary>
        /// 捕获到微信异常
        /// </summary>
        public static event Action<string, string, object> CatchWXException;

        /// <summary>
        /// 捕获到微信异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="user">发送异常的用户</param>
        /// <param name="exceptionTag">异常信息标签</param>
        internal static void OnCatchWXException(string message, string user, object exceptionTag)
        {
            if (CatchWXException != null) CatchWXException(message, user, exceptionTag);
        }
        #endregion 

        #region 捕获到消息类微信异常
        /// <summary>
        /// 捕获到消息类微信异常
        /// </summary>
        public static event Action<string> CatchMessageWXException;

        /// <summary>
        /// 捕获到消息类微信异常
        /// </summary>
        /// <param name="message">消息</param>
        internal static void OnCatchMessageWXException(string message)
        {
            if (CatchMessageWXException != null) CatchMessageWXException(message);
        }
        #endregion 
        #endregion

        #region 添加日志事件 internal void AddLogEvent()
        /// <summary>
        /// 添加日志事件
        /// </summary>
        internal static void AddLogEvent()
        {
            Init += () => { WriteSystem("全局-初始化-开始"); };
            InitD += () => { WriteSystem("全局-初始化-结束"); };
            InitWeixinModule += () => { WriteSystem("基于Module的入口管理类-初始化-开始"); };
            InitWeixinModuleD += () => { WriteSystem("基于Module的入口管理类-初始化-结束"); };

            GetAccessToken += a => { WriteSystem("获取AccessToken-开始"); };
            GetAccessTokenD += (a, at) => WriteSystem("获取AccessToken-新-结束" + Environment.NewLine + at, at);
            GetCacheAccessTokenD += (a, at) => WriteSystem("获取AccessToken-缓存-结束" + Environment.NewLine + at, at);
            FindFailureAccessTokenD += (a, at) => { WriteSystem("发现无效AccessToken 正在重新获取AccessToken"); };

            AddTempReceiveEvent += (t, f, r) => { WriteSystem("添加临时接收事件-开始"); };
            AddTempReceiveEventD += (t, f, r) => { WriteSystem("添加临时接收事件-结束"); };
            AddSystemReceiveEvent += (t, r) => { WriteSystem("添加系统接收事件-开始"); };
            AddSystemReceiveEventD += (t, r) => { WriteSystem("添加系统接收事件-结束"); };
            AddGloablReceiveEvent += (e, t, r, p) => { WriteSystem("添加全局接收事件-开始"); };
            AddGloablReceiveEventD += (e, t, r, p) => { WriteSystem("添加全局接收事件-结束"); };
            AddReceiveEBEvent += (e, t, eb, p) => { WriteSystem("添加普通接收事件(IEventBuilder<T>)-开始"); };
            AddReceiveEBEventD += (e, t, eb, p) => { WriteSystem("添加普通接收事件(IEventBuilder<T>)-结束"); };
            AddReceiveEvent += (e, t, r, p) => { WriteSystem("添加普通接收事件(委托)-开始"); };
            AddReceiveEventD += (e, t, r, p) => { WriteSystem("添加普通接收事件(委托)-结束"); };

            ActionRequest += r => { WriteSystem("请求：" + Environment.NewLine + r); };
            ActionRequestD += (r, rs) => WriteSystem("响应：" + Environment.NewLine + rs, rs);
            ActionEvent += r => { WriteSystem("执行事件-开始"); };
            ActionEventD += (r, rs) => WriteSystem("执行事件-结束", rs);
            ActionMessageFilterEvent += r => { WriteSystem("执行消息过滤-开始"); };
            ActionMessageFilterEventD += (r, rs) => WriteSystem("执行消息过滤-结束", rs);
            ActionTempEvent += r => { WriteSystem("执行临时事件-开始"); };
            ActionTempEventD += (r, rs) => WriteSystem("执行临时事件-删除临时事件-结束", rs);
            ActionTempEventKeepD += r => WriteSystem<Response>("执行临时事件-保留临时事件-结束", null);
            ActionSystemEvent += r => { WriteSystem("执行系统事件-开始"); };
            ActionSystemEventD += (r, rs) => WriteSystem("执行系统事件-发现响应-结束", rs);
            ActionSystemEventOver += r => WriteSystem<Response>("执行系统事件-未发现响应-结束", null);
            ActionGlobalEvent += r => { WriteSystem("执行全局事件-开始"); };
            ActionGlobalEventD += (r, rs) => WriteSystem("执行全局事件-发现响应-结束", rs);
            ActionGlobalEventOver += r => WriteSystem<Response>("执行全局事件-未发现响应-结束", null);
            ActionReceiveEvent += r => { WriteSystem("执行普通事件-开始"); };
            ActionReceiveEventD += (r, rs) => WriteSystem("执行普通事件-发现响应-结束", rs);
            ActionReceiveEventOver += r => WriteSystem<Response>("执行普通事件-未发现响应-结束", null);

            HttpGet += u => { WriteSystem("HTTP-GET-请求-开始" + Environment.NewLine + u); };
            HttpGetD += (u, r) => WriteSystem("HTTP-GET-请求-结束" + Environment.NewLine + r, r);
            HttpPost += (u, d) => { WriteSystem("HTTP-POST-请求-开始" + Environment.NewLine + u + Environment.NewLine + d); };
            HttpPostD += (u, d, r) => WriteSystem("HTTP-POST-请求-结束" + Environment.NewLine + r, r);
            HttpDownload += (u, p, d) => { WriteSystem("HTTP-Download-请求-开始" + Environment.NewLine + u + Environment.NewLine + d + Environment.NewLine + p); };
            HttpDownloadHasError += (u, p, d, r) => WriteSystem("HTTP-Download-请求-结束（有错误信息）" + Environment.NewLine + r, r);
            HttpDownloadD += (u, p, d) => { WriteSystem("HTTP-Download-请求-结束"); };
            HttpUpload += (u, p, n) => { WriteSystem("HTTP-Upload-请求-开始" + Environment.NewLine + u + Environment.NewLine + p + Environment.NewLine + n); };
            HttpUploadD += (u, p, n, r) => WriteSystem("HTTP-Upload-请求-结束" + Environment.NewLine + r, r);

            CatchException += e => { LogManager.WriteError("发生不可预料的异常", e); };
            CatchWXException += (m, u, e) => { LogManager.WriteInfo("ErrorMsg异常-" + m); };
            CatchMessageWXException += m => { LogManager.WriteInfo("消息类异常" + m); };
        } 
        #endregion

        #region 记录系统日志 private static void WriteSystem(string message)
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="message">消息内容</param>
        private static void WriteSystem(string message)
        {
            LogManager.WriteSystem(message);
        } 
        #endregion

        #region 记录系统日志 private static T WriteSystem<T>(string message, T result)
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="message">消息内容</param>
        /// <param name="result">结果对象</param>
        /// <returns>结果对象</returns>
        private static T WriteSystem<T>(string message, T result)
        {
            WriteSystem(message);

            return result;
        }
        #endregion
    }
}
