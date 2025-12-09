using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using DBase_EReport;

public partial class MasterFiles_MR_rptOrder_Booking_View : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
    DataSet dsDoctor = new DataSet();
    DB_EReporting db_ER = new DB_EReporting();
    string stockist_code = string.Empty;
    string stockist_name = string.Empty;



    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        Month = Request.QueryString["FMonth"].ToString();
        Year = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        stockist_code = Request.QueryString["stockist_code"];
        stockist_name = Request.QueryString["stockist_name"];

        if (!Page.IsPostBack)
        {
            lblRegionName.Text = sfname;
            lblstockist.Text = stockist_name;
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);

            lblHead.Text = "Order Booking View for the Month of " + "<span style=color:red>" + strFMonthName + " " + Year;

            Fill_Order();
        }
    }

    private void Fill_Order()
    {
        Doctor dr = new Doctor();
        string strQry = string.Empty;
        strQry = "EXEC Order_Book_View '" + sfCode + "','" + divcode + "','" + Month + "','" + Year + "','" + stockist_code + "' ";
        dsDoctor = db_ER.Exec_DataSet(strQry);

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            grddr.DataSource = dsDoctor;
            grddr.DataBind();
        }
        else
        {
            grddr.DataSource = dsDoctor;
            grddr.DataBind();
        }

    }

    protected void grddr_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblclr = (Label)e.Row.FindControl("lblclr");

            string bcolor = "#" + lblclr.Text;
            e.Row.BackColor = System.Drawing.Color.FromName(bcolor);



            Label lblHos_cnt = (Label)e.Row.FindControl("lblHos_cnt");
            Label lblHos_value = (Label)e.Row.FindControl("lblHos_value");
            Label lblPhar_cnt = (Label)e.Row.FindControl("lblPhar_cnt");
            Label lblPhar_value = (Label)e.Row.FindControl("lblPhar_value");
            Label lblDoctor_cnt = (Label)e.Row.FindControl("lblDoctor_cnt");
            Label lblDoctor_value = (Label)e.Row.FindControl("lblDoctor_value");

            if (lblHos_cnt.Text == "0")
            {
                lblHos_cnt.Text = "";
            }
            if (lblHos_value.Text == "0")
            {
                lblHos_value.Text = "";
            }
            if (lblPhar_cnt.Text == "0")
            {
                lblPhar_cnt.Text = "";
            }

            if (lblPhar_value.Text == "0")
            {
                lblPhar_value.Text = "";
            }

            if (lblDoctor_cnt.Text == "0")
            {
                lblDoctor_cnt.Text = "";
            }

            if (lblDoctor_value.Text == "0")
            {
                lblDoctor_value.Text = "";
            }
        }
    }

    protected void grddr_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {

            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();
            GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Fieldforce Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#F1F5F8", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, 2, "Hospital", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Order Count", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Value", "#F1F5F8", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, 2, "Pharmacy", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Order Count", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Value", "#F1F5F8", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, 2, "Listed Doctor", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Order Count", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Value", "#F1F5F8", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
            //
            #endregion
        }

    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        if (celltext == "#")
        {
            objtablecell.Style.Add("color", "#fff");
            objtablecell.Style.Add("border-radius", "8px 0 0 8px");
        }
        else
        {
            objtablecell.Style.Add("color", "#636d73");
        }
        objtablecell.Style.Add("border-bottom", "10px solid #fff");
        objtablecell.Style.Add("font-weight", "401");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
        objtablecell.Wrap = false;


    }





    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}