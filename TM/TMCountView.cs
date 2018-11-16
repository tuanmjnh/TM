using System;
using TM.Helper;

namespace TM
{
    public class ViewCount
    {
        public static string ckInfo = "inf";
        public static string ip = "ip";
        public static string item = "item";
        public static void CountView(string sql, int id)
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ckInfo];
            if (cookie != null)
            {
                if (!cookie.hasKey(item + id))
                {
                    TM.Cookie.setCookie(ckInfo, new[] { item + id + ",true" }, DateTime.Now.AddDays(1));
                    TM.SQL.DBStatic.Execute(sql);
                }
            }
            else
            {
                TM.Cookie.setCookie(ckInfo, new[] { item + id + ",true" }, DateTime.Now.AddDays(1));
                TM.SQL.DBStatic.Execute(sql);
            }
        }
    }
}