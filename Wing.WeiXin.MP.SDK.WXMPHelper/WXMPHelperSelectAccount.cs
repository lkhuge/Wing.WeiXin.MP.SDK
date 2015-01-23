using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wing.WeiXin.MP.SDK;
using Wing.WeiXin.MP.SDK.ConfigSection.BaseConfig;
using Wing.WeiXin.MP.SDK.Entities;
using Wing.WeiXin.MP.SDK.Enumeration;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    /// <summary>
    /// 微信公共平台简易助手选择账号界面
    /// </summary>
    public partial class WXMPHelperSelectAccount : Form
    {
        /// <summary>
        /// 回调方法
        /// </summary>
        private readonly Action<WXAccount> callback;

        #region 根据回调方法实例化 public FrmSelectAccount(Action<WXAccount> callback)
        /// <summary>
        /// 根据回调方法实例化
        /// </summary>
        /// <param name="callback">回调方法</param>
        public WXMPHelperSelectAccount(Action<WXAccount> callback)
        {
            this.callback = callback;
            InitializeComponent();
        } 
        #endregion

        #region 初始化 private void FrmSelectAccount_Load(object sender, EventArgs e)
        /// <summary>
        /// 初始化
        /// </summary>
        private void FrmSelectAccount_Load(object sender, EventArgs e)
        {
            foreach (WXAccount a in GlobalManager.ConfigManager.BaseConfig.AccountList.GetWXAccountList())
            {
                dgv.Rows.Add(
                    a.ID, 
                    a.Type == WeixinMPType.Service ? "服务号" : "订阅号",
                    GlobalManager.ConfigManager.BaseConfig.Token,
                    a.AppID,
                    a.AppSecret);
            }
        } 
        #endregion

        #region 执行操作 private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        /// <summary>
        /// 执行操作
        /// </summary>
        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            callback(new WXAccount(
                dgv.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString()));
            Close();
        } 
        #endregion
    }
}
