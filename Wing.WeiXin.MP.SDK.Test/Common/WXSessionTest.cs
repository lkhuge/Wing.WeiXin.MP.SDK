using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Test.Common
{
    [TestClass]
    public class WXSessionTest
    {
        [TestMethod]
        public void StaticStringTest()
        {
            StaticWXSession session = new StaticWXSession();
            const string user = "Test";
            const string keyString = "SS";
            const string valueString = "SSV";

            session.Set(user, keyString, keyString);

            Assert.IsTrue(session.Get<string>(user, keyString).Equals(valueString));
        }

        [TestMethod]
        public void StaticIntTest()
        {
            StaticWXSession session = new StaticWXSession();
            const string user = "Test";
            const string keyInt = "SI";
            const int valueInt = 123;

            session.Set(user, keyInt, valueInt);

            Assert.IsTrue(session.Get<int>(user, keyInt).Equals(valueInt));
        }

        [TestMethod]
        public void StaticDateTimeTest()
        {
            StaticWXSession session = new StaticWXSession();
            const string user = "Test";
            const string keyDT = "SD";
            DateTime valueDT = DateTime.Now;

            session.Set(user, keyDT, valueDT);

            Assert.IsTrue(session.Get<DateTime>(user, keyDT).Equals(valueDT));
        }

        [TestMethod]
        public void StaticObjTest()
        {
            StaticWXSession session = new StaticWXSession();
            const string user = "Test";
            const string keyObj = "SO";
            TestVClass valueObj = new TestVClass
            {
                S = "SSS",
                I = 1111,
                D = DateTime.Now,
                L = new List<string> { "123", "qwe" }
            };

            session.Set(user, keyObj, valueObj);

            Assert.IsTrue(session.Get<TestVClass>(user, keyObj).Equals(valueObj));
        }

        public class TestVClass
        {
            public string S { get; set; }
            public int I { get; set; }
            public DateTime D { get; set; }
            public List<string> L { get; set; }
        }
    }
}
