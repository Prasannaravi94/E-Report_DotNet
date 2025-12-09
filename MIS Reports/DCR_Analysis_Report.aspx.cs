//
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
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Text;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Coverage_New : System.Web.UI.Page
{
    //
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
    string sUsr_Name = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    List<DataTable> result = new List<System.Data.DataTable>();
    List<DataTable> dlyd = new List<System.Data.DataTable>();
    List<DataTable> dlSampl = new List<System.Data.DataTable>();
    List<DataTable> dlInpt = new List<System.Data.DataTable>();
    int iFtrJnt = 0, iFtrDev = 0, iFtrDrMt = 0, iFtrDrPob = 0,
        iFtrUlMt = 0, iFtrChmMt = 0, iFtrStkMt = 0;
    decimal iFtrChmPob = 0;
    string sfName;
    string sName1 = "", sName2 = "";
    int iDelayedTblCnt = 0;
    int sDeActiveDr = 0;
    int iDevCount = 0;
    string Is_SamInp = string.Empty;

    string ff_code = string.Empty;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sUsr_Name = "";
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        Is_SamInp = Request.QueryString["Is_SamInp"].ToString();
        //strFieledForceName = Request.QueryString["sf_name"].ToString();
        //sDesignation = Request.QueryString["Designation"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = "";//sf.getMonthName(FMonth);
        lblHead.Text = "Tour Plan - Consolidated View for - (" + strFrmMonth + " - " + FYear + ")";
        LblForceName.Text = "Field Force Name : " + strFieledForceName;

        //
        ViewReports();
    }
    #endregion
    //
    #region ViewReports
    //private void ViewReports()
    //{
    //    #region Get Data from Database
    //    //
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("DCR_Analysis_Temp", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", div_code);
    //    cmd.Parameters.AddWithValue("@Msf_code", sf_code);
    //    //cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(FMonth));
    //    //cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(FYear));
    //    cmd.Parameters.AddWithValue("@Self", Convert.ToInt32(Request.QueryString["isSelf"].ToString()));
    //    cmd.CommandTimeout = 500;
    //    //
    //    string sDate = "";
    //    if (FMonth == "12")
    //        sDate = "01-01-" + (Convert.ToInt32(FYear) + 1).ToString();
    //    else
    //        sDate = (Convert.ToInt32(FMonth) + 1).ToString() + "-01-" + FYear;
    //    //
    //    if (sDate.Substring(0, 2).Contains("-"))
    //        sDate = "0" + sDate;
    //    //
    //    cmd.Parameters.AddWithValue("@cDate", sDate);
    //    cmd.CommandTimeout = 500;
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    DataSet ds = new DataSet();
    //    SalesForce sf = new SalesForce();
    //    da.Fill(ds);
    //    con.Close();
    //    //
    //    dt = ds.Tables[0];
    //    dt.Columns.RemoveAt(12);
    //    dt.Columns.RemoveAt(11);
    //    dt.Columns.RemoveAt(7);
    //    dt.Columns.RemoveAt(6);
    //    dt.Columns.RemoveAt(0);

    //    //
    //    #endregion
    //    // Spliting DataTable based on Sf_code
    //    result = dt.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("main_Sf_Code"))
    //        .Select(g => g.CopyToDataTable())
    //        .ToList();
    //    //
    //    dt = ds.Tables[1];
    //    dt.Columns.RemoveAt(0);
    //    //
    //    dlyd = dt.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("main_Sf_Code"))
    //        .Select(g => g.CopyToDataTable())
    //        .ToList();
    //    //
    //    string sCrnt_Sf_Code = null;
    //    //
    //    for (int k = 0; k < result.Count; k++)
    //    {
    //        StringBuilder html = new StringBuilder();
    //        //
    //        #region Titles
    //        //
    //        sCrnt_Sf_Code = result[k].Rows[0][0].ToString();
    //        sfName = result[k].Rows[0][1].ToString();
    //        string sEmp_Id = result[k].Rows[0][21].ToString();
    //        string sTtl_Dr_Mt = result[k].Rows[0][22].ToString();
    //        string sChemist_Mt = result[k].Rows[0][23].ToString();
    //        string[] sSfName = result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart().Split(',');
    //        sName1 = ""; sName2 = "";
    //        if (sSfName.Length > 0)
    //        {
    //            foreach (string sval in sSfName)
    //            {
    //                sval.TrimStart().TrimEnd().Trim().Replace("\t", "");
    //                if (sval != "" && (sName1 == "" || sval == sfName.TrimEnd().TrimStart()))
    //                    sName1 = sval;
    //                else if (sval != "" && sName1 != sval)
    //                    sName2 = sval;
    //            }
    //        }
    //        //
    //        if (sName1 == "")
    //            sName1 = sfName;
    //        if (sName2 == "")
    //            sName2 = sName1;
    //        //
    //        string sSF_Name = "";
    //        if (result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart().Contains(sfName))
    //            sSF_Name = result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart();
    //        else if (result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart() != "")
    //            sSF_Name = sfName + ", <font color='red'>[" + result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart() + "]</font>";
    //        //
    //        if (sSF_Name == "")
    //            sSF_Name = sfName;
    //        //
    //        string sfName_w_Desig = sSF_Name + " - ( " + result[k].Rows[0][2].ToString() + " )";
    //        string sClr = result[k].Rows[0][4].ToString();
    //        //
    //        html.Append("<table align='left' width='99%' cellspacing='0' style='border-collapse: collapse; margin-left:10px;'>");
    //        html.Append("<tr style='height:30px;'><td colspan='8'></td></tr>");
    //        html.Append("<tr><td colspan='8'></td></tr>");
    //        html.Append("<tr><td colspan='8'></td></tr>");
    //        html.Append("<tr><td colspan='2' align='left' style='float:left; margin-left:10px;'><b><h4><font color='green'>" + sUsr_Name + " </font></h4></b></td><td colspan='2'></td><td colspan='4' align='right' style='float:right; margin-right:10px;'><i><h6><b> Date : (" + System.DateTime.Now.ToString() + ")</b></h6></i></td></tr>");
    //        html.Append("<tr><td colspan='8' align='center'><u><b><h3> DCR Analysis For the Month of :  " + sf.getMonthName(FMonth).ToString() + " - " + FYear + " </h3></b></u></td></tr>");
    //        html.Append("<tr><td colspan='2' bgcolor='#" + sClr + "' style='float:left; margin-left:15px;' align='left'> FieldForce Name : <b>" + sfName_w_Desig + "</b></td><td colspan='2' bgcolor='#" + sClr + "' align='center'><b> Emp Code :  " + sEmp_Id + " </b></td><td colspan='5' bgcolor='#" + sClr + "' style='float:right; margin-right:20px;' align='right'> HQ Name : <b>" + result[k].Rows[0][3].ToString() + "</b></td></tr></table>");
    //        //
    //        pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //        html = new StringBuilder();
    //        html.Append("<table align='left' width='99%' cellspacing='0' border='1' style='border-collapse: collapse; margin-left:10px;'>");
    //        //
    //        #endregion
    //        //
    //        string sTtl_Drs = "-";
    //        for (int i = 0; i < 3; i++)
    //        {
    //            html.Append("<tr>");
    //            if (i == 0)
    //            {
    //                iDevCount = 0;
    //                html.Append("<td align='center' colspan=8 valign='top'>");
    //                pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                html = new StringBuilder();
    //                //
    //                #region Gridview Main
    //                GridView gv = new GridView();
    //                gv.Attributes.Add("width", "99.8%");
    //                gv.HeaderStyle.BackColor = System.Drawing.Color.SkyBlue;
    //                gv.EmptyDataText = "*** No Data Found ***";
    //                gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.MidnightBlue;
    //                gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
    //                gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.White;
    //                gv.EmptyDataRowStyle.Font.Bold = true;
    //                gv.EmptyDataRowStyle.Font.Size = 12;
    //                gv.ShowHeader = false;
    //                gv.ShowFooter = true;
    //                //
    //                iFtrJnt = 0; iFtrDev = 0; iFtrDrMt = 0; iFtrDrPob = 0; iFtrUlMt = 0; iFtrChmMt = 0; iFtrChmPob = 0; iFtrStkMt = 0;
    //                //
    //                result[k].Columns.RemoveAt(24);
    //                result[k].Columns.RemoveAt(23);
    //                result[k].Columns.RemoveAt(22);
    //                result[k].Columns.RemoveAt(21);
    //                result[k].Columns.RemoveAt(4);
    //                result[k].Columns.RemoveAt(3);
    //                result[k].Columns.RemoveAt(2);
    //                result[k].Columns.RemoveAt(1);
    //                //result[k].Columns.RemoveAt(0);

    //                sTtl_Drs = result[k].Rows[0][11].ToString();
    //                //
    //                if (result[k].Rows[0][0].ToString() == "")
    //                    result[k].Rows.RemoveAt(0);
    //                gv.DataSource = result[k];
    //                //
    //                gv.RowCreated += new GridViewRowEventHandler(this.grdMain_RowCreated);
    //                gv.RowDataBound += new GridViewRowEventHandler(this.grdMain_RowDataBound);
    //                gv.DataBind();

    //                pnlTbl.Controls.Add(gv);
    //                #endregion
    //                //
    //                html.Append("</td>");
    //                html.Append("</tr>");
    //                pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                html = new StringBuilder();
    //            }
    //            else if (i == 1 && dlyd.Count >= k)
    //            {
    //                html.Append("<td colspan=8 align='center' bgcolor='#B0C4DE'>DCR Delayed Status</td><tr><td align='center' colspan=8 valign='top' nowrap>");
    //                pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                html = new StringBuilder();
    //                //
    //                #region Gridview DelayedDate
    //                GridView gv = new GridView();
    //                gv.Attributes.Add("width", "99.8%");
    //                gv.ShowHeader = false;
    //                if (dlyd[k].Rows[0][1].ToString() == null || dlyd[k].Rows[0][1].ToString() == "&nbsp;" || dlyd[k].Rows[0][1].ToString() == "")
    //                {
    //                    DataTable dts = new DataTable();
    //                    dts.Columns.Add("Name");
    //                    dts.Columns.Add("Sample");
    //                    dts.Rows.Add("Locked Date", "Nil");
    //                    dts.Rows.Add("Released Date", "Nil");
    //                    gv.ShowHeader = false;
    //                    gv.DataSource = dts;
    //                }
    //                else
    //                {
    //                    var delayed = from a in dlyd[k].AsEnumerable()
    //                                  select new
    //                                  {
    //                                      subject = a.Field<string>("subject"),
    //                                      dates = a.Field<string>("dates").Replace("[", "").Replace("]", "")
    //                                  };
    //                    DataTable dttbldlyd = new System.Data.DataTable();
    //                    dttbldlyd.Columns.Add("dlyed");
    //                    dttbldlyd.Columns.Add("dt");

    //                    foreach (var item in delayed)
    //                    {
    //                        List<int> iDt = new List<int>();
    //                        string sdt = "";
    //                        string[] dtt = item.dates.ToString().Split(',');
    //                        foreach (string sdts in dtt)
    //                        {
    //                            if (sdts.TrimStart() != "")
    //                                iDt.Add(Convert.ToInt32(sdts.TrimStart()));
    //                        }
    //                        iDt.Sort();
    //                        foreach (var newval in iDt)
    //                        {
    //                            sdt += newval + ", ";
    //                        }
    //                        dttbldlyd.Rows.Add(item.subject, sdt.Remove(sdt.LastIndexOf(",")));
    //                    }
    //                    gv.DataSource = dttbldlyd;
    //                    gv.RowDataBound += new GridViewRowEventHandler(this.grdDlyd_RowDataBound);
    //                }
    //                gv.DataBind();
    //                pnlTbl.Controls.Add(gv);
    //                #endregion
    //                //
    //                html.Append("</td></tr>");
    //            }
    //            else
    //            {
    //                int sTtl_Drs_Sn = 0, iTtl_Fld_Wrk_Dys = 0;
    //                for (int j = 0; j < 3; j++)
    //                {
    //                    //
    //                    #region Gridview WorkType
    //                    GridView gv = new GridView();
    //                    gv.Attributes.Add("width", "99.8%");
    //                    DataTable dts = new DataTable();
    //                    if (j == 0)
    //                    {
    //                        html.Append("<td align='center' valign='top' nowrap>");
    //                        pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                        html = new StringBuilder();

    //                        var groupedData = from b in result[k].AsEnumerable()
    //                                          where b.Field<string>("WorkType") != null
    //                                          group b by b.Field<string>("WorkType") into g
    //                                          select new
    //                                          {
    //                                              WorkType = g.Key,
    //                                              Count = g.Count()
    //                                          };
    //                        //
    //                        DataTable dtAttendance = new System.Data.DataTable();
    //                        dtAttendance.Columns.Add("Attendance Details");
    //                        dtAttendance.Columns.Add("Total");
    //                        int iTtl = 0;
    //                        foreach (var item in groupedData)
    //                        {
    //                            dtAttendance.Rows.Add(item.WorkType, item.Count);
    //                            iTtl += Convert.ToInt32(item.Count);
    //                        }
    //                        //
    //                        if (iTtl != 0)
    //                            dtAttendance.Rows.Add("Total No of Days", iTtl);
    //                        gv.HeaderStyle.BackColor = System.Drawing.Color.Thistle;
    //                        gv.EmptyDataText = "*** No Data ***";
    //                        gv.EmptyDataRowStyle.Font.Bold = true;
    //                        gv.EmptyDataRowStyle.Font.Size = 12;
    //                        gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
    //                        gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
    //                        gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.Thistle;
    //                        gv.DataSource = dtAttendance;
    //                    }
    //                    //
    //                    #endregion
    //                    //
    //                    #region GridView Consolidate View
    //                    //
    //                    else if (j == 1)
    //                    {
    //                        html.Append("<td align='center' valign='top' nowrap>");
    //                        pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                        html = new StringBuilder();
    //                        gv.EmptyDataText = "*** No Data ***";
    //                        gv.EmptyDataRowStyle.Font.Bold = true;
    //                        gv.EmptyDataRowStyle.Font.Size = 12;
    //                        gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
    //                        gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
    //                        gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGreen;
    //                        if (sTtl_Drs == "")
    //                            sTtl_Drs = "0";
    //                        if (sTtl_Dr_Mt == "")
    //                            sTtl_Dr_Mt = "0";
    //                        gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGreen;
    //                        var ttlConsolidateView = from p in result[k].AsEnumerable()
    //                                                 group p by "Total" into g
    //                                                 select new
    //                                                 {
    //                                                     ConsolidateView = g.Key,
    //                                                     Ttl_Dr = sTtl_Drs,
    //                                                     Dr_Mt = sTtl_Dr_Mt,
    //                                                     Dr_Sn = g.Sum(x => x.IsNull("dr Sn") ? 0 : x.Field<Int32>("dr Sn")),
    //                                                     NL_Mt = g.Sum(x => x.IsNull("Ul Mt") ? 0 : x.Field<Int32>("Ul Mt")),
    //                                                     Tp_Dev = iDevCount,
    //                                                     No_Joint_Wrk = g.Count(x => x.Field<Int32?>("wrkd with cnt") != 0 && x.Field<Int32?>("wrkd with cnt") != null),
    //                                                     Joint_Wrk_Ttl = g.Sum(x => x.IsNull("wrkd with cnt") ? 0 : x.Field<Int32>("wrkd with cnt")),
    //                                                     No_Of_FldWrk_Dys = g.Count(x => x.Field<string>("WorkType") == "Field Work") == 0 ? 0 : g.Count(x => x.Field<string>("WorkType") == "Field Work"),
    //                                                     Chemist_Met = sChemist_Mt,
    //                                                     Chemist_Sn = g.Sum(x => x.IsNull("Chm_Mt") ? 0 : x.Field<Int32>("Chm_Mt")),
    //                                                     Chemist_POB_Value = g.Sum(x => x.IsNull("Chm POB") ? 0 : x.Field<Double>("Chm POB"))
    //                                                 };
    //                        //
    //                        DataTable dtconsl = new System.Data.DataTable();
    //                        dtconsl.Columns.Add("Calls Details");
    //                        dtconsl.Columns.Add("Total");
    //                        foreach (var item in ttlConsolidateView)
    //                        {
    //                            if (sCrnt_Sf_Code.Contains("MR"))
    //                                dtconsl.Rows.Add("Total No Of Doctors", item.Ttl_Dr);
    //                            dtconsl.Rows.Add("Total No Of Doctors Met", item.Dr_Mt);
    //                            dtconsl.Rows.Add("Total Calls Seen", item.Dr_Sn);
    //                            sTtl_Drs_Sn = item.Dr_Sn; // for Next Col
    //                            dtconsl.Rows.Add("No of N.L Drs Met", item.NL_Mt);
    //                            string val = "-";
    //                            if (item.Ttl_Dr != "0")
    //                                if (Convert.ToInt32(item.Dr_Mt) != 0)
    //                                    val = (Decimal.Divide(Convert.ToInt32(item.Dr_Mt), Convert.ToInt32(item.Ttl_Dr)) * 100).ToString("#.##");
    //                                else
    //                                    val = "0";
    //                            if (sCrnt_Sf_Code.Contains("MR"))
    //                                dtconsl.Rows.Add("Coverage", val);

    //                            if (item.No_Of_FldWrk_Dys != 0)
    //                                if (item.Dr_Sn != 0)
    //                                    val = (Decimal.Divide(item.Dr_Sn, item.No_Of_FldWrk_Dys)).ToString("#.##");
    //                                else
    //                                    val = "0";
    //                            dtconsl.Rows.Add("Call Average", val);
    //                            dtconsl.Rows.Add("No of TP Deviation", item.Tp_Dev);
    //                            dtconsl.Rows.Add("No of Joint Work Days", item.No_Joint_Wrk);
    //                            val = "-";
    //                            if (item.No_Joint_Wrk != 0)
    //                                if (item.Joint_Wrk_Ttl != 0)
    //                                    val = (Decimal.Divide(item.Joint_Wrk_Ttl, item.No_Joint_Wrk)).ToString("#.##");
    //                                else
    //                                    val = "0";
    //                            dtconsl.Rows.Add("Joint Work Call Avg", val);
    //                            dtconsl.Rows.Add("Chemist POB Value", item.Chemist_POB_Value);
    //                            val = "0";
    //                            if (item.Chemist_Met.ToString() != "" && item.Chemist_Met.ToString() != null && item.Chemist_Met.ToString() != "&nbsp;")
    //                                val = item.Chemist_Met;
    //                            dtconsl.Rows.Add("Chemist Met", val);
    //                            dtconsl.Rows.Add("Chemist Seen", item.Chemist_Sn);
    //                            val = "-";
    //                            if (item.No_Of_FldWrk_Dys != 0)
    //                                if (item.Chemist_Sn != 0)
    //                                    val = (Decimal.Divide(item.Chemist_Sn, item.No_Of_FldWrk_Dys)).ToString("#.##");
    //                                else
    //                                    val = "0";
    //                            dtconsl.Rows.Add("Chemist Call Avg", val);
    //                            iTtl_Fld_Wrk_Dys = item.No_Joint_Wrk;
    //                        }
    //                        bool bBreak = false;
    //                        foreach (DataRow row in ds.Tables[2].Rows)
    //                        {
    //                            if (row[0].ToString() == sCrnt_Sf_Code)
    //                            {
    //                                decimal dLst = 0, dMt = 0;
    //                                List<string> lstCol = new List<string>();
    //                                foreach (DataColumn col in ds.Tables[2].Columns)
    //                                {
    //                                    lstCol.Add(col.ColumnName.Substring(0, 1));
    //                                }
    //                                for (int inx = 2; inx < ds.Tables[2].Columns.Count; inx++)
    //                                {
    //                                    if (inx % 2 == 0)
    //                                    {
    //                                        if (sCrnt_Sf_Code.Contains("MR"))
    //                                        {
    //                                            try
    //                                            {
    //                                                dLst = Convert.ToDecimal(row[inx].ToString());
    //                                            }
    //                                            catch
    //                                            {
    //                                                dLst = 0;
    //                                            }
    //                                            dtconsl.Rows.Add("Total No of " + lstCol[inx].ToString() + "V Drs", dLst.ToString());
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        try
    //                                        {
    //                                            dMt = Convert.ToDecimal(row[inx].ToString());
    //                                        }
    //                                        catch
    //                                        {
    //                                            dMt = 0;
    //                                        }
    //                                        dtconsl.Rows.Add("Total No of " + lstCol[inx].ToString() + "V Drs Met", dMt.ToString());
    //                                        if (sCrnt_Sf_Code.Contains("MR"))
    //                                        {
    //                                            string sCovg = "-";
    //                                            if (dLst != 0)
    //                                            {
    //                                                if (dMt == 0)
    //                                                    sCovg = "0";
    //                                                else
    //                                                    sCovg = (Decimal.Divide(dMt, dLst) * 100).ToString("#.##");
    //                                            }
    //                                            dtconsl.Rows.Add(lstCol[inx].ToString() + "V Coverage", sCovg);
    //                                        }
    //                                        dLst = 0; dMt = 0;
    //                                    }
    //                                }
    //                                bBreak = true;
    //                                break;
    //                            }
    //                            if (bBreak)
    //                                break;
    //                        }
    //                        gv.DataSource = dtconsl;
    //                    }
    //                    //
    //                    #endregion
    //                    //
    //                    #region GridView Worked With
    //                    //
    //                    else
    //                    {
    //                        html.Append("<td align='center' valign='top'>");
    //                        pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //                        html = new StringBuilder();
    //                        //Regex regex = new Regex("[0-9\\()-]");
    //                        Regex regex = new Regex("]");

    //                        var workedWithData = from b in result[k].AsEnumerable()
    //                                             group b by new
    //                                             {
    //                                                 ID = b.Field<string>("wrkd with name"),
    //                                                 Day = b.Field<Int32?>("DAY"),
    //                                             } into g

    //                                             select new
    //                                             {
    //                                                 WorkedWithName = g.Key.ID,
    //                                                 DaysWorked = g.Key.Day,                                                     
    //                                                 CallsSeen = g.Sum(x => x.Field<Int32?>("wrkd with cnt"))
    //                                             };
    //                        // Remove Additional Symbols in Worked With Name
    //                        var wrkw = (from a in workedWithData
    //                                    where a.WorkedWithName != null
    //                                        && a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim() != sName1
    //                                        && a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim() != sName2

    //                                    select new
    //                                    {
    //                                        Joint_Work_Details = a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim().Replace(", ]", "").Trim().Replace(",", "")
    //                                        .Replace("SELF,", ",").Replace("SELF ,", ",").Replace(",SELF", ",").Replace(", SELF", ",").Replace(",,", ",").Replace(", ,", ",").TrimStart().TrimEnd().Trim()
    //                                        .Replace("SELF,", ",").Replace("SELF ,", ",").Replace(",SELF", ",").Replace(", SELF", ",").Replace(",,", ",").Replace(", ,", ",").TrimStart().TrimEnd().Trim(),
    //                                        a.DaysWorked,
    //                                        a.CallsSeen
    //                                    }).OrderBy(x => x.Joint_Work_Details);
    //                        DataTable dtJointWork = new System.Data.DataTable();
    //                        dtJointWork.Columns.Add("Joint Work Details");
    //                        dtJointWork.Columns.Add("No of Days");
    //                        dtJointWork.Columns.Add("No of Calls");
    //                        //
    //                        int sCls = 0, sRowVal = 0, iTtl_Slf = 0, iTtl_Sn = 0;
    //                        string sDys = "";
    //                        string sNames = "";
    //                        //
    //                        List<string> Name = new List<string>();
    //                        List<string> Name1 = new List<string>();

    //                        foreach (var item in wrkw)
    //                        {
    //                            var num = "";
    //                            var jName = item.Joint_Work_Details;
    //                            jName = regex.Replace(jName.Replace("]", ""), "").Replace("SELF", "").ToString().Replace("(", "").Replace(")", "");
    //                            string str = jName;
    //                            var array = Regex.Matches(jName, @"\D+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
    //                            jName = jName.Replace("-", "");
    //                            var numAlpha = new Regex("(?<Alpha>[a-zA-Z.\\s]*)(?<Numeric>[0-9]*)");
    //                            var match = numAlpha.Match(jName);

    //                            if (array.Length > 3)
    //                            {
    //                                for (int l = 2; l < array.Length; ++l)
    //                                {
    //                                    jName = array[l] + array[l + 1];
    //                                    //match = numAlpha.Match(jName);
    //                                    l = l + 1;
    //                                    //if (Name1.Contains(jName))
    //                                    //{
    //                                    Name1.Add(jName + "," + item.DaysWorked + ",");


    //                                }

    //                            }
    //                            //else
    //                            //{

    //                            var alpha = match.Groups["Alpha"].Value;
    //                            num = match.Groups["Numeric"].Value;
    //                            jName = alpha;


    //                            if (jName != "")
    //                                if (jName.Substring(0, 1) == ",")
    //                                    jName = jName.Remove(0, 1);
    //                            if (jName.Length > 0 && jName != "" && jName != null)
    //                            {
    //                                if (jName.Substring(jName.Length - 1, 1) == ",")
    //                                    jName = jName.Remove(jName.Length - 1, 1);

    //                                //
    //                                //string[] sJName = jName.Trim().TrimStart().TrimEnd().Split(',');
    //                                //if (sJName.Length > 1)
    //                                //{
    //                                //    jName = "";
    //                                //    List<string> sNew_Sort_Name = new List<string>();
    //                                //    foreach (string sName in sJName)
    //                                //    {
    //                                //        if (jName.Contains(sName.TrimStart().TrimEnd().Trim()) || sName == "")
    //                                //        { }
    //                                //        else
    //                                //        {
    //                                //            jName += sName + ", ";
    //                                //        }
    //                                //    }
    //                                //    jName = regex.Replace(jName.Remove(jName.LastIndexOf(",")).Replace("]", ""), "").Replace("SELF", "");
    //                                //}
    //                                //


    //                                if (Name.Contains(jName.TrimStart().TrimEnd().Trim()) && num != "")
    //                                {
    //                                    sNames = jName.TrimStart().TrimEnd();
    //                                    Name.Add(sNames);
    //                                    sDys += item.DaysWorked + ",";
    //                                    sCls += Convert.ToInt16(num);
    //                                    dtJointWork.Rows.RemoveAt(sRowVal - 1);
    //                                    sRowVal--;
    //                                }
    //                                else if (num != "")
    //                                {
    //                                    sNames = jName.TrimStart().TrimEnd();
    //                                    Name.Add(sNames);
    //                                    sDys = "";
    //                                    sCls = 0;
    //                                    sDys = item.DaysWorked.ToString() + ",";
    //                                    sCls = Convert.ToInt16(num);
    //                                }
    //                                //iTtl_Slf += item.DaysWorked;
    //                                iTtl_Sn += Convert.ToInt32(item.CallsSeen);
    //                                dtJointWork.Rows.Add(jName, sDys, sCls);
    //                                sRowVal++;
    //                            }
    //                        }

    //                        Name1.Sort();
    //                        sDys = "";
    //                        sCls = 0;
    //                        string str1 = "";
    //                        foreach (var item in Name1)
    //                        {
    //                            sDys = "";
    //                            sCls = 0;
    //                            var jName = item;
    //                            string num = "";
    //                            jName = jName.Replace("-", "");
    //                            var numAlpha = new Regex("(?<Alpha>[a-zA-Z.\\s]*)(?<Numeric>[0-9]*)");
    //                            var match = numAlpha.Match(jName);
    //                            string[] arr = jName.Split(',');
    //                            var alpha = match.Groups["Alpha"].Value;
    //                            num = match.Groups["Numeric"].Value;
    //                            jName = alpha;

    //                            if (Name.Contains(jName.TrimStart().TrimEnd().Trim()))
    //                            {

    //                                sNames = jName.TrimStart().TrimEnd();
    //                                Name.Add(sNames);
    //                                sDys += arr[1] + ",";
    //                                sCls += Convert.ToInt16(num);
    //                                //dtJointWork.Rows.RemoveAt(sRowVal - 1);
    //                                //sRowVal--;
    //                            }
    //                            else if (num != "")
    //                            {
    //                                sNames = jName.TrimStart().TrimEnd();
    //                                Name.Add(sNames);
    //                                sDys = arr[1] + ",";
    //                                sCls = Convert.ToInt16(num);
    //                            }
    //                            //iTtl_Slf += item.DaysWorked;
    //                            //iTtl_Sn += Convert.ToInt32(sCls);
    //                            dtJointWork.Rows.Add(sNames, sDys, sCls);
    //                            sRowVal++;
    //                        }

    //                        if (iTtl_Sn != 0)
    //                        {
    //                            iTtl_Sn = sTtl_Drs_Sn - iTtl_Sn;
    //                            //iTtl_Slf = iTtl_Fld_Wrk_Dys - iTtl_Slf;
    //                            dtJointWork.Rows.Add("SELF ", iTtl_Slf, iTtl_Sn);
    //                            dtJointWork.Rows.Add("ZTotal ", iTtl_Fld_Wrk_Dys, sTtl_Drs_Sn);
    //                        }
    //                        if (dtJointWork.Rows.Count > 0)
    //                        {
    //                            DataRow[] dataRows = dtJointWork.Select().OrderBy(u => u["Joint Work Details"]).ToArray();
    //                            dtJointWork = dataRows.CopyToDataTable();
    //                        }
    //                        DataTable dtCopy = new DataTable();
    //                        dtCopy = dtJointWork.Copy();
    //                        dtJointWork.Rows.Clear();
    //                        //
    //                        sRowVal = 0;
    //                        Name.Clear();
    //                        string sExstName = ""; int iDys = 0, iTtl = 0; DataRow dr = null;
    //                        for (int iRws = 0; iRws < dtCopy.Rows.Count; iRws++)
    //                        {
    //                            //if (dtCopy.Rows[iRws][0].ToString() == sExstName && sExstName != "")
    //                            //{
    //                            //{
    //                            //    iDys = Convert.ToInt32(dtCopy.Rows[iRws - 1][1].ToString()) + Convert.ToInt32(dtCopy.Rows[iRws][1].ToString());
    //                            //    iTtl = Convert.ToInt32(dtCopy.Rows[iRws - 1][2].ToString()) + Convert.ToInt32(dtCopy.Rows[iRws][2].ToString());
    //                            //    dr[0] = dtCopy.Rows[iRws][0].ToString();
    //                            //    dr[1] = iDys.ToString();
    //                            //    dr[2] = iTtl.ToString();
    //                            //}
    //                            //else
    //                            //{

    //                            if (Name.Contains(dtCopy.Rows[iRws][0].ToString()))
    //                            {
    //                                dr = dtJointWork.NewRow();
    //                                dr[0] = dtCopy.Rows[iRws][0].ToString();
    //                                dr[1] = dtCopy.Rows[iRws][1].ToString();
    //                                dr[2] = dtCopy.Rows[iRws][2].ToString();
    //                                sNames = dtCopy.Rows[iRws][0].ToString();
    //                                Name.Add(sNames);
    //                                sDys += dr[1];
    //                                sCls += Convert.ToInt16(dr[2]);
    //                                dtJointWork.Rows.RemoveAt(sRowVal - 1);
    //                                sRowVal--;
    //                            }
    //                            else
    //                            {
    //                                dr = dtJointWork.NewRow();
    //                                dr[0] = dtCopy.Rows[iRws][0].ToString();
    //                                dr[1] = dtCopy.Rows[iRws][1].ToString();
    //                                dr[2] = dtCopy.Rows[iRws][2].ToString();
    //                                Session["Val"] = dr[0];
    //                                sNames = Session["Val"].ToString();
    //                                Name.Add(sNames);
    //                                sDys = dr[1].ToString();
    //                                sCls = Convert.ToInt16(dr[2]);
    //                            }

    //                            dtJointWork.Rows.Add(sNames, sDys, sCls);
    //                            sExstName = dtCopy.Rows[iRws][0].ToString();
    //                            sRowVal++;
    //                            //}
    //                            // }
    //                        }
    //                        //Empty Text
    //                        gv.EmptyDataText = "*** No Joint Work ***";
    //                        gv.EmptyDataRowStyle.Font.Bold = true;
    //                        gv.EmptyDataRowStyle.Font.Size = 12;
    //                        gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
    //                        gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
    //                        gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
    //                        gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
    //                        //
    //                        gv.DataSource = dtJointWork;
    //                    }
    //                    //
    //                    #endregion
    //                    //
    //                    gv.DataBind();
    //                    pnlTbl.Controls.Add(gv);

    //                    html.Append("</td>");
    //                }
    //            }
    //            html.Append("</tr>");
    //        }
    //        html.Append("</table><br>");
    //        pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
    //        iDelayedTblCnt++;
    //    }
    //}
    #endregion
    //
    #region ViewReports
    private void ViewReports()
    {
        #region Get Data from Database
        //
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("DCR_Analysis_temp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        //cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(FMonth));
        //cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(FYear));
        cmd.Parameters.AddWithValue("@Self", Convert.ToInt32(Request.QueryString["isSelf"].ToString()));
        cmd.Parameters.AddWithValue("@Is_SamInp", Convert.ToInt32(Is_SamInp));

        cmd.CommandTimeout = 1000;
        //
        string sDate = "";
        if (FMonth == "12")
            sDate = "01-01-" + (Convert.ToInt32(FYear) + 1).ToString();
        else
            sDate = (Convert.ToInt32(FMonth) + 1).ToString() + "-01-" + FYear;
        //
        if (sDate.Substring(0, 2).Contains("-"))
            sDate = "0" + sDate;
        //
        cmd.Parameters.AddWithValue("@cDate", sDate);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        con.Close();
        //
        dt = ds.Tables[0];
        dt.Columns.RemoveAt(12);
        dt.Columns.RemoveAt(11);
        dt.Columns.RemoveAt(7);
        dt.Columns.RemoveAt(6);
        dt.Columns.RemoveAt(0);

        //
        #endregion
        // Spliting DataTable based on Sf_code
        result = dt.AsEnumerable()
            .GroupBy(row => row.Field<string>("main_Sf_Code"))
            .Select(g => g.CopyToDataTable())
            .ToList();
        //
        dt = ds.Tables[1];
        dt.Columns.RemoveAt(0);
        //
        dlyd = dt.AsEnumerable()
            .GroupBy(row => row.Field<string>("main_Sf_Code"))
            .Select(g => g.CopyToDataTable())
            .ToList();
        //

        if (Is_SamInp == "1")
        {
            dt = ds.Tables[3];
            //dt.Columns.RemoveAt(0);
            //
            dlSampl = dt.AsEnumerable()
              .GroupBy(row => row.Field<string>("main_Sf_Code"))
                  // .GroupBy(row => row.Field<string>("Sf_Code"))
                .Select(g => g.CopyToDataTable())
                .ToList();

            dt = ds.Tables[4];
            //dt.Columns.RemoveAt(0);
            //
            dlInpt = dt.AsEnumerable()
                .GroupBy(row => row.Field<string>("main_Sf_Code"))
                .Select(g => g.CopyToDataTable())
                .ToList();
        }
        string sCrnt_Sf_Code = null;
        //
        string str2 = null;
        for (int k = 0; k < result.Count; k++)
        {
            StringBuilder html = new StringBuilder();
            //
            #region Titles
            //
            sCrnt_Sf_Code = result[k].Rows[0][0].ToString();
            sfName = result[k].Rows[0][1].ToString();
            string sEmp_Id = result[k].Rows[0][21].ToString();
            string sTtl_Dr_Mt = result[k].Rows[0][22].ToString();
            string sChemist_Mt = result[k].Rows[0][23].ToString();
            string[] sSfName = result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart().Split(',');
            sName1 = ""; sName2 = "";
            if (sSfName.Length > 0)
            {
                foreach (string sval in sSfName)
                {
                    sval.TrimStart().TrimEnd().Trim().Replace("\t", "");
                    if (sval != "" && (sName1 == "" || sval == sfName.TrimEnd().TrimStart()))
                        sName1 = sval;
                    else if (sval != "" && sName1 != sval)
                        sName2 = sval;
                }
            }
            //
            if (sName1 == "")
                sName1 = sfName;
            if (sName2 == "")
                sName2 = sName1;
            //
            string sSF_Name = "";
            if (result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart().Contains(sfName))
                sSF_Name = result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart();
            else if (result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart() != "")
                sSF_Name = sfName + ", <font color='red'>[" + result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart() + "]</font>";
            //
            if (sSF_Name == "")
                sSF_Name = sfName;
            //


            if (result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart() != "")
            {
                str2 = result[k].Rows[0][24].ToString().Trim().TrimEnd().TrimStart();
            }

            string[] strSplSfname = sfName.Split(',');


            string sfName_w_Desig = sSF_Name + " - ( " + result[k].Rows[0][2].ToString() + " )";
            string sClr = result[k].Rows[0][4].ToString();
            lblHeadDCS.Text = "DCR Analysis For the Month of :" + sf.getMonthName(FMonth).ToString() + " - " + FYear;
            //
            html.Append("<table align='left' width='99%' cellspacing='0' style='border-collapse: collapse; margin-left:10px;background-color:white'>");
            html.Append("<tr style='height:30px;'><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='2' align='left'  style='float:left; '><b><h4><font color='#0077FF'>" + sUsr_Name + " </font></h4></b></td><td colspan='2'></td><td colspan='4' align='right' style='float:right; margin-right:10px;'><i><h6><b> Date : (" + System.DateTime.Now.ToString() + ")</b></h6></i></td></tr>");
            //html.Append("<tr><td colspan='8' align='center' class='reportheader' style='padding-bottom:30px'> DCR Analysis For the Month of :  " + sf.getMonthName(FMonth).ToString() + " - " + FYear + " </td></tr>");
            html.Append("<tr style='font-size:14px'><td colspan='2'   style='float:left; padding-bottom:15px;' align='left'> FieldForce Name : <b>" + sfName_w_Desig + "</b></td><td colspan='2' style='padding-bottom:15px'  align='center'><b> Emp Code :  " + sEmp_Id + " </b></td><td colspan='5'  style='float:right;padding-bottom:15px;' align='right'> HQ Name : <b>" + result[k].Rows[0][3].ToString() + "</b></td></tr></table>");
            //
            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
            html = new StringBuilder();
            html.Append("<table align='left' width='99%' cellspacing='0'  border='0' style='border-collapse: collapse; margin-left:10px;background-color:white'>");
            //
            #endregion
            //
            string sTtl_Drs = "-";
            for (int i = 0; i < 3; i++)
            {
                html.Append("<tr>");
                if (i == 0)
                {
                    iDevCount = 0;
                    html.Append("<td align='center' colspan=8 valign='top'>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                    //
                    #region Gridview Main
                    GridView gv = new GridView();
                    gv.Attributes.Add("width", "99.8%");
                    //gv.GridLines = GridLines.None;
                    gv.GridLines = GridLines.Both;
                    gv.BorderColor = System.Drawing.Color.WhiteSmoke;
                    gv.Attributes.Add("class", "table");
                    gv.HeaderStyle.BackColor = System.Drawing.Color.SkyBlue;
                    gv.EmptyDataText = "*** No Data Found ***";
                    gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.MidnightBlue;
                    gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                    gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.White;
                    gv.EmptyDataRowStyle.Font.Bold = true;
                    gv.EmptyDataRowStyle.Font.Size = 12;
                    gv.ShowHeader = false;
                    gv.ShowFooter = true;
                    //
                    iFtrJnt = 0; iFtrDev = 0; iFtrDrMt = 0; iFtrDrPob = 0; iFtrUlMt = 0; iFtrChmMt = 0; iFtrChmPob = 0; iFtrStkMt = 0;
                    //
                    result[k].Columns.RemoveAt(24);
                    result[k].Columns.RemoveAt(23);
                    result[k].Columns.RemoveAt(22);
                    result[k].Columns.RemoveAt(21);
                    result[k].Columns.RemoveAt(4);
                    result[k].Columns.RemoveAt(3);
                    result[k].Columns.RemoveAt(2);
                    result[k].Columns.RemoveAt(1);
                    //result[k].Columns.RemoveAt(0);

                    sTtl_Drs = result[k].Rows[0][11].ToString();
                    //
                    if (result[k].Rows[0][0].ToString() == "")
                        result[k].Rows.RemoveAt(0);
                    gv.DataSource = result[k];
                    //
                    gv.RowCreated += new GridViewRowEventHandler(this.grdMain_RowCreated);
                    gv.RowDataBound += new GridViewRowEventHandler(this.grdMain_RowDataBound);
                    gv.DataBind();

                    pnlTbl.Controls.Add(gv);
                    #endregion
                    //
                    html.Append("</td>");
                    html.Append("</tr>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                }
                else if (i == 1 && dlyd.Count >= k)
                {
                    html.Append("<td colspan=8 align='center' bgcolor='#B0C4DE' style='font-size:14px'>DCR Delayed Status</td><tr><td align='center' colspan=8 valign='top' nowrap>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                    //
                    #region Gridview DelayedDate
                    GridView gv = new GridView();
                    gv.Attributes.Add("width", "99.8%");

                    gv.ShowHeader = false;
                    if (dlyd[k].Rows[0][1].ToString() == null || dlyd[k].Rows[0][1].ToString() == "&nbsp;" || dlyd[k].Rows[0][1].ToString() == "")
                    {
                        DataTable dts = new DataTable();
                        dts.Columns.Add("Name");
                        dts.Columns.Add("Sample");
                        dts.Rows.Add("Locked Date", "Nil");
                        dts.Rows.Add("Released Date", "Nil");
                        gv.ShowHeader = false;
                        gv.DataSource = dts;
                    }
                    else
                    {
                        var delayed = from a in dlyd[k].AsEnumerable()
                                      select new
                                      {
                                          subject = a.Field<string>("subject"),
                                          dates = a.Field<string>("dates").Replace("[", "").Replace("]", "")
                                      };
                        DataTable dttbldlyd = new System.Data.DataTable();
                        dttbldlyd.Columns.Add("dlyed");
                        dttbldlyd.Columns.Add("dt");

                        foreach (var item in delayed)
                        {
                            List<int> iDt = new List<int>();
                            string sdt = "";
                            string[] dtt = item.dates.ToString().Split(',');
                            foreach (string sdts in dtt)
                            {
                                if (sdts.TrimStart() != "")
                                    iDt.Add(Convert.ToInt32(sdts.TrimStart()));
                            }
                            iDt.Sort();
                            foreach (var newval in iDt)
                            {
                                sdt += newval + ", ";
                            }
                            dttbldlyd.Rows.Add(item.subject, sdt.Remove(sdt.LastIndexOf(",")));
                        }
                        gv.DataSource = dttbldlyd;
                        gv.RowDataBound += new GridViewRowEventHandler(this.grdDlyd_RowDataBound);
                    }
                    gv.DataBind();
                    pnlTbl.Controls.Add(gv);
                    #endregion
                    //
                    html.Append("</td></tr>");
                }
                else
                {
                    int sTtl_Drs_Sn = 0, iTtl_Fld_Wrk_Dys = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        //
                        #region Gridview WorkType
                        GridView gv = new GridView();
                        gv.Attributes.Add("width", "99.8%");
                        gv.Style.Add("margin-top", "10px");
                        DataTable dts = new DataTable();
                        if (j == 0)
                        {
                            html.Append("<td align='center' valign='top' nowrap>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();

                            var groupedData = from b in result[k].AsEnumerable()
                                              where b.Field<string>("WorkType") != null
                                              group b by b.Field<string>("WorkType") into g
                                              select new
                                              {
                                                  WorkType = g.Key,
                                                  Count = g.Count()
                                              };
                            //
                            DataTable dtAttendance = new System.Data.DataTable();
                            dtAttendance.Columns.Add("Attendance Details");
                            dtAttendance.Columns.Add("Total");


                            dtAttendance.Columns.Add(" ");

                            int iTtl = 0;
                            foreach (var item in groupedData)
                            {
                                if (item.WorkType.Contains("Field Work"))
                                {
                                    // dtAttendance.Rows.Add("<span style='font-weight: bold;'>" + item.WorkType + "</span>", "<span style='font-weight: bold;'>" + item.Count + "</span>");
                                    //datagridview1.Columns[0].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                                    //string str = "<span style='font-weight: bold;'>" + item.WorkType + "</span>";

                                    dtAttendance.Rows.Add(item.WorkType, item.Count, "");

                                }
                                else
                                {
                                    dtAttendance.Rows.Add(item.WorkType, item.Count, "");
                                }
                                iTtl += Convert.ToInt32(item.Count);
                            }
                            //
                            if (iTtl != 0)
                                dtAttendance.Rows.Add("Total No of Dates", iTtl, "");

                            if (Is_SamInp == "1")
                            {

                                dtAttendance.Rows.Add("", "", "");
                                dtAttendance.Rows.Add("", "Sample", "Input");
                                
                                    var Sample = from a in dlSampl[k].AsEnumerable()
                                             select new
                                             {
                                                 main_Sf_Code = a.Field<string>("main_Sf_Code"),
                                                 opening = a.Field<double>("opening"),
                                                 Despatch_Qty = a.Field<double>("Despatch_Qty"),
                                                 issedd2 = a.Field<double>("issedd2"),
                                                 issed_Chem = a.Field<double>("issed_Chem"),
                                                 closing = a.Field<double>("closing")
                                             };

                                double opening_Sample = 0;
                                double Despatch_Qty_Sample = 0;
                                double issedd2_Sample = 0;
                                double issed_Chem_Sample = 0;
                                double closing_Sample = 0;


                                foreach (var item in Sample)
                                {
                                    opening_Sample = item.opening;

                                    Despatch_Qty_Sample = item.Despatch_Qty;
                                    issedd2_Sample = item.issedd2;
                                    issed_Chem_Sample = item.issed_Chem;
                                    closing_Sample = item.closing;
                                }


                                var Input = from a in dlInpt[k].AsEnumerable()
                                            select new
                                            {
                                                opening = a.Field<double>("opening"),
                                                Despatch_Qty = a.Field<double>("Despatch_Qty"),
                                                issedd2 = a.Field<double>("issedd2"),
                                                issed_Chem = a.Field<double>("issed_Chem"),
                                                closing = a.Field<double>("closing")
                                            };

                                double opening_Input = 0;
                                double Despatch_Qty_Input = 0;
                                double issedd2_Input = 0;
                                double issed_Chem_Input = 0;
                                double closing_Input = 0;


                                foreach (var item in Input)
                                {
                                    opening_Input = item.opening;
                                    Despatch_Qty_Input = item.Despatch_Qty;
                                    issedd2_Input = item.issedd2;
                                    issed_Chem_Input = item.issed_Chem;
                                    closing_Input = item.closing;
                                }


                                dtAttendance.Rows.Add("OB", opening_Sample, opening_Input);
                                dtAttendance.Rows.Add("Despatch Qty", Despatch_Qty_Sample, Despatch_Qty_Input);
                                dtAttendance.Rows.Add("Dr Issued Qty", issedd2_Sample, issedd2_Input);
                                dtAttendance.Rows.Add("Chem Issued Qty", issed_Chem_Sample, issed_Chem_Input);
                                dtAttendance.Rows.Add("CB", closing_Sample, closing_Input);


                            }
                            gv.HeaderStyle.BackColor = System.Drawing.Color.Thistle;
                            gv.EmptyDataText = "*** No Data ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.Thistle;
                            gv.RowDataBound += new GridViewRowEventHandler(this.grdAttend_RowDataBound);
                            gv.DataSource = dtAttendance;
                        
                        }
                        //
                        #endregion
                        //
                        #region GridView Consolidate View
                        //
                        else if (j == 1)
                        {
                            html.Append("<td align='center' valign='top' nowrap>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();
                            gv.EmptyDataText = "*** No Data ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGreen;
                            if (sTtl_Drs == "")
                                sTtl_Drs = "0";
                            if (sTtl_Dr_Mt == "")
                                sTtl_Dr_Mt = "0";
                            gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGreen;
                            var ttlConsolidateView = from p in result[k].AsEnumerable()
                                                     group p by "Total" into g
                                                     select new
                                                     {
                                                         ConsolidateView = g.Key,

                                                         Ttl_Dr = sTtl_Drs,
                                                         Dr_Mt = sTtl_Dr_Mt,

                                                         Dr_Sn = g.Sum(x => x.IsNull("dr Sn") ? 0 : x.Field<Int32>("dr Sn")),
                                                         //  NL_Mt = g.Sum(x => x.IsNull("Ul Mt") ? 0 : x.Field<Int32>("Ul Mt")),
                                                         Tp_Dev = iDevCount,
                                                         No_Joint_Wrk = g.Count(x => x.Field<Int32?>("wrkd with cnt") != 0 && x.Field<Int32?>("wrkd with cnt") != null),
                                                         Joint_Wrk_Ttl = g.Sum(x => x.IsNull("wrkd with cnt") ? 0 : x.Field<Int32>("wrkd with cnt")),
                                                         No_Of_FldWrk_Dys = g.Count(x => x.Field<string>("WorkType") == "Field Work") == 0 ? 0 : g.Count(x => x.Field<string>("WorkType") == "Field Work"),
                                                         Chemist_Met = sChemist_Mt,
                                                         Chemist_Sn = g.Sum(x => x.IsNull("Chm_Mt") ? 0 : x.Field<Int32>("Chm_Mt")),
                                                         Chemist_POB_Value = g.Sum(x => x.IsNull("Chm POB") ? 0 : x.Field<Double>("Chm POB"))

                                                     };
                            //
                            DataTable dtconsl = new System.Data.DataTable();
                            dtconsl.Columns.Add("Calls Details");
                            dtconsl.Columns.Add("Total");

                            dtconsl.Rows.Add("FFcode", sCrnt_Sf_Code);  //Add by Preethi 

                            foreach (var item in ttlConsolidateView)
                            {
                                if (sCrnt_Sf_Code.Contains("MR"))


                                    dtconsl.Rows.Add("Total No Of Doctors", item.Ttl_Dr);
                                dtconsl.Rows.Add("Total No Of Doctors Met", item.Dr_Mt);
                                string val = "-";
                                if (item.Ttl_Dr != "0")

                                    val = (Convert.ToInt32(item.Ttl_Dr) - Convert.ToInt32(item.Dr_Mt)).ToString();
                                else
                                    val = "0";
                                if (sCrnt_Sf_Code.Contains("MR"))
                                    dtconsl.Rows.Add("Total Missed", val);

                                dtconsl.Rows.Add("Total Calls Seen", item.Dr_Sn);
                                sTtl_Drs_Sn = item.Dr_Sn; // for Next Col
                                                          //dtconsl.Rows.Add("No of N.L Drs Met", item.NL_Mt);

                                if (item.Ttl_Dr != "0")
                                    if (Convert.ToInt32(item.Dr_Mt) != 0)
                                        val = (Decimal.Divide(Convert.ToInt32(item.Dr_Mt), Convert.ToInt32(item.Ttl_Dr)) * 100).ToString("#.##");
                                    else
                                        val = "0";
                                if (sCrnt_Sf_Code.Contains("MR"))
                                    dtconsl.Rows.Add("Coverage", val);

                                if (item.No_Of_FldWrk_Dys != 0)
                                    if (Convert.ToInt32(item.Dr_Mt) != 0)
                                        val = (Decimal.Divide(Convert.ToInt32(item.Dr_Mt), item.No_Of_FldWrk_Dys)).ToString("#.##");
                                    else
                                        val = "0";
                                dtconsl.Rows.Add("Met Call Average", val);
                                if (item.No_Of_FldWrk_Dys != 0)
                                    if (item.Dr_Sn != 0)
                                        val = (Decimal.Divide(item.Dr_Sn, item.No_Of_FldWrk_Dys)).ToString("#.##");
                                    else
                                        val = "0";
                                dtconsl.Rows.Add("Visit Call Average", val);



                                dtconsl.Rows.Add("No of TP Deviation", item.Tp_Dev);
                                dtconsl.Rows.Add("No of Joint Work Days", item.No_Joint_Wrk);
                                val = "-";
                                if (item.No_Joint_Wrk != 0)
                                    if (item.Joint_Wrk_Ttl != 0)
                                        val = (Decimal.Divide(item.Joint_Wrk_Ttl, item.No_Of_FldWrk_Dys)).ToString("#.##");
                                    else
                                        val = "0";
                                dtconsl.Rows.Add("Joint Work Call Avg", val);
                                dtconsl.Rows.Add("Chemist POB Value", item.Chemist_POB_Value);
                                val = "0";
                                if (item.Chemist_Met.ToString() != "" && item.Chemist_Met.ToString() != null && item.Chemist_Met.ToString() != "&nbsp;")
                                    val = item.Chemist_Met;
                                dtconsl.Rows.Add("Chemist Met", val);
                                dtconsl.Rows.Add("Chemist Seen", item.Chemist_Sn);
                                val = "-";
                                if (item.No_Of_FldWrk_Dys != 0)
                                    if (item.Chemist_Sn != 0)
                                        val = (Decimal.Divide(item.Chemist_Sn, item.No_Of_FldWrk_Dys)).ToString("#.##");
                                    else
                                        val = "0";
                                dtconsl.Rows.Add("Chemist Call Avg", val);
                                iTtl_Fld_Wrk_Dys = item.No_Joint_Wrk;
                            }




                            bool bBreak = false;
                            foreach (DataRow row in ds.Tables[2].Rows)
                            {
                                if (row[0].ToString() == sCrnt_Sf_Code)
                                {
                                    decimal dLst = 0, dMt = 0;
                                    List<string> lstCol = new List<string>();
                                    foreach (DataColumn col in ds.Tables[2].Columns)
                                    {
                                        lstCol.Add(col.ColumnName.Substring(0, 1));
                                    }
                                    for (int inx = 2; inx < ds.Tables[2].Columns.Count; inx++)
                                    {
                                        if (inx % 2 == 0)
                                        {
                                            if (sCrnt_Sf_Code.Contains("MR"))
                                            {
                                                try
                                                {
                                                    dLst = Convert.ToDecimal(row[inx].ToString());
                                                }
                                                catch
                                                {
                                                    dLst = 0;
                                                }
                                                dtconsl.Rows.Add("Total No of " + lstCol[inx].ToString() + "V Drs", dLst.ToString());
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                dMt = Convert.ToDecimal(row[inx].ToString());
                                            }
                                            catch
                                            {
                                                dMt = 0;
                                            }
                                            dtconsl.Rows.Add("Total No of " + lstCol[inx].ToString() + "V Drs Met", dMt.ToString());
                                            if (sCrnt_Sf_Code.Contains("MR"))
                                            {
                                                string sCovg = "-";
                                                if (dLst != 0)
                                                {
                                                    if (dMt == 0)
                                                        sCovg = "0";
                                                    else
                                                        sCovg = (Decimal.Divide(dMt, dLst) * 100).ToString("#.##");
                                                }
                                                dtconsl.Rows.Add(lstCol[inx].ToString() + "V Coverage", sCovg);
                                            }
                                            dLst = 0; dMt = 0;
                                        }
                                    }
                                    bBreak = true;
                                    break;
                                }
                                if (bBreak)
                                    break;
                            }
                            gv.RowDataBound += new GridViewRowEventHandler(this.grdCallDetaol_RowDataBound);
                            gv.DataSource = dtconsl;
                        }
                        //
                        #endregion
                        //
                        #region GridView Worked With
                        //
                        else
                        {
                            html.Append("<td align='center' valign='top'>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();
                            //Regex regex = new Regex("[0-9\\()-]");
                            Regex regex = new Regex("]");
                            //calculation based on Worked With Name
                            //var workedWithData = from b in result[k].AsEnumerable() 
                            //                     group b by b.Field<string>("wrkd with name") into g                                                
                            //                     select new 
                            //                     {
                            //                         WorkedWithName = g.Key,
                            //                         DaysWorked = g.Select(m => m.Field<Int32?>),
                            //                         //DaysWorked = g.Sum(x => x.Field<Int32?>("DAY")),
                            //                         //DaysWorked = Convert.ToInt16(result[k].Rows[0]["DAY"]),
                            //                         CallsSeen = g.Sum(x => x.Field<Int32?>("wrkd with cnt"))
                            //                     };

                            var workedWithData = from b in result[k].AsEnumerable()
                                                 group b by new
                                                 {
                                                     ID = b.Field<string>("wrkd with name"),
                                                     Day = b.Field<Int32?>("DAY"),
                                                 } into g

                                                 select new
                                                 {
                                                     WorkedWithName = g.Key.ID,
                                                     DaysWorked = g.Key.Day,
                                                     //DaysWorked = g.Sum(x => x.Field<Int32?>("DAY")),
                                                     //DaysWorked = Convert.ToInt16(result[k].Rows[0]["DAY"]),
                                                     CallsSeen = g.Sum(x => x.Field<Int32?>("wrkd with cnt"))
                                                 };
                            // Remove Additional Symbols in Worked With Name
                            var wrkw = (from a in workedWithData
                                        where a.WorkedWithName != null
                                            && a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim() != sName1
                                            && a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim() != sName2

                                        select new
                                        {
                                            Joint_Work_Details = a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim().Replace(", ]", "").Trim().Replace(",", "")
                                            .Replace("SELF,", ",").Replace("SELF ,", ",").Replace(",SELF", ",").Replace(", SELF", ",").Replace(",,", ",").Replace(", ,", ",").TrimStart().TrimEnd().Trim()
                                            .Replace("SELF,", ",").Replace("SELF ,", ",").Replace(",SELF", ",").Replace(", SELF", ",").Replace(",,", ",").Replace(", ,", ",").TrimStart().TrimEnd().Trim(),
                                            a.DaysWorked,
                                            a.CallsSeen
                                        }).OrderBy(x => x.Joint_Work_Details);
                            DataTable dtJointWork = new System.Data.DataTable();
                            dtJointWork.Columns.Add("Joint Work Details");
                            dtJointWork.Columns.Add("No of Dates");
                            dtJointWork.Columns.Add("No of Calls");
                            //
                            int sCls = 0, sRowVal = 0, iTtl_Slf = 0, iTtl_Sn = 0;
                            string sDys = "";
                            string sNames = "";
                            //
                            List<string> Name = new List<string>();
                            List<string> Name1 = new List<string>();

                            foreach (var item in wrkw)
                            {
                                var num = "";
                                var jName = item.Joint_Work_Details;
                                jName = regex.Replace(jName.Replace("]", ""), "").Replace("SELF", "").ToString().Replace("(", "").Replace(")", "");
                                string str = jName;
                                var array = Regex.Matches(jName, @"\D+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
                                jName = jName.Replace("-", "");
                                var numAlpha = new Regex("(?<Alpha>[a-zA-Z.\\s]*)(?<Numeric>[0-9]*)");
                                var match = numAlpha.Match(jName);

                                if (array.Length > 3)
                                {
                                    for (int l = 2; l < array.Length; ++l)
                                    {
                                        if ((l + 1) < array.Length)
                                        {
                                            jName = array[l] + array[l + 1];
                                            //match = numAlpha.Match(jName);
                                            l = l + 1;
                                            //if (Name1.Contains(jName))
                                            //{
                                            Name1.Add(jName + "," + item.DaysWorked + ",");
                                        }


                                    }

                                }
                                //else
                                //{

                                var alpha = match.Groups["Alpha"].Value;
                                num = match.Groups["Numeric"].Value;
                                jName = alpha;


                                if (jName != "")
                                    if (jName.Substring(0, 1) == ",")
                                        jName = jName.Remove(0, 1);
                                if (jName.Length > 0 && jName != "" && jName != null)
                                {
                                    if (jName.Substring(jName.Length - 1, 1) == ",")
                                        jName = jName.Remove(jName.Length - 1, 1);

                                    //
                                    //string[] sJName = jName.Trim().TrimStart().TrimEnd().Split(',');
                                    //if (sJName.Length > 1)
                                    //{
                                    //    jName = "";
                                    //    List<string> sNew_Sort_Name = new List<string>();
                                    //    foreach (string sName in sJName)
                                    //    {
                                    //        if (jName.Contains(sName.TrimStart().TrimEnd().Trim()) || sName == "")
                                    //        { }
                                    //        else
                                    //        {
                                    //            jName += sName + ", ";
                                    //        }
                                    //    }
                                    //    jName = regex.Replace(jName.Remove(jName.LastIndexOf(",")).Replace("]", ""), "").Replace("SELF", "");
                                    //}
                                    //


                                    if (Name.Contains(jName.TrimStart().TrimEnd().Trim()) && num != "")
                                    {
                                        sNames = jName.TrimStart().TrimEnd();
                                        Name.Add(sNames);
                                        sDys += item.DaysWorked + ",";
                                        sCls += Convert.ToInt32(num);
                                        dtJointWork.Rows.RemoveAt(sRowVal - 1);
                                        sRowVal--;
                                    }
                                    else if (num != "")
                                    {
                                        sNames = jName.TrimStart().TrimEnd();
                                        Name.Add(sNames);
                                        sDys = "";
                                        sCls = 0;
                                        sDys = item.DaysWorked.ToString() + ",";
                                        sCls = Convert.ToInt32(num);
                                    }
                                    //iTtl_Slf += item.DaysWorked;
                                    iTtl_Sn += Convert.ToInt32(item.CallsSeen);
                                    dtJointWork.Rows.Add(jName, sDys, sCls);
                                    sRowVal++;
                                }
                            }

                            Name1.Sort();
                            sDys = "";
                            sCls = 0;
                            string str1 = "";
                            foreach (var item in Name1)
                            {
                                sDys = "";
                                sCls = 0;
                                var jName = item;
                                string num = "";
                                jName = jName.Replace("-", "");
                                var numAlpha = new Regex("(?<Alpha>[a-zA-Z.\\s]*)(?<Numeric>[0-9]*)");
                                var match = numAlpha.Match(jName);
                                string[] arr = jName.Split(',');
                                var alpha = match.Groups["Alpha"].Value;
                                num = match.Groups["Numeric"].Value;
                                jName = alpha;

                                if (Name.Contains(jName.TrimStart().TrimEnd().Trim()))
                                {

                                    sNames = jName.TrimStart().TrimEnd();
                                    Name.Add(sNames);
                                    sDys += arr[1] + ",";
                                    sCls += num == "" ? 0 : Convert.ToInt32(num);
                                    //dtJointWork.Rows.RemoveAt(sRowVal - 1);
                                    //sRowVal--;
                                }
                                else if (num != "")
                                {
                                    sNames = jName.TrimStart().TrimEnd();
                                    Name.Add(sNames);
                                    sDys = arr[1] + ",";
                                    sCls = Convert.ToInt32(num);
                                }
                                //iTtl_Slf += item.DaysWorked;
                                //iTtl_Sn += Convert.ToInt32(sCls);
                                dtJointWork.Rows.Add(sNames, sDys, sCls);
                                sRowVal++;
                            }

                            if (iTtl_Sn != 0 || iTtl_Sn == 0)
                            {
                                iTtl_Sn = sTtl_Drs_Sn - iTtl_Sn;
                                //iTtl_Slf = iTtl_Fld_Wrk_Dys - iTtl_Slf;
                                //dtJointWork.Rows.Add("SELF ", iTtl_Slf, iTtl_Sn);
                                dtJointWork.Rows.Add("ZTotal ", iTtl_Fld_Wrk_Dys, sTtl_Drs_Sn);
                            }
                            if (dtJointWork.Rows.Count > 0)
                            {
                                DataRow[] dataRows = dtJointWork.Select().OrderBy(u => u["Joint Work Details"]).ToArray();
                                dtJointWork = dataRows.CopyToDataTable();
                            }
                            DataTable dtCopy = new DataTable();
                            dtCopy = dtJointWork.Copy();
                            dtJointWork.Rows.Clear();
                            //
                            sRowVal = 0;
                            Name.Clear();
                            string sExstName = ""; int iDys = 0, iTtl = 0; DataRow dr = null;
                            for (int iRws = 0; iRws < dtCopy.Rows.Count; iRws++)
                            {

                                if (Name.Contains(dtCopy.Rows[iRws][0].ToString().Trim()))
                                {
                                    dr = dtJointWork.NewRow();
                                    dr[0] = dtCopy.Rows[iRws][0].ToString();
                                    dr[1] = dtCopy.Rows[iRws][1].ToString();
                                    dr[2] = dtCopy.Rows[iRws][2].ToString();
                                    sNames = dtCopy.Rows[iRws][0].ToString();
                                    Name.Add(sNames);
                                    sDys += dr[1];
                                    sCls += Convert.ToInt32(dr[2]);
                                    dtJointWork.Rows.RemoveAt(sRowVal - 1);
                                    sRowVal--;
                                }
                                else
                                {
                                    dr = dtJointWork.NewRow();
                                    dr[0] = dtCopy.Rows[iRws][0].ToString();
                                    dr[1] = dtCopy.Rows[iRws][1].ToString();
                                    dr[2] = dtCopy.Rows[iRws][2].ToString();
                                    Session["Val"] = dr[0];
                                    sNames = Session["Val"].ToString();
                                    Name.Add(sNames);
                                    sDys = dr[1].ToString();
                                    sCls = Convert.ToInt32(dr[2]);
                                }

                                string strJointName = "";

                                string st = strSplSfname[0].Trim().Replace("(", "").Replace(")", "").Replace("-", "");
                                if (str2 != null)
                                {
                                    if (sNames.Trim() == str2.Trim() || sNames.Trim() == st.Trim())
                                    {
                                        strJointName = " ( SELF )";
                                    }

                                }
                                else
                                {
                                    if (sNames.Trim() == st.Trim())
                                    {
                                        strJointName = " ( SELF )";
                                    }
                                }

                                dtJointWork.Rows.Add(sNames + strJointName, sDys, sCls);
                                sExstName = dtCopy.Rows[iRws][0].ToString();
                                sRowVal++;
                                //}
                                // }
                            }



                            //Empty Text
                            gv.EmptyDataText = "*** No Joint Work ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
                            gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
                            //
                            gv.DataSource = dtJointWork;
                            gv.RowDataBound += new GridViewRowEventHandler(this.grdJoint_RowDataBound);
                        }
                        //
                        #endregion
                        //
                        gv.DataBind();
                        pnlTbl.Controls.Add(gv);

                        html.Append("</td>");
                    }
                }
                html.Append("</tr>");
            }
            html.Append("</table><br>");
            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
            iDelayedTblCnt++;
        }
    }
    #endregion
    #region grdMain_RowDataBound
    private void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int indx = e.Row.RowIndex;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 4;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            //
            e.Row.Cells[5].Text = iFtrJnt.ToString();
            e.Row.Cells[6].Text = "-";
            e.Row.Cells[7].Text = "-";
            e.Row.Cells[8].Text = iFtrDev.ToString();

            double valTotal = new double();
            if (sDeActiveDr != 0)
            {
                valTotal = iFtrDrMt - sDeActiveDr;
            }
            else
            {
                valTotal = iFtrDrMt;
            }
            string str = "(" + sDeActiveDr + ")";
            e.Row.Cells[9].Text = valTotal.ToString() + str;
            e.Row.Cells[12].Text = iFtrDrPob.ToString();
            //e.Row.Cells[13].Text = iFtrUlMt.ToString();
            //e.Row.Cells[14].Text = iFtrChmMt.ToString();
            //e.Row.Cells[15].Text = iFtrChmPob.ToString();
            //e.Row.Cells[16].Text = iFtrStkMt.ToString();
            e.Row.Cells[13].Text = iFtrChmMt.ToString();
            e.Row.Cells[14].Text = iFtrChmPob.ToString();
            e.Row.Cells[15].Text = iFtrStkMt.ToString();
            e.Row.Cells[16].Text = "";


            e.Row.Cells[0].Text = "Total";
            e.Row.HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Font.Bold = true;
            e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            sDeActiveDr = 0;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = e.Row.Cells[3].Text;
            }
            for (int r = 1; r < e.Row.Cells.Count; r++)
            {
                if (r == 1)
                {

                    string sDlydDate = dlyd[iDelayedTblCnt].Rows[0][2].ToString().Replace("[", "").Replace("]", "").Trim();
                    string sDlydType = dlyd[iDelayedTblCnt].Rows[0][1].ToString().Trim();
                    if (sDlydDate != "" && sDlydType == "Locked Date")
                    {
                        string[] dates = sDlydDate.Split(',');
                        foreach (string date in dates)
                        {
                            if (date.TrimStart() == e.Row.Cells[1].Text && date != "")
                            {
                                e.Row.Cells[3].Text = e.Row.Cells[3].Text + " (delay)";
                                e.Row.Cells[3].Attributes.Add("style", "color:red;");
                                break;
                            }
                        }
                    }
                    if (e.Row.Cells[3].Text.ToLower().Trim().TrimStart().TrimEnd().Contains("field work"))
                    {
                        HyperLink hLnk = new HyperLink();
                        hLnk.Text = e.Row.Cells[r].Text;
                        hLnk.Attributes.Add("class", "link_cursor");
                        string sUrl = "http://" + Request.Url.Authority.ToString() + "/MasterFiles/Reports/rptDCRViewApprovedDetails.aspx?sf_Name=";
                        hLnk.Attributes.Add("onclick", "javascript:window.open('" + sUrl + "&sf_code=" + e.Row.Cells[0].Text + "&Month=" + Request.QueryString["FMonth"].ToString() + "&Year=" + Request.QueryString["FYear"].ToString() +
                            "&div_code=" + div_code + "&Day=" + e.Row.Cells[r].Text + "' ,null,'');");
                        hLnk.Font.Underline = false;
                        hLnk.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[r].Controls.Add(hLnk);
                    }
                }
                else if (r == 11 || r == 12)
                {
                    e.Row.Cells[r].Visible = false;
                    /*
                    HyperLink hyLnk = new HyperLink();
                    hyLnk.Text = e.Row.Cells[r].Text;
                    hyLnk.NavigateUrl = "#";
                    e.Row.Cells[r].Controls.Add(hyLnk);*/
                }
                else if (r == 4 || r == 7)
                {
                    e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace("[", "").Replace(",]", "").Replace(", ]", "").Replace("]", "");
                    //e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace(sName1, "SELF").Replace(sName2, "SELF");
                    e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace(",SELF", ",").Replace(", SELF", ",").Replace("SELF,", ",").Replace(",,", ",").Replace(", ,", ",");
                    //
                    try
                    {
                        if (e.Row.Cells[r].Text.Substring(0, 1) == ",")
                            e.Row.Cells[r].Text = e.Row.Cells[r].Text.Remove(0, 1);
                    }
                    catch (Exception exx) { }
                    try
                    {
                        if (e.Row.Cells[r].Text.Substring(e.Row.Cells[r].Text.Length - 1, 1) == ",")
                            e.Row.Cells[r].Text = e.Row.Cells[r].Text.Remove(e.Row.Cells[r].Text.Length - 1, 1);
                    }
                    catch (Exception ex) { }
                    //
                    string[] sJName = e.Row.Cells[r].Text.Trim().TrimStart().TrimEnd().Split(',');
                    //if (sJName.Length > 1)
                    //{
                    //    string jName = "";
                    //    foreach (string sName in sJName)
                    //    {
                    //        if (jName.Contains(sName.TrimStart().TrimEnd().Trim()) || sName == "" || jName.Contains(sName))
                    //        { }
                    //        else
                    //        {
                    //            jName += sName + ", ";
                    //        }
                    //    }
                    //    e.Row.Cells[r].Text = jName.Remove(jName.LastIndexOf(","));
                    //}
                    //
                    if (r == 7)
                    {
                        if (e.Row.Cells[3].Text == "Field Work" || e.Row.Cells[3].Text == "Field Work (delay)" || e.Row.Cells[3].Text.Contains("Field Work"))
                        {
                            try
                            {
                                if (e.Row.Cells[r - 1].Text.Substring(e.Row.Cells[r - 1].Text.Length - 1, 1) == ",")
                                {
                                    e.Row.Cells[r - 1].Text = e.Row.Cells[r - 1].Text.Remove(e.Row.Cells[r - 1].Text.LastIndexOf(","));
                                }
                                else if (e.Row.Cells[r - 1].Text.Substring(0, 1) == ",")
                                {
                                    e.Row.Cells[r - 1].Text = e.Row.Cells[r - 1].Text.Remove(0, 1);
                                }
                            }
                            catch (Exception ex) { }
                            if (e.Row.Cells[r - 1].Text.Replace("&nbsp;", "") != "" || e.Row.Cells[r].Text.Replace("&nbsp;", "") != "")
                            {
                                string[] sPlan = e.Row.Cells[r - 1].Text.Replace("&nbsp;", "").Split(',');
                                if (sPlan.Length > 0 && sPlan[0] != "")
                                {
                                    e.Row.Cells[r + 1].Text = "*";
                                    iDevCount++;
                                    foreach (string plan in sPlan)
                                    {
                                        if (e.Row.Cells[r].Text.ToLower().Replace("&nbsp;", "").Contains(plan.ToLower()))
                                        {
                                            e.Row.Cells[r + 1].Text = "";
                                            iDevCount--;
                                            break;
                                        }
                                        if (plan.ToLower().TrimEnd().TrimStart().Contains("weekly off") || plan.TrimStart().TrimEnd().ToLower().Contains("leave")
                                            || plan.TrimEnd().TrimStart().ToLower().Contains("holiday"))
                                        {
                                            e.Row.Cells[r - 1].Attributes.Add("style", "color:red;");
                                        }
                                    }
                                }
                                else
                                {
                                    e.Row.Cells[r + 1].Text = "*";
                                    iDevCount++;
                                }
                            }
                        }
                        else
                        {
                            if (e.Row.Cells[3].Text.ToLower().Contains(e.Row.Cells[r - 1].Text.TrimStart().TrimEnd().ToLower()) || e.Row.Cells[r - 1].Text.TrimStart().TrimEnd() == "&nbsp;")
                            {
                                e.Row.Cells[r + 1].Text = "";
                            }
                            else
                            {
                                e.Row.Cells[r + 1].Text = "*";
                                iDevCount++;
                            }
                        }

                        if (e.Row.Cells[3].Text.ToLower() == "weekly off" || e.Row.Cells[3].Text.ToLower() == "holiday" || e.Row.Cells[3].Text.ToLower() == "leave")
                        {
                            e.Row.Attributes.Add("style", "font-weight:bold;");
                        }
                    }
                }
                else if (r == 5 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrJnt += Convert.ToInt32(e.Row.Cells[r].Text);
                    HyperLink hLnkJnt = new HyperLink();
                    hLnkJnt.Text = e.Row.Cells[r].Text;
                    hLnkJnt.Attributes.Add("class", "link_cursor");
                    hLnkJnt.Attributes.Add("onclick", "javascript:window.open('DCR_Analysis_Expand.aspx?sf_code=" + e.Row.Cells[0].Text + "&cMnth=" + Request.QueryString["FMonth"].ToString() + "&cYr=" + Request.QueryString["FYear"].ToString() +
                            "&type=2&cMode=" + e.Row.Cells[1].Text + "' ,null,'');");
                    hLnkJnt.Font.Underline = false;
                    hLnkJnt.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[r].Controls.Add(hLnkJnt);
                }
                else if (r == 8 && e.Row.Cells[r].Text == "*")
                {
                    iFtrDev++;
                }
                else if (r == 9 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrDrMt += Convert.ToInt32(e.Row.Cells[r].Text);
                    HyperLink hLnkMt = new HyperLink();
                    hLnkMt.Text = e.Row.Cells[r].Text;
                    hLnkMt.Attributes.Add("class", "link_cursor");
                    hLnkMt.Attributes.Add("onclick", "javascript:window.open('DCR_Analysis_Expand.aspx?sf_code=" + e.Row.Cells[0].Text + "&cMnth=" + Request.QueryString["FMonth"].ToString() + "&cYr=" + Request.QueryString["FYear"].ToString() +
                            "&type=3&cMode=" + e.Row.Cells[1].Text + "' ,null,'');");
                    hLnkMt.Font.Underline = false;
                    hLnkMt.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[r].Controls.Add(hLnkMt);

                    if (e.Row.Cells[3].Text == "Field Work" || e.Row.Cells[3].Text.Contains("Field Work") || (e.Row.Cells[3].Text == "Field Work" && e.Row.Cells[4].Text.Replace("&nbsp;", "").Trim() == ""))
                    {
                        //if (e.Row.Cells[r].Text.Replace("&nbsp;", "").Trim() != "" && e.Row.Cells[r + 3].Text.Replace("&nbsp;", "").Trim() != "")
                        //{

                        int i;
                        if (e.Row.Cells[3].Text == "Field Work" && e.Row.Cells[4].Text.Replace("&nbsp;", "").Trim() == "")
                        {
                            i = Convert.ToInt32(e.Row.Cells[r].Text);
                        }
                        else
                        {
                            i = Convert.ToInt32(e.Row.Cells[r].Text) - Convert.ToInt32(e.Row.Cells[r + 3].Text);
                        }

                        if (i != 0)
                        {
                            sDeActiveDr += i;
                            hLnkMt.Text = e.Row.Cells[r].Text + "(" + i + ")";
                        }

                        if (e.Row.Cells[4].Text.Replace("&nbsp;", "").Trim() == "")
                        {
                            hLnkMt.Attributes.Add("style", "color:red;");
                        }
                    }
                }
                else if (r == 10 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrDrPob += Convert.ToInt32(e.Row.Cells[r].Text);
                }
                //else if (r == 13 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                //{
                //    iFtrUlMt += Convert.ToInt32(e.Row.Cells[r].Text);
                //    HyperLink hLnkUMt = new HyperLink();
                //    hLnkUMt.Text = e.Row.Cells[r].Text;
                //    hLnkUMt.Attributes.Add("class", "link_cursor");
                //    hLnkUMt.Attributes.Add("onclick", "javascript:window.open('DCR_Analysis_Expand.aspx?sf_code=" + e.Row.Cells[0].Text + "&cMnth=" + Request.QueryString["FMonth"].ToString() + "&cYr=" + Request.QueryString["FYear"].ToString() +
                //            "&type=4&cMode=" + e.Row.Cells[1].Text + "' ,null,'');");
                //    hLnkUMt.Font.Underline = false;
                //    hLnkUMt.ForeColor = System.Drawing.Color.Blue;
                //    e.Row.Cells[r].Controls.Add(hLnkUMt);
                //}
                else if (r == 13 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrChmMt += Convert.ToInt32(e.Row.Cells[r].Text);
                    HyperLink hLnkChmMt = new HyperLink();
                    hLnkChmMt.Text = e.Row.Cells[r].Text;
                    hLnkChmMt.Attributes.Add("class", "link_cursor");
                    hLnkChmMt.Attributes.Add("onclick", "javascript:window.open('DCR_Analysis_Expand.aspx?sf_code=" + e.Row.Cells[0].Text + "&cMnth=" + Request.QueryString["FMonth"].ToString() + "&cYr=" + Request.QueryString["FYear"].ToString() +
                            "&type=5&cMode=" + e.Row.Cells[1].Text + "' ,null,'');");
                    hLnkChmMt.Font.Underline = false;
                    hLnkChmMt.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[r].Controls.Add(hLnkChmMt);
                }
                else if (r == 14 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrChmPob += Convert.ToDecimal(e.Row.Cells[r].Text);
                }
                else if (r == 15 && e.Row.Cells[r].Text != "" && e.Row.Cells[r].Text != null && e.Row.Cells[r].Text != "&nbsp;")
                {
                    iFtrStkMt += Convert.ToInt32(e.Row.Cells[r].Text);
                    //HyperLink hLnkStkst = new HyperLink();
                    //hLnkStkst.Text = e.Row.Cells[r].Text;
                    //hLnkStkst.Attributes.Add("class", "link_cursor");
                    //hLnkStkst.Attributes.Add("onclick", "javascript:window.open('DCR_Analysis_Expand.aspx?sf_code=" + e.Row.Cells[0].Text + "&cMnth=" + Request.QueryString["FMonth"].ToString() + "&cYr=" + Request.QueryString["FYear"].ToString() +
                    //        "&type=6&cMode=" + e.Row.Cells[1].Text + "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                    //hLnkStkst.Font.Underline = false;
                    //hLnkStkst.ForeColor = System.Drawing.Color.Blue;
                    //e.Row.Cells[r].Controls.Add(hLnkStkst);
                }
                else if (r == 3 && e.Row.Cells[r].Text.ToLower().Trim().Contains("weekly off") || e.Row.Cells[r].Text.ToLower().Trim().Contains("leave")
                    || e.Row.Cells[r].Text.ToLower().Trim().Contains("holiday"))
                {
                    e.Row.Attributes.Add("style", "background-color:#FFEAEA;");
                }

                if (e.Row.Cells[r].Text == "0")
                {
                    e.Row.Cells[r].Text = "";
                }
            }
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion
    //
    #region grdMain_RowAttance
    private void grdMain_RowAttance(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str = e.Row.Cells[0].Text;

            if (e.Row.Cells[0].Text == "Field Work" || e.Row.Cells[0].Text == "Total Calls Seen" || e.Row.Cells[0].Text == "Call Average")
            {
                e.Row.Cells[0].Style["font-weight"] = "bold";
                e.Row.Cells[1].Style["font-weight"] = "bold";
            }
        }
    }
    #endregion
    //
    #region grdMain_RowCreated
    private void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, "Date", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Submitted Date", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Work Type", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Worked With", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Joint Calls", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "As Per TP", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Worked", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Dev", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Listed Dr Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Drs POB", true, 0);
            // AddMergedCells(objgridviewrow, objtablecell, "Unlist Dr Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Chemist Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Chemist POB", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Stockist Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Stockist POB", true, 0);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
        }
    }
    #endregion
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, string celltext, bool wrap, int iVal)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        if (iVal == 0)
        {
            //objtablecell.Style.Add("background-color", "#F1F5F8");

            //objtablecell.ForeColor = System.Drawing.Color.White;
        }
        else
        {
            objtablecell.Style.Add("background-color", "#EEE8AA");
            objtablecell.ForeColor = System.Drawing.Color.Black;
        }
        objtablecell.Font.Bold = true;
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = wrap;
        objtablecell.Width = 25;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    // 
    #region grdDlyd_RowDataBound
    private void grdDlyd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Font.Bold = true;
            if (e.Row.Cells[0].Text.Contains("Locked"))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    #endregion
    //

    #region grdAttend_RowDataBound
    private void grdAttend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text == "Field Work" || e.Row.Cells[0].Text == "Weekly Off" || e.Row.Cells[0].Text == "Leave" || e.Row.Cells[0].Text == "Holiday")
            {
                e.Row.Cells[0].Font.Bold = true;
                e.Row.Cells[1].Font.Bold = true;
            }
            if (Is_SamInp == "1")
            {
                if (e.Row.Cells[1].Text == "Sample")
                {
                    e.Row.BackColor = System.Drawing.Color.Thistle;
                    e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[1].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp_Sam('" + Request.QueryString["sfcode"].ToString() + "',  '" + FMonth + "', '" + FYear + "', '" + FMonth + "', '" + FYear + "','" + Request.QueryString["sf_name"].ToString() + "','" + div_code + "')");     //Convert.ToString(dtrowClr.Rows[indx][1].ToString())
                    hLink.ToolTip = "Click here";
                    hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[1].Controls.Add(hLink);
                }

                if (e.Row.Cells[2].Text == "Input")
                {
                    e.Row.BackColor = System.Drawing.Color.Thistle;
                    e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[2].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp_Input('" + Request.QueryString["sfcode"].ToString() + "',  '" + FMonth + "', '" + FYear + "', '" + FMonth + "', '" + FYear + "','" + Request.QueryString["sf_name"].ToString() + "','" + div_code + "')");     //Convert.ToString(dtrowClr.Rows[indx][1].ToString())
                    hLink.ToolTip = "Click here";
                    hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[2].Controls.Add(hLink);
                }



            }
        }
    }
    #endregion



    #region grdCallDetaol_RowDataBound
    private void grdCallDetaol_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            if (e.Row.Cells[0].Text == "Call Average" || e.Row.Cells[0].Text == "Total Calls Seen")
            {
                e.Row.Cells[0].Font.Bold = true;
                e.Row.Cells[1].Font.Bold = true;
            }

            //Add S by Preethi 
            if (e.Row.Cells[0].Text == "FFcode")
            {
                ff_code = e.Row.Cells[1].Text;
                e.Row.Visible = false;
            }

            if (e.Row.Cells[0].Text == "Total Calls Seen")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = e.Row.Cells[1].Text;
                hLink.Attributes.Add("class", "btnDrSn");
                hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "3" + "','" + Request.QueryString["sf_name"].ToString() + "','" + ff_code + "')");     //Convert.ToString(dtrowClr.Rows[indx][1].ToString())
                hLink.ToolTip = "Click here";
                hLink.Font.Underline = true;
                hLink.Attributes.Add("style", "cursor:pointer");
                hLink.ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[1].Controls.Add(hLink);
            }


            //Add E by Preethi 
        }
    }
    #endregion

    #region grdJoint_RowDataBound
    private void grdJoint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text.Contains("SELF"))
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;

            }

        }
    }
    #endregion
}