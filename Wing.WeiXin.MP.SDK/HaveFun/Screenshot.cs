using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.ReturnMessages;
using Wing.WeiXin.MP.SDK.Enumeration;
using MessageHelper = Wing.WeiXin.MP.SDK.Lib.StringManager.Message;
using ImageObject = Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject.Image;

namespace Wing.WeiXin.MP.SDK.HaveFun
{
    /// <summary>
    /// 截图
    /// </summary>
    [Obsolete("无法在IIS环境下使用")]
    public class Screenshot
    {
        #region 截图 public void Shot(string pathName, ImageFormat format)
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="pathName">图片路径</param>
        /// <param name="format">图片格式</param>
        public void Shot(string pathName, ImageFormat format)
        {
            Bitmap baseImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(baseImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            g.Dispose();
            baseImage.Save(pathName, format);
        } 
        #endregion
    }
}
