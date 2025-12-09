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
using Bus_EReport;
public partial class MasterFiles_ActivityReports_rpt_Login_Details : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    DataSet dsSalesforce = null;
    string Vacant = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
            Vacant = Request.QueryString["strValue"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Login Details For the Month Of - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }
    private void FillReport()
    {
        DCR dc = new DCR();
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        if (Vacant == "1")
        {
            dsSalesforce = dc.Login_Det(sf_code, div_code, cmonth, cyear);
            //dsSalesforce.Tables[0].DefaultView.RowFilter = "csv <> '" + DateTime.Now.ToString() + "' ";

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
        }
        else
        {
            dsSalesforce = dc.Login_Det_Vacant(sf_code, div_code, cmonth, cyear);
            //dsSalesforce.Tables[0].DefaultView.RowFilter = "csv <> '" + DateTime.Now.ToString() + "' ";

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
        }
    }
    protected void RowDataBound_grdSalesForce(object sender, GridViewRowEventArgs e)
    {
      //Label lblcount = (Label)e.Row.FindControl("lblcount");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desig_color")));
            //if (lblcount.Text.Contains("-"))
            //{
            //    lblcount.Text = lblcount.Text.Remove(0, 1);
            //}
        }
       
    }
}