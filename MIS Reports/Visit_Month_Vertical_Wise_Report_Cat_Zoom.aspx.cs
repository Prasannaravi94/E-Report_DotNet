
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
public partial class MIS_Reports_Visit_Month_Vertical_Wise_Report_Cat_Zoom : System.Web.UI.Page
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
    int Tot_Count_Vst = 0;
    string Doc_Cat_Code = string.Empty;
    string Vst = string.Empty;
    string Brand_Name = string.Empty;

    DB_EReporting db = new DB_EReporting();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();


    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {


        sf_name = Request.QueryString["sf_name"].ToString();
        if (!Page.IsPostBack)
        {
            FMonth = Request.QueryString["FMnth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            sMode = Request.QueryString["mode"].ToString();
            //sType = Request.QueryString["Type"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            Doc_Cat_Code = "-2";
            if (sMode == "6")
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                div_code = Session["div_code"].ToString();
            }
            else if (sMode == "8")
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                if (sf_code.Contains("MR") || sf_code.Contains("MGR"))
                {
                    sf_code = Request.QueryString["sfcode"].ToString();
                }
                else
                {
                    sf_code = "admin";
                }
                Doc_Cat_Code = Request.QueryString["Doc_Cat_Code"].ToString();
                div_code = Request.QueryString["Division_code"].ToString();
                Brand_Name = Request.QueryString["Brand_Name"].ToString();
                lblBrndName.Text = "      Category Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
                LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + sf_name + "</span>";
            }
            else if (sMode == "9")
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                sf_code = Request.QueryString["sfcode"].ToString();
                if (sf_code.Contains("MR") || sf_code.Contains("MGR"))
                {
                    sf_code = Request.QueryString["sfcode"].ToString();
                }
                else
                {
                    sf_code = "admin";
                }
                Doc_Cat_Code = Request.QueryString["Doc_Special_Code"].ToString();
                div_code = Request.QueryString["Division_code"].ToString();

                Brand_Name = Request.QueryString["Brand_Name"].ToString();
                lblBrndName.Text = "      Speciality Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
                LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + sf_name + "</span>";

            }
            else if (sMode == "10")
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                sf_code = Request.QueryString["sfcode"].ToString();
                if (sf_code.Contains("MR") || sf_code.Contains("MGR"))
                {
                    sf_code = Request.QueryString["sfcode"].ToString();
                }
                else
                {
                    sf_code = "admin";
                }
                Doc_Cat_Code = Request.QueryString["Doc_Cat_Code"].ToString();
                div_code = Request.QueryString["Division_code"].ToString();
                Vst = Request.QueryString["Vst"].ToString();

                Brand_Name = Request.QueryString["Brand_Name"].ToString();
                lblBrndName.Text = "      Category Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
                LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + sf_name + "</span>";
            }
            else if (sMode == "11")
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                sf_code = Request.QueryString["sfcode"].ToString();
                if (sf_code.Contains("MR") || sf_code.Contains("MGR"))
                {
                    sf_code = Request.QueryString["sfcode"].ToString();
                }
                else
                {
                    sf_code = "admin";
                }
                Doc_Cat_Code = Request.QueryString["Doc_Special_Code"].ToString();
                div_code = Request.QueryString["Division_code"].ToString();

                Brand_Name = Request.QueryString["Brand_Name"].ToString();
                lblBrndName.Text = "      Campaign Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
                LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + sf_name + "</span>";

            }
            else if (sMode == "12")
            {
                sf_code = Request.QueryString["sfcode"].ToString();

                if (sf_code.Contains("MR") || sf_code.Contains("MGR"))
                {
                    sf_code = Request.QueryString["sfcode"].ToString();
                }
                else
                {
                    sf_code = "admin";
                }
                //Doc_Cat_Code = Request.QueryString["Doc_Special_Code"].ToString();
                div_code = Request.QueryString["Division_code"].ToString();

                //  Brand_Name = Request.QueryString["Brand_Name"].ToString();
                //   lblBrndName.Text = "      Campaign Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
                LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + sf_name + "</span>";

            }
            else
            {
                sf_code = Request.QueryString["SfMGR"].ToString();
                div_code = Session["div_code"].ToString();
            }


            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            string strToMonth = sf.getMonthName(TMonth);

            if (sMode == "4_Visit" || sMode == "4_Tl_Dr" || sMode == "4_Missed")  //visit_At_a_Glance
            {
                lblHead.Text = "Visit Details At a Glance Of  - " + strFrmMonth + " " + FYear;
            }

            else if (sMode == "5")  // Visit Details (Based on Mode Wise)
            {
                lblHead.Text = "Mode Wise Visit Details Of Category Wise  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "6")  //rptTerrTypeWise_DrVisit
            {
                lblHead.Text = "Territory wise - Listed Doctor Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "7")  //Coverage_New-->FixationWise
            {
                lblHead.Text = "Category Wise Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "8")  //Home page Dashboard Catwise
            {
                lblHead.Text = "Category Wise Missed Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "9")  //Home page Dashboard Specwise
            {
                lblHead.Text = "Speciality Wise Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "10")  //Home page Dashboard Specwise
            {
                lblHead.Text = "Category Wise Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "11")  //Home page Dashboard Specwise
            {
                lblHead.Text = "Campaign Wise Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else if (sMode == "12")  //Home page Dashboard Specwise
            {
                lblHead.Text = "MVD Wise Visit Details of  - " + strFrmMonth + " " + FYear;
            }
            else
            {
                lblHead.Text = "Visit Analysis Of  - " + strFrmMonth + " " + FYear;
            }
            lblFFName.Text = "Field Force Name   : " + sf_name;
            lblHQ1.Text = "HQ : " + sType;
            lblDesignation1.Text = "Desingnation : " + sMode;
            FillReport();
        }
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
            sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Zoom";
        }
        else if (sMode == "2")
        {
            sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Mgr_Zoom";
        }
        else if (sMode == "3")
        {
            sProc_Name = "DCR_Analysis_temp1_Zoom1";   //DCR
        }
        else if (sMode == "4_Visit" || sMode == "9")
        {
            sProc_Name = "Callfeedbackwise_Dr_Zoom_s";  //visit_At_a_Glance
        }
        else if (sMode == "4_Tl_Dr")
        {
            sProc_Name = "Callfeedbackwise_Dr_Zoom_Tl_Missed";  //visit_At_a_Glance
        }
        else if (sMode == "4_Missed" || sMode == "8")
        {
            sProc_Name = "Callfeedbackwise_Dr_Zoom_Tl_Missed";  //visit_At_a_Glance
        }

        else if (sMode == "5")
        {
            sProc_Name = "visit_BasedOn_ModeWise_Cat_Zoom";  //Visit Details (Based on Mode Wise)
        }

        else if (sMode == "6")
        {
            sProc_Name = "getTerr_TypeDrCnt_Zoom";  //rptTerrTypeWise_DrVisit
        }
        else if (sMode == "7")
        {
            sProc_Name = "visit_fixation_Cat_Zoom";  //Coverage_New-->FixationWise
        }
        else if (sMode == "10")
        {
            sProc_Name = "CatWise_Visit_View";  // Dashbrd
        }
        else if (sMode == "11")
        {
            sProc_Name = "Campaign_Dr_Zoom";  // Dashbrd
        }
        else if (sMode == "12")
        {
            sProc_Name = "MVD_Dr_Zoom_s";  // Dashbrd
        }
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Mnth", Convert.ToInt32(FMonth));
        cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(FYear));
        if (sMode == "4_Tl_Dr")
        {
            cmd.Parameters.AddWithValue("@Mode", "1");
            cmd.Parameters.AddWithValue("@Cat_Code", Doc_Cat_Code);
        }
        if (sMode == "4_Missed" || sMode == "8")
        {
            cmd.Parameters.AddWithValue("@Mode", "2");
            cmd.Parameters.AddWithValue("@Cat_Code", Doc_Cat_Code);
        }
        if (sMode == "4_Visit" || sMode == "9" || sMode == "11")
        {
            cmd.Parameters.AddWithValue("@Spec_Code", Doc_Cat_Code);
        }
        if (sMode == "10")
        {
            cmd.Parameters.AddWithValue("@Cat_Code", Doc_Cat_Code);
            cmd.Parameters.AddWithValue("@Vst", Vst);
        }
        if (sMode == "12")
        {
            cmd.Parameters.AddWithValue("@sf", Request.QueryString["sf"].ToString());
        }
        if (sMode != "12")
        {
            if ((sf_code).Substring(0, 2) == "MG")
            {
                if (sMode == "4_Visit" || sMode == "7" || sMode == "5" || sMode == "9" || sMode == "11")
                {
                    cmd.Parameters.AddWithValue("@Field", "Name with ff");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Field", "Listed Dr Name");
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@Field", "Listed Dr Name");
            }

            //.Substring(0, 3) == "MGR"

            cmd.Parameters.AddWithValue("@Order", "ASC");
        }
        if (sMode == "6")
        {
            cmd.Parameters.AddWithValue("@TerrCode", Convert.ToInt32(TYear));
        }
        if (sMode == "7")
        {
            cmd.Parameters.AddWithValue("@Dr_Cat_Code", TMonth);
        }
        DataSet dsts = new DataSet();
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        if (sMode == "6")
        {
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
            dsts.Tables[0].Columns.Remove("Sf_Code");
        }
        else if (sMode == "4_Tl_Dr" || sMode == "4_Missed" || sMode == "8")
        {
            dsts.Tables[0].Columns.Remove("ListedDrCode");
        }


        else
        {
            dsts.Tables[0].Columns.Remove("ListedDrCode");
            dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
        }

        if (dsts.Tables[0].Rows.Count > 0)
        {
            GrdFixationMode.DataSource = dsts;
            GrdFixationMode.DataBind();
        }
        else
        {
            GrdFixationMode.DataSource = dsts;
            GrdFixationMode.DataBind();
        }
    }
    #endregion


    //protected void LinkBtn_Click(object sender, EventArgs e)
    //{
    //    div_code = Session["div_code"].ToString();
    //    sMode = Request.QueryString["mode"].ToString();
    //    if (sMode == "6")
    //    {
    //        sf_code = Request.QueryString["sfcode"].ToString();
    //    }
    //    else
    //    {
    //        sf_code = Request.QueryString["SfMGR"].ToString();
    //    }
    //    FMonth = Request.QueryString["FMnth"].ToString();
    //    FYear = Request.QueryString["FYear"].ToString();
    //    TMonth = Request.QueryString["TMonth"].ToString();
    //    TYear = Request.QueryString["TYear"].ToString();


    //    SqlConnection con = new SqlConnection(strConn);
    //    DataSet dsts = new DataSet();

    //    string sProc_Name = "";
    //    //if ((lblMonthExc.Text) == "Territory")
    //    {

    //        if (sMode == "1")
    //        {
    //            sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Zoom";
    //        }
    //        else if (sMode == "2")
    //        {
    //            sProc_Name = "visit_Month_Vertical_wise_Rpt_Cat_Met_Mgr_Zoom";
    //        }
    //        else if (sMode == "3")
    //        {
    //            sProc_Name = "DCR_Analysis_temp1_Zoom";   //DCR
    //        }
    //        else if (sMode == "4_Visit")
    //        {
    //            sProc_Name = "Callfeedbackwise_Dr_Zoom_s";  //visit_At_a_Glance
    //        }
    //        else if (sMode == "4_Tl_Dr")
    //        {
    //            sProc_Name = "Callfeedbackwise_Dr_Zoom_Tl_Missed";  //visit_At_a_Glance
    //        }
    //        else if (sMode == "4_Missed")
    //        {
    //            sProc_Name = "Callfeedbackwise_Dr_Zoom_Tl_Missed";  //visit_At_a_Glance
    //        }
    //        else if (sMode == "5")
    //        {
    //            sProc_Name = "visit_BasedOn_ModeWise_Cat_Zoom";  //Visit Details (Based on Mode Wise)
    //        }
    //        else if (sMode == "6")
    //        {
    //            sProc_Name = "getTerr_TypeDrCnt_Zoom";  //rptTerrTypeWise_DrVisit
    //        }
    //        else if (sMode == "7")
    //        {
    //            sProc_Name = "visit_fixation_Cat_Zoom";  //Coverage_New-->FixationWise
    //        }
    //        SqlCommand cmd = new SqlCommand(sProc_Name, con);

    //        if (dir1 == SortDirection.Ascending)
    //        {
    //            dir1 = SortDirection.Descending;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Order", "DSC");
    //        }
    //        else
    //        {
    //            dir1 = SortDirection.Ascending;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Order", "ASC");
    //        }

    //        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
    //        cmd.Parameters.AddWithValue("@Mnth", Convert.ToInt32(FMonth));
    //        cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(FYear));
    //        if (sMode == "4_Tl_Dr")
    //        {
    //            cmd.Parameters.AddWithValue("@Mode", "1");
    //        }
    //        if (sMode == "4_Missed")
    //        {
    //            cmd.Parameters.AddWithValue("@Mode", "2");
    //        }
    //        if ((lblMonthExc.Text) == "Territory")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Territory");
    //        }
    //        if ((lblMonthExc.Text) == "Listed Dr Name")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Listed Dr Name");
    //        }
    //        if ((lblMonthExc.Text) == "Category")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Category");
    //        }
    //        if ((lblMonthExc.Text) == "Speciality")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Speciality");
    //        }
    //        if ((lblMonthExc.Text) == "Field Force Name with HQ")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Name with ff");
    //        }
    //        if ((lblMonthExc.Text) == "Territory Type")
    //        {
    //            cmd.Parameters.AddWithValue("@Field", "Territory Type");
    //        }
    //        if (sMode == "6")
    //        {
    //            cmd.Parameters.AddWithValue("@TerrCode", Convert.ToInt32(TYear));
    //        }
    //        if (sMode == "7")
    //        {
    //            cmd.Parameters.AddWithValue("@Dr_Cat_Code", TMonth);
    //        }

    //        cmd.CommandTimeout = 600;

    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dsts);
    //        dtrowClr = dsts.Tables[0].Copy();

    //        if (sMode == "6")
    //        {
    //            dsts.Tables[0].Columns.Remove("ListedDrCode");
    //            dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
    //            dsts.Tables[0].Columns.Remove("Sf_Code");
    //        }
    //        else if (sMode == "4_Tl_Dr" || sMode == "4_Missed")
    //        {
    //            dsts.Tables[0].Columns.Remove("ListedDrCode");
    //        }
    //        else
    //        {
    //            dsts.Tables[0].Columns.Remove("ListedDrCode");
    //            dsts.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
    //        }
    //    }

    //    if (dsts.Tables[0].Rows.Count > 0)
    //    {
    //        GrdFixationMode.DataSource = dsts;
    //        GrdFixationMode.DataBind();
    //        //this.FillReport();
    //    }
    //    else
    //    {
    //        GrdFixationMode.DataSource = dsts;
    //        GrdFixationMode.DataBind();
    //    }
    //}

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
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Listed Dr Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Speciality", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Category", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Territory", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Territory Type", "#0097AC", true);


            //int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            string cmonth = (Request.QueryString["FMnth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
            SalesForce sf = new SalesForce();
            //for (int i = 0; i <= months; i++)
            //{
            string sTxt = cmonth + "-" + cyear;
            //AddMergedCells(objgridviewrow, objtablecell, 0, 4, sTxt, "#99FF99", true);

            if (sMode == "1" || sMode == "2" || sMode == "5")
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Visit Count", "#0097AC", true);

                AddMergedCells(objgridviewrow, objtablecell, 0, 3, "Visit Date", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "0-10", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "11-20", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "21-31", "#0097AC", false);
            }
            else if (sMode == "3" || sMode == "4_Visit" || sMode == "6" || sMode == "9"  || sMode == "12")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 3, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", false);

                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Count", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Once(Date)", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Twice & Above(Date)", "#0097AC", false);


            }
            else if ( sMode == "11")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 6, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", false);

                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Count-(MR)", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Once(Date)-(MR)", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Twice & Above(Date)-(MR)", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Count-(MGR)", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Once(Date)-(MGR)", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Twice & Above(Date)-(MGR)", "#0097AC", false);
            }
            else if (sMode == "10")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 1, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", false);

                //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Count", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Date", "#0097AC", false);
                //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Twice & Above(Date)", "#0097AC", false);
            }
            else if (sMode == "7")
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Visit Count", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, 4, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", false);


                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "I Visit Date", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "II Visit Date", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "III Visit Date", "#0097AC", true);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "IV & Above Visit Date", "#0097AC", true);
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
                Tot_Count_Vst = 0;
            }


            if (dtrowClr.Rows.Count == 1)
            {
                dtrowClr.Rows[0].Delete();

                GrdFixationMode.DataSource = dtrowClr;
                GrdFixationMode.DataBind();
            }

            #region Calculations

            sMode = Request.QueryString["mode"].ToString();

            e.Row.Cells[0].Text = (indx + 1).ToString();
            e.Row.Cells[0].Attributes.Add("align", "center");
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                //e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            if (sMode == "4_Tl_Dr" || sMode == "4_Missed")
            {

            }
            //------------------------------------------------------------------------
            else if (sMode == "4_Visit" || sMode == "9")
            {
                string count = string.Empty;
                string count_Vst = string.Empty;
                string[] counts;
                string[] counts_Vst;

                string Rowcount = string.Empty;
                string Rowcount_vst = string.Empty;
                //ArrayList list = new ArrayList();

                int months1 = 2;

                int Uniqueid = 6;
                int Uniqueid_Vst = 7;

                Rowcount = e.Row.Cells[8].Text;
                Rowcount_vst = e.Row.Cells[9].Text;
                e.Row.Cells[8].Attributes.Add("align", "center");
                e.Row.Cells[7].Attributes.Add("align", "center");
                e.Row.Cells[9].Attributes.Add("align", "center");
                if (Rowcount != "" && Rowcount != "-" && Rowcount != "&nbsp;")  //&nbsp;
                {
                    count = count + "," + Rowcount;
                }
                Uniqueid = Uniqueid + 1;

                counts = count.Split(',');
                int numberOfElements = counts.Count();
                string[] stringArray = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx)
                {
                    Tot_Count = Tot_Count + (numberOfElements - 1);
                }



                if (Rowcount_vst != "" && Rowcount_vst != "-" && Rowcount_vst != "&nbsp;")  //&nbsp;
                {
                    count_Vst = count_Vst + "," + Rowcount_vst;
                }
                Uniqueid_Vst = Uniqueid_Vst + 1;

                counts_Vst = count_Vst.Split(',');
                int numberOfElements_Vst = counts_Vst.Count();
                string[] stringArray_Vst = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx)
                {
                    Tot_Count_Vst = Tot_Count_Vst + (numberOfElements_Vst - 1);
                }





                int RowSpan = 8;
                string[] countss;
                int Uniqueid1 = 8;
                string Rowcounts = string.Empty;


                if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 2;

                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "10pt");

                    e.Row.Cells[2].Text = (Tot_Count).ToString();
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "10pt");

                    e.Row.Cells[3].Text = (Tot_Count_Vst).ToString();
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[3].Height = 30;
                    e.Row.Cells[3].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[3].Style.Add("font-size", "10pt");

                    for (int n = 4, m = 0; n < e.Row.Cells.Count; n++)
                    {
                        e.Row.Cells[n].Visible = false;
                    }
                    //RowSpan++;
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------
            else if (sMode == "3" )
            {

                string count = string.Empty;
                
                int Row1 = 0;
                string Rowcount = string.Empty;
              

                for (int n1 = 7; n1 <= e.Row.Cells.Count - 1; n1++)
                {
                    Rowcount = e.Row.Cells[7].Text;

                    if (Rowcount != "&nbsp;")
                    {
                        Row1 = Convert.ToInt32(Rowcount.ToString());

                    }


                }
                if (Row1 > 0)
                {
                    Tot_Count = Tot_Count + Row1;
                }



                if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 3;
                    // e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 2;
                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[2].Text = (Tot_Count).ToString();

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "10pt");

                    for (int z = 3, m = 0; z < e.Row.Cells.Count; z++)
                    {
                        e.Row.Cells[z].Visible = false;
                    }
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "10pt");
                    //RowSpan++;
                }

            }
           
            //------------------------------------------------------------------------------------------------------------------------------
            else if (sMode == "6")
            {
                string count = string.Empty;
                string[] counts;

                string Rowcount = string.Empty;
                //ArrayList list = new ArrayList();

                int months1 = 2;

                int Uniqueid = 6;
                //for (int n1 = 7; n1 <= e.Row.Cells.Count - 1; n1++)
                {
                    Rowcount = e.Row.Cells[8].Text;
                    e.Row.Cells[8].Attributes.Add("align", "center");
                    e.Row.Cells[7].Attributes.Add("align", "center");
                    if (Rowcount != "" && Rowcount != "-" && Rowcount != "&nbsp;")  //&nbsp;
                    {
                        count = count + "," + Rowcount;
                    }
                    Uniqueid = Uniqueid + 1;
                }

                counts = count.Split(',');
                int numberOfElements = counts.Count();
                string[] stringArray = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx)
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
                    //e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 1;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 2;
                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[2].Text = (Tot_Count).ToString();

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "10pt");

                    for (int n = 3, m = 0; n < e.Row.Cells.Count; n++)
                    {
                        e.Row.Cells[n].Visible = false;
                    }
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "10pt");
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
                for (int n1 = 8; n1 <= e.Row.Cells.Count - 1; n1++)
                {
                    Rowcount = e.Row.Cells[n1].Text;

                    if (e.Row.Cells[n1].Text == "" || e.Row.Cells[n1].Text == "&nbsp;")
                    {
                        e.Row.Cells[n1].Text = "-";
                    }
                    e.Row.Cells[n1].Attributes.Add("align", "center");
                    if (Rowcount != "" && Rowcount != "-" && Rowcount != "&nbsp;")
                    {
                        count = count + "," + Rowcount;
                    }
                    Uniqueid = Uniqueid + 1;
                }

                counts = count.Split(',');
                int numberOfElements = counts.Count();
                string[] stringArray = new string[] { count };
                if (dtrowClr.Rows.Count - 1 != indx)
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
                            for (int i = 0; i < GrdFixationMode.Rows.Count - 2; i++)
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
                            e.Row.Cells[n - 6].Style.Add("font-size", "10pt");
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
                    e.Row.Cells[1].Style.Add("font-size", "10pt");
                    //RowSpan++;
                }


                if (e.Row.Cells[1].Text == "zzz") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    int Ass = 0;
                    e.Row.Cells[0].Visible = false;
                    //e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 1;
                    e.Row.Cells[1].ColumnSpan = e.Row.Cells.Count - 2;
                    e.Row.Cells[1].Text = "Total Visit Count :";
                    e.Row.Cells[2].Text = (Tot_Count).ToString();

                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].Height = 30;
                    e.Row.Cells[2].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[2].Style.Add("font-size", "10pt");

                    for (int n = 3, m = 0; n < e.Row.Cells.Count; n++)
                    {
                        e.Row.Cells[n].Visible = false;
                    }
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].Height = 30;
                    e.Row.Cells[1].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[1].Style.Add("font-size", "10pt");
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
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
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