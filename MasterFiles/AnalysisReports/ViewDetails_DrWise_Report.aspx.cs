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
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Coverage_New : System.Web.UI.Page
{
    //
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string cMnth = string.Empty;
    string cYr = string.Empty;
    string sCode = string.Empty;
    string sType = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsts = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Spclty_Code = string.Empty;
    string Spclty_Name = string.Empty;
    string HQ_Code = string.Empty;
    string HQ_Name = string.Empty;

    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

        string sTitle = "";
        div_code = Session["div_code"].ToString();// Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        if (sMode == "6")
        {
            FMonth = Request.QueryString["FMnth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            Spclty_Code = Request.QueryString["Spclty_Code"].ToString();
            Spclty_Name = Request.QueryString["Spclty_Name"].ToString();
            if (Spclty_Name == "Grand Total")
            {
                Spclty_Name = "All";
            }

            sTitle = "Speciality Wise Coverage Analysis";
            lblSpci.Text = "Speciality Name : " + Spclty_Name;
        }
        else if (sMode == "7")
        {
            FMonth = Request.QueryString["FMnth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            HQ_Code = Request.QueryString["HQ_Code"].ToString();
            HQ_Name = Request.QueryString["HQ_Name"].ToString();


            sTitle = "HQ Wise Stockist ";
            lblSpci.Text = "HQ Name : " + HQ_Name;
        }
        else
        {
            cMnth = Request.QueryString["cMnth"].ToString();
            cYr = Request.QueryString["cYr"].ToString();
        }
        sCode = Request.QueryString["cTyp_cd"].ToString();
        sType = Request.QueryString["Typ"].ToString();

        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(cMnth);

        if (sType == "1")
            sTitle = "Total Listed Doctors";
        else if (sType == "2")
            sTitle = "Listed Doctors Missed";
        else if (sType == "3")
            sTitle = "Listed Doctors Met";
        else if (sType == "4")
            sTitle = "Listed Doctors Seen";


        lblHead.Text = sTitle + " For the Month of - " + strFrmMonth + " " + cYr;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        if (!Page.IsPostBack)
        {
            FillDetails(div_code, sf_code, cMnth, cYr, sCode, sType, sMode);
        }
    }
    #endregion
    //
    #region FillDetails
    private void FillDetails(string div_code, string sf_code, string cMnth, string cYr, string sCode, string sType, string sMode)
    {
        SalesForce sf = new SalesForce();
        DB_EReporting db = new DB_EReporting();

        string cDate = string.Empty;
        if (Request.QueryString["cMode"].ToString() != "6" && Request.QueryString["cMode"].ToString() != "7")
        {
            cDate = (Convert.ToInt32(cMnth) + 1).ToString() + "-01-" + cYr;
            if (cMnth == "12")
            {
                cDate = "01-01-" + (Convert.ToInt32(cYr) + 1).ToString();
            }
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //
        string sProc_Name = "";
        if (sMode == "1")
        {
            sProc_Name = "visit_details_Dr_Wise_Cat";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.Pink;
        }
        else if (sMode == "2")
        {
            sProc_Name = "visit_details_Dr_Wise_Spclty";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
        }
        else if (sMode == "3")
        {
            sProc_Name = "visit_details_Dr_Wise_Cls";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightSeaGreen;
        }
        else if (sMode == "4")
        {
            sProc_Name = "visit_details_Dr_Wise";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        }
        else if (Request.QueryString["cMode"].ToString() == "6")
        {
            sProc_Name = "Sp_Spcltywise_Coverage_Analysis_Zoom";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        }
        else if (Request.QueryString["cMode"].ToString() == "7")
        {
            sProc_Name = "Sp_PSaleHQwise_Stk_Zoom";
            GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        }
        //
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        if (Request.QueryString["cMode"].ToString() == "6")
        {
            cmd.Parameters.AddWithValue("@FMonth", Request.QueryString["FMnth"].ToString());
            cmd.Parameters.AddWithValue("@FYear", Request.QueryString["FYear"].ToString());
            cmd.Parameters.AddWithValue("@TTMonth", Request.QueryString["TMonth"].ToString());
            cmd.Parameters.AddWithValue("@TTYear", Request.QueryString["TYear"].ToString());
            cmd.Parameters.AddWithValue("@Doc_Special_Code", Request.QueryString["Spclty_Code"].ToString());
        }
      else  if (Request.QueryString["cMode"].ToString() == "7")
        {
            cmd.Parameters.AddWithValue("@FMonth", Request.QueryString["FMnth"].ToString());
            cmd.Parameters.AddWithValue("@FYear", Request.QueryString["FYear"].ToString());
            cmd.Parameters.AddWithValue("@TTMonth", Request.QueryString["TMonth"].ToString());
            cmd.Parameters.AddWithValue("@TTYear", Request.QueryString["TYear"].ToString());
            cmd.Parameters.AddWithValue("@HQ_Code", Request.QueryString["HQ_Code"].ToString());
        }
        else
        {
            cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(cMnth));
            cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(cYr));
            cmd.Parameters.AddWithValue("@cDate", cDate);
            //
            if (sMode != "4")
                cmd.Parameters.AddWithValue("@cCode", Convert.ToInt32(sCode));
            //    
            cmd.Parameters.AddWithValue("@sTyp", Convert.ToInt32(sType));
        }
        cmd.CommandTimeout = 250;
        //
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        con.Close();
        if (sType == "3")
            dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(1);
        //
        GrdDoctor.DataSource = dsts;
        GrdDoctor.DataBind();
    }
    #endregion    
    //
    #region Sorting
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
    //
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (sMode == "6")
        {
            FillDetails(Session["div_code"].ToString(), Request.QueryString["sf_code"].ToString(),
      Request.QueryString["FMnth"].ToString(), Request.QueryString["FYear"].ToString(),
      Request.QueryString["TMonth"].ToString(), Request.QueryString["TYear"].ToString(), Request.QueryString["Spclty_Code"].ToString());
        }
        else if (sMode == "7")
        {
            FillDetails(Session["div_code"].ToString(), Request.QueryString["sf_code"].ToString(),
      Request.QueryString["FMnth"].ToString(), Request.QueryString["FYear"].ToString(),
      Request.QueryString["TMonth"].ToString(), Request.QueryString["TYear"].ToString(), Request.QueryString["HQ_Code"].ToString());
        }
        else
        {
            FillDetails(Session["div_code"].ToString(), Request.QueryString["sf_code"].ToString(),
                Request.QueryString["cMnth"].ToString(), Request.QueryString["cYr"].ToString(),
                Request.QueryString["cTyp_cd"].ToString(), Request.QueryString["Typ"].ToString(), Request.QueryString["cMode"].ToString());
        }
        DataTable dt = new DataTable();
        dt = dsts.Tables[0];
        {
            string SortDir = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                SortDir = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                SortDir = "Asc";
            }
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + SortDir;
            GrdDoctor.DataSource = sortedView;
            GrdDoctor.DataBind();
        }
    }
    #endregion
    //
}
