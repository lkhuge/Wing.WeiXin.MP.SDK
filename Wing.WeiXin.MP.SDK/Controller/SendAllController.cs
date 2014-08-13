using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.SendAll;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 群发控制器
    /// </summary>
    public class SendAllController
    {
        #region 上传图文消息素材 public Media UploadNews(WXAccount account, SendAllMessageNews news)
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="news">群发图文消息</param>
        /// <returns>多媒体对象</returns>
        public Media UploadNews(WXAccount account, SendAllMessageNews news)
        {
            account.CheckIsService();
            string result = HTTPHelper.Post(URLManager.GetURLForSendAllUploadNews(account), JSONHelper.JSONSerialize(news));
            if (JSONHelper.HasKey(result, "errcode"))
            {
                throw new Exception(JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return JSONHelper.JSONDeserialize<Media>(result);
        } 
        #endregion

        #region 根据分组进行群发 public SendAllReturnMessage SendAllByGroup(WXAccount account, SendAllByGroup group)
        /// <summary>
        /// 根据分组进行群发
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="group">群发组</param>
        /// <returns>群发回复消息</returns>
        public SendAllReturnMessage SendAllByGroup(WXAccount account, SendAllByGroup group)
        {
            account.CheckIsService();

            return JSONHelper.JSONDeserialize<SendAllReturnMessage>(
                HTTPHelper.Post(URLManager.GetURLForSendAllByGroup(account), JSONHelper.JSONSerialize(group)));
        } 
        #endregion

        #region 根据OpenID列表群发 public SendAllReturnMessage SendAllByOpenIDList(WXAccount account, SendAllByOpenIDList openIDList)
        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="openIDList">OpenID列表</param>
        /// <returns>群发回复消息</returns>
        public SendAllReturnMessage SendAllByOpenIDList(WXAccount account, SendAllByOpenIDList openIDList)
        {
            account.CheckIsService();

            return JSONHelper.JSONDeserialize<SendAllReturnMessage>(HTTPHelper.Post(URLManager.GetURLForSendAllByOpenIDList(account), JSONHelper.JSONSerialize(openIDList)));
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
            account.CheckIsService();

            return JSONHelper.JSONDeserialize<ErrorMsg>(HTTPHelper.Post(URLManager.GetURLForSendAllDelete(account), JSONHelper.JSONSerialize(delete)));
        } 
        #endregion
    }
}
