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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();

    int tot_miss = 0, TempValue = 0, iTtlMnth = 0, iTtl_Visit = 0;
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
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        //        
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        //
        lblHead.Text = "Visit Analysis At A Glance Between  - " + strFrmMonth.Substring(0,3) + " " + FYear + "  To  " + strToMonth.Substring(0,3) + " " + TYear;
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
        iTtlMnth = months;

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
        con.Open();
        SqlCommand cmd = new SqlCommand("visit_Month_Vertical_wise_Rpts_Cat", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(1);
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
            //
            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            //
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #endregion
            //
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Emp Code", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "DOJ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Last DCR Recvd", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Month", "#008080", true);
            //
            //int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            string strQry = "";
            //
            /*if (sMode == 1)
            {
                strQry = "SELECT case isnull(min(No_of_visit),'') " +
                 " when '' then 1 when 0 then 1 else min(No_of_visit) end Min_visit, " +
                 "case isnull(max(No_of_visit),'') when '' then 1 when 0 then 1 else max(No_of_visit) end Max_visit" +
                 " FROM  Mas_Doctor_Category WHERE Doc_Cat_Active_Flag=0 AND Division_Code= " + div_code + " ";
            }
            else if (sMode == 2)
            {
                strQry = "SELECT case isnull(min(No_of_visit),'') " +
                 " when '' then 1 when 0 then 1 else min(No_of_visit) end Min_visit, " +
                 "case isnull(max(No_of_visit),'') when '' then 1 when 0 then 1 else max(No_of_visit) end Max_visit" +
                 " FROM Mas_Doctor_Speciality WHERE Doc_Special_Active_Flag=0 AND Division_Code= " + div_code + " ";
            }
            else if (sMode == 3)
            {
                strQry = "SELECT case isnull(min(No_of_visit),'') " +
                 " when '' then 1 when 0 then 1 else min(No_of_visit) end Min_visit, " +
                 "case isnull(max(No_of_visit),'') when '' then 1 when 0 then 1 else max(No_of_visit) end Max_visit" +
                 " FROM  Mas_Doc_Class WHERE Doc_Cls_ActiveFlag=0 AND Division_Code= " + div_code + " ";
            }*/
            strQry = "SELECT case isnull(min(No_of_visit),'') " +
                 " when '' then 1 when 0 then 1 else min(No_of_visit) end Min_visit, " +
                 "case isnull(max(No_of_visit),'') when '' then 1 when 0 then 1 else max(No_of_visit) end Max_visit" +
                 " FROM  Mas_Doctor_Category WHERE Doc_Cat_Active_Flag=0 AND Division_Code= " + div_code + " ";
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            //
            int iMin_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][0].ToString());
            int iMax_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][1].ToString());
            iTtl_Visit = (iMax_Visit * 4) + 4;
            string sNo_Of_Vst = "Dr Met";
            for (int i = 0; i <= iMax_Visit; i++)
            {
                if (i == 0)
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "Total", "#008080", false);
                else
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "V" + i.ToString(), "#008080", false);
                //
                if (i != 0)
                    sNo_Of_Vst = i.ToString() + " & More";
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "List Dr", "#008080", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, sNo_Of_Vst, "#008080", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Seen", "#008080", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Missed", "#008080", false);
            }
            //
            AddMergedCells(objgridviewrow, objtablecell, 0, iTtl_Visit, "Dr Call Detail", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 4, "Total Dr Calls", "#008080", true);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Morning Calls", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Evening Calls", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Both Calls", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Call average", "#008080", false);
            //
            AddMergedCells(objgridviewrow, objtablecell, 2, 5, "Days Details", "#008080", true);
            //
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Avail Work Days", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Field Work", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Leave", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "No Field Work", "#008080", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Week Off & Holiday", "#008080", false);
            //  
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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "#FFFFFF");
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
            #region Calculations

            int iEnd = e.Row.Cells.Count;
            //
            bool blTtl = true;
            int iCls_Sn = 0;
            for (int i = 8; i < e.Row.Cells.Count; i++)
            {
                string sVl = (iTtl_Visit + 7).ToString();
                if (i < iTtl_Visit + 8)
                {
                    if (i % 4 == 3)
                    {
                        if (blTtl)
                        {
                            iCls_Sn = (e.Row.Cells[i - 1].Text == "" || e.Row.Cells[i - 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                            blTtl = false;
                        }
                        int iTtl = (e.Row.Cells[i - 3].Text == "" || e.Row.Cells[i - 3].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i - 3].Text);
                        int iMt = (e.Row.Cells[i - 2].Text == "" || e.Row.Cells[i - 2].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i - 2].Text);
                        if (iTtl == 0)
                            e.Row.Cells[i].Text = "";
                        else
                            e.Row.Cells[i].Text = (iTtl - iMt).ToString();
                    }
                }
                if (i == iTtl_Visit + 11)
                {
                    if (iCls_Sn != 0)
                    {
                        int iTtl_FW = (e.Row.Cells[i + 2].Text == "" || e.Row.Cells[i + 2].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[i + 2].Text);
                        if (iTtl_FW != 0)
                            e.Row.Cells[i].Text = Decimal.Divide(iCls_Sn, iTtl_FW).ToString("#.##");
                        else
                            e.Row.Cells[i].Text = "-";
                    }
                }
                e.Row.Cells[i].Text = (e.Row.Cells[i].Text == "0") ? "" : e.Row.Cells[i].Text;
            }
            //
            try
            {
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][6].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            //
            #endregion
            //
            if (e.Row.Cells[7].Text != "&nbsp;")
            {
                var result = e.Row.Cells[7].Text.Aggregate("", (res, c) => res + ((byte)c).ToString("X"));
                if (Convert.ToInt32(result) > 40)
                    e.Row.Cells[7].Text = (Convert.ToInt32(result) - 31).ToString();
                e.Row.Cells[7].Text = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[7].Text) - 1]["MNTH"].ToString()).Substring(0, 3) +
                    " - " + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[7].Text) - 1]["YR"].ToString();
            }
            else
                e.Row.Cells[7].Text = "-";
            e.Row.Cells[2].Attributes.Add("align", "left");
            e.Row.Cells[7].Attributes.Add("align", "left");
            e.Row.Cells[7].Attributes.Add("style", "color:#0000CD;");
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[7].Wrap = false;
        }
        //
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
                currRow.Cells[4].RowSpan = RowSpan;
                prevRow.Cells[4].Visible = false;
                currRow.Cells[5].RowSpan = RowSpan;
                prevRow.Cells[5].Visible = false;
                currRow.Cells[6].RowSpan = RowSpan;
                prevRow.Cells[6].Visible = false;
                RowSpan++;
            }
            else
            {
                RowSpan = 2;
            }
        }
        #endregion
        //
    }
    #endregion
    //
}
//
#endregion