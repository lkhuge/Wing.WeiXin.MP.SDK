using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    /// <summary>
    ///这是 CSControllerTest 的测试类，旨在
    ///包含所有 CSControllerTest 单元测试
    ///</summary>
    [TestClass]
    public class CSControllerTest : BaseTest
    {
        #region SendCSMessage 的测试 public void SendCSMessageTest()
        /// <summary>
        /// SendCSMessage 的测试
        ///</summary>
        [TestMethod]
        public void SendCSMessageTest()
        {
            Assert.AreEqual(CSController.SendCSMessage(AccountContainer.GetWXAccountFirstService(), csMessageText).errcode, "0");
        } 
        #endregion
    }
}
