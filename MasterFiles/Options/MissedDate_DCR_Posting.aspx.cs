using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_MissedDate_DCR_Posting : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DCR sf = new DCR();
    DataSet dsTP = null;
    DataSet dsTotal = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "DCREdit.aspx";
        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;
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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

        }
        grdMissedDCR.Visible = false;
        btnSubmit.Visible = false;

    }
    protected void grdMissedDCR_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void FillMissedDCR()
    {

        dsSalesForce = sf.FillMissed_DCR_Date(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdMissedDCR.Visible = true;
            grdMissedDCR.DataSource = dsSalesForce;
            grdMissedDCR.DataBind();

            FillTotalCount();

        }
        else
        {
            grdMissedDCR.DataSource = dsSalesForce;
            grdMissedDCR.DataBind();

        }

    }

    private void FillTotalCount()
    {
        dsTotal = sf.FillTotal_SfCount(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsTotal.Tables[0].Rows.Count > 0)
        {
            lblsf_Count.Visible = true;
            Label1.Visible = true;
            Label2.Visible = true;
            lblsf_Count.Text = dsTotal.Tables[0].Rows[0]["Total"].ToString();
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdMissedDCR.Rows)
        {
            Label lblSF_Code = (Label)gridRow.Cells[0].FindControl("lblSF_Code");
            string lblSFCode = lblSF_Code.Text.ToString();
            Label lblTrans_SlNo = (Label)gridRow.Cells[0].FindControl("lblTrans_SlNo");
            string lblTransSlNo = lblTrans_SlNo.Text.ToString();
            Label lblDate = (Label)gridRow.Cells[0].FindControl("lblDate");
            string Date = lblDate.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;


            if ((lblSFCode.Trim().Length > 0) && (lblTransSlNo.Trim().Length > 0) && (bCheck == true))
            {

                iReturn = sf.Insert_Deleted_DCR(lblSF_Code.Text, lblTrans_SlNo.Text, Convert.ToDateTime(Date));
               
            }
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Moved Successfully');</script>");
                //FillMissedDCR();

            }
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillMissedDCR();
        grdMissedDCR.Visible = true;
        btnSubmit.Visible = true;
    }
}