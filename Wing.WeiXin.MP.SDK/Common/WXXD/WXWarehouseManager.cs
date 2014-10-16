using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店仓库管理
    /// </summary>
    public class WXWarehouseManager : WXXDManager
    {
        /// <summary>
        /// 增加商品的URL
        /// </summary>
        private const String URLWarehouseAdd =
            "https://api.weixin.qq.com/merchant/stock/add?access_token={AccessToken}";

        /// <summary>
        /// 删除商品的URL
        /// </summary>
        private const String URLWarehouseDelete =
            "https://api.weixin.qq.com/merchant/stock/reduce?access_token={AccessToken}";

        #region 增加库存 public ErrorMsg WarehouseAdd(String product_id, String sku_info, String quantity)
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="product_id">商品ID</param>
        /// <param name="sku_info">sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可</param>
        /// <param name="quantity">增加的库存数量</param>
        /// <returns>错误信息</returns>
        public ErrorMsg WarehouseAdd(String product_id, String sku_info, String quantity)
        {
            return GetData<ErrorMsg>(URLWarehouseAdd, new
            {
                product_id,
                sku_info,
                quantity
            });
        } 
        #endregion

        #region 删除库存 public ErrorMsg WarehouseDelete(String product_id, String sku_info, String quantity)
        /// <summary>
        /// 删除库存
        /// </summary>
        /// <param name="product_id">商品ID</param>
        /// <param name="sku_info">sku信息, 格式"id1:vid1;id2:vid2"</param>
        /// <param name="quantity">减少的库存数量</param>
        /// <returns>错误信息</returns>
        public ErrorMsg WarehouseDelete(String product_id, String sku_info, String quantity)
        {
            return GetData<ErrorMsg>(URLWarehouseDelete, new
            {
                product_id,
                sku_info,
                quantity
            });
        }
        #endregion
    }
}