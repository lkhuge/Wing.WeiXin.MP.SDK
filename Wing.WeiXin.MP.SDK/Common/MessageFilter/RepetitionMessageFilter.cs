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
    public static class RepetitionMessageFilter
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

        #region 添加重复消息过滤器 public static void AddFilter(string toUserName)
        /// <summary>
        /// 添加重复消息过滤器
        /// </summary>
        /// <param name="toUserName">开发者微信号（如果为空则为全局事件）</param>
        public static void AddFilter(string toUserName)
        {
            GlobalManager.EventManager.AddSystemReceiveEvent(toUserName, request =>
            {
                if (request.MsgType != ReceiveEntityType.text) return null;
                RequestText requestText = RequestAMessage.GetRequestAMessage<RequestText>(request);
                string content = RemoveImpurity(requestText.Content);
                string contentTextTemp = GlobalManager.WXSession.Get<string>(request.FromUserName, Settings.Default.RepetitionMessageTextSign);
                int contentCountTemp = GlobalManager.WXSession.Get<int>(request.FromUserName, Settings.Default.RepetitionMessageCountSign);
                contentTextTemp = contentTextTemp ?? "";
                contentCountTemp = contentTextTemp.ToString().Equals(content) ? contentCountTemp + 1 : 0;
                GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.RepetitionMessageTextSign, content);
                GlobalManager.WXSession.Set(request.FromUserName, Settings.Default.RepetitionMessageCountSign, contentCountTemp);

                return contentCountTemp > MaxRepetition
                    ? OutRepetitionEvent(request) 
                    : null;
            });
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
            return message
                .Replace(" ", "")
                .Replace(",", "")
                .Replace("，", "")
                .Replace(".", "")
                .Replace("。", "")
                .Replace("?", "")
                .Replace("？", "")
                .Replace("!", "")
                .Replace("！", "")
                .Replace(";", "")
                .Replace("；", "")
                .Replace(":", "")
                .Replace("：", "");
        } 
        #endregion
    }
}
