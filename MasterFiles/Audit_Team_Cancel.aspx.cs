using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Audit_Team_Cancel : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    SalesForce sf = new SalesForce();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "Audit_Team.aspx";
        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillSalesForce();
        }

    }
    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void FillSalesForce()
    {
       
        dsSalesForce = sf.FillAudit_Team_List(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();

        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSF_Code = (Label)gridRow.Cells[0].FindControl("lblSF_Code");
            string lblSFCode = lblSF_Code.Text.ToString();
            Label lblAudit_Id = (Label)gridRow.Cells[0].FindControl("lblAudit_Id");
            string lblAuditId = lblAudit_Id.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;
           

            if ((lblSFCode.Trim().Length > 0) && (lblAuditId.Trim().Length > 0) && (bCheck == true))
            {
               
                iReturn = sf.Delete_Audit_Id(lblSF_Code.Text, lblAudit_Id.Text);
            }
            if (iReturn > 0)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Audit ID Deleted Successfully');</script>");
                FillSalesForce();

            }
        }
    }
}