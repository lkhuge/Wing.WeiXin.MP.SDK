using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wing.WeiXin.MP.SDK.Controller;
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
            DatetimeSemantic ds = new SemanticController().GetSemantic<DatetimeSemantic>(
                account,
                new SemanticRequest
                {
                    query = "明天",
                    category = "datetime",
                    city = "昆山",
                    uid = "orImOuC33jQiJFrVelQGGTmwPSFE"
                });
        } 
    }
}
