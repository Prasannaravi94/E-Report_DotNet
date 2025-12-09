using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

public partial class Default_MR_Basic : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsProduct = null;
    DataSet dsProd = null;
    DataSet dsTerritory = null;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    DataSet dsWeek = null;
    static string sSf_Code = string.Empty;
    static string sDiv_Code = string.Empty;
    int iWeek = -1;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    DataSet dsLogin = null;
    DataSet dsHoliday = new DataSet();
    DataSet dsTP = new DataSet();
    string strday = string.Empty;
    string strWeek = string.Empty;
    string state_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sSf_Code = Session["sf_code"].ToString();
        sDiv_Code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
           // shrtct_div.Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            AdminSetup adm1 = new AdminSetup();
            dsAdmin1 = adm1.Get_Flash_News(div_code);

            if (dsAdmin1.Tables[0].Rows.Count > 0)
            {
                lblFlash.Text = dsAdmin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lblFlash.Text = lblFlash.Text.Replace("asdf", "'");
            }
            Calendar1.TodaysDate = System.DateTime.Now;
            Calendar1.NextPrevFormat = NextPrevFormat.FullMonth;
            gettalk();
            GetWorkName();
            Session["backurl"] = "~/Default_MR_Basic.aspx";
          //  BindDropDown_Month_Year();
            //ModalPopupExtender2.Show();
            //if (div_code != "2")
            //{
            //    btnDCRView.Visible = false;

            //}

            //if (div_code == "8" || div_code == "9" || div_code == "10")
            //{
            //    btnDCR.Visible = false;
            //}
            if (div_code == "23" || div_code == "104")
            {
                btnEx_entry.Visible = false;
            }
        }

    }
    private void gettalk()
    {
        AdminSetup adm = new AdminSetup();
        dsAdmin = adm.Get_talktous(div_code);

        if (dsAdmin.Tables[0].Rows.Count > 0)
        {
            lblsup.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

        }
        else
        {
            lblsup.Text = "<font color='Red'>Field Support No:-   07806914081</font> <br>Field Support Timings:- From <b>Monday to Saturday </b>(9.30 a.m to 6.00 p.m) <br> In field force login in Home Page find the 'Queries' link at Right top Corner. You can Post Your Queries/Errors/Modifications which Will be resolved within 12 Hours.";
        }
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            btnTerr.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Entry";

        }
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

        if (e.Day.IsOtherMonth)
        {

            e.Cell.Controls.Clear();
            e.Cell.Text = string.Empty;


        }
        else
        {
            DataSet dsDCR = new DataSet();
            DCR dc = new DCR();
            dsDCR = dc.getDCR_Report_MR_Calendar(sf_code, e.Day.Date.Day, e.Day.Date.Month, e.Day.Date.Year);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                Label lblArea = new Label();
                lblArea.Visible = true;
                lblArea.Text = "<br>" + dsDCR.Tables[0].Rows[0][0].ToString();
                if (dsDCR.Tables[0].Rows[0][0].ToString() == "H")
                {
                    lblArea.ForeColor = System.Drawing.Color.Green;
                    lblArea.Font.Size = 7;
                    lblArea.Font.Name = "Verdana";
                    lblArea.Font.Bold = true;
                    e.Cell.BackColor = System.Drawing.Color.Yellow;
                    e.Cell.Controls.Add(lblArea);
                }
                else if (dsDCR.Tables[0].Rows[0][0].ToString() == "WO")
                {
                    lblArea.ForeColor = System.Drawing.Color.Blue;
                    lblArea.Font.Size = 7;
                    lblArea.Font.Name = "Verdana";
                    lblArea.Font.Bold = true;
                    e.Cell.BackColor = System.Drawing.Color.Lavender;
                    e.Cell.Controls.Add(lblArea);
                }
                else
                {
                    lblArea.Font.Size = 7;
                    lblArea.Font.Name = "Verdana";
                    lblArea.Font.Bold = true;
                    lblArea.ForeColor = System.Drawing.Color.Red;

                    e.Cell.Controls.Add(lblArea);
                }
            }
        }


    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    protected void btntp_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/MR/TourPlan.aspx");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Report/TP_View_Report.aspx");
    }
    protected void btnTerr_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/MR/Territory/Territory.aspx");
    }
    protected void btnlisteddr_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx");
    }
    protected void btndcr_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        /*Response.Redirect("~/MasterFiles/MR/DCR/DCR_New.aspx");*/
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
    protected void btnNDCR_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
    protected void btndcrview_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Reports/DCR_View.aspx");
    }
    protected void btnmail_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Mails/Mail_Head.aspx");
    }
    protected void lnkreject_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Server.Transfer("~/MasterFiles/Rejection_ReEntries.aspx");

    }
    protected void lnkfile_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/MR/Usermanual_View.aspx");
    }
    protected void btnEx_entry_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        DataTable DCExp = new DataTable();
        Distance_calculation Exp = new Distance_calculation();
        DCExp = Exp.osCalcRwwisestpCnt(div_code);
        DataTable DCExp1 = new DataTable();
        DCExp1 = Exp.osCalcRwwisestpCnt1(div_code);
        DataTable DCExp2 = new DataTable();
        DCExp2 = Exp.anthemDirectAmt(div_code);
        DataTable DCExp3 = new DataTable();
        DCExp3 = Exp.OSEXNormalLogic(div_code);
        if ("1".Equals(DCExp2.Rows[0]["Anthem_direct_amt"].ToString()))
        {
            Response.Redirect("~/MasterFiles/MR/RptAutoExpense_RowWise_Anthem.aspx");
        }
        else if ("1".Equals(DCExp3.Rows[0]["Normal_Expense_Logic"].ToString()))
        {
            Response.Redirect("~/MasterFiles/MGR/RptAutoExpense_RowWise.aspx");
        }
        else
        {
            if ("1".Equals(DCExp1.Rows[0]["Row_wise_textbox"].ToString()) && "1".Equals(DCExp.Rows[0]["rwwise_calc"].ToString()))
            {
                Response.Redirect("~/MasterFiles/MR/RptAutoExpense_RowWise_Textbox.aspx");
            }
            else if ("1".Equals(DCExp.Rows[0]["rwwise_calc"].ToString()))
            {

                Response.Redirect("~/MasterFiles/MR/RptAutoExpense_RowWise.aspx");
            }
            else
            {
                Response.Redirect("~/MasterFiles/MR/RptAutoExpense_RowWise.aspx");
            }
        }


    }
    protected void btnEx_view_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Reports/RptAutoExpense_Approve_View.aspx");
    }
    protected void btnSS_entry_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/MR/SecSales/SecSalesEntry_New.aspx");
    }
    protected void btnSS_view_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Reports/SecSalesReport.aspx");
    }

    protected void btn_shrtcut_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/Default_MR.aspx");
    }
}