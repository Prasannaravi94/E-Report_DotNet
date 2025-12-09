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
using System.Drawing.Text;

public partial class MasterFiles_MR_ListedDoctor_rptService_Doctorwise_Detail : System.Web.UI.Page
{
    #region
    string div_code = string.Empty;
    DataSet dsSecSales = null;
    DataSet dsSale = null;
    DataSet dsState = new DataSet();
    DataSet dsReport = null;
    string state_code = string.Empty;
    string sf_code = string.Empty;
    int FMonth = -1;
    int FYear = -1;
    int TMonth = -1;
    int TYear = -1;
    int stock_code = -1;
    int iDay = -1;
    DateTime SelDate;
    string sDate = string.Empty;
    string sf_name = string.Empty;
    int rpttype = -1;
    DataSet dssf = null;
    DataSet dsStock = null;
    DataSet dsRate = new DataSet();

    DataSet dsSub = null;
    string sub_code = string.Empty;

    string Month = string.Empty;
    string Year = string.Empty;
    string sfName = string.Empty;
    string DrCode = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Month = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        sfName = Request.QueryString["S_Name"].ToString();
        DrCode = Request.QueryString["Dr_Code"].ToString();

        if (!Page.IsPostBack)
        {
            // strFieledForceName = Request.QueryString["Sf_Name"].ToString();   
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(Month.Trim());
            // string strToMonth = sf.getMonthName(Year.Trim());
            lblHead.Text = "Doctorwise Service Detail " + strFrmMonth + " " + Year;
            LblForceName.Text = "Field Force Name : " + sfName;
            // // lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";
            // FillReport();

            FillReport_1();
        }
    }

    private void FillReport_1()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        if (sf_code.Contains("MGR"))
        {
            
            sProc_Name = "SP_Get_Doctorwise_EntryDetail_SingleDr";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@Dr_Code", DrCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();

            string Total = dsts.Tables[1].Rows[0]["Service_Amt"].ToString();
            string Tot_Bus = dsts.Tables[1].Rows[0]["Total_Business_Expect"].ToString();

            GrdFixation.FooterRow.Cells[0].Text = "Total";
            GrdFixation.FooterRow.Cells[0].ColumnSpan = 10;
            GrdFixation.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            GrdFixation.FooterRow.Cells[0].ForeColor = System.Drawing.Color.Red;
            //GrdFixation.FooterRow.Cells[0].Font = "12px";
            GrdFixation.FooterRow.Cells[1].Text = Total;
            GrdFixation.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells[2].Text = Tot_Bus;
            GrdFixation.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(5);
            GrdFixation.FooterRow.Cells.RemoveAt(6);
            //  GrdFixation.FooterRow.Cells.RemoveAt(2);
            GrdFixation.FooterRow.Cells.RemoveAt(3);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
        }
        else if (sf_code == "admin")
        {
            sProc_Name = "SP_Get_Doctorwise_EntryDetail_SingleDr";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@Dr_Code", DrCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();

            string Total = dsts.Tables[1].Rows[0]["Service_Amt"].ToString();
            string Tot_Bus = dsts.Tables[1].Rows[0]["Total_Business_Expect"].ToString();

            GrdFixation.FooterRow.Cells[0].Text = "Total";
            GrdFixation.FooterRow.Cells[0].ColumnSpan = 10;
            GrdFixation.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            GrdFixation.FooterRow.Cells[0].ForeColor = System.Drawing.Color.Red;
            //GrdFixation.FooterRow.Cells[0].Font = "12px";
            GrdFixation.FooterRow.Cells[1].Text = Total;
            GrdFixation.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells[2].Text = Tot_Bus;
            GrdFixation.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(5);
            GrdFixation.FooterRow.Cells.RemoveAt(6);
            //  GrdFixation.FooterRow.Cells.RemoveAt(2);
            GrdFixation.FooterRow.Cells.RemoveAt(3);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
        }
        else
        {
            sProc_Name = "SP_Get_Doctorwise_EntryDetail_SingleDr";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@Dr_Code", DrCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();

            string Total = dsts.Tables[1].Rows[0]["Service_Amt"].ToString();
            string Tot_Bus = dsts.Tables[1].Rows[0]["Total_Business_Expect"].ToString();

            GrdFixation.FooterRow.Cells[0].Text = "Total";
            GrdFixation.FooterRow.Cells[0].ColumnSpan = 10;
            GrdFixation.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            GrdFixation.FooterRow.Cells[0].ForeColor = System.Drawing.Color.Red;
            //GrdFixation.FooterRow.Cells[0].Font = "12px";
            GrdFixation.FooterRow.Cells[1].Text = Total;
            GrdFixation.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells[2].Text = Tot_Bus;
            GrdFixation.FooterRow.Cells[2].ForeColor = System.Drawing.Color.Red;

            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(5);
            GrdFixation.FooterRow.Cells.RemoveAt(6);
            //  GrdFixation.FooterRow.Cells.RemoveAt(2);
            GrdFixation.FooterRow.Cells.RemoveAt(3);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
            GrdFixation.FooterRow.Cells.RemoveAt(4);
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Employee Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Speciality", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Given", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Amount(in Rs/-)", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Business Expected", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "ROI Duration (in Months)", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Business Received (Rs)", "#0097AC", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            for (int i = 0; i < GrdFixation.Columns.Count - 1; i++)
            {
                e.Row.Cells.RemoveAt(1);
            }
            e.Row.Cells[0].ColumnSpan = GrdFixation.Columns.Count;
        }

    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        //objtablecell.ColumnSpan = colspan;
        //if ((colspan == 0) && bRowspan)
        //{
        //    objtablecell.RowSpan = 3;
        //}
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void gvDoctor_OnDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int rowIndex = GrdFixation.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = GrdFixation.Rows[rowIndex];
            GridViewRow gvPreviousRow = GrdFixation.Rows[rowIndex + 1];
            for (int cellCount = 0; cellCount < gvRow.Cells.Count; cellCount++)
            {
                if (gvRow.Cells[1].Text ==
                                    gvPreviousRow.Cells[1].Text)
                {
                    if (gvPreviousRow.Cells[1].RowSpan < 2)
                    {
                        gvRow.Cells[1].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[1].RowSpan =
                            gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[1].Visible = false;
                }
                if (gvRow.Cells[2].Text ==
                                  gvPreviousRow.Cells[2].Text)
                {
                    if (gvPreviousRow.Cells[2].RowSpan < 2)
                    {
                        gvRow.Cells[2].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[2].RowSpan =
                            gvPreviousRow.Cells[2].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[2].Visible = false;
                }


            }
        }


    }

}