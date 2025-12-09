using Bus_EReport;
using DBase_EReport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterFiles_AnalysisReports_rptMGRWorking_Hrs_ViewZoom : System.Web.UI.Page
{
    DataSet dsDoctor = null;

    string sMode = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string Mnth = string.Empty;
    string Year = string.Empty;
    string Day = string.Empty;
    string IsDash = string.Empty;
    string sf_type = string.Empty;
    string parameter = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //div_code = Session["div_code"].ToString();
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            Mnth = Request.QueryString["Month"].ToString();
            Year = Request.QueryString["Year"].ToString();
            Day = Request.QueryString["Day"].ToString();
            IsDash = Request.QueryString["IsDash"].ToString();
            parameter = Request.QueryString["parameter"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();


            //hdnSf_code.Value = sf_code;
            //hdnMnth.Value = Mnth;
            //hdnYear.Value = Year;
            //hdnSf_Type.Value = sf_type;
            //hdnDiv_code.Value = div_code;
            if (IsDash == "1")
            {
                btnClose.Visible = false;
                btnExcel.Visible = false;
                btnBack.Visible = true;
            }
            else
            {
                btnClose.Visible = true;
                btnExcel.Visible = true;
                btnBack.Visible = false;
            }
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSFCodeInfo(sf_code, div_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                tblMsgInfo.Visible = true;
                sf_name = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
                lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
                lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
                lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
                lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
                lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -";
            }
            else { tblMsgInfo.Visible = false; }

            if (Day == "undefined" || Day == "ALL")
            {
                GridBindMonth();
                string dt = Convert.ToString(Convert.ToDateTime("01," + Mnth + ", 1900").ToString("MMM"));
                lblHead.Text = "Time status for the month of " + dt + " - " + Year;
            }
            else
            {
                GridBindDate();
                lblHead.Text = "Time status for the Date of " + Day + "/" + Mnth + "/" + Year;
            }
            if (parameter == "Drs")
                lblParameterHead.Text = "Total No. Of Dr Calls";
            else if (parameter == "Chemist")
                lblParameterHead.Text = "Total No.of Chemist Calls";
            else if (parameter == "Reminder")
                lblParameterHead.Text = "Total No. of Reminders";
            else if (parameter == "DrsList")
                lblParameterHead.Text = "No.of Drs in the list";
            else if (parameter == "FWDays")
                lblParameterHead.Text = "No.Of Field Work Days";
            else if (parameter == "VisitDrsOne")
                lblParameterHead.Text = "Total No. of Drs Visited once";
            else if (parameter == "VisitDrsTwo")
                lblParameterHead.Text = "Total No. of Drs Visited twice & above";
            else if (parameter == "DrMet")
                lblParameterHead.Text = "Total No. of Drs Not Covered";
            else if (parameter == "DrsCoreSeen")
                lblParameterHead.Text = "Total no of Core Drs called";
            else if (parameter == "DrsCoreList")
                lblParameterHead.Text = "Total No. of Core Drs in the list";
            else if (parameter == "DrsCoreVisit")
                lblParameterHead.Text = "Total No. of Core Drs Doctor Visited";
            else if (parameter == "DrsCoreMissed")
                lblParameterHead.Text = "Total No. of Core Drs Not Covered";
        }
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridViewMonth()
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        Mnth = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Day = Request.QueryString["Day"].ToString();
        IsDash = Request.QueryString["IsDash"].ToString();
        parameter = Request.QueryString["parameter"].ToString();

        int months = (Convert.ToInt32(Year) - Convert.ToInt32(Year)) * 12 + Convert.ToInt32(Mnth) - Convert.ToInt32(Mnth);
        int cmonth = Convert.ToInt32(Mnth);
        int cyear = Convert.ToInt32(Year);


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

        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Sp_TimeStatusTwoMonthZoom", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@parameter", parameter);
        cmd.CommandTimeout = 6000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();

        da.Fill(dsts);

        DataTable dtGrid = new DataTable();
        Doctor dv = new Doctor();
        dtGrid = dsts.Tables[0];
        return dtGrid;
    }


    private DataTable BindGridViewDate()
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        Mnth = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Day = Request.QueryString["Day"].ToString();
        IsDash = Request.QueryString["IsDash"].ToString();
        parameter = Request.QueryString["parameter"].ToString();


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        
        DataSet dstsDay = new DataSet();
        string sProc_Name = "Sp_TimeStatusTwoDateZoom";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Month", Mnth);
        cmd.Parameters.AddWithValue("@Year", Year);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Days", Day);
        cmd.Parameters.AddWithValue("@parameter", parameter);


        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dstsDay);
        

        DataTable dtGrid = new DataTable();
        Doctor dv = new Doctor();
        dtGrid = dstsDay.Tables[0];
        return dtGrid;
    }
    protected void GrdTimeSt_Sorting(object sender, GridViewSortEventArgs e)
    {
        Day = Request.QueryString["Day"].ToString();
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView();
        if (Day == "ALL")
        { sortedView = new DataView(BindGridViewMonth()); }
        else { sortedView = new DataView(BindGridViewDate()); }

        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        GrdTimeSt.DataSource = dtGrid;
        GrdTimeSt.DataBind();
        //}
    }
    #region btnback_Click
    //protected void btnback_Click(object sender, EventArgs e)
    //{
    //    div_code = Request.QueryString["div_code"].ToString();
    //    sf_code = Request.QueryString["sfcode"].ToString();
    //    Mnth = Request.QueryString["Month"].ToString();
    //    Year = Request.QueryString["Year"].ToString();
    //    Day = Request.QueryString["Day"].ToString();
    //    IsDash = Request.QueryString["IsDash"].ToString();

    //    Response.Redirect("~/MasterFiles/Dashboard_MGR_Working_Hrs_View2.aspx?sfcode=" + sf_code + "&cMnth=" + Mnth+ "&cYr=" + Year + "&cDay=" + Day + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    //}
    #endregion
    private void GridBindMonth()
    {

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

        int months = (Convert.ToInt32(Year) - Convert.ToInt32(Year)) * 12 + Convert.ToInt32(Mnth) - Convert.ToInt32(Mnth);
        int cmonth = Convert.ToInt32(Mnth);
        int cyear = Convert.ToInt32(Year);


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

        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Sp_TimeStatusTwoMonthZoom", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@parameter", parameter);
        cmd.CommandTimeout = 6000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();

        da.Fill(dsts);
        dsts.Tables[0].Columns.Remove("sno");
        dsts.Tables[0].Columns.Remove("Code");

        //dsts.Tables[0].Columns.Remove("valuess1");
        GrdTimeSt.DataSource = dsts;
        GrdTimeSt.DataBind();
        //if (parameter == "DrsList" || parameter == "VisitDrsOne" || parameter == "VisitDrsTwo" || parameter == "DrMet" || parameter == "DrsCoreList" || parameter == "DrsCoreVisit" || parameter == "DrsCoreMissed")
        //if ( parameter == "DrsCoreList" || parameter == "DrsCoreVisit" || parameter == "DrsCoreMissed")
		if (parameter == "DrsList" || parameter == "DrMet" ||parameter == "DrsCoreList" || parameter == "DrsCoreMissed")
        {
            GrdTimeSt.Columns[2].Visible = false;
            GrdTimeSt.Columns[3].Visible = false;
        }

    }

    private void GridBindDate()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        DataSet dtrowClr = null;
        DataSet dstsDay = new DataSet();

        dsSalesForce = sf.getSFCodeInfo(sf_code, div_code);
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

        string sProc_Name = "Sp_TimeStatusTwoDateZoom";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Month", Mnth);
        cmd.Parameters.AddWithValue("@Year", Year);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Days", Day);
        cmd.Parameters.AddWithValue("@parameter", parameter);


        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dstsDay);
        DataSet dsWorkType = sf.getSFCodeWorkType(sf_code, div_code, Mnth, Year, Day);
        if (dsWorkType.Tables[0].Rows.Count > 0)
        {
            lblWorkType.Text = "<b style='color:#245884'>Work Type:</b>" + dsWorkType.Tables[0].Rows[0]["WorkType_Name"];
        }
        else { lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -"; }
        dstsDay.Tables[0].Columns.Remove("sno");
        dstsDay.Tables[0].Columns.Remove("Code");
        GrdTimeSt.DataSource = dstsDay;
        GrdTimeSt.DataBind();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();


    }

    public override void VerifyRenderingInServerForm(Control txt_salutaion)
    {
        /* Verifies that the control is rendered */
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string Export = this.Page.Title;
        string attachment = "attachment; filename=" + Export + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

}