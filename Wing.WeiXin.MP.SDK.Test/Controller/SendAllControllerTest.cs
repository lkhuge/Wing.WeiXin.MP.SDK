﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class SendAllControllerTest : BaseTest
    {
        #region 群发全部测试 public void SendAllTest()
        /// <summary>
        /// 群发全部测试
        /// </summary>
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
        #endregion

        #region 上传图文消息素材测试 public Media UploadNewsTest()
        /// <summary>
        /// 上传图文消息素材测试
        /// </summary>
        [TestMethod]
        public Media UploadNewsTest()
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
            try
            {
                Media m = new SendAllController().UploadNews(account, news);
                return m;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return null;
        } 
        #endregion

        #region 根据分组进行群发测试 public ReturnMessage SendAllByGroupTest()
        /// <summary>
        /// 根据分组进行群发测试
        /// </summary>
        [TestMethod]
        public ReturnMessage SendAllByGroupTest(Media m)
        {
            SendAllByGroup group = new SendAllByGroupText("content", "101");
            return new SendAllController().SendAllByGroup(account, group);
        } 
        #endregion

        #region 根据OpenID列表群发测试 public ReturnMessage SendAllByOpenIDListTest(SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发测试
        /// </summary>
        public ReturnMessage SendAllByOpenIDListTest(SendAllByOpenIDList openIDList)
        {
            return new SendAllController().SendAllByOpenIDList(account, openIDList);
        } 
        #endregion

        #region 删除群发 public void DeleteSendAllTest(SendAllDelete delete)
        /// <summary>
        /// 删除群发
        /// </summary>
        public void DeleteSendAllTest(SendAllDelete delete)
        {
            ErrorMsg msg = new SendAllController().DeleteSendAll(account, delete);
            Assert.AreEqual(msg.errcode, "0");
        } 
        #endregion
    }
}
