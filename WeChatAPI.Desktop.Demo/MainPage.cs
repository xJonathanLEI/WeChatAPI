using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeChatAPI.Desktop;

namespace WeChatAPI.Desktop.Demo
{
    public partial class MainPage : Form
    {
        int numberOfLines = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_Shown(object sender, EventArgs e)
        {
            AddLineToOutput("Init Process...");
            await Program.wechat.InitAsync();
            AddLineToOutput("GetContact Process...");
            await Program.wechat.GetContactListAsync();
            AddLineToOutput("Getting user avatar...");
            userAvatarBox.Image = await Program.wechat.GetUserAvatar();
            labelUserNickname.Text = Program.wechat.InitResponse.User.NickName;
            foreach (WeChatContact member in Program.wechat.ContactList)
                listBoxContactList.Items.Add(member.Nickname);

            //Triggers the SyncCheck loop
            Program.wechat.SyncCheckCompleted += Wechat_SyncCheckCompleted;
            Program.wechat.SyncCompleted += Wechat_SyncCompleted;
            Program.wechat.SyncOrSyncCheckError += Wechat_SyncOrSyncCheckError;
            Program.wechat.StartChekcingNewMessages();
        }

        private void Wechat_SyncOrSyncCheckError(object sender, EventArgs e)
        {
            AddLineToOutput("[ERROR] " + (e as SyncOrSyncCheckErrorEventArgs).errorMessage);
        }

        private void Wechat_SyncCompleted(object sender, EventArgs e)
        {
            AddLineToOutput("Sync completed.");
            List<Message> messages = (e as SyncCompletedEventArgs).Messages;
            foreach (Message msg in messages)
            {
                AddLineToOutput("Synced message: " + msg.MessageID);
                if (msg.MessageType == Message.MessageTypes.TextMessage)
                {
                    AddLineToOutput("    Text Message content: " + (msg as TextMessage).MessageContent);
                }
                else
                {
                    AddLineToOutput("    Unknown message type");
                }
            }
        }

        private void Wechat_SyncCheckCompleted(object sender, EventArgs e)
        {
            //Selector = "0" indicates that there is no new content to sync
            AddLineToOutput("SynCheck completed. " + ((e as SyncCheckCompletedEventArgs).selector == "0" ? "No need to Sync." : "Starting Sync..."));
        }

        private void AddLineToOutput(string line)
        {
            if (numberOfLines == 100)
            {
                numberOfLines = 0;
                textBoxOutPut.Text = "";
            }
            textBoxOutPut.AppendText(line + "\r\n");
            textBoxOutPut.Select(textBoxOutPut.TextLength - 1, 0);
            numberOfLines++;
        }
    }
}
