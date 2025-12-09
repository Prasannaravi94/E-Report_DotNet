using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Net;
using System.Drawing;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class Fw_Zoom : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string strFieledForceName = string.Empty;
    string div_code = string.Empty;
    DataTable dtrowClr = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();

            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);

            lblHead.Text = "Fieldwork Days for the month of " + strFrmMonth + " - " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }

    }
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "FW_Single_Graph_Des_Only_Detail";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth);
        cmd.Parameters.AddWithValue("@cYrs", FYear);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("des_code");
        dsts.Tables[0].Columns.Remove("des_code1");
        dsts.Tables[0].Columns.Remove("sf_code");
        dsts.Tables[0].Columns.Remove("sf_code1");
        dsts.Tables[0].Columns.Remove("clr");

        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();


            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Desig", "#0097AC", true);        
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "FieldForce Name", "#0097AC", true);           
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Emp Id", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "FW", "#0097AC", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 14;
        //objtablecell.Height = 30;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            

            int RowSpan = 2;
            if (indx == dtrowClr.Rows.Count - 1)
            {
                
                for (int i = GrdFixation.Rows.Count - 2; i >= 0; i--)
                {
                    GridViewRow currRow = GrdFixation.Rows[i];
                    GridViewRow prevRow = GrdFixation.Rows[i + 1];
                    if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
                    {
                        currRow.Cells[0].RowSpan = RowSpan;
                        prevRow.Cells[0].Visible = false;
                        //currRow.Cells[1].RowSpan = RowSpan;
                        //prevRow.Cells[1].Visible = false;
                        //currRow.Cells[2].RowSpan = RowSpan;
                        //prevRow.Cells[2].Visible = false;
                        RowSpan += 1;
                    }
                    else
                    {
                        RowSpan = 2;
                    }
                }
            }
            string a1 = dtrowClr.Rows[indx][2].ToString();
            //if (a1 == "ZZZZ")
            //{
              
            //    e.Row.Cells[0].RowSpan = 1;
               
            //}
            if (a1 == "ZZZZ")
            {
             
                   
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].ColumnSpan = 4;
                e.Row.Cells[4].Text = "Total (%)";
                for (int n = 0; n <= e.Row.Cells.Count - 1; n++)
                {
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[n].Height = 30;
                    e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[n].Style.Add("font-size", "12pt");
                    //e.Row.Cells[n].Style.Add("font-size", "12pt");

                    e.Row.Cells[n].Style.Add("color", "Red");
                    //e.Row.Cells[n].Style.Add("border-color", "black");
                }
            }

            //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
            //{
               
            //    e.Row.Cells[0].Text = "";

            //    e.Row.Cells[0].Style.Add("border", "none");
            //}
            if (e.Row.Cells[5].Text == "0")
            {
                e.Row.Cells[5].Text = "";
            }
            if (e.Row.Cells[6].Text != "-")
            {
                e.Row.Cells[5].Text = e.Row.Cells[5].Text + " ( " + e.Row.Cells[6].Text + " ) ";
            }
            if (e.Row.Cells[7].Text != "-")
            {
                e.Row.Cells[0].Text = e.Row.Cells[0].Text + " ( " + e.Row.Cells[7].Text + " ) ";
                e.Row.Cells[0].Style.Add("font-size", "12pt");
                e.Row.Cells[0].Style.Add("font-weight", "bold");
                e.Row.Cells[0].Style.Add("color", "Red");
                //e.Row.Cells[0].Style.Add("border-color", "Black");
            }
            else
            {
                e.Row.Cells[0].Style.Add("font-size", "14px");
                e.Row.Cells[0].Style.Add("font-weight", "bold");
                e.Row.Cells[0].Style.Add("color", "Blue");
                //e.Row.Cells[0].Style.Add("border-color", "Black");
            }
           e.Row.Cells[6].Visible = false;
           e.Row.Cells[7].Visible = false;
         
        }
    }

}