using Wing.WeiXin.MP.SDK.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.HTTP;

namespace Wing.WeiXin.MP.Test.Controller
{
    
    
    /// <summary>
    ///这是 ReceiveControllerTest 的测试类，旨在
    ///包含所有 ReceiveControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class ReceiveControllerTest : BaseTest
    {
        #region Action 的测试 public void ActionTest()
        /// <summary>
        /// Action 的测试
        ///</summary>
        [TestMethod]
        public void ActionTest()
        {
            try
            {
                ReceiveController.Action(messageText);
            }
            catch (Exception)
            {
                Assert.Fail("接收消息错误");
            }
        } 
        #endregion
    }
}
