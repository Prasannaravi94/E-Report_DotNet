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
public partial class MasterFiles_AnalysisReports_Rpt_Rep_Vs_Mgr : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataTable dtrowClr = new DataTable();
    DataTable dtrowMgr = new DataTable();
    DataTable dtsf_code = new DataTable();
    DataTable dtsf_code_Mgr = new DataTable();
    DataSet dsmgrsf = new DataSet();
    DataSet ds = new DataSet();
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
    string sSf_code = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsts = new DataSet();
    DataSet dsFinal = new DataSet();
    List<int> iLstVstday = new List<int>();
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
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Rep Vs Manager - Comparison - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
          
            FillMgr();
        }
    }
    private void FillReport()
    {
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        //
        string mnt = Convert.ToString(months);
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        int j = 0;
        DataTable SfCodes = sf1.getMonth_Day(FMonth);
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("day");
        for (int i = 0; i < SfCodes.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["day"]);
        }
        ds.Tables.Add(dtsf_code);
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Rep_Vs_Manager";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
       
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
     //   dsts.Tables[0].Columns.RemoveAt(1);
       
       
    }

    private void FillMgr()
    {
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        //
        string mnt = Convert.ToString(months);
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        int j = 0;
        DataTable SfCodeNew = sf1.getMonth_Day(FMonth);
        dtsf_code_Mgr.Columns.Add("INX1", typeof(int)).AutoIncrement = true;
        dtsf_code_Mgr.Columns["INX1"].AutoIncrementSeed = 1;
        dtsf_code_Mgr.Columns["INX1"].AutoIncrementStep = 1;
        dtsf_code_Mgr.Columns.Add("days");
        for (int i = 0; i < SfCodeNew.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code_Mgr.Rows.Add(null, SfCodeNew.Rows[i]["day"]);
        }
        dsmgrsf.Tables.Add(dtsf_code_Mgr);
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Rep_Vs_Manager_Only";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code_Mgr);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstsmgr = new DataSet();
        da.Fill(dstsmgr);
        con.Close();
        dtrowMgr = dstsmgr.Tables[0].Copy();
        dstsmgr.Tables[0].Columns.RemoveAt(6);
        dstsmgr.Tables[0].Columns.RemoveAt(5);
   //     dstsmgr.Tables[0].Columns.RemoveAt(1);
        FillReport();
        dstsmgr.Merge(dsts);
        dtrowMgr = dstsmgr.Tables[0].Copy();
        dstsmgr.Tables[0].Columns.RemoveAt(1);
        dstsmgr.Tables[0].Columns["hq"].SetOrdinal(2);
        GrdFixation.DataSource = dstsmgr;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int tmp = 0;
            LinkButton lnk_btn = new LinkButton();
            for (int l = 4, j = 0; l < e.Row.Cells.Count; l+=1)
            {

                System.Drawing.Color clr;
                if (tmp % 2 == 0)
                {
                    clr = System.Drawing.Color.SeaShell;
                    tmp++;
                }
                else
                {
                    clr = System.Drawing.Color.Honeydew;
                    tmp++;
                }
                e.Row.Cells[l].BackColor = clr;
                e.Row.Cells[l + 1].BackColor = clr;

                if (dtrowMgr.Rows[indx][1].ToString().Contains("MR"))
                {
                if (e.Row.Cells[l + 1].Text != "0" && e.Row.Cells[l + 1].Text != "-")
                {

                    string sSf_coderow = dtrowMgr.Rows[indx][1].ToString();
                    string sSfname = dtrowMgr.Rows[indx][2].ToString() + " - " + dtrowMgr.Rows[indx][3].ToString() + " - " + dtrowMgr.Rows[indx][4].ToString();
                    int fday = iLstVstday[j];
                    //int cMnth = iLstVstmnt[j];
                    //int cYr = iLstVstyr[j];

                    //if (cMnth == 12)
                    //{
                    //    sCurrentDate = "01-01-" + (cYr + 1).ToString();
                    //}
                    //else
                    //{
                    //    sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                    //}
                    // hLink.Attributes.Add("href", "javascript:showMissedDR('" + sSf_code + "',  '" + cMnth + "', '" + cYr + "',1,'','" + iDrs_Msd.ToString() + "')");
                    //hLink.ToolTip = "Click here";
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    //  hLink.Style.Add("text-decoration", "none");

                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[l + 1].Text;
                    hLink.Attributes.Add("href", "javascript:showMissedDR('" + sf_code + "', '" + strFieledForceName + "','" + fday + "', '" + FMonth + "', '" + FYear + "','" + sSf_coderow + "','" + sSfname + "')");
                    hLink.Style.Add("color", "Blue");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[l + 1].Controls.Add(hLink);
                  
                }
                
              

                }
                l += 1;
                j++;
            }

                try
                {
                    if (dtrowMgr.Rows[indx][0].ToString() == "0")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";

                        //for (int l = 2, j = 0; l < e.Row.Cells.Count; l++)
                        //{
                        //    e.Row.Cells[l].Text = "";


                        //    l += 1;
                        //}

                    }
                }
                catch
                {
                    e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }

                //e.Row.Cells[1].Wrap = false;
                //e.Row.Cells[2].Wrap = false;
                //e.Row.Cells[3].Wrap = false;
                //e.Row.Cells[4].Wrap = false;
            }

        }
    

  
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
       
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Emp.Code", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);        
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Design Name", "#0097AC", true);
            // AddMergedCells(objgridviewrow, objtablecell, 1, 0, "First Level Manager", "#0097AC", true);
            for (int w = 0; w < ds.Tables[0].Rows.Count; w++)
            {
                int iCnt = 0;
              //  iColSpan = ((dsmgrsf.Tables[0].Rows.Count)) * 2;
             
                TableCell objtablecell2 = new TableCell();
                AddMergedCells(objgridviewrow, objtablecell, 0,2, ds.Tables[0].Rows[w]["day"].ToString(), "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0,0, "Tcs", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0,0, "JW", "#0097AC", false);
                iLstVstday.Add(Convert.ToInt32(ds.Tables[0].Rows[w]["day"].ToString()));
            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);


            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 10;
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

}