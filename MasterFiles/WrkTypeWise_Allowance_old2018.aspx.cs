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
                // menu1.FindControl("btnBack").Visible = false;
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
                    TextBox txtfixed_amt6 = (TextBox)gridRow.FindControl("txtfixed_amt6");
                    TextBox txtfixed_amt7 = (TextBox)gridRow.FindControl("txtfixed_amt7");
                    TextBox txtfixed_amt8 = (TextBox)gridRow.FindControl("txtfixed_amt8");
                    TextBox txtfixed_amt9 = (TextBox)gridRow.FindControl("txtfixed_amt9");
                    TextBox txtfixed_amt10 = (TextBox)gridRow.FindControl("txtfixed_amt10");
                    TextBox txtfixed_amt11 = (TextBox)gridRow.FindControl("txtfixed_amt11");
                    TextBox txtfixed_amt12 = (TextBox)gridRow.FindControl("txtfixed_amt12");
                    TextBox txtfixed_amt13 = (TextBox)gridRow.FindControl("txtfixed_amt13");
                    TextBox txtfixed_amt14 = (TextBox)gridRow.FindControl("txtfixed_amt14");
                    TextBox txtfixed_amt15 = (TextBox)gridRow.FindControl("txtfixed_amt15");
                    TextBox txtfixed_amt16 = (TextBox)gridRow.FindControl("txtfixed_amt16");
                    TextBox txtfixed_amt17 = (TextBox)gridRow.FindControl("txtfixed_amt17");
                    HiddenField codeHidden = (HiddenField)gridRow.FindControl("code");
                    if (lblWorktype_Name.Text == "Field Work")
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type_FW");
                        iReturn = terr.WrkTypeBase_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text),
                             (txtfixed_amt6.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt6.Text),
                            (txtfixed_amt7.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt7.Text),
                            (txtfixed_amt8.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt8.Text),
                            (txtfixed_amt9.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt9.Text),
                            (txtfixed_amt10.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt10.Text),
                            (txtfixed_amt11.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt11.Text),
                            (txtfixed_amt12.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt12.Text),
                            (txtfixed_amt13.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt13.Text),
                            (txtfixed_amt14.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt14.Text),
                            (txtfixed_amt15.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt15.Text),
                            (txtfixed_amt16.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt16.Text),
                            (txtfixed_amt17.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt17.Text),
                            codeHidden.Value);
                    }
                    else
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                        iReturn = terr.WrkTypeBase_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text),
                             (txtfixed_amt6.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt6.Text),
                            (txtfixed_amt7.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt7.Text),
                            (txtfixed_amt8.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt8.Text),
                            (txtfixed_amt9.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt9.Text),
                            (txtfixed_amt10.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt10.Text),
                            (txtfixed_amt11.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt11.Text),
                            (txtfixed_amt12.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt12.Text),
                            (txtfixed_amt13.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt13.Text),
                            (txtfixed_amt14.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt14.Text),
                            (txtfixed_amt15.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt15.Text),
                            (txtfixed_amt16.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt16.Text),
                            (txtfixed_amt17.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt17.Text),
                            codeHidden.Value);
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
                    TextBox txtfixed_amt6 = (TextBox)gridRow.FindControl("txtfixed_amt6");
                    TextBox txtfixed_amt7 = (TextBox)gridRow.FindControl("txtfixed_amt7");
                    TextBox txtfixed_amt8 = (TextBox)gridRow.FindControl("txtfixed_amt8");
                    TextBox txtfixed_amt9 = (TextBox)gridRow.FindControl("txtfixed_amt9");
                    TextBox txtfixed_amt10 = (TextBox)gridRow.FindControl("txtfixed_amt10");
                    TextBox txtfixed_amt11 = (TextBox)gridRow.FindControl("txtfixed_amt11");
                    TextBox txtfixed_amt12 = (TextBox)gridRow.FindControl("txtfixed_amt12");
                    TextBox txtfixed_amt13 = (TextBox)gridRow.FindControl("txtfixed_amt13");
                    TextBox txtfixed_amt14 = (TextBox)gridRow.FindControl("txtfixed_amt14");
                    TextBox txtfixed_amt15 = (TextBox)gridRow.FindControl("txtfixed_amt15");
                    TextBox txtfixed_amt16 = (TextBox)gridRow.FindControl("txtfixed_amt16");
                    TextBox txtfixed_amt17 = (TextBox)gridRow.FindControl("txtfixed_amt17");
                    HiddenField codeHidden = (HiddenField)gridRow.FindControl("code");
                    if (lblWorktype_Name.Text == "Field Work")
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type_FW");
                        iReturn = terr.WrkTypeBase_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text), (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text),
                            (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text),
                            (txtfixed_amt6.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt6.Text),
                            (txtfixed_amt7.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt7.Text),
                            (txtfixed_amt8.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt8.Text),
                            (txtfixed_amt9.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt9.Text),
                            (txtfixed_amt10.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt10.Text),
                            (txtfixed_amt11.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt11.Text),
                            (txtfixed_amt12.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt12.Text),
                            (txtfixed_amt13.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt13.Text),
                            (txtfixed_amt14.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt14.Text),
                            (txtfixed_amt15.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt15.Text),
                            (txtfixed_amt16.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt16.Text),
                            (txtfixed_amt17.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt17.Text),
                            codeHidden.Value);
                    }
                    else
                    {
                        DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                        iReturn = terr.WrkTypeBase_Expense_Update(lblWorktype_Name.Text, ddlAllowanceType.SelectedItem.Text, div_code, ddlDesignation.SelectedValue, ddlAllowanceType.SelectedValue, (txtfixed_amt.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt.Text), (txtfixed_amt1.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt1.Text), (txtfixed_amt2.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt2.Text), (txtfixed_amt3.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt3.Text),
                            (txtfixed_amt4.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt4.Text), 
                            (txtfixed_amt5.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt5.Text),
                            (txtfixed_amt6.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt6.Text),
                            (txtfixed_amt7.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt7.Text),
                            (txtfixed_amt8.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt8.Text),
                            (txtfixed_amt9.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt9.Text),
                            (txtfixed_amt10.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt10.Text),
                            (txtfixed_amt11.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt11.Text),
                            (txtfixed_amt12.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt12.Text),
                            (txtfixed_amt13.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt13.Text),
                            (txtfixed_amt14.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt14.Text),
                            (txtfixed_amt15.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt15.Text),
                            (txtfixed_amt16.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt16.Text),
                            (txtfixed_amt17.Text == "" || ddlAllowanceType.SelectedValue != "FA") ? 0 : Convert.ToDouble(txtfixed_amt17.Text),
                            codeHidden.Value);
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
            TextBox txtfixed_amt6 = (TextBox)gridRow.FindControl("txtfixed_amt6");
            TextBox txtfixed_amt7 = (TextBox)gridRow.FindControl("txtfixed_amt7");
            TextBox txtfixed_amt8 = (TextBox)gridRow.FindControl("txtfixed_amt8");
            TextBox txtfixed_amt9 = (TextBox)gridRow.FindControl("txtfixed_amt9");
            TextBox txtfixed_amt10 = (TextBox)gridRow.FindControl("txtfixed_amt10");
            TextBox txtfixed_amt11 = (TextBox)gridRow.FindControl("txtfixed_amt11");
            TextBox txtfixed_amt12 = (TextBox)gridRow.FindControl("txtfixed_amt12");
            TextBox txtfixed_amt13 = (TextBox)gridRow.FindControl("txtfixed_amt13");
            TextBox txtfixed_amt14 = (TextBox)gridRow.FindControl("txtfixed_amt14");
            TextBox txtfixed_amt15 = (TextBox)gridRow.FindControl("txtfixed_amt15");
            TextBox txtfixed_amt16 = (TextBox)gridRow.FindControl("txtfixed_amt16");
            TextBox txtfixed_amt17 = (TextBox)gridRow.FindControl("txtfixed_amt17");

            if (frmBox.Text == "FA")
            {
                txtAllow.Visible = true;
                txtAllow1.Visible = true;
                txtAllow2.Visible = true;
                txtAllow3.Visible = true;
                txtAllow4.Visible = true;
                txtAllow5.Visible = true;
                txtfixed_amt6.Visible = true;
                txtfixed_amt7.Visible=true;
                txtfixed_amt8.Visible=true;
                txtfixed_amt9.Visible=true;
                txtfixed_amt10.Visible=true;
                txtfixed_amt11.Visible=true;
                txtfixed_amt12.Visible=true;
                txtfixed_amt13.Visible=true;
                txtfixed_amt14.Visible = true;
                txtfixed_amt15.Visible = true;
                txtfixed_amt16.Visible = true;
                txtfixed_amt17.Visible = true;


            }
            else
            {
                txtAllow.Visible = false;
                txtAllow1.Visible = false;
                txtAllow2.Visible = false;
                txtAllow3.Visible = false;
                txtAllow4.Visible = false;
                txtAllow5.Visible = false;
                txtfixed_amt6.Visible = false;
                txtfixed_amt7.Visible = false;
                txtfixed_amt8.Visible = false;
                txtfixed_amt9.Visible = false;
                txtfixed_amt10.Visible = false;
                txtfixed_amt11.Visible = false;
                txtfixed_amt12.Visible = false;
                txtfixed_amt13.Visible = false;
                txtfixed_amt14.Visible = false;
                txtfixed_amt15.Visible = false;
                txtfixed_amt16.Visible = false;
                txtfixed_amt17.Visible = false;
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
            GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Work Type", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Allowance and Fare Type", "#5E5D8E", true);


            AddMergedCells(objgridviewrow, objtablecell, 9, "Metro", "#5E5D8E", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "Confirmed", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "Trainee", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "probation", "#A6A6D2", false);
            
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);



            AddMergedCells(objgridviewrow, objtablecell, 9, "Non Metro", "#5E5D8E", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "Confirmed", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "Trainee", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 3, "probation", "#A6A6D2", false);
            
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "HQ", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "EX", "#98AFC7", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "OS", "#98AFC7", false);



            grdWTAllowance.Controls[0].Controls.AddAt(0, objgridviewrow);
            grdWTAllowance.Controls[0].Controls.AddAt(1, objgridviewrow1);
            grdWTAllowance.Controls[0].Controls.AddAt(2, objgridviewrow2);
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
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