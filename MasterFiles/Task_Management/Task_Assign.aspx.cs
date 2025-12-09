using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_Task_Management_Task_Assign : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dssf = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTask = new DataSet();

    DataSet dsFromName = new DataSet();
    string FromName = string.Empty;
    DataSet dsToName = new DataSet();
    string ToName = string.Empty;

    string strError = string.Empty;
    bool bIsValid = false;
    int iErrReturn = -1;
    int task_client = -1;
    int task_type = -1;
    string task_desc = string.Empty;
    string task_to = string.Empty;
    string D_From_date = string.Empty;
    string D_To_date = string.Empty;
    DateTime dtFromDate;
    DateTime dtToDate;
    int task_status = -1;
    int task_mode = -1;
    string task_sev = string.Empty;
  
    string sf_type = string.Empty;
    string Task_Det_ID = string.Empty;
    string Sl_No = string.Empty;
    string sf_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_name = Session["Sf_Name"].ToString();
        Session["sf_code_Tem"] = null;
        if (!Page.IsPostBack)
        {
            
            SalesForce sf = new SalesForce();
            dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblsf.Text = " ( "+ dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() +" ) ";
            }

            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
                FillMode();
                // FillColor();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {

                FillMRManagers();
                FillMode();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                liassign.Visible = false;
            }

        }

    }
    private void FillMode()
    {
        try
        {
            Task tsk = new Task();
            dsTask = tsk.getTaskMode(true,div_code);
            if (dsTask.Tables[0].Rows.Count > 0)
            {
                ddlmode.DataValueField = "Mode_ID";
                ddlmode.DataTextField = "Mode_Name";
                ddlmode.DataSource = dsTask;
                ddlmode.DataBind();
            }
            ddlmode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        //ddlFFType.Visible = false;
        //ddlAlpha.Visible = false;
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {

            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[0].Delete();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
        // FillColor();


    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        Task tsk = new Task();
        int iRet = -1;
        if (txt_Date.Text != "")
        {

            D_From_date = Convert.ToDateTime(txt_Date.Text).ToString("MM/dd/yyyy");
        }

        if (to_Date.Text != "")
        {

            D_To_date = Convert.ToDateTime(to_Date.Text).ToString("MM/dd/yyyy");
        }
        SalesForce sf=new SalesForce();

        dsFromName = sf.getSfCode_mr(sf_code);
        if (dsFromName.Tables[0].Rows.Count > 0)
        {
            FromName = dsFromName.Tables[0].Rows[0]["sf_name"].ToString();
        }
        dsToName = sf.getSfCode_mr(ddlFieldForce.SelectedValue);
        if (dsToName.Tables[0].Rows.Count > 0)
        {
            ToName = dsToName.Tables[0].Rows[0]["sf_name"].ToString();
        }

            if ((Task_Det_ID != "") && (Task_Det_ID != null))
            {
                //iRet = tsk.RecordUpdate(task_client, task_type, task_desc, task_to, comp_date, task_flexible, task_status, task_mode, task_sev, sf_code, Task_Det_ID, commit_date);
                //if (iRet > 0)
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Updated Successfully');window.location='TaskView.aspx';</script>");

                //    //ResetAll();
                //}
            }
            else
            {
                iRet = tsk.RecordAdd_Details_Task(ddlmode.SelectedValue, ddlmode.SelectedItem.Text.Trim(), sf_code, FromName.Trim(), txtdes.Text.Replace("'", " ").Trim(), ddlPri.SelectedValue.Trim(), D_From_date, D_To_date, ddlFieldForce.SelectedValue, ToName.Trim(), div_code);
                if (iRet > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Assignment has been created Successfully');</script>");
                    ResetAll();
                }
            }
            
    }
    private void ResetAll()
    {
        ddlFieldForce.SelectedIndex = 0;
        ddlmode.SelectedIndex = 0;
        txtdes.Text = "";
        ddlPri.SelectedIndex = 0;
        txt_Date.Text = "";
        to_Date.Text = "";
       
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlmode.SelectedIndex = -1;
        ddlPri.SelectedIndex = -1;
        txt_Date.Text = "";
        to_Date.Text = "";
        txtdes.Text = "";
    }
    protected void Back_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            Server.Transfer("~/BasicMaster.aspx");
        }
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            Server.Transfer("~/MGR_Home.aspx");
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            Server.Transfer("~/Default_MR.aspx");

        }

    }
}