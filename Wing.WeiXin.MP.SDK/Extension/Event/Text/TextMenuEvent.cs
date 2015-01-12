using System;
using System.Collections.Generic;
using System.Linq;
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

        #region 开始捕获菜单事件并且显示菜单 public static Response Start(string openID, TextMenuItem item, Request request)
        /// <summary>
        /// 开始捕获菜单事件并且显示菜单
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="item">菜单对象</param>
        /// <param name="request">请求对象</param>
        public static Response Start(string openID, TextMenuItem item, Request request)
        {
            Start(openID, item);
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
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);
            TextMenuItem item = (TextMenuItem)itemObj;
            if (item.ParentItem == null) return;
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListSign, item.ParentItem);
        }
        #endregion

        #region 菜单后退并且显示菜单 public static Response GoBack(string openID, Request request)
        /// <summary>
        /// 菜单后退并且显示菜单
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="request">请求对象</param>
        public static Response GoBack(string openID, Request request)
        {
            if (!CheckTimeout(openID)) return null;
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);
            TextMenuItem item = (TextMenuItem)itemObj;
            if (item.ParentItem == null) return ShowMeun(item, request);
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListSign, item.ParentItem);

            return ShowMeun(item.ParentItem, request);
        }
        #endregion

        #region 获取菜单信息 public static Response ShowMeun(string openID, Request request)
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="request">请求对象</param>
        /// <returns>菜单信息</returns>
        public static Response ShowMeun(string openID, Request request)
        {
            if (!CheckTimeout(openID)) return null;
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListSign);
            if (itemObj == null) throw WXException.GetInstance("菜单未设置", openID);

            return ShowMeun((TextMenuItem)itemObj, request);
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

            return EntityBuilder.GetMessageText(request,
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
            object dtObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListExpiresSign);
            if (dtObj == null) return true;
            if (DateTime.Now < (DateTime)dtObj) return true;
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

        #region 设置文本菜单事件列表 public static void SetEvent(string toUserName)
        /// <summary>
        /// 设置文本菜单事件列表
        /// </summary>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        public static void SetEvent(string toUserName)
        {
            GlobalManager.EventManager.AddSystemReceiveEvent(toUserName, request =>
            {
                if (request.MsgType != ReceiveEntityType.text) return null;
                object itemObj = GlobalManager.WXSessionManager.Get(request.FromUserName, Settings.Default.TextMenuEventListSign);
                if (itemObj == null) return null;
                if (!CheckTimeout(request.FromUserName)) return null;
                TextMenuItem item = (TextMenuItem)itemObj;
                TextMenuItem subItem = item.SubItem.FirstOrDefault(i => i.Key.Equals(request.GetPostData("Content")));
                if (subItem == null) return GetMenuErrorMessage(item, request);
                if (subItem.Event != null) return subItem.Event(request);
                if (subItem.SubItem == null || subItem.SubItem.Length == 0) return ShowMeun(item, request);
                subItem.ParentItem = item;
                GlobalManager.WXSessionManager.Set(request.FromUserName, Settings.Default.TextMenuEventListSign, subItem);
                GlobalManager.WXSessionManager.Set(request.FromUserName, Settings.Default.TextMenuEventListExpiresSign, DateTime.Now.AddSeconds(ExpiresSecond));
                return ShowMeun(subItem, request);
            });
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
            /// </summary>
            public Func<Request, Response> Event { get; set; }

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
