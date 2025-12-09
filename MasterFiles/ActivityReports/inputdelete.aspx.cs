using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class MasterFiles_ActivityReports_inputdelete : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    InputDespatch objInput = new InputDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    DataSet dsTP = null;
    SampleDespatch objSample = new SampleDespatch();
  

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        this.BindInputs();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMRManagers();
            this.BindAllInputs();
            //this.AddDefaultFirstRecord();
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
                ddlFieldForce.SelectedValue = sfCode;
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

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
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
                string output = objInput.Recorddelete(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
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
                  

                }


            }

        }
        this.ClearControls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoInput.SelectedValue == "All")
        {
            gvSampleProducts.Visible = true;
            tblProducts.Visible = false;
            this.BindAllInputs();
            gvSampleProducts.DataBind();
            if (ddlMR.SelectedIndex > -1)
            {
                ddlMR.Enabled = false;
                ddlMR.Attributes.Add("title", "Disabled!! Click clear list button to enable");
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
            gvSampleProducts.Visible = false;
            tblProducts.Visible = true;
        }

        btnSave.Visible = true;
    }

    private void BindAllInputs()
    {
        DataSet dsInputs = null;
        foreach (ListItem baseitem in ddlMR.Items)
        {
            if (baseitem.Selected)
            {

                dsInputs = objProduct.getGiftedit(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                if (dsInputs.Tables.Count > 0)
                {
                    if (dsInputs.Tables[0].Rows.Count > 0)
                    {
                        gvSampleProducts.DataSource = dsInputs;
                        gvSampleProducts.DataBind();
                    }
                }
            }
        }
    }

    protected void rdoInput_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void ClearControls()
    {
        //lstFieldForce.SelectedIndex = -1;
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = true;
            ddlFieldForce.Attributes.Add("title", "select Fieldforce");
            ddlFieldForce.Style["cursor"] = "pointer";
        }
        ddlMR.Enabled = true;
        ddlMR.Attributes.Add("title", "select Fieldforce");
        ddlMR.Style["cursor"] = "pointer";
        ddlFieldForce.SelectedIndex = -1;
        ddlMR.Items.Clear();
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoInput.SelectedIndex = 0;
        gvSampleProducts.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
        //divInputs.Visible = false;
    }

    protected void Submit(object sender, EventArgs e)
    {
        string strFieldForceID = "";

        foreach (ListItem itm in ddlFieldForce.Items)
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

        //this.BindBaseLevelByFieldForce(strFieldForceID);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + strFieldID + "');", true);
    }

    private void BindInputs()
    {
        pnlList.Controls.Clear();

        Product objProduct = new Product();
        DataSet dsInputs = null;

        dsInputs = objProduct.getGift(div_code);
        if (dsInputs.Tables.Count > 0)
        {
            if (dsInputs.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
                DataTable dtInputs = dsInputs.Tables[0];


                TextBox txt;
                HtmlInputCheckBox hck;
                Label lbl;
                HiddenField hdnProductCode;
                HiddenField hdnGiftType;
                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                string strGiftType;
                for (int i = 0; i < dtInputs.Rows.Count; i++)
                {
                    DataRow drProduct = dtInputs.Rows[i];
                    prodtext = Convert.ToString(drProduct["Gift_Name"]);
                    prodvalue = Convert.ToString(drProduct["Gift_Code"]);
                    strGiftType = Convert.ToString(drProduct["Gift_Type"]);

                    txt = new TextBox();
                    hck = new HtmlInputCheckBox();
                    lbl = new Label();
                    hdnProductCode = new HiddenField();
                    hdnGiftType = new HiddenField();

                    txt.ID = "txtNew" + i.ToString();
                    txt.Width = Unit.Pixel(40);
                    txt.Style.Add("display", "none");
                    txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + ");");

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;

                    hdnGiftType.ID = "hdnGiftType" + i.ToString();
                    hdnGiftType.Value = strGiftType;
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
                    cell2.Controls.Add(hdnGiftType);
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

        gvSampleProducts.Visible = false;
        btnSave.Visible = false;
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
                iReturn = TrnHead.RecordDeleteParticularInputAdmin(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            }
            else
            {
                dsSalesForce = sf.UserList_getAll_Multiple(div_code, ddlFieldForce.SelectedValue.ToString());

                foreach (DataRow dr in dsSalesForce.Tables[0].Rows)
                {
                    sf_codeJoin += "'" + dr[0] + "',";
                }
                sf_codeJoin = sf_codeJoin.TrimEnd(',');
                iReturn = TrnHead.RecordDeleteParticularInput(sf_codeJoin, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            }
        }
        else
        {
            iReturn = TrnHead.RecordDeleteParticularInputMR(ddlMR.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        }
        if (iReturn == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Deleted input Successfully!');", true);
        }
    }
}