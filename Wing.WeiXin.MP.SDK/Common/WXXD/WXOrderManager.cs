using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.WXXD.Order;

namespace Wing.WeiXin.MP.SDK.Common.WXXD
{
    /// <summary>
    /// 微信小店订单管理
    /// </summary>
    public class WXOrderManager : WXXDManager
    {
        /// <summary>
        /// 根据订单ID获取订单详情的URL
        /// </summary>
        private const String URLGetOrderByID =
            "https://api.weixin.qq.com/merchant/order/getbyid?access_token={AccessToken}";

        /// <summary>
        /// 根据订单状态/创建时间获取订单详情的URL
        /// </summary>
        private const String URLGetOrderByState =
            "https://api.weixin.qq.com/merchant/order/getbyfilter?access_token={AccessToken}";

        /// <summary>
        /// 设置订单发货信息的URL
        /// </summary>
        private const String URLSetOrderInfo =
            "https://api.weixin.qq.com/merchant/order/setdelivery?access_token={AccessToken}";

        /// <summary>
        /// 关闭订单的URL
        /// </summary>
        private const String URLCloseOrder =
            "https://api.weixin.qq.com/merchant/order/close?access_token={AccessToken}";

        #region 根据订单ID获取订单详情 public WXOrderResponse GetOrderByID(String order_id)
        /// <summary>
        /// 根据订单ID获取订单详情
        /// </summary>
        /// <param name="order_id">订单ID</param>
        /// <returns>微信小店订单响应</returns>
        public WXOrderResponse GetOrderByID(String order_id)
        {
            return GetData<WXOrderResponse>(URLGetOrderByID, order_id);
        } 
        #endregion

        #region 根据订单状态/创建时间获取订单详情 public WXOrderResponse GetOrderByState(int status, long begintime, long endtime)
        /// <summary>
        /// 根据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="status">订单状态(2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
        /// <param name="begintime">订单创建时间起始时间</param>
        /// <param name="endtime">订单创建时间终止时间</param>
        /// <returns>微信小店订单响应</returns>
        public WXOrderResponse GetOrderByState(int status, long begintime, long endtime)
        {
            return GetData<WXOrderResponse>(URLGetOrderByState, new
            {
                status,
                begintime,
                endtime
            });
        } 
        #endregion

        #region 根据订单状态/创建时间获取订单详情 public WXOrderResponse GetOrderByState(int status)
        /// <summary>
        /// 根据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="status">订单状态(2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
        /// <returns>微信小店订单响应</returns>
        public WXOrderResponse GetOrderByState(int status)
        {
            return GetData<WXOrderResponse>(URLGetOrderByState, new
            {
                status
            });
        }
        #endregion

        #region 根据订单状态/创建时间获取订单详情 public WXOrderResponse GetOrderByState(long begintime, long endtime)
        /// <summary>
        /// 根据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="begintime">订单创建时间起始时间</param>
        /// <param name="endtime">订单创建时间终止时间</param>
        /// <returns>微信小店订单响应</returns>
        public WXOrderResponse GetOrderByState(long begintime, long endtime)
        {
            return GetData<WXOrderResponse>(URLGetOrderByState, new
            {
                begintime,
                endtime
            });
        }
        #endregion

        #region 设置订单发货信息 public ErrorMsg SetOrderInfo(String order_id, String delivery_company, String delivery_track_no)
        /// <summary>
        /// 设置订单发货信息
        /// </summary>
        /// <param name="order_id">订单ID</param>
        /// <param name="delivery_company">物流公司ID(参考《物流公司ID》)</param>
        /// <param name="delivery_track_no">运单ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg SetOrderInfo(String order_id, String delivery_company, String delivery_track_no)
        {
            return GetData<ErrorMsg>(URLSetOrderInfo, new
            {
                order_id,
                delivery_company,
                delivery_track_no
            });
        } 
        #endregion

        #region 关闭订单 public ErrorMsg CloseOrder(String order_id)
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="order_id">订单ID</param>
        /// <returns>错误码</returns>
        public ErrorMsg CloseOrder(String order_id)
        {
            return GetData<ErrorMsg>(URLCloseOrder, new
            {
                order_id
            });
        } 
        #endregion
    }
}