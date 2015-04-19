using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class ReceiveControllerTest : BaseTest
    {
        [TestMethod]
        public void ActionTest()
        {
            GlobalManager.EventManager.AddReceiveEvent<RequestText>("Event1", "$", r => r.Request.GetTextResponse("qwe"));
            Response result = new ReceiveController().Action(messageText, false);
            Assert.IsNotNull(result);
        } 
    }
}
