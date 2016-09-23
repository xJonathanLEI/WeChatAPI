using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Drawing;

public class NetworkHandler
{
    public HttpClient HC { get; set; }
    public string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";
    public List<NameValuePair> Cookies { get; set; }
    public CookieContainer cookies { get; set; }
    WebResponse getResponse;
    WebResponse postResponse;
    HttpClientHandler handler;

    public NetworkHandler()
    {
        Cookies = new List<NameValuePair>();
        cookies = new CookieContainer();
        handler = new HttpClientHandler();
        handler.CookieContainer = cookies;
        handler.AllowAutoRedirect = false;
        HC = new HttpClient(handler);
    }

    public NetworkHandler(CookieContainer cookies)
    {
        Cookies = new List<NameValuePair>();
        this.cookies = cookies;
        handler = new HttpClientHandler();
        handler.CookieContainer = this.cookies ;
        handler.AllowAutoRedirect = false;
        HC = new HttpClient(handler);
    }

    public async Task<WebResponse> GETRequestCore(string URL, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null)
    {
        HttpWebRequest HWR = (HttpWebRequest)HttpWebRequest.Create(URL);
        HWR.CookieContainer = cookies;
        if (useCookieContainer)
        {
            HWR.Headers["Cookie"] = "";
            foreach (NameValuePair cookie in Cookies)
                HWR.Headers["Cookie"] += cookie.name + "=" + cookie.value + ";";
        }
        HWR.Method = "GET";
        if (presentUserAgent) HWR.Headers["User-Agent"] = USER_AGENT;
        if (!string.IsNullOrEmpty(referer)) HWR.Headers["Referer"] = referer;
        if (headers != null)
            foreach (NameValuePair header in headers)
                HWR.Headers[header.name] = header.value;
        getResponse = await HWR.GetResponseAsync();
        if (useCookieContainer)
            if (!string.IsNullOrEmpty(getResponse.Headers["Set-Cookie"]))
                AddSetCookieToCookies(getResponse.Headers["Set-Cookie"]);
        return getResponse;
    }

    public async Task<string> GETRequestCoreAsync(string URL, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null)
    {
        using (StreamReader SR = new StreamReader((await GETRequestCore(URL, useCookieContainer, headers, presentUserAgent, referer)).GetResponseStream(), System.Text.Encoding.UTF8))
            return SR.ReadToEnd();
    }

    public async Task<string> GETRequestAsync(string URL, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null)
    {
        HttpResponseMessage response = await HC.GetAsync(new Uri(URL));
        var InputStream = await response.Content.ReadAsStreamAsync();
        
        using (StreamReader SR = new StreamReader(InputStream, System.Text.Encoding.UTF8))
            return SR.ReadToEnd();
    }

    public async Task<Image> GETRequestAsImageAsync(string URL, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null)
    {
        return Image.FromStream((await GETRequestCore(URL, useCookieContainer, headers, presentUserAgent, referer)).GetResponseStream());
    }

    public async Task<HtmlDocument> GETRequestAsDocumentAsync(string URL, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.Load((await GETRequestCore(URL, useCookieContainer, headers, presentUserAgent, referer)).GetResponseStream());
        return doc;
    }

    public async Task<WebResponse> POSTRequestCore(string URL, string param, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null, HtmlDocument aspxSourcePage = null)
    {

        HttpWebRequest HWR = (HttpWebRequest)HttpWebRequest.Create(URL);
        HWR.Headers["Cookie"] = "";
        foreach (Cookie cookie in cookies.GetCookies(new Uri(URL)))
            HWR.Headers["Cookie"] += cookie.Name + "=" + cookie.Value + ";";
        if (useCookieContainer)
        {
            HWR.Headers["Cookie"] = "";
            foreach (NameValuePair cookie in Cookies)
                HWR.Headers["Cookie"] += cookie.name + "=" + cookie.value + ";";
        }
        HWR.Method = "POST";
        if (presentUserAgent) HWR.Headers["User-Agent"] = USER_AGENT;
        if (!string.IsNullOrEmpty(referer)) HWR.Headers["Referer"] = referer;
        if (headers != null)
            foreach (NameValuePair header in headers)
                HWR.Headers[header.name] = header.value;
        if (aspxSourcePage != null)
        {
            string __VIEWSTATEGENERATOR = aspxSourcePage.DocumentNode.Descendants("input").Where(i => i.Id == "__VIEWSTATEGENERATOR").ElementAt(0).Attributes["value"].Value;
            string __EVENTVALIDATION = aspxSourcePage.DocumentNode.Descendants("input").Where(i => i.Id == "__EVENTVALIDATION").ElementAt(0).Attributes["value"].Value;
            string __VIEWSTATE = aspxSourcePage.DocumentNode.Descendants("input").Where(i => i.Id == "__VIEWSTATE").ElementAt(0).Attributes["value"].Value;
            __EVENTVALIDATION = __EVENTVALIDATION.Replace("\r", "").Replace("\n", "").Replace(" ", "+");
            param += "&__VIEWSTATE=" + WebUtility.UrlEncode(__VIEWSTATE) + "&__VIEWSTATEGENERATOR=" + WebUtility.UrlEncode(__VIEWSTATEGENERATOR) + "&__EVENTVALIDATION=" + WebUtility.UrlEncode(__EVENTVALIDATION);
        }
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(param);
        HWR.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
        await (await HWR.GetRequestStreamAsync()).WriteAsync(bytes, 0, bytes.Length);
        postResponse = await HWR.GetResponseAsync();
        if (useCookieContainer)
            if (!string.IsNullOrEmpty(postResponse.Headers["Set-Cookie"]))
                AddSetCookieToCookies(postResponse.Headers["Set-Cookie"]);
        return postResponse;
    }

    public async Task<string> POSTRequestCoreAsync(string URL, string param, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null, HtmlDocument aspxSourcePage = null)
    {
        using (StreamReader SR = new StreamReader((await POSTRequestCore(URL, param, useCookieContainer, headers, presentUserAgent, referer, aspxSourcePage)).GetResponseStream(), System.Text.Encoding.UTF8))
            return SR.ReadToEnd();
    }

    public async Task<string> POSTRequestAsync(string URL, string param, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null, HtmlDocument aspxSourcePage = null)
    {
        HttpResponseMessage response = await HC.PostAsync(new Uri(URL), new StringContent(param));
        var InputStream = await response.Content.ReadAsStreamAsync();
        using (StreamReader SR = new StreamReader(InputStream, System.Text.Encoding.UTF8))
            return SR.ReadToEnd();
    }

    public async Task<Image> POSTRequestAsImageAsync(string URL, string param, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null, HtmlDocument aspxSourcePage = null)
    {
        return Image.FromStream((await POSTRequestCore(URL, param, useCookieContainer, headers, presentUserAgent, referer, aspxSourcePage)).GetResponseStream());
    }

    public async Task<HtmlDocument> POSTRequestAsDocumentAsync(string URL, string param, bool useCookieContainer = false, List<NameValuePair> headers = null, bool presentUserAgent = false, string referer = null, HtmlDocument aspxSourcePage = null)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.Load((await POSTRequestCore(URL, param, useCookieContainer, headers, presentUserAgent, referer, aspxSourcePage)).GetResponseStream());
        return doc;
    }

    private void AddSetCookieToCookies(string setCookie)
    {
        List<string> namesToExclude = new List<string>() { "domain", "expires", "path" };
        while (setCookie != "")
        {
            while (setCookie.StartsWith(" ")) setCookie = setCookie.Remove(0, 1);
            string currentElement;
            if (setCookie.IndexOf(",") == -1 && setCookie.IndexOf(";") == -1)
            {
                currentElement = setCookie;
                setCookie = "";
            }
            else if (setCookie.IndexOf(",") == -1)
                currentElement = ReadUntil(ref setCookie, ";");
            else if (setCookie.IndexOf(";") == -1)
                currentElement = ReadUntil(ref setCookie, ",");
            else if (setCookie.IndexOf(",") < setCookie.IndexOf(";"))
                currentElement = ReadUntil(ref setCookie, ",");
            else
                currentElement = ReadUntil(ref setCookie, ";");
            if (currentElement.IndexOf("=") == -1) continue;
            string currentName = ReadUntil(ref currentElement, "=");
            bool exclude = false;
            foreach (string na in namesToExclude)
                if (na == currentName.ToLower()) exclude = true;
            if (exclude) continue;
            bool isNewCookie = true;
            foreach (NameValuePair cookie in Cookies)
                if (cookie.name == currentName)
                {
                    cookie.value = currentElement;
                    isNewCookie = false;
                }
            if (isNewCookie) Cookies.Add(new NameValuePair(currentName, currentElement));
        }
    }

    public static string ReadUntil(ref string Str, string xStr)
    {
        if (!Str.Contains(xStr)) throw new Exception("xStr is not contained in Str.");
        int SP = Str.IndexOf(xStr);
        string RET = Str.Substring(0, SP);
        Str = Str.Remove(0, SP + xStr.Length);
        return RET;
    }
}

public class NameValuePair
{
    public string name { get; set; }
    public string value { get; set; }

    public NameValuePair() { }
    public NameValuePair(string name, string value)
    {
        this.name = name;
        this.value = value;
    }
}
