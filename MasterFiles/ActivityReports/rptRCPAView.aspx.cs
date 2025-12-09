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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_rptRCPAView : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_Code = string.Empty;
    string sf_Code_Name = string.Empty;
    string sf_type = string.Empty;
    string selValues = string.Empty;
    DataSet dsProd = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_Code = Request.QueryString["sf_code"].ToString();
        selValues = Request.QueryString["selValues"].ToString();
        sf_Code_Name = Request.QueryString["sf_Code_Name"].ToString();
        lblFieldForce.Text = "FieldForce Name: " + sf_Code_Name;
        FillRCPAView();
    }

    protected void FillRCPAView()
    {
        Product pro = new Product();
        DataTable dt = new DataTable();

        dsProd = pro.getRCPAView(selValues, sf_Code);
        dt = dsProd.Tables[0];
        DataTable nDt = dt;

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["OurQty"] = dt.Rows[i]["OurQty"].ToString().Trim(new Char[] { '(', ')' });
            }

            for (int j = dt.Rows.Count - 2; j >= 0; j--)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dt.Rows[i];

                    if (r["CmptrName"].ToString().Trim() == "0" && r["CmptrBrnd"].ToString().Trim() == "0"
                        && r["CmptrQty"].ToString().Trim() == "0" && r["CmptrPriz"].ToString().Trim() == "0")
                    {
                        dt.Rows.Remove(r);
                    }
                }
            }
        }
        gvRCPAList.DataSource = nDt;
        gvRCPAList.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control txt_salutaion)
    {
        /* Verifies that the control is rendered */
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string Export = this.Page.Title;
        string attachment = "attachment; filename=" + Export + ".xls";
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
        string strFileName = "rptRCPAiew";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void gvRCPAList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int rowIndex = gvRCPAList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvRCPAList.Rows[rowIndex];
            GridViewRow previousRow = gvRCPAList.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (i == 0)
                {

                    if ((row.Cells[i].FindControl("lblDRName") as Label).Text == (previousRow.Cells[i].FindControl("lblDRName") as Label).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
                if (i == 1)
                {
                    if ((row.Cells[i].FindControl("lblOurProduct") as Label).Text == (previousRow.Cells[i].FindControl("lblOurProduct") as Label).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
                if (i == 2)
                {
                    if ((row.Cells[i].FindControl("lblOurQty") as Label).Text == (previousRow.Cells[i].FindControl("lblOurQty") as Label).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
                if (i == 3)
                {
                    if ((row.Cells[i].FindControl("lblOurValue") as Label).Text == (previousRow.Cells[i].FindControl("lblOurValue") as Label).Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }
}