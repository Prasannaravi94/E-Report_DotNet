using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_WrkTypeWise_Allowance : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();

            if (!Page.IsPostBack)
            {
                menu1.Title = Page.Title;
                //// menu1.FindControl("btnBack").Visible = false;
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }

    }

    private void FillBaseLevel()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.GetExpenseBaseWorkType(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdWTAllowance.DataSource = dsSalesForce;
            grdWTAllowance.DataBind();
            lblSelect.Visible = false;
            btnSave.Visible = true;
        }
    }

    private void FillMGRLevel()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.GetExpenseMGRWorkType(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdWTAllowance.DataSource = dsSalesForce;
            grdWTAllowance.DataBind();
            lblSelect.Visible = false;
            btnSave.Visible = true;
        }
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDesignation.SelectedItem.Value == "1")
            {
                FillBaseLevel();
            }
            else
            {
                FillMGRLevel();
            }

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;
            Territory terr = new Territory();
            if (ddlDesignation.SelectedValue == "1")
            {
                foreach (GridViewRow gridRow in grdWTAllowance.Rows)
                {
                    Label lblWorktype_Name = (Label)gridRow.FindControl("lblWorktype_Name");
                    TextBox txtfixed_amt = (TextBox)gridRow.FindControl("txtfixed_amt");
                    DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                    HiddenField codeHidden = (HiddenField)gridRow.FindControl("code");
                    iReturn = terr.WrkTypeBase_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), codeHidden.Value);
                }
            }
            else
            {
                foreach (GridViewRow gridRow in grdWTAllowance.Rows)
                {
                    Label lblWorktype_Name = (Label)gridRow.FindControl("lblWorktype_Name");
                    DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");

                    iReturn = terr.WrkTypeMGR_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text,div_code,ddlDesignation.SelectedValue,ddlAllowanceType.SelectedValue);
                }
            }

            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        {
            DropDownList frmBox = ((DropDownList)gridRow.FindControl("Territory_Type"));
            TextBox txtAllow = ((TextBox)gridRow.FindControl("txtfixed_amt"));

            if (frmBox.Text == "FA")
            {
                txtAllow.Visible = true;
            }
            else
            {
                txtAllow.Visible = false;
            }

        }
    }

    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlDesignation.SelectedValue == "1")
                {
                    DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
                    if (Territory_Type != null)
                    {
                        DataRowView row = (DataRowView)e.Row.DataItem;
                        Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Expense_Type"].ToString()));
                    }
                }
                else
                {
                    DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
                    if (Territory_Type != null)
                    {
                        DataRowView row = (DataRowView)e.Row.DataItem;
                        Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Expense_Type"].ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

  
}