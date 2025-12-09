using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using DBase_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Globalization;

public partial class MasterFiles_MR_TP_Lock : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsTP2 = null;
    DataSet dsTPC = null;
    DataSet dsTPClock = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = new DataSet();
    DataSet dsTPview = null;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr_Value = string.Empty;
    string TP_Terr_Name = string.Empty;
    string strddlWT = string.Empty;
    string strddlFWText = string.Empty;
    string TP_Terr1_Value = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2_Value = string.Empty;
    string TP_Terr2_Name = string.Empty;
    bool TP_Submit = false;
    bool EmptyWT = false;
    bool EmptyTerr = false;
    string wrktype_code = string.Empty;
    string ddlWwrktype_name = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    string strTPView = string.Empty;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;
    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    DataSet dsWeekoff = null;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_type = string.Empty;
    string subdivision_code = string.Empty;
    string timezone = string.Empty;
    string submitdate = string.Empty;
    string name = string.Empty;
    string sf_mrcode = string.Empty;
    string MR_Code = string.Empty;
    string MR_Cod = string.Empty;
    string MR_Month = string.Empty;
    string MR_Mont = string.Empty;
    string MR_Year = string.Empty;
    string MR_Yea = string.Empty;
    string sQryStr = string.Empty;
    string sQrySt = string.Empty;
    string Edit = string.Empty;
    string StrMonth = string.Empty;
    string ID = string.Empty;
    DataSet dsWorkTypeSettings = null;
    string strIndex = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsWrktype = null;
    string month = string.Empty;
    string year = string.Empty;
    DataSet dsDr = null;
    string Index = string.Empty;
    string mnth = string.Empty;
    int tp_count;
    int session_tp;
    int cust_need;
    int doc_need;
    int chem_need;
    int stk_need;
    int cip_need;
    int hos_need;
    DataSet dsplan = null;
    DataSet dstpplan = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        name = Request.QueryString["name"].ToString();
        sf_mrcode = Request.QueryString["sfcode"].ToString();
        month = Request.QueryString["month"].ToString();
        year = Request.QueryString["year"].ToString();
        sf_type = Request.QueryString["sftype"].ToString();
        mnth = Request.QueryString["Mnth"].ToString();


        strQry = " select a.Sf_Name +' - ' +sf_Designation_Short_Name+' - '+a.Sf_HQ as sf_name,sf_emp_id, CONVERT(varchar,Sf_Joining_Date,103) as Sf_Joining_Date, " +
 " ((select Sf_Name from Mas_Salesforce_AM where Sf_Code=b.TP_AM) +' - '+ " +
 " (select sf_Designation_Short_Name + ' - '+Sf_HQ  from Mas_Salesforce where Sf_Code=b.TP_AM)) as Reporting_to_tp,  " +
 " (select last_tp_date from mas_salesforce_dcrtpdate where sf_code='" + sf_mrcode + "') last_tp_date " +
 " from Mas_Salesforce a , Mas_Salesforce_AM b where a.Sf_Code='" + sf_mrcode + "' " +
 " and a.Sf_Code=b.Sf_Code";

        dsTPClock = db_ER.Exec_DataSet(strQry);

        if (dsTPClock.Tables[0].Rows.Count > 0)
        {
            lblf1.Text = dsTPClock.Tables[0].Rows[0]["sf_name"].ToString();
            lble1.Text = dsTPClock.Tables[0].Rows[0]["sf_emp_id"].ToString();
            lbld1.Text = dsTPClock.Tables[0].Rows[0]["Sf_Joining_Date"].ToString();
            lblr1.Text = dsTPClock.Tables[0].Rows[0]["Reporting_to_tp"].ToString();
        }
        lbltour.Text = "TP Approval For " + "<span style='color:Red'>" + name
                    + "</span>" + "<span style='color:Green'> " + " " + month + " " + year + "</span>";
        //lblmsg.Text = " TP should be approve from App only...";
    }
    protected void btnbck_Click(object sender, EventArgs e)
    {

        Response.Redirect("../MGR/MGR_Index.aspx");
    }
    protected void btnAppRej_Click(object sender, EventArgs e)
    {
        TP_New tp = new TP_New();
        dsTPview = tp.gridTpviewAppRej(sf_mrcode, Session["div_code"].ToString(), mnth, year);
        grdTPview.Visible = true;
        grdTPview.DataSource = dsTPview;
        grdTPview.DataBind();

        tpdiv.Visible = true;
        lblmsg.Visible = false;
        btnAppRej.Visible = false;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        TP_New tp = new TP_New();//-------------------js
        iReturn = tp.Approve_TpNewMGRnew1(Request.QueryString["sfcode"].ToString(), Request.QueryString["Mnth"].ToString(), Request.QueryString["year"].ToString(), Session["sf_name"].ToString());
        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        //grdTPview.Enabled = false;
        btnApprove.Visible = false;
        btnReject.Visible = false;
        btnSendBack.Visible = true;
        lblReject.Visible = true;

        txtReason.Focus();
    }

    protected void btnSendBack_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            int iReturn = -1;
            int icount = -1;
            TP_New tp = new TP_New();

            txtReason.Text = txtReason.Text.ToString().Replace("'", "asdf");

            iReturn = tp.Reject_New(sf_mrcode, mnth, year, txtReason.Text, Session["sf_name"].ToString());

            if (iReturn > 0)
            {
                using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = " select count (*) from tourplan_detail where sfcode='" + sf_mrcode + "' and mnth='" + mnth + "'" +
                                              " and yr='" + year + "' and div='" + Session["div_code"].ToString() + "'  ";
                        icount = (Int32)command.ExecuteScalar();
                        if (icount > 0)
                        {
                            command.CommandText = "Update tourplan_detail set Change_Status='2', Rejection_Reason='" + txtReason.Text + "' where SFCode='" + sf_mrcode + "'" +
                                                    " and Mnth='" + mnth + "' and Yr = '" + year + "' ";
                        }
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("Message: {0}", ex.Message);
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Eror Exist .Kindly resubmit again'); </script>");
                    }
                }
            }
            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Rejected Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
            }
        }
        else
        {
            txtReason.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
        }
    }
}