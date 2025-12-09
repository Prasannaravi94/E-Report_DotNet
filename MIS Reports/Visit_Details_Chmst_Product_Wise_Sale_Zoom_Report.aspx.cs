
#region Assembly
using System;
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
using System.ComponentModel;
#endregion

#region MIS_Reports_Visit_Details_Basedonfield_Level1
public partial class MIS_Reports_Visit_Details_Chmst_Product_Wise_Sale_Zoom_Report : System.Web.UI.Page
{
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
    string Months = string.Empty;
    string Year = string.Empty;
    string Mode  = string.Empty;
    int k1 = 1;
    #endregion
    // 
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        string sf_name = Request.QueryString["SfName"].ToString();
        string sf_code = Request.QueryString["sfcode"].ToString();
        //FMonth = Request.QueryString["FMonth"].ToString();
        //FYear = Request.QueryString["FYear"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sType = Request.QueryString["Type"].ToString();
        Months = Request.QueryString["Months"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Mode = Request.QueryString["Mode"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(Months);
        //string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Chemist Wise Sale View For  - " + strFrmMonth + " " + Year;

        lblFFName.Text = "Field Force Name   : " + sf_name;
        lblHQ1.Text = "HQ : " + sType;
        lblDesignation1.Text = "Desingnation : " + sMode;
        FillReport();
    }
    #endregion
    //


    #region FillReport
    private void FillReport()
    {
        //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth = Convert.ToInt32(FMonth);
        //int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        //while (months >= 0)
        //{
        //if (cmonth == 13)
        //{
        //    cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //}
        //else
        //{
        //    iMn = cmonth; iYr = cyear;
        //}            
        dtMnYr.Rows.Add(null, iMn, iYr);
        //months--; 
        //cmonth++;
        //}
        //

        string cDate = (Convert.ToInt32(Months) + 1).ToString() + "-01-" + Year;
        if (Months == "12")
        {
            cDate = "01-01-" + (Convert.ToInt32(Year) + 1).ToString();
        }

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        con.Open();
        SqlCommand cmd = new SqlCommand("visit_details_Chmst_ProductWise_Sale_Zoom", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@sf_code", Request.QueryString["sfcode"].ToString());
        cmd.Parameters.AddWithValue("@cDate", cDate);
        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(Months));
        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(Year));
        cmd.Parameters.AddWithValue("@Mode", Mode);

        cmd.CommandTimeout = 250;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        //dsts.Tables[0].Columns.RemoveAt(0);
        //dsts.Tables[0].Columns.RemoveAt(1);
        //dsts.Tables[0].Columns.RemoveAt(2);
      
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
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Field Force Name", "#99FF99", true);                       
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chemist Name", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "ProductName", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Sale Product Qty", "#99FF99", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Sale Value", "#99FF99", true);

            ////int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //string cmonth = (Request.QueryString["FMonth"].ToString());
            //int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            //SalesForce sf = new SalesForce();
            ////for (int i = 0; i <= months; i++)
            ////{
            //string sTxt = cmonth + "-" + cyear;
            //AddMergedCells(objgridviewrow, objtablecell, 0, 4, sTxt, "#99FF99", true);

            //AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, "Visit Date", "#CCFFCC", false);
            //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "0-10", "#99FF99", false);
            //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "11-20", "#99FF99", false);
            //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "21-31", "#99FF99", false);

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
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
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


        int RowSpan = 2;

        for (int i = GrdFixationMode.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = GrdFixationMode.Rows[i];
            GridViewRow prevRow = GrdFixationMode.Rows[i + 1];
            if (currRow.Cells[2].Text == prevRow.Cells[2].Text)
            {

                // string val = prevRow.Cells[0].Text;
                //slno1 += 1;

                //currRow.Cells[0].Text = slno1.ToString();

                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;
                currRow.Cells[2].RowSpan = RowSpan;
                prevRow.Cells[2].Visible = false;
                currRow.Cells[3].RowSpan = RowSpan;
                prevRow.Cells[3].Visible = false;              
                RowSpan++;
            }
            else
            {
                RowSpan = 2;
                //slno1 += 1;

                //currRow.Cells[0].Text = slno1.ToString();


                // currRow.Cells[0].Text = currRow.Cells[0].Text;
            }
        }       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            e.Row.Cells[0].Text = (k1).ToString();
            k1 = k1 + 1;
            e.Row.Cells[0].Attributes.Add("align", "center");
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

            for (int i1 = 4; i1 < e.Row.Cells.Count; i1++)
            {
                if (e.Row.Cells[i1].Text == "0" || e.Row.Cells[i1].Text == "&nbsp;")
                {
                    e.Row.Cells[i1].Text = "-";
                    e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                e.Row.Cells[i1].Attributes.Add("align", "center");
            }
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
        }
    }
    #endregion
}
#endregion