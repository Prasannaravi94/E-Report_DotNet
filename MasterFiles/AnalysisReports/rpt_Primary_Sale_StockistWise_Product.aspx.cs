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
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rpt_Primary_Sale_StockistWise_Product : System.Web.UI.Page
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
    string Stok_code = string.Empty;
    string StName = string.Empty;
    double Value = 0.0;
    double Value_1 = 0.0;
    double Value_2 = 0.0;
    double Value_3 = 0.0;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    string sub_code = string.Empty;
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string tot = string.Empty;
    Double total;
    DataSet dsSub = new DataSet();
    string Brand_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sMode = Request.QueryString["selectedValue"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        if (sMode == "4") // --> Product wise
        {
            div_code = Request.QueryString["div_Code"].ToString();
            Stok_code = Request.QueryString["Stok_code"].ToString();
            StName = Request.QueryString["sk_Name"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();

            FillReport();
        }
        else  // --> Brand wise from Dashbrd
        {
            div_code = Request.QueryString["div_Code"].ToString();
            StName = Request.QueryString["Div_New"].ToString();
            if (div_code == "ALL")
            {
                if (StName == "ALL")
                {
                    sf_code = "admin";
                    div_code = Request.QueryString["sf_code"].ToString();
                }
                else
                {
                    sf_code = "admin";
                    div_code = Request.QueryString["Div_New"].ToString();
                }
            }
            else
            {
                sf_code = Request.QueryString["sf_code"].ToString();
                div_code = Request.QueryString["div_Code"].ToString();
            }
            Stok_code = Request.QueryString["Brand_Code"].ToString();
            Brand_Name = Request.QueryString["Brand_Name"].ToString();
            lblBrndName.Text = "      Brand Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
            
            FillReport1();
        }

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        lblHead.Text = "Target Vs Sales(Primary) Report From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + strFieledForceName + "</span>";
        //lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";


    }

    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

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
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        //if (Stok_code.Trim() == "-2")
        //{
        //sProc_Name = "TargetVsSales_new";
        sProc_Name = "PrimarySale_StkWise_Product";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Stk_code", Stok_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("Product_Code");
        dsts.Tables[0].Columns.Remove("Product_Code1");


        GrdFixation.DataSource = dsts.Tables[0];
        GrdFixation.DataBind();

    }


    private void FillReport1()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

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
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        //if (Stok_code.Trim() == "-2")
        //{
        //sProc_Name = "TargetVsSales_new";
        sProc_Name = "PrimarySale_StkWise_Brand_MultiDiv_P";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", (div_code) + ",");
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Brand_Code", Stok_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 500;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("Product_Code");
        dsts.Tables[0].Columns.Remove("Product_Brd_Name");
        dsts.Tables[0].Columns.Remove("Product_Brd_Code");
        dsts.Tables[0].Columns.Remove("Product_Code1");
      //  dsts.Tables[0].Columns.Remove("1_B_PSaleVal");
        GrdFixation.DataSource = dsts.Tables[0];
        GrdFixation.DataBind();
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            if (sMode == "4") // --> Product wise
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
            }
            else
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
            }
            AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            // AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Rate", "#0097AC", true);



            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months1; i++)
            {
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                if (sMode == "4") // --> Product wise
                {
                    AddMergedCells(objgridviewrow, objtablecell, 4, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 2, "Target", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 2, "Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Achieve (%)", "#0097AC", true);
                }
                else
                {
                    AddMergedCells(objgridviewrow, objtablecell, 3, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Target", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Sale", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Previous", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Achieve (%)", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Growth", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "PCPM", "#0097AC", true);
                }
                cmonth1 = cmonth1 + 1;
                if (cmonth1 == 13)
                {
                    cmonth1 = 1;
                    cyear1 = cyear1 + 1;
                }
            }
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

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



    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Font.Size = 10;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
        }
        else
        {
            objtablecell.Style.Add("background-color", "#f8f9fa");
            objtablecell.Style.Add("color", "#6c757d");
            objtablecell.Style.Add("border-color", "#DCE2E8");
        }
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", "#FCFCFC");
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //    objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
        //objgridviewrow.Attributes.Add("style", "background-color:white");
    }


    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Style.Add("border", "1px solid #99ADA5");
            e.Row.Font.Size = 10;
            e.Row.Attributes.Add("style", "background-color:white");
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            double first = 0, second = 0;
            #region Calculations
            int a = 0;
            if (sMode == "4") // --> Product wise
            {
                a = 3;
            }
            else
            {
                a = 4;
            }


            if (e.Row.Cells[2].Text == "ZZZZZ")
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].ColumnSpan = 4;
                e.Row.Cells[3].Text = "Grand Total";
                for (int n = 0; n <= e.Row.Cells.Count - 1; n++)
                {
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[n].Height = 30;

                    e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[n].Style.Add("font-size", "10pt");
                    e.Row.Cells[n].Style.Add("color", "Red");
                    e.Row.Cells[n].Style.Add("border-color", "#DCE2E8");
                }
            }
            

                for (int i = a, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    if (e.Row.Cells[i].Text.Contains("."))
                    {
                        e.Row.Cells[i].Text = Convert.ToDecimal(e.Row.Cells[i].Text).ToString("#0.000");
                    }
                    e.Row.Cells[i].Attributes.Add("align", "Right");
                }

             
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion

            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
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

}