
#region Assembly
using System;
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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;

using System.ComponentModel;
#endregion

public partial class MasterFiles_AnalysisReports_rpt_Manager_Analysis : System.Web.UI.Page
{

    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsWork = new DataSet();
    string sf_code = string.Empty;
    DataTable dtrowClr = new System.Data.DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FrmDate"].ToString();
        FYear = Request.QueryString["ToDate"].ToString();

        lblHead.Text = "Manager Analysis(HQ wise - Visit)  &nbsp;" + Convert.ToDateTime(FMonth).ToString("dd MMMM yyyy") + " &nbsp; to &nbsp;" + Convert.ToDateTime(FYear).ToString("dd MMMM yyyy");
        LblForceName.Text = "Field Force Name : " + sf_name;


        FillReport();
    }

    private void FillReport()
    {
        DataSet dsdes = new DataSet();
        strQry = "select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr where division_code='34' order by WorkType_Orderly";
        dsWork = db_ER.Exec_DataSet(strQry);
        string des_code = string.Empty;
        DataTable dtwork = new DataTable();
        dtwork.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtwork.Columns["INX"].AutoIncrementStep = 1;
        dtwork.Columns["INX"].AutoIncrementSeed = 1;
        dtwork.Columns.Add("worktype", typeof(int));

        if (dsWork.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsWork.Tables[0].Rows.Count; i++)
            {
                dtwork.Rows.Add(null, dsWork.Tables[0].Rows[i]["WorkType_Code_M"].ToString());
            }
        }

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Manager_Analysis", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code.Trim());
        cmd.Parameters.AddWithValue("@Msf_code", sf_code.Trim());
        cmd.Parameters.AddWithValue("@Frm_Date", FMonth.Trim());
        cmd.Parameters.AddWithValue("@To_Date", FYear.Trim());
        cmd.Parameters.AddWithValue("@dtwork", dtwork);

        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        dtrowClr = ds.Tables[0].Copy();
        ds.Tables[0].Columns.Remove("sf_code");
        ds.Tables[0].Columns.Remove("clm");
        ds.Tables[0].Columns.Remove("sf_designation_short_name");
        ds.Tables[0].Columns.Remove("jod");
        ds.Tables[0].Columns.Remove("username");
        ds.Tables[0].Columns.Remove("sf_emp_id");
        ds.Tables[0].Columns.Remove("sf_hq");
        ds.Tables[0].Columns.Remove("desig_clr");
        ds.Tables[0].Columns.Remove("sf_code1");


        GrdFixationMode.DataSource = ds;
        GrdFixationMode.DataBind();
        con.Close();
    }

    protected void GrdFixationMode_RowCreated(object sender, GridViewRowEventArgs e)
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
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Last DCR Date", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "No of Days", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total Drs. Met", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Call Avg", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total Chem. Met", "#0097AC", true);


            foreach (DataRow dtRow in dsWork.Tables[0].Rows)
            {
                int iCnt = 0;
                //iColSpan = ((dsWork.Tables[0].Rows.Count));
                AddMergedCells(objgridviewrow, objtablecell, 0, dtRow["Worktype_Name_M"].ToString(), "#0097AC", false);

            }
            AddMergedCells(objgridviewrow, objtablecell, 0, "Work With (Days)", "#0097AC", true);



            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);




            //
            #endregion
            //
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 1;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("color", "white");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixationMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
                int countCol = dtrowClr.Columns.Count;

                for (int iCol = 0; iCol < countCol; iCol++)
                {
                    DataColumn col = dtrowClr.Columns[iCol];
                    if (col.ColumnName == "ZZZ")
                    {
                        e.Row.Cells[e.Row.Cells.Count - 1].Text = e.Row.Cells[e.Row.Cells.Count - 1].Text.Replace(",", "<br />");  
                    }
                }
          
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j]["desig_clr"].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
        }
    }
}