using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.User.User
{
    /// <summary>
    /// 获取微信用户列表
    /// </summary>
    public class WXUserList
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public OpenIdList data { get; set; }

        /// <summary>
        /// 拉取列表的后一个用户的OPENID
        /// </summary>
        public string next_openid { get; set; }

        /// <summary>
        /// 用户OpenID列表
        /// </summary>
        public class OpenIdList
        {
            /// <summary>
            /// 用户OpenID列表
            /// </summary>
            public List<string> openid { get; set; }
        }
    }
}
