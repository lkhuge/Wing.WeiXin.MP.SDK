using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage
{
    /// <summary>
    /// 图片消息请求
    /// </summary>
    public class RequestImage : RequestAMessage
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl
        {
            get { return GetPostData("PicUrl"); }
        }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return GetPostData("MediaId"); }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public override ReceiveEntityType ReceiveEntityType
        {
            get { return ReceiveEntityType.image; }
        }
    }
}
