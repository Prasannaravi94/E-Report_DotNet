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
using System.Drawing;

public partial class MasterFiles_AnalysisReports_rpt_Coverage_New : System.Web.UI.Page
{
    DataSet dsDoctor = null;
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
    DataSet dsDocMet = null;
    DataSet dsCov = null;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataSet dsUndoc = new DataSet();
    DataSet dsFw = new DataSet();
    DataSet dsField = new DataSet();
    DataSet dsNoFW = new DataSet();
    DataSet dsleave = new DataSet();
    DataSet dsCall = new DataSet();
    DataSet dsworkday = new DataSet();
    DataSet dsJwMet = new DataSet();
    DataSet dsJwSeen = new DataSet();
    DataSet dsdocseen = new DataSet();
    SalesForce dcrdoc = new SalesForce();
        DataSet dschem = new DataSet();
    DateTime dtCurrent;
    DateTime dtCurrent1;
    int imissed_dr = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();

            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Coverage Analysis - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillSalesForce();
        }
    }


    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int DocSeen = 0;
        int Field = 0;
        int met = 0;
        int chemSeen = 0;
        double Callavg= 0.0;
        string con_Fare = string.Empty;
    
           decimal RoundLstCallAvgJW = new decimal();
                double dblaverage_JW = 0.00;
                decimal RoundLstmet = new decimal();
                double dblaverage_met = 0.00;
                Label lblsftype = (Label)e.Row.FindControl("lblsftype");
                Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
        Label lblJW_Days = (Label)e.Row.FindControl("lblJW_Days");
        Label lblJW_Met = (Label)e.Row.FindControl("lblJW_Met");
        Label lblJW_Seen = (Label)e.Row.FindControl("lblJW_Seen");
        Label lblJW_Avg = (Label)e.Row.FindControl("lblJW_Avg");
        Label lbldrCount = (Label)e.Row.FindControl("lbldrCount");
        Label lblCoverage = (Label)e.Row.FindControl("lblCoverage");
        Label lblmissed = (Label)e.Row.FindControl("lblmissed");
        Label lblRep = (Label)e.Row.FindControl("lblRep");
        Label lblRepCov = (Label)e.Row.FindControl("lblRepCov");

        Label lblchemist = (Label)e.Row.FindControl("lblchemist");
        Label lblChemAvg = (Label)e.Row.FindControl("lblChemAvg");
        
        decimal RoundLstCallAvg3 = new decimal();
        double dblaverage3 = 0.00;


        decimal RoundChemavg = new decimal();
        double dblavgchem = 0.00;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCall = (Label)e.Row.FindControl("lblCallAvg");
            
          //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Desig_color")));

            DocSeen = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "seen"));
            Field = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Fieldday"));
            met = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "met"));
            chemSeen = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Che_count"));
                if (Convert.ToDecimal(Field) != 0)
                {
                    dblaverage_JW = Convert.ToDouble((Convert.ToDecimal(DocSeen) / Convert.ToDecimal(Field)));
                }
                RoundLstCallAvgJW = Math.Round((decimal)dblaverage_JW, 2);
                lblCall.Text = "" + RoundLstCallAvgJW;
                if (Convert.ToDecimal(Field) != 0)
                {
                    dblavgchem = Convert.ToDouble((Convert.ToDecimal(chemSeen) / Convert.ToDecimal(Field)));
                }
                RoundChemavg = Math.Round((decimal)dblavgchem, 2);
                lblChemAvg.Text = "" + RoundChemavg;
                if (lblsftype.Text == "1")
                {
                    lblJW_Days.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwdaysmr"));
                    lblJW_Met.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwmetmr"));
                    lblJW_Seen.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwseenmr"));

                    if (Convert.ToDecimal(lblJW_Days.Text) != 0)
                    {
                        dblaverage_met = Convert.ToDouble((Convert.ToDecimal(lblJW_Seen.Text) / Convert.ToDecimal(lblJW_Days.Text)));
                    }
                

                }
                else
                {
                    e.Row.BackColor = Color.LightBlue;
                    lblJW_Days.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwdaysmgr"));
                    lblJW_Met.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwmetmgr"));
                    lblJW_Seen.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Jwseenmgr"));

                    if (Convert.ToDecimal(lblJW_Days.Text) != 0)
                    {
                        dblaverage_met = Convert.ToDouble((Convert.ToDecimal(lblJW_Seen.Text) / Convert.ToDecimal(lblJW_Days.Text)));
                    }
                    RoundLstmet = Math.Round((decimal)dblaverage_met, 2);
                    lblJW_Avg.Text = "" + RoundLstmet;
                  //  lbldrCount.Text = " - ";
                    lblCoverage.Text = " - ";
                    lblmissed.Text = " - ";
                }
                int cmonthd = Convert.ToInt32(FMonth);
                int cyeard = Convert.ToInt32(FYear);


                if (cmonthd == 12)
                {
                    sCurrentDate = "01-01-" + (cyeard + 1);
                }
                else
                {
                    sCurrentDate = (cmonthd + 1) + "-01-" + cyeard;
                    //sCurrentDate = cmonth  + "-01-" + cyear;
                }
                dtCurrent1 = Convert.ToDateTime(sCurrentDate);
                DCR dcrdoc = new DCR();
                dsdoc = dcrdoc.Get_LstDr_Cnt_sf(div_code, lblSF_Code.Text, cmonthd, cyeard, dtCurrent1);
                if (dsdoc.Tables[0].Rows.Count > 0)
                    lbldrCount.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                RoundLstmet = Math.Round((decimal)dblaverage_met, 2);
                lblJW_Avg.Text = "" + RoundLstmet;
              //  lbldrCount.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "drcnt"));
                if (Convert.ToDecimal(lbldrCount.Text) != 0)
                    //   imissed_dr = Convert.ToInt16(tot_fldwrkodc) / Convert.ToInt16(tot_fldwrk);
                    dblaverage3 = Convert.ToDouble((Convert.ToDecimal(met) / Convert.ToDecimal(lbldrCount.Text))) * 100;
                RoundLstCallAvg3 = Math.Round((decimal)dblaverage3, 2);
                lblCoverage.Text = "" + RoundLstCallAvg3;
                if (lblsftype.Text == "1")
                {
                    if (lbldrCount.Text != "0")
                    {
                        imissed_dr = Convert.ToInt16(lbldrCount.Text) - Convert.ToInt16(met);
                    }
                    lblmissed.Text = "" + imissed_dr;
                }
                else
                {
                    lblmissed.Text = " - ";
                }
                if (Convert.ToDecimal(met) != 0)
                    //   imissed_dr = Convert.ToInt16(tot_fldwrkodc) / Convert.ToInt16(tot_fldwrk);
                    dblaverage3 = Convert.ToDouble((Convert.ToDecimal(lblRep.Text) / Convert.ToDecimal(met))) * 100;
                RoundLstCallAvgJW = Math.Round((decimal)dblaverage3, 2);
                lblRepCov.Text = Convert.ToString(RoundLstCallAvgJW);
            }
     
        
    }
    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Call Details", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Attendance", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Summary", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Joint Work", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Repeated Calls", "#0097AC", true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Master List Doctors", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Doctors Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Coverage (%)", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed Drs Missed", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "UnListed Drs Met", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Worked", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Field", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Non Field", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days On Leave", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Doctors Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chemist Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Calls Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Coverage (%)", "#0097AC", false);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }


    private void FillSalesForce()
    {
        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();

        Product prd = new Product();


        int cmonthdoc = Convert.ToInt32(FMonth);
        int cyeardoc = Convert.ToInt32(FYear);

        int cmonthday = Convert.ToInt32(FMonth);
        int cyearday = Convert.ToInt32(FYear);

        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

       
        if (cmonth == 12)
        {
            sCurrentDate = "01-01-" + (cyear + 1);
        }
        else
        {
            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
            //sCurrentDate = cmonth  + "-01-" + cyear;
        }
        dtCurrent = Convert.ToDateTime(sCurrentDate);
     
        //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{

            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            DCR dcr = new DCR();
           
            // Check if the manager has a team
          
             
                //dsSalesForce = sf.sp_Get_Exp_Consolidate_View(sf_code, div_code,Convert.ToString(iMonth),Convert.ToString(iYear));
                dsSalesForce = dcr.DCR_Doc_Met_Cov(sf_code, div_code, cmonthdoc, cyeardoc);
                dsField = dcr.DCR_Workdays_SF(sf_code, div_code, cmonthday, cyearday);
                dsdocseen = dcr.DCR_doc_summary(sf_code, div_code, cmonthday, cyearday);
                dsworkday = dcr.JW_Workdays_Coverage(sf_code, div_code, cmonthday, cyearday);
                dsJwMet = dcr.JW_Work_met_Coverage(sf_code, div_code, cmonthday, cyearday);
                dsJwSeen = dcr.JW_Work_seen_Coverage(sf_code, div_code, cmonthday, cyearday);
             // dsdoc = dcr.Doc_Met_Cnt_Coverage(sf_code, div_code, cmonth, cyear, dtCurrent);
                dsUndoc = dcr.Doc_Unlst_Met_Cnt(sf_code, div_code, cmonthday, cyearday);
                dsCov = dcr.Doc_Met_Cnt_Repeatedcalls(sf_code, div_code, cmonthday, cyearday);
                dschem = dcr.Get_Chemist_Met(sf_code, div_code, cmonthday, cyearday);
           
        dsSalesForce.Tables[0].Columns.Add("seen");
        dsSalesForce.Tables[0].Columns.Add("Field");
        dsSalesForce.Tables[0].Columns.Add("Fieldday");

        dsSalesForce.Tables[0].Columns.Add("NoField");
        dsSalesForce.Tables[0].Columns.Add("Leave");
        dsSalesForce.Tables[0].Columns.Add("Jwdaysmr");
        dsSalesForce.Tables[0].Columns.Add("Jwdaysmgr");
        dsSalesForce.Tables[0].Columns.Add("Jwmetmr");
        dsSalesForce.Tables[0].Columns.Add("Jwmetmgr");
        dsSalesForce.Tables[0].Columns.Add("Jwseenmr");
        dsSalesForce.Tables[0].Columns.Add("Jwseenmgr");
       // dsSalesForce.Tables[0].Columns.Add("drcnt");
        dsSalesForce.Tables[0].Columns.Add("Unlstdoc");
        dsSalesForce.Tables[0].Columns.Add("repcalls1");
        dsSalesForce.Tables[0].Columns.Add("Che_count");
        for (int i = 0; i < dsSalesForce.Tables[0].Rows.Count; i++)
        {
            //dsSalesForce.Tables[0].Rows[i]["Date1"] = dsSalesPrevious.Tables[0].Rows[i]["date1"].ToString();
            dsSalesForce.Tables[0].Rows[i]["seen"] = dsdocseen.Tables[0].Rows[i]["seen"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Field"] = dsField.Tables[0].Rows[i]["Field"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Fieldday"] = dsField.Tables[0].Rows[i]["Fieldday"].ToString();
            dsSalesForce.Tables[0].Rows[i]["NoField"] = dsField.Tables[0].Rows[i]["NoField"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Leave"] = dsField.Tables[0].Rows[i]["Leave"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwdaysmr"] = dsworkday.Tables[0].Rows[i]["Jwdaysmr"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwdaysmgr"] = dsworkday.Tables[0].Rows[i]["Jwdaysmgr"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwmetmr"] = dsJwMet.Tables[0].Rows[i]["Jwmetmr"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwmetmgr"] = dsJwMet.Tables[0].Rows[i]["Jwmetmgr"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwseenmr"] = dsJwSeen.Tables[0].Rows[i]["Jwseenmr"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Jwseenmgr"] = dsJwSeen.Tables[0].Rows[i]["Jwseenmgr"].ToString();
          //  dsSalesForce.Tables[0].Rows[i]["drcnt"] = dsdoc.Tables[0].Rows[i]["drcnt"].ToString();
            dsSalesForce.Tables[0].Rows[i]["Unlstdoc"] = dsUndoc.Tables[0].Rows[i]["Unlstdoc"].ToString();
            dsSalesForce.Tables[0].Rows[i]["repcalls1"] = dsCov.Tables[0].Rows[i]["repcalls1"].ToString();
             dsSalesForce.Tables[0].Rows[i]["Che_count"] = dschem.Tables[0].Rows[i]["Che_count"].ToString();
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

        
          //  btnExcel.Visible = true;
            grdSalesForce_cov.DataSource = dsSalesForce.Tables[0].DefaultView;
            grdSalesForce_cov.DataBind();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected string GetString(string url)
    {
        WebClient wc = new WebClient();
        Stream resStream = wc.OpenRead(url);
        StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
        string ContentHtml = sr.ReadToEnd();
        return ContentHtml;
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string attachment = "attachment; filename=Export.xls";
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

}