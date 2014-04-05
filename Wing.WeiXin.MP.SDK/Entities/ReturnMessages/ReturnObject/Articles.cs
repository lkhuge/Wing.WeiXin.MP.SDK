using System.Collections.Generic;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject
{
    /// <summary>
    /// 多条图文消息信息
    /// </summary>
    public class Articles
    {
        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// </summary>
        public List<item> item { get; set; }
    }
}
