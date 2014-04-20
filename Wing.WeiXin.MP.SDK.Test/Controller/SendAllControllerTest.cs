using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Media media = UploadNewsTest("gh_7f215c8b1c91", news);
            SendAllReturnMessage re1 =
                SendAllByGroupTest("gh_7f215c8b1c91", new SendAllByGroup("105", media.media_id));
            Assert.AreEqual(re1.errcode, 0);
            SendAllReturnMessage re2 =
                SendAllByOpenIDListTest("gh_7f215c8b1c91",
                new SendAllByOpenIDList(new List<string> { "orImOuC33jQiJFrVelQGGTmwPSFE" }, media.media_id));
            Assert.AreEqual(re2.errcode, 0);
            DeleteSendAllTest("gh_7f215c8b1c91", new SendAllDelete(re2.msg_id));
        } 
        #endregion

        #region 上传图文消息素材测试 public Media UploadNewsTest(string weixinMPID, SendAllMessageNews news)
        /// <summary>
        /// 上传图文消息素材测试
        /// </summary>
        public Media UploadNewsTest(string weixinMPID, SendAllMessageNews news)
        {
            try
            {
                return SendAllController.UploadNews(weixinMPID, news);
            }
            catch (WXException e)
            {
                Assert.Fail(e.Message);
            }
            return null;
        } 
        #endregion

        #region 根据分组进行群发测试 public SendAllReturnMessage SendAllByGroupTest(string weixinMPID, SendAllByGroup group)
        /// <summary>
        /// 根据分组进行群发测试
        /// </summary>
        public SendAllReturnMessage SendAllByGroupTest(string weixinMPID, SendAllByGroup group)
        {
            return SendAllController.SendAllByGroup(weixinMPID, group);
        } 
        #endregion

        #region 根据OpenID列表群发测试 public SendAllReturnMessage SendAllByOpenIDListTest(string weixinMPID, SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发测试
        /// </summary>
        public SendAllReturnMessage SendAllByOpenIDListTest(string weixinMPID, SendAllByOpenIDList openIDList)
        {
            return SendAllController.SendAllByOpenIDList(weixinMPID, openIDList);
        } 
        #endregion

        #region 删除群发 public void DeleteSendAllTest(string weixinMPID, SendAllDelete delete)
        /// <summary>
        /// 删除群发
        /// </summary>
        public void DeleteSendAllTest(string weixinMPID, SendAllDelete delete)
        {
            ErrorMsg msg = SendAllController.DeleteSendAll(weixinMPID, delete);
            Assert.AreEqual(msg.errcode, "0");
        } 
        #endregion
    }
}
