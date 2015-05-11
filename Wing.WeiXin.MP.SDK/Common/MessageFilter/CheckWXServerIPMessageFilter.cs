using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.MessageFilter
{
    /// <summary>
    /// 检查微信服务器IP消息过滤
    /// 由于部分服务器的外层存在代理，因此该方法不一定能够获取到真正请求IP
    /// </summary>
    public class CheckWXServerIPMessageFilter : IMessageFilter
    {
        /// <summary>
        /// 微信服务器IP
        /// </summary>
        private static WXServerIPList wxServerIPList;

        #region 执行过滤 public Response Action(Request request)
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象（如果为空则跳过过滤）</returns>
        public Response Action(Request request)
        {
            string ip = request.IP;
            if (String.IsNullOrEmpty(ip)) return null;
            return GetIPList(request).Contains(ip) 
                ? null 
                : new Response("非微信服务器请求", request, Response.TEXT);
        }
        #endregion

        #region 获取IP列表 private List<string> GetIPList(Request request)
        /// <summary>
        /// 获取IP列表
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>IP列表</returns>
        private List<string> GetIPList(Request request)
        {
            if (wxServerIPList == null)
            {
                wxServerIPList = GlobalManager.FunctionManager
                    .SecurityController.GetWXServerIPList(request.WXAccount);
            }

            return wxServerIPList.ip_list;
        } 
        #endregion
    }
}
