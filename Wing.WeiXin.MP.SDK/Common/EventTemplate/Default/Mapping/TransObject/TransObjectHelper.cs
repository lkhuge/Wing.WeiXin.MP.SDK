using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Item;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate.Default.Mapping.TransObject
{
    /// <summary>
    /// 翻译对象工具类
    /// </summary>
    public static class TransObjectHelper
    {
        #region 翻译到全局事件项 public static GlobalEventItem TransToGlobalEventItem(EventTemplateObject.EventTemplateObjectItem item)
        /// <summary>
        /// 翻译到全局事件项
        /// </summary>
        /// <param name="item">事件模板对象项</param>
        /// <returns>全局事件项</returns>
        public static GlobalEventItem TransToGlobalEventItem(EventTemplateObject.EventTemplateObjectItem item)
        {
            return new GlobalEventItem
            {
                EventName = item.Name,
                ToUserName = item.ToUserName,
                Action = request => request.GetTextResponse("qwe")
            };
        } 
        #endregion

        #region 翻译到非全局事件项 public static ReceiveEventItem TransToReceiveEventItem(EventTemplateObject.EventTemplateObjectItem item)
        /// <summary>
        /// 翻译到非全局事件项
        /// </summary>
        /// <param name="item">事件模板对象项</param>
        /// <returns>非全局事件项</returns>
        public static ReceiveEventItem TransToReceiveEventItem(EventTemplateObject.EventTemplateObjectItem item)
        {
            return null;
        } 
        #endregion
    }
}
