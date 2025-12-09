using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_MGR_TourPlan_Calen : System.Web.UI.Page
{

    Hashtable events = null;

    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsWeek = null;
    DataSet dssf = null;
    DataSet dsUserList = null;
    DataSet dswt = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = null;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr = string.Empty;
    string sf_type = string.Empty;
    string strIndex = string.Empty;
    bool TP_Submit = false;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;
    string sSelectedDate = string.Empty;
    DateTime dtSelectedDate;
    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    string strApporve = string.Empty;
    string strCase = string.Empty;

    string MR_Code = string.Empty;
    string MR_Month = string.Empty;
    string MR_Year = string.Empty;
    string sQryStr = string.Empty;
    string Edit = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sQryStr = Request.QueryString["refer"];
        Edit = Request.QueryString["Edit"];
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        strApporve = Session["sf_type"].ToString();

        if (sQryStr != null && sQryStr != "")
        {
            MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Code.Length) - 1);
            MR_Month = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Month.Length) - 1);
            MR_Year = sQryStr.Trim();
            sf_code = MR_Code;

            if (sQryStr.Length > 0)
            {
                //  btnSave.Visible = false;
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                btnReject.Visible = true;
                btnApprove.Visible = true;
                menu1.Title = "TP - Approval";

            }
        }

        UnListedDR LstDR = new UnListedDR();
        dsHoliday = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }




        //txtFieldForce.Text = "Please Wait....";
        if (!Page.IsPostBack)
        {
            AdminSetup dv = new AdminSetup();
            DataSet dsadmin = new DataSet();

            TP_New tp = new TP_New();


            if (sf_type != "3" && (Request.QueryString["Index"] != "A" && Request.QueryString["Index"] != "M"))
            {
                dsadmin = dv.getAdminSetup_MGR(div_code);

                string TpBased = dsadmin.Tables[0].Rows[0]["TpBased"].ToString();
                if (TpBased == "0")
                {
                    Designation Desig = new Designation();
                    dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);
                    string strTPApprovalSys = dsadmin.Tables[0].Rows[0]["tp_approval_Sys"].ToString();

                    if (strTPApprovalSys != "0")
                    {
                        SalesForce sf = new SalesForce();
                        //TP_New tp = new TP_New();

                        DataSet dsSalesForce = new DataSet();
                        dsTP = tp.get_TP_Active_Date_New(sf_code);
                        dsSalesForce = sf.SalesForceList_TP_StartingDt_Get(div_code, sf_code, dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2));
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                            {
                                DataSet dsStatus = new DataSet();
                                TP_New TP = new TP_New();
                                dsStatus = TP.get_TP_ApprovalStatus(drFF["sf_code"].ToString(), dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2), dsTP.Tables[0].Rows[0][0].ToString().Substring(6, 4));

                                if (dsStatus.Tables[0].Rows.Count != 0)
                                {
                                    string str1 = dsStatus.Tables[0].Rows[0][1].ToString();
                                    if (str1 != "Completed")
                                    {
                                        if (Request.QueryString["Index"] != null)
                                        {
                                            Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx?Index=8");
                                        }
                                        else
                                        {
                                            Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx");
                                        }
                                    }
                                }
                                else if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    if (Request.QueryString["Index"] != null)
                                    {
                                        Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx?sf_code=" + sf_code + "&Index=8");
                                    }
                                    else
                                    {
                                        Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx?sf_code=" + sf_code + "");
                                    }
                                }
                                //else
                                //{
                                //    if (Request.QueryString["Index"] != null)
                                //    {
                                //        Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx?sf_code=" + sf_code + "&Index=8");
                                //    }
                                //    else
                                //    {
                                //        Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx?sf_code=" + sf_code + "");
                                //    }
                                //}
                            }

                        }
                    }
                }
            }
            FillWorkType();
            FillSF();

            GetWorkName();

            //Session["backurl"] = "TP_Calendar.aspx";
            menu1.FindControl("btnBack").Visible = false;
            //menu1.Title = this.Page.Title;            


        }

        if (sf_type == "3")
        {
            menu1.Visible = false;
            //menu2.FindControl("btnBack").Visible = false;
        }
        else if (sf_type == "1" || sf_type == "2")
        {
            //menu2.Visible = false;
        }

        string strIndex = string.Empty;
        if (Request.QueryString["Index"] != null)
        {
            strIndex = Request.QueryString["Index"].ToString();
        }

        if (strIndex != "M" && strIndex != "A")
        {
            btnLogin.Visible = false;

            if (sf_type == "2")
            {
                menu1.Title = this.Page.Title;
                //menu2.Visible = false;
                //menu3.Visible = false;
                menu1.Visible = true;
            }
            else if (sf_type == "1")
            {
                //menu2.Title = this.Page.Title;
                menu1.Visible = false;
                //menu3.Visible = false;
            }
        }
        else
        {
            menu1.Visible = false;
            //menu2.Visible = false;
            //menu3.Visible = false;
        }


        if (Edit != null && Edit == "E")
        {
            FillTPEdit();
            //Label2.Visible = false;
            menu1.Title = "";
            Label2.Text = "<span style='font-size:11pt;color:Green;font-family:Verdana'>Tour Plan - Edit </span>" + "(<span style='font-size:11pt;color:Black'> Before Approval </span>)" + " for the month of " + "<span style='font-size:11pt;color:Green;font-family:Verdana'>" + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + dt_TP_Active_Date.Year + "</span>";

        }
        else if (sQryStr != null && sQryStr != "")
        {
            FillTPApprove();
            GetTitleApproval();
            Label2.Visible = true;

        }
        else
        {
            Label2.Visible = true;
            GetTitle();
            FillTPDate();

        }

        for (int i = 0; i < ddlWorkType.Items.Count; i++)
        {
            if (ddlWorkType.Items[i].Text == "Field Work")
            {
                ddlWorkType.Items[i].Attributes.Add("Class", "DropDown");
            }
        }

        Calendar1.DayHeaderStyle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E3E7EA");
        Calendar1.DayHeaderStyle.BorderStyle = BorderStyle.Solid;
        Calendar1.DayHeaderStyle.BorderWidth = 1;


    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    protected void GetTitleApproval()
    {
        TP_New tp = new TP_New();
        dsTP = tp.Get_TP_ApprovalTitle(MR_Code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {

            Label2.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " " +
                             "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";

            lblStatingDate.Visible = true;
            lblStatingDate.Text = "<span style='font-style:normal;color:Black'> Joining Date : </span><span style='font-style:normal;color:Black'>" + dsTP.Tables[0].Rows[0]["DOJ"] + "</span>";
        }
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;".ToString();
        }
    }
    protected void GetTitle()
    {

        TP_New tp = new TP_New();

        dsTP = tp.Get_TP_ApprovalTitle(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Label2.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " - " +
                            "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";


            DataSet dsTPStart = new DataSet();
            dsTPStart = tp.Get_TP_Start_Title(sf_code);
            if (dsTPStart.Tables[0].Rows.Count == 0)
            {
                Label2.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " - " +
                              "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";

                lblStatingDate.Visible = true;
                lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dsTP.Tables[0].Rows[0]["DOJ"] + "</span>";
            }


        }
    }

    private void FillTPDate()
    {
        TP_New tp = new TP_New();
        DataSet dsTPC = new DataSet();

        dsTP = tp.get_TP_Active_Date_New(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            dsTPC = tp.checkmonth_new(sf_code, Convert.ToString(dt_TP_Active_Date.Month));

            if (dsTPC.Tables[0].Rows.Count == 0)
            {
                Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
                Calendar1.VisibleDate = Calendar1.TodaysDate;
                GetTitle();
            }
            else
            {
                if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    Calendar1.Visible = false;
                    dsTP = tp.get_TP_Submission_Date_New(sf_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        Label2.Text = "<span style='color:#FF3300'>" + dt_TP_Active_Date.ToString("MMM") + " - " + dt_TP_Active_Date.Year + "</span>" +
                                       " TP not yet approved by your manager (" + "<span style='color:Green'>" + dsTP.Tables[1].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP.Tables[2].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP.Tables[3].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" + ")";

                        hylEdit.Text = "Yes";
                        lblLink.Text = "Before Approval by your Manager - Do you want to change your TP - ";
                        tblTitle.Style.Add("margin-top", "140px");

                        lblStatingDate.Visible = false;
                        btnDraftSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnClear.Visible = false;
                        menu1.FindControl("btnBack").Visible = false;
                    }
                }
                else if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                {
                    ViewState["Reject"] = "Yes";
                    FillTPEdit();
                    btnClear.Visible = false;
                    if (dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString() != "")
                    {
                        lblReason.Text = "Your TP has been Rejected " + dt_TP_Active_Date.ToString("MMMM") + " - " + dt_TP_Active_Date.Year + " Rejected <br> Reason: "
                                          + dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString();

                        Label2.Text = " Month Tour Plan - Entry For " + dt_TP_Active_Date.ToString("MMMM") + " - " + dt_TP_Active_Date.Year + " ( Resubmit for Rejection )";
                        lblNote.Visible = true;
                        lblReason.Visible = true;
                        btnDraftSave.Enabled = false;
                    }
                }
            }
        }
    }


    private void FillTPEdit()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Active_Edit(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
            Calendar1.VisibleDate = Calendar1.TodaysDate;
        }

    }
    private void FillTPApprove()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Approval(MR_Code, Convert.ToInt32(MR_Month), Convert.ToInt32(MR_Year));

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
            Calendar1.VisibleDate = Calendar1.TodaysDate;
        }

    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        //txtObjective.Text = "";
        Literal lineBreak = new Literal();
        lineBreak.Text = "<BR />";
        e.Cell.Controls.Add(lineBreak);

        e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E3E7EA");

        //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f3f3f3");        
        e.Cell.BorderStyle = BorderStyle.Solid;
        e.Cell.BorderWidth = 1;
        Label lblSpace = new Label();

        Label b = new Label();
        b.Font.Size = 10;
        b.Font.Bold = true;
        b.ForeColor = System.Drawing.Color.Maroon;
        e.Cell.Controls.Add(b);
        ViewState["seldate"] = Calendar1.SelectedDate.ToString("MM/dd/yyyy"); //e.Day.Date.ToString("MM/dd/yyyy");


        TP_New tp = new TP_New();
        TourPlan tpOld = new TourPlan();
        DateTime dtholiday = e.Day.Date;

        //dsTP = tp.get_TPCalender_Active_Date(sf_code);
        if (ViewState["dt_TP_Active_Date"].ToString() != "")
        {
            if (dtholiday.Date >= Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString()))
            {
                e.Day.IsSelectable = true;

                lblSpace.Visible = true;
                lblSpace.Text = "<br>" + " ";
                lblSpace.Font.Size = 7;
                lblSpace.Font.Name = "'Roboto', sans-serif";
                lblSpace.Font.Bold = false;
                e.Cell.Controls.Add(lblSpace);
            }
            else
            {
                e.Day.IsSelectable = false;
                e.Cell.Enabled = false;
                e.Cell.Style.Add("text-decoration", "line-through");
                lblSpace.Visible = true;
                lblSpace.Text = "<br>" + " ";
                lblSpace.Font.Size = 7;
                lblSpace.Font.Name = "'Roboto', sans-serif";
                lblSpace.Font.Bold = false;
                e.Cell.Controls.Add(lblSpace);
            }
        }
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.getHolidays_TP(MR_Code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
        }
        else
        {
            dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
        }
        string strHoliday = "";
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "<br>" + "Holiday" + "<br>";
            lbl.Font.Size = 7;
            lbl.Font.Name = "'Roboto', sans-serif";
            lbl.Font.Bold = false;
            //lbl.Enable = false;

            e.Cell.Controls.Add(lbl);
            strHoliday = "Holiday";
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD7D7");
            e.Cell.ForeColor = System.Drawing.Color.Green;
            //e.Day.IsSelectable = false;
        }

        string tp_worktype = string.Empty;
        string wt = string.Empty;
        string tp_schedule = string.Empty;
        string tp_ww = string.Empty;
        string tp_obj = string.Empty;
        string tp_cur_date = e.Day.Date.ToString("yyyy/MM/dd");

        string[] tp_cd;
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.get_TourPlan_Details(MR_Code, tp_cur_date);
        }
        else
        {
            dsTP = tp.get_TourPlan_Details(sf_code, tp_cur_date);
        }
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblSpace.Visible = false;

            wt = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            tp_schedule = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            tp_obj = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();


            dswt = tp.get_TourPlan_MGRWorkType(wt);
            if (dswt.Tables[0].Rows.Count > 0)
            {
                tp_worktype = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            tp_cd = tp_schedule.Split(',');
            foreach (string tpcode in tp_cd)
            {
                dsWeek = tp.get_sf_name_schedule(tpcode);
                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    tp_schedule = "";
                    foreach (DataRow dataRow in dsWeek.Tables[0].Rows)
                    {
                        tp_schedule = tp_schedule + dataRow["sf_name"].ToString() + ", ";
                    }
                }
            }

            tp_cd = tp_ww.Split(',');
            tp_ww = "";
            foreach (string tpcode in tp_cd)
            {
                dsWeek = tp.FetchSFName(tpcode);
                if (dsWeek != null)
                {
                    if (dsWeek.Tables[0].Rows.Count > 0)
                    {
                        if (tp_ww.Contains(dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()))
                        {
                        }
                        else
                        {
                            tp_ww = tp_ww + dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + ", ";
                        }
                    }
                }

                if (tpcode == "0")
                {
                    tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() + ", ";
                }
            }

            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Font.Size = 9;
            lbl.Font.Name = "'Roboto', sans-serif";
            lbl.Font.Bold = false;

            //if (tp_worktype == "Field Work" || tp_worktype == "Camp Work")
            //{
            lbl.Text = tp_worktype + ',' + "<br>";
            lbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#404040");
            //lbl.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
            //}
            //else
            //{
            //    lbl.Text = tp_worktype + ',' + "<br>";
            //    lbl.ForeColor = System.Drawing.Color.Black;
            //}

            e.Cell.Controls.Add(lbl);

            Label lblArea = new Label();
            lblArea.Visible = true;
            //lblArea.Text = "<BR> JointWork : ";
            lblArea.Font.Size = 7;
            lblArea.Font.Name = "'Roboto', sans-serif";
            lblArea.Font.Bold = false;
            lblArea.ForeColor = System.Drawing.Color.Black;

            e.Cell.Controls.Add(lblArea);

            Label lblAreaWW = new Label();
            lblAreaWW.Visible = true;
            lblAreaWW.Text = tp_ww + "<br>";
            lblAreaWW.Font.Size = 7;
            lblAreaWW.Font.Name = "'Roboto', sans-serif";
            lblAreaWW.Font.Bold = false;
            lblAreaWW.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblAreaWW);

            Label lblBrk = new Label();
            lblBrk.Visible = true;
            //lblBrk.Text = "<BR> Territory :";
            lblBrk.Font.Size = 7;
            lblBrk.Font.Name = "'Roboto', sans-serif";
            lblBrk.Font.Bold = false;
            lblBrk.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblBrk);

            Label lblsched = new Label();
            lblsched.Visible = true;
            if (tp_schedule != "Weekly OFF" && strHoliday != "Holiday")
            {
                lblsched.Text = tp_schedule;
            }
            lblsched.Font.Size = 7;
            lblsched.Font.Name = "'Roboto', sans-serif";
            lblsched.Font.Bold = false;
            lblsched.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblsched);
        }


        int iMonth = Convert.ToInt16(dt_TP_Active_Date.Month);
        int cMonth = e.Day.Date.Month;



        if (iMonth == cMonth)
        {
            //Do Nothing

            DateTime dtCurDate = Convert.ToDateTime(tp_cur_date);

            string strDay = dtCurDate.DayOfWeek.ToString();
            if (sQryStr != null && sQryStr != "")
            {
                dsWeek = tpOld.get_WeekOff(MR_Code);
            }
            else
            {
                dsWeek = tpOld.get_WeekOff(sf_code);
            }
            if (dsWeek.Tables[0].Rows.Count > 0)
            {
                string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                foreach (string strWeek in strSplitWeek)
                {
                    if (strWeek != "")
                    {
                        iWeek = Convert.ToInt32(strWeek);
                        if (strDay == getDays(iWeek))
                        {
                            Label lblArea = new Label();
                            lblArea.Visible = true;
                            lblArea.Text = "<br>" + "Weekly Off";
                            lblArea.Font.Size = 8;
                            lblArea.Font.Name = "'Roboto', sans-serif";
                            lblArea.Font.Bold = false;
                            lblArea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#404040");
                            e.Cell.Controls.Add(lblArea);
                            e.Cell.Style.Add("text-decoration", "none");

                            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            e.Cell.Style.Add("background-image", "../images/line-gradient.png");
                            e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#404040");
                        }
                    }
                }
            }
        }
        else
        {
            e.Cell.Text = "";
        }

    }


    private void FillSF()
    {
        if (txtFieldForce.Text.Trim().Length <= 0)
        {
            TP_New tp = new TP_New();
            SalesForce sf = new SalesForce();
            DataSet dsmgrsf = new DataSet();
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                DCR dc = new DCR();
                if (sQryStr != null && sQryStr != "")
                {
                    //dsTP = sf.UserList_getMR(div_code, MR_Code);

                    dsTP = dc.getTerrHQ_DCR(sf_code);

                }
                else
                {
                    //dsTP = sf.UserList_getMR(div_code, sf_code);
                    dsTP = dc.getTerrHQ_DCR(sf_code);

                }
            }
            else
            {
                // Fetch Managers Audit Team
                DataTable dt = sf.getAuditManagerTeam_GetMR(div_code, sf_code, 0);
                dsmgrsf.Tables.Add(dt);
                dsTP = dsmgrsf;
            }
            chkFieldForce.Items.Clear();

            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ListItem liTerr_Self = new ListItem();

                //liTerr_Self.Value = "0";
                //liTerr_Self.Text = "Independent";
                //chkFieldForce.Items.Add(liTerr_Self);



               // chkFieldForce.Items.Insert(0, new ListItem("Independent", "0"));

                foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                {
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dataRow["sf_code"].ToString();
                    liTerr.Text = dataRow["Name"].ToString();
                    //chkFieldForce.Items.Add(dataRow["sf_name"].ToString());
                    if (liTerr.Value != "0")
                    {
                        chkFieldForce.Items.Add(liTerr);

                    }
                }
            }
        }
    }

    private void FillWorkType()
    {
        TP_New tp = new TP_New();
        dsTP = tp.getWorkType_Select(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            ddlWorkType.DataTextField = "Worktype_Name_M";
            ddlWorkType.DataValueField = "WorkType_Code_M";
            ddlWorkType.DataSource = dsTP;
            ddlWorkType.DataBind();
            // ddlWorkType.SelectedIndex = 1;
        }

    }
    protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {

        TP_New tp = new TP_New();
        // Added by Sridevi - TO Clear

        txtFieldForce.Enabled = true;
        txtFieldForce.Focus();
        txtTerritory.Enabled = true;
        dsTP = (DataSet)ViewState["TPData"];
        if (dsTP.Tables[0].Rows.Count == 0)
        {
            txtFieldForce.Text = string.Empty;
            for (int i = 0; i < chkFieldForce.Items.Count; i++)
            {
                chkFieldForce.Items[i].Selected = false;
            }

            txtTerritory.Text = string.Empty;
            lblMRTerritory.Text = "";
            string name = string.Empty;
            string name1 = string.Empty;
            for (int i = 0; i < chkTerritory.Items.Count; i++)
            {
                chkTerritory.Items[i].Selected = false;
            }

        }
        //ENds
        dsTP = tp.FetchWorkCode_New(ddlWorkType.SelectedValue, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            if (dsTP.Tables[0].Rows[0][0].ToString() == "N")
            {
                txtFieldForce.Text = string.Empty;
                txtTerritory.Text = string.Empty;
                txtFieldForce.Enabled = false;
                txtTerritory.Enabled = false;
                txtFieldForce.Text = "";
                txtTerritory.Text = "";


            }
            else
            {
                txtFieldForce.Enabled = true;
                txtTerritory.Enabled = true;


            }
        }

    }
    protected void chkFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

        TP_New tp = new TP_New();
        lblMRTerritory.Text = "";
        txtTerritory.Focus();
        string name = string.Empty;
        string name1 = string.Empty;
        string[] terr;
        string tp_ww = string.Empty;
        chkTerritory.Items.Clear();

        ViewState["sf_Name"] = "";
        ViewState["sf_Code"] = "";
        for (int i = 0; i < chkFieldForce.Items.Count; i++)
        {
            if (chkFieldForce.Items[i].Selected)
            {
                SalesForce sf = new SalesForce();
                ViewState["sf_Name"] += chkFieldForce.Items[i].Text + ',';
                ViewState["sf_Code"] += chkFieldForce.Items[i].Value + ',';

                dssf = sf.CheckSFType(chkFieldForce.Items[i].Value.ToString());
                if (dssf.Tables[0].Rows.Count > 0)
                {
                    dsTP = tp.FetchTerritory_Chkbox(chkFieldForce.Items[i].Value.ToString());
                    string str = GetCaseColor(Convert.ToString(i));

                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                        {
                            ListItem liTerr = new ListItem();
                            liTerr.Value = dataRow["Territory_Code"].ToString();
                            liTerr.Text = dataRow["Territory_Name"].ToString();
                            chkTerritory.Items.Add(liTerr);

                            if (chkTerritory.Items.Contains(liTerr))
                            {

                            }
                            else
                            {
                                chkTerritory.Items.Add(liTerr);
                            }
                        }

                        for (int j = 0; j < chkTerritory.Items.Count; j++)
                        {
                            for (int k = 0; k < dsTP.Tables[0].Rows.Count; k++)
                            {
                                if (chkTerritory.Items[j].Text.Trim() == dsTP.Tables[0].Rows[k]["Territory_Name"].ToString().Trim())
                                {
                                    chkTerritory.Items[j].Attributes.Add("style", "Color:" + str + "");
                                }
                            }
                        }
                    }
                }


                dsUserList = tp.get_TP_MR(chkFieldForce.Items[i].Value.ToString(), ddlWorkType.SelectedItem.Text, Calendar1.SelectedDate.Date.ToString().Substring(0, 10));

                if (dsUserList.Tables[0].Rows.Count > 0)
                {
                    string[] strChk = chkFieldForce.Items[i].Text.Split('-');
                    //txtTerritory.Text = "";
                    lblMRTerritory.Text += "<span style='color:Blue;'>" + strChk[0] + "</span>" + " : " + dsUserList.Tables[0].Rows[0][0].ToString() + "</br>";
                    tp_ww = dsUserList.Tables[0].Rows[0][0].ToString();
                    name = txtTerritory.Text + ",";

                    for (int chkTerr = 0; chkTerr < chkTerritory.Items.Count; chkTerr++)
                    {

                        if (chkTerritory.Items[chkTerr].Text.Trim() == dsUserList.Tables[0].Rows[0][0].ToString().Trim())
                        {
                            chkTerritory.Items[chkTerr].Selected = true;
                            if (name.Contains(chkTerritory.Items[chkTerr].Text))
                            {
                                if (name.EndsWith(","))
                                {

                                }
                                else
                                {
                                    name = name + ",";
                                }
                            }
                            else
                            {
                                name += chkTerritory.Items[chkTerr].Text + ",";
                            }
                        }
                    }



                }
                else
                {
                    // lblMRTerritory.Text = "";
                    // BindSeletecedValue(sender, e);
                    if (chkFieldForce.Items[i].Text == "Independent")
                    {
                        dsTP = sf.SalesForceMgrGet_new(div_code, sf_code);

                        //DataTable uniqueTable = dsTP.Tables[0].DefaultView.ToTable(true, "sf_hq");
                        // dsTP.Tables[0].Rows[0].Delete();
                        dsTP.Tables[0].Rows.RemoveAt(0);
                        //dsTP.Tables[0].DefaultView.RowFilter = "  sf_type= '1' ";
                        DataTable dt = dsTP.Tables[0].DefaultView.ToTable("table1");
                        DataSet ds1 = new DataSet();
                        dsTP.Clear();
                        ds1.Merge(dt);
                        dsTP = ds1;
                        foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                        {
                            // dsTP.Tables[0].Rows[0].Delete();
                            ListItem liTerr = new ListItem();
                            liTerr.Value = dataRow["sf_hq"].ToString();
                            liTerr.Text = dataRow["sf_hq"].ToString();
                            chkTerritory.Items.Add(liTerr);
                        }
                    }

                }

                txtTerritory.Text = name;
                string strSplitChk = "";
                string[] strTerr = txtTerritory.Text.Split(',');
                foreach (string strSplit in strTerr)
                {
                    strSplitChk = strSplit.Trim();

                    for (int chkTerr1 = 0; chkTerr1 < chkTerritory.Items.Count; chkTerr1++)
                    {
                        if (chkTerritory.Items[chkTerr1].Text.Trim() == strSplitChk.Trim())
                        {
                            chkTerritory.Items[chkTerr1].Selected = true;
                            //name1 += chkTerritory.Items[chkTerr1].Text + ",";
                        }
                    }
                }


                txtTerritory.Text = "";
                for (int chkTerr1 = 0; chkTerr1 < chkTerritory.Items.Count; chkTerr1++)
                {
                    if (chkTerritory.Items[chkTerr1].Selected == true)
                    {
                        txtTerritory.Text += chkTerritory.Items[chkTerr1].Text + ",";
                    }
                }
                if (chkTerritory.Items.Count == 0)
                {
                    txtTerritory.Text = "";
                }

                string[] strtp_ww = tp_ww.Split(',');

                foreach (string terr_code1 in strtp_ww)
                {
                    string str = "";
                    dswt = tp.FetchTerritory_TourSchedule(terr_code1, chkFieldForce.Items[i].Value);
                    if (dswt.Tables[0].Rows.Count > 0)
                    {
                        str = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }


                    tp_ww = str;
                    if (tp_ww != "")
                    {
                        terr = tp_ww.Split(',');
                        tp_ww = "";
                        foreach (string terr_code in terr)
                        {
                            //tcode = tp.FetchSFCode(terr_code.Trim());
                            for (int j = 0; j < chkTerritory.Items.Count; j++)
                            {
                                if (terr_code == chkTerritory.Items[j].Value)
                                {
                                    chkTerritory.Items[j].Selected = true;
                                    name1 += chkTerritory.Items[j].Text + ",";
                                }
                            }

                            tp_ww = tp_ww + terr_code + ",";
                        }

                    }
                    txtTerritory.Text = name1;
                    txtTerritory.ToolTip = name1;
                }
            }

        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        btnApprove.Visible = false;
        btnReject.Visible = false;
        btnSubmit.Visible = false;
        btnSendBack.Visible = true;
        lblRejectReason.Visible = true;

        txtReason.Focus();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;

            if (Request.QueryString["Index"] != null)
            {
                strIndex = Request.QueryString["Index"].ToString();
            }

            iReturn = ApproveTP(Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString()));
            if (iReturn != -1)
            {
                if (sf_type == "3")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/TP_Calendar_Approve.aspx';</script>");
                }
                else if (strIndex == "A")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
                }
                else if (strIndex == "M")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/TP_Approve.aspx'</script>");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected int ApproveTP(DateTime Tpdate)
    {
        TP_New tp = new TP_New();
        bool chk = true;
        chk = CheckTP(1);
        if (chk)
        {
            iReturn = tp.ReadyforCalanderApproval(MR_Code, Tpdate.Month.ToString(), Tpdate.Year.ToString(), Tpdate.ToString(), Session["sf_name"].ToString());
            //tp.TP_Delete(MR_Code, Tpdate.Month.ToString(), Tpdate.Year.ToString());
        }

        return iReturn;
    }
    protected void btnSendBack_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            int icount = -1;
            int iReturn = -1;
            TP_New tp = new TP_New();
            iReturn = tp.Reject_New(MR_Code, MR_Month, MR_Year, txtReason.Text.Replace("'", "asdf"), Session["sf_name"].ToString());
            if (iReturn > 0)
            {
                using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = " select count (*) from tourplan_detail where sfcode='" + MR_Code + "' and mnth='" + MR_Month + "'" +
                                              " and yr='" + MR_Year + "' and div='" + Session["div_code"].ToString() + "'  ";
                        icount = (Int32)command.ExecuteScalar();
                        if (icount > 0)
                        {
                            command.CommandText = "Update tourplan_detail set Change_Status='2', Rejection_Reason='" + txtReason.Text + "' where SFCode='" + MR_Code + "'" +
                                                    " and Mnth='" + MR_Month + "' and Yr = '" + MR_Year + "' ";
                        }
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("Message: {0}", ex.Message);
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Eror Exist .Kindly resubmit again'); </script>");
                    }
                }
            }
            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Rejected Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
            }
        }
        else
        {
            txtReason.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        string TP_Date = ViewState["seldate"].ToString();
        string TP_Day = TP_Date.Substring(3, 2) + "-" + TP_Date.Substring(0, 2) + "-" + TP_Date.Substring(6, 4);
        string TP_Month = TP_Date.Substring(0, 2);
        int iReturn = -1;
        bool TP_Submit = true;
        string WorkType_Code = ddlWorkType.SelectedValue;
        string WorkType_Name = ddlWorkType.SelectedItem.Text;
        string TP_WW = txtFieldForce.Text; //txtSFCode.Text;
        string TP_WWName = string.Empty;
        string TP_Terr = txtTerritory.Text;// txtTerr.Text;
        string TP_Terr_Code = string.Empty;
        string TP_Objective = string.Empty; // txtObj.Text.ToString();
        string sDay = TP_Date.Substring(0, 2);

        string tcode = string.Empty;
        string tName = string.Empty;
        string[] terr;
        TP_New tp = new TP_New();



        if (TP_WW != "")
        {
            //iIndex = -1;
            terr = TP_WW.Split(',');
            TP_WW = "";


            TP_WW += ViewState["sf_Code"];
            TP_WWName += ViewState["sf_Name"];


            //if (TP_WWName.Contains("Independent"))
            //{
            //    TP_WW = TP_WW.Remove(TP_WW.Length - 1);
            //    TP_WWName = TP_WWName.Remove(TP_WWName.Length - 1);
            //}
            //else
            //{
            //    TP_WW = TP_WW.Remove(TP_WW.Length - 1);
            //    TP_WWName = TP_WWName.Remove(TP_WWName.Length - 1);
            //}
        }




        if (TP_Terr != "")
        {
            //iIndex = -1;
            //terr = TP_Terr.Split(',');
            //TP_Terr = "";
            //foreach (string terr_code in terr)
            //{
            //    tcode = tp.FetchTerritoryName(terr_code.Trim(), TP_WW);
            //    tName = tp.FetchTerritoryCode(terr_code.Trim(), TP_WW);
            //    TP_Terr += tcode + ",";
            //    TP_Terr_Code += tName + ",";
            //}

            ViewState["TerritoryName"] = "";
            ViewState["TerritoryCode"] = "";

            for (int i = 0; i < chkTerritory.Items.Count; i++)
            {
                if (chkTerritory.Items[i].Selected)
                {
                    SalesForce sf = new SalesForce();
                    ViewState["TerritoryName"] += chkTerritory.Items[i].Text + ',';
                    ViewState["TerritoryCode"] += chkTerritory.Items[i].Value + ',';
                }
            }
            TP_Terr = ViewState["TerritoryName"].ToString();
            TP_Terr_Code = ViewState["TerritoryCode"].ToString();

            TP_Terr = TP_Terr.Remove(TP_Terr.Length - 1);
            TP_Terr_Code = TP_Terr_Code.Remove(TP_Terr_Code.Length - 1);
        }

        if ((TP_Terr != "" || TP_Terr == ""))
        {
            // Add New Tour Plan
            TP_Objective = txtObjective.Text.Replace("'", "asdf").Trim();
            //if (TP_WWName == "Independent")
            //{
            //    TP_WW = "0";
            //}
            if (sQryStr != null && sQryStr != "")
            {
                iReturn = tp.RecordAddMGR_TP_New(TP_Day, TP_Terr != "" ? TP_Terr + "," : "", TP_WW, WorkType_Code, WorkType_Name, TP_Objective, TP_Submit, MR_Code, TP_Terr_Code != "" ? TP_Terr_Code + "," : "", TP_WWName, div_code);
            }
            else
            {
                iReturn = tp.RecordAddMGR_TP_New(TP_Day, TP_Terr != "" ? TP_Terr + "," : "", TP_WW, WorkType_Code, WorkType_Name, TP_Objective, TP_Submit, sf_code, TP_Terr_Code != "" ? TP_Terr_Code + "," : "", TP_WWName, div_code);
            }
            //Enable();
        }

        if (iReturn != -1)
        {

            //pnlpopup.Visible = false;
            pnlpopup.Style.Add("display", "none");
            pnlpopup.Style.Add("visibility", "hidden");

        }

    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        bool bHoliday = false;
        txtObjective.Text = "";
        TP_New tp = new TP_New();
        DateTime dtholiday = Convert.ToDateTime(Calendar1.SelectedDate.ToString());

        ViewState["SelectedDate"] = Calendar1.SelectedDate.ToString();
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.getHolidays_TP(MR_Code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
        }
        else
        {
            dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
        }
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            bHoliday = true;
        }
        pnlpopup.Style.Add("display", "block");
        pnlpopup.Style.Add("visibility", "visible");
        // pnlpopup.Visible = true;
        //Disable();
        if (!bHoliday)
        {
            ViewState["seldate"] = Calendar1.SelectedDate.ToString("MM/dd/yyyy");
            lblHead.Text = "Tour Plan Entry for " + Calendar1.SelectedDate.ToString("dd/MM/yyyy") + " - " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];

            txtFieldForce.Text = "";
            txtTerritory.Text = "";


            //Existing Records
            string tp_worktype = string.Empty;
            string wt = string.Empty;
            string tp_schedule = string.Empty;
            string tp_ww = string.Empty;
            string tp_obj = string.Empty;
            string name = "";
            string[] terr;
            string name1 = "";
            wt = "";
            //dsTP = tp.get_TP_Details(sf_code, ViewState["seldate"].ToString());
            if (sQryStr != null && sQryStr != "")
            {
                dsTP = tp.get_TourPlan_MGRDetails(MR_Code, ViewState["seldate"].ToString());
            }
            else
            {
                dsTP = tp.get_TourPlan_MGRDetails(sf_code, ViewState["seldate"].ToString());
            }
            ViewState["TPData"] = dsTP;
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                txtFieldForce.Enabled = true;
                txtTerritory.Enabled = true;

                wt = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                tp_schedule = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtObjective.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Replace("asdf", "'").Trim();


                string[] str_Schedule = tp_schedule.Split(',');

                foreach (string terr_code1 in str_Schedule)
                {
                    ddlWorkType.SelectedValue = wt;
                    dswt = tp.get_TourPlan_MGRSF_Name(terr_code1);
                    tp_worktype = "";
                    if (dswt.Tables[0].Rows.Count > 0)
                    {
                        tp_worktype = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else if (terr_code1 == "0")
                    {
                        tp_worktype = "0";
                    }
                    string tcode = string.Empty;

                    tp_schedule = tp_worktype;
                    if (tp_schedule != "")
                    {
                        //iIndex = -1;

                        terr = tp_schedule.Split(',');
                        tp_schedule = "";
                        foreach (string terr_code in terr)
                        {
                            tcode = tp.FetchSFCode(terr_code.Trim());
                            if (tcode == "")
                            {
                                tcode = "0";
                            }

                            for (int i = 0; i < chkFieldForce.Items.Count; i++)
                            {
                                if (tcode == chkFieldForce.Items[i].Value)
                                {
                                    chkFieldForce.Items[i].Selected = true;
                                    name += chkFieldForce.Items[i].Text + ",";
                                }
                            }

                            tp_schedule = tp_schedule + tcode + ",";
                        }
                    }
                }

                txtFieldForce.Text = name;
                txtFieldForce.ToolTip = name;
                chkFieldForce_SelectedIndexChanged(sender, e);


            }
            else
            {
                ddlWorkType.SelectedIndex = 0;
                ddlWorkType.Focus();
                txtFieldForce.Enabled = false;
                txtTerritory.Enabled = false;
            }
        }
    }

    protected void btnCloseFF_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + txtTerritory.ClientID + "').focus();", true);
        txtTerritory.Focus();
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    pnlpopup.Visible = false;
    //    //pnlCalendar.Enabled = true;
    //    //Enable();
    //}

    protected void btnDraftSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        TP_New tp = new TP_New();
        iReturn = tp.ReadyforCalanderDraft(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');window.location='TP_Calendar.aspx'</script>");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bCheck = true;
        Int32 iRecordExistTP = -1;

        //using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        //{
        //    connection.Open();
        //    SqlCommand command = connection.CreateCommand();
        //    SqlTransaction transaction;
        //    transaction = connection.BeginTransaction();
        //    command.Connection = connection;
        //    command.Transaction = transaction;
        //    try
        //    {
        //        command.CommandText = " select count (*) from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "' and mnth='" + Calendar1.TodaysDate.Month.ToString() + "'" +
        //                           " and yr='" + Calendar1.TodaysDate.Year.ToString() + "'    ";
        //        iRecordExistTP = (Int32)command.ExecuteScalar();
        //        if (iRecordExistTP == 0)
        //        {
        //            //latest update colum wise added
        //            command.CommandText =
        //                " insert into Tourplan_detail "+
        //                " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     "+
        //                " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
        //                " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
        //                " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
        //                " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
        //                " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
        //                " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
        //                " (select top 1 FieldWork_Indicator from Mas_WorkType_Mgr where WorkType_Code_M =a.worktype_code_b) FieldWork_Indicator,  '','',Worked_With_SF_Code,Worked_With_SF_Name,'','','','',getdate(),'Desktop' " +
        //                " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "'" +
        //                " and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' order by tour_date ";

        //            command.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            command.CommandText = "delete from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "'and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "' and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' ";
        //            command.ExecuteNonQuery();

        //            command.CommandText =
        //                " insert into Tourplan_detail "+
        //                                        " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     " +

        //                " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
        //                " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
        //                " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
        //                " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
        //                " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
        //                " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
        //                " (select top 1 FieldWork_Indicator from Mas_WorkType_Mgr where WorkType_Code_M =a.worktype_code_b) FieldWork_Indicator,  '','',Worked_With_SF_Code,Worked_With_SF_Name,'','','','',getdate(),'Desktop' " +
        //                " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "'" +
        //                " and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' order by tour_date ";

        //            command.ExecuteNonQuery();
        //        }

        //        transaction.Commit();
        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
        //        Console.WriteLine("Message: {0}", ex.Message);
        //        try
        //        {
        //            transaction.Rollback();
        //        }
        //        catch (Exception ex2)
        //        {
        //            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
        //            Console.WriteLine("  Message: {0}", ex2.Message);
        //        }
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Eror Exist .Kindly resubmit again'); </script>");
        //    }
        //}
        if (bCheck = CheckTP(0))
        {
            int iReturn = -1;

            TP_New tp = new TP_New();
            iReturn = tp.ReadyforApproval(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString(), Session["sf_name"].ToString());

            //-------- Admin - TP Approval System Based -----------
            //Designation Desig = new Designation();
            //DataSet dsadmin = new DataSet();

            //dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);

            //if (dsadmin.Tables[0].Rows.Count > 0)
            //{
            //    if (dsadmin.Tables[0].Rows[0]["Designation_Short_Name"].ToString() == Session["Designation_Short_Name"] && dsadmin.Tables[0].Rows[i]["tp_approval_Sys"].ToString() == "1")
            //    {
            //        iReturn = tp.ReadyforCalanderApproval(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString(), "", Session["sf_name"].ToString());
            //        tp.TP_Delete(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());
            //    }
            //}

            using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = " select count (*) from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "' and mnth='" + Calendar1.TodaysDate.Month.ToString() + "'" +
                                       " and yr='" + Calendar1.TodaysDate.Year.ToString() + "'    ";
                    iRecordExistTP = (Int32)command.ExecuteScalar();
                    if (iRecordExistTP == 0)
                    {
                        //latest update colum wise added
                        command.CommandText =
                            " insert into Tourplan_detail " +
                            " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     " +
                            " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
                            " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
                            " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
                            " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
                            " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
                            " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
                            " (select top 1 FieldWork_Indicator from Mas_WorkType_Mgr where WorkType_Code_M =a.worktype_code_b) FieldWork_Indicator,  '','',HQCodes,HQNames,'','','','',getdate(),'Desktop' " +
                            " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "'" +
                            " and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' order by tour_date ";

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = "delete from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "'and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "' and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' ";
                        command.ExecuteNonQuery();

                        command.CommandText =
                            " insert into Tourplan_detail " +
                                                    " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     " +

                            " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
                            " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
                            " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
                            " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
                            " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
                            " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
                            " (select top 1 FieldWork_Indicator from Mas_WorkType_Mgr where WorkType_Code_M =a.worktype_code_b) FieldWork_Indicator,  '','',HQCodes,HQNames,'','','','',getdate(),'Desktop' " +
                            " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + Calendar1.TodaysDate.Month.ToString() + "'" +
                            " and tour_year='" + Calendar1.TodaysDate.Year.ToString() + "' order by tour_date ";

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    connection.Close();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted Successfully');window.location='TourPlan_Calen.aspx'</script>");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("Message: {0}", ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Eror Exist .Kindly resubmit again'); </script>");
                }
            }






            //if (iReturn != -1)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted Successfully');window.location='TourPlan_Calen.aspx'</script>");
            //}
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Tour Plan for all the dates for this Month for approval');</script>");
        }
    }

    protected void btnClr_Click(object sender, EventArgs e)
    {
        TP_New tp = new TP_New();
        Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
        iReturn = tp.ClearTPCalender(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());

        if (iReturn != -1)
        {

        }
    }


    protected bool CheckTP(int mode)
    {
        bool bTourPlan = true;
        lblReason.Visible = false;
        Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));

        int iLastDay = LastDay_Month(Calendar1.TodaysDate.Month, Calendar1.TodaysDate.Year);
        for (i = 1; i <= iLastDay; i++)
        {
            bool bHoliday = false;
            string cDate = string.Empty;

            TP_New tp = new TP_New();
            string sDate = i.ToString() + "-" + Calendar1.TodaysDate.Month.ToString() + "-" + Calendar1.TodaysDate.Year.ToString();

            DateTime dtholiday = Convert.ToDateTime(sDate);

            if (dtholiday >= Convert.ToDateTime(ViewState["dt_TP_Active_Date"]))
            {
                if (sQryStr != null && sQryStr != "")
                {
                    dsTP = tp.getHolidays_TP(MR_Code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
                }
                else
                {
                    dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year, div_code, state_code);
                }
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    bHoliday = true;
                    DataSet dsWeekly = new DataSet();
                    DataSet dsholiday = new DataSet();
                    if (mode == 0)
                    {
                        dsWeekly = tp.GetTPHolidayData(sf_code, div_code, sDate);
                        dsholiday = tp.getholidaycode(div_code);
                        if (dsWeekly.Tables[0].Rows.Count == 0)
                        {
                            tp.RecordAddMGR_TPWeekOff(sDate, TP_Terr, "", dsholiday.Tables[0].Rows[0]["WorkType_Code_M"].ToString(), dsTP.Tables[0].Rows[0]["Holiday_Name"].ToString().Replace("'", "asdf"), TP_Objective, TP_Submit, sf_code, "", "");
                        }
                    }

                }

                string strDay = dtholiday.DayOfWeek.ToString();
                if (sQryStr != null && sQryStr != "")
                {
                    dsWeek = tp.get_WeekOff(MR_Code);
                }
                else
                {
                    dsWeek = tp.get_WeekOff(sf_code);
                }

                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                    foreach (string strWeek in strSplitWeek)
                    {
                        if (strWeek != "")
                        {
                            iWeek = Convert.ToInt32(strWeek);
                            if (strDay == getDays(iWeek))
                            {
                                bHoliday = true;
                                if (Convert.ToDateTime(sDate) >= Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString()))
                                {
                                    DataSet dsWeekly = new DataSet();
                                    DataSet dsWeeklyoff = new DataSet();
                                    if (mode == 0)
                                    {
                                        dsWeekly = tp.GetTPHolidayData(sf_code, div_code, sDate);
                                        dsWeeklyoff = tp.getweekoffcode(div_code);
                                        if (dsWeekly.Tables[0].Rows.Count == 0)
                                        {

                                            tp.RecordAddMGR_TPWeekOff(sDate, TP_Terr, "", dsWeeklyoff.Tables[0].Rows[0]["WorkType_Code_M"].ToString(), "Weekly OFF", TP_Objective, TP_Submit, sf_code, "", "");
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
            }


            DataSet dsTPCount = new DataSet();

            if (!bHoliday)
            {
                cDate = dtholiday.ToString("MM/dd/yyyy");
                if (Convert.ToDateTime(dtholiday.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString()))
                {
                    if (sQryStr != null && sQryStr != "")
                    {
                        dsTPCount = tp.get_TP_Count_one(MR_Code, cDate);
                    }
                    else
                    {
                        dsTPCount = tp.get_TP_Count_one(sf_code, cDate);
                    }


                    if (dsTPCount.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        bTourPlan = false;
                    }
                }
            }
        }
        return bTourPlan;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect(Session["backurl"].ToString());
    }

    private string getMonth(int iMonth)
    {
        string sMonth = string.Empty;

        if (iMonth == 1)
        {
            sMonth = "January";
        }
        else if (iMonth == 2)
        {
            sMonth = "Febraury";
        }
        else if (iMonth == 3)
        {
            sMonth = "March";
        }
        else if (iMonth == 4)
        {
            sMonth = "April";
        }
        else if (iMonth == 5)
        {
            sMonth = "May";
        }
        else if (iMonth == 6)
        {
            sMonth = "June";
        }
        else if (iMonth == 7)
        {
            sMonth = "July";
        }
        else if (iMonth == 8)
        {
            sMonth = "August";
        }
        else if (iMonth == 9)
        {
            sMonth = "September";
        }
        else if (iMonth == 10)
        {
            sMonth = "October";
        }
        else if (iMonth == 11)
        {
            sMonth = "November";
        }
        else if (iMonth == 12)
        {
            sMonth = "December";
        }
        return sMonth;
    }

    private string getDays(int iDay)
    {
        string sWeek = string.Empty;

        if (iDay == 0)
        {
            sWeek = "Sunday";
        }
        else if (iDay == 1)
        {
            sWeek = "Monday";
        }
        else if (iDay == 2)
        {
            sWeek = "Tuesday";
        }
        else if (iDay == 3)
        {
            sWeek = "Wednesday";
        }
        else if (iDay == 4)
        {
            sWeek = "Thursday";
        }
        else if (iDay == 5)
        {
            sWeek = "Friday";
        }
        else if (iDay == 6)
        {
            sWeek = "Saturday";
        }

        return sWeek;
    }
    private int LastDay_Month(int iMonth, int iYear)
    {
        int iReturn = -1;

        if (iMonth == 1)
        {
            iReturn = 31;
        }
        else if (iMonth == 2)
        {
            iReturn = 28;
        }
        else if (iMonth == 3)
        {
            iReturn = 31;
        }
        else if (iMonth == 4)
        {
            iReturn = 30;
        }
        else if (iMonth == 5)
        {
            iReturn = 31;
        }
        else if (iMonth == 6)
        {
            iReturn = 30;
        }
        else if (iMonth == 7)
        {
            iReturn = 31;
        }
        else if (iMonth == 8)
        {
            iReturn = 31;
        }
        else if (iMonth == 9)
        {
            iReturn = 30;
        }
        else if (iMonth == 10)
        {
            iReturn = 31;
        }
        else if (iMonth == 11)
        {
            iReturn = 30;
        }
        else if (iMonth == 12)
        {
            iReturn = 31;
        }

        return iReturn;
    }


    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#0066FF";
                break;
            case "2":
                strCase = "#CC6600";
                break;
            case "3":
                strCase = "#246B24";
                break;
            case "4":
                strCase = "#FF9999";
                break;
            case "5":
                strCase = "#999966";
                break;
            case "6":
                strCase = "#B84D4D";
                break;
            case "7":
                strCase = "#CC9900";
                break;

            default:
                strCase = "#387399";
                break;
        }
        return strCase;
    }

    private void Disable()
    {
        pnlCalendar.Enabled = false;
        pnlCalendar.CssClass = "modalBackground";
        pnlHidden.CssClass = "modalBackground";
        btnDraftSave.Enabled = false;
        btnSubmit.Enabled = false;
        btnClear.Enabled = false;

        UserControl_MGR_Menu UControl = (UserControl_MGR_Menu)Page.FindControl("Menu");
    }

    private void Enable()
    {
        pnlCalendar.Enabled = true;
        btnDraftSave.Enabled = true;
        btnSubmit.Enabled = true;
        btnClear.Enabled = true;
        Server.Transfer("TP_Calendar.aspx");
    }
}