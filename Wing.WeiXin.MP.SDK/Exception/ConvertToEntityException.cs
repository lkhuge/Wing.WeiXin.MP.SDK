using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;

namespace Wing.WeiXin.MP.SDK.Exception
{
    /// <summary>
    /// 请求解析为实体的过程中发送异常
    /// </summary>
    public class ConvertToEntityException : WXException
    {
        /// <summary>
        /// 请求对象
        /// </summary>
        public Request request { get; private set; }

        /// <summary>
        /// Key-Value数据
        /// </summary>
        public Dictionary<string, string> kvList { get; private set; }

        #region 根据请求对象实例化 public ConvertToEntityException(Request request)
        /// <summary>
        /// 根据请求对象实例化
        /// </summary>
        /// <param name="request">请求对象</param>
        public ConvertToEntityException(Request request)
            : base(GetErrMsg(request))
        {
            this.request = request;
        } 
        #endregion

        #region 根据快速配置回复消息Key-Value数据实例化 public ConvertToEntityException(Dictionary<string, string> kvList)
        /// <summary>
        /// 根据快速配置回复消息Key-Value数据实例化
        /// </summary>
        /// <param name="kvList">快速配置回复消息Key-Value数据</param>
        public ConvertToEntityException(Dictionary<string, string> kvList)
            : base("快速配置回复消息文件解析为实体的过程中发生异常")
        {
            this.kvList = kvList;
        }
        #endregion

        #region 获取错误信息 private static string GetErrMsg(Request requestObj)
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        private static string GetErrMsg(Request requestObj)
        {
            const string ErrMsg = "请求解析为实体的过程中发生异常（Request:{0}）";
            string requestStr = requestObj == null ? "为空" : String.Format("[postData]:{0}", requestObj.postData);

            return String.Format(ErrMsg, requestStr);
        } 
        #endregion
    }
}
