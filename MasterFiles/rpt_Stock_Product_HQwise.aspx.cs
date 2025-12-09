using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class rpt_Stock_Product_HQwise : System.Web.UI.Page
{

    DataSet dsDoctor = null;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    private string celltext;
    private int colspan;
    private bool bRowspan;
    private string backcolor;
    int SNo = 1;
    string strFieledForceName = string.Empty;
    string strVmode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["Div_Code"].ToString();
        FMonth = Request.QueryString["Invoice_Month"].ToString();
        FYear = Request.QueryString["Invoice_Year"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();

        strFieledForceName = Request.QueryString["sf_name"].ToString();


        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        lblHead.Text = "Primary Sale - HQwise for the Month of  " + strFrmMonth + " " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillReport();
    }

    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "ProductSale_HQwise";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Invoice_Month", FMonth);
        cmd.Parameters.AddWithValue("@Invoice_Year", FYear);
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        GrdSaleHQ.DataSource = dsts;
        GrdSaleHQ.DataBind();
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
        objtablecell.Style.Add("border-bottom", "10px solid #fff");
        objtablecell.Style.Add("font-weight", "401");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdSaleHQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;

            #region Calculations


            e.Row.Cells[0].Text = SNo.ToString();

            SNo = SNo + 1;
            string a1 = e.Row.Cells[1].Text;
            if (a1 == "ZZ")
            {
                e.Row.Cells[0].Visible = false;


                e.Row.Cells[1].ColumnSpan = 2;

                e.Row.Cells[1].Text = "Grand Total";


                for (int n = 1; n <= e.Row.Cells.Count - 1; n++)
                {
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[n].Height = 30;
                    e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[n].Style.Add("font-size", "12pt");
                    e.Row.Cells[n].Style.Add("color", "Red");
                    //e.Row.Cells[n].Style.Add("border-color", "black");

                }
            }


            if (a1 != "zz")
            {
                e.Row.Cells[0].Attributes.Add("align", "Left");
                e.Row.Cells[1].Attributes.Add("align", "Left");
                e.Row.Cells[2].Attributes.Add("align", "Left");

                for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("align", "right");

                }
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;

            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }


            #endregion

        }
    }
    protected void GrdSaleHQ_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ-Wise", "#F1F5F8", true);


            AddMergedCells(objgridviewrow, objtablecell, 2, "Sales", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Sales Return", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Return(Exp/Brk)", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Total Qty", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Total Value", "#F1F5F8", true);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#F1F5F8", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#F1F5F8", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#F1F5F8", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "", "#F1F5F8", false);




            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }


}