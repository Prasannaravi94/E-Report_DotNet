//
#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;
#endregion
//
#region Class
public partial class MIS_Reports_DCR_Analysis_Expand : System.Web.UI.Page
{
    //
    #region Variables
    string sSf_Code, sDiv_Code, sF_Name;
    int iMnth, iYr;
    string sDesig;
    DataSet ds = new DataSet();
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sSf_Code = Request.QueryString["sf_code"].ToString();
            sDiv_Code = Request.QueryString["div_code"].ToString();
            iMnth = Convert.ToInt32(Request.QueryString["Month"].ToString());
            iYr = Convert.ToInt32(Request.QueryString["Year"].ToString());
            sDesig = Request.QueryString["Desig"].ToString();
            //iDay = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(iMnth.ToString());
            //
            lblHead.Text = " Vacant HQ's Doctors Visited by MANAGER For the Month Of : <font color='#0077FF'>" + strFrmMonth.ToString() + " - " + iYr.ToString() + "</font>";
            LblForceName.Text = "Vacant Field Force Name : <font color='#0077FF'>" + getSf_Name(sSf_Code) + "</font>";
            FillDatas(sSf_Code, sDiv_Code, iMnth, iYr, sDesig);
        }
    }
    #endregion
    //
    #region Get Sf_Name
    private string getSf_Name(string sfCode)
    {
        DB_EReporting db = new DB_EReporting();
        DataTable dtName = db.Exec_DataTable("SELECT Sf_Name FROM Mas_SalesForce WHERE Sf_Code = '" + sfCode + "'");
        return sF_Name = dtName.Rows[0][0].ToString();
    }
    #endregion
    //
    #region FillDatas
    private void FillDatas(string sSf_Code, string sDiv_Code, int iMnth, int iYr, string sDesig)
    {
        SalesForce sf = new SalesForce();
        DB_EReporting db = new DB_EReporting();
        string cDate = (iMnth + 1).ToString() + "-01-" + iYr.ToString();
        if (iMnth == 12)
        {
            cDate = "01-01-" + (iYr + 1).ToString();
        }
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //
        SqlCommand cmd = new SqlCommand("Visit_Detail_Vacant_MRs_DrList_Visit_By_MGR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code.ToString()));
        cmd.Parameters.AddWithValue("@Msf_Code", sSf_Code);
        cmd.Parameters.AddWithValue("@cMnth", iMnth);
        cmd.Parameters.AddWithValue("@cYr", iYr);
        cmd.Parameters.AddWithValue("@cDate", cDate);
        cmd.Parameters.AddWithValue("@Desig", sDesig);
        //cmd.Parameters.AddWithValue("@Mode", 1);
        cmd.CommandTimeout = 250;
        //
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        //
        int iDeside = 0;
        //
        GridView gView = new GridView();
        foreach (DataTable dtTbl in ds.Tables)
        {
            if (iDeside != 4)
            {
                dtTbl.Columns.RemoveAt(2);
                dtTbl.Columns.RemoveAt(1);
            }
            if (iDeside == 0)
            {
                gView = GrdUnLst;
                gView.Caption = "<b><u><h3>OVERALL DESIGNATION MET DOCTORS LIST</h3></u></b>";                
            }
            else if (iDeside == 1)
            {
                gView = GrdChmst;
                gView.Caption = "<b><u><h3>OVERALL MISSED DOCTORS LIST</h3></u></b>";
            }
            else if (iDeside == 2)
            {
                gView = GrdJoint;
                gView.Caption = "<b><u><h3>DESIGNATION (<font color='Red'>" + sDesig + "</font>) MET DOCTORS LIST</h3></u></b>";
            }
            else if (iDeside == 3)
            {
                gView = GrdTtl;
                gView.Caption = "<b><u><h3>DESIGNATION (<font color='Red'>" + sDesig + "</font>) MISSED DOCTORS LIST</h3></u></b>";
            }
            //
            if (iDeside == 4)
            {
                string sMgr_Names="";
                foreach (DataRow dtRow in dtTbl.Rows)
                {
                    sMgr_Names += dtRow[1].ToString() + ",";
                }
                lblMgr_Name.Text = "<b><font color='#696d6e'><u>MANAGERs</u> : </font>" + sMgr_Names + "</b>";
                lblMgr_Names.Text = "<b><font color='#696d6e'><u>MANAGERs</u> : </font>" + sMgr_Names + "</b>";
            }
            else
            {
                gView.DataSource = dtTbl;
                gView.DataBind();
                iDeside++;
            }
        }
    }
    #endregion
    //
    #region GrdArrangeDate_RowDataBound
    protected void GrdArrangeDate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //
            string[] sArDates = e.Row.Cells[7].Text.Split(',');
            if (sArDates.Length > 0)
            {
                List<int> iDate = new List<int>();
                string sDate = "";
                foreach (string item in sArDates)
                {
                    if (item != "")
                        iDate.Add(Convert.ToInt32(item));
                }
                //
                iDate.Sort();
                foreach (int val in iDate)
                {
                    sDate += val.ToString() + ",";
                }
                //
                if (sDate.Contains(","))
                    sDate.Remove(sDate.LastIndexOf(","), 1);
                e.Row.Cells[7].Text = sDate;
                //
            }
            //
        }
    }
    #endregion
    //
    #region grdDoctor_Sorting
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDesignation=Request.QueryString["Desig"].ToString();
        FillDatas(Request.QueryString["sf_code"].ToString(), Request.QueryString["div_code"].ToString(),
            Convert.ToInt32(Request.QueryString["Month"].ToString()), Convert.ToInt32(Request.QueryString["Year"].ToString()),
            sDesignation);
        GridView gv = (GridView)sender;
        //
        DataTable dt = new DataTable();
        //
        GridView gvid = new GridView();
        if (gv.Caption == "<b><u><h3>OVERALL DESIGNATION MET DOCTORS LIST</h3></u></b>")
        {
            dt = ds.Tables[0];
            gvid = GrdUnLst;
        }
        else if (gv.Caption == "<b><u><h3>OVERALL MISSED DOCTORS LIST</h3></u></b>")
        {
            dt = ds.Tables[1];
            gvid = GrdChmst;
        }
        else if (gv.Caption == "<b><u><h3>DESIGNATION (<font color='Red'>" + sDesignation + "</font>) MET DOCTORS LIST</h3></u></b>")
        {
            dt = ds.Tables[2];
            gvid = GrdJoint;
        }
        else if (gv.Caption == "<b><u><h3>DESIGNATION (<font color='Red'>" + sDesignation + "</font>) MISSED DOCTORS LIST</h3></u></b>")
        {
            dt = ds.Tables[3];
            gvid = GrdTtl;
        }
        //        
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
            //
            gvid.DataSource = sortedView;
            gvid.DataBind();
            //
        }
    }
    //
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
    #endregion
    //
}
#endregion
//