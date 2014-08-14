using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
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
            EventManager em = new EventManager();
            em.AddReceiveEvent("Event1", false, "gh_7f215c8b1c91", ReceiveEntityType.text, r => EntityBuilder.GetMessageText(r, "asdf"));
            GlobalManager.InitEvent(em);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i ++)
            {
                Response result = new ReceiveController().Action(messageText);
            }
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);
//            Response result = new ReceiveController().Action(messageText);
        } 
        #endregion
    }
}
