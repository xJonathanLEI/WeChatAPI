using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeChatAPI.Desktop.Demo
{
    static class Program
    {
        /// <summary>
        /// WeChat API handler
        /// </summary>
        public static WeChatAPI.Desktop.WeChat wechat { get; set; }
        public static string StartupPath { get { string ret = Application.StartupPath; return ret + (ret.EndsWith("\\") ? "" : "\\"); } }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Without setting the protocol type to TLS, HTTP requests would fail
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            wechat = new WeChat(StartupPath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QRCodePage());
        }
    }
}
