using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.MessageFilter
{
    /// <summary>
    /// 检查MsgID消息过滤器
    /// </summary>
    public class CheckMsgIDMessageFilter : IMessageFilter
    {
        #region 执行过滤 public Response Action(Request request)
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象（如果为空则跳过过滤）</returns>
        public Response Action(Request request)
        {
            if (!request.HasPostData("MsgId")) return null;
            if (GlobalManager.WXSession == null) return null;
            string msgID = request.GetMsgId();
            string msgIDTemp = GlobalManager.WXSession.Get<string>(request.FromUserName, Settings.Default.LastMsgIDKey);
            if (!String.IsNullOrEmpty(msgIDTemp) && msgID.Equals(msgIDTemp))
            {
                return new Response("MsgID重复", request, Response.TEXT);
            }
            GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.LastMsgIDKey, msgID);
            return null;
        } 
        #endregion
    }
}
