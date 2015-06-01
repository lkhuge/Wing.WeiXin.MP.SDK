using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class CSControllerTest : BaseTest
    {
        [TestMethod]
        public void SendCSMessageTest()
        {
            CSController CSController = GlobalManager.FunctionManager.CS;

            ErrorMsg e = CSController.SendCSMessage(account, csMessageText);

            Assert.AreEqual(e.errcode, "0");
        } 
    }
}
