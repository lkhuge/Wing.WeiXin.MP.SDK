using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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
            Dictionary<string, string> kvList = new Dictionary<string, string>();
            using (StreamReader srReadFile = new StreamReader(fileName))
            {
                while (!srReadFile.EndOfStream)
                {
                    string strReadLine = srReadFile.ReadLine();
                    if (String.IsNullOrEmpty(strReadLine)
                        || String.IsNullOrEmpty(strReadLine.Trim())) continue;
                    int index = strReadLine.IndexOf(':');
                    if (index == -1) continue;
                    kvList[strReadLine.Substring(0, index).Trim()] = 
                        strReadLine.Substring(index + 1).Trim()
                            .Replace("{LF}", "\n")
                            .Replace("{NowDate}", DateTime.Now.ToString("yyyy年MM月dd日"))
                            .Replace("{NowTime}", DateTime.Now.ToString("hh:mm:ss"));
                }
            }
            return kvList;
        }
        #endregion
    }
}
