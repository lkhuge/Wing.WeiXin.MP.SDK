using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.CSMessages
{
    /// <summary>
    /// 客服消息类
    /// </summary>
    public class CSMessage
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }

        #region 实例化空数据客服消息 public CSMessage()
        /// <summary>
        /// 实例化空数据客服消息
        /// </summary>
        public CSMessage()
        {
        } 
        #endregion

        #region 根据普通用户openid实例化 public CSMessage(string touser)
        /// <summary>
        /// 根据普通用户openid实例化
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        public CSMessage(string touser)
        {
            if (String.IsNullOrEmpty(touser)) throw new ArgumentNullException("touser");
            this.touser = touser;
        } 
        #endregion
    }
}
