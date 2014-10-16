using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    /// <summary>
    /// 微信公共平台简易助手设置菜单界面
    /// </summary>
    public partial class WXMPHelperSelect : Form
    {
        /// <summary>
        /// 标签
        /// </summary>
        private readonly Label lb;

        /// <summary>
        /// 标签列表
        /// </summary>
        private readonly Dictionary<string, Label> lbList;

        #region 根据标签实例化 public FrmSelect(Label lb, Dictionary<string, Label> lbList)
        /// <summary>
        /// 根据标签实例化
        /// </summary>
        /// <param name="lb">标签</param>
        /// <param name="lbList">标签列表</param>
        public WXMPHelperSelect(Label lb, Dictionary<string, Label> lbList)
        {
            this.lb = lb;
            this.lbList = lbList;
            InitializeComponent();
        } 
        #endregion

        #region 初始化 private void FrmSelect_Load(object sender, EventArgs e)
        /// <summary>
        /// 初始化
        /// </summary>
        private void FrmSelect_Load(object sender, EventArgs e)
        {
            if (lb.Name.Length == 3)
            {
                btYesMain.Enabled = true;
            }
            if (lb.Tag == null) return;
            btClear.Enabled = true;
            dynamic tag = lb.Tag;
            int index = tag.Index;
            tbName.Text = tag.Name;
            if (index == 0)
            {
                tabControl1.SelectedIndex = 0;
                tbClick.Text = tag.Key;
            }
            if (index == 1)
            {
                tabControl1.SelectedIndex = 1;
                tbView.Text = tag.Key;
            }
            if (index == 2)
            {
                tabControl1.SelectedIndex = 2;
                tbScanCodePush.Text = tag.Key;
            }
            if (index == 3)
            {
                tabControl1.SelectedIndex = 3;
                tbScanCodeWaitMsg.Text = tag.Key;
            }
            if (index == 4)
            {
                tabControl1.SelectedIndex = 4;
                tbPicSysPhoto.Text = tag.Key;
            }
            if (index == 5)
            {
                tabControl1.SelectedIndex = 5;
                tbPicPhotoOrAlbum.Text = tag.Key;
            }
            if (index == 6)
            {
                tabControl1.SelectedIndex = 6;
                tbPicWeixin.Text = tag.Key;
            }
            if (index == 7)
            {
                tabControl1.SelectedIndex = 7;
                tbLocationSelect.Text = tag.Key;
            }

        }  
        #endregion

        #region 确定按钮 private void btYes_Click(object sender, EventArgs e)
        /// <summary>
        /// 确定按钮
        /// </summary>
        private void btYes_Click(object sender, EventArgs e)
        {
            if (!CheckName(20)) return;
            if (tabControl1.SelectedIndex == 0) SetMenuClick();
            if (tabControl1.SelectedIndex == 1) SetMenuView();
            if (tabControl1.SelectedIndex == 2) SetMenuScanCodePush();
            if (tabControl1.SelectedIndex == 3) SetMenuScanCodeWaitMsg();
            if (tabControl1.SelectedIndex == 4) SetMenuPicSysPhoto();
            if (tabControl1.SelectedIndex == 5) SetMenuPicPhotoOrAlbum();
            if (tabControl1.SelectedIndex == 6) SetMenuPicWeixin();
            if (tabControl1.SelectedIndex == 7) SetMenuLocationSelect();
        } 
        #endregion

        #region 确认（主菜单）按钮 private void btYesMain_Click(object sender, EventArgs e)
        /// <summary>
        /// 确认（主菜单）按钮
        /// </summary>
        private void btYesMain_Click(object sender, EventArgs e)
        {
            if (!CheckName(8)) return;
            SetMenuMain();
        } 
        #endregion

        #region 清空按钮 private void btClear_Click(object sender, EventArgs e)
        /// <summary>
        /// 清空按钮
        /// </summary>
        private void btClear_Click(object sender, EventArgs e)
        {
            int index = Int32.Parse(lb.Name.Substring(2, 1));
            if (lb.Name.Length == 3)
            {
                if (index == WXMPHelperMenu.MaxMainMenu || lbList[(index + 1).ToString(CultureInfo.InvariantCulture)].Tag == null)
                {
                    ClearMenu(index);
                }
                else
                {
                    PushMenuMain(index);
                }
            }
            if (lb.Name.Length == 4)
            {
                int index2 = Int32.Parse(lb.Name.Substring(3, 1));
                if (index2 == WXMPHelperMenu.MaxSubMenu || lbList[index + "" + (index2 + 1)].Tag == null)
                {
                    ClearMenu(index, index2);
                }
                else
                {
                    PushMenuSub(index, index2);
                }
            }
            Close();
        } 
        #endregion

        #region 清空菜单 private void ClearMenu(int index, int index2 = 0)
        /// <summary>
        /// 清空菜单
        /// </summary>
        /// <param name="index">主菜单索引</param>
        /// <param name="index2">子菜单索引</param>
        private void ClearMenu(int index, int index2 = 0)
        {
            if (index2 == 0)
            {
                for (int i = 1; i <= WXMPHelperMenu.MaxSubMenu; i++)
                {
                    lbList[index + "" + i].Text = WXMPHelperMenu.LabelNullText;
                    lbList[index + "" + i].Tag = null;
                }
                lbList[index.ToString(CultureInfo.InvariantCulture)].Text = WXMPHelperMenu.LabelNullText;
                lbList[index.ToString(CultureInfo.InvariantCulture)].Tag = null;
            }
            else
            {
                lbList[index + "" + index2].Text = WXMPHelperMenu.LabelNullText;
                lbList[index + "" + index2].Tag = null;
            }
        } 
        #endregion

        #region 主菜单向前推 private void PushMenuMain(int index)
        /// <summary>
        /// 主菜单向前推
        /// </summary>
        /// <param name="index">主菜单索引</param>
        private void PushMenuMain(int index)
        {
            for (int i = index; i < WXMPHelperMenu.MaxMainMenu; i++)
            {
                lbList[i.ToString(CultureInfo.InvariantCulture)].Text = lbList[(i + 1).ToString(CultureInfo.InvariantCulture)].Text;
                lbList[i.ToString(CultureInfo.InvariantCulture)].Tag = lbList[(i + 1).ToString(CultureInfo.InvariantCulture)].Tag;
                for (int j = 1; j <= WXMPHelperMenu.MaxSubMenu; j++)
                {
                    lbList[i + "" + j].Text = lbList[(i + 1) + "" + j].Text;
                    lbList[i + "" + j].Tag = lbList[(i + 1) + "" + j].Tag;
                }
                if (lbList[(i + 1).ToString(CultureInfo.InvariantCulture)].Tag == null) break;
                if (i != WXMPHelperMenu.MaxMainMenu - 1) continue;
                ClearMenu(WXMPHelperMenu.MaxMainMenu);
                for (int j = 1; j <= WXMPHelperMenu.MaxSubMenu; j++)
                {
                    ClearMenu(WXMPHelperMenu.MaxMainMenu, j);
                }
            }
        } 
        #endregion

        #region 子菜单向前推 private void PushMenuSub(int index, int index2)
        /// <summary>
        /// 子菜单向前推
        /// </summary>
        /// <param name="index">主菜单索引</param>
        /// <param name="index2">子菜单索引</param>
        private void PushMenuSub(int index, int index2)
        {
            for (int i = index2; i < WXMPHelperMenu.MaxSubMenu; i++)
            {
                lbList[index + "" + i].Text = lbList[index + "" + (i + 1)].Text;
                lbList[index + "" + i].Tag = lbList[index + "" + (i + 1)].Tag;
                if (lbList[index + "" + (i + 1)].Tag == null) break;
                if (i != WXMPHelperMenu.MaxSubMenu - 1) continue;
                ClearMenu(index, WXMPHelperMenu.MaxSubMenu);
            }
        } 
        #endregion

        #region 设置主菜单 private void SetMenuMain()
        /// <summary>
        /// 设置主菜单
        /// </summary>
        private void SetMenuMain()
        {
            lb.Tag = new
            {
                Index = -1,
                Name = tbName.Text.Trim()
            };
            SetLableText(lb);
            Close();
        } 
        #endregion

        #region 设置Click菜单 private void SetMenuClick()
        /// <summary>
        /// 设置Click菜单
        /// </summary>
        private void SetMenuClick()
        {
            string key = tbClick.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单事件不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 0,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        } 
        #endregion

        #region 设置View菜单 private void SetMenuView()
        /// <summary>
        /// 设置View菜单
        /// </summary>
        private void SetMenuView()
        {
            string key = tbView.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单URL不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 1,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        } 
        #endregion

        #region 设置扫码推事件菜单 private void SetMenuScanCodePush()
        /// <summary>
        /// 设置扫码推事件菜单
        /// </summary>
        private void SetMenuScanCodePush()
        {
            string key = tbScanCodePush.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 2,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 设置扫码推事件且弹出“消息接收中”提示框菜单 private void SetMenuScanCodeWaitMsg()
        /// <summary>
        /// 设置扫码推事件且弹出“消息接收中”提示框菜单
        /// </summary>
        private void SetMenuScanCodeWaitMsg()
        {
            string key = tbScanCodeWaitMsg.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 3,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 设置弹出系统拍照发图菜单 private void SetMenuPicSysPhoto()
        /// <summary>
        /// 设置弹出系统拍照发图菜单
        /// </summary>
        private void SetMenuPicSysPhoto()
        {
            string key = tbPicSysPhoto.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 4,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 设置弹出拍照或者相册发图菜单 private void SetMenuPicPhotoOrAlbum()
        /// <summary>
        /// 设置弹出拍照或者相册发图菜单
        /// </summary>
        private void SetMenuPicPhotoOrAlbum()
        {
            string key = tbPicPhotoOrAlbum.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 5,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 设置弹出微信相册发图器菜单 private void SetMenuPicWeixin()
        /// <summary>
        /// 设置弹出微信相册发图器菜单
        /// </summary>
        private void SetMenuPicWeixin()
        {
            string key = tbPicWeixin.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 6,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 设置弹出地理位置选择器菜单 private void SetMenuLocationSelect()
        /// <summary>
        /// 设置弹出地理位置选择器菜单
        /// </summary>
        private void SetMenuLocationSelect()
        {
            string key = tbLocationSelect.Text;
            if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(key.Trim()))
            {
                MessageBox.Show("菜单Key不能为空");
                return;
            }
            lb.Tag = new
            {
                Index = 7,
                Name = tbName.Text.Trim(),
                Key = key.Trim()
            };
            SetLableText(lb);
            Close();
        }
        #endregion

        #region 检测菜单名称 private bool CheckName(int max)
        /// <summary>
        /// 检测菜单名称
        /// </summary>
        /// <param name="max">最大字数</param>
        /// <returns>结果</returns>
        private bool CheckName(int max)
        {
            string name = tbName.Text;
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("菜单名称不能为空");
                return false;
            }
            if (name.Trim().Length <= max * 2) return true;
            MessageBox.Show(String.Format("菜单名称不能超过{0}个字", max));
            return false;
        } 
        #endregion

        #region 设置标签内容 public static void SetLableText(Label lb)
        /// <summary>
        /// 设置标签内容
        /// </summary>
        /// <param name="lb">标签</param>
        public static void SetLableText(Label lb)
        {
            if (lb.Tag == null) return;
            dynamic tag = lb.Tag;
            if (tag.Index == -1)
            {
                lb.Text = String.Format("菜单名称：{0}", tag.Name);
                return;
            }
            if (tag.Index == 0)
            {
                lb.Text = String.Format("Click菜单\n菜单名称：{0}\n菜单事件：{1}",
                        tag.Name, tag.Key);
                return;
            }
            if (tag.Index == 1)
            {
                lb.Text = String.Format("View菜单\n菜单名称：{0}\n菜单URL：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 2)
            {
                lb.Text = String.Format("扫码推事件菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 3)
            {
                lb.Text = String.Format("扫码推事件且弹提示框菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 4)
            {
                lb.Text = String.Format("弹出系统拍照发图菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 5)
            {
                lb.Text = String.Format("弹出拍照或者相册发图菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 6)
            {
                lb.Text = String.Format("弹出微信相册发图器菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
            if (tag.Index == 7)
            {
                lb.Text = String.Format("弹出地理位置选择器菜单\n菜单名称：{0}\n菜单Key：{1}",
                        tag.Name, tag.Key);
            }
        } 
        #endregion

        #region 生成事件文件 private void btSave_Click(object sender, EventArgs e)
        /// <summary>
        /// 生成事件文件
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbClick.Text)) return;
            new WXMPHelperAddQRM(tbClick.Text).Show();
        } 
        #endregion
    }
}
