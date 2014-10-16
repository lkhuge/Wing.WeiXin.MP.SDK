using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.WXXD.GoodsShelf;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店货架管理
    /// </summary>
    public class WXGoodsShelfManager : WXXDManager
    {
        /// <summary>
        /// 增加货架的URL
        /// </summary>
        private const String URLAddShelf =
            "https://api.weixin.qq.com/merchant/shelf/add?access_token={AccessToken}";

        /// <summary>
        /// 删除货架的URL
        /// </summary>
        private const String URLDeleteShelf =
            "https://api.weixin.qq.com/merchant/shelf/del?access_token={AccessToken}";

        /// <summary>
        /// 修改货架的URL
        /// </summary>
        private const String URLModityShelf =
            "https://api.weixin.qq.com/merchant/shelf/mod?access_token={AccessToken}";

        /// <summary>
        /// 获取所有货架的URL
        /// </summary>
        private const String URLGetAllShelf =
            "https://api.weixin.qq.com/merchant/shelf/getall?access_token={AccessToken}";

        /// <summary>
        /// 根据货架ID获取货架信息的URL
        /// </summary>
        private const String URLGetShelfByID =
            "https://api.weixin.qq.com/merchant/shelf/getbyid?access_token={AccessToken}";

        #region 增加货架 public WXGoodsShelfResponse AddShelf(WXGoodsShelf shelf)
        /// <summary>
        /// 增加货架
        /// </summary>
        /// <param name="shelf">微信小店货架</param>
        /// <returns>微信小店货架响应</returns>
        public WXGoodsShelfResponse AddShelf(WXGoodsShelf shelf)
        {
            return GetData<WXGoodsShelfResponse>(URLAddShelf, shelf);
        } 
        #endregion

        #region 删除货架 public ErrorMsg DeleteShelf(int shelf_id)
        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="shelf_id">货架ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg DeleteShelf(int shelf_id)
        {
            return GetData<ErrorMsg>(URLDeleteShelf, new
            {
                shelf_id
            });
        } 
        #endregion

        #region 修改货架 public ErrorMsg ModityShelf(WXGoodsShelf shelf)
        /// <summary>
        /// 修改货架
        /// </summary>
        /// <param name="shelf">微信小店货架</param>
        /// <returns>错误码</returns>
        public ErrorMsg ModityShelf(WXGoodsShelf shelf)
        {
            return GetData<ErrorMsg>(URLModityShelf, shelf);
        } 
        #endregion

        #region 获取所有货架 public WXGoodsShelfAllListResponse GetAllShelf()
        /// <summary>
        /// 获取所有货架
        /// </summary>
        /// <returns>微信小店获取全部货架响应</returns>
        public WXGoodsShelfAllListResponse GetAllShelf()
        {
            return GetData<WXGoodsShelfAllListResponse>(URLGetAllShelf);
        } 
        #endregion

        #region 根据货架ID获取货架信息 public WXGoodsShelf GetShelfByID(int shelf_id)
        /// <summary>
        /// 根据货架ID获取货架信息
        /// </summary>
        /// <param name="shelf_id">货架ID</param>
        /// <returns>微信小店通过货架ID获取货架响应</returns>
        public WXGoodsShelf GetShelfByID(int shelf_id)
        {
            return GetData<WXGoodsShelf>(URLGetShelfByID, new
            {
                shelf_id
            });
        } 
        #endregion
    }
}