using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_MGR_Home : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsHoliday = new DataSet();
    DataSet dsTP = new DataSet();
    string strday = string.Empty;
    string strWeek = string.Empty;
    string state_code = string.Empty;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        //menu1.FindControl("pnlHeader").Visible = false;
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            AdminSetup adm1 = new AdminSetup();
            dsAdmin1 = adm1.Get_Flash_News(div_code);
            
            if (dsAdmin1.Tables[0].Rows.Count > 0)
            {
                
                lblFlash.Text = dsAdmin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lblFlash.Text = lblFlash.Text.Replace("asdf", "'");
            }
            CalMgr.TodaysDate = System.DateTime.Now;
            CalMgr.NextPrevFormat = NextPrevFormat.FullMonth;
            gettalk();
        }
        DataTable DCExp = new DataTable();
        Distance_calculation Exp = new Distance_calculation();
        DCExp = Exp.MGRstpCntMode(div_code, Session["Designation_Short_Name"].ToString());
        DataTable DCExp1 = new DataTable();
        DCExp1 = Exp.osCalcRwwisestpCnt1(div_code);
        if (div_code != "104")
        {
            if (DCExp.Rows.Count > 0)
            {
                if ("1".Equals(DCExp1.Rows[0]["Row_wise_textbox"].ToString()) && "A".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    btnmgrrwtxt.Visible = true;
                }
                else if ("A".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    btnTerr123.Visible = true;
                }
                else if ("SA".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    btnTerr456.Visible = true;
                }
                else
                {
                    btnTerr.Visible = true;
                }
            }
            else
            {
                btnTerr.Visible = true;
            }
        }
            //if (div_code != "2")
            //{
            //    btnDCRView.Visible = false;                
            //}

            //if (div_code == "8" || div_code == "9" || div_code == "10")
            //{
            //    btnDCR.Visible = false;
            //}
            
       

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
    protected void CalMgr_DayRender(object sender, DayRenderEventArgs e)
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
            dsDCR = dc.getDCR_Report_MGR_Calendar(sf_code, e.Day.Date.Day, e.Day.Date.Month, e.Day.Date.Year);
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
                    lblArea.ForeColor = System.Drawing.Color.BlueViolet;

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
            lblsup.Text = "<font color='Red'>Field Support No:-   07806914081</font> <br>Field Support Timings:- From <b>Monday to Saturday </b>(9.30 a.m to 6.00 p.m) <br> In field force login in Home Page find the 'Queries' link at Right top Corner. You can Post Your Queries/Errors/Modifications which Will be reSolved within 12 Hours.";
        }
    }
    
    protected void btntp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/MGR/TourPlan_Calen.aspx");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Report/TP_View_Report.aspx");
    }
    protected void btndcr_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
    protected void btndcrview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Reports/DCR_View.aspx");
    }
    protected void btnmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Mails/Mail_Head.aspx");
    }
    protected void lnkreject_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/Rejection_ReEntries.aspx");

    }
    protected void lnkfile_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/MR/Usermanual_View.aspx");
    }

    protected void btnmain_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default_MGR.aspx");
    }
    protected void btn_shrtcut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MGR_Dashboard.aspx");
    }
}