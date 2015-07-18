using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.User.User
{
    /// <summary>
    /// 批量获取用户信息
    /// </summary>
    public class BatchWXUser
    {
        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        public List<BatchWXUserItem> user_list { get; set; }

        /// <summary>
        /// 批量获取用户信息项
        /// </summary>
        public class BatchWXUserItem
        {
            /// <summary>
            /// 实例化
            /// 默认设置语言为zh-CN
            /// </summary>
            public BatchWXUserItem()
            {
                lang = "lang";
            }

            /// <summary>
            /// 用户的标识，对当前公众号唯一
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN
            /// </summary>
            public string lang { get; set; }
        }
    }
}
