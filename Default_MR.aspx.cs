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

public partial class Default_MR : System.Web.UI.Page
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
        // menu1.FindControl("btnBack").Visible = false;
        //  menu1.FindControl("pnlQueries").Visible = true;
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sSf_Code = Session["sf_code"].ToString();
        sDiv_Code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            shrtct_div.Visible = true;
            chrt.Visible = false;
             //  btn_shrtcut.Text = "Show Chart";
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
            Session["backurl"] = "~/Default_MR.aspx";
            BindDropDown_Month_Year();
            //ModalPopupExtender2.Show();
            //if (div_code != "2")
            //{
            //    btnDCRView.Visible = false;

            //}

            //if (div_code == "8" || div_code == "9" || div_code == "10")
            //{
            //    btnDCR.Visible = false;
            //}
            if (div_code == "23"|| div_code == "104")
            {
                btnEx_entry.Visible = false;
            }
        }
    }
    protected void CalMgrDet_DayRender(object sender, DayRenderEventArgs e)
    {

        if (e.Day.IsOtherMonth)
        {
            e.Cell.Controls.Clear();
            e.Cell.Text = string.Empty;
        }
        else
        {

            DataSet dsDCRF = new DataSet();
            DataSet dsDCR = new DataSet();
            DCR dc = new DCR();
            dsDCRF = dc.getField_Days_Cal(sf_code, div_code, e.Day.Date.Day, e.Day.Date.Month, e.Day.Date.Year);
            if (dsDCRF.Tables[0].Rows.Count > 0)
            {
                if (dsDCRF.Tables[0].Rows[0][0].ToString() == "F")
                {
                    dsDCR = dc.Call_Det_Calendar(sf_code, e.Day.Date.Day, e.Day.Date.Month, e.Day.Date.Year);
                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        Label lblArea = new Label();
                        lblArea.Visible = true;
                        lblArea.Text = "<br>" + "D:" + dsDCR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "<br>" + "C:" + dsDCR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() +
                            "<br>" + "S:" + dsDCR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        //+ "," + "H:" + dsDCR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() +
                        //"," + "UL:" + dsDCR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        lblArea.Font.Size = 7;
                        lblArea.Font.Name = "Verdana";
                        lblArea.Font.Bold = true;
                        lblArea.ForeColor = System.Drawing.Color.Blue;

                        e.Cell.Controls.Add(lblArea);
                    }
                }
            }
            UnListedDR LstDR = new UnListedDR();
            TP_New tp = new TP_New();
            State st = new State();
            dsHoliday = LstDR.getState(sf_code);
            if (dsHoliday.Tables[0].Rows.Count > 0)
            {
                if (dsDCRF.Tables[0].Rows.Count == 0)
                {
                    state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dsTP = tp.getHolidays_TP(sf_code, e.Day.Date.Month, e.Day.Date.Day, e.Day.Date.Year, div_code, state_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        Label lbl = new Label();
                        lbl.Visible = true;
                        lbl.Text = "<br>" + "Holiday";
                        lbl.ToolTip = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        lbl.Font.Size = 7;
                        lbl.Font.Name = "Verdana";
                        lbl.Font.Bold = false;
                        //lbl.Enable = false;

                        e.Cell.Controls.Add(lbl);
                        e.Cell.BackColor = System.Drawing.Color.Yellow;
                        e.Cell.ForeColor = System.Drawing.Color.Green;


                    }
                    dsAdmin = st.getStateChkBox_WeekOff(state_code, div_code);
                    strWeek = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    strday = Convert.ToString(e.Day.Date.DayOfWeek);

                    if (dsAdmin.Tables[0].Rows.Count > 0)
                    {
                        if (strday == strWeek)
                        {
                            Label lbl = new Label();
                            lbl.Visible = true;
                            lbl.ForeColor = System.Drawing.Color.Blue;
                            lbl.Font.Size = 7;
                            lbl.Font.Name = "Verdana";
                            lbl.Text = "<br>" + "Week Off";
                            lbl.Font.Bold = false;
                            e.Cell.Controls.Add(lbl);
                            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD7D7");

                        }
                    }
                }
            }

        }


    }
    private void BindDropDown_Month_Year()
    {
        DataTable dtMnth = new DataTable();
        dtMnth.Columns.Add("Name");
        dtMnth.Columns.Add("Value");
        dtMnth.Rows.Add("Jan", "1");
        dtMnth.Rows.Add("Feb", "2");
        dtMnth.Rows.Add("Mar", "3");
        dtMnth.Rows.Add("Apr", "4");
        dtMnth.Rows.Add("May", "5");
        dtMnth.Rows.Add("Jun", "6");
        dtMnth.Rows.Add("Jul", "7");
        dtMnth.Rows.Add("Aug", "8");
        dtMnth.Rows.Add("Sep", "9");
        dtMnth.Rows.Add("Oct", "10");
        dtMnth.Rows.Add("Nov", "11");
        dtMnth.Rows.Add("Dec", "12");
        ddlChrtMn.DataSource = dtMnth;
        ddlChrtMn.DataValueField = "Value";
        ddlChrtMn.DataTextField = "Name";
        ddlChrtMn.DataBind();
        int mnth = DateTime.Now.Month;
        ddlChrtMn.SelectedIndex = mnth - 1;

        int yr = DateTime.Now.Year;
        List<string> lstYr = new List<string>();
        lstYr.Add((yr - 1).ToString());
        lstYr.Add((yr).ToString());
        lstYr.Add((yr + 1).ToString());
        ddlChrtYr.DataSource = lstYr;
        ddlChrtYr.DataBind();
        ddlChrtYr.SelectedIndex = 1;
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
    //
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal(List<string> lst_Input_Mn_Yr)
    {
        List<values> lstCoverage = new List<values>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Chart_Single_MR";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@year", pData[0]);
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code));
            cmd.Parameters.AddWithValue("@MSf_Code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
            cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            string cDate = "";
            if (lst_Input_Mn_Yr[0].ToString() == "12")
                cDate = "01-01-" + (Convert.ToInt32(lst_Input_Mn_Yr[1].ToString()) + 1).ToString();
            else
                cDate = (Convert.ToInt32(lst_Input_Mn_Yr[0]) + 1).ToString() + "-01-" + lst_Input_Mn_Yr[1].ToString();
            cmd.Parameters.AddWithValue("@cDate", cDate);
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.Sf_Code = dr["Sf_Code"].ToString();
                    cpData.Total_Drs = Convert.ToInt32(dr["ttl"].ToString());
                    cpData.Drs_Met = Convert.ToInt32(dr["met"].ToString());
                    cpData.Drs_Seen = Convert.ToInt32(dr["seen"].ToString());
                    cpData.Drs_Rpt = Convert.ToInt32(dr["rpt_mt"].ToString());
                    cpData.Drs_Rpt_sn = Convert.ToInt32(dr["rpt"].ToString());
                    cpData.FW_Dys = Convert.ToInt32(dr["fw_dy"].ToString());
                    cpData.Coverage = Convert.ToDecimal(dr["coverage"].ToString());
                    cpData.Rpt_Coverage = Convert.ToDecimal(dr["rpt_coverage"].ToString());
                    cpData.Call_Avg = Convert.ToDecimal(dr["call_avg"].ToString());
                    cpData.Jnt_Dys = Convert.ToInt32(dr["Jnt_Dys"].ToString());
                    cpData.Jnt_Mt = Convert.ToInt32(dr["Jnt_Mt"].ToString());
                    cpData.Jnt_Sn = Convert.ToInt32(dr["Jnt_Sn"].ToString());
                    cpData.Jnt_Call_Avg = Convert.ToDecimal(dr["Jnt_call_avg"].ToString());
                    cpData.Jnt_Coverage = Convert.ToDecimal(dr["Jnt_coverage"].ToString());
                    cpData.Rpt_Mt_coverage = Convert.ToDecimal(dr["rpt_mt_coverage"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    //    
    #region Button Show/Hide Shortcut & Chart Menu
    protected void btn_shrtcut_Click(object sender, EventArgs e)
    {
        if (btn_shrtcut.Text == "Show Shortcut Menus")
        {
            chrt.Visible = false;
            shrtct_div.Visible = true;
            btn_shrtcut.Text = "Show Chart";
            lblChrtMnth.Visible = false;
            lblHeadTxt.Text = "Shortcut Menus";
            ddlChrtMn.Visible = false;
            ddlChrtYr.Visible = false;
            btnViewChart.Visible = false;
            //bodyBg.Attributes.Remove("class");
        }
        else
        {
            chrt.Visible = true;
            shrtct_div.Visible = false;
            btn_shrtcut.Text = "Show Shortcut Menus";
            lblChrtMnth.Visible = true;
            lblHeadTxt.Text = "Dashboard";
            ddlChrtMn.Visible = true;
            ddlChrtYr.Visible = true;
            btnViewChart.Visible = true;
            //bodyBg.Attributes.Add("class", "bodyBgclr");
        }
    }
    #endregion
    //
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<productVal> getChartProduct(List<string> lst_Input_Mn_Yr)
    {
        List<productVal> lstCoverage = new List<productVal>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Chart_Single_MR_Prd";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@year", pData[0]);
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code));
            cmd.Parameters.AddWithValue("@MSf_Code", sSf_Code);
            cmd.Parameters.AddWithValue("@Mnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
            cmd.Parameters.AddWithValue("@Yr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            string cDate = "";
            if (lst_Input_Mn_Yr[0].ToString() == "12")
                cDate = "01-01-" + (Convert.ToInt32(lst_Input_Mn_Yr[1].ToString()) + 1).ToString();
            else
                cDate = (Convert.ToInt32(lst_Input_Mn_Yr[0]) + 1).ToString() + "-01-" + lst_Input_Mn_Yr[1].ToString();
            cmd.Parameters.AddWithValue("@cDate", cDate);
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    productVal cpData = new productVal();
                    cpData.prd_Code = dr["Product_Code_SlNo"].ToString();
                    cpData.prd_Name = dr["Product_Detail_Name"].ToString();
                    cpData.Ttl_Drs = Convert.ToInt32(dr["0_TtlDrs"].ToString());
                    cpData.Ttl_Mt = Convert.ToInt32(dr["1_Met"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }    
}
#region Class Values
public class values
{
    public string Sf_Code { get; set; }
    public int Total_Drs { get; set; }
    public int Drs_Met { get; set; }
    public int Drs_Seen { get; set; }
    public int Drs_Rpt { get; set; }
    public int FW_Dys { get; set; }
    public decimal Coverage { get; set; }
    public decimal Rpt_Coverage { get; set; }
    public decimal Call_Avg { get; set; }
    public int Jnt_Dys { get; set; }
    public int Jnt_Mt { get; set; }
    public int Jnt_Sn { get; set; }
    public decimal Jnt_Call_Avg { get; set; }
    public decimal Jnt_Coverage { get; set; }
    public decimal Rpt_Mt_coverage { get; set; }
    public int Drs_Rpt_sn { get; set; }
}
#endregion
//
#region Class productVal
public class productVal
{
    public string prd_Code { get; set; }
    public string prd_Name { get; set; }
    public int Ttl_Drs { get; set; }
    public int Ttl_Mt { get; set; }
}
#endregion
//