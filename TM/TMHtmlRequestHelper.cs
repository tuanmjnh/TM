using System;
using System.Linq;
using System.Web.Mvc;
namespace TM
{
    public class Request
    {
        public static string Controller
        {
            get
            {
                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues.ContainsKey("controller"))
                    return (string)routeValues["controller"];
                return string.Empty;
            }
        }
        public static string Action
        {
            get
            {
                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues.ContainsKey("action"))
                    return (string)routeValues["action"];
                return string.Empty;
            }
        }
        public static string Id
        {
            get
            {
                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues.ContainsKey("id"))
                    return (string)routeValues["id"];
                else if (System.Web.HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                    return System.Web.HttpContext.Current.Request.QueryString["id"];
                return string.Empty;
            }
        }
    }
}
public static class HtmlRequestHelper
{
    public static string Controller(this HtmlHelper htmlHelper)
    {
        var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
        if (routeValues.ContainsKey("controller"))
            return (string)routeValues["controller"];
        return string.Empty;
    }
    public static string Action(this HtmlHelper htmlHelper)
    {
        var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
        if (routeValues.ContainsKey("action"))
            return (string)routeValues["action"];
        return string.Empty;
    }
    public static string Id(this HtmlHelper htmlHelper)
    {
        var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;
        if (routeValues.ContainsKey("id"))
            return (string)routeValues["id"];
        else if (System.Web.HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
            return System.Web.HttpContext.Current.Request.QueryString["id"];
        return string.Empty;
    }
}
