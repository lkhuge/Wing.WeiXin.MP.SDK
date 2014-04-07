using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Entities.Interface;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Serialize;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 实体处理
    /// </summary>
    /// <typeparam name="T">有事件处理实体</typeparam>
    public abstract class EntityEventHandler<T> where T : IEvent
    {
        #region 实体处理委托 public delegate IReturn EntityHandler(T entity);
        /// <summary>
        /// 实体处理委托
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        public delegate IReturn EntityHandler(T entity); 
        #endregion

        #region 实体处理事件 private static EntityHandler _entityEvent; 
        /// <summary>
        /// 实体处理事件
        /// </summary>
        private static EntityHandler _entityEvent; 
        #endregion

        #region 锁定标志 private const String lockMe = "lockMe"; 
        /// <summary>
        /// 锁定标志
        /// </summary>
        private const String lockMe = "lockMe"; 
        #endregion

        #region 实体处理事件 public static EntityHandler EntityEvent;
        /// <summary>
        /// 实体处理事件
        /// </summary>
        public static EntityHandler EntityEvent {
            protected get
            {
                lock (lockMe)
                {
                    return _entityEvent;
                }
            }
            set
            {
                lock (lockMe)
                {
                    _entityEvent = value;
                }
            }
        }
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
            if (ConfigManager.EventConfig.UseCustomEventHandler && EntityEvent != null)
            {
                IReturn custom = EntityEvent(entity);
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
