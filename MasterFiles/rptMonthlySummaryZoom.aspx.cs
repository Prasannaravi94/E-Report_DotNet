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
using System.Collections;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_rptMonthlySummaryZoom : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string date = string.Empty;
    string mode = string.Empty;
    string sf_name = string.Empty;

    DataTable dtrowClr = new DataTable();
    DataSet dsts = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            date = Request.QueryString["date"].ToString();
            mode = Request.QueryString["mode"].ToString();
            sf_name = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            if (mode == "1")
                lblHead.Text = "Listed doctor Fieldforce for - " + sf_name + "(" + date + ")";
            else if (mode == "2")
                lblHead.Text = "Chemist Fieldforce for -  " + sf_name + "(" + date + ")";
            else if (mode == "3")
                lblHead.Text = "Stockist Fieldforce for - " + sf_name + "(" + date + ")";
            else if (mode == "4")
                lblHead.Text = "Unlisted Doctor Fieldforce for - " + sf_name + "(" + date + ")";

            FillReport();
        }
    }
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("MonthlySummary_zoom", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@Mode", mode);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        //dsts.Tables[0].Columns.Remove("Sf_code");
        if (mode == "2" || mode == "3")
        {
            grdCheStk.DataSource = dsts;
            grdCheStk.DataBind();
            GrdFixation.Visible = false;
        }
        else
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
            grdCheStk.Visible = false;
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

            HyperLink hlink = new HyperLink();
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 1, 0, e.Row.Cells[i].Text, "#0097AC", true);
            }
        }
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (mode == "1")
            //{
            //    e.Row.Cells[8].Width = 300;
            //    e.Row.Cells[9].Width = 300;
            //    e.Row.Cells[10].Width = 300;
            //}
        }
    }
    
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 10;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
         
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
       
        objgridviewrow.Cells.Add(objtablecell);
    }
}