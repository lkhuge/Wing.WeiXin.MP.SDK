using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
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
        /// 是否计算请求响应时长
        /// </summary>
        public static bool IsSumRunTime = false;

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
            if (ReceiveStart != null)
            {
                LogManager.WriteSystem("触发接收开始事件");
                ReceiveStart(request);
                LogManager.WriteSystem("接收开始事件结束");
            }
            try
            {
                if (needCheck) request.Check();
                request.ParsePostData();
                if (needCheck && CheckMsgID(request)) return null;
                Response response = GlobalManager.EventManager.ActionEvent(request);
                if (ReceiveEnd != null)
                {
                    LogManager.WriteSystem("触发接收结束事件");
                    ReceiveEnd(request, response);
                    LogManager.WriteSystem("接收结束事件结束");
                }
                return response;
            }
            catch (WXException e)
            {
                return new Response(e);
            }
            catch (Exception e)
            {
                if (ReceiveException != null)
                {
                    LogManager.WriteSystem("触发接收异常事件");
                    ReceiveException(request, e);
                    LogManager.WriteSystem("接收异常事件结束");
                }
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
            LogManager.WriteSystem("检测MsgID");
            if (!request.HasPostData("MsgId")) return false;
            if (GlobalManager.WXSessionManager == null) return false;
            string msgID = request.GetMsgId();
            object msgIDTemp = GlobalManager.WXSessionManager.Get(request.FromUserName, "LastMsgID");
            if (msgIDTemp != null)
            {
                LogManager.WriteSystem("检测MsgID通过");
                string lastMsgID = msgIDTemp.ToString();
                if (msgID.Equals(lastMsgID)) return true;
            }
            GlobalManager.WXSessionManager.Set(request.FromUserName, "LastMsgID", msgID);
            LogManager.WriteSystem("检测MsgID未通过");
            return false;
        }
        #endregion
    }
}
