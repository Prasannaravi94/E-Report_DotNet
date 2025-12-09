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
using System.Drawing;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rpt_Consolidated_Report : System.Web.UI.Page
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
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDocMet = null;
    DataSet dsCov = null;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataSet dsUndoc = new DataSet();
    DataSet dsFw = new DataSet();
    DataSet dsField = new DataSet();
    DataSet dsNoFW = new DataSet();
    DataSet dsleave = new DataSet();
    DataSet dsCall = new DataSet();
    DataSet dsworkday = new DataSet();
    DataSet dsJwMet = new DataSet();
    DataSet dsJwSeen = new DataSet();
    DataSet dsdocseen = new DataSet();
    SalesForce dcrdoc = new SalesForce();
    List<int> iLstVstCnt = new List<int>();
    DataSet dschem = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    int imissed_dr = 0;
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {



        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();

        ViewState["div_code"] = div_code;
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        //string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Work Hygeine Report - " + strFrmMonth + " " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        screen_name = "rpt_Consolidated_Report";
        if (!Page.IsPostBack)
        {
            FillReport();
        }
    }
    private void FillReport()
    {
        // int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        ////
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
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Consolidated_Report";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", cmonth);
        cmd.Parameters.AddWithValue("@cYrs", cyear);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(17);
        dsts.Tables[0].Columns.RemoveAt(18);
        dsts.Tables[0].Columns.RemoveAt(19);
        dsts.Tables[0].Columns.RemoveAt(20);
        dsts.Tables[0].Columns["sf_name"].SetOrdinal(1);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Emp.Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "First Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Second Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Last DCR Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "No of FWD", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Leave", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Listed Dr. Visits", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Lst. Call Avg", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Unlisted Dr.Visits", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Unlst. Call Avg", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Cumulative Call Avg", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "DCR Submitted days", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "App. Pending Dates", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Delay Reporting Dates", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Total Delay Reporting", "#0097AC", true);

            div_code = ViewState["div_code"].ToString();
            string strQry = "";
            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level  from Mas_SF_Designation " +
                    " where Division_Code= '" + div_code + "' and type='2' order by Designation_Short_Name    ";

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();

            //    GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

            int iColSpan = 0;
            //  AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

            TableCell objtablecell2 = new TableCell();

            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, 0, dtRow["Designation_Short_Name"].ToString(), "#0097AC", true);
                    // AddMergedCells(objgridviewrow2, objtablecell2, iCnt, dtRow["Designation_Short_Name"].ToString(), "#0097AC", false);
                }

                //AddMergedCells(objgridviewrow, objtablecell, 2, "Repeated Calls", "#0097AC", true);
                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            }

            #endregion
        }
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            for (int l = 8; l < e.Row.Cells.Count; l++)
            {
                e.Row.Cells[l].Attributes.Add("align", "center");
                if (l == 12)
                {
                    int iDys = (e.Row.Cells[l - 3].Text == "-" || e.Row.Cells[l - 3].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 3].Text);
                    int iDrs_Mt = (e.Row.Cells[l - 1].Text == "-" || e.Row.Cells[l - 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 1].Text);

                    if (iDys != 0)
                        e.Row.Cells[l].Text = (Decimal.Divide(iDrs_Mt, iDys)).ToString("#.##");
                }
                if (l == 14)
                {
                    int iDys = (e.Row.Cells[l - 5].Text == "-" || e.Row.Cells[l - 5].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 5].Text);
                    int iDrs_Mt = (e.Row.Cells[l - 1].Text == "-" || e.Row.Cells[l - 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 1].Text);

                    if (iDys != 0)
                        e.Row.Cells[l].Text = (Decimal.Divide(iDrs_Mt, iDys)).ToString("#.##");
                }
                if (l == 15)
                {
                    int iDystot = (e.Row.Cells[l - 6].Text == "-" || e.Row.Cells[l - 6].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 6].Text);
                    int iDrs_Cnt = (e.Row.Cells[l - 4].Text == "-" || e.Row.Cells[l - 4].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 4].Text);
                    int iUnDrs_Cnt = (e.Row.Cells[l - 2].Text == "-" || e.Row.Cells[l - 2].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l - 2].Text);
                    int itot = iDrs_Cnt + iUnDrs_Cnt;
                    if (iDystot != 0)
                        e.Row.Cells[l].Text = (Decimal.Divide(itot, iDystot)).ToString("#.##");
                }
            }
            HyperLink hLink = new HyperLink();
            hLink.Text = e.Row.Cells[11].Text;
            hLink.Attributes.Add("class", "btnDrSn");
            hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "5" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
            hLink.ToolTip = "Click here";
            hLink.Font.Underline = true;
            hLink.Attributes.Add("style", "cursor:pointer");
            hLink.ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[11].Controls.Add(hLink);
        }
        try
        {
            int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
        }
        catch
        {
            e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
        }


        //
        //e.Row.Cells[1].Wrap = false;
        //e.Row.Cells[2].Wrap = false;
        //e.Row.Cells[3].Wrap = false;
        //e.Row.Cells[4].Wrap = false;
        //e.Row.Cells[5].Wrap = false;
        //e.Row.Cells[6].Wrap = false;
        //e.Row.Cells[7].Wrap = false;
        //e.Row.Cells[8].Wrap = false;
        //e.Row.Cells[9].Wrap = false;
        //e.Row.Cells[10].Wrap = false;
        //e.Row.Cells[11].Wrap = false;
        //e.Row.Cells[12].Wrap = false;
        //e.Row.Cells[13].Wrap = false;
        //e.Row.Cells[14].Wrap = false;
        //e.Row.Cells[15].Wrap = false;
        //e.Row.Cells[16].Wrap = false;
        //e.Row.Cells[17].Wrap = false;
        //e.Row.Cells[18].Wrap = false;
        //e.Row.Cells[19].Wrap = false;

        //e.Row.Cells[20].Wrap = false;
        //e.Row.Cells[21].Wrap = false;
        //e.Row.Cells[22].Wrap = false;
        //e.Row.Cells[23].Wrap = false;
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
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Emp.Code" || objtablecell.Text == "DOJ" || objtablecell.Text == "First Level Manager" || objtablecell.Text == "Second Level Manager")
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