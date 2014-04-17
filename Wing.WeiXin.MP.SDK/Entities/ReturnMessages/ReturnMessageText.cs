using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageText : BaseReturnMessage
    {
        #region 回复的消息内容
        /// <summary>
        /// 回复的消息内容（设置消息请使用此变量）
        /// </summary>
        [XmlIgnore]
        public string content
        {
            get { return Content.Value; }
            set { Content = new XmlDocument().CreateCDataSection(value); }
        }

        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public XmlNode Content { get; set; } 
        #endregion

        #region 实例化空数据回复文本消息 public ReturnMessageText()
        /// <summary>
        /// 实例化空数据回复文本消息
        /// </summary>
        public ReturnMessageText()
        {
            MsgType = "text";
        } 
        #endregion

        #region 根据回复的消息内容和接收的实体实例化 public ReturnMessageText(string content, BaseEntity entity) : base(entity)
        /// <summary>
        /// 根据回复的消息内容和接收的实体实例化
        /// </summary>
        /// <param name="content">回复的消息内容</param>
        /// <param name="entity">接收的实体</param>
        public ReturnMessageText(string content, BaseEntity entity)
            : base(entity)
        {
            if(String.IsNullOrEmpty(content)) throw new ArgumentNullException("content");
            MsgType = "text";
            this.content = content;
        }
        #endregion
    }
}
