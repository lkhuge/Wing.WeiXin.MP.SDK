using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Config.Event
{
    /// <summary>
    /// 事件信息配置信息
    /// </summary>
    public class EventInfoConfigInfo
    {
        /// <summary>
        /// 事件项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 事件项目是否生效
        /// </summary>
        public bool IsAction { get; set; }
    }
}
