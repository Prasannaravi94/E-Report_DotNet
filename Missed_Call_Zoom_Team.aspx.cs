#region Assembly
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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
#endregion

public partial class Missed_Call_Zoom_Team : System.Web.UI.Page
{
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    #endregion
    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();

        FYear = Request.QueryString["Fyear"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        lblHead.Text = "Total Missed Call for all Designations for the month of " + strFrmMonth + " " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        strQry = " select Designation_Code,Designation_Short_Name,report_level from mas_sf_designation where division_code='"+div_code+"' " +
                 " and Designation_Active_Flag=0  order by type, Manager_SNo ";

        dsDes = db_ER.Exec_DataSet(strQry);    
        FillReport();
    }
    private void FillReport()
    {
     
        DataTable dtdes = new DataTable();
        dtdes.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtdes.Columns["INX"].AutoIncrementStep = 1;
        dtdes.Columns["INX"].AutoIncrementSeed = 1;
        dtdes.Columns.Add("Desig", typeof(int));

        if (dsDes.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
            {
                dtdes.Rows.Add(null, dsDes.Tables[0].Rows[i]["Designation_Code"].ToString());
            }
        }

      
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Missed_call_team";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth.Trim());
        cmd.Parameters.AddWithValue("@cYr", FYear.Trim());
        cmd.Parameters.AddWithValue("@dtdes", dtdes);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
       
        /*
        result = dsts.Tables[1].AsEnumerable()
       .GroupBy(row => row.Field<string>("VST"))
       .Select(g => g.CopyToDataTable()).ToList();
        */
        dsts.Tables[0].Columns.Remove("sf_type");
        dsts.Tables[0].Columns.Remove("des_color");
        dsts.Tables[0].Columns.Remove("sf_code");
        dsts.Tables[0].Columns.Remove("sf_code1");
        dsts.Tables[0].Columns.Remove("division_code");
        dsts.Tables[0].Columns.Remove("designation_code");
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }



    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
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
            TableCell objtablecell2 = new TableCell();
            #endregion
            //
            #region Merge cells


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Desig", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "DOJ", "#0097AC", true);
           
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp ID", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "List", "#0097AC", true);
            
                  
                    //strQry = " select (stuff((select  '/' + Designation_Short_Name from mas_sf_designation  where division_code='" + div_code + "' " +
                    //          " and Designation_Active_Flag=0 and type=1 " +
                    //          " order by Manager_SNo " +
                    //          " for XML path('')),1,1,'')) as Designation_Short_Name ";

                    //DataSet dsDesss = db_ER.Exec_DataSet(strQry);

                    //AddMergedCells(objgridviewrow, objtablecell, 0, dsDes.Tables[0].Rows[0]["Designation_Short_Name"].ToString(), "#0097AC", true);

                    if (dsDes.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < dsDes.Tables[0].Rows.Count; k++)
                        {
                            AddMergedCells(objgridviewrow, objtablecell, 0, dsDes.Tables[0].Rows[k]["Designation_Short_Name"].ToString(), "#0097AC", true);
                            
                        }

                    }

                    AddMergedCells(objgridviewrow, objtablecell, 0, "Total Missed Calls", "#0097AC", true);
                  

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
          
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
        //objtablecell.Font.Size = 14;
        //objtablecell.Height = 30;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        cell.Attributes.Add("title", "Tooltip text for " + cell.Text);
        //    }
        //}
        int tot = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int approved = 0;

            for (int l = 6, j = 0; l < e.Row.Cells.Count - 1; l++)
            {


                int appr = (e.Row.Cells[l + 1].Text == "") || (e.Row.Cells[l + 1].Text == "&nbsp;") || (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
                approved = approved + appr;
                //    l++;

                j++;
                if ((e.Row.Cells[l].Text == "0") || (e.Row.Cells[l].Text == "-"))
                {
                    e.Row.Cells[l].Text = "";
                }
                e.Row.Cells[l].Attributes.Add("align", "center");
            }
          

            // e.Row.Cells[e.Row.Cells.Count - 2].Text = applied.ToString();
            tot = (Convert.ToInt32((e.Row.Cells[6].Text == "") || (e.Row.Cells[6].Text == "&nbsp;") || (e.Row.Cells[6].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[6].Text)) - (Convert.ToInt32(approved)));
            if (tot != 0)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = tot.ToString();
                e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("align", "center");
                e.Row.Cells[e.Row.Cells.Count - 1].Style.Add("color", "Red");
                e.Row.Cells[e.Row.Cells.Count - 1].Style.Add("font-weight", "Bold");
                //e.Row.Cells[e.Row.Cells.Count - 1].Style.Add("border-color", "black");
               
            }
            else
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "";
            }

           
              

          
            if (dtrowClr.Rows[indx][1].ToString() == "Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black;text-align:right");
                e.Row.Cells[0].Text = "";
                e.Row.Height = 30;
                e.Row.Cells[5].Attributes.Add("align", "center");
            }
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            
        }
    }
}
