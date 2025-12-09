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
public partial class MasterFiles_Task_Management_New_Task_Status : System.Web.UI.Page
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
               // lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "1")
            {
               liassign.Visible = false;
           //     lblsfName.Visible = false;
              //  ddlSF.Visible = false;
            }
            FillMRManagers();
            FillMode();
            FillCountAll();
            New_Task();

            Session["sf_code_Tem"] = ddlSF.SelectedValue;
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].Trim() == "New")
                {
                    FillCountAll();
                    New_Task();
                }
                else if (Request.QueryString["type"].Trim() == "Open")
                {
                    FillCountAll();
                    Open_Task();
                }
                else if (Request.QueryString["type"].Trim() == "Com")
                {
                    FillCountAll();
                    Com_Task();
                }
                else if (Request.QueryString["type"].Trim() == "Close")
                {
                    FillCountAll();
                    Close_Task();
                }
                else if (Request.QueryString["type"].Trim() == "Reopen")
                {
                    FillCountAll();
                    ReOpen_Task();

                }
                else if (Request.QueryString["type"].Trim() == "Hold")
                {
                    FillCountAll();
                    Hold_Task();
                }
            }
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
            ddlSF.Items[1].Attributes.Add("style", "background:lightblue");
            if (Session["sf_code_Tem"] != null)
            {
                ddlSF.SelectedValue = Session["sf_code_Tem"].ToString();
            }
            if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                ddlSF.Items.RemoveAt(0);
                ddlSF.Items.Insert(0, new ListItem("Team Task (ALL)", "0"));
                ddlSF.Items[0].Attributes.Add("style", "background:lightgray");
            }
          
        }
        // FillColor();


    }
    private void FillCountAll()
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
            sProc_Name = "Task_Self_Graph_Home_Self";

        }
        else
        {
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Self_Graph_Home_ALL";
            }

            else if (ddlSF.SelectedValue == Session["sf_code"].ToString() && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Self_Graph_Home_Self";
            }
            else if (ddlSF.SelectedItem.Text.Trim() == "Team (Assigned by Me)")
            {
                sProc_Name = "Task_Self_Graph_Home_Team";
            }
            else
            {
                sProc_Name = "Task_Self_Graph_Home_From_Mgr";

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
      
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dst = new DataSet();
        da.Fill(dst);
        con.Close();


        if (dst.Tables[0].Rows.Count > 0)
        {
            New.InnerText = dst.Tables[0].Rows[0]["New"].ToString();
            pen.InnerText = dst.Tables[0].Rows[0]["Open"].ToString();
            Com.InnerText = dst.Tables[0].Rows[0]["Completed"].ToString();
            close.InnerText = dst.Tables[0].Rows[0]["Closed"].ToString();
            Reopen.InnerText = dst.Tables[0].Rows[0]["ReOpen"].ToString();
            Hold.InnerText = dst.Tables[0].Rows[0]["Hold"].ToString();
            Cancel.InnerText = dst.Tables[0].Rows[0]["Cancel"].ToString();
          
        }
        else
        {
           
           
        }

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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == Session["sf_code"].ToString() && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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
            divid.Visible = false;
           // divnew.Attributes.Add("class", "scroll");
          
            grdNew.DataSource = dstNew;
            grdNew.DataBind();
          //  New.InnerText =  dstNew.Tables[0].Rows.Count.ToString();

           // New.Attributes.Add("class", "blink");
        }
        else
        {
            divid.Visible = true;
        //    divnew.Attributes.Add("class", "none");
            grdNew.DataSource = dstNew;
            grdNew.DataBind();
         //   New.InnerText = "( 0 )";
         //  New.Attributes.Add("class", "none");
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == Session["sf_code"].ToString() && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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
            divid.Visible = false;
            grdOpen.DataSource = dsts;
            grdOpen.DataBind();
          //  pen.InnerText = dsts.Tables[0].Rows.Count.ToString();
        }
        else
        {
            divid.Visible = true;
            grdOpen.DataSource = dsts;
            grdOpen.DataBind();
         //   pen.InnerText = "0";
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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

        if (dstC.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdComp.DataSource = dstC;
            grdComp.DataBind();
            //  pen.InnerText = dsts.Tables[0].Rows.Count.ToString();
        }
        else
        {
            divid.Visible = true;
            grdComp.DataSource = dstC;
            grdComp.DataBind();           
        }
        //if (dstC.Tables[0].Rows.Count > 0)
        //{
        //    Com.InnerText = "( " + dstC.Tables[0].Rows.Count.ToString() + " )";
        //}
        //else
        //{
        //    Com.InnerText = "( 0 )";
        //}
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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

        if (dstCl.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdClose.DataSource = dstCl;
            grdClose.DataBind();        
        }
        else
        {
            divid.Visible = true;
            grdClose.DataSource = dstCl;
            grdClose.DataBind();
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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

        if (dstR.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdReopen.DataSource = dstR;
            grdReopen.DataBind();
        }
        else
        {
            divid.Visible = true;
            grdReopen.DataSource = dstR;
            grdReopen.DataBind();
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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

        if (dstH.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdHold.DataSource = dstH;
            grdHold.DataBind();
        }
        else
        {
            divid.Visible = true;
            grdHold.DataSource = dstH;
            grdHold.DataBind();
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
            if (ddlSF.SelectedItem.Text.Trim() == "Team Task (ALL)")
            {
                sProc_Name = "Task_Status_Updation_ALL";
            }

            else if (ddlSF.SelectedValue == sf_code && ddlSF.SelectedItem.Text.Trim() != "Team (Assigned by Me)")
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

        if (dstCan.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdCancel.DataSource = dstCan;
            grdCancel.DataBind();
        }
        else
        {
            divid.Visible = true;
            grdCancel.DataSource = dstCan;
            grdCancel.DataBind();
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            // Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue'");
            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            //e.Row.ToolTip = "Click to view Task";
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
                lblPriority.Style.Add("color", "Red");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Medium")
            {
                lblPriority.ForeColor = System.Drawing.Color.Black;
                lblPriority.Style.Add("color", "Orange");
                lblPriority.Style.Add("font-size", "14pt");
                lblPriority.Style.Add("font-weight", "Bold");
            }
            if (lblPriority.Text == "Low")
            {
                lblPriority.ForeColor = System.Drawing.Color.White;
                lblPriority.Style.Add("color", "Green");
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
        ddlSF.Items[0].Attributes.Add("style", "background:lightgray");
        ddlSF.Items[1].Attributes.Add("style", "background:lightblue");
        FillCountAll();
        New_Task();
      //  FillNew();
      //  FillPending();
       // FillCompleted();
        //FillClose();
       // FillReopen();
       // FillHold();
      //  FillCancel();

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
    private void New_Task()
    {
        iNew.Attributes.Add("class", "active");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = true;
        grdOpen.Visible = false;
        grdComp.Visible = false;
        grdReopen.Visible = false;
        grdClose.Visible = false;
        grdHold.Visible = false;
        grdCancel.Visible = false;
        grdNew.PageIndex = 0;
        FillNew();
    }
    protected void New_Click(object sender, EventArgs e)
    {
        New_Task();
      
    }
    private void Open_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "active");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = false;
        grdOpen.Visible = true;
        grdComp.Visible = false;
        grdReopen.Visible = false;
        grdClose.Visible = false;
        grdHold.Visible = false;
        grdCancel.Visible = false;
        grdOpen.PageIndex = 0;
        FillPending();

    }
    protected void Open_Click(object sender, EventArgs e)
    {
        Open_Task();
       
    }
    private void Com_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "active");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = false;
        grdOpen.Visible = false;
        grdComp.Visible = true;
        grdReopen.Visible = false;
        grdClose.Visible = false;
        grdHold.Visible = false;
        grdCancel.Visible = false;
        grdComp.PageIndex = 0;
        FillCompleted();
    }
    protected void Com_Click(object sender, EventArgs e)
    {
        Com_Task();       
    }
    private void Close_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "active");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = false;
        grdOpen.Visible = false;
        grdComp.Visible = false;
        grdReopen.Visible = false;
        grdClose.Visible = true;
        grdHold.Visible = false;
        grdCancel.Visible = false;
        grdClose.PageIndex = 0;
        FillClose();
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        Close_Task();
    }
    private void ReOpen_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "active");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = false;
        grdOpen.Visible = false;
        grdComp.Visible = false;
        grdReopen.Visible = true;
        grdClose.Visible = false;
        grdHold.Visible = false;
        grdCancel.Visible = false;
        grdReopen.PageIndex = 0;
        FillReopen();
    }
    protected void ReOpen_Click(object sender, EventArgs e)
    {
        ReOpen_Task();       
    }
    private void Hold_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "active");
        iCancel.Attributes.Add("class", "None");
        grdNew.Visible = false;
        grdOpen.Visible = false;
        grdComp.Visible = false;
        grdReopen.Visible = false;
        grdClose.Visible = false;
        grdHold.Visible = true;
        grdCancel.Visible = false;
        grdHold.PageIndex = 0;
        FillHold();
    }
    protected void Hold_Click(object sender, EventArgs e)
    {
        Hold_Task();
    }
    private void Cancel_Task()
    {
        iNew.Attributes.Add("class", "None");
        ipen.Attributes.Add("class", "None");
        icom.Attributes.Add("class", "None");
        iclose.Attributes.Add("class", "None");
        iRopen.Attributes.Add("class", "None");
        iHold.Attributes.Add("class", "None");
        iCancel.Attributes.Add("class", "active");
        grdNew.Visible = false;
        grdOpen.Visible = false;
        grdComp.Visible = false;
        grdReopen.Visible = false;
        grdClose.Visible = false;
        grdHold.Visible = false;
        grdCancel.Visible = true;
        grdCancel.PageIndex = 0;
        FillCancel();
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Cancel_Task();

    }
    protected void grdNew_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNew.PageIndex = e.NewPageIndex;
        this.FillNew();
    }
    protected void grdOpen_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOpen.PageIndex = e.NewPageIndex;
        this.FillPending();
    }
    protected void grdComp_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdComp.PageIndex = e.NewPageIndex;
        this.FillCompleted();
    }
    protected void grdClose_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClose.PageIndex = e.NewPageIndex;
        this.FillClose();
    }
    protected void grdReopen_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReopen.PageIndex = e.NewPageIndex;
        this.FillReopen();
    }
    protected void grdCancel_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCancel.PageIndex = e.NewPageIndex;
        this.FillCancel();
    }
    protected void grdHold_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHold.PageIndex = e.NewPageIndex;
        this.FillHold();
    }
}