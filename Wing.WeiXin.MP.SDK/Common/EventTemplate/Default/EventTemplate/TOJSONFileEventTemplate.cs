using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Default.Mapping.TransObject;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Item;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Default.EventTemplate
{
    /// <summary>
    /// 基于翻译对象JSON的文件事件模板
    /// </summary>
    public class TOJSONFileEventTemplate : FileEventTemplate
    {
        #region 从文件文本内容获取事件项列表 protected override IEnumerable<EventItem> GetEventListFromFileText(string text)
        /// <summary>
        /// 从文件文本内容获取事件项列表
        /// </summary>
        /// <param name="text">文件文本内容</param>
        /// <returns>事件项列表</returns>
        protected override IEnumerable<EventItem> GetEventListFromFileText(string text)
        {
            EventTemplateObject obj;
            try
            {
                obj = LibManager.JSONHelper.JSONDeserialize<EventTemplateObject>(text);
            }
            catch (Exception e)
            {
                throw WXException.GetInstance(String.Format("序列化JSON错误{0}说明：{1}", 
                    Environment.NewLine, e.Message), Settings.Default.SystemUsername);
            }
            List<EventItem> list = obj.GList.Select(TransObjectHelper.TransToGlobalEventItem).ToList<EventItem>();
            list.AddRange(obj.RList.Select(TransObjectHelper.TransToReceiveEventItem));

            return list;
        } 
        #endregion
    }
}
