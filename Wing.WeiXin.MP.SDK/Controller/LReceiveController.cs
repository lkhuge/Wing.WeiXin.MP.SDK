using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Common;
using Wing.WeiXin.MP.SDK.Common.WXSession;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Controller
{
    /// <summary>
    /// 轻量级接收消息控制器
    /// 只支持单账号
    /// </summary>
    public class LReceiveController
    {
        /// <summary>
        /// 事件管理类
        /// </summary>
        public EventManager EventManager { get; private set; }

        /// <summary>
        /// 微信会话接口
        /// </summary>
        public IWXSession WXSession { get; private set; }

        /// <summary>
        /// 是否需要检查请求
        /// </summary>
        public bool NeedCheck { get; set; }

        /// <summary>
        /// 微信服务器IP
        /// </summary>
        public WXServerIPList WXServerIPList;

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

        #region 根据微信账号实例化 public LReceiveController(IWXSession wxSession)
        /// <summary>
        /// 根据微信账号实例化
        /// </summary>
        /// <param name="wxSession">微信会话接口</param>
        public LReceiveController(IWXSession wxSession)
        {
            EventManager = new EventManager();
            EventManager.IsCheckEventName = false;
            EventManager.IsCheckToUserName = false;
            WXSession = wxSession;
            NeedCheck = true;
            if (wxSession != null) 
                WXController.AccessTokenContainer = new AccessTokenContainer(wxSession);
        } 
        #endregion

        #region 执行事件 public Response Action(Request request)
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>响应对象</returns>
        public Response Action(Request request)
        {
            if (ReceiveStart != null)
            {
                LogManager.WriteSystem("触发接收开始事件");
                ReceiveStart(request);
                LogManager.WriteSystem("接收开始事件结束");
            }
            try
            {
                if (NeedCheck) request.Check();
                request.ParsePostData();
                if (NeedCheck)
                {
                    CheckRequestIP(request);
                    if (CheckMsgID(request)) return null;
                }
                Response response = EventManager.ActionEvent(request);
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
                if (!e.IsMessage && ReceiveException != null)
                {
                    LogManager.WriteSystem("触发接收异常事件");
                    ReceiveException(request, e);
                    LogManager.WriteSystem("接收异常事件结束");
                }
                return new Response(e);
            }
        } 
        #endregion

        #region 检测请求IP private void CheckRequestIP(Request request)
        /// <summary>
        /// 检测请求IP
        /// </summary>
        /// <param name="request">请求对象</param>
        private void CheckRequestIP(Request request)
        {
            if (WXServerIPList == null || String.IsNullOrEmpty(request.IP)) return;
            LogManager.WriteSystem(String.Format("触发检查微信服务器IP（请求者IP:{0}）", request.IP));
            bool result = WXServerIPList.ip_list.Contains(request.IP);
            LogManager.WriteSystem("检查微信服务器IP" + (result ? "通过" : "不通过"));
            if (!result) throw WXException.GetInstance(
                String.Format("检查微信服务器IP不通过（请求者IP:{0}）", request.IP), request.ToUserName);
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
            if (WXSession == null) return false;
            string msgID = request.GetMsgId();
            string msgIDTemp = WXSession.Get<string>(request.FromUserName, Settings.Default.LastMsgIDKey);
            if (msgIDTemp != null)
            {
                LogManager.WriteSystem("检测MsgID通过");
                string lastMsgID = msgIDTemp;
                if (msgID.Equals(lastMsgID)) return true;
            }
            WXSession.Set(request.FromUserName, Settings.Default.LastMsgIDKey, msgID);
            LogManager.WriteSystem("检测MsgID未通过");
            return false;
        }
        #endregion
    }
}
