using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.Template
{
    /// <summary>
    /// 信息模板
    /// </summary>
    public class MessageTemplate
    {
        /// <summary>
        /// OpenID
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板标号
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public Dictionary<string, MessageTemplateItem> data { get; set; }

        /// <summary>
        /// 信息模板项目
        /// </summary>
        public class MessageTemplateItem
        {
            /// <summary>
            /// 文本内容
            /// </summary>
            public string value { get; set; }

            /// <summary>
            /// 文本颜色
            /// </summary>
            public string color { get; set; }
        }
    }
}
