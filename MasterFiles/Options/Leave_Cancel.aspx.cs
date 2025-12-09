using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Options_Leave_Cancel : System.Web.UI.Page
{
    DataSet dsadmin = null;
    DataSet dsState = null;
    DataSet dsVisit = null;
    string sState = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string[] statecd;
    DataSet dsadm = null;
    DataSet dsTP = null;
    DataSet dsDivision = null;
    DataSet dsDCR = null;
    DataSet dsDCR_New = null;
    DCR dc = new DCR();
    string sReturn = string.Empty;
    string div_code = string.Empty;
    string Sf_Code = string.Empty;
    DataSet dsLeave = null;
    DataSet dsLeave2 = null;
    string LockSystem = string.Empty;
    string LockSystem2 = string.Empty;
    DataSet dsLock = null;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Sf_Code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
           
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
            fillleave_fieldname();
        
        }

        if (Session["sf_type"].ToString() != "1")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            //c1.FindControl("btnBack").Visible = false;
        }
        else
        {
            UserControl_MR_Menu c2 =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c2);
            c2.Title = this.Page.Title;
            c2.FindControl("btnBack").Visible = false;
        }
        fillcolor();
        hHeading.InnerText = Page.Title;
    }

    private void fillcolor()
    {

        foreach (ListItem item in ddlFieldForce.Items)
        {
            if (item.Value == "0")
            {
                item.Attributes.Add("style", "background-color:" + "yellow"); ;
            }

        }
    }

    private void fillleave_fieldname()
    {
        DCR dc = new DCR();
        dsLeave = dc.getleave_fieldName(div_code,ddlMonth.SelectedValue, ddlYear.SelectedValue);

        if (dsLeave.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsLeave;
            ddlFieldForce.DataBind();
        }
    }

    private void fillgrid()
    {
        DCR dcr = new DCR();
        dsLeave2 = dcr.getleave_dates(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue,ddlFieldForce.SelectedValue);
        if (dsLeave2.Tables[0].Rows.Count > 0)
        {
            grdRelease.Visible = true;
            grdRelease.DataSource = dsLeave2;
            grdRelease.DataBind();
        }
        else
        {
            grdRelease.DataSource = dsLeave2;
            grdRelease.DataBind();
        }
        
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillleave_fieldname();
        grdRelease.Visible = false;
        btnSubmit.Visible = false;
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillleave_fieldname();
        grdRelease.Visible = false;
        btnSubmit.Visible = false;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
        btnSubmit.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdRelease.Rows)
        {
            Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
            string lblsfcode = lblsf_code.Text.ToString();
            Label lblleave_id = (Label)gridRow.Cells[0].FindControl("lblleave_id");
            string lblleaveid = lblleave_id.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;
            Label lblfrom_date = (Label)gridRow.Cells[2].FindControl("lblfrom_date");
            Label lblto_date = (Label)gridRow.Cells[2].FindControl("lblto_date");
            Label lblleave_type = (Label)gridRow.FindControl("lblleave_type");
            Label lblno_days = (Label)gridRow.FindControl("lblno_days");

            if ((lblsfcode.Trim().Length > 0) && (lblleaveid.Trim().Length > 0) && (bCheck == true))
            {
                DCR dcr = new DCR();
                iReturn = dcr.Update_LeaveCancel(lblsf_code.Text, Convert.ToDateTime(lblfrom_date.Text), Convert.ToDateTime(lblto_date.Text), Convert.ToInt32(lblleaveid), div_code, lblleave_type.Text, lblno_days.Text);
            }
            if (iReturn > 0)
            {
                //Response.Write("DCR Edit Dates have been created successfully");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Cancelled successfully');</script>");
                fillgrid();

            }
        }
    }
}