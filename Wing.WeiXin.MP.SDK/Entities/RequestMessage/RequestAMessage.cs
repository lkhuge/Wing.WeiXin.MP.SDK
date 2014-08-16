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
    public abstract class RequestAMessage : Request
    {
        /// <summary>
        /// 新的对象
        /// </summary>
        public Request newRequest { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public abstract ReceiveEntityType ReceiveEntityType { get; }

        #region 实例化空对象 protected RequestAMessage()
        /// <summary>
        /// 实例化空对象
        /// </summary>
        protected RequestAMessage()
            : base(null, null, null, null, null)
        {
        } 
        #endregion

        #region 实例化请求对象 public RequestAMessage(string signature, string timestamp, string nonce, string echostr, string postData)
        /// <summary>
        /// 实例化请求对象
        /// </summary>
        /// <param name="signature">微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="postData">POST数据</param>
        protected RequestAMessage(string signature, string timestamp, string nonce, string echostr, string postData)
            : base(signature, timestamp, nonce, echostr, postData)
        {
        }
        #endregion

        #region 设置新的请求对象 public void SetNewRequest(Request request)
        /// <summary>
        /// 设置新的请求对象
        /// </summary>
        /// <param name="request">新的请求对象</param>
        public void SetNewRequest(Request request)
        {
            RootElement = request.RootElement;
            newRequest = request;
        } 
        #endregion
    }
}
