using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Properties;

namespace Wing.WeiXin.MP.SDK.Lib
{
    /// <summary>
    /// HTTP工具类
    /// </summary>
    public static class HTTPHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        static HTTPHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
        } 

        #region 使用Get方法获取字符串结果 public static string Get(string url, Encoding encoding = null)
        /// <summary>
        /// 使用Get方法获取字符串结果
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Get(string url, Encoding encoding = null)
        {
            WebClient wc = new WebClient
            {
                Encoding = encoding ?? Encoding.UTF8,
            };
            return wc.DownloadString(url);
        }
        #endregion

        #region 使用Post方法获取字符串结果 public static string Post(string url, string data, Encoding encoding = null)
        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">请求</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        public static string Post(string url, string data, Encoding encoding = null)
        {
            WebClient wc = new WebClient
            {
                Encoding = encoding ?? Encoding.UTF8,
            };
            return wc.UploadString(url, data);
        }
        #endregion

        #region 下载文件 public static string DownloadFile(string url, string pathname, string data = null)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <param name="data">POST参数（如果该参数不为空则使用POST方式下载）</param>
        /// <returns>响应内容</returns>
        public static string DownloadFile(string url, string pathname, string data = null)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null) throw new NullReferenceException();
            bool isGet = String.IsNullOrEmpty(data);
            webRequest.Method = isGet ? "GET" : "POST";
            if (!isGet)
            {
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] dataArray = Encoding.UTF8.GetBytes(data);
                    requestStream.Write(dataArray, 0, dataArray.Length);
                }
            }
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            if (webResponse == null) throw new NullReferenceException();
            using (Stream webStream = webResponse.GetResponseStream())
            {
                if (webStream == null) throw new NullReferenceException();
                if (webResponse.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw WXException.GetInstance("404", Settings.Default.SystemUsername);
                }
                if (webResponse.ContentType.Equals("text/plain"))
                {
                    return new StreamReader(webStream, Encoding.UTF8).ReadToEnd();
                }
                byte[] bt = new byte[1024];
                int osize = webStream.Read(bt, 0, bt.Length);
                using (Stream fileStream = new FileStream(pathname, FileMode.Create))
                {
                    while (osize > 0)
                    {
                        fileStream.Write(bt, 0, osize);
                        osize = webStream.Read(bt, 0, bt.Length);
                    }
                }
            }

            return null;
        }
        #endregion

        #region 上传文件 public static string Upload(string url, string path, string name)
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url">文件上传到的服务器</param>
        /// <param name="path">要上传的本地文件路径</param>
        /// <param name="name">文件上传后的名称</param>
        /// <returns>成功返回1，失败返回0</returns>
        public static string Upload(string url, string path, string name)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 60000;
            using (MemoryStream postStream = new MemoryStream())
            {
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                string fileName = String.Format("{0}/{1}", path, name);
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    string formdata = String.Format("\r\n--{0}\r\nContent-Disposition: form-data; name=\"media\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", boundary, fileName);
                    byte[] formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        postStream.Write(buffer, 0, bytesRead);
                    }
                }
                byte[] footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);
                request.ContentType = String.Format("multipart/form-data; boundary={0}", boundary);
                request.ContentLength = postStream.Length;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
                postStream.Position = 0;
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                if (responseStream == null) throw new Exception("GetResponseStream Is Null");
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    return myStreamReader.ReadToEnd();
                }
            }
        }
        #endregion

        #region 获取Post请求流 public static string GetPostStream(HttpContext httpContext, Encoding encoding = null)
        /// <summary>
        /// 获取Post请求流
        /// </summary>
        /// <param name="httpContext">上下文</param>
        /// <param name="encoding">编码</param>
        /// <returns>Post请求流字符串</returns>
        public static string GetPostStream(HttpContext httpContext, Encoding encoding = null)
        {
            try
            {
                return new StreamReader(
                    httpContext.Request.InputStream,
                    encoding ?? Encoding.UTF8).ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取请求IP public static string GetRequestIP(HttpRequest request)
        /// <summary>
        /// 获取请求IP
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>真实请求IP</returns>
        public static string GetRequestIP(HttpRequest request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!String.IsNullOrEmpty(result))
            {
                if (result.IndexOf(".") == -1) return null;
                if (result.IndexOf(",") == -1) return result;
                return result.Split(',').FirstOrDefault(i => 
                    !i.StartsWith("192.168") && !i.StartsWith("10") && !i.StartsWith("172.16"));
            }
            result = request.ServerVariables["REMOTE_ADDR"];
            return !String.IsNullOrEmpty(result) 
                ? result
                : request.UserHostAddress;
        }

        #endregion
    }
}
