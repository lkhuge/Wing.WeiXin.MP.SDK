using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.Semantic
{
    /// <summary>
    /// 语义请求
    /// </summary>
    public class SemanticRequest
    {
        /// <summary>
        /// 输入文本串
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// 需要使用的服务类型，多个用“，”隔开，不能为空
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// 纬度坐标，与经度同时传入；
        /// 与城市二选一传入
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 经度坐标，与纬度同时传入；
        /// 与城市二选一传入
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 城市名称，与经纬度二选一传入
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区域名称，在城市存在的情况下可省；
        /// 与经纬度二选一传入
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// 公众号唯一标识，用于区分公众号开发者
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户唯一id（非开发者id），
        /// 用户区分公众号下的不同用户（建议填入用户openid），
        /// 如果为空，则无法使用上下文理解功能。
        /// appid和uid同时存在的情况下，才可以使用上下文理解功能。
        /// </summary>
        public string uid { get; set; }
    }
}
