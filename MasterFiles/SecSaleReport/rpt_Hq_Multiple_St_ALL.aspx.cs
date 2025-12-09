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
public partial class MasterFiles_SecSaleReport_rpt_Hq_Multiple_St_ALL : System.Web.UI.Page
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
    DataTable ProdCodes = new DataTable();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    string st_name = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new DataTable();
    DataTable dt_ProdCode = new DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsmgrsf = new DataSet();
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string HQ_code = string.Empty;
    string HQ_name = string.Empty;
    List<string> iProd = new List<string>();
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
            st_name = Request.QueryString["St_Name"].ToString();         
            HQ_name = Request.QueryString["HQ_name"].ToString();         
            strFieledForceName = Request.QueryString["sf_name"].ToString();         
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
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        Product prod = new Product();
        int iMn = 0, iYr = 0;
        string mnt = Convert.ToString(months);
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
        int j = 0;
        // ProdCodes = prod.getProductlist_DataTable(div_code);
        //dt_ProdCode.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dt_ProdCode.Columns["INX"].AutoIncrementSeed = 1;
        //dt_ProdCode.Columns["INX"].AutoIncrementStep = 1;
        //dt_ProdCode.Columns.Add("Product_Detail_Code");
        //for (int i = 0; i < ProdCodes.Rows.Count; i++)
        //{
        //    //j += 1;
        //    //dtsf_code.Rows.Add(j.ToString());

        //    dt_ProdCode.Rows.Add(null, ProdCodes.Rows[i]["Product_Detail_Code"]);
        //}
        //dsmgrsf.Tables.Add(dt_ProdCode);
     
   //     dsmgrsf.Tables.Add(ProdCodes);
   

        DataTable dtPr = new DataTable();
        dtPr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtPr.Columns["INX"].AutoIncrementSeed = 1;
        dtPr.Columns["INX"].AutoIncrementStep = 1;
     

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "SecSale_Analysis_HQ_St_ALL";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        //cmd.Parameters.AddWithValue("@cMnth", cmonth);
        //cmd.Parameters.AddWithValue("@cYrs", cyear);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 600;
     //   cmd.Parameters.AddWithValue("@CatTbl", dtPr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
       

            da.Fill(dsts);
            dtrowClr = dsts.Tables[1].Copy();
            dtrowdt = dsts.Tables[0].Copy();
            dsts.Tables[1].Columns.RemoveAt(5);
            dsts.Tables[1].Columns.RemoveAt(2);
            dsts.Tables[1].Columns.RemoveAt(1);
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
            TableCell objtablecell = new TableCell();
            #endregion
            //
            #region Merge cells


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            SalesForce sf = new SalesForce();

            //string spclty = Request.QueryString["cbTxt"].ToString();         

         

            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
        
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {


                    int iColSpan = 0;
                    //iLstColCnt.Add(3);


                    foreach (DataRow dr in dtrowdt.Rows)
                    {
                        int iCnt = 0;
                        iColSpan += 2;
                        TableCell objtablecell2 = new TableCell();
                        TableCell objtablecell3 = new TableCell();
                        AddMergedCells(objgridviewrow2, objtablecell2, 2, dr["st_HQ"].ToString().Trim(), "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                        iLstVstmnt.Add(cmonth1);
                        iLstVstyr.Add(cyear1);
                        //  iColSpan += 2;
                    }
                    AddMergedCells(objgridviewrow, objtablecell, iColSpan, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
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
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            //
            #endregion
            //
        }
        //  }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
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
                   // e.Row.Cells[l].Text = "";

                    l += 1;
                }

            }


        }


    }

}