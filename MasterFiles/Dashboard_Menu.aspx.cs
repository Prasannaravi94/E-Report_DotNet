using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Dashboard_Menu : System.Web.UI.Page
{
    #region Declaration
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cMnth = string.Empty;
    string cYear = string.Empty;
    #endregion

    #region Page_Life_Cycle
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
        }
        catch {
            sf_code = Request.QueryString["sfcode"].ToString();
            div_code = Request.QueryString["div_Code"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
            cMnth = Request.QueryString["cMnth"].ToString();
            cYear = Request.QueryString["cYr"].ToString();
        }

        if (sf_code.Contains("MR"))
        {
            lnlbtnDashboard.Visible = true;
            LnkbtnProductMap.Visible = true;
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            lnlbtnDashboard.Visible = true;
            LnkbtnProductMap.Visible = false;
            sf_type = "2";
        }
        else
        {
            lnlbtnDashboard.Visible = false;
            LnkbtnProductMap.Visible = false;
            sf_type = "3";
        }

        //LnkbtnProductMap.Visible = false;
    } 
    #endregion

    #region Click_Events
    protected void lnkbtnVisitMonitor_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Visit_Monitor.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnSalesAnalysis_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/MasterFiles/Dashboard_TargetvsSales.aspx?sfcode=" + sf_code + "&cFMnth=" + (DateTime.Now.Month - 2).ToString().Trim() + "&cFYr=" + DateTime.Now.Year.ToString().Trim() + "&cTMnth=" + DateTime.Now.Month.ToString().Trim() + "&cTYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
        Response.Redirect("~/MasterFiles/Dashboard_TargetversusSales.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
        //Response.Redirect("~/MasterFiles/InProgress.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void lnkbtnVisitAnalysisMR_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/InProgress.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void lnkbtnVisitAnalysisManager_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/InProgress.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void lnkbtnMissedCallReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Missed_Call.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void lnkbtnProductExposure_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Product_Exp.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    protected void lnkbtnReviewReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Review_Report.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnAssessmentReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Assessment_Report.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnDrAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Single_Dr_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnSampleDespatch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Sample_Despatch.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnInputDespatch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Input_Despatch.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnCCP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_CCP.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnMyMissedCall_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_My_Missed_Call.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnCCPDaywise_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_CCP_Daywise.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnExpense_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Expense.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkbtnProd_price_lst_Click(object sender, EventArgs e) //Added by Preethi
    {
        Response.Redirect("~/MasterFiles/Dashboard_Product_prclst.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnlbtnDashboard_Click(object sender, EventArgs e) //Added by Preethi
    {
        Response.Redirect("~/MasterFiles/Dashboard_Admin_Dashboard.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void LnkbtnProductMap_Click(object sender, EventArgs e) //Added by Preethi
    {
        Response.Redirect("~/MasterFiles/Dashboard_Listeddr_Prod_Map_New.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }

    protected void lnlbtnDashboard_Sale_Click(object sender, EventArgs e) //Added by Preethi
    {
        Response.Redirect("~/MasterFiles/Dashboard_Sales_DashBoard_Admin_Brand.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnlbtnDashboard_SFE_Click(object sender, EventArgs e) //Added by Preethi
    {
        Response.Redirect("~/MasterFiles/Dashboard_Effort_Analysis_CatSpecVst_Dashboard.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lblbtnVisit_Click(object sender, EventArgs e) //Added by priya
    {
        //   Response.Redirect("~/MasterFiles/Dashboard_Visit_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
        Response.Redirect("~/MasterFiles/Dashboard_Visit_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkpob_Click(object sender, EventArgs e) //Added by priya
    {
       
        Response.Redirect("~/MasterFiles/Dashboard_POBwise.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkcomp_Click(object sender, EventArgs e) //Added by priya
    {

        Response.Redirect("~/MasterFiles/Dashboard_Comprehensive.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkSales_Click(object sender, EventArgs e) //Added by priya
    {

        Response.Redirect("~/MasterFiles/Dashboard_Sales_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkprim_Click(object sender, EventArgs e) //Added by priya
    {

        Response.Redirect("~/MasterFiles/Dashboard_Primary_Sales.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnksprim_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Slide_Primary_Sales.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkPstock_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Primary_Bill_Stk.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkPprod_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Primary_Bill_Prod.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkdate_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Visit_Datewise.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkvmode_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Visit_modewise.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkat_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Visit_At_a_glance.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkchem_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Visit_Chem_Unlstdr.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkpay_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Payslip_View.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkdcr_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_DCR_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnkcat_Click(object sender, EventArgs e) //Added by priya
    {
        Response.Redirect("~/MasterFiles/Dashboard_Cat_Drswise.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
    protected void lnktimeStatus_Click(object sender, EventArgs e) //Added by Ferooz
    {
        Response.Redirect("~/MasterFiles/Dashboard_MGR_Working_Hrs_View2.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }
	 protected void lnkbtnMonthSummary_Click(object sender, EventArgs e) //Added by Ferooz
    {
        Response.Redirect("~/MasterFiles/MonthlyReport.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }

    protected void lnkprecallAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_PreCall_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "&doc_id=" + -1 + "&cluster_code=" + -1 + "&rSF=" + -1);
    }

    protected void lnkrcpa_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/DashBoard_RCPA_Analysis.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type);
    }

    #endregion
}