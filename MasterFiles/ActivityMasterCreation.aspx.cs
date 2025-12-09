using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_ActivityMasterCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsActDiv = null;
    string Activity_ID = string.Empty;
    string divcode = string.Empty;
    string Activity_sname = string.Empty;
    string Activity_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "ActivityMasterList.aspx";
        Activity_ID = Request.QueryString["Activity_ID"];

        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtact_Sname.Focus();
            if (Activity_ID != "" && Activity_ID != null)
            {
                SubDivision sd = new SubDivision();
                dsActDiv = sd.getActivityMaster(divcode, Activity_ID);
                if (dsActDiv.Tables[0].Rows.Count > 0)
                {
                    txtact_Sname.Text = dsActDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtact_Name.Text = dsActDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Activity_sname = txtact_Sname.Text;
        Activity_name = txtact_Name.Text;
        if (Activity_ID == null)
        {
            // Add New Sub Division
            SubDivision dv = new SubDivision();
            int iReturn = dv.MasActivityRecordAdd(divcode, Activity_sname, Activity_name);

            if (iReturn > 0)
            {
                // menu1.Status = "Sub Division created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='ActivityMasterList.aspx';</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Name Already Exist');</script>");
                txtact_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Short Name Already Exist');</script>");
                txtact_Sname.Focus();
            }
        }
        else
        {
            SubDivision dv = new SubDivision();
            int ActivityID = Convert.ToInt16(Activity_ID);
            int iReturn = dv.MasActivityRecordUpdate(ActivityID, Activity_sname, Activity_name, divcode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ActivityMasterList.aspx';</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Name Already Exist');</script>");
                txtact_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Short Name Already Exist');</script>");
                txtact_Sname.Focus();
            }
        }
    }
    private void Resetall()
    {
        txtact_Sname.Text = "";
        txtact_Name.Text = "";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ActivityMasterList.aspx");
    }
}