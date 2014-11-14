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

        /// <summary>
        /// 接收异常事件
        /// </summary>
        public static event Action<Request, Exception> ReceiveException;

        #region 执行操作 public Response Action(Request request, bool needCheck = true)
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="needCheck">是否需要检查请求</param>
        /// <returns>响应对象</returns>
        public Response Action(Request request, bool needCheck = true)
        {
            GlobalManager.CheckInit();
            if (ReceiveStart != null) ReceiveStart(request);
            try
            {
                if (needCheck) request.Check();
                request.ParsePostData();
                if (CheckMsgID(request)) return null;
                Response response = GlobalManager.EventManager.ActionEvent(request);
                if (ReceiveEnd != null) ReceiveEnd(request, response);
                return response;
            }
            catch (MessageException e)
            {
                return new Response(e);
            }
            catch (Exception e)
            {
                if (ReceiveException != null) ReceiveException(request, e);
                return new Response(e);
            }
        }
        #endregion

        #region 检测MsgID防止消息重复 private bool CheckMsgID(Request request)
        /// <summary>
        /// 检测MsgID防止消息重复
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>是否息重复</returns>
        private bool CheckMsgID(Request request)
        {
            if (!request.HasPostData("MsgId")) return false;
            if (GlobalManager.WXSessionManager == null) return false;
            string msgID = request.GetMsgId();
            object msgIDTemp = GlobalManager.WXSessionManager.Get(request.FromUserName, "LastMsgID");
            if (msgIDTemp != null)
            {
                string lastMsgID = msgIDTemp.ToString();
                if (msgID.Equals(lastMsgID)) return true;
            }
            GlobalManager.WXSessionManager.Set(request.FromUserName, "LastMsgID", msgID);

            return false;
        } 
        #endregion
    }
}
