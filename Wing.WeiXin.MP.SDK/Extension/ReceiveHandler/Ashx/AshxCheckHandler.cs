using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.ReceiveHandler.Ashx
{
    /// <summary>
    /// 测试事件处理
    /// 
    /// 测试方案：
    ///     1 全局测试
    ///       1.1 运行环境
    ///     2 配置测试
    ///       2.1 类库是否可用
    ///       2.2 是否已经初始化 （GlobalManager.IsInit）
    ///       2.3 是否存在微信公众平台账号
    ///       2.4 微信用户会话管理类是否可用
    ///     3 模拟测试
    ///       3.1 对首次验证进行测试
    ///       3.2 添加测试事件到临时事件列表进行测试
    /// </summary>
    public class AshxCheckHandler : IHttpHandler
    {
        /// <summary>
        /// 用于测试的微信号
        /// </summary>
        private const string TestToUserName = "TestToUserName";

        /// <summary>
        /// 用于测试的OpenID
        /// </summary>
        private const string TestFromUserName = "TestFromUserName";

        /// <summary>
        /// 用于测试的文本
        /// </summary>
        private const string TestText = "Hello World";

        /// <summary>
        /// 运行时程序集
        /// </summary>
        private static Assembly RuntimeAssembly;

        /// <summary>
        /// 初始化
        /// </summary>
        public AshxCheckHandler()
        {
            RuntimeAssembly = Assembly.GetCallingAssembly();
        }

        #region 测试项列表 private Dictionary<string, AshxCheckItem[]> CheckItemList = new Dictionary<string, AshxCheckItem[]>
        /// <summary>
        /// 测试项列表
        /// </summary>
        private readonly Dictionary<string, AshxCheckItem[]> CheckItemList = new Dictionary<string, AshxCheckItem[]>
        {
            {"全局测试", new []
            {
                new AshxCheckItem
                {
                    Text = "运行环境",
                    Check = () =>
                    {
                        object[] list = RuntimeAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);
                        if (list.Length == 0) return RuntimeAssembly.FullName;
                        DebuggableAttribute attr = (DebuggableAttribute)list[0];
                        return attr.IsJITTrackingEnabled ? "建议使用Release模式编译以获得最佳性能" : null;
                    }
                }
            }},
            {"配置测试", new []
            {
                new AshxCheckItem
                {
                    Text = "类库是否可用",
                    Check = () =>
                    {
                        if (LibManager.DateTimeHelper == null) return "DateTime工具栏不可用";
                        if (LibManager.HTTPHelper == null) return "HTTP工具栏不可用";
                        if (LibManager.JSONHelper == null) return "JSON工具栏不可用";
                        if (LibManager.SecurityHelper == null) return "Security工具栏不可用";

                        return null;
                    }
                },
                new AshxCheckItem
                {
                    Text = "是否已经初始化",
                    Check = () => GlobalManager.IsInit ? null : "未初始化完成"
                },
                new AshxCheckItem
                {
                    Text = "是否存在微信公众平台账号",
                    Check = () => !GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountList().Any() 
                        ? "未发现任何微信公众平台账号" : null
                },
                new AshxCheckItem
                {
                    Text = "微信用户会话管理类是否可用",
                    Check = () => GlobalManager.WXSessionManager == null 
                        ? "未创建微信用户会话管理类" : null
                }
            }},
            {"模拟测试", new []
            {
                new AshxCheckItem
                {
                    Text = "对首次验证进行测试",
                    Check = () =>
                    {
                        Request TestFirstRequest = new Request(
                            GetNewSignature(),
                            "1234567890", "nonce", "echostr", null);
                        Response response = new ReceiveController().Action(TestFirstRequest);

                        return response.Text.Equals(TestFirstRequest.Echostr)
                            ? null : "首次验证模拟测试异常";
                    }
                },
                new AshxCheckItem
                {
                    Text = "添加测试事件到临时事件列表进行模拟测试",
                    Check = () =>
                    {
                        Request TestTextRequest = new Request(
                            GetNewSignature(),
                            "1234567890", "nonce", null, 
                            String.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>1348831860</CreateTime>
                                                <MsgType><![CDATA[text]]></MsgType>
                                                <Content><![CDATA[{2}]]></Content>
                                                <MsgId>{3}</MsgId>
                                            </xml>", TestToUserName, TestFromUserName, TestText, 
                                            LibManager.DateTimeHelper.GetLongTimeByDateTime(DateTime.Now)));

                        GlobalManager.EventManager.AddTempReceiveEvent(
                            TestToUserName, 
                            TestFromUserName,
                            re => re.GetTextResponse(TestText));
                        Response response = new ReceiveController().Action(TestTextRequest);
                        return response == null || response.Text.Equals(TestTextRequest.GetTextResponse(TestText).Text) 
                            ? null : "临时事件模拟测试异常";
                    }
                }
            }},
        };
        #endregion

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(GetResponse());
        }
        #endregion

        #region 获取响应字符串 private string GetResponse()
        /// <summary>
        /// 获取响应字符串
        /// </summary>
        /// <returns>响应字符串</returns>
        private string GetResponse()
        {
            return String.Join(
                Environment.NewLine,
                CheckItemList.Select(l => GetListResult(l.Key, l.Value)));
        }
        #endregion

        #region 获取列表测试结果 private string GetListResult(string key, IEnumerable<AshxCheckItem> list)
        /// <summary>
        /// 获取列表测试结果
        /// </summary>
        /// <param name="key">类别名称</param>
        /// <param name="list">测试列表</param>
        /// <returns>列表测试结果</returns>
        private string GetListResult(string key, IEnumerable<AshxCheckItem> list)
        {
            return String.Format("<h1>{0}</h1>", key) + Environment.NewLine +
                String.Join(Environment.NewLine, list.Select(GetResult));
        }
        #endregion

        #region 获取测试结果 private string GetResult(AshxCheckItem item)
        /// <summary>
        /// 获取测试结果
        /// </summary>
        /// <param name="item">测试项</param>
        /// <returns>测试结果</returns>
        private string GetResult(AshxCheckItem item)
        {
            string result = item.Check();
            return String.Format("<h4>{0}</h4>", item.Text) + Environment.NewLine +
                String.Format("<span style='color:{1}'>{0}</span>",
                    result ?? "通过",
                    result == null ? "green" : "red");
        }
        #endregion

        #region 获取新的Signature private static string GetNewSignature()
        /// <summary>
        /// 获取新的Signature
        /// </summary>
        /// <returns>新的Signature</returns>
        private static string GetNewSignature()
        {
            string[] arr = new[] 
            { 
                GlobalManager.ConfigManager.BaseConfig.Token, 
                "1234567890", 
                "nonce"
            }.OrderBy(z => z).ToArray();

            return LibManager.SecurityHelper.SHA1_Encrypt(string.Join("", arr));
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get; private set; }

        /// <summary>
        /// 测试项
        /// </summary>
        private class AshxCheckItem
        {
            /// <summary>
            /// 测试说明
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 测试方法
            /// 如果通过则返回null
            /// </summary>
            public Func<string> Check { get; set; }
        }
    }
}
