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

public partial class MasterFiles_ActivityReports_rptTargetVsSales : System.Web.UI.Page
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
    string sub_code = string.Empty;
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string tot = string.Empty;
    Double total;
    DataSet dsSub = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {


        div_code = Request.QueryString["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        //Stok_code = Request.QueryString["Stok_code"].ToString();
        //StName = Request.QueryString["sk_Name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        lblHead.Text = "Target cummulative Sales Report From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + strFieledForceName + "</span>";
        //lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";
        SubDivision sb = new SubDivision();
        dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            if (sub_code != "")
            {
                sub_code = sub_code.Remove(sub_code.Length - 1);
            }
        }
        FillReport();


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

        //if (Stok_code.Trim() == "-2")
        //{
        //sProc_Name = "TargetVsSales_new";
        sProc_Name = "Target_Vs_Sale_Cumm";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@subdiv", sub_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[1].Copy();
        dsts.Tables[1].Columns.RemoveAt(8);
        dsts.Tables[1].Columns.RemoveAt(4);
        dsts.Tables[1].Columns.RemoveAt(2);
        dsts.Tables[1].Columns.RemoveAt(0);
        GrdFixation.DataSource = dsts.Tables[1];
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
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell1 = new TableCell();
            GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 4, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, 0, "Brand Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, 0, "Product Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, 0, "Pack", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, 0, "Rate", "#F1F5F8", true);



            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months1; i++)
            {
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                AddMergedCells(objgridviewrow, objtablecell, 0, 8, sTxt, "#F1F5F8", true);

                AddMergedCells(objgridviewrow1, objtablecell1, 0, 4, "Target", "#F1F5F8", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Current", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#F1F5F8", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Cummulative", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#F1F5F8", false);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 4, "Sales", "#F1F5F8", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Current", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#F1F5F8", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Cummulative", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#F1F5F8", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#F1F5F8", false);



                //AddMergedCells(objgridviewrow, objtablecell2, 0, 2, "Achieve (%)", "#BEC8F0", true);

                cmonth1 = cmonth1 + 1;
                if (cmonth1 == 13)
                {
                    cmonth1 = 1;
                    cyear1 = cyear1 + 1;
                }

            }
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(3, objgridviewrow3);
            //
            #endregion
            //
        }
    }
    //
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        if (celltext == "#")
        {
            objtablecell.Style.Add("color", "#fff");
            objtablecell.Style.Add("border-radius", "8px 0 0 8px");
        }
        else
        {
            objtablecell.Style.Add("color", "#636d73");
        }
        objtablecell.Style.Add("border-bottom", "10px solid #fff");
        objtablecell.Style.Add("font-weight", "401");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        int RowSpan = 2;

        for (int i = GrdFixation.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = GrdFixation.Rows[i];
            GridViewRow prevRow = GrdFixation.Rows[i + 1];
            if (currRow.Cells[1].Text == prevRow.Cells[1].Text)
            {

                // string val = prevRow.Cells[0].Text;
                //slno1 += 1;

                //currRow.Cells[0].Text = slno1.ToString();

                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;

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
            double first = 0, second = 0;
            #region Calculations


            for (int l = 4, j = 0, m = 4; l < e.Row.Cells.Count; l++, m++)
            {

                if (j / 4 > 0)
                {

                    if (j == 7)
                    {
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
                else
                {
                    if (e.Row.Cells[2].Text != "zzzz" && e.Row.Cells[2].Text != "&nbsp;")
                    {
                        e.Row.Cells[l].BackColor = System.Drawing.Color.LightYellow;
                    }

                    j++;
                }

                HyperLink hLink = new HyperLink();
                if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                {

                    hLink.Text = e.Row.Cells[l].Text;


                    //if (m % 5 == 4)
                    //{
                    //    first = Convert.ToDouble(Math.Round(double.Parse(hLink.Text), 0).ToString());
                    //}

                    //else if (m % 5 == 1)
                    //{

                    //    second = Convert.ToDouble(Math.Round(double.Parse(hLink.Text), 0).ToString());
                    //}

                    //else if (m % 5 == 2)
                    //{
                    //    hLink.Text = (second / first * 100).ToString();
                    //    hLink.Text = Math.Round(double.Parse(hLink.Text), 0).ToString();

                    //}


                    //if ((hLink.Text.Contains(".")))
                    //{

                    //}
                    //else
                    //{
                    //    hLink.Text = hLink.Text + ".00";
                    //}

                    //tot = hLink.Text;
                    //if (tot != "-" && tot != "&nbsp;")
                    //{
                    //    total += Convert.ToDouble(tot);
                    //}
                    //e.Row.Cells[l].Controls.Add(hLink);
                }
                if (e.Row.Cells[l].Text == "0")
                {

                    e.Row.Cells[l].Text = "";
                }


                //j++;

            }
            if (e.Row.Cells[2].Text == "zzzz" && e.Row.Cells[2].Text != "&nbsp;")
            {
                //e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Attributes.Add("style", "background-color:" + "green");
                e.Row.Attributes.Add("style", "font-size:" + "12px");
                e.Row.Attributes.Add("style", "font-weight:" + "bold");
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }

            e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
            e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";


            for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
            {


                e.Row.Cells[i].Attributes.Add("align", "left");
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
            //

            bool Check = true;
            if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                e.Row.Cells[0].Text = "";
                for (int l = 3, j = 1; l < e.Row.Cells.Count; l++, j++)
                {

                    if (j % 5 == 0 || j % 5 == 1 || j % 5 == 3)
                    {

                        e.Row.Cells[l].Text = "";
                    }

                }

            }

            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;

            //TableCell tblRow_Count = new TableCell();
            //tblRow_Count.Text = total.ToString();

            //if ((tblRow_Count.Text.Contains(".")))
            //{

            //}
            //else
            //{
            //    tblRow_Count.Text = tblRow_Count.Text + ".00";
            //}

            //if (tblRow_Count.Text == "0" || tblRow_Count.Text == "0.00")
            //{
            //    tblRow_Count.Text = "";
            //}
            //e.Row.Cells.Add(tblRow_Count);
            //tblRow_Count.Attributes.Add("align", "Right");

            //total = 0;
        }

    }


}