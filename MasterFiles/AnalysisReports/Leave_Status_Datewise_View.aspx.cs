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
using System.Data.SqlClient;
using DBase_EReport;
//using ClosedXML.Excel;

#endregion
//


public partial class MasterFiles_AnalysisReports_Leave_Status_Datewise_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoc = null;
    DataSet dsDcrCount = new DataSet();

    int fldwrk_total = 0;
    string strDate = string.Empty;
    string iPendingDate = string.Empty;
    int doctor_total = 0;
    int iPendingCount = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_DcrCount = string.Empty;
    string strLastDCRDate = string.Empty;
    string tot_DcrPendingDate = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    int StrVacant = 0;
    int Dr_cnt = 0;
    string strFieledForceName = string.Empty;
    DataSet dsUserList = new DataSet();
    DataTable dtrowClr = new DataTable();
    string FDate = string.Empty;
    string TDate = string.Empty;
    string Vacant = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FDate = Request.QueryString["FDate"].ToString();
        TDate = Request.QueryString["TDate"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        Vacant = Request.QueryString["Vacant"].ToString();

        string sHeading = "";

        lblHead.Text = "Attendance Status For the Period of &nbsp; " + Convert.ToDateTime(FDate).ToString("dd MMMM yyyy") + " &nbsp; to &nbsp;" + Convert.ToDateTime(TDate).ToString("dd MMMM yyyy");
        LblForceName.Text = "Field Force Name : " + strFieledForceName;

        if (!Page.IsPostBack)
        {
            FillCatg();
        }
    }

    #region FillCatg
    private void FillCatg()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        //sProc_Name = "sp_Leave_Status_View_New";
        sProc_Name = "sp_Leave_Status_View_New_WithVac";
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
        cmd.Parameters.AddWithValue("@FDate", FDate);
        cmd.Parameters.AddWithValue("@TDate", TDate);
        cmd.Parameters.AddWithValue("@Vacant", Vacant);

        cmd.CommandTimeout = 600;
        cmd.CommandTimeout = 150;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        
        da.Fill(dsts);
        dsts.Tables[0].Columns.RemoveAt(11);
        con.Close();
        if (dsts.Tables[0].Rows.Count > 0)
        {
            GvDcrCount.DataSource = dsts;
            GvDcrCount.DataBind();
        }
        else
        {
            GvDcrCount.DataSource = dsts;
            GvDcrCount.DataBind();
        }
    }
    #endregion

 
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    //protected void btnPDF_Click(object sender, EventArgs e)
    //{
    //    string strFileName = "rptTPView";
    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    HtmlForm frm = new HtmlForm();
    //    pnlContents.Parent.Controls.Add(frm);
    //    frm.Attributes["runat"] = "server";
    //    frm.Controls.Add(pnlContents);
    //    frm.RenderControl(hw);
    //    StringReader sr = new StringReader(sw.ToString());
    //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
    //    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
    //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //    pdfDoc.Open();
    //    htmlparser.Parse(sr);
    //    pdfDoc.Close();
    //    Response.Write(pdfDoc);
    //    Response.End();
    //}
   

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

        string attachment = "attachment; filename=download.xls";
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


    protected void GvDcrCount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DCR dcs = new DCR();
            SalesForce sf = new SalesForce();
            fldwrk_total = 0;
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desig_color")));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                string docrow_count = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }
  





}



