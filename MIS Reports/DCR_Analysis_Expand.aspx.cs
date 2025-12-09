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

public partial class MIS_Reports_DCR_Analysis_Expand : System.Web.UI.Page
{
    //
    string sSf_Code, sDiv_Code, sDate, sF_Name;
    int iMnth, iYr, iMode, iDay;
    DataSet ds = new DataSet();
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sSf_Code = Request.QueryString["sf_code"].ToString();
            sDiv_Code = Session["div_code"].ToString();
            iMnth = Convert.ToInt32(Request.QueryString["cMnth"].ToString());
            iYr = Convert.ToInt32(Request.QueryString["cYr"].ToString());
            iMode = Convert.ToInt32(Request.QueryString["type"].ToString());
            iDay = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(iMnth.ToString());
            string sTitle = "";
            if (iMode == 1)
                sTitle = "Total Listed Doctors";
            else if (iMode == 2)
                sTitle = "Joint Calls Detail";
            else if (iMode == 3)
                sTitle = "Listed Doctors Met";
            else if (iMode == 4)
                sTitle = "UnListed Doctors Met";
            else if (iMode == 5)
                sTitle = "Chemists Met";
            lblHead.Text = sTitle + " For the Date of : <font color='#0077FF'>" + iDay.ToString() + " - " + strFrmMonth.Substring(0, 3) + " - " + iYr.ToString() + "</font>";
            LblForceName.Text = "Field Force Name : <font color='green'>" + getSf_Name(sSf_Code) + "</font>";
            FillDatas(sSf_Code, sDiv_Code, iMnth, iYr, iMode, iDay);
        }
    }

    private string getSf_Name(string sfCode)
    {
        DB_EReporting db = new DB_EReporting();
        DataTable dtName = db.Exec_DataTable("SELECT Sf_Name FROM Mas_SalesForce WHERE Sf_Code = '" + sfCode + "'");
        return sF_Name = dtName.Rows[0][0].ToString();
    }

    private void FillDatas(string sSf_Code, string sDiv_Code, int iMnth, int iYr, int iMode, int iDay)
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
        SqlCommand cmd = new SqlCommand("DCR_Analysis_Zoom", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code.ToString()));
        cmd.Parameters.AddWithValue("@Sf_Code", sSf_Code);
        cmd.Parameters.AddWithValue("@cMnth", iMnth);
        cmd.Parameters.AddWithValue("@cYr", iYr);
        cmd.Parameters.AddWithValue("@cDate", cDate);
        cmd.Parameters.AddWithValue("@cDay", iDay);
        //    
        cmd.Parameters.AddWithValue("@cMode", iMode);
        cmd.CommandTimeout = 250;
        //
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        //
        int iDeside = 0;
        if (ds.Tables.Count > 0 && ds.Tables.Count < 2)
        {
            iDeside = Convert.ToInt32(Request.QueryString["type"].ToString()) - 2;
        }
        foreach (DataTable dtTbl in ds.Tables)
        {
            dtTbl.Columns.RemoveAt(2);
            dtTbl.Columns.RemoveAt(1);            
            if (iDeside == 0)
            {
                GrdJoint.DataSource = dtTbl;
                GrdJoint.DataBind();
                iDeside++;
            }
            else if (iDeside == 1)
            {
                GrdTtl.DataSource = dtTbl;
                GrdTtl.DataBind();
                iDeside++;
            }
            else if (iDeside == 2)
            {
                GrdUnLst.DataSource = dtTbl;
                GrdUnLst.DataBind();
                iDeside++;
            }
            else if (iDeside == 3)
            {
                GrdChmst.DataSource = dtTbl;
                GrdChmst.DataBind();
                iDeside++;
            }
        }
    }
    //
    protected void GrdJoint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sF_Name == null)
            {
                sF_Name = getSf_Name(Request.QueryString["sf_code"].ToString());
            }
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Trim().TrimEnd().TrimStart().Replace(sF_Name.TrimStart().TrimEnd().Trim(), "");
            try
            {
                if (e.Row.Cells[2].Text.Substring(e.Row.Cells[2].Text.Trim().TrimEnd().TrimStart().Length - 1, 1).Contains(","))
                {
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Remove(e.Row.Cells[2].Text.Trim().TrimEnd().TrimStart().Length - 1, 1);
                }
                if (e.Row.Cells[2].Text.Substring(0, 1).Contains(","))
                {
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Remove(0, 1);
                }
            }
            catch (Exception ex)
            { }
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace(",,", ",");
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
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
    {
        FillDatas(Request.QueryString["sf_code"].ToString(), Session["div_code"].ToString(),
            Convert.ToInt32(Request.QueryString["cMnth"].ToString()), Convert.ToInt32(Request.QueryString["cYr"].ToString()),
            Convert.ToInt32(Request.QueryString["type"].ToString()), Convert.ToInt32(Request.QueryString["cMode"].ToString()));
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
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
            if (Request.QueryString["type"].ToString() == "2")
            {
                GrdJoint.DataSource = sortedView;
                GrdJoint.DataBind();
            }
            else if (Request.QueryString["type"].ToString() == "3")
            {
                GrdTtl.DataSource = sortedView;
                GrdTtl.DataBind();
            }
            else if (Request.QueryString["type"].ToString() == "4")
            {
                GrdUnLst.DataSource = sortedView;
                GrdUnLst.DataBind();
            }
            else if (Request.QueryString["type"].ToString() == "5")
            {
                GrdChmst.DataSource = sortedView;
                GrdChmst.DataBind();
            }
        }
    }
    //
}