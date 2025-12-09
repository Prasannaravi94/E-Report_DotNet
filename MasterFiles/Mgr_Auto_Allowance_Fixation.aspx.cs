using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Mgr_Auto_Allowance_Fixation : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = new DataSet();
    DataSet dsSubDivision = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillFieldForcediv(div_code);
            //FillFieldforce();
            lblSelect.Visible = true;

        }
        FillColor();

    }

    //protected void OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gridRow in grdMgrAllowance.Rows)
    //    {
    //        DropDownList frmBox = ((DropDownList)gridRow.FindControl("Territory_Type"));
    //        TextBox dist = (TextBox)gridRow.FindControl("txtdist");

    //        if (frmBox.Text == "ACT")
    //        {
    //            dist.Visible = false;
    //        }
    //        else
    //        {
    //            dist.Visible = true;
    //        }

    //    }
    //}


    private void FillFieldForcediv(string div_code)
    {
        SalesForce dv = new SalesForce();
        dsSubDivision = dv.SalesForceListMgrGet(div_code, "admin");
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            dsSubDivision.Tables[0].Rows[0].Delete();
            // dsSubDivision.Tables[0].Rows[1].Delete();

            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();

        }
    }
    private void FillColor()
    {
        int j = 0;
        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlSubdiv.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillFieldforce()
    {
        SalesForce sf = new SalesForce();
        string str = "";
        if (ddlSubdiv.SelectedValue.ToString() == "0")
        {
            str = "";
        }
        else
        {
            str = ddlSubdiv.SelectedValue.ToString();
        }
        dsSalesForce = sf.SalesForceList_Allow(div_code, str);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdMgrAllowance.DataSource = dsSalesForce;
            grdMgrAllowance.DataBind();
            lblSelect.Visible = false;
            btnSave.Visible = true;
        }
        else
        {
            grdMgrAllowance.DataSource = dsSalesForce;
            grdMgrAllowance.DataBind();
            lblSelect.Visible = true;
            btnSave.Visible = false;
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {

            FillFieldforce();


        }
        catch (Exception ex)
        {

        }

    }
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{


    //    FillFieldForcediv(div_code);
    //    ddlSubdiv.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btngo.Visible = true;
    //    FillColor();
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;
            Territory terr = new Territory();
            foreach (GridViewRow gridRow in grdMgrAllowance.Rows)
            {
                Label lblField_Name = (Label)gridRow.FindControl("lblField_Name");
                DropDownList ddlAllowanceType = (DropDownList)gridRow.FindControl("Territory_Type");
                HiddenField hidcode = (HiddenField)gridRow.FindControl("hidcode");
                TextBox Dist = (TextBox)gridRow.FindControl("txtdist");

                if (ddlAllowanceType.SelectedValue != "0")
                {
                    iReturn = terr.WrkMGR_Expense_Update(hidcode.Value, div_code, ddlAllowanceType.SelectedValue, ddlAllowanceType.SelectedItem.Text, ddlSubdiv.SelectedValue, Dist.Text);
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
    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");

                if (Territory_Type != null)
                {
                    DataRowView row = (DataRowView)e.Row.DataItem;
                    Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["type_name"].ToString()));
                    TextBox dist = (TextBox)e.Row.FindControl("txtdist");
                    if (Territory_Type.SelectedValue.ToString() == "ACT")
                    {
                        //dist.Visible = false;
                        dist.Style.Add("display", "none");
                    }
                    else
                    {
                        //dist.Visible = true;
                        dist.Style.Add("display", "block");

                    }

                }
            }
        }

        catch (Exception ex)
        {

        }
    }

}