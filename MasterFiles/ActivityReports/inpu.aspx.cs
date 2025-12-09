using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Data;


public partial class MasterFiles_ActivityReports_inpu : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    InputDespatch objInput = new InputDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    DataSet dsTP = null;
    DataSet dsSubDivision = new DataSet();

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        lstBaseLevel.Visible = false;
        Label1.Visible = false;
        this.BindInputs();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            // this.FillMasterList();
            // this.BindAllInputs();
            // //this.AddDefaultFirstRecord();
            //// // menu.FindControl("btnBack").Visible = false;
            // TourPlan tp = new TourPlan();
            // dsTP = tp.Get_TP_Edit_Year(div_code);
            // if (dsTP.Tables[0].Rows.Count > 0)
            // {
            //     for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //     {
            //         ddlYear.Items.Add(k.ToString());
            //         ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            //         ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            //     }
            // }


            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                lstFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));
                this.FillMasterList();
                this.BindAllInputs();
                lstBaseLevel.Visible = false;
                Label1.Visible = false;
                Fill_Subdiv();


            }
            else if (Session["sf_type"].ToString() == "1")
            {
                string sf_name = Session["sf_name"].ToString();
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //lstFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));
                //FillMRManagers1();
                // FillMRfor_mr();
                //this.FillMasterList();
                //Label1.Visible = false;
                //lstBaseLevel.Visible = false; 
                //lstBaseLevel.Items.Insert(0, new ListItem(sf_name, sfCode));
                this.BindAllInputs();
                //lstBaseLevel.Visible = false;
                //Label1.Visible = false;
                lblFieldForceName.Visible = false;
                lstFieldForce.Visible = false;

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
                this.BindAllInputs();
                Fill_Subdiv();
                lstBaseLevel.Visible = false;
                Label1.Visible = false;

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

        dsSalesForce = sf.UserList_Hierarchy_Managers(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();
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
        string giftname = string.Empty;
        string qnty = string.Empty;
        string giftcode = string.Empty;
        string pid = string.Empty;
        //DataSet dsProducts = null;
        //int qntnt = 0;
        foreach (ListItem baseitem in lstBaseLevel.Items)
        {
            if (baseitem.Selected)
            {
                //dsProducts = objProduct.getGifteditNew(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                //DataTable dtProd = dsProducts.Tables[0];

                //strBaseFieldForceID += baseitem.Value + ",";
                string output = objInput.RecordHeadAdd(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                if (output != "0")
                {
                    //foreach (DataRow dtRow in dtProd.Rows)
                    //{
                    foreach (DataListItem item in this.gvSampleProducts.Items)
                    {
                        giftname = (item.FindControl("lblInput") as Label).Text;
                        qnty = (item.FindControl("txtQuantity") as TextBox).Text;
                        HiddenField hd = (HiddenField)item.FindControl("HiddenField2");
                        pid = hd.Value;
                        //if (dtRow.ItemArray[1].ToString() == pid)
                        //{
                        if (qnty.Length > 0)
                        {
                            if (Convert.ToInt32(qnty) > 0)
                            {
                                // qntnt = Convert.ToInt32(qnty) + Convert.ToInt32(dtRow.ItemArray[9]);

                                objInput.RecordDetailsAddinput(output, baseitem.Value, div_code, giftname, Convert.ToInt32(qnty), pid);
                                //qntnt = 0;
                                string[] strProdArray = hdnProdCode.Value.Split(',');
                                for (int j = 0; j < strProdArray.Length; j++)
                                {
                                    //    string[] strProdAllValue = strProdArray[j].Split('|');
                                    //    if (strProdAllValue.Length > 2)
                                    //    {
                                    //        if (strProdAllValue[2] != string.Empty)
                                    //        {
                                    //            objInput.RecordDetailsAdd(output, baseitem.Value, div_code,strProdAllValue[0],Convert.ToInt32(strProdAllValue[2]));
                                    //        }
                                    //}
                                    //else
                                    //{
                                    string[] strProdSingle = strProdArray[j].Split('-');
                                    if (strProdSingle.Length > 1)
                                    {
                                        if (strProdSingle[1] != string.Empty)
                                        {
                                            objInput.RecordDetailsAdd(output, baseitem.Value, div_code, strProdSingle[0], Convert.ToInt32(strProdSingle[1]));
                                        }
                                    }
                                }
                            }
                        }
                        //}
                        //}
                    }
                }
            }


        }

        this.ClearControls();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Input Despatch HQ Saved Successfully!');", true);

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
        // dsInputs = objProduct.getGift(div_code);
        if (ddlsub.SelectedValue == "0")
        {
            dsInputs = objProduct.getGift(div_code);
        }
        else
        {
            dsInputs = objProduct.getGift_Subdiv(div_code, ddlsub.SelectedValue);
        }
        if (dsInputs.Tables.Count > 0)
        {
            if (dsInputs.Tables[0].Rows.Count > 0)
            {
                gvSampleProducts.DataSource = dsInputs;
                gvSampleProducts.DataBind();
            }
        }
    }

    protected void rdoInput_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void ClearControls()
    {
        //lstFieldForce.SelectedIndex = -1;
        if (lstFieldForce.SelectedIndex > -1)
        {
            lstFieldForce.SelectedIndex = -1;
            lstFieldForce.Disabled = false;
            lstFieldForce.Attributes.Add("title", "select Fieldforce");
        }
        //lstBaseLevel.Items.Clear();
        lstBaseLevel.Visible = false;
        Label1.Visible = false;
        //ddlYear.SelectedIndex = 0;
        //ddlMonth.SelectedIndex = 0;
        rdoInput.SelectedIndex = 0;
        gvSampleProducts.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
        //divInputs.Visible = false;
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


        gvSampleProducts.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
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
        gvSampleProducts.Visible = false;
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