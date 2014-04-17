using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

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
        public List<item> Articles { get; set; }

        #region 实例化空数据回复图文消息 public ReturnMessageNews()
        /// <summary>
        /// 实例化空数据回复图文消息
        /// </summary>
        public ReturnMessageNews()
        {
            MsgType = "news";
        }
        #endregion

        #region 根据多条图文消息信息和接收的实体实例化 public ReturnMessageNews(List<item> item, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据多条图文消息信息和接收的实体实例化
        /// </summary>
        /// <param name="item">多条图文消息信息</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageNews(List<item> item, BaseEntity entity)
            : base(entity)
        {
            if (item == null) throw new ArgumentNullException("item");
            MsgType = "news";
            ArticleCount = item.Count;
            Articles = item;
        }
        #endregion

        #region 图文消息信息 public class item
        /// <summary>
        /// 图文消息信息
        /// </summary>
        public class item
        {
            /// <summary>
            /// 图文消息标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 图文消息描述
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            /// 点击图文消息跳转链接
            /// </summary>
            public string Url { get; set; }
        } 
        #endregion
    }
}
