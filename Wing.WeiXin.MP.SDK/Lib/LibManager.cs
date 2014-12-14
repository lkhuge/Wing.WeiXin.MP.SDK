using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Net;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// 类库管理器
    /// </summary>
    public static class LibManager
    {
        /// <summary>
        /// HTTP工具类
        /// </summary>
        public static IHTTPHelper HTTPHelper { get; set; }

        /// <summary>
        /// JSON工具类
        /// </summary>
        public static IJSONHelper JSONHelper { get; set; }

        /// <summary>
        /// DateTime工具类
        /// </summary>
        public static IDateTimeHelper DateTimeHelper { get; set; }

        /// <summary>
        /// 安全工具类
        /// </summary>
        public static ISecurityHelper SecurityHelper { get; set; }

        #region 初始化默认类库 public static void InitLibByDefault()
        /// <summary>
        /// 初始化默认类库
        /// </summary>
        public static void InitLibByDefault()
        {
            HTTPHelper = new DefaultHTTPHelper();
            JSONHelper = new DefaultJSONHelper();
            DateTimeHelper = new DefaultDateTimeHelper();
            SecurityHelper = new DefaultSecurityHelper();
        } 
        #endregion
    }
}
