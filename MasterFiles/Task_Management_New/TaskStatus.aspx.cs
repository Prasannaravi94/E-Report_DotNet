using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Task_Management_TaskStatus : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsTask = null;
    string div_code = string.Empty;
    string strError = string.Empty;
    string ShortName = string.Empty;
    string TaskName = string.Empty;
    bool bIsValid = false;
    int iErrReturn = -1;
    #endregion
 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillTasks();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iRet = -1;

        TaskName = txtTaskName.Text.Trim();
        ShortName = txtTaskShortName.Text.Trim();

        bIsValid = true;

        if (ShortName.Length == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Short Name');</script>");
            bIsValid = false;
        }

        if (TaskName.Length == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Status Name');</script>");
            bIsValid = false;
        }

        Task tsk = new Task();
        if (bIsValid)
        {
            if (ViewState["hidTaskID"] == null)
            {
                bool bRecordExist = tsk.StatusExist(ShortName, TaskName,div_code);
                if (bRecordExist == false)
                    iRet = tsk.Status_RecordAdd(TaskName, ShortName,div_code);
            }
            else
                iRet = tsk.Status_RecordUpdate(Convert.ToInt32(ViewState["hidTaskID"].ToString().Trim()), TaskName, ShortName,div_code);

            if (iRet > 0)
            {
                if (ViewState["hidTaskID"] != null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Status has been updated Successfully');</script>");
                    btnSubmit.Text = "Submit";
                    ViewState["hidTaskID"] = null;
                    hidTaskID.Value = "";
                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Status has been created Successfully');</script>");


                hidTaskID.Value = "";
                ClearAll();
                FillTasks();
            }
        }

    }

    private void ClearAll()
    {
        txtTaskName.Text = "";
        txtTaskShortName.Text = "";
    }

    protected void grdTask_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int col_sno = Convert.ToInt16(e.CommandArgument);
            Task tsk = new Task();
            dsTask = tsk.getTaskStatus(col_sno);
            if (dsTask != null)
            {
                if (dsTask.Tables[0].Rows.Count > 0)
                {
                    txtTaskShortName.Text = dsTask.Tables[0].Rows[0]["Short_Name"].ToString();
                    txtTaskName.Text = dsTask.Tables[0].Rows[0]["Status_Name"].ToString();
                    btnSubmit.Text = "Update";
                    hidTaskID.Value = col_sno.ToString();
                    ViewState["hidTaskID"] = hidTaskID.Value;
                }
            }
        }
    }

    private void FillTasks()
    {
        try
        {
            Task tsk = new Task();
            dsTask = tsk.getTaskStatus();
            if (dsTask.Tables[0].Rows.Count > 0)
            {
                grdTask.DataSource = dsTask;
                grdTask.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Task Status - Maintenance", "FillTasks()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void grdTask_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void grdTask_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void grdTask_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }



}