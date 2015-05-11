using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;
using Wing.WeiXin.MP.SDK.Enumeration;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Common.MessageFilter
{
    /// <summary>
    /// 重复消息过滤器
    /// 可用于过滤刷屏
    /// </summary>
    public class RepetitionMessageFilter : IMessageFilter
    {
        /// <summary>
        /// 最大重复数
        /// </summary>
        public static int MaxRepetition = 3;

        /// <summary>
        /// 过于重复响应事件
        /// </summary>
        public static Func<Request, Response> OutRepetitionEvent = 
            request => request.GetTextResponse("禁止刷屏");

        /// <summary>
        /// 杂质元素列表
        /// </summary>
        public static string[] ImpurityList =
        {
            " ",
            ",",
            "，",
            ".",
            "。",
            "?",
            "？",
            "!",
            "！",
            ";",
            "；",
            ":",
            "："
        };

        #region 执行过滤 public Response Action(Request request)
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象（如果为空则跳过过滤）</returns>
        public Response Action(Request request)
        {
            if (request.MsgType != ReceiveEntityType.text) return null;
            string content = RemoveImpurity(RequestAMessage.GetRequestAMessage<RequestText>(request).Content);
            string contentTextTemp = GlobalManager.WXSession.Get<string>(request.FromUserName, Settings.Default.RepetitionMessageTextSign);
            int contentCountTemp = GlobalManager.WXSession.Get<int>(request.FromUserName, Settings.Default.RepetitionMessageCountSign);
            contentTextTemp = contentTextTemp ?? "";
            contentCountTemp = contentTextTemp.Equals(content) ? contentCountTemp + 1 : 0;
            GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.RepetitionMessageTextSign, content);
            GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.RepetitionMessageCountSign, contentCountTemp);

            return contentCountTemp > MaxRepetition
                ? OutRepetitionEvent(request)
                : null;
        } 
        #endregion

        #region 去除杂质内容 private static string RemoveImpurity(string message)
        /// <summary>
        /// 去除杂质内容
        /// </summary>
        /// <param name="message">原消息</param>
        /// <returns>过滤后的消息</returns>
        private static string RemoveImpurity(string message)
        {
            return ImpurityList.Aggregate(message, (current, i) => current.Replace(i, ""));
        }
        #endregion
    }
}
