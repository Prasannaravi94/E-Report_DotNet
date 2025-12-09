using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Options_Session_Release : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsDCR = null;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;
    DataSet dsTP = null;
    DataSet dsper = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
       // hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
                DateTime FromMonth = DateTime.Now;
                txtMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            }

            FillSalesForce();
            menu1.Title = this.Page.Title;
            hHeading.InnerText = Page.Title;
        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.ReleaseSession(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[0].Delete();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillPermiss();
        btnSubmit.Visible = true;
    }

    private void FillPermiss()
    {
        SalesForce sf = new SalesForce();

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        dsper = sf.ReleaseSssionTime(ddlFieldForce.SelectedValue, MonthVal.ToString(), YearVal.ToString());

        if (dsper.Tables[0].Rows.Count > 0)
        {
            grdPermission.Visible = true;
            grdPermission.DataSource = dsper;
            grdPermission.DataBind();
        }
        else
        {
            grdPermission.DataSource = dsper;
            grdPermission.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdPermission.Rows)
        {
            Label lblid = (Label)gridRow.Cells[0].FindControl("lblid");
            string LabelID= lblid.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkDate");
            bool bCheck = chkRelease.Checked;
            Label lblsf_code = (Label)gridRow.Cells[2].FindControl("lblsf_code");
            string lbSF = lblsf_code.Text.ToString();
            if ((LabelID.Trim().Length > 0) && (bCheck == true))
            {
                AdminSetup dcr = new AdminSetup();
                iReturn = dcr.RecordUpdateSession(lbSF, LabelID);
            }
        }

        if (iReturn > 0)
        {
           
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            FillPermiss();
          
        }
    }
}