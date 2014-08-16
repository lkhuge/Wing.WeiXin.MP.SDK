using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Wing.CL.StringManager;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 接收消息控制器
    /// </summary>
    public class ReceiveController
    {
        /// <summary>
        /// 接收开始事件
        /// </summary>
        public static event Action<Request> ReceiveStart;

        /// <summary>
        /// 接收结束事件
        /// </summary>
        public static event Action<Request, Response> ReceiveEnd;

        #region 执行操作 public Response Action(Request request)
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public Response Action(Request request)
        {
            if (ReceiveStart != null) ReceiveStart(request);
            try
            {
                GlobalManager.CheckInit();
                AuthenticationRequest(request);
                request.ParsePostData();
                Response response = GlobalManager.EventManager.ActionEvent(request);
                if (ReceiveEnd != null) ReceiveEnd(request, response);
                return response;
            }
            catch (Exception e)
            {
                return new Response(e);
            }
        }
        #endregion

        #region 验证请求 private void AuthenticationRequest(Request request)
        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="request">请求对象</param>
        private void AuthenticationRequest(Request request)
        {
            if (request == null) throw new Exception("无请求");
            //首次验证
            if (!String.IsNullOrEmpty(request.Echostr))
            {
                if (CheckSignature(request))
                {
                    throw new Exception(request.Echostr);
                }
                throw new Exception("首次验证未通过\nRequest:" + request);
            }
            //消息验证
            if (!CheckMessage(request))
                throw new Exception("消息验证未通过\nRequest:" + request);
        }
        #endregion

        #region 验证signature是否有效 private bool CheckSignature(Request request)
        /// <summary>
        /// 验证signature是否有效
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否有效</returns>
        private bool CheckSignature(Request request)
        {
            string[] arr = new[] 
            { 
                GlobalManager.ConfigManager.BaseConfig.Token, 
                request.Timestamp, 
                request.Nonce
            }.OrderBy(z => z).ToArray();

            return Security.SHA1_Encrypt(string.Join("", arr)).Equals(request.Signature);
        }
        #endregion

        #region 验证消息真实性 private bool CheckMessage(Request request)
        /// <summary>
        /// 验证消息真实性
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否有效</returns>
        private bool CheckMessage(Request request)
        {
            return CheckSignature(request);
        }
        #endregion
    }
}
