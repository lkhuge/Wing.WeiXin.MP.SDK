using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.EventHandle
{
    /// <summary>
    /// 实体处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityHandler<T> where T : Entity
    {
        #region 实体处理委托 public delegate IReturn Handler(T entity);
        /// <summary>
        /// 实体处理委托
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体</returns>
        public delegate IReturn Handler(T entity);
        #endregion

        #region 实体处理事件 private Handler _handler;
        /// <summary>
        /// 实体处理事件
        /// </summary>
        private Handler _handler;
        #endregion

        #region 锁定标志 private const String lockMe = "lockMe";
        /// <summary>
        /// 锁定标志
        /// </summary>
        private const String lockMe = "lockMe";
        #endregion

        #region 实体处理事件 public Handler EntityEvent;
        /// <summary>
        /// 实体处理事件
        /// </summary>
        public Handler EntityEvent
        {
            get
            {
                lock (lockMe)
                {
                    return _handler;
                }
            }
            set
            {
                if (value == null) return;
                lock (lockMe)
                {
                    _handler = value;
                }
            }
        }
        #endregion
    }
}
