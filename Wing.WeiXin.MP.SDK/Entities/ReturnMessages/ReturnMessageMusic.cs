using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复音乐消息
    /// </summary>
    [XmlRoot("xml")]
    public class ReturnMessageMusic : BaseReturnMessage
    {
        /// <summary>
        /// 音乐对象
        /// </summary>
        public Music music { get; set; }

        #region 实例化空数据回复音乐消息 public ReturnMessageMusic()
        /// <summary>
        /// 实例化空数据回复音乐消息
        /// </summary>
        public ReturnMessageMusic()
        {
            MsgType = "music";
        }
        #endregion
    }
}
