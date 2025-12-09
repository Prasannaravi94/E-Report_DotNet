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
using System.Text;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rpt_Chemist_Campaign_View_FF : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataTable dtrowdt = new System.Data.DataTable();
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = new DataTable();
    DataTable dtwork = new DataTable();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string speciality = string.Empty;
    DataSet dsDes = null;
    string test = string.Empty;
    string spec_name = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    string strCampaign = string.Empty;
    string strCamp_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        strCampaign = Request.QueryString["campaign"].ToString();
        strCamp_code = Request.QueryString["camp_code"].ToString();

        //  lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Campaign Chemist - View For " + sfname + "  - For The Month Of  : " + strFMonthName + " " + FYear + " To " + strTMonthName + " " + " " + TYear;
        LblForceName.Text = "Campaign : " + strCampaign;

        //  lblIDMonth.Visible = false;
        //lblspec2.Text = spec_name;




        strQry = "select Designation_Code,Designation_Short_Name from mas_sf_designation where division_code='" + divcode + "' and Designation_Active_Flag=0 and type=2 " +
                 " order by Manager_SNo ";
        dsDes = db_ER.Exec_DataSet(strQry);
        //FillSF();      
        FillSample_Prd();



    }



    private void FillSample_Prd()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
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

        DataTable dtSpec = new DataTable();
        dtSpec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpec.Columns["INX"].AutoIncrementSeed = 1;
        dtSpec.Columns["INX"].AutoIncrementStep = 1;
        dtSpec.Columns.Add("SPECCIA", typeof(int));

        if (dsDes.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
            {
                dtSpec.Rows.Add(null, dsDes.Tables[0].Rows[i]["Designation_Code"].ToString());
            }
        }


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Campaign_Chemist_View", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", divcode);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        //  cmd.Parameters.AddWithValue("@speciality", speciality);
        cmd.Parameters.AddWithValue("@dtdes", dtSpec);
        cmd.Parameters.AddWithValue("@camp", strCamp_code.Trim());
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();

        // dtwork = dsts.Tables[2].Copy();
        //dsts.Tables[0].Columns.RemoveAt(5);
        //dsts.Tables[0].Columns.RemoveAt(3);
        //dsts.Tables[0].Columns.RemoveAt(1);
        //  dsts.Tables[0].Columns.Remove("ListedDrCode");
        dsts.Tables[0].Columns.Remove("sf_code");
        dsts.Tables[0].Columns.Remove("chemists_code1");
        //dsts.Tables[0].Columns.RemoveAt(8);
        //dsts.Tables[0].Columns.RemoveAt(2);
        //dsts.Tables[0].Columns.RemoveAt(1);
        // dsts.Tables[0].Columns.Remove("chemist_code");
        dtrowClr = dsts.Tables[0].Copy();

        dtrowdt = dsts.Tables[1].Copy();
        // dsts.Tables[0].Columns.Remove("sub_cat_Name");
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    string attachment = "attachment; filename=DCRView.xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    HtmlForm frm = new HtmlForm();
    //    form1.Parent.Controls.Add(frm);
    //    frm.Attributes["runat"] = "server";
    //    frm.Controls.Add(pnlContents);
    //    frm.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    //}
   
    protected void btnClose_Click(object sender, EventArgs e)
    {

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


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Sf Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, " Chemist Name", "#F1F5F8", true);
          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#F1F5F8", true);
          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Specialty", "#F1F5F8", true);

          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Qualification", "#F1F5F8", true);
         //   AddMergedCells(objgridviewrow, objtablecell, 0, "Class", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Territory", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Mobile No", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Campaign", "#F1F5F8", true);
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            SalesForce sf = new SalesForce();
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {

                    TableCell objtablecell3 = new TableCell();
                    int iColSpan = 0;
                    //iLstColCnt.Add(3);
                    //for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
                    //{
                    //    int iCnt = 0;
                    //    iColSpan = ((dsmgrsf.Tables[0].Rows.Count)) * 2;
                    //    TableCell objtablecell2 = new TableCell();
                    //    AddMergedCells(objgridviewrow2, objtablecell2, 2, dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString(), "#F1F5F8", false);
                    //    //GrdFixation.HeaderRow.Cells[w].ToolTip = dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString();
                    //  //  e.Row.ToolTip = dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString();
                    //        TableCell objtablecell3 = new TableCell();
                    //        AddMergedCells(objgridviewrow3, objtablecell3, 0, "Count", "#F1F5F8", false);
                    //        AddMergedCells(objgridviewrow3, objtablecell3, 0, "Date", "#F1F5F8", false);
                    //        iLstVstmnt.Add(cmonth1);
                    //        iLstVstyr.Add(cyear1);
                    //      //  iColSpan += 2;
                    //}
                    //AddMergedCells(objgridviewrow, objtablecell, iColSpan, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#F1F5F8", true);
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    AddMergedCells(objgridviewrow, objtablecell, (dsDes.Tables[0].Rows.Count + 1) * 2, sTxt, "#F1F5F8", true);
                    strQry = " select (stuff((select  '/' + Designation_Short_Name from mas_sf_designation  where division_code='" + divcode + "' " +
                              " and Designation_Active_Flag=0 and type=1 " +
                              " order by Manager_SNo " +
                              " for XML path('')),1,1,'')) as Designation_Short_Name ";

                    DataSet dsDesss = db_ER.Exec_DataSet(strQry);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Count", "#F1F5F8", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, "Date", "#F1F5F8", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 2, dsDesss.Tables[0].Rows[0]["Designation_Short_Name"].ToString(), "#F1F5F8", true);

                    if (dsDes.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < dsDes.Tables[0].Rows.Count; k++)
                        {
                            AddMergedCells(objgridviewrow2, objtablecell2, 2, dsDes.Tables[0].Rows[k]["Designation_Short_Name"].ToString(), "#F1F5F8", true);

                            AddMergedCells(objgridviewrow3, objtablecell3, 0, "Count", "#F1F5F8", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, "Date", "#F1F5F8", false);
                        }

                    }


                    // AddMergedCells(objgridviewrow, objtablecell, 0, "Work With", "#F1F5F8", true);
                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }
            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
            // objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
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
            objtablecell.RowSpan = 3;
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

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        cell.Attributes.Add("title", "Tooltip text for " + cell.Text);
        //    }
        //}


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            //
            List<int> sDate = new List<int>();
            int iDateList;
            for (int i = 8, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
            for (int s = 0; s < dtrowdt.Rows.Count; s++)
            {
                if (e.Row.Cells[1].Text == dtrowdt.Rows[s][0].ToString())
                {
                    for (int i = 8, m = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Text = dtrowdt.Rows[s][m].ToString().Replace("[", "").Replace("]", "");
                        e.Row.Cells[i].Wrap = false;
                        iDateList = 0;
                        sDate.Clear();
                        string[] strSplit = e.Row.Cells[i].Text.Split(',');
                        foreach (string str in strSplit)
                        {
                            if (str != "")
                            {
                                iDateList = Convert.ToInt32(str);
                                sDate.Add(iDateList);
                            }
                        }
                        string sDateList = "";
                        sDate.Sort();
                        foreach (int item in sDate)
                        {
                            sDateList += item.ToString() + ",";
                        }
                        //e.Row.Cells[i].Attributes.Add("align", "center");
                        //iDateList = Convert.ToInt32(e.Row.Cells[i].Text);
                        //sDate.Add(iDateList);
                        //sDate.Sort();
                        e.Row.Cells[i].Text = sDateList;
                        //e.Row.Cells[i].Attributes.Add("align", "center");
                    //    e.Row.Cells[i].Attributes.Add("style", "color:red;border-color:black;align:center");
                        //  e.Row.Cells[i - 1].Attributes.Add("align", "center");
                        i++;
                        m++;
                    }
                    break;
                }
            }
            #endregion

            #region Workwith
            //

            //for (int s = 0; s < dtwork.Rows.Count; s++)
            //{
            //    if (e.Row.Cells[1].Text == dtwork.Rows[s][0].ToString())
            //    {
            //        for (int i = (11 + (dsDes.Tables[0].Rows.Count)), m = 1; i < e.Row.Cells.Count; i++)
            //        {
            //            e.Row.Cells[i].Text = dtwork.Rows[s][m].ToString().Replace("[", "").Replace("]", "");
            //            e.Row.Cells[i].Wrap = false;

            //            //e.Row.Cells[i].Attributes.Add("align", "center");
            //           // e.Row.Cells[i].Attributes.Add("style", "color:red;border-color:black;align:center");
            //           // e.Row.Cells[i - 1].Attributes.Add("align", "center");
            //            i++;
            //          //  m++;
            //        }
            //        break;
            //    }
            //}
            #endregion
            //
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            e.Row.Cells[8].Wrap = false;
            e.Row.Cells[9].Wrap = false;
        }
    }

}