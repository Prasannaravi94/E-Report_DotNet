using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Globalization;
using System.Drawing;
public partial class MasterFiles_ActivityReports_DoctorPotientalBusinessEntry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string state_code = string.Empty;
    string trCode = string.Empty;
    string MRP_Price = string.Empty;
    string R_Price = string.Empty;
    string Distributor_Price = string.Empty;
    string Target_Price = string.Empty;
    string NSR_Price = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();
    DataSet dsSalesForce = null;
    DataSet dsAdminSetup = null;
    DataSet st = null;
    Territory objTerritory = new Territory();
    DataSet dsListedDr = null;
    DataSet dsTrans_Bus = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    ListedDR lst = new ListedDR();
    Product objProduct = new Product();
    DataSet dsProducts = null;
    int time;

    string div_code = string.Empty;

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //linkcheck.Visible = false;
                //lblFF.Visible = false;
                lblFieldForceName.Visible = false;
                ddlFieldForce.Visible = false;
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
                AjaxControlToolkit.ToolkitScriptManager s = (AjaxControlToolkit.ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                this.FillMasterList_adm();

            }

            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                this.FillMasterList_adm();
                // c1.FindControl("btnBack").Visible = false;
                //lblFieldForceName.Visible = false;
                //ddlFieldForce.Visible = false;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            //ddlMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
        else
        {
            FillddllistedColor();

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
                sfCode = ddlFieldForce.SelectedValue;
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
            else if (Session["sf_type"].ToString() == "2")
            {
                sfCode = ddlFieldForce.SelectedValue;
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
        }

        AdminSetup ad = new AdminSetup();
        dsAdminSetup = ad.getOtherSetupfor_Drbus_Cal_Based(div_code);

        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            hdnBasedOn.Value = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        SalesForce sff = new SalesForce();

        st = sff.CheckStatecode(sfCode);
        if (st.Tables[0].Rows.Count > 0)
        {
            state_code = st.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        dsProducts = objProduct.getProdstadoctorBasedOn(div_code, state_code, hdnBasedOn.Value);
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
    private void BindListedDr()
    {
        gvListedDr.Visible = true;
        //
        #region Variable
        HiddenField ListedDr_Code;
        Label lblDrValue;
        #endregion
        //       
        ListedDR dr = new ListedDR();
        ListedDR gg = new ListedDR();
        string sf_code = Session["sf_type"].ToString() == "2" ? ddlFieldForce.SelectedValue : sfCode;
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        dsListedDr = dr.Trans_PotListedDr_Bus_View(sf_code, div_code, MonthVal, YearVal);
        DataSet ff = new DataSet();
        ff = gg.getPotListedDrDiv_new(div_code, sf_code);

        DataTable dtDr = new DataTable();
        dtDr = ff.Tables[0].Clone();

        foreach (DataRow drBus in dsListedDr.Tables[0].Rows)
        {
            var table = ff.Tables[0].AsEnumerable()
            .Where(r => r.Field<decimal>("ListedDrCode") == Convert.ToDecimal(drBus["ListedDrCode"]))
            .AsDataView().ToTable();

            foreach (DataRow row in table.Rows)
            {
                if (drBus.ItemArray[0].ToString() != row.ItemArray[0].ToString())
                {
                    DataRow dtr = dtDr.NewRow();
                    dtDr.ImportRow(row);
                }
            }
        }
        dtDr.AcceptChanges();

        if (dtDr.Rows.Count > 0)
        {
            gvListedDr.DataSource = dtDr;
            gvListedDr.DataBind();

            foreach (GridViewRow gridrow in gvListedDr.Rows)
            {
                ListedDr_Code = (HiddenField)gridrow.FindControl("hdnListedDr_Code");
                lblDrValue = (Label)gridrow.FindControl("lblDrValue");

                dsListedDr = dr.PotListedDrBus_value(sf_code, ListedDr_Code.Value, div_code, MonthVal, YearVal, hdnBasedOn.Value);

                if (dsListedDr.Tables[0].Rows.Count > 0)
                {
                    lblDrValue.Text = dsListedDr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }

            decimal dPrice = 0;
            foreach (GridViewRow gridrow in gvListedDr.Rows)
            {
                lblDrValue = (Label)gridrow.FindControl("lblDrValue");
                dPrice += Convert.ToDecimal(lblDrValue.Text);
            }

            gvListedDr.FooterRow.Cells[4].Text = "Grand Total:";
            gvListedDr.FooterRow.Cells[4].Font.Size = FontUnit.Large;
            gvListedDr.FooterRow.Cells[4].Font.Bold = true;
            gvListedDr.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            gvListedDr.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
            gvListedDr.FooterRow.Cells[5].Text = dPrice.ToString();
            gvListedDr.FooterRow.Cells[5].CssClass = "dtxt";
            gvListedDr.FooterRow.Cells[5].BackColor = System.Drawing.Color.White;
            gvListedDr.FooterRow.Cells[5].ForeColor = System.Drawing.Color.Red;
            string hex = "#666699";
            Color color = System.Drawing.ColorTranslator.FromHtml(hex);
            gvListedDr.FooterRow.Cells[5].BorderColor = color;
            gvListedDr.FooterRow.Cells[5].BorderWidth = 1;
            gvListedDr.FooterRow.Cells[5].Font.Size = FontUnit.Large;
            gvListedDr.FooterRow.Cells[5].Font.Bold = true;
        }
        else
        {
            gvListedDr.DataSource = null;
            gvListedDr.DataBind();
        }
    }
    private void FillListedDr()
    {
        ListedDR gg = new ListedDR();
        DataSet ff = new DataSet();
        DataTable dtDoctor = new DataTable();
        string sf_code = Session["sf_type"].ToString() == "2" ? ddlFieldForce.SelectedValue : sfCode;
        dsListedDr = gg.getPotListedDrDiv_new(div_code, sf_code);

        if (dsListedDr.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsListedDr.Tables[0].AsEnumerable()
                         select new
                         {
                             Dr_Name = data.Field<string>("ListedDr_Name"),
                             Dr_Code = data.Field<decimal>("ListedDrCode")
                         };
            var listOfGrades = result.OrderBy(o => o.Dr_Name).ToList();
            ddlListedDr.Visible = true;
            ddlListedDr.DataSource = listOfGrades;
            ddlListedDr.DataTextField = "Dr_Name";
            ddlListedDr.DataValueField = "Dr_Code";
            ddlListedDr.DataBind();
            ddlListedDr.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    private void FillddllistedColor()
    {
        if (gvListedDr.Rows.Count > 0)
        {
            foreach (GridViewRow row in gvListedDr.Rows)
            {
                HiddenField hdnListedDr_Code = (HiddenField)row.FindControl("hdnListedDr_Code");
                ListItem ddl = ddlListedDr.Items.FindByValue(hdnListedDr_Code.Value);
                if (ddl != null)
                {
                    ddl.Attributes.Add("style", "background-color: #ffff00 !important");
                }
            }
        }
    }
    private void FillMasterList_adm()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();
        if (Session["sf_type"].ToString() == "2")
        {
            sfCode = Session["sf_code"].ToString();
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sfCode);
        }
        else { dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin"); }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            if (Session["sf_type"].ToString() != "2")
            {
                ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = false;
        }

        BindListedDr();
        FillListedDr();
        FillddllistedColor();
        tbListedDr.Visible = true;
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void txtNew_TextChanged(object sender, EventArgs e)
    //{
    //    ddlFieldForce_SelectedIndexChanged(sender, e);
    //}
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{
    //    ddlFieldForce.Visible = true;
    //    txtNew.Visible = true;
    //    lblFieldForceName.Visible = true;
    //    linkcheck.Visible = false;
    //    lblFF.Visible = false;
    //    FillMasterList_adm();
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void ddlListedDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ListedDr = string.Empty;
        //txtNew1.Text = string.Empty;
        string sf_code = Session["sf_type"].ToString() == "2" ? ddlFieldForce.SelectedValue : sfCode;
        if (ddlListedDr.SelectedValue != "0")
        {
            ListedDR dr = new ListedDR();
            dsListedDr = dr.getListedDrDiv_new(div_code, sf_code);

            var rsTr_Name = from row in dsListedDr.Tables[0].AsEnumerable()
                            where row.Field<decimal>("ListedDrCode") == Convert.ToDecimal(ddlListedDr.SelectedValue)
                            select new
                            {
                                Sp_Name = row.Field<string>("Doc_Spec_ShortName"),
                                Cat_Name = row.Field<string>("Doc_Cat_ShortName"),
                                Tr_Name = row.Field<string>("Territory_Name"),
                                Tr_Code = row.Field<string>("Territory_Code")
                            };
            lblSpecName.Text = rsTr_Name.FirstOrDefault().Sp_Name.ToString();
            lblCatName.Text = rsTr_Name.FirstOrDefault().Cat_Name.ToString();
            lblTrName.Text = rsTr_Name.FirstOrDefault().Tr_Name.ToString();
            hdnTrCode.Value = rsTr_Name.FirstOrDefault().Tr_Code.ToString();

            txtTotalVal.Text = string.Empty;

            #region Variable
            TextBox txtQty;
            TextBox txtValue;
            #endregion

            foreach (GridViewRow gridrow in gvProduct.Rows)
            {
                #region Variables
                txtQty = (TextBox)gridrow.FindControl("txtQty");
                txtValue = (TextBox)gridrow.FindControl("txtValue");
                #endregion

                txtQty.Text = string.Empty;
                txtValue.Text = string.Empty;
            }

            dvProduct.Visible = false;
        }
        else
        {
            lblSpecName.Text = "-";
            lblCatgry.Text = "-";
            lblTrName.Text = "-";
        }
    }
    private void FillPrd()
    {
        dvProduct.Visible = true;
        //
        #region Variable
        HiddenField hdnProductCode;
        Label lblPrice;
        TextBox txtQty;
        TextBox txtValue;
        #endregion
        //

        dsProducts = objProduct.getPotProdstadoctorBasedOn(div_code, state_code, hdnBasedOn.Value);

        if (dsProducts.Tables[0].Rows.Count > 0)
        {
            gvProduct.EnableViewState = true;
            gvProduct.DataSource = dsProducts;
            gvProduct.DataBind();
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            dsListedDr = lst.ListedPotDrBus_value(sfCode, ddlListedDr.SelectedValue, div_code, MonthVal, YearVal, hdnBasedOn.Value);

            foreach (GridViewRow gridrow in gvProduct.Rows)
            {
                hdnProductCode = (HiddenField)gridrow.FindControl("hdnProductCode");
                txtQty = (TextBox)gridrow.FindControl("txtQty");
                lblPrice = (Label)gridrow.FindControl("lblPrice");
                txtValue = (TextBox)gridrow.FindControl("txtValue");

                dsTrans_Bus = lst.Trans_ListedPotDr_Bus_DetailExist(sfCode, ddlListedDr.SelectedValue, hdnProductCode.Value, div_code, MonthVal, YearVal, hdnBasedOn.Value);

                if (dsTrans_Bus.Tables[0].Rows.Count > 0)
                {
                    txtQty.Text = dsTrans_Bus.Tables[0].Rows[0]["Potiental_Quantity"].ToString();
                    txtValue.Text = dsTrans_Bus.Tables[0].Rows[0]["value"].ToString();
                }
            }
            txtTotalVal.Text = dsListedDr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        else
        {
            gvProduct.DataSource = dsProducts;
            gvProduct.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string sf_code = Session["sf_type"].ToString() == "2" ? ddlFieldForce.SelectedValue : sfCode;
        try
        {
            //
            #region Variable
            HiddenField hdnProductCode;
            Label lblProductName;
            Label lblprd_sale;
            Label lblPrice;
            TextBox txtQty;
            TextBox txtValue;
            int iReturn = 0;
            #endregion
            //
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);
            foreach (GridViewRow gridrow in gvProduct.Rows)
            {
                #region Variables
                hdnProductCode = (HiddenField)gridrow.FindControl("hdnProductCode");
                lblProductName = (Label)gridrow.FindControl("lblProductName");
                lblprd_sale = (Label)gridrow.FindControl("lblprd_sale");
                txtQty = (TextBox)gridrow.FindControl("txtQty");
                lblPrice = (Label)gridrow.FindControl("lblPrice");
                txtValue = (TextBox)gridrow.FindControl("txtValue");
                #endregion

                iReturn = InsertListedDrBusEntry(sf_code, MonthVal.ToString(), YearVal.ToString(), hdnProductCode, lblProductName, lblprd_sale, txtQty, lblPrice, txtValue);
            }

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('ListedDr Product Business Entry Updated Successfully');</script>");

            Clear();
            dvProduct.Visible = false;
            BindListedDr();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    private void Clear()
    {
        ddlListedDr.SelectedValue = "0";
        lblSpecName.Text = "-";
        lblCatName.Text = "-";
        lblTrName.Text = "-";
        txtTotalVal.Text = string.Empty;

        #region Variable
        TextBox txtQty;
        TextBox txtValue;
        #endregion

        foreach (GridViewRow gridrow in gvProduct.Rows)
        {
            #region Variables
            txtQty = (TextBox)gridrow.FindControl("txtQty");
            txtValue = (TextBox)gridrow.FindControl("txtValue");
            #endregion

            txtQty.Text = string.Empty;
            txtValue.Text = string.Empty;
        }
    }
    //
    int i = 0;
    #region InsertListedDrBusEntry
    //private int InsertListedDrBusEntry(string sf_Code, DropDownList ddlMonth, DropDownList ddlYear, HiddenField hdnProductCode, Label lblProductName, Label lblprd_sale, TextBox txtQty, Label lblPrice, TextBox txtValue)
    //{
    private int InsertListedDrBusEntry(string sf_Code, string ddlMonth, string ddlYear, HiddenField hdnProductCode, Label lblProductName, Label lblprd_sale, TextBox txtQty, Label lblPrice, TextBox txtValue)
    {
        int iReturn = -1;

        ListedDR lst = new ListedDR();

        if (txtQty.Text != string.Empty && txtQty.Text != "0" && txtValue.Text != "0" && lblPrice.Text != "0")
        {
            dsListedDr = lst.Trans_ListedPotDr_Bus_HeadExist(sfCode, ddlListedDr.SelectedValue, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), hdnBasedOn.Value);
            dsTrans_Bus = lst.Trans_ListedPotDr_Bus_DetailExist(sfCode, ddlListedDr.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), hdnBasedOn.Value);

            if (dsTrans_Bus.Tables[0].Rows.Count == 0)
            {
                i++;
                if (i == 1)
                {
                    if (dsListedDr.Tables[0].Rows.Count == 0)
                    {
                        iReturn = lst.RecordAddTrans_ListedPotDrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlListedDr.SelectedValue),
                            ddlListedDr.SelectedItem.Text.Trim());
                    }
                    else
                    {
                        iReturn = lst.RecordUpdTrans_ListedPotDrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlListedDr.SelectedValue));
                    }
                }

                dsListedDr = lst.Trans_ListedPotDr_Bus_HeadSlNo(sfCode, ddlListedDr.SelectedValue, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));
                decimal Trans_sl_No = Convert.ToDecimal(dsListedDr.Tables[0].Rows[0]["Trans_sl_No"]);

                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    var rsTr_Name = from row in dsProducts.Tables[0].AsEnumerable()
                                    where row.Field<string>("Product_Detail_Code") == hdnProductCode.Value
                                    select new
                                    {
                                        MRP_Price = row.Field<string>("MRP_Price"),
                                        R_Price = row.Field<string>("R_Price"),
                                        Distributor_Price = row.Field<string>("Distributor_Price"),
                                        Target_Price = row.Field<string>("Target_Price"),
                                        NSR_Price = row.Field<string>("NSR_Price")
                                    };

                    MRP_Price = rsTr_Name.FirstOrDefault().MRP_Price.ToString();
                    R_Price = rsTr_Name.FirstOrDefault().R_Price.ToString();
                    Distributor_Price = rsTr_Name.FirstOrDefault().Distributor_Price.ToString();
                    Target_Price = rsTr_Name.FirstOrDefault().Target_Price.ToString();
                    NSR_Price = rsTr_Name.FirstOrDefault().NSR_Price.ToString();
                }

                iReturn = lst.RecordAddTrans_ListedPotDrBus_Details(Trans_sl_No, sfCode, Convert.ToInt32(div_code), Convert.ToInt32(ddlListedDr.SelectedValue),
                        hdnProductCode.Value, lblProductName.Text, lblprd_sale.Text, hdnTrCode.Value, lblTrName.Text, Convert.ToInt32(txtQty.Text), Convert.ToDouble(MRP_Price), Convert.ToDouble(R_Price), Convert.ToDouble(Distributor_Price), Convert.ToDouble(NSR_Price), Convert.ToDouble(Target_Price),
                        Convert.ToDouble(txtValue.Text));
            }
            else
            {
                i++;
                if (i == 1)
                {
                    if (dsListedDr.Tables[0].Rows.Count > 0)
                    {
                        iReturn = lst.RecordUpdTrans_ListedPotDrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlListedDr.SelectedValue));
                    }
                }

                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    var rsTr_Name = from row in dsProducts.Tables[0].AsEnumerable()
                                    where row.Field<string>("Product_Detail_Code") == hdnProductCode.Value
                                    select new
                                    {
                                        MRP_Price = row.Field<string>("MRP_Price"),
                                        R_Price = row.Field<string>("R_Price"),
                                        Distributor_Price = row.Field<string>("Distributor_Price"),
                                        Target_Price = row.Field<string>("Target_Price"),
                                        NSR_Price = row.Field<string>("NSR_Price")
                                    };

                    MRP_Price = rsTr_Name.FirstOrDefault().MRP_Price.ToString();
                    R_Price = rsTr_Name.FirstOrDefault().R_Price.ToString();
                    Distributor_Price = rsTr_Name.FirstOrDefault().Distributor_Price.ToString();
                    Target_Price = rsTr_Name.FirstOrDefault().Target_Price.ToString();
                    NSR_Price = rsTr_Name.FirstOrDefault().NSR_Price.ToString();
                }

                iReturn = lst.RecordUpdTrans_ListedPotDrBus_Details(sfCode, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear),
                        Convert.ToInt32(div_code), Convert.ToInt32(ddlListedDr.SelectedValue), hdnProductCode.Value, Convert.ToInt32(txtQty.Text), Convert.ToDouble(txtValue.Text));
            }
        }
        else if (txtQty.Text == string.Empty || txtQty.Text == "0")
        {
            dsTrans_Bus = lst.Trans_ListedPotDr_Bus_DetailExist(sfCode, ddlListedDr.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), hdnBasedOn.Value);

            if (dsTrans_Bus.Tables[0].Rows.Count > 0)
            {
                iReturn = lst.Delete_ListedPotDrProductDetailsBusiness(sfCode, ddlListedDr.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));
            }
        }
        return iReturn;
    }
    #endregion
    protected void lblbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdnListedDr_Code = (HiddenField)gvListedDr.Rows[rowID].Cells[1].FindControl("hdnListedDr_Code");
        Label lblterr = (Label)gvListedDr.Rows[rowID].Cells[4].FindControl("lblterr");
        Label lblSpec = (Label)gvListedDr.Rows[rowID].Cells[4].FindControl("lblSpec");
        Label lblCat = (Label)gvListedDr.Rows[rowID].Cells[4].FindControl("lblCat");
        HiddenField hdnTrtyCode = (HiddenField)gvListedDr.Rows[rowID].Cells[4].FindControl("hdnTrtyCode");

        ddlListedDr.SelectedValue = hdnListedDr_Code.Value;
        lblTrName.Text = lblterr.Text;
        hdnTrCode.Value = hdnTrtyCode.Value;
        lblSpecName.Text = lblSpec.Text;
        lblCatName.Text = lblCat.Text;

        FillPrd();
    }
    protected void gvListedDr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        ListedDR lst = new ListedDR();

        try
        {
            //
            #region Variable
            HiddenField hdnListedDr_Code;
            int iReturn = 0;
            #endregion
            //

            #region Variables
            hdnListedDr_Code = (HiddenField)gvListedDr.Rows[e.RowIndex].Cells[1].FindControl("hdnListedDr_Code");
            #endregion
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            iReturn = lst.Delete_ListedPotDrBusEntry(sfCode, hdnListedDr_Code.Value, div_code, MonthVal, YearVal);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('ListedDr Product Business Entry Deleted Successfully');</script>");
            }

            Clear();
            dvProduct.Visible = false;
            BindListedDr();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    protected void lblbtnProduct_Click(object sender, EventArgs e)
    {
        FillPrd();
    }
    protected void txtNew1_TextChanged(object sender, EventArgs e)
    {
        ddlListedDr_SelectedIndexChanged(sender, e);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        dvProduct.Visible = false;
        BindListedDr();
    }
}