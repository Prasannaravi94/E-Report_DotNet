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
using System.Xml;
using System.Xml.XPath;
using System.Net;

public partial class Reports_Rpt_DCR_View_Tuned : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsDCRem = null;
    DataSet dsTerritory = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    DCR dc = new DCR();
    DataSet dsDocDate = new DataSet();
    DataSet dsDocChemist = new DataSet();
    DataSet dsDocUnlist = new DataSet();
    DataSet dsDocStk = new DataSet();
    DataSet dsDocHos = new DataSet();
    string div_code = string.Empty;
    string strDelay = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string FrmDate = string.Empty;
    string ToDate = string.Empty;
    string Sf_HQ = string.Empty;
    string sf_Designation = string.Empty;
    string JoiningDate = string.Empty;
    string StateName = string.Empty;
    string Subdivname = string.Empty;
    string Sf_Desig = string.Empty;
    string EmpId = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    double OurValue = 0, CompetitorValue = 0;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;
    //GridView gv = new GridView();

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        strMode = Request.QueryString["Mode"].ToString();

        if (strMode != "Date Wise(With out Approval)")
        {

            div_code = Session["div_code"].ToString();
            cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
            cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        }
        else
        {
            div_code = Request.QueryString["Division_Code"].ToString();
            FrmDate = Request.QueryString["FrmDate"].ToString();
            ToDate = Request.QueryString["ToDate"].ToString();
            FrmDate = FrmDate.Substring(6, 4) + "-" + FrmDate.Substring(3, 2) + "-" + FrmDate.Substring(0, 2) + " 00:00:00";
            ToDate = ToDate.Substring(6, 4) + "-" + ToDate.Substring(3, 2) + "-" + ToDate.Substring(0, 2) + " 00:00:00";

        }
        strMode = strMode.Trim();

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        //ExportButton();
        //btnExcel.Visible = false;
        ExportButtonForData();
        if (strMode.Trim() == "View All DCR Date(s)")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:#0077FF'>" + "Dates" + "</span>) view for the month of " + sMonth;
            if (!Page.IsPostBack)
            {
                dsDCR = dc.get_dcr_DCRPendingdate_Tuned(sf_code, cmonth, cyear);
                ddlDate.DataTextField = "Activity_Date";
                ddlDate.DataValueField = "Activity_Date";
                ddlDate.DataSource = dsDCR;
                ddlDate.DataBind();

                ddlDate.SelectedValue = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            }
            else
            {
                dsDCR = dc.get_dcr_DCRPendingdate_Tuned(sf_code, cmonth, cyear);
            }

            CreateDynamicTableDCRDate(cmonth, cyear, sf_code);


            //FillSalesForce(sf_code, cmonth, cyear);
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
            }
            //lblHead.Text = lblHead.Text + sMonth;
            lblHead.Visible = false;
            //btnPrint.Visible = true;
        }
        else if (strMode == "Detailed View")
        {

            ddlDate.Visible = false;
            btnSubmit.Visible = false;
            lblDate.Visible = false;
            lblView.Visible = false;
            sf_code = Request.QueryString["sf_code"].ToString();
            cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
            cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());

            lblTitle.Text = "Daily Call Report (<span style='color:#0077FF'>" + "Detail View" + "</span>) for the month of " + sMonth;

            CreateDynamicDCRDetailedView_new(cmonth, cyear, sf_code);
            //pnldcr.Visible = true;
            //FillDcrdetail(cmonth, cyear, sf_code);
            lblHead.Visible = false;
        }

    }
    private void ExportButton()
    {
        btnClose.Visible = true;
        btnPrint.Visible = false;
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        lblPDF.Visible = false;

    }
    private void ExportButtonForData()
    {
        btnClose.Visible = true;
        lblClose.Visible = true;
        btnPrint.Visible = true;
        lblPrint.Visible = true;
        btnExcel.Visible = true;
        lblExcel.Visible = true;
        btnPDF.Visible = false;
        lblPDF.Visible = false;
    }
    private void ExportButtonForNoData()
    {
        btnClose.Visible = true;
        lblClose.Visible = true;
        btnPrint.Visible = false;
        lblPrint.Visible = false;
        btnExcel.Visible = false;
        lblExcel.Visible = false;
        btnPDF.Visible = false;
        lblPDF.Visible = false;
    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }



    private void CreateDynamicTableDCRDate(int imonth, int iyear, string sf_code)
    {


        dsDocDate = dc.get_DCR_View_All_Dates_View_Temp_Tuned(sf_code, imonth, iyear, div_code, 1, ""); //1-Listed Doctor

        dsDocChemist = dc.get_dcr_All_Dates_che_details_Tuned(sf_code, imonth, iyear, 2);

        dsDocUnlist = dc.get_DCR_All_Dates_unlst_doc_details_Tuned_New(sf_code, imonth, iyear, div_code, 4);

        dsDocStk = dc.get_dcr_All_Date_stk_details_Tuned(sf_code, imonth, iyear, 3); //3-Stockist

        dsDocHos = dc.get_dcr_ALL_Date_hos_details_Tuned(sf_code, imonth, iyear, 5); //5-Hospital

    }

    string Sf_NameNew = string.Empty;
    private void CreateDynamicDCRDetailedView_new(int imonth, int iyear, string sf_code)
    {
        Sf_NameNew = Request.QueryString["Sf_Name"].ToString();
        try
        {

            DCR dc = new DCR();
            //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
            dsDCR = dc.get_dcr_DCRPendingdate_DCRDetail(sf_code, imonth, iyear);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                DCR dcsf = new DCR();
                DataSet dsName = new DataSet();
                dssf = dcsf.getSfName_HQ(sf_code);

                dsName = dcsf.sp_DcrViewNameGet(sf_code, imonth.ToString(), iyear.ToString());

                if (dssf.Tables[0].Rows.Count > 0)
                {
                    Sf_Name = dsName.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    sf_Designation = dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                }

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;
                tbldetail_main3.Style.Add("margin-top", "-130px");

                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                //tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                //tc_det_head_main4.Width = 1000;

                Table tbl = new Table();
                //tbl.Width = 1000;
                tbl.Style.Add("Width", "100%");

                TableRow tr_ff = new TableRow();
                TableCell tc_ff_name = new TableCell();
                tc_ff_name.BorderStyle = BorderStyle.None;
                tc_ff_name.Width = 500;
                Literal lit_ff_name = new Literal();
                //tc_ff_name.Style.Add("font-family", "Verdana");
                tc_ff_name.Style.Add("font-size", "10pt");
                lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                tc_ff_name.Controls.Add(lit_ff_name);
                tr_ff.Cells.Add(tc_ff_name);


                TableCell tc_Desgination = new TableCell();
                tc_Desgination.BorderStyle = BorderStyle.None;
                tc_Desgination.Width = 500;
                //tc_Desgination.Style.Add("font-family", "Verdana");
                tc_Desgination.Style.Add("font-size", "10pt");
                tc_Desgination.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_Designation = new Literal();
                lit_Designation.Text = "<b>Designation </b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;" + sf_Designation.ToString();
                tc_Desgination.Controls.Add(lit_Designation);
                tr_ff.Cells.Add(tc_Desgination);


                TableCell tc_HQ = new TableCell();
                tc_HQ.BorderStyle = BorderStyle.None;
                tc_HQ.Width = 500;
                //tc_HQ.Style.Add("font-family", "Verdana");
                tc_HQ.Style.Add("font-size", "10pt");
                tc_HQ.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_HQ = new Literal();
                lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
                tc_HQ.Controls.Add(lit_HQ);
                tr_ff.Cells.Add(tc_HQ);

                tbl.Rows.Add(tr_ff);

                TableRow tr_dcr = new TableRow();
                tbl.Rows.Add(tr_dcr);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                //form1.Controls.Add(tbldetail_main3);
                ExportDiv.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "<BR>";
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                // form1.Controls.Add(tbl_head_empty);
                ExportDiv.Controls.Add(tbl_head_empty);

                Table tbldetail_main = new Table();
                tbldetail_main.BorderStyle = BorderStyle.None;
                tbldetail_main.GridLines = GridLines.None;
                //tbldetail_main.Width = 1000;
                tbldetail_main.Style.Add("Width", "95%");
                //tbldetail_main.Style.Add("border-collapse", "collapse");
                //tbldetail_main.Style.Add("border", "solid 1px Black");
                //tbldetail_main.Style.Add("margin-left", "100px");
                TableRow tr_det_head_main = new TableRow();
                //TableCell tc_det_head_main = new TableCell();
                //tc_det_head_main.Width = 100;
                //Literal lit_det_main = new Literal();
                //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //tc_det_head_main.Controls.Add(lit_det_main);
                //tr_det_head_main.Cells.Add(tc_det_head_main);                
                TableCell tc_det_head_main2 = new TableCell();
                //tc_det_head_main2.Width = 1000;
                tc_det_head_main2.Style.Add("Width", "100%");

                Table tbldetail = new Table();
                //tbldetail.BorderStyle = BorderStyle.Solid;
                //tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.None;
                //tbldetail.Width = 1000;
                tbldetail.Style.Add("Width", "100%");
                //tbldetail.Style.Add("border-collapse", "collapse");
                //tbldetail.Style.Add("border", "solid 1px Black");
                tbldetail.Attributes.Add("class", "table");

                if (sf_code.Contains("MR"))
                {
                    dsdoc = dc.get_DCRView_Approved_All_Dates_new_Tuned(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
                }
                else
                {
                    dsdoc = dc.get_DCRView_Approved_MGR_All_Dates_new_Tuned(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
                }
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_SNo.BorderWidth = 0;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>Date</b>";
                    tc_det_head_SNo.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#4DB8FF");
                    //tc_det_head_SNo.Style.Add("color", "White");
                    //tc_det_head_SNo.Style.Add("font-size", "10pt");
                    //tc_det_head_SNo.Style.Add("font-weight", "bold");
                    //tc_det_head_SNo.Style.Add("font-family", "Calibri");
                    ////tc_det_head_SNo.Attributes.Add("Class", "tr_S.NO");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_Ses = new TableCell();
                    //tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_Ses.BorderWidth = 0;
                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_head_Ses.Visible = false;
                    Literal lit_det_head_Ses = new Literal();
                    // lit_det_head_Ses.Text = "<b>Territory Worked</b>";
                    Territory terr = new Territory();
                    dsTerritory = terr.getWorkAreaName(div_code);
                    lit_det_head_Ses.Text = "" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " Worked";
                    tc_det_head_Ses.Attributes.Add("Class", "stickyFirstRow");
                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                    tr_det_head.Cells.Add(tc_det_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_doc.BorderWidth = 0;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "Sub.Date";
                    ////tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Attributes.Add("class", "stickyFirstRow");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_Start = new TableCell();
                    //tc_det_head_Start.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_Start.BorderWidth = 0;
                    tc_det_head_Start.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_start = new Literal();
                    lit_det_head_start.Text = "Start Time";
                    tc_det_head_Start.Attributes.Add("class", "stickyFirstRow");
                    ////tc_det_head_Start.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Start.Controls.Add(lit_det_head_start);
                    tr_det_head.Cells.Add(tc_det_head_Start);

                    TableCell tc_det_head_End = new TableCell();
                    //tc_det_head_End.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_End.BorderWidth = 0;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_End = new Literal();
                    lit_det_head_End.Text = "End Time";
                    tc_det_head_End.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_End.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_End.Controls.Add(lit_det_head_End);
                    tr_det_head.Cells.Add(tc_det_head_End);

                    //TableCell tc_RemarkRejected = new TableCell();
                    //tc_RemarkRejected.HorizontalAlign = HorizontalAlign.Center;
                    //Literal lit_RemarkRejected_head = new Literal();
                    //lit_RemarkRejected_head.Text = "Rejected Reason";
                    //tc_RemarkRejected.Attributes.Add("class", "stickyFirstRow");
                    ////tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    //tc_RemarkRejected.Controls.Add(lit_RemarkRejected_head);
                    //tr_det_head.Cells.Add(tc_RemarkRejected);

                    TableCell tc_det_head_time = new TableCell();
                    //tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_time.BorderWidth = 0;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "Work Type";
                    tc_det_head_time.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_det_head.Cells.Add(tc_det_head_time);



                    TableCell tc_det_HFday = new TableCell();
                    //tc_det_HFday.Width = 200;
                    tc_det_HFday.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_HFday = new Literal();
                    lit_det_HFday.Text = "Halfday-Work";
                    tc_det_HFday.Attributes.Add("Class", "stickyFirstRow");
                    tc_det_HFday.Controls.Add(lit_det_HFday);
                    tr_det_head.Cells.Add(tc_det_HFday);


                    TableCell tc_det_head_ww = new TableCell();
                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_ww.BorderWidth = 0;
                    tc_det_head_ww.Visible = false;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "Worked With";
                    tc_det_head_ww.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    //tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_visit.BorderWidth = 0;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "Listed Dr(s) <br> Met";
                    tc_det_head_visit.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_DOC_POB = new TableCell();
                    //tc_det_DOC_POB.BorderStyle = BorderStyle.Solid;
                    //tc_det_DOC_POB.BorderWidth = 0;
                    tc_det_DOC_POB.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_Doc_POB = new Literal();
                    lit_det_Doc_POB.Text = "Listed Dr(s) <br> POB";
                    tc_det_DOC_POB.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_DOC_POB.Attributes.Add("Class", "tr_det_head");
                    tc_det_DOC_POB.Controls.Add(lit_det_Doc_POB);
                    tr_det_head.Cells.Add(tc_det_DOC_POB);

                    TableCell tc_det_head_catg = new TableCell();
                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_catg.BorderWidth = 0;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "Chemist <br> Met";
                    tc_det_head_catg.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_POB = new TableCell();
                    //tc_det_head_POB.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_POB.BorderWidth = 0;
                    tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_head_POB.Visible = false;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "Chemist <br> POB";
                    tc_det_head_POB.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_POB.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_POB.Controls.Add(lit_det_head_spec);
                    tr_det_head.Cells.Add(tc_det_head_POB);

                    TableCell tc_det_head_prod = new TableCell();
                    //tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_prod.BorderWidth = 0;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "Stockist <br> Met";
                    tc_det_head_prod.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_det_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_gift.BorderWidth = 0;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "Non Listed <br> Dr(s)Met";
                    tc_det_head_gift.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_det_head.Cells.Add(tc_det_head_gift);

                    TableCell tc_det_head_Remarks = new TableCell();
                    //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_gift.BorderWidth = 0;
                    tc_det_head_Remarks.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Remarks = new Literal();
                    lit_det_head_Remarks.Text = "View Day Remarks";
                    tc_det_head_Remarks.Attributes.Add("class", "stickyFirstRow");
                    //tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Remarks.Controls.Add(lit_det_head_Remarks);
                    tr_det_head.Cells.Add(tc_det_head_Remarks);

                    tbldetail.Rows.Add(tr_det_head);

                    iCount = 0;
                    iFieldWrkCount = 0;
                    int iTotLstCal = 0;
                    double iTotChemPOB = 0;
                    double iTotDocPOB = 0;
                    int iTotChemCal = 0;
                    int iTotStockCal = 0;
                    int iTotUnLstCal = 0;
                    int isum = 0;
                    double isumChemPOB = 0;
                    double isumDOCPOB = 0;
                    int isumChem = 0;
                    int isumStock = 0;
                    int isumUnLst = 0;

                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        strDelay = "";

                        SalesForce sf = new SalesForce();
                        DataSet dsVacant = new DataSet();
                        dsVacant = sf.CheckSFNameVacant_DCR_View(drdoctor["sf_code"].ToString(), imonth, iyear, drdoctor["Activity_Date"].ToString());
                        if (dsVacant.Tables[0].Rows.Count > 1)
                        {
                            //if (dsVacant.Tables[0].Rows[0]["SF_Name"].ToString() == Sf_Name)
                            //{
                            TableRow tr_det_Vacant = new TableRow();
                            TableCell tc_det_Vacant = new TableCell();
                            Literal lit_det_Vacant = new Literal();

                            int i = dsVacant.Tables[0].Rows.Count - 1;
                            string sf_name = dsVacant.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            string[] str = sf_name.Split('(');
                            if (str.Length > 2)
                            {
                                lit_det_Vacant.Text = str[1];
                                lit_det_Vacant.Text = str[0] + lit_det_Vacant.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + ")" + "</span>");
                            }

                            //tc_det_Vacant.Attributes.Add("Class", "tbldetail_main");
                            lit_det_Vacant.Text = dsVacant.Tables[0].Rows[0]["SF_Name"].ToString();
                            //tc_det_Vacant.BorderStyle = BorderStyle.Solid;
                            tc_det_Vacant.HorizontalAlign = HorizontalAlign.Left;
                            tc_det_Vacant.VerticalAlign = VerticalAlign.Middle;
                            tc_det_Vacant.Width = 200;
                            tc_det_Vacant.ColumnSpan = 8;
                            //tc_det_Ses.BorderWidth = 1;
                            tc_det_Vacant.Controls.Add(lit_det_Vacant);
                            tr_det_Vacant.Cells.Add(tc_det_Vacant);
                            tbldetail.Rows.Add(tr_det_Vacant);
                            // }
                        }


                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Visible = false;
                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        //tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_Ses = new TableCell();
                        HyperLink lit_det_Ses = new HyperLink();
                        lit_det_Ses.Text = drdoctor["Activity_Date"].ToString();
                        //tc_det_Ses.Attributes.Add("Class", "tbldetail_main");

                        //tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_Ses.VerticalAlign = VerticalAlign.Middle;
                        //tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        //tc_det_dr_name.Visible = false;
                        if (drdoctor["che_POB_Name"].ToString() != "[]")
                        {
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["che_POB_Name"].ToString();
                        }
                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        //tc_det_dr_name.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Submission_Date"].ToString();
                        //tc_det_time.BorderStyle = BorderStyle.Solid;
                        //tc_det_time.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        TableCell tc_det_Start = new TableCell();
                        Literal lit_det_Start = new Literal();
                        lit_det_Start.Text = "&nbsp;&nbsp;" + drdoctor["Start_Time"].ToString();
                        //tc_det_Start.BorderStyle = BorderStyle.Solid;
                        //tc_det_Start.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_Start.Controls.Add(lit_det_Start);
                        tr_det_sno.Cells.Add(tc_det_Start);

                        TableCell tc_det_End = new TableCell();
                        Literal lit_det_End = new Literal();
                        lit_det_End.Text = "&nbsp;&nbsp;" + drdoctor["End_Time"].ToString();
                        //tc_det_End.BorderStyle = BorderStyle.Solid;
                        //tc_det_End.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_End.Controls.Add(lit_det_End);
                        tr_det_sno.Cells.Add(tc_det_End);

                        //TableCell tc_RemarkRejected_Val = new TableCell();
                        //Literal lit_RemarkRejected_Val = new Literal();
                        //lit_RemarkRejected_Val.Text = "&nbsp;&nbsp;" + drdoctor["Remark_Rejected"].ToString();
                        ////tc_det_End.BorderStyle = BorderStyle.Solid;
                        ////tc_det_End.Attributes.Add("Class", "tbldetail_main");
                        ////tc_det_time.BorderWidth = 1;
                        //tc_RemarkRejected_Val.Controls.Add(lit_RemarkRejected_Val);
                        //tr_det_sno.Cells.Add(tc_RemarkRejected_Val);

                        string strWorktypeName = "";

                        if (sf_code.Contains("MR"))
                        {
                            strWorktypeName = drdoctor["Worktype_Name_B"].ToString();
                        }
                        else
                        {
                            strWorktypeName = drdoctor["Worktype_Name_M"].ToString();
                        }

                        DataSet dsDelay = new DataSet();

                        dsDelay = dc.get_DCR_Status_Delay_DCRView_new(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), cmonth, cyear);
                        if (dsDelay.Tables[0].Rows.Count == 0 || strWorktypeName == "Field Work")
                        {
                            if ((drdoctor["FieldWork_Indicator"].ToString().Trim() != "N" && drdoctor["FieldWork_Indicator"].ToString().Trim() != "W" && drdoctor["FieldWork_Indicator"].ToString().Trim() != "L" && drdoctor["FieldWork_Indicator"].ToString().Trim() != "H" && drdoctor["FieldWork_Indicator"].ToString().Trim() != ""))
                            {
                                iFieldWrkCount += 1;
                                sURL = "rptDCRViewApprovedDetails_New.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&div_code=" + div_code + "&Day=" + lit_det_Ses.Text + "";

                                lit_det_Ses.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'');";
                                lit_det_Ses.NavigateUrl = "#";
                                lit_det_Ses.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0052cc");
                                TableCell tc_det_work = new TableCell();
                                Literal lit_det_work = new Literal();

                                dsDelay = dc.get_DCR_Status_Delay_DCRView_new(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), cmonth, cyear);
                                if (dsDelay.Tables[0].Rows.Count > 0)
                                {
                                    strDelay = "<span style='color:red;'>( " + dsDelay.Tables[0].Rows[0][0].ToString() + " )";
                                }

                                if (drdoctor["Temp"].ToString() == "1")
                                {
                                    //strDelay = "<span style='color:red;'> ( " + "Approval Pending" + " ) </span>";
                                    strDelay = "<span style='color:red;'>..</span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "2")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Disapproved" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "3")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Edit - ReEntry" + " ) </span>";
                                }

                                if (sf_code.Contains("MR"))
                                {
                                    lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                }
                                else
                                {
                                    lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                }
                                //tc_det_work.BorderStyle = BorderStyle.Solid;
                                //tc_det_work.Attributes.Add("Class", "tbldetail_main");
                                tc_det_work.Width = 190;
                                //tc_det_work.BorderWidth = 1;
                                tc_det_work.Controls.Add(lit_det_work);
                                tr_det_sno.Cells.Add(tc_det_work);



                                TableCell tc_det_HFWork = new TableCell();
                                Literal lit_det_HFWork = new Literal();
                                lit_det_HFWork.Text = "&nbsp;&nbsp;" + drdoctor["Half_Day_FW"].ToString();
                                //tc_det_HFWork.BorderStyle = BorderStyle.Solid;
                                tc_det_HFWork.Width = 150;
                                //tc_det_HFWork.Attributes.Add("Class", "tbldetail_main");
                                tc_det_HFWork.Controls.Add(lit_det_HFWork);
                                tr_det_sno.Cells.Add(tc_det_HFWork);

                                TableCell tc_det_lvisit = new TableCell();
                                Literal lit_det_lvisit = new Literal();
                                lit_det_lvisit.Text = "0"; // drdoctor["lvisit"].ToString();
                                tc_det_lvisit.Visible = false;
                                //tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                                //tc_det_lvisit.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_lvisit.Controls.Add(lit_det_lvisit);
                                tr_det_sno.Cells.Add(tc_det_lvisit);

                                TableCell tc_det_spec = new TableCell();
                                HyperLink Hyllit_det_spec = new HyperLink();
                                Hyllit_det_spec.Text = drdoctor["doc_cnt"].ToString();
                                if (Hyllit_det_spec.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 1 + "";

                                    Hyllit_det_spec.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'');";
                                    Hyllit_det_spec.NavigateUrl = "#";
                                }
                                //tc_det_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_spec.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_spec.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_spec.BorderWidth = 1;
                                tc_det_spec.Controls.Add(Hyllit_det_spec);
                                tr_det_sno.Cells.Add(tc_det_spec);

                                iTotLstCal += Convert.ToInt16(Hyllit_det_spec.Text);

                                TableCell tc_det_DOC_POB_Value = new TableCell();
                                Literal lit_det_DOC_POB = new Literal();

                                if (drdoctor["Doc_POB"].ToString().ToString() != "")
                                {
                                    lit_det_DOC_POB.Text = drdoctor["Doc_POB"].ToString().ToString();
                                }
                                else
                                {
                                    lit_det_DOC_POB.Text = "0";
                                }
                                //tc_det_DOC_POB_Value.BorderStyle = BorderStyle.Solid;
                                //tc_det_DOC_POB_Value.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_head_POB.Visible = false;
                                tc_det_DOC_POB_Value.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_DOC_POB_Value.BorderWidth = 1;
                                tc_det_DOC_POB_Value.Controls.Add(lit_det_DOC_POB);
                                tr_det_sno.Cells.Add(tc_det_DOC_POB_Value);

                                iTotDocPOB += Convert.ToDouble(lit_det_DOC_POB.Text);

                                TableCell tc_det_prod = new TableCell();
                                HyperLink hyllit_det_prod = new HyperLink();
                                hyllit_det_prod.Text = drdoctor["che_cnt"].ToString().ToString();
                                if (hyllit_det_prod.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 2 + "";
                                    hyllit_det_prod.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'');";
                                    hyllit_det_prod.NavigateUrl = "#";

                                }
                                //tc_det_prod.BorderStyle = BorderStyle.Solid;
                                tc_det_prod.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_prod.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_prod.BorderWidth = 1;
                                tc_det_prod.Controls.Add(hyllit_det_prod);
                                tr_det_sno.Cells.Add(tc_det_prod);

                                iTotChemCal += Convert.ToInt16(hyllit_det_prod.Text);

                                TableCell tc_det_Che_POB = new TableCell();
                                Literal lit_det_Che_POB = new Literal();

                                if (drdoctor["che_POB"].ToString().ToString() != "")
                                {
                                    lit_det_Che_POB.Text = drdoctor["che_POB"].ToString().ToString();
                                }
                                else
                                {
                                    lit_det_Che_POB.Text = "0";
                                }
                                //tc_det_Che_POB.BorderStyle = BorderStyle.Solid;
                                //tc_det_Che_POB.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_head_POB.Visible = false;
                                tc_det_Che_POB.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_Che_POB.BorderWidth = 1;
                                tc_det_Che_POB.Controls.Add(lit_det_Che_POB);
                                tr_det_sno.Cells.Add(tc_det_Che_POB);

                                iTotChemPOB += Convert.ToDouble(lit_det_Che_POB.Text);

                                TableCell tc_det_gift = new TableCell();
                                HyperLink hyllit_det_gift = new HyperLink();
                                hyllit_det_gift.Text = drdoctor["stk_cnt"].ToString();
                                if (hyllit_det_gift.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 3 + "";

                                    hyllit_det_gift.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'');";
                                    hyllit_det_gift.NavigateUrl = "#";
                                }
                                //tc_det_gift.BorderStyle = BorderStyle.Solid;
                                tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_gift.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_gift.BorderWidth = 1;
                                tc_det_gift.Controls.Add(hyllit_det_gift);
                                tr_det_sno.Cells.Add(tc_det_gift);

                                iTotStockCal += Convert.ToInt16(hyllit_det_gift.Text);

                                TableCell tc_det_UnDoc = new TableCell();
                                HyperLink hyllit_det_UnDoc = new HyperLink();
                                hyllit_det_UnDoc.Text = drdoctor["Undoc_cnt"].ToString();
                                if (hyllit_det_UnDoc.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 4 + "";

                                    hyllit_det_UnDoc.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'');";
                                    hyllit_det_UnDoc.NavigateUrl = "#";
                                }

                                //tc_det_UnDoc.BorderStyle = BorderStyle.Solid;
                                tc_det_UnDoc.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_UnDoc.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_UnDoc.BorderWidth = 1;
                                tc_det_UnDoc.Controls.Add(hyllit_det_UnDoc);
                                tr_det_sno.Cells.Add(tc_det_UnDoc);
                                iTotUnLstCal += Convert.ToInt16(hyllit_det_UnDoc.Text);


                                //------Start Remarks------

                                TableCell tc_det_Remarks = new TableCell();
                                HyperLink hyllit_det_Remarks = new HyperLink();
                                hyllit_det_Remarks.Text = "View"; // Set the text to "View"

                                sURL = "RptDCrRemarks_HalfDay.aspx?sf_Name=" + Sf_NameNew + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text;
                                //string windowFeatures = "width=50,height=50,scrollbars=no,resizable=yes";
                                string windowFeatures = "width=500,height=400,scrollbars=no,resizable=yes,menubar=no,toolbar=no,location=no,status=no";
                                hyllit_det_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','" + windowFeatures + "');";
                                hyllit_det_Remarks.NavigateUrl = "#";


                                // Set the alignment of the TableCell
                                tc_det_Remarks.HorizontalAlign = HorizontalAlign.Center;

                                // Add the HyperLink to the TableCell
                                tc_det_Remarks.Controls.Add(hyllit_det_Remarks);

                                // Add the TableCell to the TableRow
                                tr_det_sno.Cells.Add(tc_det_Remarks);

                                //------End Remarks------


                            }
                            else
                            {
                                TableCell tc_det_NonFwk = new TableCell();
                                Literal lit_det_NonFwk = new Literal();

                                if (drdoctor["Temp"].ToString() == "1")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Approval Pending" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "2")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Disapproved" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "3")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Edit - ReEntry" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "5")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Missing Date" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "6")
                                {
                                    strDelay = "<span style='color:red;'> ( " + "Missed Released" + " ) </span>";
                                }


                                if (sf_code.Contains("MR"))
                                {

                                    if (drdoctor["Temp"].ToString() == "8")
                                    {
                                        lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + "<span style = 'color:red' >" + " ( Rejection -ReEntry )";
                                    }
                                    else
                                    {
                                        lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                    }
                                }
                                else
                                {

                                    if (drdoctor["Temp"].ToString() == "8")
                                    {
                                        lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + "<span style = 'color:red' >" + " ( Rejection -ReEntry )";
                                    }
                                    else
                                    {
                                        lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                    }
                                }
                                //tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                                tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");
                                //tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                                tc_det_NonFwk.ColumnSpan = 8;
                                tc_det_NonFwk.Controls.Add(lit_det_NonFwk);
                                tr_det_sno.Cells.Add(tc_det_NonFwk);
                                //------Start Remarks------

                                TableCell tc_det_Remarks = new TableCell();
                                HyperLink hyllit_det_Remarks = new HyperLink();
                                hyllit_det_Remarks.Text = "View"; // Set the text to "View"

                                sURL = "RptDCrRemarks_HalfDay.aspx?sf_Name=" + Sf_NameNew + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text;
                                //string windowFeatures = "width=50,height=50,scrollbars=no,resizable=yes";
                                string windowFeatures = "width=500,height=400,scrollbars=no,resizable=yes,menubar=no,toolbar=no,location=no,status=no";
                                hyllit_det_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','" + windowFeatures + "');";
                                hyllit_det_Remarks.NavigateUrl = "#";


                                // Set the alignment of the TableCell
                                tc_det_Remarks.HorizontalAlign = HorizontalAlign.Center;

                                // Add the HyperLink to the TableCell
                                tc_det_Remarks.Controls.Add(hyllit_det_Remarks);

                                // Add the TableCell to the TableRow
                                tr_det_sno.Cells.Add(tc_det_Remarks);

                                //------End Remarks------
                            }

                            tbldetail.Rows.Add(tr_det_sno);

                            tc_det_head_main2.Controls.Add(tbldetail);
                            tr_det_head_main.Cells.Add(tc_det_head_main2);
                            tbldetail_main.Rows.Add(tr_det_head_main);

                            // form1.Controls.Add(tbldetail_main);
                            ExportDiv.Controls.Add(tbldetail_main);
                        }
                        else
                        {


                            if (dsDelay.Tables[0].Rows.Count > 0)
                            {
                                strDelay = "<span style='color:red'> " + dsDelay.Tables[0].Rows[0][0].ToString() + " ";
                            }

                            TableCell tc_det_NonFwk = new TableCell();
                            Literal lit_det_NonFwk = new Literal();

                            if (sf_code.Contains("MR"))
                            {
                                if (drdoctor["Temp"].ToString() == "8")
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + "<span style = 'color:red' >" + " ( Rejection -ReEntry )";
                                }
                                else
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                }

                            }
                            else
                            {

                                if (drdoctor["Temp"].ToString() == "8")
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + "<span style = 'color:red' >" + " ( Rejection -ReEntry )";
                                }
                                else
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                }

                            }



                            //tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                            tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");
                            //tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                            tc_det_NonFwk.ColumnSpan = 7;
                            tc_det_NonFwk.Controls.Add(lit_det_NonFwk);
                            tr_det_sno.Cells.Add(tc_det_NonFwk);
                        }

                        tbldetail.Rows.Add(tr_det_sno);

                        tc_det_head_main2.Controls.Add(tbldetail);
                        tr_det_head_main.Cells.Add(tc_det_head_main2);
                        tbldetail_main.Rows.Add(tr_det_head_main);

                        // form1.Controls.Add(tbldetail_main);
                        ExportDiv.Controls.Add(tbldetail_main);
                    }
                    TableRow tr_total = new TableRow();

                    TableCell tc_Count_Total = new TableCell();
                    //tc_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Count_Total = new Literal();
                    lit_Count_Total.Text = "<center>Total</center>";
                    tc_Count_Total.Controls.Add(lit_Count_Total);
                    tc_Count_Total.Font.Bold.ToString();
                    tc_Count_Total.BackColor = System.Drawing.Color.White;
                    //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Count_Total.ColumnSpan = 7;
                    tc_Count_Total.Style.Add("text-align", "left");
                    tc_Count_Total.Style.Add("font-family", "Calibri");
                    tc_Count_Total.Style.Add("font-size", "10pt");

                    tr_total.Cells.Add(tc_Count_Total);

                    int[] arrTotDoc = new int[] { iTotLstCal };

                    for (int i = 0; i < arrTotDoc.Length; i++)
                    {
                        isum += arrTotDoc[i];
                    }

                    decimal RoundLstCal = new decimal();

                    double LstCal = (double)iTotLstCal / iFieldWrkCount;
                    if (LstCal.ToString() != "NaN")
                    {
                        RoundLstCal = Math.Round((decimal)LstCal, 2);
                    }

                    TableCell tc_Lst_Count_Total = new TableCell();
                    //tc_Lst_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_Lst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total = new Literal();
                    lit_Lst_Count_Total.Text = Convert.ToString(RoundLstCal);
                    tc_Lst_Count_Total.Controls.Add(lit_Lst_Count_Total);
                    tc_Lst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Lst_Count_Total.VerticalAlign = VerticalAlign.Middle;
                    //tc_Lst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Lst_Count_Total.Font.Bold.ToString();
                    tc_Lst_Count_Total.Style.Add("color", "Red");
                    tc_Lst_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_Lst_Count_Total.Style.Add("text-align", "left");
                    //tc_Lst_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Lst_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Lst_Count_Total);

                    double[] arrtotDocPOB = new double[] { iTotDocPOB };

                    for (int i = 0; i < arrtotDocPOB.Length; i++)
                    {
                        isumDOCPOB += arrtotDocPOB[i];
                    }

                    TableCell DOC_POB_Count_Total = new TableCell();
                    //DOC_POB_Count_Total.BorderStyle = BorderStyle.Solid;
                    //DOC_POB_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_DOC_POB_Count_Total = new Literal();
                    lit_DOC_POB_Count_Total.Text = Convert.ToString(isumDOCPOB);
                    DOC_POB_Count_Total.Controls.Add(lit_DOC_POB_Count_Total);
                    DOC_POB_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //DOC_POB_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    DOC_POB_Count_Total.Font.Bold.ToString();
                    DOC_POB_Count_Total.Style.Add("color", "Red");
                    DOC_POB_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(DOC_POB_Count_Total);

                    int[] arrTotChem = new int[] { iTotChemCal };

                    for (int i = 0; i < arrTotChem.Length; i++)
                    {
                        isumChem += arrTotChem[i];
                    }

                    decimal RoundChemCal = new decimal();

                    double ChemCal = (double)iTotChemCal / iFieldWrkCount;
                    if (ChemCal.ToString() != "NaN")
                    {
                        RoundChemCal = Math.Round((decimal)ChemCal, 2);
                    }

                    TableCell tc_Chem_Count_Total = new TableCell();
                    //tc_Chem_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_Chem_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_Count_Total = new Literal();
                    lit_Chem_Count_Total.Text = Convert.ToString(RoundChemCal);
                    tc_Chem_Count_Total.Controls.Add(lit_Chem_Count_Total);
                    tc_Chem_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //tc_Chem_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Chem_Count_Total.Font.Bold.ToString();
                    tc_Chem_Count_Total.Style.Add("color", "Red");
                    tc_Chem_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_Chem_Count_Total.Style.Add("text-align", "left");
                    //tc_Chem_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Chem_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Chem_Count_Total);

                    double[] arrtotChemPOB = new double[] { iTotChemPOB };

                    for (int i = 0; i < arrtotChemPOB.Length; i++)
                    {
                        isumChemPOB += arrtotChemPOB[i];
                    }

                    TableCell Chemist_POB_Count_Total = new TableCell();
                    //Chemist_POB_Count_Total.BorderStyle = BorderStyle.Solid;
                    //Chemist_POB_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_POB_Count_Total = new Literal();
                    lit_Chem_POB_Count_Total.Text = Convert.ToString(isumChemPOB);
                    Chemist_POB_Count_Total.Controls.Add(lit_Chem_POB_Count_Total);
                    Chemist_POB_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //Chemist_POB_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    Chemist_POB_Count_Total.Font.Bold.ToString();
                    Chemist_POB_Count_Total.Style.Add("color", "Red");
                    Chemist_POB_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(Chemist_POB_Count_Total);

                    int[] arrtotStock = new int[] { iTotStockCal };

                    for (int i = 0; i < arrtotStock.Length; i++)
                    {
                        isumStock += arrtotStock[i];
                    }

                    decimal RoundStockCal = new decimal();

                    double StockCal = (double)iTotStockCal / iFieldWrkCount;
                    if (StockCal.ToString() != "NaN")
                    {
                        RoundStockCal = Math.Round((decimal)StockCal, 2);
                    }

                    TableCell tc_Stock_Count_Total = new TableCell();
                    //tc_Stock_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_Stock_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Stock_Count_Total = new Literal();
                    lit_Stock_Count_Total.Text = Convert.ToString(RoundStockCal);
                    tc_Stock_Count_Total.Controls.Add(lit_Stock_Count_Total);
                    tc_Stock_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //tc_Stock_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Stock_Count_Total.Font.Bold.ToString();
                    //tc_Stock_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Stock_Count_Total.Style.Add("color", "Red");
                    tc_Stock_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Stock_Count_Total);

                    int[] arrtotUnLst = new int[] { iTotUnLstCal };

                    for (int i = 0; i < arrtotUnLst.Length; i++)
                    {
                        isumUnLst += arrtotUnLst[i];
                    }

                    decimal RoundUnLstCal = new decimal();

                    double UnLstCal = (double)iTotUnLstCal / iFieldWrkCount;

                    if (UnLstCal.ToString() != "NaN")
                    {
                        RoundUnLstCal = Math.Round((decimal)UnLstCal, 2);
                    }

                    TableCell tc_UnLst_Count_Total = new TableCell();
                    //tc_UnLst_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_UnLst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_UnLst_Count_Total = new Literal();
                    lit_UnLst_Count_Total.Text = Convert.ToString(RoundUnLstCal);
                    tc_UnLst_Count_Total.Controls.Add(lit_UnLst_Count_Total);
                    tc_UnLst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //tc_UnLst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_UnLst_Count_Total.Font.Bold.ToString();
                    //tc_UnLst_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_UnLst_Count_Total.Style.Add("color", "Red");
                    tc_UnLst_Count_Total.Style.Add("background-color", "#F1F5F8");
                    //tc_UnLst_Count_Total.Style.Add("text-align", "left");
                    //tc_UnLst_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_UnLst_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_UnLst_Count_Total);

                    tbldetail.Rows.Add(tr_total);

                    tc_det_head_main2.Controls.Add(tbldetail);
                    tr_det_head_main.Cells.Add(tc_det_head_main2);
                    tbldetail_main.Rows.Add(tr_det_head_main);

                    // form1.Controls.Add(tbldetail_main);
                    ExportDiv.Controls.Add(tbldetail_main);
                }
            }
            else
            {
                //lblHead.Visible = true;
                //lblHead.Style.Add("margin-top", "80px");
                //lblHead.Text = "No Record Found";

                //pnlbutton.Visible = false;
                ExportButtonForNoData();

                Table tbldetail_mainHoliday = new Table();
                tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                tbldetail_mainHoliday.Width = 1100;
                TableRow tr_det_head_mainHoliday = new TableRow();
                TableCell tc_det_head_mainHolday = new TableCell();
                //tc_det_head_mainHolday.Width = 100;
                Literal lit_det_mainHoliday = new Literal();
                lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tbldetail_mainHoliday.Style.Add("margin-top", "110px");
                tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                TableCell tc_det_head_mainHoliday = new TableCell();
                //tc_det_head_mainHoliday.Width = 800;

                Table tbldetailHoliday = new Table();
                tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                tbldetailHoliday.BorderWidth = 1;
                tbldetailHoliday.GridLines = GridLines.Both;
                tbldetailHoliday.Width = 1000;
                tbldetailHoliday.Style.Add("border-collapse", "collapse");
                tbldetailHoliday.Style.Add("border", "solid 1px Black");

                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "No Record Found";
                //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.Attributes.Add("Class", "no-result-area");
                tc_det_SNo.Style.Add("font-size", "18px");

                tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
                //tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.BorderStyle = BorderStyle.None;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                tbldetailHoliday.Rows.Add(tr_det_sno);

                tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                //form1.Controls.Add(tbldetail_mainHoliday);
                ExportDiv.Controls.Add(tbldetail_mainHoliday);
            }


            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";



        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptDCRView";
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            ExportButtonForData();

            foreach (System.Web.UI.WebControls.ListItem item in ddlDate.Items)
            {
                if (item.Selected)
                {
                    dsDCR.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + item.Value + "' ";
                    DataTable dt1 = dsDCR.Tables[0].DefaultView.ToTable("table1");
                    //dsDCR.Clear();
                    // dsDCR.Merge(dt1);
                    //DataSet ds1 = new DataSet();

                    //ds1.Merge(dt);
                    if (dt1.Rows.Count > 0)
                    {
                        DCR dcsf = new DCR();
                        dssf = dcsf.getSfName_HQ_New(sf_code);

                        if (dssf.Tables[0].Rows.Count > 0)
                        {
                            Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            Sf_Desig = dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            EmpId = dssf.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            StateName = dssf.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            JoiningDate = dssf.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                            Subdivname = dssf.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();

                        }

                        foreach (DataRow drdoc in dt1.Rows)
                        {

                            Table tbldetail_main3 = new Table();
                            tbldetail_main3.BorderStyle = BorderStyle.None;
                            //tbldetail_main3.Width = 1100;
                            TableRow tr_det_head_main3 = new TableRow();
                            TableCell tc_det_head_main3 = new TableCell();
                            //tc_det_head_main3.Width = 100;
                            Literal lit_det_main3 = new Literal();
                            lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_main3.Controls.Add(lit_det_main3);
                            tr_det_head_main3.Cells.Add(tc_det_head_main3);

                            TableCell tc_det_head_main4 = new TableCell();
                            //tc_det_head_main4.Width = 1000;


                            Table tbl = new Table();
                            tbl.Width = 1000;
                            tbl.Style.Add("Align", "Center");

                            TableRow tr_day = new TableRow();
                            tr_day.Style.Add("font-size", "12pt");
                            //tr_day.Style.Add("font-family", "Verdana");
                            TableCell tc_day = new TableCell();
                            tc_day.BorderStyle = BorderStyle.None;
                            tc_day.ColumnSpan = 2;
                            tc_day.HorizontalAlign = HorizontalAlign.Center;

                            HyperLink lit_day = new HyperLink();
                            lit_day.Text = "Daily Call Report - " + "<span style='color:Red;'>" + drdoc["Activity_Date"].ToString() + "</span>" + "";
                            lit_day.Style.Add("font-size", "12pt");
                            lit_day.Style.Add("padding-bottom", "10px");
                            lit_day.Style.Add("padding-top", "10px");


                            tc_day.Controls.Add(lit_day);
                            tr_day.Cells.Add(tc_day);
                            tbl.Rows.Add(tr_day);

                            tc_det_head_main4.Controls.Add(tbl);
                            tr_det_head_main3.Cells.Add(tc_det_head_main4);
                            tbldetail_main3.Rows.Add(tr_det_head_main3);

                            //form1.Controls.Add(tbldetail_main3);
                            ExportDiv.Controls.Add(tbldetail_main3);

                            //Pending Approval 

                            Table tbldetail_mainPending = new Table();
                            tbldetail_mainPending.BorderStyle = BorderStyle.None;
                            tbldetail_mainPending.Width = 1100;
                            TableRow tr_det_head_mainPending = new TableRow();
                            TableCell tc_det_head_mainPending = new TableCell();
                            tc_det_head_mainPending.Width = 100;
                            Literal lit_det_mainPending = new Literal();
                            lit_det_mainPending.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_mainPending.Controls.Add(lit_det_mainPending);
                            tr_det_head_mainPending.Cells.Add(tc_det_head_mainPending);

                            TableCell tc_det_head_mainPendingSub = new TableCell();
                            tc_det_head_mainPendingSub.Width = 1000;


                            Table tbldetailhosPending = new Table();
                            tbldetailhosPending.BorderStyle = BorderStyle.Solid;
                            tbldetailhosPending.BorderWidth = 1;
                            tbldetailhosPending.GridLines = GridLines.Both;
                            tbldetailhosPending.Width = 1000;
                            tbldetailhosPending.Style.Add("border-collapse", "none");
                            tbldetailhosPending.Style.Add("border", "none");


                            dsdoc = dc.get_Pending_Single_Temp_Date(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor

                            iCount = 0;
                            if (dsdoc.Tables[0].Rows.Count > 0)
                            {
                                TableRow tr_det_Pending = new TableRow();
                                TableCell tc_det_Pending = new TableCell();
                                iCount += 1;
                                Literal lit_det_SNo = new Literal();
                                lit_det_SNo.Text = "<center> <b> " + dsdoc.Tables[0].Rows[0]["Temp"].ToString() + " </b> </center>";
                                tc_det_Pending.Style.Add("color", "Red");
                                tc_det_Pending.Style.Add("border", "none");
                                tc_det_Pending.BorderStyle = BorderStyle.Solid;
                                tc_det_Pending.BorderWidth = 1;
                                tc_det_Pending.Controls.Add(lit_det_SNo);
                                tr_det_Pending.Cells.Add(tc_det_Pending);


                                tbldetailhosPending.Rows.Add(tr_det_Pending);
                            }

                            tc_det_head_mainPendingSub.Controls.Add(tbldetailhosPending);
                            tr_det_head_mainPending.Cells.Add(tc_det_head_mainPendingSub);
                            tbldetail_mainPending.Rows.Add(tr_det_head_mainPending);

                            //form1.Controls.Add(tbldetail_mainPending);
                            ExportDiv.Controls.Add(tbldetail_mainPending);


                            //Pending Approval 

                            // WeekOff 

                            Table tbldetail_mainHoliday = new Table();
                            tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                            //tbldetail_mainHoliday.Width = 1100;
                            TableRow tr_det_head_mainHoliday = new TableRow();
                            TableCell tc_det_head_mainHolday = new TableCell();
                            //tc_det_head_mainHolday.Width = 100;
                            Literal lit_det_mainHoliday = new Literal();
                            lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                            TableCell tc_det_head_mainHoliday = new TableCell();
                            //tc_det_head_mainHoliday.Width = 1000;


                            Table tbldetailHoliday = new Table();
                            tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                            tbldetailHoliday.BorderWidth = 1;
                            tbldetailHoliday.GridLines = GridLines.Both;
                            //tbldetailHoliday.Width = 1000;
                            tbldetailHoliday.Style.Add("border-collapse", "none");
                            tbldetailHoliday.Style.Add("border", "none");

                            if (sf_code.Contains("MR"))
                            {
                                dsdoc = dc.get_DCRHoliday_Name_MR(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                            }
                            else
                            {
                                dsdoc = dc.get_DCRHoliday_Name(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                            }
                            iCount = 0;
                            if (dsdoc.Tables[0].Rows.Count > 0)
                            {
                                TableRow tr_det_head = new TableRow();
                                iCount = 0;
                                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                                {
                                    TableRow tr_det_sno = new TableRow();
                                    TableCell tc_det_SNo = new TableCell();
                                    iCount += 1;
                                    Literal lit_det_SNo = new Literal();
                                    if (sf_code.Contains("MR"))
                                    {
                                        lit_det_SNo.Text = "<center>" + drdoctor["Worktype_Name_B"].ToString() + "</center>";
                                    }
                                    else
                                    {
                                        lit_det_SNo.Text = "<center>" + drdoctor["Worktype_Name_M"].ToString() + "</center>";
                                    }
                                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                    tc_det_SNo.Attributes.Add("Class", "Holiday");
                                    tc_det_SNo.BorderWidth = 1;
                                    tc_det_SNo.BorderStyle = BorderStyle.None;
                                    tc_det_SNo.Controls.Add(lit_det_SNo);
                                    tr_det_sno.Cells.Add(tc_det_SNo);

                                    tbldetailHoliday.Rows.Add(tr_det_sno);

                                    tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                                    tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                                    tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                                    Table tbl_line = new Table();
                                    tbl_line.BorderStyle = BorderStyle.None;
                                    //tbl_line.Width = 1000;
                                    tbl_line.Style.Add("width ", "90%");
                                    tbl_line.Style.Add("border-collapse", "collapse");
                                    tbl_line.Style.Add("border-top", "none");
                                    tbl_line.Style.Add("border-right", "none");
                                    //tbl_line.Style.Add("margin-left", "100px");                                     
                                    tbl_line.Style.Add("border-bottom ", "solid 2px #DCE2E8");

                                    //form1.Controls.Add(tbldetail_mainHoliday);
                                    ExportDiv.Controls.Add(tbldetail_mainHoliday);

                                    TableRow tr_line = new TableRow();

                                    TableCell tc_line0 = new TableCell();
                                    tc_line0.Width = 100;
                                    Literal lit_line0 = new Literal();
                                    lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tc_line0.Controls.Add(lit_line0);
                                    tr_line.Cells.Add(tc_line0);

                                    TableCell tc_line = new TableCell();
                                    tc_line.Width = 1000;
                                    Literal lit_line = new Literal();
                                    // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                                    tc_line.Controls.Add(lit_line);
                                    tr_line.Cells.Add(tc_line);
                                    tbl_line.Rows.Add(tr_line);
                                    //form1.Controls.Add(tbl_line);
                                    ExportDiv.Controls.Add(tbl_line);
                                }
                                DataSet dsdoc1 = null;
                                dsdoc1 = dc.get_DCR_NF_latlong_MR(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                                if (dsdoc1.Tables[0].Rows.Count > 0)
                                {


                                    TableRow tr_det_sno1 = new TableRow();

                                    TableCell tc_det_head_latlong = new TableCell();
                                    tc_det_head_latlong.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_latlong.Width = 100;
                                    tc_det_head_latlong.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_latlong = new Literal();
                                    lit_det_head_latlong.Text = "<b>Lat & Long</b>";
                                    tc_det_head_latlong.Attributes.Add("Class", "tr_NF_head");
                                    tc_det_head_latlong.Controls.Add(lit_det_head_latlong);
                                    tr_det_sno1.Cells.Add(tc_det_head_latlong);


                                    TableCell tc_det_Latlog = new TableCell();
                                    HyperLink lit_det_Latlog = new HyperLink();
                                    lit_det_Latlog.Text = "&nbsp;&nbsp;" + dsdoc1.Tables[0].Rows[0]["location"].ToString().Replace(":", " - ");
                                    sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + drdoc["Activity_Date"].ToString() + "/&Mode=" + "/" + "NF" + " ";
                                    lit_det_Latlog.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    lit_det_Latlog.NavigateUrl = "#";
                                    tc_det_Latlog.Attributes.Add("Class", "tblNF_Data");
                                    tc_det_Latlog.Controls.Add(lit_det_Latlog);
                                    tr_det_sno1.Cells.Add(tc_det_Latlog);

                                    tbldetailHoliday.Rows.Add(tr_det_sno1);

                                }
                            }
                            else
                            {
                                //form1.Controls.Add(tbldetailhos);

                                TableRow tr_ff = new TableRow();
                                tr_ff.Style.Add("font-size", "10pt");
                                //tr_ff.Style.Add("font-family", "Verdana");
                                TableCell tc_ff_name = new TableCell();
                                tc_ff_name.BorderStyle = BorderStyle.None;
                                tc_ff_name.Width = 500;
                                Literal lit_ff_name = new Literal();
                                DataSet dsSf = new DataSet();
                                SalesForce sf1 = new SalesForce();
                                dsSf = sf1.CheckSFNameVacant(sf_code, cmonth, cyear);

                                if (dsSf.Tables[0].Rows.Count > 0)
                                {
                                    string[] strVacant = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

                                    if (strVacant.Count() >= 2)
                                    {
                                        if ("( " + strVacant[0].Trim() + " )" != strVacant[1].Trim())
                                        {
                                            Sf_Name = strVacant[0] + "<span style='color: red;'>" + strVacant[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";


                                        }
                                        else
                                        {
                                            Sf_Name = strVacant[0];
                                        }
                                    }
                                    else
                                    {
                                        Sf_Name = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                                    }

                                    if (dsSf.Tables[0].Rows[0][2].ToString() != "")
                                    {
                                        Sf_Name = "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";

                                    }
                                }
                                lit_ff_name.Text = "<span><b>Field Force Name </b>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_Name.ToString();
                                tc_ff_name.Controls.Add(lit_ff_name);
                                tr_ff.Cells.Add(tc_ff_name);

                                TableCell tc_HQ = new TableCell();
                                tc_HQ.BorderStyle = BorderStyle.None;
                                tc_HQ.Width = 500;
                                tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                                Literal lit_HQ = new Literal();
                                lit_HQ.Text = "<span ><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_HQ.ToString() + "</span>";
                                //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                                tc_HQ.Controls.Add(lit_HQ);
                                tr_ff.Cells.Add(tc_HQ);
                                tbl.Rows.Add(tr_ff);

                                TableCell tc_Desig = new TableCell();
                                tc_Desig.BorderStyle = BorderStyle.None;
                                tc_Desig.Width = 500;
                                tc_Desig.HorizontalAlign = HorizontalAlign.Left;
                                Literal lit_Desig = new Literal();
                                lit_Desig.Text = "<span ><b>Designation</b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_Desig.ToString() + "</span>";
                                //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                                tc_Desig.Controls.Add(lit_Desig);
                                tr_ff.Cells.Add(tc_Desig);
                                tbl.Rows.Add(tr_ff);

                                TableRow tr_Emp = new TableRow();
                                tr_Emp.Style.Add("font-size", "10pt");
                                //tr_dcr.Style.Add("font-family", "Verdana");
                                TableCell tc_dcr_Emp = new TableCell();
                                tc_dcr_Emp.BorderStyle = BorderStyle.None;
                                tc_dcr_Emp.Width = 500;
                                Literal lit_dcr_Emp = new Literal();
                                lit_dcr_Emp.Text = "<span ><b>Employee Code</b></span>" + "&nbsp;&nbsp;&nbsp;&nbsp;<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + EmpId.ToString();
                                tc_dcr_Emp.Controls.Add(lit_dcr_Emp);
                                tr_Emp.Cells.Add(tc_dcr_Emp);

                                TableCell tc_JoinDt = new TableCell();
                                tc_JoinDt.BorderStyle = BorderStyle.None;
                                tc_JoinDt.Width = 500;
                                tc_JoinDt.HorizontalAlign = HorizontalAlign.Left;
                                Literal lit_JoinDt = new Literal();
                                lit_JoinDt.Text = "<span ><b>DOJ </b></span>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + JoiningDate.ToString() + "</span>";
                                //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                                tc_JoinDt.Controls.Add(lit_JoinDt);
                                tr_Emp.Cells.Add(tc_JoinDt);
                                tbl.Rows.Add(tr_Emp);

                                TableCell tc_State = new TableCell();
                                tc_State.BorderStyle = BorderStyle.None;
                                tc_State.Width = 500;
                                tc_State.HorizontalAlign = HorizontalAlign.Left;
                                Literal lit_State = new Literal();
                                lit_State.Text = "<span ><b>State Name </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + StateName.ToString() + "</span>";
                                //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                                tc_State.Controls.Add(lit_State);
                                tr_Emp.Cells.Add(tc_State);
                                tbl.Rows.Add(tr_Emp);


                                TableRow tr_dcr = new TableRow();
                                tr_dcr.Style.Add("font-size", "10pt");
                                //tr_dcr.Style.Add("font-family", "Verdana");
                                TableCell tc_dcr_submit = new TableCell();
                                tc_dcr_submit.BorderStyle = BorderStyle.None;
                                tc_dcr_submit.Width = 500;
                                Literal lit_dcr_submit = new Literal();
                                lit_dcr_submit.Text = "<span ><b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                                tc_dcr_submit.Controls.Add(lit_dcr_submit);
                                tr_dcr.Cells.Add(tc_dcr_submit);

                                TableCell tc_Terr = new TableCell();
                                tc_Terr.BorderStyle = BorderStyle.None;
                                tc_Terr.HorizontalAlign = HorizontalAlign.Left;
                                tc_Terr.Width = 500;
                                Literal lit_Terr = new Literal();
                                Territory terr = new Territory();
                                dsTerritory = terr.getWorkAreaName(div_code);
                                tc_Terr.Controls.Add(lit_Terr);
                                tr_dcr.Cells.Add(tc_Terr);
                                tbl.Rows.Add(tr_dcr);

                                TableCell tc_SubDiv = new TableCell();
                                tc_SubDiv.BorderStyle = BorderStyle.None;
                                tc_SubDiv.Width = 500;
                                tc_SubDiv.HorizontalAlign = HorizontalAlign.Left;
                                Literal lit_SubDiv = new Literal();
                                lit_SubDiv.Text = "<span ><b>SubDivision </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Subdivname.ToString() + "</span>";
                                //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                                tc_SubDiv.Controls.Add(lit_SubDiv);
                                tr_dcr.Cells.Add(tc_SubDiv);
                                tbl.Rows.Add(tr_dcr);

                                //tbl.Rows.Add(tr_dcr);

                                TableRow tr_remark = new TableRow();
                                tr_remark.Style.Add("font-size", "10pt");
                                //tr_remark.Style.Add("font-family", "Verdana");
                                TableCell tc_dcr_remark = new TableCell();
                                tc_dcr_remark.BorderStyle = BorderStyle.None;
                                tc_dcr_remark.Width = 2000;
                                Literal lit_dcr_remark = new Literal();
                                lit_dcr_remark.Text = "<span ><b>Day Wise Remarks</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Remarks"].ToString();
                                //lit_dcr_remark.Text =  drdoc["Remarks"].ToString();
                                tc_dcr_remark.Controls.Add(lit_dcr_remark);
                                tr_remark.Cells.Add(tc_dcr_remark);



                                tbl.Rows.Add(tr_remark);


                                tc_det_head_main4.Controls.Add(tbl);
                                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                                tbldetail_main3.Rows.Add(tr_det_head_main3);

                                //form1.Controls.Add(tbldetail_main3);
                                ExportDiv.Controls.Add(tbldetail_main3);

                                Table tbl_head_empty = new Table();
                                TableRow tr_head_empty = new TableRow();
                                TableCell tc_head_empty = new TableCell();
                                Literal lit_head_empty = new Literal();
                                lit_head_empty.Text = "<BR>";
                                tc_head_empty.Controls.Add(lit_head_empty);
                                tr_head_empty.Cells.Add(tc_head_empty);
                                tbl_head_empty.Rows.Add(tr_head_empty);
                                //form1.Controls.Add(tbl_head_empty);
                                ExportDiv.Controls.Add(tbl_head_empty);

                                Table tbldetail_main = new Table();
                                tbldetail_main.BorderStyle = BorderStyle.None;
                                //tbldetail_main.Width = 1100;
                                tbldetail_main.Style.Add("width", "95%");
                                TableRow tr_det_head_main = new TableRow();
                                TableCell tc_det_head_main = new TableCell();
                                //tc_det_head_main.Width = 100;
                                Literal lit_det_main = new Literal();
                                lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_det_head_main.Controls.Add(lit_det_main);
                                tr_det_head_main.Cells.Add(tc_det_head_main);

                                TableCell tc_det_head_main2 = new TableCell();
                                //tc_det_head_main2.Width = 2000;

                                Table tbldetail = new Table();
                                //tbldetail.BorderStyle = BorderStyle.Solid;
                                //tbldetail.BorderWidth = 1;
                                tbldetail.GridLines = GridLines.None;
                                //tbldetail.Width = 2000;
                                //tbldetail.Style.Add("border-collapse", "collapse");
                                //tbldetail.Style.Add("border", "solid 1px Black");
                                tbldetail.Attributes.Add("class", "table");



                                //dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                                dsDocDate.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + drdoc["Activity_Date"].ToString() + "' ";
                                DataTable dt = dsDocDate.Tables[0].DefaultView.ToTable("table1");
                                DataSet ds1 = new DataSet();
                                dsDocDate.Merge(dt);
                                ds1.Merge(dt);
                                iCount = 0;

                                DataSet dsRCPA = new DataSet();
                                DCR dcr = new DCR();
                                dsRCPA = dcr.get_RCPA_Capture_Head(sf_code, drdoc["Activity_Date"].ToString());

                                if (ds1.Tables[0].Rows.Count > 0)
                                {

                                    TableRow tr_det_head = new TableRow();
                                    //tr_det_head.Style.Add("font-size", "8pt");
                                    TableCell tc_det_head_SNo = new TableCell();
                                    //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_SNo.BorderWidth = 1;
                                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_SNo = new Literal();
                                    tc_det_head_SNo.Attributes.Add("Class", "stickyFirstRow");
                                    lit_det_head_SNo.Text = "#";
                                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                    tr_det_head.Cells.Add(tc_det_head_SNo);

                                    TableCell tc_det_head_Ses = new TableCell();
                                    //tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Ses.BorderWidth = 1;
                                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Ses = new Literal();
                                    tc_det_head_Ses.Attributes.Add("Class", "stickyFirstRow");
                                    lit_det_head_Ses.Text = "Session";
                                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                                    tr_det_head.Cells.Add(tc_det_head_Ses);

                                    TableCell tc_det_head_time = new TableCell();
                                    //tc_det_head_time.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_time.BorderWidth = 1;
                                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_time = new Literal();
                                    lit_det_head_time.Text = "Time";
                                    tc_det_head_time.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_time.Controls.Add(lit_det_head_time);
                                    tr_det_head.Cells.Add(tc_det_head_time);

                                    TableCell tc_det_head_doc = new TableCell();
                                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_doc.BorderWidth = 1;
                                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_doc = new Literal();
                                    lit_det_head_doc.Text = "Listed  Doctor Name";
                                    tc_det_head_doc.Width = 500;
                                    tc_det_head_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                    tr_det_head.Cells.Add(tc_det_head_doc);

                                    TableCell tc_det_Address_doc = new TableCell();
                                    //tc_det_Address_doc.BorderStyle = BorderStyle.Solid;
                                    //tc_det_Address_doc.BorderWidth = 1;
                                    tc_det_Address_doc.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_Address_doc = new Literal();
                                    lit_det_Address_doc.Text = "Address";
                                    tc_det_Address_doc.Width = 1000;
                                    tc_det_Address_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_Address_doc.Controls.Add(lit_det_Address_doc);
                                    tr_det_head.Cells.Add(tc_det_Address_doc);

                                    Territory terr_View = new Territory();
                                    DataSet dsTerritory_View = new DataSet();
                                    dsTerritory_View = terr_View.getWorkAreaName(div_code);




                                    TableCell tc_det_head_hos = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    tc_det_head_hos.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_hos = new Literal();
                                    lit_det_head_hos.Text = "Hospital as Per Master";
                                    tc_det_head_hos.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_hos.Controls.Add(lit_det_head_hos);
                                    tr_det_head.Cells.Add(tc_det_head_hos);

                                    TableCell tc_det_head_hos1 = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    tc_det_head_hos1.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_hos1 = new Literal();
                                    lit_det_head_hos1.Text = "Hospital as Per Tagging";
                                    tc_det_head_hos1.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_hos1.Controls.Add(lit_det_head_hos1);
                                    tr_det_head.Cells.Add(tc_det_head_hos1);


                                    TableCell tc_det_head_catg = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_catg = new Literal();
                                    lit_det_head_catg.Text = "Category";
                                    tc_det_head_catg.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                                    tr_det_head.Cells.Add(tc_det_head_catg);

                                    TableCell tc_det_head_spec = new TableCell();
                                    //tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_spec.BorderWidth = 1;
                                    tc_det_head_spec.Width = 100;
                                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_spec = new Literal();
                                    lit_det_head_spec.Text = "Speciality";
                                    tc_det_head_spec.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                                    tr_det_head.Cells.Add(tc_det_head_spec);

                                    TableCell tc_det_head_Qual = new TableCell();
                                    //tc_det_head_Qual.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Qual.BorderWidth = 1;
                                    tc_det_head_Qual.Width = 100;
                                    tc_det_head_Qual.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Qual = new Literal();
                                    lit_det_head_Qual.Text = "Qualification";
                                    tc_det_head_Qual.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Qual.Controls.Add(lit_det_head_Qual);
                                    tr_det_head.Cells.Add(tc_det_head_Qual);

                                    TableCell tc_det_head_Plan_Name = new TableCell();
                                    //tc_det_head_Plan_Name.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Plan_Name.BorderWidth = 1;
                                    tc_det_head_Plan_Name.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Plan_Name = new Literal();
                                    tc_det_head_Plan_Name.Width = 200;
                                    if (dsTerritory.Tables[0].Rows.Count > 0)
                                    {
                                        lit_det_head_Plan_Name.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                                    }
                                    //lit_det_head_Plan_Name.Text = "<b>Territory Name</b>";
                                    tc_det_head_Plan_Name.Width = 500;
                                    tc_det_head_Plan_Name.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Plan_Name.Controls.Add(lit_det_head_Plan_Name);
                                    tr_det_head.Cells.Add(tc_det_head_Plan_Name);

                                    TableCell tc_det_head_Tertype = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    tc_det_head_Tertype.Width = 200;
                                    tc_det_head_Tertype.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Tertype = new Literal();
                                    lit_det_head_Tertype.Text = "Territory Type";
                                    tc_det_head_Tertype.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Tertype.Controls.Add(lit_det_head_Tertype);
                                    tr_det_head.Cells.Add(tc_det_head_Tertype);

                                    //TableCell tc_det_Actual_Place_Work = new TableCell();
                                    ////tc_det_Actual_Place_Work.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_Actual_Place_Work.BorderWidth = 1;
                                    //tc_det_Actual_Place_Work.Width = 1000;
                                    //tc_det_Actual_Place_Work.HorizontalAlign = HorizontalAlign.Center;
                                    //Literal lit_det_Actual_Place_Work = new Literal();
                                    //tc_det_Actual_Place_Work.Visible = false;
                                    //lit_det_Actual_Place_Work.Text = "Actual Place of Work";
                                    //tc_det_Actual_Place_Work.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_Actual_Place_Work.Controls.Add(lit_det_Actual_Place_Work);
                                    //tr_det_head.Cells.Add(tc_det_Actual_Place_Work);

                                    //TableCell tc_det_Previous_Visit = new TableCell();
                                    ////tc_det_Previous_Visit.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_Previous_Visit.BorderWidth = 1;
                                    //tc_det_Previous_Visit.Width = 100;
                                    //tc_det_Previous_Visit.HorizontalAlign = HorizontalAlign.Center;
                                    //Literal lit_det_Previous_Visit = new Literal();
                                    //lit_det_Previous_Visit.Text = "Previous Visit";
                                    //tc_det_Previous_Visit.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_Previous_Visit.Controls.Add(lit_det_Previous_Visit);
                                    //tr_det_head.Cells.Add(tc_det_Previous_Visit);

                                    TableCell tc_det_head_ww = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    tc_det_head_ww.Width = 200;
                                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_ww = new Literal();
                                    lit_det_head_ww.Text = "Worked With";
                                    tc_det_head_ww.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                    tr_det_head.Cells.Add(tc_det_head_ww);

                                    TableCell tc_det_head_POB = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_POB.Width = 1000;
                                    tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_POB = new Literal();
                                    lit_det_head_POB.Text = "POB";
                                    tc_det_head_POB.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_POB.Controls.Add(lit_det_head_POB);
                                    tr_det_head.Cells.Add(tc_det_head_POB);

                                    TableCell tc_det_head_Target = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_Target.Width = 1000;
                                    tc_det_head_Target.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Target = new Literal();
                                    lit_det_head_Target.Text = "Product Detailed";
                                    tc_det_head_Target.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Target.Controls.Add(lit_det_head_Target);
                                    tr_det_head.Cells.Add(tc_det_head_Target);

                                    TableCell tc_det_head_sampled = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_sampled.Width = 1000;
                                    tc_det_head_sampled.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_sampled = new Literal();
                                    lit_det_head_sampled.Text = "Product Sampled";
                                    tc_det_head_sampled.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_sampled.Controls.Add(lit_det_head_sampled);
                                    tr_det_head.Cells.Add(tc_det_head_sampled);

                                    TableCell tc_det_head_PrdRx = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_PrdRx.Width = 1000;
                                    tc_det_head_PrdRx.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_PrdRx = new Literal();
                                    lit_det_head_PrdRx.Text = "Product Rx";
                                    tc_det_head_PrdRx.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_PrdRx.Controls.Add(lit_det_head_PrdRx);
                                    tr_det_head.Cells.Add(tc_det_head_PrdRx);

                                    //TableCell tc_det_head_prod_Duration = new TableCell();
                                    ////tc_det_head_prod_Duration.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_head_prod_Duration.BorderWidth = 1;
                                    //tc_det_head_prod_Duration.Width = 200;
                                    //tc_det_head_prod_Duration.HorizontalAlign = HorizontalAlign.Center;
                                    //Literal lit_det_head_prod_Duration = new Literal();
                                    //lit_det_head_prod_Duration.Text = "Detailing duration (Secs)";
                                    //tc_det_head_prod_Duration.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_prod_Duration.Controls.Add(lit_det_head_prod_Duration);
                                    //tr_det_head.Cells.Add(tc_det_head_prod_Duration);

                                    TableCell tc_det_head_CallFeed_Back = new TableCell();
                                    //tc_det_head_CallFeed_Back.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_CallFeed_Back.BorderWidth = 1;
                                    tc_det_head_CallFeed_Back.Width = 100;
                                    tc_det_head_CallFeed_Back.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_CallFeed_Back = new Literal();
                                    //lit_det_head_CallFeed_Back.Text = "Rx";
                                    lit_det_head_CallFeed_Back.Text = "Call Feedback";
                                    tc_det_head_CallFeed_Back.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_CallFeed_Back.Controls.Add(lit_det_head_CallFeed_Back);
                                    tr_det_head.Cells.Add(tc_det_head_CallFeed_Back);

                                    TableCell tc_det_head_CallRemarks = new TableCell();
                                    //tc_det_head_CallFeed_Back.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_CallFeed_Back.BorderWidth = 1;
                                    tc_det_head_CallRemarks.Width = 100;
                                    tc_det_head_CallRemarks.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_CallRemarks = new Literal();
                                    //lit_det_head_CallFeed_Back.Text = "Rx";
                                    lit_det_head_CallRemarks.Text = "Call Remarks";
                                    tc_det_head_CallRemarks.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_CallRemarks.Controls.Add(lit_det_head_CallRemarks);
                                    tr_det_head.Cells.Add(tc_det_head_CallRemarks);

                                    TableCell tc_det_head_gift = new TableCell();
                                    //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_gift.BorderWidth = 1;
                                    tc_det_head_gift.Width = 100;
                                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_gift = new Literal();
                                    lit_det_head_gift.Text = "Input";
                                    tc_det_head_gift.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                                    tr_det_head.Cells.Add(tc_det_head_gift);



                                    TableCell tc_det_head_latlong = new TableCell();
                                    //tc_det_head_latlong.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_latlong.BorderWidth = 1;
                                    tc_det_head_latlong.Width = 100;
                                    tc_det_head_latlong.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_latlong = new Literal();
                                    lit_det_head_latlong.Text = "Lat & Long";
                                    tc_det_head_latlong.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_latlong.Controls.Add(lit_det_head_latlong);
                                    tr_det_head.Cells.Add(tc_det_head_latlong);

                                    TableCell tc_det_head_ActAddress = new TableCell();
                                    tc_det_head_ActAddress.Width = 100;
                                    tc_det_head_ActAddress.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_ActAddress = new Literal();
                                    lit_det_head_ActAddress.Text = "Actual Address";
                                    tc_det_head_ActAddress.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ActAddress.Controls.Add(lit_det_head_ActAddress);
                                    tr_det_head.Cells.Add(tc_det_head_ActAddress);

                                    //TableCell tc_det_head_Call_Remark = new TableCell();
                                    ////tc_det_head_Call_Remark.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_head_Call_Remark.BorderWidth = 1;
                                    //tc_det_head_Call_Remark.Width = 100;
                                    //tc_det_head_Call_Remark.HorizontalAlign = HorizontalAlign.Center;
                                    //Literal lit_det_head_Call_Remark = new Literal();
                                    //lit_det_head_Call_Remark.Text = "Call Remark";
                                    //tc_det_head_Call_Remark.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_Call_Remark.Controls.Add(lit_det_head_Call_Remark);
                                    //tr_det_head.Cells.Add(tc_det_head_Call_Remark);

                                    //TableCell tc_det_head_Doctor_Duration = new TableCell();
                                    ////tc_det_head_Doctor_Duration.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_head_Doctor_Duration.BorderWidth = 1;
                                    //tc_det_head_Doctor_Duration.HorizontalAlign = HorizontalAlign.Center;
                                    //Literal lit_det_head_Doctor_Duration = new Literal();
                                    //lit_det_head_Doctor_Duration.Text = "Detailing Duration (in Secs)";
                                    //tc_det_head_Doctor_Duration.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_Doctor_Duration.Controls.Add(lit_det_head_Doctor_Duration);
                                    //tr_det_head.Cells.Add(tc_det_head_Doctor_Duration);

                                    TableCell tc_det_head_Click = new TableCell();
                                    //tc_det_head_Click.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Click.BorderWidth = 1;
                                    tc_det_head_Click.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Click = new Literal();
                                    lit_det_head_Click.Text = "Photo ";
                                    tc_det_head_Click.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Click.Controls.Add(lit_det_head_Click);
                                    tr_det_head.Cells.Add(tc_det_head_Click);


                                    tbldetail.Rows.Add(tr_det_head);
                                    string strlongname = "";
                                    iCount = 0;
                                    int iReturn = -1;
                                    foreach (DataRow drdoctor in ds1.Tables[0].Rows)
                                    {
                                        lit_Terr.Text = "<span ><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + drdoctor["che_POB_Name"].ToString() + "</span>";
                                        //if ((drdoctor["GeoAddrs"].ToString().Trim() == "NA" || drdoctor["GeoAddrs"].ToString().Trim() == "") && drdoctor["lati"] != "")
                                        if (drdoctor["lati"] != "")
                                        {
                                            //string str = "test";
                                            //ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'" + str + "'\');", true);

                                            //sURL = "DCR_ShowMap.aspx?sf_Code=" + sf_code + " &strDate=" + drdoc["Activity_Date"].ToString() + " ";
                                            //lit_day.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                            //lit_day.NavigateUrl = "#";

                                            //int i = 0;
                                            //XmlDocument doc = new XmlDocument();

                                            //WebClient client = new WebClient();

                                            //doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + drdoctor["lati"] + "," + drdoctor["long"] + "&sensor=false");
                                            //XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                                            //XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                                            //strlongname = "";
                                            //foreach (XmlNode xn in xnList)
                                            //{
                                            //    i += 1;
                                            //    if (i < 8)
                                            //    {
                                            //        strlongname += xn["long_name"].InnerText + ",";
                                            //    }
                                            //}
                                            //if (strlongname != "")
                                            //{
                                            //    DCR_New dcr = new DCR_New();

                                            //    iReturn = dcr.DCRView_Insert(drdoctor["trans_detail_slno"].ToString(), strlongname.Replace("'", "asdf"));
                                            //}
                                        }



                                        TableRow tr_det_sno = new TableRow();
                                        //tr_det_sno.Style.Add("font-size", "8pt");
                                        //tr_det_sno.Style.Add("font-family", "Verdana");
                                        TableCell tc_det_SNo = new TableCell();
                                        iCount += 1;
                                        Literal lit_det_SNo = new Literal();
                                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                        //tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SNo.BorderWidth = 1;
                                        tc_det_SNo.Controls.Add(lit_det_SNo);
                                        tr_det_sno.Cells.Add(tc_det_SNo);

                                        TableCell tc_det_Ses = new TableCell();
                                        Literal lit_det_Ses = new Literal();
                                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                                        //tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Ses.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Ses.BorderWidth = 1;
                                        tc_det_Ses.Controls.Add(lit_det_Ses);
                                        tr_det_sno.Cells.Add(tc_det_Ses);

                                        TableCell tc_det_time = new TableCell();
                                        Literal lit_det_time = new Literal();
                                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString().Replace("00:00", "");
                                        //tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_time.BorderStyle = BorderStyle.Solid;
                                        //tc_det_time.BorderWidth = 1;
                                        tc_det_time.Controls.Add(lit_det_time);
                                        tr_det_sno.Cells.Add(tc_det_time);

                                        TableCell tc_det_dr_name = new TableCell();
                                        Literal lit_det_dr_name = new Literal();
                                        HyperLink hyperDrName = new HyperLink();
                                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                                        //tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_name.BorderWidth = 1;
                                        tc_det_dr_name.Width = 150;
                                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                        tr_det_sno.Cells.Add(tc_det_dr_name);



                                        TableCell tc_det_Address = new TableCell();
                                        Literal lit_det_Address = new Literal();
                                        lit_det_Address.Text = "&nbsp;&nbsp;" + drdoctor["listeddr_address1"].ToString();
                                        //tc_det_Address.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Address.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Address.BorderWidth = 1;
                                        tc_det_Address.Controls.Add(lit_det_Address);
                                        tr_det_sno.Cells.Add(tc_det_Address);



                                        TableCell tc_det_Hos = new TableCell();
                                        Literal lit_det_Hos = new Literal();
                                        lit_det_Hos.Text = "&nbsp;&nbsp;" + drdoctor["HospitalName_Master"].ToString();
                                        //tc_det_Address.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Address.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Address.BorderWidth = 1;
                                        tc_det_Hos.Controls.Add(lit_det_Hos);
                                        tr_det_sno.Cells.Add(tc_det_Hos);

                                        TableCell tc_det_HosTag = new TableCell();
                                        Literal lit_det_HosTag = new Literal();
                                        lit_det_HosTag.Text = "&nbsp;&nbsp;" + drdoctor["HospitalName_Tag"].ToString();
                                        //tc_det_Address.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Address.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Address.BorderWidth = 1;
                                        tc_det_HosTag.Controls.Add(lit_det_HosTag);
                                        tr_det_sno.Cells.Add(tc_det_HosTag);

                                        TableCell tc_det_catg = new TableCell();
                                        Literal lit_det_catg = new Literal();
                                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                                        //tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_catg.BorderStyle = BorderStyle.Solid;
                                        //tc_det_catg.BorderWidth = 1;
                                        tc_det_catg.Controls.Add(lit_det_catg);
                                        tr_det_sno.Cells.Add(tc_det_catg);

                                        TableCell tc_det_spec = new TableCell();
                                        Literal lit_det_spec = new Literal();
                                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                                        //tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_spec.BorderStyle = BorderStyle.Solid;
                                        //tc_det_spec.BorderWidth = 1;
                                        tc_det_spec.Controls.Add(lit_det_spec);
                                        tr_det_sno.Cells.Add(tc_det_spec);

                                        TableCell tc_det_Qualification = new TableCell();
                                        Literal lit_det_Qual = new Literal();
                                        lit_det_Qual.Text = "&nbsp;&nbsp;" + drdoctor["doc_qua_name"].ToString();
                                        //tc_det_Qualification.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Qualification.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Qualification.BorderWidth = 1;
                                        tc_det_Qualification.Controls.Add(lit_det_Qual);
                                        tr_det_sno.Cells.Add(tc_det_Qualification);


                                        TableCell tc_det_Territory = new TableCell();
                                        Literal lit_det_Territory = new Literal();
                                        lit_det_Territory.Text = "&nbsp;&nbsp;" + drdoctor["SDP_Name"].ToString();
                                        //tc_det_Territory.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Territory.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Territory.BorderWidth = 1;
                                        tc_det_Territory.Controls.Add(lit_det_Territory);
                                        tr_det_sno.Cells.Add(tc_det_Territory);

                                        TableCell tc_det_TerritoryType = new TableCell();
                                        Literal lit_det_TerritoryType = new Literal();
                                        lit_det_TerritoryType.Text = "&nbsp;&nbsp;" + drdoctor["Territory_Cat"].ToString();
                                        //tc_det_Territory.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Territory.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Territory.BorderWidth = 1;
                                        tc_det_TerritoryType.Controls.Add(lit_det_TerritoryType);
                                        tr_det_sno.Cells.Add(tc_det_TerritoryType);


                                        //TableCell tc_det_ActualPlace = new TableCell();
                                        //Literal lit_det_ActualPlace = new Literal();
                                        //tc_det_ActualPlace.Visible = false;
                                        //if (drdoctor["GeoAddrs"].ToString().Trim() == "NA" && drdoctor["lati"] != "")
                                        //{
                                        //    lit_det_ActualPlace.Text = strlongname;
                                        //}
                                        //else if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                                        //{
                                        //    lit_det_ActualPlace.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        //}
                                        //else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                                        //{
                                        //    //lit_det_ActualPlace.Text = "";
                                        //}
                                        ////tc_det_ActualPlace.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_ActualPlace.BorderStyle = BorderStyle.Solid;
                                        //tc_det_ActualPlace.Width = 250;
                                        ////tc_det_ActualPlace.BorderWidth = 1;
                                        //tc_det_ActualPlace.Controls.Add(lit_det_ActualPlace);
                                        //tr_det_sno.Cells.Add(tc_det_ActualPlace);


                                        //TableCell tc_det_Prevoius_Visit = new TableCell();
                                        //Literal lit_data_Previous_Visit = new Literal();
                                        ////tc_det_Prevoius_Visit.Attributes.Add("Class", "tbldetail_Data");
                                        //lit_data_Previous_Visit.Text = "&nbsp;&nbsp;" + drdoctor["dt"].ToString();
                                        ////tc_det_Prevoius_Visit.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_Prevoius_Visit.BorderWidth = 1;
                                        //tc_det_Prevoius_Visit.Controls.Add(lit_data_Previous_Visit);
                                        //tr_det_sno.Cells.Add(tc_det_Prevoius_Visit);

                                        TableCell tc_det_work = new TableCell();
                                        Literal lit_det_work = new Literal();
                                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString().Replace(Sf_Name.Trim(), "Self");
                                        //tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_work.Controls.Add(lit_det_work);
                                        tr_det_sno.Cells.Add(tc_det_work);

                                        TableCell tc_det_POB = new TableCell();
                                        Literal lit_det_POB = new Literal();
                                        lit_det_POB.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                                        //tc_det_Territory.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Territory.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Territory.BorderWidth = 1;
                                        tc_det_POB.Controls.Add(lit_det_POB);
                                        tr_det_sno.Cells.Add(tc_det_POB);


                                        TableCell tc_det_prod = new TableCell();
                                        Literal lit_det_prod = new Literal();
                                        tc_det_prod.Width = 1500;
                                        lit_det_prod.Text = drdoctor["Detailed"].ToString().Replace("~", "/").Trim();
                                        tc_det_prod.Controls.Add(lit_det_prod);
                                        tr_det_sno.Cells.Add(tc_det_prod);

                                        TableCell tc_det_prodsample = new TableCell();
                                        Literal lit_det_prodsample = new Literal();
                                        tc_det_prodsample.Width = 1500;
                                        lit_det_prodsample.Text = drdoctor["Sampled"].ToString().Replace("~", "/").Trim();
                                        tc_det_prodsample.Controls.Add(lit_det_prodsample);
                                        tr_det_sno.Cells.Add(tc_det_prodsample);

                                        TableCell tc_det_prodRX = new TableCell();
                                        Literal lit_det_prodRX = new Literal();
                                        tc_det_prodRX.Width = 1500;
                                        lit_det_prodRX.Text = drdoctor["PrdRx"].ToString().Replace("~", "/").Trim();
                                        tc_det_prodRX.Controls.Add(lit_det_prodRX);
                                        tr_det_sno.Cells.Add(tc_det_prodRX);

                                        TableCell tc_det_Callfeed = new TableCell();
                                        Literal lit_det_Callfeed = new Literal();
                                        tc_det_Callfeed.Width = 500;
                                        lit_det_Callfeed.Text = drdoctor["CallFeed"].ToString().Replace("~", "/").Trim();
                                        tc_det_Callfeed.Controls.Add(lit_det_Callfeed);
                                        tr_det_sno.Cells.Add(tc_det_Callfeed);


                                        TableCell tc_det_CallRemarks = new TableCell();
                                        Literal lit_det_CallRemarks = new Literal();
                                        tc_det_CallRemarks.Width = 500;
                                        lit_det_CallRemarks.Text = drdoctor["Activity_Remarks"].ToString().Replace("~", "/").Trim();
                                        tc_det_CallRemarks.Controls.Add(lit_det_CallRemarks);
                                        tr_det_sno.Cells.Add(tc_det_CallRemarks);

                                        //TableCell tc_det_Product_Duration = new TableCell();
                                        //Literal lit_det_Product_Duration = new Literal();
                                        ////tc_det_Product_Duration.Attributes.Add("Class", "tbldetail_Data");
                                        //lit_det_Product_Duration.Text = drdoctor["Product_Duration"].ToString();
                                        ////tc_det_Product_Duration.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_Product_Duration.BorderWidth = 1;
                                        //tc_det_Product_Duration.Controls.Add(lit_det_Product_Duration);
                                        //tr_det_sno.Cells.Add(tc_det_Product_Duration);



                                        TableCell tc_det_gift = new TableCell();
                                        Literal lit_det_gift = new Literal();
                                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", "").Replace("0", "").Trim();
                                        //tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_gift.BorderStyle = BorderStyle.Solid;
                                        //tc_det_gift.BorderWidth = 1;
                                        tc_det_gift.Controls.Add(lit_det_gift);
                                        tr_det_sno.Cells.Add(tc_det_gift);

                                        //TableCell tc_det_CallFeedBack = new TableCell();
                                        //Literal lit_det_CallFeedBack = new Literal();
                                        //lit_det_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        ////tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_CallFeedBack.BorderWidth = 1;
                                        //tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                                        //tr_det_sno.Cells.Add(tc_det_CallFeedBack);

                                        TableCell tc_det_Latlog = new TableCell();
                                        HyperLink lit_det_Latlog = new HyperLink();
                                        lit_det_Latlog.Text = drdoctor["lati"].ToString() + " - " + drdoctor["long"].ToString();
                                        sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + drdoc["Activity_Date"].ToString() + "/&Mode=" + "/" + "D" + " ";
                                        lit_det_Latlog.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        lit_det_Latlog.NavigateUrl = "#";
                                        //tc_det_Latlog.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Latlog.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Latlog.BorderWidth = 1;
                                        tc_det_Latlog.Controls.Add(lit_det_Latlog);
                                        tr_det_sno.Cells.Add(tc_det_Latlog);

                                        TableCell tc_det_ActAddr = new TableCell();
                                        Literal lit_det_ActAddr = new Literal();
                                        lit_det_ActAddr.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        tc_det_ActAddr.Controls.Add(lit_det_ActAddr);
                                        tr_det_sno.Cells.Add(tc_det_ActAddr);

                                        //TableCell tc_det_CallRemarks = new TableCell();
                                        //Literal lit_det_CallRemarks = new Literal();
                                        //lit_det_CallRemarks.Text = "&nbsp;&nbsp;" + drdoctor["Activity_Remarks"].ToString();
                                        ////tc_det_CallRemarks.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_CallRemarks.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_CallRemarks.BorderWidth = 1;
                                        //tc_det_CallRemarks.Controls.Add(lit_det_CallRemarks);
                                        //tr_det_sno.Cells.Add(tc_det_CallRemarks);



                                        //TableCell tc_det_DoctorDuration = new TableCell();
                                        //Literal lit_det_DoctorDuration = new Literal();
                                        //lit_det_DoctorDuration.Text = "&nbsp;&nbsp;" + drdoctor["Doctor_Duration"].ToString();
                                        ////tc_det_DoctorDuration.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_DoctorDuration.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_DoctorDuration.BorderWidth = 1;
                                        //tc_det_DoctorDuration.Controls.Add(lit_det_DoctorDuration);
                                        //tr_det_sno.Cells.Add(tc_det_DoctorDuration);

                                        TableCell tc_det_dr_Click = new TableCell();
                                        //Literal lit_det_dr_name = new Literal();
                                        HyperLink hyperDrClick = new HyperLink();
                                        //hyperDrClick.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                                        //tc_det_dr_Click.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Click.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Click.BorderWidth = 1;
                                        tc_det_dr_Click.Width = 150;
                                        tc_det_dr_Click.Controls.Add(hyperDrClick);
                                        tr_det_sno.Cells.Add(tc_det_dr_Click);

                                        for (int i = 0; i < dsRCPA.Tables[0].Rows.Count; i++)
                                        {
                                            if (dsRCPA.Tables[0].Rows[i]["Trans_Detail_Name"].ToString() == drdoctor["ListedDr_Name"].ToString())
                                            {

                                                hyperDrClick.Text = "&nbsp;" + "Click here";

                                                // sURL = "rptRemarks.aspx?sf_code=" + sf_code + "&Trans_Detail_Slno=" + dsRCPA.Tables[0].Rows[i]["Trans_Detail_Slno"].ToString() + "&Date=" + drdoc["Activity_Date"].ToString() + "&Doc_Name=" + drdoctor["ListedDr_Name"].ToString() + "";
                                                sURL = "Rpt_PhotoCapture.aspx?sf_code=" + sf_code + "&Trans_Detail_Slno=" + dsRCPA.Tables[0].Rows[i]["Trans_Detail_Slno"].ToString() + "&Date=" + drdoc["Activity_Date"].ToString() + "&Doc_Name=" + drdoctor["ListedDr_Name"].ToString() + "";

                                                hyperDrClick.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','');";
                                                hyperDrClick.NavigateUrl = "#";
                                                //hyperDrName.Attributes.Add("href", "javascript:showModalPopUp('" + sf_code + "','" + dsRCPA.Tables[0].Rows[i]["Trans_Detail_Slno"].ToString() + "','" + drdoc["Activity_Date"].ToString() + "')");
                                                tc_det_dr_Click.Controls.Add(hyperDrClick);
                                                break;
                                            }

                                        }

                                        tr_det_sno.Cells.Add(tc_det_dr_Click);


                                        tbldetail.Rows.Add(tr_det_sno);
                                    }
                                }

                                //form1.Controls.Add(tbldetail);

                                tc_det_head_main2.Controls.Add(tbldetail);
                                tr_det_head_main.Cells.Add(tc_det_head_main2);
                                tbldetail_main.Rows.Add(tr_det_head_main);

                                //form1.Controls.Add(tbldetail_main);
                                ExportDiv.Controls.Add(tbldetail_main);

                                if (iCount > 0)
                                {
                                    Table tbl_doc_empty = new Table();
                                    TableRow tr_doc_empty = new TableRow();
                                    TableCell tc_doc_empty = new TableCell();
                                    Literal lit_doc_empty = new Literal();
                                    lit_doc_empty.Text = "<BR>";
                                    tc_doc_empty.Controls.Add(lit_doc_empty);
                                    tr_doc_empty.Cells.Add(tc_doc_empty);
                                    tbl_doc_empty.Rows.Add(tr_doc_empty);
                                    //form1.Controls.Add(tbl_doc_empty);
                                    ExportDiv.Controls.Add(tbl_doc_empty);
                                }

                                //2-Chemists

                                Table tbldetail_main5 = new Table();
                                tbldetail_main5.BorderStyle = BorderStyle.None;
                                //tbldetail_main5.Width = 1100;
                                tbldetail_main5.Style.Add("width", "95%");
                                TableRow tr_det_head_main5 = new TableRow();
                                TableCell tc_det_head_main5 = new TableCell();
                                //tc_det_head_main5.Width = 100;
                                Literal lit_det_main5 = new Literal();
                                lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_det_head_main5.Controls.Add(lit_det_main5);
                                tr_det_head_main5.Cells.Add(tc_det_head_main5);

                                TableCell tc_det_head_main6 = new TableCell();
                                //tc_det_head_main6.Width = 2000;


                                Table tbldetailChe = new Table();
                                //tbldetailChe.BorderStyle = BorderStyle.Solid;
                                //tbldetailChe.BorderWidth = 1;
                                tbldetailChe.GridLines = GridLines.None;
                                //tbldetailChe.Width = 2000;
                                //tbldetailChe.Style.Add("border-collapse", "collapse");
                                //tbldetailChe.Style.Add("border", "solid 1px Black");
                                tbldetailChe.Attributes.Add("class", "table");

                                // dsdoc = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2); //2-Chemists

                                dsDocChemist.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + drdoc["Activity_Date"].ToString() + "' ";
                                DataTable dtChemist = dsDocChemist.Tables[0].DefaultView.ToTable("table1");
                                DataSet dsChemist = new DataSet();
                                dsDocChemist.Merge(dtChemist);
                                dsChemist.Merge(dtChemist);

                                //DataSet dsRCPA = new DataSet();
                                //DCR dcr = new DCR();
                                dsRCPA = dcr.get_RCPA_Capture_Chemist(sf_code, drdoc["Activity_Date"].ToString());

                                iCount = 0;
                                if (dsChemist.Tables[0].Rows.Count > 0)
                                {
                                    TableRow tr_det_head = new TableRow();
                                    //tr_det_head.Style.Add("font-size", "8pt");
                                    TableCell tc_det_head_SNo = new TableCell();
                                    //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_SNo.BorderWidth = 1;
                                    Literal lit_det_head_SNo = new Literal();
                                    lit_det_head_SNo.Text = "#";
                                    tc_det_head_SNo.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                    tr_det_head.Cells.Add(tc_det_head_SNo);

                                    TableCell tc_det_head_Session = new TableCell();
                                    //tc_det_head_Session.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Session.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Session.BorderWidth = 1;
                                    Literal lit_det_head_Session = new Literal();
                                    lit_det_head_Session.Text = "Session";
                                    tc_det_head_Session.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Session.Controls.Add(lit_det_head_Session);
                                    tr_det_head.Cells.Add(tc_det_head_Session);

                                    TableCell tc_det_head_Time = new TableCell();
                                    //tc_det_head_Time.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Time.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Time.BorderWidth = 1;
                                    Literal lit_det_head_Time = new Literal();
                                    lit_det_head_Time.Text = "Time";
                                    tc_det_head_Time.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Time.Controls.Add(lit_det_head_Time);
                                    tr_det_head.Cells.Add(tc_det_head_Time);

                                    TableCell tc_det_head_doc = new TableCell();
                                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_doc.BorderWidth = 1;
                                    Literal lit_det_head_doc = new Literal();
                                    lit_det_head_doc.Text = "Chemists Name";
                                    tc_det_head_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                    tr_det_head.Cells.Add(tc_det_head_doc);

                                    TableCell tc_det_head_Address = new TableCell();
                                    //tc_det_head_Address.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Address.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Address.BorderWidth = 1;
                                    Literal lit_det_head_Address = new Literal();
                                    lit_det_head_Address.Text = "Address";
                                    tc_det_head_Address.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Address.Controls.Add(lit_det_head_Address);
                                    tr_det_head.Cells.Add(tc_det_head_Address);

                                    TableCell tc_det_head_catg = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    Literal lit_det_head_catg = new Literal();
                                    lit_det_head_catg.Text = "POB";
                                    tc_det_head_catg.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                                    tr_det_head.Cells.Add(tc_det_head_catg);

                                    //TableCell tc_det_head_Actual_Place = new TableCell();
                                    ////tc_det_head_Actual_Place.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Actual_Place.HorizontalAlign = HorizontalAlign.Center;
                                    ////tc_det_head_Actual_Place.BorderWidth = 1;
                                    //Literal lit_det_head_Actual_Place = new Literal();
                                    //tc_det_head_Actual_Place.Visible = false;
                                    //lit_det_head_Actual_Place.Text = "Actual Place of Work";
                                    //tc_det_head_Actual_Place.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_Actual_Place.Controls.Add(lit_det_head_Actual_Place);
                                    //tr_det_head.Cells.Add(tc_det_head_Actual_Place);

                                    TableCell tc_det_head_ww = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    Literal lit_det_head_ww = new Literal();
                                    lit_det_head_ww.Text = "Worked With";
                                    tc_det_head_ww.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                    tr_det_head.Cells.Add(tc_det_head_ww);

                                    TableCell tc_det_head_Act_Sample_Prod = new TableCell();
                                    //tc_det_head_Act_Sample_Prod.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Act_Sample_Prod.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Act_Sample_Prod.BorderWidth = 1;
                                    Literal lit_det_head_Act_Sample_Prod = new Literal();
                                    lit_det_head_Act_Sample_Prod.Text = "Product Sampled";
                                    tc_det_head_Act_Sample_Prod.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Act_Sample_Prod.Controls.Add(lit_det_head_Act_Sample_Prod);
                                    tr_det_head.Cells.Add(tc_det_head_Act_Sample_Prod);

                                    TableCell tc_det_head_Input = new TableCell();
                                    //tc_det_head_Input.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Input.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Input.BorderWidth = 1;
                                    Literal lit_det_head_Input = new Literal();
                                    lit_det_head_Input.Text = "Input";
                                    tc_det_head_Input.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Input.Controls.Add(lit_det_head_Input);
                                    tr_det_head.Cells.Add(tc_det_head_Input);

                                    TableCell tc_det_head_LatLog = new TableCell();
                                    //tc_det_head_LatLog.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_LatLog.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_LatLog.BorderWidth = 1;
                                    Literal lit_det_head_LatLog = new Literal();
                                    lit_det_head_LatLog.Text = "Lat & Log";
                                    tc_det_head_LatLog.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_LatLog.Controls.Add(lit_det_head_LatLog);
                                    tr_det_head.Cells.Add(tc_det_head_LatLog);



                                    //TableCell tc_det_head_Rx = new TableCell();
                                    ////tc_det_head_Rx.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Rx.HorizontalAlign = HorizontalAlign.Center;
                                    ////tc_det_head_Rx.BorderWidth = 1;
                                    //Literal lit_det_head_Call_Rx = new Literal();
                                    //lit_det_head_Call_Rx.Text = "Rx";
                                    //tc_det_head_Rx.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_Rx.Controls.Add(lit_det_head_Call_Rx);
                                    //tr_det_head.Cells.Add(tc_det_head_Rx);

                                    TableCell tc_det_head_ActualAdd = new TableCell();
                                    //tc_det_head_Call_Remark.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ActualAdd.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Call_Remark.BorderWidth = 1;
                                    Literal lit_det_head_ActualAdd = new Literal();
                                    lit_det_head_ActualAdd.Text = "Actual Address";
                                    tc_det_head_ActualAdd.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ActualAdd.Controls.Add(lit_det_head_ActualAdd);
                                    tr_det_head.Cells.Add(tc_det_head_ActualAdd);

                                    TableCell tc_det_head_Call_Remark = new TableCell();
                                    //tc_det_head_Call_Remark.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Call_Remark.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Call_Remark.BorderWidth = 1;
                                    Literal lit_det_head_Call_Rematk = new Literal();
                                    lit_det_head_Call_Rematk.Text = "Call Remark";
                                    tc_det_head_Call_Remark.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Call_Remark.Controls.Add(lit_det_head_Call_Rematk);
                                    tr_det_head.Cells.Add(tc_det_head_Call_Remark);

                                    TableCell tc_det_head_Click = new TableCell();
                                    //tc_det_head_Click.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Click.BorderWidth = 1;
                                    tc_det_head_Click.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Click = new Literal();
                                    lit_det_head_Click.Text = "Photo";
                                    tc_det_head_Click.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Click.Controls.Add(lit_det_head_Click);
                                    tr_det_head.Cells.Add(tc_det_head_Click);



                                    tbldetailChe.Rows.Add(tr_det_head);

                                    iCount = 0;
                                    foreach (DataRow drdoctor in dsChemist.Tables[0].Rows)
                                    {
                                        TableRow tr_det_sno = new TableRow();
                                        //tr_det_sno.Style.Add("font-size", "8pt");
                                        //tr_det_sno.Style.Add("font-family", "Verdana");
                                        TableCell tc_det_SNo = new TableCell();
                                        iCount += 1;
                                        Literal lit_det_SNo = new Literal();
                                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                        //tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SNo.BorderWidth = 1;
                                        tc_det_SNo.Controls.Add(lit_det_SNo);
                                        tr_det_sno.Cells.Add(tc_det_SNo);



                                        TableCell tc_det_dr_Session = new TableCell();
                                        Literal lit_det_dr_Session = new Literal();
                                        lit_det_dr_Session.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                                        //tc_det_dr_Session.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Session.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Session.BorderWidth = 1;
                                        tc_det_dr_Session.Controls.Add(lit_det_dr_Session);
                                        tr_det_sno.Cells.Add(tc_det_dr_Session);

                                        TableCell tc_det_dr_Time = new TableCell();
                                        Literal lit_det_dr_Time = new Literal();
                                        lit_det_dr_Time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                                        //tc_det_dr_Time.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Time.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Time.BorderWidth = 1;
                                        tc_det_dr_Time.Controls.Add(lit_det_dr_Time);
                                        tr_det_sno.Cells.Add(tc_det_dr_Time);

                                        TableCell tc_det_dr_name = new TableCell();
                                        Literal lit_det_dr_name = new Literal();
                                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Chemists_Name"].ToString();
                                        //tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_name.BorderWidth = 1;
                                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                        tr_det_sno.Cells.Add(tc_det_dr_name);

                                        TableCell tc_det_dr_Address = new TableCell();
                                        Literal lit_det_dr_Address = new Literal();
                                        lit_det_dr_Address.Text = "&nbsp;&nbsp;" + drdoctor["chemists_address1"].ToString();
                                        //tc_det_dr_Address.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Address.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Address.BorderWidth = 1;
                                        tc_det_dr_Address.Controls.Add(lit_det_dr_Address);
                                        tr_det_sno.Cells.Add(tc_det_dr_Address);

                                        TableCell tc_det_dr_POB = new TableCell();
                                        Literal lit_det_dr_POB = new Literal();
                                        lit_det_dr_POB.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                                        //tc_det_dr_POB.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_POB.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_POB.BorderWidth = 1;
                                        tc_det_dr_POB.Controls.Add(lit_det_dr_POB);
                                        tr_det_sno.Cells.Add(tc_det_dr_POB);

                                        //string strlongname = "";
                                        //int iReturn = -1;
                                        //if ((drdoctor["GeoAddrs"].ToString().Trim() == "NA" || drdoctor["GeoAddrs"].ToString().Trim() == "") && drdoctor["lati"] != "")
                                        //{
                                        //    //string str = "test";
                                        //    //ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'" + str + "'\');", true);

                                        //    sURL = "DCR_ShowMap.aspx?sf_Code=" + sf_code + " &strDate=" + drdoc["Activity_Date"].ToString() + " ";
                                        //    lit_day.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        //    lit_day.NavigateUrl = "#";

                                        //    int i = 0;
                                        //    XmlDocument doc = new XmlDocument();

                                        //    WebClient client = new WebClient();

                                        //    doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + drdoctor["lati"] + "," + drdoctor["long"] + "&sensor=false");
                                        //    XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                                        //    XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                                        //    strlongname = "";
                                        //    foreach (XmlNode xn in xnList)
                                        //    {
                                        //        i += 1;
                                        //        if (i < 8)
                                        //        {
                                        //            strlongname += xn["long_name"].InnerText + ",";
                                        //        }
                                        //    }
                                        //    if (strlongname != "")
                                        //    {
                                        //        DCR_New dcr = new DCR_New();

                                        //        iReturn = dcr.DCRView_Insert(drdoctor["trans_detail_slno"].ToString(), strlongname.Replace("'", "asdf"));
                                        //    }
                                        //}



                                        TableCell tc_det_work = new TableCell();
                                        Literal lit_det_work = new Literal();
                                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString().Replace(Sf_Name.Trim(), "Self");
                                        //tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_work.Controls.Add(lit_det_work);
                                        tr_det_sno.Cells.Add(tc_det_work);



                                        //TableCell tc_det_dr_CallFeedBack = new TableCell();
                                        //Literal lit_det_dr_CallFeedBack = new Literal();
                                        //lit_det_dr_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        //tc_det_dr_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_CallFeedBack.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_CallFeedBack.BorderWidth = 1;
                                        //tc_det_dr_CallFeedBack.Controls.Add(lit_det_dr_CallFeedBack);
                                        //tr_det_sno.Cells.Add(tc_det_dr_CallFeedBack);

                                        TableCell tc_det_Product_Sample = new TableCell();
                                        Literal lit_det_Product_Sample = new Literal();
                                        lit_det_Product_Sample.Text = "&nbsp;&nbsp;" + drdoctor["Product_Detail"].ToString();
                                        //tc_det_Product_Sample.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Product_Sample.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Product_Sample.BorderWidth = 1;
                                        tc_det_Product_Sample.Controls.Add(lit_det_Product_Sample);
                                        tr_det_sno.Cells.Add(tc_det_Product_Sample);

                                        TableCell tc_det_Gift = new TableCell();
                                        Literal lit_det_Gift = new Literal();
                                        lit_det_Gift.Text = "&nbsp;&nbsp;" + drdoctor["additional_gift_dtl"].ToString();
                                        //tc_det_Gift.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Gift.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Gift.BorderWidth = 1;
                                        tc_det_Gift.Controls.Add(lit_det_Gift);
                                        tr_det_sno.Cells.Add(tc_det_Gift);

                                        TableCell tc_det_LatLog = new TableCell();
                                        HyperLink lit_det_LatLog = new HyperLink();
                                        lit_det_LatLog.Text = drdoctor["lati"].ToString() + " - " + drdoctor["long"].ToString();
                                        sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + drdoc["Activity_Date"].ToString() + "/&Mode=" + "/" + "C" + "";
                                        lit_det_LatLog.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        lit_det_LatLog.NavigateUrl = "#";
                                        //tc_det_LatLog.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_LatLog.BorderStyle = BorderStyle.Solid;
                                        //tc_det_LatLog.BorderWidth = 1;
                                        tc_det_LatLog.Controls.Add(lit_det_LatLog);
                                        tr_det_sno.Cells.Add(tc_det_LatLog);

                                        TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                                        Literal lit_det_dr_Act_Place_Worked = new Literal();
                                        tc_det_dr_Act_Place_Worked.Visible = false;
                                        lit_det_dr_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        //tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                                        tc_det_dr_Act_Place_Worked.Width = 250;
                                        tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                                        tr_det_sno.Cells.Add(tc_det_dr_Act_Place_Worked);




                                        //TableCell tc_det_Rx = new TableCell();
                                        //Literal lit_det_Rx = new Literal();
                                        //lit_det_Rx.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        ////tc_det_Rx.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_Rx.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_Rx.BorderWidth = 1;
                                        //tc_det_Rx.Controls.Add(lit_det_Rx);
                                        //tr_det_sno.Cells.Add(tc_det_Rx);

                                        TableCell tc_det_CallRemarks = new TableCell();
                                        Literal lit_det_CallRemarks = new Literal();
                                        lit_det_CallRemarks.Text = "&nbsp;&nbsp;" + drdoctor["activity_remarks"].ToString();
                                        //tc_det_CallRemarks.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_CallRemarks.BorderStyle = BorderStyle.Solid;
                                        //tc_det_CallRemarks.BorderWidth = 1;
                                        tc_det_CallRemarks.Controls.Add(lit_det_CallRemarks);
                                        tr_det_sno.Cells.Add(tc_det_CallRemarks);

                                        TableCell tc_det_dr_Click = new TableCell();
                                        //Literal lit_det_dr_name = new Literal();
                                        HyperLink hyperDrClick = new HyperLink();
                                        //hyperDrClick.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                                        //tc_det_dr_Click.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_dr_Click.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Click.BorderWidth = 1;
                                        tc_det_dr_Click.Width = 150;
                                        tc_det_dr_Click.Controls.Add(hyperDrClick);
                                        tr_det_sno.Cells.Add(tc_det_dr_Click);

                                        for (int i = 0; i < dsRCPA.Tables[0].Rows.Count; i++)
                                        {
                                            if (dsRCPA.Tables[0].Rows[i]["Trans_Detail_Name"].ToString() == drdoctor["Chemists_Name"].ToString())
                                            {

                                                hyperDrClick.Text = "&nbsp;" + "Click here";

                                                sURL = "rptRemarks.aspx?sf_code=" + sf_code + "&Trans_Detail_Slno=" + dsRCPA.Tables[0].Rows[i]["Trans_Detail_Slno"].ToString() + "&Date=" + drdoc["Activity_Date"].ToString() + "&Doc_Name=" + drdoctor["Chemists_Name"].ToString() + "";

                                                hyperDrClick.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','');";
                                                hyperDrClick.NavigateUrl = "#";
                                                //hyperDrName.Attributes.Add("href", "javascript:showModalPopUp('" + sf_code + "','" + dsRCPA.Tables[0].Rows[i]["Trans_Detail_Slno"].ToString() + "','" + drdoc["Activity_Date"].ToString() + "')");
                                                tc_det_dr_Click.Controls.Add(hyperDrClick);
                                                break;
                                            }

                                        }

                                        tr_det_sno.Cells.Add(tc_det_dr_Click);


                                        tbldetail.Rows.Add(tr_det_sno);



                                        tbldetailChe.Rows.Add(tr_det_sno);
                                    }
                                }

                                //form1.Controls.Add(tbldetailChe);

                                tc_det_head_main6.Controls.Add(tbldetailChe);
                                tr_det_head_main5.Cells.Add(tc_det_head_main6);
                                tbldetail_main5.Rows.Add(tr_det_head_main5);

                                //form1.Controls.Add(tbldetail_main5);
                                ExportDiv.Controls.Add(tbldetail_main5);


                                if (iCount > 0)
                                {
                                    Table tbl_chem_empty = new Table();
                                    TableRow tr_chem_empty = new TableRow();
                                    TableCell tc_chem_empty = new TableCell();
                                    Literal lit_chem_empty = new Literal();
                                    lit_chem_empty.Text = "<BR>";
                                    tc_chem_empty.Controls.Add(lit_chem_empty);
                                    tr_chem_empty.Cells.Add(tc_chem_empty);
                                    tbl_chem_empty.Rows.Add(tr_chem_empty);
                                    //form1.Controls.Add(tbl_chem_empty);
                                    ExportDiv.Controls.Add(tbl_chem_empty);
                                }

                                //4-UnListed Doctor

                                Table tbldetail_main7 = new Table();
                                tbldetail_main7.BorderStyle = BorderStyle.None;
                                //tbldetail_main7.Width = 1100;
                                tbldetail_main7.Style.Add("width", "95%");
                                TableRow tr_det_head_main7 = new TableRow();
                                TableCell tc_det_head_main7 = new TableCell();
                                //tc_det_head_main7.Width = 100;
                                Literal lit_det_main7 = new Literal();
                                lit_det_main7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_det_head_main7.Controls.Add(lit_det_main7);
                                tr_det_head_main7.Cells.Add(tc_det_head_main7);

                                TableCell tc_det_head_main8 = new TableCell();
                                //tc_det_head_main8.Width = 2000;

                                Table tblUnLstDoc = new Table();
                                //tblUnLstDoc.BorderStyle = BorderStyle.Solid;
                                //tblUnLstDoc.BorderWidth = 1;
                                tblUnLstDoc.GridLines = GridLines.None;
                                ////tblUnLstDoc.Width = 2000;
                                //tblUnLstDoc.Style.Add("border-collapse", "collapse");
                                //tblUnLstDoc.Style.Add("border", "solid 1px Black");
                                tblUnLstDoc.Attributes.Add("class", "table");

                                dsDocUnlist.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + drdoc["Activity_Date"].ToString() + "' ";
                                DataTable dtUnlist = dsDocUnlist.Tables[0].DefaultView.ToTable("table1");
                                DataSet dsUnlist = new DataSet();
                                dsDocUnlist.Merge(dtUnlist);
                                dsUnlist.Merge(dtUnlist);

                                // dsdoc = dc.get_unlst_doc_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                                iCount = 0;
                                if (dsUnlist.Tables[0].Rows.Count > 0)
                                {
                                    TableRow tr_UnLst_doc_head = new TableRow();
                                    //tr_UnLst_doc_head.Style.Add("font-size", "8pt");
                                    //tr_UnLst_doc_head.Style.Add("font-family", "Verdana");
                                    TableCell tc_UnLst_doc_head_SNo = new TableCell();
                                    tc_UnLst_doc_head_SNo.BorderStyle = BorderStyle.Solid;
                                    tc_UnLst_doc_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                    tc_UnLst_doc_head_SNo.BorderWidth = 1;
                                    Literal lit_undet_head_SNo = new Literal();
                                    lit_undet_head_SNo.Text = "#";
                                    tc_UnLst_doc_head_SNo.Attributes.Add("Class", "stickyFirstRow");
                                    tc_UnLst_doc_head_SNo.Controls.Add(lit_undet_head_SNo);
                                    tr_UnLst_doc_head.Cells.Add(tc_UnLst_doc_head_SNo);

                                    TableCell tc_undet_head_Ses = new TableCell();
                                    //tc_undet_head_Ses.BorderStyle = BorderStyle.Solid;
                                    tc_undet_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_undet_head_Ses.BorderWidth = 1;
                                    Literal lit_undet_head_Ses = new Literal();
                                    lit_undet_head_Ses.Text = "Session";
                                    tc_undet_head_Ses.Attributes.Add("Class", "stickyFirstRow");
                                    tc_undet_head_Ses.Controls.Add(lit_undet_head_Ses);
                                    tr_UnLst_doc_head.Cells.Add(tc_undet_head_Ses);

                                    TableCell tc_det_head_time = new TableCell();
                                    //tc_det_head_time.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_time.BorderWidth = 1;
                                    Literal lit_det_head_time = new Literal();
                                    lit_det_head_time.Text = "Time";
                                    tc_det_head_time.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_time.Controls.Add(lit_det_head_time);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_time);

                                    TableCell tc_det_head_doc = new TableCell();
                                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_doc.BorderWidth = 1;
                                    Literal lit_det_head_doc = new Literal();
                                    lit_det_head_doc.Text = "UnListed Doctor Name";
                                    tc_det_head_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_doc);

                                    TableCell tc_det_head_Address = new TableCell();
                                    //tc_det_head_Address.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_Address.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_Address.BorderWidth = 1;
                                    Literal lit_det_head_Address = new Literal();
                                    lit_det_head_Address.Text = "Address";
                                    tc_det_head_Address.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Address.Controls.Add(lit_det_head_Address);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_Address);

                                    TableCell tc_det_head_catg = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    Literal lit_det_head_catg = new Literal();
                                    lit_det_head_catg.Text = "Category";
                                    tc_det_head_catg.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_catg);

                                    TableCell tc_det_head_spec = new TableCell();
                                    //tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_spec.BorderWidth = 1;
                                    Literal lit_det_head_spec = new Literal();
                                    lit_det_head_spec.Text = "Speciality";
                                    tc_det_head_spec.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_spec);

                                    TableCell tc_det_head_Qual = new TableCell();
                                    //tc_det_head_Qual.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Qual.BorderWidth = 1;
                                    tc_det_head_Qual.Width = 100;
                                    tc_det_head_Qual.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Qual = new Literal();
                                    lit_det_head_Qual.Text = "Qualification";
                                    tc_det_head_Qual.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Qual.Controls.Add(lit_det_head_Qual);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_Qual);

                                    TableCell tc_det_head_Plan_Name = new TableCell();
                                    //tc_det_head_Plan_Name.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Plan_Name.BorderWidth = 1;
                                    tc_det_head_Plan_Name.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_Plan_Name = new Literal();
                                    tc_det_head_Plan_Name.Width = 200;
                                    if (dsTerritory.Tables[0].Rows.Count > 0)
                                    {
                                        lit_det_head_Plan_Name.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                                    }
                                    //lit_det_head_Plan_Name.Text = "<b>Territory Name</b>";
                                    tc_det_head_Plan_Name.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_Plan_Name.Controls.Add(lit_det_head_Plan_Name);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_Plan_Name);

                                    //TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                                    //Literal lit_det_dr_Act_Place_Worked = new Literal();
                                    //tc_det_dr_Act_Place_Worked.Visible = false;
                                    //lit_det_dr_Act_Place_Worked.Text = "Actual_Place_of_Worked";
                                    //tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "stickyFirstRow");
                                    ////tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                                    ////tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                                    //tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                                    //tr_UnLst_doc_head.Cells.Add(tc_det_dr_Act_Place_Worked);

                                    TableCell tc_det_head_TerType = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_TerType.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    Literal lit_det_head_TerType = new Literal();
                                    lit_det_head_TerType.Text = "Territory Type";
                                    tc_det_head_TerType.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_TerType.Controls.Add(lit_det_head_TerType);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_TerType);

                                    TableCell tc_det_head_ww = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    Literal lit_det_head_ww = new Literal();
                                    lit_det_head_ww.Text = "Worked With";
                                    tc_det_head_ww.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_ww);

                                    TableCell tc_det_head_POB = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_POB.Width = 1000;
                                    tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_POB = new Literal();
                                    lit_det_head_POB.Text = "POB";
                                    tc_det_head_POB.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_POB.Controls.Add(lit_det_head_POB);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_POB);

                                    TableCell tc_det_head_prod = new TableCell();
                                    //tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_prod.BorderWidth = 1;
                                    Literal lit_det_head_prod = new Literal();
                                    lit_det_head_prod.Text = "Product Detailed";
                                    tc_det_head_prod.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_prod);

                                    TableCell tc_det_head_sampled = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_sampled.Width = 1000;
                                    tc_det_head_sampled.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_sampled = new Literal();
                                    lit_det_head_sampled.Text = "Product Sampled";
                                    tc_det_head_sampled.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_sampled.Controls.Add(lit_det_head_sampled);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_sampled);

                                    TableCell tc_det_head_PrdRx = new TableCell();
                                    //tc_det_head_Target.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_Target.BorderWidth = 1;
                                    tc_det_head_PrdRx.Width = 1000;
                                    tc_det_head_PrdRx.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_PrdRx = new Literal();
                                    lit_det_head_PrdRx.Text = "Product Rx";
                                    tc_det_head_PrdRx.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_PrdRx.Controls.Add(lit_det_head_PrdRx);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_PrdRx);

                                    TableCell tc_det_head_CallFeed_Back = new TableCell();
                                    //tc_det_head_CallFeed_Back.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_CallFeed_Back.BorderWidth = 1;
                                    tc_det_head_CallFeed_Back.Width = 100;
                                    tc_det_head_CallFeed_Back.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_CallFeed_Back = new Literal();
                                    //lit_det_head_CallFeed_Back.Text = "Rx";
                                    lit_det_head_CallFeed_Back.Text = "Call Feedback";
                                    tc_det_head_CallFeed_Back.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_CallFeed_Back.Controls.Add(lit_det_head_CallFeed_Back);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_CallFeed_Back);

                                    TableCell tc_det_head_CallRemarks = new TableCell();
                                    //tc_det_head_CallFeed_Back.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_CallFeed_Back.BorderWidth = 1;
                                    tc_det_head_CallRemarks.Width = 100;
                                    tc_det_head_CallRemarks.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_CallRemarks = new Literal();
                                    //lit_det_head_CallFeed_Back.Text = "Rx";
                                    lit_det_head_CallRemarks.Text = "Call Remarks";
                                    tc_det_head_CallRemarks.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_CallRemarks.Controls.Add(lit_det_head_CallRemarks);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_CallRemarks);

                                    TableCell tc_det_head_gift = new TableCell();
                                    //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_gift.BorderWidth = 1;
                                    Literal lit_det_head_gift = new Literal();
                                    lit_det_head_gift.Text = "Input";
                                    tc_det_head_gift.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_gift);


                                    TableCell tc_det_head_LatLog = new TableCell();
                                    //tc_det_head_LatLog.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_LatLog.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_LatLog.BorderWidth = 1;
                                    Literal lit_det_head_LatLog = new Literal();
                                    lit_det_head_LatLog.Text = "Lat & Log";
                                    tc_det_head_LatLog.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_LatLog.Controls.Add(lit_det_head_LatLog);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_LatLog);

                                    TableCell tc_det_head_ActAddress = new TableCell();
                                    tc_det_head_ActAddress.Width = 100;
                                    tc_det_head_ActAddress.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lit_det_head_ActAddress = new Literal();
                                    lit_det_head_ActAddress.Text = "Actual Address";
                                    tc_det_head_ActAddress.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ActAddress.Controls.Add(lit_det_head_ActAddress);
                                    tr_UnLst_doc_head.Cells.Add(tc_det_head_ActAddress);


                                    tblUnLstDoc.Rows.Add(tr_UnLst_doc_head);

                                    iCount = 0;
                                    foreach (DataRow drdoctor in dsUnlist.Tables[0].Rows)
                                    {
                                        TableRow tr_det_sno = new TableRow();
                                        //tr_det_sno.Style.Add("font-size", "8pt");
                                        //tr_det_sno.Style.Add("font-family", "Verdana");
                                        TableCell tc_det_SNo = new TableCell();
                                        iCount += 1;
                                        Literal lit_det_SNo = new Literal();
                                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SNo.BorderWidth = 1;
                                        tc_det_SNo.Controls.Add(lit_det_SNo);
                                        tr_det_sno.Cells.Add(tc_det_SNo);

                                        TableCell tc_det_Ses = new TableCell();
                                        Literal lit_det_Ses = new Literal();
                                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                                        //tc_det_Ses.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Ses.BorderWidth = 1;
                                        tc_det_Ses.Controls.Add(lit_det_Ses);
                                        tr_det_sno.Cells.Add(tc_det_Ses);

                                        TableCell tc_det_Time = new TableCell();
                                        Literal lit_det_Time = new Literal();
                                        lit_det_Time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                                        //tc_det_Time.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Time.BorderWidth = 1;
                                        tc_det_Time.Controls.Add(lit_det_Time);
                                        tr_det_sno.Cells.Add(tc_det_Time);

                                        TableCell tc_det_dr_name = new TableCell();
                                        Literal lit_det_dr_name = new Literal();
                                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["UnListedDr_Name"].ToString();
                                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_name.BorderWidth = 1;
                                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                        tr_det_sno.Cells.Add(tc_det_dr_name);

                                        TableCell tc_det_LastAddress = new TableCell();
                                        Literal lit_det_Address = new Literal();
                                        lit_det_Address.Text = "&nbsp;&nbsp;" + drdoctor["Unlisteddr_address1"].ToString();
                                        //tc_det_LastAddress.BorderStyle = BorderStyle.Solid;
                                        //tc_det_LastAddress.BorderWidth = 1;
                                        tc_det_LastAddress.Controls.Add(lit_det_Address);
                                        tr_det_sno.Cells.Add(tc_det_LastAddress);

                                        TableCell tc_det_catg = new TableCell();
                                        Literal lit_det_catg = new Literal();
                                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                                        //tc_det_catg.BorderStyle = BorderStyle.Solid;
                                        //tc_det_catg.BorderWidth = 1;
                                        tc_det_catg.Controls.Add(lit_det_catg);
                                        tr_det_sno.Cells.Add(tc_det_catg);

                                        TableCell tc_det_spec = new TableCell();
                                        Literal lit_det_spec = new Literal();
                                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                                        //tc_det_spec.BorderStyle = BorderStyle.Solid;
                                        //tc_det_spec.BorderWidth = 1;
                                        tc_det_spec.Controls.Add(lit_det_spec);
                                        tr_det_sno.Cells.Add(tc_det_spec);

                                        TableCell tc_det_Qualification = new TableCell();
                                        Literal lit_det_Qual = new Literal();
                                        lit_det_Qual.Text = "&nbsp;&nbsp;" + drdoctor["Doc_QuaName"].ToString();
                                        //tc_det_Qualification.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Qualification.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Qualification.BorderWidth = 1;
                                        tc_det_Qualification.Controls.Add(lit_det_Qual);
                                        tr_det_sno.Cells.Add(tc_det_Qualification);


                                        TableCell tc_det_SDP_Name = new TableCell();
                                        Literal lit_det_SDP_Name = new Literal();
                                        lit_det_SDP_Name.Text = "&nbsp;&nbsp;" + drdoctor["SDP_Name"].ToString();
                                        //tc_det_SDP_Name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SDP_Name.BorderWidth = 1;
                                        tc_det_SDP_Name.Controls.Add(lit_det_SDP_Name);
                                        tr_det_sno.Cells.Add(tc_det_SDP_Name);

                                        //TableCell tc_det_Act_Place_Worked = new TableCell();
                                        //Literal lit_det_Act_Place_Worked = new Literal();
                                        //tc_det_Act_Place_Worked.Visible = false;
                                        //lit_det_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        ////tc_det_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_Act_Place_Worked.BorderWidth = 1;
                                        //tc_det_Act_Place_Worked.Width = 250;
                                        //tc_det_Act_Place_Worked.Controls.Add(lit_det_Act_Place_Worked);
                                        //tr_det_sno.Cells.Add(tc_det_Act_Place_Worked);

                                        TableCell tc_det_TerType = new TableCell();
                                        Literal lit_det_TerType = new Literal();
                                        lit_det_TerType.Text = "&nbsp;&nbsp;" + drdoctor["Territory_Cat"].ToString();
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_TerType.Controls.Add(lit_det_TerType);
                                        tr_det_sno.Cells.Add(tc_det_TerType);

                                        TableCell tc_det_work = new TableCell();
                                        Literal lit_det_work = new Literal();
                                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_work.Controls.Add(lit_det_work);
                                        tr_det_sno.Cells.Add(tc_det_work);

                                        TableCell tc_det_POB = new TableCell();
                                        Literal lit_det_POB = new Literal();
                                        lit_det_POB.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                                        //tc_det_Territory.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_Territory.BorderStyle = BorderStyle.Solid;
                                        //tc_det_Territory.BorderWidth = 1;
                                        tc_det_POB.Controls.Add(lit_det_POB);
                                        tr_det_sno.Cells.Add(tc_det_POB);

                                        TableCell tc_det_prod = new TableCell();
                                        Literal lit_det_prod = new Literal();
                                        tc_det_prod.Width = 1500;
                                        lit_det_prod.Text = drdoctor["Detailed"].ToString().Replace("~", "/").Trim();
                                        tc_det_prod.Controls.Add(lit_det_prod);
                                        tr_det_sno.Cells.Add(tc_det_prod);

                                        TableCell tc_det_prodsample = new TableCell();
                                        Literal lit_det_prodsample = new Literal();
                                        tc_det_prodsample.Width = 1500;
                                        lit_det_prodsample.Text = drdoctor["Sampled"].ToString().Replace("~", "/").Trim();
                                        tc_det_prodsample.Controls.Add(lit_det_prodsample);
                                        tr_det_sno.Cells.Add(tc_det_prodsample);

                                        TableCell tc_det_prodRX = new TableCell();
                                        Literal lit_det_prodRX = new Literal();
                                        tc_det_prodRX.Width = 1500;
                                        lit_det_prodRX.Text = drdoctor["PrdRx"].ToString().Replace("~", "/").Trim();
                                        tc_det_prodRX.Controls.Add(lit_det_prodRX);
                                        tr_det_sno.Cells.Add(tc_det_prodRX);

                                        TableCell tc_det_Callfeed = new TableCell();
                                        Literal lit_det_Callfeed = new Literal();
                                        tc_det_Callfeed.Width = 500;
                                        lit_det_Callfeed.Text = drdoctor["CallFeed"].ToString().Replace("~", "/").Trim();
                                        tc_det_Callfeed.Controls.Add(lit_det_Callfeed);
                                        tr_det_sno.Cells.Add(tc_det_Callfeed);


                                        TableCell tc_det_CallRemarks = new TableCell();
                                        Literal lit_det_CallRemarks = new Literal();
                                        tc_det_CallRemarks.Width = 500;
                                        lit_det_CallRemarks.Text = drdoctor["Activity_Remarks"].ToString().Replace("~", "/").Trim();
                                        tc_det_CallRemarks.Controls.Add(lit_det_CallRemarks);
                                        tr_det_sno.Cells.Add(tc_det_CallRemarks);

                                        //TableCell tc_det_prod = new TableCell();
                                        //Literal lit_det_prod = new Literal();
                                        //lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                                        //lit_det_prod.Text = lit_det_prod.Text.Replace("$0", ")").Trim();
                                        //lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                                        ////tc_det_prod.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_prod.BorderWidth = 1;
                                        //tc_det_prod.Controls.Add(lit_det_prod);
                                        //tr_det_sno.Cells.Add(tc_det_prod);

                                        //TableCell tc_det_lvisit = new TableCell();
                                        //Literal lit_det_lvisit = new Literal();
                                        //lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                                        //tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                                        //tc_det_lvisit.BorderWidth = 1;
                                        //tc_det_lvisit.Controls.Add(lit_det_lvisit);
                                        //tr_det_sno.Cells.Add(tc_det_lvisit);

                                        TableCell tc_det_gift = new TableCell();
                                        Literal lit_det_gift = new Literal();
                                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", " ").Trim();
                                        //tc_det_gift.BorderStyle = BorderStyle.Solid;
                                        //tc_det_gift.BorderWidth = 1;
                                        tc_det_gift.Controls.Add(lit_det_gift);
                                        tr_det_sno.Cells.Add(tc_det_gift);

                                        TableCell tc_det_LatLog = new TableCell();
                                        HyperLink lit_det_Latlog = new HyperLink();
                                        lit_det_Latlog.Text = drdoctor["lati"].ToString() + " - " + drdoctor["long"].ToString();
                                        sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + drdoc["Activity_Date"].ToString() + "/&Mode=" + "/" + "ULD" + "";
                                        lit_det_Latlog.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        lit_det_Latlog.NavigateUrl = "#";
                                        //tc_det_LatLog.BorderStyle = BorderStyle.Solid;
                                        //tc_det_LatLog.BorderWidth = 1;
                                        tc_det_LatLog.Controls.Add(lit_det_Latlog);
                                        tr_det_sno.Cells.Add(tc_det_LatLog);

                                        TableCell tc_det_ActAddr = new TableCell();
                                        Literal lit_det_ActAddr = new Literal();
                                        lit_det_ActAddr.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        tc_det_ActAddr.Controls.Add(lit_det_ActAddr);
                                        tr_det_sno.Cells.Add(tc_det_ActAddr);





                                        //TableCell tc_det_CallFeedBack = new TableCell();
                                        //Literal lit_det_CallFeedBack = new Literal();
                                        //lit_det_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        //tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                                        //tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                                        //tc_det_CallFeedBack.BorderWidth = 1;
                                        //tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                                        //tr_det_sno.Cells.Add(tc_det_CallFeedBack);

                                        //TableCell tc_det_Active_Remark = new TableCell();
                                        //Literal lit_det_Active_Remark = new Literal();
                                        //lit_det_Active_Remark.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        ////tc_det_Active_Remark.Attributes.Add("Class", "tbldetail_Data");
                                        ////tc_det_Active_Remark.BorderStyle = BorderStyle.Solid;
                                        ////tc_det_Active_Remark.BorderWidth = 1;
                                        //tc_det_Active_Remark.Controls.Add(lit_det_Active_Remark);
                                        //tr_det_sno.Cells.Add(tc_det_Active_Remark);





                                        tblUnLstDoc.Rows.Add(tr_det_sno);
                                    }
                                }

                                //form1.Controls.Add(tblUnLstDoc);

                                tc_det_head_main8.Controls.Add(tblUnLstDoc);
                                tr_det_head_main7.Cells.Add(tc_det_head_main8);
                                tbldetail_main7.Rows.Add(tr_det_head_main7);

                                //form1.Controls.Add(tbldetail_main7);
                                ExportDiv.Controls.Add(tbldetail_main7);


                                if (iCount > 0)
                                {
                                    Table tbl_undoc_empty = new Table();
                                    TableRow tr_undoc_empty = new TableRow();
                                    TableCell tc_undoc_empty = new TableCell();
                                    Literal lit_undoc_empty = new Literal();
                                    lit_undoc_empty.Text = "<BR>";
                                    tc_undoc_empty.Controls.Add(lit_undoc_empty);
                                    tr_undoc_empty.Cells.Add(tc_undoc_empty);
                                    tbl_undoc_empty.Rows.Add(tr_undoc_empty);
                                    //form1.Controls.Add(tbl_undoc_empty);
                                    ExportDiv.Controls.Add(tbl_undoc_empty);
                                }

                                // 3- Stockist

                                //5-Hospitals

                                Table tbldetail_main11 = new Table();
                                tbldetail_main11.BorderStyle = BorderStyle.None;
                                //tbldetail_main11.Width = 1100;
                                tbldetail_main11.Style.Add("width", "100%");
                                TableRow tr_det_head_main11 = new TableRow();
                                TableCell tc_det_head_main11 = new TableCell();
                                //tr_det_head_main11.Width = 100;
                                Literal lit_det_main11 = new Literal();
                                lit_det_main11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_det_head_main11.Controls.Add(lit_det_main11);
                                tr_det_head_main11.Cells.Add(tc_det_head_main11);

                                TableCell tc_det_head_main12 = new TableCell();
                                //tc_det_head_main12.Width = 2000;


                                Table tbldetailstk = new Table();
                                //tbldetailstk.BorderStyle = BorderStyle.Solid;
                                //tbldetailstk.BorderWidth = 1;
                                tbldetailstk.GridLines = GridLines.None;
                                //tbldetailstk.Width = 2000;
                                //tbldetailstk.Style.Add("border-collapse", "collapse");
                                //tbldetailstk.Style.Add("border", "solid 1px Black");
                                tbldetailstk.Attributes.Add("class", "table");

                                dsDocStk.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + drdoc["Activity_Date"].ToString() + "' ";
                                DataTable dtStk = dsDocStk.Tables[0].DefaultView.ToTable("table1");
                                DataSet dsStk = new DataSet();
                                dsDocStk.Merge(dtStk);
                                dsStk.Merge(dtStk);

                                //dsdoc = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3); //3-Stockist


                                iCount = 0;
                                if (dsStk.Tables[0].Rows.Count > 0)
                                {
                                    TableRow tr_det_head = new TableRow();
                                    //tr_det_head.Style.Add("font-size", "8pt");
                                    //tr_det_head.Style.Add("font-family", "Verdana");
                                    TableCell tc_det_head_SNo = new TableCell();
                                    //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_SNo.BorderWidth = 1;
                                    Literal lit_det_head_SNo = new Literal();
                                    lit_det_head_SNo.Text = "#";
                                    tc_det_head_SNo.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                    tr_det_head.Cells.Add(tc_det_head_SNo);

                                    TableCell tc_det_head_doc = new TableCell();
                                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_doc.BorderWidth = 1;
                                    Literal lit_det_head_doc = new Literal();
                                    lit_det_head_doc.Text = "Stockist Name";
                                    tc_det_head_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                    tr_det_head.Cells.Add(tc_det_head_doc);

                                    //TableCell tc_det_head_VistTime = new TableCell();
                                    //tc_det_head_VistTime.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_VistTime.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_VistTime.BorderWidth = 1;
                                    //Literal lit_det_head_VistTime = new Literal();
                                    //lit_det_head_VistTime.Text = "<b>Visit Time</b>";
                                    //tc_det_head_VistTime.Attributes.Add("Class", "tr_det_head");
                                    //tc_det_head_VistTime.Controls.Add(lit_det_head_VistTime);
                                    //tr_det_head.Cells.Add(tc_det_head_VistTime);

                                    //TableCell tc_det_head_LastUpdate = new TableCell();
                                    //tc_det_head_LastUpdate.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_LastUpdate.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_LastUpdate.BorderWidth = 1;
                                    //Literal lit_det_head_LastUpdate = new Literal();
                                    //lit_det_head_LastUpdate.Text = "<b>Last Updated</b>";
                                    //tc_det_head_LastUpdate.Attributes.Add("Class", "tr_det_head");
                                    //tc_det_head_LastUpdate.Controls.Add(lit_det_head_LastUpdate);
                                    //tr_det_head.Cells.Add(tc_det_head_LastUpdate);

                                    TableCell tc_det_head_catg = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    Literal lit_det_head_catg = new Literal();
                                    lit_det_head_catg.Text = "POB";
                                    tc_det_head_catg.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                                    tr_det_head.Cells.Add(tc_det_head_catg);

                                    TableCell tc_det_head_ww = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    Literal lit_det_head_ww = new Literal();
                                    lit_det_head_ww.Text = "Worked With";
                                    tc_det_head_ww.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                    tr_det_head.Cells.Add(tc_det_head_ww);

                                    TableCell tc_det_head_ActualPlace = new TableCell();
                                    //tc_det_head_ActualPlace.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ActualPlace.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ActualPlace.BorderWidth = 1;
                                    Literal lit_det_head_ActualPlace = new Literal();
                                    lit_det_head_ActualPlace.Text = "Actual Address";
                                    tc_det_head_ActualPlace.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ActualPlace.Controls.Add(lit_det_head_ActualPlace);
                                    tr_det_head.Cells.Add(tc_det_head_ActualPlace);

                                    //TableCell tc_det_head_CallFeedBack = new TableCell();
                                    //tc_det_head_CallFeedBack.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_CallFeedBack.BorderWidth = 1;
                                    //Literal lit_det_head_CallFeedBack = new Literal();
                                    //lit_det_head_CallFeedBack.Text = "<b>Call Feedback</b>";
                                    //tc_det_head_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                                    //tc_det_head_CallFeedBack.Controls.Add(lit_det_head_CallFeedBack);
                                    //tr_det_head.Cells.Add(tc_det_head_CallFeedBack);

                                    TableCell tc_det_head_LatLog = new TableCell();
                                    Literal lit_det_head_Latlog = new Literal();
                                    lit_det_head_Latlog.Text = "Lat & Log";
                                    tc_det_head_LatLog.Attributes.Add("Class", "stickyFirstRow");
                                    //tc_det_head_LatLog.BorderStyle = BorderStyle.Solid;
                                    //tc_det_head_LatLog.BorderWidth = 1;
                                    tc_det_head_LatLog.Controls.Add(lit_det_head_Latlog);
                                    tr_det_head.Cells.Add(tc_det_head_LatLog);




                                    tbldetailstk.Rows.Add(tr_det_head);

                                    iCount = 0;
                                    foreach (DataRow drdoctor in dsStk.Tables[0].Rows)
                                    {
                                        TableRow tr_det_sno = new TableRow();
                                        //tr_det_sno.Style.Add("font-size", "8pt");
                                        //tr_det_sno.Style.Add("font-family", "Verdana");
                                        TableCell tc_det_SNo = new TableCell();
                                        iCount += 1;
                                        Literal lit_det_SNo = new Literal();
                                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SNo.BorderWidth = 1;
                                        tc_det_SNo.Controls.Add(lit_det_SNo);
                                        tr_det_sno.Cells.Add(tc_det_SNo);


                                        TableCell tc_det_dr_name = new TableCell();
                                        Literal lit_det_dr_name = new Literal();
                                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Stockist_Name"].ToString();
                                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_name.BorderWidth = 1;
                                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                        tr_det_sno.Cells.Add(tc_det_dr_name);

                                        TableCell tc_det_spec = new TableCell();
                                        Literal lit_det_spec = new Literal();
                                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                                        //tc_det_spec.BorderStyle = BorderStyle.Solid;
                                        //tc_det_spec.BorderWidth = 1;
                                        tc_det_spec.Controls.Add(lit_det_spec);
                                        tr_det_sno.Cells.Add(tc_det_spec);

                                        TableCell tc_det_work = new TableCell();
                                        Literal lit_det_work = new Literal();
                                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_work.Controls.Add(lit_det_work);
                                        tr_det_sno.Cells.Add(tc_det_work);

                                        //TableCell tc_det_dr_VisitTime = new TableCell();
                                        //Literal lit_det_dr_VisitTime = new Literal();
                                        //lit_det_dr_VisitTime.Text = "&nbsp;&nbsp;" + drdoctor["vstTime"].ToString();
                                        //tc_det_dr_VisitTime.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_VisitTime.BorderWidth = 1;
                                        //tc_det_dr_VisitTime.Controls.Add(lit_det_dr_VisitTime);
                                        //tr_det_sno.Cells.Add(tc_det_dr_VisitTime);

                                        //TableCell tc_det_dr_LastUpdate = new TableCell();
                                        //Literal lit_det_dr_LastUpdate = new Literal();
                                        //lit_det_dr_LastUpdate.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                                        //tc_det_dr_LastUpdate.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_LastUpdate.BorderWidth = 1;
                                        //tc_det_dr_LastUpdate.Controls.Add(lit_det_dr_LastUpdate);
                                        //tr_det_sno.Cells.Add(tc_det_dr_LastUpdate);

                                        TableCell tc_det_dr_Place_Worked = new TableCell();
                                        Literal lit_det_dr_Place_Worked = new Literal();
                                        lit_det_dr_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                                        //tc_det_dr_Place_Worked.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Place_Worked.BorderWidth = 1;
                                        //tc_det_dr_Place_Worked.Width = 250;
                                        tc_det_dr_Place_Worked.Controls.Add(lit_det_dr_Place_Worked);
                                        tr_det_sno.Cells.Add(tc_det_dr_Place_Worked);

                                        //TableCell tc_det_dr_Call_Feedback = new TableCell();
                                        //Literal lit_det_dr_Call_Feedback = new Literal();
                                        //lit_det_dr_Call_Feedback.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                                        //tc_det_dr_Call_Feedback.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_Call_Feedback.BorderWidth = 1;
                                        //tc_det_dr_Call_Feedback.Controls.Add(lit_det_dr_Call_Feedback);
                                        //tr_det_sno.Cells.Add(tc_det_dr_Call_Feedback);

                                        TableCell tc_det_LatLog = new TableCell();
                                        HyperLink lit_det_LatLog = new HyperLink();
                                        lit_det_LatLog.Text = drdoctor["lati"].ToString() + " - " + drdoctor["long"].ToString();
                                        sURL = "Location_Finder_1.aspx?sf_Name=" + "&SFCode=" + "/" + sf_code + "/" + "&DivID=" + div_code + " &StrDate=" + "/" + drdoc["Activity_Date"].ToString() + "/&Mode=" + "/" + "S" + "";
                                        lit_det_LatLog.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        lit_det_LatLog.NavigateUrl = "#";
                                        //tc_det_LatLog.BorderStyle = BorderStyle.Solid;
                                        //tc_det_LatLog.BorderWidth = 1;
                                        tc_det_LatLog.Controls.Add(lit_det_LatLog);
                                        tr_det_sno.Cells.Add(tc_det_LatLog);


                                        tbldetailstk.Rows.Add(tr_det_sno);
                                    }
                                }

                                //form1.Controls.Add(tbldetailhos);

                                tc_det_head_main12.Controls.Add(tbldetailstk);
                                tr_det_head_main11.Cells.Add(tc_det_head_main12);
                                tbldetail_main11.Rows.Add(tr_det_head_main11);

                                //form1.Controls.Add(tbldetail_main11);
                                ExportDiv.Controls.Add(tbldetail_main11);


                                if (iCount > 0)
                                {
                                    Table tbl_stk_empty = new Table();
                                    TableRow tr_stk_empty = new TableRow();
                                    TableCell tc_stk_empty = new TableCell();
                                    Literal lit_stk_empty = new Literal();
                                    lit_stk_empty.Text = "<BR>";
                                    tc_stk_empty.Controls.Add(lit_stk_empty);
                                    tr_stk_empty.Cells.Add(tc_stk_empty);
                                    tbl_stk_empty.Rows.Add(tr_stk_empty);
                                    //form1.Controls.Add(tbl_stk_empty);
                                    ExportDiv.Controls.Add(tbl_stk_empty);
                                }

                                //5-Hospitals

                                Table tbldetail_main9 = new Table();
                                tbldetail_main9.BorderStyle = BorderStyle.None;
                                //tbldetail_main9.Width = 1100;
                                tbldetail_main9.Style.Add("width", "95%");
                                TableRow tr_det_head_main9 = new TableRow();
                                TableCell tc_det_head_main9 = new TableCell();
                                //tc_det_head_main9.Width = 100;
                                Literal lit_det_main9 = new Literal();
                                lit_det_main9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_det_head_main9.Controls.Add(lit_det_main9);
                                tr_det_head_main9.Cells.Add(tc_det_head_main9);

                                TableCell tc_det_head_main10 = new TableCell();
                                //tc_det_head_main10.Width = 2000;


                                Table tbldetailhos = new Table();
                                //tbldetailhos.BorderStyle = BorderStyle.Solid;
                                //tbldetailhos.BorderWidth = 1;
                                tbldetailhos.GridLines = GridLines.None;
                                //tbldetailhos.Width = 2000;
                                //tbldetailhos.Style.Add("border-collapse", "collapse");
                                //tbldetailhos.Style.Add("border", "solid 1px Black");
                                tbldetailhos.Attributes.Add("class", "table");

                                dsDocHos.Tables[0].DefaultView.RowFilter = "  Activity_Date= '" + drdoc["Activity_Date"].ToString() + "' ";
                                DataTable dtHos = dsDocHos.Tables[0].DefaultView.ToTable("table1");
                                DataSet dsHos = new DataSet();
                                dsDocStk.Merge(dtHos);
                                dsHos.Merge(dtHos);

                                // dsdoc = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                                iCount = 0;
                                if (dsHos.Tables[0].Rows.Count > 0)
                                {
                                    TableRow tr_det_head = new TableRow();
                                    //tr_det_head.Style.Add("font-size", "8pt");
                                    //tr_det_head.Style.Add("font-family", "Verdana");
                                    TableCell tc_det_head_SNo = new TableCell();
                                    //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_SNo.BorderWidth = 1;
                                    Literal lit_det_head_SNo = new Literal();
                                    lit_det_head_SNo.Text = "#";
                                    tc_det_head_SNo.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                    tr_det_head.Cells.Add(tc_det_head_SNo);

                                    TableCell tc_det_head_doc = new TableCell();
                                    //tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_doc.BorderWidth = 1;
                                    Literal lit_det_head_doc = new Literal();
                                    lit_det_head_doc.Text = "Hospital Name";
                                    tc_det_head_doc.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                    tr_det_head.Cells.Add(tc_det_head_doc);

                                    TableCell tc_det_head_ww = new TableCell();
                                    //tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_ww.BorderWidth = 1;
                                    Literal lit_det_head_ww = new Literal();
                                    lit_det_head_ww.Text = "Worked With";
                                    tc_det_head_ww.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                    tr_det_head.Cells.Add(tc_det_head_ww);

                                    TableCell tc_det_head_catg = new TableCell();
                                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                                    //tc_det_head_catg.BorderWidth = 1;
                                    Literal lit_det_head_catg = new Literal();
                                    lit_det_head_catg.Text = "POB";
                                    tc_det_head_catg.Attributes.Add("Class", "stickyFirstRow");
                                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                                    tr_det_head.Cells.Add(tc_det_head_catg);


                                    tbldetailhos.Rows.Add(tr_det_head);

                                    iCount = 0;
                                    foreach (DataRow drdoctor in dsHos.Tables[0].Rows)
                                    {
                                        TableRow tr_det_sno = new TableRow();
                                        //tr_det_sno.Style.Add("font-size", "8pt");
                                        //tr_det_sno.Style.Add("font-family", "Verdana");
                                        TableCell tc_det_SNo = new TableCell();
                                        iCount += 1;
                                        Literal lit_det_SNo = new Literal();
                                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                        //tc_det_SNo.BorderWidth = 1;
                                        tc_det_SNo.Controls.Add(lit_det_SNo);
                                        tr_det_sno.Cells.Add(tc_det_SNo);


                                        TableCell tc_det_dr_name = new TableCell();
                                        Literal lit_det_dr_name = new Literal();
                                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Hospital_Name"].ToString();
                                        //tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                        //tc_det_dr_name.BorderWidth = 1;
                                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                        tr_det_sno.Cells.Add(tc_det_dr_name);


                                        TableCell tc_det_work = new TableCell();
                                        Literal lit_det_work = new Literal();
                                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                                        //tc_det_work.BorderStyle = BorderStyle.Solid;
                                        //tc_det_work.BorderWidth = 1;
                                        tc_det_work.Controls.Add(lit_det_work);
                                        tr_det_sno.Cells.Add(tc_det_work);


                                        TableCell tc_det_spec = new TableCell();
                                        Literal lit_det_spec = new Literal();
                                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                                        //tc_det_spec.BorderStyle = BorderStyle.Solid;
                                        //tc_det_spec.BorderWidth = 1;
                                        tc_det_spec.Controls.Add(lit_det_spec);
                                        tr_det_sno.Cells.Add(tc_det_spec);

                                        tbldetailhos.Rows.Add(tr_det_sno);
                                    }
                                }

                                //form1.Controls.Add(tbldetailhos);

                                tc_det_head_main10.Controls.Add(tbldetailhos);
                                tr_det_head_main9.Cells.Add(tc_det_head_main10);
                                tbldetail_main9.Rows.Add(tr_det_head_main9);

                                //form1.Controls.Add(tbldetail_main9);
                                ExportDiv.Controls.Add(tbldetail_main9);






                                if (iCount > 0)
                                {
                                    Table tbl_hosp_empty = new Table();
                                    TableRow tr_hosp_empty = new TableRow();
                                    TableCell tc_hosp_empty = new TableCell();
                                    Literal lit_hosp_empty = new Literal();
                                    lit_hosp_empty.Text = "<BR>";
                                    tc_hosp_empty.Controls.Add(lit_hosp_empty);
                                    tr_hosp_empty.Cells.Add(tc_hosp_empty);
                                    tbl_hosp_empty.Rows.Add(tr_hosp_empty);
                                    //form1.Controls.Add(tbl_hosp_empty);
                                    ExportDiv.Controls.Add(tbl_hosp_empty);
                                }

                                Table tbl_line = new Table();
                                tbl_line.BorderStyle = BorderStyle.None;
                                //tbl_line.Width = 1000;
                                tbl_line.Style.Add("width", "90%");
                                tbl_line.Style.Add("border-collapse", "collapse");
                                tbl_line.Style.Add("border-top", "none");
                                tbl_line.Style.Add("border-right", "none");
                                //tbl_line.Style.Add("margin-left", "100px");
                                tbl_line.Style.Add("border-bottom ", "solid 2px #DCE2E8");

                                TableRow tr_line = new TableRow();

                                TableCell tc_line0 = new TableCell();
                                tc_line0.Width = 100;
                                Literal lit_line0 = new Literal();
                                lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                tc_line0.Controls.Add(lit_line0);
                                tr_line.Cells.Add(tc_line0);

                                TableCell tc_line = new TableCell();
                                tc_line.Width = 1000;
                                Literal lit_line = new Literal();
                                // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                                tc_line.Controls.Add(lit_line);
                                tr_line.Cells.Add(tc_line);
                                tbl_line.Rows.Add(tr_line);
                                //form1.Controls.Add(tbl_line);
                                ExportDiv.Controls.Add(tbl_line);

                            }
                        }
                    }
                    else
                    {
                        //lblHead.Visible = true;
                        //lblHead.Style.Add("margin-top", "80px");
                        //lblHead.Text = "No Record Found";

                        //pnlbutton.Visible = false;
                        ExportButtonForNoData();

                        Table tbldetail_mainHoliday = new Table();
                        tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                        tbldetail_mainHoliday.Width = 1100;
                        TableRow tr_det_head_mainHoliday = new TableRow();
                        TableCell tc_det_head_mainHolday = new TableCell();
                        //tc_det_head_mainHolday.Width = 100;
                        Literal lit_det_mainHoliday = new Literal();
                        lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        tbldetail_mainHoliday.Style.Add("margin-top", "110px");
                        tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                        tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                        TableCell tc_det_head_mainHoliday = new TableCell();
                        //tc_det_head_mainHoliday.Width = 800;

                        Table tbldetailHoliday = new Table();
                        tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                        tbldetailHoliday.BorderWidth = 1;
                        tbldetailHoliday.GridLines = GridLines.Both;
                        tbldetailHoliday.Width = 1000;
                        tbldetailHoliday.Style.Add("border-collapse", "collapse");
                        tbldetailHoliday.Style.Add("border", "solid 1px Black");

                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "No Record Found";
                        //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.Attributes.Add("Class", "no-result-area");
                        tc_det_SNo.Style.Add("font-size", "18px");

                        tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_SNo.BorderWidth = 1;
                        //tc_det_SNo.BorderStyle = BorderStyle.None;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        tbldetailHoliday.Rows.Add(tr_det_sno);

                        tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                        tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                        tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                        //form1.Controls.Add(tbldetail_mainHoliday);
                        ExportDiv.Controls.Add(tbldetail_mainHoliday);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

}