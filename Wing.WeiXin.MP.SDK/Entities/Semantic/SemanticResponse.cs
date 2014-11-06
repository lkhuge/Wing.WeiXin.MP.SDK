using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Semantic
{
    /// <summary>
    /// 语义响应
    /// </summary>
    public abstract class SemanticResponse
    {
        /// <summary>
        /// 用于标识用户请求后的状态
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 用户的输入字符串
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// 服务的全局类别 id，详见 4 垂直服务协议定义
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 部分类别的结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 部分类别的结果 html5 展示，目前不支持
        /// </summary>
        public string answer { get; set; }

        /// <summary>
        /// 特殊回复说明
        /// </summary>
        public string text { get; set; }
    }
}
