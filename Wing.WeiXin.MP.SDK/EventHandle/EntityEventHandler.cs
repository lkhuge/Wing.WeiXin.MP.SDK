using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 实体处理
    /// </summary>
    /// <typeparam name="T">有事件处理实体</typeparam>
    public abstract class EntityEventHandler<T> where T : Entity
    {
        #region 实体处理 public readonly static EntityHandler<T> EntityEvent
        /// <summary>
        /// 实体处理
        /// </summary>
        public readonly static EntityHandler<T> EntityEvent = new EntityHandler<T>(); 
        #endregion

        #region 根据实体处理 public IReturn Action(T entity)
        /// <summary>
        /// 根据实体处理
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        public IReturn Action(T entity)
        {
            IReturn global = GlobalEntityEventHandler.Action(entity);
            if (global != null) return global;
            if (ConfigManager.EventConfig.UseCustomEventHandler && EntityEvent.EntityEvent != null)
            {
                IReturn custom = EntityEvent.EntityEvent(entity);
                if (custom != null) return custom;
            }
            if (ConfigManager.EventConfig.UseBaseEventHandler)
            {
                IReturn baseEvent = BaseEntityEvent(entity);
                if (baseEvent != null) return baseEvent;
            }
            throw new NoResponseException("无事件处理");
        } 
        #endregion

        #region 基础实体处理事件 protected abstract IReturn BaseEntityEvent(T entity);
        /// <summary>
        /// 基础实体处理事件
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        protected abstract IReturn BaseEntityEvent(T entity); 
        #endregion
    }
}
