
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
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using DBase_EReport;
using System.Data.SqlClient;
using System.ComponentModel;
#endregion

#region MIS_Reports_Visit_Details_Basedonfield_Level1
public partial class MasterFiles_AnalysisReports_rptCallfeedback_Zoom : System.Web.UI.Page
{
    #region variables
    DataSet dsDoctor = null;
    DataSet dsDCR = null;
    DataSet dsmgrsf = new DataSet();

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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();

    int tot_miss = 0;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblCatg = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string sCurrentDate = string.Empty;
    string sType = string.Empty;
    string Months = string.Empty;
    string Year = string.Empty;

    string sf_name = string.Empty;
    //string sf_code = string.Empty;
    int k1 = 1;
    int Tot_Count = 0;

    DB_EReporting db = new DB_EReporting();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
   
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["SfMGR"].ToString();
        FMonth = Request.QueryString["FMnth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sMode = Request.QueryString["mode"].ToString();
        //sType = Request.QueryString["Type"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "ListedDr - Call Feedbackwise for the month of  - " + strFrmMonth + " " + FYear;

        lblFFName.Text = "Field Force Name   : " + sf_name;
        lblHQ1.Text = "HQ : " + sType;
        lblDesignation1.Text = "Desingnation : " + sMode;
        FillReport();
    }
    #endregion
    //


    #region FillReport
    private void FillReport()
    {
       SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        if (sMode == "1")
        {
            sProc_Name = "Callfeedbackwise_Dr_Zoom";
        }
        //else if (sMode == "2")
        //{
        //    sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Mgr_Zoom";
        //}
        //else if (sMode == "3")
        //{
        //    sProc_Name = "DCR_Analysis_temp1_Zoom";
        //}
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", Request.QueryString["SfMGR"].ToString());
        cmd.Parameters.AddWithValue("@Mnth", Convert.ToInt32(FMonth));
        cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(FYear));
        cmd.Parameters.AddWithValue("@Field", "Listed Dr Name");
        cmd.Parameters.AddWithValue("@Order", "ASC");
        DataSet dsts = new DataSet();
        cmd.CommandTimeout = 600;
      
        SqlDataAdapter da = new SqlDataAdapter(cmd);      
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();


        dsts.Tables[0].Columns.Remove("Sf_Code");
        //dsts.Tables[0].Columns.Remove("Status");   
        dsts.Tables[0].Columns.Remove("ListedDrCode");
        dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
        GrdFixationMode.DataSource = dsts;
        GrdFixationMode.DataBind();
    }
    #endregion



    protected void LinkBtn_Click(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMnth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sMode = Request.QueryString["mode"].ToString();

        SqlConnection con = new SqlConnection(strConn);
        DataSet dsts = new DataSet();

        string sProc_Name = "";
        //if ((lblMonthExc.Text) == "Territory")
        {

            if (sMode == "1")
            {
                sProc_Name = "Callfeedbackwise_Dr_Zoom";
            }
            //else if(sMode == "2")
            //{
            //    sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Mgr_Zoom";
            //}
            //  else if(sMode == "3")
            //{
            //    sProc_Name = "DCR_Analysis_temp1_Zoom";
            //}
            SqlCommand cmd = new SqlCommand(sProc_Name, con);

            if (dir1 == SortDirection.Ascending)
            {
                dir1 = SortDirection.Descending;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order", "DSC");
            }
            else
            {
                dir1 = SortDirection.Ascending;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order", "ASC");
            }

            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", Request.QueryString["SfMGR"].ToString());
            cmd.Parameters.AddWithValue("@Mnth", Convert.ToInt32(FMonth));
            cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(FYear));
            if ((lblMonthExc.Text) == "Territory")
            {
                cmd.Parameters.AddWithValue("@Field", "Territory");
            }
            if ((lblMonthExc.Text) == "Listed Dr Name")
            {
                cmd.Parameters.AddWithValue("@Field", "Listed Dr Name");
            }
            if ((lblMonthExc.Text) == "Category")
            {
                cmd.Parameters.AddWithValue("@Field", "Category");
            }
            if ((lblMonthExc.Text) == "Speciality")
            {
                cmd.Parameters.AddWithValue("@Field", "Speciality");
            }
            if ((lblMonthExc.Text) == "Field Force Name with HQ")
            {
                cmd.Parameters.AddWithValue("@Field", "Name with ff");
            }
            if ((lblMonthExc.Text) == "Territory Type")
            {
                cmd.Parameters.AddWithValue("@Field", "Territory Type");
            }

            cmd.CommandTimeout = 600;
          
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();


            if (sMode == "1")
            {
                dsts.Tables[0].Columns.Remove("Sf_Code");
                dsts.Tables[0].Columns.Remove("rx");
                dsts.Tables[0].Columns.Remove("ListedDrCode");
                //dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
            }
        }

        if (dsts.Tables[0].Rows.Count > 0)
        {
            GrdFixationMode.DataSource = dsts;
            GrdFixationMode.DataBind();
            //this.FillReport();
        }
    }



    //
    #region GrdFixationMode_RowCreated
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
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            HyperLink hlink = new HyperLink();
            #endregion
            //
            #region Merge cells
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Field Force Name with HQ", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink, 2, 0, "Listed Dr Name", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink, 2, 0, "Speciality", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink, 2, 0, "Category", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink, 2, 0, "Territory", "#0097AC", true);
            AddMergedCellsLink(objgridviewrow, objtablecell, hlink,2, 0, "Territory Type", "#0097AC", true);
          

            //int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            string cmonth = (Request.QueryString["FMnth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
            SalesForce sf = new SalesForce();
            //for (int i = 0; i <= months; i++)
            //{
            string sTxt = cmonth + "-" + cyear;
            //AddMergedCells(objgridviewrow, objtablecell, 0, 4, sTxt, "#99FF99", true);

            if (sMode == "3" || sMode == "2")
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Visit Count", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, 3, "Visit Date", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "0-10", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "11-20", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "21-31", "#0097AC", false);
            }          
            else if (sMode == "1")
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Status", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Visit Count", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, 1, "Visit Date", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", false);            
            }                     

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            //
            #endregion
            //
        }
    }
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion

    protected void AddMergedCellsLink(GridViewRow objgridviewrow, TableCell objtablecell, HyperLink hlink, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        hlink = new HyperLink();
        objtablecell.Text = celltext;
        hlink.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        hlink.Font.Underline = true;
        hlink.Attributes.Add("style", "cursor:pointer");
        hlink.Attributes.Add("onClick", "callServerButtonEvent('" + celltext + "','" + "" + "')");
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objtablecell.Controls.Add(hlink);
        objgridviewrow.Cells.Add(objtablecell);

    }
    //
    #endregion
    //    

    #region GrdFixationMode_RowDataBound
    protected void GrdFixationMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           

            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //

            if (indx == 0)
            {
                Tot_Count = 0;
            }
            #region Calculations

            sMode = Request.QueryString["mode"].ToString();

            e.Row.Cells[0].Text = (indx + 1).ToString();
            e.Row.Cells[0].Attributes.Add("align", "center");

            if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = "-";
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                //e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            if (sMode == "1")
            {
                string count = string.Empty;
                string[] counts;

                string Rowcount = string.Empty;
                //ArrayList list = new ArrayList();

                int months1 = 2;

                int Uniqueid = 6;
                //for (int n1 = 7; n1 <= e.Row.Cells.Count - 1; n1++)
                {
                    Rowcount = e.Row.Cells[9].Text;
                    e.Row.Cells[9].Attributes.Add("align", "center");
                    e.Row.Cells[8].Attributes.Add("align", "center");
                    e.Row.Cells[7].Attributes.Add("align", "center");
                    if (Rowcount != "" && Rowcount != "-")
                    {
                        count = count + "," + Rowcount;
                    }
                    Uniqueid = Uniqueid + 1;
                }

                counts = count.Split(',');
                int numberOfElements = counts.Count();
                string[] stringArray = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx )
                {
                    Tot_Count = Tot_Count + (numberOfElements - 1);
                }
                //string[] unique = stringArray.Distinct().ToArray();

                //var result = stringArray.Distinct();

              

                //e.Row.Cells[Uniqueid - 1].Text = (numberOfElements - 1).ToString();
                //e.Row.Cells[Uniqueid - 1].Attributes.Add("align", "center");
                //e.Row.Cells[Uniqueid - 1].Attributes.Add("style", "font-weight:bold;");
                int RowSpan = 8;
                //Ass = dsChemists.Tables[0].Rows[aa].ItemArray.GetValue(3).ToString();
                //GridViewRow currRow = GrdFixation.Rows[indx+1];               


                string[] countss;
                int Uniqueid1 = 8;
                string Rowcounts = string.Empty;
                


                //if (e.Row.Cells[1].Text == "zzy") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                //{
                //    int Ass = 0;
                //    e.Row.Cells[0].Visible = false;
                //    e.Row.Cells[1].ColumnSpan = RowSpan;

                //    for (int n = 8, m = 0; n <= e.Row.Cells.Count; n++)
                //    {
                //        string counts1 = string.Empty;
                //        int numberOfElements1 = 0;
                //        if (n <= e.Row.Cells.Count - 1)
                //        {
                //            for (int i = 0; i < GrdFixationMode.Rows.Count - 1; i++)
                //            {
                //                Rowcounts = dtrowClr.Rows[i][n + 2].ToString();
                //                //Rowcounts = e.Row.Cells[n].Text;
                //                if (Rowcounts != "" && Rowcounts != "-" && Rowcounts != "&nbsp;")
                //                {
                //                    counts1 = counts1 + "," + Rowcounts;
                //                }
                //                //Uniqueid1 = Uniqueid1 + 1;
                //            }
                //            countss = counts1.Split(',');
                //            numberOfElements1 = countss.Distinct().Count();
                //            num = num + (numberOfElements1 - 1);
                //        }


                //        if ((months1 + 1) > (Ass))
                //        {
                //            if ((months1 + 1) == (Ass))
                //            {
                //                //e.Row.Cells[n - 6].Text = ("").ToString();
                //            }
                //            else
                //            {
                //                e.Row.Cells[n - 6].Text = (numberOfElements1 - 1).ToString();
                //            }
                //            e.Row.Cells[n - 6].HorizontalAlign = HorizontalAlign.Center;
                //            e.Row.Cells[n - 6].Height = 30;
                //            e.Row.Cells[n - 6].Attributes.Add("style", "font-weight:bold;");
                //            e.Row.Cells[n - 6].Style.Add("font-size", "12pt");
                //            Ass = Ass + 1;
                //            m++;
                //        }
                //        else
                //        {
                //            e.Row.Cells[n - 6].Visible = false;
                //            e.Row.Cells[n - 5].Visible = false;
                //            e.Row.Cells[n - 4].Visible = false;
                //            e.Row.Cells[n - 3].Visible = false;
                //            e.Row.Cells[n - 2].Visible = false;
                //            e.Row.Cells[n - 1].Visible = false;
                //            Ass = Ass + 1;
                //        }
                //    }

                //    e.Row.Cells[1].Text = "Total Days Worked  :  " + num;
                //    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //    e.Row.Cells[1].Height = 30;
                //    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                //    e.Row.Cells[1].Style.Add("font-size", "12pt");
                //    //RowSpan++;
                //}


                if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 1;
                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[2].Text = (Tot_Count).ToString();

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "14px");

                    for (int n = 3, m = 0; n < e.Row.Cells.Count; n++)
                    {
                        e.Row.Cells[n].Visible = false;
                    }
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "14px");
                    //RowSpan++;
                }

            }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------
            else
            {
                string count = string.Empty;
                string[] counts;

                string Rowcount = string.Empty;
                //ArrayList list = new ArrayList();

                int months1 = 2;

                int Uniqueid = 6;
                for (int n1 = 7; n1 <= e.Row.Cells.Count - 1; n1++)
                {
                    Rowcount = e.Row.Cells[n1].Text;
                    e.Row.Cells[n1].Attributes.Add("align", "center");
                    if (Rowcount != "" && Rowcount != "-")
                    {
                        count = count + "," + Rowcount;
                    }
                    Uniqueid = Uniqueid + 1;
                }

                counts = count.Split(',');
                int numberOfElements = counts.Count();
                string[] stringArray = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx && dtrowClr.Rows.Count - 2 != indx)
                {
                    Tot_Count = Tot_Count + (numberOfElements - 1);
                }
                //string[] unique = stringArray.Distinct().ToArray();

                //var result = stringArray.Distinct();

                e.Row.Cells[0].Text = (indx + 1).ToString();

                //e.Row.Cells[Uniqueid - 1].Text = (numberOfElements - 1).ToString();
                //e.Row.Cells[Uniqueid - 1].Attributes.Add("align", "center");
                //e.Row.Cells[Uniqueid - 1].Attributes.Add("style", "font-weight:bold;");
                int RowSpan = 8;
                //Ass = dsChemists.Tables[0].Rows[aa].ItemArray.GetValue(3).ToString();
                //GridViewRow currRow = GrdFixation.Rows[indx+1];               


                string[] countss;
                int Uniqueid1 = 8;
                string Rowcounts = string.Empty;
                int num = 0;


                if (e.Row.Cells[1].Text == "zzy") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].ColumnSpan = RowSpan;

                    for (int n = 8, m = 0; n <= e.Row.Cells.Count; n++)
                    {
                        string counts1 = string.Empty;
                        int numberOfElements1 = 0;
                        if (n <= e.Row.Cells.Count - 1)
                        {
                            for (int i = 0; i < GrdFixationMode.Rows.Count - 1; i++)
                            {
                                Rowcounts = dtrowClr.Rows[i][n + 2].ToString();
                                //Rowcounts = e.Row.Cells[n].Text;
                                if (Rowcounts != "" && Rowcounts != "-" && Rowcounts != "&nbsp;")
                                {
                                    counts1 = counts1 + "," + Rowcounts;
                                }
                                //Uniqueid1 = Uniqueid1 + 1;
                            }
                            countss = counts1.Split(',');
                            numberOfElements1 = countss.Distinct().Count();
                            num = num + (numberOfElements1 - 1);
                        }


                        if ((months1 + 1) > (Ass))
                        {
                            if ((months1 + 1) == (Ass))
                            {
                                //e.Row.Cells[n - 6].Text = ("").ToString();
                            }
                            else
                            {
                                e.Row.Cells[n - 6].Text = (numberOfElements1 - 1).ToString();
                            }
                            e.Row.Cells[n - 6].HorizontalAlign = HorizontalAlign.Center;
                            e.Row.Cells[n - 6].Height = 30;
                            e.Row.Cells[n - 6].Attributes.Add("style", "font-weight:bold;");
                            e.Row.Cells[n - 6].Style.Add("font-size", "14px");
                            Ass = Ass + 1;
                            m++;
                        }
                        else
                        {
                            e.Row.Cells[n - 6].Visible = false;
                            e.Row.Cells[n - 5].Visible = false;
                            e.Row.Cells[n - 4].Visible = false;
                            e.Row.Cells[n - 3].Visible = false;
                            e.Row.Cells[n - 2].Visible = false;
                            e.Row.Cells[n - 1].Visible = false;
                            Ass = Ass + 1;
                        }
                    }

                    e.Row.Cells[1].Text = "Total Days Worked  :  " + num;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "14px");
                    //RowSpan++;
                }


                if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 1;
                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[2].Text = (Tot_Count).ToString();

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "14px");

                    for (int n = 3, m = 0; n < e.Row.Cells.Count; n++)
                    {
                        e.Row.Cells[n].Visible = false;
                    }
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "14px");
                    //RowSpan++;
                }
            }

            #endregion

            //for (int i1 = 7; i1 < e.Row.Cells.Count; i1++)
            //{
            //    if (e.Row.Cells[i1].Text == "0" || e.Row.Cells[i1].Text == "&nbsp;")
            //    {
            //        //e.Row.Cells[i1].Text = "-";
            //        e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
            //    }
            //    e.Row.Cells[i1].Attributes.Add("align", "center");
            //}
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
        }
    }
    #endregion


    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    public SortDirection dir1
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
}
#endregion