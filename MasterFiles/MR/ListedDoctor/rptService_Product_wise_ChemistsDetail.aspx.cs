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

public partial class MasterFiles_MR_ListedDoctor_rptService_Product_wise_ChemistsDetail : System.Web.UI.Page
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
    #endregion
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
            lblHead.Text = "Chemistswise Product Service Detail " + strFrmMonth + " " + Year;
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
            // sProc_Name = "SP_Get_Productwise_Chemists_DetailRpt";
            sProc_Name = "SP_Get_Productwise_ChemistDet_Adminwise";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Chemists_Code");
            // dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            gvProduct.DataSource = dsts;
            gvProduct.DataBind();
        }
        else if (sf_code == "admin")
        {
            sProc_Name = "SP_Get_Productwise_ChemistDet_Adminwise";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Chemists_Code");
            // dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            gvProduct.DataSource = dsts;
            gvProduct.DataBind();

        }
        else
        {
            sProc_Name = "SP_Get_Productwise_ChemistDet_Adminwise";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dsts.Tables[0].Columns.Remove("Chemists_Code");
            // dsts.Tables[0].Columns.Remove("Doc_Class_ShortName");
            gvProduct.DataSource = dsts;
            gvProduct.DataBind();
        }
    }

    protected void gvMyDayPlan_OnDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int rowIndex = gvProduct.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = gvProduct.Rows[rowIndex];
            GridViewRow gvPreviousRow = gvProduct.Rows[rowIndex + 1];
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

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowIndex > 0)
        //    {
        //        GridViewRow previousRow = gvMyDayPlan.Rows[e.Row.RowIndex - 1];
        //        if (e.Row.Cells[1].Text == previousRow.Cells[1].Text)
        //        {
        //            if (previousRow.Cells[0].RowSpan == 0)
        //            {
        //                previousRow.Cells[0].RowSpan += 2;
        //                e.Row.Cells[0].Visible = false;
        //            }
        //        }
        //    }
        //}
    }
}