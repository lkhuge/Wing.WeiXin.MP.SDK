using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities.CSMessage.CSMessageObject;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessage
{
    /// <summary>
    /// 客服音乐消息
    /// </summary>
    public class CSMessageMusic : CSMessage
    {
        /// <summary>
        /// 音乐消息
        /// </summary>
        public CSMessageObjectMusic music { get; set; }

        #region 实例化空数据客服音乐消息 public CSMessageMusic()
        /// <summary>
        /// 实例化空数据客服音乐消息
        /// </summary>
        public CSMessageMusic()
        {
            msgtype = "music";
        } 
        #endregion
    }
}
