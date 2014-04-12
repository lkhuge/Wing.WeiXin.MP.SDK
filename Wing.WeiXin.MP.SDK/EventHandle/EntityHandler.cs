using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReceiveMessages;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 实体处理对象
    /// </summary>
    public class EntityHandler
    {
        #region 全局事件处理 public Func<BaseReceiveMessage, IReturn>[] GlobalHandler { get; set; }
        /// <summary>
        /// 全局事件处理
        /// </summary>
        public Func<BaseReceiveMessage, IReturn>[] GlobalHandler { get; set; }  
        #endregion

        #region 基于微信用户事件处理 public Dictionary<string, Func<BaseReceiveMessage, IReturn>> WXUserBaseHandler { get; set; }
        /// <summary>
        /// 基于微信用户事件处理
        /// </summary>
        public Dictionary<string, Func<BaseReceiveMessage, IReturn>> WXUserBaseHandler { get; set; }
        #endregion

        #region 基于微信用户分组事件处理 public Dictionary<int, Func<BaseReceiveMessage, IReturn>> WXUserGroupBaseHandler { get; set; }
        /// <summary>
        /// 基于微信用户分组事件处理
        /// </summary>
        public Dictionary<int, Func<BaseReceiveMessage, IReturn>> WXUserGroupBaseHandler { get; set; }
        #endregion

        #region 自定义实体处理 public Dictionary<ReceiveEntityType, Func<BaseReceiveMessage, IReturn>> CustomEntityHandler { get; set; }
        /// <summary>
        /// 自定义实体处理列表
        /// </summary>
        public Dictionary<ReceiveEntityType, Func<BaseReceiveMessage, IReturn>> CustomEntityHandler { get; set; } 
        #endregion
    }
}
