using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class LReceiveControllerTest : BaseTest
    {
        [TestMethod]
        public void ActionTest()
        {
            LReceiveController receiveController = new LReceiveController("qwe", "qwe", "qwe", "qwe", "");
            receiveController.EventManager.AddReceiveEvent<RequestText>("qwe", "gh_7f215c8b1c91",
                r => r.Request.GetTextResponse("qwe"));
            Response response = receiveController.Action(
                @"<xml>
                     <ToUserName><![CDATA[gh_7f215c8b1c91]]></ToUserName>
                     <FromUserName><![CDATA[orImOuC33jQiJFrVelQGGTmwPSFE]]></FromUserName> 
                     <CreateTime>1348831860</CreateTime>
                     <MsgType><![CDATA[text]]></MsgType>
                     <Content><![CDATA[0]]></Content>
                     <MsgId>12345678s9e012w3456</MsgId>
                 </xml>", null, null);

            Assert.IsNotNull(response);
        }
    }
}
