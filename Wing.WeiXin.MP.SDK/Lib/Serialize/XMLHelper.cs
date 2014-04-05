using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Lib.Serialize
{
    /// <summary>
    /// XML工具类
    /// </summary>
    public static class XMLHelper
    {
        #region 获取XML字段的值 public static string GetValueFromXML(string xmlData, string key)
        /// <summary>
        /// 获取XML字段的值
        /// </summary>
        /// <param name="xmlData">XML字符串</param>
        /// <param name="key">Key值</param>
        /// <returns>XML字段的值</returns>
        public static string GetValueFromXML(string xmlData, string key)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlData);
                XmlElement rootElement = doc.DocumentElement;
                if (rootElement == null) return "";
                XmlNode node = rootElement.SelectSingleNode(key);
                if (node != null) return node.InnerText;
            }
            catch
            {
                return "";
            }
            return "";
        }
        #endregion

        #region XML字符串是否存在指定节点 public static bool IsHaveNodeFromXMLString(string xmlString, string nodeKey)
        /// <summary>
        /// XML字符串是否存在指定节点
        /// </summary>
        /// <param name="xmlString">XML字符串</param>
        /// <param name="nodeKey">指定节点</param>
        /// <returns>是否存在</returns>
        public static bool IsHaveNodeFromXMLString(string xmlString, string nodeKey)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlElement rootElement = doc.DocumentElement;
                if (rootElement == null) return false;
                XmlNode node = rootElement.SelectSingleNode(nodeKey);
                if (node != null) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 将一个对象序列化为XML字符串 public static string XMLSerialize(object obj, Encoding encoding = null)
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XMLSerialize(object obj, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            using (MemoryStream stream = new MemoryStream())
            {
                XMLSerializeInternal(stream, obj, encoding);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        } 
        #endregion

        #region 从XML字符串中反序列化对象 public static T XMLDeserialize<T>(string str, Encoding encoding = null)
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="str">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XMLDeserialize<T>(string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(str)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        } 
        #endregion

        #region XML序列化预处理 private static void XMLSerializeInternal(Stream stream, object obj, Encoding encoding)
        /// <summary>
        /// XML序列化预处理
        /// </summary>
        /// <param name="stream">字节流</param>
        /// <param name="obj">序列化对象</param>
        /// <param name="encoding">编码方式</param>
        private static void XMLSerializeInternal(Stream stream, object obj, Encoding encoding)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    ",
                OmitXmlDeclaration = true
            };
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, namespaces);
                writer.Close();
            }
        } 
        #endregion
    }
}
