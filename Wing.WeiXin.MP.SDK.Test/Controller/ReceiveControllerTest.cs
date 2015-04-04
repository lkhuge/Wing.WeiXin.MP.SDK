using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Extension.Event.Attributes;
using Wing.WeiXin.MP.SDK.Extension.Event.Text;

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
