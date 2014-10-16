namespace Wing.WeiXin.MP.SDK.WXMPHelper
{
    partial class WXMPHelperQRCode
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
            this.tbParam = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btGetQRC = new System.Windows.Forms.Button();
            this.btGetQRCLimit = new System.Windows.Forms.Button();
            this.btDownload = new System.Windows.Forms.Button();
            this.tbTicket = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bgwDownload = new System.ComponentModel.BackgroundWorker();
            this.bgwCreate = new System.ComponentModel.BackgroundWorker();
            this.label4 = new System.Windows.Forms.Label();
            this.lbExpire = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbExpire = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // tbParam
            // 
            this.tbParam.Location = new System.Drawing.Point(251, 9);
            this.tbParam.Name = "tbParam";
            this.tbParam.Size = new System.Drawing.Size(115, 21);
            this.tbParam.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "参数： （目前参数只支持1--100000）";
            // 
            // btGetQRC
            // 
            this.btGetQRC.Location = new System.Drawing.Point(261, 45);
            this.btGetQRC.Name = "btGetQRC";
            this.btGetQRC.Size = new System.Drawing.Size(110, 23);
            this.btGetQRC.TabIndex = 2;
            this.btGetQRC.Text = "创建临时二维码";
            this.btGetQRC.UseVisualStyleBackColor = true;
            this.btGetQRC.Click += new System.EventHandler(this.btGetQRC_Click);
            // 
            // btGetQRCLimit
            // 
            this.btGetQRCLimit.Location = new System.Drawing.Point(257, 92);
            this.btGetQRCLimit.Name = "btGetQRCLimit";
            this.btGetQRCLimit.Size = new System.Drawing.Size(115, 23);
            this.btGetQRCLimit.TabIndex = 2;
            this.btGetQRCLimit.Text = "创建永久二维码";
            this.btGetQRCLimit.UseVisualStyleBackColor = true;
            this.btGetQRCLimit.Click += new System.EventHandler(this.btGetQRCLimit_Click);
            // 
            // btDownload
            // 
            this.btDownload.Location = new System.Drawing.Point(307, 337);
            this.btDownload.Name = "btDownload";
            this.btDownload.Size = new System.Drawing.Size(75, 23);
            this.btDownload.TabIndex = 2;
            this.btDownload.Text = "下载二维码";
            this.btDownload.UseVisualStyleBackColor = true;
            this.btDownload.Click += new System.EventHandler(this.btDownload_Click);
            // 
            // tbTicket
            // 
            this.tbTicket.Location = new System.Drawing.Point(12, 171);
            this.tbTicket.Multiline = true;
            this.tbTicket.Name = "tbTicket";
            this.tbTicket.Size = new System.Drawing.Size(370, 54);
            this.tbTicket.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "二维码ticket";
            // 
            // pbQRCode
            // 
            this.pbQRCode.Location = new System.Drawing.Point(12, 260);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(100, 100);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQRCode.TabIndex = 3;
            this.pbQRCode.TabStop = false;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(129, 276);
            this.tbPath.Multiline = true;
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(253, 49);
            this.tbPath.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "二维码路径：";
            // 
            // bgwDownload
            // 
            this.bgwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDownload_DoWork);
            this.bgwDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDownload_RunWorkerCompleted);
            // 
            // bgwCreate
            // 
            this.bgwCreate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCreate_DoWork);
            this.bgwCreate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCreate_RunWorkerCompleted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "有效期：";
            // 
            // lbExpire
            // 
            this.lbExpire.AutoSize = true;
            this.lbExpire.Location = new System.Drawing.Point(210, 149);
            this.lbExpire.Name = "lbExpire";
            this.lbExpire.Size = new System.Drawing.Size(11, 12);
            this.lbExpire.TabIndex = 5;
            this.lbExpire.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "有效时间(秒) 最大:1800";
            // 
            // tbExpire
            // 
            this.tbExpire.Location = new System.Drawing.Point(167, 46);
            this.tbExpire.Name = "tbExpire";
            this.tbExpire.Size = new System.Drawing.Size(78, 21);
            this.tbExpire.TabIndex = 0;
            // 
            // WXMPHelperQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 372);
            this.Controls.Add(this.lbExpire);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.pbQRCode);
            this.Controls.Add(this.btGetQRCLimit);
            this.Controls.Add(this.btDownload);
            this.Controls.Add(this.btGetQRC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTicket);
            this.Controls.Add(this.tbExpire);
            this.Controls.Add(this.tbParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WXMPHelperQRCode";
            this.Text = "二维码工具";
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbParam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btGetQRC;
        private System.Windows.Forms.Button btGetQRCLimit;
        private System.Windows.Forms.Button btDownload;
        private System.Windows.Forms.TextBox tbTicket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgwDownload;
        private System.ComponentModel.BackgroundWorker bgwCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbExpire;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbExpire;
    }
}