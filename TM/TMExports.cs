using System.Web;

namespace TM
{
    public class Exports
    {
        public static void ExportExcel(object data, string filename, params string[] cols)
        {
            System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            gv.DataSource = data;
            gv.DataBind();
            HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //htw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            gv.RenderControl(htw);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            //HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public static void ExportExcel(object data, params string[] cols)
        {
            ExportExcel(data, "ExportExcelData", cols);
        }
        public System.Data.DataTable ToDataTable(System.Data.Linq.DataContext ctx, object query)
        {
            if (query == null)
            {
                throw new System.ArgumentNullException("query");
            }

            System.Data.IDbCommand cmd = ctx.GetCommand(query as System.Linq.IQueryable);
            var adapter = new System.Data.SqlClient.SqlDataAdapter();
            adapter.SelectCommand = (System.Data.SqlClient.SqlCommand)cmd;
            var dt = new System.Data.DataTable();

            try
            {
                cmd.Connection.Open();
                adapter.FillSchema(dt, System.Data.SchemaType.Source);
                adapter.Fill(dt);
            }
            catch { throw; }
            finally
            {
                cmd.Connection.Close();
            }
            return dt;
        }
    }
}
