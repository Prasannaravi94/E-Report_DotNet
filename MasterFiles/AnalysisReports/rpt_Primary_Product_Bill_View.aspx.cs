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

public partial class MasterFiles_AnalysisReports_rpt_Primary_Product_Bill_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string Sf_Code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    DataTable dtrowClr = new System.Data.DataTable();
    int lastRowIndex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        Sf_Code = Request.QueryString["Sf_Code"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        lblHead.Text = "Primary Productwise From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
       
        FillReport();
        foreach (GridViewRow grv in GrdFixation.Rows)
        {
            if (grv.RowType == DataControlRowType.DataRow)
            {
                if (grv.RowIndex > lastRowIndex)
                {
                    //getting the last data row index
                    lastRowIndex = grv.RowIndex;
                }
            }
        }
        //dgTest.Rows[lastRowIndex].CssClass = "lastRowClass";
        GrdFixation.Rows[lastRowIndex].BackColor = System.Drawing.Color.LightBlue;
        GrdFixation.Rows[lastRowIndex].ForeColor = System.Drawing.Color.Red;

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


        sProc_Name = "Primary_bill_Product_View_crn";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", Sf_Code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
           // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(2);
            dsts.Tables[0].Columns.RemoveAt(1);
          //  dsts.Tables[0].Columns.RemoveAt(0);
            GrdFixation.DataSource = dsts;
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);         
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Rate", "#0097AC", true);
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#0097AC", false);
                   
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
            #endregion
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
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    //protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int indx = e.Row.RowIndex;
    //        int k = e.Row.Cells.Count - 5;
    //        //
    //        #region Calculations

    //        for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
    //        {


    //            if (e.Row.Cells[l].Text != "0")
    //            {


    //            }


    //            j++;
    //        }

    //        for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
    //        {


    //            e.Row.Cells[i].Attributes.Add("align", "Right");
    //        }
    //        try
    //        {
    //            int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
    //            //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
    //        }
    //        catch
    //        {
    //            e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
    //        }

    //        #endregion
    //        //
    //        if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
    //        {
    //            e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
    //            e.Row.Cells[0].Text = "";
              
    //            for (int l =3, j = 0; l < e.Row.Cells.Count; l++)
    //            {
    //                e.Row.Cells[l].Text = "";
                   

    //                l += 1;
                   
    //            }

    //        }


    //    }

    //}
}