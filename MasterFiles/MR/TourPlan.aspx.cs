using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_MR_TourPlan : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsTP2 = null;
    DataSet dsTPC = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = new DataSet();
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr_Value = string.Empty;
    string TP_Terr_Name = string.Empty;
    string strddlWT = string.Empty;
    string strddlFWText = string.Empty;
    string TP_Terr1_Value = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2_Value = string.Empty;
    string TP_Terr2_Name = string.Empty;
    bool TP_Submit = false;
    bool EmptyWT = false;
    bool EmptyTerr = false;
    string ddlWT = string.Empty;
    string ddlWT1 = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    string strTPView = string.Empty;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;
    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    DataSet dsWeekoff = null;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_type = string.Empty;
    string strIndex = string.Empty;
    string MR_Code = string.Empty;
    string MR_Month = string.Empty;
    string MR_Year = string.Empty;
    string sQryStr = string.Empty;
    string Edit = string.Empty;
    string StrMonth = string.Empty;
    string ID = string.Empty;
    DataSet dsWorkTypeSettings = null;
    int tp_count;
    DataSet dsplan = null;
    DataSet dstpplan = null;
    DataSet dsApptpplan = null;
    DataSet dsTPClock = null;
    int session_tp;
    int cust_need;
    int doc_need;
    int chem_need;
    int stk_need;
    int cip_need;
    int hos_need;
    string MR_Codetp = string.Empty;
    string MR_Monthtp = string.Empty;
    string MR_Yeartp = string.Empty;
    string sQryStrtp = string.Empty;
    string MR_CodetpApp = string.Empty;
    string MR_MonthtApp = string.Empty;
    string MR_YeartpApp = string.Empty;
    string sQryStrApptp = string.Empty;
    int tpAppcount;
    string sf = string.Empty;
    string name = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        sQryStr = Request.QueryString["refer"];
        Edit = Request.QueryString["Edit"];
        sQryStrtp = Request.QueryString["refer"];

        TourPlan tplan = new TourPlan();
        if (Session["sf_type"].ToString() == "1")
        { sf = Session["sf_code"].ToString(); }
        else if (Session["sf_type"].ToString() == "2")
        { sf = sQryStrtp.Substring(0, sQryStrtp.IndexOf('-')); }

        dsplan = tplan.getTplockStatus(sf, Session["div_code"].ToString());
        if (dsplan.Tables[0].Rows.Count > 0)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                tp_count = Convert.ToInt32(dsplan.Tables[0].Rows[0][0]);
                if (tp_count == 1) { tppnl.Visible = false; tpmsg.Visible = true; }
                else { tppnl.Visible = true; tpmsg.Visible = false; }
            }
        }

        if (Session["sf_type"].ToString() == "2")
        {
            MR_Codetp = sQryStrtp.Substring(0, sQryStrtp.IndexOf('-'));
            sQryStrtp = sQryStrtp.Substring(sQryStrtp.IndexOf('-') + 1, (sQryStrtp.Length - MR_Codetp.Length) - 1);
            MR_Monthtp = sQryStrtp.Substring(0, sQryStrtp.IndexOf('-'));
            sQryStrtp = sQryStrtp.Substring(sQryStrtp.IndexOf('-') + 1, (sQryStrtp.Length - MR_Monthtp.Length) - 1);
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonth = mfi.GetMonthName(Convert.ToInt16(MR_Monthtp)).ToString().Substring(0, 3);
            //string name = Request.QueryString["sf_name"].ToString();
            MR_Yeartp = sQryStrtp.Trim();


            strQry = " select a.Sf_Name +' - ' +sf_Designation_Short_Name+' - '+a.Sf_HQ as sf_name " +
" from Mas_Salesforce a   where a.Sf_Code='" + MR_Codetp + "'";

            dsTPClock = db_ER.Exec_DataSet(strQry);

            if (dsTPClock.Tables[0].Rows.Count > 0)
            {
                name = dsTPClock.Tables[0].Rows[0]["sf_name"].ToString();
            }

            dsApptpplan = tplan.getAppTpsetupStatus(Session["div_code"].ToString(), sf, MR_Monthtp, MR_Yeartp);

            dsplan = tplan.getAppTpsetupStatus_checkDesk(Session["div_code"].ToString(), MR_Codetp, MR_Monthtp, MR_Yeartp);
            Response.Redirect("TP_Lock.aspx?name=" + name + "&sfcode=" + MR_Codetp + "&month=" + strFMonth + "&year=" + MR_Yeartp + "&sftype=" + Session["sf_type"].ToString() + "&Mnth=" + MR_Monthtp);
            //if (dsApptpplan.Tables[0].Rows.Count > 0)
            if (dsplan.Tables[0].Rows.Count > 0)
            {
                //tpAppcount = Convert.ToInt32(dsApptpplan.Tables[0].Rows[0][0]);
                tpAppcount = Convert.ToInt32(dsplan.Tables[0].Rows[0][0]);
                if (tpAppcount > 0)
                {
                    dstpplan = tplan.getTpsetupStatus(Session["div_code"].ToString());
                    if (dstpplan.Tables[0].Rows.Count > 0)
                    {
                        session_tp = Convert.ToInt32(dstpplan.Tables[0].Rows[0][0]);
                        cust_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][1]);
                        doc_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][2]);
                        chem_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][3]);
                        stk_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][4]);
                        cip_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][5]);
                        hos_need = Convert.ToInt32(dstpplan.Tables[0].Rows[0][6]);

                        if (session_tp == 0 || cust_need == 0 || doc_need == 0 || chem_need == 0 || stk_need == 0 || cip_need == 0 || hos_need == 0)
                        { Response.Redirect("TP_Lock.aspx?name=" + name + "&sfcode=" + MR_Codetp + "&month=" + strFMonth + "&year=" + MR_Yeartp + "&sftype=" + Session["sf_type"].ToString() + "&Mnth=" + MR_Monthtp); }
                        else { tppnl.Visible = true; tpmsg.Visible = false; }
                    }
                }
                else { tppnl.Visible = true; tpmsg.Visible = false; }
            }
            else { tppnl.Visible = true; tpmsg.Visible = false; }
        }


        if (sQryStr != null && sQryStr != "")
        {
            MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Code.Length) - 1);
            MR_Month = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Month.Length) - 1);
            MR_Year = sQryStr.Trim();

            if (sQryStr.Length > 0)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                btnReject.Visible = true;
                btnApprove.Visible = true;
                Page.Title = "TP - Approval";
                //menu1.Visible = false;
                //menu2.Visible = false;
            }
        }

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        lblStatingDate.Visible = false;
        if (!Page.IsPostBack)
        {
            TP_New tp = new TP_New();
            dsWorkTypeSettings = tp.Tp_get_WorkType(div_code);
            if (dsWorkTypeSettings.Tables[0].Rows.Count > 0)
            {
                grdWorkType.DataSource = dsWorkTypeSettings;
                grdWorkType.DataBind();
                grdWorkType.Attributes.Add("style", "display:none");
            }
            LoadLstDocTerr();
            GetWeekOff();
            getHoliday_Wtype();
            getWeekOff_Div();
            GetSF_State();
            ViewState["Reject"] = "";


            if (Request.QueryString["Index"] != null)
            {
                strIndex = Request.QueryString["Index"].ToString();
            }



            if (strIndex != "M" && strIndex != "A")
            {
                btnLogin.Visible = false;
                btnBack1.Visible = false;

                if (sf_type == "2")
                {
                    //menu1.Title = this.Page.Title;
                    //menu2.Visible = false;
                    //menu3.Visible = false;
                    //menu1.Visible = true;
                }
                else if (sf_type == "1")
                {
                    //menu2.Title = this.Page.Title;
                    //menu1.Visible = false;
                    //menu3.Visible = false;
                }
            }
            else
            {
                btnBack.Visible = false;
                //menu1.Visible = false;
                //menu2.Visible = false;
                //menu3.Visible = false;
            }

            if (Edit != null && Edit == "E")
            {
                FillTPEdit();
                lblHead.Visible = false;
                //menu2.Title = "";
                lblmon.Text = "Tour Plan - Edit " + "(<span style='font-size:11pt;color:red;font-family:Verdana'> Before Approval </span>)" + " for the month of " + "<span style='font-size:11pt;color:red;font-family:Verdana'>" + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + dt_TP_Active_Date.Year + "</span>";
                lblFieldForce.Text = "FieldForce Name: " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            }
            else if (sQryStr != null && sQryStr != "")
            {
                FillTPApprove();
                GetTitleApproval();
                lblFieldForce.Visible = false;
            }
            else
            {
                lblHead.Visible = true;
                GetTitle();
                FillTPDate();
                lblFieldForce.Visible = false;
            }
            TourPlanTerritory();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
        FillColor();
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
    protected void TourPlanTerritory()
    {
        try
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                strTPView = dsTerritory.Tables[0].Rows[0]["No_of_TP_View"].ToString();
                if (strTPView == "3")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = true;
                    grdTP.Columns[8].Visible = true;
                }
                else if (strTPView == "2")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "1")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "0" || strTPView == "")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;

                }
            }
            else
            {
                grdTP.Columns[6].Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void GetTitle()
    {
        TP_New tp = new TP_New();

        dsTP = tp.Get_TP_ApprovalTitle(sf_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:red;'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "" +
                            " - " + dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>";
        }
    }

    protected void GetTitleApproval()
    {
        TP_New tp = new TP_New();
        dsTP = tp.Get_TP_ApprovalTitle(MR_Code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + " - " +
                            dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>";

            DataSet dsTPStart = new DataSet();
            dsTPStart = tp.Get_TP_Start_Title(MR_Code);
            if (dsTPStart.Tables[0].Rows.Count == 0)
            {
                lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + " - " +
                    dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>" + " " +
                              "<span style='color:red'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";
                lblmon.Visible = false;

            }
            lblStatingDate.Visible = true;
            lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dsTP.Tables[0].Rows[0]["DOJ"] + "</span>";
        }
    }
    protected void GetWeekOff()
    {
        TP_New tp = new TP_New();
        dsWeekoff = tp.get_WeekOff_Divcode(div_code);
        if (dsWeekoff.Tables[0].Rows.Count > 0)
            ViewState["WeekOff_Wtype_Code"] = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
    }

    protected void LoadLstDocTerr()
    {
        //TP_New tp = new TP_New();
        Territory tp = new Territory();
        DataSet ds = null;
        if (sQryStr != null && sQryStr != "")
        {
            //ds = tp.GetTPWorkTypeFieldWork(MR_Code);
            ds = tp.getTerritory_Transfer(MR_Code);
        }
        else
        {
            ds = tp.getTerritory_Transfer(sf_code);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAllTerr.DataTextField = "Territory_Name";
            ddlAllTerr.DataValueField = "Territory_Code";
            ddlAllTerr.DataSource = ds;
            ddlAllTerr.DataBind();
            ViewState["DocTerrLst"] = ds;
        }
    }
    protected void GetSF_State()
    {
        UnListedDR LstDR = new UnListedDR();
        if (sQryStr != null && sQryStr != "")
        {
            dsHoliday = LstDR.getState(MR_Code);
        }
        else
        {
            dsHoliday = LstDR.getState(sf_code);
        }
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        ViewState["state_code"] = state_code;
    }

    protected void getHoliday_Wtype()
    {
        TP_New tp = new TP_New();
        dsWeekoff = tp.get_Holiday_DivCode(div_code);
        if (dsWeekoff.Tables[0].Rows.Count > 0)
            ViewState["Hol_Wtype_Code"] = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
    }
    protected void getWeekOff_Div()
    {
        TP_New tp = new TP_New();
        TourPlan tpOld = new TourPlan();
        if (sQryStr != null && sQryStr != "")
        {
            dsWeek = tpOld.get_WeekOff(MR_Code);
        }
        else
        {
            dsWeek = tpOld.get_WeekOff(sf_code);
        }
        if (dsWeek.Tables[0].Rows.Count > 0)
            ViewState["Div_Week_Off"] = dsWeek.Tables[0].Rows[0]["WeekOff"].ToString();
    }
    private void FillTPDate2()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Active_Date_New(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            dsTPC = tp.checkmonth_new(sf_code, Convert.ToString(dt_TP_Active_Date.Month));

            if (dsTPC.Tables[0].Rows.Count == 0)
            {
                lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
                lblmon.Text = " - " + lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));
                DataSet dsTPStart = new DataSet();
                dsTPStart = tp.Get_TP_Start_Title(sf_code);
                if (dsTPStart.Tables[0].Rows.Count == 0)
                {
                    //lblmon.Text = lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year))+ "<span style='font-style:normal'> ( Joining Date " + dt_TP_Active_Date.ToString("dd/MM/yyyy")+" )" + "</span>";
                    dsTP = tp.Get_TP_ApprovalTitle(sf_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        lblStatingDate.Visible = true;
                        lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dsTP.Tables[0].Rows[0]["DOJ"] + "</span>";
                    }
                }
                dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    grdTP.Visible = true;
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }
            }
            else
            {
                if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                    dsTP = tp.get_TP_Submission_Date_New(sf_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        lblHead.Text = "Your " + "<span style='color:red'>" + dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" +
                                       " TP not yet approved by your manager (" + "<span style='color:red'>" + dsTP.Tables[1].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP.Tables[2].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP.Tables[3].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" + ")";
                        hylEdit.Text = "Yes";
                        lblLink.Text = "Before Approval by your Manager - Do you want to change your TP - ";
                        tblMargin.Style.Add("margin-top", "140px");

                    }
                    else
                    {
                        grdTP.Visible = true;
                        grdTP.DataSource = dsTP;
                        grdTP.DataBind();
                    }
                }
                else if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                {
                    ViewState["Reject"] = "Yes";
                    FillTPEdit();
                    if (dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString() != "")
                    {
                        lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year);
                        lblReason.Text = "Your TP has been Rejected  " + lblmon.Text + "<br> Rejected Reason: "
                                            + dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString();
                        lblmon.Text = "";
                        lblmon.Text = " - " + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year) + "<span style='color:#636d73'> ( Resubmit for Rejection ) </span>";
                        lblNote.Visible = true;
                        lblReason.Visible = true;
                    }
                }
            }
        }
    }
    private void FillTPDate()
    {
        TP_New tp = new TP_New();
        dsTP = tp.get_TP_Active_Date_New(sf_code);
        TourPlan tplan = new TourPlan();

        dsTP2 = tp.get_TP_Submission_Date_New(Session["sf_code"].ToString());
        if (dsTP2.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            dsplan = tplan.getAppTpsetupStatus_checkDesk(Session["div_code"].ToString(), Session["sf_code"].ToString(), Convert.ToString(dt_TP_Active_Date.Month), Convert.ToString(dt_TP_Active_Date.Year));

            if (dsTP2.Tables[0].Rows.Count > 0)
            {
                if (dsplan.Tables[0].Rows.Count > 0)
                {
                    if (dsplan.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "0")
                    {
                        lblHead.Text = "Your " + "<span style='color:#e60000'>" + dsTP2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" +
                         " TP not yet approved by your manager  (" + "<span style='color:#00802b'>" + dsTP2.Tables[1].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP2.Tables[2].Rows[0].ItemArray.GetValue(0).ToString() + " - " + dsTP2.Tables[3].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" + ")";

                        lblLink.Text = "TP has done on mobile App, Not possible to change your TP through web...!!!";
                        tblMargin.Style.Add("margin-top", "140px");
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        FillTPDate2();
                    }
                }
            }
        }
        else
        {
            FillTPDate2();
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
            //lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
            //lblmon.Text = lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));
        }
        dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }
        else
        {
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
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
        }

        DataSet dsTPC = new DataSet();

        dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
        //lblmon.Text =
        lblmon.Text = " - " + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " - " + Convert.ToString(dt_TP_Active_Date.Year);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }
        else
        {

        }

    }

    protected void btnLnkbtn(object sender, EventArgs e)
    {
        Response.Redirect("Default2.aspx?Month=" + dt_TP_Active_Date.Month + "&Year=" + dt_TP_Active_Date.Year + "");
    }

    protected void GrdTP_ddlWTSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        // int idx = row.RowIndex;
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void Dropdownvalue(DropDownList ddlWT, DropDownList ddlTerr)
    {
        TP_New tpRPDisable = new TP_New();
        DataSet dsRPDisable = new DataSet();
        if (ddlWT.SelectedIndex > 0)
        {
            if (ddlWT.SelectedItem.Text == "Field Work")
            {
                dsTP = (DataSet)ViewState["DocTerrLst"];
            }
            else
            {
                dsTP = (DataSet)ViewState["TerrList"];
            }
            ddlTerr.DataSource = dsTP;
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataBind();

            dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
            if (dsRPDisable.Tables[0].Rows.Count > 0)
            {
                ddlTerr.Enabled = false;
                ddlTerr.BackColor = System.Drawing.Color.LightGray;
                ddlTerr.ToolTip = "Disabled!!";
            }
            else
            {
                ddlTerr.Enabled = true;
                ddlTerr.ToolTip = "Enabled";
                ddlTerr.BackColor = System.Drawing.Color.White;
            }

        }
        else
        {
            ddlTerr.Enabled = false;
            ddlTerr.ToolTip = "Disabled!!";
            ddlTerr.BackColor = System.Drawing.Color.LightGray;
        }
    }



    protected void GrdTP1_ddlWT1SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT1");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr1");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void GrdTP2_ddlWT1SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        //GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT2");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr2");
        Dropdownvalue(ddlWT, ddlTerr);
    }


    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();

        dt_TP_Active_Date = Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString());

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            Label lblDay = (Label)e.Row.FindControl("lblDay");
            DropDownList ddlWT = (DropDownList)e.Row.FindControl("ddlWT");
            DropDownList ddlWT1 = (DropDownList)e.Row.FindControl("ddlWT1");
            DropDownList ddlWT2 = (DropDownList)e.Row.FindControl("ddlWT2");
            DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
            DropDownList ddlTerr1 = (DropDownList)e.Row.FindControl("ddlTerr1");
            DropDownList ddlTerr2 = (DropDownList)e.Row.FindControl("ddlTerr2");
            TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");

            if (lblSNo != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                dt_TP_Current_Date = dt_TP_Active_Date.AddDays(Convert.ToInt32(lblSNo.Text) - 1);
                lblDay.Text = dt_TP_Current_Date.DayOfWeek.ToString();
                lblDate.Text = dt_TP_Current_Date.ToString("dd/MM/yyyy");

                dsHoliday = tp.getHolidays(state_code, div_code, lblDate.Text);
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    lblDay.BackColor = System.Drawing.Color.LightGreen;
                    lblSNo.BackColor = System.Drawing.Color.LightGreen;
                    lblDate.BackColor = System.Drawing.Color.LightGreen;

                    //ddlWT.Enabled = false;
                    //ddlWT1.Enabled = false;
                    //ddlWT.Enabled = false;
                    ddlTerr.Enabled = false;
                    ddlTerr1.Enabled = false;
                    ddlTerr2.Enabled = false;

                    ddlWT.SelectedValue = ViewState["Hol_Wtype_Code"].ToString();
                    txtObjective.Enabled = false;
                    txtObjective.Text = Convert.ToString(dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Replace("asdf", ",")).Trim();
                    ddlTerr.SelectedValue = "0";
                    ddlTerr1.SelectedValue = "0";
                    ddlTerr2.SelectedValue = "0";
                }
                TourPlan tpOld = new TourPlan();
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
                    if (ViewState["Div_Week_Off"].ToString() != "")
                    {
                        string[] strSplitWeek = ViewState["Div_Week_Off"].ToString().Split(',');
                        foreach (string strWeek in strSplitWeek)
                        {
                            if (strWeek != "")
                            {
                                iWeek = Convert.ToInt32(strWeek);
                                if (lblDay.Text.Trim() == getDays(iWeek))
                                {
                                    lblDay.BackColor = System.Drawing.Color.LightPink;
                                    lblSNo.BackColor = System.Drawing.Color.LightPink;
                                    lblDate.BackColor = System.Drawing.Color.LightPink;
                                    //ddlWT.Enabled = false;
                                    //ddlWT1.Enabled = false;
                                    //ddlWT2.Enabled = false;
                                    ddlTerr.Enabled = false;
                                    ddlTerr1.Enabled = false;
                                    ddlTerr2.Enabled = false;
                                    txtObjective.Enabled = false;
                                    txtObjective.Text = "Weekly Off";
                                    ddlWT.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                                    ddlWT1.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                                    ddlWT2.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }

                // Saved but not submitted
                if (Edit != null && Edit == "E")
                {
                    dsTP = tp.get_TP_Details_New(sf_code, lblDate.Text);
                }
                else if (sQryStr != null && sQryStr != "")
                {
                    dsTP = tp.get_TP_Details_Approve_New(MR_Code, lblDate.Text);
                }
                else if (ViewState["Reject"].ToString() == "Yes")
                {
                    dsTP = tp.get_TP_Reject(sf_code, lblDate.Text);
                }
                else
                {
                    dsTP = tp.get_TP_Draft_New(sf_code, lblDate.Text);
                }
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    txtObjective.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Replace("asdf", "'").Trim();

                    ddlWT.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                    ddlWT1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                    ddlWT2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());

                    ddlTerr.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                    ddlTerr1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                    ddlTerr2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());

                    DataSet dsActiveFlag = new DataSet();
                    Territory terr = new Territory();

                    dsTerritory = terr.getWorkAreaName(div_code);



                    dsActiveFlag = tp.FetchTerritory_Active_Flag(MR_Code, ddlTerr.SelectedValue);
                    if (dsActiveFlag.Tables[0].Rows.Count > 0)
                    {
                        if (dsActiveFlag.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            for (int i = 0; i < ddlTerr.Items.Count; i++)
                            {
                                if (ddlTerr.Items[i].Value == ddlTerr.SelectedValue)
                                {
                                    ddlTerr.Items[i].Attributes.CssStyle.Add("color", "red");
                                    ddlTerr.ToolTip = "DeActivated!!";
                                    lblDeactivate.Visible = true;
                                    lblDeactivate.Text = "'Red Color indicates' Deleted " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Before Preparing the TP Kindly Modify/Delete the " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Reject the TP and submit once again for Approval.";
                                }
                            }
                            //dropdownlist1.selectedItem.Attribute.add("Style", "color:red");
                        }
                    }



                }



                TP_New tpRPDisable = new TP_New();
                DataSet dsRPDisable = new DataSet();

                if (ddlWT.SelectedIndex > 0)
                {
                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr.Enabled = false;
                        ddlTerr.BackColor = System.Drawing.Color.White;
                        ddlTerr.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr.Enabled = true;
                        ddlTerr.ToolTip = "Select";
                        ddlTerr.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr.Enabled = false;
                    ddlTerr.ToolTip = "Disabled!!";
                    ddlTerr.BackColor = System.Drawing.Color.White;
                }
                if (ddlWT1.SelectedIndex > 0)
                {
                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT1.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr1.Enabled = false;
                        ddlTerr1.BackColor = System.Drawing.Color.White;
                        ddlTerr1.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr1.Enabled = true;
                        ddlTerr1.ToolTip = "Select";
                        ddlTerr1.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr1.Enabled = false;
                    ddlTerr1.ToolTip = "Disabled!!";
                    ddlTerr1.BackColor = System.Drawing.Color.White;
                }
                if (ddlWT2.SelectedIndex > 0)
                {
                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT2.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr2.Enabled = false;
                        ddlTerr2.BackColor = System.Drawing.Color.White;
                        ddlTerr2.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr2.Enabled = true;
                        ddlTerr2.ToolTip = "Select";
                        ddlTerr2.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr2.Enabled = false;
                    ddlTerr2.BackColor = System.Drawing.Color.White;
                    ddlTerr2.ToolTip = "Disabled!!";
                }

                DataSet dsTPConfirmed = null;
                dsTPConfirmed = tp.GetTPConfirmed_Date(dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), sf_code);
                string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                if (strConfirmed != "0")
                {
                    DataSet dsTPEdit = new DataSet();
                    dsTPEdit = tp.GetTPEdit(sf_code, dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), lblDate.Text + ",");
                    if (dsTPEdit.Tables[0].Rows.Count > 0)
                    {
                        ddlWT.Enabled = true;
                        ddlWT1.Enabled = true;
                        ddlWT2.Enabled = true;
                        ddlTerr.Enabled = true;
                        ddlTerr1.Enabled = true;
                        ddlTerr2.Enabled = true;
                        txtObjective.Enabled = true;

                        lblDay.BackColor = System.Drawing.Color.Orange;
                        lblSNo.BackColor = System.Drawing.Color.LightSteelBlue;
                        lblDate.BackColor = System.Drawing.Color.LightSteelBlue;
                    }
                    else
                    {
                        ddlWT.Enabled = false;
                        ddlWT1.Enabled = false;
                        ddlWT2.Enabled = false;
                        ddlTerr.Enabled = false;
                        ddlTerr1.Enabled = false;
                        ddlTerr2.Enabled = false;
                        txtObjective.Enabled = false;
                    }
                }


            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();




            }
            DataSet dsTerrHead = new DataSet();
            dsTerrHead = terr.getWorkAreaName(div_code);
            if (dsTerrHead.Tables[0].Rows.Count > 0)
            {
                strTPView = dsTerrHead.Tables[0].Rows[0]["No_of_TP_View"].ToString();
                if (strTPView == "2")
                {
                    e.Row.Cells[4].Text = "Morning" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                    e.Row.Cells[6].Text = "Evening" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                }
            }
        }
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

    protected DataSet FillTerritory()
    {
        TP_New tp = new TP_New();
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.FetchTerritory(MR_Code);
        }
        else
        {
            dsTP = tp.FetchTerritory(sf_code);
        }
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to TP creation');window.location='../../MasterFiles/MR/Territory/TerritoryCreation.aspx';</script>");
        }
        ViewState["TerrList"] = dsTP;
        return dsTP;
    }
    protected DataSet FillWorkType()
    {
        TP_New tp = new TP_New();
        dsTP = tp.FetchWorkType_New(div_code);

        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Worktype must be loaded');</script>");
        }
        return dsTP;
    }
    protected void FillColor()
    {
        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            DropDownList ddlWT = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
            for (int i = 0; i < ddlWT.Items.Count; i++)
            {
                if (ddlWT.Items[i].Text == "Field Work")
                {
                    ddlWT.Items[i].Attributes.Add("Class", "DropDown");
                }
            }
            DropDownList ddlWT1 = (DropDownList)gridRow.Cells[3].FindControl("ddlWT1");
            for (int i = 0; i < ddlWT1.Items.Count; i++)
            {
                if (ddlWT1.Items[i].Text == "Field Work")
                {
                    ddlWT1.Items[i].Attributes.Add("Class", "DropDown");
                }
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        TP_Submit = false;

        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            iReturn = CreateTP(TP_Submit);
            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');</script>");
                if ((Edit != null && Edit == "E") || (ViewState["Reject"].ToString() == "Yes"))
                {
                    FillTPEdit();
                }
                else
                {
                    FillTPDate();
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        if (Request.QueryString["Index"] != null)
        {
            strIndex = Request.QueryString["Index"].ToString();
        }

        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            iReturn = ApproveTP();
            if (iReturn != -1)
            {
                //if (strIndex == "8")
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/TPMGRApproval.aspx'</script>");
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
                //}

                if (sf_type == "3")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='/BasicMaster.aspx';</script>");
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
    }
    protected int ApproveTP()
    {
        TP_New tp = new TP_New();

        Int32 iReturn = -1;
        Int32 iRecordExistTP = -1;
        Int32 idivision_code = -1;
        Int32 iRecordExist = -1;
        //int iReturn = -1;
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
                foreach (GridViewRow gridRow in grdTP.Rows)
                {
                    Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
                    TP_Date = lblDate.Text.ToString();
                    Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
                    TP_Day = lblDay.Text.ToString();

                    DropDownList ddlWork_Type = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
                    ddlWT = ddlWork_Type.SelectedValue.ToString();
                    if (ddlWT == "0")
                    {
                        ddlWT1 = "0";
                    }
                    else
                    {
                        ddlWT1 = ddlWork_Type.SelectedItem.Text;
                    }

                    DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT1");
                    ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();

                    if (ddlValueWT1 == "0")
                    {
                        ddlTextWT1 = "0";
                    }
                    else
                    {
                        ddlTextWT1 = ddlWork_Type1.SelectedItem.Text;
                    }

                    DropDownList ddlWork_Type2 = (DropDownList)gridRow.Cells[7].FindControl("ddlWT2");
                    ddlValueWT2 = ddlWork_Type2.SelectedValue.ToString();

                    if (ddlValueWT2 == "0")
                    {
                        ddlTextWT2 = "0";
                    }
                    else
                    {
                        ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;
                    }

                    DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
                    TP_Terr_Value = ddlTerritory_Type.SelectedValue.ToString() != "0" ? ddlTerritory_Type.SelectedValue.ToString() + "," : "0";

                    if (TP_Terr_Value == "0")
                    {
                        TP_Terr_Name = "0";
                    }
                    else
                    {
                        TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text != "0" ? ddlTerritory_Type.SelectedItem.Text + "," : "0";
                    }

                    DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr1");
                    TP_Terr1_Value = ddlTerritory_Type1.SelectedValue.ToString() != "0" ? ddlTerritory_Type1.SelectedValue.ToString() + "," : "0";

                    if (TP_Terr1_Value == "0")
                    {
                        TP_Terr1_Name = "0";
                    }
                    else
                    {
                        TP_Terr1_Name = ddlTerritory_Type1.SelectedItem.Text != "0" ? ddlTerritory_Type1.SelectedItem.Text + "," : "0";
                    }

                    DropDownList ddlTerritory_Type2 = (DropDownList)gridRow.Cells[6].FindControl("ddlTerr2");
                    TP_Terr2_Value = ddlTerritory_Type2.SelectedValue.ToString() != "0" ? ddlTerritory_Type2.SelectedValue.ToString() + "," : "0";

                    if (TP_Terr2_Value == "0")
                    {
                        TP_Terr2_Name = "0";
                    }
                    else
                    {
                        TP_Terr2_Name = ddlTerritory_Type2.SelectedItem.Text != "0" ? ddlTerritory_Type2.SelectedItem.Text + "," : "0";
                    }

                    TextBox txtObjective = (TextBox)gridRow.Cells[4].FindControl("txtObjective");

                    //TP_Objective = txtObjective.Text.ToString();

                    TP_Objective = txtObjective.Text.ToString().Replace("'", "asdf");
                    DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                    string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                    command.CommandText = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + MR_Code + "'";
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    idivision_code = Convert.ToInt32(dt.Rows[0][0]);
                    if (TP_Submit == false)
                    {
                        command.CommandText = " select count(Tour_Month) from Trans_TP_One " +
                             " where SF_Code='" + MR_Code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                        iRecordExist = (Int32)command.ExecuteScalar();
                        if (iRecordExist == 0)
                        {
                            command.CommandText = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                " VALUES('" + MR_Code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                " '" + TP_Terr_Name + "','" + TP_Terr1_Name + "','" + TP_Terr2_Name + "','" + TP_Objective + "', 'Tour Schedule', " +
                                " '" + ddlWT + "','" + ddlWT1 + "','" + idivision_code + "', 0, getdate(),'" + TP_Terr_Value + "','" + TP_Terr1_Value + "','" + TP_Terr2_Value + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + Session["sf_name"].ToString() + "') ";
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command.CommandText = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule', " +
                                 " Tour_Schedule1='" + TP_Terr_Name + "',Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "', " +
                                 " WorkType_Code_B = '" + ddlWT + "',Worktype_Name_B='" + ddlWT1 + "', Territory_Code1='" + TP_Terr_Value + "',Territory_Code2='" + TP_Terr1_Value + "'," +
                                 " Territory_Code3='" + TP_Terr2_Value + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "', " +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + Session["sf_name"].ToString() + "' " +
                                 " where SF_Code='" + MR_Code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                            command.ExecuteNonQuery();
                        }
                    }
                    else if (TP_Submit == true)
                    {
                        command.CommandText = " select count(Tour_Month) from Trans_TP_One " +
                             " where SF_Code='" + MR_Code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                        iRecordExist = (Int32)command.ExecuteScalar();
                        if (iRecordExist == 0)
                        {
                            command.CommandText = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                 " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                 " VALUES('" + MR_Code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                 " '" + TP_Terr_Name + "','" + TP_Terr1_Name + "','" + TP_Terr2_Name + "','" + TP_Objective + "', 'Tour Schedule', " +
                                 " '" + ddlWT + "','" + ddlWT1 + "','" + idivision_code + "', 1, getdate(),'" + TP_Terr_Value + "'," +
                                 "'" + TP_Terr1_Value + "','" + TP_Terr2_Value + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + Session["sf_name"].ToString() + "') ";
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command.CommandText = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1, " +
                                 " Tour_Schedule1='" + TP_Terr_Name + "',Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "', " +
                                 " WorkType_Code_B = '" + ddlWT + "',Worktype_Name_B='" + ddlWT1 + "',Territory_Code1='" + TP_Terr_Value + "',Territory_Code2='" + TP_Terr1_Value + "'," +
                                 " Territory_Code3='" + TP_Terr2_Value + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + Session["sf_name"].ToString() + "' " +
                                 " where SF_Code='" + MR_Code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                            command.ExecuteNonQuery();
                        }
                    }
                    //iReturn = tp.RecordAdd_New(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT1, TP_Objective, TP_Submit, MR_Code, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());

                    ////iReturn = tp.Approve_New(MR_Code, MR_Month, MR_Year, Session["sf_code"].ToString(), div_code, "", TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, "", ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());
                    command.CommandText = " select count(mnth) from Tourplan_detail " +
                            " where SFCode='" + MR_Code + "' and mnth=" + dt_TourPlan.Month + " and " +
                            " tpdt =  '" + strTPdt + "' and yr=" + dt_TourPlan.Year + " and Div= '" + idivision_code + "' ";
                    iRecordExistTP = (Int32)command.ExecuteScalar();

                    if (iRecordExistTP > 0)
                    {
                        command.CommandText = " update Tourplan_detail set Change_Status=3" +
                                   " where sfcode='" + MR_Code + "' and mnth='" + dt_TourPlan.Month + "' and yr='" + dt_TourPlan.Year + "' " +
                                   " and div='" + idivision_code + "' and TPDt='" + strTPdt + "'";
                        command.ExecuteNonQuery();
                        iReturn = 1;
                    }
                }
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

        iReturn = 1;
        if (iReturn != -1)
        //if (iReturn > 0)
        {
            int iretapp = tp.TP_Confirm(MR_Code, MR_Month, MR_Year);
            //int iretdel = tp.TP_Delete(MR_Code, MR_Month, MR_Year);
        }
        return iReturn;
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        grdTP.Enabled = false;
        btnApprove.Visible = false;
        btnSave.Visible = false;
        btnReject.Visible = false;
        btnSubmit.Visible = false;
        btnSendBack.Visible = true;
        lblRejectReason.Visible = true;

        txtReason.Focus();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        TP_New tp = new TP_New();
        DateTime dt_TourPlan = Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString());
        int iret = tp.TP_Delete(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString());
        if (iret >= 0)
            FillTPDate();
    }
    protected void btnSendBack_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            int iReturn = -1;
            int icount = -1;
            TP_New tp = new TP_New();

            txtReason.Text = txtReason.Text.ToString().Replace("'", "asdf");

            iReturn = tp.Reject_New(MR_Code, MR_Month, MR_Year, txtReason.Text, Session["sf_name"].ToString());
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            lblNote.Visible = false;
            lblReason.Visible = false;
            int iReturn = -1;
            TP_Submit = true;

            foreach (GridViewRow gridRow in grdTP.Rows)
            {
                DropDownList ddlWT = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
                strddlWT = ddlWT.SelectedValue.ToString();
                strddlFWText = ddlWT.SelectedItem.Text.ToString();
                DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
                TP_Terr_Value = ddlTerritory_Type.SelectedItem.Text.ToString();
                Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
                TP_Day = lblDay.Text.ToString();

                if (strddlFWText == "---Select---")
                {
                    EmptyTerr = true;
                }
            }

            if (EmptyWT == false && EmptyTerr == false)
            {
                iReturn = CreateTP(TP_Submit);
                if (iReturn != -1)
                {
                    // menu1.Status = "TourPlan Submitted for Approval!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted for Approval');window.location='TourPlan.aspx'</script>");
                    grdTP.Visible = false;
                    btnSubmit.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    lblFieldForce.Visible = false;
                }
            }
            else
            {
                if (EmptyWT == true)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type for all the dates');</script>");
                }
                if (EmptyTerr == true)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type. ');</script>");
                }
            }
        }
    }

    private int CreateTP(bool TP_Submit)
    {
        TP_New tp = new TP_New();
        bool isAutoApprove = false;
        DataSet dsadmin = new DataSet();
        Int32 iReturn = -1;
        Int32 idivision_code = -1;
        Int32 iRecordExist = -1;
        Int32 iRecordExistTP = -1;
        Int32 imonth = -1;
        Int32 iyear = -1;
        //int iReturn = -1;
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
                Designation Desig = new Designation();
                dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);

                string strSession = Session["Designation_Short_Name"].ToString();
                if (dsadmin.Tables[0].Rows.Count > 0)
                {
                    if (dsadmin.Tables[0].Rows[0]["Designation_Short_Name"].ToString() == strSession && dsadmin.Tables[0].Rows[0]["tp_approval_Sys"].ToString() == "1")
                        isAutoApprove = true;
                }

                foreach (GridViewRow gridRow in grdTP.Rows)
                {
                    Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
                    TP_Date = lblDate.Text.ToString();
                    Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
                    TP_Day = lblDay.Text.ToString();

                    DropDownList ddlWork_Type = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
                    ddlWT = ddlWork_Type.SelectedValue.ToString();
                    if (ddlWT == "0")
                    {
                        ddlWT1 = "0";
                    }
                    else
                    {
                        ddlWT1 = ddlWork_Type.SelectedItem.Text;
                    }

                    DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT1");
                    ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();


                    if (ddlValueWT1 == "0")
                    {
                        ddlTextWT1 = "0";
                    }
                    else
                    {
                        ddlTextWT1 = ddlWork_Type1.SelectedItem.Text;
                    }

                    DropDownList ddlWork_Type2 = (DropDownList)gridRow.Cells[7].FindControl("ddlWT2");
                    ddlValueWT2 = ddlWork_Type2.SelectedValue.ToString();


                    if (ddlValueWT2 == "0")
                    {
                        ddlTextWT2 = "0";
                    }
                    else
                    {
                        ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;
                    }

                    DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
                    TP_Terr_Value = ddlTerritory_Type.SelectedValue.ToString() != "0" ? ddlTerritory_Type.SelectedValue.ToString() + "," : "0";


                    if (TP_Terr_Value == "0")
                    {
                        TP_Terr_Name = "0";
                    }
                    else
                    {
                        TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text != "0" ? ddlTerritory_Type.SelectedItem.Text + "," : "0";
                    }

                    DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr1");
                    TP_Terr1_Value = ddlTerritory_Type1.SelectedValue.ToString() != "0" ? ddlTerritory_Type1.SelectedValue.ToString() + "," : "0";

                    if (TP_Terr1_Value == "0")
                    {
                        TP_Terr1_Name = "0";
                    }
                    else
                    {
                        TP_Terr1_Name = ddlTerritory_Type1.SelectedItem.Text != "0" ? ddlTerritory_Type1.SelectedItem.Text + "," : "0";
                    }

                    DropDownList ddlTerritory_Type2 = (DropDownList)gridRow.Cells[6].FindControl("ddlTerr2");
                    TP_Terr2_Value = ddlTerritory_Type2.SelectedValue.ToString() != "0" ? ddlTerritory_Type2.SelectedValue.ToString() + "," : "0";

                    if (TP_Terr2_Value == "0")
                    {
                        TP_Terr2_Name = "0";
                    }
                    else
                    {
                        TP_Terr2_Name = ddlTerritory_Type2.SelectedItem.Text != "0" ? ddlTerritory_Type2.SelectedItem.Text + "," : "0";
                    }

                    TextBox txtObjective = (TextBox)gridRow.Cells[4].FindControl("txtObjective");

                    //TP_Objective = txtObjective.Text.ToString();

                    TP_Objective = txtObjective.Text.ToString().Replace("'", "asdf");

                    DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                    string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                    imonth = dt_TourPlan.Month;
                    iyear = dt_TourPlan.Year;
                    command.CommandText = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + Session["sf_code"].ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    idivision_code = Convert.ToInt32(dt.Rows[0][0]);
                    string HQNamevalues = Session["sf_hq"].ToString() + " ( " + Session["sf_name"].ToString() + ")";
                    if (TP_Submit == false)
                    {
                        command.CommandText = "select count(Tour_Month) from Trans_TP_One where SF_Code='" + Session["sf_code"].ToString() + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                        iRecordExist = (Int32)command.ExecuteScalar();
                        if (iRecordExist == 0)
                        {
                            command.CommandText = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                " VALUES('" + Session["sf_code"].ToString() + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                " '" + TP_Terr_Name + "','" + TP_Terr1_Name + "','" + TP_Terr2_Name + "','" + TP_Objective + "', 'Tour Schedule', " +
                                " '" + ddlWT + "','" + ddlWT1 + "','" + idivision_code + "', 0, getdate(),'" + TP_Terr_Value + "','" + TP_Terr1_Value + "','" + TP_Terr2_Value + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + Session["sf_name"].ToString() + "') ";
                            command.ExecuteNonQuery();
                            //iReturn = 1;

                            command.CommandText = " Update Trans_TP_One set HQCodes='" + Session["sf_code"].ToString() + "',HQNames='" + HQNamevalues + "' where " +
                               " sf_code = '" + Session["sf_code"].ToString() + "' and Tour_Month = '" + dt_TourPlan.Month + "' and Tour_Year = '" + dt_TourPlan.Year + "' " +
                               " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' and Worktype_Name_B = 'Field Work' ";
                            command.ExecuteNonQuery();

                        }
                        else
                        {
                            command.CommandText = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule', " +
                                 " Tour_Schedule1='" + TP_Terr_Name + "',Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "', " +
                                 " WorkType_Code_B = '" + ddlWT + "',Worktype_Name_B='" + ddlWT1 + "', Territory_Code1='" + TP_Terr_Value + "',Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "', " +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + Session["sf_name"].ToString() + "' " +
                                 " where SF_Code='" + Session["sf_code"].ToString() + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                            command.ExecuteNonQuery();
                            //iReturn = 1;
                        }
                    }
                    else if (TP_Submit == true)
                    {

                        command.CommandText = " select count(Tour_Month) from Trans_TP_One " +
                             " where SF_Code='" + Session["sf_code"].ToString() + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                        iRecordExist = (Int32)command.ExecuteScalar();
                        if (iRecordExist == 0)
                        {
                            command.CommandText = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                 " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                 " VALUES('" + Session["sf_code"].ToString() + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                 " '" + TP_Terr_Name + "','" + TP_Terr1_Name + "','" + TP_Terr2_Name + "','" + TP_Objective + "', 'Tour Schedule', " +
                                 " '" + ddlWT + "','" + ddlWT1 + "','" + idivision_code + "', 1, getdate(),'" + TP_Terr_Value + "'," +
                                 "'" + TP_Terr1_Value + "','" + TP_Terr2_Value + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + Session["sf_name"].ToString() + "') ";
                            command.ExecuteNonQuery();
                            //iReturn = 1;

                            command.CommandText = " Update Trans_TP_One set HQCodes='" + Session["sf_code"].ToString() + "',HQNames='" + HQNamevalues + "' where " +
                                   " sf_code = '" + Session["sf_code"].ToString() + "' and Tour_Month = '" + dt_TourPlan.Month + "' and Tour_Year = '" + dt_TourPlan.Year + "' " +
                                   " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' and Worktype_Name_B = 'Field Work' ";
                             command.ExecuteNonQuery();
                        }
                        else
                        {
                            command.CommandText = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1, " +
                                 " Tour_Schedule1='" + TP_Terr_Name + "',Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "', " +
                                 " WorkType_Code_B = '" + ddlWT + "',Worktype_Name_B='" + ddlWT1 + "',Territory_Code1='" + TP_Terr_Value + "',Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + Session["sf_name"].ToString() + "' " +
                                 " where SF_Code='" + Session["sf_code"].ToString() + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + idivision_code + "' ";
                            command.ExecuteNonQuery();
                            //iReturn = 1;
                        }
                    }
                    //iReturn = tp.RecordAdd_New(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT1, TP_Objective, TP_Submit, Session["sf_code"].ToString(), TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());


                }
                transaction.Commit();
                command.CommandText = " select count (*) from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "' and mnth='" + imonth + "'" +
                                       " and yr='" + iyear + "' and div='" + idivision_code + "'  ";
                iRecordExistTP = (Int32)command.ExecuteScalar();

                if (iRecordExistTP == 0)
                {
                    command.CommandText =
                        " insert into Tourplan_detail "+
                                                " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     " +

                        " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
                        " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
                        " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
                        " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
                        " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
                        " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
                        " (select top 1 FieldWork_Indicator from Mas_WorkType_BaseLevel where WorkType_Code_B =a.worktype_code_b) FieldWork_Indicator,  '','',HQCodes,HQNames,'','','','',getdate(),'Desktop' " +
                        " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + imonth + "' and tour_year='" + iyear + "' order by tour_date ";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "delete from tourplan_detail where sfcode='" + Session["sf_code"].ToString() + "'and tour_month='" + imonth + "' and tour_year='" + iyear + "' ";
                    command.ExecuteNonQuery();

                    command.CommandText =
                        " insert into Tourplan_detail "+
                                                " (  SFCode ,SFName,Div ,Mnth,Yr ,dayno,Change_Status ,Rejection_Reason ,TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode  , ClusterCode2 ,ClusterCode3 ,ClusterName  ,ClusterName2 ,ClusterName3 ,ClusterSFs  ,ClusterSFNms ,JWCodes,JWNames,JWCodes2  ,JWNames2  ,JWCodes3  ,JWNames3  , Dr_Code,Dr_Name,Dr_two_code  ,Dr_two_name  ,Dr_three_code ,Dr_three_name ,Chem_Code  ,Chem_Name  ,Chem_two_code ,Chem_two_name ,Chem_three_code ,Chem_three_name , Stockist_Code ,Stockist_Name ,Stockist_two_code ,Stockist_two_name ,Stockist_three_code ,Stockist_three_name ,Day ,Tour_Month  ,Tour_Year  ,tpmonth,tpday,DayRemarks  , DayRemarks2  ,DayRemarks3  ,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2  ,HQNames2  ,HQCodes3  ,HQNames3  ,submitted_time ,Entry_mode   )     " +

                        " select '" + Session["sf_code"].ToString() + "','" + Session["sf_name"].ToString() + "' ,a.division_code,tour_month,tour_year,day(tour_date),'1'  ,''," +
                        " tour_date,a.worktype_code_b,'','',a.worktype_name_b,'','',isnull(Territory_Code1,''),isnull(Territory_Code2,''),isnull(Territory_Code3,''),isnull  (Tour_Schedule1,'')," +
                        " isnull(Tour_Schedule2,''),isnull(Tour_Schedule3,''),'','','','','','','','',isnull(Dr_Code,''),isnull(Dr_Name,''),isnull(Dr_two_code,''),isnull(Dr_two_name,'')," +
                        " isnull(Dr_three_code,''),isnull(Dr_three_name,''),isnull(Chem_Code,''),isnull(Chem_Name,''),isnull(Chem_two_code,''),isnull(Chem_two_name,''),isnull(Chem_three_code,'')," +
                        " isnull(Chem_three_name,''),isnull(Stockist_Code,''),isnull(Stockist_Name,''),isnull(Stockist_two_code,''),isnull(Stockist_two_name,''),isnull(Stockist_three_code,'')," +
                        " isnull(Stockist_three_name,''),day(tour_date),tour_month,tour_year,substring(datename(month,tour_date),0,4)  day,datename(weekday,tour_date),'','','','1','1', " +
                        " (select top 1 FieldWork_Indicator from Mas_WorkType_BaseLevel where WorkType_Code_B =a.worktype_code_b) FieldWork_Indicator,  '','',HQCodes,HQNames,'','','','',getdate(),'Desktop' " +
                        " from trans_tp_one  a where sf_code='" + Session["sf_code"].ToString() + "' and tour_month='" + imonth + "' and tour_year='" + iyear + "' order by tour_date ";
                    command.ExecuteNonQuery();
                }
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
        iReturn = 1;
        return iReturn;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (sf_type == "1")
        {
            Response.Redirect("~/Default_MR.aspx");
        }
    }
}