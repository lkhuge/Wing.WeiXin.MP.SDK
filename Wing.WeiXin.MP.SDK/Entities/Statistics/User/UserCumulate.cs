using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Statistics.User
{
    /// <summary>
    /// 累计用户数据
    /// </summary>
    public class UserCumulate
    {
        /// <summary>
        /// 累计用户数据信息列表
        /// </summary>
        public List<UserCumulateItem> list { get; set; }

        /// <summary>
        /// 累计用户数据信息
        /// </summary>
        public class UserCumulateItem
        {
            /// <summary>
            /// 数据的日期
            /// </summary>
            public string ref_date { get; set; }

            /// <summary>
            /// 总用户量
            /// </summary>
            public string cumulate_user { get; set; }
        }
    }
}
