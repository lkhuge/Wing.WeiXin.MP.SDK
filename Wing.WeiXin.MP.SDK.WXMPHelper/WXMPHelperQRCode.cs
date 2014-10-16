using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wing.WeiXin.MP.SDK;
using Wing.WeiXin.MP.SDK.Common.AccessTokenManager;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities.QRCode;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    /// <summary>
    /// 二维码工具
    /// </summary>
    public partial class WXMPHelperQRCode : Form
    {
        #region 初始化 public WXMPHelperQRCode()
        /// <summary>
        /// 初始化
        /// </summary>
        public WXMPHelperQRCode()
        {
            InitializeComponent();
        } 
        #endregion

        #region 获取临时二维码 private void btGetQRC_Click(object sender, EventArgs e)
        /// <summary>
        /// 获取临时二维码
        /// </summary>
        private void btGetQRC_Click(object sender, EventArgs e)
        {
            if (!CheckParam() || !CheckExpire()) return;
            btGetQRC.Text = "获取中。。。";
            btGetQRC.Enabled = false;
            bgwCreate.RunWorkerAsync(new QRCodeTicketRequest
            {
                expire_seconds = Int32.Parse(tbExpire.Text),
                action_name = "QR_SCENE",
                action_info = new QRCodeTicketRequest.ActionInfo
                {
                    scene = new QRCodeTicketRequest.ActionInfo.Scene
                    {
                        scene_id = Int32.Parse(tbParam.Text)
                    }
                }
            });
        } 
        #endregion

        #region 获取永久二维码 private void btGetQRCLimit_Click(object sender, EventArgs e)
        /// <summary>
        /// 获取永久二维码
        /// </summary>
        private void btGetQRCLimit_Click(object sender, EventArgs e)
        {
            if (!CheckParam()) return;
            btGetQRCLimit.Text = "获取中。。。";
            btGetQRCLimit.Enabled = false;
            bgwCreate.RunWorkerAsync(new QRCodeTicketRequest
            {
                action_name = "QR_LIMIT_SCENE",
                action_info = new QRCodeTicketRequest.ActionInfo
                {
                    scene = new QRCodeTicketRequest.ActionInfo.Scene
                    {
                        scene_id = Int32.Parse(tbParam.Text)
                    }
                }
            });
        } 
        #endregion

        #region 检测二维码参数 private bool CheckParam()
        /// <summary>
        /// 检测二维码参数
        /// </summary>
        /// <returns>结果</returns>
        private bool CheckParam()
        {
            int temp;
            if (!Int32.TryParse(tbParam.Text, out temp))
            {
                MessageBox.Show("必须输入32位非0整型");
                return false;
            }
            if (temp > 0 && temp <= 100000) return true;
            MessageBox.Show("目前参数只支持1--100000");
            return false;
        } 
        #endregion

        #region 检测二维码有效时间 private bool CheckExpire()
        /// <summary>
        /// 检测二维码有效时间
        /// </summary>
        /// <returns>结果</returns>
        private bool CheckExpire()
        {
            int temp;
            if (!Int32.TryParse(tbExpire.Text, out temp))
            {
                MessageBox.Show("必须输入整型");
                return false;
            }
            if (temp > 0 && temp <= 1800) return true;
            MessageBox.Show("最大不超过1800");
            return false;
        }
        #endregion

        #region 创建二维码 private void bgwCreate_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 创建二维码
        /// </summary>
        private void bgwCreate_DoWork(object sender, DoWorkEventArgs e)
        {
            QRCodeTicketRequest param = (QRCodeTicketRequest) e.Argument;
            try
            {
                e.Result = new QRCodeController().GetQRCodeTicket(
                    GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountFirst(WeixinMPType.Service),
                    param);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 创建二维码完成 private void bgwCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 创建二维码完成
        /// </summary>
        private void bgwCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                QRCodeTicket ticket = (QRCodeTicket)e.Result;
                lbExpire.Text = ticket.expire_seconds == 0 
                    ? "永久" 
                    : ticket.expire_seconds + "秒";
                tbTicket.Text = ticket.ticket;
            }
            btGetQRC.Text = "创建临时二维码";
            btGetQRC.Enabled = true;
            btGetQRCLimit.Text = "创建永久二维码";
            btGetQRCLimit.Enabled = true;
        } 
        #endregion

        #region 下载二维码 private void btDownload_Click(object sender, EventArgs e)
        /// <summary>
        /// 下载二维码
        /// </summary>
        private void btDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "图片文件 (*.JPG)|*.JPG",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "下载二维码"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            tbPath.Text = dialog.FileName;
            btDownload.Text = "下载中。。。";
            btDownload.Enabled = false;
            bgwDownload.RunWorkerAsync(dialog.FileName);
        } 
        #endregion

        #region 下载二维码 private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 下载二维码
        /// </summary>
        private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = (string)e.Argument;
            new QRCodeController().GetQRCode(new QRCodeTicket
            {
                ticket = tbTicket.Text
            }, fileName);
        } 
        #endregion

        #region 下载二维码完成 private void bgwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 下载二维码完成
        /// </summary>
        private void bgwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                pbQRCode.LoadAsync(tbPath.Text);
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
            btDownload.Text = "下载二维码";
            btDownload.Enabled = true;
        } 
        #endregion
    }
}
