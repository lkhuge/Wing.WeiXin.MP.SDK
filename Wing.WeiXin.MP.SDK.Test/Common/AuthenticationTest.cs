using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;

namespace Wing.WeiXin.MP.SDK.Test.Common
{
    
    
    /// <summary>
    ///这是 AuthenticationTest 的测试类，旨在
    ///包含所有 AuthenticationTest 单元测试
    ///</summary>
    [TestClass]
    public class AuthenticationTest : BaseTest
    {
        #region CheckSignature 的测试
        /// <summary>
        /// CheckSignature 的测试
        ///</summary>
        [TestMethod]
        public void CheckSignatureTest()
        {
            Assert.IsTrue(Authentication.CheckSignature(requestRight));
            Assert.IsFalse(Authentication.CheckSignature(requestError));
        } 
        #endregion

        #region CheckMessage 的测试
        /// <summary>
        /// CheckMessage 的测试
        ///</summary>
        [TestMethod]
        public void CheckMessageTest()
        {
            Assert.IsTrue(Authentication.CheckMessage(requestRight));
            Assert.IsFalse(Authentication.CheckMessage(requestError));
        } 
        #endregion

        #region CheckHaveErrorMsg 的测试
        /// <summary>
        /// CheckHaveErrorMsg 的测试
        ///</summary>
        [TestMethod]
        public void CheckHaveErrorMsgTest()
        {
            const string jsonValueRight = "{\"groupid\": 102 }";
            const string jsonValueError = "{\"errcode\":40003,\"errmsg\":\"invalid openid\"}";
            Assert.IsNull(Authentication.CheckHaveErrorMsg(jsonValueRight));
            Assert.IsNotNull(Authentication.CheckHaveErrorMsg(jsonValueError));
        } 
        #endregion
    }
}
