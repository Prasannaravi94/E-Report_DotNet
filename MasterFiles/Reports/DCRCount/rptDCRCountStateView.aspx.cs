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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_Reports_DCRCount_rptDCRCountStateView : System.Web.UI.Page
{

    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoc = null;
    DataSet dsDcrCount = new DataSet();

    int fldwrk_total = 0;
    string strDate = string.Empty;
    string iPendingDate = string.Empty;
    string state_code = string.Empty;
    int doctor_total = 0;
    int iPendingCount = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_DcrCount = string.Empty;
    string strLastDCRDate = string.Empty;
    string tot_DcrPendingDate = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string StrVacant;
    string StateName = string.Empty;
    string strFieledForceName = string.Empty;
    
    DataSet dsUserList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["Div_Code"].ToString();       
        state_code = Request.QueryString["state_code"].ToString();
        Month = Request.QueryString["Month"].ToString().Trim();
        StrVacant =Request.QueryString["Vacant"].ToString().Trim();
        Year = Request.QueryString["Year"].ToString();
        StateName = Request.QueryString["StateName"].ToString().Trim();
 

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(Month);

        lblHead.Text = "DCR Count Report for the Period of " + strFrmMonth + " / " + Year;
        LblForceName.Text = "State Name : " +StateName;
      

        FillSF();
    }

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DataTable dtSalesForce = new DataTable();        

       
        dsSalesForce = sf.sp_Get_DCRCount_StateWise_View(div_code, Month, Year, state_code, StrVacant.ToString()); // 28-Aug-15 -Sridevi 
        

        GvDcrCount.DataSource = dsSalesForce;
        GvDcrCount.DataBind();

        dsSalesForce = sf.sp_Get_DCRCount_StateWise_View_Missing_Name(div_code, Month, Year, state_code, StrVacant.ToString()); // 28-Aug-15 -Sridevi 

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvMissingName.DataSource = dsSalesForce;
            gvMissingName.DataBind();
        }
        else
        {
            gvMissingName.DataSource = dsSalesForce;
            gvMissingName.DataBind();
        }

    }

    protected void GvDcrCount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // e.Row.Cells[1].Text = DateTime.Now.AddMonths(-1).ToString("MMM") + " Count";
                //e.Row.Cells[1].Text = DateTime.Now.ToString("MMM") + " Count";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void OnDataBinding_gvMissingName(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFlag = (Label)e.Row.FindControl("lblFlag");

            if (lblFlag.Text == "1")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
                //e.Row.ForeColor = System.Drawing.Color.White;
            }

        }
    }
}