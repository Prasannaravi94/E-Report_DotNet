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
using System.Drawing;

public partial class MIS_Reports_rptTP_Dev_MGR : System.Web.UI.Page
{

    string divcode = string.Empty;
    string sfCode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string sfname = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string test = string.Empty;
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();

        if (!Page.IsPostBack)
        {
            lblRegionName.Text = sfname;

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);

            lblHead.Text = "TP - Deviation for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";

            FillDeviation();
        }

    }
    private void FillDeviation()
    {

        SalesForce sal = new SalesForce();

        dsSalesForce = sal.TP_DeviationMGR_New(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear));

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdTP.Visible = true;
            dsSalesForce.Tables[0].Columns["hq"].SetOrdinal(3);
            grdTP.DataSource = dsSalesForce;
            grdTP.DataBind();
        }
        else
        {
            grdTP.DataSource = dsSalesForce;
            grdTP.DataBind();
        }
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int index = e.Row.RowIndex;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblsf_name = (Label)e.Row.Cells[1].FindControl("lblsf_name");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblDCR_Wrkwith = (Label)e.Row.FindControl("lblDCR_Wrkwith");

            Label lblDCR_terr = (Label)e.Row.FindControl("lblDCR_terr");

            //string mystring = "josaf,mani,josaf,aaryav";  
            List<string> uniqueValues = lblDCR_Wrkwith.Text.ToLower().Split(',').Distinct().ToList();
            string Tp_wrkedwith = string.Join(",", uniqueValues);

            lblDCR_Wrkwith.Text = Tp_wrkedwith;


            List<string> uniqueValues2 = lblDCR_terr.Text.ToLower().Split(',').Distinct().ToList();
            string DCR_Terrr = string.Join(",", uniqueValues2);

            lblDCR_terr.Text = DCR_Terrr;



            if (test == lblsf_name.Text)
            {
                lblsf_name.Text = "";
                lblSNo.Text = "";
            }
            else
            {
                e.Row.BackColor = Color.FromArgb(0xB5C7DE);
                count += 1;
                lblSNo.Text = Convert.ToString(count);
                test = lblsf_name.Text;
                //lblsf_name.Text = test;
                e.Row.Cells[1].Style.Add("white-space", "normal");
                e.Row.Cells[1].Style.Add("background", "inherit");
                e.Row.Cells[2].Style.Add("background", "inherit");
            }

        }
    }



    protected void grdTP_RowCreated(object sender, GridViewRowEventArgs e)
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


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#F1F5F8", true);
           
            AddMergedCells(objgridviewrow, objtablecell, 0, "Date", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Day", "#F1F5F8", true);


            AddMergedCells(objgridviewrow, objtablecell, 3, "As Per TP", "#F1F5F8", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Worktype", "#B5C7DE", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Territory", "#B5C7DE", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Worked With", "#B5C7DE", false);



            AddMergedCells(objgridviewrow, objtablecell, 3, "As Per DCR", "#F1F5F8", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Worktype", "#B5C7DE", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Territory", "#B5C7DE", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Worked With", "#B5C7DE", false);

            AddMergedCells(objgridviewrow, objtablecell, 0, "Status", "#F1F5F8", true);



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
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "black");
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}

