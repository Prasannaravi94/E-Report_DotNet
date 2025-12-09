using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class MasterFiles_ActivityReport : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSf = null;
    DataSet dsActivityUpload = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                BindDate();
                FillManagers();
                ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
                ddlFieldForce.Enabled = false;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                FillMRManagers1();
                BindDate();
                 
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                FillManagers();
                BindDate();
            }

        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
            }
        }
    }
    private void BindDate()
    {
        DateTime MonthYear = DateTime.Now;
        txtMonthYear.Text = MonthYear.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
    }
    private void FillMRManagers1()
    {
        //SalesForce sf = new SalesForce();
        //DataSet dsmgrsf = new DataSet();
        //SalesForce ds = new SalesForce();
        //DataSet DsAudit = ds.SF_Hierarchy(div_code, Session["sf_code"].ToString());
        //if (DsAudit.Tables[0].Rows.Count > 0)
        //{
        //    dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        //}
        //else
        //{
        //    DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, Session["sf_code"].ToString(), 0);
        //    dsmgrsf.Tables.Add(dt);
        //    dsSalesForce = dsmgrsf;
        //}

        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlFieldForce.DataTextField = "sf_name";
        //    ddlFieldForce.DataValueField = "sf_code";
        //    ddlFieldForce.DataSource = dsSalesForce;
        //    ddlFieldForce.DataBind();
        //}
        string sfcode=Session["sf_code"].ToString();
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sfcode);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sfcode);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sfcode, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SubDivision dv = new SubDivision();
        int iReturn = 0;
        foreach (GridViewRow grdAct in grdActUpload.Rows)
        {
            Label lblActivity_ID = (Label)grdAct.Cells[1].FindControl("lblActivity_ID");
            Label lblSf_Code = (Label)grdAct.Cells[3].FindControl("lblSf_Code");
            Label lblDate_Activity_Approval = (Label)grdAct.Cells[5].FindControl("lblDate_Activity_Approval");
            CheckBox chkProcess = (CheckBox)grdAct.Cells[6].FindControl("chkProcess");
            int ChkProcessed = chkProcess.Checked ? 1 : 0;
            
            //iReturn = dv.TransActivityRecordUpdate(lblActivity_ID.Text, lblSf_Code.Text, lblDate_Activity_Approval.Text, div_code, ChkProcessed);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Successfully');</script>");
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnlFFDate.Visible = true;
        txtMonthYear.Enabled = false;
    }
    protected void btnGo2_Click(object sender, EventArgs e)
    {
        FillActivity();
    }
    protected void grdActUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }

    private void FillActivity()
    {
        string[] TxtDate = txtMonthYear.Text.Split('-');
		string strQry = string.Empty;
        int MonthNumber = Convert.ToInt32(DateTime.ParseExact(TxtDate[0].ToString(), "MMM", CultureInfo.CurrentCulture).Month);
        int Year = Convert.ToInt32(TxtDate[1].ToString());
        SubDivision dv = new SubDivision();
		DB_EReporting db_ER = new DB_EReporting();
        //dsActivityUpload = dv.getTransActivityUpload(div_code, ddlFieldForce.SelectedValue, MonthNumber, Year, ddlDate.SelectedValue, Session["sf_type"].ToString());
		strQry = " EXEC Sp_GetProcessingActivityReport '" + div_code + "','" + ddlFieldForce.SelectedValue + "','" + MonthNumber + "','" + Year + "'," +
         "'" + ddlDate.SelectedValue + "','" + Session["sf_type"].ToString() + "'";
		 dsActivityUpload = db_ER.Exec_DataSet(strQry);
        if (dsActivityUpload.Tables[0].Rows.Count > 0)
        {
            grdActUpload.Visible = true;
            grdActUpload.DataSource = dsActivityUpload;
            grdActUpload.DataBind();
            //btnSave.Visible = true;
        }
        else
        {

            grdActUpload.DataSource = null;
            grdActUpload.DataBind();
            btnSave.Visible = false;
        }
    }
    protected void grdActUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in grdActUpload.Rows)
        {
            HiddenField hdnProcess = (HiddenField)gridRow.Cells[6].FindControl("hdnProcess");
            Label lblProcess_Flag = (Label)gridRow.Cells[6].FindControl("lblProcess_Flag");
            //CheckBox chkProcess = (CheckBox)gridRow.Cells[6].FindControl("chkProcess");
            lblProcess_Flag.Text = hdnProcess.Value == "1" ? "Processed" : "Not Processed";
            lblProcess_Flag.Attributes.Add("style", hdnProcess.Value == "1" ? "color:green" : "color:red");
        }
    }
    protected void grdActUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActUpload.PageIndex = e.NewPageIndex;
        FillActivity();
    }
}