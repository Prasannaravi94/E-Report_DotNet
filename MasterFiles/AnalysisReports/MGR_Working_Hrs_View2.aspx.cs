using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_AnalysisReports_MGR_Working_Hrs_View2 : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataSet dsts = new DataSet();
    DataTable dtrowClr = new DataTable();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                FillMRManagers();
                FillYear();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["division_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
            }
        }
    }
    private void FillMRManagers()
    {
        //Territory sf = new Territory();
        //dsSalesForce = sf.getSFCode(div_code);
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");

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
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.Contains("MR"))
        {
            GrdTimeSt.DataSource = null;
            GrdTimeSt.DataBind();
            if (ddlDay.SelectedValue == "ALL")
            {
                //Response.Write("test1");
                FillReportMonth();
            }
            else
            {
                //Response.Write("test2");
                FillReport();
            }
        }
        else
        {
            if (ddlDay.SelectedValue == "ALL")
            {
                //Response.Write("test3");
                FillReportMonthMGR();
            }
            else
            {
                //Response.Write("test4");
                FillReportMGR();
            }
        }
    }
    private void FillReport()
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
        if (div_code != string.Empty)
        {
            div_code = Session["div_code"].ToString();
        }
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
        if (div_code != string.Empty)
        {
            div_code = Session["div_code"].ToString();
        }
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
            lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -";
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
        if (div_code != string.Empty)
        {
            div_code = Session["div_code"].ToString();
        }
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
            lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -";
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
        if (div_code != string.Empty)
        {
            div_code = Session["div_code"].ToString();
        }
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

                if (parameter == "Total No.of Chemist" || parameter == "Total No. Of Chemst Calls")
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