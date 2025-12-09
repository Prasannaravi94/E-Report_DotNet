using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_SalesForce : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsSF = null;
    DataSet dstype = null;
    DataSet dsState = null;
    DataSet dsSall = null;
    string sState = string.Empty;
    string strSfCode = string.Empty;
    string div_code = string.Empty;
    string sDesSName = string.Empty;
    string division_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sfcode = string.Empty;
    string sf_hq = string.Empty;
    string sfreason = string.Empty;
    DataSet dsSalesForce = null;
    string subdivision_code = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string sub_division = string.Empty;
    string reporting_to = string.Empty;
    bool isManager = false;
    bool isCreate = true;
    string divvalue = string.Empty;
    int iIndex = -1;
    string sChkLocation = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string usrname = string.Empty;
    string desname = string.Empty;
    string state = string.Empty;
    string Reporting_To_SF = string.Empty;
    string state_code = string.Empty;
    string type = string.Empty;
    string Effective_Date = string.Empty;
    string joiningdate = string.Empty;
    string UserDefin = string.Empty;
    string Sf_emp_id = string.Empty;
    string Vac_sfcode = string.Empty;
    string from_division = string.Empty;
    string from_designation = string.Empty;
    string Designation_Name = string.Empty;
    string Promote_Mode = string.Empty;
    string DePromote_Mode = string.Empty;
    string Reporting_To_Manager = string.Empty;
    string sf_hold = string.Empty;
    string sf_hold_realse = string.Empty;
    string make_vac = string.Empty;

    int Trans_Sl_No_From;
    int Detail_Trans_Sl_No;
    DataSet da = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        sfcode = Request.QueryString["sfcode"];
        sfreason = Request.QueryString["sfreason"];
        sf_hq = Request.QueryString["sf_hq"];
        sf_type = Request.QueryString["sf_type"];
        sfname = Request.QueryString["sfname"];
        usrname = Request.QueryString["sfusername"];
        desname = Request.QueryString["desgname"];
        state = Request.QueryString["state"];
        Reporting_To_SF = Request.QueryString["Reporting_To_SF"];
        state_code = Request.QueryString["state_code"];
        Designation_Name = Request.QueryString["Designation_Name"];
        Promote_Mode = Request.QueryString["Promote_Mode"];
        Reporting_To_Manager = Request.QueryString["Reporting_To_Manager"];
        DePromote_Mode = Request.QueryString["DePromote_Mode"];

        sf_hold = Request.QueryString["sf_hold"];
        sf_hold_realse = Request.QueryString["sf_hold_realse"];
        make_vac = Request.QueryString["make_vac"];

        //rdoMode = Request.QueryString["rdoMode"];

        //if (usrname != "" || desname != "")
        //{
        //    Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
        //}
        div_code = Session["div_code"].ToString();
        //txtFieldForceName.Focus();
        txtUserName.Enabled = false;
        RblSta.Enabled = false;
        if (!Page.IsPostBack)
        {
            menu1.Title = "FieldForce Master";
            FillState(div_code);
            FillType();
            ViewState["Reporting_To"] = "";
            ViewState["Rep_StartDate"] = "";
            //menu1.FindControl("btnBack").Visible = true;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getDesignation_div(div_code);
            txtDesignation.DataTextField = "Name";
            txtDesignation.DataValueField = "Designation_Code";
            txtDesignation.DataSource = dsSalesForce;
            txtDesignation.DataBind();
            Username();
            if ((sfcode != "") && (sfcode != null))
            {
                LoadData(sfcode);
                isCreate = false;
                ddlFieldForceType.Enabled = false;
                txtDesignation.Enabled = true;
                getLastDCR();
                lblLastDCRDate.Visible = true;
                lblLastDCRDate.Style.Add("Color", "Red");
                txtDCRDate.Visible = true;
                txtTPDCRStartDate.Enabled = false;

                txtFieldForceName.Enabled = false;
                //ddlFieldForceType_SelectedIndexChanged(sender,e);
            }
            if ((sf_hq != "") && (sf_hq != null))
            {


                if (sf_hq != "Vacant")
                {

                    int iReturn = sf.check_vac_ex(sfcode);
                    if (div_code == "104")
                    {
                        iReturn = 0;
                    }


                    if (iReturn > 0)
                    {


                        var today = DateTime.Today;
                        var month = new DateTime(today.Year, today.Month, 1);
                        var first = month.AddMonths(-1);
                        var last_month_date = month.AddDays(-1);

                        int last_month = last_month_date.Month;
                        int last_month_year = last_month_date.Year;

                        DateTime dt = DateTime.Now;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;

                        string type = "Vacant";

                        if ((Designation_Name != "") && (Designation_Name != null))
                        {
                            type = "Promote";
                        }

                        else if ((Reporting_To_Manager != "") && (Reporting_To_Manager != null))
                        {
                            type = "DePromote";
                        }

                        else if ((sf_hold != "") && (sf_hold != null))
                        {
                            type = "Hold";
                        }


                        if (sfcode.Contains("MR"))
                        {

                            if (iReturn == 5)
                            {
                                if (div_code == "19" || div_code == "20")
                                {
                                    Response.Redirect("MR/RptAutoExpense_rowwise_Anthem.aspx?type=" + type + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
                                }
                                else
                                {
                                    Response.Redirect("MR/RptAutoExpense_rowwise.aspx?type=" + type + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
                                }

                            }
                            else if (iReturn == 6)
                            {

                                if (div_code == "19" || div_code == "20")
                                {
                                    Response.Redirect("MR/RptAutoExpense_rowwise_Anthem.aspx?type=" + type + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
                                }
                                else
                                {
                                    Response.Redirect("MR/RptAutoExpense_rowwise.aspx?type=" + type + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
                                }


                            }
                        }

                        //else if (sfcode.Contains("MGR"))
                        //{
                        //    if (iReturn == 5)
                        //    {


                        //        Response.Redirect("MGR/RptAutoExpense_Mgr.aspx?type=" + type + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);

                        //    }
                        //    else if (iReturn == 6)
                        //    {



                        //        Response.Redirect("MGR/RptAutoExpense_Mgr.aspx?type=" + type + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);

                        //    }
                        //}
                    }
                }

            }
            if ((state != "") && (state != null))// Vacant Edit
            {
                RblSta.SelectedValue = "1";
                ddlFieldForceType.Enabled = false;
                txtTPDCRStartDate.Enabled = false;
                txtFieldForceName.Enabled = false;
                txtJoingingDate.Enabled = false;
                UsrDfd_UserName.Enabled = true;
            }
            if ((sfname != "") && (sfname != null))
            {
                //  Disableall();
                // chkVacant.Checked = false;
                getLastDCR();
                RblSta.SelectedValue = "0";
                btnSubmit.Text = "Activate";
                btnSave.Text = "Activate";
                ddlFieldForceType.Enabled = false;
                lblLastDCRDate.Visible = true;
                txtDCRDate.Visible = true;
                // lblTPDCRStartDate.Text = "*Vacant Starting Date";
                rblVacantBlock.Visible = true;
                lblVacantBlock.Visible = true;
                txtJoingingDate.Text = "";
                txtTPDCRStartDate.Text = "";
                lblJoingingDate.Style.Add("Color", "Magenta");
                lblTPDCRStartDate.Style.Add("Color", "Magenta");
                lblJoingingDate.Font.Bold = true;
                lblTPDCRStartDate.Font.Bold = true;

                txtFieldForceName.Enabled = true;
                txtTPDCRStartDate.Enabled = true;
                txtPassword.Text = "";

                hdnrejoin_mode.Value = "active_mode";
                hdnname_check.Value = txtFieldForceName.Text;


                ddlBankName.SelectedIndex = -1;
                txtAcountNo.Text = "";
                txtIFSC.Text = "";

                //Added by Vasanthi.P on 18-Dec-24--begin

                if (state_code == "" || state_code == null)
                {


                    txtDOB.Text = "";
                    txtDOW.Text = "";
                    txtPhone1.Text = "";
                    txtMobile.Text = "";
                    txtEMail.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtCityPin.Text = "";
                    txtPhone2.Text = "";
                    txtPerAddress1.Text = "";
                    txtPerAddress2.Text = "";
                    txtPerCityPin.Text = "";
                    txtPhone.Text = "";
                }

              //  End

            }

            if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null)) // To Vacant
            {

                Disableall();
                getLastDCR();
                RblSta.SelectedValue = "1";
                lblLastDCRDate.Visible = true;
                lblLastDCRDate.Style.Add("Color", "Red");
                lblTPDCRStartDate.Text = "*Resigned Date";
                lblTPDCRStartDate.Style.Add("Color", "Red");
                rblVacantBlock.Visible = true;
                lblVacantBlock.Visible = true;
                lblVacantBlock.Style.Add("Color", "Red");
                txtDCRDate.Visible = true;
                //  txtDCRDate.Focus();                
                // txtReason.Enabled = false;
                btnSubmit.Text = "Click here to make vacant";
                btnSubmit.Style.Add("width", "175px");
                btnSave.Text = "Click here to make vacant";
                btnSave.Style.Add("width", "175px");
                btnSubmit.Attributes.Add("OnClick", "javaScript: return Vacant();");

                ViewState["to_vacant"] = "1";

                lblVacantBlock.Text = "Status Reason";
                lbleffective.Visible = true;
                txteffe.Visible = true;
                txtTPDCRStartDate.Text = txtDCRDate.Text;
                DateTime joingdate = Convert.ToDateTime(txtJoingingDate.Text);
                DateTime startdate = Convert.ToDateTime(txtTPDCRStartDate.Text);
                if (joingdate > startdate)
                {
                    txtTPDCRStartDate.Text = txtJoingingDate.Text;
                    txtDCRDate.Visible = false;
                    txtDCRDate.Text = txtTPDCRStartDate.Text;
                }
                rblVacantBlock.SelectedValue = "R";
                //btnSubmit.Attributes.Add("onClick()", "disp_confirm();");
            }

            if ((sf_hold != "") && (sf_hold != null))
            {
                rblVacantBlock.SelectedValue = "H";
                btnSubmit.Text = "Click here to Hold";
                btnSave.Text = "Click here to Hold";
                lblReason.Visible = true;
                txtReason.Visible = true;
                lblReason.Text = "Reason for Holding th ID";
                lblReason.Style.Add("Color", "Magenta");


            }

            if ((usrname != "") && (usrname != null) || (desname != "") && (desname != null)) // Promo / De-Promo
            {

                Disableall();

                SalesForce sal = new SalesForce();
                dsSalesForce = sal.getLastDCR(sfcode);
                if (dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                {

                    DateTime ldcrdate = Convert.ToDateTime(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    txteffe.Text = ldcrdate.ToString("dd/MM/yyyy");
                }

                txteffe.Enabled = false;
                txtTPDCRStartDate.Enabled = false;

                lblReporting.Style.Add("Color", "Red");
                ddlReporting.Enabled = true;
                lblDesignation.Style.Add("Color", "Red");
                txtDesignation.Focus();
                txtDesignation.Enabled = true;
                lbleffective.Visible = true;
                txteffe.Visible = true;


                if (sfcode.Contains("MGR"))
                {
                    lblmode.Visible = true;
                    rdoMode.Visible = true;
                    txteffe.Enabled = true;
                }


                if ((usrname != "") && (usrname != null))
                {
                    txtPassword.Enabled = true;
                    btnSubmit.Text = "Promote";
                    btnSave.Text = "Promote";
                    btnSubmit.Attributes.Add("OnClick", "javaScript: return Promote();");
                    // Load only designation above the current level

                    //SalesForce sfde = new SalesForce();
                    //dstype = sfde.CheckSFType(sfcode);
                    //if (dstype.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                    //{
                    //    dsSalesForce = sfde.getDesignationtoPromoteMR(Convert.ToInt16(ViewState["cur_des"].ToString()));
                    //}
                    //else
                    //{
                    //    dsSalesForce = sfde.getDesignationtoPromoteMGR(Convert.ToInt16(ViewState["cur_des"].ToString()));
                    //}
                    //txtDesignation.DataTextField = "Name";
                    //txtDesignation.DataValueField = "Designation_Code";
                    //txtDesignation.DataSource = dsSalesForce;
                    //txtDesignation.DataBind();
                    Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
                    ViewState["promote"] = "1";
                }
                if ((desname != "") && (desname != null))
                {
                    txtPassword.Enabled = true;
                    btnSubmit.Text = "De-Promote";
                    btnSave.Text = "De-Promote";
                    btnSubmit.Attributes.Add("OnClick", "javaScript: return DePromote();");
                    // Load only designation below the current level
                    //SalesForce sfde = new SalesForce();
                    //dstype = sfde.CheckSFType(sfcode);
                    //if (dstype.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                    //{
                    //    dsSalesForce = sfde.getDesignationDePromoteMR(Convert.ToInt16(ViewState["cur_des"].ToString()));
                    //}
                    //else
                    //{
                    //    dsSalesForce = sfde.getDesignationDePromoteMGR(Convert.ToInt16(ViewState["cur_des"].ToString()));
                    //}

                    //txtDesignation.DataTextField = "Name";
                    //txtDesignation.DataValueField = "Designation_Code";
                    //txtDesignation.DataSource = dsSalesForce;
                    //txtDesignation.DataBind();
                    Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
                    ViewState["depromote"] = "1";
                }

            }
            if ((sf_type != "") && (sf_type != null))
            {

                Disableall();
                RblSta.SelectedValue = "2";
                lblReason.Visible = true;
                lblReason.Style.Add("Color", "Magenta");
                txtReason.Visible = true;
                txtReason.Focus();
                btnSubmit.Text = "Click here to Block";
                btnSubmit.Style.Add("width", "175px");
                btnSave.Text = "Click here to Block";
                btnSave.Style.Add("width", "175px");
                txtReason.Focus();
                btnSubmit.Attributes.Add("OnClick", "javaScript: return Block();");
                txtTPDCRStartDate.Enabled = false;
            }
            if (sfreason != "" && sfreason != null)
            {
                Disableall();
                txtReason.Enabled = false;
                txtReason.Visible = true;
                lblReason.Visible = true;
                lblReason.Style.Add("Color", "Magenta");
                btnSubmit.Text = "Activate";
                btnSave.Text = "Activate";
            }



            if ((Promote_Mode == "1") || (DePromote_Mode == "1"))
            {
                if (DePromote_Mode == "1")
                {
                    menu1.Title = "Manager to Base Level De-Promotion";
                }
                else if (Promote_Mode == "1")
                {
                    menu1.Title = "Base Level to Manager Promotion";
                }
                Vac_sfcode = Session["Vac_sfcode"].ToString();
                ViewState["Vac_sfcode"] = Vac_sfcode;
                SalesForce sal = new SalesForce();
                dsSall = sal.getVac_info(Vac_sfcode);
                if (dsSall.Tables[0].Rows.Count > 0)
                {
                    txtDOB.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDOW.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtMobile.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEMail.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtPerAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtPerAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    txtPerCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    txtPhone.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    txtPassword.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();

                    //txtBankName.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                    ddlBankName.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                    txtAcountNo.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                    txtIFSC.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();

                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    from_division = dsSall.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    ViewState["from_division"] = from_division;
                    from_designation = dsSall.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    ViewState["from_designation"] = from_designation;

                    ViewState["sf_emp_id"] = dsSall.Tables[0].Rows[0]["sf_emp_id"].ToString();
                }

                txtFieldForceName.Text = sfname;
                //Effective_Date = Session["Effective_Date"].ToString();
                //txtTPDCRStartDate.Text = Effective_Date;
                txtJoingingDate.Text = Session["joiningdate"].ToString();
                //txtJoingingDate.Text = joiningdate;
                lbluserdefi.Visible = true;
                lbluserdefi.Text = Session["UserDefin"].ToString();
                //lbluserdefi.Text = UserDefin;
                lbluserdefi.Font.Bold = true;
                lbluserdefi.Style.Add("Color", "Red");
                txtEmployeeID.Text = Session["Sf_emp_id"].ToString();
                //txtEmployeeID.Text = Sf_emp_id;
                txtTPDCRStartDate.Text = Session["Effiective_Date"].ToString();
                lbloldusername.Visible = true;
                RblSta.Visible = false;
                Label7.Visible = false;
                //lbleffective.Visible = true;
                //txteffe.Visible = true;

            }
            else if (Promote_Mode == "0" || DePromote_Mode == "0")
            {
                if (DePromote_Mode == "0")
                {
                    menu1.Title = "Manager to Base Level De-Promotion";

                    ddlFieldForceType.SelectedValue = "1";
                    ddlFieldForceType.Enabled = false;
                }
                else if (Promote_Mode == "0")
                {
                    menu1.Title = "Base Level to Manager Promotion";

                    ddlFieldForceType.SelectedValue = "2";
                    ddlFieldForceType.Enabled = false;
                }

                Vac_sfcode = Session["Vac_sfcode"].ToString();
                ViewState["Vac_sfcode"] = Vac_sfcode;
                SalesForce sal = new SalesForce();
                dsSall = sal.getVac_info(Vac_sfcode);
                if (dsSall.Tables[0].Rows.Count > 0)
                {
                    txtDOB.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDOW.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtMobile.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEMail.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtPerAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtPerAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    txtPerCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    txtPhone.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    txtPassword.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();

                    //txtBankName.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                    ddlBankName.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                    txtAcountNo.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                    txtIFSC.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();

                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    from_division = dsSall.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    ViewState["from_division"] = from_division;
                    from_designation = dsSall.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    ViewState["from_designation"] = from_designation;
                    ddlState.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    txtHQ.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    ddlReporting.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                    subdivision_code = dsSall.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                    txtDesignation.Enabled = true;

                    ViewState["sf_emp_id"] = dsSall.Tables[0].Rows[0]["sf_emp_id"].ToString();


                }


                txtFieldForceName.Text = Session["Fieldforce_Name"].ToString();

                //ddlFieldForceType.SelectedValue = type;
                //ddlFieldForceType.SelectedValue = "2";
                //ddlFieldForceType.Enabled = false;
                txtJoingingDate.Text = Session["joiningdate"].ToString();
                //txtJoingingDate.Text = joiningdate;
                lbluserdefi.Visible = true;
                lbluserdefi.Text = Session["UserDefin"].ToString();
                //lbluserdefi.Text = UserDefin;
                lbluserdefi.Font.Bold = true;
                lbluserdefi.Style.Add("Color", "Red");
                txtEmployeeID.Text = Session["Sf_emp_id"].ToString();
                //txtEmployeeID.Text = Sf_emp_id;
                ddlFieldForceType.Enabled = false;
                txtTPDCRStartDate.Text = Session["Effiective_Date"].ToString();
                lbloldusername.Visible = true;
                lblTPDCRStartDate.Style.Add("Color", "Magenta");
                lblTPDCRStartDate.Font.Bold = true;
                lblDesignation.Style.Add("Color", "Magenta");
                lblDesignation.Font.Bold = true;
                //lblDesignation.Attributes.Add("style", "text-decoration:blink");
                loaddes();
            }

            if (state_code != "" && state_code != null)
            {
                Disableall();
                txtJoingingDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtTPDCRStartDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                txtTPDCRStartDate.Enabled = false;
                UsrDfd_UserName.Enabled = false;
                lblJoingingDate.Style.Add("Color", "Black");
                lblTPDCRStartDate.Style.Add("Color", "Black");
                lblJoingingDate.Font.Bold = false;
                lblTPDCRStartDate.Font.Bold = false;
                txtPassword.Enabled = true;

                hdnrejoin_mode.Value = "rejoin_mode";
                ddlBankName.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
                txtAcountNo.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(37).ToString(); // Bank Account Number 
                txtIFSC.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(38).ToString(); // Bank IFSC Code 

            }

            if ((Designation_Name != "") && (Designation_Name != null))
            {
                menu1.Title = "Base Level to Manager Promotion";
                //lblTPDCRStartDate.Text = "Promoted Date";
                btnSubmit.Text = "Click here to Promote";
                btnSave.Text = "Click here to Promote";
                btnSubmit.Attributes.Add("OnClick", "javaScript: return BaseLevelPromote();");
                lblmode.Visible = true;
                rdoMode.Visible = true;
                UsrDfd_UserName.Enabled = false;
                lbleffective.Text = "Promoted Effecitve Date";
                lbleffective.Style.Add("Color", "Red");

            }
            if ((Reporting_To_Manager != "") && (Reporting_To_Manager != null))
            {
                menu1.Title = "Manager to Base Level De-Promotion";
                //lblTPDCRStartDate.Text = "Promoted Date";
                btnSubmit.Text = "Click here to De-Promotion";
                btnSave.Text = "Click here to De-Promotion";
                btnSubmit.Attributes.Add("OnClick", "javaScript: return DePromote();");
                lblmode.Visible = true;
                rdoMode.Visible = true;
                UsrDfd_UserName.Enabled = false;
                lbleffective.Text = "De-Promoted Effecitve Date";
                lbleffective.Style.Add("Color", "Red");
                lblreplace.Text = "Replacement For (Only Vacant BaseLevel ID's)";

            }



            if ((sf_hold_realse != "") && (sf_hold_realse != null))
            {
                txtFieldForceName.Enabled = true;
                txtTPDCRStartDate.Enabled = true;
                txtJoingingDate.Enabled = true;
                txtPassword.Text = "";
                txtJoingingDate.Text = "";
                txtTPDCRStartDate.Text = "";
                lblJoingingDate.Style.Add("Color", "Magenta");
                lblTPDCRStartDate.Style.Add("Color", "Magenta");
                lblJoingingDate.Font.Bold = true;
                lblTPDCRStartDate.Font.Bold = true;
                UsrDfd_UserName.Enabled = true;
                txtEmployeeID.Enabled = true;
                ddlReporting.Enabled = true;
            }



            if (ddlState.SelectedIndex == 0)
            {
                string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
                txtUserName.Text = strtxtUser;

            }

            FillCheckBoxList();

        }
    }
    private void getLastDCR()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getLastDCR(sfcode);
        if (dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
        {
            DateTime ldcrdate = Convert.ToDateTime(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ldcrdate = ldcrdate.AddDays(-1);
            DateTime joingdate = Convert.ToDateTime(txtJoingingDate.Text);
            DateTime startdate = ldcrdate;
            if (joingdate > startdate)
            {

                txtDCRDate.Text = txtTPDCRStartDate.Text;
            }
            else
            {
                txtDCRDate.Text = ldcrdate.ToString("dd/MM/yyyy");
            }

            // txtDCRDate.Text = ldcrdate.ToString("dd/MM/yyyy");
        }
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

    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        chkboxLocation.DataTextField = "subdivision_name";
        chkboxLocation.DataSource = dsSubDivision;
        chkboxLocation.DataBind();
        string[] subdiv;
        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }
    }

    private void Disableall()
    {

        txtAddress1.Enabled = false;
        txtAddress2.Enabled = false;
        txtCityPin.Enabled = false;
        txtDOB.Enabled = false;
        txtDOW.Enabled = false;
        RblSta.Enabled = false;
        txtEMail.Enabled = false;
        txtEmployeeID.Enabled = false;
        txtFieldForceName.Enabled = false;
        txtHQ.Enabled = false;
        txtJoingingDate.Enabled = false;
        txtMobile.Enabled = false;
        txtPassword.Enabled = false;
        txtPerAddress1.Enabled = false;
        txtPerAddress2.Enabled = false;
        txtPerCityPin.Enabled = false;
        txtPhone.Enabled = false;
        txtPhone1.Enabled = false;
        txtPhone2.Enabled = false;
        //txtShortName.Enabled = false;       
        txtUserName.Enabled = false;
        ddlFieldForceType.Enabled = false;
        ddlReporting.Enabled = false;
        txtDesignation.Enabled = false;
        //ddlSFCategory.Enabled = false;
        ddlState.Enabled = false;
        chkboxLocation.Enabled = false;

        ddlBankName.Enabled = false;
        txtAcountNo.Enabled = false;
        txtIFSC.Enabled = false;

    }

    private void LoadData(string sfcode)
    {
        SalesForce sf = new SalesForce();
        dsSF = sf.getSalesForce(sfcode);
        if (dsSF.Tables[0].Rows.Count > 0)
        {
            txtFieldForceName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); //sf_name 
            txtUserName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); //Sf_UserName 
            ViewState["usernamecheck"] = txtUserName.Text;
            txtPassword.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); //Sf_Password  

            string strPassword;
            strPassword = txtPassword.Text;
            txtPassword.Attributes.Add("value", strPassword);

            ddlState.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(); //state_code 
            ViewState["statecheck"] = ddlState.SelectedValue;
            txtAddress1.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); // sf_contact_address1 
            txtAddress2.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(9).ToString(); // sf_contact_address2 
            txtCityPin.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(10).ToString(); // sf_contact_citypin 
            txtEMail.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(11).ToString(); // sf_email 
            txtMobile.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(12).ToString(); // sf_mobile 
            txtPerAddress1.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); // sf_perm_address1 
            txtPerAddress2.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(15).ToString(); // sf_perm_address2 
            txtPerCityPin.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); // sf_perm_citypin 
            txtPhone.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); // sf_perm_contact 
            txtHQ.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(19).ToString(); // sf_hq 
            txtJoingingDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); // Joining Date 
            txtTPDCRStartDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(); // TP DCR Date
            ViewState["Rep_StartDate"] = txtTPDCRStartDate.Text;
            txtDCRDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            txtDOB.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(13).ToString(); // DOB
            txtDOW.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); // DOW
            ddlReporting.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); //Reporting To 

            /* txtBankName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();*/ // Bank Name 
            ddlBankName.SelectedValue= dsSF.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
            txtAcountNo.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(37).ToString(); // Bank Account Number 
            txtIFSC.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(38).ToString(); // Bank IFSC Code 

            foreach (ListItem item in ddlReporting.Items)
            {
                if (item.Value == ddlReporting.SelectedValue)
                {
                    if (item.Attributes["SF_VacantBlock"] == "H")
                    {
                        ddlReporting.Enabled = false;
                    }
                }
            }


            ViewState["Reporting_To"] = ddlReporting.SelectedValue;
            txtEmployeeID.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(25).ToString(); // DOB
            //txtShortName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(28).ToString(); // DOB
            ddlFieldForceType.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(28).ToString(); // DOB
            loaddes();
            txtDesignation.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(27).ToString(); // DOB
            ViewState["checkdesig"] = txtDesignation.SelectedValue;
            ViewState["cur_des"] = txtDesignation.SelectedValue.ToString();

            subdivision_code = dsSF.Tables[0].Rows[0].ItemArray.GetValue(29).ToString(); // Sub Division
            rblVacantBlock.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
            txtReason.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            division_code = dsSF.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            UsrDfd_UserName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();

            ddlfieldtype.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
            ddlcatg.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
            //ViewState["checkUserdefined"] = UsrDfd_UserName.Text;
        }
    }

    //private void FillDesignation()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getDesignation();
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {

    //        txtDesignation.DataTextField = "Designation_Name";
    //        txtDesignation.DataValueField = "Designation_Code";
    //        txtDesignation.DataSource = dsSalesForce;
    //        txtDesignation.DataBind();
    //    }
    //}
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
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
            dsState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }

    private void FillType()
    {
        SalesForce sf = new SalesForce();

        dsSF = sf.getSFType(div_code);

        //dsSF = sf.sp_UserList_MGR_SFC(div_code, "admin");

        if (dsSF.Tables[0].Rows.Count > 0)
        {
            //ddlReporting.DataTextField = "sf_name";
            //ddlReporting.DataValueField = "Sf_Code";
            //ddlReporting.DataSource = dsSF;
            //ddlReporting.DataBind();

            foreach (DataRow dataRow in dsSF.Tables[0].Rows)
            {
                ListItem liReport = new ListItem();
                liReport.Value = dataRow["Sf_Code"].ToString();
                liReport.Text = dataRow["sf_name"].ToString();

                liReport.Attributes["SF_VacantBlock"] = dataRow["SF_VacantBlock"].ToString();

                ddlReporting.Items.Add(liReport);
            }
        }
    }
    //private void GetState()
    //{
    //    SalesForce sf = new SalesForce();

    //    dsSF = sf.getSFStateType(ddlState.SelectedIndex.ToString(),div_code);

    //    if (dsSF.Tables[0].Rows.Count > 0)
    //    {
    //        ddlReporting.DataTextField = "sf_name";
    //        ddlReporting.DataValueField = "Sf_Code";
    //        ddlReporting.DataSource = dsSF;
    //        ddlReporting.DataBind();
    //    }
    //}

    private void ResetALL()
    {
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCityPin.Text = "";
        //txtDCRDate.Text = "";
        txtDesignation.SelectedIndex = -1;
        txtDOB.Text = "";
        txtDOW.Text = "";
        txtEMail.Text = "";
        txtEmployeeID.Text = "";
        txtFieldForceName.Text = "";
        txtHQ.Text = "";
        txtJoingingDate.Text = "";
        txtMobile.Text = "";
        txtPassword.Text = "";
        txtPerAddress1.Text = "";
        txtPerAddress2.Text = "";
        txtPerCityPin.Text = "";
        txtPhone.Text = "";
        // txtReason.Text = "";
        // txtShortName.Text = "";
        txtTPDCRStartDate.Text = "";
        txtUserName.Text = "";
        UsrDfd_UserName.Text = "";

        //txtBankName.Text = "";  
       ddlBankName.SelectedIndex = -1;
        txtAcountNo.Text = "";  
        txtIFSC.Text = "";  

        ddlState.SelectedIndex = -1;
        ddlReporting.SelectedIndex = -1;
        ddlFieldForceType.SelectedIndex = -1;
        ddlcatg.SelectedIndex = -1;
        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtJoingingDate.Text != "" && txtTPDCRStartDate.Text != "")
        {
            DateTime joingdate = Convert.ToDateTime(txtJoingingDate.Text);
            DateTime startdate = Convert.ToDateTime(txtTPDCRStartDate.Text);

            if (joingdate > startdate)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Report Starting Date must be greater than Joining Date');</script>");
                txtTPDCRStartDate.Text = "";
                txtTPDCRStartDate.Focus();
            }
            else
            {

                SalesForce sal = new SalesForce();
                dsSalesForce = sal.CheckReporting(sfcode, ddlReporting.SelectedValue, div_code);

                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reverse  Reporting is Not Possible');</script>");
                    ddlReporting.SelectedValue = "";
                    ddlReporting.Focus();
                }

                else
                {

                    if (sfcode == ddlReporting.SelectedValue)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Self Reporting is Not Possible');</script>");
                        ddlReporting.SelectedValue = "";
                        ddlReporting.Focus();
                    }
                    else
                    {

                        bool check = false;
                        string strQry = string.Empty;
                        DB_EReporting db_ER = new DB_EReporting();

                        DataSet dsSFf = null;
                        if ((sf_hq == "") || (sf_hq == null))
                        {


                            strQry = " SELECT sf_tp_active_dt FROM  Mas_Salesforce " +
                                     " WHERE SF_Code='" + sfcode + "' ";
                            dsSFf = db_ER.Exec_DataSet(strQry);

                            if (dsSFf.Tables[0].Rows.Count > 0)
                            {
                                DateTime startdate2 = Convert.ToDateTime(txtTPDCRStartDate.Text);
                                DateTime last_date = Convert.ToDateTime(dsSFf.Tables[0].Rows[0]["sf_tp_active_dt"]);

                                if (last_date > startdate2)
                                {

                                    SalesForce dss = new SalesForce();

                                    check = dss.IsDcrStarted(sfcode);

                                    if (check == true)
                                    {
                                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Already Started So Not Possible to change Start Date');</script>");
                                    }
                                }
                            }
                        }


                        if (check == false)
                        {

                            System.Threading.Thread.Sleep(time);
                            SalesForce sf = new SalesForce();
                            int iReturn = -1;
                            DateTime dtDOB;
                            DateTime dtDOW;
                            string strDOB = string.Empty;
                            string strDOW = string.Empty;

                            if (txtDOB.Text != "")
                            {
                                dtDOB = Convert.ToDateTime(txtDOB.Text);
                                strDOB = dtDOB.Month + "-" + dtDOB.Day + "-" + dtDOB.Year;
                            }
                            else
                            {
                                strDOB = "";
                            }
                            if (txtDOW.Text != "")
                            {
                                dtDOW = Convert.ToDateTime(txtDOW.Text.ToString());
                                strDOW = dtDOW.Month + "-" + dtDOW.Day + "-" + dtDOW.Year;

                            }
                            else
                            {
                                strDOW = "";
                            }

                            for (int i = 0; i < chkboxLocation.Items.Count; i++)
                            {
                                if (chkboxLocation.Items[i].Selected)
                                {
                                    sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
                                }
                            }

                            if (Convert.ToInt32(ddlFieldForceType.SelectedValue) == 2)
                            {
                                isManager = true;
                                reporting_to = ddlReporting.SelectedValue.ToString();
                            }
                            //txtJoingingDate.Text = txtJoingingDate.ToString ("dd-MMM-yyyy");
                            if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null))
                            {
                                // if ((Reporting_To_SF != "") && (Reporting_To_SF != null))
                                if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null))
                                {
                                    iReturn = sf.Activate(sfcode);
                                }
                                if ((sf_hold != "") && (sf_hold != null))
                                {
                                    string[] Desig_name = txtDesignation.SelectedItem.Text.Split('/');

                                    iReturn = sf.Block(sfcode, txtReason.Text, div_code, txtFieldForceName.Text, txtJoingingDate.Text, ddlReporting.SelectedValue, ddlReporting.SelectedItem.Text, txtHQ.Text, txtEmployeeID.Text.Trim(), Desig_name[0], "H");
                                }

                                if ((make_vac != "") && (make_vac != null))
                                {
                                    iReturn = sf.Activate(sfcode);
                                }


                                iReturn = sf.vbRecordUpdate(sfcode, rblVacantBlock.SelectedValue, txtDCRDate.Text, txtTPDCRStartDate.Text, txteffe.Text, UsrDfd_UserName.Text, div_code, txtReason.Text, txtEmployeeID.Text.Trim());
                            }
                            else if ((sf_type != "") && (sf_type != null))
                            {
                                //iReturn = sf.Block(sfcode, txtReason.Text, div_code);
                                string[] Desig_name = txtDesignation.SelectedItem.Text.Split('/');

                                iReturn = sf.Block(sfcode, txtReason.Text, div_code, txtFieldForceName.Text, txtJoingingDate.Text, ddlReporting.SelectedValue, ddlReporting.SelectedItem.Text, txtHQ.Text, txtEmployeeID.Text.Trim(), Desig_name[0], "B");
                            }
                            else if ((sfreason != "") && (sfreason != null))
                            {

                                iReturn = sf.Activate(sfcode);
                                if (iReturn > 0)
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');window.location='BlockSFList.aspx';</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
                                }
                            }
                            else if ((sfcode != "") && (sfcode != null))// Update SalesForce
                            {
                                Designation Desig = new Designation();
                                DataSet ds = Desig.getDesignationEd(txtDesignation.SelectedValue, div_code);
                                if (ds.Tables[0].Rows.Count > 0)
                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                // Added by Sridevi - Rep Starting Date Check
                                if (ViewState["Rep_StartDate"] != null)
                                {
                                    DateTime ext_Rep_date;
                                    DateTime Upd_Rep_date;
                                    bool isRepSt = false;
                                    ext_Rep_date = Convert.ToDateTime(ViewState["Rep_StartDate"].ToString());
                                    Upd_Rep_date = Convert.ToDateTime(txtTPDCRStartDate.Text);
                                    if (ext_Rep_date != Upd_Rep_date)
                                    {
                                        if (Upd_Rep_date < ext_Rep_date)
                                        {
                                            SalesForce ds1 = new SalesForce();
                                            bool isD = false;
                                            bool isT = false;
                                            isD = ds1.IsDcrStarted(sfcode);
                                            if (isD == false)
                                            {
                                                isT = ds1.IsTpStarted(sfcode);
                                                if (isT == true)
                                                {
                                                    isRepSt = true;
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = true;
                                            }
                                        }
                                        if (isRepSt == true)
                                        {
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting already Started. Kindly Enter Valid Report Start Date');</script>");
                                            txtTPDCRStartDate.Text = ViewState["Rep_StartDate"].ToString();//Addded by Vasanthi.P (not allowed to change Report Start Date)
                                        }
                                        else
                                        {
                                            iReturn = sf.Rep_StDateRecordUpdate(sfcode, Upd_Rep_date, sfname);
                                        }
                                    }
                                }
                                // Rep StartDate Check ENds Here

                                int checkstate = Convert.ToInt16(ViewState["statecheck"]);
                                int checkdes = Convert.ToInt16(ViewState["checkdesig"]);
                                if ((Convert.ToInt16(ddlState.SelectedValue) == checkstate) && (Convert.ToInt16(txtDesignation.SelectedValue) == checkdes))
                                {
                                    txtUserName.Text = ViewState["usernamecheck"].ToString();
                                    //UsrDfd_UserName.Text = ViewState["checkUserdefined"].ToString();
                                }

                                iReturn = sf.RecordUpdate(sfcode, txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text.Trim(), "", txtDesignation.SelectedValue, sChkLocation, UsrDfd_UserName.Text, sDesSName, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue, ddlBankName.SelectedValue, txtAcountNo.Text, txtIFSC.Text);

                                if (iReturn == -99)
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate UserName');</script>");
                                    UsrDfd_UserName.Focus();
                                }
                                else if (iReturn == -90)
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee ID');</script>");
                                    txtEmployeeID.Focus();
                                }
                                else
                                {
                                    //Added by Sridevi to reset the Approval Manager Details if Reporting is modified
                                    if (ViewState["Reporting_To"].ToString() != ddlReporting.SelectedValue.ToString())
                                    {
                                        SalesForce ssf = new SalesForce();
                                        int ret = ssf.RecordUpdate_App(sfcode, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue);
                                    }
                                }
                                //SalesForce ssf = new SalesForce();
                                //dsSalesForce = ssf.getSfDivision(ddlReporting.SelectedValue.ToString());
                                //if (dsSalesForce.Tables[0].Rows.Count > 0)
                                //{
                                //    divvalue = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                //}
                                //iReturn = ssf.UpdateDivisionCode(sfcode, divvalue);
                            }

                            else // Create SalesForce
                            {

                                division_code = Session["div_code"].ToString() + ",";
                                // Added by sri - to pass designation short name
                                Designation Desig = new Designation();
                                DataSet ds = Desig.getDesignationEd(txtDesignation.SelectedValue, div_code);
                                if (ds.Tables[0].Rows.Count > 0)
                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                strSfCode = sf.RecordAdd(txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text.Trim(), "", txtDesignation.SelectedValue, sChkLocation, sDesSName, UsrDfd_UserName.Text, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue, ddlBankName.SelectedValue, txtAcountNo.Text, txtIFSC.Text);
                                // strSfCode = sf.RecordAdd(txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, Convert.ToDateTime(txtDOB.Text), txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, Session["div_code"].ToString(), Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text, "", txtDesignation.Text, sChkLocation);
                                //iReturn = sf.RecordAdd(txtFieldForceName.Text       , txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, Convert.ToDateTime(txtDOB.Text), txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, Session["div_code"].ToString(), Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text, "", txtDesignation.Text);

                                //Create Approval Manager Table
                                if (strSfCode != "Dup" && strSfCode != "Dup_Emp")
                                {
                                    if ((strSfCode != "") && (strSfCode != null))
                                    {
                                        iReturn = sf.RecordAddApprovalMgr(strSfCode, txtFieldForceName.Text, txtHQ.Text, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, Session["div_code"].ToString());
                                    }
                                }
                                else if (strSfCode == "Dup")
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate UserDefined UserName');</script>");
                                    UsrDfd_UserName.Focus();
                                }
                                else if (strSfCode == "Dup_Emp")
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee Id');</script>");
                                    txtEmployeeID.Focus();
                                }
                            }

                            if (iReturn > 0)
                            {
                                if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null))
                                {
                                    SalesForce sfvac = new SalesForce();
                                    dsSalesForce = sfvac.getSalesForce_ReportingTo(Session["div_code"].ToString(), sfcode);
                                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                                    {
                                        if (sf_hold != "" && sf_hold != null)
                                        {
                                            //SampleUpdate();
                                            //InputUpdate();
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Hold this Id Successfully');window.location='SalesForceList.aspx';</script>");
                                        }
                                        else
                                        {
                                            //SampleUpdate();
                                            //InputUpdate();
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce made Vacant Successfully');</script>");
                                        }

                                        if ((Reporting_To_Manager != "") && (Reporting_To_Manager != null))
                                        {
                                            if (rdoMode.SelectedValue.ToString() == "0")
                                            {
                                                iReturn = sf.RecordUdpate_for_MangrtoBase(sfcode, div_code);
                                                Response.Redirect("~/MasterFiles/MapReportingStructure.aspx?reporting_to=" + sfcode + "&DePromote_Mode=" + rdoMode.SelectedValue.ToString() + "&type=" + ddlFieldForceType.SelectedValue.ToString() + "&sfname=" + txtFieldForceName.Text + "&joiningdate=" + txtJoingingDate.Text + "&UserDefin=" + UsrDfd_UserName.Text + "&Sf_emp_id=" + txtEmployeeID.Text.Trim() + "&Effiective_Date=" + txteffe.Text + "&Newsf_code=" + ddlrepla.SelectedValue.ToString() + "&Vac_sfcode=" + sfcode);

                                            }
                                            else
                                            {
                                                iReturn = sf.RecordUdpate_for_MangrtoBase(sfcode, div_code);
                                                Response.Redirect("~/MasterFiles/MapReportingStructure.aspx?reporting_to=" + sfcode + "&DePromote_Mode=" + rdoMode.SelectedValue.ToString() + "&type=" + ddlFieldForceType.SelectedValue.ToString() + "&sfname=" + txtFieldForceName.Text + "&joiningdate=" + txtJoingingDate.Text + "&UserDefin=" + UsrDfd_UserName.Text + "&Sf_emp_id=" + txtEmployeeID.Text.Trim() + "&Effiective_Date=" + txteffe.Text + "&Newsf_code=" + ddlrepla.SelectedValue.ToString() + "&Vac_sfcode=" + sfcode);
                                            }
                                        }

                                        Response.Redirect("~/MasterFiles/MapReportingStructure.aspx?reporting_to=" + sfcode + "&hold=" + sf_hold);
                                        //}
                                        //HttpContext.Current.Response.Clear();
                                        //HttpContext.Current.Response.Write("<html><head>");
                                        //HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.form1.submit()\">"));
                                        //HttpContext.Current.Response.Write(string.Format("<form name=\"form1\" method=\"Post\" action=\"MapReportingStructure.aspx\">"));

                                        //HttpContext.Current.Response.Write(string.Format("<input name=\"reporting_to\" type=\"hidden\" value=\"" + sfcode + "\">"));

                                        //HttpContext.Current.Response.Write("</form>");
                                        //HttpContext.Current.Response.Write("</body></html>");
                                        //HttpContext.Current.Response.End();


                                    }
                                    else
                                    {
                                        if ((Designation_Name != "") && (Designation_Name != null) || (Reporting_To_Manager != "") && (Reporting_To_Manager != null))
                                        {
                                            if (rdoMode.SelectedValue.ToString() == "0")
                                            {
                                                System.Threading.Thread.Sleep(time);
                                                Session["joiningdate"] = txtJoingingDate.Text;
                                                Session["Fieldforce_Name"] = txtFieldForceName.Text;
                                                Session["UserDefin"] = UsrDfd_UserName.Text;
                                                Session["Sf_emp_id"] = txtEmployeeID.Text.Trim();
                                                Session["Vac_sfcode"] = sfcode;
                                                Session["Effiective_Date"] = txteffe.Text;
                                                if ((Reporting_To_Manager != "") && (Reporting_To_Manager != null))
                                                {
                                                    iReturn = sf.RecordUdpate_for_MangrtoBase(sfcode, div_code);
                                                    Response.Redirect("SalesForce.aspx?DePromote_Mode=" + rdoMode.SelectedValue.ToString());
                                                }
                                                else if (((Designation_Name != "") && (Designation_Name != null)))
                                                {
                                                    iReturn = sf.RecordUdpate_forBase_Manag(sfcode, div_code);
                                                    Response.Redirect("SalesForce.aspx?Promote_Mode=" + rdoMode.SelectedValue.ToString());
                                                }
                                            }
                                            else
                                            {
                                                System.Threading.Thread.Sleep(time);
                                                Session["joiningdate"] = txtJoingingDate.Text;
                                                Session["UserDefin"] = UsrDfd_UserName.Text;
                                                Session["Sf_emp_id"] = txtEmployeeID.Text.Trim();
                                                Session["Vac_sfcode"] = sfcode;
                                                Session["Effiective_Date"] = txteffe.Text;
                                                if ((Reporting_To_Manager != "") && (Reporting_To_Manager != null))
                                                {
                                                    iReturn = sf.RecordUdpate_for_MangrtoBase(sfcode, div_code);
                                                    Response.Redirect("SalesForce.aspx?sfcode=" + ddlrepla.SelectedValue.ToString() + "&DePromote_Mode=" + rdoMode.SelectedValue.ToString() + "&sfname=" + txtFieldForceName.Text);
                                                }
                                                else if (((Designation_Name != "") && (Designation_Name != null)))
                                                {
                                                    iReturn = sf.RecordUdpate_forBase_Manag(sfcode, div_code);
                                                    Response.Redirect("SalesForce.aspx?sfcode=" + ddlrepla.SelectedValue.ToString() + "&Promote_Mode=" + rdoMode.SelectedValue.ToString() + "&sfname=" + txtFieldForceName.Text);
                                                }
                                            }
                                        }

                                        //menu1.Status = "SalesForce made Vacant Successfully ";

                                        if (sf_hold != "" && sf_hold != null)
                                        {
                                            //SampleUpdate();
                                            //InputUpdate();
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Hold this Id Successfully');window.location='SalesForceList.aspx';</script>");
                                        }
                                        else
                                        {
                                            //SampleUpdate();
                                            //InputUpdate();

                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce made Vacant Successfully');window.location='SalesForceList.aspx';</script>");
                                        }

                                        //if (rblVacantBlock.SelectedValue == "T")
                                        //{
                                        //    iReturn = sf.RecordUdpate_forUsdefd(sfcode, div_code);

                                        //    Session["type"] = ddlFieldForceType.SelectedValue.ToString();
                                        //    Response.Redirect("~/MasterFiles/SalesForce_Transfer.aspx?sfcode=" + sfcode + "&sfname=" + txtFieldForceName.Text + "&joiningdate=" + txtJoingingDate.Text + "&UserDefin=" + UsrDfd_UserName.Text + "&Sf_emp_id=" + txtEmployeeID.Text);
                                        //}
                                    }
                                }
                                else if ((usrname != "") && (usrname != null) || (desname != "") && (desname != null)) // Promo / De-Promo
                                {
                                    SalesForce sfvac = new SalesForce();
                                    if ((usrname != "") && (usrname != null))
                                    {
                                        //Create Promotion Dtls Table

                                        iReturn = sf.PromoteSf(sfcode, txtFieldForceName.Text, ViewState["cur_des"].ToString(), txtDesignation.SelectedValue, ddlReporting.SelectedValue, div_code, txteffe.Text, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue);

                                        if (sfcode.Contains("MGR"))
                                        {
                                            if (rdoMode.SelectedValue.ToString() == "0")
                                            {
                                                division_code = Session["div_code"].ToString() + ",";

                                                Designation Desig = new Designation();
                                                DataSet ds = Desig.getDesignationEd(ViewState["cur_des"].ToString(), div_code);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                                strSfCode = sf.RecordAdd(txtFieldForceName.Text, txtUserName.Text + "H", txtPassword.Text, Convert.ToDateTime(txteffe.Text), sfcode, ddlState.SelectedValue, Convert.ToDateTime(txteffe.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text.Trim() + "H", "", ViewState["cur_des"].ToString(), sChkLocation, sDesSName, UsrDfd_UserName.Text + "H", Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue, ddlBankName.SelectedValue, txtAcountNo.Text, txtIFSC.Text);

                                                if ((strSfCode != "") && (strSfCode != null))
                                                {
                                                    iReturn = sf.RecordAddApprovalMgr(strSfCode, txtFieldForceName.Text, txtHQ.Text, sfcode, sfcode, sfcode, sfcode, sfcode, sfcode, sfcode, sfcode, Session["div_code"].ToString());
                                                }

                                                iReturn = sf.vbRecordUpdate(strSfCode, "H", txteffe.Text, txteffe.Text, txteffe.Text, UsrDfd_UserName.Text + "H", div_code, txtReason.Text, txtEmployeeID.Text.Trim() + "H");

                                                iReturn = sf.RecordUpdate_AM_Hold(strSfCode, sfcode);
                                            }
                                            else
                                            {
                                                division_code = Session["div_code"].ToString() + ",";

                                                Designation Desig = new Designation();
                                                DataSet ds = Desig.getDesignationEd(ViewState["cur_des"].ToString(), div_code);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                                iReturn = sf.RecordUpdate_AM_Hold_Desg(ddlrepla.SelectedValue, sfcode, ViewState["cur_des"].ToString(), sDesSName);
                                                iReturn = sf.vbRecordUpdate(ddlrepla.SelectedValue, "H", txteffe.Text, txteffe.Text, txteffe.Text, UsrDfd_UserName.Text + "H", div_code, txtReason.Text, txtEmployeeID.Text.Trim() + "H");
                                            }
                                        }
                                    }
                                    if ((desname != "") && (desname != null))
                                    {
                                        //Create DePromotion Dtls Table
                                        iReturn = sf.DePromoteSf(sfcode, txtFieldForceName.Text, ViewState["cur_des"].ToString(), txtDesignation.SelectedValue, ddlReporting.SelectedValue, div_code, txteffe.Text, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue);

                                        if (sfcode.Contains("MGR"))
                                        {
                                            if (rdoMode.SelectedValue.ToString() == "0")
                                            {
                                                division_code = Session["div_code"].ToString() + ",";

                                                Designation Desig = new Designation();
                                                DataSet ds = Desig.getDesignationEd(ViewState["cur_des"].ToString(), div_code);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                                strSfCode = sf.RecordAdd(txtFieldForceName.Text, txtUserName.Text + "H", txtPassword.Text, Convert.ToDateTime(txteffe.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txteffe.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text.Trim() + "H", "", ViewState["cur_des"].ToString(), sChkLocation, sDesSName, UsrDfd_UserName.Text + "H", Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue, ddlBankName.SelectedValue, txtAcountNo.Text, txtIFSC.Text);

                                                if ((strSfCode != "") && (strSfCode != null))
                                                {
                                                    iReturn = sf.RecordAddApprovalMgr(strSfCode, txtFieldForceName.Text, txtHQ.Text, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, Session["div_code"].ToString());
                                                }

                                                iReturn = sf.vbRecordUpdate(strSfCode, "H", txteffe.Text, txteffe.Text, txteffe.Text, UsrDfd_UserName.Text + "H", div_code, txtReason.Text, txtEmployeeID.Text.Trim() + "H");

                                                iReturn = sf.RecordUpdate_AM_Hold_Depro(strSfCode, sfcode, ddlReporting.SelectedValue);
                                            }
                                            else
                                            {
                                                division_code = Session["div_code"].ToString() + ",";

                                                Designation Desig = new Designation();
                                                DataSet ds = Desig.getDesignationEd(ViewState["cur_des"].ToString(), div_code);
                                                if (ds.Tables[0].Rows.Count > 0)
                                                    sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();



                                                iReturn = sf.RecordUpdate_AM_Hold_Desg_Depro(ddlrepla.SelectedValue, sfcode, ViewState["cur_des"].ToString(), sDesSName, ddlReporting.SelectedValue);
                                                iReturn = sf.vbRecordUpdate(ddlrepla.SelectedValue, "H", txteffe.Text, txteffe.Text, txteffe.Text, UsrDfd_UserName.Text + "H", div_code, txtReason.Text, txtEmployeeID.Text.Trim() + "H");
                                            }
                                        }
                                    }
                                    dsSalesForce = sfvac.getSalesForce_ReportingTo(Session["div_code"].ToString(), sfcode);
                                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                                    {
                                        if ((usrname != "") && (usrname != null))
                                        {
                                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce Promoted Successfully');</script>");
                                            //Response.Redirect("~/MasterFiles/Promote.aspx?reporting_to=" + sfcode);                        
                                            Response.Write("<script>alert('SalesForce Promoted Successfully') ; location.href='Promote.aspx?reporting_to=" + sfcode + "'</script>");
                                        }
                                        if ((desname != "") && (desname != null))
                                        {
                                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce De-Promoted Successfully');</script>");
                                            //Response.Redirect("~/MasterFiles/DePromote.aspx?reporting_to=" + sfcode);

                                            Response.Write("<script>alert('SalesForce De-Promoted Successfully') ; location.href='DePromote.aspx?reporting_to=" + sfcode + "'</script>");
                                        }

                                    }
                                    else
                                    {
                                        if ((usrname != "") && (usrname != null))
                                        {
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce Promoted Successfully');window.location='Salesforce_Promo_DePromo.aspx';</script>");
                                        }
                                        if ((desname != "") && (desname != null))
                                        {
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce De-Promoted Successfully');window.location='Salesforce_Promo_DePromo.aspx';</script>");
                                        }
                                    }
                                }
                                else if ((sfname != "") && (sfname != null))
                                {
                                    SalesForce sfAc = new SalesForce();

                                    iReturn = sf.RecordUpdate(sfcode, txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text.Trim(), "", txtDesignation.SelectedValue, sChkLocation, UsrDfd_UserName.Text, sDesSName, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue, ddlBankName.SelectedValue, txtAcountNo.Text, txtIFSC.Text);
                                    //iReturn = sfAc.VacActivate(sfcode, state_code);
                                    if ((state_code != "") && (state_code != null))
                                    {

                                        if ((sf_hold_realse == "") || (sf_hold_realse == null))
                                        {
                                            iReturn = sf.RecordDele_ForRejoin(sfcode, txtFieldForceName.Text, div_code, txtEmployeeID.Text.Trim(), Convert.ToDateTime(txtJoingingDate.Text));
                                        }

                                        else
                                        {
                                            iReturn = sf.Activate(sfcode);
                                        }
                                    }


                                    iReturn = sf.desig_chane_allowance(sfcode, txtFieldForceName.Text, ViewState["cur_des"].ToString(), txtDesignation.SelectedValue, ddlReporting.SelectedValue, div_code, Convert.ToInt16(ddlfieldtype.SelectedValue), ddlcatg.SelectedValue);
                                    if ((sf_hold_realse != "") && (sf_hold_realse != null))
                                    {
                                        iReturn = sfAc.VacActivate(sfcode, "");
                                    }
                                    else
                                    {
                                        iReturn = sfAc.VacActivate(sfcode, state_code);
                                    }
                                    if (iReturn > 0)
                                    {
                                        //  dsSalesForce = sfAc.getActiveReportingTo(sfcode);
                                        dsSalesForce = sfAc.getSalesForce_ReMap_ReportingTo(div_code, sfcode, ddlReporting.SelectedValue);
                                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                                        {
                                            //if (rdoMode == "1")
                                            //{
                                            //    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);

                                            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');</script>");
                                            //}

                                            if (Promote_Mode == "1")
                                            {
                                                iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), sfcode, "");

                                                iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code, ViewState["sf_emp_id"].ToString(), txtEmployeeID.Text.Trim());

                                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');</script>");
                                            }

                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');</script>");
                                            Response.Redirect("~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sfcode + "&reporting_sf=" + ddlReporting.SelectedValue);
                                            //Response.Write("<script>alert('Activated Successfully') ; location.href=ReMapReportingStructure.aspx?reporting_to="+ sfcode + "'</script>");
                                            //Response.Redirect("ReMapReportingStructure.aspx?reporting_to=" + sfcode);
                                        }
                                        else
                                        {
                                            //if (rdoMode == "1")
                                            //{
                                            //    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);

                                            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');window.location='SalesForceList.aspx';</script>");
                                            //}

                                            if ((Promote_Mode == "1") || (DePromote_Mode == "1"))
                                            {
                                                if (Promote_Mode == "1")
                                                {
                                                    iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), sfcode, "");

                                                    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code, ViewState["sf_emp_id"].ToString(), txtEmployeeID.Text.Trim());

                                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');window.location='SalesForceList.aspx';</script>");
                                                }

                                                else if (DePromote_Mode == "1")
                                                {
                                                    iReturn = sf.Transfer_RecordInsert_Depromo(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code, ViewState["sf_emp_id"].ToString(), txtEmployeeID.Text.Trim());
                                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Promoted Successfully');window.location='SalesForceList.aspx';</script>");
                                                }
                                            }

                                            //menu1.Status = "SalesForce Activated Successfully";
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');window.location='VacantSFList.aspx';</script>");
                                        }
                                    }
                                    else
                                    {
                                        //menu1.Status = "Unable to Activate";
                                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
                                    }
                                }
                                //else if (isManager && isCreate)
                                //{
                                //    //SalesForce sfmgr = new SalesForce();
                                //    //dsSalesForce = sfmgr.getSFCode_Manager(Session["div_code"].ToString(), reporting_to, txtEmployeeID.Text, txtFieldForceName.Text, txtUserName.Text, txtPassword.Text );
                                //    //if (dsSalesForce.Tables[0].Rows.Count > 0)
                                //    //{
                                //    //    string mgr_code = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); 
                                //    //    Response.Redirect("SF_MGR_Reporting.aspx?sfcode=" + mgr_code + "&reporting_to=" + reporting_to);
                                //    //}
                                //    //else
                                //    //{
                                //    //    //menu1.Status = "SalesForce Activated Successfully";
                                //    //}
                                //}
                                else if ((sf_type != "") && (sf_type != null))
                                {
                                    //menu1.Status = "SalesForce Blocked Successfully ";
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Blocked Successfully');window.location='SalesForceList.aspx';</script>");
                                }
                                else if ((sfcode != "") && (sfcode != null))// Update SalesForce
                                {
                                    //menu1.Status = "SalesForce updated Successfully ";
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='SalesForceList.aspx';</script>");
                                }
                                else
                                {
                                    //menu1.Status = "SalesForce created Successfully ";

                                    //if (rdoMode == "0")
                                    //{
                                    //    iReturn = sf.Transfer_RecordInsert(txtFieldForceName.Text, ViewState["Vac_sfcode"].ToString(), strSfCode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);
                                    //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');window.location='SalesForceList.aspx';</script>");

                                    //}

                                    if ((Promote_Mode == "0") || (DePromote_Mode == "0"))
                                    {
                                        if (Promote_Mode == "0")
                                        {
                                            iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), strSfCode, "");

                                            iReturn = sf.Transfer_RecordInsert(txtFieldForceName.Text, ViewState["Vac_sfcode"].ToString(), strSfCode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code, ViewState["sf_emp_id"].ToString(), txtEmployeeID.Text.Trim());
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');window.location='SalesForceList.aspx';</script>");
                                        }
                                        else if (DePromote_Mode == "0")
                                        {
                                            iReturn = sf.Transfer_RecordInsert_Depromo(txtFieldForceName.Text, ViewState["Vac_sfcode"].ToString(), strSfCode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code, ViewState["sf_emp_id"].ToString(), txtEmployeeID.Text.Trim());
                                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Promoted Successfully');window.location='SalesForceList.aspx';</script>");
                                        }

                                    }

                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                                    ResetALL();
                                    FillType();
                                }
                            }
                            else if (iReturn == -2)
                            {
                                //menu1.Status = "SalesForce already Exist";
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                            }

                        }
                    }
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        Username();
        //GetState();
        //FillType();
        if (txtDesignation.SelectedIndex == 0)
        {
            string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
            txtUserName.Text = strtxtUser;
            //Modified by Sridevi - Userdefined user name to restrict auto edit 
            if ((sfcode == "") || (sfcode == null))
            {
                UsrDfd_UserName.Text = txtUserName.Text;

                txtEmployeeID.Text = txtUserName.Text;
            }
        }
    }
    protected void txtDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Username();
        //FillType();
        if (txtDesignation.SelectedIndex == 0)
        {
            string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
            txtUserName.Text = strtxtUser;
            //Modified by Sridevi - Userdefined user name to restrict auto edit 
            if ((sfcode == "") || (sfcode == null))
            {
                UsrDfd_UserName.Text = txtUserName.Text;
                txtEmployeeID.Text = txtUserName.Text;
            }
        }
    }

    protected void Username()
    {
        DataSet ds = new DataSet();
        SalesForce SF = new SalesForce();
        if (ddlState.SelectedIndex == 0)
        {
            txtDesignation.Enabled = false;
            txtDesignation.SelectedIndex = 0;
        }
        else
        {
            txtDesignation.Enabled = true;
            //ddlReporting.Enabled = true;
        }



        ds = SF.GetUserName(div_code, ddlState.SelectedValue, txtDesignation.SelectedValue);
        txtUserName.Text = ds.Tables[0].Rows[0]["Division_SName"].ToString() + ds.Tables[1].Rows[0]["ShortName"].ToString() +
                           ds.Tables[2].Rows[0]["Designation_Short_Name"].ToString() + ds.Tables[4].Rows[0]["Number"].ToString();
        //Modified by Sridevi - Userdefined user name to restrict auto edit 
        if ((sfcode == "") || (sfcode == null))
        {
            UsrDfd_UserName.Text = txtUserName.Text;
            txtEmployeeID.Text = txtUserName.Text;
        }


    }
    protected void ddlFieldForceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldForceType.SelectedIndex == 1)
        {
            SalesForce sf = new SalesForce();
            dsSF = sf.getDesignation_MR(div_code);
            if (dsSF.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsSF;
                txtDesignation.DataBind();
            }

        }
        else if (ddlFieldForceType.SelectedIndex == 2)
        {
            SalesForce sf = new SalesForce();
            dsSF = sf.getDesignation_Manager(div_code);
            if (dsSF.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsSF;
                txtDesignation.DataBind();
            }

        }
    }
    private void loaddes()
    {
        DataSet dsddl = new DataSet();
        if (ddlFieldForceType.SelectedValue.ToString() == "1")
        {
            SalesForce sf = new SalesForce();
            dsddl = sf.getDesignation_MR(div_code);
            if (dsddl.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsddl;
                txtDesignation.DataBind();
            }

        }
        else if (ddlFieldForceType.SelectedValue.ToString() == "2")
        {
            SalesForce sf = new SalesForce();
            dsddl = sf.getDesignation_Manager(div_code);
            if (dsddl.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsddl;
                txtDesignation.DataBind();
            }
        }
    }

    protected void rdoMode_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((usrname != "") && (usrname != null) || (desname != "") && (desname != null))
        {

            if (sfcode.Contains("MGR"))
            {
                if (rdoMode.SelectedValue.ToString() == "0")
                {
                    ddlrepla.Visible = false;
                    lblreplace.Visible = false;
                }
                else
                {
                    ddlrepla.Visible = true;
                    lblreplace.Visible = true;

                    SalesForce sf = new SalesForce();

                    dsSalesForce = sf.getVacant_HoldManagersonly(div_code, txtDesignation.SelectedValue);

                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        ddlrepla.DataTextField = "sf_name";
                        ddlrepla.DataValueField = "Sf_Code";
                        ddlrepla.DataSource = dsSalesForce;
                        ddlrepla.DataBind();
                    }

                }


            }
        }
        else
        {

            if (rdoMode.SelectedValue.ToString() == "0")
            {
                ddlrepla.Visible = false;
                lblreplace.Visible = false;
            }
            else
            {
                ddlrepla.Visible = true;
                lblreplace.Visible = true;
                FillVacantManagers();
            }
        }
    }

    private void FillVacantManagers()
    {
        SalesForce sf = new SalesForce();
        if (ddlFieldForceType.SelectedValue.ToString() == "1")
        {
            dsSalesForce = sf.getVacantManagersonly(div_code);
        }
        else
        {
            dsSalesForce = sf.getVacantBaselevelonly(div_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlrepla.DataTextField = "sf_name";
            ddlrepla.DataValueField = "Sf_Code";
            ddlrepla.DataSource = dsSalesForce;
            ddlrepla.DataBind();
        }
    }


    protected void lnk_Click(object sender, EventArgs e)
    {
        txtFieldForceName.Enabled = true;
    }
    protected void lnkdate_Click(object sender, EventArgs e)
    {
        txtTPDCRStartDate.Enabled = true;
    }

    protected void Reportlnk_Click(object sender, EventArgs e)
    {
        ddlReporting.Enabled = true;
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForceList.aspx");
    }

    public void SampleUpdate()
    {
        string ddlFieldForce = sfcode;
        string ddlFieldForceText = txtFieldForceName.Text;
        string toddlFieldForce = "admin";

        DateTime Effe_Date = Convert.ToDateTime(txteffe.Text.ToString());
        string ddlMonth = Effe_Date.Month.ToString();
        string ddlYear = Effe_Date.Year.ToString();

        Product Sample_Product = new Product();

        da = Sample_Product.GetAdjusmentBal(div_code, ddlFieldForce);

        if (da.Tables[0].Rows.Count > 0)
        {


            System.Threading.Thread.Sleep(time);
            try
            {
                DataSet db = new DataSet();
                int Trans_Sl_No;
                int iReturn = -1;
                int iReturn2 = -1;
                Product sample_Given_Product = new Product();
                using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;


                    try
                    {
                        db = sample_Given_Product.GetSampled_Product(toddlFieldForce, ddlMonth, ddlYear);
                        if (db.Tables[0].Rows.Count > 0)
                        {


                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Transfer_Head";
                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Sample_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce + "','" + toddlFieldForce + "','" + ddlMonth + "','" + ddlYear + "','" + div_code + "',getdate(),'" + ddlFieldForceText + "','" + toddlFieldForce + "')";

                            command.ExecuteNonQuery();




                            for (int i = 0; i <= da.Tables[0].Rows.Count - 1; i++)
                            {
                                //foreach (GridViewRow row in grdsample.Rows)
                                //{

                                //Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                                //Label lblProduct = (Label)row.Cells[3].FindControl("lblprdtName");
                                //Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                                //Label Prod_Erp_Code = (Label)row.Cells[4].FindControl("lblsaleerpcode");
                                //TextBox transfrerQty = (TextBox)row.Cells[6].FindControl("txtTransferQty");

                                string lblDR = da.Tables[0].Rows[i]["Product_Code_SlNo"].ToString();
                                string lblProduct = da.Tables[0].Rows[i]["Product_Detail_Name"].ToString();
                                string lbldespatch_qty = da.Tables[0].Rows[i]["Sample_AsonDate"].ToString();
                                string Prod_Erp_Code = da.Tables[0].Rows[i]["Sample_Erp_Code"].ToString();


                                string transfrerQty = lbldespatch_qty.ToString();


                                // string actual_despatch_Qty = db.Tables[0].Rows[i]["Despatch_Qty"].ToString();
                                string trans_Sl_No = db.Tables[0].Rows[0]["Trans_sl_No"].ToString();


                                //if (db.Tables[0].Rows[i]["Product_Code_SlNo"].ToString() == lblDR.Text)
                                //{
                                //if (transfrerQty.Text == "" || transfrerQty.Text == "0")
                                //{

                                //}
                                //else
                                //{
                                // int New_Despatch_Qty = Convert.ToInt32(db.Tables[0].Rows[i]["Despatch_Qty"].ToString()) + Convert.ToInt32(transfrerQty.Text);
                                if (transfrerQty != "" && transfrerQty != "0" && Convert.ToInt32(transfrerQty) > 0)
                                {

                                    if (ddlFieldForce != "admin")
                                    {
                                        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                        DataSet ds_SlNo_new = new DataSet();
                                        // string leave_New = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_despatch_Head where Sf_Code='" + ddlFieldForce.SelectedValue + "'";
                                        string leave_New = "select MAX(b.Trans_sl_No) from Trans_Sample_Despatch_Head a,Trans_Sample_Despatch_Details b where a.Sf_Code='" + ddlFieldForce + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                        SqlCommand cmd2;
                                        cmd2 = new SqlCommand(leave_New, con2);
                                        con2.Open();
                                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                        da2.Fill(ds_SlNo_new);
                                        con2.Close();
                                        Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new.Tables[0].Rows[0]["Column1"].ToString());
                                    }


                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty);

                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getsample_AsonDate(ddlFieldForce, div_code, lblDR);

                                    DataSet dssample2 = adm.getsample_AsonDate(toddlFieldForce, div_code, lblDR);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString() == dssample2.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(toddlFieldForce, div_code, tot_sample2, lblDR);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                        iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                        iReturn2 = adm.InsertSample_AS_ON_Date(toddlFieldForce, div_code, transfrerQty, lblDR);
                                    }

                                    command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,productc) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct + "','" + Old_Desptach_Qty + "','" + lblDR + "')";

                                    command.ExecuteNonQuery();

                                    if (toddlFieldForce != "admin")
                                    {
                                        command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,productc,Despatch_Qty_Bk) values('" + trans_Sl_No + "','" + div_code + "','" + lblProduct + "','" + transfrerQty + "','" + lblDR + "','" + transfrerQty + "')";

                                        command.ExecuteNonQuery();
                                    }



                                    command.CommandText = "Insert into Trans_Sample_Transfer_Detail        (Trans_Sl_No,Product_Code,Product_Erp_Code,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR + "','" + Prod_Erp_Code + "','" + lbldespatch_qty + "','" + transfrerQty + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }

                            }
                            //}
                            //transaction.Commit();
                            connection.Close();

                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');</script>");
                        }
                        else
                        {
                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Transfer_Head";

                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Sample_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce + "','" + toddlFieldForce + "','" + ddlMonth + "','" + ddlYear + "','" + div_code + "',getdate(),'" + ddlFieldForceText + "','" + toddlFieldForce + "')";

                            command.ExecuteNonQuery();

                            Product Insert_Head = new Product();
                            int ireturn = -1;

                            if (toddlFieldForce != "admin")
                            {
                                ireturn = Insert_Head.Insert_Existing_Product_New(toddlFieldForce, div_code, ddlMonth, ddlYear);

                                DataSet Trans_Slno = new DataSet();
                                string New_sl_No = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_Despatch_Head where sf_code='" + toddlFieldForce + "' and Trans_Month='" + ddlMonth + "' and Trans_Year='" + ddlYear + "'";
                                SqlCommand cmd2;
                                cmd2 = new SqlCommand(New_sl_No, con1);
                                con1.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                da2.Fill(Trans_Slno);
                                con1.Close();
                                Detail_Trans_Sl_No = Convert.ToInt32(Trans_Slno.Tables[0].Rows[0]["Column1"].ToString());
                            }

                            //This Part For The Receiving User Who Not Have Any Samples In The despatch detail
                            for (int i = 0; i <= da.Tables[0].Rows.Count - 1; i++)
                            {
                                //foreach (GridViewRow row in grdsample.Rows)
                                //{

                                //Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                                //Label lblProduct = (Label)row.Cells[1].FindControl("lblprdtName");
                                //Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                                //Label Prod_Erp_Code = (Label)row.Cells[3].FindControl("lblsaleerpcode");
                                //TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");

                                string lblDR = da.Tables[0].Rows[i]["Product_Code_SlNo"].ToString();
                                string lblProduct = da.Tables[0].Rows[i]["Product_Detail_Name"].ToString();
                                string lbldespatch_qty = da.Tables[0].Rows[i]["Sample_AsonDate"].ToString();
                                string Prod_Erp_Code = da.Tables[0].Rows[i]["Sample_Erp_Code"].ToString();
                                string transfrerQty = lbldespatch_qty.ToString();


                                if (transfrerQty != "" && transfrerQty != "0" && Convert.ToInt32(transfrerQty) > 0)
                                {

                                    SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                    DataSet ds_SlNo_new1 = new DataSet();
                                    string leave_New1 = "select MAX(b.Trans_sl_No) from Trans_Sample_Despatch_Head a,Trans_Sample_Despatch_Details b where a.Sf_Code='" + ddlFieldForce + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                    SqlCommand cmd3;
                                    cmd3 = new SqlCommand(leave_New1, con3);
                                    con3.Open();
                                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                    da3.Fill(ds_SlNo_new1);
                                    con3.Close();
                                    Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new1.Tables[0].Rows[0]["Column1"].ToString());

                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty);

                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getsample_AsonDate(ddlFieldForce, div_code, lblDR);

                                    DataSet dssample2 = adm.getsample_AsonDate(toddlFieldForce, div_code, lblDR);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString() == dssample2.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(toddlFieldForce, div_code, tot_sample2, lblDR);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                        iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                        iReturn2 = adm.InsertSample_AS_ON_Date(toddlFieldForce, div_code, transfrerQty, lblDR);
                                    }

                                    command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,productc) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct + "','" + Old_Desptach_Qty + "','" + lblDR + "')";


                                    command.ExecuteNonQuery();
                                    if (toddlFieldForce != "admin")
                                    {
                                        command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,productc,Despatch_Qty_Bk) values('" + Detail_Trans_Sl_No + "','" + div_code + "','" + lblProduct + "','" + transfrerQty + "','" + lblDR + "','" + transfrerQty + "')";

                                        command.ExecuteNonQuery();
                                    }



                                    command.CommandText = "Insert into Trans_Sample_Transfer_Detail(Trans_Sl_No,Product_Code,Product_Erp_Code,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR + "','" + Prod_Erp_Code + "','" + lbldespatch_qty + "','" + transfrerQty + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }
                            }
                            //transaction.Commit();
                            connection.Close();
                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                        Console.WriteLine("Message: {0}", ex.Message);


                        // Attempt to roll back the transaction.
                        try
                        {

                            //transaction.Rollback();

                        }
                        catch (Exception ex2)
                        {

                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                            Console.WriteLine("  Message: {0}", ex2.Message);

                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists while Transfer Sample!');</script>");
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
    }

    public void InputUpdate()
    {

        string ddlFieldForce = sfcode;
        string ddlFieldForceText = txtFieldForceName.Text;
        string toddlFieldForce = "admin";

        DateTime Effe_Date = Convert.ToDateTime(txteffe.Text.ToString());
        string ddlMonth = Effe_Date.Month.ToString();
        string ddlYear = Effe_Date.Year.ToString();

        DataSet da = new DataSet();
        Product Sample_Product = new Product();

        da = Sample_Product.GetInput_Product_Temp(div_code, ddlFieldForce);
        if (da.Tables[0].Rows.Count > 0)
        {


            System.Threading.Thread.Sleep(time);
            try
            {
                DataSet db = new DataSet();
                int Trans_Sl_No;
                Product sample_Given_Product = new Product();
                int iReturn = -1;
                int iReturn2 = -1;
                using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;

                    // transaction = connection.BeginTransaction();

                    // command.Connection = connection;

                    //command.Transaction = transaction;

                    try
                    {
                        db = sample_Given_Product.GetInput_Product(toddlFieldForce, ddlMonth, ddlYear);
                        if (db.Tables[0].Rows.Count > 0)
                        {
                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Input_Transfer_Head";
                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Input_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce + "','" + toddlFieldForce + "','" + ddlMonth + "','" + ddlYear + "','" + div_code + "',getdate(),'" + ddlFieldForceText + "','" + toddlFieldForce + "')";

                            command.ExecuteNonQuery();

                            //foreach (GridViewRow row in grdsample.Rows)
                            //{
                            //    Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                            //    Label lblProduct = (Label)row.Cells[3].FindControl("lblprdtName");
                            //    Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                            //    Label Prod_Erp_Code = (Label)row.Cells[4].FindControl("lblsaleerpcode");
                            //    TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");

                            for (int i = 0; i <= da.Tables[0].Rows.Count - 1; i++)
                            {
                                string lblDR = da.Tables[0].Rows[i]["Gift_Code"].ToString();
                                string lblProduct = da.Tables[0].Rows[i]["Gift_Name"].ToString();
                                string lbldespatch_qty = da.Tables[0].Rows[i]["InputQty_AsOnDate"].ToString();
                                string Prod_Erp_Code = da.Tables[0].Rows[i]["Gift_SName"].ToString();
                                string transfrerQty = lbldespatch_qty.ToString();


                                string trans_Sl_No = db.Tables[0].Rows[0]["Trans_sl_No"].ToString();
                                if (transfrerQty != "" && transfrerQty != "0" && Convert.ToInt32(transfrerQty) > 0)
                                {
                                    if (toddlFieldForce != "admin")
                                    {
                                        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                        DataSet ds_SlNo_new = new DataSet();
                                        // string leave_New = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_despatch_Head where Sf_Code='" + ddlFieldForce.SelectedValue + "'";
                                        string leave_New = "select MAX(b.Trans_sl_No) from Trans_Input_Despatch_Head a,Trans_Input_Despatch_Details b where a.Sf_Code='" + ddlFieldForce + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select IN_EM_Month + '-' + '1' + '-' + IN_EM_Year from setup_others where division_code = '" + div_code + "')";
                                        SqlCommand cmd2;
                                        cmd2 = new SqlCommand(leave_New, con2);
                                        con2.Open();
                                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                        da2.Fill(ds_SlNo_new);
                                        con2.Close();
                                        Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new.Tables[0].Rows[0]["Column1"].ToString());
                                    }

                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty);


                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getinput_AsonDate(ddlFieldForce, div_code, lblDR);

                                    DataSet dssample2 = adm.getinput_AsonDate(toddlFieldForce, div_code, lblDR);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Gift_Code"].ToString() == dssample2.Tables[0].Rows[0]["Gift_Code"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateInput_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                            iReturn2 = adm.UpdateInput_AS_ON_Date(toddlFieldForce, div_code, tot_sample2, lblDR);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                        iReturn2 = adm.UpdateInput_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                        iReturn2 = adm.InsertInput_AS_ON_Date(toddlFieldForce, div_code, transfrerQty, lblDR, lblProduct);
                                    }





                                    command.CommandText = "insert into Trans_Input_Despatch_Details(Trans_sl_No,Division_Code,Gift_Name,Despatch_Qty,productc) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct + "','" + Old_Desptach_Qty + "','" + lblDR + "')";

                                    //command.CommandText = "update Trans_Sample_Despatch_Details set Despatch_Qty='" + Old_Desptach_Qty + "' where Trans_sl_No='" + trans_Sl_No + "' and         productc='" + lblDR.Text + "' ";

                                    command.ExecuteNonQuery();

                                    if (toddlFieldForce != "admin")
                                    {
                                        command.CommandText = "insert into Trans_Input_Despatch_Details(Trans_sl_No,Division_Code,Gift_Name,Despatch_Qty,productc,Despatch_Qty_Bk) values('" + trans_Sl_No + "','" + div_code + "','" + lblProduct + "','" + transfrerQty + "','" + lblDR + "','" + transfrerQty + "')";

                                        command.ExecuteNonQuery();
                                    }
                                    command.CommandText = "Insert into Trans_Input_Transfer_Detail        (Trans_Sl_No,Gift_Code,GiftS_Name,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR + "','" + Prod_Erp_Code + "','" + lbldespatch_qty + "','" + transfrerQty + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }

                            }
                            //}
                            //transaction.Commit();
                            connection.Close();
                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');</script>");
                        }
                        else
                        {
                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Input_Transfer_Head";

                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Input_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce + "','" + toddlFieldForce + "','" + ddlMonth + "','" + ddlYear + "','" + div_code + "',getdate(),'" + ddlFieldForceText + "','" + toddlFieldForce + "')";

                            command.ExecuteNonQuery();


                            // ireturn=

                            //DataSet Trans_Slno = new DataSet();
                            //string New_sl_No = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Despatch_Head where sf";
                            //SqlCommand cmd2;
                            //cmd2 = new SqlCommand(New_sl_No, con1);
                            //con1.Open();
                            //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            //da2.Fill(Trans_Slno);
                            //con1.Close();
                            //Detail_Trans_Sl_No = Convert.ToInt32(Trans_Slno.Tables[0].Rows[0]["Column1"].ToString());

                            //command.CommandText = "insert into Trans_Sample_Despatch_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date,Updated_Date,Trans_month_year) values('" + toddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "','"+ddlYear.SelectedValue+"',getdate(),getdate(),getdate())";

                            //command.ExecuteNonQuery();
                            Product Insert_Head = new Product();
                            int ireturn = -1;



                            if (ddlFieldForce != "admin")
                            {
                                ireturn = Insert_Head.Insert_Existing_Product_New_Input(toddlFieldForce, div_code, ddlMonth, ddlYear);

                                DataSet Trans_Slno = new DataSet();
                                string New_sl_No = "SELECT MAX(Trans_Sl_No) FROM Trans_Input_Despatch_Head where sf_code='" + toddlFieldForce + "' and Trans_Month='" + ddlMonth + "' and Trans_Year='" + ddlYear + "'";
                                SqlCommand cmd2;
                                cmd2 = new SqlCommand(New_sl_No, con1);
                                con1.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                da2.Fill(Trans_Slno);
                                con1.Close();
                                Detail_Trans_Sl_No = Convert.ToInt32(Trans_Slno.Tables[0].Rows[0]["Column1"].ToString());
                            }



                            //This Part For The Receiving User Who Not Have Any Samples In The despatch detail
                            //foreach (GridViewRow row in grdsample.Rows)
                            //{

                            //    Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                            //    Label lblProduct = (Label)row.Cells[1].FindControl("lblprdtName");
                            //    Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                            //    Label Prod_Erp_Code = (Label)row.Cells[3].FindControl("lblsaleerpcode");
                            //    TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");
                            for (int i = 0; i <= da.Tables[0].Rows.Count - 1; i++)
                            {
                                string lblDR = da.Tables[0].Rows[i]["Gift_Code"].ToString();
                                string lblProduct = da.Tables[0].Rows[i]["Gift_Name"].ToString();
                                string lbldespatch_qty = da.Tables[0].Rows[i]["InputQty_AsOnDate"].ToString();
                                string Prod_Erp_Code = da.Tables[0].Rows[i]["Gift_SName"].ToString();
                                string transfrerQty = lbldespatch_qty.ToString();


                                if (transfrerQty != "" && transfrerQty != "0" && Convert.ToInt32(transfrerQty) > 0)
                                {

                                    SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                    DataSet ds_SlNo_new1 = new DataSet();
                                    //string leave_New1 = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_despatch_Head where Sf_Code='" + ddlFieldForce.SelectedValue + "'";
                                    string leave_New1 = "select MAX(b.Trans_sl_No) from Trans_Input_Despatch_Head a,Trans_Input_Despatch_Details b where a.Sf_Code='" + ddlFieldForce + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                    SqlCommand cmd3;
                                    cmd3 = new SqlCommand(leave_New1, con3);
                                    con3.Open();
                                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                    da3.Fill(ds_SlNo_new1);
                                    con3.Close();
                                    Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new1.Tables[0].Rows[0]["Column1"].ToString());


                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getinput_AsonDate(ddlFieldForce, div_code, lblDR);

                                    DataSet dssample2 = adm.getinput_AsonDate(toddlFieldForce, div_code, lblDR);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Gift_Code"].ToString() == dssample2.Tables[0].Rows[0]["Gift_Code"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateInput_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                            iReturn2 = adm.UpdateInput_AS_ON_Date(toddlFieldForce, div_code, tot_sample2, lblDR);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["InputQty_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty);
                                        iReturn2 = adm.UpdateInput_AS_ON_Date(ddlFieldForce, div_code, tot_Sample, lblDR);
                                        iReturn2 = adm.InsertInput_AS_ON_Date(toddlFieldForce, div_code, transfrerQty, lblDR, lblProduct);
                                    }



                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty);

                                    //command.CommandText = "update Trans_Sample_Despatch_Details set Despatch_Qty='" + Old_Desptach_Qty + "' where Trans_sl_No='" + lblTrans_Sl_No.Text + "' and         productc='" + lblDR.Text + "' and sl_No='" + lblUnique_Sl_No.Text + "' ";


                                    command.CommandText = "insert into Trans_Input_Despatch_Details(Trans_sl_No,Division_Code,Gift_Name,Despatch_Qty,productc) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct + "','" + Old_Desptach_Qty + "','" + lblDR + "')";


                                    command.ExecuteNonQuery();

                                    //if (ddlFieldForce != "admin")
                                    if (toddlFieldForce != "admin")
                                    {
                                        command.CommandText = "insert into Trans_Input_Despatch_Details(Trans_sl_No,Division_Code,Gift_Name,Despatch_Qty,productc,Despatch_Qty_Bk) values('" + Detail_Trans_Sl_No + "','" + div_code + "','" + lblProduct + "','" + transfrerQty + "','" + lblDR + "','" + transfrerQty + "')";

                                        command.ExecuteNonQuery();
                                    }

                                    command.CommandText = "Insert into Trans_Input_Transfer_Detail(Trans_Sl_No,Gift_Code,GiftS_Name,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR + "','" + Prod_Erp_Code + "','" + lbldespatch_qty + "','" + transfrerQty + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }
                            }
                            //transaction.Commit();
                            connection.Close();
                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                        Console.WriteLine("Message: {0}", ex.Message);


                        // Attempt to roll back the transaction.
                        try
                        {

                            //transaction.Rollback();

                        }

                        catch (Exception ex2)
                        {

                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                            Console.WriteLine("  Message: {0}", ex2.Message);

                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists while Transfer Input!');</script>");
                    }

                }

            }
            catch (Exception ex)
            {

            }

        }

    }

    //protected void lnk_Click(object sender, EventArgs e)
    //{
    //    txtFieldForceName.Enabled = true;
    //}


    //private void FillVacantManagers()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getVacantManagersonly(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlrepla.DataTextField = "sf_name";
    //        ddlrepla.DataValueField = "Sf_Code";
    //        ddlrepla.DataSource = dsSalesForce;
    //        ddlrepla.DataBind();
    //    }
    //}
}