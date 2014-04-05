using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Wing.WeiXin.MP.SDK.HaveFun
{
    /// <summary>
    /// 简易验证码
    /// </summary>
    public static class Captcha
    {
        /// <summary>
        /// 随机类
        /// </summary>
        private static readonly Random ran = new Random();

        #region 参数
        /// <summary>
        /// 图片宽度
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public static Brush BackgroundColor { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public static Font Font { get; set; }

        /// <summary>
        /// 开始绘制左上角坐标
        /// </summary>
        public static Point DrawStart { get; set; }

        /// <summary>
        /// 噪点指数（1-10）（越小越多）
        /// </summary>
        public static int PixNum { get; set; }

        /// <summary>
        /// 字符串弯曲最大角度（0-10）
        /// </summary>
        public static int RandAngle { get; set; }
        #endregion

        #region 初始化简易数据 static Captcha()
        /// <summary>
        /// 初始化简易数据
        /// </summary>
        static Captcha()
        {
            Width = 100;
            Height = 40;
            BackgroundColor = Brushes.White;
            Font = new Font("Arial", 16, FontStyle.Italic);
            DrawStart = new Point(0, 10);
            PixNum = 8;
            RandAngle = 10;
        } 
        #endregion

        #region 输出到流 public static void OutputStream(string str, Stream stream)
        /// <summary>
        /// 输出到流
        /// </summary>
        /// <param name="str">验证码字符串</param>
        /// <param name="stream">流</param>
        public static void OutputStream(string str, Stream stream)
        {
            str = str.ToUpper();
            using (Bitmap b = new Bitmap(Width, Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                    g.FillRectangle(BackgroundColor, rect);
                    g.DrawRectangle(new Pen(Color.Red, 0), rect);
                    DrawRandomPixel(b, g);
                    DrawString(str, g);
                    b.Save(stream, ImageFormat.Jpeg);
                }
            }
        }
        #endregion

        #region 输出到响应流 public static void OutputResponse(string str, HttpResponse response)
        /// <summary>
        /// 输出到响应流
        /// </summary>
        /// <param name="str">验证码字符串</param>
        /// <param name="response">响应</param>
        public static void OutputResponse(string str, HttpResponse response)
        {
            OutputStream(str, response.OutputStream);
            response.ContentType = "image/jpeg";
        }
        #endregion

        #region 绘制文字 private static void DrawString(string str, Graphics g, SolidBrush drawBrush)
        /// <summary>
        /// 绘制文字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="g">画笔</param>
        private static void DrawString(string str, Graphics g)
        {
            using (SolidBrush drawBrush = new SolidBrush(Color.Black))
            {
                char[] strList = str.ToCharArray();
                for (int i = 0; i < strList.Length; i++)
                {
                    float angle = ran.Next(-RandAngle, RandAngle);
                    g.RotateTransform(angle);
                    drawBrush.Color = GetRandomDeepColor();
                    g.DrawString(strList[i].ToString(CultureInfo.InvariantCulture), Font, drawBrush,
                        new PointF(DrawStart.X + i * Font.Size, DrawStart.Y));
                    g.RotateTransform(-angle);
                }
            }
        }
        #endregion

        #region 添加噪点 private static void DrawRandomPixel(Bitmap b, Graphics g)
        /// <summary>
        /// 添加噪点
        /// </summary>
        /// <param name="b">画板</param>
        /// <param name="g">画笔</param>
        private static void DrawRandomPixel(Bitmap b, Graphics g)
        {
            for (int i = 0; i < (Width * Height) / PixNum; i++)
            {
                int x = ran.Next(b.Width);
                int y = ran.Next(b.Height);
                b.SetPixel(x, y, GetRandomLightColor());
                if ((x + 1) < b.Width && (y + 1) < b.Height)
                {
                    g.DrawRectangle(new Pen(Color.Silver), ran.Next(b.Width), ran.Next(b.Height), 1, 1);
                }
            }
        }
        #endregion

        #region 随机生成颜色 private static Color GetRandomColor()
//        /// <summary>
//        /// 随机生成颜色
//        /// </summary>
//        /// <returns>颜色</returns>
//        private static Color GetRandomColor()
//        {
//            return Color.FromArgb(ran.Next(255) % 245 + 10,
//                ran.Next(255) % 245 + 10, ran.Next(255) % 245 + 10);
//        }
        #endregion

        #region 随机生成深色 private static Color GetRandomDeepColor()
        /// <summary>
        /// 随机生成深色
        /// </summary>
        /// <returns>深色</returns>
        private static Color GetRandomDeepColor()
        {
            return Color.FromArgb(ran.Next(160), ran.Next(160), ran.Next(160));
        } 
        #endregion

        #region 随机生成淡色 private static Color GetRandomLightColor()
        /// <summary>
        /// 随机生成淡色
        /// </summary>
        /// <returns>淡色</returns>
        private static Color GetRandomLightColor()
        {
            return Color.FromArgb(ran.Next(255) % 75 + 10, 
                ran.Next(255) % 75 + 10, ran.Next(255) % 75 + 10);
        }
        #endregion
    }
}
