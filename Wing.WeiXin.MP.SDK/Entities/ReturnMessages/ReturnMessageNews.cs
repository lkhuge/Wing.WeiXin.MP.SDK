using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复图文消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageNews : BaseReturnMessage
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }

        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// </summary>
        public Articles Articles { get; set; }

        #region 实例化空数据回复图文消息 public ReturnMessageNews()
        /// <summary>
        /// 实例化空数据回复图文消息
        /// </summary>
        public ReturnMessageNews()
        {
            MsgType = "news";
        }
        #endregion
    }
}
