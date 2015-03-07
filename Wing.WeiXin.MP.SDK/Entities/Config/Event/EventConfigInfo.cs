using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Config.Event
{
    /// <summary>
    /// 事件配置信息
    /// </summary>
    public class EventConfigInfo
    {
        /// <summary>
        /// 事件信息列表
        /// </summary>
        public List<EventInfoConfigInfo> EventInfoList { get; set; }
    }
}
