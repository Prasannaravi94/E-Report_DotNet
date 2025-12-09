using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_MR_Territory_Territory_Weekly_Mapping : System.Web.UI.Page
{
    DataSet dsTerritory = null;
    string div_code=string.Empty;
    string sf_code = string.Empty;
    string terr_code = string.Empty;
    string weekname = string.Empty;
    string weekcode = string.Empty;
    string terr_category = string.Empty;
    string terr_name = string.Empty;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            getddlSF_Code();
            Territory_Weekbasis();

        }
    }
   
   
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                sf_code = ddlSFCode.SelectedValue.ToString();
                Session["sf_code_Temp"] = sf_code;
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Session["sf_code"] = ddlSFCode.SelectedValue.ToString();
            Territory_Weekbasis();
        }
        catch (Exception ex)
        {

        }
    }
    private void Territory_Weekbasis()
    {
        Territory terr = new Territory();
         dsTerritory = terr.get_Territory_WeekMap(ddlSFCode.SelectedValue);
        

        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
           
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
        //Territory_Weekbasis();
    }
    protected void grdTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritory.PageIndex = e.NewPageIndex;

    }
    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList weekname = (DropDownList)e.Row.FindControl("ddl_cat_type");
            if (weekname != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                weekname.SelectedIndex = weekname.Items.IndexOf(weekname.Items.FindByText(row["Category"].ToString()));
            }
        }
    }
    protected void btnsave_click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;

            foreach (GridViewRow row in grdTerritory.Rows)
            {

                Label lbl_Territory_Code = (Label)row.Cells[4].FindControl("lblTerritory_Code");
                terr_code = lbl_Territory_Code.Text.ToString();
                Label lblTerritory_Name = (Label)row.Cells[4].FindControl("lblTerritory_Name");
                terr_name = lblTerritory_Name.Text.ToString();
                Label lblType = (Label)row.Cells[4].FindControl("lblType");
                terr_category = lblType.Text.ToString();

                DropDownList ddl_Weekname = (DropDownList)row.Cells[4].FindControl("ddl_cat_type");
                weekname = ddl_Weekname.SelectedItem.Text.ToString();

                DropDownList ddl_Weekcode = (DropDownList)row.Cells[4].FindControl("ddl_cat_type");
                weekcode = ddl_Weekname.SelectedValue.ToString();

                Label lblTerritory_Code = (Label)row.FindControl("lblTerritory_Code");

                terr_code = lblTerritory_Code.Text;


                Territory terr = new Territory();
                iReturn = terr.Update_Territory_WeekMap(weekcode, weekname, ddlSFCode.SelectedValue, terr_code, div_code);
            }


            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                Territory_Weekbasis();
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
}