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
public partial class Product_Det_Drs_Zoom : System.Web.UI.Page
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
    string strPrdName = string.Empty;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            cMnth = Request.QueryString["FMonth"].ToString();
            cYr = Request.QueryString["FYear"].ToString();
          //  sCode = Request.QueryString["cTyp_cd"].ToString();
            sType = Request.QueryString["prod"].ToString();
            sMode = Request.QueryString["cMode"].ToString();
            strPrdName = Request.QueryString["PrdName"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(cMnth);
            string sTitle = "";
           
                sTitle = "Product Detailing Doctors";
          
            lblHead.Text = sTitle + " For the Month of - " + strFrmMonth + " " + cYr;
            lblProd.Text = "Product Name : " + strPrdName;
            FillDetails(div_code, sf_code, cMnth, cYr,  sType, sMode);
        }
    }    
    #endregion
    //
    #region FillDetails
    private void FillDetails(string div_code, string sf_code, string cMnth, string cYr,  string sType, string sMode)
    {
        SalesForce sf = new SalesForce();
        DB_EReporting db = new DB_EReporting();
        string cDate = (Convert.ToInt32(cMnth) + 1).ToString() + "-01-" + cYr;
        if (cMnth == "12")
        {
            cDate = "01-01-" + (Convert.ToInt32(cYr) + 1).ToString();
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //
        string sProc_Name = "";

        sProc_Name = "Product_Details_Dr";
           // GrdDoctor.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
       
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(cMnth));
        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(cYr));
        cmd.Parameters.AddWithValue("@cDate", cDate);
       
        cmd.Parameters.AddWithValue("@sTyp", Convert.ToInt32(sType));
        cmd.Parameters.AddWithValue("@mode", Convert.ToInt32(sMode));
        cmd.CommandTimeout = 300;
        //
        SqlDataAdapter da = new SqlDataAdapter(cmd);        
        da.Fill(dsts);
        con.Close();
        dsts.Tables[0].Columns.RemoveAt(11);
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
        FillDetails(Request.QueryString["div_code"].ToString(), Request.QueryString["sfcode"].ToString(),
            Request.QueryString["FMonth"].ToString(), Request.QueryString["FYear"].ToString(), 
            Request.QueryString["prod"].ToString(), Request.QueryString["cMode"].ToString());
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
