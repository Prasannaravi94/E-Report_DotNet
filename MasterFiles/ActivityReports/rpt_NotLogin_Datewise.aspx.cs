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

public partial class MasterFiles_ActivityReports_rpt_NotLogin_Datewise : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    string Vacant = string.Empty;
    DataSet dsSalesforce = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["From"].ToString();
            FYear = Request.QueryString["To"].ToString();
            Vacant = Request.QueryString["strValue"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
          //  string strFrmMonth = sf.getMonthName(FMonth);
            //string strToMonth = sf.getMonthName(TMonth);
            lblHead.Text = "Not - Login Details" + " (Duration Between " + FMonth + " and " + FYear + " )";
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }
    private void FillReport()
    {
        DCR dc = new DCR();

        if (Vacant == "1")
        {
            dsSalesforce = dc.Login_Datewise(sf_code, div_code, FMonth, FYear);
            //dsSalesforce.Tables[0].DefaultView.RowFilter = "csv <> '" + DateTime.Now.ToString() + "' ";

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }


        }
        else
        {
            dsSalesforce = dc.Login_Datewise_Vacant(sf_code, div_code, FMonth, FYear);
            //dsSalesforce.Tables[0].DefaultView.RowFilter = "csv <> '" + DateTime.Now.ToString() + "' ";

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesforce;
                grdSalesForce.DataBind();
            }
        }

    }
    protected void RowDataBound_grdSalesForce(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desig_color")));
            //} 
            Label lbldays = (Label)e.Row.FindControl("lbldays");
             lbldays.Attributes.Add("style", "Color:Red; font-size:14px; font-weight:bold");
            if (lbldays.Text == "350")
            {
                lbldays.Text = "";
            } 
            if (lbldays.Text.Contains("-"))
            {
                DateTime dold = Convert.ToDateTime(FMonth);
                DateTime dnew = Convert.ToDateTime(FYear);
                TimeSpan daydif = (dnew - dold);
                double dayd = (daydif.TotalDays) + 1;
                lbldays.Text = dayd.ToString();
                e.Row.Attributes.Add("style", "background-color:Yellow");
            }

        }

    }
}