using System;
using System.Web;

namespace TM
{
    public class Url
    {
        public static string BaseUrl = GetBaseUrl();
        public static string GetBaseHost()
        {
            var request = HttpContext.Current.Request;
            return request.Url.Scheme + "://" + request.ServerVariables["HTTP_HOST"];
        }
        public static string GetBaseUrl()
        {
            //if (HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("//localhost") == -1) return "/";
            //else return "/" + HttpContext.Current.Request.Url.AbsolutePath.Split('/')[1] + "/";
            //return HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
            return (GetBaseHost() + HttpContext.Current.Request.ApplicationPath).Trim('/');
        }
        public static string getQueryString()
        {
            return HttpContext.Current.Request.QueryString.ToString();
        }
        public static string getQueryString(string QueryString, string ReturnString)
        {
            var q = HttpContext.Current.Request.QueryString;
            if (q[QueryString] != null)
                return q[QueryString].ToString();
            return ReturnString;
        }
        public static string getQueryString(string QueryString)
        {
            return getQueryString(QueryString, null);
        }
        public static string LastPath()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            url = url.Substring(url.Length - 1, 1) == "/" ? url.Substring(0, url.Length - 1) : url;
            return url.Split('/')[url.Split('/').Length - 1];
        }
        public static string RedirectLogin(string loginUrl)
        {
            if (HttpContext.Current.Request.QueryString["continue"] == null)
                //HttpContext.Current.Response.Redirect(TM.Url.BaseUrl + "/" + loginUrl + "?continue=" + urlEncode(HttpContext.Current.Request.Url.ToString()));
                return BaseUrl + "/" + loginUrl + "?continue=" + urlEncode(HttpContext.Current.Request.Url.ToString());
            return BaseUrl;
        }
        public static string RedirectLogin()
        {
            return RedirectLogin("auth");
        }
        public static string RedirectContinue()
        {
            return RedirectContinue(BaseUrl);
        }
        public static string RedirectContinue(string url, bool ajaxRequest = false)
        {
            var rs = BaseUrl;
            if (ajaxRequest)
                rs = url != null ? $"{rs}/{url.Replace("?continue=", "")}" : rs;
            else
                //HttpContext.Current.Response.Redirect(urlDecode(query.Replace("?continue=", "")));
                rs = HttpContext.Current.Request.QueryString["continue"] != null ? urlDecode(HttpContext.Current.Request.Url.Query.Replace("?continue=", "")) : BaseUrl;
            return rs; //HttpContext.Current.Response.Redirect(url);
        }
        public static string ContinueUrl()
        {
            return System.Web.HttpContext.Current.Request.Url.ToString().Replace(TM.Url.BaseUrl, "").Trim('/');
        }
        public static bool CheckUrlBad()
        {
            if ((LastPath().Split('.').Length > 1 ? LastPath().Split('.')[1] : LastPath()) == "aspx")
                return true;
            return false;
        }
        public static void CheckUrlBadRedirect()
        {
            if (LastPath() == "Default.aspx")
                HttpContext.Current.Response.Redirect(BaseUrl + "?error=404");
        }
        public static string htmlEncode(string str)
        {
            return HttpContext.Current.Server.HtmlEncode(str.Trim());
        }
        public static string htmlDecode(string str)
        {
            return HttpContext.Current.Server.HtmlDecode(str.Trim());
        }
        public static string urlEncode(string str)
        {
            return HttpContext.Current.Server.UrlEncode(str.Trim());
        }
        public static string urlDecode(string str)
        {
            return HttpContext.Current.Server.UrlDecode(str.Trim());
        }
        public static void redirectCurrentUrl()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath.Replace(HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1], "");
            HttpContext.Current.Response.Redirect(url.Substring(url.Length - 1, 1) == "/" ? url : url + "/", false);
        }
        public static void redirectRawUrl()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath.Replace(HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1], "");
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1], ""), false);
        }
        public static void redirectDelay(string url)
        {
            HttpContext.Current.Response.AddHeader("REFRESH", "1;URL=" + url);
        }
        public static void redirectDelay(int time, string url)
        {
            HttpContext.Current.Response.AddHeader("REFRESH", time + ";URL=" + url);
        }
    }
}