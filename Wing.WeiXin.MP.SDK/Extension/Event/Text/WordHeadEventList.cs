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
    public class WordHeadEventList : IEventBuilder<RequestText>
    {
        /// <summary>
        /// 字符串头部是否区分大小写
        /// </summary>
        public bool IsCaseSensitive;

        /// <summary>
        /// 事件列表
        /// </summary>
        private readonly Dictionary<string, Func<string, Request, Response>> eventList;

        /// <summary>
        /// 返回事件列表
        /// </summary>
        private readonly Func<RequestText, Response> returnEvent;

        #region 根据事件列表和分割字符实例化根据分割字符获取以字符串作为头部区分的事件列表 public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, char separatorWord)
        /// <summary>
        /// 根据事件列表和分割字符实例化根据分割字符获取以字符串作为头部区分的事件列表
        /// </summary>
        /// <param name="eventList">事件列表</param>
        /// <param name="separatorWord">分割字符</param>
        public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, char separatorWord)
        {
            this.eventList = eventList;
            returnEvent = request =>
            {
                string text = request.Content;
                if (String.IsNullOrEmpty(text)) return null;
                int index = text.IndexOf(separatorWord);
                if (index == -1) return null;

                return Handler(
                    text.Substring(0, index).Trim(),
                    text.Substring(index + 1).Trim(),
                    request.Request);
            };
        } 
        #endregion

        #region 根据事件列表和头部字符串长度写实例化根据头部字符串长度获取以字符串作为头部区分的事件列表 public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, int headWordLength)
        /// <summary>
        /// 根据事件列表和头部字符串长度写实例化根据头部字符串长度获取以字符串作为头部区分的事件列表
        /// </summary>
        /// <param name="eventList">事件列表</param>
        /// <param name="headWordLength">头部字符串长度</param>
        public WordHeadEventList(Dictionary<string, Func<string, Request, Response>> eventList, int headWordLength)
        {
            this.eventList = eventList;
            returnEvent = request =>
            {
                string text = request.Content;
                if (String.IsNullOrEmpty(text) || text.Length <= headWordLength) return null;

                return Handler(
                    text.Substring(0, headWordLength).Trim(),
                    text.Substring(headWordLength).Trim(),
                    request.Request);
            };
        }
        #endregion

        #region 获取以字符串作为头部区分的事件列表 public Func<RequestText, Response> GetEvent()
        /// <summary>
        /// 获取以字符串作为头部区分的事件列表
        /// </summary>
        /// <returns>以字符串作为头部区分的事件列表</returns>
        public Func<RequestText, Response> GetEvent()
        {
            return returnEvent;
        }
        #endregion

        #region 执行事件 private Response Handler(string head, string content, Request request)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="head">字符串头部</param>
        /// <param name="content">字符串主体</param>
        /// <param name="request">请求</param>
        /// <returns>响应</returns>
        private Response Handler(string head, string content, Request request)
        {
            if (!eventList.ContainsKey(head)) return null;
            Func<string, Request, Response> eventTemp = eventList
                .FirstOrDefault(e => IsCaseSensitive ? e.Key.Equals(head) : e.Key.ToLower().Equals(head.ToLower())).Value;

            return eventTemp == null ? null : eventTemp(content, request);
        } 
        #endregion
    }
}
