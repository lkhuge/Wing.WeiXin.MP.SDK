using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    public partial class WXMPHelperMain : Form
    {
        public WXMPHelperMain()
        {
            InitializeComponent();
        }

        #region 菜单管理 private void btMenu_Click(object sender, EventArgs e)
        /// <summary>
        /// 菜单管理
        /// </summary>
        private void btMenu_Click(object sender, EventArgs e)
        {
            new WXMPHelperMenu().Show();
        } 
        #endregion

        #region 二维码管理 private void buQRCode_Click(object sender, EventArgs e)
        /// <summary>
        /// 二维码管理
        /// </summary>
        private void buQRCode_Click(object sender, EventArgs e)
        {
            new WXMPHelperQRCode().Show();
        } 
        #endregion
    }
}
