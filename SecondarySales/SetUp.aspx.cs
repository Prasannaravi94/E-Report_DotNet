using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text.RegularExpressions;


public partial class SecondarySales_SetUp : System.Web.UI.Page
{

    #region "Variable Declarations"
    DataSet dsSale = null;
    string div_code = string.Empty;
    string strError = string.Empty;
    int iErrReturn = -1;
    int Sec_Code;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            btnSubmit.Visible = false;
            btnClear.Visible = false;
            FillSecSales();
            Bind_ReActive_Parameter_Plus();
            Bind_ReActive_Parameter_Minus();
            Bind_ReActive_Parameter_Close();
            Bind_ReActive_Parameter_UserCol();
            Bind_Field_Parameter();

            //string strGroupID = "ASD";           
            //var result = "<span style='font-weight:bold;'>" + strGroupID + "</span>";
            //lbltest.Text = result;

            if (div_code == "45")
            {
                Bind_PrimarySaleColumn();
            }
            if (div_code == "3" || div_code == "36")
            {
                Bind_PrimaryBill_Column();
            }
        }
    }

    //public string formaatedstring(string mainvalue, string partvalue)
    //{
    //    return Regex.Replace(mainvalue, partvalue, @"<b>$0</b>", RegexOptions.IgnoreCase);
    //}

    private void Bind_ReActive_Parameter_Plus()
    {

        SecSale ss = new SecSale();
        dsSale = ss.Get_ReActivate_SS_Parameter_Plus("+", div_code);

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkParam_Plus.DataSource = dsSale;
            ChkParam_Plus.DataTextField = "Sec_Sale_Name";
            ChkParam_Plus.DataValueField = "Sec_Sale_Code";
            ChkParam_Plus.DataBind();

            lbl_Param_Plus.Visible = false;
            btnActive_Minus.Visible = true;

        }
        else
        {
            lbl_Param_Plus.Visible = true;
            btnActive_Minus.Visible = false;
        }

    }
    private void Bind_ReActive_Parameter_Minus()
    {

        SecSale ss = new SecSale();
        dsSale = ss.Get_ReActivate_SS_Parameter_Plus("-", div_code);

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkParam_Minus.DataSource = dsSale;
            ChkParam_Minus.DataTextField = "Sec_Sale_Name";
            ChkParam_Minus.DataValueField = "Sec_Sale_Code";
            ChkParam_Minus.DataBind();

            lbl_Param_Minus.Visible = false;
            btnActive_Minus.Visible = true;
        }
        else
        {
            lbl_Param_Minus.Visible = true;
            btnActive_Minus.Visible = false;
        }

    }


    private void Bind_ReActive_Parameter_Close()
    {

        SecSale ss = new SecSale();
        dsSale = ss.Get_ReActivate_SS_Parameter_Plus("C", div_code);

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkParam_ParamClose.DataSource = dsSale;
            ChkParam_ParamClose.DataTextField = "Sec_Sale_Name";
            ChkParam_ParamClose.DataValueField = "Sec_Sale_Code";
            ChkParam_ParamClose.DataBind();

            lbl_Param_Close.Visible = false;
            btnActive_ParamClose.Visible = true;
        }
        else
        {
            lbl_Param_Close.Visible = true;
            btnActive_ParamClose.Visible = false;
        }

    }


    private void Bind_ReActive_Parameter_UserCol()
    {
        SecSale ss = new SecSale();
        dsSale = ss.Get_ReActivate_SS_Parameter_Plus("D", div_code);

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkParam_UserColumn.DataSource = dsSale;
            ChkParam_UserColumn.DataTextField = "Sec_Sale_Name";
            ChkParam_UserColumn.DataValueField = "Sec_Sale_Code";
            ChkParam_UserColumn.DataBind();

            lbl_Param_UserColumn.Visible = false;
            btnActive_UserColumn.Visible = true;
        }
        else
        {
            lbl_Param_UserColumn.Visible = true;
            btnActive_UserColumn.Visible = false;
        }

    }

    private void Bind_Field_Parameter()
    {

        SecSale ss = new SecSale();
        dsSale = ss.Get_Field_Parameter_Calc(div_code);

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            //ChkParamList_plus.DataSource = dsSale;
            //ChkParamList_plus.DataTextField = "Sec_Sale_Name";
            //ChkParamList_plus.DataValueField = "Sec_Sale_Code";
            //ChkParamList_plus.DataBind();

            //ChkParamList_minus.DataSource = dsSale;
            //ChkParamList_minus.DataTextField = "Sec_Sale_Name";
            //ChkParamList_minus.DataValueField = "Sec_Sale_Code";
            //ChkParamList_minus.DataBind();

            //ChkParamList_Other.DataSource = dsSale;
            //ChkParamList_Other.DataTextField = "Sec_Sale_Name";
            //ChkParamList_Other.DataValueField = "Sec_Sale_Code";
            //ChkParamList_Other.DataBind();


            for (int i = 0; i < dsSale.Tables[0].Rows.Count; i++)
            {
                ListItem item = new ListItem();
                item.Text = dsSale.Tables[0].Rows[i]["Sec_Sale_Name"].ToString();
                item.Value = dsSale.Tables[0].Rows[i]["Sec_Sale_Code"].ToString();
                item.Attributes.Add("hiddenValue", item.Value);
                ChkParamList_plus.Items.Add(item);
                ChkParamList_minus.Items.Add(item);
                ChkParamList_Other.Items.Add(item);
                ChkParamList_formula.Items.Add(item);
            }


        }

    }

    private void Bind_PrimarySaleColumn()
    {
        SecSale ss = new SecSale();
        dsSale = ss.Get_Primary_Field();

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkPrime.DataSource = dsSale;
            ChkPrime.DataTextField = "ColumnName";
            ChkPrime.DataValueField = "ColumnName";
            ChkPrime.DataBind();
        }
    }

    private void Bind_PrimaryBill_Column()
    {
        SecSale ss = new SecSale();
        dsSale = ss.Get_Primary_Billwise_Field();

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            ChkParamList_Bill.DataSource = dsSale;
            ChkParamList_Bill.DataTextField = "ColumnName";
            ChkParamList_Bill.DataValueField = "ColumnName";
            ChkParamList_Bill.DataBind();
        }

    }

    private void FillSecSales()
    {
        try
        {
            SecSale ss = new SecSale();

            //Positive fields
            dsSale = ss.getSaleMaster("+", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSales.DataSource = dsSale;
                grdSecSales.DataBind();
                lblPlus.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
                btnReActivate_Plus.Visible = true;
                gvAddSecSale.Visible = true;
            }
            else
            {
                lblPlus.Visible = false;
                btnReActivate_Plus.Visible = false;
                gvAddSecSale.Visible = false;
            }
            //Negative fields
            dsSale = ss.getSaleMaster("-", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSalesMinus.DataSource = dsSale;
                grdSecSalesMinus.DataBind();
                lblMinus.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
                btnReActivate_Minus.Visible = true;
                gvMinusSecSale.Visible = true;
            }
            else
            {
                lblMinus.Visible = false;
                btnReActivate_Minus.Visible = false;
                gvMinusSecSale.Visible = false;
            }
            //Closing Balance
            dsSale = ss.getSaleMaster("C", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSalesOthers.DataSource = dsSale;
                grdSecSalesOthers.DataBind();
                LblOth.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
                btnReActivate_ParamClose.Visible = true;
                gvOtherSecSale.Visible = true;
            }
            else
            {
                LblOth.Visible = false;
                btnReActivate_ParamClose.Visible = false;
                gvOtherSecSale.Visible = false;
            }

            dsSale = ss.getSaleMaster("D", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdCol.DataSource = dsSale;
                grdCol.DataBind();
                lblCol.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
                btnReActive_UserColumn.Visible = true;
            }
            else
            {
                lblCol.Visible = false;
                btnReActive_UserColumn.Visible = false;
            }

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)e.Row.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)e.Row.FindControl("chkFreeQty");

                //  hdnSecSaleCode.Value = lblSaleCode.Text;

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Length > 0)
                            txtSub1.Text = dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Free_Needed"].ToString() == "1")
                            chkFreeNeed.Checked = true;
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }

            if (div_code == "45")
            {
                for (int i = 0; i < grdSecSales.Columns.Count; i++)
                {
                    if (grdSecSales.Columns[i].HeaderText == "Default Field Value Select")
                    {
                        grdSecSales.Columns[4].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }

            if (div_code == "3" || div_code == "36")
            {
                for (int i = 0; i < grdSecSales.Columns.Count; i++)
                {
                    if (grdSecSales.Columns[i].HeaderText == "Primary Bill")
                    {
                        grdSecSales.Columns[15].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }
          

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSales_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void grdSecSalesMinus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)e.Row.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");
                CheckBox chkFreeNeed = (CheckBox)e.Row.FindControl("chkFreeQty");

                // hdnSecSaleCode.Value = lblSaleCode.Text;

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Free_Needed"].ToString() == "1")
                            chkFreeNeed.Checked = true;

                        if (dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Length > 0)
                            txtSub1.Text = dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Trim();

                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }


            if (div_code == "45")
            {
                for (int i = 0; i < grdSecSalesMinus.Columns.Count; i++)
                {
                    if (grdSecSalesMinus.Columns[i].HeaderText == "Default Field Value Select")
                    {
                        grdSecSalesMinus.Columns[4].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }
            if (div_code == "3" || div_code == "36")
            {
                for (int i = 0; i < grdSecSalesMinus.Columns.Count; i++)
                {
                    if (grdSecSalesMinus.Columns[i].HeaderText == "Primary Bill")
                    {
                        grdSecSalesMinus.Columns[15].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }


        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSalesMinus_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }



    protected void grdSecSalesOthers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)e.Row.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)e.Row.FindControl("chkFreeQty");

                // hdnSecSaleCode.Value = lblSaleCode.Text;

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                       
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Free_Needed"].ToString() == "1")
                            chkFreeNeed.Checked = true;

                        if (dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Length > 0)
                            txtSub1.Text = dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }


            if (div_code == "45")
            {
                for (int i = 0; i < grdSecSalesOthers.Columns.Count; i++)
                {
                    if (grdSecSalesOthers.Columns[i].HeaderText == "Default Field Value Select")
                    {
                        grdSecSalesOthers.Columns[4].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }
            if (div_code == "3" || div_code == "36")
            {
                for (int i = 0; i < grdSecSalesOthers.Columns.Count; i++)
                {
                    if (grdSecSalesOthers.Columns[i].HeaderText == "Primary Bill")
                    {
                        grdSecSalesOthers.Columns[15].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSalesOthers_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }


    protected void grdCol_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)e.Row.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)e.Row.FindControl("chkFreeQty");

                //hdnSecSaleCode.Value = lblSaleCode.Text;

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();

                        if (dsSale.Tables[0].Rows[0]["Free_Needed"].ToString() == "1")
                            chkFreeNeed.Checked = true;

                        if (dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Length > 0)
                            txtSub1.Text = dsSale.Tables[0].Rows[0]["Sub_Label_1"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }

            if (div_code == "45" )
            {
                for (int i = 0; i < grdCol.Columns.Count; i++)
                {
                    if (grdCol.Columns[i].HeaderText == "Default Field Value Select")
                    {
                        grdCol.Columns[4].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }
            if (div_code == "3" || div_code == "36")
            {
                for (int i = 0; i < grdCol.Columns.Count; i++)
                {
                    if (grdCol.Columns[i].HeaderText == "Primary Bill")
                    {
                        grdCol.Columns[15].Visible = true;
                        //grdSecSales.Columns[4].HeaderText = "Primary Field";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdCol_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
                txtSub1.Text = "";

            }
            foreach (GridViewRow gridRow in grdSecSalesMinus.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");

                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
                txtSub1.Text = "";
            }
            foreach (GridViewRow gridRow in grdSecSalesOthers.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");

                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
                txtSub1.Text = "";
            }

            foreach (GridViewRow gridRow in grdCol.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");

                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
                txtSub1.Text = "";
            }


            txtParamName.Text = "";
            txtShortName.Text = "";
            ddlType.SelectedIndex = 0;
            FillSecSales();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "btnClear_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bRecordUpdated = false;
        int iSecSaleCode = 0;
        int iDisplay = 0;
        int iValue = 0;
        int iCarryFwd = 0;
        int iDisable = 0;
        int iCalc = 0;
        int iCalcDis = 0;
        int iCalcSale = 0;
        int iCarryFld = 0;
        int iSub = 0;
        string sSubLabel = string.Empty;
        string sSubLabel_1 = string.Empty;
        int iOrder = 0;
        int iRet = -1;
        bool bRecordExist = false;
        string sOrder = string.Empty;

        int iFree = 0;

        try
        {
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";
                sSubLabel_1 = "";
                iFree = 0;

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)gridRow.FindControl("chkFreeQty");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;
                if (chkFreeNeed.Checked)
                    iFree = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtSub1.Text.Trim().Length > 0)
                    sSubLabel_1 = txtSub1.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                string SubField = sSubLabel + "," + sSubLabel_1;

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.Add_SecSale_SetUp(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel, iFree, sSubLabel_1);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Duplicate Order ID exists');</script>");
                    break;
                }
            }


            //Minus
            sOrder = "";
            foreach (GridViewRow gridRow in grdSecSalesMinus.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel_1 = "";
                sSubLabel = "";
                iFree = 0;

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)gridRow.FindControl("chkFreeQty");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;
                if (chkFreeNeed.Checked)
                    iFree = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtSub1.Text.Trim().Length > 0)
                    sSubLabel_1 = txtSub1.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                string SubField = sSubLabel + "," + sSubLabel_1;

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.Add_SecSale_SetUp(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel, iFree, sSubLabel_1);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Duplicate Order ID exists');</script>");
                    break;
                }
            }


            // Closing Balance
            sOrder = "";
            foreach (GridViewRow gridRow in grdSecSalesOthers.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";
                sSubLabel_1 = "";
                iFree = 0;

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)gridRow.FindControl("chkFreeQty");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;
                if (chkFreeNeed.Checked)
                    iFree = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtSub1.Text.Trim().Length > 0)
                    sSubLabel_1 = txtSub1.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                string SubField = sSubLabel + "," + sSubLabel_1;

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.Add_SecSale_SetUp(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel, iFree, sSubLabel_1);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Duplicate Order ID exists');</script>");
                    break;
                }
            }

            // Closing Balance
            sOrder = "";
            foreach (GridViewRow gridRow in grdCol.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";
                sSubLabel_1 = "";
                iFree = 0;

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtSub1 = (TextBox)gridRow.FindControl("txtSub1");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                CheckBox chkFreeNeed = (CheckBox)gridRow.FindControl("chkFreeQty");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;
                if (chkFreeNeed.Checked)
                    iFree = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtSub1.Text.Trim().Length > 0)
                    sSubLabel_1 = txtSub1.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                string SubField = sSubLabel + "," + sSubLabel_1;

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.Add_SecSale_SetUp(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel, iFree, sSubLabel_1);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Duplicate Order ID exists');</script>");
                    break;
                }
            }

            if (bRecordUpdated)
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Secondary Sale Setup has been updated Successfully');</script>");
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "btnSubmit_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void grdSecSales_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSales.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSales.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSales.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSales.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSales.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSales.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        TextBox txtShortName = (TextBox)grdSecSales.Rows[e.RowIndex].Cells[3].FindControl("txtShortName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim(), txtShortName.Text.Trim());
        FillSecSales();
    }


    protected void grdSecSalesMinus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSalesMinus.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSalesMinus.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSalesMinus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSalesMinus.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSalesMinus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSalesMinus.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSalesMinus.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSalesMinus.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        TextBox txtShortName = (TextBox)grdSecSalesMinus.Rows[e.RowIndex].Cells[3].FindControl("txtShortName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim(), txtShortName.Text.Trim());
        FillSecSales();
    }

    protected void grdSecSalesOthers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSalesOthers.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSalesOthers.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSalesOthers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSalesOthers.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSalesOthers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSalesOthers.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSalesOthers.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSalesOthers.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        TextBox txtShortName = (TextBox)grdSecSalesOthers.Rows[e.RowIndex].Cells[3].FindControl("txtShortName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim(), txtShortName.Text.Trim());
        FillSecSales();
    }

    protected void grdSecSales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            Sec_Code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SecSale dv = new SecSale();
            int iReturn = dv.DeActivate_SecondarySale(Sec_Code, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            FillSecSales();

            Bind_ReActive_Parameter_Plus();
            // Bind_ReActive_Parameter_Minus();
            //  Bind_ReActive_Parameter_Close();
        }
    }

    protected void grdSecSalesMinus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            Sec_Code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SecSale dv = new SecSale();
            int iReturn = dv.DeActivate_SecondarySale(Sec_Code, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            FillSecSales();

            //Bind_ReActive_Parameter_Plus();
            Bind_ReActive_Parameter_Minus();
            // Bind_ReActive_Parameter_Close();
        }
    }

    protected void grdSecSalesOthers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            Sec_Code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SecSale dv = new SecSale();
            int iReturn = dv.DeActivate_SecondarySale(Sec_Code, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            FillSecSales();

            // Bind_ReActive_Parameter_Plus();
            // Bind_ReActive_Parameter_Minus();
            Bind_ReActive_Parameter_Close();
        }
    }


    protected void grdCol_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            Sec_Code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SecSale dv = new SecSale();
            int iReturn = dv.DeActivate_SecondarySale(Sec_Code, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            FillSecSales();

            // Bind_ReActive_Parameter_Plus();
            // Bind_ReActive_Parameter_Minus();
            Bind_ReActive_Parameter_UserCol();
        }
    }

    protected void grdCol_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCol.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdCol.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdCol_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCol.EditIndex = -1;
        FillSecSales();
    }

    protected void grdCol_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdCol.EditIndex = -1;
        Label lblSaleCode = (Label)grdCol.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdCol.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        TextBox txtShortName = (TextBox)grdCol.Rows[e.RowIndex].Cells[3].FindControl("txtShortName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim(), txtShortName.Text.Trim());
        FillSecSales();
    }

    protected void btnAddParam_Click(object sender, EventArgs e)
    {
        bool bIsValid = true;
        int iReturn = -1;

        //if (txtParamName.Text.Trim().Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Param Name should not be empty');</script>");
        //    bIsValid = false;
        //}
        //else if (txtParamName.Text.Trim().Length > 50)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Param Name should not exceed 50 characters');</script>");
        //    bIsValid = false;
        //}

        //if (txtShortName.Text.Trim().Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name should not be empty');</script>");
        //    bIsValid = false;
        //}
        //else if (txtShortName.Text.Trim().Length > 50)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name should not exceed 30 characters');</script>");
        //    bIsValid = false;
        //}

        if (bIsValid)
        {
            SecSale ss = new SecSale();
            bIsValid = ss.sParamRecordExist(div_code, txtParamName.Text.Trim());
            if (bIsValid)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Param already exists');</script>");
            }
            else
            {
                iReturn = ss.ParamRecordAdd(Session["div_code"].ToString(), txtParamName.Text.Trim(), txtShortName.Text.Trim(), ddlType.SelectedValue.ToString());

                if (iReturn > 0)
                {
                    txtParamName.Text = "";
                    txtShortName.Text = "";
                    ddlType.SelectedIndex = 0;
                    FillSecSales();
                }
            }
        }
    }


    protected void btnAdd_Param_Click(object sender, EventArgs e)
    {
        bool bIsValid = true;
        int iReturn = -1;

        if (bIsValid)
        {
            SecSale ss = new SecSale();
            bIsValid = ss.sParamRecordExist(div_code, txtParamName.Text.Trim());
            if (bIsValid)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Param already exists');</script>");
            }
            else
            {
                iReturn = ss.ParamRecordAdd(Session["div_code"].ToString(), txtParamName.Text.Trim(), txtShortName.Text.Trim(), ddlType.SelectedValue.ToString());

                if (iReturn > 0)
                {
                    txtParamName.Text = "";
                    txtShortName.Text = "";
                    ddlType.SelectedIndex = 0;
                    FillSecSales();
                }
            }
        }
    }

    protected void btnActive_Plus_Click(object sender, EventArgs e)
    {

        SecSale ss = new SecSale();
        int iReturn = ss.ReActivate_SSEntry_Parameter(Convert.ToInt32(ChkParam_Plus.SelectedValue), div_code, "+");

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Activated Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Activate.\');", true);
        }

        FillSecSales();

        ChkParam_Plus.ClearSelection();

    }

    protected void btnActive_Minus_Click(object sender, EventArgs e)
    {

        SecSale ss = new SecSale();
        int iReturn = ss.ReActivate_SSEntry_Parameter(Convert.ToInt32(ChkParam_Minus.SelectedValue), div_code, "-");

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Activated Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Activate.\');", true);
        }

        FillSecSales();

        ChkParam_Minus.ClearSelection();
    }

    protected void btnActive_Close_Click(object sender, EventArgs e)
    {
        SecSale ss = new SecSale();
        int iReturn = ss.ReActivate_SSEntry_Parameter(Convert.ToInt32(ChkParam_ParamClose.SelectedValue), div_code, "C");

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Activated Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Activate.\');", true);
        }

        FillSecSales();

        ChkParam_ParamClose.ClearSelection();
    }


    protected void btnActive_User_Column(object sender, EventArgs e)
    {
        SecSale ss = new SecSale();
        int iReturn = ss.ReActivate_SSEntry_Parameter(Convert.ToInt32(ChkParam_UserColumn.SelectedValue), div_code, "D");

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Activated Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Activate.\');", true);
        }

        FillSecSales();

        ChkParam_UserColumn.ClearSelection();
    }

    // Calculated Field Select

    protected void btnFieldAdd_plus_Click(object sender, EventArgs e)
    {

        int SecCode = Convert.ToInt32(hidSecSaleCode_plus.Value.Trim());

        SecSale ss = new SecSale();
        int iReturn = ss.FieldAdd_Parameter(SecCode, div_code, ChkParamList_plus.SelectedValue);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
        }

        FillSecSales();

        ChkParamList_plus.ClearSelection();

    }


    protected void btnFieldAdd_minus_Click(object sender, EventArgs e)
    {

        int SecCode = Convert.ToInt32(hidSecSaleCode_minus.Value.Trim());

        SecSale ss = new SecSale();
        int iReturn = ss.FieldAdd_Parameter(SecCode, div_code, ChkParamList_minus.SelectedValue);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
        }

        FillSecSales();

        ChkParamList_minus.ClearSelection();

    }


    protected void btnFieldAdd_Other_Click(object sender, EventArgs e)
    {

        int SecCode = Convert.ToInt32(hidSecSaleCode_Other.Value.Trim());

        SecSale ss = new SecSale();
        int iReturn = ss.FieldAdd_Parameter(SecCode, div_code, ChkParamList_Other.SelectedValue);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
        }

        FillSecSales();

        ChkParamList_Other.ClearSelection();

    }


    protected void btnFieldAdd_formula_Click(object sender, EventArgs e)
    {

        int SecCode = Convert.ToInt32(hidSecSaleCode_formula.Value.Trim());

        SecSale ss = new SecSale();
        int iReturn = ss.FieldAdd_Parameter(SecCode, div_code, ChkParamList_formula.SelectedValue);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
        }

        FillSecSales();

        ChkParamList_formula.ClearSelection();

    }

    // Primary  Field Select

    protected void btnPrimeAdd_Click(object sender, EventArgs e)
    {

        int SecCode = Convert.ToInt32(hidSecSaleCodePrime.Value.Trim());

        SecSale ss = new SecSale();
        int iReturn = ss.BindPrimary_Field(SecCode, div_code, ChkPrime.SelectedValue);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
        }
        else
        {
            // menu1.Status ="Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
        }

        FillSecSales();

        ChkPrime.ClearSelection();

    }

    //protected void btnBillAdd_Click(object sender, EventArgs e)
    //{

    //    //int SecCode = Convert.ToInt32(hidSecSaleCode_Bill.Value.Trim());        

    //    //SecSale ss = new SecSale();
    //    //int iReturn = ss.Update_PrimaryBill(SecCode, div_code, ChkParamList_Bill.SelectedValue);

    //    //if (iReturn > 0)
    //    //{
    //    //    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Added Successfully.\');", true);
    //    //}
    //    //else
    //    //{
    //    //    // menu1.Status ="Unable to Deactivate";
    //    //    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Add.\');", true);
    //    //}

    //    //FillSecSales();

    //    //ChkParamList_Bill.ClearSelection();

    //}


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Field_Col> GetParameter_Check(string objParam)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        List<Field_Col> objParamData = new List<Field_Col>();
        SecSale ss = new SecSale();
        DataSet dsParam = ss.GetClosingCheckParam(div_code, objParam.Trim());

        if (dsParam.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsParam.Tables[0].Rows)
            {
                Field_Col objData = new Field_Col();
                objData.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objData.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objData.Sec_Operator = dr["Sel_Sale_Operator"].ToString();
                objData.CalcF_Field = dr["CalcF_Field"].ToString();
                objParamData.Add(objData);
            }

        }

        return objParamData;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Field_Col> Get_PrimaryBill(string objBill)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        List<Field_Col> objParamData = new List<Field_Col>();
        SecSale ss = new SecSale();
        DataSet dsParam = ss.Get_PrimaryBill_Field(div_code, objBill.Trim());

        if (dsParam.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsParam.Tables[0].Rows)
            {
                Field_Col objData = new Field_Col();
                objData.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objData.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objData.Sec_Operator = dr["Sel_Sale_Operator"].ToString();
                objData.CalcF_Field = dr["Primary_Bill"].ToString();
                objParamData.Add(objData);
            }

        }

        return objParamData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static int BillField_Update(string objPrimary, string objSecSale)
    {
        int iReturn = 0;

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string BillField = objPrimary;
        int SecSaleCode =Convert.ToInt32(objSecSale.Trim());

        SecSale ss = new SecSale();

        iReturn = ss.Update_PrimaryBill(SecSaleCode, div_code, BillField);

        return iReturn;
    }
}

public class Field_Col
{
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }
    public string Sec_Operator { get; set; }
    public string CalcF_Field { get; set; }
}