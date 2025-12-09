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

public partial class MasterFiles_AnalysisReports_rpt_SS_Inventory_Prodwise_Prod : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["Frm_Month"].ToString();
            TYear = Request.QueryString["Frm_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
             strFrmMonth = sf.getMonthName(FMonth.Trim());
             strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Secondary Sale - Inventory Productwise From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            str= " " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear +" Highest Monthly SS Value";
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            // lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:#0077FF;'> " + StName + "</span>";
            SubDivision sb = new SubDivision();
            if (sf_code.Trim() == "admin")
            {
                sub_code = "-1";
            }
            else
            {
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


        sProc_Name = "SecSale_Inventory_Productwise_Sale";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);

        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);

        //dsts.Tables[1].Columns.RemoveAt(6);
        //dsts.Tables[1].Columns.RemoveAt(5);
        //dsts.Tables[1].Columns.RemoveAt(4);
        //dsts.Tables[1].Columns.RemoveAt(3);

        if (dsts.Tables[1].Rows.Count > 1)
        {
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Hq");
            dsts.Tables[1].Columns.Remove("Hq_Order");
            dsts.Tables[1].Columns.Remove("Product_Detail_Code");
            dsts.Tables[1].Columns.Remove("Stock_Code");
            dsts.Tables[1].Columns.Remove("Prod_Code");

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

            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["Frm_Month"].ToString();
            TYear = Request.QueryString["Frm_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
           GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            // GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();
            dsSecSales = ss.getrptfield(div_code);
            TableCell objtablecell2 = new TableCell();
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell,4, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1 , "#0097AC", true);


                    //if (dsSecSales.Tables[0].Rows.Count > 0)
                    //{ 
                    //    for (int k = 0; k < dsSecSales.Tables[0].Rows.Count; k++)
                    //    {
                   
                    //    TableCell objtablecell3 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Sales Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Sales Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Closing Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Closing Value", "#0097AC", false);
                    // AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                    //  AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                    //    }
                    //}
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
            //AddMergedCells(objgridviewrow, objtablecell, 0, "" + str + " " , "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, " " + strToMonth + " Closing Inventory Value", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "No of Man days Closing Inventory", "#0097AC", true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //  objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell,  int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            //   int sal = Math.Abs(Convert.ToInt32(e.Row.Cells[5].Text));
            
                double sale = (e.Row.Cells[5].Text == null) ? 0 : (e.Row.Cells[5].Text == "") ? 0 : (e.Row.Cells[5].Text == "&nbsp;") ? 0 : (e.Row.Cells[5].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[5].Text);
                double close = (e.Row.Cells[7].Text == null) ? 0 : (e.Row.Cells[7].Text == "") ? 0 : (e.Row.Cells[7].Text == "&nbsp;") ? 0 : (e.Row.Cells[7].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[7].Text);
                if (close != 0)
                {
                    double num3 = (double)close / sale * 30;
                    e.Row.Cells[8].Text = Math.Round(num3).ToString();

                }

            string Val = (e.Row.Cells[e.Row.Cells.Count - 1].Text).ToString();
            if (Val != "" && !Val.Contains("-") && Val != null && Val != "&nbsp;" && Val != "∞")
            {
                int a = Convert.ToInt32((Val));
                //int a = Convert.ToInt32(Sale);
                // if (Sale == "66")
                if (a > 45)
                {
                    e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("style", "font-bold:true; font-size:14px; Color:Red;");
                }
            }

            for (int l = 4, j = 0; l < e.Row.Cells.Count; l++)
            {
                
                e.Row.Cells[l].Attributes.Add("align", "Right");
               // l++;
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
            e.Row.Cells[3].Wrap = false;
            #endregion
            //
            if (dtrowClr.Rows[indx][3].ToString() == "Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightYellow;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                //for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                //{
                //    e.Row.Cells[l].Text = "";

                //    l += 1;
                //}

            }
            if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                e.Row.Cells[0].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                //for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                //{
                //    e.Row.Cells[l].Text = "";

                //    l += 1;
                //}

            }
            if (dtrowClr.Rows[indx][2].ToString() == "Total")
            {

                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "Total";
                e.Row.Cells[3].Text = "";
                e.Row.Attributes.Add("style", "background-color:LightGray;font-bold:true; font-size:14px; Color:Red; border-color:Black");
            }

           
        }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //  {
        //      e.Row.Cells[0].ColumnSpan = 4;
        //      e.Row.Cells[1].Visible = false;
        //      e.Row.Cells[2].Visible = false;
        //      e.Row.Cells[3].Visible = false;

        //      //

        //      e.Row.Cells[0].Text = "Total";
        //      e.Row.Cells[5].Text = Value.ToString();
        //      e.Row.Cells[7].Text = Value_1.ToString();
        //      e.Row.Cells[9].Text = Value_2.ToString();
        //  //    e.Row.Cells[11].Text = Value_3.ToString();
        //      e.Row.HorizontalAlign = HorizontalAlign.Center;

        //      e.Row.Font.Bold = true;
        //      e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
        //  }
        //else if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //     //iLstVstmnt
        //    for (int i = 5, j = 0, m = 0; i < e.Row.Cells.Count; i += iLstVstmnt.Count)
        //    {
        //        for (int r = 5; r < e.Row.Cells.Count; r++)
        //        {

        //            if (r == 5)
        //            {
        //                Value += Convert.ToDouble(e.Row.Cells[r].Text);
        //            }
        //            else if (r == 7)
        //            {
        //                Value_1 += Convert.ToDouble(e.Row.Cells[r].Text);
        //            }
        //            else if (r == 9)
        //            {
        //                Value_2 += Convert.ToDouble(e.Row.Cells[r].Text);
        //            }
        //            else if (r == 11)
        //            {
        //                Value_3 += Convert.ToDouble(e.Row.Cells[r].Text);
        //            }
        //            r++;
        //        }
        //    }
        // }
    }

    
    protected void btnExcel_Click(object sender, EventArgs e)
    { 
        string attachment = "attachment; filename=SS_Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm(); 
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}