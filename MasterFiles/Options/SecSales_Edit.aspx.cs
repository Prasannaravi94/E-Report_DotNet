using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Options_SecSales_Edit : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsYear = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSecSale = new DataSet();
    DataSet dsSalesforce = new DataSet();
    int iErrReturn = -1;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {
            //Populate Year dropdown
            FillYear();

            //Populate MR dropdown as per sf_code
            FillMR();

            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
        hHeading.InnerText = Page.Title;
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillStockiest();
    }

    //Populate the Year dropdown
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division
            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                }
            }
            //ddlYear.SelectedIndex = 0;
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.getSecSales_MR(div_code, sf_code);

        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();


        }
    }

    private void FillStockiest()
    {

        string sub_code = "";

        string S_Code = ddlFieldForce.SelectedValue.ToString().Trim();

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(S_Code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        SecSale ss = new SecSale();
        //dsSecSale = ss.getStockiestDet(ddlFieldForce.SelectedValue.ToString().Trim());
        dsSecSale = ss.Get_SS_Stockiest_Details(div_code, ddlFieldForce.SelectedValue.ToString().Trim(), Convert.ToInt16(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()), sub_code);
        if (dsSecSale != null)
        {
            if (dsSecSale.Tables[0].Rows.Count > 0)
            {
                grdSecSales.DataSource = dsSecSale;
                grdSecSales.DataBind();

                btnSubmit.Visible = true;
            }
            else
            {
                grdSecSales.DataSource = null;
                grdSecSales.DataBind();

                btnSubmit.Visible = false;
            }
        }
        else
        {
            grdSecSales.DataSource = null;
            grdSecSales.DataBind();

            btnSubmit.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            string sub_code = "";

            string S_Code = ddlFieldForce.SelectedValue.ToString().Trim();

            SubDivision sb = new SubDivision();
            DataSet dsSub = sb.getSub_sf(S_Code);
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }


            int iReturn = -1;
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                Label lblStockiestCode = (Label)gridRow.Cells[1].FindControl("lblStockiestCode");
                CheckBox chkSaleEntry = (CheckBox)gridRow.Cells[3].FindControl("chkSaleEntry");
                if (chkSaleEntry.Checked)
                {
                    SecSale ss = new SecSale();
                    iReturn = ss.RecordUpdate(div_code, ddlFieldForce.SelectedValue.ToString().Trim(), Convert.ToInt32(lblStockiestCode.Text.Trim()), Convert.ToInt32(MonthVal.ToString().Trim()), Convert.ToInt32(YearVal.ToString().Trim()), Session["sf_code"].ToString().Trim(), 4, sub_code);
                }
            }

            if (iReturn > 0)
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sales Edit request(s) have been created successfully');</script>");

            FillStockiest();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Option Edit", "btnSubmit_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }
}