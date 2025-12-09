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
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    string sDesignation="";
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["cMonth"].ToString();
        FYear = Request.QueryString["cYear"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        sDesignation = Request.QueryString["Designation"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        lblHead.Text = "Tour Plan - Consolidated View for - (" + strFrmMonth + " - " + FYear + ")";
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        ViewReport();
    }
    #endregion
    //
    #region ViewReport
    private void ViewReport()
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Tp_Consol_View", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth);
        cmd.Parameters.AddWithValue("@cYr", FYear);
        cmd.Parameters.AddWithValue("@sDesg", sDesignation);
        cmd.Parameters.AddWithValue("@iBaseLvl", Convert.ToInt32(Request.QueryString["bsLvl"].ToString()));
        cmd.CommandTimeout = 90;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        dt.Columns.RemoveAt(8);
        dt.Columns.RemoveAt(4);

        /************ OUTPUT TABLE STRUCTURE ******************
        Sf_Code Sf_Name             Des HQ          Sf_Code Day WType       TP                                              Cnt
        ---(1)----(2)---------------(3)---(4)---------(5)---(6)---(7)--------(8)--------------------------------------------(9)-
        ************************************************************************************************************************
        MGR0341	MASOOD ALI	        ASM	VARANASI	MGR0341	1	Field Work	AVINASH SRIVASTAV(IND) - (JAUNPUR (JAUNPUR))	1
        MGR0341	MASOOD ALI	        ASM	VARANASI	MGR0341	2	Field Work	NILESH SINGH - (PANDEYPUR (VARANASI))	        1
        MGR0341	MASOOD ALI	        ASM	VARANASI	MGR0341	19	Weekly OFF		                                            1
        ----------- MR & MGR ---------------
        MR0844	ASHISH SRIVASTAV	DSO	VARANASI	MR0844	3	Field Work	CHUNAR ROAD (VARANSI)	                        1
        MR0844	ASHISH SRIVASTAV	DSO	VARANASI	MR0844	4	Field Work	SIGRA (VARANASI)	                            1
        MR0844	ASHISH SRIVASTAV	DSO	VARANASI	MR0844	5	Weekly Off	0	                                            1
        MR0844	ASHISH SRIVASTAV	DSO	VARANASI	MR0844	6	Field Work	LANKA (VARANASI)	                            1      
        */
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("Sf_Code");
        dtNew.Columns.Add("sf_name");
        dtNew.Columns.Add("desg");
        dtNew.Columns.Add("hq");
        dtNew.Columns.Add("Dt");
        dtNew.Columns.Add("Territory Planed");

        foreach (DataRow dr in dt.Rows)
        {            
            string sTP="";
            if (dr["WType"].ToString() == "Field Work")
                sTP = dr["TP"].ToString();
            else
                sTP = dr["WType"].ToString();

            dtNew.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), sTP);
        }
        // Spliting DataTable based on Sf_code
        List<DataTable> result = dtNew.AsEnumerable()
            .GroupBy(row => row.Field<string>("Sf_Code"))
            .Select(g => g.CopyToDataTable())
            .ToList();
        //
        if (result.Count > 0)
        {
            StringBuilder html = new StringBuilder();
            //
            //Table start.
            html.Append("&nbsp;&nbsp;&nbsp;&nbsp; <table cellspacing='0' border='1' style='border-collapse: collapse;border:1px solid #dee2e6'>");
            int icount = 0, iVal = 0;
            //Building the Header row.
            html.Append("<tr>");
            //
            if (result.Count > 3)
                icount = 4;
            else
                icount = result.Count;
            //
            for (int i = 0; i < icount; i++)
            {
                html.Append("<td align='center' valign='top' nowrap style='border-collapse: collapse;border:1px solid #dee2e6'>");
                html.Append("<table width='98%' align='center' style='border-collapse: collapse; margin-top:3px; margin-bottom:2px; margin-left:1px; margin-right:1px;border:1px solid #dee2e6';' cellspacing='0' border = '1'>");
                DataTable dtFinal = result[i].Copy();
                //
                html.Append("<th colspan='2' bgcolor='#F1F5F8' nowrap style='height:50px;border:1px solid #dee2e6;'>");
                html.Append(dtFinal.Rows[0]["sf_name"].ToString() + " - " + dtFinal.Rows[0]["desg"].ToString() + " - " + dtFinal.Rows[0]["hq"].ToString());
                html.Append("</th>");
                html.Append("<tr>");
                //
                dtFinal.Columns.RemoveAt(3); // Removing hq Column
                dtFinal.Columns.RemoveAt(2); // Removing desg Column
                dtFinal.Columns.RemoveAt(1); // Removing Sf_name Column
                dtFinal.Columns.RemoveAt(0); // Removing Sf_code Column
                //
                string sDataExist = dtFinal.Rows[0][0].ToString();
                if (sDataExist!="")
                {
                    foreach (DataColumn column in dtFinal.Columns)
                    {
                        html.Append("<th bgcolor='white' nowrap style='border:1px solid #dee2e6'><font color='#0077FF'>");
                        html.Append(column.ColumnName);
                        html.Append("</font></th>");
                    }
                    html.Append("</tr>");
                    //
                    //Building the Data rows.
                    foreach (DataRow row in dtFinal.Rows)
                    {
                        html.Append("<tr style='border:1px solid #dee2e6;'>");
                        foreach (DataColumn column in dtFinal.Columns)
                        {
                            if (row[1].ToString().ToLower()=="weekly off" || row[1].ToString().ToLower()=="week off" 
                                || row[1].ToString().ToLower()=="leave" || row[1].ToString().ToLower() == "holiday")
                                html.Append("<td bgcolor='#F1F5F8' style='border:1px solid #dee2e6'>");
                            else
                                html.Append("<td style='border:1px solid #dee2e6'>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                        html.Append("</tr>");
                    }
                }
                else
                {
                    html.Append("<td colspan='2' bgcolor='white' align='center'><font size='2' color='red'><b> **** Not Planned **** </b></font></td>");
                }
                html.Append("</table>");
                html.Append("</td>");
                iVal = i;
                iVal++;
                if (iVal == icount)
                {
                    html.Append("</tr>");
                    html.Append("<tr>");
                    if (iVal + 4 > result.Count)
                        icount = result.Count;
                    else
                        icount += 4;
                }
            }
            //Table end.
            html.Append("</table><br><br>");
            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
    //
    #endregion
    //
}