using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    public partial class FrmAddQRM : Form
    {
        /// <summary>
        /// 功能列表
        /// </summary>
        private Func<string[]>[] FuncList;

        public FrmAddQRM()
        {
            InitializeComponent();
        }

        #region 根据事件Key实例化 public FrmAddQRM(string key)
        /// <summary>
        /// 根据事件Key实例化
        /// </summary>
        /// <param name="key">事件Key</param>
        public FrmAddQRM(string key)
        {
            InitializeComponent();
            tbKey.Text = key;
            tbKey.Enabled = false;
        } 
        #endregion

        #region 初始化 private void FrmAddQRM_Load(object sender, EventArgs e)
        /// <summary>
        /// 初始化
        /// </summary>
        private void FrmAddQRM_Load(object sender, EventArgs e)
        {
            FuncList = new Func<string[]>[]
            {
                ReturnMessageImage,
                ReturnMessageMusic,
                ReturnMessageNews,
                ReturnMessageText,
                ReturnMessageVideo,
                ReturnMessageVoice
            };
        } 
        #endregion

        #region 保存按钮 private void btSave_Click(object sender, EventArgs e)
        /// <summary>
        /// 保存按钮
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            string[] data = FuncList[tabControl1.SelectedIndex]();
            if (data == null) return;
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "请选择文件夹",
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = true,
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            File.WriteAllLines(
                String.Format("{0}\\{1}.wx.txt", 
                    dialog.SelectedPath, 
                    tbKey.Text),
                data);
            MessageBox.Show("保存完成");
            Close();
        } 
        #endregion

        #region 回复图片信息事件 private string[] ReturnMessageImage()
        /// <summary>
        /// 回复图片信息事件
        /// </summary>
        /// <returns>图片信息</returns>
        private string[] ReturnMessageImage()
        {
            if (String.IsNullOrEmpty(tbImageMediaID.Text)) return null;
            return new []
            {
                "Type:ReturnMessageImage",
                "MediaId:" + tbImageMediaID.Text
            };
        } 
        #endregion

        #region 打开图片 private void btImageOpen_Click(object sender, EventArgs e)
        /// <summary>
        /// 打开图片
        /// </summary>
        private void btImageOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "图片文件 (*.JPG)|*.JPG",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "打开图片"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            pbShow.LoadAsync(dialog.FileName);
            tbImagePath.Text = dialog.FileName;
        } 
        #endregion

        #region 上传图片 private void btUpload_Click(object sender, EventArgs e)
        /// <summary>
        /// 上传图片
        /// </summary>
        private void btUpload_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbImagePath.Text)) return;
            new FrmSelectAccount(a =>
            {
                btUpload.Enabled = false;
                btUpload.Text = "正在上传中";
                bgwUploadImage.RunWorkerAsync(new
                {
                    Account = a,
                    Path = tbImagePath.Text
                });
            }).ShowDialog();
        } 
        #endregion

        #region 上传图片 private void bgwUploadImage_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 上传图片
        /// </summary>
        private void bgwUploadImage_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic data = e.Argument;
            WXAccount a = data.Account;
            string path = data.Path;
            List<string> temp = path.Split('\\').ToList();
            string name = temp[temp.Count - 1];
            temp.RemoveAt(temp.Count - 1);
            try
            {
                e.Result = new MediaController().Upload(a, UploadMediaType.image, String.Join("\\", temp), name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 上传图片完成 private void bgwUploadImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 上传图片完成
        /// </summary>
        private void bgwUploadImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Media media = (Media)e.Result;
                tbImageMediaID.Text = media.media_id;
                MessageBox.Show("上传成功");
            }
            btUpload.Enabled = true;
            btUpload.Text = "图片上传";
        } 
        #endregion

        #region 回复音乐信息事件 private string[] ReturnMessageMusic()
        /// <summary>
        /// 回复音乐信息事件
        /// </summary>
        /// <returns>音乐信息</returns>
        private string[] ReturnMessageMusic()
        {
            if (String.IsNullOrEmpty(tbMusicThumbMediaID.Text))
            {
                MessageBox.Show("缩略图不能为空");
                return null;
            }
            return new[]
            {
                "Type:ReturnMessageMusic",
                "Title:" + tbMusicTitle.Text,
                "Description:" + tbMusicDescription.Text,
                "MusicURL:" + tbMusicUrl.Text,
                "HQMusicUrl:" + tbMusicHDUrl.Text,
                "ThumbMediaId:" + tbMusicThumbMediaID.Text
            };
        } 
        #endregion

        #region 试听音乐 private void btMusicListen_Click(object sender, EventArgs e)
        /// <summary>
        /// 试听音乐
        /// </summary>
        private void btMusicListen_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMusicUrl.Text)) return;
            Process.Start(tbMusicUrl.Text);
        } 
        #endregion

        #region 试听高质量音乐 private void btMusicHDListen_Click(object sender, EventArgs e)
        /// <summary>
        /// 试听高质量音乐
        /// </summary>
        private void btMusicHDListen_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMusicHDUrl.Text)) return;
            Process.Start(tbMusicHDUrl.Text);
        } 
        #endregion

        #region 打开缩略图 private void btMusicOpenPic_Click(object sender, EventArgs e)
        /// <summary>
        /// 打开缩略图
        /// </summary>
        private void btMusicOpenPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "图片文件 (*.JPG)|*.JPG",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "打开图片"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            pbThumb.LoadAsync(dialog.FileName);
            tbMusicThumbPath.Text = dialog.FileName;
        } 
        #endregion

        #region 上传缩略图 private void btUploadPic_Click(object sender, EventArgs e)
        /// <summary>
        /// 上传缩略图
        /// </summary>
        private void btUploadPic_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMusicThumbPath.Text)) return;
            new FrmSelectAccount(a =>
            {
                btUploadPic.Enabled = false;
                btUploadPic.Text = "正在上传中";
                bgwUploadThumb.RunWorkerAsync(new
                {
                    Account = a,
                    Path = tbMusicThumbPath.Text
                });
            }).ShowDialog();
        } 
        #endregion

        #region 上传缩略图 private void bgwUploadThumb_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 上传缩略图
        /// </summary>
        private void bgwUploadThumb_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic data = e.Argument;
            WXAccount a = data.Account;
            string path = data.Path;
            List<string> temp = path.Split('\\').ToList();
            string name = temp[temp.Count - 1];
            temp.RemoveAt(temp.Count - 1);
            try
            {
                e.Result = new MediaController().Upload(a, UploadMediaType.thumb, String.Join("\\", temp), name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 上传缩略图完成 private void bgwUploadThumb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 上传缩略图完成
        /// </summary>
        private void bgwUploadThumb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Media media = (Media)e.Result;
                tbMusicThumbMediaID.Text = media.media_id;
                MessageBox.Show("上传成功");
            }
            btUploadPic.Enabled = true;
            btUploadPic.Text = "缩略图上传";
        } 
        #endregion

        #region 回复图文信息事件 private string[] ReturnMessageNews()
        /// <summary>
        /// 回复图文信息事件
        /// </summary>
        /// <returns>图文信息</returns>
        private string[] ReturnMessageNews()
        {
            if (dgvNews.Rows.Count == 0) return null;
            List<string> msg = new List<string>
            {
                "Type:ReturnMessageNews"
            };
            int index = 1;
            foreach (DataGridViewRow row in dgvNews.Rows)
            {
                string title = row.Cells[0].EditedFormattedValue.ToString();
                string description = row.Cells[1].EditedFormattedValue.ToString();
                string picUrl = row.Cells[2].EditedFormattedValue.ToString();
                string url = row.Cells[3].EditedFormattedValue.ToString();
                if (!String.IsNullOrEmpty(title)) 
                    msg.Add(String.Format("Articles{0}Title:{1}", index, title));
                if (!String.IsNullOrEmpty(description))
                    msg.Add(String.Format("Articles{0}Description:{1}", index, description));
                if (!String.IsNullOrEmpty(picUrl))
                    msg.Add(String.Format("Articles{0}PicUrl:{1}", index, picUrl));
                if (!String.IsNullOrEmpty(url))
                    msg.Add(String.Format("Articles{0}Url:{1}", index, url));
            }
            return msg.ToArray();
        } 
        #endregion

        #region 回复文本信息事件 private string[] ReturnMessageText()
        /// <summary>
        /// 回复文本信息事件
        /// </summary>
        /// <returns>文本信息</returns>
        private string[] ReturnMessageText()
        {
            if (String.IsNullOrEmpty(tbText.Text)) return null;
            return new[]
            {
                "Type:ReturnMessageText",
                "Content:" + tbText.Text
            };
        } 
        #endregion

        #region 回复视频信息事件 private string[] ReturnMessageVideo()
        /// <summary>
        /// 回复视频信息事件
        /// </summary>
        /// <returns>视频信息</returns>
        private string[] ReturnMessageVideo()
        {
            if (String.IsNullOrEmpty(tbVideoMediaID.Text))
            {
                MessageBox.Show("视频不能为空");
                return null;
            }
            return new[]
            {
                "Type:ReturnMessageVideo",
                "Title:" + tbVideoTitle.Text,
                "Description:" + tbVideoDescription.Text,
                "MediaId:" + tbVideoMediaID.Text
            };
        } 
        #endregion

        #region 播放视频 private void btPlayVideo_Click(object sender, EventArgs e)
        /// <summary>
        /// 播放视频
        /// </summary>
        private void btPlayVideo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbVideoPath.Text)) return;
            Process.Start(tbVideoPath.Text);
        } 
        #endregion

        #region 打开视频 private void btOpenVideo_Click(object sender, EventArgs e)
        /// <summary>
        /// 打开视频
        /// </summary>
        private void btOpenVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "视频文件 (*.MP4)|*.MP4",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "打开视频"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            if (new FileInfo(dialog.FileName).Length > 10000000)
            {
                MessageBox.Show("视频不能超过10M");
                return;
            }
            tbVideoPath.Text = dialog.FileName;
        } 
        #endregion

        #region 上传视频 private void btUploadVideo_Click(object sender, EventArgs e)
        /// <summary>
        /// 上传视频
        /// </summary>
        private void btUploadVideo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbVideoPath.Text)) return;
            new FrmSelectAccount(a =>
            {
                btUploadVideo.Enabled = false;
                btUploadVideo.Text = "正在上传中";
                bgwUploadVideo.RunWorkerAsync(new
                {
                    Account = a,
                    Path = tbVideoPath.Text
                });
            }).ShowDialog();
        } 
        #endregion

        #region 上传视频 private void bgwUploadVideo_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 上传视频
        /// </summary>
        private void bgwUploadVideo_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic data = e.Argument;
            WXAccount a = data.Account;
            string path = data.Path;
            List<string> temp = path.Split('\\').ToList();
            string name = temp[temp.Count - 1];
            temp.RemoveAt(temp.Count - 1);
            try
            {
                e.Result = new MediaController().Upload(a, UploadMediaType.video, String.Join("\\", temp), name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 上传视频完成 private void bgwUploadVideo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 上传视频完成
        /// </summary>
        private void bgwUploadVideo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Media media = (Media)e.Result;
                tbVideoMediaID.Text = media.media_id;
                MessageBox.Show("上传成功");
            }
            btUploadVideo.Enabled = true;
            btUploadVideo.Text = "语音上传";
        } 
        #endregion

        #region 回复语音信息事件 private string[] ReturnMessageVoice()
        /// <summary>
        /// 回复语音信息事件
        /// </summary>
        /// <returns>语音信息</returns>
        private string[] ReturnMessageVoice()
        {
            if (String.IsNullOrEmpty(tbVoiceMediaID.Text)) return null;
            return new[]
            {
                "Type:ReturnMessageVoice",
                "MediaId:" + tbVoiceMediaID.Text
            };
        } 
        #endregion

        #region 打开语音 private void btOpenVoice_Click(object sender, EventArgs e)
        /// <summary>
        /// 打开语音
        /// </summary>
        private void btOpenVoice_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "语音文件 (*.MP3)|*.MP3|语音文件 (*.AMR)|*.AMR",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "打开语音"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            if (new FileInfo(dialog.FileName).Length > 2000000)
            {
                MessageBox.Show("语音不能超过2M");
                return;
            }
            tbVoicePath.Text = dialog.FileName;
        } 
        #endregion

        #region 上传语音 private void btUploadVoice_Click(object sender, EventArgs e)
        /// <summary>
        /// 上传语音
        /// </summary>
        private void btUploadVoice_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbVoicePath.Text)) return;
            new FrmSelectAccount(a =>
            {
                btUploadVoice.Enabled = false;
                btUploadVoice.Text = "正在上传中";
                bgwUploadVoice.RunWorkerAsync(new
                {
                    Account = a,
                    Path = tbVoicePath.Text
                });
            }).ShowDialog();
        } 
        #endregion

        #region 上传语音 private void bgwUploadVoice_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 上传语音
        /// </summary>
        private void bgwUploadVoice_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic data = e.Argument;
            WXAccount a = data.Account;
            string path = data.Path;
            List<string> temp = path.Split('\\').ToList();
            string name = temp[temp.Count - 1];
            temp.RemoveAt(temp.Count - 1);
            try
            {
                e.Result = new MediaController().Upload(a, UploadMediaType.voice, String.Join("\\", temp), name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region 上传语音完成 private void bgwUploadVoice_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 上传语音完成
        /// </summary>
        private void bgwUploadVoice_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Media media = (Media)e.Result;
                tbVoiceMediaID.Text = media.media_id;
                MessageBox.Show("上传成功");
            }
            btUploadVoice.Enabled = true;
            btUploadVoice.Text = "语音上传";
        } 
        #endregion
    }
}
