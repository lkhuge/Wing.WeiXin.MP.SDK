﻿using Wing.WeiXin.MP.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.Test.Common
{
    /// <summary>
    ///这是 AccessTokenContainerTest 的测试类，旨在
    ///包含所有 AccessTokenContainerTest 单元测试
    ///</summary>
    [TestClass]
    public class AccessTokenContainerTest : BaseTest
    {
        #region GetAccessToken 的测试
        /// <summary>
        /// GetAccessToken 的测试
        ///</summary>
        [TestMethod]
        public void GetAccessTokenTest()
        {
            Assert.IsNotNull(AccessTokenContainer.GetAccessToken("gh_7f215c8b1c91"));
        } 
        #endregion
    }
}
