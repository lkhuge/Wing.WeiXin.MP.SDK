using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK
{
    /// <summary>
    /// 事件管理类
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// 接收事件
        /// </summary>
        private readonly Dictionary<string, Func<Request, Response>> ReceiveEvent =
            new Dictionary<string, Func<Request, Response>>();

        #region 添加接收事件 public void AddReceiveEvent(string eventName, bool isGlobal, string toUserName, ReceiveEntityType type, Func<Request, Response> receiveEvent)
        /// <summary>
        /// 添加接收事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="isGlobal">是否为全局事件</param>
        /// <param name="toUserName">开发者微信号</param>
        /// <param name="type">事件类型</param>
        /// <param name="receiveEvent">事件</param>
        public void AddReceiveEvent(string eventName, bool isGlobal, string toUserName, ReceiveEntityType type, Func<Request, Response> receiveEvent)
        {
            if (ReceiveEvent.ContainsKey(eventName))
            {
                throw new Exception(String.Format("事件名（{0}）重复", eventName));
            }
            ReceiveEvent.Add(String.Format("{0}:{1}:{2}:{3}",
                eventName,
                toUserName,
                Enum.GetName(typeof(ReceiveEntityType), type),
                isGlobal ? "G" : "S"), receiveEvent);
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
            result = ActionOneGlobalEvent(request);
            if (result != null) return result;
            result = GlobalManager.ConfigManager.EventConfig.QuickConfigReturnMessageList.GetQuickConfigReturnMessage(request);
            if (result != null) return result;

            return ActionReceiveEvent(request);
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
            return ReceiveEvent.Where(e =>
            {
                string[] l = e.Key.Split(':');
                return
                    GlobalManager.ConfigManager.EventConfig.EventList.CheckEvent(l[0]) &&
                    String.IsNullOrEmpty(l[1]) &&
                    l[2].Equals(request.MsgTypeName) &&
                    l[3].Equals("G");
            }).Select(e => e.Value(request)).FirstOrDefault();
        }
        #endregion

        #region 执行单账号全局事件 private Response ActionOneGlobalEvent(Request request)
        /// <summary>
        /// 执行单账号全局事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionOneGlobalEvent(Request request)
        {
            return ReceiveEvent.Where(e =>
            {
                string[] l = e.Key.Split(':');
                return
                    GlobalManager.ConfigManager.EventConfig.EventList.CheckEvent(l[0]) &&
                    l[1].Equals(request.ToUserName) &&
                    l[2].Equals(request.MsgTypeName) &&
                    l[3].Equals("G");
            }).Select(e => e.Value(request)).FirstOrDefault();
        }
        #endregion

        #region 执行接收事件 private Response ActionReceiveEvent(Request request)
        /// <summary>
        /// 执行接收事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private Response ActionReceiveEvent(Request request)
        {
            return ReceiveEvent.Where(e =>
            {
                string[] l = e.Key.Split(':');
                return
                    GlobalManager.ConfigManager.EventConfig.EventList.CheckEvent(l[0]) &&
                    l[1].Equals(request.ToUserName) &&
                    l[2].Equals(request.MsgTypeName) &&
                    l[3].Equals("S");
            }).Select(e => e.Value(request)).FirstOrDefault();
        }
        #endregion
    }
}
