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
using System.Globalization;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class Reports_rptDCRViewApprovedDetails_New : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsdoc = null;
    DataSet dschem = null;
    DataSet dsunlist = null;
    DataSet dsstk = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Day = string.Empty;
    string StrDate = string.Empty;
    string strProduct_Detail = string.Empty;
    string strSubArea = string.Empty;
    string strProduct_Gift = string.Empty;
    string strActivity_Remark = string.Empty;
    string strCall_Remark = string.Empty;
    string strUnProduct_Detail = string.Empty;
    string strUnProduct_Gift = string.Empty;

    string Sf_HQ = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    string sDCR = string.Empty;

    DataTable dtrowClr_Dr = null;
    DataTable dtrowClr_Chm = null;
    DataTable dtrowClr_Uld = null;
    DataTable dtrowClr_Stk = null;
    DataTable dtrowClr_Hos = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strDateMatch = string.Empty;
        string strMonth = string.Empty;
        string str = string.Empty;
        div_code = Request.QueryString["div_code"].Trim().ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["Month"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["Year"].ToString());
        Day = Request.QueryString["Day"].ToString();
        if (Day.Length != 2)
        {
            strDateMatch = "0" + Day;
        }
        else
        {
            strDateMatch = Day;
        }

        if (cmonth.ToString().Length != 2)
        {
            strMonth = "0" + cmonth;
        }
        else
        {
            strMonth = Convert.ToString(cmonth);
        }

        StrDate = strDateMatch + "/" + strMonth + "/" + cyear;

        //CultureInfo english = new CultureInfo("en-US");

        str = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(DateTime.Parse(StrDate).DayOfWeek);

        lblHead.Text = "<b>Daily Call Report - " + "<span style='color:#0077FF'>" + StrDate + " - " + str + "</span>" + "</b>";


        //CreateDynamicTable(cmonth, cyear, sf_code);
        DCR dc = new DCR();

        DCR dcsf = new DCR();
        dssf = dcsf.getSfName_HQ(sf_code);

        if (dssf.Tables[0].Rows.Count > 0)
        {
            Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }

        lblFieldForceName.Text = "<b>Field Force Name - </b>" + Sf_Name.ToString();

        lblHQ.Text = "<b>Head Quarters - </b>" + Sf_HQ.ToString();



        //dsdoc = dc.get_Temp_and_Approved_dcrLstDOC_details(sf_code, StrDate, 1); //1-Listed Doctor
        dsdoc = dc.get_Temp_and_Approved_dcrLstDOC_Tuned(sf_code, StrDate,div_code, "1"); //1-Listed Doctor---------jas
        grdLstDr.DataSource = dsdoc;
        grdLstDr.DataBind();
        dtrowClr_Dr = dsdoc.Tables[0].Copy();

        //dsdoc = dc.get_Temp_and_Approved_dcr_che_details(sf_code, StrDate, 2); //2-Chemists
        dschem = dc.get_Temp_and_Approved_dcr_che_details(sf_code, StrDate, 2); //2-Chemists
        GVChemist.DataSource = dschem;
        GVChemist.DataBind();
        dtrowClr_Chm = dschem.Tables[0].Copy();

        //dsdoc = dc.get_Temp_and_Approved_unlst_doc_details(sf_code, StrDate, 4); //1-UnListed Doctor
        dsunlist = dc.get_Temp_and_Approved_dcrUnLst_DOC_Tuned(sf_code, StrDate, div_code, "4"); //1-UnListed Doctor
        GVUnlstDr.DataSource = dsunlist;
        GVUnlstDr.DataBind();
        dtrowClr_Uld = dsunlist.Tables[0].Copy();

        //dsdoc = dc.get_Temp_and_Approved_dcr_stk_detailsView(sf_code, StrDate, 3); //3-Stockist
        dsstk = dc.get_Temp_and_Approved_dcr_stk_detailsView(sf_code, StrDate, 3); //3-Stockist
        GVStockist.DataSource = dsstk;
        GVStockist.DataBind();

        lblSubArea.Text = "<b>Sub Area Worked - </b>" + Session["strSubArea"];

       
    } 
    protected void grdLstDr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strTime = "";

        DCR dc = new DCR();
        //dsdoc = dc.get_Temp_and_Approved_dcrLstDOC_Tuned(sf_code, StrDate, div_code, "1");  //1-Listed Doctor
        dtrowClr_Dr = dsdoc.Tables[0].Copy();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;

            Label lblLatlong = (Label)e.Row.FindControl("lblLatlong");
            string strlongname = "";
            if (lblLatlong.Text != "0" && lblLatlong.Text != "-" && lblLatlong.Text != "-")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = lblLatlong.Text;
                hLink.Attributes.Add("Class", "tbldetail_Data");

                sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + dtrowClr_Dr.Rows[index][1].ToString() + "/&Mode=" + "/" + "D" + " ";
                hLink.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                hLink.NavigateUrl = "#";

                hLink.ToolTip = "Click here";
                hLink.Attributes.Add("style", "cursor:pointer");
                hLink.Font.Underline = true;
                hLink.Font.Bold = true;
                hLink.ForeColor = System.Drawing.Color.Blue;
                lblLatlong.Controls.Add(hLink);
            } 

        }
    }
    protected void GVStockist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strTime = "";

        DCR dc = new DCR();
        //dsdoc = dc.get_Temp_and_Approved_dcrLstDOC_Tuned(sf_code, StrDate, div_code, "1");  //1-Listed Doctor
        dtrowClr_Dr = dsstk.Tables[0].Copy();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;

            Label lblLatlong = (Label)e.Row.FindControl("lblLatilong");
            string strlongname = "";
            if (lblLatlong.Text != "0" && lblLatlong.Text != "-" && lblLatlong.Text != "-")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = lblLatlong.Text;
                hLink.Attributes.Add("Class", "tbldetail_Data");

                sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + dtrowClr_Dr.Rows[index][2].ToString() + "/&Mode=" + "/" + "S" + " ";
                hLink.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                hLink.NavigateUrl = "#";

                hLink.ToolTip = "Click here";
                hLink.Attributes.Add("style", "cursor:pointer");
                hLink.Font.Underline = true;
                hLink.Font.Bold = true;
                hLink.ForeColor = System.Drawing.Color.Blue;
                lblLatlong.Controls.Add(hLink);
            }
             

        }
    }

    protected void GVUnlstDr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DCR dc = new DCR();      
        dtrowClr_Uld = dsunlist.Tables[0].Copy();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //RP 
            int index = e.Row.RowIndex;

            Label lblLatlong = (Label)e.Row.FindControl("lblLatlong");
            string strlongname = "";
            if (lblLatlong.Text != "0" && lblLatlong.Text != "-" && lblLatlong.Text != "-")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = lblLatlong.Text;
                hLink.Attributes.Add("Class", "tbldetail_Data");

                sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + dtrowClr_Uld.Rows[index][1].ToString() + "/&Mode=" + "/" + "ULD" + " ";
                hLink.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                hLink.NavigateUrl = "#";

                hLink.ToolTip = "Click here";
                hLink.Attributes.Add("style", "cursor:pointer");
                hLink.Font.Underline = true;
                hLink.Font.Bold = true;
                hLink.ForeColor = System.Drawing.Color.Blue;
                lblLatlong.Controls.Add(hLink);
            } 

        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {


        string attachment = "attachment; filename=Export.xls";
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

    protected void GVChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DCR dc = new DCR(); 

        dtrowClr_Chm = dschem.Tables[0].Copy();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int index = e.Row.RowIndex;

            Label lblLatlong = (Label)e.Row.FindControl("lblLatlong");
            string strlongname = "";
            if (lblLatlong.Text != "0" && lblLatlong.Text != "-" && lblLatlong.Text != "-")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = lblLatlong.Text;
                hLink.Attributes.Add("Class", "tbldetail_Data");

                sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + dtrowClr_Chm.Rows[index][2].ToString() + "/&Mode=" + "/" + "C" + " ";
                hLink.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                hLink.NavigateUrl = "#";

                hLink.ToolTip = "Click here";
                hLink.Attributes.Add("style", "cursor:pointer");
                hLink.Font.Underline = true;
                hLink.Font.Bold = true;
                hLink.ForeColor = System.Drawing.Color.Blue;
                lblLatlong.Controls.Add(hLink);
            }
             
        }
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}