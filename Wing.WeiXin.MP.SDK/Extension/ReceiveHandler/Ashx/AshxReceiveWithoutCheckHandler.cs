using System.Web;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Lib;

namespace Wing.WeiXin.MP.SDK.Extension.ReceiveHandler.Ashx
{
    /// <summary>
    /// 接收事件处理（不需要检查请求）（一般处理程序扩展类）
    /// 用于测试
    /// </summary>
    public class AshxReceiveWithoutCheckHandler : IHttpHandler
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
            Response response = receiveController.Action(
                new Request(
                    HTTPHelper.GetPostStream(context), 
                    HTTPHelper.GetRequestIP(context.Request)), 
                false);

            context.Response.Write(response == null ? "" : response.Text);
        } 
        #endregion

        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable { get { return false; } }
    }
}
