using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.SendAll
{
    /// <summary>
    /// 群发组
    /// </summary>
    public class SendAllByGroup
    {
        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public Filter filter { get; set; }

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public MPNews mpnews { get; set; }

        /// <summary>
        /// 群发的消息类型，图文消息为mpnews
        /// </summary>
        public string msgtype { get; set; }

        #region 根据group_id和media_id实例化 public SendAllByGroup(string group_id, string media_id)
        /// <summary>
        /// 根据group_id和media_id实例化
        /// </summary>
        /// <param name="group_id">群发到的分组的group_id</param>
        /// <param name="media_id">用于群发的消息的media_id</param>
        public SendAllByGroup(string group_id, string media_id)
        {
            filter = new Filter { group_id = group_id };
            mpnews = new MPNews { media_id = media_id };
            msgtype = "mpnews";
        } 
        #endregion

        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public class Filter
        {
            /// <summary>
            /// 群发到的分组的group_id
            /// </summary>
            public string group_id { get; set; }
        }

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public class MPNews
        {
            /// <summary>
            /// 用于群发的消息的media_id
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
