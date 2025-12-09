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

public partial class MasterFiles_MR_ListedDoctor_rpt_DoctorService_MR_wise_Status : System.Web.UI.Page
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

    string Status = string.Empty;


    int Sec_Cnt = 0;

    DataSet dsSub = null;
    string sub_code = string.Empty;

    DataTable dtrowClr = new System.Data.DataTable();

    string FMonth1 = string.Empty;
    string FYear1 = string.Empty;
    string TMonth1 = string.Empty;
    string TYear1 = string.Empty;
    string sfName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth1 = Request.QueryString["FMonth"].ToString();
        FYear1 = Request.QueryString["FYear"].ToString();
        TMonth1 = Request.QueryString["TMonth"].ToString();
        TYear1 = Request.QueryString["TYear"].ToString();
        Status = Request.QueryString["Status"].ToString();

        if (!Page.IsPostBack)
        {
           
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth1.Trim());
           
            DateTime startOfMonth = new DateTime(Convert.ToInt32(FYear1), Convert.ToInt32(FMonth1), 1);   //new DateTime(year, month, 1);
            DateTime endOfMonth = new DateTime(Convert.ToInt32(TYear1), Convert.ToInt32(TMonth1), DateTime.DaysInMonth(Convert.ToInt32(TYear1), Convert.ToInt32(TMonth1)));

            string formatted = startOfMonth.ToString("dd-MM-yyyy");
            string ToDate = endOfMonth.ToString("dd-MM-yyyy");

            lblHead.Text = "CRM Status from " + formatted + " to " + ToDate;
           
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
            sProc_Name = "SP_Get_DoctorService_MR_wise_Status_Rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@FMonth", FMonth1);
            cmd.Parameters.AddWithValue("@FYear", FYear1);
            cmd.Parameters.AddWithValue("@TMonth", TMonth1);
            cmd.Parameters.AddWithValue("@TYear", TYear1);
            cmd.Parameters.AddWithValue("@Status", Status);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);

            dtrowClr = dsts.Tables[0].Copy();

            dsts.Tables[0].Columns.Remove("sf_code");
            dsts.Tables[0].Columns.Remove("Employee_Id");
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");     
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else if (sf_code == "admin")
        {
            sProc_Name = "SP_Get_DoctorService_MR_wise_Status_Rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@FMonth", FMonth1);
            cmd.Parameters.AddWithValue("@FYear", FYear1);
            cmd.Parameters.AddWithValue("@TMonth", TMonth1);
            cmd.Parameters.AddWithValue("@TYear", TYear1);
            cmd.Parameters.AddWithValue("@Status", Status);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("sf_code");
            dsts.Tables[0].Columns.Remove("Employee_Id");
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");     
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();

        }
        else
        {
            sProc_Name = "SP_Get_DoctorService_MR_wise_Status_Rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@FMonth", FMonth1);
            cmd.Parameters.AddWithValue("@FYear", FYear1);
            cmd.Parameters.AddWithValue("@TMonth", TMonth1);
            cmd.Parameters.AddWithValue("@TYear", TYear1);
            cmd.Parameters.AddWithValue("@Status", Status);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("sf_code");
            dsts.Tables[0].Columns.Remove("Employee_Id");
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
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Employee Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Speciality", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Raised Date", "#0097AC", true);
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