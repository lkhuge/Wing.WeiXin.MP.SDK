using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.DKF
{
    /// <summary>
    /// 客服基本信息列表
    /// </summary>
    public class DKFList
    {
        /// <summary>
        /// 客服基本信息列表
        /// </summary>
        public List<DKFInfo> kf_list { get; set; }

        /// <summary>
        /// 客服基本信息
        /// </summary>
        public class DKFInfo
        {
            /// <summary>
            /// 客服账号@微信别名
            /// 微信别名如有修改，旧账号返回旧的微信别名，新增的账号返回新的微信别名
            /// </summary>
            public string kf_account { get; set; }

            /// <summary>
            /// 客服昵称
            /// </summary>
            public string kf_nick { get; set; }

            /// <summary>
            /// 客服工号
            /// </summary>
            public string kf_id { get; set; }
        }
    }
}
