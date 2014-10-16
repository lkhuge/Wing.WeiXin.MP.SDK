using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.WXXD.Group;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店分组管理
    /// </summary>
    public class WXGroupManager : WXXDManager
    {
        /// <summary>
        /// 增加分组的URL
        /// </summary>
        private const String URLAddGroup =
            "https://api.weixin.qq.com/merchant/group/add?access_token={AccessToken}";

        /// <summary>
        /// 删除分组的URL
        /// </summary>
        private const String URLDeleteGroup =
            "https://api.weixin.qq.com/merchant/group/del?access_token={AccessToken}";

        /// <summary>
        /// 修改分组属性的URL
        /// </summary>
        private const String URLModityGroupProperty =
            "https://api.weixin.qq.com/merchant/group/propertymod?access_token={AccessToken}";

        /// <summary>
        /// 修改分组商品的URL
        /// </summary>
        private const String URLModityGroup =
            "https://api.weixin.qq.com/merchant/group/productmod?access_token={AccessToken}";

        /// <summary>
        /// 获取所有分组的URL
        /// </summary>
        private const String URLGetAllGroup =
            "https://api.weixin.qq.com/merchant/group/getall?access_token={AccessToken}";

        /// <summary>
        /// 根据分组ID获取分组信息的URL
        /// </summary>
        private const String URLGetAllGroupByID =
            "https://api.weixin.qq.com/merchant/group/getbyid?access_token={AccessToken}";

        #region 增加分组 public WXGoodsGroupResponse AddGroup(WXGoodsGroup goodsGroup)
        /// <summary>
        /// 增加分组
        /// </summary>
        /// <param name="goodsGroup">微信小店分组</param>
        /// <returns>微信小店分组响应</returns>
        public WXGoodsGroupResponse AddGroup(WXGoodsGroup goodsGroup)
        {
            return GetData<WXGoodsGroupResponse>(URLAddGroup, goodsGroup);
        } 
        #endregion

        #region 删除分组 public ErrorMsg DeleteGroup(int group_id)
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="group_id">分组ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg DeleteGroup(int group_id)
        {
            return GetData<ErrorMsg>(URLDeleteGroup, new
            {
                group_id
            });
        } 
        #endregion

        #region 修改分组属性 public ErrorMsg ModityGroupProperty(String group_id, String group_name)
        /// <summary>
        /// 修改分组属性
        /// </summary>
        /// <param name="group_id">分组ID</param>
        /// <param name="group_name">分组名称</param>
        /// <returns>错误码</returns>
        public ErrorMsg ModityGroupProperty(String group_id, String group_name)
        {
            return GetData<ErrorMsg>(URLModityGroupProperty, new
            {
                group_id,
                group_name
            });
        } 
        #endregion

        #region 修改分组商品 public ErrorMsg ModityGroup(WXGoodsGroupModityProductList list)
        /// <summary>
        /// 修改分组商品
        /// </summary>
        /// <param name="list">微信小店分组商品列表</param>
        /// <returns>错误码</returns>
        public ErrorMsg ModityGroup(WXGoodsGroupModityProductList list)
        {
            return GetData<ErrorMsg>(URLModityGroup, list);
        } 
        #endregion

        #region 获取所有分组 public WXGoodsGroupAllList GetAllGroup()
        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <returns>微信小店分组全部列表</returns>
        public WXGoodsGroupAllList GetAllGroup()
        {
            return GetData<WXGoodsGroupAllList>(URLGetAllGroup);
        } 
        #endregion

        #region 根据分组ID获取分组信息 public WXGoodsGroupInfo GetAllGroupByID(int group_id)
        /// <summary>
        /// 根据分组ID获取分组信息
        /// </summary>
        /// <param name="group_id">分组ID</param>
        /// <returns>微信小店分组信息</returns>
        public WXGoodsGroupInfo GetAllGroupByID(int group_id)
        {
            return GetData<WXGoodsGroupInfo>(URLGetAllGroupByID, new
            {
                group_id
            });
        } 
        #endregion
    }
}