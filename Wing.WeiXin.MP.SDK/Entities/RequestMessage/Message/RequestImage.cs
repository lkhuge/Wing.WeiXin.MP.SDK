using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message
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

        #region 下载图片 public void DownloadImage(WXAccount account, string pathName)
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="pathName">下载路径加文件名</param>
        public void DownloadImage(WXAccount account, string pathName)
        {
            new MaterialController().GetTemp(account, MediaId, pathName);
        }
        #endregion

        #region 下载图片（异步） public void DownloadImageAsync(WXAccount account, string pathName, Action callback)
        /// <summary>
        /// 下载图片（异步）
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="pathName">下载路径加文件名</param>
        /// <param name="callback">回调方法</param>
        public void DownloadImageAsync(WXAccount account, string pathName, Action callback)
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                DownloadImage(account, pathName);
                if (callback != null) callback();
            });
        }
        #endregion
    }
}
