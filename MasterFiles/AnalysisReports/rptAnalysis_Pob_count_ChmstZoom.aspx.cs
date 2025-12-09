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
public partial class MasterFiles_AnalysisReports_rptAnalysis_Pob_count_ChmstZoom : System.Web.UI.Page  
{
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
            //div_code = Request.QueryString["div_Code"].ToString();
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            FMonth = Request.QueryString["FMnth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            //strFieledForceName = Request.QueryString["sf_name"].ToString();
            mode = Request.QueryString["mode"].ToString();
            Sf_CodeMGR = Request.QueryString["SfMGR"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());

            strFieledForceName = Request.QueryString["Sf_Name"].ToString();

            lblHead.Text = "POB Wise Report for - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            dsSalesForce = sf.GetJoiningdate(sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Join_Date = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            //LblForceName.Text = "Field Force Name : " + strFieledForceName + "<span style='font-weight: bold;color:Red;'> " + " (DOJ: " + Join_Date + ") " + "</span>";
            //  CreateDynamicTable();
            FillReport();

        }
    }




    private void FillReport()
    {
        //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
      
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Dr_Chen_Pobcount_ChmstZoom";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Mnth", FMonth);
        cmd.Parameters.AddWithValue("@Year", FYear);
        cmd.Parameters.AddWithValue("@Msf_code", Sf_CodeMGR);
        cmd.Parameters.AddWithValue("@Field", "Chemist Name");
        cmd.Parameters.AddWithValue("@Order", "ASC");

        cmd.CommandTimeout = 600;
       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //[Listeddr_Period_Vst]
     
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        
        dsts.Tables[0].Columns.Remove("Chemists_Code");
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();

    }


    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMnth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        mode = Request.QueryString["mode"].ToString();
        Sf_CodeMGR = Request.QueryString["SfMGR"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        string sProc_Name = "";
        //if ((lblMonthExc.Text) == "Territory")
        {
            sProc_Name = "Dr_Chen_Pobcount_ChmstZoom";  //rptMgr_Coverage_Detail
            SqlCommand cmd = new SqlCommand(sProc_Name, con);

            if (dir1 == SortDirection.Ascending)
            {
                dir1 = SortDirection.Descending;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order", "DSC");
            }
            else
            {
                dir1 = SortDirection.Ascending;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order", "ASC");
            }

            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Mnth", FMonth);
            cmd.Parameters.AddWithValue("@Year", FYear);
            cmd.Parameters.AddWithValue("@Msf_code", Sf_CodeMGR);
            if ((lblMonthExc.Text) == "Territory")
            {
                cmd.Parameters.AddWithValue("@Field", "Territory");
            }
            if ((lblMonthExc.Text) == "Chemist Name")
            {
                cmd.Parameters.AddWithValue("@Field", "Chemist Name");
            }
            if ((lblMonthExc.Text) == "Field Force Name with HQ")
            {
                cmd.Parameters.AddWithValue("@Field", "Name with ff");
            }

            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("Chemists_Code");
        }

        if (dsts.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
            //this.FillReport();
        }
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

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell,  2, 0, "Field Force Name with HQ", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink,2, 0, "Chemist Name", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell,hlink, 2, 0, "Territory", "#0097AC", true);


            FMonth = Request.QueryString["FMnth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();

            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            AddMergedCells(objgridviewrow, objtablecell, 0, 1, "Visit Date", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Total Visit Count", "#0097AC", true);

            //ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();
            TableCell objtablecell2 = new TableCell();
            TableCell objtablecell3 = new TableCell();

            AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "POB Amount", "#0097AC", true);




            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);


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
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void AddMergedCellsLink(GridViewRow objgridviewrow, TableCell objtablecell, HyperLink hlink, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        hlink = new HyperLink();
        objtablecell.Text = celltext;
        hlink.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        hlink.Font.Underline = true;
        hlink.Attributes.Add("style", "cursor:pointer");
        hlink.Attributes.Add("onClick", "callServerButtonEvent('" + celltext + "','" + "" + "')");
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objtablecell.Controls.Add(hlink);
        objgridviewrow.Cells.Add(objtablecell);

    }


    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;
            //
            #region Calculations
            e.Row.Cells[0].Text = (indx + 1).ToString();

            if (e.Row.Cells[5].Text != "" && e.Row.Cells[5].Text != "&nbsp;")
            {
                Tot_Count = Tot_Count + Convert.ToInt32(e.Row.Cells[5].Text);
            }
            e.Row.Cells[0].Attributes.Add("align", "center");
            e.Row.Cells[4].Attributes.Add("align", "center");
            e.Row.Cells[5].Attributes.Add("align", "center");

            int RowSpan = 5;

            if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
            {
                int Ass = 0;
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].ColumnSpan = RowSpan;
                e.Row.Cells[1].Text = "Total Amount :";
                e.Row.Cells[2].Text = (Tot_Count).ToString();


                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Height = 30;
                e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                e.Row.Cells[2].Style.Add("font-size", "10pt");

                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;               

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Height = 30;
                e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                e.Row.Cells[1].Style.Add("font-size", "10pt");

                //RowSpan++;
            }
            #endregion

        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
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

    public SortDirection dir1
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
}