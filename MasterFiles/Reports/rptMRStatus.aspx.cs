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

public partial class Reports_rptMRStatus : System.Web.UI.Page
{
    DataSet dsFF = null;
    string sf_code = string.Empty;
    string catg = string.Empty;
    string cat_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sDRCatg_Count = string.Empty;
    string strSF_code = string.Empty;
    string div_code = string.Empty;
    int iType = -1;
    string sf_type = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        string sf_type = Session["Sf_code"].ToString();

        if (Request.QueryString["div_code"] != null)
        {
            div_code = Request.QueryString["div_code"].ToString();
        }

        if (Session["StrDiv_Code"] != null)
        {
            div_code = Session["StrDiv_Code"].ToString();
        }
        type = Request.QueryString["type"].ToString();

        iType = Convert.ToInt32( Request.QueryString["status"].ToString());
        if (Session["Sf_Code_multiple"] != null)
        {
            strSF_code = Session["Sf_Code_multiple"].ToString();
        }
        if (!Page.IsPostBack)
        {
            if (sf_code != "0")
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " )";
            }
        }

        

        SalesForce sf = new SalesForce();

        if (type == "1")
        {
            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Territory Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Territory Details";
            }
            Territory terr = new Territory();
            if (sf_code != "0")
            {
                //dsFF = sf.getTerritory_Rep(sf_code, iType);
                
                dsFF = terr.getTerritory(sf_code);
            }
            else
            {
                //dsFF = sf.getTerritory_Rep_Total(div_code, iType, strSF_code);
                dsFF = terr.getTerritory_Total(strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsFF;
                grdTerritory.DataBind();
            }

            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "2")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Listed Doctor Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Listed Doctor Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getDoctor_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getDoctor_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsFF;
                grdDoctor.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "3")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active UnListed Doctor Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active UnListed Doctor Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getNonDoctor_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getNonDoctor_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdNonDR.Visible = true;
                grdNonDR.DataSource = dsFF;
                grdNonDR.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "4")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Chemists Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Chemists Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getChemists_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getChemists_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdChem.Visible = true;
                grdChem.DataSource = dsFF;
                grdChem.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "5")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Stockist Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Stockist Details";
            }

            strSF_code = Session["Sf_Code_StrStockist"].ToString();

            if (sf_code != "0")
            {
                dsFF = sf.getStockiest_Rep(sf_code, iType);
            }
            else
            {
                if (sf_type.Contains("MR"))
                {
                    dsFF = sf.getStockiest_Rep_Total(div_code, iType, sf_type);
                }
               
                else
                {
                    dsFF = sf.getStockiest_Rep_Total(div_code, iType, strSF_code);
                }
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdStok.Visible = true;
                grdStok.DataSource = dsFF;
                grdStok.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
           
        }
        Exportbutton();
    }

    private void Exportbutton()
    {
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }
    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[2].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[18].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
            if (iType == 0)
            {
                e.Row.Cells[1].Text = "Listed Doctor Created Date";
            }
            else
            {
                e.Row.Cells[1].Text = "Listed Doctor DeActivate Date";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            Label lblProd = (Label)e.Row.FindControl("lblProd");
            Label lblBrd1 = (Label)e.Row.FindControl("lblBrd1");
            Label lblBrd2 = (Label)e.Row.FindControl("lblBrd2");
            Label lblBrd3 = (Label)e.Row.FindControl("lblBrd3");
            Label lblBrd4 = (Label)e.Row.FindControl("lblBrd4");
            string[] strProductSplit = lblProd.Text.Split('/');
            int Priority = 1;
            foreach (string strprod in strProductSplit)
            {
                if (strprod != "")
                {
                    if (Priority == 1)
                    {
                        lblBrd1.Text = strprod;
                    }
                    else if (Priority == 2)
                    {
                        lblBrd2.Text = strprod;
                    }
                    else if (Priority == 3)
                    {
                        lblBrd3.Text = strprod;
                    }
                    else if (Priority == 4)
                    {
                        lblBrd4.Text = strprod;
                    }
                    Priority++;
                }

            }

          

        }
    }
    protected void grdNonDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[12].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }

            if (iType == 0)
            {
                e.Row.Cells[1].Text = "UnListed Doctor Created Date";
            }
            else
            {
                e.Row.Cells[1].Text = "UnListed Doctor DeActivate Date";
            }
        }
    }
    protected void grdChem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[13].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
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

        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MRStatus.xls"));
        //Response.ContentType = "application/ms-excel";
        ////DataTable dt = BindDatatable();
        //string str = string.Empty;

        //DataTable dt = dsFF.Tables[0];
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName);
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]));
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}
        //Response.End();

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