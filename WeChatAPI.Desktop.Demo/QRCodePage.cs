using System;
using System.Windows.Forms;

namespace WeChatAPI.Desktop.Demo
{
    /// <summary>
    /// This page is where the user scans the QR code and login
    /// </summary>
    public partial class QRCodePage : Form
    {
        public QRCodePage()
        {
            InitializeComponent();
        }

        private async void QRCodePage_Shown(object sender, EventArgs e)
        {
            //Load QR code image
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = await Program.wechat.GetQRCodeAsync();
        }

        private void QRCodePage_Load(object sender, EventArgs e)
        {
            //Handles events
            Program.wechat.QRCodeExpired += Wechat_QRCodeExpired;
            Program.wechat.QRCodeScanned += Wechat_QRCodeScanned;
            Program.wechat.LoginConfirmed += Wechat_LoginConfirmed;
            Program.wechat.LoginFinished += Wechat_LoginFinished;
        }

        private void Wechat_LoginFinished(object sender, EventArgs e)
        {
            //Login process finished. Navigate to MainPage
            Hide();
            new MainPage().ShowDialog();
            Close();
        }

        private void Wechat_LoginConfirmed(object sender, EventArgs e)
        {
            //The user has confirmed login on the phone
            labelConfirmLogin.Hide();
            labelLoggingIn.Show();
        }

        private void Wechat_QRCodeScanned(object sender, EventArgs e)
        {
            //The QR code has been scanned
            //Show the avatar
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = (e as QRCodeScannedEventArgs).profilePicture;
            labelScanToLogin.Hide();
            labelConfirmLogin.Show();
        }

        private async void Wechat_QRCodeExpired(object sender, EventArgs e)
        {
            //QR code expired. 
            pictureBox1.Image = null;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = await Program.wechat.GetQRCodeAsync();
        }
    }
}
