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

public partial class MasterFiles_AnalysisReports_Rpt_EffortVsSales_Work_Analysis : System.Web.UI.Page
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
    string Stok_code = string.Empty;
    string StName = string.Empty;
    double Value = 0.0;
    double Value_1 = 0.0;
    double Value_2 = 0.0;
    double Value_3 = 0.0;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsSecSales = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string sub_code = string.Empty;
    DataSet dsSub = new DataSet();
    string strFrmMonth = string.Empty;
    string strToMonth = string.Empty;
    string str = string.Empty;
    DataSet dsts = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // div_code = Session["div_code"].ToString();
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            TYear = Request.QueryString["To_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
            strFrmMonth = sf.getMonthName(FMonth.Trim());
            strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Comprehensive Work Analysis From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            str = " " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear + " Highest Monthly SS Value";
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            SubDivision sb = new SubDivision();

            FillReport();
        }
    }
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "SP_Effort_vs_Sale_Pri";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@FMnth", FMonth);
        cmd.Parameters.AddWithValue("@FYr", FYear);
        cmd.Parameters.AddWithValue("@TMnth", TMonth);
        cmd.Parameters.AddWithValue("@TYr", TYear);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dsts);


        dsts.Tables[0].Columns.Remove("Mnth");
        dsts.Tables[0].Columns.Remove("Yr");

        dtrowClr = dsts.Tables[0].Copy();

        GrdFixation.DataSource = dsts.Tables[0];
        GrdFixation.DataBind();

    }


    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {


        SalesForce sf = new SalesForce();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            int Doc_Count_head = dsts.Tables[0].Rows.Count;

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Month", "#0097AC", true);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            // GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, 18, "Effort", "#0097AC", true);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "FW days", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Act FW days", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Meeting", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Transit", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Training", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Others", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Others2", "#0097AC", false);
            //foreach (DataRow dtRow in dsts.Tables[0].Rows)
            //{
            //    AddMergedCells(objgridviewrow2, objtablecell2, 0, dtRow["Othr_Typ"].ToString(), "#0097AC", false);
            //}

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Leave", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "H/W", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Tot Days in a Month", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Drs Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Visit", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "MVD Drs List", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "MVD Drs Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Avg", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chemist Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Visit", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chem Avg", "#0097AC", false);


            AddMergedCells(objgridviewrow, objtablecell, 7, "Sales", "#0097AC", true);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Target", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "LY Net Sales", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "CY Net Sales", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "% Achieved", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Sales Growth", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "PCPM", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Free Goods", "#0097AC", false);

            AddMergedCells(objgridviewrow, objtablecell, 5, "Credit Note", "#0097AC", true);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Non Salable", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Non Salable % on Total CN", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Salable Return", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Salable Return % on Total CN", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Total CN Value", "#0097AC", false);

            AddMergedCells(objgridviewrow, objtablecell, 3, "Secondary Sales", "#0097AC", true);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Sec Sales", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Closing Inventory", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Closing Inventory Days", "#0097AC", false);

            AddMergedCells(objgridviewrow, objtablecell, 4, "Admin Effort", "#0097AC", true);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "ss", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "os", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Resignation", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Recruitment", "#0097AC", false);


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //  objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Font.Size = 10;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }

        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            e.Row.Style.Add("font-size", "10pt");
            for (int l = 2, j = 0; l < e.Row.Cells.Count; l++)
            {
                if (e.Row.Cells[l].Text == "0" || e.Row.Cells[l].Text == "&nbsp;" || e.Row.Cells[l].Text == "")
                {
                    e.Row.Cells[l].Text = "-";
                    //e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                e.Row.Cells[l].Attributes.Add("align", "Right");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;

            #endregion
            //

            //if (dtrowClr.Rows[indx][2].ToString() == "Total")
            if (indx == dtrowClr.Rows.Count - 1)
            {
                e.Row.Cells[0].Text = "";
                // e.Row.Cells[1].Text = "Total";

                for (int i = 20; i < e.Row.Cells.Count; i++)
                {
                    //  if (i != 21)
                    // {
                    e.Row.Cells[i].Text = "";
                    // }
                }

                e.Row.Attributes.Add("style", "background-color:LightGray;font-bold:true; font-size:14px; Color:Red; border-color:Black");
            }
            if (indx == dtrowClr.Rows.Count - 2)
            {
                //if (e.Row.Cells[18].Text != "&nbsp;" && e.Row.Cells[18].Text != "0" && e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                //{
                //   e.Row.Cells[21].Text = (((Convert.ToDecimal(e.Row.Cells[20].Text)) / (Convert.ToDecimal(e.Row.Cells[18].Text))) * (100)).ToString("#0");
                    
                //}
                //if (e.Row.Cells[19].Text != "&nbsp;" && e.Row.Cells[19].Text != "0" && e.Row.Cells[20].Text != "0" && e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "" && !e.Row.Cells[19].Text.Contains("-"))
                //{
                //    e.Row.Cells[22].Text = ((((Convert.ToDecimal(e.Row.Cells[20].Text)) - (Convert.ToDecimal(e.Row.Cells[19].Text))) / ((Convert.ToDecimal(e.Row.Cells[19].Text)))) * 100).ToString("#0");
                //}

                //if (e.Row.Cells[20].Text != "&nbsp;" && e.Row.Cells[20].Text != "0" && e.Row.Cells[20].Text != "-" && e.Row.Cells[20].Text != "")
                //{
                //    e.Row.Cells[23].Text = (((Convert.ToDecimal(e.Row.Cells[23].Text)) / Convert.ToDecimal(months +1))).ToString("#0.000");
                //}


                if (e.Row.Cells[21].Text != "&nbsp;" && e.Row.Cells[21].Text != "0" && e.Row.Cells[22].Text != "0" && e.Row.Cells[21].Text != "-" && e.Row.Cells[21].Text != "" && !e.Row.Cells[21].Text.Contains("-") && e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "0" && e.Row.Cells[22].Text != "" && e.Row.Cells[22].Text != "&nbsp;")
                {
                    e.Row.Cells[24].Text = ((((Convert.ToDecimal(e.Row.Cells[22].Text)) - (Convert.ToDecimal(e.Row.Cells[21].Text))) / ((Convert.ToDecimal(e.Row.Cells[21].Text)))) * 100).ToString("#0");
                }

                if (e.Row.Cells[22].Text != "&nbsp;" && e.Row.Cells[22].Text != "0" && e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "")
                {
                    e.Row.Cells[25].Text = (((Convert.ToDecimal(e.Row.Cells[25].Text)) / Convert.ToDecimal(months + 1))).ToString("#0.000");
                }

            }
        }
    }





}