using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.DKF;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 多客服控制器
    /// </summary>
    public class DKFController
    {
        /// <summary>
        /// 获取客服基本信息的URL
        /// </summary>
        private const string UrlGetDKFList = "https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}";

        /// <summary>
        /// 获取在线客服接待信息的URL
        /// </summary>
        private const string UrlGetDKFOnlineList = "https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}";

        /// <summary>
        /// 获取客服基本信息的URL
        /// </summary>
        private const string UrlGetDKFrecordList = "https://api.weixin.qq.com/cgi-bin/customservice/getrecord?access_token={0}";

        #region 获取客服基本信息 public DKFList GetDKFList(WXAccount account)
        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>客服基本信息</returns>
        public DKFList GetDKFList(WXAccount account)
        {
            account.CheckIsService();
            string result = LibManager.HTTPHelper.Get(String.Format(
                    UrlGetDKFList,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return LibManager.JSONHelper.JSONDeserialize<DKFList>(result);
        } 
        #endregion

        #region 获取在线客服接待信息 public DKFOnlineList GetDKFOnlineList(WXAccount account)
        /// <summary>
        /// 获取在线客服接待信息
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <returns>在线客服接待信息</returns>
        public DKFOnlineList GetDKFOnlineList(WXAccount account)
        {
            account.CheckIsService();
            string result = LibManager.HTTPHelper.Get(String.Format(
                    UrlGetDKFOnlineList,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return LibManager.JSONHelper.JSONDeserialize<DKFOnlineList>(result);
        } 
        #endregion

        #region 获取会话记录 public DKFrecordList GetDKFrecordList(WXAccount account, DateTime starttime, DateTime endtime, string openid, int pagesize, int pageindex)
        /// <summary>
        /// 获取会话记录
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="starttime">查询开始时间</param>
        /// <param name="endtime">查询结束时间</param>
        /// <param name="openid">普通用户openid，若不填则查询该appid下所有用户</param>
        /// <param name="pagesize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageindex">查询第几页，从1开始</param>
        /// <returns>会话记录</returns>
        public DKFrecordList GetDKFrecordList(WXAccount account, DateTime starttime, DateTime endtime, string openid, int pagesize, int pageindex)
        {
            account.CheckIsService();
            string result = LibManager.HTTPHelper.Post(String.Format(
                    UrlGetDKFrecordList,
                    GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token),
                    LibManager.JSONHelper.JSONSerialize(new
                    {
                        starttime = LibManager.DateTimeHelper.GetLongTimeByDateTime(starttime),
                        endtime = LibManager.DateTimeHelper.GetLongTimeByDateTime(endtime),
                        openid, pagesize, pageindex
                    }));
            if (LibManager.JSONHelper.HasKey(result, "errcode"))
            {
                throw MessageException.GetInstance(LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result).GetIntroduce());
            }

            return LibManager.JSONHelper.JSONDeserialize<DKFrecordList>(result);
        } 
        #endregion
    }
}
