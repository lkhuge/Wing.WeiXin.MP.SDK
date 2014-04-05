using Wing.WeiXin.MP.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Exception;

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
    }
}
