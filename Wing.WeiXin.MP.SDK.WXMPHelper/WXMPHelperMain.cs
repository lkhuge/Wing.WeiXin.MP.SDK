using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wing.CL.Net;
using Wing.CL.Serialize;
using Wing.WeiXin.MP.SDK.Controller;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Entities.Menu;
using Wing.WeiXin.MP.SDK.Entities.Menu.ForGet;
using WXMenu = Wing.WeiXin.MP.SDK.Entities.Menu.Menu;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    /// <summary>
    /// 微信公共平台简易助手主界面
    /// </summary>
    public partial class WXMPHelperMain : Form
    {
        /// <summary>
        /// 最大主菜单数量
        /// </summary>
        public const int MaxMainMenu = 3;

        /// <summary>
        /// 最大子菜单数量
        /// </summary>
        public const int MaxSubMenu = 5;

        /// <summary>
        /// 标签为空时的内容
        /// </summary>
        public const string LabelNullText = "无";

        /// <summary>
        /// 标签列表
        /// </summary>
        private Dictionary<string, Label> lbList;

        #region 实例化 public FrmMain()
        /// <summary>
        /// 实例化
        /// </summary>
        public WXMPHelperMain()
        {
            InitializeComponent();
        } 
        #endregion

        #region 初始化 private void FrmMain_Load(object sender, EventArgs e)
        /// <summary>
        /// 初始化
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            GlobalManager.Init();
            lbList = new Dictionary<string, Label>
            {
                {"1",  lb1},  {"2",  lb2},  {"3",  lb3},
                {"11", lb11}, {"21", lb21}, {"31", lb31},
                {"12", lb12}, {"22", lb22}, {"32", lb32},
                {"13", lb13}, {"23", lb23}, {"33", lb33},
                {"14", lb14}, {"24", lb24}, {"34", lb34},
                {"15", lb15}, {"25", lb25}, {"35", lb35}
            };
            foreach (KeyValuePair<string, Label> kv in lbList)
            {
                kv.Value.Text = LabelNullText;
            }
        } 
        #endregion

        #region 设置菜单 private void lb_MouseDoubleClick(object sender, MouseEventArgs e)
        /// <summary>
        /// 设置菜单
        /// </summary>
        private void lb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Label lb = (Label) sender;
            if (CheckCreate(lb)) new WXMPHelperSelect(lb, lbList).ShowDialog();
        } 
        #endregion

        #region 检测是否可以创建 private bool CheckCreate(Label lb)
        /// <summary>
        /// 检测是否可以创建
        /// </summary>
        /// <param name="lb">标签</param>
        /// <returns>结果</returns>
        private bool CheckCreate(Label lb)
        {
            int index = Int32.Parse(lb.Name.Substring(2, 1));
            if (lb.Name.Length == 3)
            {
                if (index != 1 && lbList[(index - 1).ToString(CultureInfo.InvariantCulture)].Tag == null) return false;
                return true;
            }
            if (lb.Name.Length == 4)
            {
                if (lbList[index.ToString(CultureInfo.InvariantCulture)].Tag == null) return false;
                dynamic tag = lbList[index.ToString(CultureInfo.InvariantCulture)].Tag;
                if (tag.Index != -1) return false;
                int index2 = Int32.Parse(lb.Name.Substring(3, 1));
                if (index2 != 1 && lbList[index + "" + (index2 - 1)].Tag == null) return false;
                return true;
            }

            return false;
        } 
        #endregion

        #region 创建菜单按钮 private void btSave_Click(object sender, EventArgs e)
        /// <summary>
        /// 创建菜单按钮
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            WXMenu menu = new WXMenu
            {
                button = new List<AMenuItem>()
            };
            for (int index = 1; index <= MaxMainMenu; index++)
            {
                AddMenuSub(menu.button, index);
            }
            new WXMPHelperSelectAccount(a =>
            {
                btSave.Enabled = false;
                btSave.Text = "正在创建中";
                bgwCreate.RunWorkerAsync(new
                {
                    Account = a,
                    Menu = menu
                });
            }).ShowDialog();
        } 
        #endregion

        #region 创建菜单 private void bgwCreate_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 创建菜单
        /// </summary>
        private void bgwCreate_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic data = e.Argument;
            WXAccount account = data.Account;
            WXMenu menu = data.Menu;
            e.Result = new MenuController().CreateMenu(account, menu);
        } 
        #endregion

        #region 创建菜单完成 private void bgwCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 创建菜单完成
        /// </summary>
        private void bgwCreate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.Enabled = true;
            btSave.Text = "创建菜单";
            ErrorMsg msg = (ErrorMsg) e.Result;
            MessageBox.Show(msg.GetIntroduce());
        } 
        #endregion

        #region 重置按钮 private void btReset_Click(object sender, EventArgs e)
        /// <summary>
        /// 重置按钮
        /// </summary>
        private void btReset_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Label> kv in lbList)
            {
                kv.Value.Text = LabelNullText;
                kv.Value.Tag = null;
            }
        } 
        #endregion

        #region 导出按钮 private void btOutput_Click(object sender, EventArgs e)
        /// <summary>
        /// 导出按钮
        /// </summary>
        private void btOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "微信公共平台菜单备份文件(*.wxmp)|*.wxmp",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "导出微信公共平台菜单",
                AddExtension = true,
                DefaultExt = "wxmp",
                FileName = "微信公共平台菜单备份文件",
                OverwritePrompt = true
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            File.WriteAllLines(dialog.FileName, lbList.Select(kv => String.Format(
                "{0};{1};{2}", 
                kv.Key, 
                kv.Value.Text.Replace("\n", "{LF}"),
                JSONHelper.JSONSerializeN(kv.Value.Tag))));
            MessageBox.Show("导出完成");
        } 
        #endregion

        #region 导入按钮 private void btInput_Click(object sender, EventArgs e)
        /// <summary>
        /// 导入按钮
        /// </summary>
        private void btInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "微信公共平台菜单备份文件(*.wxmp)|*.wxmp",
                RestoreDirectory = true,
                FilterIndex = 1,
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "导入微信公共平台菜单"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string[] lines = File.ReadAllLines(dialog.FileName);
            foreach (string l in lines)
            {
                string[] info = l.Split(';');
                lbList[info[0]].Text = info[1].Replace("{LF}", "\n");
                lbList[info[0]].Tag = JSONHelper.JSONDeserializeN(info[2]);
            }
            MessageBox.Show("导入完成");
        } 
        #endregion

        #region 查看按钮 private void btView_Click(object sender, EventArgs e)
        /// <summary>
        /// 查看按钮
        /// </summary>
        private void btView_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Label> kv in lbList)
            {
                kv.Value.Text = LabelNullText;
                kv.Value.Tag = null;
            }
            new WXMPHelperSelectAccount(a =>
            {
                btView.Enabled = false;
                btView.Text = "正在查询中";
                bgwView.RunWorkerAsync(a);
            }).ShowDialog();
        } 
        #endregion

        #region 查询菜单 private void bgwView_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 查询菜单
        /// </summary>
        private void bgwView_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = new MenuController().GetMenu((WXAccount) e.Argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                e.Result = null;
            }
        } 
        #endregion

        #region 查询菜单完成 private void bgwView_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 查询菜单完成
        /// </summary>
        private void bgwView_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                MenuForGet menu = (MenuForGet)e.Result;
                int index = 1;
                foreach (MenuButtonForGet item in menu.menu.button)
                {
                    if (item.sub_button == null || item.sub_button.Count == 0)
                    {
                        SetLabelMenuSub(item, index);
                    }
                    else
                    {
                        SetLabelMenuMain(item, index);
                    }
                    index++;
                }
            }

            btView.Enabled = true;
            btView.Text = "查看";
        } 
        #endregion

        #region 删除按钮 private void btDelete_Click(object sender, EventArgs e)
        /// <summary>
        /// 删除按钮
        /// </summary>
        private void btDelete_Click(object sender, EventArgs e)
        {
            new WXMPHelperSelectAccount(a =>
            {
                if (MessageBox.Show("确认删除？", "警告！！", MessageBoxButtons.YesNo) 
                    == DialogResult.No) return;
                btDelete.Enabled = false;
                btDelete.Text = "正在删除中";
                bgwDelete.RunWorkerAsync(a);
            }).ShowDialog();
        } 
        #endregion

        #region 删除菜单 private void bgwDelete_DoWork(object sender, DoWorkEventArgs e)
        /// <summary>
        /// 删除菜单
        /// </summary>
        private void bgwDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            WXAccount account = (WXAccount)e.Argument;
            e.Result = new MenuController().DeleteMenu(account);
        } 
        #endregion

        #region 删除菜单完成 private void bgwDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        /// <summary>
        /// 删除菜单完成
        /// </summary>
        private void bgwDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btDelete.Enabled = true;
            btDelete.Text = "删除";
            ErrorMsg msg = (ErrorMsg)e.Result;
            MessageBox.Show(msg.GetIntroduce());
        } 
        #endregion

        #region 添加菜单项 private void AddMenuSub(List<AMenuItem> subButton, int index, int index2 = 0)
        /// <summary>
        /// 添加菜单项
        /// </summary>
        /// <param name="subButton">菜单列表</param>
        /// <param name="index">主菜单索引</param>
        /// <param name="index2">子菜单索引</param>
        private void AddMenuSub(List<AMenuItem> subButton, int index, int index2 = 0)
        {
            string id = index + (index2 == 0 ? "" : index2.ToString(CultureInfo.InvariantCulture));
            if (lbList[id].Tag == null) return;
            dynamic tag = lbList[id].Tag;
            if (tag.Index == -1)
            {
                MenuList mainButton = new MenuList
                {
                    name = tag.Name,
                    sub_button = new List<AMenuItem>()
                };
                for (int i = 1; i <= MaxSubMenu; i++)
                {
                    AddMenuSub(mainButton.sub_button, index, i);
                }
                subButton.Add(mainButton);
            }
            if (tag.Index == 0)
            {
                subButton.Add(new MenuButtonClick
                {
                    name = tag.Name,
                    key = tag.Key
                });
            }
            if (tag.Index == 1)
            {
                subButton.Add(new MenuButtonView
                {
                    name = tag.Name,
                    url = tag.Key
                });
            }
        } 
        #endregion

        #region 设置子菜单标签 private void SetLabelMenuSub(MenuButtonForGet item, int index, int index2 = 0)
        /// <summary>
        /// 设置子菜单标签
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <param name="index">主菜单索引</param>
        /// <param name="index2">子菜单索引</param>
        private void SetLabelMenuSub(MenuButtonForGet item, int index, int index2 = 0)
        {
            string id = index + ((index2 != 0) ? index2.ToString(CultureInfo.InvariantCulture) : "");
            if (item.type.Equals("click"))
            {
                lbList[id].Tag = new
                {
                    Index = 0,
                    Name = item.name,
                    Key = item.key
                };
            }
            if (item.type.Equals("view"))
            {
                lbList[id].Tag = new
                {
                    Index = 1,
                    Name = item.name,
                    Key = item.url
                };
            }
            WXMPHelperSelect.SetLableText(lbList[id]);
        } 
        #endregion

        #region 设置主菜单标签 private void SetLabelMenuMain(MenuButtonForGet item, int index)
        /// <summary>
        /// 设置主菜单标签
        /// </summary>
        /// <param name="item">菜单对象</param>
        /// <param name="index">主菜单索引</param>
        private void SetLabelMenuMain(MenuButtonForGet item, int index)
        {
            lbList[index.ToString(CultureInfo.InvariantCulture)].Tag = new
            {
                Index = -1,
                Name = item.name,
            };
            WXMPHelperSelect.SetLableText(lbList[index.ToString(CultureInfo.InvariantCulture)]);
            int index2 = 1;
            foreach (MenuButtonForGet itemSub in item.sub_button)
            {
                SetLabelMenuSub(itemSub, index, index2);
                index2++;
            }
        } 
        #endregion
    }
}
