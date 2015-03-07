using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.Config.Base;
using Wing.WeiXin.MP.SDK.Entities.Config.Event;

namespace Wing.WeiXin.MP.SDK.Entities.Config
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigInfo
    {
        /// <summary>
        /// 基本配置信息
        /// </summary>
        public BaseConfigInfo Base { get; set; }

        /// <summary>
        /// 事件配置信息
        /// </summary>
        public EventConfigInfo Event { get; set; }
    }
}
