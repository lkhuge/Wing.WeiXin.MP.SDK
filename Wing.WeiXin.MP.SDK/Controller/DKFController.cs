using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.DKF;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 多客服控制器
    /// </summary>
    public class DKFController : WXController
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
        /// 添加多客服账号的URL
        /// </summary>
        private const string UrlAddDKF = "https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}";

        /// <summary>
        /// 设置多客服账号的URL
        /// </summary>
        private const string UrlSetDKF = "https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}";

        /// <summary>
        /// 上传多客服头像图片的URL
        /// </summary>
        private const string UrlUploadDKFPic = "http://api.weixin.qq.com/customservice/kfacount/uploadheadimg?access_token={0}&kf_account={1}";

        /// <summary>
        /// 删除多客服账号的URL
        /// </summary>
        private const string UrlDeleteDKF = "https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}";

        /// <summary>
        /// 获取会话记录的URL
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
            return Action<DKFList>(UrlGetDKFList, account, true);
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
            return Action<DKFOnlineList>(UrlGetDKFOnlineList, account, true);
        } 
        #endregion

        #region 添加多客服账号 public ErrorMsg AddDKF(WXAccount account, string kf_account, string nickname, string password)
        /// <summary>
        /// 添加多客服账号
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="kf_account">
        /// 完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。
        /// 如果没有公众号微信号，请前往微信公众平台设置。
        /// </param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值（无需加密，方法自动加密）</param>
        /// <returns>错误码</returns>
        public ErrorMsg AddDKF(WXAccount account, string kf_account, string nickname, string password)
        {
            return Action<ErrorMsg>(
                UrlAddDKF,
                new { kf_account, nickname, password = LibManager.SecurityHelper.MD5(password) },
                account, false, false);
        } 
        #endregion

        #region 设置多客服账号 public ErrorMsg SetDKF(WXAccount account, string kf_account, string nickname, string password)
        /// <summary>
        /// 设置多客服账号
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="kf_account">
        /// 完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。
        /// 如果没有公众号微信号，请前往微信公众平台设置。
        /// </param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值（无需加密，方法自动加密）</param>
        /// <returns>错误码</returns>
        public ErrorMsg SetDKF(WXAccount account, string kf_account, string nickname, string password)
        {
            return Action<ErrorMsg>(
                UrlSetDKF,
                new { kf_account, nickname, password = LibManager.SecurityHelper.MD5(password) },
                account, false, false);
        } 
        #endregion

        #region 上传多客服头像图片 public ErrorMsg UploadDKFPic(WXAccount account, string kf_account, string path, string name)
        /// <summary>
        /// 上传多客服头像图片
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="kf_account">
        /// 完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。
        /// 如果没有公众号微信号，请前往微信公众平台设置。
        /// </param>
        /// <param name="path">文件目录</param>
        /// <param name="name">文件名</param>
        /// <returns>错误码</returns>
        public ErrorMsg UploadDKFPic(WXAccount account, string kf_account, string path, string name)
        {
            string result = LibManager.HTTPHelper.Upload(String.Format(
                UrlUploadDKFPic,
                GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token,
                kf_account), path, name);

            return LibManager.JSONHelper.JSONDeserialize<ErrorMsg>(result);
        } 
        #endregion

        #region 删除多客服账号 public ErrorMsg DeleteDKF(WXAccount account, string kf_account)
        /// <summary>
        /// 删除多客服账号
        /// </summary>
        /// <param name="account">微信公共平台账号</param>
        /// <param name="kf_account">
        /// 完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。
        /// 如果没有公众号微信号，请前往微信公众平台设置。
        /// </param>
        /// <returns>错误码</returns>
        public ErrorMsg DeleteDKF(WXAccount account, string kf_account)
        {
            return ActionWithoutAccessToken<ErrorMsg>(
                String.Format(UrlDeleteDKF, GlobalManager.AccessTokenContainer.GetAccessToken(account).access_token, kf_account),
                account, false, false);
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
            return Action<DKFrecordList>(UrlGetDKFrecordList, new
                    {
                        starttime = LibManager.DateTimeHelper.GetLongTimeByDateTime(starttime),
                        endtime = LibManager.DateTimeHelper.GetLongTimeByDateTime(endtime),
                        openid, pagesize, pageindex
                    }, account, true);
        } 
        #endregion
    }
}
