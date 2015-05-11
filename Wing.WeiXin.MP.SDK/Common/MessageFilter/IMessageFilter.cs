using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Common.MessageFilter
{
    /// <summary>
    /// 消息过滤接口
    /// </summary>
    public interface IMessageFilter
    {
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象（如果为空则跳过过滤）</returns>
        Response Action(Request request);
    }
}
