﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Common
{
    /// <summary>
    /// 实体创建工具类
    /// </summary>
    public static class EntityBuilder
    {
        #region 图片响应内容
        /// <summary>
        /// 图片响应内容
        /// </summary>
        private const string MessageImage = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[image]]></MsgType>
                <Image>
                    <MediaId><![CDATA[{0}]]></MediaId>
                </Image>
            </xml>"; 
        #endregion

        #region 音乐响应内容
        /// <summary>
        /// 音乐响应内容
        /// </summary>
        private const string MessageMusic = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[music]]></MsgType>
                <Music>
                    <Title><![CDATA[{0}]]></Title>
                    <Description><![CDATA[{1}]]></Description>
                    <MusicUrl><![CDATA[{2}]]></MusicUrl>
                    <HQMusicUrl><![CDATA[{3}]]></HQMusicUrl>
                    <ThumbMediaId><![CDATA[{4}]]></ThumbMediaId>
                </Music>
            </xml>"; 
        #endregion

        #region 图文响应内容
        /// <summary>
        /// 图文响应内容
        /// </summary>
        private const string MessageNews = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[news]]></MsgType>
                <ArticleCount>{0}</ArticleCount>
                <Articles>
                    {1}
                </Articles>
            </xml> "; 
        #endregion

        #region 图文内容响应内容
        /// <summary>
        /// 图文内容响应内容
        /// </summary>
        private const string MessageNewsOne = @"
            <item>
                <Title><![CDATA[{0}]]></Title> 
                <Description><![CDATA[{1}]]></Description>
                <PicUrl><![CDATA[{2}]]></PicUrl>
                <Url><![CDATA[{3}]]></Url>
            </item>"; 
        #endregion

        #region 文本响应内容
        /// <summary>
        /// 文本响应内容
        /// </summary>
        private const string MessageText = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[text]]></MsgType>
                <Content><![CDATA[{0}]]></Content>
            </xml>"; 
        #endregion

        #region 视频响应内容
        /// <summary>
        /// 视频响应内容
        /// </summary>
        private const string MessageVideo = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[video]]></MsgType>
                <Video>
                    <MediaId><![CDATA[{0}]]></MediaId>
                    <Title><![CDATA[{1}]]></Title>
                    <Description><![CDATA[{2}]]></Description>
                </Video> 
            </xml>"; 
        #endregion

        #region 语音响应内容
        /// <summary>
        /// 语音响应内容
        /// </summary>
        private const string MessageVoice = @"
            <xml>
                <ToUserName><![CDATA[{ToUserName}]]></ToUserName>
                <FromUserName><![CDATA[{FromUserName}]]></FromUserName>
                <CreateTime>{CreateTime}</CreateTime>
                <MsgType><![CDATA[voice]]></MsgType>
                <Voice>
                    <MediaId><![CDATA[{0}]]></MediaId>
                </Voice>
            </xml>"; 
        #endregion

        #region 获取图片消息 public static Response GetMessageImage(Request request, string mediaId)
        /// <summary>
        /// 获取图片消息
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageImage(Request request, string mediaId)
        {
            return GetResponse(MessageImage, request, mediaId);
        } 
        #endregion

        #region 获取音乐消息 public static Response GetMessageMusic(Request request, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaId)
        /// <summary>
        /// 获取音乐消息
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

        #region 获取图文消息 public static Response GetMessageNews(Request request, List<string> titleList, List<string> descriptionList, List<string> picUrlList, List<string> urlList)
        /// <summary>
        /// 获取图文消息
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
                throw new Exception("图文消息格式不正确");
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

        #region 获取文本消息 public static Response GetMessageText(Request request, string content)
        /// <summary>
        /// 获取文本消息
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="content">回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageText(Request request, string content)
        {
            return GetResponse(MessageText, request, content);
        } 
        #endregion

        #region 获取视频消息 public static Response GetMessageVideo(Request request, string mediaId, string title, string description)
        /// <summary>
        /// 获取视频消息
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

        #region 获取语音消息 public static Response GetMessageVoice(Request request, string mediaId)
        /// <summary> 
        /// 获取语音消息
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="mediaId">通过上传多媒体文件，得到的id。</param>
        /// <returns>响应对象</returns>
        public static Response GetMessageVoice(Request request, string mediaId)
        {
            return GetResponse(MessageVoice, request, mediaId);
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
                    .Replace("{CreateTime}", Message.GetLongTimeNow().ToString(CultureInfo.InvariantCulture)), 
                paramList), Response.XML);
        } 
        #endregion
    }
}