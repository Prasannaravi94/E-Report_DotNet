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

public partial class MIS_Reports_rptTerritory_Format1 : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsVisit = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string sReturn = string.Empty;
    int iCount = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
        Year = Request.QueryString["Year"];
        Month = Request.QueryString["Month"];
        //Prod_Name = Request.QueryString["Prod_Name"];
        Prod = Request.QueryString["Prod"];
        sf_name = Request.QueryString["sf_name"];
        //sCurrentDate = Request.QueryString["sCurrentDate"];

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        sf_name = sf_name.TrimStart(',');

        if (!Page.IsPostBack)
        {
            FillDr();
            //CreateDynamicTable();

            //lblProd.Text = "ListedDr - Product Visit" + "<span style='color:red'> " + "(" + Prod_Name + ")" + "</span>" + "<br />" + " Listed Doctor Details for the Month of " + "" + strMonthName + "" + " " + Year + "";
            lblProd.Text = "Listed Doctor - Product Map";
            lblProd.Font.Bold = true;
        }
    }

    private void FillDr()
    {
        Doctor dr = new Doctor();
        lblname.Text = sf_name;
        dsDr = dr.getDr_Prd_Mapped_Name(div_code, sf_code);
        
        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdDr.Visible = true;
            grdDr.DataSource = dsDr;
            grdDr.DataBind();
        }
        else
        {
            grdDr.DataSource = dsDr;
            grdDr.DataBind();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void grdDr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
      
    }

    protected void grdDr_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView objGridView = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#0097AC", true);

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            //if (dsTerritory.Tables[0].Rows.Count > 0)
            //{
            //    e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            //}

            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "#0097AC", true);
            }
            else
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory", "#0097AC", true);
            }
            AddMergedCells(objgridviewrow, objtablecell, 0, "Speciality", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Class", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 3, "Tagged Product", "#0097AC", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "P1", "#0097AC", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "P2", "#0097AC", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "P3", "#0097AC", false);


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
        }
    }


    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
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

}