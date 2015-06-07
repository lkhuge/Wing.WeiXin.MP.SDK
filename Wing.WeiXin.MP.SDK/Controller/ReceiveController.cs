using System;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 接收消息控制器
    /// </summary>
    public class ReceiveController
    {
        /// <summary>
        /// 接收异常事件
        /// </summary>
        public static event Action<Request, Exception> ReceiveException;

        #region 执行操作 public Response Action(Request request, bool needCheck = true)
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="needCheck">是否检查请求</param>
        /// <returns>响应对象</returns>
        public Response Action(Request request, bool needCheck = true)
        {
            DebugManager.OnActionRequest(request);
            try
            {
                if (needCheck) request.Check();
                request.ParsePostData();

                return DebugManager.OnActionRequestD(request, 
                    GlobalManager.EventManager.ActionEvent(request));
            }
            catch (WXException e)
            {
                if (e.IsMessage || ReceiveException == null) return new Response(e);
                ReceiveException(request, e);
                return new Response(e);
            }
            catch (Exception e)
            {
                DebugManager.OnCatchException(e);
                throw;
            }
        }
        #endregion
    }
}
