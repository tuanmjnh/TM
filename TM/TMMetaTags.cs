using System;

namespace TM
{
    public class MetaTags
    {
        public static string copyright { set; get; }
        public static string author { set; get; }
        public static string audience { set; get; }
        public static string ogType { set; get; }
        public static string ogUrl { set; get; }
        public static string ogSite_name { set; get; }
        public static string ogTitle { set; get; }
        public static string ogImage { set; get; }
        public static string ogDescription { set; get; }
        public static string articleAuthor { set; get; }
        public static string articleSection { set; get; }
        public static string articleTag { set; get; }

        //private static string _copyright { get { return "<meta name=\"copyright\" content=\"" + copyright + "\">"; } }
        //private static string _author { get { return "<meta name=\"author\" content=\"" + author + "\">"; } }
        //private static string _audience { get { return "<meta http-equiv=\"audience\" content=\"General\">"; } }
        //private static string _ogType { get { return "<meta property=\"og:type\" content=\"article\">"; } }
        //private static string _ogUrl { get { return "<meta property=\"og:url\" content=\"" + ogUrl + "\">"; } }
        //private static string _ogSite_name { get { return "<meta property=\"og:site_name\" content=\"" + ogSite_name + "\">"; } }
        //private static string _ogTitle { get { return "<meta property=\"og:title\" content=\"" + ogTitle + "\">"; } }
        //private static string _ogImage { get { return "<meta property=\"og:image\" content=\"" + ogImage + "\">"; } }
        //private static string _ogDescription { get { return "<meta property=\"og:description\" content=\"" + ogDescription + "\">"; } }
        //private static string _articleAuthor { get { return "<meta property=\"article:author\" content=\"" + articleAuthor + "\">"; } }
        //private static string _articleSection { get { return "<meta property=\"article:section\" content=\"" + articleSection + "\">"; } }
        //private static string _articleTag { get { return "<meta property=\"article:tag\" content=\"" + articleTag + "\">"; } }
        
        public static string outString()
        {
            string _copyright = "<meta name=\"copyright\" content=\"" + copyright + "\">";
            string _author = "<meta name=\"author\" content=\"" + author + "\">";
            string _audience = "<meta http-equiv=\"audience\" content=\"General\">";
            string _ogType = "<meta property=\"og:type\" content=\"article\">";
            string _ogUrl = "<meta property=\"og:url\" content=\"" + ogUrl + "\">";
            string _ogSite_name = "<meta property=\"og:site_name\" content=\"" + ogSite_name + "\">";
            string _ogTitle = "<meta property=\"og:title\" content=\"" + ogTitle + "\">";
            string _ogImage = "<meta property=\"og:image\" content=\"" + ogImage + "\">";
            string _ogDescription = "<meta property=\"og:description\" content=\"" + ogDescription + "\">";
            string _articleAuthor = "<meta property=\"article:author\" content=\"" + articleAuthor + "\">";
            string _articleSection = "<meta property=\"article:section\" content=\"" + articleSection + "\">";
            string _articleTag = "<meta property=\"article:tag\" content=\"" + articleTag + "\">";
            return _copyright +
                    _author +
                    _audience +
                    _ogType +
                    _ogUrl +
                    _ogSite_name +
                    _ogTitle +
                    _ogImage +
                    _ogDescription +
                    _articleAuthor +
                    _articleSection +
                    _articleTag;
        }
        public static void setDefalut()
        {
            //MetaTags.copyright = TMWeb.Information.GetInfSite(TMObj.InfSite.CompanyName);
            //MetaTags.author = TMWeb.Information.GetInfSite(TMObj.InfSite.CompanyName);
            //MetaTags.audience = "General";
            ////MetaTags.ogType = "";
            ////MetaTags.ogUrl = System.Web.HttpContext.Current.Request.QueryString.ToString();
            //MetaTags.ogSite_name = TMWeb.Information.GetInfSite(TMObj.InfSite.Website);
            ////MetaTags.ogTitle = TM.Config.PageTitle;
            ////MetaTags.ogImage = "";
            //MetaTags.ogDescription = TMWeb.Information.GetInfSite(TMObj.InfSite.Address);
            //MetaTags.articleAuthor = TMWeb.Information.GetInfSite(TMObj.InfSite.CompanyName);
            //MetaTags.articleSection = TMWeb.Information.GetInfSite(TMObj.InfSite.Slogan);
            //MetaTags.articleTag = TMWeb.Information.GetInfSite(TMObj.InfSite.Slogan);
            ////MetaDispay = MetaTags.outString();
        }
        public static string MetaDispay { get { return outString(); } }
        //<meta name="copyright" content="Công ty cổ phần Truyền Thông Việt Nam - VCCorp">
        //<meta name="author" content="Công nghệ ChannelVN">
        //<meta http-equiv="audience" content="General">
        //<meta property="og:type" content="article">
        //<meta id="ctl00_idOgUrl" property="og:url" content="http://dantri.com.vn/xa-hoi/ha-noi-nghiem-cam-csgt-dung-nup-rut-chia-khoa-xe-vi-pham-1005814.htm">
        //<meta property="og:site_name" content="Dantri">
        //<meta id="ctl00_idOgIitle" property="og:title" content="Hà Nội: Nghiêm cấm CSGT đứng núp, rút chìa khóa xe vi phạm">
        //<meta id="ctl00_idOgImg" property="og:image" content="http://dantri21.vcmedia.vn/WxnUMCKrnDOssmdRa8sb2D05Ficccc/Image/2014/10/CSGT-e0eb5.jpg">
        //<meta id="ctl00_idOgDes" property="og:description" content="(Dân trí) - Đại tá Đào Vịnh Thắng - Trưởng Phòng Cảnh sát giao thông đường bộ-đường sắt Hà Nội (CSGT) - cho biết sẽ xử lý nghiêm những CSGT đứng núp khi làm nhiệm vụ tuần tra kiểm soát trên đường; đồng thời nghiêm cấm CSGT có hành vi rút hoặc giật chìa khóa xe vi phạm giao thông.">
        //<meta property="article:author" content="baodantridientu">
        //<meta property="article:section" content="News">
        //<meta property="article:tag" content="Tin tuc">
    }
}