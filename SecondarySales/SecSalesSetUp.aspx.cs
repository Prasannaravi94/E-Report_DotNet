using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class SecondarySales_SecSalesSetUp : System.Web.UI.Page
{

    #region "Variable Declarations"
    DataSet dsSale = null;
    string div_code = string.Empty;
    string strError = string.Empty;
    int iErrReturn = -1;
    int total_needed = -1;
    int value_needed = -1;
    int approval_needed = -1;
    string calc_rate = string.Empty;
    string prod_grp = string.Empty;
    string Sale_Field = string.Empty;
    string Closing_Field = string.Empty;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            //btnSubmit.Visible = false;
            //btnClear.Visible = false;
            FillSecSales();
            FillSaleField();
            FillClosingField();
            PopulateSecSales();
        }
    }

    private void FillSecSales()
    {
        try
        {
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                chklstSecSale.DataValueField = "Sec_Sale_Code";
                chklstSecSale.DataTextField = "Sec_Sale_Name";
                chklstSecSale.DataSource = dsSale;
                chklstSecSale.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }


    private void FillSaleField()
    {
        try
        {
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                chkSaleField.DataValueField = "Sec_Sale_Code";
                chkSaleField.DataTextField = "Sec_Sale_Name";
                chkSaleField.DataSource = dsSale;
                chkSaleField.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void FillClosingField()
    {
        try
        {
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                chkClosingField.DataValueField = "Sec_Sale_Code";
                chkClosingField.DataTextField = "Sec_Sale_Name";
                chkClosingField.DataSource = dsSale;
                chkClosingField.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void PopulateSecSales()
    {
        int cur_ind = 0;
        try
        {
            int secsalecd = -1;
            SecSale ss = new SecSale();
            dsSale = ss.getAddionalSaleMaster(div_code);
            //dsSale = ss.Get_SecSaleCode_TotalField(div_code);
            //if (dsSale.Tables[0].Rows.Count > 0)
            //{
            //    if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(0) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim().Length > 0))
            //        secsalecd = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            //}

            //dsSale = ss.getSaleSetup(Convert.ToInt16(div_code), secsalecd);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(0) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim().Length > 0))
                    total_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(1) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim().Length > 0))
                    value_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                calc_rate = dsSale.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(3) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim().Length > 0))
                    approval_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());

                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(4) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim().Length > 0))
                    prod_grp = dsSale.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(5) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim().Length > 0))
                     Sale_Field= dsSale.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(6) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(6).ToString().Trim().Length > 0))
                    Closing_Field = dsSale.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                rdoTotalNeeded.SelectedValue = total_needed.ToString().Trim();
                rdoValueNeeded.SelectedValue = value_needed.ToString().Trim();
                rdoApproval.SelectedValue = approval_needed.ToString().Trim();
                rdoSale.SelectedValue = calc_rate.Trim();
                rdoProd.SelectedValue = prod_grp.Trim();

                string[] arr = Sale_Field.Split(',');

                for (int i = 0; i < arr.Length; i++)
                {
                   // chkSaleField.SelectedValue = arr[i];

                    if (arr[i] != "")
                    {
                        chkSaleField.Items.FindByValue(arr[i]).Selected = true;
                    }
                }

                string[] arrClosing = Closing_Field.Split(',');

                for (int i = 0; i < arrClosing.Length; i++)
                {
                    // chkSaleField.SelectedValue = arr[i];

                    if (arrClosing[i] != "")
                    {
                        chkClosingField.Items.FindByValue(arrClosing[i]).Selected = true;
                    }
                }
            }

            dsSale = ss.getrptfield(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in dsSale.Tables[0].Rows)
                {
                    for (cur_ind = 0; cur_ind < chklstSecSale.Items.Count; cur_ind++)
                    {
                        if (dRow["Sec_Sale_Code"].ToString().Trim() == chklstSecSale.Items[cur_ind].Value.Trim())
                            chklstSecSale.Items[cur_ind].Selected = true;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iRet = -1;
        int i = 0;
        int sec_sale_code = -1;

        total_needed = Convert.ToInt16(rdoTotalNeeded.SelectedValue.ToString()); //1; // 
        value_needed = Convert.ToInt16(rdoValueNeeded.SelectedValue.ToString()); //1; // 
        calc_rate = rdoSale.SelectedValue.ToString();
        approval_needed = Convert.ToInt16(rdoApproval.SelectedValue.ToString());
        prod_grp = rdoProd.SelectedValue.ToString();

        if (total_needed.ToString().Trim().Length == 0)
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please select option for Total');</script>");

        if (value_needed.ToString().Trim().Length == 0)
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please select option for Value');</script>");

        if (approval_needed.ToString().Trim().Length == 0)
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please select option for Approval');</script>");

        if (calc_rate.Trim().Length == 0)
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please select option for Rate');</script>");

        if (prod_grp.Trim().Length == 0)
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please select option for Product Group');</script>");

        SecSale ss = new SecSale();

        // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
        bool bRecordExist = ss.SetupRecordExist(div_code.Trim());


        string SaleField = string.Empty;
        string ClosingField = string.Empty;

        for (i = 0; i < chkSaleField.Items.Count; i++)
        {
            string ChkCode = (chkSaleField.Items[i].Value.ToString());

            if (chkSaleField.Items[i].Selected == true)
            {
                SaleField = SaleField + ChkCode + ",";

            }

        }


        for (i = 0; i < chkClosingField.Items.Count; i++)
        {
            string ChkCode = (chkClosingField.Items[i].Value.ToString());

            if (chkClosingField.Items[i].Selected == true)
            {
                ClosingField = ClosingField + ChkCode + ",";

            }

        }

        iRet = ss.RecordAdd(div_code, total_needed, value_needed, calc_rate, approval_needed, prod_grp, bRecordExist, SaleField, ClosingField);

        iRet = ss.RecordAdd_TotalValue_Needed(div_code, total_needed, value_needed);


        if (iRet > 0)
        {
            int ReportField;

            for (i = 0; i < chklstSecSale.Items.Count; i++)
            {
                sec_sale_code = Convert.ToInt16(chklstSecSale.Items[i].Value.ToString());

                if (chklstSecSale.Items[i].Selected == true)
                {
                    ReportField = 0;
                    iRet = ss.IsReportField(div_code, sec_sale_code, ReportField);
                }
                else
                {
                    ReportField = 1;
                    iRet = ss.IsReportField(div_code, sec_sale_code, ReportField);
                }
            }

            if (iRet > 0)
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Secondary Sale Additional Setup has been updated Successfully');</script>");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}