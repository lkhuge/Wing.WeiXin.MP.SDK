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
        #region 实体类解析委托列表 private static readonly Dictionary<string, Func<Request, BaseReceiveMessage>> EntityList
        /// <summary>
        /// 实体类解析委托列表
        /// </summary>
        private static readonly Dictionary<string, Func<Request, BaseReceiveMessage>> EntityList =
            new Dictionary<string, Func<Request, BaseReceiveMessage>>
            {
                {"text", EntityDeserialize<MessageText>},               //文本消息
                {"image", EntityDeserialize<MessageImage>},             //图片消息
                {"voice", EntityDeserialize<MessageVoice>},             //语音消息
                {"video", EntityDeserialize<MessageVideo>},             //视频消息
                {"location", EntityDeserialize<MessageLocation>},       //地理位置消息
                {"link", EntityDeserialize<MessageLink>},               //链接消息
                {"event", request =>
                    {
                        string typeEvent = XMLHelper.GetValueFromXML(request.postData, "Event");
                        if (!EventList.ContainsKey(typeEvent)) throw new ConvertToEntityException(request);

                        return EventList[typeEvent](request);
                    }}
            }; 
        #endregion

        #region 事件实体类解析委托列表 private static readonly Dictionary<string, Func<Request, BaseReceiveMessage>> EventList
        /// <summary>
        /// 事件实体类解析委托列表
        /// </summary>
        private static readonly Dictionary<string, Func<Request, BaseReceiveMessage>> EventList =
            new Dictionary<string, Func<Request, BaseReceiveMessage>>
            {
                {"subscribe", request =>
                    {
                        if (XMLHelper.IsHaveNodeFromXMLString(request.postData, "Ticket"))
                        {
                            return EntityDeserialize<EventSubscribeByQRScene>(request); //带参数二维码关注事件
                        }
                        return EntityDeserialize<EventSubscribe>(request);              //关注事件
                    }},
                {"unsubscribe", EntityDeserialize<EventUnsubscribe>},                   //取消关注事件
                {"SCAN", EntityDeserialize<EventWithQRScene>},                          //带参数二维码事件
                {"LOCATION", EntityDeserialize<EventLocation>},                         //上报地理位置事件
                {"CLICK", EntityDeserialize<EventClick>},                               //自定义菜单事件（点击菜单拉取消息时的事件推送）
                {"VIEW", EntityDeserialize<EventView>}                                  //自定义菜单事件（点击菜单跳转链接时的事件推送）
            }; 
        #endregion

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
                if (Authentication.CheckSignature(request))
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
            string typeEntity = XMLHelper.GetValueFromXML(request.postData, "MsgType");
            if (!EntityList.ContainsKey(typeEntity)) throw new ConvertToEntityException(request);

            return EntityList[typeEntity](request);
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
    }
}
