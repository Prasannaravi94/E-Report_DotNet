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

public partial class MasterFiles_SecSaleReport_rpt_Sale_Hqwise : System.Web.UI.Page
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

    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
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
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            //TMonth = Request.QueryString["To_Month"].ToString();
            //TYear = Request.QueryString["To_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            //Stok_code = Request.QueryString["Stok_code"].ToString();
            //StName = Request.QueryString["sk_Name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Stockist - HQ wise Sale From " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            //    lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";
            FillReport();
        }
    }
    private void FillReport()
    {
     //   int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "SecSale_Analysis_Hqwise_Temp";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@cMnth", cmonth);
            cmd.Parameters.AddWithValue("@cYrs", cyear);
            cmd.CommandTimeout = 600;
          //  cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            //   cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[1].Copy();
            dsts.Tables[1].Columns.RemoveAt(5);
            dsts.Tables[1].Columns.RemoveAt(1);
            dsts.Tables[1].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();

      





    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        string Sf_Code = string.Empty;
        if (e.Row.RowType == DataControlRowType.Header)
        {
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
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Product Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Pack", "#0097AC", true);
          //  AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Rate", "#0097AC", true);
           // int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            //ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            string strQry = "";
            SalesForce sf1 = new SalesForce();
            DCR dc = new DCR();
            //if (sf_code.Contains("MR"))
            //{
            //    dsSalesForce = sf1.getSfName(sf_code);
            //}
            //else
            //{
            //    dsSalesForce = dc.get_Team(div_code, sf_code);
            //}
            //if (dsSalesForce.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dsSalesForce.Tables[0].Rows)
            //    {
            //        string SfCode = dr["SF_Code"].ToString();

            //        Sf_Code += SfCode + ",";

            //    }

            //    Sf_Code = Sf_Code.Substring(0, Sf_Code.Length - 1);

            //    //DataSet dsStockist;
            //    Stockist objStock = new Stockist();

            //    dsDoctor = objStock.GetStockist_Detail_Hq(div_code, Sf_Code);
         

            //}

            Stockist objStock = new Stockist();
            int iR = 0;
            dsDoctor = objStock.GetSale_HQ(div_code, Request.QueryString["sf_code"].ToString(), cmonth1, cyear1);
            SecSale sc = new SecSale();

           
                    int iColSpan = 0;

                   
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsDoctor.Tables[0].Rows.Count; j++)
                        {
                            int iCnt = 0;
                            iColSpan += 2;
                            //   iColSpan += Convert.ToInt32(dtRow["Territory"].ToString()) + 2;
                            //iCnt = Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 2;s
                            iLstVstCnt.Add(1);

                            AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, dsDoctor.Tables[0].Rows[j]["st_Hq"].ToString(), "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false);
                        }
                    }
                    // iColSpan += 2;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#0097AC", true);
                    
                    //   string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    //    AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#0097AC", true);
                    //cmonth1 = cmonth1 + 1;
                    //if (cmonth1 == 13)
                    //{
                    //    cmonth1 = 1;
                    //    cyear1 = cyear1 + 1;
                    //}




                 



               // }

                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            #endregion
            }
      //  }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
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

            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;

            for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
            {


                e.Row.Cells[i].Attributes.Add("align", "Right");
            }
          
            #endregion
            //
            if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                e.Row.Cells[0].Text = "";
                for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                {
                    e.Row.Cells[l].Text = "";

                    l += 1;
                }

            }


        }


    }

}