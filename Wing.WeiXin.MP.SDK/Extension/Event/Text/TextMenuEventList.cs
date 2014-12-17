using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Text
{
    /// <summary>
    /// 文本菜单事件列表
    /// </summary>
    public class TextMenuEventList : IEventBuilder<RequestText>
    {
        /// <summary>
        /// 菜单标示
        /// </summary>
        private readonly string menuSign;

        /// <summary>
        /// 菜单选项错误信息
        /// 为空则表示重复提示
        /// </summary>
        public Response MenuErrorMessage { get; set; }

        #region 根据菜单标示和文本菜单事件实例化文本菜单事件列表 public TextMenuEventList(string menuSign)
        /// <summary>
        /// 根据菜单标示和文本菜单事件实例化文本菜单事件列表
        /// </summary>
        /// <param name="menuSign">菜单标示</param>
        public TextMenuEventList(string menuSign)
        {
            if (GlobalManager.WXSessionManager == null) throw new Exception("未初始化微信会话管理类");
            this.menuSign = menuSign;
        } 
        #endregion

        #region 开始捕获菜单事件 public static void Start(string openID, string menuSign, TextMenuItem item)
        /// <summary>
        /// 开始捕获菜单事件
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        /// <param name="item">菜单对象</param>
        public static void Start(string openID, string menuSign, TextMenuItem item)
        {
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListHead + menuSign, item);
        } 
        #endregion

        #region 开始捕获菜单事件并且显示菜单 public static Response Start(string openID, string menuSign, TextMenuItem item, Request request)
        /// <summary>
        /// 开始捕获菜单事件并且显示菜单
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        /// <param name="item">菜单对象</param>
        /// <param name="request">请求对象</param>
        public static Response Start(string openID, string menuSign, TextMenuItem item, Request request)
        {
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListHead + menuSign, item);

            return ShowMeun(item, request);
        }
        #endregion

        #region 停止捕获菜单事件 public static void Stop(string openID, string menuSign)
        /// <summary>
        /// 停止捕获菜单事件
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        public static void Stop(string openID, string menuSign)
        {
            GlobalManager.WXSessionManager.Delete(openID, Settings.Default.TextMenuEventListHead + menuSign);
        }
        #endregion

        #region 菜单后退 public static void GoBack(string openID, string menuSign)
        /// <summary>
        /// 菜单后退
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        public static void GoBack(string openID, string menuSign)
        {
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListHead + menuSign);
            if (itemObj == null) throw MessageException.GetInstance("菜单未设置");
            TextMenuItem item = (TextMenuItem) itemObj;
            if (item.ParentItem == null) return;
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListHead + menuSign, item.ParentItem);
        } 
        #endregion

        #region 菜单后退并且显示菜单 public static Response GoBack(string openID, string menuSign, Request request)
        /// <summary>
        /// 菜单后退并且显示菜单
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        /// <param name="request">请求对象</param>
        public static Response GoBack(string openID, string menuSign, Request request)
        {
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListHead + menuSign);
            if (itemObj == null) throw MessageException.GetInstance("菜单未设置");
            TextMenuItem item = (TextMenuItem)itemObj;
            if (item.ParentItem == null) return ShowMeun(item, request);
            GlobalManager.WXSessionManager.Set(openID, Settings.Default.TextMenuEventListHead + menuSign, item.ParentItem);

            return ShowMeun(item.ParentItem, request);
        }
        #endregion

        #region 获取菜单信息 public static Response ShowMeun(string openID, string menuSign, Request request)
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="openID">用户OpenID</param>
        /// <param name="menuSign">菜单标示</param>
        /// <param name="request">请求对象</param>
        /// <returns>菜单信息</returns>
        public static Response ShowMeun(string openID, string menuSign, Request request)
        {
            object itemObj = GlobalManager.WXSessionManager.Get(openID, Settings.Default.TextMenuEventListHead + menuSign);
            if (itemObj == null) throw MessageException.GetInstance("菜单未设置");

            return ShowMeun((TextMenuItem) itemObj, request);
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
            if (item.MenuFormat != null) return item.MenuFormat(item, request);

            return EntityBuilder.GetMessageText(request,
                String.Join("\n", item.SubItem.Select(i => String.Format("{0} ： {1}", i.Key, i.Text))));
        }
        #endregion

        #region 获取菜单选择错误信息 private Response GetMenuErrorMessage(TextMenuItem item, Request request)
        /// <summary>
        /// 获取菜单选择错误信息
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <param name="request">请求对象</param>
        /// <returns>菜单选择错误信息</returns>
        private Response GetMenuErrorMessage(TextMenuItem item, Request request)
        {
            if (MenuErrorMessage != null) return MenuErrorMessage;

            return ShowMeun(item, request);
        } 
        #endregion

        #region 获取文本菜单事件列表 public Func<RequestText, Response> GetEvent()
        /// <summary>
        /// 获取文本菜单事件列表
        /// </summary>
        /// <returns>文本菜单事件列表</returns>
        public Func<RequestText, Response> GetEvent()
        {
            return request =>
            {
                object itemObj = GlobalManager.WXSessionManager.Get(request.Request.FromUserName, Settings.Default.TextMenuEventListHead + menuSign);
                if (itemObj == null) return null;
                TextMenuItem item = (TextMenuItem)itemObj;
                TextMenuItem subItem = item.SubItem.FirstOrDefault(i => i.Key.Equals(request.Content));
                if (subItem == null) return GetMenuErrorMessage(item, request.Request);
                if (subItem.Event != null) return subItem.Event(request.Request);
                if (subItem.SubItem == null || subItem.SubItem.Length == 0) return ShowMeun(item, request.Request);
                subItem.ParentItem = item;
                GlobalManager.WXSessionManager.Set(request.Request.FromUserName, Settings.Default.TextMenuEventListHead + menuSign, subItem);

                return ShowMeun(subItem, request.Request);
            };
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

            /// <summary>
            /// 菜单格式化事件
            /// </summary>
            public Func<TextMenuItem, Request, Response> MenuFormat { get; set; }
        }
    }
}
