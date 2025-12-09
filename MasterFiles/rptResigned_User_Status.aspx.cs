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
using System.Data.SqlClient;
using DBase_EReport;

public partial class MasterFiles_rptResigned_User_Status : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoctr = null;
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        //divcode = Request.QueryString["div_code"].ToString();
        divcode = Session["div_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Resigned User for the Period of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        Fill_Resigned_User();

    }

    private void Fill_Resigned_User()
    {
        Random rndm = new Random();
        int t = rndm.Next(1, 28);
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        string from_date = t + "-" + mnth.ToString() + "-" + year.ToString();
        ViewState["from_date"] = from_date;



        Random ran = new Random();
        int da = ran.Next(1, 28);
        int mnths = Convert.ToInt32(TMonth), year1 = Convert.ToInt32(TYear);

        string to_date = da + "-" + mnths.ToString() + "-" + year1.ToString();
        ViewState["to_date"] = to_date;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getResigned_User(divcode, Convert.ToDateTime(from_date), Convert.ToDateTime(to_date));

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        SalesForce sf = new SalesForce();
        dtGrid = sf.getResigned_User_soring(divcode, Convert.ToDateTime(ViewState["from_date"]), Convert.ToDateTime(ViewState["to_date"]));
        //sCmd = Session["GetCmdArgChar"].ToString();

        //if (sCmd == "All")
        //{
        //    dtGrid = sf.getSalesForcelist_Sort(div_code);
        //}
        //else if (sCmd != "")
        //{
        //    dtGrid = sf.getDTSalesForcelist(div_code, sCmd);
        //}
        //else if (txtsearch.Text != "")
        //{
        //    string sFind = string.Empty;
        //    sFind = " AND a." + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
        //             " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        //    dtGrid = sf.FindDTSalesForcelist(sFind);
        //}
        //else if (ddlSrc.SelectedIndex > 0)
        //{
        //    search = ddlFields.SelectedValue.ToString();

        //    if (search == "StateName")
        //    {
        //        dtGrid = sf.getDTSalesForce_st(div_code, ddlSrc.SelectedValue);
        //    }
        //    else if (search == "Designation_Name")
        //    {
        //        dtGrid = sf.getDTSalesForce_des(div_code, ddlSrc.SelectedValue);
        //    }
        //}
        //else if (ddlFilter.SelectedIndex > 0)
        //{

        //    dtGrid = sf.getDTSalesForcelist_Reporting(div_code, ddlFilter.SelectedValue);
        //}

        return dtGrid;
    }

    protected void grdSalesForce_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdSalesForce.DataSource = sortedView;
        grdSalesForce.DataBind();

    }

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
}