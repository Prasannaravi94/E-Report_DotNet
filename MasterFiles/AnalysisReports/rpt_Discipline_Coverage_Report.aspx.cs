//
#region Assembly
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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using DBase_EReport;
using System.Data.SqlClient;
#endregion
//
#region MIS_Reports_Visit_Details_Basedonfield_Level1
public partial class MasterFiles_AnalysisReports_rpt_Discipline_Discipline_Coverage_Report : System.Web.UI.Page
{
    //
    #region variables
    DataSet dsDoctor = null;
    DataSet dsDCR = null;
    DataSet dsmgrsf = new DataSet();
    DataTable dtMnYr = new DataTable();
    SalesForce sf = new SalesForce();
    string strmode = string.Empty;
       string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();

    int tot_miss = 0, TempValue = 0, iTtlMnth = 0, iTtl_Visit = 0;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblCatg = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string sCurrentDate = string.Empty;
    string sType = string.Empty, sLvl="";
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    string Sf_Code = string.Empty;
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Sf_Code = Session["sf_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        strmode = Request.QueryString["Vacant"].ToString();
        ViewState["div_code"] = div_code;

        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        
      
        lblHead.Text = "Discipline - Coverage Report Between  - " + strFrmMonth.Substring(0,3) + " " + FYear + "  To  " + strToMonth.Substring(0,3) + " " + TYear;
        LblForceName.Text = "Field Force Name : " + sf_name;
        screen_name = "rpt_Discipline_Coverage_Report";
        if (!Page.IsPostBack)
        {
            FillCatg();
        }
    }
    //
    #endregion
    //
    #region getMonthName
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
    #endregion
    //
    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    #endregion
    //
    #region btnExcel_Click
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
    #endregion
    //
    #region btnPDF_Click
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
    #endregion
    //
    #region PrintWebControl
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
    //
    #endregion
    //
    #region FillCatg
    private void FillCatg()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        iTtlMnth = months;

        int iMn = 0, iYr = 0;        
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
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        string sProc_Name = "";

        sProc_Name = "Discipline_Coverage_Final";
       
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Vacant", strmode);
        cmd.CommandTimeout = 650;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        //dsts.Tables[0].Columns.RemoveAt(10);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["sf_emp_id"].SetOrdinal(4);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
        GrdFixationMode.DataSource = dsts;
        GrdFixationMode.DataBind();
    }
    #endregion
    //
    #region GrdFixationMode_RowCreated
    protected void GrdFixationMode_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #endregion
            //
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#008080", true);
           
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Design", "#008080", true);
            
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Emp Code", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "DOJ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Last DCR Recvd", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Month", "#008080", true);
            //
            div_code = ViewState["div_code"].ToString();
            string strQry = "";
         

            strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
            " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
            " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
            " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
            " ORDER BY c.Doc_Cat_Code";
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            
            //int iMin_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][0].ToString());
            //int iMax_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][1].ToString());
            //iTtl_Visit = (iMax_Visit * 3) + 3;
            //for (int i = 0; i <= iMax_Visit; i++)
            //{
            //    if (i==0)
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, "Total", "#008080", false);
            //    else
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, "V"+i.ToString(), "#008080", false);
            //    //
            //    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "List Dr", "#008080", false);
            //    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Met", "#008080", false);
            //    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Seen", "#008080", false);
            //}            
            //
            AddMergedCells(objgridviewrow, objtablecell, 0, 6, "Working Days Details", "#008080", true);
           
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Days in month", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Field Days", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Non Field Days", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Worked Days", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Leave", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Holiday", "#008080", false);
            //
            AddMergedCells(objgridviewrow, objtablecell, 0, Convert.ToInt32(dsDoctor.Tables[0].Rows.Count) + 1, "Coverage %", "#008080", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "DOCTOR Coverage %", "#008080", false);
            for (int j = 0; j < dsDoctor.Tables[0].Rows.Count; j++)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() + " Coverage %", "#008080", false);

       
            }
            //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Non Reported Days", "#008080", false);
            //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "T P Deviation days", "#008080", false);
            //


            AddMergedCells(objgridviewrow, objtablecell, 0, 2, "Call Average", "#008080", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "DOCTOR Call Average", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "CHEMIST Call Average", "#008080", false);

            for (int j = 0; j < dsDoctor.Tables[0].Rows.Count; j++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 11, dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Customer " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Customer " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() + " Calls", "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Once " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Once " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() +" %", "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() + " %", "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "More than Twice " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "More than Twice " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() + " %", "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed Customer " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed Customer " + dsDoctor.Tables[0].Rows[j]["ShortName"].ToString() + " %", "#008080", false);

            }
            AddMergedCells(objgridviewrow, objtablecell, 0, 11, "Total", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Customer", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Customer", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Calls", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Met Once", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Met Once %", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice %", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "More than Twice", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "More than Twice %", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed Customer ", "#008080", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed Customer %", "#008080", false);

            //
            #region Absolute values
            /*
            int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            string strQry = "";
            int iMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            if (iMode==1)
            {
                strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
                 " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                 " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                 " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                 " ORDER BY c.Doc_Cat_SName";
            }
            else if (iMode==2)
            {
                strQry = " SELECT c.Doc_Special_Code,c.Doc_Special_SName AS ShortName,c.Doc_Special_Name,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_special_code) from Mas_ListedDr d where d.Doc_special_code = c.Doc_Special_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Speciality c WHERE c.Doc_Special_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_Special_SName";
            }
            else if (iMode==3)
            {
                strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName AS ShortName,c.Doc_ClsName,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cat_Count" +
                     " FROM  Mas_Doc_Class c WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_ClsCode";
            }            
            
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();
            */

            /*
            for (int i = 0; i <= months; i++)
            {
                //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                int iColSpan = 0;
                iLstColCnt.Add(3);
                for (int j = 0, k=0; j < dsDoctor.Tables[0].Rows.Count; j++)
                {
                    int iCnt = 0;
                    if (k == 0)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, "COVERAGE", "#008080", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Ttl Drs", "#008080", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Drs Met", "#008080", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#008080", false);
                        iColSpan += 3;
                        k++;
                    }
                    //
                    iColSpan += 3;
                    iCnt = 3;
                    iLstColCnt.Add(iCnt);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#008080", false);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Ttl Drs", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Drs Met", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#008080", false);
                }
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#008080", true);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }*/
            #endregion
            //
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            //
            #endregion
            //
        }
    }
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "#FFFFFF");
        //objtablecell.Style.Add("border-color", "#000000");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, Sf_Code);

        if (objtablecell.Text == "Emp Code" || objtablecell.Text == "DOJ" || objtablecell.Text == "Last DCR Recvd")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, Sf_Code, true, 3);
        }

        dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, objtablecell.Text, Sf_Code);
        if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
        {
            if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
            {
                //objtablecell.Visible = false;
                objtablecell.Style.Add("display", "none");
            }
        }
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
    #endregion
    //    
    #region GrdFixationMode_RowDataBound
    protected void GrdFixationMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations            
            //int iEnd = e.Row.Cells.Count;
            //e.Row.Cells[iTtl_Visit + ((iTtl_Visit - 3) / 3) + 13].Text = (Convert.ToInt32(e.Row.Cells[8].Text == "&nbsp;" ? "0" : e.Row.Cells[8].Text) - Convert.ToInt32(e.Row.Cells[iEnd - 1].Text == "&nbsp;" ? "0" : e.Row.Cells[iEnd - 1].Text)).ToString();
            //e.Row.Cells[iEnd-1].Visible = false;
            //e.Row.Cells[iTtl_Visit + ((iTtl_Visit - 3) / 3) + 13].ForeColor = System.Drawing.Color.Red;
            //
            for (int i = 7, j = 0; i < e.Row.Cells.Count; i++)
            {                           
                e.Row.Cells[i].Text = (e.Row.Cells[i].Text == "0") ? "" : e.Row.Cells[i].Text;
            }
            //
            try
            {
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][6].ToString()));                    
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            //
            #endregion
            //
            if (e.Row.Cells[7].Text != "&nbsp;")
            {
                var result = e.Row.Cells[7].Text.Aggregate("", (res, c) => res + ((byte)c).ToString("X"));
                if (Convert.ToInt32(result) > 40)
                {
                    e.Row.Cells[7].Text = (Convert.ToInt32(result) - 31).ToString();
                }
                e.Row.Cells[7].Text = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[7].Text) - 1]["MNTH"].ToString()).Substring(0, 3) +
                    " - " + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[7].Text) - 1]["YR"].ToString();
                e.Row.Cells[2].Attributes.Add("align", "left");
                e.Row.Cells[7].Attributes.Add("align", "left");
                e.Row.Cells[7].Attributes.Add("style", "color:#0000CD;");
                e.Row.Cells[7].Font.Bold = true;
                //e.Row.Cells[1].Wrap = false;
                //e.Row.Cells[2].Wrap = false;
                e.Row.Cells[3].Wrap = false;
                e.Row.Cells[4].Wrap = false;
                e.Row.Cells[5].Wrap = false;
                e.Row.Cells[6].Wrap = false;
                e.Row.Cells[7].Wrap = false;
            }
            e.Row.Cells[2].Attributes.Add("align", "left");
            e.Row.Cells[3].Attributes.Add("align", "left");
            e.Row.Cells[4].Attributes.Add("align", "left");
            e.Row.Cells[0].Attributes.Add("class", "stickyfirstdata");
            e.Row.Cells[1].Attributes.Add("class", "stickySeconddata");
            e.Row.Cells[2].Attributes.Add("class", "stickyThirddata");
        }
        //
        #region merge columns
        int RowSpan = 2;
        for (int i = GrdFixationMode.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = GrdFixationMode.Rows[i];
            GridViewRow prevRow = GrdFixationMode.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;
                currRow.Cells[2].RowSpan = RowSpan;
                prevRow.Cells[2].Visible = false;
                currRow.Cells[3].RowSpan = RowSpan;
                prevRow.Cells[3].Visible = false;
                currRow.Cells[4].RowSpan = RowSpan;
                prevRow.Cells[4].Visible = false;
                currRow.Cells[5].RowSpan = RowSpan;
                prevRow.Cells[5].Visible = false;
                currRow.Cells[6].RowSpan = RowSpan;
                prevRow.Cells[6].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
        #endregion
        //

        Chemist chem = new Chemist();

        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, Sf_Code);
        if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("column_name"),
                             Ch_Code = data.Field<string>("column_name")
                         };
            var listOfGrades = result.ToList();
            cblGridColumnList.Visible = true;
            cblGridColumnList.DataSource = listOfGrades;
            cblGridColumnList.DataTextField = "Ch_Name";
            cblGridColumnList.DataValueField = "Ch_Code";
            cblGridColumnList.DataBind();

            string headerText = string.Empty;

            for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
            {
                headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

                System.Web.UI.WebControls.ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                if (ddl != null)
                {
                    if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                    }
                    else
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                    }
                }

                if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                {

                    int j = i + 4;

                    e.Row.Cells[j].Visible = false;
                }
            }
        }
    }
    #endregion
    //
    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && Sf_Code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, Sf_Code);
        }

        Response.Redirect(Request.RawUrl);
    }
}
//
#endregion