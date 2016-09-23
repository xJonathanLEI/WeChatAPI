/*
 * Working on the comments!!
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Drawing;
using HtmlAgilityPack;

namespace WeChatAPI.Desktop
{
    public class WeChat
    {
        /// <summary>
        /// The QR code has been scanned by user. You should show the avatar in place of the QR code.
        /// </summary>
        public event EventHandler QRCodeScanned;
        /// <summary>
        /// The QR code has expired. Use GetQRCodeAsync() to refresh the image.
        /// </summary>
        public event EventHandler QRCodeExpired;
        /// <summary>
        /// The user has clicked on the login confirmation button.
        /// </summary>
        public event EventHandler LoginConfirmed;
        /// <summary>
        /// The login process is done and the status has been saved.
        /// </summary>
        public event EventHandler LoginFinished;
        /// <summary>
        /// The current session has expired. Navigate to the login page.
        /// </summary>
        public event EventHandler SessionExpired;
        /// <summary>
        /// Something wrong happened in the Sync or SyncCheck process.
        /// </summary>
        public event EventHandler SyncOrSyncCheckError;
        /// <summary>
        /// Started the Sync process.
        /// </summary>
        public event EventHandler SyncStarted;
        /// <summary>
        /// Completed the Sync process.
        /// </summary>
        public event EventHandler SyncCompleted;
        /// <summary>
        /// Started the SyncCheck process.
        /// </summary>
        public event EventHandler SyncCheckStarted;
        /// <summary>
        /// Completed the SyncCheck process.
        /// </summary>
        public event EventHandler SyncCheckCompleted;

        string QRURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        const string jsLoginURL = "https://login.wx.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=en_US";
        string qrStatusURL;
        /// <summary>
        /// 
        /// </summary>
        string redirectUri;
        /// <summary>
        /// Depending on the user account, they may be assigned to different hosts.
        /// This variable stores the host right after login.
        /// </summary>
        string host;
        /// <summary>
        /// SyncCheck keys are stored in this list.
        /// </summary>
        List<SyncKeyItem> SyncCheckKeys;
        /// <summary>
        /// Sync keys are stored in this list.
        /// </summary>
        List<SyncKeyItem> SyncKeys;


        private NetworkHandler netHandler;
        BaseRequest BR;
        SaveState save;
        bool qrCodeScanned;
        InitResponse initRes;
        bool checkingMessages;
        string rootPath;

        public WeChat(string rootPath)
        {
            this.rootPath = rootPath;
            netHandler = new NetworkHandler();
            qrCodeScanned = false;
        }

        public void ClearSave()
        {
            File.Delete(rootPath + "saved");
        }

        public bool LoadFromSave()
        {
            save = new SaveState();
            if (File.Exists(rootPath + "saved"))
            {
                try
                {
                    save = JsonConvert.DeserializeObject<SaveState>(new StreamReader(File.OpenRead(rootPath + "saved"), System.Text.Encoding.UTF8).ReadToEnd());
                    host = save.host;
                    CookieContainer cc = new CookieContainer();
                    foreach (Cookie cookie in save.cookies)
                    {
                        cookie.Secure = true;
                        cc.Add(cookie);

                    }
                    netHandler = new NetworkHandler(cc);
                    return true;
                }
                catch(Exception ex)
                {
                    string err = ex.ToString();
                    ClearSave();
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Get and activate the QR code
        /// </summary>
        /// <returns>BitmapImage containing the QR code</returns>
        public async Task<Image> GetQRCodeAsync()
        {
            string getResult = await netHandler.GETRequestAsync(jsLoginURL);
            NetworkHandler.ReadUntil(ref getResult, "uuid");
            NetworkHandler.ReadUntil(ref getResult, "\"");
            string uuid = NetworkHandler.ReadUntil(ref getResult, "\"");
            string qrCodeURL = "https://login.weixin.qq.com/qrcode/" + uuid;
            Image qrCode = await netHandler.GETRequestAsImageAsync(qrCodeURL);
            qrStatusURL = "https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=" + System.Net.WebUtility.UrlEncode(uuid);
            qrCodeScanned = false;
            WaitUntilScanned();
            return qrCode;
        }

        private async Task WaitUntilScanned()
        {
            while (true)
            {
                string getResult = await netHandler.GETRequestAsync(qrStatusURL + "&tip=" + (qrCodeScanned ? "0" : "1"), true);
                if (getResult.Contains("408"))
                    continue;
                else if (getResult.Contains("400"))
                {
                    QRCodeExpired?.Invoke(this, null);
                    return;
                }
                else if (getResult.Contains("201"))
                {
                    NetworkHandler.ReadUntil(ref getResult, "base64,");
                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(NetworkHandler.ReadUntil(ref getResult, "'")));
                    Image pp = Image.FromStream(ms);
                    qrCodeScanned = true;
                    QRCodeScanned.Invoke(this, new QRCodeScannedEventArgs() { profilePicture = pp });
                }
                else if (getResult.Contains("200"))
                {
                    NetworkHandler.ReadUntil(ref getResult, "uri=\"");
                    redirectUri = NetworkHandler.ReadUntil(ref getResult, "\"");
                    host = redirectUri;
                    NetworkHandler.ReadUntil(ref host, "//");
                    host = NetworkHandler.ReadUntil(ref host, "/");
                    LoginConfirmed.Invoke(this, null);
                    FinishLoginAsync();
                    break;
                }
            }
        }

        public async Task FinishLoginAsync()
        {
            string getResult = await netHandler.GETRequestAsync(redirectUri, true);
            HtmlDocument getDoc = new HtmlDocument();
            getDoc.LoadHtml(getResult);
            save = new SaveState();
            save.cookies = new List<Cookie>();
            foreach (Cookie cookieToSave in netHandler.cookies.GetCookies(new Uri("https://" + host)))
                save.cookies.Add(cookieToSave);
            save.skey = getDoc.DocumentNode.Descendants("skey").ElementAt(0).InnerText;
            save.wxsid = getDoc.DocumentNode.Descendants("wxsid").ElementAt(0).InnerText;
            save.wxuin = getDoc.DocumentNode.Descendants("wxuin").ElementAt(0).InnerText;
            save.pass_ticket = getDoc.DocumentNode.Descendants("pass_ticket").ElementAt(0).InnerText;
            save.host = host;
            using (StreamWriter SW = new StreamWriter(File.Create(rootPath + "saved"), System.Text.Encoding.UTF8))
                await SW.WriteAsync(JsonConvert.SerializeObject(save));
            LoginFinished?.Invoke(this, null);
        }

        public async Task<InitResponse> InitAsync()
        {
            PostRequest postContent = new PostRequest();
            BR = new BaseRequest() { DeviceID = "", Skey = save.skey };
            BR.Sid = save.wxsid;
            BR.Uin = save.wxuin;
            postContent.BaseRequest = BR;
            string postResult = await netHandler.POSTRequestCoreAsync("https://" + host + "/cgi-bin/mmwebwx-bin/webwxinit", JsonConvert.SerializeObject(postContent));
            initRes = JsonConvert.DeserializeObject<InitResponse>(postResult);
            SyncCheckKeys = new List<SyncKeyItem>();
            SyncKeys = new List<SyncKeyItem>();
            foreach (SyncKeyItem key in initRes.SyncKey.List)
            {
                SyncCheckKeys.Add(key);
                SyncKeys.Add(key);
            }
            return initRes;
        }

        public async Task<GetContactListResponse> GetContactListAsync()
        {
            string getResult = await netHandler.GETRequestAsync("https://" + host + "/cgi-bin/mmwebwx-bin/webwxgetcontact?lang=en_US&r=" + GenerateMessageID() + "&seq=0&skey=" + System.Net.WebUtility.UrlEncode(save.skey));
            GetContactListResponse contactListRes = JsonConvert.DeserializeObject<GetContactListResponse>(getResult);
            return contactListRes;
        }

        public string UnixNow()
        {
            var timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(timeSpan.TotalSeconds).ToString() + timeSpan.Milliseconds.ToString("000");
        }

        public string GenerateMessageID()
        {
            return UnixNow() + Math.Round(new Random().NextDouble(), 3).ToString("0.000").Replace(".", "");
        }

        public async Task<string> SendMessageToUsername(string content, string username)
        {
            if (username == initRes.User.UserName) return "";
            SendMessageRequest SMR = new SendMessageRequest() { BaseRequest = BR };
            SMR.Scene = 0;
            SMR.Msg = new Message();
            SMR.Msg.FromUserName = initRes.User.UserName;
            SMR.Msg.ClientMsgId = GenerateMessageID();
            SMR.Msg.LocalID = SMR.Msg.ClientMsgId;
            SMR.Msg.Content = content;
            SMR.Msg.Type = 1;
            SMR.Msg.ToUserName = username;
            string postResult = await netHandler.POSTRequestAsync("https://" + host + "/cgi-bin/mmwebwx-bin/webwxsendmsg", JsonConvert.SerializeObject(SMR));
            return postResult;
        }

        public void StartChekcingNewMessages()
        {
            checkingMessages = true;
            CheckNewMessages();
        }

        public async Task CheckNewMessages()
        {
            string syncCheckResultCopy;
            while (checkingMessages)
            {
                try
            {

                    SyncCheckStarted?.Invoke(this, null);
                    string syncCheckUrl = "https://webpush." + host + "/cgi-bin/mmwebwx-bin/synccheck?r=" + GenerateMessageID() + "&skey=" + System.Net.WebUtility.UrlEncode(save.skey) + "&sid=" + System.Net.WebUtility.UrlEncode(save.wxsid) + "&uin=" + System.Net.WebUtility.UrlEncode(save.wxuin) + "&deviceid=e177768502217168&synckey=";
                    for (int i = 0; i < SyncCheckKeys.Count; i++)
                        if (i == SyncCheckKeys.Count - 1)
                            syncCheckUrl += SyncCheckKeys[i].Key + "_" + SyncCheckKeys[i].Val;
                        else
                            syncCheckUrl += SyncCheckKeys[i].Key + "_" + SyncCheckKeys[i].Val + "%7C";
                    string syncCheckResult = await netHandler.GETRequestAsync(syncCheckUrl);
                    syncCheckResultCopy = syncCheckResult;
                    NetworkHandler.ReadUntil(ref syncCheckResult, "retcode:\"");
                    string retCode = NetworkHandler.ReadUntil(ref syncCheckResult, "\"");
                    NetworkHandler.ReadUntil(ref syncCheckResult, "selector:\"");
                    string selector = NetworkHandler.ReadUntil(ref syncCheckResult, "\"");
                    SyncCheckCompleted?.Invoke(this, new SyncCheckCompletedEventArgs() { retcode = retCode, selector = selector });
                    if (selector != "0")
                    {
                        SyncAgain:
                        SyncStarted?.Invoke(this, null);
                        SyncRequest SR = new SyncRequest() { BaseRequest = BR, rr = -Convert.ToInt64(GenerateMessageID()) };
                        SR.SyncKey = new SyncKey() { Count = SyncKeys.Count };
                        SR.SyncKey.List = new List<SyncKeyItem>();
                        foreach (SyncKeyItem key in SyncKeys)
                            SR.SyncKey.List.Add(key);
                        string syncResult = await netHandler.POSTRequestAsync("https://" + host + "/cgi-bin/mmwebwx-bin/webwxsync?sid=" + System.Net.WebUtility.UrlEncode(save.wxsid) + "&skey=" + System.Net.WebUtility.UrlEncode(save.skey), JsonConvert.SerializeObject(SR));
                        SyncResponse syncRes = JsonConvert.DeserializeObject<SyncResponse>(syncResult);

                        try
                        {
                            List<SyncKeyItem> SyncCheckKeysCopy = new List<SyncKeyItem>();
                            List<SyncKeyItem> SyncKeysCopy = new List<SyncKeyItem>();
                            foreach (SyncKeyItem key in syncRes.SyncKey.List)
                                SyncKeysCopy.Add(key);
                            foreach (SyncKeyItem key in syncRes.SyncCheckKey.List)
                                SyncCheckKeysCopy.Add(key);
                            SyncCheckKeys = SyncCheckKeysCopy;
                            SyncKeys = SyncKeysCopy;
                        }
                        catch
                        {
                            SyncOrSyncCheckError?.Invoke(this, new SyncOrSyncCheckErrorEventArgs() { errorMessage = "Sync returns EMPTY" });
                        }

                        SyncCompleted?.Invoke(this, new SyncCompletedEventArgs() { syncResponse = syncRes });
                    }
                    else if (retCode == "1101")
                    {
                        SessionExpired?.Invoke(this, null);
                        checkingMessages = false;
                        return;
                    }
                }
            catch(Exception ex)
            {
                    SyncOrSyncCheckError?.Invoke(this, new SyncOrSyncCheckErrorEventArgs() { errorMessage = ex.ToString() });
                }
            }
        }
    }

    public class QRCodeScannedEventArgs : EventArgs
    {
        public Image profilePicture { get; set; }
    }

    public class SyncCompletedEventArgs : EventArgs
    {
        public SyncResponse syncResponse { get; set; }
    }

    public class SyncOrSyncCheckErrorEventArgs : EventArgs
    {
        public string errorMessage { get; set; }
    }

    public class SyncCheckCompletedEventArgs : EventArgs
    {
        public string retcode { get; set; }
        public string selector { get; set; }
    }
}
