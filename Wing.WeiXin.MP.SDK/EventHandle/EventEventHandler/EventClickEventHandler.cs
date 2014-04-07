using System;
using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Events;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.EventHandle.EventEventHandler
{
    /// <summary>
    /// 点击菜单拉取消息时的事件处理
    /// </summary>
    public class EventClickEventHandler : EntityEventHandler<EventClick>
    {
        #region 根据Key回复委托 public delegate IReturn ReturnByKeyHandler(EventClick eventClick);
        /// <summary>
        /// 根据Key回复委托
        /// </summary>
        /// <param name="eventClick">自定义菜单事件（点击菜单拉取消息时的事件推送）</param>
        /// <returns>回复对象</returns>
        public delegate IReturn ReturnByKeyHandler(EventClick eventClick);
        #endregion

        #region 根据Key回复委托列表 private static Dictionary<string, ReturnByKeyHandler> ReturnByKeyHandlerList;
        /// <summary>
        /// 根据Key回复委托列表
        /// </summary>
        private static readonly Dictionary<string, ReturnByKeyHandler> ReturnByKeyHandlerList
            = new Dictionary<string, ReturnByKeyHandler>();
        #endregion

        #region 基础事件处理 protected override IReturn BaseEntityEvent(EventClick entity)
        /// <summary>
        /// 基础事件处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected override IReturn BaseEntityEvent(EventClick entity)
        {
            return new ReturnMessageText
            {
                ToUserName = entity.FromUserName,
                FromUserName = entity.ToUserName,
                CreateTime = Message.GetLongTimeNow(),
                content = String.Format("事件:自定义菜单事件（点击菜单拉取消息时的事件推送）/n事件KEY值：{0}", entity.EventKey)
            };
        }
        #endregion

        #region 添加根据Key回复委托 public static void AddReturnByKeyHandler(string key, ReturnByKeyHandler hander, bool isFinish = false)
        /// <summary>
        /// 添加根据Key回复委托
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="hander">自动回复委托</param>
        /// <param name="isFinish">是否完成添加</param>
        public static void AddReturnByKeyHandler(string key, ReturnByKeyHandler hander, bool isFinish = false)
        {
            ReturnByKeyHandlerList[key] = hander;
            if (isFinish) EntityEvent.EntityEvent = ReturnByKeyHandlerTemp;
        }
        #endregion

        #region 临时根据Key回复事件 private static IReturn ReturnByKeyHandlerTemp(EventClick eventClick)
        /// <summary>
        /// 临时根据Key回复事件
        /// </summary>
        /// <param name="eventClick">自定义菜单事件（点击菜单拉取消息时的事件推送）</param>
        /// <returns>回复对象</returns>
        private static IReturn ReturnByKeyHandlerTemp(EventClick eventClick)
        {
            string key = eventClick.EventKey;
            if (ReturnByKeyHandlerList.ContainsKey(key))
            {
                return ReturnByKeyHandlerList[key](eventClick);
            }
            throw new NoResponseException("无对应Key的事件");
        } 
        #endregion
    }
}
