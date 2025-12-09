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
using System.Drawing;
public partial class MasterFiles_AnalysisReports_rpt_Not_At_All_Visit_HQs : System.Web.UI.Page
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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    string des_code = string.Empty;
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    int mode;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    DataSet dsdes = new DataSet();
    DataSet dsHir = new DataSet();
    string strdes = string.Empty;
    string strHir = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            sMode = Request.QueryString["mode"].ToString();
            mode = Convert.ToInt32(Request.QueryString["mode"].ToString());
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            lblHead.Text = "Managers - Not at all Visited HQs for Last " + sMode + "  Months  ";
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            var previousDate = DateTime.Today.AddMonths(-Convert.ToInt32(Request.QueryString["mode"].ToString())).Month.ToString();
            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + div_code + "' and type='2' order by Manager_SNo ";

            dsDes = db_ER.Exec_DataSet(strQry);
            FillReport();


        }
    }
    private void FillReport()
    {
        var lastSixMonths = Enumerable
     .Range(0, mode)
     .Select(i => DateTime.Now.AddMonths(i - mode + 1))
     .Select(date => date.ToString("MM/yyyy"));
        // FillReport();
        string[] sck;
        sck = lastSixMonths.ToArray();
        var last = sck.Last().Split('/');
        var first = sck.First().Split('/');
        int months = (Convert.ToInt32(last[1]) - Convert.ToInt32(first[1])) * 12 + Convert.ToInt32(last[0]) - Convert.ToInt32(first[0]); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(first[0]);
        int cyear = Convert.ToInt32(first[1]);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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
        DataTable dtdes = new DataTable();
        dtdes.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtdes.Columns["INX"].AutoIncrementStep = 1;
        dtdes.Columns["INX"].AutoIncrementSeed = 1;
        dtdes.Columns.Add("Desig", typeof(int));

        if (dsDes.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
            {
                dtdes.Rows.Add(null, dsDes.Tables[0].Rows[i]["Designation_Code"].ToString());
            }
        }
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Not_at_all_visit_HQ";

        //else if (sReportType == 2)
        //{
        //    sProc_Name = "visit_fixation_Spclty";
        //}
        //else if (sReportType == 3)
        //{
        //    sProc_Name = "visit_fixation_Class";
        //}
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@dtdes", dtdes);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(8);
        dsts.Tables[0].Columns.RemoveAt(7);

        dsts.Tables[0].Columns.RemoveAt(3);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts;
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp Id.", "#0097AC", true);
            //  AddMergedCells(objgridviewrow, objtablecell, 0, "Cnt", "#0097AC", true);
            string strQry = "";

            //  strQry = "select Feedback_Id,Feedback_Content,Division_Code,Act_Flag from Mas_App_CallFeedback where Division_Code = '" + div_code + "' and Act_Flag=0 ";

            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + div_code + "' and type='2' order by Manager_SNo ";

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, dtRow["Designation_Short_Name"].ToString(), "#0097AC", false);
            }
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);



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
            objtablecell.RowSpan = 1;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // TableCell cell = e.Row.Cells[5];


            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {
            }
            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
            {


                for (int l = 5, j = 0; l < e.Row.Cells.Count; l++)
                {

                    TableCell cell = e.Row.Cells[l];
                    if (cell.Text.Trim() != "-" && cell.Text.Trim() != "&nbsp;")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00FF00");
                        cell.Text = "";
                    }
                    else
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#FF0000");
                        cell.Text = "";
                    }
                    // l += 2;

                    j++;
                    BoundField field = (BoundField)((DataControlFieldCell)cell).ContainingField;
                    string[] dess = field.HeaderText.Split('_');
                    des_code = dess[1].Trim().ToString();
                    Designation des = new Designation();
                    SalesForce sf = new SalesForce();
                    dsdes = des.getDesig_graph(des_code, div_code);
                    dsHir = sf.Level_Reporting(dtrowClr.Rows[indx][1].ToString(), div_code);
                    if(dsdes.Tables[0].Rows.Count >0)
                    {
                        strdes = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString();
                    }
                    if (dsHir.Tables[0].Rows.Count > 0)
                    {
                        strHir = dsHir.Tables[0].Rows[0]["sfname"].ToString();
                    }
                    if (strHir.Contains(strdes))
                    {

                    }
                    else
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#FFE400");
                    }
                }


            }

            for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {/*
                    HyperLink hLnk = new HyperLink();
                    hLnk.Text = e.Row.Cells[i].Text;
                    hLnk.NavigateUrl = "#";
                    hLnk.ForeColor = System.Drawing.Color.Black;
                    hLnk.Font.Underline = false;
                    hLnk.ToolTip = "Click to View Details";
                    e.Row.Cells[i].Controls.Add(hLnk);*/
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

}