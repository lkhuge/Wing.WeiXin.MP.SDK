using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;

namespace Wing.WeiXin.MP.SDK.Extension.Event
{
    /// <summary>
    /// 非全局事件生成器接口
    /// </summary>
    /// <typeparam name="T">非全局事件</typeparam>
    public interface IEventBuilder<in T> where T : RequestAMessage
    {
        /// <summary>
        /// 生成非全局事件
        /// </summary>
        /// <returns>非全局事件</returns>
        Func<T, Response> GetEvent();
    }
}
