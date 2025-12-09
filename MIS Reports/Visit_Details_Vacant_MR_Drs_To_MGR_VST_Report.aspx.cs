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
#region Class
public partial class MIS_Reports_Visit_Details_Basedonfield_Level1 : System.Web.UI.Page
{
    //
    #region variables
    DataSet dsDoctor = null;
    DataSet dsmgrsf = new DataSet();
    //
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
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();
    List<string> iLstDesig = new List<string>();
    List<int> iLstMnth = new List<int>();
    List<int> iLstYr = new List<int>();
    //
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
        if (!IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            string sf_name = Request.QueryString["sf_name"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            //
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Vacant HQ's Doctors Visited By MANAGER For the Month Between  - <font color='#0077FF'>" + strFrmMonth.Substring(0, 3) + " " + FYear + "  and  " + strToMonth.Substring(0, 3) + " " + TYear + "</font>";
            LblForceName.Text = "Field Force Name : <font color='#0077FF'>" + sf_name + "</font>";
            //
            FillReport();
        }
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
        //
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
        con.Open();
        SqlCommand cmd = new SqlCommand("Visit_Detail_Vacunt_Mr_Drs_To_Mgr", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 300;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        if (dsts.Tables[0].Rows.Count > 0 && dsts.Tables[0].Rows[0][0].ToString() != "")
        {
            if (dsts.Tables[0].Columns.Count > 5)
            {
                dsts.Tables[0].Columns.RemoveAt(5);
                dsts.Tables[0].Columns.RemoveAt(1);
                dsts.Tables[0].Columns["sf_desgignation_short_name"].SetOrdinal(3);

            }
        }
        else
            dsts = null;
       
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
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Sf_code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Field Force Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
            
            //
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);
            //
            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            //
            string strQry = "";
            //
            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + div_code + "' and type='2' AND Designation_Active_Flag=0 order by Designation_Short_Name ";
            //
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();
            //
            if (months1 >= 0)
            {
                int iR = 0;
                for (int j = 1; j <= months1 + 1; j++)
                {
                    int iColSpan = 0;
                    //  AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                    foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                    {
                        iColSpan = ((dsDoctor.Tables[0].Rows.Count));
                        iLstDesig.Add(dtRow["Designation_Short_Name"].ToString());
                        iLstMnth.Add(cmonth1);
                        iLstYr.Add(cyear1);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dtRow["Designation_Short_Name"].ToString(), "#0097AC", false);                        
                    }
                    iR++;
                    AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1 + " (No.of Doctors Visited)", "#0097AC", true);
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
            #region Calculations
            //            
            for (int l = 4, j=0; l < e.Row.Cells.Count; l++, j++)
            {
                HyperLink hLnk = new HyperLink();
                hLnk.Text = e.Row.Cells[l].Text;
                hLnk.NavigateUrl = "#";
                hLnk.ForeColor = System.Drawing.Color.Red;
                hLnk.Font.Underline = false;
                hLnk.ToolTip = "Click to View Details";
                hLnk.Attributes.Add("onclick", "javascript:window.open('Visit_Details_Vacant_MR_Drs_To_MGR_VST_Expand.aspx?sf_code=" + dtrowClr.Rows[e.Row.RowIndex][1].ToString() + "&Month=" + iLstMnth[j] + "&Year=" + iLstYr[j] +
                            "&div_code=" + div_code + "&Desig=" + iLstDesig[j].ToString() + "' ,null,'');");
                e.Row.Cells[l].Controls.Add(hLnk);
                //
                if (e.Row.Cells[l].Text == "0")
                {
                    e.Row.Cells[l].Text = "";
                    e.Row.Cells[l].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                e.Row.Cells[l].Attributes.Add("align", "center");
                //
            }
            //
            #endregion            
            //
            if (e.Row.Cells.Count > 3)
            {
                //e.Row.Cells[1].Wrap = false;
                //e.Row.Cells[2].Wrap = false;
                //e.Row.Cells[3].Wrap = false;
            }
            //
        }
    }
    #endregion
    //
}
//
#endregion