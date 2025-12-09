using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_Common_Doctor_Updation_FDC : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDoctor = new DataSet();
    DataSet dsdiv = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string strdiv_code = string.Empty;
    string sf_type = string.Empty;
    string Common_DR_Code = string.Empty;
    string strsf_code = string.Empty;
    string strFieledForceName = string.Empty;
    string str_terrcode = string.Empty;
    string AllocateIds = string.Empty;
    string Allocate = string.Empty;
    DataSet dsSpec = new DataSet();
    string Spec_SName = string.Empty;
    ListItem liTerr = new ListItem();
    DataTable dtDoctor;
    DataTable dtCatg = new DataTable();
    DataTable dtDoctor1;
    DataTable dtDoctor2;
    DataTable dtDoctor3;
    DataTable dtDoctor4;
    DataTable dtDoctor5;
    DataTable dtDoctor6;
    DataSet dsTP = null;
    string[] sfCode;
    string[] terrCode;
    int maxrows = 0;
    int currow = 0;
    int i = 0;
    int request_type = -1;
    int iIndex;
    int iIndexterr;
    int request_doctor = -1;
    DataSet dsTerr = new DataSet();
    DataSet dsTerritory = new DataSet();
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
    DataSet dsProd = new DataSet();
    string MrCode = string.Empty;
    string mode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();
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
        MrCode = Request.QueryString["mrcode"].ToString();
        mode = Request.QueryString["mode"].ToString();

        //    div_code = Session["div_code"].ToString();
        Division dv = new Division();
        // // menu1.FindControl("btnBack").Visible = false;
        // menu1.Title = Page.Title;
        Session["backurl"] = "Common_Doctor_List_FDC.aspx";


        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            dsdiv = sf.getSfName_Mr(sf_code);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                strFieledForceName = dsdiv.Tables[0].Rows[0]["Sf_Name"].ToString();
                LblForceName.Text = "Field Force Name : " + strFieledForceName;
            }
            //if (Request.QueryString["type"] != null)
            //{
            //    if ((Request.QueryString["type"].ToString() == "1"))
            //    {
            FillCategory();
            FillClass();
            FillSpeciality();
            FillTerritory();
            FillProd1();
            FillProd2();
            FillProd3();
            FillProd4();
            FillProd5();
            FillQualification();
            request_doctor = Convert.ToInt32(Request.QueryString["C_Doctor_Code"]);
            request_type = Convert.ToInt16(Request.QueryString["type"]);


            LoadDoctor(request_type, request_doctor);


            //}

            //}
        }
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


    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Speciality(div_code);
        ddlSpec.DataTextField = "Doc_Special_Name";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Common_Qualification(div_code);
        ddlQual.DataTextField = "Doc_QuaName";
        ddlQual.DataValueField = "Doc_QuaCode";
        ddlQual.DataSource = dsListedDR;
        ddlQual.DataBind();
    }

    private void LoadDoctor(int request_type, int request_doctor)
    {
        if (request_type == 1)
        {
            btnsubmit.Visible = true;
            ListedDR lst = new ListedDR();
            dsListedDR = lst.getCommonDr_List_Edit_Fdc(request_doctor);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                Common_DR_Code = dsListedDR.Tables[0].Rows[0]["C_Doctor_Code"].ToString();
                lbldr.Text = dsListedDR.Tables[0].Rows[0]["C_Doctor_Name"].ToString();
                lblRAddr.Text = dsListedDR.Tables[0].Rows[0]["C_Doctor_Address"].ToString();
                lblCliAddr.Text = dsListedDR.Tables[0].Rows[0]["C_Doctor_Hos_Addr"].ToString();
                lblMob.Text = dsListedDR.Tables[0].Rows[0]["C_Doctor_Mobile"].ToString();
                lblHosp.Text = dsListedDR.Tables[0].Rows[0]["C_Doc_Hospital"].ToString();
                //    ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                lblQua.Text = dsListedDR.Tables[0].Rows[0]["Qual_Short_Name"].ToString();
                lblland.Text = dsListedDR.Tables[0].Rows[0]["Drs_Landline_No"].ToString();
                lblReg.Text = dsListedDR.Tables[0].Rows[0]["Drs_Registration_No"].ToString();
                lblstate.Text = dsListedDR.Tables[0].Rows[0]["State"].ToString();
                lblSpe.Text = dsListedDR.Tables[0].Rows[0]["Speciality_Short_Name"].ToString();
                lblPin.Text = dsListedDR.Tables[0].Rows[0]["Pincode"].ToString();
                lblCity.Text = dsListedDR.Tables[0].Rows[0]["Drs_City"].ToString();
                //  strsf_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                //  str_terrcode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                //lblHQ.Text = dsListedDR.Tables[0].Rows[0]["MR_HQ_Name"].ToString();
                lblMail.Text = dsListedDR.Tables[0].Rows[0]["Email_ID"].ToString();
                SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select  '~/Visiting_Card_One/'+C_Visiting_Card as Visiting_Card from mas_common_drs where C_Doctor_Code='" + Common_DR_Code + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);


                con.Close();
                if (ds.Tables[0].Rows[0][0].ToString() != "" || ds.Tables[0].Rows[0][0].ToString() != "NULL")
                {
                    pnlVis.Visible = true;
                    string vis = ds.Tables[0].Rows[0][0].ToString();
                    lblVis.Text = vis;
                    if (vis != "~/Visiting_Card_One/")
                    {
                        //this.imgVisFile.ImageUrl = "~/Visiting_Card/" + ds.Tables[0].Rows[0][0].ToString();
                        dtvis.DataSource = ds;
                        dtvis.DataBind();
                    }
                    else
                    {
                        pnlVis.Visible = false;
                    }
                }
                else
                {
                    pnlVis.Visible = false;
                    dtvis.Visible = false;
                    dtvis.DataSource = null;
                    dtvis.DataBind();

                }
            }
        }
        else if (mode == "Existing")
        {
            btnsubmit.Visible = false;
            ListedDR lst = new ListedDR();
            dsListedDR = lst.getExisting_dr(MrCode, Convert.ToString(request_doctor), Request.QueryString["ListedDrCode"]);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                Common_DR_Code = dsListedDR.Tables[0].Rows[0]["C_Doctor_Code"].ToString();
                lbldr.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Name"].ToString();
                lblRAddr.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Address1"].ToString();
                lblCliAddr.Text = dsListedDR.Tables[0].Rows[0]["Hospital_Address"].ToString();
                lblHosp.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Hospital"].ToString();
                lblMob.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Mobile"].ToString();
                ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_QuaCode"].ToString();
                ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_QuaName"].ToString();
                ddlCls.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_ClsCode"].ToString();
                ddlCls.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_ClsName"].ToString();
                //lblQua.Text = dsListedDR.Tables[0].Rows[0]["Qual_Short_Name"].ToString();
               // lblSpe.Text = dsListedDR.Tables[0].Rows[0]["Speciality_Short_Name"].ToString();
                lblQua.Visible = false;
                lblSpe.Visible = false;
                lblqum.Visible = false;
                lblS.Visible = false;
                lblcol.Visible = false;
                lblcolQ.Visible = false;
                if (dsListedDR.Tables[0].Rows[0]["Product1"].ToString() != "")
                {
                    ddlProd1.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Product1"].ToString();
                }
                else
                {
                    ddlProd1.SelectedItem.Text = "---Select---";
                }
                if (dsListedDR.Tables[0].Rows[0]["Product2"].ToString() != "")
                {
                    ddlProd2.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Product2"].ToString();
                }
                else
                {
                    ddlProd2.SelectedItem.Text = "---Select---";
                }
                if (dsListedDR.Tables[0].Rows[0]["Product3"].ToString() != "")
                {
                    ddlProd3.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Product3"].ToString();
                }
                else
                {
                    ddlProd3.SelectedItem.Text = "---Select---";
                }
                if (dsListedDR.Tables[0].Rows[0]["Product4"].ToString() != "")
                {
                    ddlProd4.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Product4"].ToString();
                }
                else
                {
                    ddlProd4.SelectedItem.Text = "---Select---";
                }
                if (dsListedDR.Tables[0].Rows[0]["Product5"].ToString() != "")
                {
                    ddlProd5.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Product5"].ToString();
                }
                else
                {
                    ddlProd5.SelectedItem.Text = "---Select---";
                }
                ddlCatg.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Code"].ToString();
                ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Name"].ToString();

                ddlterr.SelectedValue = dsListedDR.Tables[0].Rows[0]["territory_code"].ToString();
                ddlterr.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["territory_Name"].ToString();

                //   ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Qual_Short_Name"].ToString();
                lblland.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Phone"].ToString();
                lblReg.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_RegNo"].ToString();
                lblstate.Text = dsListedDR.Tables[0].Rows[0]["State_Code"].ToString();
                ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Special_Code"].ToString();
                Doctor dc = new Doctor();
                dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
                if (dsSpec.Tables[0].Rows.Count > 0)
                {
                    Spec_SName = dsSpec.Tables[0].Rows[0]["Doc_Special_Name"].ToString();
                }
                ddlSpec.SelectedItem.Text = Spec_SName.Trim();
                lblPin.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_PinCode"].ToString();
                lblCity.Text = dsListedDR.Tables[0].Rows[0]["City"].ToString();
                //  strsf_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                //  str_terrcode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                //  lblHQ.Text = dsListedDR.Tables[0].Rows[0]["MR_HQ_Name"].ToString();
                lblMail.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Email"].ToString();
                ddlterr.Enabled = false;
                ddlQual.Enabled = false;
                ddlSpec.Enabled = false;
                ddlCatg.Enabled = false;
                ddlProd1.Enabled = false;
                ddlProd2.Enabled = false;
                ddlProd3.Enabled = false;
                ddlProd4.Enabled = false;
                ddlProd5.Enabled = false;
                ddlCls.Enabled = false;
                SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select  '~/Visiting_Card_One/'+C_Visiting_Card as Visiting_Card from mas_common_drs where C_Doctor_Code='" + Common_DR_Code + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);


                con.Close();
                if (ds.Tables[0].Rows[0][0].ToString() != "" || ds.Tables[0].Rows[0][0].ToString() != "NULL")
                {
                    pnlVis.Visible = true;
                    string vis = ds.Tables[0].Rows[0][0].ToString();
                    lblVis.Text = vis;
                    if (vis != "~/Visiting_Card_One/")
                    {
                        //this.imgVisFile.ImageUrl = "~/Visiting_Card/" + ds.Tables[0].Rows[0][0].ToString();
                        dtvis.DataSource = ds;
                        dtvis.DataBind();
                    }
                    else
                    {
                        pnlVis.Visible = false;
                    }
                }
                else
                {
                    pnlVis.Visible = false;
                    dtvis.Visible = false;
                    dtvis.DataSource = null;
                    dtvis.DataBind();

                }

            }


        }
        else if (mode == "New")
        {
            btnsubmit.Visible = false;
            ListedDR lst2 = new ListedDR();
            dsListedDR = lst2.getNew_dr(MrCode, Convert.ToString(request_doctor));
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                Common_DR_Code = dsListedDR.Tables[0].Rows[0]["C_Doctor_Code"].ToString();
                lbldr.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Name"].ToString();
                lblRAddr.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Address1"].ToString();
                lblCliAddr.Text = dsListedDR.Tables[0].Rows[0]["Hospital_Address"].ToString();
                lblMob.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Mobile"].ToString();
                ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_QuaCode"].ToString();
                ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_QuaName"].ToString();


                ddlCatg.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Code"].ToString();
                ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_Cat_Name"].ToString();

                ddlterr.SelectedValue = dsListedDR.Tables[0].Rows[0]["territory_code"].ToString();
                ddlterr.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["territory_Name"].ToString();

                //   ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Qual_Short_Name"].ToString();
                lblland.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Phone"].ToString();
                lblReg.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_RegNo"].ToString();
                ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0]["Doc_Special_Code"].ToString();
                ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0]["Doc_Special_Name"].ToString();
                lblPin.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_PinCode"].ToString();
                lblCity.Text = dsListedDR.Tables[0].Rows[0]["City"].ToString();
                //  strsf_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                //  str_terrcode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                //  lblHQ.Text = dsListedDR.Tables[0].Rows[0]["MR_HQ_Name"].ToString();
                lblMail.Text = dsListedDR.Tables[0].Rows[0]["ListedDr_Email"].ToString();
                ddlterr.Enabled = false;
                ddlQual.Enabled = false;
                ddlSpec.Enabled = false;
                ddlCatg.Enabled = false;
                ddlProd1.Enabled = false;
                ddlProd2.Enabled = false;
                ddlProd3.Enabled = false;
                ddlProd4.Enabled = false;
                ddlProd5.Enabled = false;

            }
        }
    }

    private void FillDiv()
    {

        for (int j = 0; j < dsdiv.Tables[0].Rows.Count; j++)
        {
            GridView gv = new GridView();
            gv.CssClass = "aclass";
            gv.Attributes.Add("class", "aclass");

            Label lbl = new Label();
            lbl.CssClass = "lbl";
            lbl.Attributes.Add("class", "lbl");

            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                lbl.Text = dsdiv.Tables[0].Rows[j]["Division_Name"].ToString();
                // state_code = dsdiv.Tables[0].Rows[j]["State_Code"].ToString();
            }

            gv.DataSource = dsdiv;
            gv.DataBind();
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                lbl.Attributes.Add("align", "center");
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(gv);

            }
        }
    }




    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string Spec_Short_Name = string.Empty;
        string StrSpec_Code = string.Empty;
        string StrTerritory_Code = string.Empty;
        string StrCat_Code = string.Empty;
        string StrCls_Code = string.Empty;
        string strUsername = string.Empty;
        string Doc_Type = string.Empty;
        string StrQua_Code = string.Empty;
        string StrSpec_SName = string.Empty;
        string StrCat_SName = string.Empty;
        string StrCls_SName = string.Empty;
        string StrQua_SName = string.Empty;
        int ListerDrCode;

        string DR_Name = lbldr.Text.Trim();
        string DR_Qual_Name = ddlQual.SelectedItem.Text.Trim();
        string DR_Spe = ddlSpec.SelectedItem.Text.Trim();
        string DR_Mobile = lblMob.Text.ToString();
        string DR_Address = lblRAddr.Text.ToString();
        string DR_Clinic = lblHosp.Text.Trim();
        string Hosp_Address = lblCliAddr.Text.Trim();
        // string DR_Hq = lblHQ.Text.Trim();
        string DR_Lane = lblland.Text.Trim();
        string DR_Pin = lblPin.Text.Trim();
        string DR_City = lblCity.Text.Trim();
        string DR_Email = lblMail.Text.Trim();
        string DR_Reg = lblReg.Text.Trim();
        //  string DR_Area = lblArea.Text.Trim();
        SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        conn.Open();
        DataSet dslstQua = new DataSet();
        ListedDR lstDR = new ListedDR();

        //dslstQua = lstDR.GetQua_Upload(ddlQual.SelectedItem.Text.Trim(), div_code);
        //if (dslstQua.Tables[0].Rows.Count > 0)
        //{
        //    StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
        //    StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
        //}
        //dslstSpec = lstDR.GetCategory_Special_Code(ddlSpec.SelectedItem.Text.Trim(), div_code);
        //if (dslstSpec.Tables[0].Rows.Count > 0)
        //{
        //    StrSpec_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
        //    StrSpec_SName = dslstSpec.Tables[0].Rows[0][1].ToString();

        //}

        //dslstCat = lstDR.GetDoc_Cat_Code(ddlCatg.SelectedItem.Text.Trim(),div_code);
        //if (dslstCat.Tables[0].Rows.Count > 0)
        //{
        //    StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
        //    StrCat_SName = dslstCat.Tables[0].Rows[0][1].ToString();
        //}
        Doctor dc = new Doctor();
        dslstCls = dc.getDocCls(div_code, ddlCls.SelectedValue);
        if (dslstCls.Tables[0].Rows.Count > 0)
        {
          //  StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
            StrCls_SName = dslstCls.Tables[0].Rows[0]["Doc_ClsSName"].ToString();
        }
        
        dsSpec = dc.getDocSpe(div_code, ddlSpec.SelectedValue);
        if (dsSpec.Tables[0].Rows.Count > 0)
        {
            Spec_Short_Name = dsSpec.Tables[0].Rows[0]["Doc_Special_SName"].ToString();
        }
        //Territory terr = new Territory();
        //int terrcode = terr.GetterrCode();
        //dsTerr = lstDR.GetTerritory_Upload(ddlterr.SelectedItem.Text.Trim(), sf_code, div_code);
        //if (dsTerr.Tables[0].Rows.Count > 0)
        //{
        //    StrTerritory_Code = dsTerr.Tables[0].Rows[0][0].ToString();
        //}
        //if (dsTerr.Tables[0].Rows.Count == 0)
        //{
        //    string strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
        //        " SF_Code,Territory_Active_Flag,Created_date,Alias_Name) " +
        //        " VALUES('" + terrcode + "', '" + DR_Hq + "' , 1 , null, " +
        //        " '" + div_code + "', '" + sf_code + "', 0, getdate(),'"+lblCity.Text.Trim()+"')";
        //    SqlCommand cmd1 = new SqlCommand(strQry, conn);
        //    cmd1.ExecuteNonQuery();
        //}
        //dsTerr = lstDR.GetTerritory_Upload(DR_Hq, sf_code, div_code);
        //if (dsTerr.Tables[0].Rows.Count > 0)
        //{
        //    StrTerritory_Code = dsTerr.Tables[0].Rows[0][0].ToString();
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
                   
                SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                string ChkHead = "select C_Doctor_Code from Mas_ListedDr where C_Doctor_Code ='" + Request.QueryString["C_Doctor_Code"] + "' and sf_code = '" + sf_code + "' and ListedDr_Active_Flag = 0 ";
                SqlCommand cmd1;
                cmd1 = new SqlCommand(ChkHead, con1);
                con1.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                
                da1.Fill(dsDoctor);
                con1.Close();
            //    dsSecHead = ss.Get_Head_Slno_Tran(ddlDivision.SelectedValue, strSf_Code);
                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    //  command.CommandText = "select count(C_Doctor_Code) from Mas_ListedDr where C_Doctor_Code =" + Request.QueryString["C_Doctor_Code"] + " and sf_code = '" + sf_code + "' and ListedDr_Active_Flag=0 ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Already Exist');window.close();window.opener.location.reload();</script>");
                }
                else
                {
                    ListedDR objListedDR = new ListedDR();
                    ListerDrCode = objListedDR.GetListedDrCode();
                    command.CommandText = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Hospital,Hospital_Address,ListedDr_Email,Doc_QuaCode,ListedDr_Phone,ListedDr_Mobile,Territory_Code,Doc_Special_Code,Doc_Cat_Code,Doc_ClsCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,Visit_Hours,visit_days,ListedDr_Sl_No,SLVNo,LastUpdt_Date, Doc_Type,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName,C_Doctor_Code,State_Code,Visiting_Card,City) " +
                     " VALUES('" + ListerDrCode + "', '" + sf_code + "', '" + DR_Name + "', '" + DR_Address + "','" + DR_Clinic + "','" + Hosp_Address + "','" + DR_Email + "','" + ddlQual.SelectedValue + "','" + DR_Lane + "','" + DR_Mobile + "', '" + ddlterr.SelectedValue + "', '" + ddlSpec.SelectedValue + "','" + ddlCatg.SelectedValue + "', '" + ddlCls.SelectedValue + "', 2, getdate(),'" + div_code + "', '','','" + ListerDrCode + "' ,'" + ListerDrCode + "',getdate(), '','" + ddlCatg.SelectedItem.Text.Trim() + "','" + Spec_Short_Name.Trim() + "','" + ddlQual.SelectedItem.Text.Trim() + "','" + StrCls_SName.Trim() + "','" + Request.QueryString["C_Doctor_Code"] + "','" + lblstate.Text.Trim() + "','" + lblVis.Text.Replace("~/Visiting_Card_One/", "").Trim() + "','" + DR_City + "')";


                    command.ExecuteNonQuery();


                    command.CommandText = "update mas_common_drs set Allocated_IDs =Allocated_IDs+'" + sf_code + "'+','  where C_Doctor_Code= '" + Request.QueryString["C_Doctor_Code"] + "' ";

                    command.ExecuteNonQuery();

                    if (ddlProd1.SelectedValue != "0")
                    {
                        ListedDR lst = new ListedDR();
                        int iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCode), ddlProd1.SelectedValue, DR_Name, sf_code, div_code, 1, ddlProd1.SelectedItem.Text.Trim());
                    }
                    if (ddlProd2.SelectedValue != "0")
                    {
                        ListedDR lst = new ListedDR();
                        int iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCode), ddlProd2.SelectedValue, DR_Name, sf_code, div_code, 2, ddlProd2.SelectedItem.Text.Trim());
                    }
                    if (ddlProd3.SelectedValue != "0")
                    {
                        ListedDR lst = new ListedDR();
                        int iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCode), ddlProd3.SelectedValue, DR_Name, sf_code, div_code, 3, ddlProd3.SelectedItem.Text.Trim());
                    }
                    if (ddlProd4.SelectedValue != "0")
                    {
                        ListedDR lst = new ListedDR();
                        int iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCode), ddlProd4.SelectedValue, DR_Name, sf_code, div_code, 4, ddlProd4.SelectedItem.Text.Trim());
                    }
                    if (ddlProd5.SelectedValue != "0")
                    {
                        ListedDR lst = new ListedDR();
                        int iReturn = lst.RecordAdd_ProductMap_Unique(Convert.ToString(ListerDrCode), ddlProd5.SelectedValue, DR_Name, sf_code, div_code, 5, ddlProd5.SelectedItem.Text.Trim());
                    }

                    transaction.Commit();
                    connection.Close();
                    //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully'); { self.close() };</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Submitted Successfully');window.close();window.opener.location.reload();</script>");
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
    }
}
