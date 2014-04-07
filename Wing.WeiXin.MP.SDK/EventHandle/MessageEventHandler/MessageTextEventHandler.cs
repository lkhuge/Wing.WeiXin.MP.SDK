using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.MessageEventHandler
{
    /// <summary>
    /// 文本消息事件处理
    /// </summary>
    public class MessageTextEventHandler : EntityEventHandler<MessageText>
    {
        #region 自动回复委托 public delegate string AutoReturnMessageHandler(string content);
        /// <summary>
        /// 自动回复委托
        /// </summary>
        /// <param name="content">接收消息</param>
        /// <returns>回复消息</returns>
        public delegate string AutoReturnMessageHandler(string content);
        #endregion

        #region 自动回复事件 private static AutoReturnMessageHandler AutoReturnMessageEvent;
        /// <summary>
        /// 自动回复事件
        /// </summary>
        private static AutoReturnMessageHandler AutoReturnMessageEvent;
        #endregion

        #region 基础事件处理 protected override IReturn BaseEntityEvent(MessageText entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(MessageText entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = entity.Content
            };
        }
        #endregion

        #region 添加自动回复事件 public static void AddAutoReturnMessageHandler(AutoReturnMessageHandler hander)
        /// <summary>
        /// 添加自动回复事件
        /// </summary>
        /// <param name="hander">自动回复委托</param>
        public static void AddAutoReturnMessageHandler(AutoReturnMessageHandler hander)
        {
            AutoReturnMessageEvent = hander;
            EntityEvent.EntityEvent = TempAutoReturnMessageEvent;
        }
        #endregion

        #region 临时自动回复事件 private static IReturn TempAutoReturnMessageEvent(MessageText message)
        /// <summary>
        /// 临时自动回复事件
        /// </summary>
        /// <param name="message">接收消息对象</param>
        /// <returns>回复消息对象</returns>
        private static IReturn TempAutoReturnMessageEvent(MessageText message)
        {
            if (AutoReturnMessageEvent == null) return null;
            return new ReturnMessageText
            {
                FromUserName = message.ToUserName,
                ToUserName = message.FromUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = AutoReturnMessageEvent(message.Content)
            };
        }
        #endregion
    }

}
