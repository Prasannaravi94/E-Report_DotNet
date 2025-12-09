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

public partial class MasterFiles_AnalysisReports_rpt_Fieldwork_Pivot : System.Web.UI.Page
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
    List<string> iLstdes = new List<string>();
    List<DataTable> result = new List<System.Data.DataTable>();
    DataSet dsts = new DataSet();
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
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

        lblHead.Text = "Field Manager Work Analysis - View for the month of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        screen_name = "rpt_Fieldwork_Pivot";
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

        string mnt = Convert.ToString(months);
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

        sProc_Name = "Mgr_Coverage_Analysis";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
      
        da.Fill(dsts);
        dtrowClr = dsts.Tables[dtMnYr.Rows.Count].Copy();
        //  dtrowdt = dsts.Tables[1].Copy();

        // result = dsts.Tables[dtMnYr.Rows.Count-1].AsEnumerable()
        //.GroupBy(row => row.Field<string>("month"))
        //.Select(g => g.CopyToDataTable()).ToList();

        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(6);
        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(9);
        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(1);
        dsts.Tables[dtMnYr.Rows.Count].Columns["sf_name"].SetOrdinal(1);
        dsts.Tables[dtMnYr.Rows.Count].Columns["hq"].SetOrdinal(2);
        dsts.Tables[dtMnYr.Rows.Count].Columns["desg"].SetOrdinal(3);
        dsts.Tables[dtMnYr.Rows.Count].Columns["Sf_Joining_Date"].SetOrdinal(4);
        GrdFixation.DataSource = dsts.Tables[dtMnYr.Rows.Count];
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
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Desig", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Joining Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "First Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Second Level Manager", "#0097AC", true);

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

            string strQry = "";

            //  strQry = "select Feedback_Id,Feedback_Content,Division_Code,Act_Flag from Mas_App_CallFeedback where Division_Code = '" + div_code + "' and Act_Flag=0 ";

            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + div_code + "' and type='2' order by Manager_SNo ";

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {
                int iR = 0;
                for (int j = 1; j <= months1 + 1; j++)
                {
                    int iColSpan = 0;
                    //  AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                    {
                        int iCnt = 0;
                        iColSpan = ((dsDoctor.Tables[0].Rows.Count));
                        AddMergedCells(objgridviewrow2, objtablecell2, iCnt, dtRow["Designation_Short_Name"].ToString(), "#0097AC", false);
                        if (iR==0)                        
                            iLstdes.Add(dtRow["Designation_Short_Name"].ToString());
                    }
                    iR++;
                    iLstVstmnt.Add(cmonth1);
                    iLstVstyr.Add(cyear1);
                    AddMergedCells(objgridviewrow, objtablecell, iColSpan, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                 
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
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Joining Date" || objtablecell.Text == "First Level Manager" || objtablecell.Text == "Second Level Manager")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true, 3);
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
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations


            SalesForce desig = new SalesForce();
            DataSet dsdes = new DataSet();
            for (int i = 7, j = 0, m = 0; i < e.Row.Cells.Count; i += iLstdes.Count)
            {
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);            
                int iDateList;
                List<int> sDate = new List<int>();               
                //int j = 0;
                for (int l = 0; l < dsts.Tables[j].Rows.Count; l++)
                {                    
                    
                    if (dtrowClr.Rows[indx][1].ToString() == dsts.Tables[j].Rows[l][0].ToString())
                    {
                        for (int s = 0; s < iLstdes.Count; s++)
                        {
                            if(dsts.Tables[j].Rows[l][1].ToString() == iLstdes[s])
                                e.Row.Cells[i+s].Text = dsts.Tables[j].Rows[l][2].ToString();
                            e.Row.Cells[i + s].Text = e.Row.Cells[i + s].Text.ToString().Replace("[", "").Replace("]","").Trim();
                            e.Row.Cells[i + s].Attributes.Add("style", "color:red;");

                        }
                        //if(dsts.Tables[j].Rows[l][1].ToString() == iLstdes[0])
                        //{
                        //    e.Row.Cells[i].Text = dsts.Tables[j].Rows[l][2].ToString(); 
                        //}

                        //else if (dsts.Tables[j].Rows[l][1].ToString() == iLstdes[1])
                        //{
                        //    e.Row.Cells[i+1].Text = dsts.Tables[j].Rows[l][2].ToString();
                        //}
                        //else if (dsts.Tables[j].Rows[l][1].ToString() == iLstdes[2])
                        //{
                        //    e.Row.Cells[i + 2].Text = dsts.Tables[j].Rows[l][2].ToString();
                        //}
                        //else if (dsts.Tables[j].Rows[l][1].ToString() == iLstdes[3])
                        //{
                        //    e.Row.Cells[i + 3].Text = dsts.Tables[j].Rows[l][2].ToString();
                        //}
                        //else if (dsts.Tables[j].Rows[l][1].ToString() == iLstdes[4])
                        //{
                        //    e.Row.Cells[i + 4].Text = dsts.Tables[j].Rows[l][2].ToString();
                        //}
                        //else if (dsts.Tables[j].Rows[l][1].ToString() == iLstdes[5])
                        //{
                        //    e.Row.Cells[i + 5].Text = dsts.Tables[j].Rows[l][2].ToString();
                        //}
                      
                    }
                }
                j++;
                //string sDateList = "";
                //sDate.Sort();
                //foreach (int item in sDate)
                //{
                //    sDateList += item.ToString() + ",";
                //}
                //if (sDateList != "")
                //{
                //    e.Row.Cells[i].Text = sDateList.Remove(sDateList.Length - 1, 1);
                //    e.Row.Cells[i].Attributes.Add("style", "color:red;border-color:darkblue;");

                //}
                //e.Row.Cells[i].Attributes.Add("align", "center");
                //e.Row.Cells[i - 1].Attributes.Add("align", "center");

                //i++;
                m++;

                //  j++;
            }



            //ABP
            int countRow = 0;
            SalesForce sf = new SalesForce();

            string Mode = string.Empty;
            countRow = ((dsDoctor.Tables[0].Rows.Count));


            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);


            int a = 7;
            string desg = string.Empty;
            if (months1 >= 0)
            {
                int iR = 0;
                for (int j1 = 1; j1 <= months1 + 1; j1++)
                {
                    for (int i = 0, j = 0; i < countRow; i++, a++, j++)
                    {
                        string cmonth = sf.getMonthName(cmonth1.ToString());
                        string cyear = cyear1.ToString();

                        if (e.Row.Cells[a].Text != "&nbsp;" && e.Row.Cells[a].Text != "0" && e.Row.Cells[a].Text != "-")
                        {
                            desg = dsDoctor.Tables[0].Rows[j].ItemArray.GetValue(1).ToString();

                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[a].Text;
                            hLink.Attributes.Add("class", "btnDrSn");
                            hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + "4" + "',  '" + cmonth1 + "', '" + cyear + "', '" + TMonth + "', '" + desg + "','" + "" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                            hLink.ToolTip = "Click here";
                            hLink.Attributes.Add("style", "cursor:pointer");
                            hLink.Font.Underline = true;
                            hLink.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[a].Controls.Add(hLink);
                            e.Row.Cells[a].Attributes.Add("align", "center");

                            e.Row.Cells[a].Style.Add("word-wrap", "anywhere");
                            e.Row.Cells[a].Style.Add("width", "90px");


                            //j = j + 1;
                        }
                    }
                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }
            }
            //ABP

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