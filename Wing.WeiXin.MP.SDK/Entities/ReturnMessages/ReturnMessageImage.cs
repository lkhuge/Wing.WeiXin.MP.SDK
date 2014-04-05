using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageImage : AReturnMessage
    {
        /// <summary>
        /// 图片对象
        /// </summary>
        public Image Image { get; set; }

        #region 实例化空数据回复图片消息 public ReturnMessageImage()
        /// <summary>
        /// 实例化空数据回复图片消息
        /// </summary>
        public ReturnMessageImage()
        {
            MsgType = "image";
        }
        #endregion
    }
}
