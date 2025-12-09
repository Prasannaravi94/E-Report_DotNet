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

public partial class MasterFiles_MR_ListedDoctor_rptService_Hospitalwise_DoctorDetail : System.Web.UI.Page
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Month = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        sfName = Request.QueryString["S_Name"].ToString();

        if (!Page.IsPostBack)
        {
            // strFieledForceName = Request.QueryString["Sf_Name"].ToString();   
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(Month.Trim());
            // string strToMonth = sf.getMonthName(Year.Trim());
            lblHead.Text = "Hospitalwise Service Detail " + strFrmMonth + " " + Year;
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
            sProc_Name = "SP_Get_Hospitalwise_DoctorList_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Division_Code");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            dsts.Tables[0].Columns.Remove("Hospital_Code");
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else if (sf_code == "admin")
        {
            sProc_Name = "SP_Get_Hospitalwise_DoctorList_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Division_Code");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            dsts.Tables[0].Columns.Remove("Hospital_Code");
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();

        }
        else
        {
            sProc_Name = "SP_Get_Hospitalwise_DoctorList_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Division_Code");
            dsts.Tables[0].Columns.Remove("Sf_Code");
            dsts.Tables[0].Columns.Remove("Hospital_Code");
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            GrdFixation.DataSource = dsts;
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Hospital Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Hospital Address", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Speciality", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Given", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Service Amount", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Business Received (Rs)", "#0097AC", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

}