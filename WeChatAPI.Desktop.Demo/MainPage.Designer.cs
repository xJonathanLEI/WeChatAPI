namespace WeChatAPI.Desktop.Demo
{
    partial class MainPage
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
            this.userAvatarBox = new System.Windows.Forms.PictureBox();
            this.labelSponsor = new System.Windows.Forms.Label();
            this.linkLabelSponsor = new System.Windows.Forms.LinkLabel();
            this.labelUserNickname = new System.Windows.Forms.Label();
            this.textBoxOutPut = new System.Windows.Forms.TextBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.labelContactList = new System.Windows.Forms.Label();
            this.listBoxContactList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.userAvatarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // userAvatarBox
            // 
            this.userAvatarBox.Location = new System.Drawing.Point(20, 20);
            this.userAvatarBox.Name = "userAvatarBox";
            this.userAvatarBox.Size = new System.Drawing.Size(100, 100);
            this.userAvatarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userAvatarBox.TabIndex = 0;
            this.userAvatarBox.TabStop = false;
            // 
            // labelSponsor
            // 
            this.labelSponsor.AutoSize = true;
            this.labelSponsor.Location = new System.Drawing.Point(1149, 842);
            this.labelSponsor.Name = "labelSponsor";
            this.labelSponsor.Size = new System.Drawing.Size(131, 25);
            this.labelSponsor.TabIndex = 1;
            this.labelSponsor.Text = "Powered by ";
            // 
            // linkLabelSponsor
            // 
            this.linkLabelSponsor.AutoSize = true;
            this.linkLabelSponsor.Location = new System.Drawing.Point(1266, 842);
            this.linkLabelSponsor.Name = "linkLabelSponsor";
            this.linkLabelSponsor.Size = new System.Drawing.Size(92, 25);
            this.linkLabelSponsor.TabIndex = 2;
            this.linkLabelSponsor.TabStop = true;
            this.linkLabelSponsor.Text = "MSFaith";
            // 
            // labelUserNickname
            // 
            this.labelUserNickname.AutoSize = true;
            this.labelUserNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserNickname.Location = new System.Drawing.Point(135, 31);
            this.labelUserNickname.Name = "labelUserNickname";
            this.labelUserNickname.Size = new System.Drawing.Size(100, 29);
            this.labelUserNickname.TabIndex = 3;
            this.labelUserNickname.Text = "Loading";
            // 
            // textBoxOutPut
            // 
            this.textBoxOutPut.Location = new System.Drawing.Point(482, 44);
            this.textBoxOutPut.Multiline = true;
            this.textBoxOutPut.Name = "textBoxOutPut";
            this.textBoxOutPut.ReadOnly = true;
            this.textBoxOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutPut.Size = new System.Drawing.Size(874, 795);
            this.textBoxOutPut.TabIndex = 4;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(477, 9);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(82, 25);
            this.labelOutput.TabIndex = 5;
            this.labelOutput.Text = "Output:";
            // 
            // labelContactList
            // 
            this.labelContactList.AutoSize = true;
            this.labelContactList.Location = new System.Drawing.Point(15, 141);
            this.labelContactList.Name = "labelContactList";
            this.labelContactList.Size = new System.Drawing.Size(132, 25);
            this.labelContactList.TabIndex = 6;
            this.labelContactList.Text = "Contact List:";
            // 
            // listBoxContactList
            // 
            this.listBoxContactList.FormattingEnabled = true;
            this.listBoxContactList.ItemHeight = 25;
            this.listBoxContactList.Location = new System.Drawing.Point(20, 183);
            this.listBoxContactList.Name = "listBoxContactList";
            this.listBoxContactList.Size = new System.Drawing.Size(440, 654);
            this.listBoxContactList.TabIndex = 7;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1368, 874);
            this.Controls.Add(this.listBoxContactList);
            this.Controls.Add(this.labelContactList);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.textBoxOutPut);
            this.Controls.Add(this.labelUserNickname);
            this.Controls.Add(this.linkLabelSponsor);
            this.Controls.Add(this.labelSponsor);
            this.Controls.Add(this.userAvatarBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WeChatAPI Desktop Demo";
            this.Shown += new System.EventHandler(this.MainPage_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.userAvatarBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox userAvatarBox;
        private System.Windows.Forms.Label labelSponsor;
        private System.Windows.Forms.LinkLabel linkLabelSponsor;
        private System.Windows.Forms.Label labelUserNickname;
        private System.Windows.Forms.TextBox textBoxOutPut;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label labelContactList;
        private System.Windows.Forms.ListBox listBoxContactList;
    }
}