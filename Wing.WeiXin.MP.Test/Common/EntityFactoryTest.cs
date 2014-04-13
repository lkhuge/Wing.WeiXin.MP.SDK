using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.EventHandle;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.Test.Common
{
    /// <summary>
    ///这是 EntityFactoryTest 的测试类，旨在
    ///包含所有 EntityFactoryTest 单元测试
    ///</summary>
    [TestClass]
    public class EntityFactoryTest : BaseTest
    {
        #region RequestHandle 的测试
        /// <summary>
        /// RequestHandle 的测试
        ///</summary>
        [TestMethod]
        public void RequestHandleTest()
        {
            EventHandleManager.Init(new EntityHandler
            {
                MessageTextHandler = new EntityHandler.CustomEntityHandler<MessageText>[]
                {
                    GlobalEntityEvent
                }
            });
            Assert.AreEqual(EntityFactory.RequestHandle(requestRight).Text, requestRight.echostr);
            try
            {
                EntityFactory.RequestHandle(requestError);
                Assert.Fail("应该发生异常");
            }
            catch (FirstInvalidMessageException)
            {
            }
            Assert.IsNotNull(EntityFactory.RequestHandle(messageText));
        } 
        #endregion

        #region 全局事件 public IReturn GlobalEntityEvent(MessageText message)
        /// <summary>
        /// 全局事件
        /// </summary>
        /// <returns></returns>
        public IReturn GlobalEntityEvent(MessageText message)
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
