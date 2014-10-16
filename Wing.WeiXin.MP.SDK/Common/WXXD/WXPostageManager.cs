using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.WXXD.PostageTemp;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店邮费管理
    /// </summary>
    public class WXPostageManager : WXXDManager
    {
        /// <summary>
        /// 增加邮费模板的URL
        /// </summary>
        private const String URLAddPostageTemp =
            "https://api.weixin.qq.com/merchant/express/add?access_token={AccessToken}";

        /// <summary>
        /// 删除邮费模板的URL
        /// </summary>
        private const String URLDetelePostageTemp =
            "https://api.weixin.qq.com/merchant/express/del?access_token={AccessToken}";

        /// <summary>
        /// 修改邮费模板的URL
        /// </summary>
        private const String URLModityPostageTemp =
            "https://api.weixin.qq.com/merchant/express/update?access_token={AccessToken}";

        /// <summary>
        /// 获取指定ID的邮费模板的URL
        /// </summary>
        private const String URLGetPostageTempByID =
            "https://api.weixin.qq.com/merchant/express/getbyid?access_token={AccessToken}";

        /// <summary>
        /// 获取所有邮费模板的URL
        /// </summary>
        private const String URLGetAllPostageTemp =
            "https://api.weixin.qq.com/merchant/express/getall?access_token={AccessToken}";

        #region 增加邮费模板 public WXPostageTempResponse AddPostageTemp(WXPostageTemp postageTemp)
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="postageTemp">微信小店邮费模板</param>
        /// <returns>微信小店邮费模板响应</returns>
        public WXPostageTempResponse AddPostageTemp(WXPostageTemp postageTemp)
        {
            return GetData<WXPostageTempResponse>(URLAddPostageTemp, postageTemp);
        } 
        #endregion

        #region 删除邮费模板 public ErrorMsg DetelePostageTemp(int template_id)
        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="template_id">邮费模板ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg DetelePostageTemp(int template_id)
        {
            return GetData<ErrorMsg>(URLDetelePostageTemp, new
            {
                template_id
            });
        } 
        #endregion

        #region 修改邮费模板 public ErrorMsg ModityPostageTemp(WXPostageTemp postageTemp)
        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="postageTemp">微信小店邮费模板</param>
        /// <returns>错误码</returns>
        public ErrorMsg ModityPostageTemp(WXPostageTemp postageTemp)
        {
            return GetData<ErrorMsg>(URLModityPostageTemp, postageTemp);
        } 
        #endregion

        #region 获取指定ID的邮费模板 public WXPostageTempQueryResponse GetPostageTempByID(int template_id)
        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="template_id">邮费模板ID</param>
        /// <returns>查询邮费模板响应</returns>
        public WXPostageTempQueryResponse GetPostageTempByID(int template_id)
        {
            return GetData<WXPostageTempQueryResponse>(URLGetPostageTempByID, new
            {
                template_id
            });
        } 
        #endregion

        #region 获取所有邮费模板 public WXPostageTempQueryListResponse GetAllPostageTemp()
        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <returns>查询邮费模板列表响应</returns>
        public WXPostageTempQueryListResponse GetAllPostageTemp()
        {
            return GetData<WXPostageTempQueryListResponse>(URLGetAllPostageTemp);
        } 
        #endregion
    }
}