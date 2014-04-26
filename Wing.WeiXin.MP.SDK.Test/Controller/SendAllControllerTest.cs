using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class SendAllControllerTest
    {
        #region 群发全部测试 public void SendAllTest()
        /// <summary>
        /// 群发全部测试
        /// </summary>
        [TestMethod]
        public void SendAllTest()
        {
            SendAllMessageNews news = new SendAllMessageNews
            {
                articles = new List<SendAllMessageNews.Articles>
                {
                    new SendAllMessageNews.Articles
                    {
                        thumb_media_id = new MediaControllerTest().UploadTest(),
                        title = "test",
                        content = "asdfasdfwefe"
                    }
                }
            };
            Media media = UploadNewsTest( news);
            SendAllReturnMessage re1 =
                SendAllByGroupTest(new SendAllByGroup("105", media.media_id));
            Assert.AreEqual(re1.errcode, 0);
            SendAllReturnMessage re2 =
                SendAllByOpenIDListTest(
                new SendAllByOpenIDList(new List<string> { "orImOuC33jQiJFrVelQGGTmwPSFE" }, media.media_id));
            Assert.AreEqual(re2.errcode, 0);
            DeleteSendAllTest(new SendAllDelete(re2.msg_id));
        } 
        #endregion

        #region 上传图文消息素材测试 public Media UploadNewsTest(SendAllMessageNews news)
        /// <summary>
        /// 上传图文消息素材测试
        /// </summary>
        public Media UploadNewsTest(SendAllMessageNews news)
        {
            try
            {
                return SendAllController.UploadNews(AccountContainer.GetWXAccountFirstService(), news);
            }
            catch (WXException e)
            {
                Assert.Fail(e.Message);
            }
            return null;
        } 
        #endregion

        #region 根据分组进行群发测试 public SendAllReturnMessage SendAllByGroupTest(SendAllByGroup group)
        /// <summary>
        /// 根据分组进行群发测试
        /// </summary>
        public SendAllReturnMessage SendAllByGroupTest(SendAllByGroup group)
        {
            return SendAllController.SendAllByGroup(AccountContainer.GetWXAccountFirstService(), group);
        } 
        #endregion

        #region 根据OpenID列表群发测试 public SendAllReturnMessage SendAllByOpenIDListTest(SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发测试
        /// </summary>
        public SendAllReturnMessage SendAllByOpenIDListTest(SendAllByOpenIDList openIDList)
        {
            return SendAllController.SendAllByOpenIDList(AccountContainer.GetWXAccountFirstService(), openIDList);
        } 
        #endregion

        #region 删除群发 public void DeleteSendAllTest(SendAllDelete delete)
        /// <summary>
        /// 删除群发
        /// </summary>
        public void DeleteSendAllTest(SendAllDelete delete)
        {
            ErrorMsg msg = SendAllController.DeleteSendAll(AccountContainer.GetWXAccountFirstService(), delete);
            Assert.AreEqual(msg.errcode, "0");
        } 
        #endregion
    }
}
