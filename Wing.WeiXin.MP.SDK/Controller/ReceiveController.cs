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
                request.Check();
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
    }
}
