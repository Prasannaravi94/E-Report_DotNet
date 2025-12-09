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
                    TextBox txtfixed_amt1 = (TextBox)gridRow.FindControl("txtfixed_amt1");
                    TextBox txtfixed_amt2 = (TextBox)gridRow.FindControl("txtfixed_amt2");
                    TextBox txtfixed_amt3 = (TextBox)gridRow.FindControl("txtfixed_amt3");
                    TextBox txtfixed_amt4 = (TextBox)gridRow.FindControl("txtfixed_amt4");
                    TextBox txtfixed_amt5 = (TextBox)gridRow.FindControl("txtfixed_amt5");
                    HiddenField codeHidden = (HiddenField)gridRow.FindControl("code");
                    if (lblWorktype_Name.Text == "Field Work")
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type_FW");
                        iReturn = terr.WrkTypeBase_Expense_Update_MGR(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text), codeHidden.Value);
                    }
                    else
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                        iReturn = terr.WrkTypeBase_Expense_Update_MGR(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text), codeHidden.Value);
                    }
                }
            }
            else
            {
                foreach (GridViewRow gridRow in grdWTAllowance.Rows)
                {
                    Label lblWorktype_Name = (Label)gridRow.FindControl("lblWorktype_Name");
                    TextBox txtfixed_amt = (TextBox)gridRow.FindControl("txtfixed_amt");
                    TextBox txtfixed_amt1 = (TextBox)gridRow.FindControl("txtfixed_amt1");
                    TextBox txtfixed_amt2 = (TextBox)gridRow.FindControl("txtfixed_amt2");
                    TextBox txtfixed_amt3 = (TextBox)gridRow.FindControl("txtfixed_amt3");
                    TextBox txtfixed_amt4 = (TextBox)gridRow.FindControl("txtfixed_amt4");
                    TextBox txtfixed_amt5 = (TextBox)gridRow.FindControl("txtfixed_amt5");
                    HiddenField codeHidden = (HiddenField)gridRow.FindControl("code");
                    if (lblWorktype_Name.Text == "Field Work")
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type_FW");
                        iReturn = terr.WrkTypeBase_Expense_Update_MGR(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text), codeHidden.Value);
                    }
                    else
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                        iReturn = terr.WrkTypeBase_Expense_Update_MGR(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text), codeHidden.Value);
                    }
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
            TextBox txtAllow1 = ((TextBox)gridRow.FindControl("txtfixed_amt1"));
            TextBox txtAllow2 = ((TextBox)gridRow.FindControl("txtfixed_amt2"));
            TextBox txtAllow3 = ((TextBox)gridRow.FindControl("txtfixed_amt3"));
            TextBox txtAllow4 = ((TextBox)gridRow.FindControl("txtfixed_amt4"));
            TextBox txtAllow5 = ((TextBox)gridRow.FindControl("txtfixed_amt5"));

            if (frmBox.Text == "FA")
            {
                txtAllow.Visible = true;
                txtAllow1.Visible = true;
                txtAllow2.Visible = true;
                txtAllow3.Visible = true;
                txtAllow4.Visible = true;
                txtAllow5.Visible = true;
            }
            else
            {
                txtAllow.Visible = false;
                txtAllow1.Visible = false;
                txtAllow2.Visible = false;
                txtAllow3.Visible = false;
                txtAllow4.Visible = false;
                txtAllow5.Visible = false;
            }

        }
    }





    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView grdWTAllowance = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Work Type", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Allowance and Fare Type", "#5E5D8E", true);


            AddMergedCells(objgridviewrow, objtablecell, 3, "Metro", "#5E5D8E", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "HQ", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "EX", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "OS", "#A6A6D2", false);



            AddMergedCells(objgridviewrow, objtablecell, 3, "Non Metro", "#5E5D8E", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "HQ", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "EX", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "OS", "#A6A6D2", false);



            grdWTAllowance.Controls[0].Controls.AddAt(0, objgridviewrow);
            grdWTAllowance.Controls[0].Controls.AddAt(1, objgridviewrow1);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlDesignation.SelectedValue == "1" || ddlDesignation.SelectedValue == "2")
                {
                    Label lblwrktype = (Label)e.Row.FindControl("lblWorktype_Name");
                    DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
                    DropDownList Territory_Type_FW = (DropDownList)e.Row.FindControl("Territory_Type_FW");
                    if (lblwrktype.Text == "Field Work")
                    {
                        Territory_Type_FW.Visible = true;
                    }
                    else
                    {
                        Territory_Type.Visible = true;
                    }
                    if (Territory_Type != null)
                    {
                        DataRowView row = (DataRowView)e.Row.DataItem;
                        Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Expense_Type"].ToString()));
                        Territory_Type_FW.SelectedIndex = Territory_Type_FW.Items.IndexOf(Territory_Type_FW.Items.FindByText(row["Expense_Type"].ToString()));
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