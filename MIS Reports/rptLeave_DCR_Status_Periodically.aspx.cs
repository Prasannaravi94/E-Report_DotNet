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

public partial class MIS_Reports_rptLeave_DCR_Status_Periodically : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;

    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    string Detailed = string.Empty;
    DataSet dsSales = new DataSet();
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int total = 0;
    int total2 = 0;
    int total3 = 0;
    int total4 = 0;
    int fulltotal = 0;
    string tot = string.Empty;
    DateTime Fdate2;
    DateTime Tdate2;
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        Fdate = Request.QueryString["txtEffFrom"].ToString();
        Tdate = Request.QueryString["txtEffTo"].ToString();
   
        sfname = Request.QueryString["sf_name"].ToString();
        Detailed = Request.QueryString["Detailed"].ToString();
        lblRegionName.Text = sfname;
        //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);



       

        Fdate2 = Convert.ToDateTime(Fdate);
        Tdate2 = Convert.ToDateTime(Tdate);


        lblHead.Text = "Leave Status between " + "<span style='color:#0077FF'>" + Fdate2.ToString("MMM") + " " + Fdate2.Day + " " + Fdate2.Year + "</span>" + " To " + " " + "<span style='color:#0077FF'>" + Tdate2.ToString("MMM") + " " +Tdate2.Day + " " + Tdate2.Year + "</span>";

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        screen_name = "rptLeave_DCR_Status_Periodically";
        if (!Page.IsPostBack)
        {
            FillSample_Prd();
        }
        //if (Detailed == "1")
        //{
        //    FillSF_Leave_Type();
        //}
        //else
        //{
        //    FillSF();
        //}   

    }

    private void FillSample_Prd()
    {
        //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        //int cmonth = Convert.ToInt32(FMonth);
        //int cyear = Convert.ToInt32(FYear);


        //int iMn = 0; int iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        //dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));

        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        if (Detailed == "1")
        {
            SqlCommand cmd = new SqlCommand("Leave_Status_DCR_withtype_Periodically", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            //cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("Fdate", Fdate2.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Tdate", Tdate2.ToString("MM/dd/yyyy"));
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(9);
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);
            dsts.Tables[0].Columns.RemoveAt(1);
            dsts.Tables[0].Columns["sf_hq"].SetOrdinal(3);
            dsts.Tables[0].Columns["sf_emp_id"].SetOrdinal(4);
            Grdprd.DataSource = dsts;
            Grdprd.DataBind();
        }
        else
        {

            SqlCommand cmd = new SqlCommand("Leave_Status_DCR_Periodically", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            //cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Fdate", Fdate2.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("@Tdate", Tdate2.ToString("MM/dd/yyyy"));
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();

            dsts.Tables[0].Columns.RemoveAt(10);
            dsts.Tables[0].Columns.RemoveAt(9);
            dsts.Tables[0].Columns.RemoveAt(8);
            dsts.Tables[0].Columns.RemoveAt(7);          
            dsts.Tables[0].Columns.RemoveAt(1);
            dsts.Tables[0].Columns["sf_hq"].SetOrdinal(3);
            dsts.Tables[0].Columns["sf_emp_id"].SetOrdinal(4);
            Grdprd.DataSource = dsts;
            Grdprd.DataBind();
        }

    }

    protected void Grdprd_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (Detailed == "1")
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
                GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell2 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);              
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Joining Date", "#0097AC", true);


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
                //    iLstMonth.Add(cmonth);
                //    iLstYear.Add(cyear);
                //    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;



                    Doctor dr = new Doctor();
                    dsDoctor = dr.getDCR_Leave_Type(divcode);
                    AddMergedCells(objgridviewrow, objtablecell, 0, dsDoctor.Tables[0].Rows.Count, Fdate + " To " + " " + Tdate, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, dsDoctor.Tables[0].Rows.Count, "Leave Count", "#0097AC", true);

                    for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[k]["Leave_SName"].ToString(), "#0097AC", true);
                    }
                //    cmonth = cmonth + 1;

                //    if (cmonth == 13)
                //    {
                //        cmonth = 1;
                //        cyear = cyear + 1;
                //    }
                //}

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total", "#0097AC", true);
                
                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
                //
                #endregion
                //
            }
        }
        else
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
                GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell2 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);                
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Joining Date", "#0097AC", true);




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
                   // string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 0,  Fdate + " To " + " " + Tdate, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "Leave Count", "#0097AC", true);

                    //cmonth = cmonth + 1;

                    //if (cmonth == 13)
                    //{
                    //    cmonth = 1;
                    //    cyear = cyear + 1;
                    //}

               // }
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total", "#0097AC", true);
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
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Employee id" || objtablecell.Text == "Joining Date")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true, 2);
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

    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Detailed == "1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = 6, m = 0, n = 0; i < e.Row.Cells.Count; i++, n++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                        //int cMnth = iLstMonth[m];
                        //int cYr = iLstYear[m];

                        //if (cMnth == 12)
                        //{
                        //    sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        //}
                        //else
                        //{
                        //    sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        //}
                        //hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                        //SalesForce sf = new SalesForce();
                        //dsLeave = sf.getDCR_Leave_Count_ToolTip(sSf_code, divcode, cMnth, cYr);

                        //if (dsLeave.Tables[0].Rows.Count > 0)
                        //    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //hLink.ToolTip = Days;
                        //hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        tot = hLink.Text;
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][7].ToString()));
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToInt16(tot);
                        }
                        // m++;
                    //    if (tot != "-" && tot != "&nbsp;")
                    //    {
                    //        fulltotal += Convert.ToInt16(tot);

                    //        if (n % 4 == 0)
                    //        {
                    //            total += Convert.ToInt16(tot);
                    //        }
                    //        else if (n % 4 == 1)
                    //        {
                    //            total2 += Convert.ToInt16(tot);
                    //        }
                    //        else if (n % 4 == 2)
                    //        {
                    //            total3 += Convert.ToInt16(tot);
                    //        }
                    //        else if (n % 4 == 3)
                    //        {
                    //            total4 += Convert.ToInt16(tot);
                    //        }
                    //    }
                    }

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }

                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";

                //TableCell tblRow_Count = new TableCell();

                //tblRow_Count.Text = total.ToString();
                //if (tblRow_Count.Text == "0")
                //{
                //    tblRow_Count.Text = "";
                //}

                //TableCell tblRow_Count2 = new TableCell();
                //tblRow_Count2.Text = total2.ToString();
                //if (tblRow_Count2.Text == "0")
                //{
                //    tblRow_Count2.Text = "";
                //}

                //TableCell tblRow_Count3 = new TableCell();
                //tblRow_Count3.Text = total3.ToString();

                //if (tblRow_Count3.Text == "0")
                //{
                //    tblRow_Count3.Text = "";
                //}

                //TableCell tblRow_Count4 = new TableCell();
                //tblRow_Count4.Text = total4.ToString();

                //if (tblRow_Count4.Text == "0")
                //{
                //    tblRow_Count4.Text = "";
                //}

                //TableCell tblRow_Count5 = new TableCell();
                //tblRow_Count5.Text = fulltotal.ToString();

                //if (tblRow_Count5.Text == "0")
                //{
                //    tblRow_Count5.Text = "";
                //}


                //e.Row.Cells.Add(tblRow_Count);
                //e.Row.Cells.Add(tblRow_Count2);
                //e.Row.Cells.Add(tblRow_Count3);
                //e.Row.Cells.Add(tblRow_Count4);
                //e.Row.Cells.Add(tblRow_Count5);

                //total = 0;
                //total2 = 0;
                //total3 = 0;
                //total4 = 0;
                //fulltotal = 0;
                TableCell tblRow_Count = new TableCell();
                tblRow_Count.Text = total.ToString();
                if (tblRow_Count.Text == "0")
                {
                    tblRow_Count.Text = "";
                }
                e.Row.Cells.Add(tblRow_Count);

                total = 0;

                //e.Row.Cells[1].Wrap = false;
                //e.Row.Cells[2].Wrap = false;
                //e.Row.Cells[3].Wrap = false;
                //e.Row.Cells[4].Wrap = false;
            }
        }
        else
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = 6, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                        //int cMnth = iLstMonth[j];
                        //int cYr = iLstYear[j];

                        //if (cMnth == 12)
                        //{
                        //    sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        //}
                        //else
                        //{
                        //    sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        //}
                        //hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                        //SalesForce sf = new SalesForce();
                        //dsLeave = sf.getDCR_Leave_Count_ToolTip(sSf_code, divcode, cMnth, cYr);

                        //if (dsLeave.Tables[0].Rows.Count > 0)
                        //    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        SalesForce sf = new SalesForce();
                        //string sTxt = sf.getMonthName(cMnth.ToString()) + "-" + cYr;
                        //hLink.ToolTip = sTxt;
                        //hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        tot = hLink.Text;
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][7].ToString()));
                        j++;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToInt16(tot);
                        }

                    }


                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }
                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                TableCell tblRow_Count = new TableCell();

                tblRow_Count.Text = total.ToString();
                if (tblRow_Count.Text == "0")
                {
                    tblRow_Count.Text = "";
                }
                e.Row.Cells.Add(tblRow_Count);

                total = 0;


            }
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

                System.Web.UI.WebControls.ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

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
                }

                if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                {

                    int j = i + 4;

                    e.Row.Cells[j].Visible = false;
                }
            }
        }
    }



    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);
    //}
    //public static void PrintWebControl(Control ControlToPrint)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ControlToPrint is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage);
    //        ((WebControl)ControlToPrint).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ControlToPrint);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();

    //}

    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    string attachment = "attachment; filename=DCRView.xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    HtmlForm frm = new HtmlForm();
    //    form1.Parent.Controls.Add(frm);
    //    frm.Attributes["runat"] = "server";
    //    frm.Controls.Add(pnlContents);
    //    frm.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    //}

    //protected void btnClose_Click(object sender, EventArgs e)
    //{

    //}
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in cblGridColumnList.Items)
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
}