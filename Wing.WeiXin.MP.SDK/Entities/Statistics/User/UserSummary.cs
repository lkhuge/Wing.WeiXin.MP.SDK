using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Statistics.User
{
    /// <summary>
    /// 用户增减数据
    /// </summary>
    public class UserSummary
    {
        /// <summary>
        /// 用户增减数据信息列表
        /// </summary>
        public List<UserSummaryItem> list { get; set; }

        /// <summary>
        /// 用户增减数据信息
        /// </summary>
        public class UserSummaryItem
        {
            /// <summary>
            /// 数据的日期
            /// </summary>
            public string ref_date { get; set; }

            /// <summary>
            /// 用户的渠道，数值代表的含义如下：
            /// 
            /// 0    代表其他 
            /// 30   代表扫二维码 
            /// 17   代表名片分享 
            /// 35   代表搜号码（即微信添加朋友页的搜索） 
            /// 39   代表查询微信公众帐号 
            /// 43   代表图文页右上角菜单
            /// </summary>
            public int user_source { get; set; }

            /// <summary>
            /// 新增的用户数量
            /// </summary>
            public int new_user { get; set; }
            
            /// <summary>
            /// 取消关注的用户数量，new_user减去cancel_user即为净增用户数量
            /// </summary>
            public int cancel_user { get; set; }
        }
    }
}
