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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
#endregion
//Add by Preethi
public partial class MasterFiles_AnalysisReports_RptDrs_Add_Del_at_a_glance : System.Web.UI.Page
{

    #region Variables
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
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());

        lblHead.Text = "Doctors - Addition/Deactivation Status for the month of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        LblForceName.Visible = false;
        FillReport();
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

        //Dr_Chen_Pobcount_DrZoom

        SqlCommand cmd = new SqlCommand("sp_Drs_add_del_at_aglance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("Sf_Code");
        dsts.Tables[0].Columns.Remove("Sf_Code1");
        if (dsts.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else
        {
            GrdFixation.DataSource = null;
            GrdFixation.DataBind();
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
        string attachment = "attachment; filename=Visit_Details_Class_Wise.xls";
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", attachment);
        Response.ContentType = "application/x-msexcel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
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
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //grdDoctor.Attributes["runat"] = "server";
        //grdDoctor.RenderControl(hw);
        HtmlForm frm = new HtmlForm();
        GrdFixation.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        //grdDoctor.AllowPaging = true;
        GrdFixation.DataBind();
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

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,  "Level 3 Mgr", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,  "Level 2 Mgr", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,  "Level 1 Mgr", "#0097AC", true);

            int months = (Convert.ToInt32(Request.QueryString["To_year"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_year"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["To_Month"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_Month"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());
            //ViewState["months"] = months1;
            //ViewState["cmonth"] = cmonth1;
            //ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Addition Count", "#008080", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deactivation Count", "#008080", false);
                    // AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed", "#0097AC", false);
                    // iLstVstmnt.Add(cmonth1);
                    // iLstVstyr.Add(cyear1);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

            }

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


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
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;
            int mnth = Convert.ToInt32(FMonth);
            int Yr = Convert.ToInt32(FYear);

            e.Row.Cells[0].Text = (iInx + 1).ToString();
            e.Row.Cells[0].Attributes.Add("align", "center");
            string a1 = e.Row.Cells[1].Text;
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
            if (a1 == "ZZZZ")
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                // e.Row.Cells[4].Visible = false;
                e.Row.Cells[8].ColumnSpan = 9;
                e.Row.Cells[8].Text = "Total";
                for (int n = 0; n <= e.Row.Cells.Count - 1; n++)
                {
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[n].Height = 20;
                    e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[n].Style.Add("font-size", "10pt");
                    //e.Row.Cells[n].Style.Add("font-size", "12pt");

                    e.Row.Cells[n].Style.Add("color", "Red");
                    e.Row.Cells[n].Style.Add("border-color", "black");
                }
                int a = 9;
                int b = 10;
                months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);
                if (months1 >= 0)
                {
                    for (int j1 = 1; j1 <= months1 + 1; j1++, a++, b++)
                    {
                        if (e.Row.Cells[a].Text != "&nbsp;" && e.Row.Cells[a].Text != "0" && e.Row.Cells[a].Text != "-" && e.Row.Cells[a].Text != "-")
                        {
                            if (e.Row.Cells[a].Text != "&nbsp;" && e.Row.Cells[a].Text != "0" && e.Row.Cells[a].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[a].Text;
                                hLink.Attributes.Add("class", "btnLstDr");
                                hLink.Attributes.Add("onClick", "callServerButtonEvent('" + cmonth1 + "','" + cyear1 + "','1')");
                                hLink.ToolTip = "Click here";
                                hLink.Attributes.Add("style", "cursor:pointer");
                                hLink.Font.Underline = true;
                                hLink.ForeColor = System.Drawing.Color.Brown;
                                e.Row.Cells[a].Controls.Add(hLink);
                                e.Row.Cells[a].Attributes.Add("align", "center");
                                //j = j + 1;
                            }
                        }

                        if (e.Row.Cells[b].Text != "&nbsp;" && e.Row.Cells[b].Text != "0" && e.Row.Cells[b].Text != "-" && e.Row.Cells[b].Text != "-")
                        {
                            if (e.Row.Cells[b].Text != "&nbsp;" && e.Row.Cells[b].Text != "0" && e.Row.Cells[b].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[b].Text;
                                hLink.Attributes.Add("class", "btnLstDr");
                                hLink.Attributes.Add("onClick", "callServerButtonEvent('" + cmonth1 + "','" + cyear1 + "','2')");
                                hLink.ToolTip = "Click here";
                                hLink.Attributes.Add("style", "cursor:pointer");
                                hLink.Font.Underline = true;
                                hLink.ForeColor = System.Drawing.Color.Brown;
                                e.Row.Cells[b].Controls.Add(hLink);
                                e.Row.Cells[b].Attributes.Add("align", "center");
                                //j = j + 1;
                            }
                        }
                        a = a + 1;
                        b = b + 1;

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 9, j = 10; i < e.Row.Cells.Count; i++)
                {
                    if (dtrowClr.Rows.Count - 1 != iInx)
                    {
                        if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "&nbsp;")
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            hLink.Attributes.Add("class", "btnDrSn");
                            hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + mnth + "', '" + Yr + "', '" + "1" + "', '" + "" + "','" + "" + "','" + Convert.ToString(dtrowClr.Rows[iInx][2].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[iInx][1].ToString()) + "')");
                            hLink.ToolTip = "Click here";
                            hLink.Attributes.Add("style", "cursor:pointer");
                            hLink.Font.Underline = true;
                            hLink.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                        if (e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "" && e.Row.Cells[j].Text != "-" && e.Row.Cells[j].Text != "&nbsp;")
                        {
                            HyperLink hLink1 = new HyperLink();
                            hLink1.Text = e.Row.Cells[j].Text;
                            hLink1.Attributes.Add("class", "btnDrMt");
                            hLink1.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + mnth + "', '" + Yr + "', '" + "2" + "', '" + "" + "','" + "" + "','" + Convert.ToString(dtrowClr.Rows[iInx][2].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[iInx][1].ToString()) + "')");
                            hLink1.ToolTip = "Click here";
                            hLink1.Attributes.Add("style", "cursor:pointer");
                            hLink1.Font.Underline = true;
                            hLink1.ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[j].Controls.Add(hLink1);
                        }
                    }
                    string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                    string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    if (e.Row.Cells[j].Text == "&nbsp;" || e.Row.Cells[j].Text == "0")
                    {
                        e.Row.Cells[j].Text = "-";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");
                    e.Row.Cells[j].Attributes.Add("align", "center");

                    j = j + 2;
                    i = i + 1;
                    mnth = mnth + 1;

                    if (mnth == 13)
                    {
                        mnth = 1;
                        Yr = Yr + 1;
                    }
                }
            }
        }
    }

    protected void btnExcelGrid_Click(object sender, EventArgs e)
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("sp_Drs_add_del_at_aglance_Dump", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lblMonthExc.Text));
        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lblYearExc.Text));
        cmd.Parameters.AddWithValue("@Mode", Convert.ToInt32(lblMode.Text));

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        ds.Tables[0].Columns.Remove("sf_code");
        ds.Tables[0].Columns.Remove("ListedDrCode");

        DataTable dt = ds.Tables[0];

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(lblMonthExc.Text)).ToString().Substring(0, 3);

        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        var ws = wbook.Worksheets.Add(dt, "Drs Add Deactive Status");

        ws.Row(1).InsertRowsAbove(1);
        ws.Cell(1, 1).Value = "Doctors - Addition/Deactivation Status for the month of  " + strFMonthName + " " + lblYearExc.Text + " ";
        ws.Cell(1, 1).Style.Font.Bold = true;
        ws.Cell(1, 1).Style.Font.FontSize = 15;
        //  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        ws.Range("A1:K1").Row(1).Merge();

        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"Doctors - Addition/Deactivation Status.xlsx\"");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }
        httpResponse.End();
    }

}