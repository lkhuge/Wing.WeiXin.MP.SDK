using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Config.Base
{
    /// <summary>
    /// 基本配置信息
    /// </summary>
    public class BaseConfigInfo
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否为Debug模式
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// 公共平台账户列表
        /// </summary>
        public List<WXAccount> AccountList { get; set; }
    }
}
