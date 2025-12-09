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

public partial class MIS_Reports_rptTourPlan_Datewise : System.Web.UI.Page
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

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string mode = string.Empty;
    string day = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_code"].ToString();
        FMonth = Request.QueryString["cmon"].ToString();
        FYear = Request.QueryString["cyear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        mode = Request.QueryString["modee"];
        day = Request.QueryString["days"];

        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Tour Plan Datewise of " + strFMonthName + " " + FYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF();      
        FillTourdays();

    }



    private void FillTourdays()
    {
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth);
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


        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtDays.Columns["INX"].AutoIncrementSeed = 1;
        dtDays.Columns["INX"].AutoIncrementStep = 1;
        dtDays.Columns.Add("Dayss", typeof(string));


        string dd = day.Trim();
        dd = day.Remove(day.Length - 1);

        string[] dayss = { dd };

        dayss = dd.Split(',');

        foreach (string d in dayss)
        {
            dtDays.Rows.Add(null, d.ToString());
        }

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("TourPlan_Datewise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@Fmonth", FMonth);
        cmd.Parameters.AddWithValue("@Fyear", FYear);
        cmd.Parameters.AddWithValue("@dtDays", dtDays);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(10);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(8);
        dsts.Tables[0].Columns.RemoveAt(1);
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

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();


            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "State", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Joining Date", "#0097AC", true);

            int months = (Convert.ToInt32(Request.QueryString["cyear"].ToString()) - Convert.ToInt32(Request.QueryString["cyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["cmon"].ToString()) - Convert.ToInt32(Request.QueryString["cmon"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["cmon"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["cyear"].ToString());

            //int delimiters = day.Count(x => x == '/');
            int count =day.Count(x => x== ',');

            SalesForce sf = new SalesForce();
            string sTxt = "&nbsp;" + sf.getMonthName(FMonth.ToString()) + "-" + cyear;
            AddMergedCells(objgridviewrow, objtablecell, 0, count, sTxt, "#0097AC", true);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, dsDCR.Tables[0].Rows.Count + 9, "Field Activty Report", "#0097AC", true);

            string dd = day.Trim();
            dd = day.Remove(day.Length - 1);

            string[] dayss = { dd };

            dayss = dd.Split(',');

            foreach (string d in dayss)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, d, "#0097AC", true);
            }


                //for (int i = 0; i < dsDCR.Tables[0].Rows.Count; i++)
                //{
                //    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDCR.Tables[0].Rows[i]["Worktype_Name_B"].ToString(), "#0097AC", true);
                //}
            

           





       
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
            //
            #endregion
            //
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;
            for (int i = 7, j = 0; i < e.Row.Cells.Count; i++)
            {
                //if (e.Row.Cells[i].Text != "0")
                //{
                HyperLink hLink = new HyperLink();
                hLink.Text = e.Row.Cells[i].Text;
                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                //int cMnth = iLstMonth[j];
                //int cYr = iLstYear[j];



                e.Row.Cells[i].Controls.Add(hLink);
                int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                j++;
                //}

                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                {
                    e.Row.Cells[i].Text = "";
                }
                e.Row.Cells[i].Attributes.Add("align", "left");
                e.Row.Cells[i].Wrap = false;

            }


            //e.Row.Cells[0].Wrap = false;
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
            //e.Row.Cells[4].Wrap = false;
            //e.Row.Cells[5].Wrap = false;
            //e.Row.Cells[6].Wrap = false;
            //e.Row.Cells[7].Wrap = false;
            //e.Row.Cells[8].Wrap = false;
        }
    }

}