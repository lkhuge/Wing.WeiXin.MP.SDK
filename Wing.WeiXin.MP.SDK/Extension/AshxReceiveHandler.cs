using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Wing.CL.Net;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Extension
{
    /// <summary>
    /// 接收事件处理
    /// </summary>
    public class AshxReceiveHandler : IHttpHandler
    {
        /// <summary>
        /// 接收消息控制器
        /// </summary>
        private readonly ReceiveController receiveController = new ReceiveController();

        #region 响应事件 public void ProcessRequest(HttpContext context)
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>响应结果</returns>
        public void ProcessRequest(HttpContext context)
        {
            Response response = receiveController.Action(new Request(
                    context.Request.QueryString["signature"],
                    context.Request.QueryString["timestamp"],
                    context.Request.QueryString["nonce"],
                    context.Request.QueryString["echostr"],
                    HTTPHelper.GetPostStream(context)));

            context.Response.Write(response == null ? "" : response.Text);
        } 
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get; private set; }
    }
}
