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


public partial class MIS_Reports_rptsamplestatus_New_New_DCR : System.Web.UI.Page
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
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string sf_name = string.Empty;
    string strQry = string.Empty;


    DB_EReporting db_ER = new DB_EReporting();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        SF_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();


     
        //lblname.Text = sf_name;


        if (!Page.IsPostBack)
        {
            FillDespatch_status();

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblname.Text = sf_name;

            lblProd.Text = "Sample Status as on Date";

            
        }

    }
    private void FillDespatch_status()
    {

        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.sample_Des_New(div_code, SF_code, FMonth, FYear, TMonth, TYear);
        // strQry = "EXEC sample_Des_New_Modify_SingleFF '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + ", '" + "1" + "', '" + "0" + "', '" + "" + "' ";
        //strQry = "EXEC sample_Des_New_Modify_For_Dcr_Entry_Screen '" + div_code + "', '" + SF_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + ", '" + "1" + "', '" + "0" + "', '" + "" + "' ";
        strQry = "select Product_Code_SlNo,Product_Detail_Name,case when Sample_AsonDate<0 then 0 else Sample_AsonDate end as closing from Trans_Sample_Stock_FFWise_AsonDate a,Mas_Product_Detail b where sf_Code='" + SF_code+ "' and a.Prod_Detail_Sl_No=b.Product_Code_SlNo and Sample_AsonDate!<0 and Sample_AsonDate!=0";

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

    protected void grdDr_RowDataBoud(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblprod_name = (Label)e.Row.FindControl("lblprod_name");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            //Label lblopening = (Label)e.Row.FindControl("lblopening");
            //Label lblDes = (Label)e.Row.FindControl("lblDes");
            //Label lblissued = (Label)e.Row.FindControl("lblissued");
            Label lblclosing = (Label)e.Row.FindControl("lblclosing");


            if (lblprod_name.Text == "Total")
            {
                lblprod_name.Font.Bold = true;
                lblprod_name.ForeColor = System.Drawing.Color.Red;
                lblSNo.Text = "";
                //lblopening.Font.Bold = true;
                //lblDes.Font.Bold = true;
                //lblissued.Font.Bold = true;
                lblclosing.Font.Bold = true;
                lblprod_name.Attributes.Add("align", "right");

            }
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
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
   
}