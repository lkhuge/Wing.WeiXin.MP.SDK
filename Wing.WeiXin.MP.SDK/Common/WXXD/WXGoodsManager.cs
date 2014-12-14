using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.WXXD.Goods;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店商品管理
    /// </summary>
    public class WXGoodsManager : WXXDManager
    {
        /// <summary>
        /// 增加商品的URL
        /// </summary>
        private const String URLAddGoods = 
            "https://api.weixin.qq.com/merchant/create?access_token={AccessToken}";

        /// <summary>
        /// 删除商品的URL
        /// </summary>
        private const String URLDeleteGoods =
            "https://api.weixin.qq.com/merchant/del?access_token={AccessToken}";

        /// <summary>
        /// 修改商品的URL
        /// </summary>
        private const String URLModityGoods =
            "https://api.weixin.qq.com/merchant/update?access_token={AccessToken}";

        /// <summary>
        /// 查询商品的URL
        /// </summary>
        private const String URLQueryGoods =
            "https://api.weixin.qq.com/merchant/get?access_token={AccessToken}";

        /// <summary>
        /// 获取指定状态的所有商品的URL
        /// </summary>
        private const String URLGetGoodsByState =
            "https://api.weixin.qq.com/merchant/getbystatus?access_token={AccessToken}";

        /// <summary>
        /// 商品上下架的URL
        /// </summary>
        private const String URLModityGoodsState =
            "https://api.weixin.qq.com/merchant/modproductstatus?access_token={AccessToken}";

        /// <summary>
        /// 获取指定分类的所有子分类的URL
        /// </summary>
        private const String URLGetSubGroupByGroup =
            "https://api.weixin.qq.com/merchant/category/getsub?access_token={AccessToken}";

        /// <summary>
        /// 获取指定子分类的所有SKU的URL
        /// </summary>
        private const String URLGetSKUBySubGroup =
            "https://api.weixin.qq.com/merchant/category/getsku?access_token={AccessToken}";

        /// <summary>
        /// 获取指定分类的所有属性的URL
        /// </summary>
        private const String URLGetPropertiesByGroup =
            "https://api.weixin.qq.com/merchant/category/getproperty?access_token={AccessToken}";

        #region 增加商品 public WXGoodsResponse AddGoods(WXGoods goods)
        /// <summary>
        /// 增加商品
        /// </summary>
        /// <param name="goods">商品</param>
        /// <returns>商品编号</returns>
        public WXGoodsResponse AddGoods(WXGoods goods)
        {
            return GetData<WXGoodsResponse>(URLAddGoods, goods);
        } 
        #endregion

        #region 删除商品 public ErrorMsg DeleteGoods(string productID)
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="productID">商品编号</param>
        /// <returns>错误编号</returns>
        public ErrorMsg DeleteGoods(string productID)
        {
            return GetData<ErrorMsg>(URLDeleteGoods, new
            {
                product_id = productID
            });
        } 
        #endregion

        #region 修改商品 public ErrorMsg ModityGoods(WXGoods goods)
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="goods">需要修改的商品</param>
        /// <returns>错误编号</returns>
        public ErrorMsg ModityGoods(WXGoods goods)
        {
            return GetData<ErrorMsg>(URLModityGoods, goods);
        } 
        #endregion

        #region 查询商品 public WXGoodsQueryResponse QueryGoods(string productID)
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="productID">商品编号</param>
        /// <returns>查询商品响应</returns>
        public WXGoodsQueryResponse QueryGoods(string productID)
        {
            return GetData<WXGoodsQueryResponse>(URLQueryGoods, new
            {
                product_id = productID
            });
        } 
        #endregion

        #region 获取指定状态的所有商品 public WXGoodsQueryListResponse GetGoodsByState(int status)
        /// <summary>
        /// 获取指定状态的所有商品
        /// </summary>
        /// <param name="status">商品状态(0-全部, 1-上架, 2-下架)</param>
        /// <returns>查询商品列表响应</returns>
        public WXGoodsQueryListResponse GetGoodsByState(int status)
        {
            return GetData<WXGoodsQueryListResponse>(URLGetGoodsByState, new
            {
                status
            });
        } 
        #endregion

        #region 商品上下架 public ErrorMsg ModityGoodsState(string productID, int status)
        /// <summary>
        /// 商品上下架
        /// </summary>
        /// <param name="productID">商品编号</param>
        /// <param name="status">商品上下架标识(0-下架, 1-上架)</param>
        /// <returns>错误编号</returns>
        public ErrorMsg ModityGoodsState(string productID, int status)
        {
            return GetData<ErrorMsg>(URLModityGoodsState, new
            {
                product_id = productID,
                status
            });
        } 
        #endregion

        #region 获取指定分类的所有子分类 public WXSubGroupListResponse GetSubGroupByGroup(int cate_id)
        /// <summary>
        /// 获取指定分类的所有子分类
        /// </summary>
        /// <param name="cate_id">大分类ID(根节点分类id为1)</param>
        /// <returns>微信小店分类列表响应</returns>
        public WXSubGroupListResponse GetSubGroupByGroup(int cate_id)
        {
            return GetData<WXSubGroupListResponse>(URLGetSubGroupByGroup, new
            {
                cate_id
            });
        } 
        #endregion

        #region 获取指定子分类的所有SKU public WXSKUListResponse GetSKUBySubGroup(int cate_id)
        /// <summary>
        /// 获取指定子分类的所有SKU
        /// </summary>
        /// <param name="cate_id">商品子分类ID</param>
        /// <returns>微信小店SKU列表响应</returns>
        public WXSKUListResponse GetSKUBySubGroup(int cate_id)
        {
            return GetData<WXSKUListResponse>(URLGetSKUBySubGroup, new
            {
                cate_id
            });
        } 
        #endregion

        #region 获取指定分类的所有属性 public WXPropertiesListResponse GetPropertiesByGroup(int cate_id)
        /// <summary>
        /// 获取指定分类的所有属性
        /// </summary>
        /// <param name="cate_id">分类ID</param>
        /// <returns>微信小店属性列表响应</returns>
        public WXPropertiesListResponse GetPropertiesByGroup(int cate_id)
        {
            return GetData<WXPropertiesListResponse>(URLGetPropertiesByGroup, new
            {
                cate_id
            });
        } 
        #endregion
    }
}