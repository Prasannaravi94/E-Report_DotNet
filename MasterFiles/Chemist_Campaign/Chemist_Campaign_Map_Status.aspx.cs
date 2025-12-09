using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Chemist_Campaign_Chemist_Campaign_Map_Status : System.Web.UI.Page
{
    DataSet dsChemist = null;
    string sf_code = string.Empty;
    string strFieledForceName = string.Empty;
    string hq = string.Empty;
    string MR = string.Empty;
    string desig = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();

        if (!Page.IsPostBack)
        {
            sf_code = Request.QueryString["sfcode"].ToString();

            strFieledForceName = Request.QueryString["sf_name"].ToString();
            hq = Request.QueryString["sf_hq"].ToString();
            desig = Request.QueryString["sf_short"].ToString();
            MR = Request.QueryString["MR"].ToString();

        }
        //Doctor dc = new Doctor();
        ChemistCampaign dc = new ChemistCampaign();
        dsChemist = dc.CheCampaignMap(sf_code);
        //DataTable prod = dc.CheCampaignMap_Details(sf_code);
        DataTable mainTable = dsChemist.Tables[0];

        //mainTable.Columns.Add("Campaign");

        if (mainTable.Rows.Count > 0)
        {
            //foreach (DataRow row in mainTable.Rows)
            //{
            //    String filter = "Chemists_Code='" + row["Chemists_Code"].ToString() + "'";
            //    DataRow[] rows = prod.Select(filter);

            //    if (rows.Count() > 0)
            //    {
            //        row["Campaign"] = rows[0]["Campaign"];
            //    }

            //}

            if (MR == "1")
            {
                DataView dv = mainTable.DefaultView;
                dv.Sort = "Territory_Name";
                mainTable = dv.ToTable();

                grdChemist.Visible = true;
                grdChemist.DataSource = mainTable;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.Visible = true;
                grdChemist.DataSource = mainTable;
                grdChemist.DataBind();
            }


        }
        else
        {
            grdChemist.DataSource = dsChemist;
            grdChemist.DataBind();
        }
        LblForceName.Text = "Field Force Name : " + strFieledForceName;

    }




    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblDOB = (Label)e.Row.FindControl("lblDOB");


        //    HyperLink sts = new HyperLink();
        //    if (lblDOB.Text == "01/Jan/1900")
        //    {
        //        lblDOB.Text = "";
        //    }

        //}

    }
    private void FillSalesForce()
    {

        ChemistCampaign dr = new ChemistCampaign();
        if (dsChemist.Tables[0].Rows.Count > 0)
        {
            ViewState["dsChemist"] = dsChemist;
        }

    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["ctrl"] = pnlContents;
            Control ctrl = (Control)Session["ctrl"];
            PrintWebControl(ctrl);
        }
        catch (Exception ex)
        {

        }
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

    private void Exportbutton()
    {
        btnExcel.Visible = true;
       // btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string Export = Title;
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
        catch (Exception ex)
        {

        }
    }
    //protected void btnPDF_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strFileName = Title;
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);
    //        HtmlForm frm = new HtmlForm();
    //        pnlContents.Parent.Controls.Add(frm);
    //        frm.Attributes["runat"] = "server";
    //        frm.Controls.Add(pnlContents);
    //        frm.RenderControl(hw);
    //        StringReader sr = new StringReader(sw.ToString());
    //        Document pdfChe = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
    //        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfChe);
    //        PdfWriter.GetInstance(pdfChe, Response.OutputStream);
    //        pdfChe.Open();
    //        htmlparser.Parse(sr);
    //        pdfChe.Close();
    //        Response.Write(pdfChe);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
}