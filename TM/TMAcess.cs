using System;
using System.Web;

namespace TM
{
    public class Access
    {
        public static string Cookie = "ck";
        public static string Session = "ss";
        public static string Value { set; get; }
        public static string LoggedKey { set; get; }
        public static string login = "TokenApp";
        public static string path = "/";
        public static string domain = "localhost";
        public static DateTime cookieExpires { set; get; }
        public static void Login() { if (Value == "ck") LoginCookie.Login(); else LoginSession.Login(); }
        public static void Logout() { if (Value == "ck") LoginCookie.Logout(); else LoginSession.Logout(); }
        public static bool CheckLogin() { if (Value == "ck") return LoginCookie.CheckLogin(); else return LoginSession.CheckLogin(); }
        //public static string GetUserName() { if (Value == "ck") return LoginCookie.GetUserName(); else return LoginSession.GetUserName(); }
        //public static string GetPassword() { if (Value == "ck") return LoginCookie.GetPassword(); else return LoginSession.GetPassword(); }
        //public static string GetLastVisit() { if (Value == "ck") return LoginCookie.GetLastVisit(); else return LoginSession.GetLastVisit(); }
    }
    public class LoginCookie
    {
        static HttpCookie cookie = new HttpCookie(Access.login);
        public static void Login()
        {
            try
            {
                Access.LoggedKey = Guid.NewGuid().ToString();
                cookie.Values["Key"] = Access.LoggedKey;
                //cookie.Values[pass] = _pass;
                //cookie.Values[lastVisit] = DateTime.Now.ToString();
                cookie.Expires = Access.cookieExpires != null ? Access.cookieExpires : DateTime.Now.AddDays(30);
                //cookie.Path = "/";    //Limiting Cookies to a Folder or Application
                //cookie.Domain = "localhost"; //Limiting Cookie Domain Scope
                HttpContext.Current.Response.Cookies.Add(cookie);   //HttpContext.Current.Request
                Access.Value = "ck";
            }
            catch (Exception) { }
        }
        public static void Logout()
        {
            try
            {
                cookie = new HttpCookie(Access.login);
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception) { }
        }
        public static bool CheckLogin()
        {
            try { if (cookie["Key"] == Access.LoggedKey) return true; return false; }
            catch (Exception) { return false; }
        }
        public static HttpCookie GetCkLogin()
        {
            try { if (cookie != null) return HttpContext.Current.Request.Cookies[Access.login]; return null; }
            catch (Exception) { return null; }
        }
        //public static string GetUserName()
        //{
        //    if (cookie[user] != null)
        //        return cookie[user];
        //    return null;
        //}
        //public static string GetLastVisit()
        //{
        //    if (cookie[lastVisit] != null)
        //        return cookie[lastVisit];
        //    return null;
        //}
    }
    public class LoginSession
    {
        public static void Login()
        {
            try
            {
                if (HttpContext.Current.Session[Access.login] == null)
                {
                    Access.LoggedKey = Guid.NewGuid().ToString();
                    HttpContext.Current.Session[Access.login] = Access.LoggedKey;
                    Access.Value = "ss";
                }
            }
            catch (Exception) { }
        }
        public static void Logout()
        {
            try { HttpContext.Current.Session.Remove(Access.login); }
            catch (Exception) { }
        }
        public static bool CheckLogin()
        {
            try { if (HttpContext.Current.Session[Access.login] != null) return true; return false; }
            catch (Exception) { return false; }
        }
        //public static string GetUserName()
        //{
        //    return HttpContext.Current.Session[user] != null ? HttpContext.Current.Session[user].ToString() : null;
        //}
        //public static string GetPassword()
        //{
        //    return HttpContext.Current.Session[pass] != null ? HttpContext.Current.Session[pass].ToString() : null;
        //}
        //public static string GetLastVisit()
        //{
        //    return HttpContext.Current.Session[lastVisit] != null ? HttpContext.Current.Session[lastVisit].ToString() : null;
        //}
        //public static void SetUserName(string _user)
        //{
        //    HttpContext.Current.Session[user] = _user;
        //}
        //public static void SetPassword(string _pass)
        //{
        //    HttpContext.Current.Session[pass] = _pass;
        //}
        //public static void SetLastVisit(string _lastVisit)
        //{
        //    HttpContext.Current.Session[lastVisit] = _lastVisit;
        //}
    }
}