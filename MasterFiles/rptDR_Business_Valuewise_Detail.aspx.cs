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
using System.Text;
using Bus_EReport;
using System.Net;
using DBase_EReport;

public partial class MIS_Reports_rptDR_Business_Valuewise_Detail : System.Web.UI.Page
{
    int cmonth = -1;
    int cyear = -1;
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    string sPending = string.Empty;
    string dcrdays = string.Empty;
    string SF_code = string.Empty;
    int tot_days = -1;
    string div_code = string.Empty;
    DateTime ldcrdate;
    int count = 0;
    DataTable dtrowClr = null;
    string Month = string.Empty;
    string Year = string.Empty;
    string strQry = string.Empty;



    DB_EReporting db_ER = new DB_EReporting();

    DataSet dsSF = null;


    protected void Page_Load(object sender, EventArgs e)
    {

      
        SF_code = Request.QueryString["sf_code"].ToString();
        Month = Request.QueryString["month"].ToString();
        Year = Request.QueryString["year"].ToString();
 
        //lblname.Text = sf_name;


        if (!Page.IsPostBack)
        {
            FillDespatch_status();

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
           

            //lblname.Text = sf_name;

            lblProd.Text = "Listed Dr-Business ValueWise for the Month of  " + strFMonthName + " " + Year ;
           
        }

    }
    private void FillDespatch_status()
    {

       
        strQry = "EXEC get_BusinessEntry_Valuewise_Detailed '" + SF_code + "', '" + Month + "', " + Year + " ";
        dsSalesForce = db_ER.Exec_DataSet(strQry);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            grdDespatch.DataSource = dsSalesForce;
            grdDespatch.DataBind();
        }
        else
        {
            grdDespatch.DataSource = dsSalesForce;
            grdDespatch.DataBind();
        }
    }



 


  
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }
   

    protected void grdDespatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProduct_Name = (Label)e.Row.FindControl("lblProduct_Name");

            Label lblValue = (Label)e.Row.FindControl("lblValue");
          


            if (lblProduct_Name.Text == "Total")
            {
                lblProduct_Name.Font.Bold = true;
                lblProduct_Name.ForeColor = System.Drawing.Color.Red;
               
                lblProduct_Name.Attributes.Add("align", "right");
                lblValue.Font.Bold = true;
                lblValue.ForeColor = System.Drawing.Color.Red;

                lblValue.Attributes.Add("align", "right");

            }
        }
    }
}