using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Data.SqlClient;

public partial class MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsTerritory = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;
    string Doc_ClsCode = string.Empty;
    string Qual_Code = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string listeddrcode = string.Empty;
    string ListedDrCode = string.Empty;
    int request_type = -1;
    string request_doctor = string.Empty;
    int i;
    int iReturn = -1;
    string doctorcode = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string name1 = "";
    string id1 = "";
    string DR_Terr = string.Empty;
    DataSet dsadm = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        doctorcode = Request.QueryString["ListedDrCode"];
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = true;

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"].ToString() != "")
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            UserControl_MGR_Menu Usc_MR =
        (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                    "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = true;
            //Usc_MR.FindControl("btnBack").Visible = false;
        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"].ToString() != "")
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            UserControl_MenuUserControl Usc_Menu =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(Usc_Menu);
            //Divid.FindControl("btnBack").Visible = false;
            Usc_Menu.Title = this.Page.Title;
            //menu1.Visible = false;
            Session["backurl"] = "LstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;;'>For " + Session["sfName"] + " </span>" + " - " +
                             "<span style='font-weight: bold;color:#696D6E;;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:#696D6E;;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "LstDoctorList.aspx";
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillCategory();
            FillTerritory();
            FillSpeciality();
            FillClass();
            FillQualification();
            FillState();
            Call_Date();
            GetWorkName();
            ShowHideTerritory();

            if (Request.QueryString["type"] != null)
            {
                if ((Request.QueryString["type"].ToString() == "1") || (Request.QueryString["type"].ToString() == "2"))
                {
                    request_doctor = Convert.ToString(Request.QueryString["ListedDrCode"]);
                    request_type = Convert.ToInt16(Request.QueryString["type"]);
                    LoadDoctor(request_type, request_doctor);
                    BindGridviewData();
                }

            }

        }

    }

    private void ShowHideTerritory()
    {
        ListedDR lstDR = new ListedDR();
        iCnt = lstDR.Single_Multi_Select_Territory(div_code);
        ViewState["ShowHideTerritory"] = iCnt.ToString();
        if (iCnt == 1)
        {
            //   grdListedDR.Columns[8].Visible = false;
            //  grdListedDR.Columns[9].Visible = true;
            ddlTerritory.Visible = false;
            txtTerritory.Visible = true;
            txtTerritory.Text = "----Select----";
            ListedDR lst = new ListedDR();
            dsListedDR = lst.FetchTerritory(sf_code);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                ChkTerritory.DataTextField = "Territory_Name";
                ChkTerritory.DataValueField = "Territory_Code";
                ChkTerritory.DataSource = dsListedDR;
                ChkTerritory.DataBind();
            }
        }
        else
        {
            ddlTerritory.Visible = true;
            txtTerritory.Visible = false;



            //grdListedDR.Columns[8].Visible = true;
            //grdListedDR.Columns[9].Visible = false;
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
    private void GetWorkName()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            lblTerritory.Text =  dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString()+"<span style='Color:Red;padding-left:2px;'>" + "*" + "</span>" ;
        }
    }
    protected void Call_Date()
    {
        //for (int i = 1; i <= 31; i++)
        //{
        //    ddlDobDate.Items.Add(i.ToString());
        //}

        //for (int i = 1; i <= 31; i++)
        //{
        //    ddlDowDate.Items.Add(i.ToString());
        //}

        for (int k = DateTime.Now.Year - 90; k <= DateTime.Now.Year; k++)
        {
            ddlDobYear.Items.Add(k.ToString());
        }
        for (int k = DateTime.Now.Year - 90; k <= DateTime.Now.Year; k++)
        {
            ddlDowYear.Items.Add(k.ToString());
        }
    }

    private void LoadDoctor(int request_type, string request_doctor)
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.ViewListedDr(request_doctor);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            Listed_DR_Code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtAddress1.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            ddlCatg.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            ddlClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ddlClass.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            if (ViewState["ShowHideTerritory"].ToString() != "1")
            {
                ddlTerritory.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                ddlTerritory.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            }
            else
            {
                string value = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                txtTerritory.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                string[] strStateSplit = value.Split('~');
                foreach (string strstate in strStateSplit)
                {

                    string[] strchkstate;
                    strchkstate = txtTerritory.Text.Split('~');
                    foreach (string chkst in strchkstate)
                    {
                        for (int iIndex = 0; iIndex < ChkTerritory.Items.Count; iIndex++)
                        {
                            if (chkst.Trim() == ChkTerritory.Items[iIndex].Text.Trim())
                            {
                                ChkTerritory.Items[iIndex].Selected = true;

                            }
                        }
                    }
                }
            }
            txtStreet.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            txtCity.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            txtPin.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            txtTel.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
            txtMobile.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            txtEMail.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
            //ddlDowMonth.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
            //ddlDobMonth.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
            ddlState.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
            rdoProfile.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            chkVisit.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            txtIUICycle.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
            txtAvg.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
            txtDayTime.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
            txtHospital.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
            chkPatientClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
            rdoFees.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();
            txtHosAddress.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
            txtRegNo.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            //  rdoCommunication.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            txtERPcode.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
            GetDOB();
            GetDOW();
        }

        if (request_type == 1)
        {
            txtAddress1.Enabled = false;
            txtAvg.Enabled = false;
            txtCity.Enabled = false;
            txtDayTime.Enabled = false;
            ddlDowMonth.Enabled = false;
            ddlDobMonth.Enabled = false;
            ddlDobDate.Enabled = false;
            ddlDobYear.Enabled = false;
            ddlDowDate.Enabled = false;
            ddlDowYear.Enabled = false;
            txtEMail.Enabled = false;
            txtHospital.Enabled = false;
            txtHosAddress.Enabled = false;
            txtIUICycle.Enabled = false;
            txtMobile.Enabled = false;
            txtName.Enabled = false;
            txtPin.Enabled = false;
            txtRegNo.Enabled = false;
            txtStreet.Enabled = false;
            txtTel.Enabled = false;
            ddlCatg.Enabled = false;
            ddlClass.Enabled = false;
            ddlQual.Enabled = false;
            ddlSpec.Enabled = false;
            ddlState.Enabled = false;
            ddlTerritory.Enabled = false;
            btnSave.Enabled = false;
            rdoCommunication.Enabled = false;
            rdoFees.Enabled = false;
            rdoGender.Enabled = false;
            rdoProfile.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            txtTerritory.Enabled = false;
            txtERPcode.Enabled = false;
        }
    }

    private void GetDOB()
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.ViewListedDr(request_doctor);
        string strDate = dsListedDR.Tables[0].Rows[0]["ListedDr_DOB"].ToString();
        if (strDate != "")
        {
            for (int i = 0; i < ddlDobDate.Items.Count; i++)
            {
                if (ddlDobDate.Items[i].Text == strDate.ToString().Substring(0, 2))
                {
                    ddlDobDate.SelectedValue = i.ToString();
                }
            }


            for (int i = 0; i < ddlDobMonth.Items.Count; i++)
            {
                if (ddlDobMonth.Items[i].Text == strDate.ToString().Substring(3, 3))
                {
                    ddlDobMonth.SelectedValue = i.ToString();
                }
            }


            for (int i = 0; i < ddlDobYear.Items.Count; i++)
            {
                if (ddlDobYear.Items[i].Value == strDate.ToString().Substring(6, 5).Trim())
                {
                    ddlDobYear.SelectedValue = strDate.ToString().Substring(6, 5).Trim();
                }
            }
        }
        if (strDate == "01 Jan 1900")
        {
            ddlDobDate.SelectedValue = "01";
            ddlDobMonth.SelectedValue = "01";
        }

    }

    private void GetDOW()
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.ViewListedDr(request_doctor);
        string strDate = dsListedDR.Tables[0].Rows[0]["ListedDr_DOW"].ToString();
        if (strDate != "")
        {
            for (int i = 0; i < ddlDowDate.Items.Count; i++)
            {
                if (ddlDowDate.Items[i].Text == strDate.ToString().Substring(0, 2))
                {
                    ddlDowDate.SelectedValue = i.ToString(); ;
                }

            }

            for (int i = 0; i < ddlDowMonth.Items.Count; i++)
            {
                if (ddlDowMonth.Items[i].Text == strDate.ToString().Substring(3, 3))
                {
                    ddlDowMonth.SelectedValue = i.ToString();
                }
            }

            for (int i = 0; i < ddlDowYear.Items.Count; i++)
            {
                if (ddlDowYear.Items[i].Value == strDate.ToString().Substring(6, 5).Trim())
                {
                    ddlDowYear.SelectedValue = strDate.ToString().Substring(6, 5).Trim();
                }
            }
        }

        if (strDate == "01 Jan 1900")
        {
            ddlDowDate.SelectedValue = "01";
            ddlDowMonth.SelectedValue = "01";
        }

    }

    private void FillState()
    {
        ListedDR lst = new ListedDR();
        string divcode = Convert.ToString(lst.Div_Code(sf_code));
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(divcode);
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
            dsListedDR = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsListedDR;
            ddlState.DataBind();
        }
    }

    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsListedDR;
        ddlTerritory.DataBind();
        if (dsListedDR.Tables[0].Rows.Count <= 1)
        {
            Response.Redirect("../Territory/TerritoryCreation.aspx");
            // menu1.Status = "Territory must be created prior to Doctor creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
        }

    }

    private void FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        ddlCatg.DataTextField = "Doc_Cat_SName";
        ddlCatg.DataValueField = "Doc_Cat_Code";
        ddlCatg.DataSource = dsListedDR;
        ddlCatg.DataBind();
    }

    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        ddlSpec.DataTextField = "Doc_Special_SName";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        ddlQual.DataTextField = "Doc_QuaName";
        ddlQual.DataValueField = "Doc_QuaCode";
        ddlQual.DataSource = dsListedDR;
        ddlQual.DataBind();
    }
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        ddlClass.DataTextField = "Doc_ClsSName";
        ddlClass.DataValueField = "Doc_ClsCode";
        ddlClass.DataSource = dsListedDR;
        ddlClass.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string DR_Name = txtName.Text.Trim();
        string DR_Sex = rdoGender.SelectedValue;
        string DR_DOB = ddlDobMonth.SelectedValue + "-" + ddlDobDate.SelectedValue + "-" + ddlDobYear.SelectedValue;
        string DR_Qual = ddlQual.SelectedValue;
        string DR_DOW = ddlDowMonth.SelectedValue + "-" + ddlDowDate.SelectedValue + "-" + ddlDowYear.SelectedValue;
        string DR_Spec = ddlSpec.SelectedValue;
        string DR_RegNo = txtRegNo.Text.Trim();
        string DR_Catg = ddlCatg.SelectedValue;
        if (ViewState["ShowHideTerritory"].ToString() != "1")
        {
            DR_Terr = ddlTerritory.SelectedValue;
        }
        else
        {

            for (int i = 0; i < ChkTerritory.Items.Count; i++)
            {
                if (ChkTerritory.Items[i].Selected)
                {
                    //if (chkst.Items[i].Text != "ALL")
                    //{
                    name1 += ChkTerritory.Items[i].Text + "~";
                    id1 += ChkTerritory.Items[i].Value + "~";
                    //}
                }
            }

            DR_Terr = id1;
        }
        string DR_Comm = rdoCommunication.SelectedValue;
        string DR_Class = ddlClass.SelectedValue;
        string DR_Address1 = txtAddress1.Text.ToString();
        string DR_Address2 = txtStreet.Text.Trim();
        string DR_Address3 = txtCity.Text.Trim();
        string DR_State = ddlState.SelectedValue;
        string DR_Pin = txtPin.Text.Trim();
        string DR_Mobile = txtMobile.Text.Trim();
        string DR_Phone = txtTel.Text.Trim();
        string DR_EMail = txtEMail.Text.Trim();
        string DR_Profile = rdoProfile.SelectedValue;
        string DR_Visit_Days = chkVisit.SelectedValue;
        string DR_DayTime = txtDayTime.Text.Trim();
        string DR_IUI = txtIUICycle.Text.Trim();
        string DR_Avg_Patients = txtAvg.Text.Trim();
        string DR_Hospital = txtHospital.Text.Trim();
        string DR_Class_Patients = chkPatientClass.SelectedValue;
        string DR_Consultation_Fee = rdoFees.SelectedValue;
        request_doctor = Convert.ToString(Request.QueryString["ListedDrCode"]);
        string Hospital_Addr = txtHosAddress.Text.Trim();
        string Cat_SName = ddlCatg.SelectedItem.Text;
        string Spec_SName = ddlSpec.SelectedItem.Text;
        string Cls_SName = ddlClass.SelectedItem.Text;
        string Qua_SName = ddlQual.SelectedItem.Text;
        int iflag = -1;

        if (Session["sf_type"].ToString() == "1")
        {

            ListedDR lisapp = new ListedDR();
            dsadm = lisapp.getListDr_allow_app(div_code);
            if (dsadm.Tables[0].Rows.Count > 0)
            {
                if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                {
                    iflag = 0;
                }
                else
                {
                    iflag = 2;
                }
            }
        }
        else
        {
            iflag = 0;
        }


        if (doctorcode == null)
        //if ((DR_Name.Trim().Length > 0) && (DR_Address1.Trim().Length > 0) && (DR_Catg.Trim().Length > 0) && (DR_Spec.Trim().Length > 0) && (DR_Qual.Trim().Length > 0) && (DR_Class.Trim().Length > 0) && (DR_Terr.Trim().Length > 0))
        {
            // Add New Listed Doctor
            ListedDR lstDR = new ListedDR();
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            else if (Session["sf_code"] != null)
            {
                sf_code = Session["sf_code"].ToString();
            }

            if (iflag == 0)
            {

                iReturn = lstDR.RecordAdd(DR_Name, DR_Sex, DR_DOB, DR_Qual, DR_DOW, DR_Spec, DR_RegNo, DR_Catg, DR_Terr, DR_Comm, DR_Class,
                     DR_Address1, DR_Address2, DR_Address3, DR_State, DR_Pin, DR_Mobile, DR_Phone, DR_EMail, DR_Profile, DR_Visit_Days,
                     DR_DayTime, DR_IUI, DR_Avg_Patients, DR_Hospital, DR_Class_Patients, DR_Consultation_Fee, sf_code, Hospital_Addr, Cat_SName, Spec_SName, Cls_SName, Qua_SName, txtERPcode.Text, iflag);
            }
            else
            {


                iReturn = lstDR.RecordAdd_One(DR_Name, DR_Sex, DR_DOB, DR_Qual, DR_DOW, DR_Spec, DR_RegNo, DR_Catg, DR_Terr, DR_Comm, DR_Class,
                         DR_Address1, DR_Address2, DR_Address3, DR_State, DR_Pin, DR_Mobile, DR_Phone, DR_EMail, DR_Profile, DR_Visit_Days,
                         DR_DayTime, DR_IUI, DR_Avg_Patients, DR_Hospital, DR_Class_Patients, DR_Consultation_Fee, sf_code, Hospital_Addr, Cat_SName, Spec_SName, Cls_SName, Qua_SName, txtERPcode.Text, iflag);
            }

            if (iReturn > 0)
            {
                //menu1.Status = "Listed Doctor Created Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='ListedDR_DetailAdd.aspx'</script>");

                //FillListedDR();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Name Already Exist');</script>");

            }
        }
        else
        {
            //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            //if (FilUpImage.HasFile)
            //{
               
            //   string strname = FilUpImage.FileName.ToString();
            //   FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card/") + strname);
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("update mas_Listeddr set Visiting_Card='" + strname + "' where Division_Code='" + div_code + "'  and ListedDrCode='" + doctorcode + "'", con);
               
            //    cmd.ExecuteNonQuery();
               



            //   // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Updated Successfully');</script>");
            //    BindGridviewData();

                ListedDR lstDR = new ListedDR();
                iReturn = lstDR.RecordUpdate(request_doctor, DR_Name, DR_Sex, DR_DOB, DR_Qual, DR_DOW, DR_Spec, DR_RegNo, DR_Catg, DR_Terr, DR_Comm, DR_Class,
                            DR_Address1, DR_Address2, DR_Address3, DR_State, DR_Pin, DR_Mobile, DR_Phone, DR_EMail, DR_Profile, DR_Visit_Days,
                            DR_DayTime, DR_IUI, DR_Avg_Patients, DR_Hospital, DR_Class_Patients, DR_Consultation_Fee, sf_code, Hospital_Addr, Cat_SName, Spec_SName, Cls_SName, Qua_SName, txtERPcode.Text);




            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Visiting Card');</script>");
            //}

          

               
            if (iReturn > 0)
            {
                //menu1.Status = "Listed Doctor Created Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='LstDoctorList.aspx'</script>");

                //FillListedDR();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Name Already Exist');</script>");

            }
        }
        //else
        //{
        //    //menu1.Status = "Enter all the values!!";
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter all the values');</script>");
        //}




    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtAddress1.Text = "";
        txtCity.Text = "";
        txtHospital.Text = "";
        txtName.Text = "";
        txtHosAddress.Text = "";
        txtCity.Text = "";
        txtMobile.Text = "";
        txtPin.Text = "";
        txtRegNo.Text = "";
        txtStreet.Text = "";
        txtERPcode.Text = "";
        txtTel.Text = "";
        FillCategory();
        FillTerritory();
        FillSpeciality();
        FillClass();
        FillQualification();
        FillState();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("LstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
    protected void bt_upload_OnClick(object sender, EventArgs e)
    {
        //if (FileUpload1.HasFile)
        //{
        //    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card/") + fileName);
        //    Response.Redirect(Request.Url.AbsoluteUri);
        //}
        //  FileUpload FilUpImage = new FileUpload();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        //Label lblSF_Code = (Label)GrdImage.Rows[i].FindControl("lblSF_Code");
        if (FilUpImage.HasFile)
        {
            string strname = FilUpImage.FileName.ToString();
            FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card/") + strname);
            con.Open();
            //SqlCommand cmd = new SqlCommand("update mas_Listeddr set  values('" + txtname.Text + "','" + strname + "')", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            SqlCommand cmd = new SqlCommand("update mas_Listeddr set Visiting_Card='" + strname + "' where Division_Code='" + div_code + "'  and ListedDrCode='" + doctorcode + "'", con);


            //  cmd.Parameters.AddWithValue("@Visiting_Card", "~/Visiting_Card/" + strname);

            //  cmd.Parameters.AddWithValue("@Division_Code", div_code);
            ////  cmd.Parameters.AddWithValue("@Sf_Code", lblSF_Code.Text);
            //  cmd.Parameters.AddWithValue("@ListedDrCode", ListedDrCode);
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Updated Successfully');</script>");
            BindGridviewData();
            //Label1.Visible = true;
            //Label1.Text = "Image Uploaded successfully";
            //txtname.Text = "";
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Visiting Card');</script>");
        }
    }
    private void BindGridviewData()
    {
        //   DataSet ds = new DataSet();
        //ds.Clear();
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("select Visiting_Card from mas_Listeddr where Division_Code = '" + div_code + "' and ListedDrCode='" + doctorcode + "'", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);



        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select '~/Visiting_Card/'+ ISNULL(Visiting_Card,'0') Visiting_Card from mas_Listeddr where Division_Code = '" + div_code + "' and ListedDrCode='" + doctorcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables[0].Rows[0][0].ToString() != "0")
        {
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }
        else
        {
            DataList1.Visible = false;

        }

    }
    protected void ChkTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {

        //    GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        //   CheckBoxList chkst = (CheckBoxList)gv1.FindControl("ChkTerritory");
        //  TextBox txtstate = (TextBox)gv1.FindControl("txtTerritory");
        // HiddenField hdnStateId = (HiddenField)gv1.FindControl("hdnTerritoryId");
        txtTerritory.Text = "";
        hdnTerritoryId.Value = "";

        //if (chkst.Items[0].Selected == true)
        //{
        //    for (int i = 0; i < chkst.Items.Count; i++)
        //    {
        //        chkst.Items[i].Selected = true;
        //    }
        //}
        for (int i = 0; i < ChkTerritory.Items.Count; i++)
        {
            if (ChkTerritory.Items[i].Selected)
            {
                ChkTerritory.Items[i].Selected = true;
            }

        }

        //int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        //if (countSelected == chkst.Items.Count - 1)
        //{
        //    for (int i = 0; i < chkst.Items.Count; i++)
        //    {

        //        chkst.Items[i].Selected = false;
        //    }

        //}

        for (int i = 0; i < ChkTerritory.Items.Count; i++)
        {
            if (ChkTerritory.Items[i].Selected)
            {
                //if (chkst.Items[i].Text != "ALL")
                //{
                name1 += ChkTerritory.Items[i].Text + ",";
                id1 += ChkTerritory.Items[i].Value + ",";
                //}
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtTerritory.Text = name1.TrimEnd(',');
        hdnTerritoryId.Value = id1.TrimEnd(',');
        //chkst.Attributes.Add("onclick", "checkAll(this);");


    }

}