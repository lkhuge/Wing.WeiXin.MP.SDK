using System.IO;
using System.Text;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.HTTP.Request
{
    /// <summary>
    /// 根据HttpContext获取的请求对象
    /// </summary>
    public class HttpContextRequest : Request
    {
        #region 根据HTTP上下文实例化 public RequestFromHttpContext(HttpContext context)
        /// <summary>
        /// 根据HTTP上下文实例化
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        public HttpContextRequest(HttpContext context)
        {
            signature = context.Request.QueryString["signature"];
            timestamp = context.Request.QueryString["timestamp"];
            nonce = context.Request.QueryString["nonce"];
            echostr = context.Request.QueryString["echostr"];
            try
            {
                postData = new StreamReader(context.Request.InputStream, Encoding.UTF8).ReadToEnd();
            }
            catch
            {
                postData = "";
            }
        } 
        #endregion
    }
}
