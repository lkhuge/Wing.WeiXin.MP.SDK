using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message
{
    /// <summary>
    /// 语音消息请求
    /// </summary>
    public class RequestVoice : RequestAMessage
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return GetPostData("MediaId"); }
        }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format
        {
            get { return GetPostData("Format"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.voice; }
        }

        #region 下载语音 public void DownloadVoice(WXAccount account, string pathName)
        /// <summary>
        /// 下载语音
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="pathName">下载路径加文件名</param>
        public void DownloadVoice(WXAccount account, string pathName)
        {
            new MediaController().DownLoad(account, MediaId, pathName);
        }
        #endregion
    }
}
