﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Lib.Net
{
    /// <summary>
    /// HTTP请求工具类
    /// </summary>
    public static class HTTPHelper
    {
        #region 使用Get方法获取字符串结果 public static string Get(string url, Encoding encoding = null)
        /// <summary>
        /// 使用Get方法获取字符串结果
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        public static string Get(string url, Encoding encoding = null)
        {
            WebClient wc = new WebClient { Encoding = encoding ?? Encoding.UTF8 };
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
            encoding = encoding ?? Encoding.UTF8;
            byte[] postData = encoding.GetBytes(data);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) return "";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postData, 0, postData.Length);
                outstream.Flush();
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response == null) return "";
            using (Stream instream = response.GetResponseStream())
            {
                if (instream == null) return "";
                using (StreamReader sr = new StreamReader(instream, encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        #endregion

        #region 将本地文件上传到指定的服务器(HttpWebRequest方法) public static string Upload(string address, string path, string name, string method = "POST")
        /// <summary>
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)
        /// </summary>
        /// <param name="address">文件上传到的服务器</param>
        /// <param name="path">要上传的本地文件路径</param>
        /// <param name="name">文件上传后的名称</param>
        /// <param name="method">上传方式</param>
        /// <returns>成功返回1，失败返回0</returns>
        public static string Upload(string address, string path, string name, string method = "POST")
        {
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");
            const string h = "--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            string header = String.Format(h, strBoundary, name);
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(header);
            HttpWebRequest httpReq = WebRequest.Create(new Uri(address)) as HttpWebRequest;
            if (httpReq == null) return "";
            httpReq.Method = method;
            httpReq.AllowWriteStreamBuffering = false;
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            using (FileStream fs = new FileStream(String.Format("{0}/{1}", path, name), FileMode.Open, FileAccess.Read))
            {
                long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
                httpReq.ContentLength = length;
                using (BinaryReader r = new BinaryReader(fs))
                {
                    const int bufferLength = 4096;
                    byte[] buffer = new byte[bufferLength];
                    int size = r.Read(buffer, 0, bufferLength);
                    using (Stream postStream = httpReq.GetRequestStream())
                    {
                        postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                        while (size > 0)
                        {
                            postStream.Write(buffer, 0, size);
                            size = r.Read(buffer, 0, bufferLength);
                        }
                        postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        postStream.Close();
                        using (Stream s = httpReq.GetResponse().GetResponseStream())
                        {
                            if (s == null) return "";
                            using (StreamReader sr = new StreamReader(s))
                            {
                                return sr.ReadLine();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 下载文件 public static string DownloadFile(string url, string pathname)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <returns>响应内容</returns>
        public static string DownloadFile(string url, string pathname)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null) throw new NullReferenceException();
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            if (webResponse == null) throw new NullReferenceException();
            using (Stream webStream = webResponse.GetResponseStream())
            {
                if (webStream == null) throw new NullReferenceException();
                if (webResponse.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw new WXException("404");
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

        #region 判断请求的用户代理 public static bool CheckUserAgent(HttpRequest request, string userAgent)
        /// <summary>
        /// 判断请求的用户代理
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="userAgent">用户代理</param>
        /// <returns>是否为该用户代理</returns>
        public static bool CheckUserAgent(HttpRequest request, string userAgent)
        {
            return request.UserAgent != null &&
                request.UserAgent.Contains(userAgent);
        }
        #endregion
    }
}
