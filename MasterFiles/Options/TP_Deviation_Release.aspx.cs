using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_TP_Deviation_Release : System.Web.UI.Page
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
    DataSet dsAdminSetup = null;
    DataSet dsAdminSetup2 = null;
    string LockSystem = string.Empty;
    string LockSystem2 = string.Empty;
    DataSet dsLock = null;
    string strSf_Code = string.Empty;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        Sf_Code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillYear();

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                 c1.FindControl("btnBack").Visible = false;
                Fillall();

            }

            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                 c1.FindControl("btnBack").Visible = false;
                fillMgr();
            }
        }

        else
        {

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = this.Page.Title;
                 c1.FindControl("btnBack").Visible = false;
            }
            if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                      (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                 c1.FindControl("btnBack").Visible = false;
            }
        }
        hHeading.InnerText = Page.Title;
    }

    private void FillYear()
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
    }

    private void Fillall()
    {
        dsDCR = dc.get_Release_TPdeviation(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);

          if (dsDCR.Tables[0].Rows.Count > 0)
          {
             
              ddlFieldForce.DataTextField = "sf_name";
              ddlFieldForce.DataValueField = "sf_code";
              ddlFieldForce.DataSource = dsDCR;
              ddlFieldForce.DataBind();
          }
    }

    private void fillMgr()
    {
        SalesForce sf = new SalesForce();
        dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            //dsDCR.Tables[0].Rows[0].Delete();

            foreach (DataRow drFF in dsDCR.Tables[0].Rows)
            {
                string code = drFF["sf_code"].ToString();
                if (!code.Contains(Sf_Code))
                {
                    strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";
                }
            }

            strSf_Code = strSf_Code.Substring(0, strSf_Code.Length - 1);


            ViewState["strSf_Code"] = strSf_Code;
            // dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);
            dsDCR = dc.get_Release_TpDevMRR(strSf_Code, ddlMonth.SelectedValue, ddlYear.SelectedValue);

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                dsDCR.Tables[0].Rows[0].Delete();
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsDCR;
                ddlFieldForce.DataBind();


            }
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            Fillall();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            fillMgr();
        }

        grdRelease.Visible = false;   
        btnSubmit.Visible = false;
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            Fillall();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            fillMgr();
        }


        grdRelease.Visible = false;
        btnSubmit.Visible = false;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GetRelease();
    }

    private void GetRelease()
    {

        dsDCR = dc.getReleaseDate_Tpdeviation(ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
        
            btnSubmit.Visible = true;
            grdRelease.Visible = true;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
        else
        {
            grdRelease.Visible = true;
            btnSubmit.Visible = false;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        foreach (GridViewRow gridRow in grdRelease.Rows)
        {
            Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
            string lblsfcode = lblsf_code.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;
            Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
            TextBox txtreason = (TextBox)gridRow.FindControl("txtreason");
           

            if ((lblsfcode.Trim().Length > 0) && (bCheck == true))
            {
                DCR dcr = new DCR();
                iReturn = dcr.Update_Delayed_TPdeviation(lblsf_code.Text, Convert.ToDateTime(lblDate.Text), div_code, txtreason.Text);
            }

            if (iReturn > 0)
            {
          
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");

                grdRelease.Visible = false;
                btnSubmit.Visible = false;

            }
        }
    }
}