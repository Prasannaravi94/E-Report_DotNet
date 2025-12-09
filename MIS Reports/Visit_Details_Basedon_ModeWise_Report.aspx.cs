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
        //div_code = Request.QueryString["div_code"].ToString();
        div_code = Session["div_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sType = Request.QueryString["cMode"].ToString();
        int sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());
        
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        string sHeading = "";
        if (sReportType == 1)
        {
            sHeading = " Category ";
        }
        else if (sReportType == 2)
        {
            sHeading = " Speciality ";
        }
        else if (sReportType == 3)
        {
            sHeading = " Class ";
        }
        else if (sReportType == 4)
        {
            sHeading = " Campaign ";
        }
        lblHead.Text = "Mode Wise Visit Details Of " + sHeading + " Wise Between  - " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + sf_name;
        FillCatg();
    }
    //
    #endregion
    //
    #region getMonthName
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
    #endregion
    //
    #region FillCatg
    private void FillCatg()
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
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        string sProc_Name = "";
        int sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());
        if (sReportType == 1)
        {
            sProc_Name = "visit_BasedOn_ModeWise_Cat";
        }
        else if (sReportType == 2)
        {
            sProc_Name = "visit_BasedOn_ModeWise_Spclty";
        }
        else if (sReportType == 3)
        {
            sProc_Name = "visit_BasedOn_ModeWise_Class";
        }
        else if (sReportType == 4)
        {
            sProc_Name = "visit_BasedOn_ModeWise_Camp";
        }
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 500;
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
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#006633", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#006633", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#006633", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design", "#006633", true);
            

            int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            string strQry = "";
            int iMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            if (iMode==1)
            {
                strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
                 " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                 " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                 " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                 " ORDER BY c.Doc_Cat_Code";
            }
            else if (iMode==2)
            {
                strQry = " SELECT c.Doc_Special_Code,c.Doc_Special_SName AS ShortName,c.Doc_Special_Name,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_special_code) from Mas_ListedDr d where d.Doc_special_code = c.Doc_Special_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Speciality c WHERE c.Doc_Special_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_Special_SName";
            }
            else if (iMode==3)
            {
                strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName AS ShortName,c.Doc_ClsName,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cat_Count" +
                     " FROM  Mas_Doc_Class c WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_ClsSName";
            }
            else if (iMode == 4)
            {
                strQry = " SELECT c.Doc_SubCatCode,c.Doc_SubCatSName AS ShortName,c.Doc_SubCatName,case isnull(c.No_Visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_Visit end as No_of_visit, " +
                     " (select count(d.Doc_SubCatCode) from Mas_ListedDr d where d.Doc_SubCatCode like CONCAT('%' + (CAST(c.Doc_SubCatCode as varchar(50)) + ','), '%')) as Cat_Count" +
                     " FROM  Mas_Doc_SubCategory c WHERE c.Doc_SubCat_ActiveFlag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_SubCatCode";
            }  

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                int iColSpan = 0;
                iLstColCnt.Add(3);
                for (int j = 0, k=0; j < dsDoctor.Tables[0].Rows.Count; j++)
                {
                    int iCnt = 0;
                    if (k == 0)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 5, "COVERAGE", "#006633", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Ttl Drs", "#006633", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Drs Met", "#006633", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#006633", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Missed", "#006633", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Missed (%)", "#006633", false);
                        iColSpan += 5;
                        k++;
                    }
                    //
                    iColSpan += 5;
                    iCnt = 5;
                    iLstColCnt.Add(iCnt);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 5, dsDoctor.Tables[0].Rows[j]["ShortName"].ToString(), "#006633", false);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Ttl Drs", "#006633", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Drs Met", "#006633", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#006633", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Missed", "#006633", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Missed (%)", "#006633", false);

                }
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#006633", true);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            //
            #endregion
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
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "#FFFFFF");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
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
            #region Calculations

//ABP
            int cat_count = dsDoctor.Tables[0].Rows.Count;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
            if (Convert.ToInt32(Request.QueryString["cMode"].ToString()) == 1)
            {
                for (int m = 5, n = 0; m < e.Row.Cells.Count; m++)
                {
                    if (e.Row.Cells[m].Text != "0" && e.Row.Cells[m].Text != "-" && e.Row.Cells[m].Text != "-")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[m].Text;
                        hLink.Attributes.Add("class", "btnDrSn");
                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "5" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                        hLink.ToolTip = "Click here";
                        hLink.Font.Underline = true;
                        hLink.Attributes.Add("style", "cursor:pointer");
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[m].Controls.Add(hLink);
                    }
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    m = m + 4 + (5 * cat_count);
                }
            }
            //ABP

            int iMsd = 0;
            int iMsdAvg = 0;
            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {/*
                int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
                int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());
                string dttme = "";
                if (months == 12)
                    dttme = "01-01-" + (cyear + 1).ToString();
                else
                    dttme = (months + 1).ToString() + "-01-" + (cyear).ToString();

                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);

                SqlCommand cmd = new SqlCommand("VisitDetail_LstDr_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@div_code", div_code);
                cmd.Parameters.AddWithValue("@sf_code", dtrowClr.Rows[indx][1].ToString());
                cmd.Parameters.AddWithValue("@cdate", dttme);
                cmd.CommandTimeout = 100;
                string txtc = dtrowClr.Rows[indx][1].ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dst = new DataTable();
                da.Fill(dst);
                e.Row.Cells[4].Text = dst.Rows[0][0].ToString();
              */
            }

            for (int i = 4, j = 0; i < e.Row.Cells.Count; i += 5)
            {
                //if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                //{/*
                //    HyperLink hLnk = new HyperLink();
                //    hLnk.Text = e.Row.Cells[i].Text;
                //    hLnk.NavigateUrl = "#";
                //    hLnk.ForeColor = System.Drawing.Color.Black;
                //    hLnk.Font.Underline = false;
                //    hLnk.ToolTip = "Click to View Details";
                //    e.Row.Cells[i].Controls.Add(hLnk);*/
                //}
                //else if (e.Row.Cells[i].Text == "0")
                //{
                //    int ist = iLstColCnt[j];
                //    int iMax = (e.Row.Cells[i - 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 2].Text);
                //    int iMin = (e.Row.Cells[i - 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                // //   int iDrs_Msd = iMin - iMax;
                //   // e.Row.Cells[i - 4].Text = iDrs_Msd.ToString();
                //    decimal dCvg = 0;
                //    if (iMax != 0 && iMin!=0)
                //        dCvg = Decimal.Divide((iMin * 100), iMax);
                //    else if ((iMax != 0 && iMin == 0))
                //        dCvg = -250;

                //    e.Row.Cells[i].Text = dCvg.ToString("0.##");
                //    e.Row.Cells[i].Attributes.Add("style", "color:#FF33CC;font-weight:bolder;");
                //    if (e.Row.Cells[i].Text == "0")
                //    {
                //        e.Row.Cells[i].Text = "-";
                //        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                //    }
                //    else if (dCvg==-250)
                //    {
                //        e.Row.Cells[i].Text = "0";
                //        e.Row.Cells[i].Attributes.Add("style", "color:red;font-weight:bolder;");
                //    }
                //    j++;
                //}
                //e.Row.Cells[i].Attributes.Add("align", "center");
                int iMax = (e.Row.Cells[i].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i].Text);
                int iMin = (e.Row.Cells[i + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i + 1].Text);
                decimal dCvg = 0;
                decimal mCvg = 0;
                if (iMax != 0 && iMin != 0)
                    dCvg = Decimal.Divide((iMin * 100), iMax);
                else if ((iMax != 0 && iMin == 0))
                    dCvg = -250;
                if (iMax != 0)
                {
                    iMsd = iMax - iMin;
                    e.Row.Cells[i + 3].Text = iMsd.ToString();
                }
                else
                {

                }
                if (iMax != 0 && iMsd != 0)
                    mCvg = Decimal.Divide((iMsd * 100), iMax);
                else if ((iMax != 0 && iMsd == 0))
                    mCvg = -250;
                e.Row.Cells[i + 2].Text = dCvg.ToString("0.##");
                e.Row.Cells[i + 4].Text = mCvg.ToString("0.##");
                e.Row.Cells[i + 2].Attributes.Add("style", "color:#FF33CC;font-weight:bolder;");
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

                if (e.Row.Cells[i + 4].Text == "0")
                {
                    e.Row.Cells[i + 4].Text = "-";
                    e.Row.Cells[i + 4].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                else if (mCvg == -250)
                {
                    e.Row.Cells[i + 4].Text = "0";

                }
                j++;
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
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
        }
    }
    #endregion
    //
}
//
#endregion