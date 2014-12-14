using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Lib.Net
{
    /// <summary>
    /// HTTP工具类接口
    /// </summary>
    public interface IHTTPHelper
    {
        /// <summary>
        /// 使用Get方法获取字符串结果
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        string Get(string url, Encoding encoding = null);

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">请求</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        string Post(string url, string data, Encoding encoding = null);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="pathname">下载后的存放地址以及文件名</param>
        /// <returns>响应内容</returns>
        string DownloadFile(string url, string pathname);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="address">文件上传到的服务器</param>
        /// <param name="path">要上传的本地文件路径</param>
        /// <param name="name">文件上传后的名称</param>
        /// <param name="method">上传方式</param>
        /// <returns>成功返回1，失败返回0</returns>
        string Upload(string address, string path, string name, string method = "POST");

        /// <summary>
        /// 获取Post请求流
        /// </summary>
        /// <param name="httpContext">上下文</param>
        /// <param name="encoding">编码</param>
        /// <returns>Post请求流字符串</returns>
        string GetPostStream(HttpContext httpContext, Encoding encoding = null);
    }
}
