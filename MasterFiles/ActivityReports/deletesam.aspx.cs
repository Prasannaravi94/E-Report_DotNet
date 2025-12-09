using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_AnalysisReports_deletesam : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsTerritory = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    DateTime dtCurrent;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_type = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillMRManagers();
            this.BindAllProducts();
            // menu.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
        }
        if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;

            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
        }
        //ViewState["sf_type"] = "";
        //SalesForce sf = new SalesForce();
        //dsSf = sf.getReportingTo(sf_code);
        //if (dsSf.Tables[0].Rows.Count > 0)
        //{
        //    sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //}


            //TourPlan tp = new TourPlan();
        //dsTP = tp.Get_TP_Edit_Year(div_code);
        //if (dsTP.Tables[0].Rows.Count > 0)
        //{
        //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
        //    {
        //        ddlFYear.Items.Add(k.ToString());
        //    }
        //    for (int i = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); i <= DateTime.Now.Year + 1; i++)
        //    {
        //        ddlTYear.Items.Add(i.ToString());
        //    }
        //}





        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }

        }
        else
        {
            //ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                FillManagers();
            }
        }

        FillColor();


        //getWorkName();
        //TourPlan tp = new TourPlan();
        //dsTP = tp.Get_TP_Edit_Year(div_code);
        //if (dsTP.Tables[0].Rows.Count > 0)
        //{
        //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
        //    {
        //        ddlFYear.Items.Add(k.ToString());
        //        ddlTYear.Items.Add(k.ToString());
        //        ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
        //        ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
        //    }


        //ddlFMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
        //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlFieldForce.Focus();

    }




    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getAll_Multiple(div_code, ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
            //{
            //    lblMR.Visible = true;
            ddlMR.Visible = true;
        Label1.Visible = true;
        ddlMR.DataTextField = "sf_name";
        ddlMR.DataValueField = "sf_code";
        ddlMR.DataSource = dsSalesForce;
        ddlMR.DataBind();
        ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
    }


    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
        FillColor();


    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }


    private void AddNewRecordRowToGrid()
    {
        string strBaseFieldForceID = "";




        foreach (ListItem baseitem in ddlMR.Items)
        {
            if (baseitem.Selected)
            {
                ViewState["baselevel"] = baseitem.Selected.ToString();
                //strBaseFieldForceID += baseitem.Value + ",";
                string output = objSample.Recorddelete(baseitem.Value, div_code, ddlMonth.SelectedValue,ddlYear.SelectedValue);
                if (output != "0")
                {
                    //string[] strProdArray = hdnProdCode.Value.Split(',');
                    //for (int j = 0; j < strProdArray.Length; j++)
                    //{
                    //    string[] strProdAllValue = strProdArray[j].Split('|');
                    //    if (strProdAllValue.Length > 2)
                    //    {
                    //        if (strProdAllValue[2] != string.Empty)
                    //        {
                    //string name = string.Empty;
                    //string qnty = string.Empty;
                    //foreach (DataListItem item in this.data.Items)
                    //{
                    //    name = (item.FindControl("lblProducts") as Label).Text;
                    //    qnty = (item.FindControl("txtQuantity") as Label).Text;

                    //    if (qnty.Length > 0)
                    //    {
                    //        objSample.RecordDetailsAdd(output, baseitem.Value, div_code, name, Convert.ToInt32(qnty));

                    //        this.ClearControls();
                    //        data.Visible = false;
                    //        btnSave.Visible = false;
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg",
                    //            "alert('Updated Sample Successfully!');", true);

                    //    }
                        //    else
                        //    {
                        //        data.Visible = true;
                        //        btnSave.Visible = true;
                        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Enter Product!');", true);
                        //    }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg",
                            "alert('Deleted Sample Successfully!');", true);
                        this.ClearControls();

                    }


                }

            }

        }
    


    

    private void AddDefaultFirstRecord()
    {
        //creating dataTable   
        DataTable dt = new DataTable();
        DataRow dr;
        dt.TableName = "SampleDespatch";
        dt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
        dt.Columns.Add(new DataColumn("DespatchQty", typeof(string)));
        dr = dt.NewRow();
        dt.Rows.Add(dr);
        //saving databale into viewstate   
        ViewState["SampleDes"] = dt;
        //bind Gridview  

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoProduct.SelectedValue == "All")
        {
            data.Visible = true;
            this.BindAllProducts();
        }
        else
        {
            data.Visible = false;

        }

        btnSave.Visible = true;
    }

    private void BindAllProducts()
    {
        DataSet dsProducts = null;
        foreach (ListItem baseitem in ddlMR.Items)
        {
            if (baseitem.Selected)
            {
                dsProducts = objProduct.getPr(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                if (dsProducts.Tables.Count > 0)
                {
                    if (dsProducts.Tables[0].Rows.Count > 0)
                    {

                        data.DataSource = dsProducts;
                        data.DataBind();

                        //string strProductCodes = string.Empty;
                        //for (int cnt = 0; cnt < dsProducts.Tables[0].Rows.Count; cnt++)
                        //{
                        //    if (strProductCodes != string.Empty)
                        //    {
                        //        strProductCodes = strProductCodes + "," + Convert.ToString(dsProducts.Tables[0].Rows[cnt]["Product_Detail_Code"]);
                        //    }
                        //    else
                        //    {
                        //        strProductCodes = Convert.ToString(dsProducts.Tables[0].Rows[cnt]["Product_Detail_Code"]);
                        //    }
                        //}

                        //hdnProducts.Value = strProductCodes;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "No record found!!";

                        btnSave.Visible = false;
                    }
                }
            }
        }
    }

    protected void rdoProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void ClearControls()
    {
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = true;
            ddlFieldForce.Attributes.Add("title", "select Fieldforce");
            ddlFieldForce.Style["cursor"] = "pointer";
        }
        ddlMR.Enabled = true;
        ddlMR.Attributes.Add("title", "select Fieldforce");
        ddlMR.Style["cursor"] = "pointer";
        data.Visible = false;
        btnSave.Visible = false;
        divProducts.Visible = false;
        ddlFieldForce.SelectedIndex = -1;

        ddlMR.Visible = false;
        Label1.Visible = false;
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoProduct.SelectedIndex = 0;

    }

    protected void Submit(object sender, EventArgs e)
    {
        string strFieldForceID = "";

        foreach (ListItem itm in ddlMR.Items)
        {
            if (itm.Selected)
            {
                strFieldForceID += itm.Value + ",";
            }
        }

        //foreach (ListItem item in lstFieldForce.Items)
        //{
        //    if (item.Selected)
        //    {
        //        strFieldForceID += item.Value + ",";
        //    }
        //}

        //  lstBaseLevel.Visible = true;
        Label1.Visible = true;

        // this.BindBaseLevelByFieldForce(strFieldForceID);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + strFieldID + "');", true);
    }



    protected void btnSave_Click1(object sender, ImageClickEventArgs e)
    {
        string name = string.Empty;
        string qnty = string.Empty;
        // This will Update and insert one by one in Sql. You can Insert or Update as per your requirement
        foreach (DataListItem item in this.data.Items)
        {
            name = (item.FindControl("lblProducts") as Label).Text;
            qnty = (item.FindControl("txtQuantity") as TextBox).Text;



            // For inserting name and email
            //this.Insert(name, qnty);
            //// For Updating name and email
            //this.Update(name, email);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = true;
            ddlFieldForce.Attributes.Add("title", "select Fieldforce");
            ddlFieldForce.Style["cursor"] = "pointer";
        }
        ddlMR.Enabled = true;
        ddlMR.Attributes.Add("title", "select Baselevel");
        ddlMR.Style["cursor"] = "pointer";
        ddlFieldForce.SelectedIndex = -1;
        ddlMR.Items.Clear();
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoProduct.SelectedIndex = 0;
        data.Visible = false;
        btnSave.Visible = false;
         lblMessage.Visible = false;
    
    }

    //protected void Insert(string name, string qnty)
    //{
    //    SqlConnection con = new SqlConnection(s);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("insert into  Trans_Sample_Despatch_Details([Trans_sl_No],[Division_Code],Product_Code,Despatch_Qty)values('28','1','" + name + "','" + qnty + "')", con);
    //    cmd.ExecuteNonQuery();
    //    SqlCommand cmd1 = new SqlCommand("insert into  Trans_Sample_Despatch_Head([Trans_sl_No],[Division_Code],Product_Code,Despatch_Qty)values('28','1','" + ddlYear.SelectedValue.ToString() + "','" + ddlMonth.SelectedValue.ToString() + "')", con);
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Sample Despatch HQ Saved Successfully!');", true);
    //    this.ClearControls();

    //}








    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }

  

  
    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        data.Visible = false;
        btnSave.Visible = false;
        lblMessage.Visible = false;

    }

    protected void btnGo_Click1(object sender, EventArgs e)
    {
        if (rdoProduct.SelectedValue == "All")
        {
            data.Visible = true;
            btnSave.Visible = true;
            this.BindAllProducts();
            data.DataBind();
            if (ddlMR.SelectedIndex > -1)
            {
                ddlMR.Enabled = false;
                ddlMR.Attributes.Add("title", "Disabled!! Click clear list to enable");
                ddlMR.Style["cursor"] = "not-allowed";
            }
            if (ddlFieldForce.SelectedIndex > -1)
            {
                ddlFieldForce.Enabled = false;
                ddlFieldForce.Attributes.Add("title", "Disabled!! Click clear list to enable");
                ddlFieldForce.Style["cursor"] = "not-allowed";
            }
        }
        else
        {
            data.Visible = false;
            btnSave.Visible = false;

        }

    }
    protected void btnSave_Click2(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        Product TrnHead = new Product();
        string sf_codeJoin = string.Empty;
        if (ddlMR.SelectedValue == "0")
        {
            if (ddlFieldForce.SelectedValue == "admin")
            {
                iReturn = TrnHead.RecordDeleteParticularSampleAdmin(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            }
            else
            {
                dsSalesForce = sf.UserList_getAll_Multiple(div_code, ddlFieldForce.SelectedValue.ToString());

                foreach (DataRow dr in dsSalesForce.Tables[0].Rows)
                {
                    sf_codeJoin += "'" + dr[0] + "',";
                }
                sf_codeJoin = sf_codeJoin.TrimEnd(',');
                iReturn = TrnHead.RecordDeleteParticularSample(sf_codeJoin, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            }
        }
        else
        {
            iReturn = TrnHead.RecordDeleteParticularSampleMR(ddlMR.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        }
        if (iReturn == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Deleted input Successfully!');", true);
        }
    }
}