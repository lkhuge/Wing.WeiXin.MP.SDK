using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Config.Handler
{
    /// <summary>
    /// Handler信息配置信息
    /// </summary>
    public class HandlerInfoConfigInfo
    {
        /// <summary>
        /// Handler项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Handler项目别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Handler项目是否生效
        /// </summary>
        public bool IsAction { get; set; }
    }
}
