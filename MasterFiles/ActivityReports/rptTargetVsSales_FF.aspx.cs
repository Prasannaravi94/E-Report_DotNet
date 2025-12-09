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

public partial class MasterFiles_ActivityReports_rptTargetVsSales : System.Web.UI.Page
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

    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string tot = string.Empty;
    Double total;
    string mode = string.Empty;
    int Tomato2 = 0;
    int Yellow2 = 0;
    int LightGreen2 = 0;
    int LightPink2 = 0;
    int Aqua2 = 0;
    int SkyBlue2 = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        mode = Request.QueryString["mode"];
        //Stok_code = Request.QueryString["Stok_code"].ToString();
        //StName = Request.QueryString["sk_Name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        lblHead.Text = "Target Vs Primary Sales Report From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + strFieledForceName + "</span>";
        //lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";
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
        string sProc_Name = "";

        //if (Stok_code.Trim() == "-2")
        //{
        //sProc_Name = "TargetVsSales";
        if (mode == "1")
        {
            sProc_Name = "TargetVsSales_Growth";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 30000;
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else if (mode == "2")
        {
            sProc_Name = "TargetVsPrimary_Fieldforcewise";//TargetVsSales_Fieldforcewise_New
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 30000;
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);
            // dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            // dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else if (mode == "6")
        {
           // sProc_Name = "TargetVsPrimary_HQwise_New";//TargetVsPrimary_HQwise  
            sProc_Name = "TargetVsPrimary_HQwise_New_t";//TargetVsPrimary_HQwise  
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 30000;
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(10);
            //dsts.Tables[0].Columns.RemoveAt(8);
            //dsts.Tables[0].Columns.RemoveAt(7);
            // dsts.Tables[0].Columns.RemoveAt(6);
            //dsts.Tables[0].Columns.RemoveAt(5);
            //dsts.Tables[0].Columns.RemoveAt(4);

            dsts.Tables[0].Columns.Remove("cntRept");
            dsts.Tables[0].Columns.Remove("minMR");
            dsts.Tables[0].Columns.Remove("maxMR");
            dsts.Tables[0].Columns.Remove("HQ_code");

            //dsts.Tables[0].Columns.RemoveAt(5);
            // dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }


        else if (mode == "3")
        {
            Tomatocolor.Visible = true;
            Tomato.Visible = true;
            Yellowcolor.Visible = true;
            Yellow.Visible = true;
            LightGreencolor.Visible = true;
            LightGreen.Visible = true;
            LightPinkcolor.Visible = true;
            LightPink.Visible = true;
            Aquacolor.Visible = true;
            SkyBluecolor.Visible = true;


            sProc_Name = "TargetVsSales_Fieldforcewise_Dashborad";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(1);
            // dsts.Tables[0].Columns.RemoveAt(0);
            grdAppdashboard.DataSource = dsts;
            grdAppdashboard.DataBind();

            FillgridColor();
        }

        else if (mode == "4")
        {

            sProc_Name = "TargetVsSales_Hqwise_New";//AllFieldforce_withVacant_HQWise_new
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(4);
            dsts.Tables[0].Columns.RemoveAt(1);

            // dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }

        else if (mode == "5")
        {

            sProc_Name = "[TargetVsSales_Statewise]";//TargetVsSales_Statewise_New
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(2);

            // dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }


    }

    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdAppdashboard.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
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
            if (mode == "1")
            {
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "S.No", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Product Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Pack", "#0097AC", true);



                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months1; i++)
                {
                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;

                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 10, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Target", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, "Achieve (%)", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, sTxt_last + " Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, "Growth (%)", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);


                    //AddMergedCells(objgridviewrow, objtablecell2, 0, 2, "Achieve (%)", "#0097AC", true);

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
            else if (mode == "2")
            {

                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Fieldforce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Desigantion", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Pool</br>HQ Code", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Pool</br>HQ Name", "#0097AC", true);


                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);



                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months1; i++)
                {

                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 6, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Target", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, "Achieve (%)", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);






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
            }


            else if (mode == "6")
            {

                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Fieldforce Name", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Desigantion", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ Code", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Stk Count", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FF Count", "#0097AC", true);


                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);


                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months1; i++)
                {

                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 3, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Target", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Sale", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 1, "Achieve (%)", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);






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
            }

            else if (mode == "4")
            {

                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "S.No", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ Code", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ Name", "#0097AC", true);




                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);



                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months1; i++)
                {

                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 6, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Target", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, "Achieve (%)", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);






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
            }


            else if (mode == "5")
            {

                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "S.No", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "State Name", "#0097AC", true);




                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);



                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months1; i++)
                {

                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 6, sTxt, "#0097AC", true);


                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Target", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Sale", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell, 0, 2, "Achieve (%)", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);






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
            }

        }
    }
    //
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.ForeColor = System.Drawing.Color.White;
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    //protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int indx = e.Row.RowIndex;
    //        int k = e.Row.Cells.Count - 5;
    //        //

    //        float first = 0, second = 0;
    //        #region Calculations

    //        for (int l = 3, j = 0, m = 3; l < e.Row.Cells.Count; l++, m++)
    //        {

    //            if (j / 5 > 0)
    //            {

    //                if (j == 9)
    //                {
    //                    j = 0;
    //                }
    //                else
    //                {
    //                    j++;
    //                }
    //            }
    //            else
    //            {
    //                e.Row.Cells[l].BackColor = System.Drawing.Color.LightYellow;
    //                j++;
    //            }


    //            HyperLink hLink = new HyperLink();
    //            if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
    //            {

    //                hLink.Text = e.Row.Cells[l].Text;
    //                hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();




    //                if (m % 5 == 3)
    //                {
    //                    first = Convert.ToInt32(hLink.Text);
    //                }

    //                else if (m % 5 == 0)
    //                {
    //                    second = Convert.ToInt32(hLink.Text);
    //                }

    //                else if (m % 5 == 2)
    //                {
    //                    hLink.Text = (second / first * 100).ToString();
    //                    hLink.Text = Math.Round(double.Parse(hLink.Text), 0).ToString();

    //                }



    //                //if ((hLink.Text.Contains(".")))
    //                //{

    //                //}
    //                //else
    //                //{
    //                //    hLink.Text = hLink.Text + ".00";
    //                //}

    //                tot = hLink.Text;
    //                if (tot != "-" && tot != "&nbsp;")
    //                {
    //                    total += Convert.ToDouble(tot);
    //                }
    //                e.Row.Cells[l].Controls.Add(hLink);
    //            }
    //            if (e.Row.Cells[l].Text == "0")
    //            {

    //                e.Row.Cells[l].Text = "";
    //            }


    //            //first = 0;
    //            //second = 0;
    //            //j++;
    //        }




    //        e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
    //        e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";


    //        for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
    //        {


    //            e.Row.Cells[i].Attributes.Add("align", "Right");
    //        }
    //        try
    //        {
    //            int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
    //            //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
    //        }
    //        catch
    //        {
    //            e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
    //        }


    //        #endregion
    //        //

    //        bool Check = true;
    //        if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
    //        {
    //            e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
    //            e.Row.Cells[0].Text = "";
    //            for (int l = 3, j = 1; l < e.Row.Cells.Count; l++, j++)
    //            {

    //                if (j % 5 == 0 || j % 5 == 1 || j % 5 == 3)
    //                {

    //                    e.Row.Cells[l].Text = "";
    //                }

    //            }

    //        }

    //        e.Row.Cells[1].Wrap = false;
    //        e.Row.Cells[2].Wrap = false;
    //        e.Row.Cells[3].Wrap = false;



    //        //TableCell tblRow_Count = new TableCell();
    //        //tblRow_Count.Text = total.ToString();

    //        //if ((tblRow_Count.Text.Contains(".")))
    //        //{

    //        //}
    //        //else
    //        //{
    //        //    tblRow_Count.Text = tblRow_Count.Text + ".00";
    //        //}

    //        //if (tblRow_Count.Text == "0" || tblRow_Count.Text == "0.00")
    //        //{
    //        //    tblRow_Count.Text = "";
    //        //}
    //        //e.Row.Cells.Add(tblRow_Count);
    //        //tblRow_Count.Attributes.Add("align", "Right");

    //        //total = 0;
    //    }

    //}

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (mode == "1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //

                float first = 0, second = 0, third = 0, fourth = 0;
                #region Calculations

                for (int l = 3, j = 0, m = 3; l < e.Row.Cells.Count; l++, m++)
                {

                    if (j / 10 > 0)
                    {

                        if (j == 19)
                        {
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        e.Row.Cells[l].BackColor = System.Drawing.Color.LightYellow;
                        j++;
                    }


                    HyperLink hLink = new HyperLink();
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                    {


                        hLink.Text = e.Row.Cells[l].Text;
                        hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();



                        tot = hLink.Text;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToDouble(tot);
                        }

                        if (hLink.Text == "Infinity")
                        {
                            hLink.Text = "";
                        }

                        e.Row.Cells[l].Controls.Add(hLink);
                    }
                    if (e.Row.Cells[l].Text == "0")
                    {

                        e.Row.Cells[l].Text = "";
                    }

                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;



                    //first = 0;
                    //second = 0;
                    //j++;
                }




                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";


                for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
                {


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
                //

                bool Check = true;
                if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
                {
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                    for (int l = 3, j = 1; l < e.Row.Cells.Count; l++, j++)
                    {

                        //if (j % 6 == 0 || j % 6 == 1 || j % 6 == 3)
                        //{

                        //    e.Row.Cells[l].Text = "";
                        //}

                        string test = e.Row.Cells[l].Text;

                        string sValue1 = String.Format("{0:C}", test);

                        string sValue2 = String.Format("{0:#,#.}", test);

                        //e.Row.Cells[l].Controls.Add(string.Format("{0:C}", lastvalue));

                        //if (j % 6 == 1 || j % 6 == 5 || j % 6 == 3)
                        //{

                        //    e.Row.Cells[l].Text = "";
                        //}

                        if (j % 10 == 1 || j % 10 == 5 || j % 10 == 3 || j % 10 == 0 || j % 10 == 7 || j % 10 == 6 || j % 10 == 9)
                        {

                            e.Row.Cells[l].Text = "";
                        }



                    }

                }

                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
                e.Row.Cells[3].Wrap = false;



                //TableCell tblRow_Count = new TableCell();
                //tblRow_Count.Text = total.ToString();

                //if ((tblRow_Count.Text.Contains(".")))
                //{

                //}
                //else
                //{
                //    tblRow_Count.Text = tblRow_Count.Text + ".00";
                //}

                //if (tblRow_Count.Text == "0" || tblRow_Count.Text == "0.00")
                //{
                //    tblRow_Count.Text = "";
                //}
                //e.Row.Cells.Add(tblRow_Count);
                //tblRow_Count.Attributes.Add("align", "Right");

                //total = 0;
            }
        }


        else if (mode == "2")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //

                float first = 0, second = 0, third = 0, fourth = 0;


                for (int l = 6, j = 0, m = 6; l < e.Row.Cells.Count; l++, m++)
                {


                    HyperLink hLink = new HyperLink();
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                    {


                        hLink.Text = e.Row.Cells[l].Text;
                 //       hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();



                        tot = hLink.Text;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToDouble(tot);
                        }

                        if (hLink.Text == "Infinity")
                        {
                            hLink.Text = "";
                        }

                        e.Row.Cells[l].Controls.Add(hLink);
                        //int f = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[f][8].ToString()));
                    }
                    if (e.Row.Cells[l].Text == "0")
                    {

                        e.Row.Cells[l].Text = "";
                    }



                }

                for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
                {


                    //e.Row.Cells[i].Attributes.Add("align", "Right");
                }
                try
                {
                    int f = Convert.ToInt32(e.Row.Cells[0].Text);
                    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][8].ToString()));
                }
                catch
                {
                    e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }
            }
        }


        else if (mode == "6")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;

                if (e.Row.Cells[3].Text != "0" && e.Row.Cells[3].Text != "" && e.Row.Cells[3].Text != "&nbsp;" && e.Row.Cells[1].Text != "ZZZZZ")
                {
                    //HyperLink hLink = new HyperLink();
                    //hLink.Text = e.Row.Cells[3].Text;
                    //hLink.Attributes.Add("class", "btnDrSn");
                    ////hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + "" + "',  '" + "" + "', '" + "" + "', '" + "" + "', '" + ""+ "','" + "" + "','" + "" + ",'" + "" + "')");

                    //hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["sf_code"].ToString() + "',  '" + Request.QueryString["Frm_Month"].ToString() + "', '" + Request.QueryString["Frm_year"].ToString() + "', '" + Request.QueryString["To_Month"].ToString() + "', '" + Request.QueryString["To_year"].ToString() + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + ",'" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "')");
                    //hLink.ToolTip = "Click here";
                    //hLink.Attributes.Add("style", "cursor:pointer");
                    //hLink.Font.Underline = true;
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    //e.Row.Cells[3].Controls.Add(hLink);
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[3].Text;
                    if (Convert.ToInt32(dtrowClr.Rows[indx][4].ToString()) > 1)
                    {
                        hLink.Attributes.Add("class", "btnDrMt");
                    }
                    else if (Convert.ToInt32(dtrowClr.Rows[indx][5].ToString()) != Convert.ToInt32(dtrowClr.Rows[indx][6].ToString()))
                    {
                        hLink.Attributes.Add("class", "btnDrMt");
                    }
                    else
                    {
                        hLink.Attributes.Add("class", "btnDrSn");
                    }
                    //hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["sf_code"].ToString() + "',  '" + Request.QueryString["Frm_Month"].ToString() + "', '" + Request.QueryString["Frm_year"].ToString() + "', '" + Request.QueryString["To_Month"].ToString() + "', '" + Request.QueryString["To_year"].ToString() + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + ",'" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "')");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["div_code"].ToString() + "', '" + Request.QueryString["sf_code"].ToString() + "',  '" + Request.QueryString["Frm_Month"].ToString() + "', '" + Request.QueryString["Frm_year"].ToString() + "', '" + Request.QueryString["To_Month"].ToString() + "', '" + Request.QueryString["To_year"].ToString() + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + "2" + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.Font.Underline = true;
                    if (Convert.ToInt32(dtrowClr.Rows[indx][4].ToString()) > 1)
                    {
                        hLink.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (Convert.ToInt32(dtrowClr.Rows[indx][5].ToString()) != Convert.ToInt32(dtrowClr.Rows[indx][6].ToString()))
                    {
                        hLink.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        hLink.ForeColor = System.Drawing.Color.Blue;
                    }
                    e.Row.Cells[3].Controls.Add(hLink);

                    //if (Convert.ToInt32(dtrowClr.Rows[indx][4].ToString()) > 1)
                    //{
                    //    e.Row.Cells[3].Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; ");
                    //}
                }



                if (e.Row.Cells[4].Text != "0" && e.Row.Cells[4].Text != "" && e.Row.Cells[4].Text != "&nbsp;" && e.Row.Cells[1].Text != "ZZZZZ")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[4].Text;

                    hLink.Attributes.Add("class", "btnDrSn");
                    //hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["sf_code"].ToString() + "',  '" + Request.QueryString["Frm_Month"].ToString() + "', '" + Request.QueryString["Frm_year"].ToString() + "', '" + Request.QueryString["To_Month"].ToString() + "', '" + Request.QueryString["To_year"].ToString() + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + ",'" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "')");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["div_code"].ToString() + "','" + Request.QueryString["sf_code"].ToString() + "',  '" + Request.QueryString["Frm_Month"].ToString() + "', '" + Request.QueryString["Frm_year"].ToString() + "', '" + Request.QueryString["To_Month"].ToString() + "', '" + Request.QueryString["To_year"].ToString() + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + "4" + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.Font.Underline = true;
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[4].Controls.Add(hLink);
                }

                //
                if (e.Row.Cells[1].Text == "ZZZZZ")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "Grand Total";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }
                float first = 0, second = 0, third = 0, fourth = 0;

                int cnt = 0;

                string saleval = "0";
                string TargetVal = "0";

                int cmonth1 = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
                int cyear1 = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());

                for (int l = 5; l < e.Row.Cells.Count; l++)
                {
                    string value = string.Empty;

                    cnt += 1;
                    if (cnt != 3)
                    {
                        if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[2].Text == "Grand Total")
                            {
                                if (cnt == 2)
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        saleval = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");

                                        HyperLink hLink_sale = new HyperLink();
                                        hLink_sale.Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                        hLink_sale.Attributes.Add("class", "btnDrMsd");
                                        hLink_sale.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["div_code"].ToString() + "','" + Request.QueryString["sf_code"].ToString() + "',  '" + cmonth1 + "', '" + cyear1 + "', '" + cmonth1 + "', '" + cyear1 + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + "3" + "')");
                                        hLink_sale.ToolTip = "Click here";
                                        hLink_sale.Attributes.Add("style", "cursor:pointer");
                                        hLink_sale.Font.Underline = true;
                                        hLink_sale.Font.Bold = true;
                                        hLink_sale.ForeColor = System.Drawing.Color.Red;
                                        e.Row.Cells[l].Controls.Add(hLink_sale);
                                    }
                                    else
                                    {
                                        saleval = "0";
                                    }
                                }
                                else
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        TargetVal = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                        e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                    }
                                }
                                //e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.00");
                            }
                            else
                            {

                                if (cnt == 2)
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        saleval = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                    }
                                }
                                else
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        TargetVal = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                    }
                                }
                                //decimal step = (decimal)Math.Pow(10, 2);
                                //decimal tmp = Math.Truncate(step * (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000));
                                //value = (tmp / step).ToString();

                                //e.Row.Cells[l].Text = value;
                                if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                {
                                    e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                }
                            }
                            //
                        }
                    }

                    if (cnt == 3)
                    {
                        if (e.Row.Cells[l - 2].Text != "0" && e.Row.Cells[l - 2].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[2].Text == "Grand Total")
                            {
                                //e.Row.Cells[l].Text = (((Convert.ToDecimal(saleval)) / (Convert.ToDecimal(TargetVal))) * 100).ToString("0.00");
                                e.Row.Cells[l].Text = ((((Convert.ToDecimal(saleval)) / (Convert.ToDecimal(TargetVal))) * 100)).ToString("0");
                            }
                            else
                            {

                                //decimal step = (decimal)Math.Pow(10, 2);
                                //decimal tmp = Math.Truncate(step * (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100));
                                //value = (tmp / step).ToString();

                                //e.Row.Cells[l].Text = value;
                                if (Convert.ToDecimal( e.Row.Cells[l - 2].Text) != 0 )
                                {
                                    e.Row.Cells[l].Text = (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100).ToString("0");
                                }
                                //e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text)).ToString("0.00");
                            }
                        }

                        saleval = "0";
                        TargetVal = "0";
                        //l += 3;
                        cnt = 0;

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                    else
                    {
                        //  l += 1;
                    }


                }

                //for (int l = 5; l < e.Row.Cells.Count; l++)
                //{
                //    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                //    {
                //        e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.00");
                //    }
                //    if (e.Row.Cells[l - 1].Text != "0" && e.Row.Cells[l - 1].Text != "&nbsp;")
                //    {
                //        e.Row.Cells[l - 1].Text = Math.Round(double.Parse(e.Row.Cells[l - 1].Text), 0).ToString();
                //    }
                //    cnt += 1;
                //    if (cnt == 2)
                //    {

                //        if (e.Row.Cells[l + 1].Text != "0" && e.Row.Cells[l + 1].Text != "&nbsp;")
                //        {
                //            e.Row.Cells[l + 1].Text = (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 3].Text))) * 100).ToString("0.00");


                //            e.Row.Cells[l + 1].Text = (Convert.ToDecimal(e.Row.Cells[l + 1].Text)).ToString("0.00");
                //        }

                //        if (e.Row.Cells[l + 2].Text != "0" && e.Row.Cells[l + 2].Text != "&nbsp;")
                //        {
                //            e.Row.Cells[l + 2].Text = (((Convert.ToDecimal(e.Row.Cells[l].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100).ToString("0.00");

                //            e.Row.Cells[l + 2].Text = (Convert.ToDecimal(e.Row.Cells[l + 2].Text)).ToString("0.00");
                //        }

                //        l += 3;
                //        cnt = 0;
                //    }
                //    else
                //    {
                //        l += 1;
                //    }
                //}




                for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "" || e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "Right");
                }
                try
                {
                    int f = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    // e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[f][8].ToString()));
                }
                catch
                {
                    // e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }
            }
        }


        else if (mode == "4")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //

                float first = 0, second = 0, third = 0, fourth = 0;


                for (int l = 3, j = 0, m = 3; l < e.Row.Cells.Count; l++, m++)
                {
                    HyperLink hLink = new HyperLink();
                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                    {


                        hLink.Text = e.Row.Cells[l].Text;
                        hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();




                        tot = hLink.Text;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToDouble(tot);
                        }

                        if (hLink.Text == "Infinity")
                        {
                            hLink.Text = "";
                        }

                        e.Row.Cells[l].Controls.Add(hLink);

                        //int f = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[f][7].ToString()));
                    }


                    if (e.Row.Cells[l].Text == "0")
                    {

                        e.Row.Cells[l].Text = "";
                    }


                }

                for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
                {


                    //e.Row.Cells[i].Attributes.Add("align", "Right");
                }
                try
                {
                    int f = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[f][7].ToString()));
                }
                catch
                {
                    e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }

            }
        }

        else if (mode == "5")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //

                float first = 0, second = 0, third = 0, fourth = 0;
                #region Calculations

                for (int l = 2, j = 0, m = 2; l < e.Row.Cells.Count; l++, m++)
                {

                    if (j / 6 > 0)
                    {

                        if (j == 11)
                        {
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    else
                    {
                        e.Row.Cells[l].BackColor = System.Drawing.Color.LightYellow;
                        j++;
                    }


                    HyperLink hLink = new HyperLink();
                    if (e.Row.Cells[l].Text != "&nbsp;")
                    {


                        hLink.Text = e.Row.Cells[l].Text;
                        hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();
                    }


                    if (m % 6 == 2)
                    {
                        first = Convert.ToInt32(hLink.Text);
                    }

                    else if (m % 6 == 4)
                    {
                        second = Convert.ToInt32(hLink.Text);
                    }

                    else if (m % 6 == 0)
                    {
                        hLink.Text = (second / first * 100).ToString();
                        hLink.Text = Math.Round(double.Parse(hLink.Text), 0).ToString();

                    }

                    else if (m % 6 == 3)
                    {
                        third = Convert.ToInt32(hLink.Text);
                    }

                    else if (m % 6 == 5)
                    {
                        fourth = Convert.ToInt32(hLink.Text);
                    }


                    else if (m % 6 == 1)
                    {
                        hLink.Text = (fourth / third * 100).ToString();
                        hLink.Text = Math.Round(double.Parse(hLink.Text), 0).ToString();

                    }




                    //if ((hLink.Text.Contains(".")))
                    //{

                    //}
                    //else
                    //{
                    //    hLink.Text = hLink.Text + ".00";
                    //}

                    tot = hLink.Text;
                    if (tot != "-" && tot != "&nbsp;")
                    {
                        total += Convert.ToDouble(tot);
                    }

                    if (hLink.Text == "Infinity" || hLink.Text == "NaN" || hLink.Text == "0")
                    {
                        hLink.Text = "";
                    }



                    e.Row.Cells[l].Controls.Add(hLink);



                    //first = 0;
                    //second = 0;
                    //j++;
                }




                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";


                for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
                {


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

    }

    protected void grdAppdashboard_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //hLink.Text = e.Row.Cells[l].Text;
            //hLink.Text = Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString();

            Label lblvalue = (Label)e.Row.FindControl("lblvalue");
            Label lbl1 = (Label)e.Row.FindControl("lbl1");
            Label lbl2 = (Label)e.Row.FindControl("lbl2");
            Label lbl3 = (Label)e.Row.FindControl("lbl3");
            Label lbl4 = (Label)e.Row.FindControl("lbl4");
            Label lbl5 = (Label)e.Row.FindControl("lbl5");
            Label lbl6 = (Label)e.Row.FindControl("lbl6");
            //Label lbl7 = (Label)e.Row.FindControl("lbl7");
            //Label lbl8 = (Label)e.Row.FindControl("lbl8");
            //Label lbl9 = (Label)e.Row.FindControl("lbl9");
            //Label lbl10 = (Label)e.Row.FindControl("lbl10");
            //Label lbl11 = (Label)e.Row.FindControl("lbl11");

            e.Row.Cells[5].BackColor = System.Drawing.Color.White;
            e.Row.Cells[6].BackColor = System.Drawing.Color.White;
            e.Row.Cells[7].BackColor = System.Drawing.Color.White;
            e.Row.Cells[8].BackColor = System.Drawing.Color.White;
            e.Row.Cells[9].BackColor = System.Drawing.Color.White;
            e.Row.Cells[10].BackColor = System.Drawing.Color.White;
            //e.Row.Cells[11].BackColor = System.Drawing.Color.White;
            //e.Row.Cells[12].BackColor = System.Drawing.Color.White;
            //e.Row.Cells[13].BackColor = System.Drawing.Color.White;
            //e.Row.Cells[14].BackColor = System.Drawing.Color.White;
            //e.Row.Cells[15].BackColor = System.Drawing.Color.White;

            if (lblvalue.Text != "")
            {

                lblvalue.Text = Math.Round(double.Parse(lblvalue.Text), 0).ToString();

                if (Convert.ToInt16(lblvalue.Text) <= 85)
                {
                    lbl1.Text = lblvalue.Text;

                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;

                    Tomato2 += 1;
                }

                if (Convert.ToInt16(lblvalue.Text) >= 86 && Convert.ToInt16(lblvalue.Text) <= 90)
                {
                    lbl2.Text = lblvalue.Text;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                    Yellow2 += 1;
                }

                if (Convert.ToInt16(lblvalue.Text) >= 91 && Convert.ToInt16(lblvalue.Text) <= 95)
                {
                    lbl3.Text = lblvalue.Text;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    LightGreen2 += 1;
                }

                if (Convert.ToInt16(lblvalue.Text) >= 96 && Convert.ToInt16(lblvalue.Text) <= 100)
                {
                    lbl4.Text = lblvalue.Text;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.LightPink;
                    LightPink2 += 1;
                }

                if (Convert.ToInt16(lblvalue.Text) >= 101 && Convert.ToInt16(lblvalue.Text) <= 105)
                {
                    lbl5.Text = lblvalue.Text;
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Aqua;
                    Aqua2 += 1;
                }

                if (Convert.ToInt16(lblvalue.Text) > 105)
                {
                    lbl6.Text = lblvalue.Text;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.SkyBlue;
                    SkyBlue2 += 1;
                }

                //if (Convert.ToInt16(lblvalue.Text) >= 61 && Convert.ToInt16(lblvalue.Text) <= 70)
                //{
                //    lbl7.Text = lblvalue.Text;
                //    e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
                //    Yellow2 += 1;
                //}

                //if (Convert.ToInt16(lblvalue.Text) >= 71 && Convert.ToInt16(lblvalue.Text) <= 80)
                //{
                //    lbl8.Text = lblvalue.Text;
                //    e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                //    Yellow2 += 1;
                //}

                //if (Convert.ToInt16(lblvalue.Text) >= 81 && Convert.ToInt16(lblvalue.Text) <= 90)
                //{
                //    lbl9.Text = lblvalue.Text;
                //    e.Row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                //    LightGreen2 += 1;
                //}

                //if (Convert.ToInt16(lblvalue.Text) >= 91 && Convert.ToInt16(lblvalue.Text) <= 100)
                //{
                //    lbl10.Text = lblvalue.Text;
                //    e.Row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                //    LightGreen2 += 1;
                //}

                //if (Convert.ToInt16(lblvalue.Text) > 100)
                //{
                //    lbl11.Text = lblvalue.Text;
                //    e.Row.Cells[15].BackColor = System.Drawing.Color.LightPink;
                //    LightPink2 += 1;
                //}

                Tomato.Text = Tomato2.ToString() + " ( Below 85 % )";
                Yellow.Text = Yellow2.ToString() + " ( 86 - 90 % )";
                LightGreen.Text = LightGreen2.ToString() + " ( 91 - 95 % )";
                LightPink.Text = LightPink2.ToString() + " ( 96 - 100 % )";
                Aqua.Text = Aqua2.ToString() + " ( 101 - 105 % )";
                SkyBlue.Text = SkyBlue2.ToString() + " ( Above 105 % )";
            }


        }
    }

    protected void grdAppdashboard_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView objGridView = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#8FBC8F", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#8FBC8F", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#8FBC8F", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#8FBC8F", true);


            AddMergedCells(objgridviewrow, objtablecell, 6, "Achievement (%) Value", "#8FBC8F", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "  < 85 ", "#D6E9C6", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, " 86 - 90	", "#D6E9C6", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, " 91 - 95 ", "#D6E9C6", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, " 96 - 100	", "#D6E9C6", false);

            AddMergedCells(objgridviewrow1, objtablecell1, 0, "	101 - 105 ", "#D6E9C6", false);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, " 51 - 60 ", "#D6E9C6", false);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, " 61 - 70 ", "#D6E9C6", false);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, " 71 - 80 ", "#D6E9C6", false);

            //AddMergedCells(objgridviewrow1, objtablecell1, 0, " 81 - 90 ", "#D6E9C6", false);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, " 91 - 100 ", "#D6E9C6", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, " > 105 ", "#D6E9C6", false);


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
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
        objtablecell.Style.Add("color", "black");
        objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }


}