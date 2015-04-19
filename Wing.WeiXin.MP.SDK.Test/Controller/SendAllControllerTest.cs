using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Material;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class SendAllControllerTest : BaseTest
    {
        private readonly SendAllController SendAllController = GlobalManager.FunctionManager.SendAllController;

        [TestMethod]
        public void SendAllTest()
        {
            Media media = UploadNewsTest();
            ReturnMessage re1 =
                SendAllByGroupTest(media);
            Assert.AreEqual(re1.errcode, 0);
            ReturnMessage re2 =
                SendAllByOpenIDListTest(
                new SendAllByOpenIDListText("content", new List<string> { "orImOuC33jQiJFrVelQGGTmwPSFE" }));
            Assert.AreEqual(re2.errcode, 0);
            DeleteSendAllTest(new SendAllDelete(re2.msg_id));
        } 

        [TestMethod]
        public Media UploadNewsTest()
        {
            SendAllMessageNews news = new SendAllMessageNews
            {
                articles = new List<SendAllMessageNews.Articles>
                {
                    new SendAllMessageNews.Articles
                    {
                        thumb_media_id = "",
                        title = "test",
                        content = "asdfasdfwefe"
                    }
                }
            }; 
            try
            {
                Media m = SendAllController.UploadNews(account, news);
                return m;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return null;
        } 

        [TestMethod]
        public ReturnMessage SendAllByGroupTest(Media m)
        {
            SendAllByGroup group = new SendAllByGroupText("content", "101");
            return SendAllController.SendAllByGroup(account, group);
        } 

        public ReturnMessage SendAllByOpenIDListTest(SendAllByOpenIDList openIDList)
        {
            return SendAllController.SendAllByOpenIDList(account, openIDList);
        } 

        public void DeleteSendAllTest(SendAllDelete delete)
        {
            ErrorMsg msg = SendAllController.DeleteSendAll(account, delete);
            Assert.AreEqual(msg.errcode, "0");
        } 
    }
}
