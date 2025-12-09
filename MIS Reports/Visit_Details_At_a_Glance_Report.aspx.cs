//
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
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using DBase_EReport;
using System.Data.SqlClient;
#endregion
//
#region MIS_Reports_Visit_Details_Basedonfield_Level1
public partial class MIS_Reports_Visit_Details_Basedonfield_Level1 : System.Web.UI.Page
{
    //
    #region variables
    DataSet dsDoctor = null;
    DataSet dsDCR = null;
    DataSet dsmgrsf = new DataSet();
    DataTable dtMnYr = new DataTable();
    SalesForce sf = new SalesForce();

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
    string modewise = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();

    int tot_miss = 0;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblCatg = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string sCurrentDate = string.Empty;
    string sType = string.Empty;
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        modewise = Request.QueryString["modewise"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Visit Details At a Glance Between  - " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + sf_name;

        FillReport();
    }
    //
    #endregion
    //
    #region FillReport
    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;

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
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        con.Open();
        SqlCommand cmd = new SqlCommand("visit_At_a_Glance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@modewise", modewise);
        cmd.CommandTimeout = 200;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
        GrdFixationMode.DataSource = dsts;
        GrdFixationMode.DataBind();
    }
    #endregion
    //
    #region GrdFixationMode_RowCreated
    protected void GrdFixationMode_RowCreated(object sender, GridViewRowEventArgs e)
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
            if (modewise == "0")
            {
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Design", "#99FF99", true);


                int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());

                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 13, sTxt, "#99FF99", true);

                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "No Of FWD", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Ttl Drs", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Drs Met", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Once", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice & Above", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Drs Seen", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Unlist Met", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Rpt Calls", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Coverage (%)", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Call Avg", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed (%)", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Repeated (%)", "#99FF99", false);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
                //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.   
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                //
                #endregion
            }
            else
            {
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Design", "#99FF99", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Month", "#99FF99", true);


                int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());

                SalesForce sf = new SalesForce();
                
                    //string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    //AddMergedCells(objgridviewrow, objtablecell, 0, 13, sTxt, "#99FF99", true);

                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "No Of FWD", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Ttl Drs", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Drs Met", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Once", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met Twice & Above", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Drs Seen", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Unlist Met", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Rpt Calls", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Coverage (%)", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Call Avg", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed (%)", "#99FF99", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Repeated (%)", "#99FF99", false);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
               
                //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
                //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.   
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                //
                #endregion
            }
            //
        }
    }
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }

        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
    #endregion
    //    
    #region GrdFixationMode_RowDataBound
    protected void GrdFixationMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            if (modewise == "0")
            {
                #region Calculations
                //
                for (int l = 4; l < e.Row.Cells.Count; l++)
                {
                    int iDys = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                    int iTtl_Drs = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
                    int iDrs_Mt = (e.Row.Cells[l + 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 2].Text);
                    int iDrs_Sn = (e.Row.Cells[l + 5].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 5].Text);
                    int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                    if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                    {
                        iDrs_Msd = 0;
                    }
                    e.Row.Cells[l + 6].Text = iDrs_Msd.ToString();
                    int iDrs_Rpt = (e.Row.Cells[l + 8].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 8].Text);
                    if (iTtl_Drs != 0)
                    {
                        e.Row.Cells[l + 9].Text = (Decimal.Divide((iDrs_Mt * 100), iTtl_Drs)).ToString("0.##");
                        e.Row.Cells[l + 11].Text = (Decimal.Divide((iDrs_Msd * 100), iTtl_Drs)).ToString("0.##");
                        e.Row.Cells[l + 12].Text = (Decimal.Divide((iDrs_Rpt * 100), iTtl_Drs)).ToString("0.##");
                    }
                    if (iDys != 0)
                    {
                        e.Row.Cells[l + 10].Text = (Decimal.Divide(iDrs_Sn, iDys)).ToString("0.##");
                    }
                    l += 12;
                }

                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());

                for (int m = 6, n = 0; m < e.Row.Cells.Count; m++)
                {
                    if (e.Row.Cells[m - 1].Text != "0" && e.Row.Cells[m - 1].Text != "-" && e.Row.Cells[m - 1].Text != "-")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[m - 1].Text;
                        hLink.Attributes.Add("class", "btnDrMt");
                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "4_Tl_Dr" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                        hLink.ToolTip = "Click here";
                        // hLink.Font.Underline = true;
                        hLink.Attributes.Add("style", "cursor:pointer");
                        hLink.ForeColor = System.Drawing.Color.Fuchsia;
                        e.Row.Cells[m - 1].Controls.Add(hLink);
                    }

                    if (e.Row.Cells[m].Text != "0" && e.Row.Cells[m].Text != "-" && e.Row.Cells[m].Text != "-")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[m].Text;
                        hLink.Attributes.Add("class", "btnDrSn");
                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "4_Visit" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                        hLink.ToolTip = "Click here";
                        // hLink.Font.Underline = true;
                        hLink.Attributes.Add("style", "cursor:pointer");
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[m].Controls.Add(hLink);
                    }


                    if (e.Row.Cells[m + 4].Text != "0" && e.Row.Cells[m + 4].Text != "-" && e.Row.Cells[m + 4].Text != "-")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[m + 4].Text;
                        hLink.Attributes.Add("class", "btnDrMsd");
                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "4_Missed" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                        hLink.ToolTip = "Click here";
                        // hLink.Font.Underline = true;
                        hLink.Attributes.Add("style", "cursor:pointer");
                        hLink.ForeColor = System.Drawing.Color.DarkViolet;
                        e.Row.Cells[m + 4].Controls.Add(hLink);
                    }
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    m = m + 12;
                }

                for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
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
            }
            else
            {
                #region Calculations
                //
                int l = 5;
                int iDys = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int iTtl_Drs = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
                int iDrs_Mt = (e.Row.Cells[l + 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 2].Text);
                int iDrs_Sn = (e.Row.Cells[l + 5].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 5].Text);
                int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                {
                    iDrs_Msd = 0;
                }
                e.Row.Cells[l + 6].Text = iDrs_Msd.ToString();
                decimal iDrs_Rpt = (e.Row.Cells[l + 8].Text == "0") ? 0 : Convert.ToDecimal(e.Row.Cells[l + 8].Text);
                if (iTtl_Drs != 0)
                {
                    e.Row.Cells[l + 9].Text = (Decimal.Divide((iDrs_Mt * 100), iTtl_Drs)).ToString("0.##");
                    e.Row.Cells[l + 11].Text = (Decimal.Divide((iDrs_Msd * 100), iTtl_Drs)).ToString("0.##");
                    e.Row.Cells[l + 12].Text = (Decimal.Divide((iDrs_Rpt * 100), iTtl_Drs)).ToString("0.##");
                }
                if (iDys != 0)
                {
                    e.Row.Cells[l + 10].Text = (Decimal.Divide(iDrs_Sn, iDys)).ToString("0.##");
                }
                //l += 12;

                //int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                //int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
                int m = 7, n = 0;

                if (e.Row.Cells[m - 1].Text != "0" && e.Row.Cells[m - 1].Text != "-" && e.Row.Cells[m - 1].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[m - 1].Text;
                    hLink.Attributes.Add("class", "btnDrMt");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + (dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString()) + "', '" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "', '" + TMonth + "', '" + TYear + "','" + "4_Tl_Dr" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    // hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.Fuchsia;
                    e.Row.Cells[m - 1].Controls.Add(hLink);
                }

                if (e.Row.Cells[m].Text != "0" && e.Row.Cells[m].Text != "-" && e.Row.Cells[m].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[m].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" +( dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString()) + "', '" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "', '" + TMonth + "', '" + TYear + "','" + "4_Visit" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    // hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[m].Controls.Add(hLink);
                }


                if (e.Row.Cells[m + 4].Text != "0" && e.Row.Cells[m + 4].Text != "-" && e.Row.Cells[m + 4].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[m + 4].Text;
                    hLink.Attributes.Add("class", "btnDrMsd");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + (dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString()) + "', '" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "', '" + TMonth + "', '" + TYear + "','" + "4_Missed" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    // hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.DarkViolet;
                    e.Row.Cells[m + 4].Controls.Add(hLink);
                }
                //cmonth = cmonth + 1;
                //if (cmonth == 13)
                //{
                //    cmonth = 1;
                //    cyear = cyear + 1;
                //}
                //m = m + 12;

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
                e.Row.Cells[4].Text = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString()).Substring(0, 3) +
               " - " + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString();                
            }

            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
        }
        #region merge columns
        int RowSpan = 2;
        for (int i = GrdFixationMode.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = GrdFixationMode.Rows[i];
            GridViewRow prevRow = GrdFixationMode.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;
                currRow.Cells[2].RowSpan = RowSpan;
                prevRow.Cells[2].Visible = false;
                currRow.Cells[3].RowSpan = RowSpan;
                prevRow.Cells[3].Visible = false;

                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
        #endregion
    }
    #endregion
    //
}
//
#endregion