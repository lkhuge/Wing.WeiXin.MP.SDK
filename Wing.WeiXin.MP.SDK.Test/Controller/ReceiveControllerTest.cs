﻿using System;
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
//            GlobalManager.EventManager.AddGloablReceiveEvent("Event1",null, r =>
//            {
//                if (r.MsgType == ReceiveEntityType.text &&
//                    RequestAMessage.GetRequestAMessage<RequestText>(r).Content.Equals("test1"))
//                {
//                    return EntityBuilder.GetMessageTCS(r);
//                }
//                return null;
//            });
//            GlobalManager.EventManager.AddGloablReceiveEvent("Event2",null, r => EntityBuilder.GetMessageFromFriend(r, "http://huwing.vicp.cc/Receive"));
//            GlobalManager.EventManager.AddReceiveEvent<RequestText>("Event1", "gh_7f215c8b1c91", r => EntityBuilder.GetMessageText(r.Request, "qwe"));
//            GlobalManager.EventManager.AddReceiveEvent<RequestEventClick>("Event2", "gh_7f215c8b1c91", E2);
//            Stopwatch sw = new Stopwatch();
//            sw.Start();
//            for (int i = 0; i < 10000; i ++)
//            {
//                new ReceiveController().Action(messageText);
//            }
//            sw.Stop();
//            Debug.WriteLine(sw.ElapsedMilliseconds);
            Response result = new ReceiveController().Action(messageText, false);
            Assert.IsNotNull(result);
        } 
    }
}
