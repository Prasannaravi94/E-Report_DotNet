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
public partial class MasterFiles_Report_rpt_TP_Status : System.Web.UI.Page
{
    string state_code = string.Empty;

    string iMonth = string.Empty;
    string iYear = string.Empty;
    string sf_type = string.Empty;
    string isVacant;
    string Option = string.Empty;
    string div_code = string.Empty;
    string sf_name = string.Empty;
    string sf_code = string.Empty;
    DataSet dsFF = null;
    DataSet dsState = null;
    DataSet dstpstatus = null;
    string entry_date;
    string confirm_date;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        div_code = Session["div_code"].ToString();
        state_code =Request.QueryString["state_code"].ToString();
        iMonth = Request.QueryString["cur_month"].ToString();
        iYear = Request.QueryString["cur_year"].ToString();
        isVacant = Request.QueryString["strValue"].ToString();
        Option = Request.QueryString["Option"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        sf_type = Session["sf_type"].ToString();
        FillSalesForce();

        string strMonth = getMonthName(Convert.ToInt32(iMonth));

        lblHead.Text = "TP - Status for the Month of " + strMonth + " " + iYear;

        lblFieldForce.Text = "Field Force Name : " + sf_name;
        lblMonth.Text = " Month : " + strMonth.Substring(0, 3);
        lblYear.Text = " Year : " + iYear;

    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        if (Option == "0" || sf_type == "2" || sf_type == "1") //included sf_type 1 //sujee
        {
            if (isVacant == "1")
            {
                dsFF = sf.get_tp_status_newvacant(sf_code, div_code, iMonth, iYear);
            }
            else
            {
                dsFF = sf.get_tp_status_new(sf_code, div_code, iMonth, iYear);
            }
        }
        else if (Option == "1")
        {
            if (isVacant == "1")
            {
                dsFF = sf.get_tp_status_state_vacant(div_code, iMonth, iYear, state_code);
            }
            else
            {
                dsFF = sf.get_tp_status_state(div_code, iMonth, iYear, state_code);
            }
           
          
        }

        if(dsFF.Tables[0].Rows.Count > 0)
        {
            grdtpstatus.DataSource = dsFF;
            grdtpstatus.DataBind();
        }
       
    }
  protected void grdtpstatus_row_databound(object sender, GridViewRowEventArgs e)  
    {  
        
        if(e.Row.RowType==DataControlRowType.DataRow)  
        {
            Label lbl = (Label)e.Row.FindControl("lbldesig");
           

            e.Row.Attributes.Add("style", "background-Color:" + "#" + Convert.ToString(lbl.Text));
           

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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

}