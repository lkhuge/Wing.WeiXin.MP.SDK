using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.DKF
{
    /// <summary>
    /// 在线客服接待信息列表
    /// </summary>
    public class DKFOnlineList
    {
        /// <summary>
        /// 在线客服接待信息列表
        /// </summary>
        public List<DKFOnlineInfo> kf_online_list { get; set; }

        /// <summary>
        /// 在线客服接待信息
        /// </summary>
        public class DKFOnlineInfo
        {
            /// <summary>
            /// 客服账号@微信别名
            /// 微信别名如有修改，旧账号返回旧的微信别名，新增的账号返回新的微信别名
            /// </summary>
            public string kf_account { get; set; }

            /// <summary>
            /// 客服在线状态 1：pc在线，2：手机在线
            /// 若pc和手机同时在线则为 1+2=3
            /// </summary>
            public int status { get; set; }

            /// <summary>
            /// 客服工号
            /// </summary>
            public string kf_id { get; set; }

            /// <summary>
            /// 客服设置的最大自动接入数
            /// </summary>
            public int auto_accept { get; set; }

            /// <summary>
            /// 客服当前正在接待的会话数
            /// </summary>
            public int accepted_case { get; set; }
        }
    }
}
