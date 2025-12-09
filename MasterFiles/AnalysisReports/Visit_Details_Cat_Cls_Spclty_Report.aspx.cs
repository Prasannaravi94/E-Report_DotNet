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
using Bus_EReport;
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Text;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Coverage_New : System.Web.UI.Page
{
    //
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
    string headerRowText = string.Empty;
    string screen_name = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    List<int> iLstMonthStart = new List<int>();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    List<string> sLstCat_Spclty_Cls = new List<string>();
    List<int> iLstCat_Spclty_Cls = new List<int>();
    string sSpeciality;
    string XlsDown = string.Empty;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["Tyear"].ToString();
        sSpeciality = Request.QueryString["cbVal"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        XlsDown = Request.QueryString["XlsDown"].ToString();
       
        screen_name = "Visit_Details_Cat_Cls_Spclty_Report";
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string sHeader = "";
        string sMode = Request.QueryString["cMode"].ToString();
        string strToMonth = sf.getMonthName(TMonth);
        if (sMode == "1")
            sHeader = "Category Wise ";
        else if (sMode == "2")
            sHeader = "Speciality Wise ";
        else if (sMode == "3")
            sHeader = "Class Wise ";
        else if (sMode == "5")
            sHeader = "Campaign Wise ";

        lblHead.Text = sHeader + "Visit Details Between - " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        if (!IsPostBack)
        {
            FillSalesForce();
        }
        if (XlsDown == "0") // download excel
        { FrameDownload(); }

    }

    #endregion
    //
    #region FillSalesForce
    private void FillSalesForce()
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
        //dtMnYr.Rows.Add(null, Convert.ToInt32(Request.QueryString["FMonth"].ToString()), Convert.ToInt32(Request.QueryString["Fyear"].ToString()));

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
        string sProcName = "", sTblName = "";
        if (Request.QueryString["cMode"].ToString() == "1")
        {
            sProcName = "visit_details_Cat_total"; //visit_details_Cat //sujee
            sTblName = "@CatTbl";
        }
        else if (Request.QueryString["cMode"].ToString() == "2")
        {
            sProcName = "visit_details_Splty_total_mr"; //visit_details_Splty //sujee //visit_details_Splty_total //sujee 31 jan 2022
            sTblName = "@SpcltyTbl";
        }
        else if (Request.QueryString["cMode"].ToString() == "3")
        {
            sProcName = "visit_details_Class_total"; //visit_details_Class //sujee
            sTblName = "@ClsTbl";
        }
        else if (Request.QueryString["cMode"].ToString() == "5")
        {
            sProcName = "visit_details_Camp_total"; //visit_details_Camp //sujee
            sTblName = "@SubCatTbl";
        }
        //
        if (sProcName != "")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);

            con.Open();
            SqlCommand cmd = new SqlCommand(sProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue(sTblName, dtSpclty);
            cmd.CommandTimeout = 600;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            sMode = Request.QueryString["cMode"].ToString();
            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(7);
            //  dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            //start
            //sujee
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); 
            int temp = dsts.Tables[0].Columns.Count;
            if (months1 != 0)
            {
                dsts.Tables[0].Columns.RemoveAt(temp - 1);
                dsts.Tables[0].Columns.RemoveAt(temp - 2);
                dsts.Tables[0].Columns.RemoveAt(temp - 3);
                dsts.Tables[0].Columns.RemoveAt(temp - 4);
                dsts.Tables[0].Columns.RemoveAt(temp - 5);
            }
            //end
            dsts.Tables[0].Columns["sf_name"].SetOrdinal(1);
            dsts.Tables[0].Columns["hq"].SetOrdinal(2);
            dsts.Tables[0].Columns["desg"].SetOrdinal(3);
            dsts.Tables[0].Columns["emp_id"].SetOrdinal(4);
            dsts.Tables[0].Columns["doj"].SetOrdinal(5);
            dsts.Tables[0].Columns["firstlevel"].SetOrdinal(6);
            dsts.Tables[0].Columns["secondlevel"].SetOrdinal(7);
            GrdDoctor.DataSource = dsts;
            GrdDoctor.DataBind();
        }
    }
    #endregion
    // 
    #region GridView Header
    protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            //
            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell1 = new TableCell();
            //
            GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design Name", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Emp.Code", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "DOJ", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "First Level Manager", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Second Level Manager", "#DDEECC", true);

            string spclty = Request.QueryString["cbTxt"].ToString();
            spclty = spclty.Remove(spclty.LastIndexOf(','));
            string[] ttlSpc = spclty.Split(',');
            string sCode = Request.QueryString["cbVal"].ToString();
            sCode = sCode.Remove(sCode.LastIndexOf(','));
            string[] ttlCode = sCode.Split(',');

            int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12
                + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            SalesForce sf = new SalesForce();
            bool flag = true;
            //multimonth
            //sujee
            if (months != 0)
            {

                for (int j = 0; j <= months; j++)
                {
                    int icolspan = (ttlSpc.Length * 4) + 1;
                    iLstMonthStart.Add(icolspan);
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    string sTxt = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#DDEECC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 2, 0, "Total Lstd Drs", "#DDEECC", true);

                    for (int i = 0; i < ttlSpc.Length; i++)
                    {
                        if (flag)
                        {
                            sLstCat_Spclty_Cls.Add(ttlSpc[i].ToString());
                            iLstCat_Spclty_Cls.Add(Convert.ToInt32(ttlCode[i].ToString()));
                        }
                        AddMergedCells(objgridviewrow1, objtablecell1, 0, 4, ttlSpc[i].ToString(), "#DDEECC", true);
                    }
                    //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total", "#33FF66", true);
                    flag = false;
                    for (int i = 0; i < ttlSpc.Length; i++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "List", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Seen", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#DDEECC", false);
                    }
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }
             //end multimonth
            //singlemonth
            else if (months == 0)
            {

                for (int j = 0; j <= months; j++)
                {
                    int icolspan = (((ttlSpc.Length + 1) * 4) + 1) + 1;
                    iLstMonthStart.Add(icolspan);
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    string sTxt = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#DDEECC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 2, 0, "Total Lstd Drs", "#DDEECC", true);

                    for (int i = 0; i <= ttlSpc.Length; i++)
                    {
                        if (i == ttlSpc.Length)
                        {
                            AddMergedCells(objgridviewrow1, objtablecell1, 0, 4, "Total", "#DDEECC", true);
                        }
                        else
                        {
                            if (flag)
                            {
                                sLstCat_Spclty_Cls.Add(ttlSpc[i].ToString());
                                iLstCat_Spclty_Cls.Add(Convert.ToInt32(ttlCode[i].ToString()));
                            }
                            AddMergedCells(objgridviewrow1, objtablecell1, 0, 4, ttlSpc[i].ToString(), "#DDEECC", true);
                        }
                    }
                    //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total", "#33FF66", true);
                    flag = false;
                    for (int i = 0; i <= ttlSpc.Length; i++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "List", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Seen", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#DDEECC", false);
                    }
                    AddMergedCells(objgridviewrow1, objtablecell1, 2, 0, "Coverage %", "#DDEECC", true);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }
            //endsinglemonth
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
            //
            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowspan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowspan;
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Emp.Code" || objtablecell.Text == "DOJ" || objtablecell.Text == "First Level Manager" || objtablecell.Text == "Second Level Manager")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true, 4);
        }


        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "pinnedheaderrow1");
        }
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "pinnedheaderrow2");
        }
        else if (objgridviewrow.RowIndex == 3)
        {
            objtablecell.Attributes.Add("class", "pinnedheaderrow3");
        }

        dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, objtablecell.Text, sf_code);
        if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
        {
            if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
            {
                //objtablecell.Visible = false;
                objtablecell.Style.Add("display", "none");
            }
        }

        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //    
    #region grid doctor rowdatabound
    int index;
    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12
                + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iLstTmp = 0, iMtTmp = 0, iSnTmp = 0, iMsdTmp = 0;
            int iLst = 0, iMt = 0, iSn = 0, iMsd = 0;
            int k = e.Row.Cells.Count - 5, tmp = 0, indx = e.Row.RowIndex;

            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][7].ToString()));
            //e.Row.CssClass = "rowscrolled";
            HyperLink lnk_btn = new HyperLink();
            int iTtl = 1, l = 0;
            //multimonth
            //sujee
            if (months !=0)
            {
            for (int j = 9, inxMnth = 0, inxYr = 0, inxCode = 0; j < e.Row.Cells.Count; j += 4)
            {
                iLstTmp = Convert.ToInt32((e.Row.Cells[j].Text == "&nbsp;") || (e.Row.Cells[j].Text == "-") ? "0" : e.Row.Cells[j].Text);
                iLst += iLstTmp;
                iMtTmp = Convert.ToInt32((e.Row.Cells[j + 1].Text == "&nbsp;") || (e.Row.Cells[j + 1].Text == "-") ? "0" : e.Row.Cells[j + 1].Text);
                iMt += iMtTmp;
                iSnTmp = Convert.ToInt32((e.Row.Cells[j + 2].Text == "&nbsp;") || (e.Row.Cells[j + 2].Text == "-") ? "0" : e.Row.Cells[j + 2].Text);
                iSn += iSnTmp;
                //
                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                {
                    iMsdTmp = 0;
                }
                else
                {
                    if (iLstTmp != 0)
                        iMsdTmp = iLstTmp - iMtTmp;
                    else
                        iMsdTmp = 0;
                }
                //
                e.Row.Cells[j + 3].Text = iMsdTmp.ToString();
                iMsd += iMsdTmp;
                //
                System.Drawing.Color clr;
                if (tmp % 2 == 0)
                {
                    clr = System.Drawing.Color.LightPink;
                    tmp++;
                }
                else
                {
                    clr = System.Drawing.Color.LightGray;
                    tmp++;
                }
                e.Row.Cells[j].BackColor = clr;
                e.Row.Cells[j + 1].BackColor = clr;
                e.Row.Cells[j + 2].BackColor = clr;
                e.Row.Cells[j + 3].BackColor = clr;
                //
                if (Request.QueryString["cMode"].ToString() != "5")
                {
                        if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR" || dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR")
                        {
                            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                            {
                                if (e.Row.Cells[j].Text != "&nbsp;" && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j].Text;
                                    lnk_btn.Attributes.Add("class", "btnLstDr");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=1&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j].Controls.Add(lnk_btn);
                                }
                            }
                            //start
                            if (Request.QueryString["cMode"].ToString() != "2")
                            {
                                if (e.Row.Cells[j + 1].Text != "&nbsp;" && e.Row.Cells[j + 1].Text != "0" && e.Row.Cells[j + 1].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 1].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrMt");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=3&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Met");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 1].Controls.Add(lnk_btn);
                                }
                                //
                                if (e.Row.Cells[j + 2].Text != "&nbsp;" && e.Row.Cells[j + 2].Text != "0" && e.Row.Cells[j + 2].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 2].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrSn");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=4&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Seen");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 2].Controls.Add(lnk_btn);
                                }
                                //
                                if (e.Row.Cells[j + 3].Text != "&nbsp;" && e.Row.Cells[j + 3].Text != "0" && e.Row.Cells[j + 3].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 3].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrMsd");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=2&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Missed");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 3].Controls.Add(lnk_btn);
                                }
                            }
                            else if (Request.QueryString["cMode"].ToString() == "2")
                            {
                                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                                { 
                                    if (e.Row.Cells[j + 1].Text != "&nbsp;" && e.Row.Cells[j + 1].Text != "0" && e.Row.Cells[j + 1].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 1].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrMt");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=3&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Met");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 1].Controls.Add(lnk_btn);
                                }
                                //
                                if (e.Row.Cells[j + 2].Text != "&nbsp;" && e.Row.Cells[j + 2].Text != "0" && e.Row.Cells[j + 2].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 2].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrSn");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=4&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Seen");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 2].Controls.Add(lnk_btn);
                                }
                                //
                                if (e.Row.Cells[j + 3].Text != "&nbsp;" && e.Row.Cells[j + 3].Text != "0" && e.Row.Cells[j + 3].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j + 3].Text;
                                    lnk_btn.Attributes.Add("class", "btnDrMsd");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=2&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Missed");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j + 3].Controls.Add(lnk_btn);
                                }
                            }
                            }
                            //end
                            //sujee
                        }
            }
                //
                iTtl += 4;
                inxCode++;
                if (inxCode == iLstCat_Spclty_Cls.Count)
                {
                    inxCode = 0;
                }
                if (iLstMonthStart[l] == iTtl)
                {
                    iTtl = 1;
                    l++;
                    if (iLstMonthStart.Count != l)
                    {
                        inxMnth++;
                        inxYr++;
                        j++;
                    }
                }
            }
        }
            //endmultimonth
            //singlemonth
            if (months == 0)
            {

                for (int j = 9, inxMnth = 0, inxYr = 0, inxCode = 0; j < e.Row.Cells.Count - 5; j += 4)
                {
                    iLstTmp = Convert.ToInt32((e.Row.Cells[j].Text == "&nbsp;") || (e.Row.Cells[j].Text == "-") ? "0" : e.Row.Cells[j].Text);
                    iLst += iLstTmp;
                    iMtTmp = Convert.ToInt32((e.Row.Cells[j + 1].Text == "&nbsp;") || (e.Row.Cells[j + 1].Text == "-") ? "0" : e.Row.Cells[j + 1].Text);
                    iMt += iMtTmp;
                    iSnTmp = Convert.ToInt32((e.Row.Cells[j + 2].Text == "&nbsp;") || (e.Row.Cells[j + 2].Text == "-") ? "0" : e.Row.Cells[j + 2].Text);
                    iSn += iSnTmp;
                    //
                    if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                    {
                        iMsdTmp = 0;
                    }
                    else
                    {
                        if (iLstTmp != 0)
                            iMsdTmp = iLstTmp - iMtTmp;
                        else
                            iMsdTmp = 0;
                    }
                    //
                    e.Row.Cells[j + 3].Text = iMsdTmp.ToString();
                    iMsd += iMsdTmp;
                    //
                    System.Drawing.Color clr;
                    if (tmp % 2 == 0)
                    {
                        clr = System.Drawing.Color.LightPink;
                        tmp++;
                    }
                    else
                    {
                        clr = System.Drawing.Color.LightGray;
                        tmp++;
                    }
                    e.Row.Cells[j].BackColor = clr;
                    e.Row.Cells[j + 1].BackColor = clr;
                    e.Row.Cells[j + 2].BackColor = clr;
                    e.Row.Cells[j + 3].BackColor = clr;
                    //
                    if (Request.QueryString["cMode"].ToString() != "5")
                    {
                        if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR" || dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR")
                        {
                            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                            {
                                if (e.Row.Cells[j].Text != "&nbsp;" && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                                {
                                    lnk_btn = new HyperLink();
                                    lnk_btn.Text = e.Row.Cells[j].Text;
                                    lnk_btn.Attributes.Add("class", "btnLstDr");
                                    lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                        "&typ=1&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                    lnk_btn.Font.Underline = false;
                                    lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                                    lnk_btn.ForeColor = System.Drawing.Color.Black;
                                    e.Row.Cells[j].Controls.Add(lnk_btn);
                                }
                            }
                            //
                            //start
                            if (Request.QueryString["cMode"].ToString() != "2")
                            { 
                                if (e.Row.Cells[j + 1].Text != "&nbsp;" && e.Row.Cells[j + 1].Text != "0" && e.Row.Cells[j + 1].Text != "-")
                            {
                                lnk_btn = new HyperLink();
                                lnk_btn.Text = e.Row.Cells[j + 1].Text;
                                lnk_btn.Attributes.Add("class", "btnDrMt");
                                lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                    "&typ=3&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                lnk_btn.Font.Underline = false;
                                lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Met");
                                lnk_btn.ForeColor = System.Drawing.Color.Black;
                                e.Row.Cells[j + 1].Controls.Add(lnk_btn);
                            }
                            //
                            if (e.Row.Cells[j + 2].Text != "&nbsp;" && e.Row.Cells[j + 2].Text != "0" && e.Row.Cells[j + 2].Text != "-")
                            {
                                lnk_btn = new HyperLink();
                                lnk_btn.Text = e.Row.Cells[j + 2].Text;
                                lnk_btn.Attributes.Add("class", "btnDrSn");
                                lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                    "&typ=4&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                lnk_btn.Font.Underline = false;
                                lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Seen");
                                lnk_btn.ForeColor = System.Drawing.Color.Black;
                                e.Row.Cells[j + 2].Controls.Add(lnk_btn);
                            }
                            //
                            if (e.Row.Cells[j + 3].Text != "&nbsp;" && e.Row.Cells[j + 3].Text != "0" && e.Row.Cells[j + 3].Text != "-")
                            {
                                lnk_btn = new HyperLink();
                                lnk_btn.Text = e.Row.Cells[j + 3].Text;
                                lnk_btn.Attributes.Add("class", "btnDrMsd");
                                lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                    "&typ=2&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                lnk_btn.Font.Underline = false;
                                lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Missed");
                                lnk_btn.ForeColor = System.Drawing.Color.Black;
                                e.Row.Cells[j + 3].Controls.Add(lnk_btn);
                            }
                            }
                            else if (Request.QueryString["cMode"].ToString() == "2")
                            {
                                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                                {
                                    if (e.Row.Cells[j + 1].Text != "&nbsp;" && e.Row.Cells[j + 1].Text != "0" && e.Row.Cells[j + 1].Text != "-")
                                    {
                                        lnk_btn = new HyperLink();
                                        lnk_btn.Text = e.Row.Cells[j + 1].Text;
                                        lnk_btn.Attributes.Add("class", "btnDrMt");
                                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                            "&typ=3&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                        lnk_btn.Font.Underline = false;
                                        lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Met");
                                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                                        e.Row.Cells[j + 1].Controls.Add(lnk_btn);
                                    }
                                    //
                                    if (e.Row.Cells[j + 2].Text != "&nbsp;" && e.Row.Cells[j + 2].Text != "0" && e.Row.Cells[j + 2].Text != "-")
                                    {
                                        lnk_btn = new HyperLink();
                                        lnk_btn.Text = e.Row.Cells[j + 2].Text;
                                        lnk_btn.Attributes.Add("class", "btnDrSn");
                                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                            "&typ=4&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                        lnk_btn.Font.Underline = false;
                                        lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Seen");
                                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                                        e.Row.Cells[j + 2].Controls.Add(lnk_btn);
                                    }
                                    //
                                    if (e.Row.Cells[j + 3].Text != "&nbsp;" && e.Row.Cells[j + 3].Text != "0" && e.Row.Cells[j + 3].Text != "-")
                                    {
                                        lnk_btn = new HyperLink();
                                        lnk_btn.Text = e.Row.Cells[j + 3].Text;
                                        lnk_btn.Attributes.Add("class", "btnDrMsd");
                                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][4].ToString() + "&cMnth=" + iLstMonth[inxMnth] + "&cYr=" + iLstYear[inxYr] + "&cTyp_cd=" + iLstCat_Spclty_Cls[inxCode] +
                                            "&typ=2&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                                        lnk_btn.Font.Underline = false;
                                        lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "Missed");
                                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                                        e.Row.Cells[j + 3].Controls.Add(lnk_btn);
                                    }
                                }
                            }
                        }
                    }
                    //
                    iTtl += 4;
                    inxCode++;

                    if (inxCode == iLstCat_Spclty_Cls.Count + 1)
                    {
                        inxCode = 0;
                    }

                    if (iLstMonthStart[l] == iTtl)
                    {
                        iTtl = 1;
                        l++;
                        if (iLstMonthStart.Count != l)
                        {
                            inxMnth++;
                            inxYr++;
                            j++;
                        }
                    }
                }
            }
            //endsinglemonth
           //singlemonthtotal
            //single month total
            if (months == 0)
            {
                iTtl = 1; l = 0;
                int totallist = 0, totalmet = 0, totalseen = 0, totalmissed = 0;
                // int totalmet1=0, totallist1=0;
                for (int i = 9, inxMnth = 0, inxYr = 0, inxCode = 0; i < e.Row.Cells.Count - 5; i += 4)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                        {
                            totallist = totallist + Convert.ToInt32(e.Row.Cells[i].Text);
                        }
                        //else
                        //{
                        //    e.Row.Cells[i].Text = "0";
                        //}

                        if (e.Row.Cells[i + 1].Text != "&nbsp;" && e.Row.Cells[i + 1].Text != "0" && e.Row.Cells[i + 1].Text != "-")
                        {
                            totalmet = totalmet + Convert.ToInt32(e.Row.Cells[i + 1].Text);
                        }
                        //else
                        //    totalmet = 0;
                        if (e.Row.Cells[i + 2].Text != "&nbsp;" && e.Row.Cells[i + 2].Text != "0" && e.Row.Cells[i + 2].Text != "-")
                        {
                            totalseen = totalseen + Convert.ToInt32(e.Row.Cells[i + 2].Text);
                        }
                        //else
                        //    totalseen = 0;
                        if (e.Row.Cells[i + 3].Text != "&nbsp;" && e.Row.Cells[i + 3].Text != "0" && e.Row.Cells[i + 3].Text != "-")
                        {
                            totalmissed = totalmissed + Convert.ToInt32(e.Row.Cells[i + 3].Text);
                        }
                        //else
                        //    totalmissed = 0;
                    }
                    if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                    {
                        e.Row.Cells[e.Row.Cells.Count - 5].Text = totallist.ToString();
                        Label mylbl = new Label();
                        mylbl.Text = e.Row.Cells[e.Row.Cells.Count - 5].Text;

                        e.Row.Cells[e.Row.Cells.Count - 4].Text = totalmet.ToString();
                        Label mylbl1 = new Label();
                        mylbl1.Text = e.Row.Cells[e.Row.Cells.Count - 4].Text;

                        e.Row.Cells[e.Row.Cells.Count - 3].Text = totalseen.ToString();
                        Label mylbl2 = new Label();
                        mylbl1.Text = e.Row.Cells[e.Row.Cells.Count - 3].Text;

                        e.Row.Cells[e.Row.Cells.Count - 2].Text = totalmissed.ToString();
                        Label mylbl3 = new Label();
                        mylbl1.Text = e.Row.Cells[e.Row.Cells.Count - 2].Text;
                    }
                    if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR" || dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR")
                    {
                        if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                        {
                            //
                            e.Row.Cells[e.Row.Cells.Count - 5].Text = totallist.ToString();
                            //
                            e.Row.Cells[e.Row.Cells.Count - 4].Text = totalmet.ToString();
                            //
                            e.Row.Cells[e.Row.Cells.Count - 3].Text = totalseen.ToString();
                           //
                            e.Row.Cells[e.Row.Cells.Count - 2].Text = totalmissed.ToString();
                            //
                        }
                    }

                    iTtl += 4;
                    inxCode++;
                    if (inxCode == iLstCat_Spclty_Cls.Count + 1)
                    {
                        inxCode = 0;
                    }
                    if (iLstMonthStart[l] == iTtl)
                    {
                        iTtl = 1;
                        l++;
                        if (iLstMonthStart.Count != l)
                        {
                            inxMnth++;
                            inxYr++;
                            i++;
                        }
                    }


                }

                for (int j = e.Row.Cells.Count - 5; j < e.Row.Cells.Count - 3; j++)
                {
                    if (e.Row.Cells[j].Text == "&nbsp;" && e.Row.Cells[j].Text == "0" && e.Row.Cells[j].Text == "-" && e.Row.Cells[j].Text == "")
                    {
                        e.Row.Cells[j].Text = "0";
                    }
                }
                double coverage = 0;

                for (int j = e.Row.Cells.Count - 5, list1 = 0, met1 = 0; j < e.Row.Cells.Count - 4; j++)
                {

                    met1 = Convert.ToInt32(e.Row.Cells[j + 1].Text);
                    list1 = Convert.ToInt32(e.Row.Cells[j].Text);
                    if (list1 == 0 && met1 == 0)
                    {
                        coverage = 0;
                    }
                    else
                    {

                        coverage = Math.Round((Convert.ToDouble(met1)) / (Convert.ToDouble(list1)) * 100, 2);
                    }
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = coverage.ToString();
                    Label mylbl0 = new Label();
                    mylbl0.Text = e.Row.Cells[e.Row.Cells.Count - 1].Text;

                }


            }

            //endtotal
            for (int i = 8; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
         
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
        }

        Chemist chem = new Chemist();

        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
        if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("column_name"),
                             Ch_Code = data.Field<string>("column_name")
                         };
            var listOfGrades = result.ToList();
            cblGridColumnList.Visible = true;
            cblGridColumnList.DataSource = listOfGrades;
            cblGridColumnList.DataTextField = "Ch_Name";
            cblGridColumnList.DataValueField = "Ch_Code";
            cblGridColumnList.DataBind();

            string headerText = string.Empty;

            for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
            {
                headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

                ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                if (ddl != null)
                {
                    if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                    }
                    else
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                    }
                    //if (headerText == "Emp.Code" || headerText == "FieldForce Name" || headerText == "Designation Name")
                    //{
                    //    cblGridColumnList.Items.FindByValue(headerText).Enabled = false;
                    //}
                }

                if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                {
                    int j = i + 4;

                    e.Row.Cells[j].Visible = false;
                }
            }
        }
    }

    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
    //
    private string AssignToolTip(string sMnth, string sYr, string sCode, string sType)
    {
        SalesForce sf = new SalesForce();
        string sTxt = sf.getMonthName(sMnth.ToString()).Substring(0, 3) + "/" + sYr.Substring(2, 2);
        return sTxt += " (" + sCode + " - " + sType + ")";
    }
    //
    #endregion
    //    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && sf_code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, sf_code);
        }

        Response.Redirect(Request.RawUrl);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Download.xls";
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

    private void FrameDownload()
    {
        string Export = this.Page.Title;
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>FrameDownload();</script>");

    }
}