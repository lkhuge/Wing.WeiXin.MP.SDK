namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    partial class WXMPHelperMain
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
            this.btMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btMenu
            // 
            this.btMenu.Location = new System.Drawing.Point(27, 22);
            this.btMenu.Name = "btMenu";
            this.btMenu.Size = new System.Drawing.Size(75, 23);
            this.btMenu.TabIndex = 0;
            this.btMenu.Text = "菜单管理";
            this.btMenu.UseVisualStyleBackColor = true;
            this.btMenu.Click += new System.EventHandler(this.btMenu_Click);
            // 
            // WXMPHelperMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btMenu);
            this.Name = "WXMPHelperMain";
            this.Text = "微信公共平台简易助手";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btMenu;
    }
}