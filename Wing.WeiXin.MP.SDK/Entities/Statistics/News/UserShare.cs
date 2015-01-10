using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Statistics.News
{
    /// <summary>
    /// 图文分享转发数据
    /// </summary>
    public class UserShare
    {
        /// <summary>
        /// 图文分享转发数据信息列表
        /// </summary>
        public List<UserShareItem> list { get; set; }

        /// <summary>
        /// 图文分享转发数据信息
        /// </summary>
        public class UserShareItem
        {
            /// <summary>
            /// 数据的日期，需在begin_date和end_date之间
            /// </summary>
            public string ref_date { get; set; }

            /// <summary>
            /// 分享的场景
            /// 
            /// 1    代表好友转发 
            /// 2    代表朋友圈 
            /// 3    代表腾讯微博 
            /// 255  代表其他
            /// </summary>
            public int share_scene { get; set; }

            /// <summary>
            /// 分享的次数
            /// </summary>
            public int share_count { get; set; }

            /// <summary>
            /// 分享的人数
            /// </summary>
            public int share_user { get; set; }
        }
    }
}
