using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Wing.WeiXin.MP.SDK.Exception;

namespace Wing.WeiXin.MP.SDK.Lib.StringManager
{
    /// <summary>
    /// 二维码工具类
    /// </summary>
    public static class QRCodeHelper
    {
        /// <summary>
        /// 二维码编码器
        /// </summary>
        private static QrEncoder qrEncoder;

        /// <summary>
        /// 图形渲染
        /// </summary>
        private static GraphicsRenderer graphicsRenderer;

        #region 初始化 static QRCodeHelper()
        /// <summary>
        /// 初始化
        /// </summary>
        static QRCodeHelper()
        {
            qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            graphicsRenderer = new GraphicsRenderer(
                new FixedModuleSize(12, QuietZoneModules.Two), 
                Brushes.Black, Brushes.White);
        } 
        #endregion

        #region 生成二维码到流 public static void Make(string message, Stream stream)
        /// <summary>
        /// 生成二维码到流
        /// </summary>
        /// <param name="message">二维码内容</param>
        /// <param name="stream">流</param>
        public static void Make(string message, Stream stream)
        {
            QrCode qr;
            if (!qrEncoder.TryEncode(message, out qr))
            {
                throw new WXException("字符串无法生成二维码");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                graphicsRenderer.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
                ms.WriteTo(stream);
            }
        } 
        #endregion

        #region 设置容错级别 public static void SetErrorCorrectionLevel(ErrorCorrectionLevel level)
        /// <summary>
        /// 设置容错级别
        /// </summary>
        /// <param name="level">容错级别</param>
        public static void SetErrorCorrectionLevel(ErrorCorrectionLevel level)
        {
            qrEncoder = new QrEncoder(level);
        } 
        #endregion

        #region 设置渲染 public static void SetRenderer(int size, QuietZoneModules modules, Brush pointColor, Brush backgroundColor)
        /// <summary>
        /// 设置渲染
        /// </summary>
        /// <param name="size">尺寸</param>
        /// <param name="modules">内边距</param>
        /// <param name="pointColor">点颜色</param>
        /// <param name="backgroundColor">背景颜色</param>
        public static void SetRenderer(int size, QuietZoneModules modules,
            Brush pointColor, Brush backgroundColor)
        {
            graphicsRenderer = new GraphicsRenderer(
                new FixedModuleSize(size, modules), pointColor, backgroundColor);
        } 
        #endregion
    }
}
