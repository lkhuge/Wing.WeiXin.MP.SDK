using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Material;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup;
using Wing.WeiXin.MP.SDK.Entities.SendAll.ByOpenIDList;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 群发控制器
    /// </summary>
    public class SendAllController : WXController
    {
        /// <summary>
        /// 上传图文消息素材的URL
        /// </summary>
        private const string UrlUploadNews = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}";

        /// <summary>
        /// 根据分组进行群发的URL
        /// </summary>
        private const string UrlSendAllByGroup = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";

        /// <summary>
        /// 根据OpenID列表群发的URL
        /// </summary>
        private const string UrlSendAllByOpenIDList = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";

        /// <summary>
        /// 删除群发的URL
        /// </summary>
        private const string UrlDeleteSendAll = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}";

        #region 上传图文消息素材 public Media UploadNews(WXAccount account, SendAllMessageNews news)
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="news">群发图文消息</param>
        /// <returns>多媒体对象</returns>
        public Media UploadNews(WXAccount account, SendAllMessageNews news)
        {
            return Action<Media>(UrlUploadNews, news, account);
        } 
        #endregion

        #region 根据分组进行群发 public ReturnMessage SendAllByGroup(WXAccount account, SendAllByGroup group)
        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">群发组</param>
        /// <returns>群发回复消息</returns>
        public ReturnMessage SendAllByGroup(WXAccount account, SendAllByGroup group)
        {
            return Action<ReturnMessage>(UrlSendAllByGroup, group, account);
        } 
        #endregion

        #region 根据OpenID列表群发 public ReturnMessage SendAllByOpenIDList(WXAccount account, SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="openIDList">OpenID列表</param>
        /// <returns>群发回复消息</returns>
        public ReturnMessage SendAllByOpenIDList(WXAccount account, SendAllByOpenIDList openIDList)
        {
            return Action<ReturnMessage>(UrlSendAllByOpenIDList, openIDList, account);
        }
        #endregion

        #region 删除群发 public ErrorMsg DeleteSendAll(WXAccount account, SendAllDelete delete)
        /// <summary>
        /// 删除群发
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="delete">删除群发对象</param>
        /// <returns>返回码对象</returns>
        public ErrorMsg DeleteSendAll(WXAccount account, SendAllDelete delete)
        {
            return Action<ErrorMsg>(UrlDeleteSendAll, delete, account);
        } 
        #endregion
    }
}
