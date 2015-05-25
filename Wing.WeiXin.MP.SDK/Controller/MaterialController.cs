using System.Collections.Generic;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Material;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 素材控制器
    /// </summary>
    public class MaterialController : WXController
    {
        /// <summary>
        /// 新增临时素材的URL
        /// </summary>
        private const string UrlAddTemp = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token=[AT]&type={0}";

        /// <summary>
        /// 获取临时素材的URL
        /// </summary>
        private const string UrlGetTemp = "https://api.weixin.qq.com/cgi-bin/media/get?access_token=[AT]&media_id={0}";

        /// <summary>
        /// 新增永久图文素材的URL
        /// </summary>
        private const string UrlAddNews = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token=[AT]";

        /// <summary>
        /// 新增除了图文的其他类型永久素材的URL
        /// </summary>
        private const string UrlAdd = "http://api.weixin.qq.com/cgi-bin/material/add_material?access_token=[AT]";

        /// <summary>
        /// 获取永久素材的URL
        /// </summary>
        private const string UrlGet = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token=[AT]";

        /// <summary>
        /// 删除永久素材的URL
        /// </summary>
        private const string UrlDelete = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=[AT]";

        /// <summary>
        /// 修改永久图文素材的URL
        /// </summary>
        private const string UrlUpdate = "https://api.weixin.qq.com/cgi-bin/material/update_news?access_token=[AT]";

        /// <summary>
        /// 获取素材总数的URL
        /// </summary>
        private const string UrlGetCount = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token=[AT]";

        /// <summary>
        /// 获取素材列表的URL
        /// </summary>
        private const string UrlGetList = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=[AT]";

        #region 根据AccessToken容器初始化 public MaterialController(AccessTokenContainer accessTokenContainer)
        /// <summary>
        /// 根据AccessToken容器初始化
        /// </summary>
        /// <param name="accessTokenContainer">AccessToken容器</param>
        public MaterialController(AccessTokenContainer accessTokenContainer)
            : base(accessTokenContainer)
        {
        } 
        #endregion

        #region 新增临时素材 public Media AddTemp(WXAccount account, UploadMediaType type, string path, string name)
        /// <summary>
        /// 新增临时素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="type">多媒体类型</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>多媒体对象</returns>
        public Media AddTemp(WXAccount account, UploadMediaType type, string path, string name)
        {
            return Upload(UrlAddTemp, account, type, path, name);
        }
        #endregion

        #region 获取临时素材 public void GetTemp(WXAccount account, string media_id, string pathName)
        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">多媒体编号</param>
        /// <param name="pathName">下载路径加文件名</param>
        public void GetTemp(WXAccount account, string media_id, string pathName)
        {
            Download(UrlGetTemp, account, media_id, pathName);
        }
        #endregion

        #region 新增永久图文素材 public Media AddNews(WXAccount account, MediaNews mediaNews)
        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="mediaNews">永久图文素材</param>
        /// <returns>多媒体对象</returns>
        public Media AddNews(WXAccount account, MediaNews mediaNews)
        {
            return Action<Media>(UrlAddNews, mediaNews, account);
        }
        #endregion

        #region 新增除了图文的其他类型永久素材 public Media Add(WXAccount account, string path, string name)
        /// <summary>
        /// 新增除了图文的其他类型永久素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>多媒体对象</returns>
        public Media Add(WXAccount account, string path, string name)
        {
            return Upload(UrlAdd, account, UploadMediaType.image, path, name);
        }
        #endregion

        #region 获取永久图文素材 public MediaNews GetNews(WXAccount account, string media_id)
        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">素材ID</param>
        /// <returns>永久图文素材</returns>
        public MediaNews GetNews(WXAccount account, string media_id)
        {
            return Action<MediaNews>(UrlGet, new { media_id }, account);
        }
        #endregion

        #region 获取除了图文的其他类型永久素材 public void Get(WXAccount account, string media_id , string pathName)
        /// <summary>
        /// 获取除了图文的其他类型永久素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">素材ID</param>
        /// <param name="pathName">下载路径加文件名</param>
        public void Get(WXAccount account, string media_id, string pathName)
        {
            Download(UrlGet, account, null, pathName,
                JSONHelper.JSONSerialize(new { media_id }));
        }
        #endregion

        #region 删除永久素材 public ErrorMsg Delete(WXAccount account, string media_id)
        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="media_id">素材ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg Delete(WXAccount account, string media_id)
        {
            return Action<ErrorMsg>(UrlDelete, new { media_id }, account);
        }
        #endregion

        #region 修改永久图文素材 public ErrorMsg Update(WXAccount account, List<NewsArticles> articlesList, string media_id, int index)
        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="articlesList">图文消息列表</param>
        /// <param name="media_id">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <returns>错误码</returns>
        public ErrorMsg Update(WXAccount account, List<NewsArticles> articlesList, string media_id, int index)
        {
            return Action<ErrorMsg>(UrlUpdate, new { media_id, index, articles = articlesList }, account);
        }
        #endregion

        #region 获取素材总数 public MediaCount GetCount(WXAccount account)
        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>素材总数</returns>
        public MediaCount GetCount(WXAccount account)
        {
            return Action<MediaCount>(UrlGetCount, account);
        }
        #endregion

        #region 获取图文素材列表 public MediaNewsList GetNewsList(WXAccount account, int offset, int count)
        /// <summary>
        /// 获取图文素材列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns>图文素材列表</returns>
        public MediaNewsList GetNewsList(WXAccount account, int offset, int count)
        {
            return Action<MediaNewsList>(UrlGetList, new { type = "news", offset, count }, account);
        }
        #endregion

        #region 获取除了图文的其他类型素材列表 public MediaList GetList(WXAccount account, UploadMediaType type, int offset, int count)
        /// <summary>
        /// 获取除了图文的其他类型素材列表
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="type">多媒体类型</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns>图文素材列表</returns>
        public MediaList GetList(WXAccount account, UploadMediaType type, int offset, int count)
        {
            return Action<MediaList>(UrlGetList, new { type = type.ToString(), offset, count }, account);
        }
        #endregion
    }
}
