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
public partial class MasterFiles_Common_Doctors_Unique_Dr_View_admin : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    DataSet dsSalesForce = null;
    DataSet dsadm = null;
    DataSet dsPro1 = new DataSet();
    DataSet dsPro2 = new DataSet();
    DataSet dsPro3 = new DataSet();
    DataSet dsPro4 = new DataSet();
    DataSet dsPro5 = new DataSet();
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
    string MrCode = string.Empty;
    int request_doctor;
    string Listeddrcode = string.Empty;
    string Common_DR_Code = string.Empty;
    string typedr = string.Empty;
    string Product_Map = string.Empty;
    DataSet dsProd = new DataSet();
    DataSet dsSpec = new DataSet();
    string sf_name = string.Empty;
    string Spec_SName = string.Empty;
    string spec_short_name = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_name = Session["sf_name"].ToString();
        MrCode = Request.QueryString["mrcode"].ToString();
        request_doctor = Convert.ToInt32(Request.QueryString["C_Doctor_Code"]);
        Listeddrcode = Request.QueryString["ListedDrCode"];
        typedr = Request.QueryString["type"].ToString();

        
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
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
        if (!Page.IsPostBack)
        {
            FillQualification();
            //FillState();
            FillSpeciality();
            FillTerritory();
            FillCategory();
            FillClass();
            Call_Date();
            FillProd1();
            FillProd2();
            FillProd3();
            FillProd4();
            FillProd5();
                FillDoc();
                FindSalesForce(txtName.Text, ddlQual.SelectedItem.Text, lblCity.Text.Trim(), txtMobile.Text, lblState.Text, txtRegNo.Text, txtpin.Text);
           
        }
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
    private void FindSalesForce(string Doc_Name, string Qual, string city, string mob, string st, string RegNo, string Pin)
    {

        string sFind = string.Empty;
        string sFind1 = string.Empty;

        //  sFind = "  (C_Doctor_Name like '%" + Doc_Name + "%' and C_Doctor_Cat_Name like '% " + Cat + "%' and C_Doctor_HQ like '% " + Hq + "%' and C_Doctor_Qual_Name like '% " + Qual + "%' ) AND Division_Code in ('" + div_code + "') ";


      //  sFind += " and ( ([C_Doctor_Name] = '" + Doc_Name.Trim() + "')  )  ";     

        sFind += " and ([C_Doctor_Mobile] != '') and ( (([C_Doctor_Name] = '" + Doc_Name.Trim() + "')  and ([Drs_City] = '" + city.Trim() + "')) or (([C_Doctor_Name] = '" + Doc_Name.Trim() + "')  and (C_Doctor_Qual_Name = '" + Qual.Trim() + "')) or ([C_Doctor_Mobile] = '" + mob.Trim() + "')) and C_Active_Flag=0 ";     
       
       


        if (sFind != "")
        {

            ListedDR sf = new ListedDR();
            dsSalesForce = sf.FindCommonDr_ALL_Exist(sFind, sf_code);
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
        dsTerritory = terr.getTerritory_Transfer(MrCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlterr.DataTextField = "Territory_Name";
            ddlterr.DataValueField = "Territory_Code";
            ddlterr.DataSource = dsTerritory;
            ddlterr.DataBind();
        }
    }
    //private void FillState()
    //{
    //    ListedDR lst = new ListedDR();
    //    string divcode = Convert.ToString(lst.Div_Code(MrCode));
    //    Division dv = new Division();
    //    dsDivision = dv.getStatePerDivision(divcode);
    //    if (dsDivision.Tables[0].Rows.Count > 0)
    //    {
    //        int i = 0;
    //        state_cd = string.Empty;
    //        sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //        statecd = sState.Split(',');
    //        foreach (string st_cd in statecd)
    //        {
    //            if (i == 0)
    //            {
    //                state_cd = state_cd + st_cd;
    //            }
    //            else
    //            {
    //                if (st_cd.Trim().Length > 0)
    //                {
    //                    state_cd = state_cd + "," + st_cd;
    //                }
    //            }
    //            i++;
    //        }

    //        State st = new State();
    //        dsListedDR = st.getState(state_cd);
    //        ddlState.DataTextField = "statename";
    //        ddlState.DataValueField = "state_code";
    //        ddlState.DataSource = dsListedDR;
    //        ddlState.DataBind();
    //    }
    //}
    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(MrCode);
        ddlSpec.DataTextField = "Doc_Special_Name";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(MrCode);
        ddlQual.DataTextField = "Doc_QuaName";
        ddlQual.DataValueField = "Doc_QuaCode";
        ddlQual.DataSource = dsListedDR;
        ddlQual.DataBind();
    }
    private void FillDoc()
    {
   
        ListedDR lst2 = new ListedDR();
        if (typedr == "1")
        {
            dsListedDR = lst2.getNew_dr_MGR_View(MrCode,div_code, Listeddrcode);
        }
        else if (typedr == "2")
        {
            dsListedDR = lst2.getNew_dr_admin_View(MrCode, div_code, Listeddrcode);
        }
        else if (typedr == "5")
        {
            dsListedDR = lst2.getNew_dr_MR_Entry(MrCode, div_code, Listeddrcode);
            Panel1.Visible = false;
            btnReject.Visible = false;
        }
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
          //  Common_DR_Code = dsListedDR.Tables[0].Rows[0]["C_Doctor_Code"].ToString();
            txtName.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Name"].ToString();
            txtAddress1.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Address1"].ToString();
            txtHosAddress.Text = dsListedDR.Tables[0].Rows[0]["Hospital_Address"].ToString();
            txtHospital.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Hospital"].ToString();
            txtMobile.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Mobile"].ToString();
            ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_QuaCode"].ToString();
            ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_QuaName"].ToString();
            ddlCls.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_ClsCode"].ToString();
            ddlCls.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_ClsName"].ToString();


            lblState.Text = dsListedDR.Tables[0].Rows[0]["State_Code"].ToString();
            ddlCatg.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Code"].ToString();
            ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Name"].ToString();

            ddlterr.SelectedValue = dsListedDR.Tables[0].Rows[0]["territory_code"].ToString();
            ddlterr.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["territory_Name"].ToString();

            //   ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Qual_Short_Name"].ToString();
            txtland.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Phone"].ToString();
            txtRegNo.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_RegNo"].ToString();
            ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Special_Code"].ToString();
           // ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_Special_Name"].ToString();
            Doctor dc = new Doctor();
            dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
            if (dsSpec.Tables[0].Rows.Count > 0)
            {
                Spec_SName = dsSpec.Tables[0].Rows[0]["Doc_Special_Name"].ToString();
            }
            ddlSpec.SelectedItem.Text = Spec_SName.Trim();
            txtpin.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_PinCode"].ToString();
            lblCity.Text = dsListedDR.Tables[0].Rows[0]["City"].ToString();
            //  strsf_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            //  str_terrcode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            //  lblHQ.Text = dsListedDR.Tables[0].Rows[0]["MR_HQ_Name"].ToString();
            txtMail.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Email"].ToString();
            lblVis.Text = dsListedDR.Tables[0].Rows[0]["Visiting_Card"].ToString();
            Product_Map = dsListedDR.Tables[0].Rows[0]["Product_Map"].ToString();
            string[] arr = new string[5];
            arr = Product_Map.Split('/');
            if (arr[0] != "")
            {
                ddlProd1.SelectedItem.Text = arr[0];
            }
            if (arr[1] != "")
            {
                ddlProd2.SelectedItem.Text = arr[1];
            }
            if (arr[2] != "")
            {
                ddlProd3.SelectedItem.Text = arr[2];
            }
            if (arr[3] != "")
            {
                ddlProd4.SelectedItem.Text = arr[3];
            }
            if (arr[4] != "")
            {
                ddlProd5.SelectedItem.Text = arr[4];
            }
            BindVisiting();
            GetDOB();
            GetDOW();
            //if (typedr == "1")
            //{
            //    txtName.Enabled = false;
            //    txtpin.Enabled = false;
            //    txtRegNo.Enabled = false;
            //    txtAddress1.Enabled = false;
            //    txtHosAddress.Enabled = false;
            //    txtHospital.Enabled = false;
            //    txtland.Enabled = false;
            //    txtMobile.Enabled = false;
            //    txtMail.Enabled = false;
            //    lblState.Enabled = false;
            //    lblCity.Enabled = false;
            //    ddlDobMonth.Enabled = false;
            //    ddlDobYear.Enabled = false;
            //    ddlDobDate.Enabled = false;
            //    ddlDowDate.Enabled = false;
            //    ddlDowMonth.Enabled = false;
            //    ddlDowYear.Enabled = false;


            //    FilUpImage.Enabled = false;
            //    ddlterr.Enabled = false;
            //    ddlQual.Enabled = false;
            //    ddlSpec.Enabled = false;
            //    ddlCatg.Enabled = false;
            //    ddlCls.Enabled = false;
            //    ddlProd1.Enabled = false;
            //    ddlProd2.Enabled = false;
            //    ddlProd3.Enabled = false;
            //    ddlProd4.Enabled = false;
            //    ddlProd5.Enabled = false;
            //}
        }
    }
    private void BindVisiting()
    {
        

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select '~/Visiting_Card_One/'+Visiting_Card as Visiting_Card from mas_Listeddr_One where Division_Code = '" + div_code + "' and ListedDrCode='" + Request.QueryString["ListedDrCode"] + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        con.Close();
        if (ds.Tables[0].Rows[0][0].ToString() != "" || ds.Tables[0].Rows[0][0].ToString() != "NULL")
        {
            // this.imgVisFile.ImageUrl = "Visiting_Card/" + ds.Tables[0].Rows[0][0].ToString();

            //   string[] filePaths = Directory.GetFiles(Server.MapPath("~/Visiting_Card/"));
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }
        else
        {
            imgVisFile.Visible = false;
        }
    }
    private void GetDOB()
    {
        ListedDR lst = new ListedDR();
        if (typedr == "1")
        {
            dsListedDR = lst.getNew_dr_MGR_View(MrCode, div_code, Listeddrcode);
        }
        else if (typedr == "2")
        {
            dsListedDR = lst.ViewListedDr_New(Listeddrcode, div_code);
        }
        else if (typedr == "5")
        {
            dsListedDR = lst.getNew_dr_MR_Entry(MrCode, div_code, Listeddrcode);
        }
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
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

    }

    private void GetDOW()
    {
        ListedDR lst = new ListedDR();
        if (typedr == "1")
        {
            dsListedDR = lst.getNew_dr_MGR_View(MrCode, div_code, Listeddrcode);
        }
        else if (typedr == "2")
        {
            dsListedDR = lst.ViewListedDr_New(Listeddrcode, div_code);
        }
        else if (typedr == "5")
        {
            dsListedDR = lst.getNew_dr_MR_Entry(MrCode, div_code, Listeddrcode);
        }
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
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

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (typedr == "1")
        {
            //ListedDR LstDoc = new ListedDR();
            //int iReturn = -1;
            ////  iReturn = LstDoc.Approve(sQryStr, ListedDR, 1, 3);
            //iReturn = LstDoc.ApproveAdd_New_Unique(MrCode, Listeddrcode, 5, 2, "", "");
            //if (iReturn > 0)
            //{
            //    // menu1.Status = "Listed Doctor has been Approved Successfully";
            //  //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unique Doctor has been Approved Successfully');{ self.close() };</script>");
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Sent to admin approval');window.close();</script>");
               
            //}
            if (FilUpImage.HasFile)
            {
                Visiting_card = FilUpImage.FileName.ToString();
                Visiting_card = Request.QueryString["C_Doctor_Code"] + "_" + Visiting_card;
                
                FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card_One/") + Visiting_card);

            }
            else
            {
                Visiting_card = lblVis.Text;
            }
              string DR_Addr = txtAddress1.Text.Replace("'", "");
            string DR_Hospital = txtHospital.Text.Replace("'", "");
            string DR_Hos_Addr = txtHosAddress.Text.Replace("'", "");
            string DR_DOB = ddlDobMonth.SelectedValue + "-" + ddlDobDate.SelectedValue + "-" + ddlDobYear.SelectedValue;
            string DR_DOW = ddlDowMonth.SelectedValue + "-" + ddlDowDate.SelectedValue + "-" + ddlDowYear.SelectedValue;
            string DR_Mobile = txtMobile.Text.Trim();
             if (DR_Mobile.Trim() == "")
                {
                    DR_Mobile = "0";
                }
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
                    int iReturn = -1;
                    int ListerDrCodeNew;
                    ListedDR LstDoc = new ListedDR();
                    Product pro = new Product();
                    Doctor dc = new Doctor();
                    dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
                    if (dsSpec.Tables[0].Rows.Count > 0)
                    {
                        spec_short_name = dsSpec.Tables[0].Rows[0]["Doc_Special_SName"].ToString();
                    }
                    //  int iRet = LstDoc.Final_App_FDC_New(MrCode, Listeddrcode, 0, 5, "", "", Request.QueryString["C_Doctor_Code"]);
                    command.CommandText = " update Mas_ListedDr_One " +
                                            " set listeddr_active_flag=5, ListedDr_Name='" + txtName.Text + "',Territory_Code='" + ddlterr.SelectedValue + "', Doc_ClsCode='" + ddlCls.SelectedValue + "'," +
                                            " Doc_Cat_Code='" + ddlCatg.SelectedValue + "', Doc_Special_Code='" + ddlSpec.SelectedValue + "', Doc_QuaCode='" + ddlQual.SelectedValue + "', " +
                                            " Doc_Cat_ShortName = '" + ddlCatg.SelectedItem.Text.Trim() + "', Doc_Spec_ShortName = '" + spec_short_name.Trim() + "', Doc_Class_ShortName = '" + ddlCls.SelectedItem.Text.Trim() + "', Doc_Qua_Name = '" + ddlQual.SelectedItem.Text.Trim() + "', " +
                                            " ListedDr_PinCode='" + txtpin.Text + "',ListedDr_RegNo='" + txtRegNo.Text + "',ListedDr_DOB='" + DR_DOB + "',ListedDr_DOW='" + DR_DOW + "', " +
                                            " City='" + lblCity.Text + "',ListedDr_Phone='" + txtland.Text + "',ListedDr_Mobile='" + txtMobile.Text + "'," +
                                            " ListedDr_Address1='" + DR_Addr + "',ListedDr_Hospital='" + DR_Hospital + "',Hospital_Address='" + DR_Hos_Addr + "', " +
                                            " ListedDr_Email ='" + txtMail.Text.Trim() + "',Visiting_Card='" + Visiting_card + "',Listeddr_App_Mgr='" + sf_name + "' " +
                                            " where ListedDrCode='" + Listeddrcode + "' and sf_code='" + MrCode + "' ";

                    command.ExecuteNonQuery();
               

                   

                    command.CommandText = "update Mas_Common_Drs set C_Doctor_Name='" + txtName.Text.Trim() + "', C_Doctor_Address='" + DR_Addr + "', " +
                                          " C_Doctor_Cat_Code='" + ddlCatg.SelectedValue + "',C_Doctor_Cat_Name = '" + ddlCatg.SelectedItem.Text.Trim() + "', " +
                                          " C_Doctor_Qual_Code='" + ddlQual.SelectedValue + "',C_Doctor_Qual_Name='" + ddlQual.SelectedItem.Text.Trim() + "',C_Doctor_Spl_Code='" + ddlSpec.SelectedValue + "',C_Doctor_Spl_Name='" + spec_short_name.Trim() + "', " +
                                          " C_Doctor_Cls_Code='" + ddlCls.SelectedValue + "' ,C_Doctor_Cls_Name='" + ddlCls.SelectedItem.Text.Trim() + "',  " +
                                          " C_Doctor_Hos_Addr='" + DR_Hos_Addr + "',C_Doctor_Mobile='" + DR_Mobile + "', " +
                                          " Drs_Landline_No='" + txtland.Text + "',Drs_Registration_No='" + txtRegNo.Text + "',Drs_City='" + lblCity.Text + "',Pincode='" + txtpin.Text + "', " +
                                          " DOB ='" + DR_DOB + "', DOW='" + DR_DOW + "',C_Doc_Hospital='" + DR_Hospital + "',C_Visiting_Card='" + Visiting_card + "',MGR_Approved_Date=getdate(),MGR_Approved_Name='" + sf_name + "' " +
                                          " where c_doctor_code='" + Request.QueryString["C_Doctor_Code"] + "' ";
                    command.ExecuteNonQuery();


                 

                    transaction.Commit();
                    connection.Close();
                 
                        // menu1.Status = "Listed Doctor has been Approved Successfully";
                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unique Doctor has been Approved Successfully');{ self.close() };window.opener.location.reload();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Sent to admin approval');window.close();</script>");
                    

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
        else if (typedr == "2")
        {
            if (FilUpImage.HasFile)
            {
                Visiting_card = FilUpImage.FileName.ToString();
                Visiting_card = Request.QueryString["C_Doctor_Code"] + "_" + Visiting_card;
                
                FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card_One/") + Visiting_card);

            }
            else
            {
                Visiting_card = lblVis.Text;
            }
            string DR_Mobile = txtMobile.Text.Trim();
            if (DR_Mobile.Trim() == "")
            {
                DR_Mobile = "0";
            }
            string DR_Addr = txtAddress1.Text.Replace("'", "");
            string DR_Hospital = txtHospital.Text.Replace("'", "");
            string DR_Hos_Addr = txtHosAddress.Text.Replace("'", "");
            string DR_DOB = ddlDobMonth.SelectedValue + "-" + ddlDobDate.SelectedValue + "-" + ddlDobYear.SelectedValue;
            string DR_DOW = ddlDowMonth.SelectedValue + "-" + ddlDowDate.SelectedValue + "-" + ddlDowYear.SelectedValue;
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
                    int iReturn = -1;
                    int ListerDrCodeNew;
                    ListedDR LstDoc = new ListedDR();
                    Product pro = new Product();
                    Doctor dc = new Doctor();
                    dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
                    if (dsSpec.Tables[0].Rows.Count > 0)
                    {
                        spec_short_name = dsSpec.Tables[0].Rows[0]["Doc_Special_SName"].ToString();
                    }
                    //  int iRet = LstDoc.Final_App_FDC_New(MrCode, Listeddrcode, 0, 5, "", "", Request.QueryString["C_Doctor_Code"]);
                    command.CommandText = " update Mas_ListedDr_One " +
                                            " set ListedDr_Name='" + txtName.Text + "',Territory_Code='" + ddlterr.SelectedValue + "', Doc_ClsCode='" + ddlCls.SelectedValue + "'," +
                                            " Doc_Cat_Code='" + ddlCatg.SelectedValue + "', Doc_Special_Code='" + ddlSpec.SelectedValue + "', Doc_QuaCode='" + ddlQual.SelectedValue + "', " +
                                            " Doc_Cat_ShortName = '" + ddlCatg.SelectedItem.Text.Trim() + "', Doc_Spec_ShortName = '" + spec_short_name.Trim() + "', Doc_Class_ShortName = '" + ddlCls.SelectedItem.Text.Trim() + "', Doc_Qua_Name = '" + ddlQual.SelectedItem.Text.Trim() + "', " +
                                            " ListedDr_PinCode='" + txtpin.Text + "',ListedDr_RegNo='" + txtRegNo.Text + "',ListedDr_DOB='" + DR_DOB + "',ListedDr_DOW='" + DR_DOW + "', " +
                                            " City='" + lblCity.Text + "',ListedDr_Phone='" + txtland.Text + "',ListedDr_Mobile='" + txtMobile.Text + "'," +
                                            " ListedDr_Address1='" + DR_Addr + "',ListedDr_Hospital='" + DR_Hospital + "',Hospital_Address='" + DR_Hos_Addr + "', " +
                                            " ListedDr_Email ='" + txtMail.Text.Trim() + "',Visiting_Card='" + Visiting_card + "' " +
                                            " where ListedDrCode='" + Listeddrcode + "' and sf_code='" + MrCode + "' ";

                    command.ExecuteNonQuery();

                    ListerDrCodeNew = LstDoc.GetListedDrCode();

                    command.CommandText = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
                                          " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
                                          " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date,  " +
                                          " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Listeddr_App_Mgr,ListedDr_RegNo,City,Visiting_Card,ListedDr_Hospital,Hospital_Address,C_Doctor_Code,ListedDr_PinCode) " +
                                          " Select '" + ListerDrCodeNew + "' as ListedDrCode,  '" + MrCode + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
                                          " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,  " +
                                          " Territory_Code,  Doc_QuaCode,'0' as ListedDr_Active_Flag, ListedDr_Created_Date,ListedDr_Deactivate_Date,  " +
                                          " '" + ListerDrCodeNew + "'  as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + ListerDrCodeNew + "'  as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,  Listeddr_App_Mgr,ListedDr_RegNo,City,Visiting_Card,ListedDr_Hospital,Hospital_Address,C_Doctor_Code,ListedDr_PinCode " +
                                          " from Mas_ListedDr_One a where  Sf_Code=  '" + MrCode + "' and  ListedDr_Active_Flag=5 and ListedDrCode='" + Listeddrcode + "' ";

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE from Mas_ListedDr_One where Sf_Code = '" + MrCode + "'  and ListedDrCode = '" + Listeddrcode + "' and  ListedDr_Active_Flag=5 ";
                    command.ExecuteNonQuery();

                    //inserted column C_Approved_Date for update current date

                    command.CommandText = "update Mas_Common_Drs set C_Active_Flag=0,C_Approved_Date=getdate(),C_Doctor_Name='" + txtName.Text.Trim() + "', C_Doctor_Address='" + DR_Addr + "', " +
                                          " C_Doctor_Cat_Code='" + ddlCatg.SelectedValue + "',C_Doctor_Cat_Name = '" + ddlCatg.SelectedItem.Text.Trim() + "', " +
                                          " C_Doctor_Qual_Code='" + ddlQual.SelectedValue + "',C_Doctor_Qual_Name='" + ddlQual.SelectedItem.Text.Trim() + "',C_Doctor_Spl_Code='" + ddlSpec.SelectedValue + "',C_Doctor_Spl_Name='" + spec_short_name.Trim() + "', " +
                                          " C_Doctor_Cls_Code='" + ddlCls.SelectedValue + "' ,C_Doctor_Cls_Name='" + ddlCls.SelectedItem.Text.Trim() + "',  " +
                                          " C_Doctor_Hos_Addr='" + DR_Hos_Addr + "',C_Doctor_Mobile='" + DR_Mobile + "', " +
                                          " Drs_Landline_No='" + txtland.Text + "',Drs_Registration_No='" + txtRegNo.Text + "',Drs_City='" + lblCity.Text + "',Pincode='" + txtpin.Text + "', "+
                                          " DOB ='" + DR_DOB + "', DOW='" + DR_DOW + "',C_Doc_Hospital='" + DR_Hospital + "',C_Visiting_Card='" + Visiting_card + "',AdminHO_Approved_Date=getdate(),Admin_HO_Approved_Name='"+sf_name+"' " +
                                          " where c_doctor_code='" + Request.QueryString["C_Doctor_Code"] + "' ";
                    command.ExecuteNonQuery();

             
                    if (ddlProd1.SelectedItem.Text != "---Select---")
                    {
                        dsPro1 = pro.getProdBrd_Name(div_code, ddlProd1.SelectedItem.Text.Trim());
                        ListedDR lst = new ListedDR();
                        if (dsPro1.Tables[0].Rows.Count > 0)
                        {
                            iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCodeNew), dsPro1.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 1, dsPro1.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
                        }
                    }
                    if (ddlProd2.SelectedItem.Text != "---Select---")
                    {
                        dsPro2 = pro.getProdBrd_Name(div_code, ddlProd2.SelectedItem.Text.Trim());
                        ListedDR lst = new ListedDR();
                        if (dsPro2.Tables[0].Rows.Count > 0)
                        {
                            iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCodeNew), dsPro2.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 2, dsPro2.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
                        }
                    }
                    if (ddlProd3.SelectedItem.Text != "---Select---")
                    {
                        dsPro3 = pro.getProdBrd_Name(div_code, ddlProd3.SelectedItem.Text.Trim());
                        ListedDR lst = new ListedDR();
                        if (dsPro3.Tables[0].Rows.Count > 0)
                        {
                            iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCodeNew), dsPro3.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 3, dsPro3.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
                        }
                    }
                    if (ddlProd4.SelectedItem.Text != "---Select---")
                    {
                        dsPro4 = pro.getProdBrd_Name(div_code, ddlProd4.SelectedItem.Text.Trim());
                        ListedDR lst = new ListedDR();
                        if (dsPro4.Tables[0].Rows.Count > 0)
                        {
                            iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCodeNew), dsPro4.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 4, dsPro4.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
                        }
                    }
                    if (ddlProd5.SelectedItem.Text != "---Select---")
                    {
                        dsPro5 = pro.getProdBrd_Name(div_code, ddlProd5.SelectedItem.Text.Trim());
                        ListedDR lst = new ListedDR();
                        if (dsPro5.Tables[0].Rows.Count > 0)
                        {
                            iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCodeNew), dsPro5.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 5, dsPro5.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
                        }
                    }

                    transaction.Commit();
                    connection.Close();
                    if (iReturn > 0)
                    {
                        // menu1.Status = "Listed Doctor has been Approved Successfully";
                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unique Doctor has been Approved Successfully');{ self.close() };window.opener.location.reload();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Unique Doctor has been Approved Successfully');window.close();</script>");
                    }

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
          //  int iReturn = -1;
          //  ListedDR LstDoc = new ListedDR();
          //  Product pro = new Product();
          ////  iReturn = LstDoc.Final_App_FDC(MrCode, Listeddrcode, 0, 5, "", "", Request.QueryString["C_Doctor_Code"]);
          //  int iRet = LstDoc.Final_App_FDC_New(MrCode, Listeddrcode, 0, 5, "", "", Request.QueryString["C_Doctor_Code"]);
          //  if (ddlProd1.SelectedItem.Text != "---Select---")
          //  {
          //      dsPro1 = pro.getProdBrd_Name(div_code, ddlProd1.SelectedItem.Text.Trim());
          //      ListedDR lst = new ListedDR();
          //      if (dsPro1.Tables[0].Rows.Count > 0)
          //      {
          //          iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(iRet), dsPro1.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 1, dsPro1.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
          //      }
          //  }
          //  if (ddlProd2.SelectedItem.Text != "---Select---")
          //  {
          //      dsPro2 = pro.getProdBrd_Name(div_code, ddlProd2.SelectedItem.Text.Trim());
          //      ListedDR lst = new ListedDR();
          //      if (dsPro2.Tables[0].Rows.Count > 0)
          //      {
          //          iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(iRet), dsPro2.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 2, dsPro2.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
          //      }
          //  }
          //  if (ddlProd3.SelectedItem.Text != "---Select---")
          //  {
          //      dsPro3 = pro.getProdBrd_Name(div_code, ddlProd3.SelectedItem.Text.Trim());
          //      ListedDR lst = new ListedDR();
          //      if (dsPro3.Tables[0].Rows.Count > 0)
          //      {
          //          iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(iRet), dsPro3.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 3, dsPro3.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
          //      }
          //  }
          //  if (ddlProd4.SelectedItem.Text != "---Select---")
          //  {
          //      dsPro4 = pro.getProdBrd_Name(div_code, ddlProd4.SelectedItem.Text.Trim());
          //      ListedDR lst = new ListedDR();
          //      if (dsPro4.Tables[0].Rows.Count > 0)
          //      {
          //          iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(iRet), dsPro4.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 4, dsPro4.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
          //      }
          //  }
          //  if (ddlProd5.SelectedItem.Text != "---Select---")
          //  {
          //      dsPro5 = pro.getProdBrd_Name(div_code, ddlProd5.SelectedItem.Text.Trim());
          //      ListedDR lst = new ListedDR();
          //      if (dsPro5.Tables[0].Rows.Count > 0)
          //      {
          //          iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(iRet), dsPro5.Tables[0].Rows[0]["Product_Brd_Code"].ToString(), txtName.Text, MrCode, div_code, 5, dsPro5.Tables[0].Rows[0]["Product_Brd_Name"].ToString());
          //      }
          //  }
          //  if (iReturn > 0)
          //  {
          //      // menu1.Status = "Listed Doctor has been Approved Successfully";
          //      //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unique Doctor has been Approved Successfully');{ self.close() };window.opener.location.reload();</script>");
          //      ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Unique Doctor has been Approved Successfully');window.close();</script>");
          //  }
        }
        else if (typedr == "5")
        {
            
            string DR_Addr = txtAddress1.Text.Replace("'", "");
            string DR_Hospital = txtHospital.Text.Replace("'", "");
            string DR_Hos_Addr = txtHosAddress.Text.Replace("'", "");
            string DR_DOB = ddlDobMonth.SelectedValue + "-" + ddlDobDate.SelectedValue + "-" + ddlDobYear.SelectedValue;
            string DR_DOW = ddlDowMonth.SelectedValue + "-" + ddlDowDate.SelectedValue + "-" + ddlDowYear.SelectedValue;
            string DR_Mobile = txtMobile.Text.Trim();
            if (DR_Mobile.Trim() == "")
            {
                DR_Mobile = "0";
            }
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

                    int Common_code;
                    ListedDR LstDoc = new ListedDR();
                    Product pro = new Product();

                    ListedDR objListedDR = new ListedDR();
                    Common_code = objListedDR.GetCommonDrCode();


                    string Prod_Map = string.Empty;
                    if (FilUpImage.HasFile)
                    {
                        Visiting_card = FilUpImage.FileName.ToString();
                        Visiting_card = Common_code + "_" + Visiting_card;

                        FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Visiting_Card_One/") + Visiting_card);

                    }
                    else
                    {


                        string extension = Path.GetExtension("~/Visiting_Card_One/" + lblVis.Text);
                        var sourcePath = Server.MapPath("~/Visiting_Card_One/") + lblVis.Text;
                        var destinationPath = Server.MapPath("~/Visiting_Card_One/") + Common_code + extension;
                        FileInfo info = new FileInfo(sourcePath);
                        info.MoveTo(destinationPath);

                        Visiting_card = Common_code + extension;
                    }
                    Doctor dc = new Doctor();
                    dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
                    if (dsSpec.Tables[0].Rows.Count > 0)
                    {
                        spec_short_name = dsSpec.Tables[0].Rows[0]["Doc_Special_SName"].ToString();
                    }

                    command.CommandText = "insert into Mas_Common_Drs (C_Doctor_Code, C_Doctor_Name, C_Doctor_Address,C_Doctor_Cat_Code,C_Doctor_Cat_Name, " +
                    " C_Doctor_Qual_Code,C_Doctor_Qual_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,   " +
                    " C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_HQ,C_Territory_Code,C_Territory_Name,Allocated_IDs,Allocated_Id_Name,C_Created_Date,C_Active_Flag,Division_Code,Deactivate_Flag, " +
                    " Email_ID,Qual_Short_Name,Speciality_Short_Name,Drs_Landline_No,Drs_Registration_No,Drs_City,Pincode ,Ref_No,MR_HQ_Name,Unique_No,DOB,DOW,State,C_Visiting_Card,New_Unique,C_div_code,C_Doc_Hospital,MR_Raised_Date,MR_Raised_Name) " +
                  " VALUES('" + Common_code + "','" + txtName.Text.Trim() + "', " +
                  " '" + DR_Addr + "', '" + ddlCatg.SelectedValue + "', '" + ddlCatg.SelectedItem.Text.Trim() + "', '" + ddlQual.SelectedValue + "', '" + ddlQual.SelectedItem.Text.Trim() + "', " +
                  " '" + ddlSpec.SelectedValue + "', '" + spec_short_name.Trim() + "', '" + ddlCls.SelectedValue + "', '" + ddlCls.SelectedItem.Text.Trim() + "', '" + DR_Hos_Addr + "', '" + DR_Mobile + "', '', " +
                  " '', '', '" + sf_code + ',' + "', ''  , " +
                  "  getdate(), 2, '',0,'" + txtMail.Text.Trim() + "','" + ddlQual.SelectedItem.Text + "','" + ddlSpec.SelectedItem.Text.Trim() + "','" + txtland.Text + "','" + txtRegNo.Text + "', " +
                  " '" + lblCity.Text + "','" + txtpin.Text + "','" + Common_code + "','" + ddlterr.SelectedItem.Text + "','" + Common_code + "','" + DR_DOB + "','" + DR_DOW + "','" + lblState.Text + "','" + Visiting_card + "','N','" + div_code + "','" + DR_Hospital + "',getdate(),'" + sf_name + "')";
                    command.ExecuteNonQuery();

                    //  int iRet = LstDoc.Final_App_FDC_New(MrCode, Listeddrcode, 0, 5, "", "", Request.QueryString["C_Doctor_Code"]);

                    Prod_Map = ddlProd1.SelectedItem.Text + "/" + ddlProd2.SelectedItem.Text + "/" + ddlProd3.SelectedItem.Text + "/" + ddlProd4.SelectedItem.Text + "/" + ddlProd5.SelectedItem.Text;

                    command.CommandText = " update Mas_ListedDr_One " +
                                            " set listeddr_active_flag=2, ListedDr_Name='" + txtName.Text + "',Territory_Code='" + ddlterr.SelectedValue + "', Doc_ClsCode='" + ddlCls.SelectedValue + "'," +
                                            " Doc_Cat_Code='" + ddlCatg.SelectedValue + "', Doc_Special_Code='" + ddlSpec.SelectedValue + "', Doc_QuaCode='" + ddlQual.SelectedValue + "', " +
                                            " Doc_Cat_ShortName = '" + ddlCatg.SelectedItem.Text.Trim() + "', Doc_Spec_ShortName = '" + spec_short_name.Trim() + "', Doc_Class_ShortName = '" + ddlCls.SelectedItem.Text.Trim() + "', Doc_Qua_Name = '" + ddlQual.SelectedItem.Text.Trim() + "', " +
                                            " ListedDr_PinCode='" + txtpin.Text + "',ListedDr_RegNo='" + txtRegNo.Text + "',ListedDr_DOB='" + DR_DOB + "',ListedDr_DOW='" + DR_DOW + "', " +
                                            " City='" + lblCity.Text + "',ListedDr_Phone='" + txtland.Text + "',ListedDr_Mobile='" + txtMobile.Text + "'," +
                                            " ListedDr_Address1='" + DR_Addr + "',ListedDr_Hospital='" + DR_Hospital + "',Hospital_Address='" + DR_Hos_Addr + "', " +
                                            " ListedDr_Email ='" + txtMail.Text.Trim() + "',Visiting_Card='" + Visiting_card + "',Product_Map='" + Prod_Map + "',Re_Entry='Y',c_doctor_code='" + Common_code + "' " +
                                            " where ListedDrCode='" + Listeddrcode + "' and sf_code='" + MrCode + "' ";

                    command.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();

                    // menu1.Status = "Listed Doctor has been Approved Successfully";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unique Doctor has been Approved Successfully');{ self.close() };window.opener.location.reload();</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Sent to Manager approval');window.close();</script>");


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
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            btnApprove.Visible = false;
            txtreject.Visible = true;
            btnSubmit.Visible = true;
            btnReject.Visible = false;
            lblRejectReason.Visible = true;
            txtreject.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(time);
        //Response.Redirect("../MGR/MGR_Index.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        ListedDR LstDoc = new ListedDR();
        if (typedr == "1")
        {
            iReturn = LstDoc.Reject_New_Unique_Reason(MrCode, Listeddrcode, 1, 2, sf_name, Request.QueryString["C_Doctor_Code"], txtreject.Text.Trim());
        }
        else if (typedr == "2")
        {
            iReturn = LstDoc.Reject_New_Unique_Reason(MrCode, Listeddrcode, 1, 5, "admin", Request.QueryString["C_Doctor_Code"], txtreject.Text.Trim());
        }
        if (iReturn > 0)
        {
            // menu1.Status = "Listed Doctor has been Approved Successfully";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');</script>");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Rejected Successfully');window.close();</script>");
        }
        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Rejected Successfully');window.close();</script>");
            FillDoc();
            txtreject.Visible = false;
            lblRejectReason.Visible = false;
            btnSubmit.Visible = false;
            btnApprove.Visible = true;
        }
    }

}