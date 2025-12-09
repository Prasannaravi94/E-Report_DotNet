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

public partial class DoctorBusinessEntry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string state_code = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();
    DataSet dsDoc = null;
    DataSet dsdoc = null;
    DataSet ff = null;
    DataSet st = null;
    Territory objTerritory = new Territory();
    DataSet dsChemists = null;
    DataSet dsTrans_Bus = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
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
                this.getddlSF_Code();
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
            getddlSF_Code();
        }

        else
        {
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
    private void BindChemist()
    {
        gvChemists.Visible = true;
        //
        #region Variable
        HiddenField Chemists_Code;
        Label lblChValue;
        #endregion
        //

        Chemist chem = new Chemist();

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        dsChemists = chem.Trans_ChPr_Bus_View(sfCode, div_code, Convert.ToInt32(MonthVal.ToString()),
            Convert.ToInt32(YearVal.ToString()));
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            gvChemists.DataSource = dsChemists;
            gvChemists.DataBind();

            foreach (GridViewRow gridrow in gvChemists.Rows)
            {
                Chemists_Code = (HiddenField)gridrow.FindControl("hdnChemists_Code");
                lblChValue = (Label)gridrow.FindControl("lblChValue");

                dsChemists = chem.ChPrBus_value(sfCode, Chemists_Code.Value, div_code, Convert.ToInt32(MonthVal.ToString()), Convert.ToInt32(YearVal.ToString()));

                if (dsChemists.Tables[0].Rows.Count > 0)
                {
                    lblChValue.Text = dsChemists.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }

            decimal dPrice = 0;
            foreach (GridViewRow gridrow in gvChemists.Rows)
            {
                lblChValue = (Label)gridrow.FindControl("lblChValue");
                dPrice += Convert.ToDecimal(lblChValue.Text);
            }

            gvChemists.FooterRow.Cells[2].Text = "Grand Total:";
            gvChemists.FooterRow.Cells[2].Font.Size = FontUnit.Large;
            gvChemists.FooterRow.Cells[2].Font.Bold = true;
            gvChemists.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            gvChemists.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
            gvChemists.FooterRow.Cells[3].Text = dPrice.ToString();
            gvChemists.FooterRow.Cells[3].CssClass = "dtxt";
            gvChemists.FooterRow.Cells[3].BackColor = System.Drawing.Color.White;
            gvChemists.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;
            string hex = "#666699";
            Color color = System.Drawing.ColorTranslator.FromHtml(hex);
            gvChemists.FooterRow.Cells[3].BorderColor = color;
            gvChemists.FooterRow.Cells[3].BorderWidth = 1;
            gvChemists.FooterRow.Cells[3].Font.Size = FontUnit.Large;
            gvChemists.FooterRow.Cells[3].Font.Bold = true;
        }
        else
        {
            gvChemists.DataSource = dsChemists;
            gvChemists.DataBind();
        }
    }
    private void FillChemist()
    {
        Chemist chem = new Chemist();

        dsChemists = chem.getChemists(sfCode);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsChemists.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("Chemists_Name"),
                             Ch_Code = data.Field<int>("Chemists_Code")
                         };
            var listOfGrades = result.OrderBy(o => o.Ch_Name).ToList();
            ddlChemists.Visible = true;
            ddlChemists.DataSource = listOfGrades;
            ddlChemists.DataTextField = "Ch_Name";
            ddlChemists.DataValueField = "Ch_Code";
            ddlChemists.DataBind();
            ddlChemists.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);

        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsTerritory;
            ddlFieldForce.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlFieldForce.SelectedIndex = 1;
                sfCode = ddlFieldForce.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
            }
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedIndex > -1)
        {
            ddlFieldForce.Enabled = false;
        }

        FillChemist();
        tbChemists.Visible = true;
        BindChemist();
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0")
        {
            FillChemist();
        }
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
    //    getddlSF_Code();
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void ddlChemists_SelectedIndexChanged(object sender, EventArgs e)
    {
        string chemists = string.Empty;
        //txtNew1.Text = string.Empty;

        if (ddlChemists.SelectedValue != "0")
        {
            Chemist chem = new Chemist();
            dsChemists = chem.getChemists(sfCode);

            var rsTr_Name = from row in dsChemists.Tables[0].AsEnumerable()
                            where row.Field<int>("Chemists_Code") == Convert.ToInt32(ddlChemists.SelectedValue)
                            select new
                            {
                                Tr_Name = row.Field<string>("territory_Name"),
                                Tr_Code = row.Field<decimal>("Territory_Code")
                            };

            lblTrName.Text = rsTr_Name.FirstOrDefault().Tr_Name;
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

        SalesForce sff = new SalesForce();

        st = sff.CheckStatecode(sfCode);
        if (st.Tables[0].Rows.Count > 0)
        {
            state_code = st.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        Chemist lst = new Chemist();
        Product objProduct = new Product();
        DataSet dsProducts = null;

        dsProducts = objProduct.getProdstadoctor(div_code, state_code);
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        if (dsProducts.Tables[0].Rows.Count > 0)
        {
            gvProduct.EnableViewState = true;
            gvProduct.DataSource = dsProducts;
            gvProduct.DataBind();

            dsChemists = lst.ChPrBus_value(sfCode, ddlChemists.SelectedValue, div_code, MonthVal, YearVal);

            foreach (GridViewRow gridrow in gvProduct.Rows)
            {
                hdnProductCode = (HiddenField)gridrow.FindControl("hdnProductCode");
                txtQty = (TextBox)gridrow.FindControl("txtQty");
                lblPrice = (Label)gridrow.FindControl("lblPrice");
                txtValue = (TextBox)gridrow.FindControl("txtValue");

                dsTrans_Bus = lst.Trans_ChPr_Bus_DetailExist(sfCode, ddlChemists.SelectedValue, hdnProductCode.Value, div_code, MonthVal, YearVal);

                if (dsTrans_Bus.Tables[0].Rows.Count > 0)
                {
                    txtQty.Text = dsTrans_Bus.Tables[0].Rows[0]["Product_Quantity"].ToString();
                    txtValue.Text = dsTrans_Bus.Tables[0].Rows[0]["value"].ToString();
                }
            }
            txtTotalVal.Text = dsChemists.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
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
                
                iReturn = InsertChPrBusEntry(sfCode, MonthVal.ToString(), YearVal.ToString(), hdnProductCode, lblProductName, lblprd_sale, txtQty, lblPrice, txtValue);
            }

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Chemists Product Business Entry Updated Successfully');</script>");

            Clear();
            dvProduct.Visible = false;
            BindChemist();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    private void Clear()
    {
        ddlChemists.SelectedValue = "0";
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
    #region InsertDocBusEntry
    //private int InsertChPrBusEntry(string sf_Code, DropDownList ddlMonth, DropDownList ddlYear, HiddenField hdnProductCode, Label lblProductName, Label lblprd_sale, TextBox txtQty, Label lblPrice, TextBox txtValue)
    //{
    private int InsertChPrBusEntry(string sf_Code, string ddlMonth, string ddlYear, HiddenField hdnProductCode, Label lblProductName, Label lblprd_sale, TextBox txtQty, Label lblPrice, TextBox txtValue)
    {
        int iReturn = -1;

        Chemist lst = new Chemist();

        if (txtQty.Text != string.Empty && txtQty.Text != "0" && txtValue.Text != "0" && lblPrice.Text != "0")
        {
            dsChemists = lst.Trans_ChPr_Bus_HeadExist(sfCode, ddlChemists.SelectedValue, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));
            dsTrans_Bus = lst.Trans_ChPr_Bus_DetailExist(sfCode, ddlChemists.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));

            if (dsTrans_Bus.Tables[0].Rows.Count == 0)
            {
                i++;
                if (i == 1)
                {
                    if (dsChemists.Tables[0].Rows.Count == 0)
                    {
                        iReturn = lst.RecordAddTrans_ChPrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlChemists.SelectedValue),
                            ddlChemists.SelectedItem.Text.Trim());
                    }
                    else
                    {
                        iReturn = lst.RecordUpdTrans_ChPrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlChemists.SelectedValue));
                    }
                }
                iReturn = lst.RecordAddTrans_ChPrBus_Details(sfCode, Convert.ToInt32(div_code), Convert.ToInt32(ddlChemists.SelectedValue),
                        hdnProductCode.Value, lblProductName.Text, lblprd_sale.Text, hdnTrCode.Value, lblTrName.Text, Convert.ToInt32(txtQty.Text), null, Convert.ToDecimal(lblPrice.Text), null, null, null,
                        Convert.ToDecimal(txtValue.Text));
            }
            else
            {
                i++;
                if (i == 1)
                {
                    if (dsChemists.Tables[0].Rows.Count > 0)
                    {
                        iReturn = lst.RecordUpdTrans_ChPrBus_Head(sfCode, Convert.ToInt32(ddlMonth),
                            Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), null, Convert.ToInt32(ddlChemists.SelectedValue));
                    }
                }
                iReturn = lst.RecordUpdTrans_ChPrBus_Details(sfCode, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear),
                        Convert.ToInt32(div_code), Convert.ToInt32(ddlChemists.SelectedValue), hdnProductCode.Value, hdnTrCode.Value,
                        Convert.ToInt32(txtQty.Text), null, Convert.ToDecimal(lblPrice.Text), null, null, null, Convert.ToDecimal(txtValue.Text));
            }


        }
        else if (txtQty.Text == string.Empty || txtQty.Text == "0")
        {
            dsTrans_Bus = lst.Trans_ChPr_Bus_DetailExist(sfCode, ddlChemists.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));

            if (dsTrans_Bus.Tables[0].Rows.Count > 0)
            {
                iReturn = lst.Delete_ChemistProductDetailsBusiness(sfCode, ddlChemists.SelectedValue, hdnProductCode.Value, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));
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
        HiddenField hdnChemists_Code = (HiddenField)gvChemists.Rows[rowID].Cells[1].FindControl("hdnChemists_Code");
        Label lblterr = (Label)gvChemists.Rows[rowID].Cells[2].FindControl("lblterr");
        HiddenField hdnTrtyCode = (HiddenField)gvChemists.Rows[rowID].Cells[2].FindControl("hdnTrtyCode");

        ddlChemists.SelectedValue = hdnChemists_Code.Value;
        lblTrName.Text = lblterr.Text;
        hdnTrCode.Value = hdnTrtyCode.Value;

        FillPrd();
    }
    protected void gvChemists_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        Chemist lst = new Chemist();

        try
        {
            //
            #region Variable
            HiddenField hdnChemists_Code;
            int iReturn = 0;
            #endregion
            //

            #region Variables
            hdnChemists_Code = (HiddenField)gvChemists.Rows[e.RowIndex].Cells[1].FindControl("hdnChemists_Code");
            #endregion
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            iReturn = lst.Delete_ChemistProductBusiness(sfCode, hdnChemists_Code.Value, div_code, MonthVal, YearVal);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Chemists Product Business Entry Deleted Successfully');</script>");
            }

            Clear();
            dvProduct.Visible = false;
            BindChemist();
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
    //protected void txtNew1_TextChanged(object sender, EventArgs e)
    //{
    //    ddlChemists_SelectedIndexChanged(sender, e);
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        dvProduct.Visible = false;
        BindChemist();
    }
}