using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Event.Menu;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Click
{
    /// <summary>
    /// 菜单Click事件列表
    /// </summary>
    public class ClickEventList : IEventBuilder<RequestEventClick>
    {
        /// <summary>
        /// 是否需要根据配置判断是否执行
        /// </summary>
        public bool ActionByConfig = true;

        /// <summary>
        /// 配置前缀（配置名称格式：配置前缀@消息头部）
        /// </summary>
        public string ActionNameHead = null;

        /// <summary>
        /// 事件列表
        /// </summary>
        private readonly Dictionary<string, Func<Request, Response>> eventList;

        #region 根据事件列表实例化菜单Click事件列表 public ClickEventList(Dictionary<string, Func<Request, Response>> eventList)
        /// <summary>
        /// 根据事件列表实例化菜单Click事件列表
        /// </summary>
        /// <param name="eventList">事件列表</param>
        public ClickEventList(Dictionary<string, Func<Request, Response>> eventList)
        {
            this.eventList = eventList;
        } 
        #endregion

        #region 获取事件 public Func<RequestEventClick, Response> GetEvent()
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <returns>事件</returns>
        public Func<RequestEventClick, Response> GetEvent()
        {
            return request =>
            {
                string key = request.EventKey;
                if (!eventList.ContainsKey(key)) return null;
                if (ActionByConfig && !GlobalManager.CheckEventAction(String.Format("{0}@{1}", ActionNameHead, key))) return null;

                return eventList[key](request.Request);
            };
        } 
        #endregion
    }
}
