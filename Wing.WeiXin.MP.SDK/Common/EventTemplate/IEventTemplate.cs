using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common.EventTemplate.Item;

namespace Wing.WeiXin.MP.SDK.Common.EventTemplate
{
    /// <summary>
    /// 事件模板接口
    /// </summary>
    public interface IEventTemplate
    {
        /// <summary>
        /// 获取事件项列表
        /// </summary>
        /// <param name="path">事件模版路径</param>
        /// <returns>事件项列表</returns>
        IEnumerable<EventItem> GetEventList(string path);
    }
}
