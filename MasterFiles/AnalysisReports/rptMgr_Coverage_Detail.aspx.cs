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
#region MasterFiles_AnalysisReports_rptMgr_Coverage_Detail
public partial class MasterFiles_AnalysisReports_rptMgr_Coverage_Detail : System.Web.UI.Page
{
    //
    #region variables
    DataSet dsDoctor = null;
    DataSet dsDCR = null;
    DataSet dsmgrsf = new DataSet();
    int iTtl_Visit = 0;
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
    string strFieledForceName = string.Empty;
    string mode = string.Empty;
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        mode = Request.QueryString["mode"].ToString();
    //    int sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());

        ViewState["FMonth"] = FMonth;
        ViewState["FYear"] = FYear;
        ViewState["TMonth"] = TMonth;
        ViewState["TYear"] = TYear;
        ViewState["div_code"] = div_code;

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        string sHeading = "";
     
        lblHead.Text = "HQ Coverage Analysis Detail Between  - " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        screen_name = "rptMgr_Coverage_Detail";
         if (!Page.IsPostBack)
         {
             FillCatg();
         }
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

        sProc_Name = "Hq_Coverage_Detail";
       
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 600;
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("sf_code");
        dsts.Tables[0].Columns.Remove("clr");
     //   dsts.Tables[0].Columns.Remove("sf_TP_Active_Flag");
        dsts.Tables[0].Columns.Remove("sf_code1");

        dsts.Tables[0].Columns["sf_name"].SetOrdinal(1);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
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

            FMonth = ViewState["FMonth"].ToString();
            FYear = ViewState["FYear"].ToString();
            TMonth = ViewState["TMonth"].ToString();
            TYear = ViewState["TYear"].ToString();
            div_code = ViewState["div_code"].ToString();

            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #endregion
            //
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Emp Code", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "DOJ", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Start DCR Date", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Last DCR Date", "#008080", true);

            string strQry = "";
            SalesForce sf = new SalesForce();
            strQry = "SELECT case isnull(min(No_of_visit),'') " +
             " when '' then 1 when 0 then 1 else min(No_of_visit) end Min_visit, " +
             "case isnull(max(No_of_visit),'') when '' then 1 when 0 then 1 else max(No_of_visit) end Max_visit" +
             " FROM  Mas_Doctor_Category WHERE Doc_Cat_Active_Flag=0 AND Division_Code= " + div_code + " ";
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            //
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {

                    int iMin_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][0].ToString());
                    int iMax_Visit = Convert.ToInt32(dsDoctor.Tables[0].Rows[0][1].ToString());

                    iTtl_Visit = (iMax_Visit);
                    AddMergedCells(objgridviewrow, objtablecell, 0, 9 + iTtl_Visit, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#008080", true);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Days Detail", "#008080", true);
                   // AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "NR Days", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "FW Days", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "NFW Days", "#008080", false);

                 
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, iTtl_Visit + 1, "Doctor Category", "#008080", true);
                   // string sNo_Of_Vst = "Dr Met";
                    for (int i = 1; i <= iMax_Visit; i++)
                    {
                        //if (i == 0)
                        //    AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "Total", "#008080", false);
                        //else
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "V" + i.ToString(), "#008080", false);

                        //
                      //  if (i != 0)
                        //    sNo_Of_Vst = i.ToString() + " & More";
                       // AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "List Dr", "#008080", false);
                      //  AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, sNo_Of_Vst, "#008080", false);
                     //   AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Seen", "#008080", false);
                       // AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Dr Missed", "#008080", false);
                    }

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Missed", "#008080", false);
                    //
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 6, "Dr Call Detail", "#008080", true);
                  //  AddMergedCells(objgridviewrow, objtablecell, 2, 4, "Total Dr Calls", "#008080", true);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Mor. Calls", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Eve. Calls", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Both Calls", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Met", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Total Calls", "#008080", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Avg Calls", "#008080", false);
                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
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
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "#FFFFFF");
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
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Emp Code" || objtablecell.Text == "DOJ" || objtablecell.Text == "Start DCR Date" || objtablecell.Text == "Last DCR Date")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true, 4);
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
                    e.Row.Cells[i].Controls.Add(hLnk);*/
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    //int ist = iLstColCnt[j];
                    //int iMax = (e.Row.Cells[i - 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 2].Text);
                    //int iMin = (e.Row.Cells[i - 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                    //decimal dCvg = 0;
                    //if (iMax != 0 && iMin!=0)
                    //    dCvg = Decimal.Divide((iMin * 100), iMax);
                    //else if ((iMax != 0 && iMin == 0))
                    //    dCvg = -250;

                    //e.Row.Cells[i].Text = dCvg.ToString("0.##");
                    //e.Row.Cells[i].Attributes.Add("style", "color:#FF33CC;font-weight:bolder;");
                    //if (e.Row.Cells[i].Text == "0")
                    //{
                    //    e.Row.Cells[i].Text = "-";
                    //    e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    //}
                    //else if (dCvg==-250)
                    //{
                    //    e.Row.Cells[i].Text = "0";
                    //    e.Row.Cells[i].Attributes.Add("style", "color:red;font-weight:bolder;");
                    //}
                    //j++;
                }
              //  e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][6].ToString()));
                
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
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
    #endregion
    //

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
//
#endregion