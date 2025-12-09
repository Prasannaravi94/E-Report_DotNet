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

public partial class MIS_Reports_rptsamplestatus_New : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        SF_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();

        if (!Page.IsPostBack)
        {
            FillDespatch_status();
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblRegionName.Text = sf_name;

            lblHead.Text = "Sample Despatch Status of  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        }


   
    }
    

    private void FillDespatch_status()
    {
  
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sample_Status_New(div_code, SF_code, FMonth, FYear,TMonth,TYear);
       // dtrowClr = dsSalesForce.Tables[0].Copy();
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string FileName = this.Page.Title;
        string attachment = "attachment; filename=" + FileName + ".xls";
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
        string strFileName = this.Page.Title;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
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

    protected void grdDespatch_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "clr")));
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
      
                Label lblSfCode = (Label)e.Row.FindControl("lblSfCode");
                LinkButton linksf_name =(LinkButton)e.Row.FindControl("linksf_name");

                Label lblopening = (Label)e.Row.FindControl("lblopening");
                Label lblsamqty = (Label)e.Row.FindControl("lblsamqty");
                Label lblissued = (Label)e.Row.FindControl("lblissued");
                Label lblclosing = (Label)e.Row.FindControl("lblclosing");

                linksf_name.Attributes.Add("style", "text-decoration:none;color:black;");

                linksf_name.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                linksf_name.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";

                if ((lblissued.Text != "") && (lblopening.Text != "") && (lblsamqty.Text !=""))
                {
                    int open1 = Convert.ToInt32(lblopening.Text);
                    int samp1 = Convert.ToInt32(lblsamqty.Text);
                    if ((Convert.ToInt32(lblissued.Text)) > (open1+samp1))
                    {
                     
                        int issue1 = open1 + samp1;

                        lblissued.Text = issue1.ToString();
                        lblclosing.Text = "";
                    }
                    else
                    {
                        int opening = Convert.ToInt32(lblopening.Text);
                        int sampldes = Convert.ToInt32(lblsamqty.Text);
                        int issued = Convert.ToInt32(lblissued.Text);

                        int clos = opening + sampldes - issued;

                        lblclosing.Text = clos.ToString();
                    }
                }

                else if (lblopening.Text != "" && lblsamqty.Text != "" && lblissued.Text == "")
                {
                    int ope = Convert.ToInt32(lblopening.Text);
                    int samm = Convert.ToInt32(lblsamqty.Text);
                    int clg = ope + samm;

                    lblclosing.Text = clg.ToString();
                }
                else if (lblopening.Text != "" && lblsamqty.Text == "" && lblissued.Text != "")
                {
                    int opening2 = Convert.ToInt32(lblopening.Text);
                   // int sampldes2 = Convert.ToInt32(lblsamqty.Text);
                    int issued2 = Convert.ToInt32(lblissued.Text);

                    if (issued2 > opening2)
                    {
                        lblclosing.Text = "";
                        lblissued.Text = lblopening.Text;
                    }
                    else
                    {
                        int clos2 = (opening2 - issued2);

                        lblclosing.Text = clos2.ToString();
                    }

                }
                else if (lblopening.Text == "" && lblsamqty.Text != "" && lblissued.Text != "")
                {
                   // int opening3 = Convert.ToInt32(lblopening.Text);
                    int sampldes3 = Convert.ToInt32(lblsamqty.Text);
                    int issued3 = Convert.ToInt32(lblissued.Text);

                    if (issued3 > sampldes3)
                    {
                        lblclosing.Text = "";
                        lblissued.Text = lblsamqty.Text;
                    }
                    else
                    {
                        int close3 = (sampldes3 - issued3);

                        lblclosing.Text = close3.ToString();
                    }
                }
                else if (lblopening.Text != "" && lblsamqty.Text == "" && lblissued.Text == "")
                {
                    lblclosing.Text = lblopening.Text;
                }


                else if (lblopening.Text == "" && lblsamqty.Text != "" && lblissued.Text == "")
                {
                    lblclosing.Text = lblsamqty.Text;
                }

                //else if (lblopening.Text == "" && lblsamqty.Text != "" && lblissued.Text != "")
                //{
                //    // int opening4 = Convert.ToInt32(lblopening.Text);
                //    int sampldes4 = Convert.ToInt32(lblsamqty.Text);
                //    int issued4 = Convert.ToInt32(lblissued.Text);
                    
               

                //}


                if (lblopening.Text == "" && lblsamqty.Text == "")
                {
                    lblissued.Text = "";
                }

                else if (lblopening.Text == "0" && lblsamqty.Text == "")
                {
                    lblissued.Text = "";
                }
                else if (lblopening.Text == "" && lblsamqty.Text == "0")
                {
                    lblissued.Text = "";
                }
                else if (lblopening.Text == "0" && lblsamqty.Text == "0")
                {
                    lblissued.Text = "";
                }

                

                linksf_name.Attributes.Add("href", "javascript:showModalPopUp('" + lblSfCode.Text + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + linksf_name.Text + "')");

        }
    }


}