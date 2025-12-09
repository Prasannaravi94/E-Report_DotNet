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
using System.Text;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MIS_Reports_Visit_Summary_AM_View : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsfixclmn = null;
    DataSet dsDoctorDNM = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    string Detailed = string.Empty;
    DataSet dsSales = new DataSet();
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int total = 0;
    int total2 = 0;
    int total3 = 0;
    int total4 = 0;
    int fulltotal = 0;
    string tot = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["cur_month"].ToString();
        FYear = Request.QueryString["cur_year"].ToString();

        sfname = Request.QueryString["sf_name"].ToString();

        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);


        lblHead.Text = "Expense Consolidated View for the Month of " + strFMonthName + " " + FYear;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        FillSample_Prd();
        

    }

    private void FillSample_Prd()
    {
       // int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));

      

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();

        SqlCommand cmd = new SqlCommand("View_Exp_cons", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@month", cmonth);
            cmd.Parameters.AddWithValue("@year", cyear);
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(7);
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(1);
            Grdprd.DataSource = dsts;
            Grdprd.DataBind();
        


    }

    protected void Grdprd_RowCreated(object sender, GridViewRowEventArgs e)
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
           
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "S.No", "#5E5D8E", true,true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Emp ID", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "FieldForce Name", "#5E5D8E", true, false);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Designation", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "HQ", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Fare + Metro Allowance", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "DA", "#5E5D8E", true, true);
            Distance_calculation dr = new Distance_calculation();
            dsfixclmn = dr.getfixedmeridian(divcode);


            for (int k = 0; k < dsfixclmn.Tables[0].Rows.Count; k++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 0, dsfixclmn.Tables[0].Rows[k]["expense_parameter_name"].ToString(), "#5E5D8E", true, true);
            }
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Addition", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Deduction", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Claimed Amount", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Grand Expense Total", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Total Incentive", "#5E5D8E", true, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Remarks (Up to 40char.)", "#5E5D8E", true, false);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "H.O. Approval for Expense Checking", "#5E5D8E", true, false);


           
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
           
            //
            #endregion
            //
        }


    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan,bool wra)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Wrap = wra;
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = 5,  n = 0; i < e.Row.Cells.Count; i++, n++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                       
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][6].ToString()));
                       
                      
                    }

                   

                }

               
                
               

             
            }
        
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}