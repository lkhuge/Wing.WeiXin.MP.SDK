using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Semantic
{
    /// <summary>
    /// 时间语义
    /// </summary>
    public class DatetimeSemantic : SemanticResponse
    {
        /// <summary>
        /// 时间语义项目
        /// </summary>
        public DatetimeSemanticItem semantic { get; set; }

        /// <summary>
        /// 时间语义项目
        /// </summary>
        public class DatetimeSemanticItem
        {
            /// <summary>
            /// 大类型：“ DT_SINGLE”。
            /// DT_SINGLE 又细分为两个类别： DT_ORI 和 DT_INFER。
            /// DT_ORI 是字面时间，比如：“上午九点”；
            /// DT_INFER 是推理时间，比如：“提前 5 分 钟”
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 格式： YYYY-MM-DD，默认是当天时间
            /// </summary>
            public string date { get; set; }

            /// <summary>
            /// date 的原始字符串
            /// </summary>
            public string date_ori { get; set; }

            /// <summary>
            /// 24 小时制，格式： HH:MM:SS，默认为 00:00:00
            /// </summary>
            public string time { get; set; }

            /// <summary>
            /// Time 的原始字符串
            /// </summary>
            public string time_ori { get; set; }
        }
    }
}
