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

public partial class MIS_Reports_sampledesDatewiseZoom : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    
    string FDate = string.Empty;
    string TDate = string.Empty;
    int months;
    int cmonth;
    int cyear;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
        
        FDate = Request.QueryString["FromDate"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
        
        Prod_Name = Request.QueryString["Prod_Name"];
        Prod = Request.QueryString["Product_Code_SlNo"];
        sf_name = Request.QueryString["sf_name"];
        sCurrentDate = Request.QueryString["sCurrentDate"];
        //MultiProd_Code = Session["MultiProd_Code"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        
        sf_name = sf_name.TrimStart(',');


        if (!Page.IsPostBack)
        {
            //FillProd();
            lblProd.Text = "Sample Despatch View for the Period of " + FDate + " " + TDate + " ";
            lblProd.Font.Bold = true;
            lblname.Text = sf_name;
            CreateDynamicTable();
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
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    private void CreateDynamicTable()
    {
       
        int tot = 0;
        Doctor dr = new Doctor();
        
        FDate = Request.QueryString["FromDate"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
        string dateFormat = "M/d/yyyy";
        DateTime parsedFDate = DateTime.ParseExact(FDate, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        DateTime parsedTDate = DateTime.ParseExact(TDate, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
       
        int Fday = parsedFDate.Day;
        int FFMonth = parsedFDate.Month;
        int FFYear = parsedFDate.Year;
        int Tday = parsedTDate.Day;
        int TTMonth = parsedTDate.Month;
        int TTYear = parsedTDate.Year;
        string FromDate = FFMonth + "/" + Fday + "/" + FFYear;
        string ToDate = TTMonth + "/" + Tday + "/" + TTYear;
        dsDr = dr.getdespatch_productname_Datewise(div_code, sf_code, FromDate, ToDate);


        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_det_head = new TableRow();
            tr_det_head.BorderStyle = BorderStyle.Solid;
            tr_det_head.BorderWidth = 1;
            tr_det_head.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_det_head.Style.Add("Color", "#636d73");
            tr_det_head.Style.Add("border-bottom", "10px solid #fff");
            tr_det_head.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "#";
            tc_det_head_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_det_head_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_det_head_SNo.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            tc_det_head_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_det_head.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            tc_det_head_doc.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Fieldforce Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);

          

            TableCell tc_det_head_Qua = new TableCell();
            tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua.BorderWidth = 1;
            tc_det_head_Qua.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_qua = new Literal();
            lit_det_head_qua.Text = "<b>Product Name</b>";
            tc_det_head_Qua.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua.Controls.Add(lit_det_head_qua);
            tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua);

            TableCell tc_det_head_Territory = new TableCell();
            tc_det_head_Territory.BorderStyle = BorderStyle.Solid;
            tc_det_head_Territory.BorderWidth = 1;
            tc_det_head_Territory.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_Territory = new Literal();
            lit_det_head_Territory.Text = "<b>Despatch Quantity</b>";
            tc_det_head_Territory.Attributes.Add("Class", "tblHead");
            tc_det_head_Territory.Controls.Add(lit_det_head_Territory);
            tc_det_head_Territory.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Territory);



            tbl.Rows.Add(tr_det_head);

            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

              

                TableCell tc_det_dr_name = new TableCell();
                Literal lit_det_dr_name = new Literal();
                lit_det_dr_name.Text = drdoctor["FeildforceName"].ToString();
                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                tc_det_dr_name.BorderWidth = 1;
                tc_det_dr_name.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                tr_det_sno.Cells.Add(tc_det_dr_name);

               
                TableCell tc_det_dr_qua = new TableCell();
                Literal lit_det_dr_qua = new Literal();
                lit_det_dr_qua.Text = drdoctor["Product_Code"].ToString();
                tc_det_dr_qua.Attributes.Add("Class", "tblRow");
                tc_det_dr_qua.BorderStyle = BorderStyle.Solid;
                tc_det_dr_qua.BorderWidth = 1;
                tc_det_dr_qua.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dr_qua.Controls.Add(lit_det_dr_qua);
                tr_det_sno.Cells.Add(tc_det_dr_qua);

              
                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();
                hyp_lst_month.Text = drdoctor["Despatch_qty"].ToString();
                tc_lst_month.Attributes.Add("Class", "tblRow");
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_lst_month.Controls.Add(hyp_lst_month);
                tr_det_sno.Cells.Add(tc_lst_month);

                tbl.Rows.Add(tr_det_sno);
            }

        }


    }
}
