namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    partial class FrmSelectAccount
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAppID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAppSecret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colType,
            this.colToken,
            this.colAppID,
            this.colAppSecret});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(784, 162);
            this.dgv.TabIndex = 0;
            this.dgv.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentDoubleClick);
            // 
            // colID
            // 
            this.colID.FillWeight = 67.47639F;
            this.colID.HeaderText = "微信OpenID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 200;
            // 
            // colType
            // 
            this.colType.FillWeight = 86.08454F;
            this.colType.HeaderText = "账号类型";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            // 
            // colToken
            // 
            this.colToken.FillWeight = 102.1815F;
            this.colToken.HeaderText = "Token";
            this.colToken.Name = "colToken";
            this.colToken.ReadOnly = true;
            this.colToken.Width = 150;
            // 
            // colAppID
            // 
            this.colAppID.FillWeight = 116.1061F;
            this.colAppID.HeaderText = "AppID";
            this.colAppID.Name = "colAppID";
            this.colAppID.ReadOnly = true;
            this.colAppID.Width = 130;
            // 
            // colAppSecret
            // 
            this.colAppSecret.FillWeight = 128.1515F;
            this.colAppSecret.HeaderText = "AppSecret";
            this.colAppSecret.Name = "colAppSecret";
            this.colAppSecret.ReadOnly = true;
            this.colAppSecret.Width = 150;
            // 
            // FrmSelectAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 162);
            this.Controls.Add(this.dgv);
            this.Name = "FrmSelectAccount";
            this.Text = "选择账号（双击选择）";
            this.Load += new System.EventHandler(this.FrmSelectAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToken;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAppID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAppSecret;
    }
}