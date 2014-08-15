using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 快速配置回复消息管理类
    /// </summary>
    public static class QuickConfigReturnMessageManager
    {
        #region 解析Key-Value数据方法 private delegate Response GetReturnMessageHandler(Dictionary<string, string> kvList, Request request);
        /// <summary>
        /// 解析Key-Value数据方法
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        private delegate Response GetReturnMessageHandler(Dictionary<string, string> kvList, Request request);
        #endregion

        #region 解析Key-Value数据方法列表 private static readonly Dictionary<ReturnEntityType, GetReturnMessageHandler> funcList
        /// <summary>
        /// 解析Key-Value数据方法列表
        /// </summary>
        private static readonly Dictionary<ReturnEntityType, GetReturnMessageHandler> funcList
            = new Dictionary<ReturnEntityType, GetReturnMessageHandler>
        {
            {ReturnEntityType.ReturnMessageImage, GetReturnMessageImage},
            {ReturnEntityType.ReturnMessageMusic, GetReturnMessageMusic},
            {ReturnEntityType.ReturnMessageNews, GetReturnMessageNews},
            {ReturnEntityType.ReturnMessageText, GetReturnMessageText},
            {ReturnEntityType.ReturnMessageVideo, GetReturnMessageVideo},
            {ReturnEntityType.ReturnMessageVoice, GetReturnMessageVoice},
            {ReturnEntityType.ReturnMessageTCS, GetReturnMessageTCS}
        };
        #endregion

        #region 根据Key-Value数据获取回复消息 public static Response GetReturnMessage(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 根据Key-Value数据获取回复消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public static Response GetReturnMessage(Dictionary<string, string> kvList, Request request)
        {
            ReturnEntityType type = (!kvList.ContainsKey("Type"))
                ? ReturnEntityType.ReturnMessageText
                : (ReturnEntityType)Enum.Parse(typeof(ReturnEntityType), kvList["Type"]);
            if (!funcList.ContainsKey(type)) throw new Exception("未知快速配置回复消息类型");

            return funcList[type](kvList, request);
        }
        #endregion

        #region 获取回复图片消息 private static Response GetReturnMessageImage(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复图片消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复图片消息</returns>
        private static Response GetReturnMessageImage(Dictionary<string, string> kvList, Request request)
        {
            if (!kvList.ContainsKey("MediaId")) throw new Exception("回复图片消息格式错误（缺少‘MediaId’）");

            return EntityBuilder.GetMessageImage(request, kvList["MediaId"]);
        }
        #endregion

        #region 获取回复音乐消息 private static Response GetReturnMessageMusic(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复音乐消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复音乐消息</returns>
        private static Response GetReturnMessageMusic(Dictionary<string, string> kvList, Request request)
        {
            if (!kvList.ContainsKey("ThumbMediaId")) throw new Exception("回复音乐消息格式错误（缺少‘ThumbMediaId’）");

            return EntityBuilder.GetMessageMusic(request,
                kvList.ContainsKey("Title") ? kvList["Title"] : "",
                kvList.ContainsKey("Description") ? kvList["Description"] : "",
                kvList.ContainsKey("MusicURL") ? kvList["MusicURL"] : "",
                kvList.ContainsKey("HQMusicUrl") ? kvList["HQMusicUrl"] : "",
                kvList["ThumbMediaId"]);
        }
        #endregion

        #region 获取回复图文消息 private static Response GetReturnMessageNews(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复图文消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复图文消息</returns>
        private static Response GetReturnMessageNews(Dictionary<string, string> kvList, Request request)
        {
            List<string> titleList = new List<string>();
            List<string> descriptionList = new List<string>();
            List<string> picUrlList = new List<string>();
            List<string> urlList = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                if (!kvList.ContainsKey(String.Format("Articles{0}Title", i))) break;
                titleList.Add(kvList[String.Format("Articles{0}Title", i)]);
                descriptionList.Add(kvList.ContainsKey(String.Format("Articles{0}Description", i))
                        ? kvList[String.Format("Articles{0}Description", i)] : "");
                picUrlList.Add(kvList.ContainsKey(String.Format("Articles{0}PicUrl", i))
                        ? kvList[String.Format("Articles{0}PicUrl", i)] : "");
                urlList.Add(kvList.ContainsKey(String.Format("Articles{0}Url", i))
                        ? kvList[String.Format("Articles{0}Url", i)] : "");
            }
            if (titleList.Count == 0) throw new Exception("回复图文消息项目数量不能为空");

            return EntityBuilder.GetMessageNews(request,
                titleList,
                descriptionList,
                picUrlList,
                urlList);
        }
        #endregion

        #region 获取回复文本消息 private static Response GetReturnMessageText(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复文本消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复文本消息</returns>
        private static Response GetReturnMessageText(Dictionary<string, string> kvList, Request request)
        {
            if (!kvList.ContainsKey("Content")) throw new Exception("回复文本消息格式错误（缺少‘Content’）");

            return EntityBuilder.GetMessageText(request, kvList["Content"]);
        }
        #endregion

        #region 获取回复视频消息 private static Response GetReturnMessageVideo(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复视频消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复视频消息</returns>
        private static Response GetReturnMessageVideo(Dictionary<string, string> kvList, Request request)
        {
            if (!kvList.ContainsKey("MediaId")) throw new Exception("回复视频消息格式错误（缺少‘MediaId’）");

            return EntityBuilder.GetMessageVideo(request,
                kvList["MediaId"],
                kvList.ContainsKey("Title") ? kvList["Title"] : "",
                kvList.ContainsKey("Description") ? kvList["Description"] : "");
        }
        #endregion

        #region 获取回复语音消息 private static Response GetReturnMessageVoice(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复语音消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复语音消息</returns>
        private static Response GetReturnMessageVoice(Dictionary<string, string> kvList, Request request)
        {
            if (!kvList.ContainsKey("MediaId")) throw new Exception("回复语音消息格式错误（缺少‘MediaId’）");

            return EntityBuilder.GetMessageVoice(request, kvList["MediaId"]);
        }
        #endregion

        #region 获取回复转发多客服消息 private static Response GetReturnMessageTCS(Dictionary<string, string> kvList, Request request)
        /// <summary>
        /// 获取回复转发多客服消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="request">响应对象</param>
        /// <returns>回复转发多客服消息</returns>
        private static Response GetReturnMessageTCS(Dictionary<string, string> kvList, Request request)
        {
            return EntityBuilder.GetMessageTCS(request);
        }
        #endregion
    }
}
