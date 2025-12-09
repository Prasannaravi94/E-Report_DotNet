using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_frmProductMinus_Dls : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string iMonth = string.Empty;
    string iYear = string.Empty;
    string div_code = string.Empty;
    string Sf_Name = string.Empty;
    string Sf_hq = string.Empty;
    string Sf_Desig = string.Empty;
    double PrdTotal = 0;
    double DrsTotal = 0;
    string Emp_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();
        iMonth = Request.QueryString["Mon"].ToString();
        iYear = Request.QueryString["Year"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        Sf_hq = Request.QueryString["Sf_HQ"].ToString();
        Sf_Desig = Request.QueryString["sf_Designation_Short_Name"].ToString();
        Emp_Code = Request.QueryString["sf_emp_id"].ToString();

        lblName.Text = "Product Wise Details for " + getMonthName(Convert.ToInt16(iMonth)) + " " + iYear;

        lblFieldForce.Text = " FieldForce Name: " + Sf_Name + " HQ: " + Sf_hq + " Designation: " + Sf_Desig;

        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        ds = dcr.Get_Product_Minuts_Detail(sf_code, iMonth, iYear, div_code);

        GvDcrCount.DataSource = ds;
        GvDcrCount.DataBind();
    }

    protected void OnRowDataBound_GvDcrCount(object sender, GridViewRowEventArgs e)
    {
        double Avg = new double();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSf_Hq = (Label)e.Row.FindControl("lblSf_Hq");
            if (lblSf_Hq.Text != "")
            {
                PrdTotal += Convert.ToDouble(lblSf_Hq.Text);
            }
            Label lblReporting = (Label)e.Row.FindControl("lblReporting");
            if (lblReporting.Text != "")
            {
                DrsTotal += Convert.ToDouble(lblReporting.Text);
            }
            Label lblAverage = (Label)e.Row.FindControl("lblAverage");
           
            if (Convert.ToInt16(lblSf_Hq.Text) > 60)
            {
                Avg = Convert.ToDouble(lblSf_Hq.Text) / 60 ;
                
            }
            else
            {
                Avg = Convert.ToDouble(lblSf_Hq.Text);
               
            }
            lblAverage.Text = Avg.ToString().Substring(0, 3);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblPrdCount = (Label)e.Row.FindControl("lblPrdCount");
            if (PrdTotal != 0)
            {
                lblPrdCount.Text = PrdTotal.ToString();
            }

            Label lblTotalDr = (Label)e.Row.FindControl("lblTotalDr");
            if (DrsTotal != 0)
            {
                lblTotalDr.Text = DrsTotal.ToString();
            }
        }
    }

   private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }

   
}