using System;
using System.Web;
namespace TM
{
    public class Asset
    {
        static System.Collections.Generic.List<string> css = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> js = new System.Collections.Generic.List<string>();
        public static string AssetDisPlay
        {
            get
            {
                string s = "";//\r\n
                foreach (var i in css)
                    s += "<link rel='stylesheet' type='text/css' href='" + i + "'/>";
                foreach (var i in js)
                    s += "<script type='text/javascript' src='" + i + "'></script>";
                return s;
            }
        }
        public static void addCss(string _css)
        {
            _css = TM.Url.BaseUrl + _css;
            if (_css != string.Empty)
                if (!css.Contains(_css)) css.Add(_css);
        }
        public static void addJs(string _js)
        {
            _js = TM.Url.BaseUrl + _js;
            if (_js != string.Empty)
                if (!js.Contains(_js)) js.Add(_js);
        }
        public static void addAsset(string _asset)
        {
            if (_asset != string.Empty)
            {
                string[] s = _asset.Split('.');
                if (s[s.Length - 1].ToLower() == "css") addCss(_asset);
                else addJs(_asset);
            }
        }
        public static void clearAsset()
        {
            css.Clear();
            js.Clear();
        }
        public static void addAsset(System.Collections.Generic.List<string> _css, System.Collections.Generic.List<string> _js)
        {
            if (_css.Count > 0) for (int i = 0; i < _css.Count; i++) addCss(_css[i]);
            if (js.Count > 0) for (int i = 0; i < _js.Count; i++) addJs(_js[i]);
        }
        public static void addAsset(string[] _css, string[] _js)
        {
            if (_css.Length > 0) for (int i = 0; i < _css.Length; i++) addCss(_css[i]);
            if (js.Count > 0) for (int i = 0; i < _js.Length; i++) addJs(_js[i]);
        }
        public static void newAsset(System.Collections.Generic.List<string> _css, System.Collections.Generic.List<string> _js)
        {
            clearAsset();
            addAsset(_css, _js);
        }
        public static void newAsset(string[] _css, string[] _js)
        {
            clearAsset();
            addAsset(_css, _js);
        }
        public static void addAsset(System.Collections.Generic.List<string> _asset)
        {
            if (_asset.Count > 0) for (int i = 0; i < _asset.Count; i++) addAsset(_asset[i]);
        }
        public static void addAsset(string[] _asset)
        {
            if (_asset.Length > 0) for (int i = 0; i < _asset.Length; i++) addAsset(_asset[i]);
        }
        public static void newAsset(System.Collections.Generic.List<string> _asset)
        {
            clearAsset();
            if (_asset.Count > 0) for (int i = 0; i < _asset.Count; i++) addAsset(_asset[i]);
        }
        public static void newAsset(string[] _asset)
        {
            clearAsset();
            if (_asset.Length > 0) for (int i = 0; i < _asset.Length; i++) addAsset(_asset[i]);
        }
        //public static void addAsset()
        //{
        //    //System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
        //    //if(!page.IsPostBack)
        //    if (css.Count > 0) for (int i = 0; i < css.Count; i++) addCss(css[i]);
        //    if (js.Count > 0) for (int i = 0; i < js.Count; i++) addJs(js[i]);
        //}
    }
}