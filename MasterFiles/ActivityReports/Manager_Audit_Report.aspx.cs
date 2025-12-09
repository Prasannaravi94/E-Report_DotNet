#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.IO;
using System.Data;
using Bus_EReport;
#endregion
public partial class Manager_Audit_Report : System.Web.UI.Page
{
    #region Variables
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    DataSet dsSalesforce = null;
    DataTable dtrowClr = new DataTable();
    private string Mode;
    private string ModeValue;
    string name = string.Empty;
    string mode = string.Empty;
    string FFName = string.Empty;
    string code = string.Empty;
    string Num = string.Empty;

    #endregion
    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
            Mode = Request.QueryString["Mode"].ToString();
            ModeValue = Request.QueryString["ModeValue"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Manager Audit Report For the Month Of - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;

            FillReport();
        }
    }
    #endregion
    #region Report
    private void FillReport()
    {
        DCR dc = new DCR();
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        if (Mode == "0")
        {
            dsSalesforce = dc.AuditLogin_Details(sf_code, div_code, cmonth, cyear);

            dtrowClr = dsSalesforce.Tables[0].Copy();
            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdReport.DataSource = dsSalesforce;
                grdReport.DataBind();
            }
            else
            {
                grdReport.DataSource = dsSalesforce;
                grdReport.DataBind();
            }
        }
        else
        {
            dsSalesforce = dc.AuditLogin_DetailsMode(sf_code, div_code, cmonth, cyear, ModeValue);

            dtrowClr = dsSalesforce.Tables[0].Copy();
            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdReport.DataSource = dtrowClr;
                grdReport.DataBind();
            }
            else
            {
                grdReport.DataSource = dtrowClr;
                grdReport.DataBind();
            }
        }
    }
#endregion Report
    #region RowData
    protected void grdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            string bcolor = "#" + dtrowClr.Rows[indx][5].ToString();

            e.Row.BackColor = System.Drawing.Color.FromName(bcolor);
            Label lblname = (Label)e.Row.FindControl("lblName");
            Label lblmode = (Label)e.Row.FindControl("lblMode");
            Label lblEmpcode = (Label)e.Row.FindControl("lblSFid");
            Label lblNum = (Label)e.Row.FindControl("lblNumber");
            if (mode == lblmode.Text)
            {
               
                lblmode.Text = "";
            }
            else
            {
                mode = lblmode.Text;
            }

            if (name == lblname.Text)
            {
                lblname.Text = "";
                lblNum.Text = "";
            }
            else
            {
                name = lblname.Text;
            }
            if (code == lblEmpcode.Text)
            {
                lblEmpcode.Text = "";
            }
            else
            {
                code = lblEmpcode.Text;
            }
        }
    }
    #endregion
}
