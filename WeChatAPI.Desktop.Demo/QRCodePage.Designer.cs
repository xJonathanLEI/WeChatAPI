namespace WeChatAPI.Desktop.Demo
{
    partial class QRCodePage
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelScanToLogin = new System.Windows.Forms.Label();
            this.labelLoggingIn = new System.Windows.Forms.Label();
            this.labelConfirmLogin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(101, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(430, 430);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelScanToLogin
            // 
            this.labelScanToLogin.AutoSize = true;
            this.labelScanToLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScanToLogin.Location = new System.Drawing.Point(135, 564);
            this.labelScanToLogin.Name = "labelScanToLogin";
            this.labelScanToLogin.Size = new System.Drawing.Size(355, 37);
            this.labelScanToLogin.TabIndex = 1;
            this.labelScanToLogin.Text = "Scan QR Code to Login";
            // 
            // labelLoggingIn
            // 
            this.labelLoggingIn.AutoSize = true;
            this.labelLoggingIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoggingIn.Location = new System.Drawing.Point(220, 564);
            this.labelLoggingIn.Name = "labelLoggingIn";
            this.labelLoggingIn.Size = new System.Drawing.Size(193, 37);
            this.labelLoggingIn.TabIndex = 2;
            this.labelLoggingIn.Text = "Logging in...";
            this.labelLoggingIn.Visible = false;
            // 
            // labelConfirmLogin
            // 
            this.labelConfirmLogin.AutoSize = true;
            this.labelConfirmLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfirmLogin.Location = new System.Drawing.Point(97, 564);
            this.labelConfirmLogin.Name = "labelConfirmLogin";
            this.labelConfirmLogin.Size = new System.Drawing.Size(442, 37);
            this.labelConfirmLogin.TabIndex = 3;
            this.labelConfirmLogin.Text = "Confirm Login on Your Phone";
            this.labelConfirmLogin.Visible = false;
            // 
            // QRCodePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(649, 679);
            this.Controls.Add(this.labelConfirmLogin);
            this.Controls.Add(this.labelLoggingIn);
            this.Controls.Add(this.labelScanToLogin);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QRCodePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WeChatAPI Desktop Demo";
            this.Load += new System.EventHandler(this.QRCodePage_Load);
            this.Shown += new System.EventHandler(this.QRCodePage_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelScanToLogin;
        private System.Windows.Forms.Label labelLoggingIn;
        private System.Windows.Forms.Label labelConfirmLogin;
    }
}