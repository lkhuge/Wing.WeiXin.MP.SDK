using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Test.Controller
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
            GlobalManager.EventManager.AddReceiveEvent<RequestText>("Event1", "gh_7f215c8b1c91", r => EntityBuilder.GetMessageText(r, "asdf"));
            GlobalManager.EventManager.AddReceiveEvent<RequestEventClick>("Event2", "gh_7f215c8b1c91", E2);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i ++)
            {
                Response result = new ReceiveController().Action(messageText);
            }
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);
//            Response result = new ReceiveController().Action(messageText);
//            Assert.IsNotNull(result);
        } 
        #endregion

        public Response E2(RequestEventClick r)
        {
            return EntityBuilder.GetMessageText(r, "qwe");
        }
    }
}
