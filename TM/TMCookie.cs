using System;

namespace TM
{
    public class Cookie
    {
        public static System.Web.HttpCookie getCookie(string ckName)
        {
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ckName];
                if (cookie != null)
                    return System.Web.HttpContext.Current.Request.Cookies[ckName];
                return null;
            }
            catch { return null; }
        }
        public static void destroyCookie(string ckName)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(ckName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static bool checkCookie(string ckName, string ckKey)
        {
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ckName];
                if (cookie.Values[ckKey] != null) return true;
                return false;
            }
            catch (Exception) { return false; }
        }
        public static void setCookie(string ckName, string[] KeysValues, DateTime datetime)
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ckName];
            if (cookie == null)
                cookie = new System.Web.HttpCookie(ckName);
            if (KeysValues.Length > 0)
                for (int i = 0; i < KeysValues.Length; i++)
                {
                    string[] s = KeysValues[i].Split(',');
                    if (s.Length > 1) cookie.Values[s[0]] = s[1];
                }
            cookie.Expires = datetime;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}