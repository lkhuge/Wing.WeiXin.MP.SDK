using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Extension.Event;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// 全局接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, Func<Request, Response>>> GloablReceiveEvent =
            new Dictionary<string, Dictionary<string, Func<Request, Response>>>();

        /// <summary>
        /// 非全局接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>> ReceiveEvent =
            new Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>>();

        #region 添加全局接收事件 public void AddGloablReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent)
        /// <summary>
        /// 添加全局接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        /// <param name="receiveEvent">事件</param>
        public void AddGloablReceiveEvent(string eventName, string toUserName, Func<Request, Response> receiveEvent)
        {
            if (String.IsNullOrEmpty(toUserName)) toUserName = "";
            if (!GloablReceiveEvent.ContainsKey(toUserName))
            {
                GloablReceiveEvent.Add(toUserName, new Dictionary<string, Func<Request, Response>>());
            }
            if (GloablReceiveEvent[toUserName].ContainsKey(eventName))
            {
                throw WXException.GetInstance(String.Format("事件名（{0}）重复", eventName), toUserName);
            }
            GloablReceiveEvent[toUserName].Add(eventName, receiveEvent);
        } 
        #endregion

        #region 添加接收事件 public void AddReceiveEvent<T>(string eventName, string toUserName, Func<Request, T> receiveEvent)
        /// <summary>
        /// 添加接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEvent">事件</param>
        public void AddReceiveEvent<T>(string eventName, string toUserName, Func<T, Response> receiveEvent) where T : RequestAMessage, new()
        {
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
                r => receiveEvent(RequestAMessage.GetRequestAMessage<T>(r)));
        }
        #endregion

        #region 添加接收事件 public void AddReceiveEvent<T>(string toUserName, Dictionary<string, Func<T, Response>> receiveEventList)
        /// <summary>
        /// 添加接收事件
        /// </summary>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="receiveEventList">事件列表</param>
        public void AddReceiveEvent<T>(string toUserName, Dictionary<string, Func<T, Response>> receiveEventList) where T : RequestAMessage, new()
        {
            if (!ReceiveEvent.ContainsKey(toUserName))
            {
                ReceiveEvent.Add(toUserName, new Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>());
            }
            ReceiveEntityType typeName = new T().ReceiveEntityType;
            if (!ReceiveEvent[toUserName].ContainsKey(typeName))
            {
                ReceiveEvent[toUserName].Add(typeName, new Dictionary<string, Func<Request, Response>>());
            }
            foreach (KeyValuePair<string, Func<T, Response>> eve in receiveEventList)
            {
                if (ReceiveEvent[toUserName][typeName].ContainsKey(eve.Key))
                {
                    throw WXException.GetInstance(String.Format("事件名（{0}）重复", eve.Key), toUserName);
                }
                Func<T, Response> eveT = eve.Value;
                ReceiveEvent[toUserName][typeName].Add(
                    eve.Key,
                    r => eveT(RequestAMessage.GetRequestAMessage<T>(r)));
            }
            
        }
        #endregion

        #region 添加接收事件 public void AddReceiveEvent<T>(string eventName, string toUserName, IEventBuilder<T> eventBuilder)
        /// <summary>
        /// 添加接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="eventBuilder">事件生成器</param>
        public void AddReceiveEvent<T>(string eventName, string toUserName, IEventBuilder<T> eventBuilder) where T : RequestAMessage, new()
        {
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
            LogManager.WriteSystem("执行事件");

            LogManager.WriteSystem("执行全局事件");
            Response result = ActionGlobalEvent(request);
            if (result != null)
            {
                LogManager.WriteInfo("执行全局事件发送响应" + Environment.NewLine + result.Text, 
                    request.FromUserName);
                return result;
            }

            LogManager.WriteSystem("执行全局单账号事件");
            result = ActionGlobalOneEvent(request);
            if (result != null)
            {
                LogManager.WriteInfo("执行全局单账号事件发送响应" + Environment.NewLine + result.Text,
                    request.FromUserName);
                return result;
            }

            LogManager.WriteSystem("执行单账号事件");
            result = ActionOneEvent(request);
            if (result != null)
            {
                LogManager.WriteInfo("执行单账号事件发送响应" + Environment.NewLine + result.Text,
                    request.FromUserName);

                return result;
            }

            LogManager.WriteSystem("执行快速配置回复消息事件");
            result = GlobalManager.ConfigManager.EventConfig.QuickConfigReturnMessageList
                .GetQuickConfigReturnMessage(request);
            if (result != null)
            {
                LogManager.WriteInfo("执行快速配置回复消息事件发送响应" + Environment.NewLine + result.Text,
                    request.FromUserName);
            }
            return result;
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
            if (!GloablReceiveEvent.ContainsKey("")) return null;
            return GloablReceiveEvent[""]
                .Where(e => GlobalManager.CheckEventAction(e.Key))
                .Select(e => e.Value(request)).FirstOrDefault(r => r != null);
        }
        #endregion

        #region 执行全局单账号事件 private Response ActionGlobalOneEvent(Request request)
        /// <summary>
        /// 执行全局单账号事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionGlobalOneEvent(Request request)
        {
            if (!GloablReceiveEvent.ContainsKey(request.ToUserName)) return null;
            return GloablReceiveEvent[request.ToUserName]
                .Where(e => GlobalManager.CheckEventAction(e.Key))
                .Select(e => e.Value(request)).FirstOrDefault(r => r != null);
        }
        #endregion

        #region 执行单账号事件 private Response ActionOneEvent(Request request)
        /// <summary>
        /// 执行单账号事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionOneEvent(Request request)
        {
            if (!ReceiveEvent.ContainsKey(request.ToUserName)) return null;
            if (!ReceiveEvent[request.ToUserName].ContainsKey(request.MsgType)) return null;
            return ReceiveEvent[request.ToUserName][request.MsgType]
                .Where(e => GlobalManager.CheckEventAction(e.Key))
                .Select(e => e.Value(request)).FirstOrDefault(r => r != null);
        }
        #endregion
    }
}
