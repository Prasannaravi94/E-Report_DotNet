using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Options_frmDCRLeaveAddOption : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();


            FillSalesForce();
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
        }

    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getSalesForcelist_New(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    protected void btnSub_Click(object sender, System.EventArgs e)
    {
        DCR dcr = new DCR();
        int iReturn = -1;

        iReturn = dcr.DcrDelLeaveOption(ddlFieldForce.SelectedValue, txtFromdte.Text, txtTodte.Text);

        if (iReturn > 0)
        {
            //Response.Write("DCR Edit Dates have been created successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Leave Dates Deleted successfully');</script>");
        }
    }
}