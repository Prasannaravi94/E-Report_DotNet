using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Processing_Activity : System.Web.UI.Page
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
    protected void chkTick_ChckedChanged(object sender, EventArgs e)
    {
        FillActivity();
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
            Label lblActivity_Approved_Bill_Amount = (Label)grdAct.Cells[5].FindControl("lblActivity_Approved_Bill_Amount");
            HiddenField hdnMonth_Activity = (HiddenField)grdAct.Cells[5].FindControl("hdnMonth_Activity");
            HiddenField hdnYear_Activity = (HiddenField)grdAct.Cells[5].FindControl("hdnYear_Activity");
            TextBox txtProcess_Date = (TextBox)grdAct.Cells[0].FindControl("txtProcess_Date");
            int ChkProcessed = chkProcess.Checked ? 1 : 0;
            string Process_Date = string.Empty;
            if (txtProcess_Date.Text != string.Empty)
            {
                string[] SplProcess_Date = txtProcess_Date.Text.Split('/');
                Process_Date = SplProcess_Date[1] + "/" + SplProcess_Date[0] + "/" + SplProcess_Date[2];
            }

            iReturn = dv.TransActivityRecordUpdate(lblActivity_ID.Text, lblSf_Code.Text, lblDate_Activity_Approval.Text, div_code, ChkProcessed, lblActivity_Approved_Bill_Amount.Text, hdnMonth_Activity.Value, hdnYear_Activity.Value, Process_Date);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Successfully');</script>");
            FillActivity();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SubDivision dv = new SubDivision();
        int iReturn = 0;
        foreach (GridViewRow grdAct in grdActUpload.Rows)
        {
            Label lblActivity_ID = (Label)grdAct.Cells[1].FindControl("lblActivity_ID");
            Label lblSf_Code = (Label)grdAct.Cells[3].FindControl("lblSf_Code");
            Label lblDate_Activity_Approval = (Label)grdAct.Cells[5].FindControl("lblDate_Activity_Approval");
            CheckBox chkdelete = (CheckBox)grdAct.Cells[6].FindControl("chkdelete");
            Label lblActivity_Approved_Bill_Amount = (Label)grdAct.Cells[5].FindControl("lblActivity_Approved_Bill_Amount");
            HiddenField hdnMonth_Activity = (HiddenField)grdAct.Cells[5].FindControl("hdnMonth_Activity");
            HiddenField hdnYear_Activity = (HiddenField)grdAct.Cells[5].FindControl("hdnYear_Activity");
            TextBox txtProcess_Date = (TextBox)grdAct.Cells[0].FindControl("txtProcess_Date");
            string Process_Date = string.Empty;
            if (txtProcess_Date.Text != string.Empty)
            {
                string[] SplProcess_Date = txtProcess_Date.Text.Split('/');
                Process_Date = SplProcess_Date[1] + "/" + SplProcess_Date[0] + "/" + SplProcess_Date[2];
            }
            if (chkdelete.Checked)
            {
                iReturn = dv.TransActivityRecordDelete(lblActivity_ID.Text, lblSf_Code.Text, lblDate_Activity_Approval.Text, div_code, lblActivity_Approved_Bill_Amount.Text, hdnMonth_Activity.Value, hdnYear_Activity.Value, Process_Date);
            }


        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            FillActivity();
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
        chkTick.Visible = true;
    }
    protected void grdActUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {

        }
    }

    private void FillActivity()
    {
        string[] TxtDate = txtMonthYear.Text.Split('-');
        string strQry = string.Empty;
        int MonthNumber = Convert.ToInt32(DateTime.ParseExact(TxtDate[0].ToString(), "MMM", CultureInfo.CurrentCulture).Month);
        int Year = Convert.ToInt32(TxtDate[1].ToString());
        SubDivision dv = new SubDivision();
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsActUpload = null;
        int Processed = chkTick.Checked ? 1 : 0;

        strQry = " EXEC Sp_GetProcessingActivity '" + div_code + "','" + ddlFieldForce.SelectedValue + "','" + MonthNumber + "','" + Year + "'," +
          "'" + ddlDate.SelectedValue + "','" + Session["sf_type"].ToString() + "', '" + Processed + "'";

        dsActivityUpload = db_ER.Exec_DataSet(strQry);

        //dsActivityUpload = dv.getTransActivityUpload(div_code, ddlFieldForce.SelectedValue, MonthNumber, Year, ddlDate.SelectedValue, Session["sf_type"].ToString());
        if (dsActivityUpload.Tables[0].Rows.Count > 0)
        {
            grdActUpload.Visible = true;
            grdActUpload.DataSource = dsActivityUpload;
            grdActUpload.DataBind();
            btnSave.Visible = true;
            chkTick.Visible = true;
        }
        else
        {

            grdActUpload.DataSource = null;
            grdActUpload.DataBind();
            btnSave.Visible = false;
            chkTick.Visible = false;
        }
    }
    protected void grdActUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in grdActUpload.Rows)
        {
            HiddenField hdnProcess = (HiddenField)gridRow.Cells[6].FindControl("hdnProcess");
            TextBox txtProcess_Date = (TextBox)gridRow.Cells[6].FindControl("txtProcess_Date");
            CheckBox chkProcess = (CheckBox)gridRow.Cells[6].FindControl("chkProcess");
            CheckBox chkdelete = (CheckBox)gridRow.Cells[6].FindControl("chkdelete");
            chkProcess.Checked = hdnProcess.Value == "1" ? true : false;
            txtProcess_Date.Enabled = chkTick.Checked ? false : true;
            if (chkProcess.Checked)
            {
                grdActUpload.Columns[4].Visible = false;
                btndelete.Visible = false;
                //e.Row.Cells[2].Visible = false;
            }
            else
            {
                grdActUpload.Columns[4].Visible = true;
                btndelete.Visible = true;
                //e.Row.Cells[2].Visible = true;
            }
            //string Process_Date = string.Empty;
            //if (txtProcess_Date.Text != string.Empty)
            //{
            //    if (txtProcess_Date.Text.Contains("/"))
            //    {
            //        string[] SplProcess_Date = txtProcess_Date.Text.Split('/');
            //        Process_Date = SplProcess_Date[1] + "/" + SplProcess_Date[0] + "/" + SplProcess_Date[2];
            //    }
            //    else {
            //        string[] SplProcess_Date = txtProcess_Date.Text.Split('-');
            //        Process_Date = SplProcess_Date[0] + "/" + SplProcess_Date[1] + "/" + SplProcess_Date[2];
            //    }
            //}
            //txtProcess_Date.Text = Process_Date;
        }
    }
    protected void grdActUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActUpload.PageIndex = e.NewPageIndex;
        FillActivity();
    }
}