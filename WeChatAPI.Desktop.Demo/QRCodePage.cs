/*
 * This page allows the user to scan the QR code and login
 */
using System;
using System.Windows.Forms;

namespace WeChatAPI.Desktop.Demo
{
    public partial class QRCodePage : Form
    {
        public QRCodePage()
        {
            InitializeComponent();
        }

        private async void QRCodePage_Shown(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = await Program.wechat.GetQRCodeAsync();
        }

        private void QRCodePage_Load(object sender, EventArgs e)
        {
            Program.wechat.QRCodeExpired += Wechat_QRCodeExpired;
            Program.wechat.QRCodeScanned += Wechat_QRCodeScanned;
            Program.wechat.LoginConfirmed += Wechat_LoginConfirmed;
            Program.wechat.LoginFinished += Wechat_LoginFinished;
        }

        private void Wechat_LoginFinished(object sender, EventArgs e)
        {
            MessageBox.Show("It works!");
        }

        private void Wechat_LoginConfirmed(object sender, EventArgs e)
        {
            labelConfirmLogin.Hide();
            labelLoggingIn.Show();
        }

        private void Wechat_QRCodeScanned(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = (e as QRCodeScannedEventArgs).profilePicture;
            labelScanToLogin.Hide();
            labelConfirmLogin.Show();
        }

        private async void Wechat_QRCodeExpired(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = await Program.wechat.GetQRCodeAsync();
        }
    }
}
