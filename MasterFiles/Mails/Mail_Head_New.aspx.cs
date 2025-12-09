//
#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bus_EReport;
using System.Net;
#endregion
//
#region Class Mail
public partial class MasterFiles_Mails_Mail_Head : System.Web.UI.Page
{
    //
    #region Variables
    //
    DataSet dsMail = null;
    DataSet dsFrom = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Type = string.Empty;
    string HO_ID = string.Empty;
    string sNew_Sf_Code = string.Empty;
    DataSet dsUserList = new DataSet();
    string sLevel = string.Empty;
    string temp_code = string.Empty;
    string mail_to_sf_code = string.Empty;
    string temp_Name = string.Empty;
    string mail_to_sf_Name = string.Empty;
    string mail_cc_sf_code = string.Empty;
    string strSF_Name = string.Empty;
    string mail_bcc_sf_code = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsSalesForce = null;
    string strMail_CC = string.Empty;
    string sf_Name = string.Empty;
    string strMail_To = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strFileDateTime = string.Empty;
    string div_Name = string.Empty;
    //
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        // 
        #region Assign Values to Variables
        //
        string productName = Request.QueryString["ProductName"];        
        //
        if (Session["sf_code"]!=null && Session["sf_type"]!=null && Session["HO_ID"]!=null)
        {
            sf_code = Session["sf_code"].ToString();
            sf_Type = Session["sf_type"].ToString();
            HO_ID = Session["HO_ID"].ToString();
            //
            sNew_Sf_Code = sf_code;
            if (sNew_Sf_Code == "admin")
                sNew_Sf_Code = HO_ID;
            //
        }
        //
        if (Session["div_code"] != null)
        {
            if (Session["div_code"].ToString() != "")
            {
                div_code = Session["div_code"].ToString();
                div_Name = Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_Name = Session["div_Name"].ToString();
        }
        //
        #endregion
        //
        #region page not Postback
        if (!Page.IsPostBack)
        {
            //
            sf_code = Session["sf_code"].ToString();
            sf_Type = Session["sf_type"].ToString();
            HO_ID = Session["HO_ID"].ToString();
            //
            if (Session["div_code"] != null)
            {
                if (Session["div_code"].ToString() != "")
                {
                    div_code = Session["div_code"].ToString();
                    div_Name = Session["sf_name"].ToString();
                }
            }
            else if (sf_Type == "3")
            {
                div_code = Session["division_code"].ToString();
                div_Name = Session["div_Name"].ToString();
            }
            //
            sNew_Sf_Code = sf_code;
            if (sNew_Sf_Code == "admin")
                sNew_Sf_Code = HO_ID;
            //
            ViewState["Current_Mail_Location"] = "Inbox";
            //
            //ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //
            FillMails(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
            pnlCompose.Visible = false;
            pnlFolder.Visible = false;
            pnlpopup.Visible = false;
            pnlViewInbox.Visible = false;
            pnlSent.Visible = false;
            pnlViewMail.Visible = false;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            FillState(div_code);
            lblStateName.Visible = false;
            ddlState.Visible = false;
           
            //
            txtboxSearch.Enabled = false;
            txtboxSearch.BackColor = System.Drawing.Color.LightGray;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_HOID_TP_Edit_Year(sf_Type, div_code);
            if (div_code != "")
            {
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        ddlYr.Items.Add(k.ToString());
                        ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                        ddlYr.SelectedValue = DateTime.Now.Year.ToString();
                    }
                }
            }
            else
            {
                string dt = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString() + "-" + DateTime.Now.Minute;
                ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                ddlYr.Items.Add(DateTime.Now.Year.ToString());
            }
            //
           
            GetFolderList();
            string sQry = "SELECT Sf_Code, Sf_Name, Theme, Photo FROM Mas_Mail_Home WHERE Sf_Code='" + sNew_Sf_Code + "' AND Division_Code in(" + div_code + ")";
            DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
            DataTable dtMail = db.Exec_DataTable(sQry);
            //
            if (dtMail.Rows.Count > 0)
            {
                //set_Theme(dtMail.Rows[0]["Theme"].ToString());
                if (dtMail.Rows[0]["Photo"].ToString() != null && dtMail.Rows[0]["Photo"].ToString() != "")
                    imgSF.Src = dtMail.Rows[0]["Photo"].ToString();
                //
                if (sNew_Sf_Code.Contains("MR") || sNew_Sf_Code.Contains("MGR"))
                    sQry = "SELECT Sf_Code, Sf_Name FROM Mas_SalesForce WHERE Sf_Code='" + sNew_Sf_Code + "' AND (Division_Code LIKE '%," + div_code + ",%' OR Division_Code LIKE '" + div_code + ",%')";
                else
                    sQry = "SELECT HO_ID as Sf_Code, Name as Sf_Name FROM Mas_Ho_Id_Creation WHERE HO_ID='" + sNew_Sf_Code + "' AND (Division_Code LIKE '%," + div_code + ",%' OR Division_Code LIKE '" + div_code + ",%')";
                DataTable dtSf_Name = db.Exec_DataTable(sQry);
                if (dtSf_Name.Rows.Count > 0)
                    lblSfName.Text = " " + dtSf_Name.Rows[0]["Sf_Name"].ToString();
            }
            else
            {
                if (sNew_Sf_Code.Contains("MR") || sNew_Sf_Code.Contains("MGR"))
                    sQry = "INSERT INTO Mas_Mail_Home (Sf_Code, Sf_Name, Division_Code, Theme, Photo, Remarks) " +
                        "VALUES('" + sNew_Sf_Code + "', (SELECT Sf_Name FROM Mas_SalesForce WHERE Sf_Code='" + sNew_Sf_Code + "' AND (Division_Code LIKE '%," + div_code + ",%' OR Division_Code LIKE '" + div_code + ",%')), " +
                        "" + div_code + ", 0, '', '') " +
                        "SELECT Sf_Code, Sf_Name, Theme, Photo FROM Mas_Mail_Home WHERE sf_code='" + sNew_Sf_Code + "' AND Division_Code=" + div_code + "";
                else
                    sQry = "INSERT INTO Mas_Mail_Home (Sf_Code, Sf_Name, Division_Code, Theme, Photo, Remarks) " +
                        "VALUES('" + sNew_Sf_Code + "', (SELECT Name as Sf_Name FROM Mas_Ho_Id_Creation WHERE HO_ID='" + sNew_Sf_Code + "' AND (Division_Code LIKE '%," + div_code + ",%' OR Division_Code LIKE '" + div_code + ",%')), " +
                        "" + div_code + ", 0, '', '') " +
                        "SELECT Sf_Code, Sf_Name, Theme, Photo FROM Mas_Mail_Home WHERE sf_code='" + sNew_Sf_Code + "' AND Division_Code=" + div_code + "";
                //
                dtMail = db.Exec_DataTable(sQry);
                //
                //set_Theme(dtMail.Rows[0]["Theme"].ToString());
                if (dtMail.Rows[0]["Photo"].ToString() != null && dtMail.Rows[0]["Photo"].ToString() != "")
                    imgSF.Src = dtMail.Rows[0]["Photo"].ToString();
                //
                lblSfName.Text = dtMail.Rows[0]["Sf_Name"].ToString();
            }
            //bdy.Style.Add("background-image", "url(BgImgs/imgbg3.jpg)");
            bdy.Style.Add("background-repeat", "repeat-y");
            bdy.Style.Add("background-attachment", "fixed");
            bdy.Style.Add("background-size", "cover");
            bdy.Style.Add("width", "100%");
            bdy.Style.Add("height", "100%");
            bdy.Style.Add("-webkit-background-size", "cover");
            bdy.Style.Add("-moz-background-size", "cover");
            bdy.Style.Add("-o-background-size", "cover");
        }
        #endregion
        //
        //DeleteDisable();        
    }
    #endregion
    //
    #region Page Load Time
    //
    #region OnLoadComplete
    protected override void OnLoadComplete(EventArgs e)
    {
        //ServerEndTime = DateTime.Now;
        //TrackPageTime();//It will give you page load time  
    }
    #endregion
    //
    #region TrackPageTime
    public void TrackPageTime()
    {
        //TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        //time = serverTimeDiff.Minutes;
    }
    #endregion
    //
    #endregion
    //
    #region ADDRESS BOOK
    //
    #region FillAddressBook
    private void FillAddressBook()
    {
        try
        {
            //FillDesignation();
            //
            DataTable dt1 = new DataTable();
            //
            DataTable dtMR = new DataTable();
            DataSet dsmgrsf = new DataSet();
            SalesForce sf = new SalesForce();
            string sCrnt_Sf_Code = sf_code;

            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
         
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sCrnt_Sf_Code);
            //
            DataTable dtHO = new DataTable();
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
                
                if (Session["sf_type"].ToString() == "1")
                {
                    DCR dc = new DCR();
                    dtMR = sf.getMail_MRJointWork_New(div_code, sCrnt_Sf_Code, 0);

                    //dtHO = sf.sp_UserList_HOID(div_code);
                    
                    dtHO = sf.sp_UserList_HOID(div_code);
                    if (dtHO.Rows.Count > 0)
                    {
                        dtMR.Merge(dtHO);
                    }
                    //dtMR.Merge(dtHO);
                    //
                    gvFF.DataSource = dtMR;
                    gvFF.DataBind();
                    //
                    if (dtMR.Rows.Count < 1 || dtMR.Rows.Count == null)
                        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
                    //
                }
                else if (Session["sf_code"].ToString() == "admin")
                {
                    if (div_code.Contains(','))
                        div_code = div_code.Substring(0, div_code.Length - 1);
                    //
                    dtMR.Clear();


                   
                        dsSalesForce = sf.SalesForceListMgrGet_Mail(div_code, "admin", HO_ID);
                        //dtMR = sf.getAddressBookWithoutAdmin(div_code, sCrnt_Sf_Code, 0);

                        dtHO = sf.sp_UserList_HOID(div_code);
                        dtHO.Merge(dsSalesForce.Tables[0]);
                        //dsSalesForce.Merge(dtHO);

                        dtMR.Merge(dtHO);
                        ViewState["dsSalesForce"] = dtMR;
                    

                    //
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    if (div_code.Contains(','))
                        div_code = div_code.Substring(0, div_code.Length - 1);
                    //
                    dtMR = sf.getAddressBookMgr_New(div_code, sCrnt_Sf_Code, 0);
                    //dtHO = sf.sp_UserList_HOID(div_code);
                    //dtMR.Merge(dtHO);
                    //
                   
                }
            }
            else
            {
                dtMR = sf.getAuditManagerTeam_mail_New(div_code, sCrnt_Sf_Code, 0);
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            }
            //
            if (ViewState["dsSalesForce"] != null)
            {
                dtMR = (DataTable)ViewState["dsSalesForce"];
            }
            gvFF.DataSource = dtMR;
            gvFF.DataBind();
            //
            string[] sDesg_Col = { "Designation_Code", "Designation_Short_Name" };
            DataTable dtDes = dtMR.DefaultView.ToTable(true, sDesg_Col);
            chkDesgn.DataTextField = "Designation_Short_Name";
            chkDesgn.DataValueField = "Designation_Code";
            chkDesgn.DataSource = dtDes;
            chkDesgn.DataBind();
            //
            if (dtMR.Rows.Count < 1 || dtMR == null)
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            //
            FillgridColor();
            //
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    #endregion
    //
    #region imgAddressClose_Click
    protected void imgAddressClose_Click(object sender, EventArgs e)
    {
        string sel_ff = string.Empty;
        if (ViewState["from"] != null)
        {
            if (ViewState["from"].ToString() == "To")
            {
                txtAddr.Text = "";
            }
            else if (ViewState["from"].ToString() == "CC")
            {
                txtAddr1.Text = "";
            }
            else if (ViewState["from"].ToString() == "BCC")
            {
                txtAddr2.Text = "";
            }
            //
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox ChkboxRows = (CheckBox)row.FindControl("chkSf");
                Label lblSf_Name = (Label)row.FindControl("lblSf_Name");
                Label lblsf_mail = (Label)row.FindControl("lblsf_mail");
                //
                if (ChkboxRows.Checked)
                {
                    sel_ff = sel_ff + lblSf_Name.Text;
                    sel_ff = sel_ff.Replace("&nbsp;", "");
                    sel_ff = sel_ff + " , ";
                    //
                    temp_code = lblsf_mail.Text.ToString().Substring(0, lblsf_mail.Text.ToString().IndexOf('-'));
                    temp_Name = lblSf_Name.Text.ToString().Replace("&nbsp;", "");
                    mail_to_sf_code = mail_to_sf_code + temp_code + ",";
                    mail_to_sf_Name = mail_to_sf_Name + temp_Name + ",";
                }
            }
            pnlpopup.Visible = false;
        }
        //
        if (ViewState["from"].ToString() == "To")
        {
            txtAddr.Text = sel_ff;
            ViewState["mail_to_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_Name"] = mail_to_sf_Name;
        }
        else if (ViewState["from"].ToString() == "CC")
        {
            txtAddr1.Text = sel_ff;
            ViewState["mail_cc_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_NameCC"] = mail_to_sf_Name;
        }
        else if (ViewState["from"].ToString() == "BCC")
        {
            txtAddr2.Text = sel_ff;
            ViewState["mail_bcc_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_NameBCC"] = mail_to_sf_Name;
        }
        //
        ViewState["pnlpopup"] = "";
        ViewState["from"] = "";
    }
    #endregion
    //
    #region rdoadr_SelectedIndexChanged
    protected void rdoadr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            ddlFFType.SelectedValue = "0";
            chkMR.Checked = false;
            ddlAlpha.Visible = false;
            Enable_Disable_Control_For_AddressBook(false, true);
            lblStateName.Visible = false;
            ddlState.Visible = false;
            //
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                item.Selected = false;
            }
            //
        }
        else if(rdoadr.SelectedValue.ToString() == "1")
        {
            Enable_Disable_Control_For_AddressBook(true, false);
            lblStateName.Visible = false;
            ddlState.Visible = false;
        }
        else if (rdoadr.SelectedValue.ToString() == "2")
        {
            Enable_Disable_Control_For_AddressBook(false, false);
            lblStateName.Visible = true;
            ddlState.Visible = true;
            FillState(div_code);
        }
        //
        bool blCnt = false;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
            blCnt = true;
        }
        if (blCnt)
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
        //
        ddlFFType.SelectedValue = "2";
        //ddlFieldForce.SelectedValue = "0";
        FillManagers();
        FillgridColor();
        //
    }
    #endregion

    #region FillState
    private void FillState(string div_code)
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        string state_cd = string.Empty;
        string sState = string.Empty;
        string[] statecd;

        dsDivision = dv.getStateMailDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            DataSet dsSf = new DataSet();
            dsSf = st.getSt(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsSf;
            ddlState.DataBind();

        }
    }
    #endregion
    //
    #region imgAddressBook_Click
    protected void imgAddressBook_Click(object sender, EventArgs e)
    {
        if (ViewState["dsSalesForce"] == null)
        {
            FillAddressBook();
        }
        //
        //if ((txtAddr.Text.Length == 0) && (txtAddr1.Text.Length == 0) && (txtAddr2.Text.Length == 0))
         // {
         //   lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
         //   foreach (GridViewRow row in gvFF.Rows)
         //   {
         //       CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
         //       chkSf.Checked = false;
         //   }
       // }
        // End by Sridevi - 10-Oct-15
        //foreach (ListItem item in chkDesgn.Items)
        //{
        //    chkLevelAll.Checked = true;
        //    chkMR.Checked = true;
        //    item.Selected = true;            
        //}
        //
        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        //
        ViewState["from"] = "To";
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        //
        if (rdoadr.SelectedValue.ToString() == "0")
            Enable_Disable_Control_For_AddressBook(false, true);
        else
        {
            Enable_Disable_Control_For_AddressBook(true, false);
            //
            ddlAlpha.Visible = true;
            //
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
        }
        //
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }
        //FillgridColor();
        pnlpopup.Visible = true;
    }
    #endregion
    //
    #region Enable_Disable_Control_For_AddressBook
    private void Enable_Disable_Control_For_AddressBook(bool ddl_FFType_FldFrc_lblFF, bool Chk_LvlAll_Desg_Lbl13)
    {
        ddlFFType.Visible = ddl_FFType_FldFrc_lblFF;
        ddlFieldForce.Visible = ddl_FFType_FldFrc_lblFF;
        lblFF.Visible = ddl_FFType_FldFrc_lblFF;
        //
        chkLevelAll.Visible = Chk_LvlAll_Desg_Lbl13;
        chkDesgn.Visible = Chk_LvlAll_Desg_Lbl13;
        Label3.Visible = Chk_LvlAll_Desg_Lbl13;
        
        //
    }
    #endregion
    //
    #region FillDesignation
    private void FillDesignation()
    {
        DataTable dt = new DataTable();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            AdminSetup adm = new AdminSetup();
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }
            //
            dt = sf.getAddressBookDesign(div_code, "admin", 0);
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            DCR dc = new DCR();
            dt = dc.LoadMailWorkwithDes(sNew_Sf_Code);
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            SalesForce sf = new SalesForce();
            DataSet dsmgrsf = new DataSet();
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sNew_Sf_Code);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
                dt = sf.getAddressBookDesign(div_code, sf_code, 0);
            }
            else
            {
                // Fetch Managers Audit Team
                dt = sf.getAuditManagerTeam_mail(div_code, sNew_Sf_Code, 0);
                DataView view = new DataView(dt);
                dt = view.ToTable(true, "Designation_Short_Name", "Designation_Code");
            }
        }
        if (dt.Rows.Count > 0)
        {
            chkDesgn.DataTextField = "Designation_Short_Name";
            chkDesgn.DataValueField = "Designation_Code";
            chkDesgn.DataSource = dt;
            chkDesgn.DataBind();
        }
    }
    #endregion
    // 
    #region FillSalesForce
    private void FillSalesForce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlFieldForce.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);
        //
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblsf_Code = (Label)grid_row.FindControl("lblsf_Code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            {
                if (dsUserList.Tables[0].Rows[i]["sf_code"].ToString() == lblsf_Code.Text)
                {
                    chkSf.Checked = true;
                    break;
                }
                //
            }
        }
    }
    #endregion
    //
    #region FillDllSalesForce
    private void FillDllSalesForce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlState.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        //dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);

        Division dv = new Division();

        //dsUserList = dv.getStatePerDivision(div_code);      

        //
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblState = (Label)grid_row.FindControl("lblState");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            //for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            //{
                if (ddlState.SelectedValue == lblState.Text)
                {
                    chkSf.Checked = true;
                    //break;
                }
                //
            //}
        }
    }
    #endregion
    //

    #region FillSF_Alpha
    private void FillSF_Alpha()
    {/*
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }*/
    }
    #endregion
    //
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //
        string sCrnt_Sf_Code = sf_code;
        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            ddlAlpha.Visible = false;
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }
            dsSalesForce = sf.sp_UserList_Hierarchy_Mail(div_code, sCrnt_Sf_Code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "1")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, sCrnt_Sf_Code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "3")
        {
            SubDivision subDiv = new SubDivision();
            dsSalesForce = subDiv.getSubdivision(div_code);
        }
        //
        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();
                //
                ddlSF.DataTextField = "des_color";
                ddlSF.DataValueField = "SF_Code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();
                //
                FillColor();
                //
            }
        }

        else if (ddlFFType.SelectedValue.ToString() == "3")
        {
            ddlFieldForce.DataTextField = "subdivision_name";
            ddlFieldForce.DataValueField = "subdivision_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
        //
        if (Session["sf_type"].ToString() == "1")
        {
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            lblFF.Visible = false;
        }
    }
    #endregion
    //
    #region FillColor (for Dropdown-Manager)
    private void FillColor()
    {
        int j = 0;
        //
        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "";
            try
            {
                bcolor = "#" + ColorItems.Text;
            }
            catch
            {
                bcolor = "#FFFFFF";
            }
            //
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j++;
        }
    }
    #endregion
    //
    #region FillgridColor
    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }
    }
    #endregion
    //
    #region chkLevelAll_CheckedChanged
    protected void chkLevelAll_CheckedChanged(object sender, EventArgs e)
    {
        bool blChkLvlAll_Chkd = chkLevelAll.Checked;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = blChkLvlAll_Chkd;
            chkMR.Checked = blChkLvlAll_Chkd;
        }
        //
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = blChkLvlAll_Chkd;
            int j = 0;
            if (blChkLvlAll_Chkd)
            {
                j = gvFF.Rows.Count;
            }
            lblSelectedCount.Text = "No.of Filed Force Selected : " + j.ToString();
        }
        FillgridColor();
        //
    }
    #endregion
    //
    #region chkMR_OnCheckChanged
    protected void chkMR_OnCheckChanged(object sender, EventArgs e)
    {
        if (chkMR.Checked == false)
        {
            chkLevelAll.Checked = false;
        }
    }
    #endregion
    //    
    #region chkDesgn_OnSelectedIndexChanged
    protected void chkDesgn_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");
            //
            if (lblSf_Name.Text.Contains("admin"))
            {
                if (chkLevelAll.Checked)
                    chkSf.Checked = true;
            }
        }
        //
        int i = 0;
        foreach (ListItem item in chkDesgn.Items)
        {
            bool blChckd = item.Selected;
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                Label lblDesignation_Code = (Label)grid_row.FindControl("lblDesignation_Code");
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                if (item.Value == lblDesignation_Code.Text)
                {
                    chkSf.Checked = blChckd;
                    if (chkSf.Checked)
                        i++;
                }
            }
        }
        //
        //int i = 0;
        //foreach (GridViewRow row in gvFF.Rows)
        //{
        //    CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
        //    if (chkSf.Checked)
        //        i++;
        //}
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        //FillgridColor();
        //
    }
    #endregion
    //
    #region gvFF_OnCheckedChanged
    protected void gvFF_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSf = (CheckBox)sender;
        GridViewRow row1 = (GridViewRow)chkSf.Parent.Parent;
        row1.Focus();
        int count = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSf");
            if (ChkBoxRows.Checked)
                count++;
        }
        lblSelectedCount.Text = "No.of Filed Force Selected : " + count;
        FillgridColor();
    }
    #endregion
    //
    #region grdFF_RowDataBound
    protected void grdFF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBackColor = (Label)e.Row.FindControl("lblsf_color");
                Label lblSFType = (Label)e.Row.FindControl("lblsf_Type");

                if (lblBackColor != null)
                {
                    string sClrCode = "#FFFFFF";
                    if (lblBackColor.Text == "-Level1")/*level 1*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#37C8FF";
                        else
                            sClrCode = "#BADCF7";
                    }
                    else if (lblBackColor.Text == "-Level2")/*level 2*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#718FC7";
                        else
                            sClrCode = "#ccffcc";
                    }
                    else if (lblBackColor.Text == "-Level3")/*level 3*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#e0ffff";
                        else
                            sClrCode = "#ffffcc";
                    }
                    else if (lblBackColor.Text == "-Level4")/*level 4*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#fff0f5";
                        else
                            sClrCode = "e0ffff";
                    }
                    e.Row.BackColor = System.Drawing.Color.FromName(sClrCode);
                }
            }
        }
        catch { }
    }
    #endregion
    //
    #region ddlAlpha_SelectedIndexChanged
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {/*
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //
            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }*/
    }
    #endregion
    //
    #region ddlFFType_SelectedIndexChanged
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
        }
        //
        ddlFieldForce.Items.Clear();
        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
        lblFF.Text = "Field Force";
        FillManagers();
        //FillgridColor();
        //
    }
    #endregion
    //
    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkLevelAll.Checked = false;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = false;
        }
        if (ddlFFType.SelectedItem.Text == "Team")
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillSalesForce();
        }
        //else if (ddlFFType.SelectedItem.Text == "Division")
        //{
        //    dsUserList = sf.UserList_Self(ddlFieldForce.SelectedValue, "admin");
        //    //
        //    if (dsUserList.Tables[0].Rows.Count > 0)
        //    {
        //        gvFF.Visible = true;
        //        gvFF.DataSource = dsUserList;
        //        gvFF.DataBind();
        //    }
        //}

        else if (ddlFFType.SelectedItem.Text == "Sub Division")
        {
            FillSubDivSalesForce();
            //dsUserList = sf.UserList_Self(ddlFieldForce.SelectedValue, "admin");
            ////
            //if (dsUserList.Tables[0].Rows.Count > 0)
            //{
            //    gvFF.Visible = true;
            //    gvFF.DataSource = dsUserList;
            //    gvFF.DataBind();
            //}
        }
        //
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked)
                i++;
        }
        //
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        FillgridColor();
        //FillColor();
        //
    }
    //
    #endregion
    #region FillSubDivSalesForce
    private void FillSubDivSalesForce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlFieldForce.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        //dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);

        Division dv = new Division();

        //dsUserList = dv.getStatePerDivision(div_code);      

        //
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblsubdivision = (Label)grid_row.FindControl("lblsubdivision_code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            //for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            //{
            if (ddlFieldForce.SelectedValue == lblsubdivision.Text)
            {
                chkSf.Checked = true;
                //break;
            }
            //
            //}
        }
    }
    #endregion
    //
    //
    #region ddlState_SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlState.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillDllSalesForce();
        }
       
        //
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked)
                i++;
        }
        //
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        FillgridColor();
        FillColor();
        //
    }
    //
    #endregion
    //
    /*
    private void FillDividion()
    {
        AdminSetup Ad = new AdminSetup();
        dsSalesForce = Ad.GetDivision(ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvFF.DataSource = dsSalesForce;
            gvFF.DataBind();
        }
        else
        {
            gvFF.DataSource = dsSalesForce;
            gvFF.DataBind();
            for (int i = 0; i < gvFF.Rows.Count; i++)
            {
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            }
        }
    }
    */
    //
    #endregion
    //
    #region COMPOSE
    //
    #region btnCompose_Onclick
    protected void btnCompose_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        lnkClear_Click(sender, e);
        divInboxList.Visible = false;
        pnlViewMail.Visible = false;
        pnlCompose.Visible = true;
        pnlSent.Visible = false;
        pnlInbox.Visible = false;
        pnlFolder.Visible = false;
        ViewState["Attachment_Forward"] = null;
        ViewState["CurrentTable"] = null;
        SetInitialRow(true);
    }
    #endregion
    //
    #region lnkRemoveCC_Click
    protected void lnkRemoveCC_Click(object sender, EventArgs e)
    {
        ViewState["pnlCompose"] = "true";

        if (ViewState["cc"] != null)
        {
            if (ViewState["cc"].ToString() == "1")
            {
                ViewState["cc"] = "2";
                lnkRemoveCC.Text = "Add Cc";
                TrCC.Visible = false;
            }
            else
            {
                ViewState["cc"] = "1";
                lnkRemoveCC.Text = "Remove Cc";
                TrCC.Visible = true;
            }
        }
        else
        {
            ViewState["cc"] = "1";
            lnkRemoveCC.Text = "Add Cc";
            TrCC.Visible = false;
        }
    }
    //
    #endregion
    //
    #region lnkClear_Click
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        txtAddr.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = "";
        txtMsg.Text = "";
        txtSub.Text = "";
        if (grdAttchmentFiles.Rows.Count > 1)
            SetInitialRow(true);
    }
    #endregion
    //
    #region Button Send Mail Click
    protected void lnkBtnSend_Click(object sender, EventArgs e)
    {
        string DivCode = string.Empty;
        string to_sf_code = string.Empty;
        DataSet dsMailCompose = null;
        string cc_sf_code = string.Empty;
        string bcc_sf_code = string.Empty;
        string tobcc_sf_name = string.Empty;
        string toCC_sf_name = string.Empty;
        string Attachpath = string.Empty;
        //
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
        DataTable dtAll = new DataTable();
        if (dtFrwd != null)
            dtAll.Merge(dtFrwd);
        if (dt == null)
            SetInitialRow(false);
        dt = (DataTable)ViewState["CurrentTable"];
        dtAll.Merge(dt);
        //
        foreach (DataRow dtRow in dtAll.Rows)
        {
            if (dtRow["New_File_Name"].ToString() != "" && dtRow["New_File_Name"].ToString() != null)
                Attachpath += dtRow["New_File_Name"].ToString() + ",";
        }
        for (int i = 0; i < grdAttchmentFiles.Rows.Count; i++)
        {
            FileUpload attachFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
            Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
            HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
            if (attachFile.HasFile)
            {
                string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + attachFile.FileName;
                attachFile.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + sNewFileName));
                lblFileAttach.Text = attachFile.FileName;
                hdnAttachFile.Value = sNewFileName;
                Attachpath += sNewFileName + ",";
            }
        }
        if (Attachpath!="")
            Attachpath = Attachpath.Remove(Attachpath.LastIndexOf(","));
        //
        if (div_code.Contains(','))
            div_code = div_code.Remove(div_code.Length - 1);
        //
        string cur_sf_code = string.Empty;
        string cur_sf_level = string.Empty;
        Boolean blnMail = false;
        //
        if (ViewState["mail_to_sf_code"] != null)
        {
            blnMail = true;
            //
            string sMail_To_Sf_Codes = ViewState["mail_to_sf_code"].ToString();
            if (ViewState["mail_cc_sf_code"] != null)
                cc_sf_code = ViewState["mail_cc_sf_code"].ToString();
            //
            if (ViewState["mail_bcc_sf_code"] != null)
                bcc_sf_code = ViewState["mail_bcc_sf_code"].ToString();
            //
            if (ViewState["mail_to_sf_NameBCC"] != null)
                tobcc_sf_name = ViewState["mail_to_sf_NameBCC"].ToString();
            //
            if (ViewState["mail_to_sf_NameCC"] != null)
                toCC_sf_name = ViewState["mail_to_sf_NameCC"].ToString();
            //
            string strSF_Name = "";
            if (sf_Type == "3")
                strSF_Name = Session["Corporate"].ToString();
            else
                strSF_Name = Session["sf_name"].ToString() + "-" + Session["Sf_HQ"] + "-" + Session["Designation_Short_Name"];
            //
            AdminSetup adm = new AdminSetup();   
            //
            dsMailCompose = adm.ComposeMail(sNew_Sf_Code, ViewState["mail_to_sf_code"].ToString(), txtSub.Text.Trim(), txtMsg.Text.Trim(),
                                            Attachpath, cc_sf_code, bcc_sf_code, div_code, Request.ServerVariables["REMOTE_ADDR"].ToString(),
                                            toCC_sf_name, tobcc_sf_name, strSF_Name, ViewState["mail_to_sf_Name"].ToString());
            //
            if (ViewState["inbox_id"] != null)
            {
                int iRet = adm.ChangeMailStatus(sNew_Sf_Code, Convert.ToInt32(ViewState["inbox_id"].ToString()), 10, "");
            }
            //
        }
        //
        if (blnMail)
        {
            txtAddr.Text = "";
            txtSub.Text = "";
            txtMsg.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            foreach (ListItem item in chkFF.Items)
            {
                item.Selected = false;
            }
            //
            if (ViewState["Current_Mail_Location"] != null)
            {
                if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
                    btnInbox_Click(sender, e);
                else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
                    btnView_Click(sender, e);
                else if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
                    btnSentItem_Click(sender, e);
            }
            //
            ViewState["Attachment_Forward"] = null;
            ViewState["CurrentTable"] = null;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been sent successfully');</script>");
        }
    }
    #endregion
    //
    #region Add Image to Ritch TextBox
    protected void btnAddImg_Click(object sender, EventArgs e)
    {
        if (fileUpld.HasFile)
        {
            string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + fileUpld.FileName;
            //string FileName = System.IO.Path.GetFileName(fileUpld.PostedFile.FileName);
            string FilePath = "Imgs/" + sNewFileName;
            fileUpld.SaveAs(Server.MapPath(FilePath));
            txtMsg.Text = string.Format("<img src = '{0}' alt = '{1}' />", FilePath, sNewFileName) + "<br/>" + txtMsg.Text;
        }
    }
    #endregion
    //
    #region Discard Message
    protected void btnMsgDiscard_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (dt != null)
        {
            if (dt.Rows.Count > 1)
            {
                int iTtlRowCnt = dt.Rows.Count, iStrtIndx = 0;
                for (int i = 0; i < iTtlRowCnt - 1; i++)
                {
                    string fileName = Server.MapPath("~/MasterFiles/Mails/Attachment/" + dt.Rows[iStrtIndx]["New_File_Name"].ToString());
                    if (System.IO.File.Exists(fileName))
                        System.IO.File.Delete(fileName);
                    dt.Rows.Remove(dt.Rows[iStrtIndx]);
                }
            }
        }
        pnlCompose.Visible = false;
        pnlInbox.Visible = true;
        divInboxList.Visible = true;
    }
    #endregion
    //
    #region imgComposeCC_Click
    protected void imgComposeCC_Click(object sender, EventArgs e)
    {
        if (ViewState["dsSalesForce"] == null)
        {
            FillAddressBook();
        }
        //
        if ((txtAddr.Text.Length == 0) && (txtAddr1.Text.Length == 0) && (txtAddr2.Text.Length == 0))
        {
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        // 
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = true;
            chkMR.Checked = true;
            item.Selected = true;
        }
        //
        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        //
        ViewState["from"] = "CC";
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        //
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            Enable_Disable_Control_For_AddressBook(false, true);
            ddlAlpha.Visible = false;
        }
        else if(rdoadr.SelectedValue.ToString() == "1")
        {
            Enable_Disable_Control_For_AddressBook(true, false);
            ddlAlpha.Visible = true;
            //
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
        }
        else if (rdoadr.SelectedValue.ToString() == "2")
        {
            Enable_Disable_Control_For_AddressBook(false, false);
        }
        //
        bool blIsFalse = true;
        foreach (ListItem item in chkDesgn.Items)
        {
            if (blIsFalse)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                blIsFalse = false;
            }
            item.Selected = false;
        }
        //
        //FillgridColor();
        pnlpopup.Visible = true;
    }
    #endregion
    //
    #region imgRemoveBCC_Click
    protected void imgRemoveBCC_Click(object sender, EventArgs e)
    {
        ViewState["pnlCompose"] = "true";

        if (ViewState["bcc"] != null)
        {
            if (ViewState["bcc"].ToString() == "1")
            {
                ViewState["bcc"] = "2";
                imgRemoveBCC.Text = "Add Bcc";
                TrBCC.Visible = false;
            }
            else
            {
                ViewState["bcc"] = "1";
                imgRemoveBCC.Text = "Remove Bcc";
                TrBCC.Visible = true;
            }
        }
        else
        {
            ViewState["bcc"] = "1";
            imgRemoveBCC.Text = "Add Bcc";
            TrBCC.Visible = false;
        }
    }
    #endregion
    //
    #region imgComposeBCC_Click
    protected void imgComposeBCC_Click(object sender, EventArgs e)
    {
        if (ViewState["dsSalesForce"] == null)
        {
            FillAddressBook();
        }
        // 
        if ((txtAddr.Text.Length == 0) && (txtAddr1.Text.Length == 0) && (txtAddr2.Text.Length == 0))
        {
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        // 
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = true;
            chkMR.Checked = true;
            item.Selected = true;
        }
        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        //
        ViewState["from"] = "BCC";
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        //
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        ddlFieldForce.Visible = false;
        chkLevelAll.Visible = true;
        chkDesgn.Visible = true;
        Label3.Visible = true;
        lblFF.Visible = false;
        //
        if (rdoadr.SelectedValue.ToString() != "0")
        {
            lblFF.Visible = true;
            ddlFFType.Visible = true;
            ddlAlpha.Visible = true;
            ddlFieldForce.Visible = true;
            chkLevelAll.Visible = false;
            chkDesgn.Visible = false;
            Label3.Visible = false;
            //foreach (ListItem item in chkDesgn.Items)
            //{
            //    chkLevelAll.Checked = false;
            //    chkMR.Checked = false;
            //    item.Selected = false;
            //}
        }
        //
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }
        //
        //FillgridColor();
        pnlpopup.Visible = true;
    }
    #endregion
    //
    #region Attachment Files
    protected void grdAttchmentFiles_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
            LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemoveAttach");
            DataTable dtAll = new DataTable();
            if (dtFrwd != null)
                dtAll.Merge(dtFrwd);
            if (dt == null)
                SetInitialRow(false);
            dt = (DataTable)ViewState["CurrentTable"];
            dtAll.Merge(dt);
            if (lb != null)
            {
                if (dtAll.Rows.Count > 1)
                {
                    if (dtFrwd != null)
                        if (dtFrwd.Rows.Count > 0)
                            if (e.Row.RowIndex < dtFrwd.Rows.Count)
                                lb.Visible = false;
                }
                if (e.Row.RowIndex == dtAll.Rows.Count - 1)
                    lb.Visible = false;
                else
                    lb.Visible = false;
            }
        }
    }
    //
    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //
        DataTable dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
        DataTable dtCrntTbl = (DataTable)ViewState["CurrentTable"];
        DataTable dtAllAttach = new DataTable();
        if (dtFrwdAttach != null)
            dtAllAttach.Merge(dtFrwdAttach);
        dtAllAttach.Merge(dtCrntTbl);
        //
        int rowID = gvRow.RowIndex, iDtFrwdRowsCnt = 0;
        if (dtFrwdAttach != null)
            iDtFrwdRowsCnt = dtFrwdAttach.Rows.Count;
        //
        if (dtCrntTbl != null)
        {
            if (dtCrntTbl.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dtAllAttach.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number  
                    string fileName = Server.MapPath("~/MasterFiles/Mails/Attachment/" + dtAllAttach.Rows[rowID]["New_File_Name"].ToString());
                    if (System.IO.File.Exists(fileName))
                        System.IO.File.Delete(fileName);
                    dtCrntTbl.Rows.Remove(dtCrntTbl.Rows[rowID - iDtFrwdRowsCnt]);
                    ResetRowID(dtCrntTbl);
                }
            }
            //Store the current data in ViewState for future reference  
            ViewState["CurrentTable"] = dtCrntTbl;
        }
        dtAllAttach = new DataTable();
        if (dtFrwdAttach != null)
            dtAllAttach.Merge(dtFrwdAttach);
        dtAllAttach.Merge(dtCrntTbl);
        //Set Previous Data on Postbacks  
        grdAttchmentFiles.DataSource = dtAllAttach;
        grdAttchmentFiles.DataBind();
        SetPreviousData(dtAllAttach, false);
    }
    //
    protected void btnNewFile_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
        DataTable dtAll = new DataTable();
        if (dtFrwd != null)
            dtAll.Merge(dtFrwd);
        if (dt == null)
            SetInitialRow(false);
        dt = (DataTable)ViewState["CurrentTable"];
        dtAll.Merge(dt);
        AddNewRowToGrid(dtAll);
    }
    //
    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }
    //
    private void SetPreviousData(DataTable dtTbl, bool blTyp)
    {
        int rowIndex = 0;
        DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
        if (dtTbl != null)
        {
            if (dtTbl.Rows.Count > 0)
            {
                for (int i = 0; i < dtTbl.Rows.Count - 1; i++)
                {
                    FileUpload upldFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
                    Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
                    HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
                    LinkButton lnkBtnRmv = (LinkButton)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lnkRemoveAttach");
                    if (i < dtTbl.Rows.Count - 1)
                    {
                        //Assign the value from DataTable to the TextBox   
                        lblFileAttach.Text = dtTbl.Rows[i]["Original_Name"].ToString();
                        hdnAttachFile.Value = dtTbl.Rows[i]["New_File_Name"].ToString();
                        lblFileAttach.Visible = true;
                        upldFile.Visible = false;
                        if (blTyp)
                            lnkBtnRmv.Visible = false;
                        if (dtFrwd == null)
                            lnkBtnRmv.Visible = true;
                        else
                        {
                            bool blRmvBtnVsbl = true;
                            foreach (DataRow dtRow in dtFrwd.Rows)
                            {
                                if (hdnAttachFile.Value == dtRow["New_File_Name"].ToString() && hdnAttachFile.Value != "" && hdnAttachFile.Value != null)
                                {
                                    lnkBtnRmv.Visible = false;
                                    blRmvBtnVsbl = false;
                                    break;
                                }
                            }
                            if (blRmvBtnVsbl)
                                lnkBtnRmv.Visible = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
    }
    //
    private void AddNewRowToGrid(DataTable dtAttach)
    {
        DataTable dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
        DataTable dtCrntTbl = (DataTable)ViewState["CurrentTable"];
        DataTable dtAllAttach = new DataTable();
        //
        if (dtAttach != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
            DataRow drCurrentRow = null;
            //
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                int iEmptyRowCnt = 0;
                //add new row to DataTable 
                foreach (DataRow dtRows in dtCurrentTable.Rows)
                {
                    if (dtRows["New_File_Name"].ToString() == null || dtRows["New_File_Name"].ToString() == "")
                        iEmptyRowCnt++;
                }

                if (iEmptyRowCnt == 1 || iEmptyRowCnt == 0)
                    dtCurrentTable.Rows.Add(drCurrentRow);

                //Store the current data to ViewState for future reference   
                //dtAttach.Rows.Add(drCurrentRow);
                //
                ViewState["CurrentTable"] = dtCurrentTable;
                //                
                dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
                dtCrntTbl = (DataTable)ViewState["CurrentTable"];
                dtAllAttach = new DataTable();
                if (dtFrwdAttach != null)
                    dtAllAttach.Merge(dtFrwdAttach);
                dtAllAttach.Merge(dtCrntTbl);
                grdAttchmentFiles.DataSource = dtAllAttach;
                grdAttchmentFiles.DataBind();
                //
                for (int i = 0, k = 0; i < dtAllAttach.Rows.Count - 1; i++)
                {
                    FileUpload attachFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
                    Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
                    HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
                    if (attachFile.HasFile)
                    {
                        string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + attachFile.FileName;
                        attachFile.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + sNewFileName));
                        lblFileAttach.Text = attachFile.FileName;
                        hdnAttachFile.Value = sNewFileName;
                    }
                    if (k < dtCurrentTable.Rows.Count)
                    {
                        if (dtFrwd != null)
                        {
                            if (i >= dtFrwd.Rows.Count)
                            {
                                dtCurrentTable.Rows[k]["Original_Name"] = lblFileAttach.Text;
                                dtCurrentTable.Rows[k]["New_File_Name"] = hdnAttachFile.Value;
                                k++;
                            }
                        }
                        else
                        {
                            dtCurrentTable.Rows[k]["Original_Name"] = lblFileAttach.Text;
                            dtCurrentTable.Rows[k]["New_File_Name"] = hdnAttachFile.Value;
                            k++;
                        }
                    }
                    dtAttach.Rows[i]["Original_Name"] = lblFileAttach.Text;
                    dtAttach.Rows[i]["New_File_Name"] = hdnAttachFile.Value;
                }

                //Rebind the Grid with the current data to reflect changes  
                dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
                dtCrntTbl = (DataTable)ViewState["CurrentTable"];
                dtAllAttach = new DataTable();
                if (dtFrwdAttach != null)
                    dtAllAttach.Merge(dtFrwdAttach);
                dtAllAttach.Merge(dtCrntTbl);
                //
                grdAttchmentFiles.DataSource = dtAllAttach;
                grdAttchmentFiles.DataBind();
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks   
        dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
        dtCrntTbl = (DataTable)ViewState["CurrentTable"];
        dtAllAttach = new DataTable();
        if (dtFrwdAttach != null)
            dtAllAttach.Merge(dtFrwdAttach);
        dtAllAttach.Merge(dtCrntTbl);
        SetPreviousData(dtAllAttach, false);
    }
    //
    private void SetInitialRow(bool blType)
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Original_Name", typeof(string)));//for TextBox value   
        dt.Columns.Add(new DataColumn("New_File_Name", typeof(string)));
        //
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        //dr["Column1"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        if (blType)
        {
            grdAttchmentFiles.DataSource = dt;
            grdAttchmentFiles.DataBind();
        }
    }
    //
    #endregion
    //
    #endregion
    //
    #region INBOX
    //
    #region btnInbox_Click
    protected void btnInbox_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillMails(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
        //
        ViewState["Current_Mail_Location"] = "Inbox";
        divInboxList.Visible = true;
        pnlFolder.Visible = false;
        pnlInbox.Visible = true;
        pnlSent.Visible = false;
        pnlCompose.Visible = false;
        pnlViewMail.Visible = false;
    }
    //
    #endregion
    //
    #region gvInbox_RowDataBound
    protected void gvInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.background='none repeat scroll 0 0 #d5d7de'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.ToolTip = "Click to view mail";
            //
            AdminSetup adm = new AdminSetup();
            //
            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
                e.Row.Cells[4].Visible = false;
        }
    }
    #endregion
    //
    #region gvInbox_RowCommand
    protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            ViewState["Current_Mail_Location"] = "Inbox";
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString(), true);

            AdminSetup adm = new AdminSetup();
            int iReturn = adm.ChangeMailStatus(sNew_Sf_Code, Convert.ToInt32(slno.ToString()), 10, sf_Name);
            //if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
            //{
            //    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(slno.ToString()), 0, Session["sf_name"].ToString());
            //}
            //foreach (GridViewRow grRow in gvInbox.Rows)
            //{
            //    HiddenField hd_SlNo=(HiddenField)grRow.FindControl("hdnslNo");
            //    if (Convert.ToInt32(hd_SlNo.Value) == slno)
            //        grRow.Font.Bold = true;
            //    else
            //        grRow.Font.Bold = false;
            //}
            FillMails(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
        }
    }
    #endregion
    //
    #region gvInbox_OnPageIndexChanging
    protected void gvInbox_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInbox.PageIndex = e.NewPageIndex;
        FillMails(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
    }
    //
    #endregion
    //
    #region cbSelectAll_OnCheckedChanged
    protected void cbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(gvInbox, true, false);
    }
    #endregion
    //
    #region chkId_OnCheckedChanged
    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(gvInbox, false, false);
    }
    #endregion
    //
    #endregion
    //
    #region VIEWED
    //
    #region imgViewMail_Click
    protected void imgViewMail_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        divInboxList.Visible = false;
        pnlSent.Visible = false;
        pnlViewMail.Visible = false;
        // 
        if (ViewState["Current_Mail_Location"] != null)
        {
            if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
            {
                //int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(ViewState["inbox_id"].ToString()), 10, Session["sf_name"].ToString());
                FillMails(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
                divInboxList.Visible = true;
            }
            else if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
            {
                FillMails(grdSent, "sent", lblSent, "Sent Mails", 2);
                pnlSent.Visible = true;
            }
            else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
            {
                FillMails(grdView, "view", lblViewed, "Viewed Mails", 3);
                pnlViewMail.Visible = true;
            }
        }
        //  
        //int iMail_Count = MailCount();
        //
        pnlInbox.Visible = true;
        pnlViewInbox.Visible = false;
        //
    }
    //
    private void FillMails(GridView grdMail, string sType, Label lblMailCnt, string sLblText, int iType)
    {
        txtboxSearch.Visible = true;
        imgSearch.Visible = true;
        //int iTtl_Mails = MailCount();
        int iTtl_Mails = 0;
        //
        AdminSetup adm = new AdminSetup();
        string sTxtSearch = txtboxSearch.Text;
        txtboxSearch.Enabled = true;
        txtboxSearch.BackColor = System.Drawing.Color.White;
        //
        if (iType == 1)
        {
            sTxtSearch = "";
            txtboxSearch.Text = "";
            txtboxSearch.Enabled = false;
            txtboxSearch.BackColor = System.Drawing.Color.LightGray;
        }
        //
        if (iType != 4)
            dsMail = adm.getMailInbox(sNew_Sf_Code, div_code, sType, "", ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), txtboxSearch.Text);
        //
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            grdMail.Style.Add("margin-top", "5px");
            grdMail.Style.Add("margin-left", "5px");
            grdMail.DataSource = dsMail;
            grdMail.DataBind();
            //
            if (dsMail.Tables[0].Rows.Count > 0)
            {
                iTtl_Mails = dsMail.Tables[0].Rows.Count;
                foreach (GridViewRow row in grdMail.Rows)
                {
                    LinkButton lblSubject = (LinkButton)row.FindControl("lnk_MailSub");
                    //lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    //lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                    //
                    //if (iType == 1)
                    //if (dsMail.Tables[0].Rows[row.RowIndex]["Read_Flag"].ToString() == "0")
                    row.Font.Bold = true;
                }
            }
        }
        else
        {
            grdMail.Style.Add("margin-top", "5%");
            grdMail.Style.Add("margin-bottom", "5%");
            grdMail.Style.Add("margin-left", "20%");
            grdMail.DataSource = null;
            grdMail.DataBind();
        }
        lblMailCnt.Text = sLblText + " (" + iTtl_Mails.ToString() + ")";
        //
        if (iType == 1)
            if (iTtl_Mails == 0)
                divBtnHome.Visible = true;
            else
                if (Session["sf_type"].ToString() != "3")
                {
                    divBtnHome.Visible = false;
                }
    }
    #endregion
    //
    #region btnView_Mail_Click
    protected void btnView_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        txtboxSearch.Text = "";
        pnlSent.Visible = false;
        pnlViewMail.Visible = true;
        divInboxList.Visible = false;
        pnlInbox.Visible = true;
        pnlFolder.Visible = false;
        //
        FillMails(grdView, "view", lblViewed, "Viewed Mails", 3);
        //
        ViewState["Current_Mail_Location"] = "Viewed";
    }
    //
    #endregion
    //
    #region grdView_RowDataBound
    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.background='none repeat scroll 0 0 #d5d7de'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.ToolTip = "Click to view mail";

            AdminSetup adm = new AdminSetup();            

            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[4].Visible = false;
            }
        }
    }
    #endregion
    //
    #region grdView_RowCommand
    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            ViewState["Current_Mail_Location"] = "Viewed";
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString(), false);
        }
    }
    #endregion
    //
    #region grdView_PageIndexChanging
    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;
        FillMails(grdView, "view", lblViewed, "Viewed Mails", 3);
    }
    #endregion
    //ddlTheme
    #region grdViewcbSelectAll_OnCheckedChanged
    protected void grdViewcbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdView, true, true);
    }
    #endregion
    //
    #region grdViewchkId_OnCheckedChanged
    protected void grdViewchkId_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdView, false, true);
    }
    #endregion
    //
    #endregion
    //
    #region MONTH, YEAR, MOVED_TO, SEARCH MAIL & ENABLE_DISABLE_CONTROLS
    //
    #region ddlMoved_OnSelectedIndexChanged
    protected void ddlMoved_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //
        if(ViewState["Current_Mail_Location"].ToString() == "Inbox" || ViewState["Current_Mail_Location"] == null)
            Move_Mail_To_Folder(gvInbox, "inbox", lblInboxCnt, "Inbox", 1, false);
        else if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
            Move_Mail_To_Folder(grdSent, "sent", lblSent, "Sent Mails", 2, false);
        else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
            Move_Mail_To_Folder(grdView, "view", lblViewed, "Viewed Mails", 3, false);
        else
            Move_Mail_To_Folder(grdFolder, "Folder", lblInboxCnt, "Folders", 4, true);
        //
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been Moved successfully');</script>");
    }
    #endregion
    //
    #region Move_Mail_To_Folder
    private void Move_Mail_To_Folder(GridView grdMail, string sType, Label lblMailCnt, string sLblVal, int iType, bool blType)
    {
        AdminSetup adm = new AdminSetup();
        int iReturn = 0;
        foreach (GridViewRow row in grdMail.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            Label lblSlno = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked)
            {
                iReturn = adm.ChangeMailFolder(sNew_Sf_Code, Convert.ToInt16(lblSlno.Text), ddlMoved.SelectedItem.Text, 12);
            }
            //
            if (blType)
                GetFolderList();
            else
                FillMails(grdMail, sType, lblMailCnt, sLblVal, iType);
        }
        if (iReturn != 0)
        {
            divBtnDelete.Visible = false;
            divBtnForward.Visible = false;
            divBtnReply.Visible = false;
            ddlMoved.SelectedIndex = 0;
            ddlMoved.Enabled = false;
            ddlMoved.BackColor = System.Drawing.Color.Gray;
        }
    }
    #endregion
    //
    #region ddlMon_OnSelectedIndex
    protected void ddlMon_OnSelectedIndex(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);        
        DropDown(sender, e);
    }
    #endregion
    //
    #region imgSearch_Click
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        DropDown(sender, e);
    }
    #endregion
    //
    #region ddlYr_OnSelectedIndex
    protected void ddlYr_OnSelectedIndex(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        DropDown(sender, e);
    }
    #endregion
    //
    #region DropDown
    private void DropDown(object sender, EventArgs e)
    {
        if (ViewState["Current_Mail_Location"] != null)
        {
            if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
                FillMails(grdSent, "sent", lblSent, "Sent Mails", 2);

            else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
                FillMails(grdView, "view", lblViewed, "Viewed Mails", 3);

            else if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
                btnInbox_Click(sender, e);
        }
    }
    #endregion
    //
    #region Enable_Disable_Controls
    private void Enable_Disable_Controls(GridView gview, bool cbAll, bool divBtnDel)
    {
        CheckBox ChkBoxHeader = (CheckBox)gview.HeaderRow.FindControl("cbSelectAll");
        bool bExist = false;
        foreach (GridViewRow row in gview.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (cbAll)
            {
                if (ChkBoxHeader.Checked)
                {
                    ChkBoxRows.Checked = true;
                    bExist = true;
                    btnReply.Enabled = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            else
            {
                if (ChkBoxRows.Checked)
                {
                    ChkBoxRows.Checked = true;
                    bExist = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                    ddlMoved.Enabled = false;
                }
            }
        }
        if (bExist)
        {
            AdminSetup adm = new AdminSetup();
            DataSet dsFldr = new DataSet();
            if (divBtnDel)
            {
                divBtnDelete.Visible = true;
                dsFldr = adm.getMail(div_code);
                ddlMoved.DataValueField = "Move_MailFolder_Id";
                ddlMoved.DataTextField = "Move_MailFolder_Name";
                ddlMoved.DataSource = dsFldr;
                ddlMoved.DataBind();
                ddlMoved.Items.Insert(0, new ListItem("---Select---", "0"));
                ddlMoved.BackColor = System.Drawing.Color.Wheat;
                ddlMoved.Enabled = true;
            }
            else
            {
                ddlMoved.Enabled = false;
                ddlMoved.BackColor = System.Drawing.Color.Gray;
                divBtnDelete.Visible = false;
            }
            divBtnReply.Visible = true;
            divBtnForward.Visible = true;
        }
        else
        {
            ddlMoved.Enabled = false;
            ddlMoved.BackColor = System.Drawing.Color.Gray;
            divBtnDelete.Visible = false;
            divBtnReply.Visible = false;
            divBtnForward.Visible = false;
        }
    }
    #endregion
    //
    #endregion
    //
    #region FOLDER LIST & MAIL
    //
    #region FOLDER LIST
    //
    #region GetFolderList
    private void GetFolderList()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMail(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            grdClickFolder.DataSource = dsMail;
            grdClickFolder.DataBind();
            txtboxSearch.Enabled = true;
            txtboxSearch.BackColor = System.Drawing.Color.White;
        }
    }
    #endregion
    //
    #region grdClickFolder_RowCommand
    protected void grdClickFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //
        ViewState["Current_Mail_Location"] = null;
        //
        if (e.CommandName == "Folder")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            Image image = (Image)FindControl("imgFolder");
            string lectureId = lnkView.CommandArgument;
            GetMovedFolder(lnkView.Text);
            pnlFolder.Visible = true;
            pnlCompose.Visible = false;
            divInboxList.Visible = false;
            pnlSent.Visible = false;
            pnlViewInbox.Visible = false;
            pnlViewMail.Visible = false;
            divBtnDelete.Visible = false;
            divBtnReply.Visible = false;
            divBtnForward.Visible = false;
            ddlMoved.Enabled = false;
            txtboxSearch.Enabled = true;
            txtboxSearch.BackColor = System.Drawing.Color.White;
        }
    }
    #endregion
    //
    #region grdClickFolder_RowDataBound
    protected void grdClickFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.background='none repeat scroll 0 0 #d5d7de'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.ToolTip = "Click to view mail";
        }
    }
    #endregion
    //
    #endregion
    //
    #region FOLDER MAIL
    #region grdFolder_RowDataBound
    protected void grdFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.background='none repeat scroll 0 0 #d5d7de'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click to view mail";
            //
            AdminSetup adm = new AdminSetup();
            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
                e.Row.Cells[4].Visible = false;
        }
    }
    #endregion
    //
    #region grdFolder_RowCommand
    protected void grdFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            ViewState["Current_Mail_Location"] = "Folders";
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString(), false);
        }
    }
    #endregion
    //
    #region grdFolder_PageIndexChanging
    protected void grdFolder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFolder.PageIndex = e.NewPageIndex;
        FillMails(grdFolder, "Folder", lblViewed, "Folders", 4);
    }
    #endregion
    //
    #region grdFoldercbSelected_OnCheckedChanged
    protected void grdFoldercbSelected_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdFolder, true, true); 
    }
    #endregion
    //
    #region grdFoldercbChkId_OnCheckedChanged
    protected void grdFoldercbChkId_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdFolder, false, true); 
    }
    #endregion
    //
    #region GetMovedFolder
    private void GetMovedFolder(string FolderName)
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sNew_Sf_Code, div_code, "Flder", FolderName, ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "");
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            grdFolder.Style.Add("margin-top", "10px");
            grdFolder.Style.Add("margin-left", "5px");
            grdFolder.DataSource = dsMail;
            grdFolder.DataBind();
            //
            foreach (GridViewRow row in grdFolder.Rows)
            {
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    Label lblSubject = (Label)row.FindControl("lblMail_subject");
                    lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                }
            }
        }
        else
        {
            grdView.Style.Add("margin-top", "5%");
            grdView.Style.Add("margin-bottom", "5%");
            grdView.Style.Add("margin-left", "20%");
            grdFolder.DataSource = null;
            grdFolder.DataBind();
        }
    }
    #endregion
    //
    #endregion
    //
    #endregion
    //
    #region SENT
    //
    #region btnSentItem_Click
    protected void btnSentItem_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        txtboxSearch.Text = "";
        divInboxList.Visible = false;
        pnlInbox.Visible = true;
        pnlSent.Visible = true;
        pnlCompose.Visible = false;
        pnlViewMail.Visible = false;
        pnlFolder.Visible = false;
        //
        FillMails(grdSent, "sent", lblSent, "Sent Mails", 2);
        //
        ViewState["Current_Mail_Location"] = "SentItem";
    }
    //
    #endregion
    //
    #region grdSent_RowDataBound
    protected void grdSent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            Label lblOpenMailId = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.background='none repeat scroll 0 0 #d5d7de'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.ToolTip = "Click to view mail";
            //
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(lblSF_Code.Text);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblSF_Code.Text = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }
            //
            AdminSetup adm = new AdminSetup();
            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[4].Visible = false;
            }
        }
    }
    //
    #endregion
    //
    #region grdSent_RowCommand
    protected void grdSent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            ViewState["Current_Mail_Location"] = "SentItem";
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString(), false);
        }
    }
    #endregion
    // 
    #region grdSent_PageIndexChanging
    protected void grdSent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSent.PageIndex = e.NewPageIndex;
        FillMails(grdSent, "sent", lblSent, "Sent Mails", 2);
    }
    #endregion
    //
    #region grdcbSelectAll_OnCheckedChanged
    protected void grdcbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdSent, true, true);
    }
    #endregion
    //
    #region grdchkId_OnCheckedChanged
    protected void grdchkId_OnCheckedChanged(object sender, EventArgs e)
    {
        Enable_Disable_Controls(grdSent, false, true);
    }
    #endregion
    //
    #endregion
    //
    #region OPEN MAIL, REPLY, FORWARD & DELETE
    //
    #region OpenPopup
    private void OpenPopup(string sid, bool blInbox)
    {
        ViewState["pnlViewInbox"] = "true";
        ViewState["pnlInbox"] = "true";
        bool blIsAttach = false;
        //
        AdminSetup ast = new AdminSetup();
        if (Request.QueryString["inbox_id"] != null)
            dsFrom = ast.ViewMail(Convert.ToInt32(Request.QueryString["inbox_id"].ToString()));
        else
            dsFrom = ast.ViewMail(Convert.ToInt32(sid));
        //
        if (dsFrom.Tables[0].Rows.Count > 0)
        {
            lblViewFrom.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            lblViewTo.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblViewCC.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            lblViewSub.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            //
            lblViewSub.Text = lblViewSub.Text.Replace("asdf", "'");
            lblViewSent.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            lblMailBody.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            lblMailBody.Text = lblMailBody.Text.Replace("asdf", "'");
            //
            ViewState["mail_sf_from"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            ViewState["strMail_To"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ViewState["strMail_CC"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            //
            strSF_Name = Session["sf_name"].ToString();
            //
            AdminSetup adm = new AdminSetup();
            ViewState["inbox_id"] = sid;
            DataTable dt = new DataTable();
            DataRow dr = null;
            //
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Original_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("New_File_Name", typeof(string)));
            //
            DataSet dsMailAttach = adm.getMailAttach(sid);
            if (dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString() != "")
            {
                string[] str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString().Split(',');                
                Label lblSpace = new Label();
                lblSpace.Text = "&nbsp;&nbsp;&nbsp;&nbsp;";
                int iRowNo = 1;
                foreach (string sAttach_File in str)
                {
                    if (sAttach_File != "")
                    {
                        dr = dt.NewRow();
                        HyperLink hlnk_File = new HyperLink();
                        string sOriginal_Name = sAttach_File;
                        if (sAttach_File.Contains("MasterFiles/Mails/Attachment/"))
                        {
                            string[] sStrOld = sAttach_File.TrimStart().TrimEnd().Trim().Split('/');
                            if (sStrOld.Length > 1)
                            {
                                sOriginal_Name = sStrOld[sStrOld.Length - 1].ToString();
                                dr["New_File_Name"] = sOriginal_Name;
                            }
                        }
                        else
                            dr["New_File_Name"] = sOriginal_Name;
                        //                        
                        hlnk_File.NavigateUrl = "~/MasterFiles/Mails/Attachment/" + sOriginal_Name;
                        if (sOriginal_Name.Length > 20)
                        {
                            sOriginal_Name = sOriginal_Name.Remove(0, 19);
                        }
                        hlnk_File.Target = "_blank";
                        hlnk_File.ToolTip = "Click to Download";
                        hlnk_File.ForeColor = System.Drawing.Color.Yellow;                        
                        //
                        hlnk_File.Text = sOriginal_Name;
                        plcHldr_Attachments.Controls.Add(hlnk_File);
                        plcHldr_Attachments.Controls.Add(new LiteralControl("&nbsp;|&nbsp;"));
                        //
                        dr["RowNumber"] = iRowNo;
                        dr["Original_Name"] = sOriginal_Name;
                        dt.Rows.Add(dr);
                        iRowNo++;
                        blIsAttach = true;
                    }
                    //
                    /* for download all type of files--- Place inside Click event of link---
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
                    byte[] data = req.DownloadData(Server.MapPath(strURL));
                    response.BinaryWrite(data);
                    response.End();*/
                }
                imgViewAttach.Visible = true;                
            }
            //
            ViewState["Attachment_Forward"] = dt;
            //
            pnlViewInbox.Visible = true;
            pnlInbox.Visible = false;
            divInboxList.Visible = false;
            pnlCompose.Visible = false;
            //
            if (blInbox && blIsAttach)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please VIEW ATTACHED Files...');</script>");
            }
            //
        }
    }
    #endregion
    //
    #region btnDelete_Onclick
    protected void btnDelete_Onclick(Object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
                Delete_Selected_Mail(gvInbox, "inbox", lblInboxCnt, "Inbox", 1);
            else if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
                Delete_Selected_Mail(grdSent, "sent", lblSent, "Sent Mails", 2);
            else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
                Delete_Selected_Mail(grdView, "view", lblViewed, "Viewed Mails", 3);
            else if (ViewState["Current_Mail_Location"].ToString() == "Folders")
                Delete_Selected_Mail(grdFolder, "Folder", lblViewed, "Folders", 4);            
        }
        catch { }
    }
    //
    #region Delete_Selected_Mail
    private void Delete_Selected_Mail(GridView grdMails, string sType, Label lblMailCnt, string sLblVal, int iType)
    {
        AdminSetup adm = new AdminSetup();
        int iReturn = 0;
        try
        {
            foreach (GridViewRow row in grdMails.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    if (iType == 2)
                    {
                        iReturn = Delete_Sent_Item_Mail(sNew_Sf_Code, Convert.ToInt32(lblslNo.Text), div_code);
                    }
                    else
                        iReturn = adm.ChangeMailStatus(sNew_Sf_Code, Convert.ToInt32(lblslNo.Text), -1, "");
                    //
                    if (iType == 4)
                        GetFolderList();
                    else
                        FillMails(grdMails, sType, lblMailCnt, sLblVal, iType);
                }
            }
        }
        catch { }
        if (iReturn != 0)
        {
            pnlpopup.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been Deleted successfully');</script>");
        }
        //
        Enable_Disable_Controls(grdMails, false, false);
    }
    //
    #region Delete_Sent_Item_Mail
    private int Delete_Sent_Item_Mail(string sNew_Sf_Code, int iMail_Slno, string div_code)
    {
        DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
        string sQuery = "UPDATE Trans_Mail_Head SET Sent_Flag=-1 WHERE Mail_Sf_From='" + sNew_Sf_Code + "' AND Trans_Sl_No=" + iMail_Slno + " AND Division_Code=" + div_code + "";
        int iReturn = db.ExecQry(sQuery);
        return iReturn;
    }
    #endregion
    //
    #endregion
    //
    #endregion
    //
    #region btnForward_Onclick
    protected void btnForward_Onclick(object sender, EventArgs e)
    {
        try
        {
            Read_Selected_Mail(gvInbox, sender, e, 1, false);
            Read_Selected_Mail(grdSent, sender, e, 2, false);
            Read_Selected_Mail(grdView, sender, e, 3, false);
            Read_Selected_Mail(grdFolder, sender, e, 4, false);
        }
        catch { }
    }
    #endregion
    //
    #region btnReply_Onclick
    protected void btnReply_Onclick(object sender, EventArgs e)
    {
        try
        {
            ViewState["Attachment_Forward"] = null;
            ViewState["CurrentTable"] = null;
            //
            if (ViewState["Current_Mail_Location"].ToString() == "Inbox")
                Read_Selected_Mail(gvInbox, sender, e, 1, true);
            else if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
                Read_Selected_Mail(grdSent, sender, e, 2, true);
            else if (ViewState["Current_Mail_Location"].ToString() == "Viewed")
                Read_Selected_Mail(grdView, sender, e, 3, true);
            else if (ViewState["Current_Mail_Location"].ToString() == "Folders")
                Read_Selected_Mail(grdFolder, sender, e, 4, true);
            //
        }
        catch { }
    }
    //
    private void Read_Selected_Mail(GridView gViewMail, object sender, EventArgs e, int iType, bool blReply)
    {
        foreach (GridViewRow row in gViewMail.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            Label lblslNo = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked == true)
            {
                ViewState["Current_Mail_Location"] = null;
                //
                ddlMoved.Enabled = true;
                bool blIsInbox = false;
                if (iType == 1)
                    blIsInbox = true;
                //
                OpenPopup(lblslNo.Text, blIsInbox);
                //
                if (iType == 1)
                    ViewState["Current_Mail_Location"] = "Inbox";
                else if (iType == 2)
                    ViewState["Current_Mail_Location"] = "SentItem";
                else if (iType == 3)
                    ViewState["Current_Mail_Location"] = "Viewed";
                //
                if (blReply)
                    imgbtnReplyViewMail_Click(sender, e);
                else
                    imgbtnFwdViewMail_Click(sender, e);
                //
            }
        }
    }
    #endregion
    //
    #region Reply & Forward Mails
    protected void imgbtnReplyViewMail_Click(object sender, EventArgs e)
    {
        ViewState["mail_to_sf_Name"] = "";
        if (txtAddr.Text == "")
        {
            ViewState["mail_to_sf_Name"] = lblViewFrom.Text + ",";
        }
        else
        {
            ViewState["mail_to_sf_Name"] = txtAddr.Text + ",";
        }

        ViewState["mail_to_sf_code"] = ViewState["mail_sf_from"].ToString() + ",";
        //
        Message_Reply_Forward("Re: ", lblViewFrom.Text, "");
        Disable_All_Mail_Panel();
        ViewState["Attachment_Forward"] = null;
        ViewState["CurrentTable"] = null;
        SetInitialRow(true);
    }
    //
    protected void imgbtnFwdViewMail_Click(object sender, EventArgs e)
    {
        ViewState["CurrentTable"] = null;
        Message_Reply_Forward("Fw: ", "", "Forwarded");
        Disable_All_Mail_Panel();
    }
    //
    private void Disable_All_Mail_Panel()
    {
        pnlFolder.Visible = false;
        pnlViewInbox.Visible = false;
        pnlSent.Visible = false;
        pnlViewMail.Visible = false;
    }
    //
    private void Message_Reply_Forward(string sMsgTyp, string sFrm_Addr, string sMsg_Head_Type)
    {
        ViewState["from"] = "To";
        ViewState["pnlCompose"] = "true";
        if (sFrm_Addr != "")
            txtAddr.Text = sFrm_Addr;
        else
            txtAddr.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = ""; //BCC
        txtSub.Text = sMsgTyp + lblViewSub.Text;
        txtMsg.Text = "<br/><br/><hr/>------ <b><font color='darkblue'>" + sMsg_Head_Type + " Message</font></b> ------ <br/><b>From: </b>" + lblViewFrom.Text +
            "<br/><b>To: </b>" + lblViewTo.Text + "<br/><b>Sent: </b>" + lblViewSent.Text + "<br/>---------------------------------<br/><br/>" +
            lblMailBody.Text;
        if (sMsg_Head_Type == "Forwarded")
        {
            DataTable dtFrwd_Attchmnt = (DataTable)ViewState["Attachment_Forward"];
            if (dtFrwd_Attchmnt != null)
            {
                if (dtFrwd_Attchmnt.Rows.Count > 0)
                {
                    DataTable dtAll = new DataTable();
                    dtAll.Merge(dtFrwd_Attchmnt);
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt == null)
                        SetInitialRow(false);
                    dt = (DataTable)ViewState["CurrentTable"];
                    dtAll.Merge(dt);
                    grdAttchmentFiles.DataSource = dtAll;
                    grdAttchmentFiles.DataBind();
                    SetPreviousData(dtAll, true);
                }
            }
        }
        pnlCompose.Visible = true;
        pnlViewInbox.Visible = false;
        txtMsg.Focus();
    }
    #endregion
    //
    #region imgbtnDeleteViewMail_Click
    protected void imgbtnDeleteViewMail_Click(object sender, EventArgs e)
    {
        if (ViewState["inbox_id"] != null)
        {
            AdminSetup adm = new AdminSetup();
            int iRet;
            if (ViewState["Current_Mail_Location"].ToString() == "SentItem")
                iRet = Delete_Sent_Item_Mail(sNew_Sf_Code, Convert.ToInt32(ViewState["inbox_id"].ToString()), div_code);
            else
                iRet = adm.ChangeMailStatus(sNew_Sf_Code, Convert.ToInt32(ViewState["inbox_id"].ToString()), -1, "");
            //
            if (iRet > 0)
                pnlpopup.Visible = false;
            //
            txtAddr.Text = "";
            txtSub.Text = "";
            txtMsg.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            //
            foreach (ListItem item in chkFF.Items)
            {
                item.Selected = false;
            }
        }
    }
    #endregion
    //
    #endregion
    //
    #region HOME BUTTON, MAIL THEME & PROFILE PHOTO
    //
    #region UPLOAD PROFILE PHOTO
    //
    #region btnProfilePhoto_Click
    protected void btnProfilePhoto_Click(object sender, EventArgs e)
    {
        pnlProfile_Img.Visible = true;
        divMain.Attributes.Add("class", "deactive");
    }
    #endregion
    //
    #region btnPhoto_Upld_Click
    protected void btnPhoto_Upld_Click(object sender, EventArgs e)
    {
        if (upldProfile_Img.HasFile && upldProfile_Img.PostedFile.ContentType.ToLower().Contains("image"))
        {
            string[] sExts = upldProfile_Img.PostedFile.FileName.Split('.');
            string sFileName = "~/MasterFiles/Mails/Profile_Imgs/" + sNew_Sf_Code + "_" + div_code + "." + sExts[sExts.Length - 1].ToString();
            string sExt = upldProfile_Img.PostedFile.ContentType;
            upldProfile_Img.PostedFile.SaveAs(Server.MapPath(sFileName));
            //
            string sNewQry = "UPDATE Mas_Mail_Home SET Photo = '" + sFileName + "', Remarks = '" + sExt + "' WHERE Sf_Code = '" + sNew_Sf_Code + "' " +
                "AND Division_Code = " + div_code + " SELECT Photo FROM Mas_Mail_Home WHERE Sf_Code = '" + sNew_Sf_Code + "' AND Division_Code = " + div_code + "";
            DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
            try
            {
                DataTable dtPhoto = db.Exec_DataTable(sNewQry);
                if (dtPhoto.Rows.Count > 0)
                    if (dtPhoto.Rows[0]["Photo"].ToString() != "" && dtPhoto.Rows[0]["Photo"].ToString() != null)
                        imgSF.Src = dtPhoto.Rows[0]["Photo"].ToString();
                //                
                btnPhoto_Cncl_Click(sender, e);
                //
            }
            catch
            {
                upldProfile_Img.Dispose();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload Failed.. Try Again...');</script>");
            }
        }
        else
        {
            upldProfile_Img.Dispose();
            pnlProfile_Img.Visible = true;
            divMain.Attributes.Add("class", "deactive");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Valid Image File..');</script>");
        }
    }
    #endregion
    //
    #region btnPhoto_Cncl_Click
    protected void btnPhoto_Cncl_Click(object sender, EventArgs e)
    {
        upldProfile_Img.Dispose();
        pnlProfile_Img.Visible = false;
        divMain.Attributes.Remove("class");
    }
    #endregion
    //
    #endregion
    //
    #region btnHome_Click
    protected void btnHome_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ListedDR LstDoc = new ListedDR();
        DCR dr = new DCR();
        AdminSetup adm = new AdminSetup();
        TourPlan tp = new TourPlan();
        //
        DataSet dsDoc, dsDcr, dsAdmin, dsTP = new DataSet();
        //
        dsDoc = LstDoc.getListedDr_RejectList(sf_code);
        dsDcr = dr.get_DCR_Rejected_Approval(sf_code);
        dsAdmin = adm.getLeave_Reject(sf_code, 1);
        dsTP = tp.get_TP_Rejected_Approval(sf_code);
        //
        if (dsDoc.Tables[0].Rows.Count > 0 || dsDcr.Tables[0].Rows.Count > 0 || dsAdmin.Tables[0].Rows.Count > 0 || dsTP.Tables[0].Rows.Count > 0)
        {
            Response.Redirect("~/MasterFiles/Rejection_ReEntries.aspx");
        }
        else if (sf_code.StartsWith("MR"))
        {
            Response.Redirect("~/Default_MR.aspx");
        }
        else if (sf_code.StartsWith("MGR"))
        {
            Response.Redirect("~/MGR_Home.aspx");
        }
        else if (sf_Type == "3")
        {
            Session["div_code"] = div_code;
            Response.Redirect("~/BasicMaster.aspx");
        }
    }
    #endregion
    //
    #region Theme Change
    //protected void ddlTheme_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    set_Theme(ddlTheme.SelectedValue);
    //    DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
    //    if (ddlTheme.SelectedIndex != 0)
    //    {
    //        string sQuery = "UPDATE Mas_Mail_Home SET Theme=" + ddlTheme.SelectedValue + " WHERE Sf_Code='" + sNew_Sf_Code + "' AND Division_Code=" + div_code + "";
    //        db.ExecQry(sQuery);
    //    }
    //}
    //
    private void set_Theme(string sTheme_Id)
    {
        string url = "imgbg10.jpg";
        if (sTheme_Id == "0")
            url = "imgbg10.jpg";
        else if (sTheme_Id == "1")
            url = "imgbg2.jpg";
        else if (sTheme_Id == "2")
            url = "imgbg16.jpg";
        else if (sTheme_Id == "3")
            url = "imgbg3.jpg";
        else if (sTheme_Id == "4")
            url = "imgbg15.jpg";
        else if (sTheme_Id == "5")
            url = "imgbg4.jpg";
        else if (sTheme_Id == "6")
            url = "imgbg5.jpg";
        else if (sTheme_Id == "7")
            url = "imgbg6.jpg";
        else if (sTheme_Id == "8")
            url = "imgbg7.png";
        else if (sTheme_Id == "9")
            url = "imgbg8.jpg";
        else if (sTheme_Id == "10")
            url = "imgbg9.jpg";
        else if (sTheme_Id == "11")
            url = "imgbg10.jpg";
        else if (sTheme_Id == "12")
            url = "imgbg11.jpg";
        else if (sTheme_Id == "13")
            url = "imgbg12.jpg";
        else if (sTheme_Id == "14")
            url = "imgbg13.jpg";
        else if (sTheme_Id == "15")
            url = "imgbg14.jpg";
        else if (sTheme_Id == "16")
            url = "imgbg17.jpg";
        else if (sTheme_Id == "17")
            url = "imgbg18.jpg";
        else if (sTheme_Id == "18")
            url = "imgbg19.jpg";
        else if (sTheme_Id == "19")
            url = "imgbg20.jpg";
        else if (sTheme_Id == "20")
            url = "imgbg21.jpg";
        else if (sTheme_Id == "21")
            url = "imgbg22.jpg";
        else if (sTheme_Id == "22")
            url = "imgbg23.jpg";
        else if (sTheme_Id == "23")
            url = "imgbg24.jpg";
        else if (sTheme_Id == "24")
            url = "imgbg25.jpg";
        else if (sTheme_Id == "25")
            url = "imgbg26.jpg";
        else
            url = "imgbg3.jpg";
        bdy.Style.Add("background-image", "url(BgImgs/" + url + ")");
    }
    #endregion
    //
    #endregion
    //
}
#endregion