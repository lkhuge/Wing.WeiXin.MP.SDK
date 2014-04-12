using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessages.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服视频消息
    /// </summary>
    public class CSMessageVideo : CSMessage
    {
        /// <summary>
        /// 视频消息
        /// </summary>
        public CSMessageObjectVideo video { get; set; }

        #region 实例化空数据客服视频消息 public CSMessageVideo()
        /// <summary>
        /// 实例化空数据客服视频消息
        /// </summary>
        public CSMessageVideo()
        {
            msgtype = "video";
        }
        #endregion
    }
}
