using Wing.WeiXin.MP.SDK.Entities.Interface;

namespace Wing.WeiXin.MP.SDK.Entities.HTTP.Request
{
    /// <summary>
    /// 请求对象
    /// </summary>
    public class Request : IRequest
    {
        /// <summary>
        /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string echostr { get; set; }

        /// <summary>
        /// POST数据
        /// </summary>
        public string postData { get; set; }

        #region 获取请求全部信息 public override string ToString()
        /// <summary>
        /// 获取请求全部信息
        /// </summary>
        /// <returns>请求全部信息</returns>
        public override string ToString()
        {
            return string.Format("[signature]:{0}[timestamp]:{1}[nonce]:{2}[echostr]:{3}[postData]:{4}",
                signature, timestamp, nonce, echostr, postData);
        } 
        #endregion
    }
}
