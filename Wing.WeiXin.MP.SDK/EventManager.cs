using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Extension.Event;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// 临时接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, Func<Request, Response>>> TempReceiveEvent =
            new Dictionary<string, Dictionary<string, Func<Request, Response>>>();

        /// <summary>
        /// 系统接收事件列表
        /// </summary>
        private readonly Dictionary<string, List<Func<Request, Response>>> SystemReceiveEvent =
            new Dictionary<string, List<Func<Request, Response>>>();

        /// <summary>
        /// 全局接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, Func<Request, Response>>> GloablReceiveEvent =
            new Dictionary<string, Dictionary<string, Func<Request, Response>>>();

        /// <summary>
        /// 全局接收事件优先级列表
        /// </summary>
        private readonly Dictionary<string, int> GloablReceiveEventPriorityList = new Dictionary<string, int>();

        /// <summary>
        /// 普通接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>> ReceiveEvent =
            new Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>>();

        /// <summary>
        /// 普通接收事件优先级列表
        /// </summary>
        private readonly Dictionary<string, int> ReceiveEventPriorityList = new Dictionary<string, int>();

        #region 添加临时接收事件 public void AddTempReceiveEvent(string toUserName, string fromUserName, Func<Request, Response> receiveEvent)
        /// <summary>
        /// 添加临时接收事件
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="fromUserName">微信用户</param>
        /// <param name="receiveEvent">事件</param>
        public void AddTempReceiveEvent(string toUserName, string fromUserName, Func<Request, Response> receiveEvent)
        {
            toUserName = CheckToUserName(toUserName, false);
            if (!TempReceiveEvent.ContainsKey(toUserName))
                TempReceiveEvent.Add(toUserName, new Dictionary<string, Func<Request, Response>>());
            TempReceiveEvent[toUserName].Add(fromUserName, receiveEvent);
        }
        #endregion

        #region 添加系统接收事件 internal void AddSystemReceiveEvent(string toUserName, Func<Request, Response> receiveEvent)
        /// <summary>
        /// 添加系统接收事件
        /// </summary>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        /// <param name="receiveEvent">事件</param>
        internal void AddSystemReceiveEvent(string toUserName, Func<Request, Response> receiveEvent)
        {
            toUserName = CheckToUserName(toUserName, false);
            if (!SystemReceiveEvent.ContainsKey(toUserName))
                SystemReceiveEvent.Add(toUserName, new List<Func<Request, Response>>());
            SystemReceiveEvent[toUserName].Add(receiveEvent);
        }
        #endregion

        #region 添加全局接收事件 public void AddGloablReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority = 0)
        /// <summary>
        /// 添加全局接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        public void AddGloablReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority = 0)
        {
            if (String.IsNullOrEmpty(eventName)) throw WXException.GetInstance("事件名不能为空", toUserName);
            toUserName = CheckToUserName(toUserName);
            if (!GloablReceiveEvent.ContainsKey(toUserName))
                GloablReceiveEvent.Add(toUserName, new Dictionary<string, Func<Request, Response>>());
            if (GloablReceiveEvent[toUserName].ContainsKey(eventName))
                throw WXException.GetInstance(String.Format("事件名（{0}）重复", eventName), toUserName);
            GloablReceiveEvent[toUserName].Add(eventName, receiveEvent);
            GloablReceiveEventPriorityList.Add(eventName, priority);
        }
        #endregion

        #region 添加普通接收事件 public void AddReceiveEvent<T>(string eventName, string toUserName, Func<Request, T> receiveEvent, int priority = 0)
        /// <summary>
        /// 添加普通接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        public void AddReceiveEvent<T>(string eventName, string toUserName, Func<T, Response> receiveEvent, int priority = 0) where T : RequestAMessage, new()
        {
            AddReceiveEvent(new T().ReceiveEntityType, eventName, toUserName,
                r => receiveEvent(RequestAMessage.GetRequestAMessage<T>(r)), priority);
        }
        #endregion

        #region 添加普通接收事件 public void AddReceiveEvent<T>(string eventName, string toUserName, IEventBuilder<T> eventBuilder, int priority = 0)
        /// <summary>
        /// 添加普通接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="eventBuilder">事件生成器</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        public void AddReceiveEvent<T>(string eventName, string toUserName, IEventBuilder<T> eventBuilder, int priority = 0) where T : RequestAMessage, new()
        {
            if (String.IsNullOrEmpty(eventName)) throw WXException.GetInstance("事件名不能为空", toUserName);
            toUserName = CheckToUserName(toUserName);
            if (!ReceiveEvent.ContainsKey(toUserName))
            {
                ReceiveEvent.Add(toUserName, new Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>());
            }
            ReceiveEntityType typeName = new T().ReceiveEntityType;
            if (!ReceiveEvent[toUserName].ContainsKey(typeName))
            {
                ReceiveEvent[toUserName].Add(typeName, new Dictionary<string, Func<Request, Response>>());
            }
            if (ReceiveEvent[toUserName][typeName].ContainsKey(eventName))
            {
                throw WXException.GetInstance(String.Format("事件名（{0}）重复", eventName), toUserName);
            }
            ReceiveEvent[toUserName][typeName].Add(
                eventName,
                r => eventBuilder.GetEvent()(RequestAMessage.GetRequestAMessage<T>(r)));
            ReceiveEventPriorityList.Add(eventName, priority);

        }
        #endregion

        #region 添加普通接收事件 internal void AddReceiveEvent(ReceiveEntityType typeName, string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority = 0)
        /// <summary>
        /// 添加普通接收事件
        /// </summary>
        /// <param name="typeName">消息类型</param>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        /// <param name="priority">优先级（值越高优先级越大）</param>
        internal void AddReceiveEvent(ReceiveEntityType typeName, string eventName, string toUserName, Func<Request, Response> receiveEvent, int priority = 0)
        {
            if (String.IsNullOrEmpty(eventName)) throw WXException.GetInstance("事件名不能为空", toUserName);
            toUserName = CheckToUserName(toUserName);
            if (!ReceiveEvent.ContainsKey(toUserName))
            {
                ReceiveEvent.Add(toUserName, new Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>());
            }
            if (!ReceiveEvent[toUserName].ContainsKey(typeName))
            {
                ReceiveEvent[toUserName].Add(typeName, new Dictionary<string, Func<Request, Response>>());
            }
            if (ReceiveEvent[toUserName][typeName].ContainsKey(eventName))
            {
                throw WXException.GetInstance(String.Format("事件名（{0}）重复", eventName), toUserName);
            }
            ReceiveEvent[toUserName][typeName].Add(eventName, receiveEvent);
            ReceiveEventPriorityList.Add(eventName, priority);
        }
        #endregion

        #region 执行事件 public Response ActionEvent(Request request)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public Response ActionEvent(Request request)
        {
            Response result = ActionTempEvent(request);
            if (result != null) return result;
            result = ActionSystemEvent(request);
            if (result != null) return result;
            result = ActionGlobalEvent(request);
            if (result != null) return result;
            result = ActionReceiveEvent(request);
            if (result != null) return result;

            return GlobalManager.ConfigManager.EventConfig.QuickConfigReturnMessageList
                .GetQuickConfigReturnMessage(request);
        }
        #endregion

        #region 执行临时事件 private Response ActionTempEvent(Request request)
        /// <summary>
        /// 执行临时事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionTempEvent(Request request)
        {
            if (!TempReceiveEvent.ContainsKey(request.ToUserName)) return null;
            if (!TempReceiveEvent[request.ToUserName].ContainsKey(request.FromUserName)) return null;
            LogManager.WriteSystem("触发临时事件");
            Response response = TempReceiveEvent[request.ToUserName][request.FromUserName](request);
            if (response != null)
            {
                response.ActionEventName = String.Format("临时事件（账号ID:{0} 用户ID:{1}）", 
                    request.ToUserName, request.FromUserName);
                LogManager.WriteInfo("临时事件响应" + Environment.NewLine + response.Text,
                    request.FromUserName);
                bool deleteResult = TempReceiveEvent[request.ToUserName].Remove(request.FromUserName);
                LogManager.WriteInfo("删除临时事件响应" + (deleteResult ? "成功" : "失败"),
                    request.FromUserName);
                return response;
            }
            LogManager.WriteSystem("临时事件无响应");

            return null;
        }
        #endregion

        #region 执行系统事件 private Response ActionSystemEvent(Request request)
        /// <summary>
        /// 执行系统事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionSystemEvent(Request request)
        {
            if (!SystemReceiveEvent.ContainsKey(request.ToUserName)) return null;
            LogManager.WriteSystem("触发系统事件");
            Response response = SystemReceiveEvent[request.ToUserName]
                .Select(e => e(request)).FirstOrDefault(r => r != null);
            if (response != null)
            {
                response.ActionEventName = String.Format("系统事件（账号ID:{0}）",
                    request.ToUserName);
                LogManager.WriteInfo("系统事件响应" + Environment.NewLine + response.Text,
                    request.FromUserName);
                return response;
            }
            LogManager.WriteSystem("系统事件无响应");

            return null;
        }
        #endregion

        #region 执行全局事件 private Response ActionGlobalEvent(Request request)
        /// <summary>
        /// 执行全局事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionGlobalEvent(Request request)
        {
            if (!GloablReceiveEvent.ContainsKey(request.ToUserName)) return null;
            LogManager.WriteSystem("触发全局单账号事件");
            Response response = GloablReceiveEventPriorityList
                .Where(e => GlobalManager.CheckEventAction(e.Key))
                .OrderByDescending(e => e.Value)
                .Select(e =>
                {
                    Response r = GloablReceiveEvent[request.ToUserName][e.Key](request);
                    if(r != null) r.ActionEventName = String.Format("全局事件（账号ID:{0} 事件ID:{1}）",
                        request.ToUserName, e.Key);
                    return r;
                })
                .FirstOrDefault(r => r != null); 
            if (response != null)
            {
                LogManager.WriteInfo("全局单账号事件响应" + Environment.NewLine + response.Text,
                    request.FromUserName);
                return response;
            }
            LogManager.WriteSystem("全局单账号事件无响应");

            return null;
        }
        #endregion

        #region 执行普通事件 private Response ActionReceiveEvent(Request request)
        /// <summary>
        /// 执行普通事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionReceiveEvent(Request request)
        {
            if (!ReceiveEvent.ContainsKey(request.ToUserName)) return null;
            if (!ReceiveEvent[request.ToUserName].ContainsKey(request.MsgType)) return null;
            LogManager.WriteSystem("触发普通事件");
            Response response = ReceiveEventPriorityList
                .Where(e => GlobalManager.CheckEventAction(e.Key))
                .OrderByDescending(e => e.Value)
                .Select(e =>
                {
                    Response r = ReceiveEvent[request.ToUserName][request.MsgType][e.Key](request);
                    if (r != null) r.ActionEventName = String.Format("普通事件（账号ID:{0} 事件类型:{1} 事件ID:{2}）",
                         request.ToUserName, request.MsgType, e.Key);
                    return r;
                })
                .FirstOrDefault(r => r != null); 
            if (response != null)
            {
                LogManager.WriteInfo("普通事件响应" + Environment.NewLine + response.Text,
                    request.FromUserName);
                return response;
            }
            LogManager.WriteSystem("普通事件无响应");

            return null;
        }
        #endregion

        #region 检测开发者微信号 private void CheckToUserName(string toUserName, bool isCheckToUserName = true)
        /// <summary>
        /// 检测开发者微信号
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="isCheckToUserName">是否检测开发者微信号</param>
        /// <returns>开发者微信号</returns>
        private string CheckToUserName(string toUserName, bool isCheckToUserName = true)
        {
            if (isCheckToUserName && !GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountList().Any())
                throw WXException.GetInstance("未发现任何微信公众平台账号", Settings.Default.SystemUsername);
            if (String.IsNullOrEmpty(toUserName))
                throw WXException.GetInstance("微信公众账号不能为空", toUserName);
            if (toUserName.Equals(Settings.Default.FirstAccountToUserNameSign)) 
                toUserName = GlobalManager.GetFirstAccount().ID;
            if (isCheckToUserName && !GlobalManager.ConfigManager.BaseConfig.AccountList
                .GetWXAccountList().Any(w => w.ID.Equals(toUserName)))
                throw WXException.GetInstance(String.Format("微信公众平台账号ID({0})不存在", toUserName), Settings.Default.SystemUsername);

            return toUserName;
        } 
        #endregion

        #region 获取全局事件信息列表 internal IEnumerable<string> GetGloablReceiveEventInfoList()
        /// <summary>
        /// 获取全局事件信息列表
        /// </summary>
        /// <returns>全局事件信息列表</returns>
        internal IEnumerable<string> GetGloablReceiveEventInfoList()
        {
            return GloablReceiveEvent.SelectMany(g => g.Value.Select(e =>
                String.Format("账号ID:{0} 事件ID:{1} 优先级:{2} 是否生效:{3}",
                    g.Key, e.Key, GloablReceiveEventPriorityList[e.Key], GlobalManager.CheckEventAction(e.Key))));
        } 
        #endregion

        #region 获取普通事件信息列表 internal IEnumerable<string> GetReceiveEventInfoList()
        /// <summary>
        /// 获取普通事件信息列表
        /// </summary>
        /// <returns>普通事件信息列表</returns>
        internal IEnumerable<string> GetReceiveEventInfoList()
        {
            return ReceiveEvent.SelectMany(g => g.Value.SelectMany(e => e.Value.Select(t =>
                String.Format("账号ID:{0} 事件类型:{1} 事件ID:{2} 优先级:{3} 是否生效:{4}",
                    g.Key, e.Key, t.Key, ReceiveEventPriorityList[t.Key], GlobalManager.CheckEventAction(t.Key)))));
        }
        #endregion
    }
}
