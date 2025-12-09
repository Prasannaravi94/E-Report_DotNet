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

public partial class MasterFiles_AnalysisReports_rpt_PrimarySale_Multi_Mnth_HQWise : System.Web.UI.Page
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

    string Sf_CodeMGR = string.Empty;
    DataTable dtrowClr = new DataTable();
    DataTable dtrowClr2 = new DataTable();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int seen = 0;
    int Fw = 0;

    int Tot_Count = 0;
    ArrayList Values = new ArrayList();
    string txtEffFrom = string.Empty;
    string txtEffTo = string.Empty;
    string sSpeciality = string.Empty;
    int Sec_Cnt = 0;
    DataSet dsSecSales = new DataSet();
    DataSet dsts = new DataSet();
    string Reporting2 = string.Empty;
    string Reporting1 = string.Empty;
    string Mode = string.Empty;
    string Stok_code = string.Empty;
    string sk_Name = string.Empty;
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        Mode = Request.QueryString["selectedValue"].ToString();

        Stok_code = Request.QueryString["Stok_code"].ToString();
        sk_Name = Request.QueryString["sk_Name"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        if (Mode == "3")
        {
            lblHead.Text = "Primary Sale HQ Wise(Bifurcation) for the Period of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        }
        else if (Mode == "4")
        {
            lblHead.Text = "Primary Sale Product Wise for the Period of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        }

        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        if (!Page.IsPostBack)
        {
            FillReport();
        }
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

        string Proce_Name = string.Empty;

        if (Mode == "3")
        {
            //   Proce_Name = "PrimarySale_MultiMnth_FF_HQwise_P";
            Proce_Name = "PrimarySale_MultiMnth_FF_HQwise_P_Pcpm";
        }
        else if (Mode == "4")
        {
          //Proce_Name = "PrimarySale_MultiMnth_GrpBrandProductwise_P_Pcpm";
          Proce_Name = "PrimarySale_MultiMnth_GrpBrandProductwise_P_t";
        }

        SqlCommand cmd = new SqlCommand(Proce_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        if (Mode == "4")
        {
            cmd.Parameters.AddWithValue("@Stk_code", Stok_code);
        }
        cmd.CommandTimeout = 500;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        if (Mode == "3")
        {
            //dsts.Tables[0].Columns.Remove("HQ Code");
            dsts.Tables[0].Columns.Remove("MR_Count");

            dsts.Tables[0].Columns.Remove("sf_cat_code");
            dsts.Tables[0].Columns.Remove("Reporting_2_HQ");
            dsts.Tables[0].Columns.Remove("Reporting_1_HQ");
            //dsts.Tables[0].Columns.Remove("MR_Count");
        }
        else if (Mode == "4")
        {
            dsts.Tables[0].Columns.Remove("Product_Code");
            dsts.Tables[0].Columns.Remove("Product_Grp_Code");
            dsts.Tables[0].Columns.Remove("Product_Brd_Code");
            dsts.Tables[0].Columns.Remove("Product_Code1");
            dsts.Tables[0].Columns.Remove("Product_Grp_Code1");
            dsts.Tables[0].Columns.Remove("Product_Brd_Code1");
        }

        GrdFixation.DataSource = dsts.Tables[0];
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
            int cnt = 0;
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            if (Mode == "3")
            {
                cnt = 7;
                AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting 2", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting 1", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "HQ Code", "#0097AC", true);

                AddMergedCells(objgridviewrow, objtablecell, 0, "SS", "#0097AC", true);
            }
            else if (Mode == "4")
            {
                cnt = 9;
                AddMergedCells(objgridviewrow, objtablecell, 0, "Group Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Brand Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            }

            int months = (Convert.ToInt32(Request.QueryString["To_year"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_year"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["To_Month"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_Month"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            TableCell objtablecell3 = new TableCell();
            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, cnt, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", true);
                    if (Mode == "4")
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETLYQTY", "#0097AC", false);
                    }
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETLYVAL", "#0097AC", false);
                    if (Mode == "4")
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETCYQTY", "#0097AC", false);
                    }
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETCYVAL", "#0097AC", false);
                    if (Mode == "4")
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "TGTQTY", "#0097AC", false);
                    }
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "TGT", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "ACHT%", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "GTH%", "#0097AC", false);
                    if (Mode == "4")
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETPCPVAL", "#0097AC", false);
                    }

                    if (Mode == "3")
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "PCP", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "PCPVAL", "#0097AC", false);
                    }
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, "% ON<br/>Over<br/>All<br/>Sales", "#0097AC", false);                        

                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            AddMergedCells(objgridviewrow, objtablecell, cnt, "Upto " + sf.getMonthName((Convert.ToInt32(Request.QueryString["To_Month"].ToString())).ToString()).Substring(0, 3) + " - " + (Request.QueryString["To_year"].ToString()), "#0097AC", true);
            if (Mode == "4")
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETLYQTY", "#0097AC", false);
            }

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETLYVAL", "#0097AC", false);
            if (Mode == "4")
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETCYQTY", "#0097AC", false);
            }
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETCYVAL", "#0097AC", false);
            if (Mode == "4")
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "TGTQTY", "#0097AC", false);
            }
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "TGT", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "ACHT%", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "GTH%", "#0097AC", false);
            if (Mode == "4")
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "NETPCPVAL", "#0097AC", false);
            }
            if (Mode == "3")
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "PCP", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "PCPVAL", "#0097AC", false);
            }

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
        }
        #endregion
    }


    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Font.Size = 10;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
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
    }


    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("border", "1px solid #99ADA5");
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;

            if ((e.Row.Cells[1].Text == Reporting2) && (e.Row.Cells[2].Text == Reporting1))
            {
                e.Row.Cells[2].Text = "";
            }
            if (e.Row.Cells[1].Text == Reporting2)
            {
                e.Row.Cells[1].Text = "";
            }
            //
            #region Calculations
            e.Row.Font.Size = 10;
            e.Row.Attributes.Add("style", "background-color:white");

            decimal MRCount = 0;
            decimal HQCount = 0;
            if (Mode == "3")
            {
                MRCount = Convert.ToDecimal(dtrowClr.Rows[indx][5].ToString());
                HQCount = Convert.ToDecimal(dtrowClr.Rows[indx][6].ToString());
            }
            int hq_cnt = 0;
            if (Mode == "3")
            {
                hq_cnt = 6;
            }
            else if (Mode == "4")
            {
                hq_cnt = 5;
            }
            int cnt = 11;
            for (int i = hq_cnt; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "" || e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "0";
                }

                if (Mode == "3")
                {
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        // e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)) / MRCount) * (HQCount)).ToString("#0.00");
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)))).ToString("#0.000");
                    }
                    i = i + 1;
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        //  e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)) / MRCount) * (HQCount)).ToString("#0.00");
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)))).ToString("#0.000");
                    }
                    i = i + 1;
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        //  e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)) / MRCount) * (HQCount)).ToString("#0.00");
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)))).ToString("#0.000");
                    }

                    i = i + 1;
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i - 1].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i - 2].Text)) / (Convert.ToDecimal(e.Row.Cells[i - 1].Text))) * (100)).ToString("#0");
                    }
                    i = i + 1;
                    if (e.Row.Cells[i - 4].Text != "&nbsp;" && e.Row.Cells[i - 4].Text != "0" && e.Row.Cells[i - 3].Text != "0" && e.Row.Cells[i - 4].Text != "-" && e.Row.Cells[i - 4].Text != "" && !e.Row.Cells[i - 4].Text.Contains("-"))
                    {
                        e.Row.Cells[i].Text = ((((Convert.ToDecimal(e.Row.Cells[i - 3].Text)) - (Convert.ToDecimal(e.Row.Cells[i - 4].Text))) / ((Convert.ToDecimal(e.Row.Cells[i - 4].Text)))) * 100).ToString("#0");
                    }
                    i = i + 1;
                    if (e.Row.Cells[i - 4].Text != "&nbsp;" && e.Row.Cells[i - 4].Text != "0" && e.Row.Cells[i - 4].Text != "-" && e.Row.Cells[i - 4].Text != "")
                    {
                      //  e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i - 4].Text)) / HQCount)).ToString("#0.000");
                    }
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                       // e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i - 4].Text)) / HQCount)).ToString("#0.000");
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)))).ToString("#0.000");
                    }
                    i = i + 1;
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        // e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i - 4].Text)) / HQCount)).ToString("#0.000");
                        e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i].Text)))).ToString("#0.000");
                    }
                }
                else if (Mode == "4")
                {
                    decimal SaleVal = 0;
                    decimal TrgetVal = 0;

                    decimal CurSaleVal = 0;
                    decimal LastSaleVal = 0;

                    //if (i == cnt)
                    //{
                    //    if (e.Row.Cells[2].Text == "ZZTotal" || e.Row.Cells[3].Text == "ZZZZ")
                    //    {
                    //        if (e.Row.Cells[i - 3].Text != "&nbsp;" && e.Row.Cells[i - 3].Text != "0" && e.Row.Cells[i - 3].Text != "-" && e.Row.Cells[i - 3].Text != "")
                    //        {
                    //            SaleVal = Convert.ToDecimal(e.Row.Cells[i - 3].Text);
                    //        }
                    //        else
                    //        {
                    //            SaleVal = 0;
                    //        }
                    //        if (e.Row.Cells[i - 1].Text != "&nbsp;" && e.Row.Cells[i - 1].Text != "0" && e.Row.Cells[i - 1].Text != "-" && e.Row.Cells[i - 1].Text != "")
                    //        {
                    //            TrgetVal = Convert.ToDecimal(e.Row.Cells[i - 1].Text);
                    //        }
                    //        else
                    //        {
                    //            TrgetVal = 0;
                    //        }

                    //        if ( TrgetVal != 0)
                    //        {
                    //            e.Row.Cells[i].Text = (((Convert.ToDecimal(SaleVal)) / (Convert.ToDecimal(TrgetVal))) * (100)).ToString("#0");

                    //            //e.Row.Cells[i].Text = e.Row.Cells[i-1].Text;
                    //            //e.Row.Cells[i].Text = Math.Round(double.Parse(((((Convert.ToDecimal(e.Row.Cells[i - 2].Text)) / (Convert.ToDecimal(e.Row.Cells[i - 1].Text))) * (100)).ToString())), 0).ToString();
                    //        }
                    //    }
                    //}
                    //if (i == (cnt + 1))
                    //{
                    //    if (e.Row.Cells[2].Text == "ZZTotal" || e.Row.Cells[3].Text == "ZZZZ")
                    //    {
                    //        if (e.Row.Cells[i - 4].Text != "&nbsp;" && e.Row.Cells[i - 4].Text != "0" && e.Row.Cells[i - 4].Text != "-" && e.Row.Cells[i - 4].Text != "")
                    //        {
                    //            CurSaleVal = Convert.ToDecimal(e.Row.Cells[i - 4].Text);
                    //        }
                    //        else
                    //        {
                    //            CurSaleVal = 0;
                    //        }
                    //        if (e.Row.Cells[i - 6].Text != "&nbsp;" && e.Row.Cells[i - 6].Text != "0" && e.Row.Cells[i - 6].Text != "-" && e.Row.Cells[i - 6].Text != "")
                    //        {
                    //            LastSaleVal = Convert.ToDecimal(e.Row.Cells[i - 6].Text);
                    //        }
                    //        else
                    //        {
                    //            LastSaleVal = 0;
                    //        }


                    //        if (LastSaleVal != 0)
                    //        {
                    //            //e.Row.Cells[i].Text = e.Row.Cells[i - 6].Text;

                    //            e.Row.Cells[i].Text = ((((Convert.ToDecimal(CurSaleVal)) - (Convert.ToDecimal(LastSaleVal))) / ((Convert.ToDecimal(LastSaleVal)))) * 100).ToString("#0");
                    //        }
                    //        else
                    //        {
                    //            e.Row.Cells[i].Text ="0";
                    //        }
                    //        cnt = cnt + 7;
                    //    }


                    //}

                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                        if (e.Row.Cells[i].Text.Contains("."))
                        {
                            e.Row.Cells[i].Text = (Convert.ToDecimal(e.Row.Cells[i].Text)).ToString("#0.000");
                        }

                    }
                    //i = i + 1;
                }
                if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "" || e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
                //e.Row.Cells[i].Wrap = true;
            }

            if (Mode == "3")
            {
                if (e.Row.Cells[1].Text == "ZZTotal" && e.Row.Cells[2].Text == "ZZTotal" && e.Row.Cells[3].Text == "ZZTotal")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "All India";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Cells[5].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }
                else if (e.Row.Cells[2].Text == "ZZTotal" && e.Row.Cells[3].Text == "ZZTotal")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "Grand Total";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }



                else if (e.Row.Cells[2].Text != "ZZTotal" && e.Row.Cells[3].Text == "ZZZZ")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[2].Text = e.Row.Cells[1].Text + " Total";
                    e.Row.Cells[3].Text = "";
                    //e.Row.Cells[2].Text = "";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Attributes.Add("style", "font-bold:true; font-size:14px; Color:#C71585; border-color:Black");
                }
            }
            else if (Mode == "4")
            {
                if (e.Row.Cells[1].Text == "ZZTotal" && e.Row.Cells[2].Text == "ZZTotal" && e.Row.Cells[3].Text == "ZZZZ")
                {
                    //#8B2252
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = " <span style='color:Red';> " + "Final Total " + "</span>";
                    //e.Row.Cells[1].Text = "Grand Total (" + Reporting2 + ")";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }
                if (e.Row.Cells[1].Text != "ZZTotal" && e.Row.Cells[2].Text == "ZZTotal" && e.Row.Cells[3].Text == "ZZZZ")
                {
                    //#8B2252
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = " <span style='color:Red';> " + "Grand Total - (" + "</span> " + "<span style='color:#8B2252';> " + Reporting2 + "</span>" + ") ";
                    //e.Row.Cells[1].Text = "Grand Total (" + Reporting2 + ")";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }

                if (e.Row.Cells[1].Text != "ZZTotal" && e.Row.Cells[2].Text != "ZZTotal" && e.Row.Cells[3].Text == "ZZZZ")
                {
                    //#8B1C62
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[2].Text = " <span style='color:#C71585';font-bold:true;> " + "Total - (" + "</span>" + "<span style='color:#7D26CD';font-bold:true;>" + Reporting1 + "</span>" + ") ";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    //e.Row.Cells[2].Text = "";

                    e.Row.Attributes.Add("style", "font-bold:true; font-size:14px; Color:#C71585; border-color:Black");
                }

                e.Row.Cells[3].Wrap = false;
            }



            if (Mode == "3")
            {
                Reporting2 = dtrowClr.Rows[indx][1].ToString();
                Reporting1 = dtrowClr.Rows[indx][2].ToString();
            }
            else if (Mode == "4")
            {
                Reporting2 = dtrowClr.Rows[indx][4].ToString();
                Reporting1 = dtrowClr.Rows[indx][5].ToString();
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