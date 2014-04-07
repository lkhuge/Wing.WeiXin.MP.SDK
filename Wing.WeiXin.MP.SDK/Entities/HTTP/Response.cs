using System;
using System.Web;
using Wing.WeiXin.MP.SDK.Exception;
using Wing.WeiXin.MP.SDK.Lib.Serialize;

namespace Wing.WeiXin.MP.SDK.Entities.HTTP
{
    /// <summary>
    /// 响应对象
    /// </summary>
    public class Response
    {
        #region 类型
        /// <summary>
        /// JSON类型
        /// </summary>
        public static string JSON = "application/json";

        /// <summary>
        /// XML类型
        /// </summary>
        public static string XML = "application/xml";

        /// <summary>
        /// 文本类型
        /// </summary>
        public static string TEXT = "text/plain"; 
        #endregion

        #region 成员变量
        /// <summary>
        /// 响应类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string Text { get; private set; } 
        #endregion

        #region 根据响应内容实例化文本类型对象 public Response(string text)
        /// <summary>
        /// 根据响应内容实例化文本类型对象
        /// </summary>
        /// <param name="text">响应内容</param>
        public Response(string text)
        {
            Text = text;
            ContentType = TEXT;
        } 
        #endregion

        #region 根据消息对象实例化文本类型对象 public Response(Note note)
        /// <summary>
        /// 根据消息对象实例化文本类型对象
        /// </summary>
        /// <param name="note">消息对象</param>
        public Response(Note note)
        {
            Text = note.Message;
            ContentType = TEXT;
        }
        #endregion

        #region 根据微信异常实例化文本类型对象 public Response(WXException e)
        /// <summary>
        /// 根据微信异常实例化文本类型对象
        /// </summary>
        /// <param name="e">消息对象</param>
        public Response(WXException e)
        {
            Text = e.GetNote().Message;
            ContentType = TEXT;
        }
        #endregion

        #region 根据响应内容和响应类型实例化 public Response(string text, string type)
        /// <summary>
        /// 根据响应内容和响应类型实例化
        /// </summary>
        /// <param name="text">响应内容</param>
        /// <param name="type">响应类型</param>
        public Response(string text, string type)
        {
            Text = text;
            ContentType = type;
        } 
        #endregion

        #region 根据XML对象实例化 public Response(IXML data)
        /// <summary>
        /// 根据XML对象实例化
        /// </summary>
        /// <param name="data">XML对象</param>
        public Response(IXML data)
        {
            Text = XMLHelper.XMLSerialize(data);
            ContentType = XML;
        } 
        #endregion

        #region 根据JSON对象实例化 public Response(IJSON data)
        /// <summary>
        /// 根据JSON对象实例化
        /// </summary>
        /// <param name="data">JSON对象</param>
        public Response(IJSON data)
        {
            Text = JSONHelper.JSONSerialize(data);
            ContentType = JSON;
        } 
        #endregion

        #region 输出响应 public void ResponseOutput(HttpResponse response)
        /// <summary>
        /// 输出响应
        /// </summary>
        /// <param name="response">HTTP响应</param>
        public void ResponseOutput(HttpResponse response)
        {
            response.ContentType = ContentType;
            response.Write(Text);
        } 
        #endregion

        #region 获取完整响应信息 public override string ToString()
        /// <summary>
        /// 获取完整响应信息
        /// </summary>
        /// <returns>完整响应信息</returns>
        public override string ToString()
        {
            return String.Format("[ContentType]:{0}[Text]:{1}",
                ContentType, Text);
        } 
        #endregion
    }
}
