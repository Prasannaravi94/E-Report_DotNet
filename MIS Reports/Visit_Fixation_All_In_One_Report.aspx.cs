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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
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
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    List<int> iLstVstCnt= new List<int>();
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {       
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["Tyear"].ToString();
        sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        strmode = Request.QueryString["Vacant"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        if (sReportType==1)
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
        lblHead.Text = "Visit Details "+sHeading+" Wise Between  - " + strFrmMonth + " " + FYear +"  To  "+strToMonth+" "+TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
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
        if (sReportType==1)
        {
            sProc_Name = "visit_fixation_Cat";
        }
        else if (sReportType == 2)
        {
            sProc_Name = "visit_fixation_Spclty";
        }
        else if (sReportType == 3)
        {
            sProc_Name = "visit_fixation_Class";
        }
        else if (sReportType == 4)
        {
            sProc_Name = "visit_fixation_Camp";
        }
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Vacant", strmode);
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    #endregion
    //  
    #region GrdFixation_RowCreated
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
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#003300", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#003300", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#003300", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Design", "#003300", true);
            
            //
            int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            int sMode=Convert.ToInt32(Request.QueryString["cMode"].ToString());
            string strQry = "";
            if (sMode == 1)
            {
                strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
                 " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                 " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                 " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                 " ORDER BY c.Doc_Cat_Code";
            }
            else if (sMode == 2)
            {
                strQry = " SELECT c.Doc_Special_Code,c.Doc_Special_SName AS ShortName,c.Doc_Special_Name,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_special_code) from Mas_ListedDr d where d.Doc_special_code = c.Doc_Special_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Speciality c WHERE c.Doc_Special_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
                     " ORDER BY c.Doc_Special_Code";
            }
            else if (sMode == 3)
            {
                strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName AS ShortName,c.Doc_ClsName,case isnull(c.No_of_visit,'') " +
                 " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
                 " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cat_Count" +
                 " FROM  Mas_Doc_Class c WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code= '" + div_code + "' " +
                 " ORDER BY c.Doc_ClsCode";
            }
            else if (sMode == 4)
            {
                strQry = " SELECT c.Doc_SubCatCode,c.Doc_SubCatSName AS ShortName,c.Doc_SubCatName,case isnull(c.No_Visit,'') " +
                 " when '' then 1 when 0 then 1 else c.No_Visit end as No_of_visit, " +
                 " (select count(d.Doc_SubCatCode) from Mas_ListedDr d where d.Doc_SubCatCode like CONCAT((CAST(c.Doc_SubCatCode as varchar(50)) + ','), '%')) as Cat_Count" +
                 " FROM  Mas_Doc_SubCategory c WHERE c.Doc_SubCat_ActiveFlag=0 AND c.Division_Code= " + Convert.ToInt32(div_code) + " " +
                 " ORDER BY c.Doc_SubCatCode";
            }
            //
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();
            //
            for (int i = 0; i <= months; i++)
            {
                //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                int iColSpan = 0;
                foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                {
                    int iCnt = 0;
                    iColSpan += Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 4;
                    iCnt = Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 4;
                    iLstVstCnt.Add(iCnt - 2);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, iCnt, dtRow["ShortName"] + "(" + dtRow["No_of_visit"].ToString() + ")", "#003300", false);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "TDrs", "#003300", false);
                    string sVstVal = "";
                    for (int j = 0; j < iCnt - 2; j++)
                    {
                        if (j == iCnt - 3)
                            sVstVal = "M " + (j - 1).ToString() + " V";
                        else
                            sVstVal = j.ToString() + " V";
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, sVstVal, "#003300", false);
                    }
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Miss", "#003300", false);
                }
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "NL Drs", "#003300", false);
                AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan + 1, sTxt, "#003300", true);
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
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
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
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
    #endregion
    //    
    #region GrdFixation_RowDataBound
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

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
                    e.Row.Cells[i].Controls.Add(hLnk);
                    */
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    try
                    {
                        int ist = iLstVstCnt[j];
                        int iTtl = (e.Row.Cells[i - 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                        int i0V = 0, iMin = 0;
                        for (int m = 1; m < ist - 1; m++)
                        {
                            i0V += (e.Row.Cells[i + m].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i + m].Text);
                            if (m == ist - 2)
                            {
                                iMin = (e.Row.Cells[i + m].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i + m].Text);
                            }
                        }
                        e.Row.Cells[i + ist].Attributes.Add("style", "color:#FF00CC;");
                        if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                            e.Row.Cells[i + ist].Text = "0";
                        else
                            e.Row.Cells[i + ist].Text = (iTtl - iMin).ToString();
                        if (e.Row.Cells[i + ist].Text == "0")
                            e.Row.Cells[i + ist].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:red;");
                        e.Row.Cells[i].Text = (iTtl - i0V).ToString();
                        //
                        j++;
                    }
                    catch (Exception ex)
                    { }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
                if (e.Row.Cells[i].Text == "0")
                    e.Row.Cells[i].Text = "-";   
            }

            //ABP
            int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;


            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


            if (Convert.ToInt32(Request.QueryString["cMode"].ToString()) == 1)
            {
                for (int m = 4, n = 0; m < e.Row.Cells.Count; m++)
                {

                    //for (int i = 0; i <= months; i++)
                    {
                        //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                        int iColSpan = 0;
                        foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                        {
                            int iCnt = 0;
                            iColSpan += Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 4;
                            iCnt = Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 4;
                            string cat_code = dtRow["Doc_Cat_Code"].ToString();

                            if (e.Row.Cells[m].Text != "0" && e.Row.Cells[m].Text != "-" && e.Row.Cells[m].Text != "&nbsp;")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[m].Text;
                                hLink.Attributes.Add("class", "btnDrSn");
                                hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + cat_code + "', '" + TYear + "','" + "7" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                                hLink.ToolTip = "Click here";
                                hLink.Font.Underline = true;
                                hLink.Attributes.Add("style", "cursor:pointer");
                                hLink.ForeColor = System.Drawing.Color.Blue;
                                e.Row.Cells[m].Controls.Add(hLink);
                            }

                            m = iCnt + m;
                        }
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                }
            }

            //int cat_count = dsDoctor.Tables[0].Rows.Count;

            //if (Convert.ToInt32(Request.QueryString["cMode"].ToString()) == 1)
            //{
            //    for (int m = 5, n = 0; m < e.Row.Cells.Count; m++)
            //    {
            //        if (e.Row.Cells[m].Text != "0" && e.Row.Cells[m].Text != "-" && e.Row.Cells[m].Text != "-")
            //        {
            //            HyperLink hLink = new HyperLink();
            //            hLink.Text = e.Row.Cells[m].Text;
            //            hLink.Attributes.Add("class", "btnDrSn");
            //            hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "7" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
            //            hLink.ToolTip = "Click here";
            //            hLink.Font.Underline = true;
            //            hLink.Attributes.Add("style", "cursor:pointer");
            //            hLink.ForeColor = System.Drawing.Color.Blue;
            //            e.Row.Cells[m].Controls.Add(hLink);
            //        }
            //        cmonth = cmonth + 1;
            //        if (cmonth == 13)
            //        {
            //            cmonth = 1;
            //            cyear = cyear + 1;
            //        }
            //        m = m + 2 + (6 * cat_count);
            //    }
            //}


 //ABP
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