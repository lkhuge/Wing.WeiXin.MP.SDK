using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Test
{
    /// <summary>
    /// 事件处理测试
    /// </summary>
    [TestClass]
    public class EventHandlerTest : BaseTest
    {
        #region 文本事件测试
        /// <summary>
        /// 文本事件测试
        /// </summary>
        [TestMethod]
        public void MessageTextEventHandlerTest()
        {
            EventHandleManager.Init(
                new Dictionary<string, EntityHandler>
                {
                    {"gh_7f215c8b1c91", 
                        new EntityHandler
                        {
                            MessageTextHandlerList = new EntityHandler.CustomEntityHandler<MessageText>[]
                            {
                                GlobalEntityEvent
                            }
                        }}
                });
            ReturnMessageText text = XMLHelper.XMLDeserialize<ReturnMessageText>(ReceiveController.Action(messageText).Text);
            Assert.AreEqual(text.content, "ok");
        } 
        #endregion

        #region 全局事件 public IReturn GlobalEntityEvent(BaseReceiveMessage message)
        /// <summary>
        /// 全局事件
        /// </summary>
        /// <returns></returns>
        public IReturn GlobalEntityEvent(BaseReceiveMessage message)
        {
            if (message.FromUserName.Equals("olPjZjsXuQPJoV0HlruZkNzKc91E"))
            {
                return new ReturnMessageText
                {
                    ToUserName = message.FromUserName,
                    FromUserName = message.ToUserName,
                    CreateTime = Message.GetLongTimeNow(),
                    content = "ok"
                };
            }
            return new ReturnMessageText
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = "no"
            };
        } 
        #endregion
    }
}
