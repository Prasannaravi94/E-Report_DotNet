using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MIS_Reports_Employee_Application_Zoom : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string division_code = string.Empty;
    string Reporting_To_SF = string.Empty;
    string Sf_HQ = string.Empty;
    string sf_Designation_Short_Name = string.Empty;
    string imgName = string.Empty;
    DataSet dsAdminSetup = null;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_name = Session["SF_Name"].ToString();
        sf_code = Session["sf_code"].ToString();
        division_code = Session["div_code"].ToString();
        //Sf_HQ = Session["Sf_HQ"].ToString();
        if (!IsPostBack)
        {
            //if (Request.QueryString["type"] == "1")
            //{
            //    if (Session["sf_type"].ToString() == "2")
            //    {
            //        FillMRManagers1();
            //    }
            //    else if (Session["sf_type"].ToString() == "1")
            //    {
            //        FillMRManagers();
            //    }
            //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            //    {
            //        FillManagers();
            //    }
            //    //ddlFieldForce.Visible = true;
            //    //linkcheck.Visible = false;
            //    //txt_ddl_fieldforce.Visible = true;
            //}
            //else
            //{
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                getddlSF_Code();
                txtNew.Visible = false;

            }
            else
            {
                txtNew.Text = sf_name;
                ddlSFCode.Visible = false;
                txtNew.Enabled = false;
            }
            //}




            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Division_Name,Division_Add1,Division_Add2 from Mas_Division where Division_Code='" + division_code + "' ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblh.Text = ds.Tables[0].Rows[0].Field<string>("Division_Name");
                add1.Text = ds.Tables[0].Rows[0].Field<string>("Division_Add1");
                addd2.Text = ds.Tables[0].Rows[0].Field<string>("Division_Add2");
            }


        }
       

    }
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(division_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();

            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                sf_code = ddlSFCode.SelectedValue.ToString();
                Session["sf_code_Temp"] = sf_code;
            }

        }

    }
    private void BindGridviewData()
    {


        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select top 1 ISNULL(Photogragh,'0') Photogragh from Trans_Personal_Data_Head where sf_code = '" + sf_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        // if (ds.Tables[0].Rows[0][0].ToString() != "0")

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }
        else
        {
            DataList1.Visible = false;

        }

    }
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        FillMRManagers1();
    //    }
    //    else if (Session["sf_type"].ToString() == "1")
    //    {
    //        FillMRManagers();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillManagers();
    //    }
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txt_ddl_fieldforce.Visible = true;
    //    //btnSubmit.Enabled = true;

    //}
    //private void FillMRManagers()
    //{

    //    SalesForce sf = new SalesForce();
    //    //ddlFFType.Visible = false;  
    //    //    ddlAlpha.Visible = false;
    //    dsSalesForce = sf.SalesForceListMgrGet(division_code, sf_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "Desig_Color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();


    //    }

   // }
    //private void FillMRManagers1()
    //{
    //    SalesForce sf = new SalesForce();

    //    dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(division_code, sf_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();



    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();
    //    }



    //}

    //private void FillManagers()
    //{

    //    SalesForce sf = new SalesForce();


    //    dsSalesForce = sf.UserListTP_Hierarchy_Sale(division_code, "admin");


    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }

    //}
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
            Response.Redirect("~/Default_MR.aspx");
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            Response.Redirect("~/MGR_Home.aspx");
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

    }
   
    protected void btn_go_click(object sender, EventArgs e)
    {
        pnlhidden.Visible = true;
        pnlhide.Visible = true;
        pnl_hide2.Visible = true;
        this.chkNew.Enabled = false;
        this.chkgender.Enabled = false;
        this.chk_marrital.Enabled = false;
        this.chk_diesease.Enabled = false;
        this.chk_mode.Enabled = false;
        this.chk_legal_oblig.Enabled = false;
        this.chk_crime.Enabled = false;

        //Label1.Visible = true;
        //Label2.Visible = true;
        //Label3.Visible = true;

        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.personal_data_view(sf_code);
         if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
           // chkNew.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Emp_Name"].ToString();
            string refer = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            if(refer!="")
            {
                chkNew.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }
           
            txt_emp_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txt_emp_code.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txt_full_name.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            panno.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            adrname.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            bnknme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            branchname.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            adrno.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            accno.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ifsccode.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            add_cmtn.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            mob_no.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            mail_id.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            tel_no.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            per_add.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
            per_telno.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            per_mob_no.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
            txt_dte_of_birth.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
            txtvillage.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
            txt_city.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
            txt_taluk.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            txt_state.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            txt_district.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
            txt_country.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
            string gender = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
            if (gender != "")
            {
            chkgender.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
            }
            txt_religion.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
            string m_status = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
            if (m_status !="")
            {
              chk_marrital.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
            }
            if (chk_marrital.SelectedValue == "UNMRD")
            {
                txt_wed_dte.Text = "";
            }
            else
            {
                txt_wed_dte.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();
            }
            txt_bld_gp.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
            txt_lg_sight.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            txt_sht_sight.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
            txt_illnes.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();
            string disease = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
            if(disease!="")
            {
                chk_diesease.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
            }
           
            txt_father_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
            txt_mother_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
            txt_spouse_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
            txt_child1_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(38).ToString();
            txt_child2_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(39).ToString();
            txt_bro1_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(40).ToString();
            txt_bro2_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(41).ToString();
            txt_sis1_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();
            txt_sis2_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(43).ToString();
            txt_father_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(44).ToString();
            txt_mother_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(45).ToString();
            txt_spouse_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(46).ToString();
            txt_child1_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(47).ToString();
            txt_child2_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(48).ToString();
            txt_bro1_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(49).ToString();
            txt_bro2_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(50).ToString();
            txt_sis1_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(51).ToString();
            txt_sis2_age.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(52).ToString();
            txt_dob.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(53).ToString();
            txt_dob_mother.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(54).ToString();
            txt_dob_spouse.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(55).ToString();
            txt_dob_child1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(56).ToString();
            txt_dob_child2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(57).ToString();
            txt_dob_bro1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(58).ToString();
            txt_dob_bro2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(59).ToString();
            txt_dob_sis1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(60).ToString();
            txt_dob_sis2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(61).ToString();
            txt_occ.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(62).ToString();
            txt_occ_mother.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(63).ToString();
            txt_occ_spouse.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(64).ToString();
            txt_occ_child1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(65).ToString();
            txt_occ_child2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(66).ToString();
            txt_occ_bro1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(67).ToString();
            txt_occ_bro2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(68).ToString();
            txt_occ_sis1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(69).ToString();
            txt_occ_sis2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(70).ToString();
            family_add_1.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(71).ToString();
            add2.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(72).ToString();
            txt_X_institude.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(73).ToString();
            txt_inter_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(74).ToString();
            txt_grad_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(75).ToString();
            txt_PG_gradr_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(76).ToString();
            txt_other_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(77).ToString();
            txt_board.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(78).ToString();
            txt_inter_board.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(79).ToString();
            txt_grad_univ.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(80).ToString();
            txt_PG_grad_univ.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(81).ToString();
            txt_other_univ.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(82).ToString();
            Yr_from.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(83).ToString();
            from_yr_inter.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(84).ToString();
            from_yr_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(85).ToString();
            from_yr_PG_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(86).ToString();
            from_yr_other.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(87).ToString();
            Yr_to.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(88).ToString();
            To_yr_inter.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(89).ToString();
            To_yr_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(90).ToString();
            To_yr_PG_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(91).ToString();
            To_yr_other.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(92).ToString();
            txt_Medium.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(93).ToString();
            medium_inter.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(94).ToString();
            medium_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(95).ToString();
            medium_PG_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(96).ToString();
            medium_other.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(97).ToString();
            txt_special.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(98).ToString();
            txt_spcl_inter.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(99).ToString();
            txt_spcl_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(100).ToString();
            txt_spcl_PG_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(101).ToString();
            txt_spcl_other.Text = dsAdminSetup.Tables[0].Rows[0]["Others_Specialization"].ToString();
            txt_marks.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(102).ToString();
            txt_marks_inter.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(103).ToString();
            txt_marks_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(104).ToString();
            txt_marks_PG_grad.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(105).ToString();
            txt_marks_other.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(106).ToString();
            txt_course_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(107).ToString();
            txt_univer_name.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(108).ToString();
            course_duration.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(109).ToString();
            string mode = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(110).ToString();
            if(mode!="")
            {
                chk_mode.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(110).ToString();
            }
           
            txt_compl_yr.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(111).ToString();
            Aca_achieve.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(112).ToString();
            Extra_curricular.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(113).ToString();
            txtorg1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(114).ToString();
            txt_org_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(115).ToString();
            txt_org_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(116).ToString();
            txt_frm_my.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(117).ToString();
            txt_frm_my_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(118).ToString();
            txt_frm_my_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(119).ToString();
            txt_to_my.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(120).ToString();
            txt_to_my_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(121).ToString();
            txt_to_my_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(122).ToString();
            txt_duration.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(123).ToString();
            txt_duration_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(124).ToString();
            txt_duration_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(125).ToString();
            txt_full_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(126).ToString();
            txt_full_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(127).ToString();
            txt_full_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(128).ToString();
            txt_designation.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(129).ToString();
            txt_designation_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(130).ToString();
            txt_designation_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(131).ToString();
            txt_reason_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(132).ToString();
            txt_reason_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(133).ToString();
            txt_reason_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(134).ToString();
            txt_last_drawn_salary.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(135).ToString();
            txt_last_drawn_salary_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(136).ToString();
            txt_last_drawn_salary_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(137).ToString();
            txt_nomini_nme.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(138).ToString();
            txt_nomini_relation.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(139).ToString();
            txt_contact_nomini.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(140).ToString();
            txt_nomini_add.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(141).ToString();
            txt_nme_add.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(142).ToString();
            txt_nme_add2.InnerText = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(143).ToString();
            txt_occ1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(144).ToString();
            txt_occ2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(145).ToString();
            txt_mail1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(146).ToString();
            txt_mail2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(147).ToString();
            txt_num1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(148).ToString();
            txt_num2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(149).ToString();
            string known = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(150).ToString();
            if(known!="")
            {
                chk_vivo_known_emp.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(150).ToString();
            }
           
            txt_vivo_emp_name.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(151).ToString();
            txt_vivo_desig_name.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(152).ToString();
            txt_vivo_relation.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(153).ToString();
            txt_vivo_contact.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(154).ToString();
            txt_mother_tongue.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(155).ToString();
            lang_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(156).ToString();
            lang2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(157).ToString();
            lang_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(158).ToString();
            under_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(159).ToString();
            under_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(160).ToString();
            under_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(161).ToString();
            speak_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(162).ToString();
            speak_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(163).ToString();
            speak_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(164).ToString();
            read_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(165).ToString();
            read_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(166).ToString();
            read_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(167).ToString();
            write_1.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(168).ToString();
            write_2.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(169).ToString();
            write_3.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(170).ToString();
            string oblig = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(171).ToString();
            if(oblig!="")
            {
                chk_legal_oblig.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(171).ToString();
            }
           
            txt_obligation.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(172).ToString();
            string crime = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(173).ToString();
            if(crime!="")
            {
                chk_crime.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(173).ToString();
            }
            
            txt_crime_detail.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(174).ToString();
            txt_dte.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(175).ToString();
            txt_sig.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(176).ToString();
            off_dte_application.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(177).ToString();
            txt_empcode.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(178).ToString();
            txt_dte_accept.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(179).ToString();
            txt_depart.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(180).ToString();
            txt_sig_hr.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(181).ToString();
            off_join_dte.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(182).ToString();
            txt_desig_offuse.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(183).ToString();
            txt_report_relation.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(184).ToString();
            txt_loc.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(185).ToString();
            txt_corporat_email.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(186).ToString();
            BindGridviewData();


        }
      else
        {
            pnlhidden.Visible = false;
           // pnlhide.Visible = false;
            tablenorec.Visible = true;
            lblnorec.Text = "No Record Found!";
        }
    
        }
       
    

}