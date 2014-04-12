using System;
using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 实体工厂类
    /// </summary>
    public static class EntityFactory
    {
        #region 请求处理 public static Response GetEntity(Request request)
        /// <summary>
        /// 请求处理
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应</returns>
        public static Response RequestHandle(Request request)
        {
            if (request == null) throw new ConvertToEntityException(null);
            //首次验证
            if (!String.IsNullOrEmpty(request.echostr))
            {
                if (Authentication.CheckSignature(request) )
                {
                    return new Response(request.echostr);
                }
                throw new FirstInvalidMessageException(request);
            }
            //消息验证
            if (!Authentication.CheckMessage(request)) throw new InvalidMessageException(request);
            IReturn ireturn = EventHandleManager.Action(IEntity(request));
            if (ireturn == null) throw new ConvertToEntityException(request);

            return new Response(ireturn);
        } 
        #endregion

        #region 实体类解析 private static BaseReceiveMessage IEntity(Request request)
        /// <summary>
        /// 实体类解析
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>接收消息</returns>
        private static BaseReceiveMessage IEntity(Request request)
        {
            string typeEntity = GetEntityType(request);
            //文本消息
            if ("text".Equals(typeEntity)) return EntityDeserialize<MessageText>(request);
            //图片消息
            if ("image".Equals(typeEntity)) return EntityDeserialize<MessageImage>(request);
            //语音消息
            if ("voice".Equals(typeEntity)) return EntityDeserialize<MessageVoice>(request);
            //视频消息
            if ("video".Equals(typeEntity)) return EntityDeserialize<MessageVideo>(request);
            //地理位置消息
            if ("location".Equals(typeEntity)) return EntityDeserialize<MessageLocation>(request);
            //链接消息
            if ("link".Equals(typeEntity)) return EntityDeserialize<MessageLink>(request);
            if (!"event".Equals(typeEntity)) throw new ConvertToEntityException(request);

            return IEvent(request);
        } 
        #endregion

        #region 事件实体类解析 private static BaseReceiveMessage IEvent(Request request)
        /// <summary>
        /// 事件实体类解析
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>事件接收消息</returns>
        private static BaseReceiveMessage IEvent(Request request)
        {
            string typeEvent = GetEventType(request);
            if ("subscribe".Equals(typeEvent))
            {
                if (XMLHelper.IsHaveNodeFromXMLString(request.postData, "Ticket"))
                {
                    return EntityDeserialize<EventSubscribeByQRScene>(request); //带参数二维码关注事件
                }
                return EntityDeserialize<EventSubscribe>(request); //关注事件
            }
            //取消关注事件
            if ("unsubscribe".Equals(typeEvent)) return EntityDeserialize<EventUnsubscribe>(request);
            //带参数二维码事件
            if ("SCAN".Equals(typeEvent)) return EntityDeserialize<EventWithQRScene>(request);
            //上报地理位置事件
            if ("LOCATION".Equals(typeEvent)) return EntityDeserialize<EventLocation>(request);
            //自定义菜单事件（点击菜单拉取消息时的事件推送）
            if ("CLICK".Equals(typeEvent)) return EntityDeserialize<EventClick>(request);
            //自定义菜单事件（点击菜单跳转链接时的事件推送）
            if ("VIEW".Equals(typeEvent)) return EntityDeserialize<EventView>(request);

            throw new ConvertToEntityException(request);
        } 
        #endregion

        #region 将请求解析为实体 private static T EntityDeserialize<T>(Request request) where T :IXML
        /// <summary>
        /// 将请求解析为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="request">请求</param>
        /// <returns>实体</returns>
        private static T EntityDeserialize<T>(Request request) where T :IXML
        {
            try
            {
                return XMLHelper.XMLDeserialize<T>(request.postData);
            }
            catch (BaseException)
            {
                throw new ConvertToEntityException(request);
            }
        } 
        #endregion

        #region 获取请求对象中实体类型 private static string GetEntityType(Request request)
        /// <summary>
        /// 获取请求对象中实体类型
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>实体类型</returns>
        private static string GetEntityType(Request request)
        {
            return XMLHelper.GetValueFromXML(request.postData, "MsgType");
        } 
        #endregion

        #region 获取请求对象中事件类型 private static string GetEventType(Request request)
        /// <summary>
        /// 获取请求对象中事件类型
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>事件类型</returns>
        private static string GetEventType(Request request)
        {
            return XMLHelper.GetValueFromXML(request.postData, "Event");
        }
        #endregion
    }
}
