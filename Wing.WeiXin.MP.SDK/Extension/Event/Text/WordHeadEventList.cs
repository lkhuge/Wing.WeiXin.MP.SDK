using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.RequestMessage.Message;

namespace Wing.WeiXin.MP.SDK.Extension.Event.Text
{
    /// <summary>
    /// 以字符串作为头部区分的事件列表
    /// </summary>
    public class WordHeadEventList
    {
        /// <summary>
        /// 事件列表
        /// </summary>
        private readonly Dictionary<string, Func<string, Request, Response>> eventList;

        /// <summary>
        /// 字符串头部是否区分大小写
        /// </summary>
        private readonly bool isCaseSensitive;

        #region 根据事件列表和字符串头部是否区分大小写实例化以字符串作为头部区分的事件列表 public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, bool isCaseSensitive = false)
        /// <summary>
        /// 根据事件列表和字符串头部是否区分大小写实例化以字符串作为头部区分的事件列表
        /// </summary>
        /// <param name="eventList">事件列表</param>
        /// <param name="isCaseSensitive">字符串头部是否区分大小写</param>
        public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, bool isCaseSensitive = false)
        {
            if (eventList == null) throw new ArgumentNullException("eventList");
            this.eventList = eventList;
            this.isCaseSensitive = isCaseSensitive;
        } 
        #endregion

        #region 根据分割字符获取以字符串作为头部区分的事件列表 public Func<RequestText, Response> GetEventWithSeparatorWord(char separatorWord, bool actionByConfig = true, string actionNameHead = null)
        /// <summary>
        /// 根据分割字符获取以字符串作为头部区分的事件列表
        /// </summary>
        /// <param name="separatorWord">分割字符</param>
        /// <param name="actionByConfig">是否需要根据配置判断是否执行</param>
        /// <param name="actionNameHead">配置前缀（配置名称格式：配置前缀@消息头部）</param>
        /// <returns>以字符串作为头部区分的事件列表</returns>
        public Func<RequestText, Response> GetEventWithSeparatorWord(char separatorWord, bool actionByConfig = true, string actionNameHead = null)
        {
            return request =>
            {
                string text = request.Content;
                if (String.IsNullOrEmpty(text)) return null;
                int index = text.IndexOf(separatorWord);
                if (index == -1) return null;

                return Handler(
                    text.Substring(0, index).Trim(), 
                    text.Substring(index + 1).Trim(),
                    request.Request,
                    actionByConfig,
                    actionNameHead);
            };
        } 
        #endregion

        #region 根据头部字符串长度获取以字符串作为头部区分的事件列表 public Func<RequestText, Response> GetEventWithoutSeparatorWord(int headWordLength, bool actionByConfig = true, string actionNameHead = null)
        /// <summary>
        /// 根据头部字符串长度获取以字符串作为头部区分的事件列表
        /// </summary>
        /// <param name="headWordLength">头部字符串长度</param>
        /// <param name="actionByConfig">是否需要根据配置判断是否执行</param>
        /// <param name="actionNameHead">配置前缀（配置名称格式：配置前缀@消息头部）</param>
        /// <returns>以字符串作为头部区分的事件列表</returns>
        public Func<RequestText, Response> GetEventWithoutSeparatorWord(int headWordLength, bool actionByConfig = true, string actionNameHead = null)
        {
            return request =>
            {
                string text = request.Content;
                if (String.IsNullOrEmpty(text) || text.Length <= headWordLength) return null;

                return Handler(
                    text.Substring(0, headWordLength).Trim(),
                    text.Substring(headWordLength).Trim(),
                    request.Request,
                    actionByConfig,
                    actionNameHead);
            };
        }
        #endregion

        #region 执行事件 private Response Handler(string head, string content, Request request, bool actionByConfig, string actionNameHead)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="head">字符串头部</param>
        /// <param name="content">字符串主体</param>
        /// <param name="request">请求</param>
        /// <param name="actionByConfig">是否需要根据配置判断是否执行</param>
        /// <param name="actionNameHead">配置前缀（配置名称格式：配置前缀@消息头部）</param>
        /// <returns>响应</returns>
        private Response Handler(string head, string content, Request request, bool actionByConfig, string actionNameHead)
        {
            if (!eventList.ContainsKey(head)) return null;
            if (actionByConfig && !GlobalManager.CheckEventAction(String.Format("{0}@{1}", actionNameHead, head))) return null;
            Func<string, Request, Response> eventTemp = eventList
                .FirstOrDefault(e => isCaseSensitive ? e.Key.Equals(head) : e.Key.ToLower().Equals(head.ToLower())).Value;

            return eventTemp == null ? null : eventTemp(content, request);
        } 
        #endregion
    }
}
