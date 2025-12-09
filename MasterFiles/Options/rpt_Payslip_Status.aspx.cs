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
public partial class MasterFiles_ActivityReports_rpt_Payslip_Status : System.Web.UI.Page
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
           
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Payslip Status For the Month Of - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }
    private void FillReport()
    {
        DCR dc = new DCR();
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
       
            dsSalesforce = dc.Payslip_Status(div_code, sf_code, FMonth, FYear);
            //dsSalesforce.Tables[0].DefaultView.RowFilter = "csv <> '" + DateTime.Now.ToString() + "' ";

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
       
    }
    protected void RowDataBound_grdSalesForce(object sender, GridViewRowEventArgs e)
    {
      //Label lblcount = (Label)e.Row.FindControl("lblcount");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblpay = (Label)e.Row.FindControl("lblpay");
            //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desig_color")));
            //if (lblcount.Text.Contains("-"))
            //{
            //    lblcount.Text = lblcount.Text.Remove(0, 1);
            //}
            if (lblpay.Text == "Yes")
            {
                lblpay.Text = "✔";
                lblpay.ForeColor = System.Drawing.Color.Green;
                lblpay.Style.Add("font-size", "14pt");
                lblpay.Style.Add("font-weight", "Bold");

            }
            else if (lblpay.Text == "No")
            {
                lblpay.Text = "✕";
                lblpay.ForeColor = System.Drawing.Color.Red;
                lblpay.Style.Add("font-size", "14pt");
                lblpay.Style.Add("font-weight", "Bold");
            }
        }
       
    }
}