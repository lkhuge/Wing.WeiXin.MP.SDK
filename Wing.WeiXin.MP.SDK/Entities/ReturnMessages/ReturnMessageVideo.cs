using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复视频消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageVideo : BaseReturnMessage
    {
        /// <summary>
        /// 视频对象
        /// </summary>
        public Video Video { get; set; }

        #region 实例化空数据回复视频消息 public ReturnMessageVideo()
        /// <summary>
        /// 实例化空数据回复视频消息
        /// </summary>
        public ReturnMessageVideo()
        {
            MsgType = "video";
        }
        #endregion
    }
}
