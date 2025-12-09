using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_Dashboard_MGR_Working_Hrs_View2 : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string sCurrentDate = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string parameter = string.Empty;
    DataSet dsts = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    #endregion
    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
        }
        catch
        {
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            cFMnth = Request.QueryString["cMnth"].ToString();
            cFYear = Request.QueryString["cYr"].ToString();
            cTMnth = Request.QueryString["cMnth"].ToString();
            cTYear = Request.QueryString["cYr"].ToString();
            
        }

        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {
          
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                //ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }
            }
             //DateTime dateTime = DateTime.UtcNow.Date;
            ddlYear.SelectedValue = cFYear;
            ddlMonth.SelectedValue = cFMnth;
            ddlDay.SelectedValue = DateTime.Today.Day.ToString();
			if (sf_type == "1" || sf_type == "MR")
            {
				FillReport();
			}
			else{
				FillReportMGR();
			}
        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Hierarchy_Team(div_code, ddlFieldForce.SelectedValue);
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int i = dsSalesForce.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dsSalesForce.Tables[0].Rows[i];
                if (dr["sf_code"].ToString() == "admin")
                    dr.Delete();
            }

            dsSalesForce.AcceptChanges();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    #endregion
    
    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0"
            && ddlFieldForce.SelectedValue != ""
            && ddlMonth.SelectedValue != "0")
        {
            if (ddlFieldForce.SelectedValue.Contains("MR"))
            {
                GrdTimeSt.DataSource = null;
                GrdTimeSt.DataBind();
                if (ddlDay.SelectedValue == "ALL")
                {

                    FillReportMonth();
                }
                else
                {
                    FillReport();
                }
            }
            else
            {
                if (ddlDay.SelectedValue == "ALL")
                {

                    FillReportMonthMGR();
                }
                else
                {
                    FillReportMGR();
                }
            }
        }
    }
    #endregion

    #region FillReport
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        //string[] msg = ddlFieldForce.SelectedItem.Text.Split('-');

        dsSalesForce = sf.getSFCodeInfo(ddlFieldForce.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
            lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
            lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
            lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
            lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
        }
        else
        { tblMsgInfo.Visible = false; }

        string sProc_Name = "Sp_Time_StatusDaywise";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@FMonth", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@FYear", ddlYear.SelectedValue);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Days", ddlDay.SelectedValue);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        DataSet dsWorkType = sf.getSFCodeWorkType(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, ddlDay.SelectedValue);
        if (dsWorkType.Tables[0].Rows.Count > 0)
        {
            lblWorkType.Text = "<b style='color:#245884'>Work Type:</b>" + dsWorkType.Tables[0].Rows[0]["WorkType_Name"];
        }
        else { lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -"; }
        GrdTimeSt.DataSource = dtrowClr;
        GrdTimeSt.DataBind();

        //else
        //{
        //    tblMsgInfo.Visible = false;
        //    GrdTimeSt.DataSource = null;
        //    GrdTimeSt.DataBind();
        //}
    }
    #endregion
    private void FillReportMGR()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        string[] msg = ddlFieldForce.SelectedItem.Text.Split('-');

        dsSalesForce = sf.getSFCodeInfo(ddlFieldForce.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
            lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
            lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
            lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
            lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
        }
        else
        { tblMsgInfo.Visible = false; }

        string sProc_Name = "Sp_Time_StatusDaywiseMGR";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@FMonth", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@FYear", ddlYear.SelectedValue);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Days", ddlDay.SelectedValue);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        DataSet dsWorkType = sf.getSFCodeWorkType(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, ddlDay.SelectedValue);
        if (dsWorkType.Tables[0].Rows.Count > 0)
        {
            lblWorkType.Text = "<b style='color:#245884'>Work Type:</b>" + dsWorkType.Tables[0].Rows[0]["WorkType_Name"];
        }
        else { lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -"; }
        GrdTimeSt.DataSource = dtrowClr;
        GrdTimeSt.DataBind();
    }
    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion

    private void FillReportMonth()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSFCodeInfo(ddlFieldForce.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
            lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
            lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
            lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
            lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
            lblWorkType.Text = "";
        }
        else { tblMsgInfo.Visible = false; }

        int months = (Convert.ToInt32(ddlYear.SelectedValue) - Convert.ToInt32(ddlYear.SelectedValue)) * 12 + Convert.ToInt32(ddlMonth.SelectedValue) - Convert.ToInt32(ddlMonth.SelectedValue);
        int cmonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int cyear = Convert.ToInt32(ddlYear.SelectedValue);


        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        //

        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Sp_Time_StatusMonth", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 6000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();

        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("valuess");
        dsts.Tables[0].Columns.Remove("valuess1");

        GrdTimeSt.DataSource = dsts;
        GrdTimeSt.DataBind();
    }

    private void FillReportMonthMGR()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSFCodeInfo(ddlFieldForce.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
            lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
            lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
            lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
            lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
            lblWorkType.Text = "";
        }
        else { tblMsgInfo.Visible = false; }

        int months = (Convert.ToInt32(ddlYear.SelectedValue) - Convert.ToInt32(ddlYear.SelectedValue)) * 12 + Convert.ToInt32(ddlMonth.SelectedValue) - Convert.ToInt32(ddlMonth.SelectedValue);
        int cmonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int cyear = Convert.ToInt32(ddlYear.SelectedValue);


        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        //

        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Sp_Time_StatusMonthMGR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 6000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();

        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("valuess");
        dsts.Tables[0].Columns.Remove("valuess1");

        GrdTimeSt.DataSource = dsts;
        GrdTimeSt.DataBind();
    }

    protected void GrdTimeSt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            for (int l = 2, j = 0; l < e.Row.Cells.Count; l++)
            {

             
                HyperLink hLink = new HyperLink();
                hLink.Text = e.Row.Cells[l].Text;
                string parameter = dtrowClr.Rows[indx][1].ToString();
                string Date = ddlDay.SelectedValue == "" ? "" : ddlDay.SelectedValue;
                if (parameter == "Total No. of Dr. calls" || parameter == "Total No. Of Dr Calls")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','Drs')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total No.of Chemist" || parameter =="Total No. Of Chemst Calls")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','Chemist')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total No.of Reminder" || parameter == "Total No. of Reminders")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','Reminder')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }
                if (parameter == "No.of Drs in the list")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrsList')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }
                
                if (parameter == "Total No.Of Field Work Days" || parameter == "Field work Day")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','FWDays')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }
                if (parameter == "Total No. of Drs Visited once")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','VisitDrsOne')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }
                if (parameter == "Total No. of Drs Visited >=2")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','VisitDrsTwo')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total No. of Drs Not Covered")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrMet')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total no of Core Drs called")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrsCoreSeen')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total No. of Core Drs in the list")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrsCoreList')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }

                if (parameter == "Total No. of Core Drs Doctor Visited")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrsCoreVisit')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }


                if (parameter == "Total No. of Core Drs Not Covered")
                {
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-")
                    {
                        hLink.Attributes.Add("href", "javascript:showTimeStatusZoom('" + ddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "', '" + ddlYear.SelectedValue + "','" + Date + "','DrsCoreMissed')");

                        hLink.Style.Add("text-decoration", "none");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[2].Controls.Add(hLink);
                    }
                }
            }

            #endregion

        }
    }
}