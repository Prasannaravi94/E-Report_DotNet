using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
public partial class MasterFiles_Task_Management_Task_Edit : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dssf = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTask = new DataSet();


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
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;
    string task_id =string.Empty;
    string Type = string.Empty;
    string sSfCode = string.Empty;
    string Task_From = string.Empty;
    string Task_To = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
      
        sf_code = Session["Sf_code"].ToString();
        Task_From = Request.QueryString["Assign_From"].ToString();
        Task_To = Request.QueryString["Assign_To"].ToString();
       // sf_name = Session["Sf_Name"].ToString();
        task_id = Request.QueryString["Task_ID"].ToString();
        Type = Request.QueryString["type"].ToString();
        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "2")
            {
              // FillMRManagers();
              
                FillMode();
                Viewtask();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                FillMode();
                Viewtask();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                liassign.Visible = false;
               
              //  FillMRManagers();
               // ddlFieldForce.SelectedValue = sf_code;
              //  ddlFieldForce.Enabled = false;

                FillMode();
                Viewtask();
            }

        }
    }
    private void Viewtask()
    {
        con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        if (sf_code.Trim() == Task_To.Trim())
        {
            string strQry = "update Trans_Task_Details set Task_Status_Id='2',Task_Status_Name='Open' where Task_ID='" + task_id + "'";
            SqlCommand cmd1 = new SqlCommand(strQry, con);
            cmd1.ExecuteNonQuery();
        }
        if (Type.Trim() == "1")
        {
            head.InnerText = "Task - View";

            cmd = new SqlCommand("select Mode_Id,Mode_Name,Task_From_Code,Task_From_Name,Task_Desc,Priority,convert(char(10),DeadLine_From,103) as DeadLine_From,convert(char(10),DeadLine_To,103) as DeadLine_To, " +
                                 "Task_To_Code,Task_To_Name,Task_Status_Id,Task_Status_Name from Trans_Task_Details where Task_ID='" + task_id + "' ", con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmode.SelectedValue = ds.Tables[0].Rows[0]["Mode_Id"].ToString();
                lblAssignfrom.Text = ds.Tables[0].Rows[0]["Task_From_Name"].ToString();
                lblAssignTo.Text = ds.Tables[0].Rows[0]["Task_To_Name"].ToString();
                ddlPri.SelectedValue = ds.Tables[0].Rows[0]["Priority"].ToString();
                txt_Date.Text = ds.Tables[0].Rows[0]["DeadLine_From"].ToString();
                to_Date.Text = ds.Tables[0].Rows[0]["DeadLine_To"].ToString();
                txtdes.Text = ds.Tables[0].Rows[0]["Task_Desc"].ToString();
            }
            ddlmode.Enabled = false;
           // ddlFieldForce.Enabled = false;
            ddlPri.Enabled = false;
            txt_Date.Enabled = false;
            to_Date.Enabled = false;
            txtdes.Enabled = false;
            

        }
        con.Close();
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
    //private void FillMRManagers()
    //{
    //    SalesForce sf = new SalesForce();
    //    //ddlFFType.Visible = false;
    //    //ddlAlpha.Visible = false;
    //    DataSet dsmgrsf = new DataSet();
    //    SalesForce ds = new SalesForce();

    //    // Check if the manager has a team
    //    DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
    //    if (DsAudit.Tables[0].Rows.Count > 0)
    //    {

    //        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
    //    }
    //    else
    //    {

    //        // Fetch Managers Audit Team
    //        DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
    //        dsmgrsf.Tables.Add(dt);
    //        dsSalesForce = dsmgrsf;
    //    }

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        dsSalesForce.Tables[0].Rows[0].Delete();

    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "Desig_Color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();


    //    }
    //    // FillColor();


    //}
    //private void FillColor()
    //{
    //    int j = 0;


    //    foreach (ListItem ColorItems in ddlSF.Items)
    //    {
    //        //ddlFieldForce.Items[j].Selected = true;
    //        string bcolor = "#" + ColorItems.Text;
    //        ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

    //        j = j + 1;

    //    }
    //}


    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("Task_Status.aspx");
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