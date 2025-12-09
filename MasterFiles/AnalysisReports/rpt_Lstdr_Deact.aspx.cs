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

public partial class MasterFiles_AnalysisReports_rpt_Lstdr_Deact : System.Web.UI.Page
{
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
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    string screen_name = string.Empty;
    string SfCode = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
          div_code = Session["div_code"].ToString();
        SfCode = Session["sf_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        ViewState["FMonth"] = FMonth;
        ViewState["FYear"] = FYear;
        ViewState["TMonth"] = TMonth;
        ViewState["TYear"] = TYear;

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());

        lblHead.Text = "Listed Doctor Visit - Deactivaion for the month of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        screen_name = "rpt_Lstdr_Deact";
        if (!Page.IsPostBack)
        {
            FillReport();
        }
    }

      private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        sProc_Name = "Listeddr_Period_Deact";
       
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dtrowdt = dsts.Tables[1].Copy();

        result = dsts.Tables[1].AsEnumerable()
       .GroupBy(row => row.Field<string>("VST"))
       .Select(g => g.CopyToDataTable()).ToList();

        dsts.Tables[0].Columns.RemoveAt(9);
        //dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
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
            TableCell objtablecell = new TableCell();
            #endregion
            //
            #region Merge cells


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Listed Doctor Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Specialty", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Class", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Territory", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Deactivate Date", "#0097AC", true);

            FMonth = ViewState["FMonth"].ToString();
            FYear = ViewState["FYear"].ToString();
            TMonth = ViewState["TMonth"].ToString();
            TYear = ViewState["TYear"].ToString();

            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            SalesForce sf = new SalesForce();
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Count", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Date", "#0097AC", false);
                    iLstVstmnt.Add(cmonth1);
                    iLstVstyr.Add(cyear1);

                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }

            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
           // objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            //
            #endregion
            //
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
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("color", "white");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, SfCode);

        if (objtablecell.Text == "Specialty" || objtablecell.Text == "Class" || objtablecell.Text == "Qualification")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, SfCode, true, 3);
        }

        dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, objtablecell.Text, SfCode);
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
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

       
           
            for (int i = 9, m = 0; i < e.Row.Cells.Count; i++)
            {
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);
                DataTable Dt_Date = result[m].Copy();
                int iDateList;
                List<int> sDate = new List<int>();
                for (int l = 0; l < Dt_Date.Rows.Count; l++)
                {
                    if (dtrowClr.Rows[indx][1].ToString()==Dt_Date.Rows[l][0].ToString())
                    {
                        iDateList = Convert.ToInt32(Dt_Date.Rows[l][2].ToString());
                        sDate.Add(iDateList);
                    }
                }
                string sDateList="";
                sDate.Sort();
                foreach (int item in sDate)
                {
                    sDateList += item.ToString() + ",";
                }
                if (sDateList != "")
                {
                    e.Row.Cells[i].Text = sDateList.Remove(sDateList.Length - 1, 1);
                    e.Row.Cells[i].Attributes.Add("style", "color:red;");
                  
                }
                e.Row.Cells[i].Attributes.Add("align","center");
                e.Row.Cells[i-1].Attributes.Add("align", "center");
                //SqlCommand cmd = new SqlCommand("Listeddr_Act_date", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@div_code", div_code);
                //cmd.Parameters.AddWithValue("@Msf_code", sf_code);
                //cmd.Parameters.AddWithValue("@cMnth", iLstVstmnt[j]);
                //cmd.Parameters.AddWithValue("@cYr", iLstVstyr[j]);
                //cmd.Parameters.AddWithValue("@Lst_code", dtrowClr.Rows[indx][1].ToString());

                ////  cmd.Parameters.AddWithValue("@@Msf_code", dttme);
                //cmd.CommandTimeout = 100;
                //string txtc = dtrowClr.Rows[indx][1].ToString();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dst = new DataTable();
                //da.Fill(dst);
                //try
                //{
                //    e.Row.Cells[i].Text = dst.Rows[0][0].ToString();
                //}
                //catch
                //{
                //    e.Row.Cells[i].Text = " - ";
                //}
                i++;
                m++;
              
              //  e.Row.Cells[i].Attributes.Add("align", "left");
            }

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
        }
        Chemist chem = new Chemist();

        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, SfCode);
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

                    int j = i + 3;

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

        if (screen_name != "" && SfCode != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, SfCode);
        }

        Response.Redirect(Request.RawUrl);
    }

}