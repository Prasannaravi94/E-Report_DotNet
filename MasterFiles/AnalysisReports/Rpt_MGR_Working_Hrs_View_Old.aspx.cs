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

using System.Collections;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_AnalysisReports_Rpt_MGR_Working_Hrs_View : System.Web.UI.Page
{
    //Added By Preethi
    DataSet dsDoctor = null;
    DataSet dsdcr = null;
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
    string Join_Date = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string tot_docSeen = "";
    string tot_Mor = "";
    string tot_Eve = "";
    string tot_Both = "";
    string tot_fldwrkDays = "";
    string strFieledForceName = string.Empty;
    string mode = string.Empty;
    string Sf_CodeMGR = string.Empty;
    DataTable dtrowClr = new DataTable();
    DataTable dtrowClr2 = new DataTable();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int seen = 0;
    int Fw = 0;

    int Tot_Count = 0;
    ArrayList Values = new ArrayList();

    DataSet dsts = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();

            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();

            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            lblHead.Text = "Fieldforce Working Hours for - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "FieldForce Name : " + strFieledForceName;

            FillReport();

        }
    }




    private void FillReport()
    {      
         
        int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);

        string sProc_Name = "";

        sProc_Name = "MGR_Working_Hrs_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@FMonth", FMonth);
        cmd.Parameters.AddWithValue("@FYear", FYear);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Days", days);
         
        cmd.CommandTimeout = 600;       
        SqlDataAdapter da = new SqlDataAdapter(cmd);      
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("Sf_code");
        dsts.Tables[0].Columns.Remove("Desig_Color");
        dsts.Tables[0].Columns.Remove("Sf_code1");
        //dsts.Tables[0].Columns.Remove("Sf_code1");

        //dsts.Tables[0].Columns.Remove("sf_tp_active_Dt");
        //dsts.Tables[0].Columns.Remove("UserName");
        //dsts.Tables[0].Columns.Remove("Desig_Color");
        //dsts.Tables[0].Columns.Remove("Reporting_SF");
        //dsts.Tables[0].Columns.Remove("Last_DCR_Date");
        //dsts.Tables[0].Columns.Remove("sf_Type");
        //dsts.Tables[0].Columns.Remove("lvl");
        //dsts.Tables[0].Columns.Remove("Sortid");
        dsts.Tables[0].Columns.Remove("Cal_Sno");
        ////dsts.Tables[0].Columns.Remove("Territory_Cat");
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();

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


    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            HyperLink hlink = new HyperLink();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Employee id", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Joining Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "Call Status" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "#0097AC", true);

            int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
            for (int i = 1; i <= days; i++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 1, 0, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "#0097AC", true);
            }
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 10;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

 
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            
            //
            #region Calculations
            //e.Row.Cells[0].Text = (indx + 1).ToString();


            int RowSpan = 2;
            
            for (int i = GrdFixation.Rows.Count - 2; i >= 0; i--)
            {
                string a = GrdFixation.Rows[indx - 1].Cells[3].Text;

                GridViewRow currRow = GrdFixation.Rows[i];
                GridViewRow prevRow = GrdFixation.Rows[i + 1];
                if (currRow.Cells[1].Text == prevRow.Cells[1].Text)
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
                    RowSpan++;
                }
                else
                {
                    RowSpan = 2;
                }
            }
             if ((dtrowClr.Rows.Count - 1) == indx)
            {
                for (int n = 0; n <= e.Row.Cells.Count - 1; n++)
                {
                    e.Row.Cells[n].Visible = false;
                }
             }
             e.Row.Cells[6].Attributes.Add("style", "font-weight:bold;");
             //e.Row.Cells[n].Style.Add("font-size", "12pt");

             e.Row.Cells[6].Style.Add("color", "#3D59AB");
             e.Row.Cells[6].Style.Add("border-color", "black");
              
            e.Row.Cells[0].Attributes.Add("align", "center");
            e.Row.Cells[8].Attributes.Add("align", "center");
            e.Row.Cells[7].Attributes.Add("align", "center");

            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            #endregion
        }
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

   
   
}