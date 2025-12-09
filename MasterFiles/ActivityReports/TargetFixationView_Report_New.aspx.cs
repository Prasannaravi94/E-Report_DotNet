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


public partial class MasterFiles_ActivityReports_TargetFixationView_Report_New : System.Web.UI.Page
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

    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string tot = string.Empty;
    Double total;
    Double Valuee;
    int Qty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{

            div_code = Session["div_code"].ToString();
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
            lblHead.Text = "Target View From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + strFieledForceName + "</span>";
            //lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";
            FillReport();
      //  }
    
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
        sProc_Name = "TargetFixation_Report_new1";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
           // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            dsts.Tables[0].Columns.RemoveAt(0);
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
            TableCell objtablecell2 = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#F1F5F8", true);
          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Rate", "#F1F5F8", true);
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
                    AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#F1F5F8", true);

                    
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#F1F5F8", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#F1F5F8", false);
                   
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
            AddMergedCells(objgridviewrow, objtablecell, 3, "Total", "#F1F5F8", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#F1F5F8", false);

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
        objtablecell.Style.Add("border-bottom", "2px solid #dce2e8");
        objtablecell.Style.Add("font-weight", "401");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            bool Check = true;
            #region Calculations

            for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
            {


                if (e.Row.Cells[l].Text != "0")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[l].Text;

                    tot = hLink.Text;
                    if (tot != "-" && tot != "&nbsp;")
                    {
                        //total += Convert.ToDouble(tot);

                        if (Check == true)
                        {
                            Qty += Convert.ToInt32(Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString());
                            


                            Check = false;
                        }
                        else
                        {
                            Valuee += Convert.ToDouble(Math.Round(double.Parse(e.Row.Cells[l].Text), 0).ToString());
                            Check = true;
                        }
                    }
                }
                if (e.Row.Cells[l].Text == "0")
                {

                    e.Row.Cells[l].Text = "";
                }


                j++;
            }


            e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
            e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                

            for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
            {


                e.Row.Cells[i].Attributes.Add("align", "Right");
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

            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;


            TableCell tblRow_Count = new TableCell();
            tblRow_Count.Text = Qty.ToString();

            if (tblRow_Count.Text == "0")
            {
                tblRow_Count.Text = "";
            }
            e.Row.Cells.Add(tblRow_Count);
            tblRow_Count.Attributes.Add("align", "Right");

            TableCell tblRow_Count2 = new TableCell();
            tblRow_Count2.Text = Valuee.ToString();

            if (tblRow_Count2.Text == "0")
            {
                tblRow_Count2.Text = "";
            }
            e.Row.Cells.Add(tblRow_Count2);
            tblRow_Count2.Attributes.Add("align", "Right");
            total = 0;
            Qty = 0;
            Valuee = 0;
        }     
   
    }
  

}