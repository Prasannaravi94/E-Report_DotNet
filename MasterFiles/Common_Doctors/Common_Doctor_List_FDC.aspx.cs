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
public partial class MasterFiles_Common_Doctor_List_FDC : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    DataSet dsSalesForce = null;
    DataSet dsadm = null;
    DataSet dsProd = new DataSet();
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Activity_Date = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    string sf_type = string.Empty;
    int iCnt = -1;
    string sf_code = string.Empty;
    string Find = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int i = 0;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsDivision = new DataSet();
    DataSet dslstCls = new DataSet();
    string Visiting_card = string.Empty;
    string sf_name = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_name = Session["sf_name"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        hdndivCode.Value = div_code;
        // menu1.FindControl("btnBack").Visible = false;
        Menu1.Title = Page.Title;
        if (!Page.IsPostBack)
        {
            if (div_code != "2")
            {
                rdoNew.Enabled = true;
            }
            //if (div_code == "8" || div_code == "9" || div_code == "10" || div_code == "2")
            //{
            //    rdoNew.Enabled = false;
            //}
            pnlNew.Visible = false;

            Call_Date();
            FillCity();
            FillState_Doc();
        }
        FindSalesForce(txtDoctor.Text, txtLast.Text, txtQual.Text, cboCountry.Text.Trim(), txtMob.Text, txtSt.Text, txtReg.Text, txtPin.Text);
    }
    private void FillCity()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.GetCity();
        cboCountry.DataTextField = "Drs_City";
        cboCountry.DataValueField = "Drs_City";
        cboCountry.DataSource = dsListedDR;
        cboCountry.DataBind();
        cboCountry.Items.Insert(0, "City");
       
    }
    private void FillState_Doc()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.GetState_Doc();
        txtSt.DataTextField = "State";
        txtSt.DataValueField = "State";
        txtSt.DataSource = dsListedDR;
        txtSt.DataBind();
        txtSt.Items.Insert(0, "State");
    }
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getCommonDr_List(div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
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
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Common_Doctor_Updation.aspx");
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Find = ddlFields.SelectedValue.ToString();
        // if (Find == "C_Doctor_Name" || Find == "C_Doctor_HQ")
        // {
        txtRef.Text = "";
        FindSalesForce(txtDoctor.Text, txtLast.Text, txtQual.Text, cboCountry.Text.Trim(), txtMob.Text, txtSt.Text, txtReg.Text, txtPin.Text);

        // }
        // else
        // {
        //     FillDoc();
        // }
    }

    protected void grdDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoctor.PageIndex = e.NewPageIndex;

        FindSalesForce(txtDoctor.Text, txtLast.Text, txtQual.Text, cboCountry.Text.Trim(), txtMob.Text, txtSt.Text, txtReg.Text, txtPin.Text);

    }
    private void FindSalesForce(string Doc_Name, string LastName, string Qual, string city, string mob, string st, string RegNo, string Pin)
    {
        if (Doc_Name == "FirstName")
        {
            Doc_Name = "";
            txtDoctor.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtDoctor.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtDoctor.Font.Bold = false;
        }
        else
        {
            txtDoctor.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtDoctor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtDoctor.Font.Bold = true;
        }
        if (LastName == "LastName")
        {
            LastName = "";
            txtLast.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtLast.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtLast.Font.Bold = false;
        }
        else
        {
            txtLast.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtLast.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtLast.Font.Bold = true;
        }

        if (Qual == "Qualification")
        {
            Qual = "";
            txtQual.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtQual.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtQual.Font.Bold = false;
        }
        else
        {
            txtQual.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtQual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtQual.Font.Bold = true;
        }
        if (city == "City")
        {
            city = "";
            cboCountry.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            cboCountry.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            cboCountry.Font.Bold = false;
        }
        else
        {
            cboCountry.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            cboCountry.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            cboCountry.Font.Bold = true;
        }
        if (mob == "Mobile")
        {
            mob = "";
            txtMob.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtMob.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtMob.Font.Bold = false;
        }
        else
        {
            txtMob.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtMob.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtMob.Font.Bold = true;
        }
        if (RegNo == "RegNo")
        {
            RegNo = "";
            txtReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtReg.Font.Bold = false;
        }
        else
        {
            txtReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtReg.Font.Bold = true;
        }
        if (Pin == "PinCode")
        {
            Pin = "";
            txtPin.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtPin.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtPin.Font.Bold = false;
        }
        else
        {
            txtPin.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtPin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtPin.Font.Bold = true;
        }
        if (st == "State")
        {
            st = "";
            txtSt.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F8FF");
            txtSt.ForeColor = System.Drawing.ColorTranslator.FromHtml("gray");
            txtSt.Font.Bold = false;
        }
        else
        {
            txtSt.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
            txtSt.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");
            txtSt.Font.Bold = true;
        }
        string sFind = string.Empty;
        string sFind1 = string.Empty;

        //  sFind = "  (C_Doctor_Name like '%" + Doc_Name + "%' and C_Doctor_Cat_Name like '% " + Cat + "%' and C_Doctor_HQ like '% " + Hq + "%' and C_Doctor_Qual_Name like '% " + Qual + "%' ) AND Division_Code in ('" + div_code + "') ";


        if (!string.IsNullOrWhiteSpace(Doc_Name))
        {
            sFind += " and ([C_Doctor_Name] like '%" + Doc_Name.Trim() + "%')";
        }


        if (!string.IsNullOrWhiteSpace(LastName))
        {
            sFind += " and ([C_Doctor_Name] like '%" + LastName.Trim() + "%')";
        }
        if (!string.IsNullOrWhiteSpace(Qual))
        {
            sFind += " and ([C_Doctor_Qual_Name] like '%" + Qual.Trim() + "%')";
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            sFind += " and ([Drs_City] like '%" + city.Trim() + "%')";
        }
        if (!string.IsNullOrWhiteSpace(mob))
        {
            sFind += " and ([C_Doctor_Mobile] like '%" + mob.Trim() + "%')";
        }
        if (!string.IsNullOrWhiteSpace(RegNo))
        {
            sFind += " and ([Drs_Registration_No] like '%" + RegNo.Trim() + "%')";
        }
        if (!string.IsNullOrWhiteSpace(Pin))
        {
            sFind += " and ([Pincode] like '%" + Pin.Trim() + "%')";
        }
        if (!string.IsNullOrWhiteSpace(st))
        {
            sFind += " and ([State] like '%" + st.Trim() + "%')";
        }

        if (sFind != "")
        {

            ListedDR sf = new ListedDR();
            dsSalesForce = sf.FindCommonDr_ALL(sFind, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsSalesForce;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsSalesForce;
                grdDoctor.DataBind();
            }
        }



    }
    protected void rdoEx_CheckedChanged(object sender, EventArgs e)
    {
        pnlexist.Visible = true;
        pnlNew.Visible = false;
    }
    protected void rdoNew_CheckedChanged(object sender, EventArgs e)
    {
        pnlNew.Visible = true;
        pnlexist.Visible = false;
        FillQualification();
       // FillState();
        FillSpeciality();
        FillTerritory();
        FillCategory();
        FillClass();
        FillProd1();
        FillProd2();
        FillProd3();
        FillProd4();
        FillProd5();

    }
    private void FillProd1()
    {
        Product prod = new Product();
        dsProd = prod.GetProductBrand(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            ddlProd1.DataTextField = "Product_Brd_Name";
            ddlProd1.DataValueField = "Product_Brd_Code";
            ddlProd1.DataSource = dsProd;
            ddlProd1.DataBind();
        }
    }
    private void FillProd2()
    {
        Product prod = new Product();
        dsProd = prod.GetProductBrand(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            ddlProd2.DataTextField = "Product_Brd_Name";
            ddlProd2.DataValueField = "Product_Brd_Code";
            ddlProd2.DataSource = dsProd;
            ddlProd2.DataBind();
        }
    }
    private void FillProd3()
    {
        Product prod = new Product();
        dsProd = prod.GetProductBrand(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            ddlProd3.DataTextField = "Product_Brd_Name";
            ddlProd3.DataValueField = "Product_Brd_Code";
            ddlProd3.DataSource = dsProd;
            ddlProd3.DataBind();
        }
    }
    private void FillProd4()
    {
        Product prod = new Product();
        dsProd = prod.GetProductBrand(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            ddlProd4.DataTextField = "Product_Brd_Name";
            ddlProd4.DataValueField = "Product_Brd_Code";
            ddlProd4.DataSource = dsProd;
            ddlProd4.DataBind();
        }
    }
    private void FillProd5()
    {
        Product prod = new Product();
        dsProd = prod.GetProductBrand(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            ddlProd5.DataTextField = "Product_Brd_Name";
            ddlProd5.DataValueField = "Product_Brd_Code";
            ddlProd5.DataSource = dsProd;
            ddlProd5.DataBind();
        }
    }
    private void FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Category(div_code);
        ddlCatg.DataTextField = "Doc_Cat_SName";
        ddlCatg.DataValueField = "Doc_Cat_Code";
        ddlCatg.DataSource = dsListedDR;
        ddlCatg.DataBind();
    }
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Class(div_code);
        ddlCls.DataTextField = "Doc_ClsSName";
        ddlCls.DataValueField = "Doc_ClsCode";
        ddlCls.DataSource = dsListedDR;
        ddlCls.DataBind();
    }

    private void FillTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Transfer(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlterr.DataTextField = "Territory_Name";
            ddlterr.DataValueField = "Territory_Code";
            ddlterr.DataSource = dsTerritory;
            ddlterr.DataBind();
        }
    }
    private void FillState()
    {
        ListedDR lst = new ListedDR();
        string divcode = Convert.ToString(lst.Div_Code(Session["sf_code"].ToString()));
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
    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        ddlSpec.DataTextField = "Doc_Special_Name";
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
  
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtDoctor.Text = "FirstName";
        txtQual.Text = "Qualification";

        cboCountry.Text = "City";
        txtMob.Text = "Mobile";
        txtReg.Text = "RegNo";
        txtPin.Text = "PinCode";
        txtSt.Text = "State";
        txtRef.Text = "";
        grdDoctor.Visible = false;


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataSet dsSpec = new DataSet();
        string spec_short_name = string.Empty;
        string DR_Name = txtName.Text.Trim();

        string DR_DOB = ddlDobMonth.SelectedValue + "-" + ddlDobDate.SelectedValue + "-" + ddlDobYear.SelectedValue;

        string DR_Qual = ddlQual.SelectedValue;
        string DR_DOW = ddlDowMonth.SelectedValue + "-" + ddlDowDate.SelectedValue + "-" + ddlDowYear.SelectedValue;
        string DR_Spec = ddlSpec.SelectedValue;
        string DR_Cat = ddlCatg.SelectedValue;
        string DR_RegNo = txtRegNo.Text.Trim();
        string DR_Address1 = txtAddress1.Text.Replace("'", "");
        string DR_Address3 = lblCity.Text.Trim();
        string DR_Pin = txtPincode.Text.Trim();
        string DR_Mobile = txtMobile.Text.Trim();
        string Cat_SName = ddlCatg.SelectedItem.Text;
        string Spec_SName = ddlSpec.SelectedItem.Text;
        string Qua_SName = ddlQual.SelectedItem.Text;
        string DR_Hospital = txtHospital.Text.Replace("'", "");
        string DR_Hos_Addr = txtHosAddress.Text.Replace("'", "");
        string DR_MR_Terr = ddlterr.SelectedValue;
        string DR_EMail = txtMail.Text.Trim();
        string DR_Lane = txtland.Text.Trim();
        int iReturn = -1;

        int Common_code;
        int ListerDrCode;
        string StrCls_Code = string.Empty;
        string StrCls_SName = string.Empty;
        Doctor dc = new Doctor();
        dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
        if (dsSpec.Tables[0].Rows.Count > 0)
        {
            spec_short_name = dsSpec.Tables[0].Rows[0]["Doc_Special_SName"].ToString();
        }
        StrCls_Code = ddlCls.SelectedValue;
        StrCls_SName = ddlCls.SelectedItem.Text.Trim();
        //ListedDR lstDR = new ListedDR();
        //dslstCls = lstDR.GetClass_Unique(div_code);
        //if (dslstCls.Tables[0].Rows.Count > 0)
        //{
        //  StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
        //  StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
        //}
        
        string Prod_Map = string.Empty;
        //ListedDR lstDR = new ListedDR();
        //iReturn = lstDR.Uni_Doc_ADD(DR_Name, DR_Address1, "", "", DR_Qual, Qua_SName, DR_Spec, Spec_SName, "", "", DR_Hos_Addr, DR_Mobile, "", "", "", "", "", div_code, DR_EMail, Qua_SName, Spec_SName, DR_Lane, DR_RegNo, DR_Address3, DR_Pin, ddlterr.SelectedItem.Text.Trim(), DR_DOB, DR_DOW, ddlState.SelectedItem.Text.Trim());
        //if (iReturn > 0)
        //{

        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");

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
                ListedDR objListedDR = new ListedDR();
                Common_code = objListedDR.GetCommonDrCode();

                if (FilUpImage.HasFile)
                {
                    Visiting_card = FilUpImage.FileName.ToString();
                    Visiting_card = Common_code + "_" + Visiting_card;
                    FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card_One/") + Visiting_card);

                }
                if (DR_Mobile.Trim() == "")
                {
                    DR_Mobile = "0";
                }
                command.CommandText = "insert into Mas_Common_Drs (C_Doctor_Code, C_Doctor_Name, C_Doctor_Address,C_Doctor_Cat_Code,C_Doctor_Cat_Name, " +
                      " C_Doctor_Qual_Code,C_Doctor_Qual_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,   " +
                      " C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_HQ,C_Territory_Code,C_Territory_Name,Allocated_IDs,Allocated_Id_Name,C_Created_Date,C_Active_Flag,Division_Code,Deactivate_Flag, " +
                      " Email_ID,Qual_Short_Name,Speciality_Short_Name,Drs_Landline_No,Drs_Registration_No,Drs_City,Pincode ,Ref_No,MR_HQ_Name,Unique_No,DOB,DOW,State,C_Visiting_Card,New_Unique,C_div_code,C_Doc_Hospital,MR_Raised_Date,MR_Raised_Name) " +
                    " VALUES('" + Common_code + "','" + DR_Name + "', " +
                    " '" + DR_Address1 + "', '" + ddlCatg.SelectedValue + "', '" + Cat_SName + "', '" + DR_Qual + "', '" + Qua_SName + "', " +
                    " '" + DR_Spec + "', '" + spec_short_name + "', '" + StrCls_Code + "', '" + StrCls_SName + "', '" + DR_Hos_Addr + "', '" + DR_Mobile + "', '', " +
                    " '', '', '" + sf_code + ',' + "', ''  , " +
                    "  getdate(), 2, '',0,'" + DR_EMail + "','" + Qua_SName + "','" + spec_short_name + "','" + DR_Lane + "','" + DR_RegNo + "', " +
                    " '" + DR_Address3 + "','" + DR_Pin + "','" + Common_code + "','" + ddlterr.SelectedItem.Text + "','" + Common_code + "','" + DR_DOB + "','" + DR_DOW + "','" + ddlState.SelectedItem.Text + "','" + Visiting_card + "','N','" + div_code + "','" + DR_Hospital + "',getdate(),'" + sf_name + "')";
                command.ExecuteNonQuery();

                Prod_Map = ddlProd1.SelectedItem.Text + "/" + ddlProd2.SelectedItem.Text + "/" + ddlProd3.SelectedItem.Text + "/" + ddlProd4.SelectedItem.Text + "/" + ddlProd5.SelectedItem.Text;
               

                ListedDR ListedDR = new ListedDR();
                ListerDrCode = objListedDR.GetListedDrCode_One();
                command.CommandText = "insert into Mas_ListedDr_One (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Hospital,Hospital_Address,ListedDr_Email,Doc_QuaCode,ListedDr_Phone,ListedDr_Mobile,Territory_Code,Doc_Special_Code,Doc_Cat_Code,Doc_ClsCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,Visit_Hours,visit_days,ListedDr_Sl_No,SLVNo,LastUpdt_Date, Doc_Type,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName,C_Doctor_Code,Visiting_Card,ListedDr_PinCode,State_Code,ListedDr_RegNo,ListedDr_DOB,ListedDr_DOW,City,Product_Map) " +
                 " VALUES('" + ListerDrCode + "', '" + sf_code + "', '" + DR_Name + "', '" + DR_Address1 + "','" + DR_Hospital + "','" + DR_Hos_Addr + "','" + DR_EMail + "','" + ddlQual.SelectedValue + "','" + DR_Lane + "','" + DR_Mobile + "', '" + ddlterr.SelectedValue + "', '" + ddlSpec.SelectedValue + "','" + ddlCatg.SelectedValue + "', '" + ddlCls.SelectedValue + "', 2, getdate(),'" + div_code + "', '','','" + ListerDrCode + "' ,'" + ListerDrCode + "',getdate(), '','" + ddlCatg.SelectedItem.Text.Trim() + "','" + spec_short_name.Trim() + "','" + ddlQual.SelectedItem.Text.Trim() + "','" + ddlCls.SelectedItem.Text.Trim() + "','" + Common_code + "','" + Visiting_card + "','" + DR_Pin + "','" + ddlState.SelectedItem.Text + "','" + DR_RegNo + "','" + DR_DOB + "','" + DR_DOW + "','" + DR_Address3 + "','" + Prod_Map + "')";


                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='Common_Doctor_List_FDC.aspx';</script>");
               

            }

            catch (Exception ex)
            {

                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                Console.WriteLine("Message: {0}", ex.Message);


                // Attempt to roll back the transaction.
                try
                {

                    transaction.Rollback();

                }

                catch (Exception ex2)
                {

                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                    Console.WriteLine("  Message: {0}", ex2.Message);

                }
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");

            }
        }
    }
    protected void btnRef_Click(object sender, EventArgs e)
    {
        grdDoctor.Visible = false;
        if (txtRef.Text.Trim() != "")
        {
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(" SELECT C_Doctor_Code,C_Doctor_Name,C_Doctor_Address,C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_Qual_Code, " +
                         " C_Doctor_Qual_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name, " +
                         " C_Doctor_Cat_Code,C_Doctor_Cat_Name,C_Created_Date,C_Approved_Date,Allocated_IDs,Allocated_Id_Name, " +
                         " C_Territory_Code,C_Territory_Name,C_Active_Flag,Division_Code,C_Doctor_HQ,Drs_City,Drs_Registration_No,Pincode,MR_HQ_Name,Unique_No,Qual_Short_Name,Speciality_Short_Name " +

                         " from Mas_Common_Drs where C_Doctor_Name !='' and C_Active_Flag=0 and c_doctor_code='" + txtRef.Text.Trim() + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = ds;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = ds;
                grdDoctor.DataBind();
            }
        }
        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Ref No.');</script>");
        //}

    }
}
