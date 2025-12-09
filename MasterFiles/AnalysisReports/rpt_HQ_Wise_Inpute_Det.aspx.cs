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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_rpt_HQ_Wise_Inpute_Det : System.Web.UI.Page
{
    //
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
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    List<int> iLstVstCnt = new List<int>();
    string state1, state;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        //TMonth = Request.QueryString["TMonth"].ToString();
        //TYear = Request.QueryString["Tyear"].ToString();
        //sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        //strmode = Request.QueryString["Vacant"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        //string strToMonth = sf.getMonthName(TMonth);


        lblHead.Text = "Input Utilization Update Details of " + strFieledForceName + " for the Month of " + strFrmMonth + " - " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillReport();
    }
    //
    #endregion
    //
    #region FillReport
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Input_Des";//jasmine

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@month", FMonth);
        cmd.Parameters.AddWithValue("@Year", FYear);
        cmd.Parameters.AddWithValue("@Mode", "3");
        cmd.Parameters.AddWithValue("@Gift_Mode", "0");
        cmd.Parameters.AddWithValue("@Gift_Code", "");
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        ///////////////////////
        int cnt = 0;
        DataTable dsts1 = new DataTable();

        decimal a4 = 0, a5 = 0, a6 = 0, a7 = 0;
        decimal a44 = 0, a55 = 0, a66 = 0, a77 = 0;
        for (int k = 0; k <= dsts.Tables[0].Columns.Count - 1; k++)
        {
            string clm = dsts.Tables[0].Columns[k].ToString();
            dsts1.Columns.Add(clm);
        }
        for (int kk = 0; kk <= dsts.Tables[0].Rows.Count - 1; kk++)
        {

            state = dsts.Tables[0].Rows[kk][3].ToString();
            if (kk > 0)
                state1 = dsts.Tables[0].Rows[kk - 1][3].ToString();
            if (kk == 0)
            {
                dsts1.Rows.Add();
                dsts1.Rows[cnt][0] = dsts.Tables[0].Rows[kk][0];
                dsts1.Rows[cnt][1] = dsts.Tables[0].Rows[kk][1];
                dsts1.Rows[cnt][2] = dsts.Tables[0].Rows[kk][2];
                dsts1.Rows[cnt][3] = dsts.Tables[0].Rows[kk][3];
                dsts1.Rows[cnt][4] = dsts.Tables[0].Rows[kk][4];
                a4 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][4].ToString());
                a44 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][4].ToString());
                dsts1.Rows[cnt][5] = dsts.Tables[0].Rows[kk][5];
                a5 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][5].ToString());
                a55 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][5].ToString());
                dsts1.Rows[cnt][6] = dsts.Tables[0].Rows[kk][6];
                a6 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][6].ToString());
                a66 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][6].ToString());
                dsts1.Rows[cnt][7] = dsts.Tables[0].Rows[kk][7];
                a7 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][7].ToString());
                a77 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][7].ToString());
                cnt += 1;
            }
            if (kk > 0)
            {
                if (state == state1)
                {
                    dsts1.Rows.Add();
                    dsts1.Rows[cnt][0] = dsts.Tables[0].Rows[kk][0];
                    dsts1.Rows[cnt][1] = dsts.Tables[0].Rows[kk][1];
                    dsts1.Rows[cnt][2] = dsts.Tables[0].Rows[kk][2];
                    dsts1.Rows[cnt][3] = dsts.Tables[0].Rows[kk][3];
                    dsts1.Rows[cnt][4] = dsts.Tables[0].Rows[kk][4];
                    a4 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][4].ToString());
                    a44 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][4].ToString());
                    dsts1.Rows[cnt][5] = dsts.Tables[0].Rows[kk][5];
                    a5 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][5].ToString());
                    a55 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][5].ToString());
                    dsts1.Rows[cnt][6] = dsts.Tables[0].Rows[kk][6];
                    a6 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][6].ToString());
                    a66 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][6].ToString());
                    dsts1.Rows[cnt][7] = dsts.Tables[0].Rows[kk][7];
                    a7 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][7].ToString());
                    a77 += Convert.ToDecimal(dsts.Tables[0].Rows[kk][7].ToString());
                    cnt += 1;
                }
                if (state != state1)
                {
                    dsts1.Rows.Add();
                    dsts1.Rows[cnt][1] = state1 + " - Sub Total";
                    dsts1.Rows[cnt][4] = a4;
                    dsts1.Rows[cnt][5] = a5;
                    dsts1.Rows[cnt][6] = a6;
                    dsts1.Rows[cnt][7] = a7;
                    a4 = 0; a5 = 0; a6 = 0; a7 = 0;
                    cnt += 1;
                }
            }
        }
        dsts1.Rows.Add();
        dsts1.Rows[cnt][1] = "Grand Total";
        dsts1.Rows[cnt][4] = a44;
        dsts1.Rows[cnt][5] = a55;
        dsts1.Rows[cnt][6] = a66;
        dsts1.Rows[cnt][7] = a77;

        dsts1.Columns.RemoveAt(3);
        dsts1.Columns.RemoveAt(2);
        dsts1.Columns.RemoveAt(0);
        ///////////////////////

        dtrowClr = dsts.Tables[0].Copy();
        dtrowClr = dsts1.Copy();
        dsts.Tables[0].Columns.RemoveAt(3);
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(0);

        GrdFixation.DataSource = dsts1;
        GrdFixation.DataBind();
    }
    #endregion
    //  
    #region GrdFixation_RowCreated
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
            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#003300", true);

            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#003300", true);
            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "SS", "#003300", true);
            //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "OS", "#003300", true);

            //for (int i = 0; i <= 5; i++)
            {
                //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                int iColSpan = 0;
                AddMergedCells(objgridviewrow, objtablecell, 0, 5, "INPUT Utilization Update", "#003300", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Division", "#003300", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "Input Name", "#003300", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Opening", "#003300", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Despatch Qty", "#003300", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Doctor Issued", "#003300", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Chemist Issued", "#003300", false);


            }
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

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
        //objtablecell.Style.Add("color", "white");
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
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
    #endregion
    //    
    #region GrdFixation_RowDataBound
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;

            try
            {
                int j = Convert.ToInt32(e.Row.Cells[2].Text) - 1;
                if (e.Row.Cells[0].Text.Contains( "Sub Total"))
                    e.Row.Attributes.Add("style", "background-color:" + "#bfdef3");
                else if (e.Row.Cells[0].Text == "Grand Total")
                    e.Row.Attributes.Add("style", "background-color:" + "#4e96c7");
                //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][2].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

        }
    }
    #endregion
    //
}