using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Text
{
    /// <summary>
    /// 文本菜单事件
    /// </summary>
    public static class TextMenuEvent
    {
        /// <summary>
        /// 后退事件
        /// </summary>
        public const string EventBack = "Back";

        /// <summary>
        /// 菜单格式化事件
        /// </summary>
        public static Func<TextMenuItem, Request, Response> MenuFormat { get; set; }

        /// <summary>
        /// 菜单选项错误信息
        /// 为空则表示重复提示
        /// </summary>
        public static Response MenuErrorMessage { get; set; }

        /// <summary>
        /// 有效时长（秒）
        /// 0则表示无没有限制
        /// </summary>
        public static uint ExpiresSecond { get; set; }

        /// <summary>
        /// 事件承载对象
        /// 当该对象不为空的时 菜单事件将优先从该对象中寻找 如果找不到则使用原先规则
        /// </summary>
        public static object EventObject { get; set; }

        /// <summary>
        /// 事件承载对象方法列表
        /// </summary>
        private static Dictionary<string, Func<Request, Response>> EventObjectMethodList;

        /// <summary>
        /// 事件列表
        /// </summary>
        private static readonly Dictionary<string, Func<Request, Response>> EventList =
            new Dictionary<string, Func<Request, Response>>
            {
                { EventBack, GoBack }
            };

        #region 开始捕获菜单事件 public static void Start(string openID, TextMenuItem item)
        /// <summary>
        /// 开始捕获菜单事件
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="item">菜单对象</param>
        public static void Start(string openID, TextMenuItem item)
        {
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListSign, item);
            if (ExpiresSecond == 0) return;
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListExpiresSign, DateTime.Now.AddSeconds(ExpiresSecond));
        }
        #endregion

        #region 开始捕获菜单事件并且显示菜单 public static Response Start(TextMenuItem item, Request request)
        /// <summary>
        /// 开始捕获菜单事件并且显示菜单
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <param name="request">请求对象</param>
        public static Response Start(TextMenuItem item, Request request)
        {
            Start(request.FromUserName, item);
            return ShowMeun(item, request);
        }
        #endregion

        #region 停止捕获菜单事件 public static void Stop(string openID)
        /// <summary>
        /// 停止捕获菜单事件
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        public static void Stop(string openID)
        {
            GlobalManager.WXSessionManager.Delete(openID, Settings.Default.TextMenuEventListSign);
        }
        #endregion

        #region 菜单后退 public static void GoBack(string openID)
        /// <summary>
        /// 菜单后退
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        public static void GoBack(string openID)
        {
            if (!CheckTimeout(openID)) return;
            TextMenuItem itemObj = GlobalManager.WXSessionManager.Get<TextMenuItem>(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);
            if (itemObj.ParentItem == null) return;
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListSign, itemObj.ParentItem);
        }
        #endregion

        #region 菜单后退并且显示菜单 public static Response GoBack(Request request)
        /// <summary>
        /// 菜单后退并且显示菜单
        /// </summary>
        /// <param name="request">请求对象</param>
        public static Response GoBack(Request request)
        {
            string openID = request.FromUserName;
            if (!CheckTimeout(openID)) return null;
            TextMenuItem itemObj = GlobalManager.WXSessionManager.Get<TextMenuItem>(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);
            if (itemObj.ParentItem == null) return ShowMeun(itemObj, request);
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListSign, itemObj.ParentItem);

            return ShowMeun(itemObj.ParentItem, request);
        }
        #endregion

        #region 获取菜单信息 public static Response ShowMeun(Request request)
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>菜单信息</returns>
        public static Response ShowMeun(Request request)
        {
            string openID = request.FromUserName;
            if (!CheckTimeout(openID)) return null;
            TextMenuItem itemObj = GlobalManager.WXSessionManager.Get<TextMenuItem>(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);

            return ShowMeun(itemObj, request);
        }
        #endregion

        #region 获取菜单信息 public static Response ShowMeun(TextMenuItem item, Request request)
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="item">用户菜单对象</param>
        /// <param name="request">请求对象</param>
        /// <returns>菜单信息</returns>
        public static Response ShowMeun(TextMenuItem item, Request request)
        {
            if (MenuFormat != null) return MenuFormat(item, request);

            return request.GetTextResponse(
                String.Join("\n", item.SubItem.Select(i => String.Format("{0} ： {1}", i.Key, i.Text))));
        }
        #endregion

        #region 检测菜单是否超时 public static bool CheckTimeout(string openID)
        /// <summary>
        /// 检测菜单是否超时
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <returns>菜单是否超时</returns>
        public static bool CheckTimeout(string openID)
        {
            if (ExpiresSecond == 0) return true;
            DateTime dtObj = GlobalManager.WXSessionManager.Get<DateTime>(openID, Settings.Default.TextMenuEventListExpiresSign);
            if (dtObj == default(DateTime)) return true;
            if (DateTime.Now < dtObj) return true;
            Stop(openID);
            return false;
        }
        #endregion

        #region 获取菜单选择错误信息 private static Response GetMenuErrorMessage(TextMenuItem item, Request request)
        /// <summary>
        /// 获取菜单选择错误信息
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <param name="request">请求对象</param>
        /// <returns>菜单选择错误信息</returns>
        private static Response GetMenuErrorMessage(TextMenuItem item, Request request)
        {
            if (MenuErrorMessage != null) return MenuErrorMessage;

            return ShowMeun(item, request);
        }
        #endregion

        #region 添加文本菜单事件 public static void AddEvent(string toUserName)
        /// <summary>
        /// 添加文本菜单事件
        /// </summary>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        public static void AddEvent(string toUserName)
        {
            GlobalManager.EventManager.AddSystemReceiveEvent(toUserName, request =>
            {
                if (request.MsgType != ReceiveEntityType.text) return null;
                TextMenuItem itemObj = GlobalManager.WXSessionManager.Get<TextMenuItem>(request.FromUserName, Settings.Default.TextMenuEventListSign);
                if (itemObj == null) return null;
                if (!CheckTimeout(request.FromUserName)) return null;
                TextMenuItem subItem = itemObj.SubItem.FirstOrDefault(i => i.Key.Equals(request.GetPostData("Content")));
                if (subItem == null) return GetMenuErrorMessage(itemObj, request);
                if (!String.IsNullOrEmpty(subItem.Event)) return GetEvent(subItem.Event)(request);
                if (subItem.SubItem == null || subItem.SubItem.Length == 0) return ShowMeun(itemObj, request);
                subItem.ParentItem = itemObj;
                GlobalManager.WXSessionManager.Set(request.FromUserName, Settings.Default.TextMenuEventListSign, subItem);
                GlobalManager.WXSessionManager.Set(request.FromUserName, Settings.Default.TextMenuEventListExpiresSign, DateTime.Now.AddSeconds(ExpiresSecond));
                return ShowMeun(subItem, request);
            });
        }
        #endregion

        #region 根据事件名称获取事件对象 private static Func<Request, Response> GetEvent(string eventName)
        /// <summary>
        /// 根据事件名称获取事件对象
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns>事件对象</returns>
        private static Func<Request, Response> GetEvent(string eventName)
        {
            if (EventList.ContainsKey(eventName)) return EventList[eventName];
            if (EventObject != null)
            {
                if (EventObjectMethodList == null) EventObjectMethodList = EventObject.GetType().GetMethods().ToDictionary(
                    k => k.Name,
                    v => (Func<Request, Response>)(request => (Response)v.Invoke(EventObject, new object[] { request })));
                if (EventObjectMethodList.ContainsKey(eventName)) return EventObjectMethodList[eventName];
            }
            string[] eventNameList = eventName.Split('-');
            if (eventNameList.Length != 2) 
                throw WXException.GetInstance("事件名称格式不正确（格式： 完整类名-方法名）", Settings.Default.SystemUsername);
            Type objType = GlobalManager.CallingAssembly.GetTypes()
                .FirstOrDefault(o => o.FullName.Equals(eventNameList[0]));
            if (objType == null) throw WXException.GetInstance("找不到事件对应的类", Settings.Default.SystemUsername);
            ConstructorInfo obj = objType.GetConstructors().FirstOrDefault(c => c.IsPublic && !c.GetParameters().Any());
            if (obj == null) 
                throw WXException.GetInstance("无法实例化（必须存在一个公共没有参数的构造方法）", Settings.Default.SystemUsername);
            MethodInfo methodInfo = objType.GetMethod(eventNameList[1]);
            if (methodInfo == null)
                throw WXException.GetInstance(String.Format("找不到（{0}）方法", eventNameList[1]), Settings.Default.SystemUsername);
            return request => (Response)methodInfo.Invoke(obj.Invoke(null), new object[] { request });
        } 
        #endregion

        /// <summary>
        /// 文本菜单事件
        /// </summary>
        public class TextMenuItem
        {
            /// <summary>
            /// 查询序号
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// 显示信息
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 叶子节点
            /// 执行事件
            /// 格式： 完整类名-方法名
            /// </summary>
            public string Event { get; set; }

            /// <summary>
            /// 子节点
            /// </summary>
            public TextMenuItem[] SubItem { get; set; }

            /// <summary>
            /// 父节点
            /// </summary>
            public TextMenuItem ParentItem { get; set; }
        }
    }
}
