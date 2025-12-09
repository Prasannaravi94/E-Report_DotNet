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
public partial class MasterFiles_AnalysisReports_Rpt_Core_drs_Speclty_wise : System.Web.UI.Page
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

    DataSet dsts = new DataSet();

    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //div_code = Request.QueryString["div_Code"].ToString();
            div_code = Session["div_code"].ToString();
            sf_code = Request.QueryString["Sf_code"].ToString();
            strFieledForceName = Request.QueryString["Sf_Name"].ToString();
            txtEffFrom = Request.QueryString["txtEffFrom"].ToString();
            txtEffTo = Request.QueryString["txtEffTo"].ToString();
            sSpeciality = Request.QueryString["cbVal"].ToString();

            lblHead.Text = "MGR Fieldwork Days and Coverage Details From &nbsp;" + Convert.ToDateTime(txtEffFrom).ToString("dd MMMM yyyy") + " &nbsp; to &nbsp;" + Convert.ToDateTime(txtEffTo).ToString("dd MMMM yyyy");
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }

    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);

        DataTable dtSpclty = new DataTable();
        dtSpclty.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpclty.Columns["INX"].AutoIncrementSeed = 1;
        dtSpclty.Columns["INX"].AutoIncrementStep = 1;
        dtSpclty.Columns.Add("CODE", typeof(int));

        string spclty = Request.QueryString["cbVal"].ToString();
        spclty = spclty.Remove(spclty.LastIndexOf(','));
        string[] ttlSpc = spclty.Split(',');

        foreach (string sSpclty in ttlSpc)
        {
            if (sSpclty != "")
                dtSpclty.Rows.Add(null, Convert.ToInt32(sSpclty));
        }

        string sProc_Name = "";
        sProc_Name = "CoreDrs_details_Splty";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Frm_Date", txtEffFrom);
        cmd.Parameters.AddWithValue("@To_Date", txtEffTo);
        cmd.Parameters.AddWithValue("@SpcltyTbl", dtSpclty);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("Sf_Code");
        dsts.Tables[0].Columns.Remove("Sf_code1");
        dsts.Tables[0].Columns.Remove("clr");
        //dsts.Tables[0].Columns.Remove("ListedDrCode1");
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
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();


            sSpeciality = (Request.QueryString["cbVal"].ToString()).Remove((Request.QueryString["cbVal"].ToString()).Length - 1);

            HyperLink hlink = new HyperLink();
            DB_EReporting db = new DB_EReporting();
            string strQry = "";
            strQry = "SELECT Doc_Special_Code, Doc_Special_SName, Doc_Special_Name FROM Mas_Doctor_Speciality WHERE Doc_Special_Active_Flag=0 AND  division_code='" + div_code + "' AND Doc_Special_Code IN(" + sSpeciality + ") ORDER BY Doc_Special_Code ";
            dsDoctor = db.Exec_DataSet(strQry);

            int Doc_Count_head = dsDoctor.Tables[0].Rows.Count;

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Desig", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "EMP<br>Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "LAST<br>Dcr Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FW<br>Days", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "MVD<br>List", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "MVD<br>Met", "#0097AC", true);

            //AddMergedCells(objgridviewrow, objtablecell, (Doc_Count_head * 3), "Speciality", "#0097AC", true);
            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {
                AddMergedCells(objgridviewrow, objtablecell, (3), dtRow["Doc_Special_SName"].ToString(), "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "List", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "%", "#0097AC", false);
            }

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
        objtablecell.Font.Size = 7;
        //objtablecell.Style.Add("border-top-style", "solid");

        //objtablecell.Attributes.Add("style", "border: 1px solid #fff000");
        //objtablecell.Style.Add("border", "1px solid #99ADA5");
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Style.Add("border", "1px solid #99ADA5");
            //e.Row.Attributes.Add("style", "border: 1px solid Black");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("border", "1px solid #99ADA5");
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;
            //
            #region Calculations
            e.Row.Font.Size = 7;
            e.Row.Attributes.Add("style", "background-color:white");

            for (int i =9, j = 7; i < e.Row.Cells.Count; i+=3)
            {
                //  e.Row.Cells[i].Attributes.Add("align", "left");


                int iMax = (e.Row.Cells[i].Text == "-" || e.Row.Cells[i].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i].Text);
                int iMin = (e.Row.Cells[i + 1].Text == "-" || e.Row.Cells[i + 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i + 1].Text);
                decimal dCvg = 0;
                decimal mCvg = 0;
                if (iMax != 0 && iMin != 0)
                    dCvg = Decimal.Divide((iMin * 100), iMax);
                else if ((iMax != 0 && iMin == 0))
                    dCvg = -250;
                e.Row.Cells[i + 2].Text = dCvg.ToString("0.##");
                if (e.Row.Cells[i + 2].Text == "0")
                {
                    e.Row.Cells[i + 2].Text = "-";
                    e.Row.Cells[i + 2].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                else if (dCvg == -250)
                {
                    e.Row.Cells[i + 2].Text = "0";
                    e.Row.Cells[i + 2].Attributes.Add("style", "color:red;font-weight:bolder;");
                }
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                string backcolor = Convert.ToString(dtrowClr.Rows[j][6].ToString()) == "DEFFBE" ? "A9FFCA" : Convert.ToString(dtrowClr.Rows[j][6].ToString());

                e.Row.Attributes.Add("style", "background-color:" + "#" + backcolor);
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
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