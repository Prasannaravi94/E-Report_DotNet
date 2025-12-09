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
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
public partial class MasterFiles_AnalysisReports_rpt_Not_At_All_Visit_Drs_Zoom : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string Mode = string.Empty;
    int mode;
    DataTable dtrowClr = new DataTable();
    string sf_name = string.Empty;
    string sf_hq = string.Empty;
    string sf_desig = string.Empty;
    DataSet dsdate = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sSf_code"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        Mode = Request.QueryString["mode"].ToString();
        mode = Convert.ToInt32(Request.QueryString["mode"].ToString());
        SalesForce sf = new SalesForce();
        DataSet dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
        {
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            sf_desig = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
            sf_hq = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
        }
        if (!Page.IsPostBack)
        {
            lblHead.Text = lblHead.Text +  "<span style='font-weight: bold;color:Black;'> " + " ( " + sf_name + " - " + sf_desig + " - " + sf_hq + " )" + "</span>";
            FillDoctor();
        }
    }
    private void FillDoctor()
    {
        var lastSixMonths = Enumerable
  .Range(0, mode)
  .Select(i => DateTime.Now.AddMonths(i - mode + 1))
  .Select(date => date.ToString("MM/yyyy"));
        // FillReport();
        string[] sck;
        sck = lastSixMonths.ToArray();
        var last = sck.Last().Split('/');
        var first = sck.First().Split('/');
        int months = (Convert.ToInt32(last[1]) - Convert.ToInt32(first[1])) * 12 + Convert.ToInt32(last[0]) - Convert.ToInt32(first[0]); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(first[0]);
        int cyear = Convert.ToInt32(first[1]);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
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
        //

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Not_At_all_Visit_drs_List";

        //else if (sReportType == 2)
        //{
        //    sProc_Name = "visit_fixation_Spclty";
        //}
        //else if (sReportType == 3)
        //{
        //    sProc_Name = "visit_fixation_Class";
        //}
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
   

        if (dsts.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsts;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsts;
            grdDoctor.DataBind();

        }

    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldrcode = (Label)e.Row.FindControl("lblDRCode");
            Label lblvisit = (Label)e.Row.FindControl("lblVisit");
            string sActive_date = string.Empty;
            DCR dcrdr = new DCR();
            dsdate = dcrdr.getPrevious_Visit_Miss(sf_code, lbldrcode.Text);
            if (dsdate.Tables[0].Rows.Count > 0)
                //foreach (DataRow drSF in dsdate.Tables[0].Rows)
                //{
                //    sActive_date = sActive_date + drSF["Activity_Date"].ToString() + " , ";
                //}
                //if (sActive_date.Length > 0)
                //    sActive_date = sActive_date.Substring(0, sActive_date.Length - 2);
                lblvisit.Text = "" + dsdate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


            if (lblvisit.Text == "")
            {
                lblvisit.Text = " - ";
            }
        }
    }

}