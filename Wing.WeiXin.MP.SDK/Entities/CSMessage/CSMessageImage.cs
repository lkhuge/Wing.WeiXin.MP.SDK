using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessage.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessage
{
    /// <summary>
    /// 客服图片消息
    /// </summary>
    public class CSMessageImage : CSMessage
    {
        /// <summary>
        /// 图片消息
        /// </summary>
        public CSMessageObjectImage image { get; set; }

        #region 实例化空数据客服图片消息 public CSMessageImage()
        /// <summary>
        /// 实例化空数据客服图片消息
        /// </summary>
        public CSMessageImage()
        {
            msgtype = "image";
        } 
        #endregion
    }
}
