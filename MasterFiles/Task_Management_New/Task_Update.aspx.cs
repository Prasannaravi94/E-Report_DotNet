using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
public partial class MasterFiles_Task_Management_New_Task_Update : System.Web.UI.Page
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
    string newSFCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        sf_code = Session["sf_code"].ToString();
        if (Session["sf_code_Tem"] != null)
        {
            newSFCode = Session["sf_code_Tem"].ToString();
        }
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
               // lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "2")
            {
               
                Viewtask();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                Viewtask();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
              liassign.Visible = false;
                Viewtask();
            }

        }
    }
    private void Viewtask()
    {
        con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        //if (sf_code == Task_To)
        //{
        //    string strQry = "update Trans_Task_Details set Task_Status_Id='2',Task_Status_Name='Open' where Task_ID='" + task_id + "'";
        //    SqlCommand cmd1 = new SqlCommand(strQry, con);
        //    cmd1.ExecuteNonQuery();
        //}
       
           

            cmd = new SqlCommand("select Mode_Id,Mode_Name,Task_From_Code,Task_From_Name,Task_Desc,case when Priority='H' then 'High' else  " +
                                 " case when Priority='L' then 'Low' else case when Priority='M' then 'Medium' end end end Priority " +

                                 " ,convert(char(10),DeadLine_From,103) as DeadLine_From,convert(char(10),DeadLine_To,103) as DeadLine_To, convert(char(10),Completed_Date,103) as Completed_Date," +
                                 "Task_To_Code,Task_To_Name,Task_Status_Id,Task_Status_Name,Completed_Comments,convert(char(10),Closed_Date,103) as Closed_Date,Closed_Comments,convert(char(10),ReOpen_Date,103) as ReOpen_Date,ReOpen_Comments, " +
                                 " convert(char(10),Hold_Date,103) as Hold_Date,Hold_Comments,convert(char(10),Cancel_Date,103) as Cancel_Date,Cancel_Comments from Trans_Task_Details where Task_ID='" + task_id + "' ", con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmode.Text = ds.Tables[0].Rows[0]["Mode_Name"].ToString();
                lblAssignFrom.Text = ds.Tables[0].Rows[0]["Task_From_Name"].ToString();
                lblAssignTo.Text = ds.Tables[0].Rows[0]["Task_To_Name"].ToString();
                lblprior.Text = ds.Tables[0].Rows[0]["Priority"].ToString();
                txt_Date.Text = ds.Tables[0].Rows[0]["DeadLine_From"].ToString();
                to_Date.Text = ds.Tables[0].Rows[0]["DeadLine_To"].ToString();
                //to_Date.Text = ds.Tables[0].Rows[0]["DeadLine_To"].ToString();
                txtdes.Text = ds.Tables[0].Rows[0]["Task_Desc"].ToString();
                txtComment.Text = ds.Tables[0].Rows[0]["Completed_Comments"].ToString();
                txtC_Date.Text = ds.Tables[0].Rows[0]["Completed_Date"].ToString();
                string status = ds.Tables[0].Rows[0]["Task_Status_Id"].ToString();
                if (status == "4")
                {
                    txtCReason.Text = ds.Tables[0].Rows[0]["Closed_Comments"].ToString();
                    lblReasonDate.Text = ds.Tables[0].Rows[0]["Closed_Date"].ToString();
                }
                else if (status == "5")
                {
                    txtCReason.Text = ds.Tables[0].Rows[0]["ReOpen_Comments"].ToString();
                    lblReasonDate.Text = ds.Tables[0].Rows[0]["ReOpen_Date"].ToString();
                }
                else if (status == "6")
                {
                    txtCReason.Text = ds.Tables[0].Rows[0]["Hold_Comments"].ToString();
                    lblReasonDate.Text = ds.Tables[0].Rows[0]["Hold_Date"].ToString();
                }
                else if (status == "7")
                {
                    txtCReason.Text = ds.Tables[0].Rows[0]["Cancel_Comments"].ToString();
                    lblReasonDate.Text = ds.Tables[0].Rows[0]["Cancel_Date"].ToString();
                }
            }
            if (newSFCode == "0")
            {
                if (Type == "1")
                {
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    btnHold.Visible = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;

                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    head.InnerText = "New Task";
                }
                else if (Type == "2")
                {
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    btnHold.Visible = false;
                    // lblCom.Visible = false;
                    txtComment.Visible = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    head.InnerText = "Pending Task";
                }
                else if (Type == "3")
                {
                    //  lblCom.Visible = true;
                    txtComment.Enabled = false;
                    //   txt_Date.Enabled = false;
                    //  to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtC_Date.Enabled = false;
                    btnClose.Visible = false;
                    btnReopen.Visible = false;
                    head.InnerText = "Completed Task";
                }
                else if (Type == "4")
                {

                    txtComment.Enabled = false;

                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtCReason.Enabled = false;
                    btnReopen.Visible = false;
                    head.InnerText = "Closed Task";
                    lblCReason.Text = "Closed Comments";
                    lblRDate.Text = "Closed Date : ";
                }
                else if (Type == "5")
                {
                    //  lblCom.Visible = false;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtC_Date.Enabled = false;
                    txtComment.Enabled = false;
                    btnReopen.Visible = false;
                    txtCReason.Enabled = false;
                    head.InnerText = "ReOpen Task";
                    lblCReason.Text = "ReOpen Comments";
                    lblRDate.Text = "ReOpen Date : ";
                }
                else if (Type == "6")
                {

                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "Hold Task";
                    lblCReason.Text = "Hold Comments";
                    lblRDate.Text = "Hold Date : ";
                }
                else if (Type == "7")
                {

                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;

                    head.InnerText = "Cancel Task";
                    lblCReason.Text = "Cancel Comments";
                    lblRDate.Text = "Cancel Date : ";
                }
            }
            else if (sf_code == Task_To)
            {

                if (Type == "1")
                {
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = true;
                  //  trcomm.Visible = true;
                    btnComp.Visible = true;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    string strQry = "update Trans_Task_Details set Task_Status_Id='2',Task_Status_Name='Open' where Task_ID='" + task_id + "'";
                    SqlCommand cmd1 = new SqlCommand(strQry, con);
                    cmd1.ExecuteNonQuery();
                    head.InnerText = "New Task";
                }
                else if (Type == "2")
                {
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = true;
                   trcomm.Visible = true;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    btnComp.Visible = true;
                    txtC_Date.Text = "";
                    txtComment.Text = "";
                    head.InnerText = "Pending Task";

                }
                else if (Type == "3")
                {
                   // lblCom.Visible = true;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = true;
                    trcomm.Visible = true;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtC_Date.Enabled = false;
                    head.InnerText = "Completed Task";
                }
                else if (Type == "4")
                {
                   // lblCom.Visible = true;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "Closed Task";
                    lblCReason.Text = "Closed Comments";
                    lblRDate.Text = "Closed Date : ";
                }
                else if (Type == "5")
                {
                   //lblCom.Visible = true;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "ReOpen Task";
                    lblCReason.Text = "ReOpen Comments";
                    lblRDate.Text = "ReOpen Date : ";
                    string strQry = "update Trans_Task_Details set Task_Status_Id='2',Task_Status_Name='Open' where Task_ID='" + task_id + "'";
                    SqlCommand cmd1 = new SqlCommand(strQry, con);
                    cmd1.ExecuteNonQuery();
                }
                else if (Type == "6")
                {
                   // lblCom.Visible = true;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "Hold Task";
                    lblCReason.Text = "Hold Comments";
                    lblRDate.Text = "Hold Date : ";
                }
                else if (Type == "7")
                {
                   // lblCom.Visible = true;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "Cancel Task";
                    lblCReason.Text = "Cancel Comments";
                    lblRDate.Text = "Cancel Date : ";
                }
            }
            else
            {
                if (Type == "1")
                {
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                    btnHold.Visible = true;
                    txt_Date.Enabled = true;
                    to_Date.Enabled = true;
                    txtdes.Enabled = true;
                  
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    head.InnerText = "New Task";
                }
                else if (Type == "2")
                {
                    btnUpdate.Visible = false;
                    btnCancel.Visible = true;
                    btnHold.Visible = true;
                   // lblCom.Visible = false;
                    txtComment.Visible = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    head.InnerText = "Pending Task";
                }
                else if (Type == "3")
                {
                  //  lblCom.Visible = true;
                    txtComment.Enabled = false;
                 //   txt_Date.Enabled = false;
                  //  to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = true;
                    trcomm.Visible = true;
                    trreasonD.Visible = false;
                    trreason.Visible = false;
                    txtC_Date.Enabled = false;
                    btnClose.Visible = true;
                    btnReopen.Visible = true;
                    head.InnerText = "Completed Task";
                }
                else if (Type == "4")
                {

                    txtComment.Enabled = false;

                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    btnReopen.Visible = true;
                    head.InnerText = "Closed Task";
                    lblCReason.Text = "Closed Comments";
                    lblRDate.Text = "Closed Date : ";
                }
                else if (Type == "5")
                {
                  //  lblCom.Visible = false;
                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtC_Date.Enabled = false;
                    txtComment.Enabled = false;
                    btnReopen.Visible = false;
                    txtCReason.Enabled = false;
                    head.InnerText = "ReOpen Task";
                    lblCReason.Text = "ReOpen Comments";
                    lblRDate.Text = "ReOpen Date : ";
                }
                else if (Type == "6")
                {

                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;
                    head.InnerText = "Hold Task";
                    lblCReason.Text = "Hold Comments";
                    lblRDate.Text = "Hold Date : ";
                }
                else if (Type == "7")
                {

                    txtComment.Enabled = false;
                    txt_Date.Enabled = false;
                    to_Date.Enabled = false;
                    txtdes.Enabled = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    btnReopen.Visible = false;
                    trcomp.Visible = false;
                    trcomm.Visible = false;
                    trreasonD.Visible = true;
                    trreason.Visible = true;
                    txtCReason.Enabled = false;
                    txtComment.Enabled = false;

                    head.InnerText = "Cancel Task";
                    lblCReason.Text = "Cancel Comments";
                    lblRDate.Text = "Cancel Date : ";
                }
            }
            
       
        con.Close();
    }
   


    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("Task_Status.aspx");
    }
    protected void btnComp_Click(object sender, EventArgs e)
    {
      
        con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();

        string strQry = "update Trans_Task_Details set Task_Status_Id='3',Task_Status_Name='Completed',Completed_Date=getdate(),Completed_Comments='" + txtComment.Text.Replace("'", " ").Trim() + "' where Task_ID='" + task_id + "' ";
            SqlCommand cmd1 = new SqlCommand(strQry, con);
            cmd1.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Completed Successfully');window.location='Task_Status.aspx'</script>");
       
        con.Close();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        btnReopen.Visible = false;
        btnClose.Visible = false;
        btnConfirm.Visible = true;
        txtReason.Visible = true;
        txtReason.Focus();
        lblReason.Visible = true;
        ViewState["status"] = "4";

        
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        btnUpdate.Visible = false;
        btnReopen.Visible = false;
        btnHold.Visible = false;
        btnCancel.Visible = false;
        btnConfirm.Visible = true;
        txtReason.Visible = true;
        txtReason.Focus();
        lblReason.Visible = true;
        ViewState["status"] = "6";

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ViewState["status"] != null)
        {
            string status = ViewState["status"].ToString();
            if (status == "4")
            {
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();

                string strQry = "update Trans_Task_Details set Task_Status_Id='4',Task_Status_Name='Closed',Closed_Date=getdate(),Closed_Comments='" + txtReason.Text.Replace("'", " ").Trim() + "' where Task_ID='" + task_id + "' ";
                SqlCommand cmd1 = new SqlCommand(strQry, con);
                cmd1.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Closed Successfully');window.location='Task_Status.aspx'</script>");

                con.Close();
            }
            else if (status == "5")
            {
                if (txt_Date.Text != "")
                {

                    D_From_date = Convert.ToDateTime(txt_Date.Text).ToString("MM/dd/yyyy");
                }

                if (to_Date.Text != "")
                {

                    D_To_date = Convert.ToDateTime(to_Date.Text).ToString("MM/dd/yyyy");
                }
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();

                string strQry = "update Trans_Task_Details set Task_Status_Id='5',Task_Status_Name='ReOpen',ReOpen_Comments='" + txtReason.Text.Replace("'", " ").Trim() + "',ReOpen_Date=getdate(),DeadLine_From='" + D_From_date + "',DeadLine_To='" + D_To_date + "' where Task_ID='" + task_id + "' ";
                SqlCommand cmd1 = new SqlCommand(strQry, con);
                cmd1.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task ReOpen Successfully');window.location='Task_Status.aspx'</script>");

                con.Close();
            }
            else if (status == "6")
            {
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();

                string strQry = "update Trans_Task_Details set Task_Status_Id='6',Task_Status_Name='Hold',Hold_Comments='" + txtReason.Text.Replace("'", " ").Trim() + "',Hold_Date=getdate() where Task_ID='" + task_id + "' ";
                SqlCommand cmd1 = new SqlCommand(strQry, con);
                cmd1.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Hold Successfully');window.location='Task_Status.aspx'</script>");

                con.Close();
            }
            else if (status == "7")
            {
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();

                string strQry = "update Trans_Task_Details set Task_Status_Id='7',Task_Status_Name='Cancel',Cancel_Comments='" + txtReason.Text.Replace("'", " ").Trim() + "',Cancel_Date=getdate() where Task_ID='" + task_id + "' ";
                SqlCommand cmd1 = new SqlCommand(strQry, con);
                cmd1.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Cancel Successfully');window.location='Task_Status.aspx'</script>");

                con.Close();
            }
        }
    }
    protected void btnReopen_Click(object sender, EventArgs e)
    {
        btnReopen.Visible = false;
        btnClose.Visible = false;
        btnConfirm.Visible = true;
        txtReason.Visible = true;
        txtReason.Focus();
        lblReason.Visible = true;
        ViewState["status"] = "5";
      
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnReopen.Visible = false;
        btnUpdate.Visible = false;
        btnHold.Visible = false;
        btnCancel.Visible = false;
        btnConfirm.Visible = true;
        txtReason.Visible = true;
        txtReason.Focus();
        lblReason.Visible = true;
        ViewState["status"] = "7";

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {

            D_From_date = Convert.ToDateTime(txt_Date.Text).ToString("MM/dd/yyyy");
        }

        if (to_Date.Text != "")
        {

            D_To_date = Convert.ToDateTime(to_Date.Text).ToString("MM/dd/yyyy");
        }
        con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();

        string strQry = "update Trans_Task_Details set DeadLine_From='" + D_From_date + "',DeadLine_To='" + D_To_date + "',Task_Desc='" + txtdes.Text.Replace("'"," ").Trim() + "',Updated_Date=getdate() where Task_ID='" + task_id + "' ";
        SqlCommand cmd1 = new SqlCommand(strQry, con);
        cmd1.ExecuteNonQuery();
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Task Updated Successfully');window.location='Task_Status.aspx'</script>");

        con.Close();
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