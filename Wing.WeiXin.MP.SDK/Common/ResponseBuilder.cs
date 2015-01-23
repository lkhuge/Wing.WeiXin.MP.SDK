using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.ConfigSection.EventConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 响应创建工具类
    /// </summary>
    public static class ResponseBuilder
    {
        #region 图片响应内容
        /// <summary>
        /// 图片响应内容
        /// </summary>
        private const string MessageImage =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>image</MsgType><Image><MediaId>{0}</MediaId></Image></xml>";
        #endregion

        #region 音乐响应内容
        /// <summary>
        /// 音乐响应内容
        /// </summary>
        private const string MessageMusic =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>music</MsgType><Music><Title>{0}</Title><Description>{1}</Description><MusicUrl>{2}</MusicUrl><HQMusicUrl>{3}</HQMusicUrl><ThumbMediaId>{4}</ThumbMediaId></Music></xml>";
        #endregion

        #region 图文响应内容
        /// <summary>
        /// 图文响应内容
        /// </summary>
        private const string MessageNews =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>news</MsgType><ArticleCount>{0}</ArticleCount><Articles>{1}</Articles></xml> ";
        #endregion

        #region 图文内容响应内容
        /// <summary>
        /// 图文内容响应内容
        /// </summary>
        private const string MessageNewsOne =
            "<item><Title>{0}</Title> <Description>{1}</Description><PicUrl>{2}</PicUrl><Url>{3}</Url></item>";
        #endregion

        #region 文本响应内容
        /// <summary>
        /// 文本响应内容
        /// </summary>
        private const string MessageText =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>text</MsgType><Content><![CDATA[{0}]]></Content></xml>";
        #endregion

        #region 视频响应内容
        /// <summary>
        /// 视频响应内容
        /// </summary>
        private const string MessageVideo =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>video</MsgType><Video><MediaId>{0}</MediaId><Title>{1}</Title><Description>{2}</Description></Video> </xml>";
        #endregion

        #region 语音响应内容
        /// <summary>
        /// 语音响应内容
        /// </summary>
        private const string MessageVoice =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>voice</MsgType><Voice><MediaId>{0}</MediaId></Voice></xml>";
        #endregion

        #region 转发多客服响应内容
        /// <summary>
        /// 转发多客服响应内容
        /// </summary>
        private const string MessageTCS =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>transfer_customer_service</MsgType></xml>";
        #endregion

        #region 转发指定多客服响应内容
        /// <summary>
        /// 转发指定多客服响应内容
        /// </summary>
        private const string MessageTCSForOne =
            "<xml><ToUserName>{ToUserName}</ToUserName><FromUserName>{FromUserName}</FromUserName><CreateTime>{CreateTime}</CreateTime><MsgType>transfer_customer_service</MsgType><TransInfo><KfAccount>{0}</KfAccount></TransInfo></xml>";
        #endregion

        #region 获取图片响应对象 public static Response GetMessageImage(Request request, string mediaId)
        /// <summary>
        /// 获取图片响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageImage(Request request, string mediaId)
        {
            return GetResponse(MessageImage, request, mediaId);
        }
        #endregion

        #region 获取音乐响应对象 public static Response GetMessageMusic(Request request, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaId)
        /// <summary>
        /// 获取音乐响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">缩略图的媒体id，通过上传多媒体文件，得到的id</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageMusic(Request request, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaId)
        {
            return GetResponse(MessageMusic, request, title, description, musicUrl, hqMusicUrl, thumbMediaId);
        }
        #endregion

        #region 获取图文响应对象 public static Response GetMessageNews(Request request, List<string> titleList, List<string> descriptionList, List<string> picUrlList, List<string> urlList)
        /// <summary>
        /// 获取图文响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="titleList">图文消息标题</param>
        /// <param name="descriptionList">图文消息描述</param>
        /// <param name="picUrlList">图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200</param>
        /// <param name="urlList">点击图文消息跳转链接</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageNews(Request request, List<string> titleList, List<string> descriptionList, List<string> picUrlList, List<string> urlList)
        {
            int count = titleList.Count;
            if (descriptionList.Count != count
                || picUrlList.Count != count
                || urlList.Count != count)
            {
                throw WXException.GetInstance("图文消息格式不正确", request.FromUserName);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendFormat(MessageNewsOne,
                    titleList[i],
                    descriptionList[i],
                    picUrlList[i],
                    urlList[i]);
            }

            return GetResponse(MessageNews, request, count.ToString(CultureInfo.InvariantCulture), sb.ToString());
        }
        #endregion

        #region 获取文本响应对象 public static Response GetMessageText(Request request, string content)
        /// <summary>
        /// 获取文本响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="content">回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageText(Request request, string content)
        {
            return GetResponse(MessageText, request, content);
        }
        #endregion

        #region 获取视频响应对象 public static Response GetMessageVideo(Request request, string mediaId, string title, string description)
        /// <summary>
        /// 获取视频响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageVideo(Request request, string mediaId, string title, string description)
        {
            return GetResponse(MessageVideo, request, mediaId, title, description);
        }
        #endregion

        #region 获取语音响应对象 public static Response GetMessageVoice(Request request, string mediaId)
        /// <summary> 
        /// 获取语音响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageVoice(Request request, string mediaId)
        {
            return GetResponse(MessageVoice, request, mediaId);
        }
        #endregion

        #region 获取转发多客服响应对象 public static Response GetMessageTCS(Request request)
        /// <summary> 
        /// 获取转发多客服响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageTCS(Request request)
        {
            return GetResponse(MessageTCS, request);
        }
        #endregion

        #region 获取转发指定多客服响应对象 public static Response GetMessageTCSForOne(Request request, string csID)
        /// <summary> 
        /// 获取转发指定多客服响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="csID">客服ID</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageTCSForOne(Request request, string csID)
        {
            return GetResponse(MessageTCSForOne, request, csID);
        }
        #endregion

        #region 从其他服务器上获取响应对象 public static Response GetMessageFromFriend(Request request, string apiUrl)
        /// <summary>
        /// 从其他服务器上获取响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="apiUrl">接口名称</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageFromFriend(Request request, string apiUrl)
        {
            return new Response(HTTPHelper.Post(String.Format("{0}?signature={1}&timestamp={2}&nonce={3}",
                apiUrl,
                request.Signature,
                request.Timestamp,
                request.Nonce), request.PostData), request, Response.XML);
        }
        #endregion

        #region 从快速配置回复消息上获取响应对象 public static Response GetMessageFromQuickConfigReturnMessage(Request request, string filename)
        /// <summary>
        /// 从快速配置回复消息上获取响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="filename">文件名</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageFromQuickConfigReturnMessage(Request request, string filename)
        {
            return QuickConfigReturnMessageManager.GetReturnMessage(
                File.ReadAllLines(filename)
                .Where(r => !String.IsNullOrEmpty(r) && !String.IsNullOrEmpty(r.Trim()) && r.IndexOf(':') != -1)
                .ToDictionary(
                    k => k.Substring(0, k.IndexOf(':')).Trim(),
                    v => v.Substring(v.IndexOf(':') + 1).Trim()
                            .Replace("{LF}", "\n")
                            .Replace("{NowDate}", DateTime.Now.ToString("yyyy年MM月dd日"))
                            .Replace("{NowTime}", DateTime.Now.ToString("hh:mm:ss"))),
                request);
        }
        #endregion

        #region 获取响应 private static Response GetResponse(string content, Request request, params object[] paramList)
        /// <summary>
        /// 获取响应
        /// </summary>
        /// <param name="content">消息模板</param>
        /// <param name="request">请求对象</param>
        /// <param name="paramList">其余参数列表</param>
        /// <returns>响应对象</returns>
        private static Response GetResponse(string content, Request request, params object[] paramList)
        {
            return new Response(String.Format(content
                    .Replace("{ToUserName}", request.FromUserName)
                    .Replace("{FromUserName}", request.ToUserName)
                    .Replace("{CreateTime}", DateTimeHelper.GetLongTimeByDateTime(DateTime.Now).ToString()),
                paramList), request, Response.XML);
        }
        #endregion

        #region 获取图片响应对象 public static Response GetImageResponse(this Request request, string mediaId)
        /// <summary>
        /// 获取图片响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetImageResponse(this Request request, string mediaId)
        {
            return GetMessageImage(request, mediaId);
        }
        #endregion

        #region 获取音乐响应对象 public static Response GetMusicResponse(this Request request, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaId)
        /// <summary>
        /// 获取音乐响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">缩略图的媒体id，通过上传多媒体文件，得到的id</param>
        /// <returns>响应对象</returns>
        public static Response GetMusicResponse(this Request request, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaId)
        {
            return GetResponse(MessageMusic, request, title, description, musicUrl, hqMusicUrl, thumbMediaId);
        }
        #endregion

        #region 获取图文响应对象 public static Response GetNewsResponse(this Request request, List<string> titleList, List<string> descriptionList, List<string> picUrlList, List<string> urlList)
        /// <summary>
        /// 获取图文响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="titleList">图文消息标题</param>
        /// <param name="descriptionList">图文消息描述</param>
        /// <param name="picUrlList">图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200</param>
        /// <param name="urlList">点击图文消息跳转链接</param>
        /// <returns>响应对象</returns>
        public static Response GetNewsResponse(this Request request, List<string> titleList, List<string> descriptionList, List<string> picUrlList, List<string> urlList)
        {
            return GetMessageNews(request, titleList, descriptionList, picUrlList, urlList);
        }
        #endregion

        #region 获取文本响应对象 public static Response GetTextResponse(this Request request, string content)
        /// <summary>
        /// 获取文本响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="content">回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）</param>
        /// <returns>响应对象</returns>
        public static Response GetTextResponse(this Request request, string content)
        {
            return GetMessageText(request, content);
        }
        #endregion

        #region 获取视频响应对象 public static Response GetVideoResponse(this Request request, string mediaId, string title, string description)
        /// <summary>
        /// 获取视频响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <returns>响应对象</returns>
        public static Response GetVideoResponse(this Request request, string mediaId, string title, string description)
        {
            return GetMessageVideo(request, mediaId, title, description);
        }
        #endregion

        #region 获取语音响应对象 public static Response GetVoiceResponse(this Request request, string mediaId)
        /// <summary> 
        /// 获取语音响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetVoiceResponse(this Request request, string mediaId)
        {
            return GetMessageVoice(request, mediaId);
        }
        #endregion

        #region 获取转发多客服响应对象 public static Response GetTCSResponse(this Request request)
        /// <summary> 
        /// 获取转发多客服响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public static Response GetTCSResponse(this Request request)
        {
            return GetMessageTCS(request);
        }
        #endregion

        #region 获取转发指定多客服响应对象 public static Response GetTCSForOneResponse(this Request request, string csID)
        /// <summary> 
        /// 获取转发指定多客服响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="csID">客服ID</param>
        /// <returns>响应对象</returns>
        public static Response GetTCSForOneResponse(this Request request, string csID)
        {
            return GetMessageTCSForOne(request, csID);
        }
        #endregion

        #region 从其他服务器上获取响应对象 public static Response GetFromFriendResponse(this Request request, string apiUrl)
        /// <summary>
        /// 从其他服务器上获取响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="apiUrl">接口名称</param>
        /// <returns>响应对象</returns>
        public static Response GetFromFriendResponse(this Request request, string apiUrl)
        {
            return GetMessageFromFriend(request, apiUrl);
        }
        #endregion

        #region 从快速配置回复消息上获取响应对象 public static Response GetFromQuickConfigReturnMessageResponse(this Request request, string filename)
        /// <summary>
        /// 从快速配置回复消息上获取响应对象
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="filename">文件名</param>
        /// <returns>响应对象</returns>
        public static Response GetFromQuickConfigReturnMessageResponse(this Request request, string filename)
        {
            return GetMessageFromQuickConfigReturnMessage(request, filename);
        }
        #endregion
    }
}
