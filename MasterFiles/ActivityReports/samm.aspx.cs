using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;

public partial class MasterFiles_AnalysisReports_samm : System.Web.UI.Page
{
    private string s = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    DataSet dsTP = null;
    string sf_name = string.Empty;
    DataSet dsSubDivision = new DataSet();
    protected void Page_Init(object sender, EventArgs e)
    {
        lstBaseLevel.Visible = false;
        Label1.Visible = false;
    }
     
    protected void Page_Load(object sender, EventArgs e)
    {
       
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
       
        if (!Page.IsPostBack)
        {
        //    lstFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));
        //    this.FillMasterList();
        //    this.BindAllProducts();
        //    lstBaseLevel.Visible = false;
        //    Label1.Visible = false;
        //    //this.AddDefaultFirstRecord();
        //    // menu.FindControl("btnBack").Visible = false;
        //    TourPlan tp = new TourPlan();
        //    dsTP = tp.Get_TP_Edit_Year(div_code);
           
        //    if (dsTP.Tables[0].Rows.Count > 0)
        //    {
        //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
        //        {
        //            ddlYear.Items.Add(k.ToString());
        //            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        //            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        //        }
        //    }
        //}

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                lstFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));
                FillMRManagers1();
                this.BindAllProducts();
                lstBaseLevel.Visible = false;
                Label1.Visible = false;
                Fill_Subdiv();

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                sf_name = Session["sf_name"].ToString();
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //lstBaseLevel.ClearSelection();
               // lstBaseLevel.Items.Insert(0, new ListItem(sf_name, sfCode));
                //FillMRManagers1();
               // FillMRfor_mr();
                this.BindAllProducts();
                //lstBaseLevel.Visible = false;
                //Label1.Visible = false;
                lblFieldForceName.Visible = false;
                lstFieldForce.Visible = false;
                //Label1.Visible = true;
                //lstBaseLevel.Visible = true;

            }


            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

                lstFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));
                this.FillMasterList();
                this.BindAllProducts();
                lstBaseLevel.Visible = false;
                Label1.Visible = false;
                Fill_Subdiv();

            }
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

        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }


        }
    }

    private void FillMasterList()
    {

        lstBaseLevel.Visible = true;
        Label1.Visible = true;
        DataSet dsProducts = null;

        dsSalesForce = sf.UserList_Hierarchy_Managers(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            //ListItem li = lstFieldForce.Items.Add(0, "select manager");
          
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();
        }

        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
            }
        }
        
    }

    private void BindBaseLevelByFieldForce(string strFieldForceID)
    {
        DataSet dsBaseLevel = new DataSet();
        // dsBaseLevel = sf.UserList_getAll_Multiple(div_code, strFieldForceID);
        if (ddlsub.SelectedValue == "0")
        {
            dsBaseLevel = sf.UserList_getAll_Multiple(div_code, strFieldForceID);
        }
        else
        {
            dsBaseLevel = sf.sp_UserList_getALL_Multiple_Subdiv(div_code, strFieldForceID, ddlsub.SelectedValue);
        }
        if (dsBaseLevel.Tables[0].Rows.Count > 0)
        {
            lstBaseLevel.DataTextField = "sf_name";
            lstBaseLevel.DataValueField = "sf_code";
            lstBaseLevel.DataSource = dsBaseLevel;
            lstBaseLevel.DataBind();
        }
        if (lstFieldForce.SelectedIndex > -1)
        {
            //string imageURL = "Images/disable.jpg";
            lstFieldForce.Disabled = true;

            lstFieldForce.Attributes.Add("title", "Disabled!! Click clear list to enable");
            //lstFieldForce.Attributes.Add("cursor","NotAllowed");
            lstFieldForce.Style["cursor"] = "not-allowed";
            //lstFieldForce item = lstFieldForce.ItemContainer.ContainerFromItem(lstFieldForce.SelectedItem) as ListBoxItem;
            //lstFieldForce.Background = new SolidColorBrush(Colors.Red);
            //lstFieldForce.Style.Add(HtmlTextWriterStyle.BorderColor, "#FF0000");
            //lstFieldForce.Attributes.Add("style","border-color:red;");
            //lstFieldForce.Attributes["style"] = "background: url(" + imageURL + ");background-repeat:no-repeat;";
        }

       
    }

    private void AddNewRecordRowToGrid()
    {
        string strBaseFieldForceID = "";


        //for (int j = 0; j < data.Controls.Count; j++)
        //    {
        //        // if controls named "lbl1" & "lbl2" exist
        //        if (data.Items[j].FindControl("lblProducts") != null && data.Items[j].FindControl("lblprd_sale") != null)
        //        {
        //            // cast the controls to TextBoxes and then get their Text Property
        //            string lbl1Text = ((Label)data.Items[j].FindControl("lblProducts")).Text;
        //            string lbl2Text = ((Label)data.Items[j].FindControl("lblprd_sale")).Text;

        //            // Display them
        //            TextBox tb = (TextBox)data.Items[j].FindControl("txtQuantity");
        //            tb.Text = lbl1Text + " " + lbl2Text;
        //        }
        //    }



        // This will Update and insert one by one in Sql. You can Insert or Update as per your requirement




        // For inserting name and email
        // this.Insert(name, qnty);
        //// For Updating name and email
        //this.Update(name, email);

        foreach (ListItem baseitem in lstBaseLevel.Items)
        {
            if (baseitem.Selected)
            {
                ViewState["baselevel"] = baseitem.Selected.ToString();
                //strBaseFieldForceID += baseitem.Value + ",";
                string output = objSample.RecordHeadAdd(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
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
                    string name = string.Empty;
                    string qnty = string.Empty;
                    string saleunit = string.Empty;
                    string pid = string.Empty;
                    string prdtsampleunt = string.Empty;
                    foreach (DataListItem item in this.data.Items)
                    {
                        name = (item.FindControl("lblProducts") as Label).Text;
                        qnty = (item.FindControl("txtQuantity") as TextBox).Text;
                        saleunit = (item.FindControl("lblprd_sale") as Label).Text;
                         var prdid = (HiddenField)item.FindControl("HiddenField1");
                         HiddenField hd = (HiddenField)item.FindControl("HiddenField2");
                         pid = hd.Value;
                         HiddenField hd1 = (HiddenField)item.FindControl("HiddenField3");
                         prdtsampleunt = hd1.Value;
                        if (qnty.Length > 0)
                        {
                            if (Convert.ToInt32(qnty) > 0)
                            {
                                objSample.RecordDetailsAddsampleinsert(output, baseitem.Value, div_code, name, Convert.ToInt32(qnty), saleunit, pid, prdtsampleunt);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Sample Despatched Successfully!');", true);

                            }
                        }
                       

                    }


                }

            }
            this.ClearControls();
        }








    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }


    protected void gvSampleDespatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSampleDespatch_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            DataTable dt = ViewState["SampleDes"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["SampleDes"] = dt;
            this.BindGrid();
        }
    }

    protected void BindGrid()
    {
        DataTable dtSample = (DataTable)ViewState["SampleDes"];


        gvSampleDespatch.DataSource = dtSample;
        gvSampleDespatch.DataBind();

        //if (dtSample.Rows.Count == 0)
        //{
        //    this.AddDefaultFirstRecord();
        //}
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
        gvSampleDespatch.DataSource = dt;
        gvSampleDespatch.DataBind();
    }
    protected void gvSampleDespatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlProducts = (e.Row.FindControl("ddlProducts") as DropDownList);

            DataSet dsProducts = null;

            dsProducts = objProduct.getProdall(div_code);
            if (dsProducts.Tables.Count > 0)
            {
                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    ddlProducts.DataTextField = "Product_Detail_Name";
                    ddlProducts.DataValueField = "Product_Detail_Code";
                    ddlProducts.DataSource = dsProducts;
                    ddlProducts.DataBind();
                }
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoProduct.SelectedValue == "All")
        {
            data.Visible = true;
            tblProducts.Visible = false;
            this.BindAllProducts();
        }
        else
        {
            data.Visible = false;
            tblProducts.Visible = true;
        }

        btnSave.Visible = true;
    }

    private void BindAllProducts()
    {
        DataSet dsProducts = null;
        // dsProducts = objProduct.getProdall(div_code);
        if (ddlsub.SelectedValue == "0")
        {
            dsProducts = objProduct.getProdall(div_code);
        }
        else
        {
            dsProducts = objProduct.getProd_Subdiv(div_code, ddlsub.SelectedValue);
        }
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
        }
    }

    protected void rdoProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void ClearControls()
    {
        if (lstFieldForce.SelectedIndex > -1)
        {
            lstFieldForce.Disabled = false;
            lstFieldForce.Attributes.Add("title", "select Fieldforce");
        }
        data.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
        divProducts.Visible = false;
        lstFieldForce.SelectedIndex = -1;
        
        lstBaseLevel.Visible = false;
        Label1.Visible = false;
        //ddlYear.SelectedIndex = 0;
        //ddlMonth.SelectedIndex = 0;
        rdoProduct.SelectedIndex = 0;
      
    }

    protected void Submit(object sender, EventArgs e)
    {
        string strFieldForceID = "";

        foreach (ListItem itm in lstFieldForce.Items)
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

        lstBaseLevel.Visible = true;
        Label1.Visible = true;

        this.BindBaseLevelByFieldForce(strFieldForceID);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + strFieldID + "');", true);
    }

    private void BindProducts()
    {
        pnlList.Controls.Clear();

        Product objProduct = new Product();
        DataSet dsProducts = null;

        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
                DataTable dtProducts = dsProducts.Tables[0];


                TextBox txt;
                HtmlInputCheckBox hck;
                Label lbl;
                HiddenField hdnProductCode;
                HiddenField hdnPack;
                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                string pack;
                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    DataRow drProduct = dtProducts.Rows[i];
                    prodtext = Convert.ToString(drProduct["Product_Detail_Name"]);
                    prodvalue = Convert.ToString(drProduct["Product_Detail_Code"]);
                    pack = Convert.ToString(drProduct["Product_Sale_Unit"]);

                    txt = new TextBox();
                    hck = new HtmlInputCheckBox();
                    lbl = new Label();
                    hdnProductCode = new HiddenField();
                    hdnPack = new HiddenField();

                    txt.ID = "txtNew" + i.ToString();
                    txt.Width = Unit.Pixel(40);
                    txt.Style.Add("display", "none");
                    txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + ");");

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;

                    hdnPack.ID = "hdnPack" + i.ToString();
                    hdnPack.Value = pack;
                    htmltbl.ID = "tbl";

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell1 = new HtmlTableCell();
                    cell2 = new HtmlTableCell();
                    hck.ID = "chkNew" + i.ToString();
                    hck.Attributes.Add("onclick", "ControlVisibility(" + i.ToString() + ");");

                    cell.Controls.Add(hck);
                    cell1.Controls.Add(lbl);
                    cell2.Controls.Add(txt);
                    cell2.Controls.Add(hdnProductCode);
                    cell2.Controls.Add(hdnPack);
                    cell1.Align = "left";
                    //cell.Style.Add("white-space", "nowrap");
                    //row.Style.Add("white-space", "nowrap");
                    row.Controls.Add(cell);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    htmltbl.Controls.Add(row);

                    pnlList.Controls.Add(htmltbl);

                }
            }
        }
    }

    protected void btnSave_Click1(object sender, ImageClickEventArgs e)
    {
        this.AddNewRecordRowToGrid();
        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (lstFieldForce.SelectedIndex > -1)
        {
            lstFieldForce.Disabled = false;
            lstFieldForce.Attributes.Add("title", "select Fieldforce");
        }
        lstFieldForce.SelectedIndex = -1;
        lstBaseLevel.Items.Clear();
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoProduct.SelectedIndex = 0;
        data.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
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
        //Response.Redirect("E-Report_DotNet/MasterFiles/AnalysisReports/editsample.aspx");
    }
   
    
    protected void btnSave_Click2(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }

    private void FillMRManagers1()
    {
        lstBaseLevel.Visible = true;
        Label1.Visible = true;
        DataSet dsProducts = null;

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            //ListItem li = lstFieldForce.Items.Add(0, "select manager");

            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();
        }

        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
            }
        }

    }

    private void FillMRfor_mr()
    {
        DataSet dsSalesforce = new DataSet();
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet(div_code, sfCode);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            lstBaseLevel.DataValueField = "SF_Code";
            lstBaseLevel.DataTextField = "Sf_Name";
            lstBaseLevel.DataSource = dsSalesforce;
            lstBaseLevel.DataBind();



        }
    }


    private void Fill_Subdiv()
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);

        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlsub.DataTextField = "subdivision_name";
            ddlsub.DataValueField = "subdivision_code";
            ddlsub.DataSource = dsSubDivision;
            ddlsub.DataBind();
            ddlsub.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlsub.DataSource = dsSubDivision;
            ddlsub.DataBind();
        }
    }
    protected void ddlsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        data.Visible = false;
        btnSave.Visible = false;

        string strFieldForceID = "";

        foreach (ListItem itm in lstFieldForce.Items)
        {
            if (itm.Selected)
            {
                strFieldForceID += itm.Value + ",";
            }
        }



        lstBaseLevel.Visible = true;
        Label1.Visible = true;
        this.BindBaseLevelByFieldForce(strFieldForceID);
    }
}

