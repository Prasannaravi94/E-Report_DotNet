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

public partial class MasterFiles_AnalysisReports_rpt_CoreDr_Mgr_Period : System.Web.UI.Page
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
      
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());

        lblHead.Text = "Core Doctor Visit - Periodically for the month of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillReport();
    }

      private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0; int iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));

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
        string sProc_Name = "";

        sProc_Name = "CoreDR_Mgr_Period";
       
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
    
        con.Close();
   

        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
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
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Desig", "#0097AC", true);
           

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
                    AddMergedCells(objgridviewrow, objtablecell, 4, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Tot Drs", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Visited Calls", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed Calls", "#0097AC", false);
                   AddMergedCells(objgridviewrow2, objtablecell2, 0, "Per (%)", "#0097AC", false);
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

            HyperLink hLink2 = new HyperLink();
            hLink2.Text = e.Row.Cells[1].Text;
            string sSf_code = dtrowClr.Rows[indx][1].ToString();
            string sSf_Name = dtrowClr.Rows[indx][2].ToString();



            hLink2.Attributes.Add("href", "javascript:showMissedDR_2('" + sSf_code + "',  '" + FMonth + "', '" + TMonth + "',  '" + FYear + "', '" + TYear + "','" + sSf_Name + "')");
            //hLink.ToolTip = "Click here";
            //hLink.ForeColor = System.Drawing.Color.Blue;
            hLink2.Style.Add("text-decoration", "none");
            //hLink2.Style.Add("color", "black");
            hLink2.Style.Add("cursor", "hand");
            e.Row.Cells[1].Controls.Add(hLink2);

            for (int l = 4, j = 0; l < e.Row.Cells.Count; l ++)
            {

                int iTtl_Drs = (e.Row.Cells[l].Text == "-" || e.Row.Cells[l].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int iDrs_Mt = (e.Row.Cells[l + 1].Text == "-" || e.Row.Cells[l + 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);

                int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                e.Row.Cells[l + 2].Text = iDrs_Msd.ToString();
                if (iTtl_Drs != 0)
                {
                    e.Row.Cells[l + 3].Text = (Decimal.Divide((iDrs_Mt * 100), iTtl_Drs)).ToString("0.##");
                }

                if (e.Row.Cells[l].Text != "0")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[l].Text;
                    sSf_code = dtrowClr.Rows[indx][1].ToString();
                    sSf_Name = dtrowClr.Rows[indx][2].ToString();
                    int cMnth = iLstVstmnt[j];
                    int cYr = iLstVstyr[j];

                    if (cMnth == 12)
                    {
                        sCurrentDate = "01-01-" + (cYr + 1).ToString();
                    }
                    else
                    {
                        sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                    }
                    hLink.Attributes.Add("href", "javascript:showMissedDR('" + sSf_code + "',  '" + cMnth + "', '" + cYr + "','" + sSf_Name + "')");
                    //hLink.ToolTip = "Click here";
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    hLink.Style.Add("text-decoration", "none");
                    //hLink.Style.Add("color", "black");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[l].Controls.Add(hLink);

                }

                l += 3;

                j++;
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
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
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
            e.Row.Cells[3].Wrap = false;
        }
    }

   
    
}