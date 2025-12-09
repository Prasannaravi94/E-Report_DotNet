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
public partial class MasterFiles_Task_Management_Task_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dssf = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTask = new DataSet();
    string newSFCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
       
        if (Session["sf_code_Tem"] != null)
        {
            newSFCode = Session["sf_code_Tem"].ToString();
        }
        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "1")
            {
                liassign.Visible = false;
                lblsfName.Visible = false;
                ddlSF.Visible = false;
            }
            FillMRManagers();
            FillMode();
            FillNew();
            FillPending();
            FillCompleted();
            FillClose();
            FillReopen();
            FillHold();
            FillCancel();
        }
        if (Session["sf_type"].ToString() == "2")
        {


        }
    }
    private void FillMode()
    {
        try
        {
            Task tsk = new Task();
            dsTask = tsk.getTaskMode_DV(div_code);
            if (dsTask.Tables[0].Rows.Count > 0)
            {
                ddlmode.DataValueField = "Mode_ID";
                ddlmode.DataTextField = "Mode_Name";
                ddlmode.DataSource = dsTask;
                ddlmode.DataBind();
            }
            ddlmode.Items.Insert(0, new ListItem("ALL", "0"));
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
            for (int k = 0; k < dsSalesForce.Tables[0].Rows.Count; k++)
            {
                if (dsSalesForce.Tables[0].Rows[k]["sf_code"].ToString() == sf_code)
                {
                    ddlSF.SelectedItem.Text = ddlSF.SelectedItem.Text + " (My Task)";
                    ddlSF.Items[k].Attributes.Add("style", "background:lightblue");

                }
            }

            ddlSF.Items.Insert(1, new ListItem("Team (Assigned by Me)", "1"));
            if (Session["sf_code_Tem"] != null)
            {
                ddlSF.SelectedValue = Session["sf_code_Tem"].ToString();
            }
            if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                ddlSF.Items.RemoveAt(0);
            }
          
        }
        // FillColor();


    }
    private void FillNew()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";
        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";

        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {

            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }

        cmd.Parameters.AddWithValue("@status", 1);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstNew = new DataSet();
        da.Fill(dstNew);
        con.Close();


        if (dstNew.Tables[0].Rows.Count > 0)
        {

            grdNew.DataSource = dstNew;
            grdNew.DataBind();
            New.InnerText = "( " + dstNew.Tables[0].Rows.Count.ToString() + " )";

            New.Attributes.Add("class", "blink");
        }
        else
        {
            grdNew.DataSource = dstNew;
            grdNew.DataBind();
            New.InnerText = "( 0 )";
            New.Attributes.Add("class", "none");
        }

    }
    private void FillPending()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";
        }
        else
        {

            if (ddlSF.SelectedValue == Session["sf_code"].ToString() && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {

            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }

        cmd.Parameters.AddWithValue("@status", 2);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();


        if (dsts.Tables[0].Rows.Count > 0)
        {
            grdOpen.DataSource = dsts;
            grdOpen.DataBind();
            pen.InnerText = "( " + dsts.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            grdOpen.DataSource = dsts;
            grdOpen.DataBind();
            pen.InnerText = "( 0 )";
        }
    }
    private void FillCompleted()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";
        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {
            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }

        cmd.Parameters.AddWithValue("@status", 3);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstC = new DataSet();
        da.Fill(dstC);
        con.Close();

        grdComp.DataSource = dstC;
        grdComp.DataBind();
        if (dstC.Tables[0].Rows.Count > 0)
        {
            Com.InnerText = "( " + dstC.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            Com.InnerText = "( 0 )";
        }
    }
    private void FillClose()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";

        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {
            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }
        cmd.Parameters.AddWithValue("@status", 4);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstCl = new DataSet();
        da.Fill(dstCl);
        con.Close();

        grdClose.DataSource = dstCl;
        grdClose.DataBind();
        if (dstCl.Tables[0].Rows.Count > 0)
        {
            close.InnerText = "( " + dstCl.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            close.InnerText = "( 0 )";
        }
    }
    private void FillReopen()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";

        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {
            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }
        cmd.Parameters.AddWithValue("@status", 5);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstR = new DataSet();
        da.Fill(dstR);
        con.Close();

        grdReopen.DataSource = dstR;
        grdReopen.DataBind();
        if (dstR.Tables[0].Rows.Count > 0)
        {
            Reopen.InnerText = "( " + dstR.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            Reopen.InnerText = "( 0 )";
        }
    }
    private void FillHold()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";
        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";

            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {
            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }
        cmd.Parameters.AddWithValue("@status", 6);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstH = new DataSet();
        da.Fill(dstH);
        con.Close();

        grdHold.DataSource = dstH;
        grdHold.DataBind();
        if (dstH.Tables[0].Rows.Count > 0)
        {
            Hold.InnerText = "( " + dstH.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            Hold.InnerText = "( 0 )";
        }
    }
    private void FillCancel()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (Session["sf_type"].ToString() == "1")
        {
            sProc_Name = "Task_Status_Updation_Self";
        }
        else
        {

            if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Status_Updation";
            }
            else
            {
                sProc_Name = "Task_Status_Updation_Others";
            }
        }

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        if (Session["sf_type"].ToString() == "1")
        {
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
        }
        else
        {
            if ((ddlSF.SelectedValue != Session["sf_code"].ToString()) && ddlSF.SelectedValue != "1")
            {
                cmd.Parameters.AddWithValue("@sf_code", ddlSF.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mgr_code", Session["sf_code"].ToString());
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sf_code", sf_code);
                cmd.Parameters.AddWithValue("@Prior", ddlPri.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue.Trim());
            }
        }
        cmd.Parameters.AddWithValue("@status", 7);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstCan = new DataSet();
        da.Fill(dstCan);
        con.Close();

        grdCancel.DataSource = dstCan;
        grdCancel.DataBind();
        if (dstCan.Tables[0].Rows.Count > 0)
        {
            Cancel.InnerText = "( " + dstCan.Tables[0].Rows.Count.ToString() + " )";
        }
        else
        {
            Cancel.InnerText = "( 0 )";
        }
    }
    protected void grdNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            // Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.ToolTip = "Click to view Task";
        }
    }
    protected void grdOpen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }

    protected void grdComp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }
    protected void grdClose_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }
    protected void grdReopen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }
    protected void grdHold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }
    protected void grdCancel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPriority = (Label)e.Row.FindControl("lblPri");
            if (lblPriority.Text == "High")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("background", "Yellow");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("background", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
        }
    }
    //protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    //{
    //    if (TabContainer1.ActiveTabIndex == 1)
    //    {

    //    }
    //}
    protected void btnGo_Click(object sender, EventArgs e)
    {


        Session["sf_code_Tem"] = ddlSF.SelectedValue;
      
        FillNew();
        FillPending();
        FillCompleted();
        FillClose();
        FillReopen();
        FillHold();
        FillCancel();

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