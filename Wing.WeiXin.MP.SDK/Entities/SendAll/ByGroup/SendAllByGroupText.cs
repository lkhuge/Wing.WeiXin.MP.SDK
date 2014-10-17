﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发文本消息（根据微信用户分组）
    /// </summary>
    public class SendAllByGroupText : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public MPText text { get; set; }

        #region 实例化空的高级群发文本消息 public SendAllByGroupText()
        /// <summary>
        /// 实例化空的高级群发文本消息
        /// </summary>
        public SendAllByGroupText()
        {
            msgtype = "text";
        } 
        #endregion

        #region 根据文本消息和微信用户分组实例化高级群发文本消息 public SendAllByGroupText(string content, string group_id)
        /// <summary>
        /// 根据文本消息和微信用户分组实例化高级群发文本消息
        /// </summary>
        /// <param name="content">文本消息</param>
        /// <param name="group_id">微信用户分组</param>
        public SendAllByGroupText(string content, string group_id)
        {
            msgtype = "text";
            filter = new Filter
            {
                group_id = group_id
            };
            text = new MPText
            {
                content = content
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的文本消息
        /// </summary>
        public class MPText
        {
            /// <summary>
            /// 文本消息
            /// </summary>
            public string content { get; set; }
        }
    }
}