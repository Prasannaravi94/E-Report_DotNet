using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DBase_EReport;
using Bus_EReport;

public partial class MasterFiles_Reports_DCR_Approve_Reject_Report : System.Web.UI.Page
{
    string FMonth = string.Empty;
    string FYear = string.Empty;
    DataSet dsDCR = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        FYear = Request.QueryString["Year"].ToString();
        FMonth = Request.QueryString["Month"].ToString();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);

        lblHead.Text = "DCR Reject/Approve View From "  + strFrmMonth + "- " + FYear;
        FillDCR();
    }

    private void FillDCR()
    {        
        DCR dr = new DCR();

        dsDCR = dr.getDCR_App_Rej_Report(div_code,FMonth, FYear,sf_code);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            gvApproveReject.Visible = true;
            gvApproveReject.DataSource = dsDCR;
            gvApproveReject.DataBind(); 
        }
        else
        {
            gvApproveReject.DataSource = dsDCR;
            gvApproveReject.DataBind();
        }
    }
}