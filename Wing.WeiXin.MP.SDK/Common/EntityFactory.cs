using System;
using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Events;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.Interface;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.EventHandle.EventEventHandler;
using Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler;
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
            IReturn ireturn = IEntityHandle(request);
            if (ireturn == null) throw new ConvertToEntityException(request);

            return new Response(ireturn);
        } 
        #endregion

        #region 实体类处理 private static IReturn IEntityHandle(Request request)
        /// <summary>
        /// 实体类处理
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>返回对象</returns>
        private static IReturn IEntityHandle(Request request)
        {
            string typeEntity = GetEntityType(request);
            //文本消息
            if ("text".Equals(typeEntity)) return new MessageTextEventHandler().Action(EntityDeserialize<MessageText>(request));
            //图片消息
            if ("image".Equals(typeEntity)) return new MessageImageEventHandler().Action(EntityDeserialize<MessageImage>(request));
            //语音消息
            if ("voice".Equals(typeEntity)) return new MessageVoiceEventHandler().Action(EntityDeserialize<MessageVoice>(request));
            //视频消息
            if ("video".Equals(typeEntity)) return new MessageVideoEventHandler().Action(EntityDeserialize<MessageVideo>(request));
            //地理位置消息
            if ("location".Equals(typeEntity)) return new MessageLocationEventHandler().Action(EntityDeserialize<MessageLocation>(request));
            //链接消息
            if ("link".Equals(typeEntity)) return new MessageLinkEventHandler().Action(EntityDeserialize<MessageLink>(request));

            return "event".Equals(typeEntity) ? IEventHandle(request) : null;
        } 
        #endregion

        #region 事件实体类处理 private static IReturn IEventHandle(Request request)
        /// <summary>
        /// 事件实体类处理
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>返回对象</returns>
        private static IReturn IEventHandle(Request request)
        {
            string typeEvent = GetEventType(request);
            if ("subscribe".Equals(typeEvent))
            {
                return XMLHelper.IsHaveNodeFromXMLString(request.postData, "Ticket")
                    ? new EventSubscribeByQRSceneEventHandler().Action(EntityDeserialize<EventSubscribeByQRScene>(request))   //带参数二维码关注事件
                    : new EventSubscribeEventHandler().Action(EntityDeserialize<EventSubscribe>(request)); //关注事件
            }
            //取消关注事件
            if ("unsubscribe".Equals(typeEvent)) return new EventUnsubscribeEventHandler().Action(EntityDeserialize<EventUnsubscribe>(request));
            //带参数二维码事件
            if ("SCAN".Equals(typeEvent)) return new EventWithQRSceneEventHandler().Action(EntityDeserialize<EventWithQRScene>(request));
            //上报地理位置事件
            if ("LOCATION".Equals(typeEvent)) return new EventLocationEventHandler().Action(EntityDeserialize<EventLocation>(request));
            //自定义菜单事件（点击菜单拉取消息时的事件推送）
            if ("CLICK".Equals(typeEvent)) return new EventClickEventHandler().Action(EntityDeserialize<EventClick>(request));
            //自定义菜单事件（点击菜单跳转链接时的事件推送）
            if ("VIEW".Equals(typeEvent)) return new EventViewEventHandler().Action(EntityDeserialize<EventView>(request));

            return null;
        } 
        #endregion

        #region 将请求解析为实体 private static T EntityDeserialize<T>(Request request) where T : IEntity
        /// <summary>
        /// 将请求解析为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="request">请求</param>
        /// <returns>实体</returns>
        private static T EntityDeserialize<T>(Request request) where T : IEntity
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
