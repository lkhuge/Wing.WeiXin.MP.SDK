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
        /// 是否检查事件名称
        /// </summary>
        internal static bool IsCheckEventName = true;

        /// <summary>
        /// 是否检查开发者微信号
        /// </summary>
        internal static bool IsCheckToUserName = true;

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

        /// <summary>
        /// 执行事件列表
        /// </summary>
        private readonly Func<Request, Response>[] ActionEventList;

        #region 初始化 public EventManager()
        /// <summary>
        /// 初始化
        /// </summary>
        public EventManager()
        {
            ActionEventList = new Func<Request, Response>[]
            {
                ActionTempEvent,
                ActionSystemEvent,
                ActionGlobalEvent,
                ActionReceiveEvent
            };
        } 
        #endregion

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
            LogManager.WriteSystem("添加临时接收事件-开始");
            TempReceiveEvent[toUserName].Add(fromUserName, receiveEvent);
            LogManager.WriteSystem("添加临时接收事件-结束");
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
            LogManager.WriteSystem("添加系统接收事件-开始");
            SystemReceiveEvent[toUserName].Add(receiveEvent);
            LogManager.WriteSystem("添加系统接收事件-结束");
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
            if (!CheckEventAction(eventName)) return;
            LogManager.WriteSystem("添加全局接收事件-开始");
            GloablReceiveEvent[toUserName].Add(eventName, receiveEvent);
            GloablReceiveEventPriorityList.Add(eventName, priority);
            LogManager.WriteSystem("添加全局接收事件-结束");
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
            if (!CheckEventAction(eventName)) return;
            LogManager.WriteSystem("添加普通接收事件(IEventBuilder<T>)-开始");
            ReceiveEvent[toUserName][typeName].Add(
                eventName,
                r => eventBuilder.GetEvent()(RequestAMessage.GetRequestAMessage<T>(r)));
            ReceiveEventPriorityList.Add(eventName, priority);
            LogManager.WriteSystem("添加普通接收事件(IEventBuilder<T>)-结束");
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
            if (!CheckEventAction(eventName)) return;
            LogManager.WriteSystem("添加普通接收事件(委托)-开始");
            ReceiveEvent[toUserName][typeName].Add(eventName, receiveEvent);
            ReceiveEventPriorityList.Add(eventName, priority);
            LogManager.WriteSystem("添加普通接收事件(委托)-结束");
        }
        #endregion

        #region 执行事件 internal Response ActionEvent(Request request, bool needCheck)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="needCheck">是否检查请求</param>
        /// <returns>响应对象</returns>
        internal Response ActionEvent(Request request, bool needCheck)
        {
            LogManager.WriteSystem("执行事件-开始");
            Response response = MessageFilter(request, needCheck);
            Response result = response ?? ActionEventList.Select(r => r(request)).FirstOrDefault(r => r != null);
            LogManager.WriteSystem("执行事件-结束");

            return result;
        }
        #endregion

        #region 执行消息过滤 private Response MessageFilter(Request request, bool needCheck)
        /// <summary>
        /// 执行消息过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="needCheck">是否检查请求</param>
        /// <returns>响应对象</returns>
        private Response MessageFilter(Request request, bool needCheck)
        {
            WXAccount account = request.WXAccount;
            if (account == null)
                throw WXException.GetInstance("无法确定微信公共平台账号", Settings.Default.SystemUsername);
            LogManager.WriteSystem("执行消息过滤-开始");
            Response result = account.MessageFilter(request, needCheck);
            LogManager.WriteSystem("执行消息过滤-结束");
            return result;
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
            LogManager.WriteSystem("执行临时事件-开始");
            Response response = TempReceiveEvent[request.ToUserName][request.FromUserName](request);
            if (response != null)
            {
                response.ActionEventName = String.Format("临时事件（账号ID:{0} 用户ID:{1}）", 
                    request.ToUserName, request.FromUserName);
                TempReceiveEvent[request.ToUserName].Remove(request.FromUserName);
                LogManager.WriteSystem("执行临时事件-删除临时事件-结束");
                return response;
            }
            LogManager.WriteSystem("执行临时事件-保留临时事件-结束");
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
            LogManager.WriteSystem("执行系统事件-开始");
            Response response = SystemReceiveEvent[request.ToUserName]
                .Select(e => e(request)).FirstOrDefault(r => r != null);
            if (response != null)
            {
                response.ActionEventName = String.Format("系统事件（账号ID:{0}）",
                    request.ToUserName);
                LogManager.WriteSystem("执行系统事件-发现响应-结束");
                return response;
            }

            LogManager.WriteSystem("执行系统事件-未发现响应-结束");
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
            LogManager.WriteSystem("执行全局事件-开始");
            Response response = GloablReceiveEventPriorityList
                .OrderByDescending(e => e.Value)
                .Select(e =>
                {
                    Response r = GloablReceiveEvent[request.ToUserName][e.Key](request);
                    if(r != null) r.ActionEventName = String.Format("全局事件（账号ID:{0} 事件ID:{1}）",
                        request.ToUserName, e.Key);
                    LogManager.WriteSystem("执行全局事件-发现响应-结束");
                    return r;
                })
                .FirstOrDefault(r => r != null);

            LogManager.WriteSystem("执行全局事件-未发现响应-结束");
            return response;
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
            LogManager.WriteSystem("执行普通事件-开始");
            Response response = ReceiveEventPriorityList
                .Where(e => ReceiveEvent[request.ToUserName][request.MsgType].ContainsKey(e.Key))
                .OrderByDescending(e => e.Value)
                .Select(e =>
                {
                    Response r = ReceiveEvent[request.ToUserName][request.MsgType][e.Key](request);
                    if (r != null) r.ActionEventName = String.Format("普通事件（账号ID:{0} 事件类型:{1} 事件ID:{2}）",
                         request.ToUserName, request.MsgType, e.Key);
                    LogManager.WriteSystem("执行普通事件-发现响应-结束");
                    return r;
                })
                .FirstOrDefault(r => r != null);

            LogManager.WriteSystem("执行普通事件-未发现响应-结束");
            return response;
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
            toUserName = GetWXAccountID(toUserName);
            if (!IsCheckToUserName) return toUserName;
            if (isCheckToUserName && !GlobalManager.ConfigManager.HasAccount())
                throw WXException.GetInstance("未发现任何微信公众平台账号", Settings.Default.SystemUsername);
            if (String.IsNullOrEmpty(toUserName))
                throw WXException.GetInstance("微信公众账号不能为空", toUserName);
            if (isCheckToUserName && GlobalManager.ConfigManager.GetWXAccountByID(toUserName) == null)
                throw WXException.GetInstance(String.Format("微信公众平台账号ID({0})不存在", toUserName), 
                    Settings.Default.SystemUsername);

            return toUserName;
        } 
        #endregion

        #region 检测事件是否可以执行 private bool CheckEventAction(string actionName)
        /// <summary>
        /// 检测事件是否可以执行
        /// </summary>
        /// <param name="actionName">事件名称</param>
        /// <returns>事件是否可以执行</returns>
        private bool CheckEventAction(string actionName)
        {
            if (!IsCheckEventName) return true;
            bool result = GlobalManager.ConfigManager.CheckEvent(actionName);

            return result;
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
                String.Format("账号ID:{0} 事件ID:{1} 优先级:{2}",
                    g.Key, e.Key, GloablReceiveEventPriorityList[e.Key])));
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
                String.Format("账号ID:{0} 事件类型:{1} 事件ID:{2} 优先级:{3}",
                    g.Key, e.Key, t.Key, ReceiveEventPriorityList[t.Key]))));
        }
        #endregion

        #region 根据ToUserName获取微信平台账号ID internal static string GetWXAccountID(string toUserName)
        /// <summary>
        /// 根据ToUserName获取微信平台账号ID
        /// </summary>
        /// <param name="toUserName">ToUserName</param>
        /// <returns>微信平台账号ID</returns>
        internal static string GetWXAccountID(string toUserName)
        {
            if (String.IsNullOrEmpty(toUserName)) return toUserName;
            if (toUserName.Equals(Settings.Default.FirstAccountToUserNameSign))
                return GlobalManager.GetFirstAccount().ID;
            if (toUserName.Equals(Settings.Default.DefaultAccountToUserNameSign))
                return GlobalManager.GetDefaultAccount().ID;

            return toUserName;
        }
        #endregion
    }
}
