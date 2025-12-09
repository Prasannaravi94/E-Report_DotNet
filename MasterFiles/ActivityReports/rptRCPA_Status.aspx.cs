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
using System.Text;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data.SqlClient;


public partial class MasterFiles_ActivityReports_rptRCPA_Status : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsts = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;
    double TotPotn = new double();
    double TotContr = new double();
    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();


    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        //lblRegionName.Text = sfname;
        //LinkButton1.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "RCPA Analysis for the Month of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        if (sfCode.Contains("MR"))
        {
            //LinkButton1.Visible = false;
            //lblRegionName.Visible = true;
        }
        else
        {
            //LinkButton1.Visible = true;
            //lblRegionName.Visible = false;
        }

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF();      
        FillSample_Prd();

    }



    private void FillSample_Prd()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));

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

        int type;

        if (sfCode.Contains("MR"))
        {
            type = 1;
        }
        else
        {
            type = 2;
        }

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("RCPA_STATUS_RP_demo1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@type", type);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandTimeout = 300;
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        //dsts.Tables[0].Columns.RemoveAt(6);
        //dsts.Tables[0].Columns.RemoveAt(5);
        //dsts.Tables[0].Columns.RemoveAt(1);
        Grdprd.DataSource = dsts;
        Grdprd.DataBind();

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
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void Grdprd_RowCreated(object sender, GridViewRowEventArgs e)
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

            GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();

            #endregion
            //
            #region Merge cells

            if (sfCode.Contains("MGR"))
            {

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", true);

            }
            else
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Listed Dr Name", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Qualification", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Territory", true);
            }

            int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                iLstMonth.Add(cmonth);
                iLstYear.Add(cyear);
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, 2, sTxt, true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Potential", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Contribution", true);

                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //
            #endregion
            //
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        if (objtablecell.Text == "#")
        {
            objtablecell.Style.Add("background-color", "#414d55");
            objtablecell.Style.Add("color", "#ffffff");
            objtablecell.Style.Add("border-left", "0px solid #F1F5F8");
            objtablecell.Style.Add("border-radius", "8px 0 0 8px");
            objtablecell.Style.Add("font-weight", "400");
            objtablecell.Style.Add("font-size", "14px");
            objtablecell.Style.Add("border-bottom", "10px solid #fff");
        }
        else
        {
            objtablecell.Style.Add("background-color", "#F1F5F8");
            objtablecell.Style.Add("color", "#636d73");
            objtablecell.Style.Add("border-left", "1px solid #DCE2E8");
            objtablecell.Style.Add("font-weight", "400");
            objtablecell.Style.Add("font-size", "14px");
            objtablecell.Style.Add("border-bottom", "10px solid #fff");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }



    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int type;


        type = 1;
        string sURL = "";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[1].Visible = false;

            int iInx = e.Row.RowIndex;
            HyperLink lb = new HyperLink();
            lb.Text = e.Row.Cells[2].Text;
            e.Row.Cells[2].Controls.Add(lb);
            sURL = "rptMonPonContr.aspx?FMonth=" + FMonth + "&sf_code=" + e.Row.Cells[1].Text + "&TMonth=" + TMonth + "&FYear=" + FYear + "&TYear=" + TYear;
            lb.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
            lb.NavigateUrl = "#";

        }
    }

    protected void Grdprd_DataBound(object sender, EventArgs e)
    {
        if (Grdprd.Rows.Count > 0)
        {
            int TotalRows = Grdprd.Rows.Count;
            int TotalCol = Grdprd.Rows[0].Cells.Count;
            int FixedCol = 7;
            int ComputedCol = TotalCol - FixedCol;

            //Grdprd.Columns[6].Visible = false;
            //Grdprd.Columns[5].Visible = false;
            //Grdprd.Columns[1].Visible = false;

            Grdprd.FooterRow.Cells[6].Visible = false;
            Grdprd.FooterRow.Cells[5].Visible = false;
            Grdprd.FooterRow.Cells[1].Visible = false;


            Grdprd.FooterRow.Cells[FixedCol - 1].Text = "Total : ";

            for (int i = FixedCol; i < TotalCol; i++)
            {
                double sum = 0.000;

                for (int j = 0; j < TotalRows; j++)
                {
                    if (Grdprd.Rows[j].Cells[i].Text != "-")
                        sum += Grdprd.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(Grdprd.Rows[j].Cells[i].Text) : 0.000;
                }

                Grdprd.FooterRow.Cells[i].Text = sum.ToString("#.00");
            }
        }
    }
}
