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
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MIS_Reports_rptInputIssued_Details : System.Web.UI.Page
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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();

    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Input Issued Report From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
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
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //dtMnYr.Rows.Add(null, Convert.ToInt32(Request.QueryString["FMonth"].ToString()), Convert.ToInt32(Request.QueryString["Fyear"].ToString()));

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

        //sProc_Name = "Input_Statement_New_Temp";
        sProc_Name = "Input_Statement_New_Temp_Modify";

        

        //else if (sReportType == 2)
        //{
        //    sProc_Name = "visit_fixation_Spclty";
        //}
        //else if (sReportType == 3)
        //{
        //    sProc_Name = "visit_fixation_Class";
        //}
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 500;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(4);
        dsts.Tables[0].Columns.RemoveAt(3);
        dsts.Tables[0].Columns.RemoveAt(0);
        GrdFixation.DataSource = dsts.Tables[0];
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            #region Object

            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            #endregion

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Product Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "OB", "#0097AC", true);




            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months1; i++)
            {
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                AddMergedCells(objgridviewrow, objtablecell, 0, 2, sTxt, "#0097AC", true);


                AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Receipt", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Issue", "#0097AC", false);


                cmonth1 = cmonth1 + 1;
                if (cmonth1 == 13)
                {
                    cmonth1 = 1;
                    cyear1 = cyear1 + 1;
                }

            }
            string sTxt1 = "Total";
            AddMergedCells(objgridviewrow, objtablecell, 0, 3, sTxt1, "#0097AC", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "CB", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Receipt", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 1, "Issued", "#0097AC", false);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

            #endregion

        }
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            e.Row.Cells[0].Text = (indx+1).ToString();
            if (dtrowClr.Rows[indx][0].ToString() == "Grand Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:15px; Color:Red; border-color:Black");
                e.Row.Cells[0].Text = "";
            }
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 10;
        objtablecell.ForeColor = System.Drawing.Color.White;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

}