namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 事件抽象类
    /// </summary>
    public abstract class BaseEvent : BaseReceiveMessage
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        #region 实例化空数据事件抽象类 public BaseEvent()
        /// <summary>
        /// 实例化空数据事件抽象类
        /// </summary>
        protected BaseEvent()
        {
            MsgType = "event";
        }
        #endregion
    }
}
