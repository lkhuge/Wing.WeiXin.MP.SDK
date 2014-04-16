using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Lib.Net
{
    /// <summary>
    /// 邮件工具类
    /// </summary>
    public class MailHelper : IDisposable
    {
        /// <summary>
        /// 发送者邮箱
        /// </summary>
        public static string FromMail { get; set; }

        /// <summary>
        /// 发送服务器地址
        /// </summary>
        public static string SMTPHost { get; set; }

        /// <summary>
        /// 发送服务器端口
        /// </summary>
        public static int SMTPPort { get; set; }

        /// <summary>
        /// 发送邮件用户名
        /// </summary>
        public static string SMTPUser { get; set; }

        /// <summary>
        /// 发送邮件密码
        /// </summary>
        public static string SMTPPassword { get; set; }

        /// <summary>
        /// 发送客户端对象
        /// </summary>
        private readonly SmtpClient client;

        #region 实例化 public MailSender()
        /// <summary>
        /// 实例化
        /// </summary>
        public MailHelper()
        {
            if (String.IsNullOrEmpty(SMTPHost)
                || String.IsNullOrEmpty(SMTPUser)
                || String.IsNullOrEmpty(SMTPPassword)
                || SMTPPort == 0)
                throw new WXException("发送邮件参数未完全输入");
            client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Host = SMTPHost,
                Port = SMTPPort,
                Credentials = new NetworkCredential(SMTPUser, SMTPPassword)
            };
        }
        #endregion

        #region 发送邮件 public void SendMail(string toMail, string subject, string body)
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toMail">接收端邮箱</param>
        /// <param name="subject">主体</param>
        /// <param name="body">邮件主体</param>
        public void SendMail(string toMail, string subject, string body)
        {
            client.Send(new MailMessage(FromMail, toMail, subject, body));
        }
        #endregion

        #region 回收资源 public void Dispose()
        /// <summary>
        /// 回收资源
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
        #endregion
    }
}
