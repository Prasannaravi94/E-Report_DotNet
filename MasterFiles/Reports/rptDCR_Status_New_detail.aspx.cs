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
using System.Globalization;

public partial class MasterFiles_Reports_rptDCR_Status_New_Detail : System.Web.UI.Page
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
    DataTable dtrowClr_New = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    DataSet dsDCR = null;
    string tot = string.Empty;
    int total = 0;
    int total2 = 0;
    string chkatten = string.Empty;
    string checkVacant = string.Empty;
    int totHoli = 0;
    int totWeek = 0;
    int totDel = 0;
    string type = string.Empty;
    string fdate = string.Empty;
    string todate = string.Empty;
    DateTime dtDCRfrom;
    DateTime dtDCRto;
    int first = 0;
    int second = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"];
        FMonth = Request.QueryString["cmon"];
        FYear = Request.QueryString["cyear"];
        sfname = Request.QueryString["sf_name"];
        chkatten = Request.QueryString["chkatten"];
        checkVacant = Request.QueryString["checkVacant"];
        type = Request.QueryString["type"];
        fdate = Request.QueryString["fdate"];
        todate = Request.QueryString["todate"];

        dtDCRfrom = Convert.ToDateTime(fdate);
        dtDCRto = Convert.ToDateTime(todate);


        lblRegionName.Text = sfname;

        //lblHead.CssClass = "blink_me";
        blinkText.Attributes.Add("class", "blink_me");




        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF();

        if (type == "1")
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            lblHead.Text = "Daily Call Status for the Month of " + "<span style=color:red>" + strFMonthName + " " + FYear + "</span>";
            FillSample_Prd();
        }
        else if (type == "2")
        {
            lblHead.Text = lblHead.Text + fdate + " and " + todate;
            //status_peridically_test();
            status_peridically();
        }
        FillWorkType();

    }
    //private void status_peridically_test()
    //{


    //    int months = (Convert.ToInt32(dtDCRto.Year) - Convert.ToInt32(dtDCRfrom.Year)) * 12 + Convert.ToInt32(dtDCRto.Month) - Convert.ToInt32(dtDCRfrom.Month);
    //    int cmonth = Convert.ToInt32(dtDCRfrom.Month);
    //    int cyear = Convert.ToInt32(dtDCRfrom.Year);

    //    DataTable dtfrmth = new DataTable();
    //    dtfrmth.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtfrmth.Columns["INX"].AutoIncrementSeed = 1;
    //    dtfrmth.Columns["INX"].AutoIncrementStep = 1;
    //    dtfrmth.Columns.Add("fromnth", typeof(int));


    //    int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));

    //    for (int i = dtDCRfrom.Day; i <= days; i++)
    //    {

    //        dtfrmth.Rows.Add(null, i);

    //    }

    //    DataTable dtTomth = new DataTable();
    //    dtTomth.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtTomth.Columns["INX"].AutoIncrementSeed = 1;
    //    dtTomth.Columns["INX"].AutoIncrementStep = 1;
    //    dtTomth.Columns.Add("Tmnth", typeof(int));


    //    int to_days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRto.Year), Convert.ToInt16(dtDCRto.Month));

    //    for (int i = 1; i <= dtDCRto.Day; i++)
    //    {

    //        dtTomth.Rows.Add(null, i);

    //    }


    //    int iMn = 0; int iYr = 0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
    //    dtMnYr.Columns["INX"].AutoIncrementStep = 1;
    //    dtMnYr.Columns.Add("MNTH", typeof(int));
    //    dtMnYr.Columns.Add("YR", typeof(int));

    //    while (months >= 0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null, iMn, iYr);
    //        months--; cmonth++;
    //    }

    //    string fromdate = dtDCRfrom.ToString("MM-dd-yyyy");
    //    string todate = dtDCRto.ToString("MM-dd-yyyy");

    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);

    //    SqlCommand cmd = new SqlCommand("DCR_STATUS_NEW_NEW_Periodically_Test", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
    //    cmd.Parameters.AddWithValue("@Msf_code", sfCode);
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    cmd.Parameters.AddWithValue("@Strfromth", dtfrmth);
    //    cmd.Parameters.AddWithValue("@StrTomth", dtTomth);
    //    cmd.Parameters.AddWithValue("@fromdate", fromdate);
    //    cmd.Parameters.AddWithValue("@todate", todate);
    //    cmd.Parameters.AddWithValue("@fromonth", dtDCRfrom.Month);
    //    cmd.Parameters.AddWithValue("@fromyear", dtDCRfrom.Year);
    //    cmd.Parameters.AddWithValue("@Tomonth", dtDCRto.Month);
    //    cmd.Parameters.AddWithValue("@Toyear", dtDCRto.Year);
    //    cmd.Parameters.AddWithValue("@Detailed", 0);
    //    cmd.CommandTimeout = 150;

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    // dsts.Tables[0].Columns.RemoveAt(11);
    //    dsts.Tables[0].Columns.RemoveAt(10);
    //    dsts.Tables[0].Columns.RemoveAt(9);
    //    dsts.Tables[0].Columns.RemoveAt(8);
    //    dsts.Tables[0].Columns.RemoveAt(7);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    Grdprd.DataSource = dsts;
    //    Grdprd.DataBind();




    //}




    private void status_peridically()
    {


        int months = (Convert.ToInt32(dtDCRto.Year) - Convert.ToInt32(dtDCRfrom.Year)) * 12 + Convert.ToInt32(dtDCRto.Month) - Convert.ToInt32(dtDCRfrom.Month);
        int cmonth = Convert.ToInt32(dtDCRfrom.Month);
        int cyear = Convert.ToInt32(dtDCRfrom.Year);

        DataTable dtfrmth = new DataTable();
        dtfrmth.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtfrmth.Columns["INX"].AutoIncrementSeed = 1;
        dtfrmth.Columns["INX"].AutoIncrementStep = 1;
        dtfrmth.Columns.Add("fromnth", typeof(int));


        int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));

        for (int i = dtDCRfrom.Day; i <= days; i++)
        {

            dtfrmth.Rows.Add(null, i);

        }

        DataTable dtTomth = new DataTable();
        dtTomth.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtTomth.Columns["INX"].AutoIncrementSeed = 1;
        dtTomth.Columns["INX"].AutoIncrementStep = 1;
        dtTomth.Columns.Add("Tmnth", typeof(int));


        int to_days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRto.Year), Convert.ToInt16(dtDCRto.Month));

        for (int i = 1; i <= dtDCRto.Day; i++)
        {

            dtTomth.Rows.Add(null, i);

        }


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

        string fromdate = dtDCRfrom.ToString("MM-dd-yyyy");
        string todate = dtDCRto.ToString("MM-dd-yyyy");

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        DateTime Current_month_Year =DateTime.Now;
        if (dtDCRfrom.Month == Current_month_Year.Month && dtDCRfrom.Year == Current_month_Year.Year && dtDCRto.Month == Current_month_Year.Month && dtDCRto.Year == Current_month_Year.Year || dtDCRfrom.Month == Current_month_Year.Month && dtDCRfrom.Year == Current_month_Year.Year || dtDCRto.Month == Current_month_Year.Month && dtDCRto.Year == Current_month_Year.Year)
        {


            SqlCommand cmd = new SqlCommand("DCR_STATUS_NEW_NEW_Detl_Periodically_Currnt_Month", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Strfromth", dtfrmth);
            cmd.Parameters.AddWithValue("@StrTomth", dtTomth);
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);
            cmd.Parameters.AddWithValue("@fromonth", dtDCRfrom.Month);
            cmd.Parameters.AddWithValue("@fromyear", dtDCRfrom.Year);
            cmd.Parameters.AddWithValue("@Detailed", 0);
            cmd.Parameters.AddWithValue("@checkVacant", checkVacant);
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(11);
            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(9);
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);
            dsts.Tables[0].Columns.RemoveAt(1);
            Grdprd.DataSource = dsts;
            Grdprd.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("DCR_STATUS_NEW_NEW_Detl_Periodically", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Strfromth", dtfrmth);
            cmd.Parameters.AddWithValue("@StrTomth", dtTomth);
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);
            cmd.Parameters.AddWithValue("@fromonth", dtDCRfrom.Month);
            cmd.Parameters.AddWithValue("@fromyear", dtDCRfrom.Year);
            cmd.Parameters.AddWithValue("@Detailed", 0);
            cmd.Parameters.AddWithValue("@checkVacant", checkVacant);
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(11);
            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(9);
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);
            dsts.Tables[0].Columns.RemoveAt(1);
            Grdprd.DataSource = dsts;
            Grdprd.DataBind();
        }




    }


    private void FillSample_Prd()
    {

        int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
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

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

           DateTime Current_month_Year =DateTime.Now;

           if (Convert.ToInt16(FMonth) == Current_month_Year.Month && Convert.ToInt16(FYear) == Current_month_Year.Year)
           {


               SqlCommand cmd = new SqlCommand("DCR_STATUS_NEW_NEW_Detl_Currnt_Month", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
               cmd.Parameters.AddWithValue("@Msf_code", sfCode);
               cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
               cmd.Parameters.AddWithValue("@Days", days);
               cmd.Parameters.AddWithValue("@Detailed", 1);
               cmd.Parameters.AddWithValue("@checkVacant", checkVacant);
               cmd.Parameters.AddWithValue("@FMonth", FMonth);
               cmd.Parameters.AddWithValue("@FYear", FYear);
               cmd.CommandTimeout = 150;

               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataSet dsts = new DataSet();
               da.Fill(dsts);
               dtrowClr = dsts.Tables[0].Copy();
               //dsts.Tables[0].Columns.RemoveAt(11);
               dsts.Tables[0].Columns.RemoveAt(10);
               dsts.Tables[0].Columns.RemoveAt(9);
               dsts.Tables[0].Columns.RemoveAt(8);
               dsts.Tables[0].Columns.RemoveAt(7);
               dsts.Tables[0].Columns.RemoveAt(1);


               Grdprd.DataSource = dsts;
               Grdprd.DataBind();
           }
           else
           {

               SqlCommand cmd = new SqlCommand("DCR_STATUS_NEW_NEW_Detl", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
               cmd.Parameters.AddWithValue("@Msf_code", sfCode);
               cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
               cmd.Parameters.AddWithValue("@Days", days);
               cmd.Parameters.AddWithValue("@Detailed", 1);
               cmd.Parameters.AddWithValue("@checkVacant", checkVacant);
               cmd.Parameters.AddWithValue("@FMonth", FMonth);
               cmd.Parameters.AddWithValue("@FYear", FYear);
               cmd.CommandTimeout = 150;

               SqlDataAdapter da = new SqlDataAdapter(cmd);
               DataSet dsts = new DataSet();
               da.Fill(dsts);
               dtrowClr = dsts.Tables[0].Copy();
               //dsts.Tables[0].Columns.RemoveAt(11);
               dsts.Tables[0].Columns.RemoveAt(10);
               dsts.Tables[0].Columns.RemoveAt(9);
               dsts.Tables[0].Columns.RemoveAt(8);
               dsts.Tables[0].Columns.RemoveAt(7);
               dsts.Tables[0].Columns.RemoveAt(1);


               Grdprd.DataSource = dsts;
               Grdprd.DataBind();
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

        if (type == "1")
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

                GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell objtablecel2 = new TableCell();


                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Joining Date", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                // AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Delayed Dates", "#0097AC", true);




                //int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                //int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());

                //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //int cmonth = Convert.ToInt32(FMonth);
                //int cyear = Convert.ToInt32(FYear);

                //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

                SalesForce sf = new SalesForce();

                //for (int i = 0; i <= months; i++)
                //{
                //iLstMonth.Add(cmonth);
                //iLstYear.Add(cyear);


                if (divcode == "104" )
                {

                    int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
                    string sTxt = "&nbsp;" + sf.getMonthName(FMonth) + "-" + FYear;

                  
                        AddMergedCells(objgridviewrow, objtablecell, 0, days * 3, sTxt, "#0097AC", true);
                    
                   


                    for (int i = 1; i <= days; i++)
                    {
                      
                            AddMergedCells(objgridviewrow1, objtablecell1, 0, 3, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " SD ", "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " Drs ", "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "Chem", "#0097AC", true);
                            //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "UDr", "#0097AC", true);
                        
                    }
                }
                else
                {
                    int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
                    string sTxt = "&nbsp;" + sf.getMonthName(FMonth) + "-" + FYear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, days * 2, sTxt, "#0097AC", true);



                    for (int i = 1; i <= days; i++)
                    {
                        AddMergedCells(objgridviewrow1, objtablecell1, 0, 2, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);

                        AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " SD ", "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " Drs ", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "Chem", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "UDr", "#0097AC", true);
                    }
                }


                //cmonth = cmonth + 1;

                //if (cmonth == 13)
                //{
                //    cmonth = 1;
                //    cyear = cyear + 1;
                //}

                // }
                //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Total", "#0097AC", true);

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No of Days <br> Present", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TL &nbsp;&nbsp;", "#0097AC", true);

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TH &nbsp;&nbsp;", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TW &nbsp;&nbsp;", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TD &nbsp;&nbsp;", "#0097AC", true);



                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
                //
                #endregion
                //
            }
        }
        else if (type == "2")
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
                GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell objtablecel2 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Joining Date", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                // AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Delayed Dates", "#0097AC", true);





                SalesForce sf = new SalesForce();


                int months = (Convert.ToInt32(dtDCRto.Year) - Convert.ToInt32(dtDCRfrom.Year)) * 12 + Convert.ToInt32(dtDCRto.Month) - Convert.ToInt32(dtDCRfrom.Month); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(dtDCRfrom.Month);
                int cyear = Convert.ToInt32(dtDCRfrom.Year);

                //int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                //int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


                int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));

                var firstDayOfMonth = new DateTime(dtDCRto.Year, dtDCRto.Month, 1);


                DateTime firstOfNextMonth = new DateTime(dtDCRfrom.Year, dtDCRfrom.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

                TimeSpan difference = lastOfThisMonth - dtDCRfrom;
                var dayss = difference.TotalDays;

                TimeSpan nextmonthdiff = dtDCRto - firstDayOfMonth;

                var nextmonthsdays = nextmonthdiff.TotalDays;

                int tat_nxt = Convert.ToInt16(nextmonthsdays)+1;

                int daaaaay =Convert.ToInt16(dayss)+1;

                for (int j = 0; j <= months; j++)
                {
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    if (dtDCRfrom.Month == cmonth)
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 0, daaaaay * 2, sTxt, "#0097AC", true);
                    }
                    else if (dtDCRto.Month == cmonth)
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 0, tat_nxt * 2, sTxt, "#0097AC", true);
                    }

                    if (dtDCRfrom.Month == cmonth)
                    {

                        for (int i = dtDCRfrom.Day; i <= days; i++)
                        {
                            AddMergedCells(objgridviewrow1, objtablecell1, 0, 2, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);

                            AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " SD ", "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " Drs ", "#0097AC", true);

                            first += 1;
                            //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "Chem", "#0097AC", true);
                            //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "UDr", "#0097AC", true);
                        }
                    }

                    if (dtDCRfrom.Month != dtDCRto.Month)
                    {

                        if (dtDCRto.Month == cmonth)
                        {
                            for (int i = 1; i <= dtDCRto.Day; i++)
                            {
                                AddMergedCells(objgridviewrow1, objtablecell1, 0, 2, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);

                                AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " SD ", "#0097AC", true);
                                AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, " Drs ", "#0097AC", true);

                                first += 1;
                                //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "Chem", "#0097AC", true);
                                //AddMergedCells(objgridviewrow2, objtablecel2, 0, 0, "UDr", "#0097AC", true);
                            }
                        }
                    }

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }


                //int to_days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRto.Year), Convert.ToInt16(dtDCRto.Month));


                //if (dtDCRfrom.Month != dtDCRto.Month)
                //{

                //    for (int i = 1; i <= dtDCRto.Day; i++)
                //    {

                //        AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);

                //    }
                //}

                // int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));
                //string sTxt = "&nbsp;" + sf.getMonthName(dtDCRfrom.Month.ToString()) + "-" + dtDCRfrom.Year;
                //AddMergedCells(objgridviewrow, objtablecell, 0, first, sTxt, "#0097AC", true);



                //for (int i = 1; i <= days; i++)
                //{
                //    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);
                //}


                //cmonth = cmonth + 1;

                //if (cmonth == 13)
                //{
                //    cmonth = 1;
                //    cyear = cyear + 1;
                //}

                // }

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No of Days <br> Present", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TL &nbsp;&nbsp;", "#0097AC", true);

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TH &nbsp;&nbsp;", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TW &nbsp;&nbsp;", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "&nbsp;&nbsp; TD &nbsp;&nbsp;", "#0097AC", true);


                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
                //
                #endregion
                //
            }
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
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
            #endregion
            //
            #region Merge cells

            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Employee id", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Joining Date", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            // AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Delayed Dates", "#0097AC", true);




            //int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            //int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());

            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

            SalesForce sf = new SalesForce();

            //for (int i = 0; i <= months; i++)
            //{
            //iLstMonth.Add(cmonth);
            //iLstYear.Add(cyear);

            //int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));

            //for (int i = dtDCRfrom.Day; i <= days; i++)
            //{

            //    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);

            //}

            int to_days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRto.Year), Convert.ToInt16(dtDCRto.Month));


            if (dtDCRfrom.Month != dtDCRto.Month)
            {

                for (int i = 1; i <= dtDCRto.Day; i++)
                {

                    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);
                    second += 1;
                }
            }

            // int days = DateTime.DaysInMonth(Convert.ToInt16(dtDCRfrom.Year), Convert.ToInt16(dtDCRfrom.Month));
            string sTxt = "&nbsp;" + sf.getMonthName(dtDCRto.Month.ToString()) + "-" + dtDCRto.Year;
            AddMergedCells(objgridviewrow, objtablecell, 0, second, sTxt, "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "No of Days <br> Present", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TL &nbsp;&nbsp;", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TH &nbsp;&nbsp;", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TW &nbsp;&nbsp;", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TD &nbsp;&nbsp;", "#0097AC", true);





            //for (int i = 1; i <= days; i++)
            //{
            //    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "&nbsp;&nbsp; &nbsp;" + i.ToString() + "&nbsp;&nbsp;&nbsp;", "#0097AC", true);
            //}


            //cmonth = cmonth + 1;

            //if (cmonth == 13)
            //{
            //    cmonth = 1;
            //    cyear = cyear + 1;
            //}

            // }
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Total", "#0097AC", true);

            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "No of Days <br> Present", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TL &nbsp;&nbsp;", "#0097AC", true);

            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TH &nbsp;&nbsp;", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TW &nbsp;&nbsp;", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "&nbsp;&nbsp; TD &nbsp;&nbsp;", "#0097AC", true);



            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
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
        if (objgridviewrow.RowIndex == 1 && rowSpan == 0 && colspan == 0)
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
        }
        else if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }

        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

   

    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (type == "1")
        {

            int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                string sf_tp_active_flag = dtrowClr.Rows[iInx][8].ToString();
                if (sf_tp_active_flag == "1")
                {
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ToolTip = "Vacant";
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text + " ( Resigned )";
                }
                for (int i = 6, j = 1; i < e.Row.Cells.Count; i++, j++)
                {


                  
                    if (e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "&nbsp;")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sdate = dtrowClr.Rows[iInx][11].ToString();
                        string sSf_Name = dtrowClr.Rows[iInx][4].ToString();

                        //getDisplayPlaceOfWork(sSf_Name);

                        //  bool isBracketed = sSf_Name.StartsWith("(") && sSf_Name.EndsWith(")");

                        //bool isBracketed;

                        //if (sSf_Name.StartsWith("(") && sSf_Name.EndsWith(")"))
                        //{

                        //    string p1 = sSf_Name.Substring(0, sSf_Name.IndexOf('(')) + "<span style=\"background-color:yellow\">" + type + "</span>";
                        //}

                        DateTime dt = Convert.ToDateTime(dtrowClr.Rows[iInx][3].ToString());
                        int month = dt.Month;
                        int year = dt.Year;

                        if ((month == Convert.ToInt16(FMonth)) && (year == Convert.ToInt16(FYear)))
                        {

                            e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                        }



                        //e.Row.Cells[3].BackColor = System.Drawing.Color.Chocolate;

                        //Label name = dtrowClr.Rows[iInx][4].ToString();

                        // hLink.Style.Add("CSS", "blink_me");
                        //e.Row.Cells[3].CssClass = "blink_me";

                        //  e.Row.CssClass = "blink_me"; 

                        e.Row.Attributes.Add("class", "blink_me");

                        string[] sf_namee = sSf_Name.Split('/');
                        //int cMnth = iLstMonth[j];
                        //int cYr = iLstYear[j];

                        //sf_namee[1] = Color.Black;



                        bool check = false;

                        //if (sdate != "-")
                        //{

                        //    //ContainsInt(sdate, j);

                        //    string[] a1_values = sdate.Split(',');


                        //    foreach (string item in a1_values)
                        //    {
                        //        if (item == j.ToString())
                        //        {

                        //            check = true;
                        //            //hLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                        //            // e.Row.Cells[i].Attributes.Add("BackColor", "#eee8aa");
                        //            e.Row.Cells[i].BackColor = System.Drawing.Color.PaleGoldenrod;
                        //        }
                        //        else
                        //        {

                        //            check = false;

                        //        }

                        //    }
                        //}




                        e.Row.Cells[i].Attributes.Add("align", "center");
                        e.Row.Cells[i].Attributes.Add("width", "500");
                        // int day = j;

                        //string input = "hello123world";
                        //bool isDigitPresent = input.Any(c => char.IsDigit(c));


                        //DataSet dsDelay = new DataSet();
                        //DCR dc = new DCR();
                        //dsDelay = dc.get_DCR_Status_Delay(sSf_code,day.ToString(),Convert.ToInt16(FMonth),Convert.ToInt16(FYear));
                        //if (dsDelay.Tables[0].Rows.Count > 0)
                        //{
                        //    hLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                        //}



                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                        {
                            e.Row.Cells[i].Text = "";
                        }

                        e.Row.Cells[i].Controls.Add(hLink);
                        tot = hLink.Text;

                        int day = j;

                        if (tot != "L" && tot != "LP" && tot != "NA" && tot != "D" && tot != "DR" && tot != "MD" && tot != "MR" && tot != "TD" && tot != "TDR")
                        {
                            if (chkatten == "1")
                            {
                                hLink.Text = "P";
                                e.Row.Cells[i].Controls.Add(hLink);
                            }
                            total += 1;
                        }

                        else if (tot == "L" || tot == "LP")
                        {
                            if (chkatten == "1")
                            {
                                hLink.Text = "A";
                                e.Row.Cells[i].Controls.Add(hLink);
                                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                            }

                            total2 += 1;
                        }

                        if (tot == "H")
                        {
                            totHoli += 1;
                        }

                        if (tot == "WO")
                        {
                            totWeek += 1;
                        }
                        if (tot == "D" || tot == "DR")
                        {
                            totDel += 1;
                            e.Row.Cells[i].BackColor = System.Drawing.Color.MistyRose;
                        }
                        if (tot == "MD" || tot == "MR" || tot == "TD" || tot == "TDR")
                        {

                            e.Row.Cells[i].BackColor = System.Drawing.Color.MistyRose;
                        }




                    }

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-" || e.Row.Cells[i].Text == "888")
                    {
                        e.Row.Cells[i].Text = "";
                    }

                    //e.Row.Cells[1].Wrap = false;
                    //e.Row.Cells[2].Wrap = false;
                    //e.Row.Cells[3].Wrap = false;
                    //e.Row.Cells[4].Wrap = false;
                    //e.Row.Cells[5].Wrap = false;
                    //e.Row.Cells[6].Wrap = false;
                    //e.Row.Cells[7].Wrap = false;
                    //e.Row.Cells[8].Wrap = false;
                }


                //TableCell tblRow_Count = new TableCell();

                //tblRow_Count.Text = total.ToString();
                //if (tblRow_Count.Text == "0")
                //{
                //    tblRow_Count.Text = "";
                //}
                //tblRow_Count.HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells.Add(tblRow_Count);

                //total = 0;

                //TableCell tblRow_Count2 = new TableCell();

                //tblRow_Count2.Text = total2.ToString();
                //if (tblRow_Count2.Text == "0")
                //{
                //    tblRow_Count2.Text = "";
                //}
                //tblRow_Count2.HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells.Add(tblRow_Count2);
                //total2 = 0;

                //TableCell tblHoliday = new TableCell();
                //tblHoliday.Text = totHoli.ToString();
                //if (tblHoliday.Text == "0")
                //{
                //    tblHoliday.Text = "";
                //}
                //tblHoliday.HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells.Add(tblHoliday);

                //totHoli = 0;

                //TableCell tblWeek = new TableCell();
                //tblWeek.Text = totWeek.ToString();
                //if (tblWeek.Text == "0")
                //{
                //    tblWeek.Text = "";
                //}
                //tblWeek.HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells.Add(tblWeek);
                //totWeek = 0;

                //TableCell tbldelay = new TableCell();
                //tbldelay.Text = totDel.ToString();
                //if (tbldelay.Text == "0")
                //{
                //    tbldelay.Text = "";
                //}
                //tbldelay.HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells.Add(tbldelay);
                //totDel = 0;


            }
        }
        else if (type == "2")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                string sf_tp_active_flag = dtrowClr.Rows[iInx][8].ToString();
                if (sf_tp_active_flag == "1")
                {
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ToolTip = "Vacant";
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text + " ( Resigned )";
                }
                for (int i = 6, j = dtDCRfrom.Day; i < e.Row.Cells.Count; i++, j++)
                {

                  
                    if (e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "&nbsp;")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sdate = dtrowClr.Rows[iInx][11].ToString();

                        DateTime dt = Convert.ToDateTime(dtrowClr.Rows[iInx][3].ToString());
                        int month = dt.Month;
                        int year = dt.Year;

                        if ((month == Convert.ToInt16(dtDCRfrom.Month)) && (year == Convert.ToInt16(dtDCRfrom.Year)))
                        {

                            e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                        }


                        //int cMnth = iLstMonth[j];
                        //int cYr = iLstYear[j];

                        bool check = false;

                        if (sdate != "-")
                        {

                            //ContainsInt(sdate, j);

                            string[] a1_values = sdate.Split(',');


                            foreach (string item in a1_values)
                            {
                                if (item == j.ToString())
                                {

                                    check = true;
                                    //hLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                                    // e.Row.Cells[i].Attributes.Add("BackColor", "#eee8aa");
                                    e.Row.Cells[i].BackColor = System.Drawing.Color.PaleGoldenrod;
                                }
                                else
                                {

                                    check = false;

                                }

                            }
                        }

                        tot = hLink.Text;
                        if (tot != "L" && tot != "LP" && tot != "NA" && tot != "D" && tot != "DR" && tot != "MD" && tot != "MR" && tot != "TD" && tot != "TDR")
                        {
                            if (chkatten == "1")
                            {
                                hLink.Text = "P";
                                e.Row.Cells[i].Controls.Add(hLink);
                            }

                        }

                        else if (tot == "L" || tot == "LP")
                        {
                            if (chkatten == "1")
                            {
                                hLink.Text = "A";
                                e.Row.Cells[i].Controls.Add(hLink);
                                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                            }


                        }
                        if (tot == "D" || tot == "DR")
                        {

                            e.Row.Cells[i].BackColor = System.Drawing.Color.MistyRose;
                        }
                        if (tot == "MD" || tot == "MR" || tot == "TD" || tot == "TDR")
                        {

                            e.Row.Cells[i].BackColor = System.Drawing.Color.MistyRose;
                        }




                        e.Row.Cells[i].Attributes.Add("align", "center");
                        e.Row.Cells[i].Attributes.Add("width", "500");





                    }

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-" || e.Row.Cells[i].Text == "888")
                    {
                        e.Row.Cells[i].Text = "";
                    }

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

    }

    private void FillWorkType()
    {
        int j = 1;

        DCR dc = new DCR();

        dsDCR = dc.DCR_get_WorkType_Forstatus(divcode);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            DataList2.DataSource = dsDCR;
            DataList2.DataBind();
        }
        else
        {
            DataList2.DataSource = dsDCR;
            DataList2.DataBind();
        }

        //TableCell tc_wt = new TableCell();
        //Literal lit_wt = new Literal();
        //TableRow tr_wt = new TableRow();
        //TableRow tr_wt2 = new TableRow();

        //foreach (DataRow drFF in dsDCR.Tables[0].Rows)
        //{
        //    //if (j <= 5)
        //    //{


        //    TableCell tc_wt = new TableCell();
        //    Literal lit_wt = new Literal();
        //    lit_wt.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";


        //    tc_wt.Controls.Add(lit_wt);
        //    tr_wt.Cells.Add(tc_wt);
        //   // tc_wt.Text = "<br />";
        //    tr_wt.Cells.Add(tc_wt);
        //    tblworktype.Rows.Add(tr_wt);
        //   // lit_wt.Controls.Add(new LiteralControl("<br />"));
        //    //}
        //    //else
        //    //{
        //    //    TableCell tc_wt2 = new TableCell();
        //    //    Literal lit_wt2 = new Literal();
        //    //    lit_wt2.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";
        //    //    tc_wt2.Controls.Add(lit_wt2);
        //    //    tr_wt2.Cells.Add(tc_wt2);
        //    //}

        //    //j = j + 1;
        //}

        //TableCell tc_wt2 = new TableCell();
        //Literal lit_wt2 = new Literal();
        //lit_wt2.Text = "LP - Leave Approval Pending";
        //tc_wt2.Style.Add("font-weight", "bold");
        //tc_wt2.Controls.Add(lit_wt2);
        //tr_wt.Cells.Add(tc_wt2);
        //tblworktype.Rows.Add(tr_wt);
        //tblworktype.Rows.Add(tr_wt2);

    }


    //visit_Month_Vertical_wise_Cat

    private string getDisplayPlaceOfWork(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                int startInx = p.IndexOf('(');

                int endInx = p.LastIndexOf(')') + 1;

                string typeee = p.Substring(startInx, endInx - startInx);
                string p1 = p.Substring(0, p.IndexOf('(')) + "<span style=\"background-color:yellow\">" + typeee + "</span>";

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        //distinctValues = distinctValues + p1;
                        distinctValues = distinctValues + "," + p1;
                    else
                        distinctValues = distinctValues + "," + p1;

                }

                else
                    distinctValues = p1;

                counter++;

            }

        }
        return distinctValues;
    }

}