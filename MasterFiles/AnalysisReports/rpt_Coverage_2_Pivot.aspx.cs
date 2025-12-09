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
public partial class MasterFiles_AnalysisReports_rpt_Coverage_2_Pivot : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();

            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Coverage Analysis 2 - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }

    private void FillReport()
    {
     // int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Coverage_Analysis_2_new";
      
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
        dsts.Tables[0].Columns.RemoveAt(7);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {
            }

            //for (int l = 8; l < e.Row.Cells.Count; l++)
            //{
          
            //    int itotdrs = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
            //     int idays = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);


            //    int iDys = (e.Row.Cells[l + 3].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 3].Text);
            //    int iTtl_Drs = (e.Row.Cells[l + 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 2].Text);
            //    int iDrs_Mt = (e.Row.Cells[l + 4].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 4].Text);
            //    int iDrs_Sn = (e.Row.Cells[l + 5].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 5].Text);

            //    int iDys_ex = (e.Row.Cells[l + 9].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 9].Text);
            //    int iTtl_Drs_ex = (e.Row.Cells[l + 8].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 8].Text);
            //    int iDrs_Mt_ex = (e.Row.Cells[l + 10].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 10].Text);
            //    int iDrs_Sn_ex = (e.Row.Cells[l + 11].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 11].Text);

            //    int iDys_os = (e.Row.Cells[l + 15].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 15].Text);
            //    int iTtl_Drs_os = (e.Row.Cells[l + 14].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 14].Text);
            //    int iDrs_Mt_os = (e.Row.Cells[l + 16].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 16].Text);
            //    int iDrs_Sn_os = (e.Row.Cells[l + 17].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 17].Text);

            //    //int imor_met = (e.Row.Cells[l + 20].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 20].Text);
            //   // int imor_seen = (e.Row.Cells[l + 21].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 21].Text);
            //    //int ieve_met = (e.Row.Cells[l + 24].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 24].Text);
            //    //int ieve_seen = (e.Row.Cells[l + 25].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 25].Text);

            //    if (iTtl_Drs != 0)
            //    {
            //        e.Row.Cells[l + 6].Text = (Decimal.Divide((iDrs_Mt * 100), iTtl_Drs)).ToString("0.##");
                    
            //    }
            //    if (iDys != 0)
            //    {
            //        e.Row.Cells[l + 7].Text = (Decimal.Divide(iDrs_Sn, iDys)).ToString("0.##");                  

            //    }
            //    if (iTtl_Drs_ex != 0)
            //    {
            //        e.Row.Cells[l + 12].Text = (Decimal.Divide((iDrs_Mt_ex * 100), iTtl_Drs_ex)).ToString("0.##");

            //    }
            //    if (iDys_ex != 0)
            //    {
            //        e.Row.Cells[l + 13].Text = (Decimal.Divide(iDrs_Sn_ex, iDys_ex)).ToString("0.##");

            //    }
            //    if (iTtl_Drs_os != 0)
            //    {
            //        e.Row.Cells[l + 18].Text = (Decimal.Divide((iDrs_Mt_os * 100), iTtl_Drs_os)).ToString("0.##");

            //    }
            //    if (iDys_os != 0)
            //    {
            //        e.Row.Cells[l + 19].Text = (Decimal.Divide(iDrs_Sn_os, iDys_os)).ToString("0.##");

            //    }
            //    if (itotdrs != 0)
            //    {
            //       // e.Row.Cells[l + 22].Text = (Decimal.Divide((imor_met * 100), itotdrs)).ToString("0.##");
            //      //  e.Row.Cells[l + 26].Text = (Decimal.Divide((ieve_met * 100), itotdrs)).ToString("0.##");

            //    }
            //    if (idays != 0)
            //    {
            //       // e.Row.Cells[l + 23].Text = (Decimal.Divide(imor_seen, idays)).ToString("0.##");
            //       // e.Row.Cells[l + 27].Text = (Decimal.Divide(ieve_seen, idays)).ToString("0.##");

            //    }

            //    l += 28;
            //}

            for (int i = 8, j = 0; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {
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
                string backcolor = Convert.ToString(dtrowClr.Rows[j][7].ToString()) == "DEFFBE" ? "A9FFCA" : Convert.ToString(dtrowClr.Rows[j][7].ToString());

                e.Row.Attributes.Add("style", "background-color:" + "#" + backcolor);
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            e.Row.Cells[11].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[18].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[19].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[25].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[26].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[27].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[33].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[34].BackColor = System.Drawing.Color.Yellow;
        }
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

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Emp.Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "First Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Second Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No Of FWD", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No Of FWD Exp", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Ttl Drs", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 24, "Territory Type", "#0097AC", true);
           // AddMergedCells(objgridviewrow, objtablecell,2, 8, "Session Coverage", "#0097AC", true);
           
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2,0, 8, "HQ", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2,0, 8, "EX", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2,0, 8, "OS", "#0097AC", false);

            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();


            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "TC", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "DW", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "Met", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cal Avg", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "Coverage", "#0097AC", false);
           
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt/Call(in Rs)", "#0097AC", false);

            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "TC", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "DW", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Met", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cal Avg", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#0097AC", false);
         
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt/Call(in Rs)", "#0097AC", false);

            AddMergedCells(objgridviewrow3, objtablecell3,0, 0, "TC", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "DW", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Met", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cal Avg", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#0097AC", false);
           
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt", "#0097AC", false);
            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Amt/Call(in Rs)", "#0097AC", false);

            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Mor.Calls Met", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Mor.Calls Seen", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cal Avg", "#0097AC", false);

            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Eve.Calls Met", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Eve.Calls Seen", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Coverage", "#0097AC", false);
            ////AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cal Avg", "#0097AC", false);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
    }

}