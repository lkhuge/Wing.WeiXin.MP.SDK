using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Extension.ReceiveHandler.Ashx
{
    /// <summary>
    /// 测试事件处理
    /// 
    /// 测试方案：
    ///     1 全局测试
    ///       1.1 [Warn]运行环境
    ///       1.2 [Info]SDK版本
    ///     2 配置测试
    ///       2.1 [Error]是否已经初始化 （GlobalManager.IsInit）
    ///       2.2 [Warn]是否存在微信公众平台账号
    ///       2.3 [Warn]微信用户会话管理类是否可用
    ///       2.4 [Info]全局事件列表
    ///       2.5 [Info]普通事件列表
    ///     3 模拟测试
    ///       3.1 [Error]对首次验证进行测试
    ///       3.2 [Error]添加测试事件到临时事件列表进行测试
    ///       3.3 [Info]运行时长测试
    /// </summary>
    public class AshxCheckHandler : IHttpHandler
    {
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
                    Type = AshxCheckItem.AshxCheckItemType.Warn,
                    Text = "运行环境",
                    Check = () =>
                    {
                        object[] list = RuntimeAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);
                        if (list.Length == 0) return "无法获取编译方式（可能是未运行于32位模式造成的，对程序没有影响）";
                        DebuggableAttribute attr = (DebuggableAttribute)list[0];
                        return attr.IsJITTrackingEnabled ? "建议使用Release模式编译以获得最佳性能" : null;
                    }
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Info,
                    Text = "SDK版本",
                    Check = () => Assembly.GetExecutingAssembly().GetName().Version.ToString()
                }
            }},
            {"配置测试", new []
            {
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Error,
                    Text = "是否已经初始化",
                    Check = () => GlobalManager.IsInit ? null : "未初始化完成"
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Warn,
                    Text = "是否存在微信公众平台账号",
                    Check = () => !GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountList().Any() 
                        ? "未发现任何微信公众平台账号" : null
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Warn,
                    Text = "微信用户会话管理类是否可用",
                    Check = () => GlobalManager.WXSessionManager == null 
                        ? "未创建微信用户会话管理类（将会影响排除重复消息和文本菜单等功能）" : null
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Info,
                    Text = "全局事件列表",
                    Check = () => String.Join("</br>", 
                        GlobalManager.EventManager.GetGloablReceiveEventInfoList())
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Info,
                    Text = "普通事件列表",
                    Check = () => String.Join("</br>", 
                        GlobalManager.EventManager.GetReceiveEventInfoList())
                }
            }},
            {"模拟测试", new []
            {
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Error,
                    Text = "对首次验证进行测试",
                    Check = () =>
                    {
                        Request TestFirstRequest = RequestBuilder.GetRequest();
                        Response response = new ReceiveController().Action(TestFirstRequest);

                        return response.Text.Equals(TestFirstRequest.Echostr)
                            ? null : "首次验证模拟测试异常";
                    }
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Error,
                    Text = "添加测试事件到临时事件列表进行模拟测试",
                    Check = () =>
                    {
                        Request TestTextRequest = RequestBuilder.GetMessageText(TestText);

                        GlobalManager.EventManager.AddTempReceiveEvent(
                            RequestBuilder.TestToUserName,
                            RequestBuilder.TestFromUserName,
                            re => re.GetTextResponse(TestText));
                        Response response = new ReceiveController().Action(TestTextRequest);
                        return response == null || response.Text.Equals(TestTextRequest.GetTextResponse(TestText).Text) 
                            ? null : "临时事件模拟测试异常";
                    }
                },
                new AshxCheckItem
                {
                    Type = AshxCheckItem.AshxCheckItemType.Info,
                    Text = "运行时长测试",
                    Check = () =>
                    {
                        bool tempState = ReceiveController.IsSumRunTime;
                        ReceiveController.IsSumRunTime = true;
                        Request TestTextRequest = RequestBuilder.GetMessageText(TestText);
                        GlobalManager.EventManager.AddTempReceiveEvent(
                            RequestBuilder.TestToUserName,
                            RequestBuilder.TestFromUserName,
                            re => re.GetTextResponse(TestText));
                        Response response = new ReceiveController().Action(TestTextRequest);
                        ReceiveController.IsSumRunTime = tempState;

                        return response.RunTime + 
                            (response.RunTime > 5000 ? "ms（运行时间过长，请适当优化）" : "ms");
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
                    GetResultColor(result, item.Type));
        }
        #endregion

        #region 获取测试结果文本颜色 private string GetResultColor(string result, AshxCheckItem.AshxCheckItemType type)
        /// <summary>
        /// 获取测试结果文本颜色
        /// </summary>
        /// <param name="result">测试结果</param>
        /// <param name="type">测试类型</param>
        /// <returns>测试结果文本颜色</returns>
        private string GetResultColor(string result, AshxCheckItem.AshxCheckItemType type)
        {
            if (type == AshxCheckItem.AshxCheckItemType.Info) return "blue";
            if (result == null) return "green";
            if (type == AshxCheckItem.AshxCheckItemType.Error) return "red";
            if (type == AshxCheckItem.AshxCheckItemType.Warn) return "gold";

            return "red";
        }
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }

        /// <summary>
        /// 测试项
        /// </summary>
        private class AshxCheckItem
        {
            /// <summary>
            /// 测试项类型
            /// </summary>
            public enum AshxCheckItemType
            {
                /// <summary>
                /// 警告
                /// </summary>
                Warn,

                /// <summary>
                /// 严重错误
                /// </summary>
                Error,

                /// <summary>
                /// 信息
                /// </summary>
                Info
            }

            /// <summary>
            /// 类型
            /// </summary>
            public AshxCheckItemType Type { get; set; }

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
