using System;
using System.Linq;
using System.Collections.Generic;

namespace TM.Page
{
    public class Pages
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int RowIndex { get; set; }
        public int TotalRow { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<dynamic> query { get; set; }
        public Pages() { }
        public Pages(IEnumerable<dynamic> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            this.query = query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
        public List<dynamic> ToList()
        {
            return query.ToList();
        }

        public IEnumerable<T> PagesAnonymous<T>(IEnumerable<T> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            return query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }

        public List<T> ToList<T>(IEnumerable<T> query)
        {
            return PagesAnonymous(query).ToList();
        }

        //public IEnumerable<dynamic> DapperPage(IEnumerable<dynamic> query, int PageNumber, int PageSize)
        //{
        //    this.PageNumber = PageNumber;
        //    this.PageSize = PageSize;
        //    this.TotalRow = query.Count();
        //    this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
        //    return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        //}
        public string getRowIndexStr(Int32 index)
        {
            index = (index + (this.PageNumber - 1) * this.PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public int getRowIndex(int index) { return Convert.ToInt32(getRowIndexStr(index)); }
        public string PaginationList(int links, string linkPage)
        {
            int start = ((this.PageNumber - links) > 0) ? this.PageNumber - links : 1;
            int end = ((this.PageNumber + links) < this.TotalPage) ? this.PageNumber + links : this.TotalPage;
            string html = "<div class=\"pagination-container\"><ul class=\"pagination\">";
            string href = TM.Url.GetBaseHost() + linkPage;
            if (this.PageNumber > 1)
            {
                //html += "<li><a href=\"?page=1\">&laquo;&laquo;</a></li>";
                html += "<li><a href=\"" + ReplacePage(href, this.PageNumber - 1) + "\">&laquo;</a></li>";
            }

            if (start > 1)
            {
                html += "<li><a href=\"" + ReplacePage(href, 1) + "\">1</a></li>";
                html += "<li class=\"disabled\"><span>...</span></li>";
            }

            for (int i = start; i <= end; i++)
            {
                string active = this.PageNumber == i ? "active" : "";
                html += "<li class=\"" + active + "\"><a href=\"" + ReplacePage(href, i) + "\">" + i + "</a></li>";
            }

            if (end < this.TotalPage)
            {
                html += "<li class=\"disabled\"><span>...</span></li>";
                html += "<li><a href=\"" + ReplacePage(href, this.TotalPage) + "\">" + this.TotalPage + "</a></li>";
            }

            if (this.PageNumber < this.TotalPage)
            {
                html += "<li><a href=\"" + ReplacePage(href, this.PageNumber + 1) + "\">&raquo;</a></li>";
                //html += "<li><a href=\"?page=" + TMDapperPage.TotalPage + "\">&raquo;&raquo;</a></li>";
            }
            html += "</ul></div>";

            return html;
        }
        private string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
    public static class Dapper
    {
        public static int PageNumber { get; set; }
        public static int TotalPage { get; set; }
        public static int RowIndex { get; set; }
        public static int TotalRow { get; set; }
        public static int PageSize = 15;

        public static IEnumerable<dynamic> DapperPage(this IEnumerable<dynamic> query, int PageNumber, int PageSize)
        {
            Dapper.PageNumber = PageNumber;
            Dapper.PageSize = PageSize;
            Dapper.TotalRow = query.Count();
            Dapper.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Dapper.TotalRow) / Convert.ToDecimal(PageSize)));
            return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize); ;
        }
        public static string getRowIndexStr(Int32 index)
        {
            index = (index + (Dapper.PageNumber - 1) * Dapper.PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public static Int32 getRowIndex(Int32 index) { return Convert.ToInt32(getRowIndexStr(index)); }
        public static string PaginationList(int links, string linkPage)
        {
            int start = ((Dapper.PageNumber - links) > 0) ? Dapper.PageNumber - links : 1;
            int end = ((Dapper.PageNumber + links) < Dapper.TotalPage) ? Dapper.PageNumber + links : Dapper.TotalPage;
            string html = "<div class=\"pagination-container\"><ul class=\"pagination\">";
            string href = TM.Url.GetBaseHost() + linkPage;
            if (Dapper.PageNumber > 1)
            {
                //html += "<li><a href=\"?page=1\">&laquo;&laquo;</a></li>";
                html += "<li><a href=\"" + ReplacePage(href, Dapper.PageNumber - 1) + "\">&laquo;</a></li>";
            }

            if (start > 1)
            {
                html += "<li><a href=\"" + ReplacePage(href, 1) + "\">1</a></li>";
                html += "<li class=\"disabled\"><span>...</span></li>";
            }

            for (int i = start; i <= end; i++)
            {
                string active = Dapper.PageNumber == i ? "active" : "";
                html += "<li class=\"" + active + "\"><a href=\"" + ReplacePage(href, i) + "\">" + i + "</a></li>";
            }

            if (end < Dapper.TotalPage)
            {
                html += "<li class=\"disabled\"><span>...</span></li>";
                html += "<li><a href=\"" + ReplacePage(href, Dapper.TotalPage) + "\">" + Dapper.TotalPage + "</a></li>";
            }

            if (Dapper.PageNumber < Dapper.TotalPage)
            {
                html += "<li><a href=\"" + ReplacePage(href, Dapper.PageNumber + 1) + "\">&raquo;</a></li>";
                //html += "<li><a href=\"?page=" + TMDapperPage.TotalPage + "\">&raquo;&raquo;</a></li>";
            }
            html += "</ul></div>";

            return html;
        }
        private static string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
    public class ExmPage
    {
        private static int Paging = 0;
        private static int TotalPage = 0;
        //private static int RowIndex = 0;
        private static int TotalRow = 0;
        private static int Offset = 0;
        public static int getPaging() { return Paging; }
        public static int getTotalPage() { return TotalPage; }
        public static int getRowIndex(int index) { return (index + (Paging - 1) * Offset); }
        public static string getRowIndexStr(int index)
        {
            index = (index + (Paging - 1) * Offset);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public static int getTotalRow() { return TotalRow; }
        public static int getOffset() { return Offset; }
        public static System.Data.DataTable getTable(string _table, string _cdt, string _orderBy, int _offset)
        {
            string str = "SELECT COUNT(*) AS counts FROM " + _table + " " + _cdt;
            System.Data.DataTable dt = TM.SQL.DBStatic.ToDataTable(str);
            if (dt.Rows.Count > 0)
            {
                //
                Offset = _offset;
                //
                TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["counts"]) / Convert.ToDecimal(Offset)));
                //
                if (System.Web.HttpContext.Current.Request["page"] != null) Paging = Convert.ToInt32(System.Web.HttpContext.Current.Request["page"]);
                else Paging = 1;
                if (Paging > TotalPage) Paging = TotalPage;
                if (Paging < 1) Paging = 1;
                //
                TotalRow = Convert.ToInt32(dt.Rows[0]["counts"]);
            }


            int start = Convert.ToInt32((getPaging() - 1) * _offset) + 1;
            int end = start + _offset - 1;
            //string str = "WITH LIMIT AS(" +
            //    "SELECT *,ROW_NUMBER() OVER(ORDER BY SIID) AS rowIndex FROM " + _table + _cdt + ")" +
            //    "SELECT *,(SELECT count(*) FROM LIMIT) AS counts FROM LIMIT WHERE rowIndex BETWEEN " + start + " AND " + end;
            str = "WITH LIMIT AS(" +
                "SELECT *,ROW_NUMBER() OVER(ORDER BY " + _orderBy + ") AS rowIndex FROM " + _table + " " + _cdt + ")" +
                "SELECT * FROM LIMIT WHERE rowIndex BETWEEN " + start + " AND " + end;
            dt = TM.SQL.DBStatic.ToDataTable(str);

            return dt;

            //WITH LIMIT AS (SELECT TOP 20 *,ROW_NUMBER() OVER(ORDER BY SIID) AS numRow FROM tbl_Sim)
            //SELECT * FROM LIMIT WHERE numRow>0 

            //WITH LIMIT AS(SELECT TOP 30 *,ROW_NUMBER() OVER(ORDER BY SIID) AS rowNum FROM tbl_Sim)
            //SELECT * FROM LIMIT WHERE rowNum>20 ORDER BY rowNum
        }
        public static System.Data.DataTable getTable(string _table, string _cdt, string _orderBy, int _page, int _offset)
        {
            string str = "SELECT COUNT(*) AS counts FROM " + _table + " " + _cdt;
            System.Data.DataTable dt = TM.SQL.DBStatic.ToDataTable(str);
            if (dt.Rows.Count > 0)
            {
                Offset = _offset;
                TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["counts"]) / Convert.ToDecimal(Offset)));
                if (_page > 0) Paging = _page;
                else Paging = 1;
                if (Paging > TotalPage) Paging = TotalPage;
                if (Paging < 1) Paging = 1;
                TotalRow = Convert.ToInt32(dt.Rows[0]["counts"]);
            }
            int start = Convert.ToInt32((getPaging() - 1) * _offset) + 1;
            int end = start + _offset - 1;
            str = "WITH LIMIT AS(" +
                "SELECT *,ROW_NUMBER() OVER(ORDER BY " + _orderBy + ") AS rowIndex FROM " + _table + " " + _cdt + ")" +
                "SELECT * FROM LIMIT WHERE rowIndex BETWEEN " + start + " AND " + end;
            dt = TM.SQL.DBStatic.ToDataTable(str);
            return dt;
        }
        public static System.Data.DataTable getTable(string _select, string _table, string _cdt, string _orderBy, int _page, int _offset)
        {
            string str = "SELECT COUNT(*) AS counts FROM " + _table + " " + _cdt;
            System.Data.DataTable dt = TM.SQL.DBStatic.ToDataTable(str);
            if (dt.Rows.Count > 0)
            {
                Offset = _offset;
                TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["counts"]) / Convert.ToDecimal(Offset)));
                if (_page > 0) Paging = _page;
                else Paging = 1;
                if (Paging > TotalPage) Paging = TotalPage;
                if (Paging < 1) Paging = 1;
                TotalRow = Convert.ToInt32(dt.Rows[0]["counts"]);
            }
            int start = Convert.ToInt32((getPaging() - 1) * _offset) + 1;
            int end = start + _offset - 1;
            str = "WITH LIMIT AS(" +
                "SELECT " + _select + ",ROW_NUMBER() OVER(ORDER BY " + _orderBy + ") AS rowIndex FROM " + _table + " " + _cdt + ")" +
                "SELECT * FROM LIMIT WHERE rowIndex BETWEEN " + start + " AND " + end;
            dt = TM.SQL.DBStatic.ToDataTable(str);
            return dt;
        }
        public static System.Collections.Generic.List<string> getArrayPaging()
        {
            System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();
            for (int i = 1; i <= getTotalPage(); i++)
            {
                ls.Add(i.ToString());
            }
            return ls;
        }
        public static string querySearch()
        {
            return System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.QueryString.ToString()).Replace(System.Web.HttpContext.Current.Request["page"] != null ? "&page=" + System.Web.HttpContext.Current.Request["page"] : " ", "");
        }
    }
}