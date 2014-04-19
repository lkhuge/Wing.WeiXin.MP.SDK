using System;
using System.IO;
using System.Web;
using log4net;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.HTTP;
using Wing.WeiXin.MP.SDK.Entities.HTTP.Request;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.StringManager;
using BaseException = System.Exception;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 接收消息控制器
    /// </summary>
    public static class ReceiveController
    {
        #region 执行操作 public static Response Action(Request request)
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public static Response Action(Request request)
        {
            if(ConfigManager.DebugConfig.IsDebug) LogHelper.Info(request.ToString(), typeof(ReceiveController));
            Response response;
            try
            {
                response = EntityFactory.RequestHandle(request);
                if (ConfigManager.DebugConfig.IsDebug) LogHelper.Info(response.ToString(), typeof(ReceiveController));
            }
            catch (WXException e)
            {
                response = new Response(e);
            }
            catch (BaseException e)
            {
                throw new WXException("接收消息", e);
            }
            return response;
        } 
        #endregion

        #region 为HttpContext简化代码执行方法 public static void ActionForHttpContext(HttpContext context)
        /// <summary>
        /// 为HttpContext简化代码执行方法
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        public static void ActionForHttpContext(HttpContext context)
        {
            Action(new HttpContextRequest(context)).ResponseOutput(context.Response);
        }
        #endregion
    }
}
