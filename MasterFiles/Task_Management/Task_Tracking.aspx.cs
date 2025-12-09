using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_Task_Management_Task_Tracking : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dssf = new DataSet();
    DataSet dsSalesForce = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            BindDate();
            FillMRManagers();
            FillTask();
            SalesForce sf = new SalesForce();
            dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "1")
            {
                liassign.Visible = false;
            }

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
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }


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

            ddlSF.DataTextField = "sf_name";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
         
          
        }
        // FillColor();


    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillTask();
    }
    private void FillTask()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Task_Track_ALL";

         
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);       
        cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@imonth", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@iyear", ddlYear.SelectedValue);    
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();


        if (dsts.Tables[0].Rows.Count > 0)
        {
            grdTask.DataSource = dsts;
            grdTask.DataBind();
         
        }
        else
        {
            grdTask.DataSource = dsts;
            grdTask.DataBind();
            
        }
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTask.PageIndex = e.NewPageIndex;
        this.FillTask();
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