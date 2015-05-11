using System;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

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
            LogManager.WriteInfo("请求：" + Environment.NewLine + request);
            try
            {
                if (needCheck) request.Check();
                request.ParsePostData();
                Response response = GlobalManager.EventManager.ActionEvent(request, needCheck);
                LogManager.WriteInfo("响应：" + Environment.NewLine + response);

                return response;
            }
            catch (WXException e)
            {
                if (e.IsMessage || ReceiveException == null) return new Response(e);
                ReceiveException(request, e);
                return new Response(e);
            }
            catch (Exception e)
            {
                LogManager.WriteError("发生不可预料的异常", e);
                throw;
            }
        }
        #endregion
    }
}
