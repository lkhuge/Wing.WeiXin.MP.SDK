﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll.ByGroup
{
    /// <summary>
    /// 高级群发图片消息（根据微信用户分组）
    /// </summary>
    public class SendAllByGroupImage : SendAllByGroup
    {
        /// <summary>
        /// 用于设定即将发送的图片消息
        /// </summary>
        public MPImage image { get; set; }

        #region 实例化空的高级群发图片消息 public SendAllByGroupImage()
        /// <summary>
        /// 实例化空的高级群发图片消息
        /// </summary>
        public SendAllByGroupImage()
        {
            msgtype = "image";
        } 
        #endregion

        #region 根据用于群发的消息的media_id和微信用户分组实例化高级群发图片消息 public SendAllByGroupImage(string media_id, string group_id)
        /// <summary>
        /// 根据用于群发的消息的media_id和微信用户分组实例化高级群发图片消息
        /// </summary>
        /// <param name="media_id">用于群发的消息的media_id</param>
        /// <param name="group_id">微信用户分组</param>
        public SendAllByGroupImage(string media_id, string group_id)
        {
            msgtype = "image";
            filter = new Filter
            {
                group_id = group_id
            };
            image = new MPImage
            {
                media_id = media_id
            };
        } 
        #endregion

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public class MPImage
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
