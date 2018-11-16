using System;

namespace TM
{
    public class IpAddress
    {
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        private string GetUserIP()
        {
            string ipList = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList))
                return ipList.Split(',')[0];
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static System.Net.IPAddress[] IpAddressList()
        {
            //string IP = HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;
            //string s = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
        }
        public static string GetIpAddress()
        {
            try { return IpAddressList()[2].ToString(); }
            catch (Exception) { return ""; }
        }
        public static string GetHostName()
        {
            try { return System.Net.Dns.GetHostName(); }
            catch (Exception) { return ""; }
        }
    }
}