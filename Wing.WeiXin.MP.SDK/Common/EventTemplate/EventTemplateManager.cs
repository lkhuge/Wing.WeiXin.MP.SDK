using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Item;
using Wing.WeiXin.MP.SDK.ConfigSection.EventConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate
{
    /// <summary>
    /// 事件模板管理类
    /// </summary>
    public class EventTemplateManager
    {
        /// <summary>
        /// 事件模板列表
        /// </summary>
        private readonly Dictionary<string, IEventTemplate> EventTemplateList 
            = new Dictionary<string, IEventTemplate>();

        #region 添加事件模板 public EventTemplateManager AddEventTemplate(string name, IEventTemplate eventTemplate)
        /// <summary>
        /// 添加事件模板
        /// </summary>
        /// <param name="name">事件模板类型名称</param>
        /// <param name="eventTemplate">事件模板</param>
        /// <returns>事件模板管理类</returns>
        public EventTemplateManager AddEventTemplate(string name, IEventTemplate eventTemplate)
        {
            if (EventTemplateList.ContainsKey(name)) return this;
            EventTemplateList.Add(name, eventTemplate);

            return this;
        } 
        #endregion

        #region 设置事件 public void SetEvent(IEnumerable<EventTemplateConfigSection> eventTemplateList)
        /// <summary>
        /// 设置事件
        /// </summary>
        /// <param name="eventTemplateList">事件模板列表</param>
        public void SetEvent(IEnumerable<EventTemplateConfigSection> eventTemplateList)
        {
            foreach (EventTemplateConfigSection item in eventTemplateList)
            {
                if (!EventTemplateList.ContainsKey(item.Type))
                    throw WXException.GetInstance(String.Format("未发现类型为{0}的事件模板", 
                        item.Type), Settings.Default.SystemUsername);
                IEnumerable<EventItem> eventItemList = EventTemplateList[item.Type]
                    .GetEventList(item.Path);
                foreach (EventItem eventItem in eventItemList)
                {
                    if (eventItem.GetType() == typeof (GlobalEventItem))
                    {
                        GlobalEventItem globalEventItem = (GlobalEventItem) eventItem;
                        GlobalManager.EventManager.AddGloablReceiveEvent(
                            globalEventItem.EventName,
                            globalEventItem.ToUserName, 
                            globalEventItem.Action);
                        continue;
                    }
                    if (eventItem.GetType() == typeof(ReceiveEventItem))
                    {
                        ReceiveEventItem receiveEventItem = (ReceiveEventItem)eventItem;
                        GlobalManager.EventManager.AddReceiveEvent(
                            receiveEventItem.Type,
                            receiveEventItem.EventName,
                            receiveEventItem.ToUserName,
                            receiveEventItem.Action);
                    }
                }
            }
        } 
        #endregion
    }
}
