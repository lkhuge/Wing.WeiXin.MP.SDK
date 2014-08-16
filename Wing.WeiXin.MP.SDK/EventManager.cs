using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Enumeration;

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
        private readonly Dictionary<string, Func<Request, Response>> GloablReceiveEvent =
            new Dictionary<string, Func<Request, Response>>();

        /// <summary>
        /// 非全局接收事件列表
        /// </summary>
        private readonly Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>> ReceiveEvent =
            new Dictionary<string, Dictionary<ReceiveEntityType, Dictionary<string, Func<Request, Response>>>>();

        #region 添加全局接收事件 public void AddGloablReceiveEvent(string eventName, Func<Request, Response> receiveEvent)
        /// <summary>
        /// 添加全局接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="receiveEvent">事件</param>
        public void AddGloablReceiveEvent(string eventName, Func<Request, Response> receiveEvent)
        {
            if (GloablReceiveEvent.ContainsKey(eventName))
            {
                throw new Exception(String.Format("事件名（{0}）重复", eventName));
            }
            GloablReceiveEvent.Add(eventName, receiveEvent);
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
                throw new Exception(String.Format("事件名（{0}）重复", eventName));
            }
            ReceiveEvent[toUserName][typeName].Add(
                eventName,
                r =>
                {
                    T t = new T();
                    t.SetNewRequest(r);
                    return receiveEvent(t);
                });
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
            Response result = ActionGlobalEvent(request);
            if (result != null) return result;
            result = ActionOneEvent(request);
            if (result != null) return result;

            return GlobalManager.ConfigManager.EventConfig.QuickConfigReturnMessageList
                .GetQuickConfigReturnMessage(request);
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
            return GloablReceiveEvent
                .Where(e => GlobalManager.ConfigManager.EventConfig.EventList.CheckEvent(e.Key))
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
                .Where(e => GlobalManager.ConfigManager.EventConfig.EventList.CheckEvent(e.Key))
                .Select(e => e.Value(request)).FirstOrDefault(r => r != null);
        }
        #endregion
    }
}
