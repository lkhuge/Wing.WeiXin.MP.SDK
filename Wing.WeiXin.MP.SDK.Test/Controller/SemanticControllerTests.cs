using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Entities.Semantic;

namespace Wing.WeiXin.MP.SDK.Test.Controller
{
    [TestClass]
    public class SemanticControllerTests : BaseTest
    {
        [TestMethod]
        public void GetSemanticTest()
        {
            DatetimeSemantic ds = GlobalManager.FunctionManager.Semantic.GetSemantic<DatetimeSemantic>(
                account,
                new SemanticRequest
                {
                    query = "明天",
                    category = "datetime",
                    city = "上海",
                    uid = "orImOuC33jQiJFrVelQGGTmwPSFE"
                });

            Assert.IsNotNull(ds);
        } 
    }
}
