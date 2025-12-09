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

public partial class MasterFiles_rptLstDr_Search : System.Web.UI.Page
{
    DataSet dsFF = null;
    string sf_code = string.Empty;
    string catg = string.Empty;
    string cat_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string HQ = string.Empty;
    string Desig = string.Empty;
    string sDRCatg_Count = string.Empty;
    string Dr_Code = string.Empty;
    string div_code = string.Empty;
    int iType = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        div_code = Request.QueryString["Div_code"].ToString();
        Dr_Code = Request.QueryString["Dr_Code"].ToString();
        HQ = Request.QueryString["HQ"].ToString();
        Desig = Request.QueryString["Desig"].ToString();

        if (!Page.IsPostBack)
        {
            if (sf_code != "0")
            {
                lblFFName.Text = "Field Force Name   : " + sf_name;
                lblHQ1.Text = "HQ : " + HQ;
                lblDesignation1.Text = "Desingnation : " + Desig;
            }
        }

        Exportbutton();
    }

    private void Exportbutton()
    {
        ListedDR LstDoc = new ListedDR();
        DataSet dsDesignation = new DataSet();
        dsDesignation = LstDoc.Bind_ViewdDr(div_code, Dr_Code, sf_code);
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDesignation;
            grdDoctor.DataBind();

            btnExcel.Visible = true;
            btnPDF.Visible = false;
            lblPdf.Visible = false;
            btnPrint.Visible = true;
            btnClose.Visible = true;
        }
        else
        {
            grdDoctor.DataSource = dsDesignation;
            grdDoctor.DataBind();
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
        string attachment = "attachment; filename=MRStatus.xls";
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
        string strFileName = "rptMRStatusView";
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