using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wing.WeiXin.MP.SDK.Lib.StringManager;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复消息抽象类
    /// </summary>
    public abstract class BaseReturnMessage : BaseEntity, IReturn
    {
        #region 实例化空数据回复消息 protected BaseReturnMessage()
        /// <summary>
        /// 实例化空数据回复消息
        /// </summary>
        protected BaseReturnMessage()
        {
        } 
        #endregion

        #region 根据接收的实体实例化 protected BaseReturnMessage(BaseEntity entity)
        /// <summary>
        /// 根据接收的实体实例化
        /// </summary>
        /// <param name="entity">接收的实体</param>
        protected BaseReturnMessage(BaseEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            FromUserName = entity.ToUserName;
            ToUserName = entity.FromUserName;
            CreateTime = Message.GetLongTimeNow();
        } 
        #endregion
    }
}
