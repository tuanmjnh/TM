using System;
using System.Web;

namespace TM
{
    public class Data
    {
        public static int IndexDataList(object sender)
        {
            System.Web.UI.WebControls.DataList gvRow = (System.Web.UI.WebControls.DataList)(sender as System.Web.UI.Control).Parent.Parent;
            return gvRow.EditItemIndex;
        }
        //public static int IndexRepeater(object sender)
        //{
        //    System.Web.UI.WebControls.Repeater gvRow = (System.Web.UI.WebControls.Repeater)(sender as System.Web.UI.Control).Parent.Parent;
        //    return gvRow.Item;
        //}
        public static int IndexGridview(object sender)
        {
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)(sender as System.Web.UI.Control).Parent.Parent;
            return gvRow.RowIndex;
        }
        public static int IndexGVLinkButton(object sender)
        {
            System.Web.UI.WebControls.LinkButton ct = (System.Web.UI.WebControls.LinkButton)sender;
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)ct.NamingContainer;
            return gvRow.RowIndex;
        }
        public static int IndexGVButton(object sender)
        {
            System.Web.UI.WebControls.Button ct = (System.Web.UI.WebControls.Button)sender;
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)ct.NamingContainer;
            return gvRow.RowIndex;
        }
        public static int IndexGVCheckBox(object sender)
        {
            System.Web.UI.WebControls.CheckBox ct = (System.Web.UI.WebControls.CheckBox)sender;
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)ct.NamingContainer;
            return gvRow.RowIndex;
        }
        public static int IndexGVRadioButton(object sender)
        {
            System.Web.UI.WebControls.RadioButton ct = (System.Web.UI.WebControls.RadioButton)sender;
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)ct.NamingContainer;
            return gvRow.RowIndex;
        }
        public static int IndexGVTextBox(object sender)
        {
            System.Web.UI.WebControls.TextBox ct = (System.Web.UI.WebControls.TextBox)sender;
            System.Web.UI.WebControls.GridViewRow gvRow = (System.Web.UI.WebControls.GridViewRow)ct.NamingContainer;
            return gvRow.RowIndex;
        }
        public static System.Web.UI.WebControls.WebControl ControlGridview(System.Web.UI.WebControls.GridView gv, object sender, string controlName)
        {
            return (System.Web.UI.WebControls.WebControl)gv.Rows[IndexGridview(sender)].FindControl(controlName);
        }
        public static System.Web.UI.Control ControlGridviewRow(System.Web.UI.WebControls.GridViewRow row, string controlName)
        {
            return (System.Web.UI.Control)row.FindControl(controlName);
        }
    }
}