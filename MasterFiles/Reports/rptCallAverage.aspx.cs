using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Net;


public partial class Reports_rptCallAverage : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string strMode = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string spclty = string.Empty;
    SalesForce sf = new SalesForce();
    DCR dcc = new DCR();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTotalQuery = new DataSet();
    DataSet dsChemist = new DataSet();
    DataSet dsUnListDoc = new DataSet();
    DataSet dsStock = new DataSet();
    DataSet dsDoc = new DataSet();
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doctor_total_tot = 0;
    int doctor_totals = 0;
    int doctor_total_tots = 0;
    int doctor_total2 = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_met_total2 = 0;
    int doc_calls_seen_total = 0;
    int doc_calls_seen_total2 = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Reminder_tot = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_Remi_Count = string.Empty;
    string tot_dr = string.Empty;
    string tot_doctor = string.Empty;
    string tot_doctors = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string chkVacant = string.Empty;
    string chkMgr = string.Empty;
    string strFMonthName = string.Empty;
    string strTMonthName = string.Empty;
    DateTime dtFrmDate;
    DateTime dtToDate;
    DB_EReporting dbcall = new DB_EReporting();

    string sCurrentDate = string.Empty;


    //new
    decimal sub_days = 0;
    decimal fldwrk_days = 0;
    decimal leave_days = 0;
    decimal doc_met = 0;
    decimal doc_made = 0;
    decimal lst_drs = 0;
    decimal totcovg = 0;
    decimal chmvst = 0;
    decimal stkvst = 0;
    decimal specmas = 0;
    decimal speccallmet = 0;
    decimal speccallseen = 0;
    int vacflag = 99;
    //new

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        divcode = Request.QueryString["div_Code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        strMode = Request.QueryString["Mode"].ToString();
        chkMgr = Request.QueryString["chkMgr"].ToString();
        chkVacant = Request.QueryString["chkVacant"].ToString();

        vacflag = 99;
        if (chkVacant.ToString()=="0")
        {
            vacflag = 1;
        }

        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
            sfname = Session["sf_name"].ToString();
        }

        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

        //if (!Page.IsPostBack)
        //{
        //BindGrid();
        if (strMode == "MonthWise")
        {
            iMonth = Convert.ToInt32(Request.QueryString["frm_month"].ToString());
            iYear = Convert.ToInt32(Request.QueryString["frm_year"].ToString());
            
            FillSF();

            string strMonthName = mfi.GetMonthName(iMonth).ToString();
            lblMonth.Text = strMonthName;
            lblYear.Text = iYear.ToString();
         
        }
        else if (strMode == "Periodically")
        {
            FYear = Request.QueryString["frm_year"].ToString();
            FMonth = Request.QueryString["frm_month"].ToString();
            TYear = Request.QueryString["To_Year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            FillPeriodically();

            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblHead.Text = "Listed Doctor Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;
        }
        else if (strMode == "Periodically All Field Force")
        {
            FYear = Request.QueryString["frm_year"].ToString();
            FMonth = Request.QueryString["frm_month"].ToString();
            TYear = Request.QueryString["To_Year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            FillPeriodically_FieldForce();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblHead.Text = "Listed Doctor Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;
        }

        else if (strMode == "Date Wise")
        {
            dtFrmDate = Convert.ToDateTime(Request.QueryString["frm_month"].ToString());
            dtToDate = Convert.ToDateTime(Request.QueryString["frm_year"].ToString());
            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;
            lblHead.Text = lblHead.Text + " between " + dtFrmDate.ToString().Substring(0, 10) + " and " + dtToDate.ToString().Substring(0, 10);
            FillDateWise();
        }
        else if (strMode == "Date Wise(for App Approval)")
        {
            dtFrmDate = Convert.ToDateTime(Request.QueryString["frm_month"].ToString());
            dtToDate = Convert.ToDateTime(Request.QueryString["frm_year"].ToString());
            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;
            lblHead.Text = lblHead.Text + " between " + dtFrmDate.ToString().Substring(0, 10) + " and " + dtToDate.ToString().Substring(0, 10);
            FillDateWise_WithOut_Approval();
        }

        ExportButton();

        //}

        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect(ex.Message);
        //}
    }
    private void ExportButton()
    {
        btnPDF.Visible = false;
    }
    private void BindSf_Code()
    {
        try
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                DataSet DsAudit = ds.SF_Hierarchy(divcode, sfCode);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    //DataTable dt = sf.getUserListReportingTo(divcode, sfCode, 0);
                    //dsmgrsf.Tables.Add(dt);
                    //dsSalesForce = dsmgrsf;
                    dsSalesForce = sf.CallAvergeVacant(divcode, sfCode, chkVacant);
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                divcode = Session["div_code"].ToString();

                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                DataSet DsAudit = ds.SF_Hierarchy(divcode, sfCode);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    //DataTable dt = sf.getUserListReportingTo(divcode, sfCode, 0);
                    //dsmgrsf.Tables.Add(dt);
                    //dsSalesForce = dsmgrsf;
                    dsSalesForce = sf.CallAvergeVacant(divcode, sfCode, chkVacant);
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
            }

            else if (Session["sf_type"].ToString() == "1")
            {
                divcode = Session["div_code"].ToString();
                dsSalesForce = sf.sp_UserMRLogin(divcode, sfCode);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        doctor_total2 = 0;
        BindSf_Code();
        DataView dvCall = new DataView();

        if (chkMgr == "0" && Session["sf_type"].ToString() != "1")
        {
            //dsSalesForce.Clear();
            dsSalesForce.Tables[0].DefaultView.RowFilter = "sf_Type=2 ";
            dvCall = dsSalesForce.Tables[0].DefaultView;

            DataTable dt = dvCall.Table.DefaultView.ToTable();
            dsSalesForce.Clear();
            dsSalesForce.Merge(dt);
            //lblHead.Text = "Listed Doctor Manager Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            if (strMode == "MonthWise")
            {
                string spclty = Request.QueryString["cbTxt"].ToString();
                spclty = spclty.Remove(spclty.LastIndexOf(','));
                string[] ttlSpc = spclty.Split(',');

                string sQrymonthwise = "SELECT Distinct Doc_Special_Code as Code, Doc_Special_SName as Short_Name FROM Mas_Doctor_Speciality " +
               "WHERE Division_Code='" + divcode + "' AND Doc_Special_Active_Flag='0' and Doc_Special_Code in (select Data from dbo.split('" + spclty + "',',')) ORDER BY 1";
                DB_EReporting db = new DB_EReporting();
                DataTable dtstkmas = db.Exec_DataTable(sQrymonthwise);

                TableRow tr_header = new TableRow();
                ////tr_header.BorderStyle = BorderStyle.Solid;
                ////tr_header.BorderWidth = 1;


                TableCell tc_SNo = new TableCell();
                tc_SNo.Width = 50;
                tc_SNo.RowSpan = 2;
                Literal lit_SNo = new Literal();
                //// lit_SNo.Text = "#";
                lit_SNo.Text = "#";
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
                tc_SNo.Style.Add("color", "white");
                tc_SNo.Style.Add("font-weight", "400");
                tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
                tc_SNo.Style.Add("font-size", "12px");
                tc_SNo.Style.Add("border-bottom", "0px solid #fff");
                tc_SNo.HorizontalAlign = HorizontalAlign.Center;
                tc_SNo.Style.Add("font-family", "Roboto");
                tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
                tc_SNo.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_SNo);
                tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");

                TableCell tc_DR_Code = new TableCell();
                tc_DR_Code.Width = 40;
                tc_DR_Code.RowSpan = 2;
                Literal lit_DR_Code = new Literal();
                lit_DR_Code.Text = "<center>SF Code</center>";
                tc_DR_Code.Controls.Add(lit_DR_Code);
                tc_DR_Code.Visible = false;
                tr_header.Cells.Add(tc_DR_Code);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.Width = 200;
                tc_DR_Name.RowSpan = 2;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Field Force</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tc_DR_Name.Style.Add("padding", "15px 5px");
                tc_DR_Name.Style.Add("border-bottom", "0px solid #fff");
                tc_DR_Name.Style.Add("border-top", "0px");
                tc_DR_Name.Style.Add("font-size", "12px");
                tc_DR_Name.Style.Add("font-weight", "400");
                tc_DR_Name.Style.Add("text-align", "center");
                tc_DR_Name.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_Name.Style.Add("vertical-align", "inherit");
                tc_DR_Name.Style.Add("text-transform", "uppercase");
                tc_DR_Name.Attributes.Add("class", "stickyFirstRow");

                tr_header.Cells.Add(tc_DR_Name);

               

                TableCell tc_DR_DOJ = new TableCell();
                tc_DR_DOJ.Width = 100;
                tc_DR_DOJ.RowSpan = 2;
                Literal lit_DR_DOJ = new Literal();
                lit_DR_DOJ.Text = "<center>DOJ</center>";
                tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
                tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_DOJ.Style.Add("padding", "15px 5px");
                tc_DR_DOJ.Style.Add("border-bottom", "0px solid #fff");
                tc_DR_DOJ.Style.Add("border-top", "0px");
                tc_DR_DOJ.Style.Add("font-size", "12px");
                tc_DR_DOJ.Style.Add("font-weight", "400");
                tc_DR_DOJ.Style.Add("text-align", "center");
                tc_DR_DOJ.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_DOJ.Style.Add("vertical-align", "inherit");
                tc_DR_DOJ.Style.Add("text-transform", "uppercase");
                tc_DR_DOJ.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_DOJ);

                TableCell tc_DR_LastDCRDate = new TableCell();
                tc_DR_LastDCRDate.Width = 100;
                tc_DR_LastDCRDate.RowSpan = 2;
                Literal lit_DR_LastDCRDate = new Literal();
                lit_DR_LastDCRDate.Text = "<center>Last DCR Date</center>";
                tc_DR_LastDCRDate.Controls.Add(lit_DR_LastDCRDate);
                tc_DR_LastDCRDate.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_LastDCRDate.Style.Add("padding", "15px 5px");
                tc_DR_LastDCRDate.Style.Add("border-bottom", "0px solid #fff");
                tc_DR_LastDCRDate.Style.Add("border-top", "0px");
                tc_DR_LastDCRDate.Style.Add("font-size", "12px");
                tc_DR_LastDCRDate.Style.Add("font-weight", "400");
                tc_DR_LastDCRDate.Style.Add("text-align", "center");
                tc_DR_LastDCRDate.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_LastDCRDate.Style.Add("vertical-align", "inherit");
                tc_DR_LastDCRDate.Style.Add("text-transform", "uppercase");
                tc_DR_LastDCRDate.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_LastDCRDate);

                TableCell tc_DR_sf_Emp_ID = new TableCell();
                tc_DR_sf_Emp_ID.Width = 100;
                tc_DR_sf_Emp_ID.RowSpan = 2;
                Literal lit_DR_SF_Emp_ID = new Literal();
                lit_DR_SF_Emp_ID.Text = "<center>Employee ID</center>";
                tc_DR_sf_Emp_ID.Controls.Add(lit_DR_SF_Emp_ID);
                tc_DR_sf_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_sf_Emp_ID.Style.Add("padding", "15px 5px");
                tc_DR_sf_Emp_ID.Style.Add("border-bottom", "0px solid #fff");
                tc_DR_sf_Emp_ID.Style.Add("border-top", "0px");
                tc_DR_sf_Emp_ID.Style.Add("font-size", "12px");
                tc_DR_sf_Emp_ID.Style.Add("font-weight", "400");
                tc_DR_sf_Emp_ID.Style.Add("text-align", "center");
                tc_DR_sf_Emp_ID.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_sf_Emp_ID.Style.Add("vertical-align", "inherit");
                tc_DR_sf_Emp_ID.Style.Add("text-transform", "uppercase");
                tc_DR_sf_Emp_ID.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_sf_Emp_ID);

                int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


                ViewState["months"] = months;


                Doctor dr = new Doctor();
                dsDoctor = dr.getDocCat(divcode);


               // tbl.Rows.Add(tr_header);

                //Sub Header
                months = Convert.ToInt16(ViewState["months"].ToString());

                if (months > 0)
                {

                    //TableRow tr_lst_det = new TableRow();
                    TableCell tc_DR_DCR_SubDays = new TableCell();
                    tc_DR_DCR_SubDays.Width = 50;
                    tc_DR_DCR_SubDays.RowSpan = 2;
                    Literal lit_DR_DCR_SubDays = new Literal();
                    lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
                    tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
                    tc_DR_DCR_SubDays.Style.Add("padding", "15px 5px");
                    tc_DR_DCR_SubDays.Style.Add("border-bottom", "0px solid #fff");
                    tc_DR_DCR_SubDays.Style.Add("border-top", "0px");
                    tc_DR_DCR_SubDays.Style.Add("font-size", "12px");
                    tc_DR_DCR_SubDays.Style.Add("font-weight", "400");
                    tc_DR_DCR_SubDays.Style.Add("text-align", "center");
                    tc_DR_DCR_SubDays.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_DCR_SubDays.Style.Add("vertical-align", "inherit");
                    tc_DR_DCR_SubDays.Style.Add("text-transform", "uppercase");
                    tc_DR_DCR_SubDays.Style.Add("background-color", "#F1F5F8");
                    tc_DR_DCR_SubDays.Style.Add("color", "#636d73");
                    tc_DR_DCR_SubDays.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_DR_DCR_SubDays);


                    TableCell tc_DR_FldWrk = new TableCell();
                    //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_FldWrk.Width = 50;
                    tc_DR_FldWrk.RowSpan = 2;
                    Literal lit_DR_FldWrk = new Literal();
                    lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
                    tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                    tc_DR_FldWrk.Style.Add("padding", "15px 5px");
                    tc_DR_FldWrk.Style.Add("border-bottom", "0px solid #fff");
                    tc_DR_FldWrk.Style.Add("border-top", "0px");
                    tc_DR_FldWrk.Style.Add("font-size", "12px");
                    tc_DR_FldWrk.Style.Add("font-weight", "400");
                    tc_DR_FldWrk.Style.Add("text-align", "center");
                    tc_DR_FldWrk.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
                    tc_DR_FldWrk.Style.Add("text-transform", "uppercase");
                    tc_DR_FldWrk.Style.Add("background-color", "#F1F5F8");
                    tc_DR_FldWrk.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_DR_FldWrk);

                    TableCell tc_DR_Leave = new TableCell();
                    //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Leave.Width = 50;
                   tc_DR_Leave.RowSpan = 2;
                    Literal lit_DR_Leave = new Literal();
                    lit_DR_Leave.Text = "<center>Leave</center>";
                    tc_DR_Leave.Controls.Add(lit_DR_Leave);
                    tc_DR_Leave.Style.Add("padding", "15px 5px");
                    tc_DR_Leave.Style.Add("border-bottom", "0px solid #fff");
                    tc_DR_Leave.Style.Add("border-top", "0px");
                    tc_DR_Leave.Style.Add("font-size", "12px");
                    tc_DR_Leave.Style.Add("font-weight", "400");
                    tc_DR_Leave.Style.Add("text-align", "center");
                    tc_DR_Leave.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_Leave.Style.Add("vertical-align", "inherit");
                    tc_DR_Leave.Style.Add("text-transform", "uppercase");
                    tc_DR_Leave.Style.Add("background-color", "#F1F5F8");
                    tc_DR_Leave.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_DR_Leave);

                    TableCell tc_Docs_New_Drs_met = new TableCell();
                    tc_Docs_New_Drs_met.Width = 50;
                    tc_Docs_New_Drs_met.RowSpan = 2;
                    Literal lit_Docs_New_Drs_met = new Literal();
                    lit_Docs_New_Drs_met.Text = "<center>Doctos <br> Met</center>";
                    tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
                    tc_Docs_New_Drs_met.Style.Add("padding", "15px 5px");
                    tc_Docs_New_Drs_met.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_New_Drs_met.Style.Add("border-top", "0px");
                    tc_Docs_New_Drs_met.Style.Add("font-size", "12px");
                    tc_Docs_New_Drs_met.Style.Add("font-weight", "400");
                    tc_Docs_New_Drs_met.Style.Add("text-align", "center");
                    tc_Docs_New_Drs_met.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_New_Drs_met.Style.Add("vertical-align", "inherit");
                    tc_Docs_New_Drs_met.Style.Add("text-transform", "uppercase");
                    tc_Docs_New_Drs_met.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_New_Drs_met.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_New_Drs_met);

                    TableCell tc_Docs_New_Call_Avg = new TableCell();
                    tc_Docs_New_Call_Avg.Width = 50;
                  tc_Docs_New_Call_Avg.RowSpan = 2;
                    Literal lit_Docs_New_Call_Avg = new Literal();
                    lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
                    tc_Docs_New_Call_Avg.Style.Add("padding", "15px 5px");
                    tc_Docs_New_Call_Avg.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_New_Call_Avg.Style.Add("border-top", "0px");
                    tc_Docs_New_Call_Avg.Style.Add("font-size", "12px");
                    tc_Docs_New_Call_Avg.Style.Add("font-weight", "400");
                    tc_Docs_New_Call_Avg.Style.Add("text-align", "center");
                    tc_Docs_New_Call_Avg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_New_Call_Avg.Style.Add("vertical-align", "inherit");
                    tc_Docs_New_Call_Avg.Style.Add("text-transform", "uppercase");
                    tc_Docs_New_Call_Avg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_New_Call_Avg.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_New_Call_Avg);

                    TableCell tc_DR_Total = new TableCell();
                    tc_DR_Total.Width = 50;
                   tc_DR_Total.RowSpan = 2;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>Doctors <br>Visit</center>";
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tc_DR_Total.Style.Add("padding", "15px 5px");
                    tc_DR_Total.Style.Add("border-bottom", "0px solid #fff");
                    tc_DR_Total.Style.Add("border-top", "0px");
                    tc_DR_Total.Style.Add("font-size", "12px");
                    tc_DR_Total.Style.Add("font-weight", "400");
                    tc_DR_Total.Style.Add("text-align", "center");
                    tc_DR_Total.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_Total.Style.Add("vertical-align", "inherit");
                    tc_DR_Total.Style.Add("text-transform", "uppercase");
                    tc_DR_Total.Style.Add("background-color", "#F1F5F8");
                    tc_DR_Total.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_DR_Total);

                    TableCell tc_average = new TableCell();
                    tc_average.Width = 50;
                   tc_average.RowSpan = 2;
                    Literal lit_average = new Literal();
                    lit_average.Text = "<center>Call <br>Average </center>";
                    tc_average.Controls.Add(lit_average);
                    tc_average.Style.Add("padding", "15px 5px");
                    tc_average.Style.Add("border-bottom", "0px solid #fff");
                    tc_average.Style.Add("border-top", "0px");
                    tc_average.Style.Add("font-size", "12px");
                    tc_average.Style.Add("font-weight", "400");
                    tc_average.Style.Add("text-align", "center");
                    tc_average.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_average.Style.Add("vertical-align", "inherit");
                    tc_average.Style.Add("text-transform", "uppercase");
                    tc_average.Style.Add("background-color", "#F1F5F8");
                    tc_average.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_average);

                    TableCell tc_Docs_New_Drs = new TableCell();
                    tc_Docs_New_Drs.Width = 50;
                    tc_Docs_New_Drs.RowSpan = 2;
                    Literal lit_Docs_New_Drs = new Literal();
                    lit_Docs_New_Drs.Text = "<center>Total <br> Doctors</center>";
                    tc_Docs_New_Drs.Controls.Add(lit_Docs_New_Drs);
                    tc_Docs_New_Drs.Style.Add("padding", "15px 5px");
                    tc_Docs_New_Drs.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_New_Drs.Style.Add("border-top", "0px");
                    tc_Docs_New_Drs.Style.Add("font-size", "12px");
                    tc_Docs_New_Drs.Style.Add("font-weight", "400");
                    tc_Docs_New_Drs.Style.Add("text-align", "center");
                    tc_Docs_New_Drs.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_New_Drs.Style.Add("vertical-align", "inherit");
                    tc_Docs_New_Drs.Style.Add("text-transform", "uppercase");
                    tc_Docs_New_Drs.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_New_Drs.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_New_Drs);

                    TableCell tc_coverage = new TableCell();
                    tc_coverage.Width = 50;
                    tc_coverage.RowSpan = 2;
                    Literal lit_coverage = new Literal();
                    lit_coverage.Text = "<center>Total <br>Coverage<br> %</center>";
                    tc_coverage.Controls.Add(lit_coverage);
                    tc_coverage.Style.Add("padding", "15px 5px");
                    tc_coverage.Style.Add("border-bottom", "0px solid #fff");
                    tc_coverage.Style.Add("border-top", "0px");
                    tc_coverage.Style.Add("font-size", "12px");
                    tc_coverage.Style.Add("font-weight", "400");
                    tc_coverage.Style.Add("text-align", "center");
                    tc_coverage.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_coverage.Style.Add("vertical-align", "inherit");
                    tc_coverage.Style.Add("text-transform", "uppercase");
                    tc_coverage.Style.Add("background-color", "#F1F5F8");
                    tc_coverage.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_coverage);

                    TableCell tc_Docs_chemmet = new TableCell();
                    tc_Docs_chemmet.Width = 50;
                    tc_Docs_chemmet.RowSpan = 2;
                    Literal lit_Docs_Chemmet = new Literal();
                    lit_Docs_Chemmet.Text = "<center>Chemist <br> Visit</center>";
                    tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
                    tc_Docs_chemmet.Style.Add("padding", "15px 5px");
                    tc_Docs_chemmet.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_chemmet.Style.Add("border-top", "0px");
                    tc_Docs_chemmet.Style.Add("font-size", "12px");
                    tc_Docs_chemmet.Style.Add("font-weight", "400");
                    tc_Docs_chemmet.Style.Add("text-align", "center");
                    tc_Docs_chemmet.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_chemmet.Style.Add("vertical-align", "inherit");
                    tc_Docs_chemmet.Style.Add("text-transform", "uppercase");
                    tc_Docs_chemmet.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_chemmet.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_chemmet);

                    TableCell tc_Docs_CallAvg = new TableCell();
                    tc_Docs_CallAvg.Width = 50;
                  tc_Docs_CallAvg.RowSpan = 2;
                    Literal lit_Docs_CallAvg = new Literal();
                    lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
                    tc_Docs_CallAvg.Style.Add("padding", "15px 5px");
                    tc_Docs_CallAvg.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_CallAvg.Style.Add("border-top", "0px");
                    tc_Docs_CallAvg.Style.Add("font-size", "12px");
                    tc_Docs_CallAvg.Style.Add("font-weight", "400");
                    tc_Docs_CallAvg.Style.Add("text-align", "center");
                    tc_Docs_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_CallAvg.Style.Add("vertical-align", "inherit");
                    tc_Docs_CallAvg.Style.Add("text-transform", "uppercase");
                    tc_Docs_CallAvg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_CallAvg.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_CallAvg);

                    TableCell tc_Docs_ChemPOB = new TableCell();
                    tc_Docs_ChemPOB.Width = 50;
                    tc_Docs_ChemPOB.RowSpan = 2;
                    Literal lit_Docs_ChemPOB = new Literal();
                    lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
                    tc_Docs_ChemPOB.Visible = false;
                    tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
                    tc_Docs_ChemPOB.Style.Add("padding", "15px 5px");
                    tc_Docs_ChemPOB.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_ChemPOB.Style.Add("border-top", "0px");
                    tc_Docs_ChemPOB.Style.Add("font-size", "12px");
                    tc_Docs_ChemPOB.Style.Add("font-weight", "400");
                    tc_Docs_ChemPOB.Style.Add("text-align", "center");
                    tc_Docs_ChemPOB.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_ChemPOB.Style.Add("vertical-align", "inherit");
                    tc_Docs_ChemPOB.Style.Add("text-transform", "uppercase");
                    tc_Docs_ChemPOB.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_ChemPOB.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_ChemPOB);

                    TableCell tc_Docs_Stockmet = new TableCell();
                    tc_Docs_Stockmet.Width = 50;
                     tc_Docs_Stockmet.RowSpan = 2;
                    Literal lit_Docs_Stockmet = new Literal();
                    lit_Docs_Stockmet.Text = "<center>Stockist <br> Visit</center>";
                    tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
                    tc_Docs_Stockmet.Style.Add("padding", "15px 5px");
                    tc_Docs_Stockmet.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_Stockmet.Style.Add("border-top", "0px");
                    tc_Docs_Stockmet.Style.Add("font-size", "12px");
                    tc_Docs_Stockmet.Style.Add("font-weight", "400");
                    tc_Docs_Stockmet.Style.Add("text-align", "center");
                    tc_Docs_Stockmet.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_Stockmet.Style.Add("vertical-align", "inherit");
                    tc_Docs_Stockmet.Style.Add("text-transform", "uppercase");
                    tc_Docs_Stockmet.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_Stockmet.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_Stockmet);

                    TableCell tc_Docs_Stock_CallAvg = new TableCell();
                    tc_Docs_Stock_CallAvg.Width = 50;
                   tc_Docs_Stock_CallAvg.RowSpan=2;
                    Literal lit_Docs_Stock_CallAvg = new Literal();
                    lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
                    tc_Docs_Stock_CallAvg.Style.Add("padding", "15px 5px");
                    tc_Docs_Stock_CallAvg.Style.Add("border-bottom", "0px solid #fff");
                    tc_Docs_Stock_CallAvg.Style.Add("border-top", "0px");
                    tc_Docs_Stock_CallAvg.Style.Add("font-size", "12px");
                    tc_Docs_Stock_CallAvg.Style.Add("font-weight", "400");
                    tc_Docs_Stock_CallAvg.Style.Add("text-align", "center");
                    tc_Docs_Stock_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_Stock_CallAvg.Style.Add("vertical-align", "inherit");
                    tc_Docs_Stock_CallAvg.Style.Add("text-transform", "uppercase");
                    tc_Docs_Stock_CallAvg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_Stock_CallAvg.Attributes.Add("class", "stickyFirstRow");
                    tr_header.Cells.Add(tc_Docs_Stock_CallAvg);

                
                    if (strMode == "MonthWise")
                    {
                        //foreach (DataRow dt1 in dt.Rows)
                        for (int i = 0; i < dtstkmas.Rows.Count; i++)
                        // for (int i = 0; i < ttlSpc.Length * 3; i++)
                        {
                            TableCell tc_Docs_splty_test = new TableCell();
                            tc_Docs_splty_test.Height = 24;
                            tc_Docs_splty_test.Width = 50;
                            tc_Docs_splty_test.RowSpan = 0;
                            tc_Docs_splty_test.ColumnSpan = 3;
                            //tc_SNo.Attributes.Add("class", "stickySecondRow");
                            Literal lit_Docs_New_splty_test = new Literal();
                            lit_Docs_New_splty_test.Text = "<center>" + dtstkmas.Rows[i]["Short_Name"].ToString() + "</center>";


                            tc_Docs_splty_test.Controls.Add(lit_Docs_New_splty_test);
                            //tc_Docs_splty_test.Style.Add("padding", "15px 5px");
                            tc_Docs_splty_test.Style.Add("border-bottom", "1px solid #fff");
                            tc_Docs_splty_test.Style.Add("border-top", "0px");
                            tc_Docs_splty_test.Style.Add("font-size", "12px");
                            tc_Docs_splty_test.Style.Add("font-weight", "400");
                            tc_Docs_splty_test.Style.Add("text-align", "center");
                            tc_Docs_splty_test.Style.Add("border-right", "1px solid #DCE2E8");
                            tc_Docs_splty_test.Style.Add("border-left", "1px solid #DCE2E8");
                            tc_Docs_splty_test.Style.Add("vertical-align", "inherit");
                            tc_Docs_splty_test.Style.Add("text-transform", "uppercase");
                            tc_Docs_splty_test.Style.Add("background-color", "#F1F5F8");
                            tc_Docs_splty_test.Attributes.Add("class", "stickySecondRow");
                            tr_header.Cells.Add(tc_Docs_splty_test);
                        }
                        tbl.Rows.Add(tr_header);
                        TableRow tr2 = new TableRow();
                        tr2.Height = 22;
                        for (int i = 0; i < ttlSpc.Length; i++)
                        {
                            
                            TableCell tc_list = new TableCell();
                            tc_list.Width = 50;
                            // tc_Docs_splty_test.RowSpan = 2;

                            Literal lit_list = new Literal();
                            lit_list.Text = "<center> List </center>";
                            tc_list.Controls.Add(lit_list);
                            //tc_list.Style.Add("padding", "15px 5px");
                            tc_list.Style.Add("color", "#808080");
                            tc_list.Style.Add("border-bottom", "0px solid #fff");
                            tc_list.Style.Add("border-top", "0px");
                            tc_list.Style.Add("font-size", "12px");
                            tc_list.Style.Add("font-weight", "400");
                            tc_list.Style.Add("text-align", "center");
                            tc_list.Style.Add("border-left", "0px solid #DCE2E8");
                            tc_list.Style.Add("vertical-align", "inherit");
                            tc_list.Style.Add("text-transform", "uppercase");
                            
                            tc_list.Style.Add("background-color", "#F1F5F8");
                            tc_list.Attributes.Add("class", "stickyThirdRow");
                            tr2.Cells.Add(tc_list);

                            TableCell specmet = new TableCell();
                            specmet.Width = 50;
                            // tc_Docs_splty_test.RowSpan = 2;

                            Literal lit_specmet = new Literal();
                            lit_specmet.Text = "<center> met </center>";
                            specmet.Controls.Add(lit_specmet);
                            //specmet.Style.Add("padding", "15px 5px");
                            specmet.Style.Add("border-bottom", "0px solid #fff");
                            specmet.Style.Add("border-top", "0px");
                            specmet.Style.Add("font-size", "12px");
                            specmet.Style.Add("font-weight", "400");
                            specmet.Style.Add("text-align", "center");
                            specmet.Style.Add("border-left", "1px solid #DCE2E8");
                            specmet.Style.Add("vertical-align", "inherit");
                            specmet.Style.Add("text-transform", "uppercase");
                            specmet.Style.Add("background-color", "#F1F5F8");
                            specmet.Attributes.Add("class", "stickyThirdRow");
                            tr2.Cells.Add(specmet);

                            TableCell specseen = new TableCell();
                            specseen.Width = 50;
                            // tc_Docs_splty_test.RowSpan = 2;

                            Literal lit_seen = new Literal();
                            lit_seen.Text = "<center> seen </center>";
                            specseen.Controls.Add(lit_seen);
                            //specseen.Style.Add("padding", "15px 5px");
                            specseen.Style.Add("border-bottom", "0px solid #fff");
                            specseen.Style.Add("border-top", "0px");
                            specseen.Style.Add("font-size", "12px");
                            specseen.Style.Add("font-weight", "400");
                            specseen.Style.Add("text-align", "center");
                            specseen.Style.Add("border-left", "1px solid #DCE2E8");
                            specseen.Style.Add("vertical-align", "inherit");
                            specseen.Style.Add("text-transform", "uppercase");
                            specseen.Style.Add("background-color", "#F1F5F8");
                            specseen.Attributes.Add("class", "stickyThirdRow");
                            tr2.Cells.Add(specseen);
                            
                        }
                        tbl.Rows.Add(tr2);
                        
                    }
                  
                   
                }

                string strQry;
                strQry = " select count(Trans_SlNo),sf_code from DCRMain_Trans where month(Activity_Date)='" + iMonth + "' " +
                    " and YEAR(Activity_Date)='" + iYear + "' and Division_Code='" + divcode + "' group by sf_code";
                DataTable dtb_actdt = dbcall.Exec_DataTable(strQry);
                DataTable dtb_fld;
                    strQry = " select count(Trans_SlNo),Sf_Code,'MR' FldTyp from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + divcode + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F' group by Sf_Code " +
                     " union select count(Trans_SlNo),Sf_Code,'MGR' FldTyp from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + divcode + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F' group by Sf_Code";
                dtb_fld = dbcall.Exec_DataTable(strQry);

                strQry = " select count(DCR.FieldWork_Indicator),Sf_Code from DCRMain_Trans DCR " +
                    " where month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + divcode + "' " +
                    " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.FieldWork_Indicator='L' group by Sf_Code";
                DataTable dtb_leave = dbcall.Exec_DataTable(strQry);

                strQry = " Select Doc_Met,Doc_Made,Sf_Code from fn_GetDcr_DoctorMet_Monthwise('" + divcode + "', '"+ iMonth + "', '" + iYear + "')";
                DataTable dtb_docmet = dbcall.Exec_DataTable(strQry);

                string dtstr = iMonth.ToString() + "-01-" + iYear.ToString();
                int nxtmon = iMonth + 1;
                int nxtyr = iYear;
                if(nxtmon == 13)
                {
                    nxtmon = 01;
                    nxtyr = iYear + 1;
                }
                dtstr = nxtmon.ToString() + "-01-" + nxtyr.ToString();
                //strQry = " select count(listeddrcode) from mas_listeddr lst join fn_Sf_Det('"+ divcode+",'", 'admin') fn on lst.Sf_Code = fn.Sf_Code  " +
                //     " and ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), dateadd(m, 1, "+dt+"), 126)) " +
                //    " AND(CONVERT(Date, ListedDr_Deactivate_Date) >=CONVERT(VARCHAR(50), dateadd(m, 1, "+dt+"), 126) OR ListedDr_Deactivate_Date IS NULL)) " +
                //    " and cast(lst.Division_Code as varchar)+',' like '2,'";
                strQry = " Select lstcnt,Sf_Code from fn_get_mas_listeddrs('" + divcode + ",', '" + iMonth + "','"+ iYear + "', '" + dtstr + "','"+ sfCode + "','"+ vacflag + "')";
                DataTable dtb_doclst = dbcall.Exec_DataTable(strQry);

                strQry = "select count(c.trans_detail_info_code) Che_count,a.Sf_Code from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                   " where a.Trans_SlNo = c.Trans_SlNo " +
                   " and a.Sf_Code = c.sf_code " +
                   " and c.Trans_Detail_Info_Type= 2 " +
                   " and a.Division_Code = '" + divcode + "' " +
                   " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear + " group by a.Sf_Code";
                DataTable dtb_chmvst = dbcall.Exec_DataTable(strQry);

                strQry = "select count(c.trans_detail_info_code) Stk_count,a.Sf_Code from DCRMain_Trans a, DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and a.Division_Code = '" + divcode + "' " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear + " group by a.Sf_Code";
                DataTable dtb_stkvst = dbcall.Exec_DataTable(strQry);

                strQry = " Select lstcnt,Sf_Code,Doc_Special_Code from fn_get_mas_listeddrs_specialitywise('" + divcode + ",', '" + iMonth + "','" + iYear + "', '" + dtstr + "','" + sfCode + "','" + vacflag + "')";
                DataTable dtb_docspeclst = dbcall.Exec_DataTable(strQry);

                strQry = "select count(distinct Trans_Detail_Info_Code) as met,count(Trans_Detail_Info_Code) as seen,M.Sf_Code,Doc_Special_Code from DCRMain_Trans M inner join DCRDetail_Lst_Trans D on M.Trans_SlNo = D.Trans_SlNo inner join Mas_ListedDr L on L.ListedDrCode = D.Trans_Detail_Info_Code where M.Division_Code = '" + divcode + "'  and month(M.Activity_Date) = " + iMonth + "  and YEAR(M.Activity_Date) = " + iYear + " and((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '"+dtstr+"', 126)) And(CONVERT(Date, ListedDr_Deactivate_Date) >= CONVERT(VARCHAR(50), '"+dtstr+"', 126) or ListedDr_Deactivate_Date is null)) group by M.Sf_Code,Doc_Special_Code";
                DataTable dtb_specvst_det = dbcall.Exec_DataTable(strQry);

                // Details Section
                string sURL = string.Empty;
                int iCount = 0;
                int iCnt = 0;
                int imonth = 0;
                int iyear = 0;
                DCR dcs = new DCR();

                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
                    //ListedDR lstDR = new ListedDR();
                    //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                    TableRow tr_det = new TableRow();
                    if (Session["sf_type"].ToString() == "1")
                    {

                        tr_det.Attributes.Add("style", "background-color :" + "#" + drFF["Des_Color"].ToString());
                    }
                    else
                    {

                        tr_det.Attributes.Add("style", "background-color :" + "#" + drFF["Desig_Color"].ToString());
                    }

                    iCount += 1;
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Width = 50;
                    tc_det_SNo.Style.Add("color", "rgb(99, 109, 115)");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    //tc_det_SNo.Attributes.Add("class", "sticky");
                    tr_det.Cells.Add(tc_det_SNo);


                    TableCell tc_sf_code = new TableCell();
                    Literal lit_det_sf_code = new Literal();
                    lit_det_sf_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                    tc_sf_code.Controls.Add(lit_det_sf_code);
                    tc_sf_code.Visible = false;
                    //tc_sf_code.Attributes.Add("class", "sticky");
                    tr_det.Cells.Add(tc_sf_code);

                    TableCell tc_det_sf_name = new TableCell();
                    HyperLink lit_det_sf_name = new HyperLink();
                    lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                    //lit_det_sf_name.Attributes.Add("class", "sticky");
                    //DataSet dsSf = new DataSet();
                    //SalesForce sf1 = new SalesForce();
                    //dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);
                    //if (dsSf.Tables[0].Rows.Count > 1)
                    //{
                    //    int i = dsSf.Tables[0].Rows.Count - 1;
                    //    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    //    string[] str = sf_name.Split('(');
                    //    int str1 = str.Length;
                    //    if (str1 >= 2)
                    //    {

                    //        sf_name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //        str = sf_name.Split('(');
                    //        lit_det_sf_name.Text = "&nbsp;" + dsSf.Tables[0].Rows[0]["sf_name"].ToString().Replace(dsSf.Tables[0].Rows[0]["sf_name"].ToString(), "<span style='color:Red'>" + "( " + dsSf.Tables[0].Rows[0]["sf_name"].ToString() + " )" + "</span>");
                    //        lit_det_sf_name.Text = str[1];
                    //        lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                    //        //lit_det_sf_name.Text = str[1];
                    //        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");
                    //    }
                    //    else
                    //    {
                    //        //lit_det_sf_name.Text = str[1];
                    //        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                    //        lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //  lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    //}

                    tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_sf_name.Width = 200;
                    tc_det_sf_name.Style.Add("font-size", "11px");
                    tc_det_sf_name.Controls.Add(lit_det_sf_name);
                    tr_det.Cells.Add(tc_det_sf_name);

                    //TableCell tc_det_sf_HQ = new TableCell();
                    //Literal lit_det_sf_hq = new Literal();
                    //lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                    //tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                    //tc_det_sf_HQ.Width = 50;
                    //tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                    //tc_det_sf_HQ.BorderWidth = 1;
                    //tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                    //tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                    ////tc_det_sf_HQ.Visible = false;
                    //tr_det.Cells.Add(tc_det_sf_HQ);


                    //TableCell tc_det_sf_des = new TableCell();
                    //Literal lit_det_sf_des = new Literal();
                    ////lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                    //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "3")
                    //{
                    //    lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                    //}
                    //else
                    //{
                    //    lit_det_sf_des.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                    //}
                    //tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                    //tc_det_sf_des.BorderWidth = 1;
                    //tc_det_sf_des.Style.Add("font-family", "Calibri");
                    //tc_det_sf_des.Width = 50;
                    //tc_det_sf_des.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_sf_des.Controls.Add(lit_det_sf_des);
                    ////tc_det_sf_HQ.Visible = false;
                    //tr_det.Cells.Add(tc_det_sf_des);

                    TableCell tc_det_sf_doj = new TableCell();
                    Literal lit_det_sf_doj = new Literal();
                    lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                    tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                    tc_det_sf_doj.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_doj);

                    TableCell tc_det_sf_LastDCRDate = new TableCell();
                    Literal lit_det_sf_LastDCRDate = new Literal();
                    lit_det_sf_LastDCRDate.Text = "&nbsp;" + drFF["last_DCR_Date"].ToString();
                    tc_det_sf_LastDCRDate.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_LastDCRDate.Controls.Add(lit_det_sf_LastDCRDate);
                    tc_det_sf_LastDCRDate.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_LastDCRDate);

                    TableCell tc_det_sf_SF_Emp_ID = new TableCell();
                    Literal lit_det_sf_SF_Emp_ID = new Literal();
                    lit_det_sf_SF_Emp_ID.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
                    tc_det_sf_SF_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_SF_Emp_ID.Controls.Add(lit_det_sf_SF_Emp_ID);
                    tc_det_sf_SF_Emp_ID.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_SF_Emp_ID);

                    months = Convert.ToInt16(ViewState["months"].ToString());

                    if (months > 0)
                    {

                        tot_fldwrk = "";
                        tot_dr = "";
                        tot_doc_met = "";
                        tot_doc_calls_seen = "";
                        tot_CSH_calls_seen = "";
                        tot_Stock_Calls_Seen = "";
                        fldwrk_total = 0;
                        doctor_total = 0;
                        doctor_total2 = 0;
                        Chemist_total = 0;
                        Stock_toatal = 0;
                        Stock_calls_Seen_Total = 0;
                        Dcr_Leave = 0;
                        UnListDoc = 0;
                        Dcr_Sub_days = 0;
                        Dcr_Reminder_tot = 0;
                        doc_met_total = 0;
                        UnLstdoc_calls_seen_total = 0;
                        doc_calls_seen_total = 0;
                        CSH_calls_seen_total = 0;
                        dblCoverage = 0.00;
                        dblaverage = 0.00;

                        sub_days = 0;
                        fldwrk_days = 0;
                        leave_days = 0;
                        doc_met = 0;
                        doc_made = 0;
                        lst_drs = 0;
                        totcovg = 0;
                        chmvst = 0;
                        stkvst = 0;
                        specmas = 0;
                        speccallmet = 0;
                        speccallseen = 0;
                        // DCR_Sub_Days
                        // DCR_TotalSubDaysQuery   

                        //o
                        //dsDoc = dcs.DCR_TotalSubDaysQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //o
                        string filterstr = "Sf_Code ='" + drFF["sf_code"].ToString() + "'";
                        DataRow[] rw_1 = dtb_actdt.Select(filterstr);
                        if(rw_1.Count()>0)
                        {
                            if(rw_1[0][0]!=null)
                            {
                                sub_days = Convert.ToDecimal(rw_1[0][0].ToString());
                            }
                        }

                        TableCell tc_det_sf_dsd = new TableCell();
                        Literal lit_det_sf_dsd = new Literal();
                        lit_det_sf_dsd.Text = "&nbsp;" + sub_days.ToString();
                        tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_dsd);

                        // DCR_Sub_Days

                        // Field Work
                        //o
                        //if (drFF["sf_code"].ToString().Contains("MR"))
                        //{
                        //    dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        //}
                        //else
                        //{
                        //    dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        //}

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //  tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //o

                        
                        if (drFF["sf_code"].ToString().Contains("MR"))
                        {
                            filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' and FldTyp='MR'";
                        }
                        else
                        {
                            filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' and FldTyp='MGR'";
                        }
                        DataRow[] rw_2 = dtb_fld.Select(filterstr);
                        if(rw_2.Count()>0)
                        {
                            if(rw_2[0][0]!=null)
                            {
                                fldwrk_days = Convert.ToDecimal(rw_2[0][0].ToString());
                            }
                        }

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_days.ToString();
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        // Field Work 

                        // Leave

                        //o
                        //dsDoc = dcs.DCR_TotalLeaveQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //o

                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_3 = dtb_leave.Select(filterstr);
                        if (rw_3.Count() > 0)
                        {
                            if (rw_3[0][0] != null)
                            {
                                leave_days = Convert.ToDecimal(rw_3[0][0].ToString());
                            }
                        }
                        
                        TableCell tc_det_sf_Leave = new TableCell();
                        Literal lit_det_sf_Leave = new Literal();
                        lit_det_sf_Leave.Text = "&nbsp;" + leave_days.ToString();
                        tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_Leave);

                        // Leave

                        // Total Doctors Met
                        sCurrentDate = months + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        //dsDoc = dcs.New_DCR_Visit_TotalDocQuery_Met(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_4 = dtb_docmet.Select(filterstr);
                        if (rw_4.Count() > 0)
                        {
                            if (rw_4[0][0] != null)
                            {
                                doc_met = Convert.ToDecimal(rw_4[0][0].ToString());
                            }
                        }

                        // Total Doctors

                        ////DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        TableCell tc_det_sf_tot_doc = new TableCell();
                        Literal lit_det_sf_tot_doc = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc.Text = "&nbsp;" + doc_met.ToString();
                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                        tr_det.Cells.Add(tc_det_sf_tot_doc);

                        //DRs Calls Seen

                        //Call Average

                        decimal RoundLstCallAvg = new decimal();
                        if (fldwrk_days > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_met) / Convert.ToDecimal(fldwrk_days)));
                            RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average

                        // Total Doctors
                        sCurrentDate = months + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        //dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_5 = dtb_docmet.Select(filterstr);
                        if (rw_5.Count() > 0)
                        {
                            if (rw_5[0][0] != null)
                            {
                                doc_made = Convert.ToDecimal(rw_5[0][1].ToString());
                            }
                        }
                        
                        // Total Doctors

                        ////DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        TableCell tc_det_sf_tot_doc2 = new TableCell();
                        Literal lit_det_sf_tot_doc2 = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc2.Text = "&nbsp;" + doc_made.ToString();
                        tc_det_sf_tot_doc2.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc2.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc2.Controls.Add(lit_det_sf_tot_doc2);
                        tr_det.Cells.Add(tc_det_sf_tot_doc2);

                        //DRs Calls Seen

                        //Call Average

                        decimal RoundLstCallAvg2 = new decimal();
                        if (fldwrk_days > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_made) / Convert.ToDecimal(fldwrk_days)));
                            RoundLstCallAvg2 = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }
                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg2;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average

                        //Total Doctors

                        sCurrentDate = months + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        //dsDoc = dcs.New_DCR_Visit_TotalDocQuery_Total(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doctor = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        ////  doctor_total_tot = doctor_total_tot + Convert.ToInt16(tot_doctor);

                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_6 = dtb_doclst.Select(filterstr);
                        if (rw_6.Count() > 0)
                        {
                            if (rw_6[0][0] != null)
                            {
                                lst_drs = Convert.ToDecimal(rw_6[0][0].ToString());
                            }
                        }
                         

                        TableCell tc_det_sf_tot_doctor = new TableCell();
                        Literal lit_det_sf_tot_doctor = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doctor.Text = "&nbsp;" + lst_drs.ToString();
                        tc_det_sf_tot_doctor.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doctor.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doctor.Controls.Add(lit_det_sf_tot_doctor);
                        tr_det.Cells.Add(tc_det_sf_tot_doctor);
                        // Total Doctors

                        //Doctor Avg

                        if(lst_drs>0)
                        { 
                            totcovg = (doc_met * 100) / lst_drs;
                            totcovg = Math.Round((decimal)totcovg, 0);
                        }

                      //  dsDoc = dcs.New_DCR_Visit_TotalDocQuery_Met_Average(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                      //  if (dsDoc.Tables[0].Rows.Count > 0)
                      //      tot_doctors = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                      ////  doctor_total_tots = doctor_total_tots + Convert.ToInt16(tot_doctors);

                         

                        TableCell tc_det_sf_tot_doctors = new TableCell();
                        Literal lit_det_sf_tot_doctors = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doctors.Text = "&nbsp;" + totcovg.ToString();
                        tc_det_sf_tot_doctors.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doctors.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doctors.Controls.Add(lit_det_sf_tot_doctors);
                        tr_det.Cells.Add(tc_det_sf_tot_doctors);
                        //Doctor Avg

                        // Chemist tot

                        //dsDoc = dcs.New_DCR_TotalChemistQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);

                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_7 = dtb_chmvst.Select(filterstr);
                        if (rw_7.Count() > 0)
                        {
                            if (rw_7[0][0] != null)
                            {
                                chmvst = Convert.ToDecimal(rw_7[0][0].ToString());
                            }
                        }

                        // Chemist tot

                        //Chemist Seen

                        //dsDoc = dcs.DCR_CSH_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);

                        TableCell tc_det_sf_tot_Chemist = new TableCell();
                        Literal lit_det_sf_tot_Chemist = new Literal();
                        //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                        lit_det_sf_tot_Chemist.Text = "&nbsp;" + chmvst.ToString();
                        tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                        tr_det.Cells.Add(tc_det_sf_tot_Chemist);
                        //Chemist Seen

                        // Chemist Call Average    
                        decimal RoundChemCallAvg = new decimal();
                        if (fldwrk_days > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(chmvst) / Convert.ToDecimal(fldwrk_days)));
                            RoundChemCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundChemCallAvg;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Chemist Call Average

                        // Chemist POB

                        //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);

                        TableCell tc_det_sf_Chemist_POB = new TableCell();
                        Literal lit_det_sf_tot_POB = new Literal();
                        lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                        tc_det_sf_Chemist_POB.Visible = false;
                        tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                        tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                        // Chemist POB

                        // Stock tot

                        //dsDoc = dcs.New_DCR_TotalStockistQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);
                        filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' ";
                        DataRow[] rw_8 = dtb_stkvst.Select(filterstr);
                        if (rw_8.Count() > 0)
                        {
                            if (rw_8[0][0] != null)
                            {
                                stkvst = Convert.ToDecimal(rw_8[0][0].ToString());
                            }
                        }


                        // Stock tot

                        //Stock Calls Seen                   


                        //dsDoc = dcs.DCR_Stock_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);

                        TableCell tc_det_sf_tot_Stock = new TableCell();
                        Literal lit_det_sf_tot_Stock = new Literal();
                        // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                        lit_det_sf_tot_Stock.Text = "&nbsp;" + stkvst.ToString();
                        tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_tot_Stock);

                        //Stock Calls Seen

                        // Call Avg Stock

                        //dsDoc = dcs.Get_Call_Total_Stock_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);

                        decimal RoundStockCallAvg = new decimal();
                        if (fldwrk_days > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));

                            dblaverage = Convert.ToDouble((Convert.ToDecimal(stkvst) / Convert.ToDecimal(fldwrk_days)));
                            RoundStockCallAvg = Math.Round((decimal)dblaverage, 2);

                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockCallAvg.ToString();
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }
                        else
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }

                        //startspeciality

                        for (int i = 0; i < dtstkmas.Rows.Count; i++)
                        {
                            specmas = 0;
                            speccallmet = 0;
                            speccallseen = 0;
                            //dsDoc = dcs.SpecialityDetails(drFF["sf_code"].ToString(), divcode, dtstkmas.Rows[i]["code"].ToString(), iMonth, iYear);

                            filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' and Doc_Special_Code='" + dtstkmas.Rows[i]["code"].ToString() + "' ";
                            DataRow[] rw_9 = dtb_docspeclst.Select(filterstr);
                            if (rw_9.Count() > 0)
                            {
                                if (rw_9[0][0] != null)
                                {
                                    specmas = Convert.ToDecimal(rw_9[0][0].ToString());
                                }
                            }

                            filterstr = "Sf_Code='" + drFF["sf_code"].ToString() + "' and Doc_Special_Code='"+ dtstkmas.Rows[i]["code"].ToString() + "' ";
                            DataRow[] rw_10 = dtb_specvst_det.Select(filterstr);
                            if (rw_10.Count() > 0)
                            {
                                if (rw_10[0][0] != null)
                                {
                                    speccallmet = Convert.ToDecimal(rw_10[0][0].ToString());
                                }
                                if (rw_10[0][1] != null)
                                {
                                    speccallseen = Convert.ToDecimal(rw_10[0][1].ToString());
                                }
                            }

                            TableCell spclist = new TableCell();
                            Literal lit_spclist = new Literal();
                            // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                            lit_spclist.Text = "&nbsp;" + specmas.ToString();
                            spclist.HorizontalAlign = HorizontalAlign.Center;
                            spclist.VerticalAlign = VerticalAlign.Middle;
                            spclist.Controls.Add(lit_spclist);
                            tr_det.Cells.Add(spclist);

                            TableCell spcmet = new TableCell();
                            Literal lit_spcmet = new Literal();
                            // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                            lit_spcmet.Text = "&nbsp;" + speccallmet.ToString();
                            spcmet.HorizontalAlign = HorizontalAlign.Center;
                            spcmet.VerticalAlign = VerticalAlign.Middle;
                            spcmet.Controls.Add(lit_spcmet);
                            tr_det.Cells.Add(spcmet);

                            TableCell spcseen = new TableCell();
                            Literal lit_spcseen = new Literal();
                            // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                            lit_spcseen.Text = "&nbsp;" + speccallseen.ToString();
                            spcseen.HorizontalAlign = HorizontalAlign.Center;
                            spcseen.VerticalAlign = VerticalAlign.Middle;
                            spcseen.Controls.Add(lit_spcseen);
                            tr_det.Cells.Add(spcseen);

                        }

                    }

                    tbl.Rows.Add(tr_det);

                }
            }
            else
            {
                TableRow tr_header = new TableRow();
                ////tr_header.BorderStyle = BorderStyle.Solid;
                ////tr_header.BorderWidth = 1;


                TableCell tc_SNo = new TableCell();
                tc_SNo.Width = 50;
                tc_SNo.RowSpan = 2;
                Literal lit_SNo = new Literal();
                //// lit_SNo.Text = "#";
                lit_SNo.Text = "#";
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
                tc_SNo.Style.Add("color", "white");
                tc_SNo.Style.Add("font-weight", "400");
                tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
                tc_SNo.Style.Add("font-size", "12px");
                tc_SNo.Style.Add("border-bottom", "10px solid #fff");
                tc_SNo.HorizontalAlign = HorizontalAlign.Center;
                tc_SNo.Style.Add("font-family", "Roboto");
                tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
                tc_SNo.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_SNo);
                tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");

                TableCell tc_DR_Code = new TableCell();
                tc_DR_Code.Width = 40;
                tc_DR_Code.RowSpan = 2;
                Literal lit_DR_Code = new Literal();
                lit_DR_Code.Text = "<center>SF Code</center>";
                tc_DR_Code.Controls.Add(lit_DR_Code);
                tc_DR_Code.Visible = false;
                tr_header.Cells.Add(tc_DR_Code);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.Width = 200;
                tc_DR_Name.RowSpan = 2;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Field Force</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tc_DR_Name.Style.Add("padding", "15px 5px");
                tc_DR_Name.Style.Add("border-bottom", "10px solid #fff");
                tc_DR_Name.Style.Add("border-top", "0px");
                tc_DR_Name.Style.Add("font-size", "12px");
                tc_DR_Name.Style.Add("font-weight", "400");
                tc_DR_Name.Style.Add("text-align", "center");
                tc_DR_Name.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_Name.Style.Add("vertical-align", "inherit");
                tc_DR_Name.Style.Add("text-transform", "uppercase");
                tc_DR_Name.Attributes.Add("class", "stickyFirstRow");

                tr_header.Cells.Add(tc_DR_Name);

                //TableCell tc_DR_HQ = new TableCell();
                //tc_DR_HQ.BorderStyle = BorderStyle.Solid;
                //tc_DR_HQ.BorderWidth = 1;
                //tc_DR_HQ.Width = 50;
                //tc_DR_HQ.RowSpan = 2;
                //Literal lit_DR_HQ = new Literal();
                //lit_DR_HQ.Text = "<center>HQ</center>";
                //tc_DR_HQ.Controls.Add(lit_DR_HQ);
                //tc_DR_HQ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                //tc_DR_HQ.Style.Add("color", "white");
                //tc_DR_HQ.Style.Add("font-weight", "bold");
                //tc_DR_HQ.Style.Add("font-family", "Calibri");
                //tc_DR_HQ.Style.Add("border-color", "Black");
                //tr_header.Cells.Add(tc_DR_HQ);

                //TableCell tc_DR_Des = new TableCell();
                //tc_DR_Des.BorderStyle = BorderStyle.Solid;
                //tc_DR_Des.BorderWidth = 1;
                //tc_DR_Des.Width = 50;
                //tc_DR_Des.RowSpan = 2;
                //Literal lit_DR_Des = new Literal();
                //lit_DR_Des.Text = "<center>Designation</center>";
                //tc_DR_Des.HorizontalAlign = HorizontalAlign.Center;
                //tc_DR_Des.Controls.Add(lit_DR_Des);
                //tc_DR_Des.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                //tc_DR_Des.Style.Add("color", "white");
                //tc_DR_Des.Style.Add("font-weight", "bold");
                //tc_DR_Des.Style.Add("font-family", "Calibri");
                //tc_DR_Des.Style.Add("border-color", "Black");
                //tr_header.Cells.Add(tc_DR_Des);

                TableCell tc_DR_DOJ = new TableCell();
                tc_DR_DOJ.Width = 100;
                tc_DR_DOJ.RowSpan = 2;
                Literal lit_DR_DOJ = new Literal();
                lit_DR_DOJ.Text = "<center>DOJ</center>";
                tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
                tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_DOJ.Style.Add("padding", "15px 5px");
                tc_DR_DOJ.Style.Add("border-bottom", "10px solid #fff");
                tc_DR_DOJ.Style.Add("border-top", "0px");
                tc_DR_DOJ.Style.Add("font-size", "12px");
                tc_DR_DOJ.Style.Add("font-weight", "400");
                tc_DR_DOJ.Style.Add("text-align", "center");
                tc_DR_DOJ.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_DOJ.Style.Add("vertical-align", "inherit");
                tc_DR_DOJ.Style.Add("text-transform", "uppercase");
                tc_DR_DOJ.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_DOJ);

                TableCell tc_DR_LastDCRDate = new TableCell();
                tc_DR_LastDCRDate.Width = 100;
                tc_DR_LastDCRDate.RowSpan = 2;
                Literal lit_DR_LastDCRDate = new Literal();
                lit_DR_LastDCRDate.Text = "<center>Last DCR Date</center>";
                tc_DR_LastDCRDate.Controls.Add(lit_DR_LastDCRDate);
                tc_DR_LastDCRDate.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_LastDCRDate.Style.Add("padding", "15px 5px");
                tc_DR_LastDCRDate.Style.Add("border-bottom", "10px solid #fff");
                tc_DR_LastDCRDate.Style.Add("border-top", "0px");
                tc_DR_LastDCRDate.Style.Add("font-size", "12px");
                tc_DR_LastDCRDate.Style.Add("font-weight", "400");
                tc_DR_LastDCRDate.Style.Add("text-align", "center");
                tc_DR_LastDCRDate.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_LastDCRDate.Style.Add("vertical-align", "inherit");
                tc_DR_LastDCRDate.Style.Add("text-transform", "uppercase");
                tc_DR_LastDCRDate.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_LastDCRDate);

                TableCell tc_DR_sf_Emp_ID = new TableCell();
                tc_DR_sf_Emp_ID.Width = 100;
                tc_DR_sf_Emp_ID.RowSpan = 2;
                Literal lit_DR_SF_Emp_ID = new Literal();
                lit_DR_SF_Emp_ID.Text = "<center>Employee ID</center>";
                tc_DR_sf_Emp_ID.Controls.Add(lit_DR_SF_Emp_ID);
                tc_DR_sf_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_sf_Emp_ID.Style.Add("padding", "15px 5px");
                tc_DR_sf_Emp_ID.Style.Add("border-bottom", "10px solid #fff");
                tc_DR_sf_Emp_ID.Style.Add("border-top", "0px");
                tc_DR_sf_Emp_ID.Style.Add("font-size", "12px");
                tc_DR_sf_Emp_ID.Style.Add("font-weight", "400");
                tc_DR_sf_Emp_ID.Style.Add("text-align", "center");
                tc_DR_sf_Emp_ID.Style.Add("border-left", "1px solid #DCE2E8");
                tc_DR_sf_Emp_ID.Style.Add("vertical-align", "inherit");
                tc_DR_sf_Emp_ID.Style.Add("text-transform", "uppercase");
                tc_DR_sf_Emp_ID.Attributes.Add("class", "stickyFirstRow");
                tr_header.Cells.Add(tc_DR_sf_Emp_ID);

                int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


                ViewState["months"] = months;


                Doctor dr = new Doctor();
                dsDoctor = dr.getDocCat(divcode);


                tbl.Rows.Add(tr_header);

                //Sub Header
                months = Convert.ToInt16(ViewState["months"].ToString());

                if (months > 0)
                {

                    TableRow tr_lst_det = new TableRow();
                    TableCell tc_DR_DCR_SubDays = new TableCell();
                    tc_DR_DCR_SubDays.Width = 50;
                    // tc_DR_DCR_SubDays.RowSpan = 2;
                    Literal lit_DR_DCR_SubDays = new Literal();
                    lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
                    tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
                    tc_DR_DCR_SubDays.Style.Add("padding", "15px 5px");
                    tc_DR_DCR_SubDays.Style.Add("border-bottom", "10px solid #fff");
                    tc_DR_DCR_SubDays.Style.Add("border-top", "0px");
                    tc_DR_DCR_SubDays.Style.Add("font-size", "12px");
                    tc_DR_DCR_SubDays.Style.Add("font-weight", "400");
                    tc_DR_DCR_SubDays.Style.Add("text-align", "center");
                    tc_DR_DCR_SubDays.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_DCR_SubDays.Style.Add("vertical-align", "inherit");
                    tc_DR_DCR_SubDays.Style.Add("text-transform", "uppercase");
                    tc_DR_DCR_SubDays.Style.Add("background-color", "#F1F5F8");
                    tc_DR_DCR_SubDays.Style.Add("color", "#636d73");
                    tc_DR_DCR_SubDays.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);


                    TableCell tc_DR_FldWrk = new TableCell();
                    //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_FldWrk.Width = 50;
                    //tc_DR_FldWrk.RowSpan = 2;
                    Literal lit_DR_FldWrk = new Literal();
                    lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
                    tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                    tc_DR_FldWrk.Style.Add("padding", "15px 5px");
                    tc_DR_FldWrk.Style.Add("border-bottom", "10px solid #fff");
                    tc_DR_FldWrk.Style.Add("border-top", "0px");
                    tc_DR_FldWrk.Style.Add("font-size", "12px");
                    tc_DR_FldWrk.Style.Add("font-weight", "400");
                    tc_DR_FldWrk.Style.Add("text-align", "center");
                    tc_DR_FldWrk.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
                    tc_DR_FldWrk.Style.Add("text-transform", "uppercase");
                    tc_DR_FldWrk.Style.Add("background-color", "#F1F5F8");
                    tc_DR_FldWrk.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_DR_FldWrk);

                    TableCell tc_DR_Leave = new TableCell();
                    //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Leave.Width = 50;
                    //tc_DR_Leave.RowSpan = 2;
                    Literal lit_DR_Leave = new Literal();
                    lit_DR_Leave.Text = "<center>Leave</center>";
                    tc_DR_Leave.Controls.Add(lit_DR_Leave);
                    tc_DR_Leave.Style.Add("padding", "15px 5px");
                    tc_DR_Leave.Style.Add("border-bottom", "10px solid #fff");
                    tc_DR_Leave.Style.Add("border-top", "0px");
                    tc_DR_Leave.Style.Add("font-size", "12px");
                    tc_DR_Leave.Style.Add("font-weight", "400");
                    tc_DR_Leave.Style.Add("text-align", "center");
                    tc_DR_Leave.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_Leave.Style.Add("vertical-align", "inherit");
                    tc_DR_Leave.Style.Add("text-transform", "uppercase");
                    tc_DR_Leave.Style.Add("background-color", "#F1F5F8");
                    tc_DR_Leave.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_DR_Leave);

                    TableCell tc_Docs_New_Drs_met = new TableCell();
                    tc_Docs_New_Drs_met.Width = 50;
                    // tc_Docs_New_Drs_met.RowSpan = 2;
                    Literal lit_Docs_New_Drs_met = new Literal();
                    lit_Docs_New_Drs_met.Text = "<center>Doctos <br> Met</center>";
                    tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
                    tc_Docs_New_Drs_met.Style.Add("padding", "15px 5px");
                    tc_Docs_New_Drs_met.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_New_Drs_met.Style.Add("border-top", "0px");
                    tc_Docs_New_Drs_met.Style.Add("font-size", "12px");
                    tc_Docs_New_Drs_met.Style.Add("font-weight", "400");
                    tc_Docs_New_Drs_met.Style.Add("text-align", "center");
                    tc_Docs_New_Drs_met.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_New_Drs_met.Style.Add("vertical-align", "inherit");
                    tc_Docs_New_Drs_met.Style.Add("text-transform", "uppercase");
                    tc_Docs_New_Drs_met.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_New_Drs_met.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

                    TableCell tc_Docs_New_Call_Avg = new TableCell();
                    tc_Docs_New_Call_Avg.Width = 50;
                    // tc_Docs_New_Call_Avg.RowSpan = 2;
                    Literal lit_Docs_New_Call_Avg = new Literal();
                    lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
                    tc_Docs_New_Call_Avg.Style.Add("padding", "15px 5px");
                    tc_Docs_New_Call_Avg.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_New_Call_Avg.Style.Add("border-top", "0px");
                    tc_Docs_New_Call_Avg.Style.Add("font-size", "12px");
                    tc_Docs_New_Call_Avg.Style.Add("font-weight", "400");
                    tc_Docs_New_Call_Avg.Style.Add("text-align", "center");
                    tc_Docs_New_Call_Avg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_New_Call_Avg.Style.Add("vertical-align", "inherit");
                    tc_Docs_New_Call_Avg.Style.Add("text-transform", "uppercase");
                    tc_Docs_New_Call_Avg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_New_Call_Avg.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

                    TableCell tc_DR_Total = new TableCell();
                    tc_DR_Total.Width = 50;
                    //tc_DR_Total.RowSpan = 2;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>Doctors <br>Visit</center>";
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tc_DR_Total.Style.Add("padding", "15px 5px");
                    tc_DR_Total.Style.Add("border-bottom", "10px solid #fff");
                    tc_DR_Total.Style.Add("border-top", "0px");
                    tc_DR_Total.Style.Add("font-size", "12px");
                    tc_DR_Total.Style.Add("font-weight", "400");
                    tc_DR_Total.Style.Add("text-align", "center");
                    tc_DR_Total.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_DR_Total.Style.Add("vertical-align", "inherit");
                    tc_DR_Total.Style.Add("text-transform", "uppercase");
                    tc_DR_Total.Style.Add("background-color", "#F1F5F8");
                    tc_DR_Total.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_DR_Total);

                    TableCell tc_average = new TableCell();
                    tc_average.Width = 50;
                    // tc_average.RowSpan = 2;
                    Literal lit_average = new Literal();
                    lit_average.Text = "<center>Call <br>Average </center>";
                    tc_average.Controls.Add(lit_average);
                    tc_average.Style.Add("padding", "15px 5px");
                    tc_average.Style.Add("border-bottom", "10px solid #fff");
                    tc_average.Style.Add("border-top", "0px");
                    tc_average.Style.Add("font-size", "12px");
                    tc_average.Style.Add("font-weight", "400");
                    tc_average.Style.Add("text-align", "center");
                    tc_average.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_average.Style.Add("vertical-align", "inherit");
                    tc_average.Style.Add("text-transform", "uppercase");
                    tc_average.Style.Add("background-color", "#F1F5F8");
                    tc_average.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_average);

                    TableCell tc_Docs_chemmet = new TableCell();
                    tc_Docs_chemmet.Width = 50;
                    // tc_Docs_chemmet.RowSpan = 2;
                    Literal lit_Docs_Chemmet = new Literal();
                    lit_Docs_Chemmet.Text = "<center>Chemist <br> Visit</center>";
                    tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
                    tc_Docs_chemmet.Style.Add("padding", "15px 5px");
                    tc_Docs_chemmet.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_chemmet.Style.Add("border-top", "0px");
                    tc_Docs_chemmet.Style.Add("font-size", "12px");
                    tc_Docs_chemmet.Style.Add("font-weight", "400");
                    tc_Docs_chemmet.Style.Add("text-align", "center");
                    tc_Docs_chemmet.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_chemmet.Style.Add("vertical-align", "inherit");
                    tc_Docs_chemmet.Style.Add("text-transform", "uppercase");
                    tc_Docs_chemmet.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_chemmet.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_chemmet);

                    TableCell tc_Docs_CallAvg = new TableCell();
                    tc_Docs_CallAvg.Width = 50;
                    // tc_Docs_CallAvg.RowSpan = 2;
                    Literal lit_Docs_CallAvg = new Literal();
                    lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
                    tc_Docs_CallAvg.Style.Add("padding", "15px 5px");
                    tc_Docs_CallAvg.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_CallAvg.Style.Add("border-top", "0px");
                    tc_Docs_CallAvg.Style.Add("font-size", "12px");
                    tc_Docs_CallAvg.Style.Add("font-weight", "400");
                    tc_Docs_CallAvg.Style.Add("text-align", "center");
                    tc_Docs_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_CallAvg.Style.Add("vertical-align", "inherit");
                    tc_Docs_CallAvg.Style.Add("text-transform", "uppercase");
                    tc_Docs_CallAvg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_CallAvg.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_CallAvg);

                    TableCell tc_Docs_ChemPOB = new TableCell();
                    tc_Docs_ChemPOB.Width = 50;
                    // tc_Docs_ChemPOB.RowSpan = 2;
                    Literal lit_Docs_ChemPOB = new Literal();
                    lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
                    tc_Docs_ChemPOB.Visible = false;
                    tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
                    tc_Docs_ChemPOB.Style.Add("padding", "15px 5px");
                    tc_Docs_ChemPOB.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_ChemPOB.Style.Add("border-top", "0px");
                    tc_Docs_ChemPOB.Style.Add("font-size", "12px");
                    tc_Docs_ChemPOB.Style.Add("font-weight", "400");
                    tc_Docs_ChemPOB.Style.Add("text-align", "center");
                    tc_Docs_ChemPOB.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_ChemPOB.Style.Add("vertical-align", "inherit");
                    tc_Docs_ChemPOB.Style.Add("text-transform", "uppercase");
                    tc_Docs_ChemPOB.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_ChemPOB.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

                    TableCell tc_Docs_Stockmet = new TableCell();
                    tc_Docs_Stockmet.Width = 50;
                    // tc_Docs_Stockmet.RowSpan = 2;
                    Literal lit_Docs_Stockmet = new Literal();
                    lit_Docs_Stockmet.Text = "<center>Stockist <br> Visit</center>";
                    tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
                    tc_Docs_Stockmet.Style.Add("padding", "15px 5px");
                    tc_Docs_Stockmet.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_Stockmet.Style.Add("border-top", "0px");
                    tc_Docs_Stockmet.Style.Add("font-size", "12px");
                    tc_Docs_Stockmet.Style.Add("font-weight", "400");
                    tc_Docs_Stockmet.Style.Add("text-align", "center");
                    tc_Docs_Stockmet.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_Stockmet.Style.Add("vertical-align", "inherit");
                    tc_Docs_Stockmet.Style.Add("text-transform", "uppercase");
                    tc_Docs_Stockmet.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_Stockmet.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_Stockmet);

                    TableCell tc_Docs_Stock_CallAvg = new TableCell();
                    tc_Docs_Stock_CallAvg.Width = 50;
                    //tc_Docs_Stock_CallAvg.RowSpan=2;
                    Literal lit_Docs_Stock_CallAvg = new Literal();
                    lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
                    tc_Docs_Stock_CallAvg.Style.Add("padding", "15px 5px");
                    tc_Docs_Stock_CallAvg.Style.Add("border-bottom", "10px solid #fff");
                    tc_Docs_Stock_CallAvg.Style.Add("border-top", "0px");
                    tc_Docs_Stock_CallAvg.Style.Add("font-size", "12px");
                    tc_Docs_Stock_CallAvg.Style.Add("font-weight", "400");
                    tc_Docs_Stock_CallAvg.Style.Add("text-align", "center");
                    tc_Docs_Stock_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_Docs_Stock_CallAvg.Style.Add("vertical-align", "inherit");
                    tc_Docs_Stock_CallAvg.Style.Add("text-transform", "uppercase");
                    tc_Docs_Stock_CallAvg.Style.Add("background-color", "#F1F5F8");
                    tc_Docs_Stock_CallAvg.Attributes.Add("class", "stickyFirstRow");
                    tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

                    //TableCell tc_Docs_New_Call_Avg = new TableCell();
                    //tc_Docs_New_Call_Avg.Width = 50;
                    //Literal lit_Docs_New_Call_Avg = new Literal();
                    //lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
                    //tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
                    //tc_Docs_New_Call_Avg.Style.Add("padding", "15px 5px");
                    //tc_Docs_New_Call_Avg.Style.Add("border-bottom", "10px solid #fff");
                    //tc_Docs_New_Call_Avg.Style.Add("border-top", "0px");
                    //tc_Docs_New_Call_Avg.Style.Add("font-size", "12px");
                    //tc_Docs_New_Call_Avg.Style.Add("font-weight", "400");
                    //tc_Docs_New_Call_Avg.Style.Add("text-align", "center");
                    //tc_Docs_New_Call_Avg.Style.Add("border-left", "1px solid #DCE2E8");
                    //tc_Docs_New_Call_Avg.Style.Add("vertical-align", "inherit");
                    //tc_Docs_New_Call_Avg.Style.Add("text-transform", "uppercase");
                    //tc_Docs_New_Call_Avg.Style.Add("background-color", "#F1F5F8");
                    //tc_Docs_New_Call_Avg.Attributes.Add("class", "stickyFirstRow");
                    //tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

                    //TableCell tc_Docs_New_Rem_calls = new TableCell();
                    ////tc_Docs_New_Rem_calls.BorderStyle = BorderStyle.Solid;
                    ////tc_Docs_New_Rem_calls.BorderWidth = 1;
                    //tc_Docs_New_Rem_calls.Width = 50;
                    //Literal lit_Docs_New_Rem_calls = new Literal();
                    //lit_Docs_New_Rem_calls.Text = "<center>Reminder Call</center>";
                    //tc_Docs_New_Rem_calls.Controls.Add(lit_Docs_New_Rem_calls);
                    ////tc_Docs_New_Rem_calls.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    //tc_Docs_New_Rem_calls.Style.Add("padding", "15px 5px");
                    //tc_Docs_New_Rem_calls.Style.Add("border-bottom", "10px solid #fff");
                    //tc_Docs_New_Rem_calls.Style.Add("border-top", "0px");
                    //tc_Docs_New_Rem_calls.Style.Add("font-size", "12px");
                    //tc_Docs_New_Rem_calls.Style.Add("font-weight", "400");
                    //tc_Docs_New_Rem_calls.Style.Add("text-align", "center");
                    //tc_Docs_New_Rem_calls.Style.Add("border-left", "1px solid #DCE2E8");
                    //tc_Docs_New_Rem_calls.Style.Add("vertical-align", "inherit");
                    //tc_Docs_New_Rem_calls.Style.Add("text-transform", "uppercase");
                    //tc_Docs_New_Rem_calls.Style.Add("background-color", "#F1F5F8");
                    //tc_Docs_New_Rem_calls.Style.Add("border-radius", "0px 8px 8px 0px");
                    //tc_Docs_New_Rem_calls.Attributes.Add("class", "stickyFirstRow");

                    //tr_lst_det.Cells.Add(tc_Docs_New_Rem_calls);

                    //TableCell tc_Docs_Remarks = new TableCell();
                    //tc_Docs_Remarks.Width = 100;
                    //Literal lit_Docs_Remarks = new Literal();
                    //lit_Docs_Remarks.Text = "<center>Remarks</center>";
                    //tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
                    //tc_Docs_Remarks.Style.Add("padding", "15px 5px");
                    //tc_Docs_Remarks.Style.Add("border-bottom", "10px solid #fff");
                    //tc_Docs_Remarks.Style.Add("border-top", "0px");
                    //tc_Docs_Remarks.Style.Add("font-size", "12px");
                    //tc_Docs_Remarks.Style.Add("font-weight", "400");
                    //tc_Docs_Remarks.Style.Add("text-align", "center");
                    //tc_Docs_Remarks.Style.Add("border-left", "1px solid #DCE2E8");
                    //tc_Docs_Remarks.Style.Add("vertical-align", "inherit");
                    //tc_Docs_Remarks.Style.Add("text-transform", "uppercase");
                    //tc_Docs_Remarks.Style.Add("background-color", "#F1F5F8");
                    //tc_Docs_Remarks.Style.Add("border-radius", "0px 8px 8px 0px");
                    //tc_Docs_Remarks.Attributes.Add("class", "stickyFirstRow");

                    //tr_lst_det.Cells.Add(tc_Docs_Remarks);

                    // if (strMode == "MonthWise")
                    // {



                    //TableCell tcsplty = new TableCell();
                    //tc_Docs_Stock_CallAvg.Width = 50;
                    //Literal lit_splty = new Literal();
                    //lit_Docs_Stock_CallAvg.Text = "<center>sptly_test<br> </center>";
                    //tcsplty.Controls.Add(lit_splty);
                    //tcsplty.Style.Add("padding", "15px 5px");
                    //tcsplty.Style.Add("border-bottom", "10px solid #fff");
                    //tcsplty.Style.Add("border-top", "0px");
                    //tcsplty.Style.Add("font-size", "12px");
                    //tcsplty.Style.Add("font-weight", "400");
                    //tcsplty.Style.Add("text-align", "center");
                    //tcsplty.Style.Add("border-left", "1px solid #DCE2E8");
                    //tcsplty.Style.Add("vertical-align", "inherit");
                    //tcsplty.Style.Add("text-transform", "uppercase");
                    //tcsplty.Style.Add("background-color", "#F1F5F8");
                    //tcsplty.Attributes.Add("class", "stickyFirstRow");
                    //tr_lst_det.Cells.Add(tcsplty);
                    // }
                    tbl.Rows.Add(tr_lst_det);
                }


                // Details Section
                string sURL = string.Empty;
                int iCount = 0;
                int iCnt = 0;
                int imonth = 0;
                int iyear = 0;
                DCR dcs = new DCR();

                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
                    //ListedDR lstDR = new ListedDR();
                    //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                    TableRow tr_det = new TableRow();
                    if (Session["sf_type"].ToString() == "1")
                    {

                        tr_det.Attributes.Add("style", "background-color :" + "#" + drFF["Des_Color"].ToString());
                    }
                    else
                    {

                        tr_det.Attributes.Add("style", "background-color :" + "#" + drFF["Desig_Color"].ToString());
                    }

                    iCount += 1;
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Width = 50;
                    tc_det_SNo.Style.Add("color", "rgb(99, 109, 115)");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);


                    TableCell tc_sf_code = new TableCell();
                    Literal lit_det_sf_code = new Literal();
                    lit_det_sf_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                    tc_sf_code.Controls.Add(lit_det_sf_code);
                    tc_sf_code.Visible = false;
                    tr_det.Cells.Add(tc_sf_code);

                    TableCell tc_det_sf_name = new TableCell();
                    HyperLink lit_det_sf_name = new HyperLink();
                    lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();

                    //DataSet dsSf = new DataSet();
                    //SalesForce sf1 = new SalesForce();
                    //dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);
                    //if (dsSf.Tables[0].Rows.Count > 1)
                    //{
                    //    int i = dsSf.Tables[0].Rows.Count - 1;
                    //    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    //    string[] str = sf_name.Split('(');
                    //    int str1 = str.Length;
                    //    if (str1 >= 2)
                    //    {

                    //        sf_name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //        str = sf_name.Split('(');
                    //        lit_det_sf_name.Text = "&nbsp;" + dsSf.Tables[0].Rows[0]["sf_name"].ToString().Replace(dsSf.Tables[0].Rows[0]["sf_name"].ToString(), "<span style='color:Red'>" + "( " + dsSf.Tables[0].Rows[0]["sf_name"].ToString() + " )" + "</span>");
                    //        lit_det_sf_name.Text = str[1];
                    //        lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                    //        //lit_det_sf_name.Text = str[1];
                    //        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");
                    //    }
                    //    else
                    //    {
                    //        //lit_det_sf_name.Text = str[1];
                    //        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                    //        lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //  lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    //}

                    tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_sf_name.Width = 200;
                    tc_det_sf_name.Style.Add("font-size", "11px");
                    tc_det_sf_name.Controls.Add(lit_det_sf_name);
                    tr_det.Cells.Add(tc_det_sf_name);

                    //TableCell tc_det_sf_HQ = new TableCell();
                    //Literal lit_det_sf_hq = new Literal();
                    //lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                    //tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                    //tc_det_sf_HQ.Width = 50;
                    //tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                    //tc_det_sf_HQ.BorderWidth = 1;
                    //tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                    //tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                    ////tc_det_sf_HQ.Visible = false;
                    //tr_det.Cells.Add(tc_det_sf_HQ);


                    //TableCell tc_det_sf_des = new TableCell();
                    //Literal lit_det_sf_des = new Literal();
                    ////lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                    //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "3")
                    //{
                    //    lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                    //}
                    //else
                    //{
                    //    lit_det_sf_des.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                    //}
                    //tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                    //tc_det_sf_des.BorderWidth = 1;
                    //tc_det_sf_des.Style.Add("font-family", "Calibri");
                    //tc_det_sf_des.Width = 50;
                    //tc_det_sf_des.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_sf_des.Controls.Add(lit_det_sf_des);
                    ////tc_det_sf_HQ.Visible = false;
                    //tr_det.Cells.Add(tc_det_sf_des);

                    TableCell tc_det_sf_doj = new TableCell();
                    Literal lit_det_sf_doj = new Literal();
                    lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                    tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                    tc_det_sf_doj.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_doj);

                    TableCell tc_det_sf_LastDCRDate = new TableCell();
                    Literal lit_det_sf_LastDCRDate = new Literal();
                    lit_det_sf_LastDCRDate.Text = "&nbsp;" + drFF["last_DCR_Date"].ToString();
                    tc_det_sf_LastDCRDate.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_LastDCRDate.Controls.Add(lit_det_sf_LastDCRDate);
                    tc_det_sf_LastDCRDate.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_LastDCRDate);

                    TableCell tc_det_sf_SF_Emp_ID = new TableCell();
                    Literal lit_det_sf_SF_Emp_ID = new Literal();
                    lit_det_sf_SF_Emp_ID.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
                    tc_det_sf_SF_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_SF_Emp_ID.Controls.Add(lit_det_sf_SF_Emp_ID);
                    tc_det_sf_SF_Emp_ID.Width = 100;
                    //tc_det_sf_HQ.Visible = false;
                    tr_det.Cells.Add(tc_det_sf_SF_Emp_ID);

                    months = Convert.ToInt16(ViewState["months"].ToString());

                    if (months > 0)
                    {

                        tot_fldwrk = "";
                        tot_dr = "";
                        tot_doctor = "";
                        tot_doctors = "";
                        tot_doc_met = "";
                        tot_doc_calls_seen = "";
                        tot_CSH_calls_seen = "";
                        tot_Stock_Calls_Seen = "";
                        fldwrk_total = 0;
                        doctor_total = 0;
                        doctor_total_tot = 0;
                        doctor_totals = 0;
                        doctor_total_tots = 0;
                        doctor_total2 = 0;
                        Chemist_total = 0;
                        Stock_toatal = 0;
                        Stock_calls_Seen_Total = 0;
                        Dcr_Leave = 0;
                        UnListDoc = 0;
                        Dcr_Sub_days = 0;
                        Dcr_Reminder_tot = 0;
                        doc_met_total = 0;
                        UnLstdoc_calls_seen_total = 0;
                        doc_calls_seen_total = 0;
                        CSH_calls_seen_total = 0;
                        dblCoverage = 0.00;
                        dblaverage = 0.00;

                        // DCR_Sub_Days
                        // DCR_TotalSubDaysQuery   
                        dsDoc = dcs.DCR_TotalSubDaysQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

                        TableCell tc_det_sf_dsd = new TableCell();
                        Literal lit_det_sf_dsd = new Literal();
                        lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                        tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_dsd);

                        // DCR_Sub_Days

                        // Field Work
                        if (drFF["sf_code"].ToString().Contains("MR"))
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        }
                        else
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        }

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        // Field Work 

                        // Leave

                        dsDoc = dcs.DCR_TotalLeaveQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);

                        TableCell tc_det_sf_Leave = new TableCell();
                        Literal lit_det_sf_Leave = new Literal();
                        lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                        tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_Leave);

                        // Leave

                        // Total Doctors Met
                        sCurrentDate = months + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        dsDoc = dcs.New_DCR_Visit_TotalDocQuery_Met(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        doctor_total = doctor_total + Convert.ToInt16(tot_dr);

                        // Total Doctors

                        ////DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        TableCell tc_det_sf_tot_doc = new TableCell();
                        Literal lit_det_sf_tot_doc = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                        tr_det.Cells.Add(tc_det_sf_tot_doc);

                        //DRs Calls Seen

                        //Call Average

                        decimal RoundLstCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                            RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average

                        // Total Doctors
                        sCurrentDate = months + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        doctor_total2 = doctor_total2 + Convert.ToInt16(tot_dr);

                        // Total Doctors

                        ////DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        TableCell tc_det_sf_tot_doc2 = new TableCell();
                        Literal lit_det_sf_tot_doc2 = new Literal();
                        //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc2.Text = "&nbsp;" + doctor_total2.ToString();
                        tc_det_sf_tot_doc2.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc2.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc2.Controls.Add(lit_det_sf_tot_doc2);
                        tr_det.Cells.Add(tc_det_sf_tot_doc2);

                        //DRs Calls Seen

                        //Call Average

                        decimal RoundLstCallAvg2 = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total2) / Convert.ToDecimal(fldwrk_total)));
                            RoundLstCallAvg2 = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg2;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average

                        // Chemist tot

                        dsDoc = dcs.New_DCR_TotalChemistQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);



                        // Chemist tot

                        //Chemist Seen

                        //dsDoc = dcs.DCR_CSH_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);

                        TableCell tc_det_sf_tot_Chemist = new TableCell();
                        Literal lit_det_sf_tot_Chemist = new Literal();
                        //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                        lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();
                        tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                        tr_det.Cells.Add(tc_det_sf_tot_Chemist);
                        //Chemist Seen

                        // Chemist Call Average    
                        decimal RoundChemCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
                            RoundChemCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundChemCallAvg;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Chemist Call Average

                        // Chemist POB

                        //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);

                        TableCell tc_det_sf_Chemist_POB = new TableCell();
                        Literal lit_det_sf_tot_POB = new Literal();
                        lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                        tc_det_sf_Chemist_POB.Visible = false;
                        tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                        tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                        // Chemist POB

                        // Stock tot

                        dsDoc = dcs.New_DCR_TotalStockistQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);


                        // Stock tot

                        //Stock Calls Seen                   


                        //dsDoc = dcs.DCR_Stock_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);

                        TableCell tc_det_sf_tot_Stock = new TableCell();
                        Literal lit_det_sf_tot_Stock = new Literal();
                        // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                        lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                        tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_tot_Stock);

                        //Stock Calls Seen

                        // Call Avg Stock

                        //dsDoc = dcs.Get_Call_Total_Stock_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);

                        decimal RoundStockCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));

                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
                            RoundStockCallAvg = Math.Round((decimal)dblaverage, 2);

                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockCallAvg.ToString();
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }
                        else
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }


                        //dsDoc = dcs.New_DCR_TotalUnlstDocQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);

                        //// Unlist Doc tot

                        //// UnLstDRs Calls Seen

                        ////dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        ////if (dsDoc.Tables[0].Rows.Count > 0)
                        ////    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        ////UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);

                        //TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                        //Literal lit_det_sf_UnList_tot_Stock = new Literal();
                        ////lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                        //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                        //tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        //tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                        //tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                        //// UnLstDRs Calls Seen

                        ////Call Average
                        //decimal RoundUnLstCallAvg = new decimal();
                        //if (fldwrk_total > 0)
                        //{
                        //    //dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                        //    dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));
                        //    RoundUnLstCallAvg = Math.Round((decimal)dblaverage, 2);
                        //}
                        //else
                        //{
                        //    dblaverage = 0;
                        //}

                        //if (dblaverage != 0.0 && dblaverage != 0)
                        //{
                        //    TableCell tc_det_average = new TableCell();
                        //    Literal lit_det_average = new Literal();
                        //    lit_det_average.Text = "&nbsp;" + RoundUnLstCallAvg.ToString();
                        //    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                        //    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                        //    tc_det_average.Controls.Add(lit_det_average);
                        //    tr_det.Cells.Add(tc_det_average);
                        //}
                        //else
                        //{
                        //    TableCell tc_det_average = new TableCell();
                        //    Literal lit_det_average = new Literal();
                        //    lit_det_average.Text = "&nbsp;" + "0.0";
                        //    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                        //    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                        //    tc_det_average.Controls.Add(lit_det_average);
                        //    tr_det.Cells.Add(tc_det_average);
                        //}

                        //// Call Average 

                        ////Remainder calls count

                        //dsDoc = dcs.DCR_Reminder_calls(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Remi_Count = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Dcr_Reminder_tot = Dcr_Reminder_tot + Convert.ToInt16(tot_Remi_Count);

                        //TableCell tc_det_Remainder_Calls = new TableCell();
                        //Literal lit_det_Remainder_calls = new Literal();
                        ////lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                        //lit_det_Remainder_calls.Text = Dcr_Reminder_tot.ToString();
                        ////tc_det_Remainder_Calls.BorderStyle = BorderStyle.Solid;
                        //tc_det_Remainder_Calls.Style.Add("font-family", "Calibri");
                        ////tc_det_Remainder_Calls.BorderWidth = 1;
                        //tc_det_Remainder_Calls.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_Remainder_Calls.VerticalAlign = VerticalAlign.Middle;
                        //tc_det_Remainder_Calls.Controls.Add(lit_det_Remainder_calls);
                        //tr_det.Cells.Add(tc_det_Remainder_Calls);



                        ////Remainder calls count

                        //// Remarks

                        //TableCell tc_det_doc_Remarks = new TableCell();
                        //HyperLink lit_det_doc_Remarks = new HyperLink();
                        //lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
                        //sURL = "rptRemarks.aspx?sf_Name=" + drFF["SF_Name"].ToString() + "&sf_code=" + drFF["sf_code"].ToString() + "&Year=" + iYear + "&Month=" + iMonth + "";

                        //lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'',/*'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0'*/); ";
                        //lit_det_doc_Remarks.NavigateUrl = "#";
                        //tc_det_doc_Remarks.Width = 50;
                        //tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
                        //tr_det.Cells.Add(tc_det_doc_Remarks);

                        // Remarks

                    }

                    tbl.Rows.Add(tr_det);

                }
            }
        }
    }

    private void FillPeriodically()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();


        TableRow tr_lst_det = new TableRow();

        TableCell tc_SNo = new TableCell();
        ////tc_SNo.BorderStyle = BorderStyle.Solid;
        ////tc_SNo.BorderWidth = 1;
        ////tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
        ////tc_SNo.Style.Add("color", "white");
        ////tc_SNo.Style.Add("font-weight", "bold");
        ////tc_SNo.Style.Add("border-color", "Black");
        tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
        tc_SNo.Style.Add("color", "white");
        tc_SNo.Style.Add("font-weight", "400");
        tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
        tc_SNo.Style.Add("font-size", "12px");
        tc_SNo.Style.Add("border-bottom", "10px solid #fff");
        tc_SNo.Style.Add("font-family", "Roboto");
        tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
        tc_SNo.Attributes.Add("class", "stickyFirstRow");
        tc_SNo.Width = 50;
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "#";
        tc_SNo.Controls.Add(lit_SNo);
        tr_lst_det.Cells.Add(tc_SNo);

        TableCell tc_DR_DCR_Month = new TableCell();
        //tc_DR_DCR_Month.BackColor = System.Drawing.Color.LightSteelBlue;
        tc_DR_DCR_Month.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_DR_DCR_Month.Style.Add("padding", "15px 5px");
        tc_DR_DCR_Month.Style.Add("border-bottom", "10px solid #fff");
        tc_DR_DCR_Month.Style.Add("border-top", "0px");
        tc_DR_DCR_Month.Style.Add("font-size", "12px");
        tc_DR_DCR_Month.Style.Add("font-weight", "400");
        tc_DR_DCR_Month.Style.Add("text-align", "center");
        tc_DR_DCR_Month.Style.Add("border-left", "1px solid #DCE2E8");
        tc_DR_DCR_Month.Style.Add("vertical-align", "inherit");
        tc_DR_DCR_Month.Style.Add("text-transform", "uppercase");
        tc_DR_DCR_Month.Attributes.Add("class", "stickyFirstRow");
        tc_DR_DCR_Month.Width = 500;
        Literal lit_DR_DCR_Month = new Literal();
        lit_DR_DCR_Month.Text = "<center>Month</center>";
        tc_DR_DCR_Month.Controls.Add(lit_DR_DCR_Month);
        tr_lst_det.Cells.Add(tc_DR_DCR_Month);

        TableCell tc_DR_DCR_SubDays = new TableCell();
        tc_DR_DCR_SubDays.Width = 500;
        tc_DR_DCR_SubDays.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_DR_DCR_SubDays.Style.Add("padding", "15px 5px");
        tc_DR_DCR_SubDays.Style.Add("border-bottom", "10px solid #fff");
        tc_DR_DCR_SubDays.Style.Add("border-top", "0px");
        tc_DR_DCR_SubDays.Style.Add("font-size", "12px");
        tc_DR_DCR_SubDays.Style.Add("font-weight", "400");
        tc_DR_DCR_SubDays.Style.Add("text-align", "center");
        tc_DR_DCR_SubDays.Style.Add("border-left", "1px solid #DCE2E8");
        tc_DR_DCR_SubDays.Style.Add("vertical-align", "inherit");
        tc_DR_DCR_SubDays.Style.Add("text-transform", "uppercase");
        tc_DR_DCR_SubDays.Attributes.Add("class", "stickyFirstRow");
        Literal lit_DR_DCR_SubDays = new Literal();
        lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
        tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
        tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);

        TableCell tc_DR_FldWrk = new TableCell();
        tc_DR_FldWrk.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_DR_FldWrk.Style.Add("padding", "15px 5px");
        tc_DR_FldWrk.Style.Add("border-bottom", "10px solid #fff");
        tc_DR_FldWrk.Style.Add("border-top", "0px");
        tc_DR_FldWrk.Style.Add("font-size", "12px");
        tc_DR_FldWrk.Style.Add("font-weight", "400");
        tc_DR_FldWrk.Style.Add("text-align", "center");
        tc_DR_FldWrk.Style.Add("border-left", "1px solid #DCE2E8");
        tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
        tc_DR_FldWrk.Style.Add("text-transform", "uppercase");
        tc_DR_FldWrk.Attributes.Add("class", "stickyFirstRow");
        tc_DR_FldWrk.Width = 500;
        Literal lit_DR_FldWrk = new Literal();
        lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
        tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
        tr_lst_det.Cells.Add(tc_DR_FldWrk);

        TableCell tc_DR_Leave = new TableCell();
        tc_DR_Leave.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_DR_Leave.Width = 500;
        tc_DR_Leave.Style.Add("padding", "15px 5px");
        tc_DR_Leave.Style.Add("border-bottom", "10px solid #fff");
        tc_DR_Leave.Style.Add("border-top", "0px");
        tc_DR_Leave.Style.Add("font-size", "12px");
        tc_DR_Leave.Style.Add("font-weight", "400");
        tc_DR_Leave.Style.Add("text-align", "center");
        tc_DR_Leave.Style.Add("border-left", "1px solid #DCE2E8");
        tc_DR_Leave.Style.Add("vertical-align", "inherit");
        tc_DR_Leave.Style.Add("text-transform", "uppercase");
        tc_DR_Leave.Attributes.Add("class", "stickyFirstRow");
        Literal lit_DR_Leave = new Literal();
        lit_DR_Leave.Text = "<center>Leave</center>";
        tc_DR_Leave.Controls.Add(lit_DR_Leave);
        tr_lst_det.Cells.Add(tc_DR_Leave);

        TableCell tc_DR_Total = new TableCell();
        tc_DR_Total.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_DR_Total.Style.Add("padding", "15px 5px");
        tc_DR_Total.Style.Add("border-bottom", "10px solid #fff");
        tc_DR_Total.Style.Add("border-top", "0px");
        tc_DR_Total.Style.Add("font-size", "12px");
        tc_DR_Total.Style.Add("font-weight", "400");
        tc_DR_Total.Style.Add("text-align", "center");
        tc_DR_Total.Style.Add("border-left", "1px solid #DCE2E8");
        tc_DR_Total.Style.Add("vertical-align", "inherit");
        tc_DR_Total.Style.Add("text-transform", "uppercase");
        tc_DR_Total.Attributes.Add("class", "stickyFirstRow");
        tc_DR_Total.Width = 500;
        Literal lit_DR_Total = new Literal();
        lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
        tc_DR_Total.Controls.Add(lit_DR_Total);
        tr_lst_det.Cells.Add(tc_DR_Total);

        TableCell tc_average = new TableCell();
        tc_average.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_average.Style.Add("padding", "15px 5px");
        tc_average.Style.Add("border-bottom", "10px solid #fff");
        tc_average.Style.Add("border-top", "0px");
        tc_average.Style.Add("font-size", "12px");
        tc_average.Style.Add("font-weight", "400");
        tc_average.Style.Add("text-align", "center");
        tc_average.Style.Add("border-left", "1px solid #DCE2E8");
        tc_average.Style.Add("vertical-align", "inherit");
        tc_average.Style.Add("text-transform", "uppercase");
        tc_average.Attributes.Add("class", "stickyFirstRow");
        tc_average.Width = 500;
        Literal lit_average = new Literal();
        lit_average.Text = "<center>Call <br>Avg </center>";
        tc_average.Controls.Add(lit_average);
        tr_lst_det.Cells.Add(tc_average);

        TableCell tc_Docs_chemmet = new TableCell();
        tc_Docs_chemmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_chemmet.Style.Add("padding", "15px 5px");
        tc_Docs_chemmet.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_chemmet.Style.Add("border-top", "0px");
        tc_Docs_chemmet.Style.Add("font-size", "12px");
        tc_Docs_chemmet.Style.Add("font-weight", "400");
        tc_Docs_chemmet.Style.Add("text-align", "center");
        tc_Docs_chemmet.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_chemmet.Style.Add("vertical-align", "inherit");
        tc_Docs_chemmet.Style.Add("text-transform", "uppercase");
        tc_Docs_chemmet.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_chemmet.Width = 500;
        Literal lit_Docs_Chemmet = new Literal();
        lit_Docs_Chemmet.Text = "<center>Chemist <br> Visit</center>";
        tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
        tr_lst_det.Cells.Add(tc_Docs_chemmet);

        TableCell tc_Docs_CallAvg = new TableCell();
        tc_Docs_CallAvg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_CallAvg.Style.Add("padding", "15px 5px");
        tc_Docs_CallAvg.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_CallAvg.Style.Add("border-top", "0px");
        tc_Docs_CallAvg.Style.Add("font-size", "12px");
        tc_Docs_CallAvg.Style.Add("font-weight", "400");
        tc_Docs_CallAvg.Style.Add("text-align", "center");
        tc_Docs_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_CallAvg.Style.Add("vertical-align", "inherit");
        tc_Docs_CallAvg.Style.Add("text-transform", "uppercase");
        tc_Docs_CallAvg.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_CallAvg.Width = 500;
        Literal lit_Docs_CallAvg = new Literal();
        lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
        tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
        tr_lst_det.Cells.Add(tc_Docs_CallAvg);

        TableCell tc_Docs_ChemPOB = new TableCell();
        tc_Docs_ChemPOB.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_ChemPOB.Style.Add("padding", "15px 5px");
        tc_Docs_ChemPOB.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_ChemPOB.Style.Add("border-top", "0px");
        tc_Docs_ChemPOB.Style.Add("font-size", "12px");
        tc_Docs_ChemPOB.Style.Add("font-weight", "400");
        tc_Docs_ChemPOB.Style.Add("text-align", "center");
        tc_Docs_ChemPOB.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_ChemPOB.Style.Add("vertical-align", "inherit");
        tc_Docs_ChemPOB.Style.Add("text-transform", "uppercase");
        tc_Docs_ChemPOB.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_ChemPOB.Width = 500;
        Literal lit_Docs_ChemPOB = new Literal();
        lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
        tc_Docs_ChemPOB.Visible = false;
        tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
        tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

        TableCell tc_Docs_Stockmet = new TableCell();
        tc_Docs_Stockmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_Stockmet.Style.Add("padding", "15px 5px");
        tc_Docs_Stockmet.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_Stockmet.Style.Add("border-top", "0px");
        tc_Docs_Stockmet.Style.Add("font-size", "12px");
        tc_Docs_Stockmet.Style.Add("font-weight", "400");
        tc_Docs_Stockmet.Style.Add("text-align", "center");
        tc_Docs_Stockmet.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_Stockmet.Style.Add("vertical-align", "inherit");
        tc_Docs_Stockmet.Style.Add("text-transform", "uppercase");
        tc_Docs_Stockmet.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_Stockmet.Width = 500;
        Literal lit_Docs_Stockmet = new Literal();
        lit_Docs_Stockmet.Text = "<center>Stockist <br> Visit</center>";
        tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
        tr_lst_det.Cells.Add(tc_Docs_Stockmet);

        TableCell tc_Docs_Stock_CallAvg = new TableCell();
        tc_Docs_Stock_CallAvg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_Stock_CallAvg.Style.Add("padding", "15px 5px");
        tc_Docs_Stock_CallAvg.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_Stock_CallAvg.Style.Add("border-top", "0px");
        tc_Docs_Stock_CallAvg.Style.Add("font-size", "12px");
        tc_Docs_Stock_CallAvg.Style.Add("font-weight", "400");
        tc_Docs_Stock_CallAvg.Style.Add("text-align", "center");
        tc_Docs_Stock_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_Stock_CallAvg.Style.Add("vertical-align", "inherit");
        tc_Docs_Stock_CallAvg.Style.Add("text-transform", "uppercase");
        tc_Docs_Stock_CallAvg.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_Stock_CallAvg.Width = 500;
        Literal lit_Docs_Stock_CallAvg = new Literal();
        lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
        tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
        tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

        TableCell tc_Docs_New_Drs_met = new TableCell();
        tc_Docs_New_Drs_met.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_New_Drs_met.Style.Add("padding", "15px 5px");
        tc_Docs_New_Drs_met.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_New_Drs_met.Style.Add("border-top", "0px");
        tc_Docs_New_Drs_met.Style.Add("font-size", "12px");
        tc_Docs_New_Drs_met.Style.Add("font-weight", "400");
        tc_Docs_New_Drs_met.Style.Add("text-align", "center");
        tc_Docs_New_Drs_met.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_New_Drs_met.Style.Add("vertical-align", "inherit");
        tc_Docs_New_Drs_met.Style.Add("text-transform", "uppercase");
        tc_Docs_New_Drs_met.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_New_Drs_met.Width = 500;
        Literal lit_Docs_New_Drs_met = new Literal();
        lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Visit</center>";
        tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
        tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

        TableCell tc_Docs_New_Call_Avg = new TableCell();
        tc_Docs_New_Call_Avg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_New_Call_Avg.Style.Add("padding", "15px 5px");
        tc_Docs_New_Call_Avg.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_New_Call_Avg.Style.Add("border-top", "0px");
        tc_Docs_New_Call_Avg.Style.Add("font-size", "12px");
        tc_Docs_New_Call_Avg.Style.Add("font-weight", "400");
        tc_Docs_New_Call_Avg.Style.Add("text-align", "center");
        tc_Docs_New_Call_Avg.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_New_Call_Avg.Style.Add("vertical-align", "inherit");
        tc_Docs_New_Call_Avg.Style.Add("text-transform", "uppercase");
        tc_Docs_New_Call_Avg.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_New_Call_Avg.Width = 500;
        Literal lit_Docs_New_Call_Avg = new Literal();
        lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
        tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
        tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

        TableCell tc_Docs_Remarks = new TableCell();
        tc_Docs_Remarks.BackColor = System.Drawing.Color.FromName("#F1F5F8");
        tc_Docs_Remarks.Style.Add("padding", "15px 5px");
        tc_Docs_Remarks.Style.Add("border-bottom", "10px solid #fff");
        tc_Docs_Remarks.Style.Add("border-top", "0px");
        tc_Docs_Remarks.Style.Add("font-size", "12px");
        tc_Docs_Remarks.Style.Add("font-weight", "400");
        tc_Docs_Remarks.Style.Add("text-align", "center");
        tc_Docs_Remarks.Style.Add("border-left", "1px solid #DCE2E8");
        tc_Docs_Remarks.Style.Add("vertical-align", "inherit");
        tc_Docs_Remarks.Style.Add("text-transform", "uppercase");
        tc_Docs_Remarks.Style.Add("border-radius", "0px 8px 8px 0px");
        tc_Docs_Remarks.Attributes.Add("class", "stickyFirstRow");
        tc_Docs_Remarks.Width = 500;
        Literal lit_Docs_Remarks = new Literal();
        lit_Docs_Remarks.Text = "<center>Remarks</center>";
        tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
        tr_lst_det.Cells.Add(tc_Docs_Remarks);

        tbl.Rows.Add(tr_lst_det);

        // Details Section
        string sURL = string.Empty;
        int iCount = 0;
        int iCnt = 0;
        int imonth = 0;
        int iyear = 0;
        DCR dcs = new DCR();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;


        int iTotCal = 0;
        int iTotLeave = 0;
        Dcr_Leave = 0;
        int iSumLeave = 0;
        double itotDocavg = 0;
        double itotChemavg = 0;
        int itotStockist = 0;
        double itotStockistavg = 0;
        int itotUnLstDoc = 0;
        double itotUnLstDocavg = 0;
        int iTotChem = 0;
        int iSumChem = 0;
        double iSumDocavg = 0;
        double iSumChemavg = 0;
        int iSumStock = 0;
        double iSumStockavg = 0;
        int iSumUnLst = 0;
        double iSumUnLstavg = 0;
        int isum = 0;


        for (int j = 1; j <= months + 1; j++)
        {

            TableRow tr_det = new TableRow();

            if (months >= 0)
            {
                //Sub Header


                ListedDR lstDR = new ListedDR();

                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                ////tc_det_SNo.BorderStyle = BorderStyle.Solid;
                ////tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_month = new TableCell();
                Literal lit_month = new Literal();
                lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0, 3);
                tc_month.HorizontalAlign = HorizontalAlign.Center;
                tc_month.Controls.Add(lit_month);
                tr_det.Cells.Add(tc_month);



                //months = Convert.ToInt16(ViewState["months"].ToString());
                //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                // months = Convert.ToInt16(ViewState["months"].ToString());

                tot_fldwrk = "";
                tot_dr = "";
                tot_doc_met = "";
                tot_doc_calls_seen = "";
                tot_Dcr_Leave = "";
                Chemist_visit = "";
                Stock_Visit = "";
                UnlistVisit = "";
                tot_doc_Unlstcalls_seen = "";
                tot_CSH_calls_seen = "";
                tot_Stock_Calls_Seen = "";

                fldwrk_total = 0;
                doctor_total = 0;
                Chemist_total = 0;
                Stock_toatal = 0;
                UnListDoc = 0;
                Dcr_Sub_days = 0;
                doc_met_total = 0;
                doc_calls_seen_total = 0;
                CSH_calls_seen_total = 0;
                Stock_calls_Seen_Total = 0;
                dblCoverage = 0.00;
                dblaverage = 0.00;
                tot_Sub_days = "";

                // DCR_Sub_Days

                dsDoc = dcs.DCR_TotalSubDaysQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

                TableCell tc_det_sf_dsd = new TableCell();
                Literal lit_det_sf_dsd = new Literal();
                lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_dsd);

                // DCR_Sub_Days

                // Field Work

                //dsDoc = dcs.DCR_TotalFLDWRKQuery(sfCode, divcode, cmonth, cyear);

                if (sfCode.Contains("MR"))
                {
                    dsDoc = dcs.DCR_TotalFLDWRKQuery(sfCode, divcode, cmonth, cyear);
                }
                else
                {
                    dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(sfCode, divcode, cmonth, cyear);
                }

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);

                TableCell tc_det_sf_FLDWRK = new TableCell();
                Literal lit_det_sf_FLDWRK = new Literal();
                lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                tr_det.Cells.Add(tc_det_sf_FLDWRK);

                // Field Work 

                // Leave
                dsDoc = dcs.DCR_TotalLeaveQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);
                iTotLeave += Dcr_Leave;

                TableCell tc_det_sf_Leave = new TableCell();
                Literal lit_det_sf_Leave = new Literal();
                lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_Leave);

                // Leave

                // Total Doctors

                sCurrentDate = cmonth + "-01-" + cyear;
                dtCurrent = Convert.ToDateTime(sCurrentDate);
                dsDoc = dcs.New_DCR_Visit_TotalDocQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                doctor_total = Convert.ToInt16(tot_dr);




                // Total Doctors

                //DRs Calls Seen

                //dsDoc = dcs.DCR_Doc_Calls_Seen(sfCode, divcode, cmonth, cyear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);
                //iTotCal += doc_calls_seen_total;

                TableCell tc_det_sf_tot_doc = new TableCell();
                Literal lit_det_sf_tot_doc = new Literal();
                //lit_det_sf_tot_doc.Text = doc_calls_seen_total.ToString();
                lit_det_sf_tot_doc.Text = doctor_total.ToString();
                tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                tr_det.Cells.Add(tc_det_sf_tot_doc);

                //DRs Calls Seen

                //Call Average

                decimal RoundLstCallAvg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(doc_calls_seen_total)));
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                    RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                    itotDocavg += Convert.ToDouble(RoundLstCallAvg);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundLstCallAvg.ToString();
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Call Average

                // Chemist tot

                dsDoc = dcs.New_DCR_TotalChemistQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);


                // Chemist tot

                //Chemist Seen

                //dsDoc = dcs.DCR_CSH_Calls_Seen(sfCode, divcode, cmonth, cyear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);
                //iTotChem += CSH_calls_seen_total; 

                TableCell tc_det_sf_tot_Chemist = new TableCell();
                Literal lit_det_sf_tot_Chemist = new Literal();
                //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();
                tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                tr_det.Cells.Add(tc_det_sf_tot_Chemist);

                //Chemist Seen

                // Chemist Call Average    
                decimal RoundChemavg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
                    RoundChemavg = Math.Round((decimal)dblaverage, 2);
                    itotChemavg += Convert.ToDouble(RoundChemavg);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundChemavg.ToString();
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Chemist Call Average

                // Chemist POB

                //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(sfCode, divcode);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);                        

                TableCell tc_det_sf_Chemist_POB = new TableCell();
                Literal lit_det_sf_tot_POB = new Literal();
                lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                tc_det_sf_Chemist_POB.Visible = false;
                tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                // Chemist POB


                // Stock tot

                dsDoc = dcs.New_DCR_TotalStockistQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);
                itotStockist = Stock_toatal;

                // Stock tot

                // Stock Calls Seen

                //dsDoc = dcs.DCR_Stock_Calls_Seen(sfCode, divcode, cmonth, cyear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);
                //itotStockist += Stock_calls_Seen_Total;

                TableCell tc_det_sf_tot_Stock = new TableCell();
                Literal lit_det_sf_tot_Stock = new Literal();
                //lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_tot_Stock);

                // Stock Calls Seen

                // Call Avg Stock

                decimal RoundStockistavg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
                    RoundStockistavg += Math.Round((decimal)dblaverage, 2);
                    itotStockistavg += Convert.ToDouble(RoundStockistavg);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                    Literal lit_det_sf_Call_Avg_Stock = new Literal();
                    lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockistavg.ToString();
                    tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                    tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                }
                else
                {
                    TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                    Literal lit_det_sf_Call_Avg_Stock = new Literal();
                    lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                    tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                    tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                }

                // Call Avg Stock

                // Unlist Doc tot

                dsDoc = dcs.New_DCR_TotalUnlstDocQuery(sfCode, divcode, cmonth, cyear);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);


                // Unlist Doc tot

                // UnLstDRs Calls Seen

                //dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(sfCode, divcode, cmonth, cyear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);

                //itotUnLstDoc += itotUnLstDoc + Convert.ToInt16(UnLstdoc_calls_seen_total);

                TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                Literal lit_det_sf_UnList_tot_Stock = new Literal();
                //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                // UnLstDRs Calls Seen

                //Call Average
                decimal RoundUnLstavg = new decimal();
                if (fldwrk_total > 0)
                {
                    //  dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));

                    dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));

                    RoundUnLstavg += Math.Round((decimal)dblaverage, 2);
                    itotUnLstDocavg += Convert.ToDouble(RoundUnLstavg);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundUnLstavg.ToString();
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Call Average 


                // Remarks

                TableCell tc_det_doc_Remarks = new TableCell();
                HyperLink lit_det_doc_Remarks = new HyperLink();
                lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
                sURL = "rptRemarks.aspx?sf_Name=" + "&sf_code=" + sfCode + "&Year=" + cyear + "&Month=" + cmonth + "";
                // sURL = "rptRemarks.aspx?&sf_code=" + sf_code+"";
                //if (fldwrk_total > 0)
                //{
                lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,/*'resizable=yes,toolbar=no,menubar=no,status=no,width=800,height=600,left=0,top=0'*/);";

                lit_det_doc_Remarks.NavigateUrl = "#";
                //}
                tc_det_doc_Remarks.Width = 500;
                tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
                tr_det.Cells.Add(tc_det_doc_Remarks);

                // Remarks

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }

            }

            tbl.Rows.Add(tr_det);



        }

        TableRow tr_catg_total = new TableRow();

        TableCell tc_catg_Total = new TableCell();
        //tc_catg_Total.Width = 25;
        Literal lit_catg_Total = new Literal();
        lit_catg_Total.Text = "<center>Total</center>";
        tc_catg_Total.Controls.Add(lit_catg_Total);
        tc_catg_Total.Font.Bold.ToString();
        tc_catg_Total.ColumnSpan = 4;
        tc_catg_Total.Style.Add("text-align", "left");
        tr_catg_total.Cells.Add(tc_catg_Total);

        //TableCell tc_Call_Total_FWD = new TableCell();
        //tc_Call_Total_FWD.BorderStyle = BorderStyle.Solid;
        //tc_Call_Total_FWD.BorderWidth = 1;
        ////tc_Call_Total_FWD.Width = 25;
        //Literal lit_Call_Total_FWD = new Literal();
        //lit_Call_Total_FWD.Text = "";
        //tc_Call_Total_FWD.HorizontalAlign = HorizontalAlign.Center;
        //tc_Call_Total_FWD.Controls.Add(lit_Call_Total_FWD);
        //tc_Call_Total_FWD.BackColor = System.Drawing.Color.White;
        //tc_Call_Total_FWD.ColumnSpan = 2;  
        //tc_Call_Total_FWD.Style.Add("text-align", "left");
        //tc_Call_Total_FWD.Style.Add("font-family", "Calibri");
        //tc_Call_Total_FWD.Style.Add("font-size", "10pt");
        //tr_catg_total.Cells.Add(tc_Call_Total_FWD);

        int[] arrLeave = new int[] { iTotLeave };

        for (int i = 0; i < arrLeave.Length; i++)
        {
            iSumLeave += arrLeave[i];
        }


        TableCell tc_Call_Total_Leave = new TableCell();
        //tc_Call_Total_Leave.Width = 25;
        Literal lit_Call_Total_Leave = new Literal();
        lit_Call_Total_Leave.Text = iSumLeave.ToString();
        tc_Call_Total_Leave.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_Leave.Controls.Add(lit_Call_Total_Leave);

        tr_catg_total.Cells.Add(tc_Call_Total_Leave);

        int[] arrTotDoc = new int[] { doctor_total };

        for (int i = 0; i < arrTotDoc.Length; i++)
        {
            isum += arrTotDoc[i];
        }

        TableCell tc_Call_Total_LDrs = new TableCell();
        Literal lit_Call_Total_LDrs = new Literal();
        lit_Call_Total_LDrs.Text = Convert.ToString(isum);
        tc_Call_Total_LDrs.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_LDrs.VerticalAlign = VerticalAlign.Middle;
        tc_Call_Total_LDrs.Controls.Add(lit_Call_Total_LDrs);
        tr_catg_total.Cells.Add(tc_Call_Total_LDrs);

        double[] arrDocavg = new double[] { itotDocavg };

        for (int i = 0; i < arrDocavg.Length; i++)
        {
            iSumDocavg += arrDocavg[i];
        }

        iSumDocavg = iSumDocavg / iCount;

        decimal decDocAvg = Math.Round((decimal)iSumDocavg, 2);

        TableCell tc_Call_Total_LDrs_CallAvg = new TableCell();
        //tc_Call_Total_LDrs_CallAvg.Width = 25;
        Literal lit_Call_Total_LDrs_CallAvg = new Literal();
        lit_Call_Total_LDrs_CallAvg.Text = Convert.ToString(decDocAvg);
        tc_Call_Total_LDrs_CallAvg.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_LDrs_CallAvg.VerticalAlign = VerticalAlign.Middle;
        tc_Call_Total_LDrs_CallAvg.Controls.Add(lit_Call_Total_LDrs_CallAvg);
        //tc_Call_Total_LDrs_CallAvg.Style.Add("text-align", "left");
        //tc_Call_Total_LDrs_CallAvg.Style.Add("font-family", "Calibri");
        //tc_Call_Total_LDrs_CallAvg.Style.Add("font-size", "10pt");
        tr_catg_total.Cells.Add(tc_Call_Total_LDrs_CallAvg);

        int[] arrChem = new int[] { Chemist_total };

        for (int i = 0; i < arrChem.Length; i++)
        {
            iSumChem += arrChem[i];
        }

        TableCell tc_Call_Total_Chemist = new TableCell();
        //tc_Call_Total_Chemist.Width = 25;
        Literal lit_Call_Total_Chemist = new Literal();
        lit_Call_Total_Chemist.Text = Convert.ToString(iSumChem);
        tc_Call_Total_Chemist.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_Chemist.Controls.Add(lit_Call_Total_Chemist);
        tr_catg_total.Cells.Add(tc_Call_Total_Chemist);

        double[] arrChemavg = new double[] { itotChemavg };

        for (int i = 0; i < arrChemavg.Length; i++)
        {
            iSumChemavg += arrChemavg[i];
        }
        iSumChemavg = iSumChemavg / iCount;
        decimal decimalValue = Math.Round((decimal)iSumChemavg, 2);

        TableCell tc_Call_Total_Chemist_CallAvg = new TableCell();
        //tc_Call_Total_Chemist_CallAvg.Width = 25;
        Literal lit_Call_Total_Chemist_CallAvg = new Literal();
        lit_Call_Total_Chemist_CallAvg.Text = decimalValue.ToString();
        tc_Call_Total_Chemist_CallAvg.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_Chemist_CallAvg.Controls.Add(lit_Call_Total_Chemist_CallAvg);
        tr_catg_total.Cells.Add(tc_Call_Total_Chemist_CallAvg);

        int[] arrStockist = new int[] { Stock_toatal };

        for (int i = 0; i < arrStockist.Length; i++)
        {
            iSumStock += arrStockist[i];
        }


        TableCell tc_Call_Total_Stockist = new TableCell();
        //tc_Call_Total_Stockist.Width = 25;
        Literal lit_Call_Total_Stockist = new Literal();
        lit_Call_Total_Stockist.Text = iSumStock.ToString();
        tc_Call_Total_Stockist.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_Stockist.Controls.Add(lit_Call_Total_Stockist);

        tr_catg_total.Cells.Add(tc_Call_Total_Stockist);

        double[] arrStockavg = new double[] { itotStockistavg };

        for (int i = 0; i < arrStockavg.Length; i++)
        {
            iSumStockavg += arrStockavg[i];
        }
        iSumStockavg = iSumStockavg / iCount;

        decimal DecSumStockavg = Math.Round((decimal)iSumStockavg, 2);

        TableCell tc_Call_Total_Stockist_CallAvg = new TableCell();
        //tc_Call_Total_Stockist_CallAvg.Width = 25;
        Literal lit_Call_Total_Stockist_CallAvg = new Literal();
        lit_Call_Total_Stockist_CallAvg.Text = DecSumStockavg.ToString();
        tc_Call_Total_Stockist_CallAvg.HorizontalAlign = HorizontalAlign.Center;
        tc_Call_Total_Stockist_CallAvg.Controls.Add(lit_Call_Total_Stockist_CallAvg);
        tr_catg_total.Cells.Add(tc_Call_Total_Stockist_CallAvg);

        int[] arrUnDoc = new int[] { UnListDoc };

        for (int i = 0; i < arrUnDoc.Length; i++)
        {
            iSumUnLst += arrUnDoc[i];
        }

        TableCell tc_Call_Total_UnDrs = new TableCell();
        //tc_Call_Total_UnDrs.Width = 25;
        Literal lit_Call_Total_UnDrs = new Literal();
        lit_Call_Total_UnDrs.Text = iSumUnLst.ToString();
        tc_Call_Total_UnDrs.Controls.Add(lit_Call_Total_UnDrs);
        tc_Call_Total_UnDrs.HorizontalAlign = HorizontalAlign.Center;
        tr_catg_total.Cells.Add(tc_Call_Total_UnDrs);

        double[] arrUnLstavg = new double[] { itotUnLstDocavg };

        for (int i = 0; i < arrUnLstavg.Length; i++)
        {
            iSumUnLstavg += arrUnLstavg[i];
        }
        iSumUnLstavg = iSumUnLstavg / iCount;
        decimal decSumUnLstavg = Math.Round((decimal)iSumUnLstavg, 2);

        TableCell tc_Call_Total_UnDrs_CallAvg = new TableCell();
        //tc_Call_Total_UnDrs_CallAvg.Width = 25;
        Literal lit_Call_Total_UnDrs_CallAvg = new Literal();
        lit_Call_Total_UnDrs_CallAvg.Text = decSumUnLstavg.ToString();
        tc_Call_Total_UnDrs_CallAvg.Controls.Add(lit_Call_Total_UnDrs_CallAvg);
        tc_Call_Total_UnDrs_CallAvg.HorizontalAlign = HorizontalAlign.Center;

        tr_catg_total.Cells.Add(tc_Call_Total_UnDrs_CallAvg);

        TableCell tc_Call_Total_Remarks = new TableCell();
        //tc_Call_Total_UnDrs_CallAvg.Width = 25;
        Literal lit_Call_Total_UnDrs_Remarks = new Literal();
        lit_Call_Total_UnDrs_Remarks.Text = "";
        tc_Call_Total_Remarks.Controls.Add(lit_Call_Total_UnDrs_Remarks);
        tc_Call_Total_Remarks.HorizontalAlign = HorizontalAlign.Center;

        tr_catg_total.Cells.Add(tc_Call_Total_Remarks);

        tbl.Rows.Add(tr_catg_total);

    }

    //private void FillPeriodically_FieldForce()
    //{
    //    tbl.Rows.Clear();
    //    doctor_total = 0;       

    //    dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);

    //    if (Session["sf_type"].ToString() == "1")
    //    {
    //        BindSf_Code();
    //    }

    //    DataView dvCall = new DataView();

    //    if (chkMgr == "0" && Session["sf_type"].ToString() != "1")
    //    {
    //        //dsSalesForce.Clear();
    //        dsSalesForce.Tables[0].DefaultView.RowFilter = "sf_Type=2 ";
    //        dvCall = dsSalesForce.Tables[0].DefaultView;

    //        DataTable dt = dvCall.Table.DefaultView.ToTable();
    //        dsSalesForce.Clear();
    //        dsSalesForce.Merge(dt);
    //        lblHead.Text = "Listed Doctor Manager Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
    //    }

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        TableRow tr_header = new TableRow();
    //        tr_header.BorderStyle = BorderStyle.Solid;
    //        tr_header.BorderWidth = 1;

    //        TableCell tc_SNo = new TableCell();
    //        tc_SNo.BorderStyle = BorderStyle.Solid;
    //        tc_SNo.BorderWidth = 1;
    //        tc_SNo.Width = 1;            
    //        Literal lit_SNo = new Literal();
    //        lit_SNo.Text = "#";            
    //        tc_SNo.Controls.Add(lit_SNo);
    //        tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_SNo.Style.Add("color", "white");
    //        tc_SNo.Style.Add("font-weight", "bold");
    //        tc_SNo.Style.Add("border-color", "Black");
    //        tc_SNo.Width = 50;
    //        tr_header.Cells.Add(tc_SNo);
    //        //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //        TableCell tc_DR_Name = new TableCell();
    //        tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //        tc_DR_Name.BorderWidth = 1;
    //        tc_DR_Name.Width = 200;            
    //        Literal lit_DR_Name = new Literal();
    //        lit_DR_Name.Text = "<center>Field Force</center>";
    //        tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Name.Style.Add("border-color", "Black");
    //        tc_DR_Name.Style.Add("color", "white");
    //        tc_DR_Name.Style.Add("font-weight", "bold");
    //        tc_DR_Name.Controls.Add(lit_DR_Name);
    //        tr_header.Cells.Add(tc_DR_Name);           

    //        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //        int cmonth = Convert.ToInt32(FMonth);
    //        int cyear = Convert.ToInt32(FYear);
    //        int iCount = 0;

    //        ViewState["months"] = months;
    //        ViewState["cmonth"] = cmonth;
    //        ViewState["cyear"] = cyear;

    //        for (int j = 1; j <= months + 1; j++)
    //        {
    //            TableRow tr_det = new TableRow();
    //            if (months > 0)
    //            {
    //                //Sub Header
    //                ListedDR lstDR = new ListedDR();

    //                TableCell tc_month = new TableCell();
    //                Literal lit_month = new Literal();
    //                lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0,3);
    //                tc_month.BorderStyle = BorderStyle.Solid;
    //                tc_month.BorderWidth = 1;
    //                tc_month.Width=50;
    //                tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                tc_month.VerticalAlign = VerticalAlign.Middle;
    //                tc_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //                tc_month.Style.Add("color", "white");
    //                tc_month.Style.Add("font-weight", "bold");
    //                tc_month.Style.Add("border-color", "Black");
    //                tc_month.Controls.Add(lit_month);
    //                tr_header.Cells.Add(tc_month);
    //                cmonth = cmonth + 1;
    //                if (cmonth == 13)
    //                {
    //                    cmonth = 1;
    //                    cyear = cyear + 1;
    //                }

    //            }
    //        }

    //        Doctor dr = new Doctor();
    //        dsDoctor = dr.getDocCat(divcode);
    //        tbl.Rows.Add(tr_header);
    //        //Sub Header
    //        months = Convert.ToInt16(ViewState["months"].ToString());
    //        // Details Section
    //        string sURL = string.Empty;

    //        int iCnt = 0;
    //        int imonth = 0;
    //        int iyear = 0;
    //        DCR dcs = new DCR();

    //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //        {
    //            ListedDR lstDR = new ListedDR();
    //            iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

    //            TableRow tr_det = new TableRow();
    //            iCount += 1;
    //            TableCell tc_det_SNo = new TableCell();
    //            Literal lit_det_SNo = new Literal();
    //            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //            tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_det_SNo.BorderWidth = 1;
    //            tc_det_SNo.Width = 50;
    //            tc_det_SNo.Controls.Add(lit_det_SNo);
    //            tr_det.Cells.Add(tc_det_SNo);                

    //            TableCell tc_det_sf_name = new TableCell();
    //            HyperLink lit_det_sf_name = new HyperLink();
    //            lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
    //            tc_det_sf_name.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
    //            tc_det_sf_name.BorderWidth = 1;
    //            tc_det_sf_name.Width = 200;
    //            tc_det_sf_name.Controls.Add(lit_det_sf_name);
    //            tr_det.Cells.Add(tc_det_sf_name);

    //            months = Convert.ToInt16(ViewState["months"].ToString());

    //            if (months > 0)
    //            {


    //                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //                int cmonth1 = Convert.ToInt32(FMonth);
    //                int cyear1 = Convert.ToInt32(FYear);
    //                //int iCount = 0;

    //                ViewState["months"] = months;
    //                ViewState["cmonth"] = cmonth;
    //                ViewState["cyear"] = cyear;
    //                // Total Doctors
    //                for (int j = 1; j <= months1 + 1; j++)
    //                {
    //                    doctor_total = 0;
    //                    //TableRow tr_det = new TableRow();
    //                    if (months > 0)
    //                    {

    //                        tot_fldwrk = "";
    //                        tot_dr = "";
    //                        tot_doc_met = "";
    //                        tot_doc_calls_seen = "";

    //                        fldwrk_total = 0;
    //                        doctor_total = 0;
    //                        Chemist_total = 0;
    //                        Stock_toatal = 0;
    //                        UnListDoc = 0;
    //                        Dcr_Sub_days = 0;
    //                        doc_met_total = 0;
    //                        doc_calls_seen_total = 0;
    //                        dblCoverage = 0.00;
    //                        dblaverage = 0.00;

    //                        // Field Work
    //                        //dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

    //                        if (drFF["sf_code"].ToString().Contains("MR"))
    //                        {
    //                            dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
    //                        }
    //                        else
    //                        {
    //                            dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
    //                        }

    //                        if (dsDoc.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //                        fldwrk_total = Convert.ToInt16(tot_fldwrk);

    //                        TableCell tc_det_sf_FLDWRK = new TableCell();
    //                        Literal lit_det_sf_FLDWRK = new Literal();
    //                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
    //                        tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
    //                        tc_det_sf_FLDWRK.Visible = false;
    //                        tc_det_sf_FLDWRK.BorderWidth = 1;
    //                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
    //                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
    //                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
    //                        tr_det.Cells.Add(tc_det_sf_FLDWRK);



    //                        // Field Work 

    //                        sCurrentDate = months + "-01-" + cmonth;
    //                        dtCurrent = Convert.ToDateTime(sCurrentDate);
    //                        dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

    //                        if (dsDoc.Tables[0].Rows.Count > 0)
    //                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        doctor_total = doctor_total + Convert.ToInt16(tot_dr);                          

    //                        // Total Doctors    

    //                        // Drs Calls

    //                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

    //                        //if (dsDoc.Tables[0].Rows.Count > 0)
    //                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

    //                        TableCell tc_det_sf_tot_doc = new TableCell();
    //                        Literal lit_det_sf_tot_doc = new Literal();
    //                        //lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
    //                        lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
    //                        tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
    //                        tc_det_sf_tot_doc.Visible = false;
    //                        tc_det_sf_tot_doc.Width = 5;
    //                        tc_det_sf_tot_doc.BorderWidth = 1;
    //                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
    //                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
    //                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
    //                        tr_det.Cells.Add(tc_det_sf_tot_doc);

    //                        // Drs Calls

    //                        //Call Average
    //                        decimal decDocAvg = new decimal();

    //                        if (fldwrk_total > 0)
    //                        {
    //                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
    //                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
    //                            decDocAvg = Math.Round((decimal)dblaverage, 2);
    //                        }
    //                        else
    //                        {
    //                            dblaverage = 0;
    //                        }

    //                        if (dblaverage != 0.0)
    //                        {
    //                            TableCell tc_det_average = new TableCell();
    //                            Literal lit_det_average = new Literal();
    //                            lit_det_average.Text = "&nbsp;" + decDocAvg;
    //                            tc_det_average.BorderStyle = BorderStyle.Solid;
    //                            tc_det_average.Width = 50;
    //                            tc_det_average.BorderWidth = 1;
    //                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                            tc_det_average.Controls.Add(lit_det_average);
    //                            tr_det.Cells.Add(tc_det_average);
    //                        }
    //                        else
    //                        {
    //                            TableCell tc_det_average = new TableCell();
    //                            Literal lit_det_average = new Literal();
    //                            lit_det_average.Text = "&nbsp;" + "0.0";
    //                            tc_det_average.BorderStyle = BorderStyle.Solid;
    //                            tc_det_average.Width = 50;
    //                            tc_det_average.BorderWidth = 1;
    //                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                            tc_det_average.Controls.Add(lit_det_average);
    //                            tr_det.Cells.Add(tc_det_average);
    //                        }
    //                    }

    //                    cmonth1 = cmonth1 + 1;
    //                    if (cmonth1 == 13)
    //                    {
    //                        cmonth1 = 1;
    //                        cyear1 = cyear1 + 1;
    //                    }
    //                }

    //                // Call Average   

    //            }

    //            tbl.Rows.Add(tr_det);

    //        }
    //    }
    //}

    private void FillPeriodically_FieldForce()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();

        DataView dvCall = new DataView();

        dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);

        //if (Session["sf_type"].ToString() == "1")
        //{
        //    BindSf_Code();
        //}

        if (chkMgr == "0" && Session["sf_type"].ToString() != "1")
        {
            //dsSalesForce.Clear();
            dsSalesForce.Tables[0].DefaultView.RowFilter = "sf_Type=2 ";
            dvCall = dsSalesForce.Tables[0].DefaultView;

            DataTable dt = dvCall.Table.DefaultView.ToTable();
            dsSalesForce.Clear();
            dsSalesForce.Merge(dt);
            lblHead.Text = "Listed Doctor Manager Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();

            ////tr_header.BorderStyle = BorderStyle.Solid;
            ////tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "400");
            tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo.Style.Add("font-size", "12px");
            tc_SNo.Style.Add("border-bottom", "10px solid #fff");
            tc_SNo.Style.Add("font-family", "Roboto");
            tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
            tc_SNo.Attributes.Add("class", "stickyFirstRow");
            ////tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            ////tc_SNo.Style.Add("color", "white");
            ////tc_SNo.Style.Add("font-weight", "bold");
            ////tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.Width = 30;
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Name = new TableCell();
            ////tc_DR_Name.BorderStyle = BorderStyle.Solid;
            ////tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Name.Style.Add("padding", "15px 5px");
            tc_DR_Name.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Name.Style.Add("border-top", "0px");
            tc_DR_Name.Style.Add("font-size", "12px");
            tc_DR_Name.Style.Add("font-weight", "400");
            tc_DR_Name.Style.Add("text-align", "center");
            tc_DR_Name.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Name.Style.Add("vertical-align", "inherit");
            tc_DR_Name.Style.Add("text-transform", "uppercase");
            tc_DR_Name.Attributes.Add("class", "stickyFirstRow");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.Width = 200;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>DOJ</center>";
            tc_DR_DOJ.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_DOJ.Style.Add("padding", "15px 5px");
            tc_DR_DOJ.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_DOJ.Style.Add("border-top", "0px");
            tc_DR_DOJ.Style.Add("font-size", "12px");
            tc_DR_DOJ.Style.Add("font-weight", "400");
            tc_DR_DOJ.Style.Add("text-align", "center");
            tc_DR_DOJ.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_DOJ.Style.Add("vertical-align", "inherit");
            tc_DR_DOJ.Style.Add("text-transform", "uppercase");
            tc_DR_DOJ.Attributes.Add("class", "stickyFirstRow");
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tr_header.Cells.Add(tc_DR_DOJ);

            TableCell tc_DR_sf_emp_id = new TableCell();
            tc_DR_sf_emp_id.Width = 200;
            Literal lit_DR_sf_Emp_Id = new Literal();
            lit_DR_sf_Emp_Id.Text = "<center>Emp ID</center>";
            tc_DR_sf_emp_id.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_sf_emp_id.Style.Add("padding", "15px 5px");
            tc_DR_sf_emp_id.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_sf_emp_id.Style.Add("border-top", "0px");
            tc_DR_sf_emp_id.Style.Add("font-size", "12px");
            tc_DR_sf_emp_id.Style.Add("font-weight", "400");
            tc_DR_sf_emp_id.Style.Add("text-align", "center");
            tc_DR_sf_emp_id.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_sf_emp_id.Style.Add("vertical-align", "inherit");
            tc_DR_sf_emp_id.Style.Add("text-transform", "uppercase");
            tc_DR_sf_emp_id.Attributes.Add("class", "stickyFirstRow");
            tc_DR_sf_emp_id.Controls.Add(lit_DR_sf_Emp_Id);
            tr_header.Cells.Add(tc_DR_sf_emp_id);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);
            int iCount = 0;

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            for (int j = 1; j <= months + 1; j++)
            {
                TableRow tr_det = new TableRow();

                if (months >= 0)
                {
                    //Sub Header
                    ListedDR lstDR = new ListedDR();

                    TableCell tc_month = new TableCell();
                    Literal lit_month = new Literal();
                    lit_month.Text = sf.getMonthName(cmonth.ToString()).Substring(0, 3);
                    tc_month.Width = 50;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.VerticalAlign = VerticalAlign.Middle;
                    tc_month.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                    tc_month.Style.Add("padding", "15px 5px");
                    tc_month.Style.Add("border-bottom", "10px solid #fff");
                    tc_month.Style.Add("border-top", "0px");
                    tc_month.Style.Add("font-size", "12px");
                    tc_month.Style.Add("font-weight", "400");
                    tc_month.Style.Add("text-align", "center");
                    tc_month.Style.Add("border-left", "1px solid #DCE2E8");
                    tc_month.Style.Add("vertical-align", "inherit");
                    tc_month.Style.Add("text-transform", "uppercase");
                    tc_month.Attributes.Add("class", "stickyFirstRow");
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }
            }

            TableCell tc_DR_kumulative = new TableCell();
            tc_DR_kumulative.Width = 80;
            Literal lit_DR_kumulative = new Literal();
            lit_DR_kumulative.Text = "<center>Average</center>";
            tc_DR_kumulative.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_kumulative.Style.Add("padding", "15px 5px");
            tc_DR_kumulative.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_kumulative.Style.Add("border-top", "0px");
            tc_DR_kumulative.Style.Add("font-size", "12px");
            tc_DR_kumulative.Style.Add("font-weight", "400");
            tc_DR_kumulative.Style.Add("text-align", "center");
            tc_DR_kumulative.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_kumulative.Style.Add("vertical-align", "inherit");
            tc_DR_kumulative.Style.Add("text-transform", "uppercase");
            tc_DR_kumulative.Style.Add("border-radius", "0px 8px 8px 0px");
            tc_DR_kumulative.Attributes.Add("class", "stickyFirstRow");
            tc_DR_kumulative.Controls.Add(lit_DR_kumulative);
            tr_header.Cells.Add(tc_DR_kumulative);



            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(divcode);
            tbl.Rows.Add(tr_header);
            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            // Details Section
            string sURL = string.Empty;

            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
                }
                else
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Desig_Color"].ToString());
                }
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Width = 30;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_sf_name = new TableCell();
                HyperLink lit_det_sf_name = new HyperLink();
                lit_det_sf_name.Text = drFF["SF_Name"].ToString();
                ////tc_det_sf_name.BorderStyle = BorderStyle.Solid;
                tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
                ////tc_det_sf_name.BorderWidth = 1;
                tc_det_sf_name.Width = 200;
                tc_det_sf_name.Controls.Add(lit_det_sf_name);
                tr_det.Cells.Add(tc_det_sf_name);

                TableCell tc_det_sf_doj = new TableCell();
                HyperLink lit_det_sf_doj = new HyperLink();
                lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_doj.Width = 200;
                tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                tr_det.Cells.Add(tc_det_sf_doj);

                TableCell tc_det_sf_Emp_id = new TableCell();
                HyperLink lit_det_sf_Emp_id = new HyperLink();
                lit_det_sf_Emp_id.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
                tc_det_sf_Emp_id.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_Emp_id.Width = 200;
                tc_det_sf_Emp_id.Controls.Add(lit_det_sf_Emp_id);
                tr_det.Cells.Add(tc_det_sf_Emp_id);


                months = Convert.ToInt16(ViewState["months"].ToString());
                decimal Avgkumulative = new decimal();

                if (months >= 0)
                {


                    int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth1 = Convert.ToInt32(FMonth);
                    int cyear1 = Convert.ToInt32(FYear);
                    //int iCount = 0;

                    ViewState["months"] = months;
                    ViewState["cmonth"] = cmonth;
                    ViewState["cyear"] = cyear;
                    // Total Doctors
                    decimal kumulative = new decimal();
                    int Count = 0;

                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        doctor_total = 0;
                        //TableRow tr_det = new TableRow();
                        if (months >= 0)
                        {

                            tot_fldwrk = "";
                            tot_dr = "";
                            tot_doc_met = "";
                            tot_doc_calls_seen = "";
                            Count += 1;

                            fldwrk_total = 0;
                            doctor_total = 0;
                            Chemist_total = 0;
                            Stock_toatal = 0;
                            UnListDoc = 0;
                            Dcr_Sub_days = 0;
                            doc_met_total = 0;
                            doc_calls_seen_total = 0;
                            dblCoverage = 0.00;
                            dblaverage = 0.00;

                            // Field Work
                            //dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

                            if (drFF["sf_code"].ToString().Contains("MR"))
                            {
                                dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
                            }
                            else
                            {
                                dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
                            }

                            if (dsDoc.Tables[0].Rows.Count > 0)
                                tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            fldwrk_total = Convert.ToInt16(tot_fldwrk);

                            TableCell tc_det_sf_FLDWRK = new TableCell();
                            Literal lit_det_sf_FLDWRK = new Literal();
                            if (fldwrk_total != 0)
                            {
                                lit_det_sf_FLDWRK.Text = fldwrk_total.ToString();
                            }
                            else
                            {
                                lit_det_sf_FLDWRK.Text = "";
                            }
                            tc_det_sf_FLDWRK.Visible = false;
                            tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                            tr_det.Cells.Add(tc_det_sf_FLDWRK);



                            // Field Work 
                            if (months == 0)
                            {
                                sCurrentDate = "01" + "-01-" + cmonth;
                            }
                            else
                            {
                                sCurrentDate = months + "-01-" + cmonth;
                            }
                            dtCurrent = Convert.ToDateTime(sCurrentDate);
                            dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

                            if (dsDoc.Tables[0].Rows.Count > 0)
                                tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            doctor_total = doctor_total + Convert.ToInt16(tot_dr);

                            // Total Doctors  

                            TableCell tc_det_sf_tot_doc = new TableCell();
                            Literal lit_det_sf_tot_doc = new Literal();
                            //lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                            if (doctor_total != 0)
                            {
                                lit_det_sf_tot_doc.Text = doctor_total.ToString();
                            }
                            else
                            {
                                lit_det_sf_tot_doc.Text = "";
                            }
                            tc_det_sf_tot_doc.Visible = false;
                            tc_det_sf_tot_doc.Width = 5;
                            tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                            tr_det.Cells.Add(tc_det_sf_tot_doc);

                            // Drs Calls

                            //Call Average
                            decimal decDocAvg = new decimal();

                            if (fldwrk_total > 0)
                            {
                                //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                                dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                                decDocAvg = Math.Round((decimal)dblaverage, 2);
                            }
                            else
                            {
                                dblaverage = 0;
                            }

                            if (dblaverage != 0.0)
                            {
                                TableCell tc_det_average = new TableCell();
                                Literal lit_det_average = new Literal();
                                lit_det_average.Text = decDocAvg.ToString();
                                tc_det_average.Width = 50;
                                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_average.VerticalAlign = VerticalAlign.Middle;
                                tc_det_average.Controls.Add(lit_det_average);
                                tr_det.Cells.Add(tc_det_average);
                            }
                            else
                            {
                                TableCell tc_det_average = new TableCell();
                                Literal lit_det_average = new Literal();
                                lit_det_average.Text = "";
                                tc_det_average.Width = 50;
                                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_average.VerticalAlign = VerticalAlign.Middle;
                                tc_det_average.Controls.Add(lit_det_average);
                                tr_det.Cells.Add(tc_det_average);
                            }

                            kumulative += decDocAvg;

                        }

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }

                    }

                    // Call Average   
                    Avgkumulative = kumulative / Count;
                    Avgkumulative = Math.Round((decimal)Avgkumulative, 2);
                }



                TableCell tc_det_kumulative = new TableCell();
                HyperLink lit_det_kumulative = new HyperLink();
                lit_det_kumulative.Text = Avgkumulative.ToString();
                tc_det_kumulative.HorizontalAlign = HorizontalAlign.Center;
                tc_det_kumulative.Width = 80;
                tc_det_kumulative.Controls.Add(lit_det_kumulative);
                tr_det.Cells.Add(tc_det_kumulative);

                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void FillDateWise()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            ////tr_header.BorderStyle = BorderStyle.Solid;
            ////tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 30;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.Controls.Add(lit_SNo);
            ////tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            ////tc_SNo.Style.Add("color", "white");
            ////tc_SNo.Style.Add("font-weight", "bold");
            ////tc_SNo.Style.Add("border-color", "Black");
            ////tc_SNo.HorizontalAlign = HorizontalAlign.Center;
            ////tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "400");
            tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo.Style.Add("font-size", "12px");
            tc_SNo.Style.Add("border-bottom", "10px solid #fff");
            tc_SNo.Style.Add("font-family", "Roboto");
            tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
            tc_SNo.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.Width = 40;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Code.Style.Add("padding", "15px 5px");
            tc_DR_Code.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Code.Style.Add("border-top", "0px");
            tc_DR_Code.Style.Add("font-size", "12px");
            tc_DR_Code.Style.Add("font-weight", "400");
            tc_DR_Code.Style.Add("text-align", "center");
            tc_DR_Code.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Code.Style.Add("vertical-align", "inherit");
            tc_DR_Code.Style.Add("text-transform", "uppercase");
            tc_DR_Code.Attributes.Add("class", "stickyFirstRow");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tc_DR_Name.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Name.Style.Add("padding", "15px 5px");
            tc_DR_Name.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Name.Style.Add("border-top", "0px");
            tc_DR_Name.Style.Add("font-size", "12px");
            tc_DR_Name.Style.Add("font-weight", "400");
            tc_DR_Name.Style.Add("text-align", "center");
            tc_DR_Name.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Name.Style.Add("vertical-align", "inherit");
            tc_DR_Name.Style.Add("text-transform", "uppercase");
            tc_DR_Name.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.Width = 100;
            tc_DR_DOJ.RowSpan = 2;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>DOJ</center>";
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_DOJ.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_DOJ.Style.Add("padding", "15px 5px");
            tc_DR_DOJ.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_DOJ.Style.Add("border-top", "0px");
            tc_DR_DOJ.Style.Add("font-size", "12px");
            tc_DR_DOJ.Style.Add("font-weight", "400");
            tc_DR_DOJ.Style.Add("text-align", "center");
            tc_DR_DOJ.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_DOJ.Style.Add("vertical-align", "inherit");
            tc_DR_DOJ.Style.Add("text-transform", "uppercase");
            tc_DR_DOJ.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_DOJ);

            TableCell tc_DR_sf_Emp_ID = new TableCell();
            tc_DR_sf_Emp_ID.Width = 100;
            tc_DR_sf_Emp_ID.RowSpan = 2;
            Literal lit_DR_SF_Emp_ID = new Literal();
            lit_DR_SF_Emp_ID.Text = "<center>Employee ID</center>";
            tc_DR_sf_Emp_ID.Controls.Add(lit_DR_SF_Emp_ID);
            tc_DR_sf_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_sf_Emp_ID.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_sf_Emp_ID.Style.Add("padding", "15px 5px");
            tc_DR_sf_Emp_ID.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_sf_Emp_ID.Style.Add("border-top", "0px");
            tc_DR_sf_Emp_ID.Style.Add("font-size", "12px");
            tc_DR_sf_Emp_ID.Style.Add("font-weight", "400");
            tc_DR_sf_Emp_ID.Style.Add("text-align", "center");
            tc_DR_sf_Emp_ID.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_sf_Emp_ID.Style.Add("vertical-align", "inherit");
            tc_DR_sf_Emp_ID.Style.Add("text-transform", "uppercase");
            tc_DR_sf_Emp_ID.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_sf_Emp_ID);

            int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


            ViewState["months"] = months;


            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(divcode);


            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());

            //if (months > 0)
            //{
            TableRow tr_lst_det = new TableRow();

            TableCell tc_DR_DCR_SubDays = new TableCell();
            tc_DR_DCR_SubDays.Width = 50;

            Literal lit_DR_DCR_SubDays = new Literal();
            lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
            tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
            tc_DR_DCR_SubDays.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_DCR_SubDays.Style.Add("padding", "15px 5px");
            tc_DR_DCR_SubDays.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_DCR_SubDays.Style.Add("border-top", "0px");
            tc_DR_DCR_SubDays.Style.Add("font-size", "12px");
            tc_DR_DCR_SubDays.Style.Add("font-weight", "400");
            tc_DR_DCR_SubDays.Style.Add("text-align", "center");
            tc_DR_DCR_SubDays.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_DCR_SubDays.Style.Add("vertical-align", "inherit");
            tc_DR_DCR_SubDays.Style.Add("text-transform", "uppercase");
            tc_DR_DCR_SubDays.Style.Add("color", "#636d73");
            tc_DR_DCR_SubDays.Attributes.Add("class", "stickyFirstRow");

            tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);

            TableCell tc_DR_FldWrk = new TableCell();
            //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
            tc_DR_FldWrk.Width = 50;
            Literal lit_DR_FldWrk = new Literal();
            lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
            tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
            tc_DR_FldWrk.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_FldWrk.Style.Add("padding", "15px 5px");
            tc_DR_FldWrk.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_FldWrk.Style.Add("border-top", "0px");
            tc_DR_FldWrk.Style.Add("font-size", "12px");
            tc_DR_FldWrk.Style.Add("font-weight", "400");
            tc_DR_FldWrk.Style.Add("text-align", "center");
            tc_DR_FldWrk.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
            tc_DR_FldWrk.Style.Add("text-transform", "uppercase");
            tc_DR_FldWrk.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_DR_FldWrk);

            TableCell tc_DR_Leave = new TableCell();
            //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
            tc_DR_Leave.Width = 50;
            Literal lit_DR_Leave = new Literal();
            lit_DR_Leave.Text = "<center>Leave</center>";
            tc_DR_Leave.Controls.Add(lit_DR_Leave);
            tc_DR_Leave.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Leave.Style.Add("padding", "15px 5px");
            tc_DR_Leave.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Leave.Style.Add("border-top", "0px");
            tc_DR_Leave.Style.Add("font-size", "12px");
            tc_DR_Leave.Style.Add("font-weight", "400");
            tc_DR_Leave.Style.Add("text-align", "center");
            tc_DR_Leave.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Leave.Style.Add("vertical-align", "inherit");
            tc_DR_Leave.Style.Add("text-transform", "uppercase");
            tc_DR_Leave.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_DR_Leave);

            TableCell tc_DR_Total = new TableCell();
            tc_DR_Total.Width = 50;
            Literal lit_DR_Total = new Literal();
            lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
            tc_DR_Total.Controls.Add(lit_DR_Total);
            tc_DR_Total.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Total.Style.Add("padding", "15px 5px");
            tc_DR_Total.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Total.Style.Add("border-top", "0px");
            tc_DR_Total.Style.Add("font-size", "12px");
            tc_DR_Total.Style.Add("font-weight", "400");
            tc_DR_Total.Style.Add("text-align", "center");
            tc_DR_Total.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Total.Style.Add("vertical-align", "inherit");
            tc_DR_Total.Style.Add("text-transform", "uppercase");
            tc_DR_Total.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_DR_Total);

            TableCell tc_average = new TableCell();
            tc_average.Width = 50;
            Literal lit_average = new Literal();
            lit_average.Text = "<center>Call <br>Average </center>";
            tc_average.Controls.Add(lit_average);
            tc_average.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_average.Style.Add("padding", "15px 5px");
            tc_average.Style.Add("border-bottom", "10px solid #fff");
            tc_average.Style.Add("border-top", "0px");
            tc_average.Style.Add("font-size", "12px");
            tc_average.Style.Add("font-weight", "400");
            tc_average.Style.Add("text-align", "center");
            tc_average.Style.Add("border-left", "1px solid #DCE2E8");
            tc_average.Style.Add("vertical-align", "inherit");
            tc_average.Style.Add("text-transform", "uppercase");
            tc_average.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_average);

            TableCell tc_Docs_chemmet = new TableCell();
            tc_Docs_chemmet.Width = 50;
            Literal lit_Docs_Chemmet = new Literal();
            lit_Docs_Chemmet.Text = "<center>Chemist <br> Visit</center>";
            tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
            tc_Docs_chemmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_chemmet.Style.Add("padding", "15px 5px");
            tc_Docs_chemmet.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_chemmet.Style.Add("border-top", "0px");
            tc_Docs_chemmet.Style.Add("font-size", "12px");
            tc_Docs_chemmet.Style.Add("font-weight", "400");
            tc_Docs_chemmet.Style.Add("text-align", "center");
            tc_Docs_chemmet.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_chemmet.Style.Add("vertical-align", "inherit");
            tc_Docs_chemmet.Style.Add("text-transform", "uppercase");
            tc_Docs_chemmet.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_chemmet);

            TableCell tc_Docs_CallAvg = new TableCell();
            tc_Docs_CallAvg.Width = 50;
            Literal lit_Docs_CallAvg = new Literal();
            lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
            tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
            tc_Docs_CallAvg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_CallAvg.Style.Add("padding", "15px 5px");
            tc_Docs_CallAvg.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_CallAvg.Style.Add("border-top", "0px");
            tc_Docs_CallAvg.Style.Add("font-size", "12px");
            tc_Docs_CallAvg.Style.Add("font-weight", "400");
            tc_Docs_CallAvg.Style.Add("text-align", "center");
            tc_Docs_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_CallAvg.Style.Add("vertical-align", "inherit");
            tc_Docs_CallAvg.Style.Add("text-transform", "uppercase");
            tc_Docs_CallAvg.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_CallAvg);

            TableCell tc_Docs_ChemPOB = new TableCell();
            tc_Docs_ChemPOB.Width = 50;
            Literal lit_Docs_ChemPOB = new Literal();
            lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
            tc_Docs_ChemPOB.Visible = false;
            tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
            tc_Docs_ChemPOB.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_ChemPOB.Style.Add("padding", "15px 5px");
            tc_Docs_ChemPOB.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_ChemPOB.Style.Add("border-top", "0px");
            tc_Docs_ChemPOB.Style.Add("font-size", "12px");
            tc_Docs_ChemPOB.Style.Add("font-weight", "400");
            tc_Docs_ChemPOB.Style.Add("text-align", "center");
            tc_Docs_ChemPOB.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_ChemPOB.Style.Add("vertical-align", "inherit");
            tc_Docs_ChemPOB.Style.Add("text-transform", "uppercase");
            tc_Docs_ChemPOB.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

            TableCell tc_Docs_Stockmet = new TableCell();
            tc_Docs_Stockmet.Width = 50;
            Literal lit_Docs_Stockmet = new Literal();
            lit_Docs_Stockmet.Text = "<center>Stockist <br> Seen</center>";
            tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);       
            tc_Docs_Stockmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_Stockmet.Style.Add("padding", "15px 5px");
            tc_Docs_Stockmet.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_Stockmet.Style.Add("border-top", "0px");
            tc_Docs_Stockmet.Style.Add("font-size", "12px");
            tc_Docs_Stockmet.Style.Add("font-weight", "400");
            tc_Docs_Stockmet.Style.Add("text-align", "center");
            tc_Docs_Stockmet.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_Stockmet.Style.Add("vertical-align", "inherit");
            tc_Docs_Stockmet.Style.Add("text-transform", "uppercase");
            tc_Docs_Stockmet.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_Stockmet);

            TableCell tc_Docs_Stock_CallAvg = new TableCell();
            tc_Docs_Stock_CallAvg.Width = 50;
            Literal lit_Docs_Stock_CallAvg = new Literal();
            lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
            tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
            tc_Docs_Stock_CallAvg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_Stock_CallAvg.Style.Add("padding", "15px 5px");
            tc_Docs_Stock_CallAvg.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_Stock_CallAvg.Style.Add("border-top", "0px");
            tc_Docs_Stock_CallAvg.Style.Add("font-size", "12px");
            tc_Docs_Stock_CallAvg.Style.Add("font-weight", "400");
            tc_Docs_Stock_CallAvg.Style.Add("text-align", "center");
            tc_Docs_Stock_CallAvg.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_Stock_CallAvg.Style.Add("vertical-align", "inherit");
            tc_Docs_Stock_CallAvg.Style.Add("text-transform", "uppercase");
            tc_Docs_Stock_CallAvg.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

            TableCell tc_Docs_New_Drs_met = new TableCell();
            tc_Docs_New_Drs_met.Width = 50;
            Literal lit_Docs_New_Drs_met = new Literal();
            lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Seen</center>";
            tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
            tc_Docs_New_Drs_met.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_New_Drs_met.Style.Add("padding", "15px 5px");
            tc_Docs_New_Drs_met.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_New_Drs_met.Style.Add("border-top", "0px");
            tc_Docs_New_Drs_met.Style.Add("font-size", "12px");
            tc_Docs_New_Drs_met.Style.Add("font-weight", "400");
            tc_Docs_New_Drs_met.Style.Add("text-align", "center");
            tc_Docs_New_Drs_met.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_New_Drs_met.Style.Add("vertical-align", "inherit");
            tc_Docs_New_Drs_met.Style.Add("text-transform", "uppercase");
            tc_Docs_New_Drs_met.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

            TableCell tc_Docs_New_Call_Avg = new TableCell();
            tc_Docs_New_Call_Avg.Width = 50;
            Literal lit_Docs_New_Call_Avg = new Literal();
            lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
            tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
            tc_Docs_New_Call_Avg.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_New_Call_Avg.Style.Add("padding", "15px 5px");
            tc_Docs_New_Call_Avg.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_New_Call_Avg.Style.Add("border-top", "0px");
            tc_Docs_New_Call_Avg.Style.Add("font-size", "12px");
            tc_Docs_New_Call_Avg.Style.Add("font-weight", "400");
            tc_Docs_New_Call_Avg.Style.Add("text-align", "center");
            tc_Docs_New_Call_Avg.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_New_Call_Avg.Style.Add("vertical-align", "inherit");
            tc_Docs_New_Call_Avg.Style.Add("text-transform", "uppercase");
            tc_Docs_New_Call_Avg.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

            //TableCell tc_Docs_Remarks = new TableCell();
            //tc_Docs_Remarks.BorderStyle = BorderStyle.Solid;
            //tc_Docs_Remarks.BorderWidth = 1;
            //tc_Docs_Remarks.Width = 50;
            //Literal lit_Docs_Remarks = new Literal();
            //lit_Docs_Remarks.Text = "<center>Remarks</center>";
            //tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
            //tc_Docs_Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_Docs_Remarks.Style.Add("color", "white");
            //tc_Docs_Remarks.Style.Add("font-weight", "bold");
            //tc_Docs_Remarks.Style.Add("font-family", "Calibri");
            //tc_Docs_Remarks.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_Docs_Remarks);

            tbl.Rows.Add(tr_lst_det);
            //}


            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                //ListedDR lstDR = new ListedDR();
                //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
                }
                else
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Desig_Color"].ToString());
                }

                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Width = 30;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_sf_code = new TableCell();
                Literal lit_det_sf_code = new Literal();
                lit_det_sf_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_sf_code.Controls.Add(lit_det_sf_code);
                tc_sf_code.Visible = false;
                tr_det.Cells.Add(tc_sf_code);

                TableCell tc_det_sf_name = new TableCell();
                HyperLink lit_det_sf_name = new HyperLink();
                //lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                DataSet dsSf = new DataSet();
                SalesForce sf1 = new SalesForce();

                dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);


                if (dsSf.Tables[0].Rows.Count > 1)
                {
                    int i = dsSf.Tables[0].Rows.Count - 1;
                    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    string[] str = sf_name.Split('(');
                    int str1 = str.Count();
                    if (str1 >= 2)
                    {

                        sf_name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        str = sf_name.Split('(');
                        lit_det_sf_name.Text = "&nbsp;" + dsSf.Tables[0].Rows[0]["sf_name"].ToString().Replace(dsSf.Tables[0].Rows[0]["sf_name"].ToString(), "<span style='color:Red'>" + "( " + dsSf.Tables[0].Rows[0]["sf_name"].ToString() + " )" + "</span>");
                        lit_det_sf_name.Text = str[1];
                        lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                        //lit_det_sf_name.Text = str[1];
                        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");
                    }
                    else
                    {
                        //lit_det_sf_name.Text = str[1];
                        //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

                        lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    }
                }
                else
                {
                    lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                }

                tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_name.Width = 200;
                tc_det_sf_name.Controls.Add(lit_det_sf_name);
                tr_det.Cells.Add(tc_det_sf_name);



                TableCell tc_det_sf_doj = new TableCell();
                Literal lit_det_sf_doj = new Literal();
                lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                tc_det_sf_doj.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_doj);

                TableCell tc_det_sf_SF_Emp_ID = new TableCell();
                Literal lit_det_sf_SF_Emp_ID = new Literal();
                lit_det_sf_SF_Emp_ID.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
                tc_det_sf_SF_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_SF_Emp_ID.Controls.Add(lit_det_sf_SF_Emp_ID);
                tc_det_sf_SF_Emp_ID.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_SF_Emp_ID);

                months = Convert.ToInt16(ViewState["months"].ToString());

                //if (months > 0)
                //{

                tot_fldwrk = "";
                tot_dr = "";
                tot_doc_met = "";
                tot_doc_calls_seen = "";
                tot_CSH_calls_seen = "";
                tot_Stock_Calls_Seen = "";
                fldwrk_total = 0;
                doctor_total = 0;
                Chemist_total = 0;
                Stock_toatal = 0;
                Stock_calls_Seen_Total = 0;
                Dcr_Leave = 0;
                UnListDoc = 0;
                Dcr_Sub_days = 0;
                doc_met_total = 0;
                UnLstdoc_calls_seen_total = 0;
                doc_calls_seen_total = 0;
                CSH_calls_seen_total = 0;
                dblCoverage = 0.00;
                dblaverage = 0.00;

                // DCR_Sub_Days
                // DCR_TotalSubDaysQuery   
                dsDoc = dcs.DCR_TotalSubDaysQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

                TableCell tc_det_sf_dsd = new TableCell();
                Literal lit_det_sf_dsd = new Literal();
                lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_dsd);

                // DCR_Sub_Days

                // Field Work
                if (drFF["sf_code"].ToString().Contains("MR"))
                {
                    dsDoc = dcs.DCR_TotalFLDWRKQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
                }

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);

                TableCell tc_det_sf_FLDWRK = new TableCell();
                Literal lit_det_sf_FLDWRK = new Literal();
                lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                tr_det.Cells.Add(tc_det_sf_FLDWRK);

                // Field Work 

                // Leave

                dsDoc = dcs.DCR_TotalLeaveQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);

                TableCell tc_det_sf_Leave = new TableCell();
                Literal lit_det_sf_Leave = new Literal();
                lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_Leave);

                // Leave

                // Total Doctors
                sCurrentDate = months + "-01-" + iYear;
                //dtCurrent = Convert.ToDateTime(sCurrentDate);

                dsDoc = dcs.New_DCR_Visit_TotalDocQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                doctor_total = doctor_total + Convert.ToInt16(tot_dr);

                // Total Doctors

                ////DRs Calls Seen

                //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                TableCell tc_det_sf_tot_doc = new TableCell();
                Literal lit_det_sf_tot_doc = new Literal();
                //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                tr_det.Cells.Add(tc_det_sf_tot_doc);

                //DRs Calls Seen

                //Call Average

                decimal RoundLstCallAvg = new decimal();

                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                    RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Call Average

                // Chemist tot

                dsDoc = dcs.New_DCR_TotalChemistQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);



                // Chemist tot

                //Chemist Seen

                //dsDoc = dcs.DCR_CSH_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);

                TableCell tc_det_sf_tot_Chemist = new TableCell();
                Literal lit_det_sf_tot_Chemist = new Literal();
                //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();

                tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                tr_det.Cells.Add(tc_det_sf_tot_Chemist);
                //Chemist Seen

                // Chemist Call Average    
                decimal RoundChemCallAvg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
                    RoundChemCallAvg = Math.Round((decimal)dblaverage, 2);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundChemCallAvg;
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Chemist Call Average

                // Chemist POB

                //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(drFF["sf_code"].ToString(), divcode);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);

                TableCell tc_det_sf_Chemist_POB = new TableCell();
                Literal lit_det_sf_tot_POB = new Literal();
                lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                tc_det_sf_Chemist_POB.Visible = false;
                tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                // Chemist POB

                // Stock tot

                dsDoc = dcs.New_DCR_TotalStockistQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);


                // Stock tot

                //Stock Calls Seen                   


                //dsDoc = dcs.DCR_Stock_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);

                TableCell tc_det_sf_tot_Stock = new TableCell();
                Literal lit_det_sf_tot_Stock = new Literal();
                // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_tot_Stock);

                //Stock Calls Seen

                // Call Avg Stock

                //dsDoc = dcs.Get_Call_Total_Stock_Visit_Report(drFF["sf_code"].ToString(), divcode);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);

                decimal RoundStockCallAvg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));

                    dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
                    RoundStockCallAvg = Math.Round((decimal)dblaverage, 2);

                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0)
                {
                    TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                    Literal lit_det_sf_Call_Avg_Stock = new Literal();
                    lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockCallAvg.ToString();
                    tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                    tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                }
                else
                {
                    TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                    Literal lit_det_sf_Call_Avg_Stock = new Literal();
                    lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                    tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                    tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                }

                // Call Avg Stock

                // Unlist Doc tot

                dsDoc = dcs.New_DCR_TotalUnlstDocQuery_DateWise(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                if (dsDoc.Tables[0].Rows.Count > 0)
                    UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);

                // Unlist Doc tot

                // UnLstDRs Calls Seen

                //dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);

                TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                Literal lit_det_sf_UnList_tot_Stock = new Literal();
                //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                // UnLstDRs Calls Seen

                //Call Average
                decimal RoundUnLstCallAvg = new decimal();
                if (fldwrk_total > 0)
                {
                    //dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));
                    RoundUnLstCallAvg = Math.Round((decimal)dblaverage, 2);
                }
                else
                {
                    dblaverage = 0;
                }

                if (dblaverage != 0.0 && dblaverage != 0)
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + RoundUnLstCallAvg.ToString();
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }
                else
                {
                    TableCell tc_det_average = new TableCell();
                    Literal lit_det_average = new Literal();
                    lit_det_average.Text = "&nbsp;" + "0.0";
                    tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_average.VerticalAlign = VerticalAlign.Middle;
                    tc_det_average.Controls.Add(lit_det_average);
                    tr_det.Cells.Add(tc_det_average);
                }

                // Call Average 

                // Remarks

                //TableCell tc_det_doc_Remarks = new TableCell();
                //HyperLink lit_det_doc_Remarks = new HyperLink();
                //lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
                //sURL = "rptRemarks.aspx?sf_Name=" + drFF["SF_Name"].ToString() + "&sf_code=" + drFF["sf_code"].ToString() + "&Year=" + iYear + "&Month=" + iMonth + "";

                //lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                //lit_det_doc_Remarks.NavigateUrl = "#";

                //tc_det_doc_Remarks.BorderStyle = BorderStyle.Solid;
                //tc_det_doc_Remarks.Style.Add("font-family", "Calibri");
                //tc_det_doc_Remarks.BorderWidth = 1;
                //tc_det_doc_Remarks.Width = 50;

                //tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
                //tr_det.Cells.Add(tc_det_doc_Remarks);

                // Remarks

                // }

                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void BindGrid()
    {
        SalesForce sf = new SalesForce();
        DCR dcs = new DCR();

        DataSet dsSalesForce = new DataSet();
        DataSet dsDoc = new DataSet();

        dsDoc = dcs.DCR_Total_Call_Doc_Visit_Report(sfCode, divcode, Convert.ToDateTime("10-01-2015"));

        dsSalesForce = sf.SF_ReportingTo_TourPlan(divcode, sfCode);
        dsSalesForce.Merge(dsDoc);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //rptAverage.DataSource = dsSalesForce;
            //rptAverage.DataBind();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);

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

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        //string strFileName = Title;
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //HtmlForm frm = new HtmlForm();
        //pnlContents.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(pnlContents);
        //frm.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();

        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();


        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        pnlContents.RenderControl(hw);

        StringReader sr = new StringReader(sw.ToString());

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();

        htmlparser.Parse(sr);

        pdfDoc.Close();

        Response.Write(pdfDoc);

        Response.End();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    //private void FillDateWise_WithOut_Approval()
    //{
    //    tbl.Rows.Clear();
    //    doctor_total = 0;
    //    BindSf_Code();

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        TableRow tr_header = new TableRow();
    //        tr_header.BorderStyle = BorderStyle.Solid;
    //        tr_header.BorderWidth = 1;

    //        TableCell tc_SNo = new TableCell();
    //        tc_SNo.BorderStyle = BorderStyle.Solid;
    //        tc_SNo.BorderWidth = 1;
    //        tc_SNo.Width = 50;
    //        tc_SNo.RowSpan = 2;
    //        Literal lit_SNo = new Literal();
    //        lit_SNo.Text = "#";
    //        tc_SNo.Controls.Add(lit_SNo);
    //        tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_SNo.Style.Add("color", "white");
    //        tc_SNo.Style.Add("font-weight", "bold");
    //        tc_SNo.Style.Add("border-color", "Black");
    //        tc_SNo.HorizontalAlign = HorizontalAlign.Center;
    //        tc_SNo.Style.Add("font-family", "Calibri");
    //        tr_header.Cells.Add(tc_SNo);
    //        //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //        TableCell tc_DR_Code = new TableCell();
    //        tc_DR_Code.BorderStyle = BorderStyle.Solid;
    //        tc_DR_Code.BorderWidth = 1;
    //        tc_DR_Code.Width = 40;
    //        tc_DR_Code.RowSpan = 2;
    //        Literal lit_DR_Code = new Literal();
    //        lit_DR_Code.Text = "<center>SF Code</center>";
    //        tc_DR_Code.Controls.Add(lit_DR_Code);
    //        tc_DR_Code.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Code.Style.Add("color", "white");
    //        tc_DR_Code.Style.Add("font-weight", "bold");
    //        tc_DR_Code.Style.Add("font-family", "Calibri");
    //        tc_DR_Code.Style.Add("border-color", "Black");
    //        tc_DR_Code.Visible = false;
    //        tr_header.Cells.Add(tc_DR_Code);

    //        TableCell tc_DR_Name = new TableCell();
    //        tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //        tc_DR_Name.BorderWidth = 1;
    //        tc_DR_Name.Width = 200;
    //        tc_DR_Name.RowSpan = 2;
    //        Literal lit_DR_Name = new Literal();
    //        lit_DR_Name.Text = "<center>Field Force</center>";
    //        tc_DR_Name.Controls.Add(lit_DR_Name);
    //        tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Name.Style.Add("color", "white");
    //        tc_DR_Name.Style.Add("font-weight", "bold");
    //        tc_DR_Name.Style.Add("font-family", "Calibri");
    //        tc_DR_Name.Style.Add("border-color", "Black");
    //        tr_header.Cells.Add(tc_DR_Name);

    //        TableCell tc_DR_DOJ = new TableCell();
    //        tc_DR_DOJ.BorderStyle = BorderStyle.Solid;
    //        tc_DR_DOJ.BorderWidth = 1;
    //        tc_DR_DOJ.Width = 100;
    //        tc_DR_DOJ.RowSpan = 2;
    //        Literal lit_DR_DOJ = new Literal();
    //        lit_DR_DOJ.Text = "<center>DOJ</center>";
    //        tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
    //        tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
    //        tc_DR_DOJ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_DOJ.Style.Add("color", "white");
    //        tc_DR_DOJ.Style.Add("font-weight", "bold");
    //        tc_DR_DOJ.Style.Add("font-family", "Calibri");
    //        tc_DR_DOJ.Style.Add("border-color", "Black");
    //        tr_header.Cells.Add(tc_DR_DOJ);

    //        TableCell tc_DR_sf_Emp_ID = new TableCell();
    //        tc_DR_sf_Emp_ID.BorderStyle = BorderStyle.Solid;
    //        tc_DR_sf_Emp_ID.BorderWidth = 1;
    //        tc_DR_sf_Emp_ID.Width = 100;
    //        tc_DR_sf_Emp_ID.RowSpan = 2;
    //        Literal lit_DR_SF_Emp_ID = new Literal();
    //        lit_DR_SF_Emp_ID.Text = "<center>Employee ID</center>";
    //        tc_DR_sf_Emp_ID.Controls.Add(lit_DR_SF_Emp_ID);
    //        tc_DR_sf_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
    //        tc_DR_sf_Emp_ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_sf_Emp_ID.Style.Add("color", "white");
    //        tc_DR_sf_Emp_ID.Style.Add("font-weight", "bold");
    //        tc_DR_sf_Emp_ID.Style.Add("font-family", "Calibri");
    //        tc_DR_sf_Emp_ID.Style.Add("border-color", "Black");
    //        tr_header.Cells.Add(tc_DR_sf_Emp_ID);

    //        int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


    //        ViewState["months"] = months;


    //        Doctor dr = new Doctor();
    //        dsDoctor = dr.getDocCat(divcode);


    //        tbl.Rows.Add(tr_header);

    //        //Sub Header
    //        months = Convert.ToInt16(ViewState["months"].ToString());

    //        //if (months > 0)
    //        //{
    //        TableRow tr_lst_det = new TableRow();

    //        TableCell tc_DR_DCR_SubDays = new TableCell();
    //        tc_DR_DCR_SubDays.BorderStyle = BorderStyle.Solid;
    //        tc_DR_DCR_SubDays.BorderWidth = 1;
    //        tc_DR_DCR_SubDays.Width = 50;

    //        Literal lit_DR_DCR_SubDays = new Literal();
    //        lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
    //        tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
    //        tc_DR_DCR_SubDays.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_DCR_SubDays.Style.Add("color", "white");
    //        tc_DR_DCR_SubDays.Style.Add("font-weight", "bold");
    //        tc_DR_DCR_SubDays.Style.Add("font-family", "Calibri");
    //        tc_DR_DCR_SubDays.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);

    //        TableCell tc_DR_FldWrk = new TableCell();
    //        tc_DR_FldWrk.BorderStyle = BorderStyle.Solid;
    //        tc_DR_FldWrk.BorderWidth = 1;
    //        //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
    //        tc_DR_FldWrk.Width = 50;
    //        Literal lit_DR_FldWrk = new Literal();
    //        lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
    //        tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
    //        tc_DR_FldWrk.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_FldWrk.Style.Add("color", "white");
    //        tc_DR_FldWrk.Style.Add("font-weight", "bold");
    //        tc_DR_FldWrk.Style.Add("font-family", "Calibri");
    //        tc_DR_FldWrk.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_DR_FldWrk);

    //        TableCell tc_DR_Leave = new TableCell();
    //        tc_DR_Leave.BorderStyle = BorderStyle.Solid;
    //        tc_DR_Leave.BorderWidth = 1;
    //        //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
    //        tc_DR_Leave.Width = 50;
    //        Literal lit_DR_Leave = new Literal();
    //        lit_DR_Leave.Text = "<center>Leave</center>";
    //        tc_DR_Leave.Controls.Add(lit_DR_Leave);
    //        tc_DR_Leave.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Leave.Style.Add("color", "white");
    //        tc_DR_Leave.Style.Add("font-weight", "bold");
    //        tc_DR_Leave.Style.Add("font-family", "Calibri");
    //        tc_DR_Leave.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_DR_Leave);

    //        TableCell tc_DR_Total = new TableCell();
    //        tc_DR_Total.BorderStyle = BorderStyle.Solid;
    //        tc_DR_Total.BorderWidth = 1;
    //        tc_DR_Total.Width = 50;
    //        Literal lit_DR_Total = new Literal();
    //        lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
    //        tc_DR_Total.Controls.Add(lit_DR_Total);
    //        tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Total.Style.Add("color", "white");
    //        tc_DR_Total.Style.Add("font-weight", "bold");
    //        tc_DR_Total.Style.Add("font-family", "Calibri");
    //        tc_DR_Total.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_DR_Total);

    //        TableCell tc_average = new TableCell();
    //        tc_average.BorderStyle = BorderStyle.Solid;
    //        tc_average.BorderWidth = 1;
    //        tc_average.Width = 50;
    //        Literal lit_average = new Literal();
    //        lit_average.Text = "<center>Call <br>Average </center>";
    //        tc_average.Controls.Add(lit_average);
    //        tc_average.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_average.Style.Add("color", "white");
    //        tc_average.Style.Add("font-weight", "bold");
    //        tc_average.Style.Add("font-family", "Calibri");
    //        tc_average.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_average);

    //        TableCell tc_Docs_chemmet = new TableCell();
    //        tc_Docs_chemmet.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_chemmet.BorderWidth = 1;
    //        tc_Docs_chemmet.Width = 50;
    //        Literal lit_Docs_Chemmet = new Literal();
    //        lit_Docs_Chemmet.Text = "<center>Chmist <br> Seen</center>";
    //        tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
    //        tc_Docs_chemmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_chemmet.Style.Add("color", "white");
    //        tc_Docs_chemmet.Style.Add("font-weight", "bold");
    //        tc_Docs_chemmet.Style.Add("font-family", "Calibri");
    //        tc_Docs_chemmet.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_chemmet);

    //        TableCell tc_Docs_CallAvg = new TableCell();
    //        tc_Docs_CallAvg.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_CallAvg.BorderWidth = 1;
    //        tc_Docs_CallAvg.Width = 50;
    //        Literal lit_Docs_CallAvg = new Literal();
    //        lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
    //        tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
    //        tc_Docs_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_CallAvg.Style.Add("color", "white");
    //        tc_Docs_CallAvg.Style.Add("font-weight", "bold");
    //        tc_Docs_CallAvg.Style.Add("font-family", "Calibri");
    //        tc_Docs_CallAvg.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_CallAvg);

    //        TableCell tc_Docs_ChemPOB = new TableCell();
    //        tc_Docs_ChemPOB.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_ChemPOB.BorderWidth = 1;
    //        tc_Docs_ChemPOB.Width = 50;
    //        Literal lit_Docs_ChemPOB = new Literal();
    //        lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
    //        tc_Docs_ChemPOB.Visible = false;
    //        tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
    //        tc_Docs_ChemPOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_ChemPOB.Style.Add("color", "white");
    //        tc_Docs_ChemPOB.Style.Add("font-weight", "bold");
    //        tc_Docs_ChemPOB.Style.Add("font-family", "Calibri");
    //        tc_Docs_ChemPOB.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

    //        TableCell tc_Docs_Stockmet = new TableCell();
    //        tc_Docs_Stockmet.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_Stockmet.BorderWidth = 1;
    //        tc_Docs_Stockmet.Width = 50;
    //        Literal lit_Docs_Stockmet = new Literal();
    //        lit_Docs_Stockmet.Text = "<center>Stockist <br> Seen</center>";
    //        tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
    //        tc_Docs_Stockmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_Stockmet.Style.Add("color", "white");
    //        tc_Docs_Stockmet.Style.Add("font-weight", "bold");
    //        tc_Docs_Stockmet.Style.Add("font-family", "Calibri");
    //        tc_Docs_Stockmet.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_Stockmet);

    //        TableCell tc_Docs_Stock_CallAvg = new TableCell();
    //        tc_Docs_Stock_CallAvg.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_Stock_CallAvg.BorderWidth = 1;
    //        tc_Docs_Stock_CallAvg.Width = 50;
    //        Literal lit_Docs_Stock_CallAvg = new Literal();
    //        lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
    //        tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
    //        tc_Docs_Stock_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_Stock_CallAvg.Style.Add("color", "white");
    //        tc_Docs_Stock_CallAvg.Style.Add("font-weight", "bold");
    //        tc_Docs_Stock_CallAvg.Style.Add("font-family", "Calibri");
    //        tc_Docs_Stock_CallAvg.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

    //        TableCell tc_Docs_New_Drs_met = new TableCell();
    //        tc_Docs_New_Drs_met.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_New_Drs_met.BorderWidth = 1;
    //        tc_Docs_New_Drs_met.Width = 50;
    //        Literal lit_Docs_New_Drs_met = new Literal();
    //        lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Seen</center>";
    //        tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
    //        tc_Docs_New_Drs_met.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_New_Drs_met.Style.Add("color", "white");
    //        tc_Docs_New_Drs_met.Style.Add("font-weight", "bold");
    //        tc_Docs_New_Drs_met.Style.Add("font-family", "Calibri");
    //        tc_Docs_New_Drs_met.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

    //        TableCell tc_Docs_New_Call_Avg = new TableCell();
    //        tc_Docs_New_Call_Avg.BorderStyle = BorderStyle.Solid;
    //        tc_Docs_New_Call_Avg.BorderWidth = 1;
    //        tc_Docs_New_Call_Avg.Width = 50;
    //        Literal lit_Docs_New_Call_Avg = new Literal();
    //        lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
    //        tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
    //        tc_Docs_New_Call_Avg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_Docs_New_Call_Avg.Style.Add("color", "white");
    //        tc_Docs_New_Call_Avg.Style.Add("font-weight", "bold");
    //        tc_Docs_New_Call_Avg.Style.Add("font-family", "Calibri");
    //        tc_Docs_New_Call_Avg.Style.Add("border-color", "Black");
    //        tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

    //        //TableCell tc_Docs_Remarks = new TableCell();
    //        //tc_Docs_Remarks.BorderStyle = BorderStyle.Solid;
    //        //tc_Docs_Remarks.BorderWidth = 1;
    //        //tc_Docs_Remarks.Width = 50;
    //        //Literal lit_Docs_Remarks = new Literal();
    //        //lit_Docs_Remarks.Text = "<center>Remarks</center>";
    //        //tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
    //        //tc_Docs_Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        //tc_Docs_Remarks.Style.Add("color", "white");
    //        //tc_Docs_Remarks.Style.Add("font-weight", "bold");
    //        //tc_Docs_Remarks.Style.Add("font-family", "Calibri");
    //        //tc_Docs_Remarks.Style.Add("border-color", "Black");
    //        //tr_lst_det.Cells.Add(tc_Docs_Remarks);

    //        tbl.Rows.Add(tr_lst_det);
    //        //}


    //        // Details Section
    //        string sURL = string.Empty;
    //        int iCount = 0;
    //        int iCnt = 0;
    //        int imonth = 0;
    //        int iyear = 0;
    //        DCR dcs = new DCR();

    //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //        {
    //            //ListedDR lstDR = new ListedDR();
    //            //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

    //            TableRow tr_det = new TableRow();
    //            if (Session["sf_type"].ToString() == "1")
    //            {
    //                tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
    //            }
    //            else
    //            {
    //                tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Desig_Color"].ToString());
    //            }

    //            iCount += 1;
    //            TableCell tc_det_SNo = new TableCell();
    //            Literal lit_det_SNo = new Literal();
    //            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //            tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_det_SNo.BorderWidth = 1;
    //            tc_det_SNo.Width = 50;
    //            tc_det_SNo.Controls.Add(lit_det_SNo);
    //            tr_det.Cells.Add(tc_det_SNo);

    //            TableCell tc_sf_code = new TableCell();
    //            Literal lit_det_sf_code = new Literal();
    //            lit_det_sf_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
    //            tc_sf_code.BorderStyle = BorderStyle.Solid;
    //            tc_sf_code.BorderWidth = 1;
    //            tc_sf_code.Controls.Add(lit_det_sf_code);
    //            tc_sf_code.Visible = false;
    //            tr_det.Cells.Add(tc_sf_code);

    //            TableCell tc_det_sf_name = new TableCell();
    //            HyperLink lit_det_sf_name = new HyperLink();
    //            //lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
    //            DataSet dsSf = new DataSet();
    //            SalesForce sf1 = new SalesForce();

    //            dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);


    //            if (dsSf.Tables[0].Rows.Count > 1)
    //            {
    //                int i = dsSf.Tables[0].Rows.Count - 1;
    //                string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

    //                string[] str = sf_name.Split('(');
    //                int str1 = str.Count();
    //                if (str1 >= 2)
    //                {

    //                    sf_name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                    str = sf_name.Split('(');
    //                    lit_det_sf_name.Text = "&nbsp;" + dsSf.Tables[0].Rows[0]["sf_name"].ToString().Replace(dsSf.Tables[0].Rows[0]["sf_name"].ToString(), "<span style='color:Red'>" + "( " + dsSf.Tables[0].Rows[0]["sf_name"].ToString() + " )" + "</span>");
    //                    lit_det_sf_name.Text = str[1];
    //                    lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

    //                    //lit_det_sf_name.Text = str[1];
    //                    //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");
    //                }
    //                else
    //                {
    //                    //lit_det_sf_name.Text = str[1];
    //                    //lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");

    //                    lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString(); //+ " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
    //                }
    //            }
    //            else
    //            {
    //                lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString(); //+ " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
    //            }

    //            lit_det_sf_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"].ToString() + "','" + divcode + "','" + "Date Wise(With out Approval)" + "', '" + dtFrmDate + "', '" + dtToDate + "')");
    //            tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
    //            tc_det_sf_name.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_name.Style.Add("font-family", "Calibri");
    //            tc_det_sf_name.BorderWidth = 1;
    //            tc_det_sf_name.Width = 200;
    //            tc_det_sf_name.Controls.Add(lit_det_sf_name);
    //            tr_det.Cells.Add(tc_det_sf_name);



    //            TableCell tc_det_sf_doj = new TableCell();
    //            Literal lit_det_sf_doj = new Literal();
    //            lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
    //            tc_det_sf_doj.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_doj.BorderWidth = 1;
    //            tc_det_sf_doj.Style.Add("font-family", "Calibri");
    //            tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
    //            tc_det_sf_doj.Width = 100;
    //            //tc_det_sf_HQ.Visible = false;
    //            tr_det.Cells.Add(tc_det_sf_doj);


    //            TableCell tc_det_sf_SF_Emp_ID = new TableCell();
    //            Literal lit_det_sf_SF_Emp_ID = new Literal();
    //            lit_det_sf_SF_Emp_ID.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
    //            tc_det_sf_SF_Emp_ID.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_SF_Emp_ID.BorderWidth = 1;
    //            tc_det_sf_SF_Emp_ID.Style.Add("font-family", "Calibri");
    //            tc_det_sf_SF_Emp_ID.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_SF_Emp_ID.Controls.Add(lit_det_sf_SF_Emp_ID);
    //            tc_det_sf_SF_Emp_ID.Width = 100;
    //            //tc_det_sf_HQ.Visible = false;
    //            tr_det.Cells.Add(tc_det_sf_SF_Emp_ID);

    //            months = Convert.ToInt16(ViewState["months"].ToString());

    //            //if (months > 0)
    //            //{

    //            tot_fldwrk = "";
    //            tot_dr = "";
    //            tot_doc_met = "";
    //            tot_doc_calls_seen = "";
    //            tot_CSH_calls_seen = "";
    //            tot_Stock_Calls_Seen = "";
    //            fldwrk_total = 0;
    //            doctor_total = 0;
    //            Chemist_total = 0;
    //            Stock_toatal = 0;
    //            Stock_calls_Seen_Total = 0;
    //            Dcr_Leave = 0;
    //            UnListDoc = 0;
    //            Dcr_Sub_days = 0;
    //            doc_met_total = 0;
    //            UnLstdoc_calls_seen_total = 0;
    //            doc_calls_seen_total = 0;
    //            CSH_calls_seen_total = 0;
    //            dblCoverage = 0.00;
    //            dblaverage = 0.00;

    //            // DCR_Sub_Days
    //            // DCR_TotalSubDaysQuery   
    //            dsDoc = dcs.DCR_TotalSubDaysQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

    //            TableCell tc_det_sf_dsd = new TableCell();
    //            Literal lit_det_sf_dsd = new Literal();
    //            lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
    //            tc_det_sf_dsd.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_dsd.BorderWidth = 1;
    //            tc_det_sf_dsd.Style.Add("font-family", "Calibri");
    //            tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
    //            //tc_det_sf_HQ.Visible = false;
    //            tr_det.Cells.Add(tc_det_sf_dsd);

    //            // DCR_Sub_Days

    //            // Field Work
    //            if (drFF["sf_code"].ToString().Contains("MR"))
    //            {
    //                dsDoc = dcs.DCR_TotalFLDWRKQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
    //            }
    //            else
    //            {
    //                dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
    //            }

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //            TableCell tc_det_sf_FLDWRK = new TableCell();
    //            Literal lit_det_sf_FLDWRK = new Literal();
    //            lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
    //            tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_FLDWRK.BorderWidth = 1;
    //            tc_det_sf_FLDWRK.Style.Add("font-family", "Calibri");
    //            tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
    //            tr_det.Cells.Add(tc_det_sf_FLDWRK);

    //            // Field Work 

    //            // Leave

    //            dsDoc = dcs.DCR_TotalLeaveQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);

    //            TableCell tc_det_sf_Leave = new TableCell();
    //            Literal lit_det_sf_Leave = new Literal();
    //            lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
    //            tc_det_sf_Leave.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_Leave.Style.Add("font-family", "Calibri");
    //            tc_det_sf_Leave.BorderWidth = 1;
    //            tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
    //            //tc_det_sf_HQ.Visible = false;
    //            tr_det.Cells.Add(tc_det_sf_Leave);

    //            // Leave

    //            // Total Doctors
    //            sCurrentDate = months + "-01-" + iYear;
    //            //dtCurrent = Convert.ToDateTime(sCurrentDate);

    //            dsDoc = dcs.New_DCR_Visit_TotalDocQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //            doctor_total = doctor_total + Convert.ToInt16(tot_dr);

    //            // Total Doctors

    //            ////DRs Calls Seen

    //            //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

    //            TableCell tc_det_sf_tot_doc = new TableCell();
    //            Literal lit_det_sf_tot_doc = new Literal();
    //            //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
    //            lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
    //            tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_tot_doc.BorderWidth = 1;
    //            tc_det_sf_tot_doc.Style.Add("font-family", "Calibri");
    //            tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
    //            tr_det.Cells.Add(tc_det_sf_tot_doc);

    //            //DRs Calls Seen

    //            //Call Average

    //            decimal RoundLstCallAvg = new decimal();

    //            if (fldwrk_total > 0)
    //            {
    //                //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
    //                dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
    //                RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
    //            }
    //            else
    //            {
    //                dblaverage = 0;
    //            }

    //            if (dblaverage != 0.0)
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_average.Style.Add("font-family", "Calibri");
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }
    //            else
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + "0.0";
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_average.Style.Add("font-family", "Calibri");
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }

    //            // Call Average

    //            // Chemist tot

    //            dsDoc = dcs.New_DCR_TotalChemistQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);



    //            // Chemist tot

    //            //Chemist Seen

    //            //dsDoc = dcs.DCR_CSH_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);

    //            TableCell tc_det_sf_tot_Chemist = new TableCell();
    //            Literal lit_det_sf_tot_Chemist = new Literal();
    //            //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
    //            lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();

    //            tc_det_sf_tot_Chemist.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_tot_Chemist.BorderWidth = 1;
    //            tc_det_sf_tot_Chemist.Style.Add("font-family", "Calibri");
    //            tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
    //            tr_det.Cells.Add(tc_det_sf_tot_Chemist);
    //            //Chemist Seen

    //            // Chemist Call Average    
    //            decimal RoundChemCallAvg = new decimal();
    //            if (fldwrk_total > 0)
    //            {
    //                //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
    //                dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
    //                RoundChemCallAvg = Math.Round((decimal)dblaverage, 2);
    //            }
    //            else
    //            {
    //                dblaverage = 0;
    //            }

    //            if (dblaverage != 0.0)
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + RoundChemCallAvg;
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_average.Style.Add("font-family", "Calibri");
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }
    //            else
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + "0.0";
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_sf_Leave.Style.Add("font-family", "Calibri");
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }

    //            // Chemist Call Average

    //            // Chemist POB

    //            //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(drFF["sf_code"].ToString(), divcode);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);

    //            TableCell tc_det_sf_Chemist_POB = new TableCell();
    //            Literal lit_det_sf_tot_POB = new Literal();
    //            lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
    //            tc_det_sf_Chemist_POB.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_Chemist_POB.BorderWidth = 1;
    //            tc_det_sf_Chemist_POB.Style.Add("font-family", "Calibri");
    //            tc_det_sf_Chemist_POB.Visible = false;
    //            tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
    //            tr_det.Cells.Add(tc_det_sf_Chemist_POB);

    //            // Chemist POB

    //            // Stock tot

    //            dsDoc = dcs.New_DCR_TotalStockistQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);


    //            // Stock tot

    //            //Stock Calls Seen                   


    //            //dsDoc = dcs.DCR_Stock_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);

    //            TableCell tc_det_sf_tot_Stock = new TableCell();
    //            Literal lit_det_sf_tot_Stock = new Literal();
    //            // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
    //            lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
    //            tc_det_sf_tot_Stock.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_tot_Stock.BorderWidth = 1;
    //            tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
    //            tr_det.Cells.Add(tc_det_sf_tot_Stock);

    //            //Stock Calls Seen

    //            // Call Avg Stock

    //            //dsDoc = dcs.Get_Call_Total_Stock_Visit_Report(drFF["sf_code"].ToString(), divcode);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);

    //            decimal RoundStockCallAvg = new decimal();
    //            if (fldwrk_total > 0)
    //            {
    //                //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));

    //                dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
    //                RoundStockCallAvg = Math.Round((decimal)dblaverage, 2);

    //            }
    //            else
    //            {
    //                dblaverage = 0;
    //            }

    //            if (dblaverage != 0.0)
    //            {
    //                TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
    //                Literal lit_det_sf_Call_Avg_Stock = new Literal();
    //                lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockCallAvg.ToString();
    //                tc_det_sf_Call_Avg_Stock.Style.Add("font-family", "Calibri");
    //                tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
    //                tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
    //                tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
    //                tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
    //            }
    //            else
    //            {
    //                TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
    //                Literal lit_det_sf_Call_Avg_Stock = new Literal();
    //                lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
    //                tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
    //                tc_det_sf_Call_Avg_Stock.Style.Add("font-family", "Calibri");
    //                tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
    //                tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
    //                tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
    //            }

    //            // Call Avg Stock

    //            // Unlist Doc tot

    //            dsDoc = dcs.New_DCR_TotalUnlstDocQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

    //            if (dsDoc.Tables[0].Rows.Count > 0)
    //                UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);

    //            // Unlist Doc tot

    //            // UnLstDRs Calls Seen

    //            //dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

    //            //if (dsDoc.Tables[0].Rows.Count > 0)
    //            //    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);

    //            TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
    //            Literal lit_det_sf_UnList_tot_Stock = new Literal();
    //            //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
    //            lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
    //            tc_det_sf_UnList_tot_Stock.BorderStyle = BorderStyle.Solid;
    //            tc_det_sf_UnList_tot_Stock.Style.Add("font-family", "Calibri");
    //            tc_det_sf_UnList_tot_Stock.BorderWidth = 1;
    //            tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
    //            tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
    //            tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
    //            tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

    //            // UnLstDRs Calls Seen

    //            //Call Average
    //            decimal RoundUnLstCallAvg = new decimal();
    //            if (fldwrk_total > 0)
    //            {
    //                //dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
    //                dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));
    //                RoundUnLstCallAvg = Math.Round((decimal)dblaverage, 2);
    //            }
    //            else
    //            {
    //                dblaverage = 0;
    //            }

    //            if (dblaverage != 0.0 && dblaverage != 0)
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + RoundUnLstCallAvg.ToString();
    //                tc_det_average.Style.Add("font-family", "Calibri");
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }
    //            else
    //            {
    //                TableCell tc_det_average = new TableCell();
    //                Literal lit_det_average = new Literal();
    //                lit_det_average.Text = "&nbsp;" + "0.0";
    //                tc_det_average.Style.Add("font-family", "Calibri");
    //                tc_det_average.BorderStyle = BorderStyle.Solid;
    //                tc_det_average.BorderWidth = 1;
    //                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                tc_det_average.Controls.Add(lit_det_average);
    //                tr_det.Cells.Add(tc_det_average);
    //            }

    //            // Call Average 

    //            // Remarks

    //            //TableCell tc_det_doc_Remarks = new TableCell();
    //            //HyperLink lit_det_doc_Remarks = new HyperLink();
    //            //lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
    //            //sURL = "rptRemarks.aspx?sf_Name=" + drFF["SF_Name"].ToString() + "&sf_code=" + drFF["sf_code"].ToString() + "&Year=" + iYear + "&Month=" + iMonth + "";

    //            //lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
    //            //lit_det_doc_Remarks.NavigateUrl = "#";

    //            //tc_det_doc_Remarks.BorderStyle = BorderStyle.Solid;
    //            //tc_det_doc_Remarks.Style.Add("font-family", "Calibri");
    //            //tc_det_doc_Remarks.BorderWidth = 1;
    //            //tc_det_doc_Remarks.Width = 50;

    //            //tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
    //            //tr_det.Cells.Add(tc_det_doc_Remarks);

    //            // Remarks

    //            // }

    //            tbl.Rows.Add(tr_det);

    //        }
    //    }
    //}

    private void FillDateWise_WithOut_Approval()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            ////tr_header.BorderStyle = BorderStyle.Solid;
            ////tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 30;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.Controls.Add(lit_SNo);
            ////tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            ////tc_SNo.Style.Add("color", "white");
            ////tc_SNo.Style.Add("font-weight", "bold");
            ////tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "400");
            tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo.Style.Add("font-size", "12px");
            tc_SNo.Style.Add("border-bottom", "10px solid #fff");
            tc_SNo.Style.Add("font-family", "Roboto");
            tc_SNo.Style.Add("border-left", "0px solid #F1F5F8");
            tc_SNo.Attributes.Add("class", "stickyFirstRow");
            tc_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.Width = 40;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Style.Add("padding", "15px 5px");
            tc_DR_Code.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Code.Style.Add("border-top", "0px");
            tc_DR_Code.Style.Add("font-size", "12px");
            tc_DR_Code.Style.Add("font-weight", "400");
            tc_DR_Code.Style.Add("text-align", "center");
            tc_DR_Code.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Code.Style.Add("vertical-align", "inherit");
            tc_DR_Code.Style.Add("text-transform", "uppercase");
            tc_DR_Code.Attributes.Add("class", "stickyFirstRow");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tc_DR_Name.Style.Add("padding", "15px 5px");
            tc_DR_Name.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Name.Style.Add("border-top", "0px");
            tc_DR_Name.Style.Add("font-size", "12px");
            tc_DR_Name.Style.Add("font-weight", "400");
            tc_DR_Name.Style.Add("text-align", "center");
            tc_DR_Name.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Name.Style.Add("vertical-align", "inherit");
            tc_DR_Name.Style.Add("text-transform", "uppercase");
            tc_DR_Name.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_Start = new TableCell();
            tc_DR_Start.Width = 50;
            tc_DR_Start.RowSpan = 2;
            Literal lit_DR_Start = new Literal();
            lit_DR_Start.Text = "<center>Start Time</center>";
            tc_DR_Start.Controls.Add(lit_DR_Start);
            tc_DR_Start.Style.Add("padding", "15px 5px");
            tc_DR_Start.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Start.Style.Add("border-top", "0px");
            tc_DR_Start.Style.Add("font-size", "12px");
            tc_DR_Start.Style.Add("font-weight", "400");
            tc_DR_Start.Style.Add("text-align", "center");
            tc_DR_Start.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Start.Style.Add("vertical-align", "inherit");
            tc_DR_Start.Style.Add("text-transform", "uppercase");
            tc_DR_Start.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_Start);

            TableCell tc_DR_EndTime = new TableCell();
            tc_DR_EndTime.Width = 50;
            tc_DR_EndTime.RowSpan = 2;
            Literal lit_DR_EndTime = new Literal();
            lit_DR_EndTime.Text = "<center>End Time</center>";
            tc_DR_EndTime.Controls.Add(lit_DR_EndTime);
            tc_DR_EndTime.Style.Add("padding", "15px 5px");
            tc_DR_EndTime.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_EndTime.Style.Add("border-top", "0px");
            tc_DR_EndTime.Style.Add("font-size", "12px");
            tc_DR_EndTime.Style.Add("font-weight", "400");
            tc_DR_EndTime.Style.Add("text-align", "center");
            tc_DR_EndTime.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_EndTime.Style.Add("vertical-align", "inherit");
            tc_DR_EndTime.Style.Add("text-transform", "uppercase");
            tc_DR_EndTime.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_EndTime);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.Width = 100;
            tc_DR_DOJ.RowSpan = 2;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>DOJ</center>";
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_DOJ.Style.Add("padding", "15px 5px");
            tc_DR_DOJ.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_DOJ.Style.Add("border-top", "0px");
            tc_DR_DOJ.Style.Add("font-size", "12px");
            tc_DR_DOJ.Style.Add("font-weight", "400");
            tc_DR_DOJ.Style.Add("text-align", "center");
            tc_DR_DOJ.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_DOJ.Style.Add("vertical-align", "inherit");
            tc_DR_DOJ.Style.Add("text-transform", "uppercase");
            tc_DR_DOJ.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_DR_DOJ);

            int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


            ViewState["months"] = months;


            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(divcode);


            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());

            //if (months > 0)
            //{
            TableRow tr_lst_det = new TableRow();

            TableCell tc_DR_DCR_SubDays = new TableCell();
            tc_DR_DCR_SubDays.Width = 50;

            //Literal lit_DR_DCR_SubDays = new Literal();
            //lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
            //tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
            //tc_DR_DCR_SubDays.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_DR_DCR_SubDays.Style.Add("color", "white");
            //tc_DR_DCR_SubDays.Style.Add("font-weight", "bold");
            //tc_DR_DCR_SubDays.Style.Add("font-family", "Calibri");
            //tc_DR_DCR_SubDays.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);

            TableCell tc_DR_FldWrk = new TableCell();
            //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
            tc_DR_FldWrk.Width = 50;
            Literal lit_DR_FldWrk = new Literal();
            lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
            tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
            tc_DR_FldWrk.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_FldWrk.Style.Add("padding", "15px 5px");
            tc_DR_FldWrk.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_FldWrk.Style.Add("border-top", "0px");
            tc_DR_FldWrk.Style.Add("font-size", "12px");
            tc_DR_FldWrk.Style.Add("font-weight", "400");
            tc_DR_FldWrk.Style.Add("text-align", "center");
            tc_DR_FldWrk.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
            tc_DR_FldWrk.Style.Add("text-transform", "uppercase");
            tc_DR_FldWrk.Attributes.Add("class", "stickyFirstRow");
            tc_DR_FldWrk.Style.Add("color", "#636d73");
            tr_lst_det.Cells.Add(tc_DR_FldWrk);

            TableCell tc_DR_Leave = new TableCell();
            //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
            tc_DR_Leave.Width = 50;
            Literal lit_DR_Leave = new Literal();
            lit_DR_Leave.Text = "<center>Leave</center>";
            tc_DR_Leave.Controls.Add(lit_DR_Leave);
            tc_DR_Leave.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Leave.Style.Add("padding", "15px 5px");
            tc_DR_Leave.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Leave.Style.Add("border-top", "0px");
            tc_DR_Leave.Style.Add("font-size", "12px");
            tc_DR_Leave.Style.Add("font-weight", "400");
            tc_DR_Leave.Style.Add("text-align", "center");
            tc_DR_Leave.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Leave.Style.Add("vertical-align", "inherit");
            tc_DR_Leave.Style.Add("text-transform", "uppercase");
            tc_DR_Leave.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_DR_Leave);

            TableCell tc_DR_Total = new TableCell();
            tc_DR_Total.Width = 50;
            Literal lit_DR_Total = new Literal();
            lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
            tc_DR_Total.Controls.Add(lit_DR_Total);
            tc_DR_Total.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_DR_Total.Style.Add("padding", "15px 5px");
            tc_DR_Total.Style.Add("border-bottom", "10px solid #fff");
            tc_DR_Total.Style.Add("border-top", "0px");
            tc_DR_Total.Style.Add("font-size", "12px");
            tc_DR_Total.Style.Add("font-weight", "400");
            tc_DR_Total.Style.Add("text-align", "center");
            tc_DR_Total.Style.Add("border-left", "1px solid #DCE2E8");
            tc_DR_Total.Style.Add("vertical-align", "inherit");
            tc_DR_Total.Style.Add("text-transform", "uppercase");
            tc_DR_Total.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_DR_Total);

            //TableCell tc_average = new TableCell();
            //tc_average.BorderStyle = BorderStyle.Solid;
            //tc_average.BorderWidth = 1;
            //tc_average.Width = 50;
            //Literal lit_average = new Literal();
            //lit_average.Text = "<center>Call <br>Average </center>";
            //tc_average.Controls.Add(lit_average);
            //tc_average.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_average.Style.Add("color", "white");
            //tc_average.Style.Add("font-weight", "bold");
            //tc_average.Style.Add("font-family", "Calibri");
            //tc_average.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_average);

            TableCell tc_Docs_chemmet = new TableCell();
            tc_Docs_chemmet.Width = 50;
            Literal lit_Docs_Chemmet = new Literal();
            lit_Docs_Chemmet.Text = "<center>Chemist <br> Visit</center>";
            tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
            tc_Docs_chemmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_chemmet.Style.Add("padding", "15px 5px");
            tc_Docs_chemmet.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_chemmet.Style.Add("border-top", "0px");
            tc_Docs_chemmet.Style.Add("font-size", "12px");
            tc_Docs_chemmet.Style.Add("font-weight", "400");
            tc_Docs_chemmet.Style.Add("text-align", "center");
            tc_Docs_chemmet.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_chemmet.Style.Add("vertical-align", "inherit");
            tc_Docs_chemmet.Style.Add("text-transform", "uppercase");
            tc_Docs_chemmet.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_chemmet);

            //TableCell tc_Docs_CallAvg = new TableCell();
            //tc_Docs_CallAvg.BorderStyle = BorderStyle.Solid;
            //tc_Docs_CallAvg.BorderWidth = 1;
            //tc_Docs_CallAvg.Width = 50;
            //Literal lit_Docs_CallAvg = new Literal();
            //lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
            //tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
            //tc_Docs_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_Docs_CallAvg.Style.Add("color", "white");
            //tc_Docs_CallAvg.Style.Add("font-weight", "bold");
            //tc_Docs_CallAvg.Style.Add("font-family", "Calibri");
            //tc_Docs_CallAvg.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_Docs_CallAvg);

            TableCell tc_Docs_ChemPOB = new TableCell();
            tc_Docs_ChemPOB.Width = 50;
            Literal lit_Docs_ChemPOB = new Literal();
            lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
            tc_Docs_ChemPOB.Visible = false;
            tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
            tc_Docs_ChemPOB.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_ChemPOB.Style.Add("padding", "15px 5px");
            tc_Docs_ChemPOB.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_ChemPOB.Style.Add("border-top", "0px");
            tc_Docs_ChemPOB.Style.Add("font-size", "12px");
            tc_Docs_ChemPOB.Style.Add("font-weight", "400");
            tc_Docs_ChemPOB.Style.Add("text-align", "center");
            tc_Docs_ChemPOB.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_ChemPOB.Style.Add("vertical-align", "inherit");
            tc_Docs_ChemPOB.Style.Add("text-transform", "uppercase");
            tc_Docs_ChemPOB.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

            TableCell tc_Docs_Stockmet = new TableCell();
            tc_Docs_Stockmet.Width = 50;
            Literal lit_Docs_Stockmet = new Literal();
            lit_Docs_Stockmet.Text = "<center>Stockist <br> Seen</center>";
            tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
            tc_Docs_Stockmet.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_Stockmet.Style.Add("padding", "15px 5px");
            tc_Docs_Stockmet.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_Stockmet.Style.Add("border-top", "0px");
            tc_Docs_Stockmet.Style.Add("font-size", "12px");
            tc_Docs_Stockmet.Style.Add("font-weight", "400");
            tc_Docs_Stockmet.Style.Add("text-align", "center");
            tc_Docs_Stockmet.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_Stockmet.Style.Add("vertical-align", "inherit");
            tc_Docs_Stockmet.Style.Add("text-transform", "uppercase");
            tc_Docs_Stockmet.Attributes.Add("class", "stickyFirstRow");
            tr_lst_det.Cells.Add(tc_Docs_Stockmet);

            //TableCell tc_Docs_Stock_CallAvg = new TableCell();
            //tc_Docs_Stock_CallAvg.BorderStyle = BorderStyle.Solid;
            //tc_Docs_Stock_CallAvg.BorderWidth = 1;
            //tc_Docs_Stock_CallAvg.Width = 50;
            //Literal lit_Docs_Stock_CallAvg = new Literal();
            //lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
            //tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
            //tc_Docs_Stock_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_Docs_Stock_CallAvg.Style.Add("color", "white");
            //tc_Docs_Stock_CallAvg.Style.Add("font-weight", "bold");
            //tc_Docs_Stock_CallAvg.Style.Add("font-family", "Calibri");
            //tc_Docs_Stock_CallAvg.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

            TableCell tc_Docs_New_Drs_met = new TableCell();
            tc_Docs_New_Drs_met.Width = 50;
            Literal lit_Docs_New_Drs_met = new Literal();
            lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Seen</center>";
            tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
            tc_Docs_New_Drs_met.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tc_Docs_New_Drs_met.Style.Add("padding", "15px 5px");
            tc_Docs_New_Drs_met.Style.Add("border-bottom", "10px solid #fff");
            tc_Docs_New_Drs_met.Style.Add("border-top", "0px");
            tc_Docs_New_Drs_met.Style.Add("font-size", "12px");
            tc_Docs_New_Drs_met.Style.Add("font-weight", "400");
            tc_Docs_New_Drs_met.Style.Add("text-align", "center");
            tc_Docs_New_Drs_met.Style.Add("border-left", "1px solid #DCE2E8");
            tc_Docs_New_Drs_met.Style.Add("vertical-align", "inherit");
            tc_Docs_New_Drs_met.Style.Add("text-transform", "uppercase");
            tc_Docs_New_Drs_met.Attributes.Add("class", "stickyFirstRow");
            tc_Docs_New_Drs_met.Style.Add("border-radius", "0px 8px 8px 0px");
            tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

            //TableCell tc_Docs_New_Call_Avg = new TableCell();
            //tc_Docs_New_Call_Avg.BorderStyle = BorderStyle.Solid;
            //tc_Docs_New_Call_Avg.BorderWidth = 1;
            //tc_Docs_New_Call_Avg.Width = 50;
            //Literal lit_Docs_New_Call_Avg = new Literal();
            //lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
            //tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
            //tc_Docs_New_Call_Avg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_Docs_New_Call_Avg.Style.Add("color", "white");
            //tc_Docs_New_Call_Avg.Style.Add("font-weight", "bold");
            //tc_Docs_New_Call_Avg.Style.Add("font-family", "Calibri");
            //tc_Docs_New_Call_Avg.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

            //TableCell tc_Docs_Remarks = new TableCell();
            //tc_Docs_Remarks.BorderStyle = BorderStyle.Solid;
            //tc_Docs_Remarks.BorderWidth = 1;
            //tc_Docs_Remarks.Width = 50;
            //Literal lit_Docs_Remarks = new Literal();
            //lit_Docs_Remarks.Text = "<center>Remarks</center>";
            //tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
            //tc_Docs_Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_Docs_Remarks.Style.Add("color", "white");
            //tc_Docs_Remarks.Style.Add("font-weight", "bold");
            //tc_Docs_Remarks.Style.Add("font-family", "Calibri");
            //tc_Docs_Remarks.Style.Add("border-color", "Black");
            //tr_lst_det.Cells.Add(tc_Docs_Remarks);

            tbl.Rows.Add(tr_lst_det);
            //}


            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();
            DataSet dsDocDate = new DataSet();
            DataSet dsAtten = new DataSet();

            dsAtten = dcs.DCR_Atten_App(divcode, dtToDate.ToString("MM/dd/yyyy"));

            dsDocDate = dcs.sp_DCR_TotalSubDaysQuery_DateWise(divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

            dsTotalQuery = dcs.sp_DCR_Visit_TotalDocQuery_DateWise(divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

            dsChemist = dcs.New_DCR_TotalChemist(divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"), 2);

            dsStock = dcs.New_DCR_TotalChemist(divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"), 3);

            dsUnListDoc = dcs.sp_DCR_TotalUnlstDocQuery_DateWise(divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                //ListedDR lstDR = new ListedDR();
                //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
                }
                else
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Desig_Color"].ToString());
                }

                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                //tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 30;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_sf_code = new TableCell();
                Literal lit_det_sf_code = new Literal();
                lit_det_sf_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                //tc_sf_code.BorderStyle = BorderStyle.Solid;
                //tc_sf_code.BorderWidth = 1;
                tc_sf_code.Controls.Add(lit_det_sf_code);
                tc_sf_code.Visible = false;
                tr_det.Cells.Add(tc_sf_code);

                TableCell tc_det_sf_name = new TableCell();
                HyperLink lit_det_sf_name = new HyperLink();
                //lit_det_sf_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                DataSet dsSf = new DataSet();
                SalesForce sf1 = new SalesForce();

                dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);


                if (dsSf.Tables[0].Rows.Count > 1)
                {
                    int i = dsSf.Tables[0].Rows.Count - 1;
                    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    string[] str = sf_name.Split('(');
                    int str1 = str.Count();
                    if (str1 >= 2)
                    {

                        sf_name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        str = sf_name.Split('(');
                        lit_det_sf_name.Text = "&nbsp;" + dsSf.Tables[0].Rows[0]["sf_name"].ToString().Replace(dsSf.Tables[0].Rows[0]["sf_name"].ToString(), "<span style='color:Red'>" + "( " + dsSf.Tables[0].Rows[0]["sf_name"].ToString() + " )" + "</span>");
                        lit_det_sf_name.Text = str[1];
                        lit_det_sf_name.Text = str[0] + lit_det_sf_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");


                    }
                    else
                    {

                        lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    }
                }
                else
                {
                    lit_det_sf_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                }

                lit_det_sf_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"].ToString() + "','" + divcode + "','" + "Date Wise(With out Approval)" + "', '" + dtFrmDate + "', '" + dtToDate + "')");
                tc_det_sf_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_name.Width = 200;
                tc_det_sf_name.Controls.Add(lit_det_sf_name);
                tr_det.Cells.Add(tc_det_sf_name);

                dsAtten.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "' ";
                DataTable dt = dsAtten.Tables[0].DefaultView.ToTable("table1");
                DataSet dsAt1 = new DataSet();
                dsAt1.Merge(dt);

                string strStartTime = "";
                if (dsAt1.Tables[0].Rows.Count > 0)
                {
                    strStartTime = dsAt1.Tables[0].Rows[0]["Start_Time"].ToString();
                }

                TableCell tc_det_Start = new TableCell();
                Literal lit_det_Start = new Literal();
                lit_det_Start.Text = "&nbsp;" + strStartTime.ToString();
                tc_det_Start.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Start.Controls.Add(lit_det_Start);
                tc_det_Start.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_Start);

                string strEndTime = "";
                if (dsAt1.Tables[0].Rows.Count > 0)
                {
                    strEndTime = dsAt1.Tables[0].Rows[0]["End_Time"].ToString();
                }

                TableCell tc_det_sf_EndTime = new TableCell();
                Literal lit_det_EndTime = new Literal();
                lit_det_EndTime.Text = "&nbsp;" + strEndTime.ToString();
                tc_det_sf_EndTime.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_EndTime.Controls.Add(lit_det_EndTime);
                tc_det_sf_EndTime.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_EndTime);

                TableCell tc_det_sf_doj = new TableCell();
                Literal lit_det_sf_doj = new Literal();
                lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                tc_det_sf_doj.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_doj);

                months = Convert.ToInt16(ViewState["months"].ToString());



                tot_fldwrk = "";
                tot_dr = "";
                tot_doc_met = "";
                tot_doc_calls_seen = "";
                tot_CSH_calls_seen = "";
                tot_Stock_Calls_Seen = "";
                fldwrk_total = 0;
                doctor_total = 0;
                Chemist_total = 0;
                Stock_toatal = 0;
                Stock_calls_Seen_Total = 0;
                Dcr_Leave = 0;
                UnListDoc = 0;
                Dcr_Sub_days = 0;
                doc_met_total = 0;
                UnLstdoc_calls_seen_total = 0;
                doc_calls_seen_total = 0;
                CSH_calls_seen_total = 0;
                dblCoverage = 0.00;
                dblaverage = 0.00;

                // DCR_Sub_Days
                // DCR_TotalSubDaysQuery   
                //dsDoc = dcs.DCR_TotalSubDaysQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsDocDate.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "' ";
                dt = dsDocDate.Tables[0].DefaultView.ToTable("table1");
                DataSet ds1 = new DataSet();
                //dsDoc.Clear();
                ds1.Merge(dt);


                if (ds1.Tables[0].Rows.Count > 0)
                {
                    tot_Sub_days = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);
                }

                TableCell tc_det_sf_dsd = new TableCell();
                Literal lit_det_sf_dsd = new Literal();
                if (Dcr_Sub_days != 0)
                {
                    lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                }
                else
                {
                    lit_det_sf_dsd.Text = "";
                }
                tc_det_sf_dsd.Visible = false;
                tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_dsd);

                // DCR_Sub_Days

                // Field Work

                dsDocDate.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "' and FieldWork_Indicator='F' ";
                dt = dsDocDate.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                //if (drFF["sf_code"].ToString().Contains("MR"))
                //{
                //    dsDoc = dcs.DCR_TotalFLDWRKQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
                //}
                //else
                //{
                //    dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));
                //}

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);
                }

                TableCell tc_det_sf_FLDWRK = new TableCell();
                Literal lit_det_sf_FLDWRK = new Literal();
                if (fldwrk_total != 0)
                {
                    lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                }
                else
                {
                    lit_det_sf_FLDWRK.Text = "";
                }
                tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                tr_det.Cells.Add(tc_det_sf_FLDWRK);

                // Field Work 

                // Leave

                //dsDoc = dcs.DCR_TotalLeaveQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsDocDate.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "' and FieldWork_Indicator='L' ";
                dt = dsDocDate.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);
                }

                TableCell tc_det_sf_Leave = new TableCell();
                Literal lit_det_sf_Leave = new Literal();
                if (Dcr_Leave != 0)
                {
                    lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                }
                else
                {
                    lit_det_sf_Leave.Text = "";
                }
                tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_Leave);

                // Leave

                // Total Doctors
                sCurrentDate = months + "-01-" + iYear;


                //dsDoc = dcs.New_DCR_Visit_TotalDocQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsTotalQuery.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "'";
                dt = dsTotalQuery.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                }

                // Total Doctors

                ////DRs Calls Seen


                TableCell tc_det_sf_tot_doc = new TableCell();
                Literal lit_det_sf_tot_doc = new Literal();
                //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                if (doctor_total != 0)
                {
                    lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                }
                else
                {
                    lit_det_sf_tot_doc.Text = "";
                }
                tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                tr_det.Cells.Add(tc_det_sf_tot_doc);

                //DRs Calls Seen                

                // Chemist tot

                //dsDoc = dcs.New_DCR_TotalChemistQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsChemist.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "'";
                dt = dsChemist.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);
                }

                // Chemist tot

                //Chemist Seen                

                TableCell tc_det_sf_tot_Chemist = new TableCell();
                Literal lit_det_sf_tot_Chemist = new Literal();
                //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                if (Chemist_total != 0)
                {
                    lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();
                }
                else
                {
                    lit_det_sf_tot_Chemist.Text = "";
                }

                tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                tr_det.Cells.Add(tc_det_sf_tot_Chemist);

                //Chemist Seen


                // Chemist POB               

                TableCell tc_det_sf_Chemist_POB = new TableCell();
                Literal lit_det_sf_tot_POB = new Literal();
                lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                tc_det_sf_Chemist_POB.Visible = false;
                tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                // Chemist POB

                // Stock tot

                //dsDoc = dcs.New_DCR_TotalStockistQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsStock.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "'";
                dt = dsStock.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);
                }


                // Stock tot

                //Stock Calls Seen  

                TableCell tc_det_sf_tot_Stock = new TableCell();
                Literal lit_det_sf_tot_Stock = new Literal();
                // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                if (Stock_toatal != 0)
                {
                    lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                }
                else
                {
                    lit_det_sf_tot_Stock.Text = "";
                }

                tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_tot_Stock);

                //Stock Calls Seen


                // Unlist Doc tot

                // dsDoc = dcs.New_DCR_TotalUnlstDocQuery_DateWise_WithOut(drFF["sf_code"].ToString(), divcode, dtFrmDate.ToString("MM/dd/yyyy"), dtToDate.ToString("MM/dd/yyyy"));

                dsUnListDoc.Tables[0].DefaultView.RowFilter = " sf_code = '" + drFF["sf_code"].ToString() + "'";
                dt = dsUnListDoc.Tables[0].DefaultView.ToTable("table1");
                dsDoc.Clear();
                dsDoc.Merge(dt);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);
                }

                // Unlist Doc tot

                // UnLstDRs Calls Seen              

                TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                Literal lit_det_sf_UnList_tot_Stock = new Literal();
                //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                if (UnListDoc != 0)
                {
                    lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                }
                else
                {
                    lit_det_sf_UnList_tot_Stock.Text = "";
                }
                tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                // UnLstDRs Calls Seen   

                tbl.Rows.Add(tr_det);

            }
        }
    }
}