using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Entities.RequestMessage
{
    /// <summary>
    /// 请求对象抽象对象
    /// </summary>
    public abstract class RequestAMessage 
    {
        /// <summary>
        /// 新的对象
        /// </summary>
        public Request Request { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public abstract ReceiveEntityType ReceiveEntityType { get; }

        #region 获取XML数据 public string GetPostData(string key)
        /// <summary>
        /// 获取XML数据
        /// </summary>
        /// <param name="key">数据名称</param>
        /// <returns>XML数据</returns>
        public string GetPostData(string key)
        {
            return Request.GetPostData(key);
        }
        #endregion

        #region 获取请求对象 public static T GetRequestAMessage<T>(Request newRequest)
        /// <summary>
        /// 获取请求对象
        /// </summary>
        /// <typeparam name="T">请求对象类型</typeparam>
        /// <param name="newRequest">新的请求对象</param>
        /// <returns>请求对象</returns>
        public static T GetRequestAMessage<T>(Request newRequest) where T : RequestAMessage, new()
        {
            T t = new T();
            t.SetNewRequest(newRequest);

            return t;
        } 
        #endregion

        #region 设置新的请求对象 public void SetNewRequest(Request newRequest)
        /// <summary>
        /// 设置新的请求对象
        /// </summary>
        /// <param name="newRequest">新的请求对象</param>
        public void SetNewRequest(Request newRequest)
        {
            Request = newRequest;
        }
        #endregion
    }
}
