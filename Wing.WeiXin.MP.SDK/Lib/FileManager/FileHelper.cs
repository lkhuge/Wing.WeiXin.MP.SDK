using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Lib.FileManager
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public static class FileHelper
    {
        #region 从文件中读取KeyValue数据 public static Dictionary<string, string> ReadOfKeyValueData(string fileName)
        /// <summary>
        /// 从文件中读取KeyValue数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>KeyValue数据</returns>
        public static Dictionary<string, string> ReadOfKeyValueData(string fileName)
        {
            try
            {
                Dictionary<string, string> kvList = new Dictionary<string, string>();
                using (StreamReader srReadFile = new StreamReader(fileName))
                {
                    while (!srReadFile.EndOfStream)
                    {
                        string strReadLine = srReadFile.ReadLine();
                        if (String.IsNullOrEmpty(strReadLine)
                            || String.IsNullOrEmpty(strReadLine.Trim())) continue;
                        string[] strList = strReadLine.Split(':');
                        if (strList.Length != 2) continue;
                        kvList[strList[0].Trim()] = strList[1].Trim();
                    }
                }
                return kvList;
            }
            catch (FileNotFoundException fileNotFound)
            {
                throw new WXException("KeyValue数据文件未找到", fileNotFound);
            }
            catch (DirectoryNotFoundException directoryNotFound)
            {
                throw new WXException("KeyValue数据文件未找到", directoryNotFound);
            }
            catch (IOException io)
            {
                throw new WXException("读取KeyValue数据错误", io);
            }
        }
        #endregion
    }
}
