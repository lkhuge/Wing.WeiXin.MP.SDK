using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Events
{
    /// <summary>
    /// 带参数二维码关注事件
    /// </summary>
    [XmlRoot("xml")]
    public class EventSubscribeByQRScene : BaseEvent
    {
        /// <summary>
        /// 事件KEY值前缀
        /// </summary>
        private const string eventKeyFont = "qrscene_";

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        #region 实例化空数据带参数二维码关注事件 public EventSubscribeByQRScene()
        /// <summary>
        /// 实例化空数据带参数二维码关注事件
        /// </summary>
        public EventSubscribeByQRScene()
        {
            Event = "subscribe";
        }
        #endregion

        #region 获取二维码的参数值 public string GetEventKeyValue()
        /// <summary>
        /// 获取二维码的参数值
        /// </summary>
        /// <returns></returns>
        public string GetEventKeyValue()
        {
            return EventKey.Substring(eventKeyFont.Length);
        } 
        #endregion
    }
}
