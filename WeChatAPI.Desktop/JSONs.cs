using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeChatAPI.Desktop
{
    public class SendMessageRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public Message Msg { get; set; }
        public int Scene { get; set; }
    }

    public class SyncResponse
    {
        public int AddMsgCount { get; set; }
        public List<AddMessage> AddMsgList { get; set; }
        public BaseResponse BaseResponse { get; set; }
        public string ContinueFlag { get; set; }
        public int DelContactCount { get; set; }
        //DelContactList
        public int ModChatRoomMemberCount { get; set; }
        //ModChatRoomMemberList
        public int ModContactCount { get; set; }
        //ModContactList
        public Profile Profile { get; set; }
        public string Skey { get; set; }
        public SyncKey SyncCheckKey { get; set; }
        public SyncKey SyncKey { get; set; }
    }

    public class Profile
    {
        public string Alias { get; set; }
        //BindEmail
        //BindMobile
        public string BindUin { get; set; }
        public string BitFlag { get; set; }
        public string HeadImgUpdateFlag { get; set; }
        public string HeadImgUrl { get; set; }
        //NickName
        public string PersonalCard { get; set; }
        public string Sex { get; set; }
        public string Signature { get; set; }
        public string Status { get; set; }
        //UserName
    }

    public class AddMessage
    {
        public AppInfo AppInfo { get; set; }
        public int AppMsgType { get; set; }
        public string Content { get; set; }
        public long CreateTime { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public int ForwardFlag { get; set; }
        public string FromUserName { get; set; }
        public string HasProductID { get; set; }
        public int ImgHeight { get; set; }
        public int ImgStatus { get; set; }
        public int ImgWidth { get; set; }
        public string MediaId { get; set; }
        public string MsgId { get; set; }
        public int MsgType { get; set; }
        public string NewMsgId { get; set; }
        public int PlayLength { get; set; }
        public RecommendInfo RecommendInfo { get; set; }
        public int Status { get; set; }
        public int StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public int SubMsgType { get; set; }
        public string Ticket { get; set; }
        public string ToUserName { get; set; }
        public string Url { get; set; }
        public int VoiceLength { get; set; }
    }

    public class RecommendInfo
    {
        public string Alias { get; set; }
        public int AttrStatus { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public string NickName { get; set; }
        public string OpCode { get; set; }
        public string QQNum { get; set; }
        public string Scene { get; set; }
        public string Sex { get; set; }
        public string Signature { get; set; }
        public string Ticket { get; set; }
        public string UserName { get; set; }
        public string VerifyFlage { get; set; }
    }

    public class AppInfo
    {
        public string AppID { get; set; }
        public int Type { get; set; }
    }

    public class Message
    {
        public string ClientMsgId { get; set; }
        public string Content { get; set; }
        public string FromUserName { get; set; }
        public string LocalID { get; set; }
        public string ToUserName { get; set; }
        public int Type { get; set; }
    }

    public class SyncRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public long rr { get; set; }
        public SyncKey SyncKey { get; set; }
    }

    public class PostRequest
    {
        public BaseRequest BaseRequest { get; set; }
    }

    public class BaseRequest
    {
        public string Uin { get; set; }
        public string Sid { get; set; }
        public string Skey { get; set; }
        public string DeviceID { get; set; }
    }

    public class GetContactListResponse
    {
        public BaseResponse BaseResponse { get; set; }
        public int MemberCount { get; set; }
        public List<UserInfo> MemberList { get; set; }
        public int Seq { get; set; }
    }

    public class InitResponse
    {
        public BaseResponse BaseResponse { get; set; }
        public string ChatSet { get; set; }
        public int ClickReportInterval { get; set; }
        public string ClientVersion { get; set; }
        public List<UserInfo> ContactList { get; set; }
        public int Count { get; set; }
        public string GrayScale { get; set; }
        public string InviteStartCount { get; set; }
        public int MPSubscribeMsgCount { get; set; }
        public List<MPSubscribeMsg> MPSubscribeMsgList { get; set; }
        public string SKey { get; set; }
        public SyncKey SyncKey { get; set; }
        public long SystemTime { get; set; }
        public UserInfo User { get; set; }
    }

    public class BaseResponse
    {
        public int Ret { get; set; }
        public string ErrMsg { get; set; }
    }

    public class MPSubscribeMsg
    {
        public int MPArticleCount { get; set; }
        public List<MPArticle> MPArticleList { get; set; }
        public string NickName { get; set; }
        public long Time { get; set; }
        public string UserName { get; set; }
    }

    public class MPArticle
    {
        public string Cover { get; set; }
        public string Digest { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class SyncKey
    {
        public int Count { get; set; }
        public List<SyncKeyItem> List { get; set; }
    }

    public class SyncKeyItem
    {
        public string Key { get; set; }
        public string Val { get; set; }
    }

    public class UserInfo
    {
        public bool ChatbotON { get; set; } = false;
        public string Alias { get; set; }
        public string AppAccountFlag { get; set; }
        public string AttrStatus { get; set; }
        public string ChatRoomId { get; set; }
        public string City { get; set; }
        public string ContactFlag { get; set; }
        public string DisplayName { get; set; }
        public string EncryChatRoomId { get; set; }
        public string HeadImgFlag { get; set; }
        public string HeadImgUrl { get; set; }
        public string HideInputBarFlag { get; set; }
        public string KeyWord { get; set; }
        public int MemberCount { get; set; }
        public List<UserInfo> MemberList { get; set; }
        public string NickName { get; set; }
        public string OwnerUin { get; set; }
        public string Province { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkName { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public string Sex { get; set; }
        public string Signature { get; set; }
        public string SnsFlag { get; set; }
        public string StarFriend { get; set; }
        public string Uin { get; set; }
        public string UniFriend { get; set; }
        public string UserName { get; set; }
        public string VerifyFlag { get; set; }
        public string WebWxPluginSwitch { get; set; }
    }

    public class SaveState
    {
        public List<Cookie> cookies { get; set; }
        public string skey { get; set; }
        public string wxsid { get; set; }
        public string wxuin { get; set; }
        public string pass_ticket { get; set; }
        public string host { get; set; }
    }
}
