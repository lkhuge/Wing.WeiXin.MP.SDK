using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Config.Handler
{
    /// <summary>
    /// Handler配置信息
    /// </summary>
    public class HandlerConfigInfo
    {
        /// <summary>
        /// Handler标记
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 默认Handler
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Handler信息列表
        /// </summary>
        public List<HandlerInfoConfigInfo> HandlerInfoList { get; set; }
    }
}
