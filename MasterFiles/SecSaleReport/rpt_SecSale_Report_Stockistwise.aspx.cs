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


public partial class MasterFiles_AnalysisReports_rpt_SecSale_Report_Stockistwise : System.Web.UI.Page
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
    int Sec_Cnt = 0;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;

    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsSecSales = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    DataSet dsSub = new DataSet();
    string sub_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            TYear = Request.QueryString["To_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            Stok_code = Request.QueryString["Stok_code"].Trim().ToString();
            StName = Request.QueryString["sk_Name"].Trim().ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Secondary Sale Analysis ConSolidated From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:#0077FF;'> " + StName + "</span>";
            if (sf_code.Trim() == "admin")
            {
                sub_code = "-1";
            }
            else
            {
                SubDivision sb = new SubDivision();
                dsSub = sb.getSub_sf(sf_code);
                if (dsSub.Tables[0].Rows.Count > 0)
                {
                    sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    sub_code = sub_code.Remove(sub_code.Length - 1);
                }
            }
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

        if (Stok_code.Trim() == "-2")
        {
            sProc_Name = "SecSale_Analysis_Stockistwise_FFName";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@subdiv", sub_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.CommandTimeout = 600;
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            //  dtrowClr = dsts.Tables[1].Copy();
            // dsts.Tables[1].Columns.RemoveAt(1);
            //dsts.Tables[1].Columns.RemoveAt(2);

            dsts.Tables[1].Columns.Remove("Sl_No");
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Stockist_code1");
            // dsts.Tables[1].Columns.Remove("Sl_No");
            dtrowClr = dsts.Tables[1].Copy();
            // dsts.Tables[1].Columns.RemoveAt(4);
            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();
        }
        else
        {
            sProc_Name = "SecSale_Analysis_Stockistwise_Single_FFName";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@subdiv", sub_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            //  dtrowClr = dsts.Tables[1].Copy();
            //dsts.Tables[1].Columns.RemoveAt(5);
            //dsts.Tables[1].Columns.RemoveAt(1);
            //dsts.Tables[1].Columns.RemoveAt(0);

            dsts.Tables[1].Columns.Remove("Sl_No");
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Stockist_code1");
            dtrowClr = dsts.Tables[1].Copy();
            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();

        }

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

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Stockist Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "State", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Stockist ERP Code", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            //  AddMergedCells(objgridviewrow, objtablecell, 0, "Rate", "#0097AC", true);
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();
            dsSecSales = ss.Get_Sec_Sale_Code(div_code);
            Sec_Cnt = (dsSecSales.Tables[0].Rows.Count) * 2;
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, (dsSecSales.Tables[0].Rows.Count) + 1, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                    TableCell objtablecell2 = new TableCell();
                    TableCell objtablecell3 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Submission Date", "#0097AC", false);
                    if (dsSecSales.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < dsSecSales.Tables[0].Rows.Count; k++)
                        {
                            //   TableCell objtablecell2 = new TableCell();

                            AddMergedCells(objgridviewrow2, objtablecell2, 1, 1, dsSecSales.Tables[0].Rows[k]["Sec_Sale_Name"].ToString(), "#0097AC", false);
                            // AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                        }
                    }
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


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


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
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;
            //
            #region Calculations

            for (int l = 3; l < e.Row.Cells.Count; l += (Sec_Cnt))
            {

            }

            for (int i = 6, j = 0; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].Attributes.Add("align", "Right");
            }
            for (int l = 7, j = 0; l < e.Row.Cells.Count; l++)
            {
                if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();

                    e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:normal;");


                }
                // Double app = (e.Row.Cells[l + 1].Text == "") || (e.Row.Cells[l + 1].Text == "&nbsp;") || (e.Row.Cells[l + 1].Text == "-") || e.Row.Cells[l + 1].Text.Contains("/") ? 0 : Convert.ToDouble(e.Row.Cells[l + 1].Text);
                Double appl = (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") || e.Row.Cells[l].Text.Contains("/") ? 0 : Convert.ToDouble(e.Row.Cells[l].Text);

                //  Double pApprT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString());
                Double pApplT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "-") || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString().Contains("/")) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "&nbsp;") || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString());



                // dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] = (pApprT + app).ToString();
                dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();
                // l ++;
                if (e.Row.Cells[l].Text == "0")
                {
                    e.Row.Cells[l].Text = "";
                }
           
            j++;
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

        #endregion

        if (dtrowClr.Rows[indx][2].ToString() == "Grand Total")
        {
            // e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";

                e.Row.Cells[5].Text = "";
            }
        e.Row.Cells[1].Wrap = false;
        e.Row.Cells[2].Wrap = false;
        e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace(",", "<br/>");

    }

}

}