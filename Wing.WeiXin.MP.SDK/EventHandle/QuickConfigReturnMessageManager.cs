using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages.Messages;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.FileManager;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 快速配置回复消息管理类
    /// </summary>
    public static class QuickConfigReturnMessageManager
    {
        #region 解析Key-Value数据方法 private delegate IReturn GetReturnMessageHandler(Dictionary<string, string> kvList, BaseEntity entity);
        /// <summary>
        /// 解析Key-Value数据方法
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复消息</returns>
        private delegate IReturn GetReturnMessageHandler(Dictionary<string, string> kvList, BaseEntity entity);
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
            {ReturnEntityType.ReturnMessageVoice, GetReturnMessageVoice}
        };
        #endregion

        #region 根据Key-Value数据获取回复消息 public static IReturn GetReturnMessage(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 根据Key-Value数据获取回复消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复消息</returns>
        public static IReturn GetReturnMessage(Dictionary<string, string> kvList, BaseEntity entity)
        {
            ReturnEntityType type = (!kvList.ContainsKey("Type"))
                ? ReturnEntityType.ReturnMessageText
                : (ReturnEntityType)Enum.Parse(typeof(ReturnEntityType), kvList["Type"]);
            if (!funcList.ContainsKey(type)) throw new ConvertToEntityException(kvList);

            return funcList[type](kvList, entity);
        }
        #endregion

        #region 获取回复图片消息 private static IReturn GetReturnMessageImage(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复图片消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复图片消息</returns>
        private static IReturn GetReturnMessageImage(Dictionary<string, string> kvList, BaseEntity entity)
        {
            if (!kvList.ContainsKey("MediaId")) throw new WXException("回复图片消息格式错误（缺少‘MediaId’）");

            return new ReturnMessageImage(kvList["MediaId"], entity);
        }
        #endregion

        #region 获取回复音乐消息 private static IReturn GetReturnMessageMusic(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复音乐消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复音乐消息</returns>
        private static IReturn GetReturnMessageMusic(Dictionary<string, string> kvList, BaseEntity entity)
        {
            if (!kvList.ContainsKey("ThumbMediaId")) throw new WXException("回复音乐消息格式错误（缺少‘ThumbMediaId’）");

            return new ReturnMessageMusic(
                kvList.ContainsKey("Title") ? kvList["Title"] : "",
                kvList.ContainsKey("Description") ? kvList["Description"] : "",
                kvList.ContainsKey("MusicURL") ? kvList["MusicURL"] : "",
                kvList.ContainsKey("HQMusicUrl") ? kvList["HQMusicUrl"] : "",
                kvList["ThumbMediaId"], entity);
        }
        #endregion

        #region 获取回复图文消息 private static IReturn GetReturnMessageNews(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复图文消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复图文消息</returns>
        private static IReturn GetReturnMessageNews(Dictionary<string, string> kvList, BaseEntity entity)
        {
            List<ReturnMessageNews.item> itemList = new List<ReturnMessageNews.item>();
            for (int i = 1; i <= 10; i++)
            {
                if (!kvList.ContainsKey(String.Format("Articles{0}Title", i))) break;
                itemList.Add(new ReturnMessageNews.item
                {
                    Title = kvList[String.Format("Articles{0}Title", i)],
                    Description = kvList.ContainsKey(String.Format("Articles{0}Description", i))
                        ? kvList[String.Format("Articles{0}Description", i)] : "",
                    PicUrl = kvList.ContainsKey(String.Format("Articles{0}PicUrl", i))
                        ? kvList[String.Format("Articles{0}PicUrl", i)] : "",
                    Url = kvList.ContainsKey(String.Format("Articles{0}Url", i))
                        ? kvList[String.Format("Articles{0}Url", i)] : ""
                });
            }
            if (itemList.Count == 0) throw new WXException("回复图文消息项目数量不能为空");

            return new ReturnMessageNews(itemList, entity);
        }
        #endregion

        #region 获取回复文本消息 private static IReturn GetReturnMessageText(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复文本消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复文本消息</returns>
        private static IReturn GetReturnMessageText(Dictionary<string, string> kvList, BaseEntity entity)
        {
            if (!kvList.ContainsKey("Content")) throw new WXException("回复文本消息格式错误（缺少‘Content’）");

            return new ReturnMessageText(kvList["Content"], entity);
        }
        #endregion

        #region 获取回复视频消息 private static IReturn GetReturnMessageVideo(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复视频消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复视频消息</returns>
        private static IReturn GetReturnMessageVideo(Dictionary<string, string> kvList, BaseEntity entity)
        {
            if (!kvList.ContainsKey("MediaId")) throw new WXException("回复视频消息格式错误（缺少‘MediaId’）");

            return new ReturnMessageVideo(
                kvList["MediaId"],
                kvList.ContainsKey("Title") ? kvList["Title"] : "",
                kvList.ContainsKey("Description") ? kvList["Description"] : "", entity);
        }
        #endregion

        #region 获取回复语音消息 private static IReturn GetReturnMessageVoice(Dictionary<string, string> kvList, BaseEntity entity)
        /// <summary>
        /// 获取回复语音消息
        /// </summary>
        /// <param name="kvList">Key-Value数据</param>
        /// <param name="entity">接收消息</param>
        /// <returns>回复语音消息</returns>
        private static IReturn GetReturnMessageVoice(Dictionary<string, string> kvList, BaseEntity entity)
        {
            if (!kvList.ContainsKey("MediaId")) throw new WXException("回复语音消息格式错误（缺少‘MediaId’）");

            return new ReturnMessageVoice(kvList["MediaId"], entity);
        }
        #endregion
    }
}
