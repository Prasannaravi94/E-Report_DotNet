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
public partial class MasterFiles_AnalysisReports_rpt_Missed_Call_New : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            TYear = Request.QueryString["To_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Missed Call Report From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            CreateDynamicTable();
        }
    }
    private void CreateDynamicTable()
    {
        SalesForce sf = new SalesForce();
        DCR dcr = new DCR();
        SalesForce ds = new SalesForce();
        DataSet dsFF = new DataSet();
        DataSet dsmgrsf = new DataSet();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsFF = sf.UserList_Self_Call_Vacant(div_code, sf_code);
        }
        else
        {
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsFF = dsmgrsf;
        }
        if (dsFF != null)
        {

            TourPlan tp = new TourPlan();
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header.ForeColor = System.Drawing.Color.White;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center><b>S.No</b></center>";
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 300;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Field Force Name</b></center>";
            tc_FF.Style.Add("border-color", "Black");
            tc_FF.Controls.Add(lit_FF);
            tc_FF.RowSpan = 2;
            tr_header.Cells.Add(tc_FF);

            TableCell tc_Designation = new TableCell();
            tc_Designation.BorderStyle = BorderStyle.Solid;
            tc_Designation.BorderWidth = 1;
            tc_Designation.Width = 100;
            Literal lit_Designation = new Literal();
            lit_Designation.Text = "<center><b>Designation</b></center>";
            tc_Designation.Style.Add("border-color", "Black");
            tc_Designation.Controls.Add(lit_Designation);
            tc_Designation.RowSpan = 2;
            tr_header.Cells.Add(tc_Designation);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.Solid;
            tc_HQ.BorderWidth = 1;
            tc_HQ.Width = 200;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<center><b>HQ</b></center>";
            tc_HQ.Style.Add("border-color", "Black");
            tc_HQ.Controls.Add(lit_HQ);
            tc_HQ.RowSpan = 2;
            tr_header.Cells.Add(tc_HQ);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            //    tbl.Rows.Add(tr_header);

            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                    tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    tc_month.Attributes.Add("Class", "tr_det_head");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.ColumnSpan = 3;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tbl.Rows.Add(tr_header);
            TableRow tr_lst_det = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Total Lst dr";

                    tc_lst_month.BorderStyle = BorderStyle.Solid;


                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;

                    tc_lst_month.Style.Add("font-family", "Calibri");
                    tc_lst_month.Style.Add("font-size", "10pt");
                    tc_lst_month.Style.Add("Color", "White");
                    tc_lst_month.Style.Add("border-color", "Black");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);




                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met";
                    tc_msd_month.BorderStyle = BorderStyle.Solid;

                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd_month.BorderWidth = 1;
                    tc_msd_month.Style.Add("Color", "White");
                    tc_msd_month.Style.Add("border-color", "Black");
                    tc_msd_month.Style.Add("font-family", "Calibri");
                    tc_msd_month.Style.Add("font-size", "10pt");
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_msd = new TableCell();
                    HyperLink lit_msd = new HyperLink();
                    lit_msd.Text = "Missed";
                    tc_msd.BorderStyle = BorderStyle.Solid;

                    tc_msd.HorizontalAlign = HorizontalAlign.Center;
                    //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd.BorderWidth = 1;
                    tc_msd.Style.Add("Color", "White");
                    tc_msd.Style.Add("border-color", "Black");
                    tc_msd.Style.Add("font-family", "Calibri");
                    tc_msd.Style.Add("font-size", "10pt");
                    tc_msd.Controls.Add(lit_msd);
                    tr_lst_det.Cells.Add(tc_msd);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    tbl.Rows.Add(tr_lst_det);
                }




            }
            int iCount = 0;
            string sTab = string.Empty;

            foreach (DataRow drFF in dsFF.Tables[0].Rows)
            {
                if (drFF["sf_code"].ToString() != "admin")
                {
                    TableRow tr_det = new TableRow();

                    if (drFF["sf_type"].ToString() == "2")
                    {
                        tr_det.Attributes.Add("style", "background-color:LightBlue; font-weight:Bold; ");
                    }
                    iCount += 1;
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);


                    TableCell tc_det_user = new TableCell();
                    Literal lit_det_user = new Literal();
                    lit_det_user.Text = "&nbsp;" + sTab + drFF["sf_name"].ToString();
                    tc_det_user.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_user.BorderStyle = BorderStyle.Solid;
                    tc_det_user.BorderWidth = 1;
                    tc_det_user.Controls.Add(lit_det_user);
                    tr_det.Cells.Add(tc_det_user);

                    TableCell tc_det_Designation = new TableCell();
                    Literal lit_det_Designation = new Literal();
                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "2")
                    {
                        lit_det_Designation.Text = "&nbsp;" + sTab + drFF["Designation_Short_Name"].ToString();
                    }
                    else
                    {
                        lit_det_Designation.Text = "&nbsp;" + sTab + drFF["sf_Designation_Short_Name"].ToString();
                    }
                    tc_det_Designation.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_Designation.BorderStyle = BorderStyle.Solid;
                    tc_det_Designation.BorderWidth = 1;
                    tc_det_Designation.Controls.Add(lit_det_Designation);
                    tr_det.Cells.Add(tc_det_Designation);

                    TableCell tc_det_HQ = new TableCell();
                    Literal lit_det_HQ = new Literal();
                    lit_det_HQ.Text = "&nbsp;" + sTab + drFF["sf_hq"].ToString();
                    tc_det_HQ.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_HQ.BorderStyle = BorderStyle.Solid;
                    tc_det_HQ.BorderWidth = 1;
                    tc_det_HQ.Controls.Add(lit_det_HQ);
                    tr_det.Cells.Add(tc_det_HQ);

                    int cmonthdoc = Convert.ToInt32(FMonth);
                    int cyeardoc = Convert.ToInt32(FYear);
                    DateTime dtCurrent;
                    if (cmonthdoc == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeardoc + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthdoc + 1) + "-01-" + cyeardoc;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);
                    DataSet dsdoc = new DataSet();
                    DataSet dsmet = new DataSet();
                    SalesForce dcrdoc = new SalesForce();
                    DCR dcr1 = new DCR();
                    string tot_fldwrkodc = "";
                    string tot_met = "";
                    int imissed_dr = 0;
                    if (months >= 0)
                    {

                        for (int j = 1; j <= months + 1; j++)
                        {

                            dsdoc = dcrdoc.MissedCallReport(div_code, drFF["sf_code"].ToString(), cmonthdoc, cyeardoc, dtCurrent);
                            if (dsdoc.Tables[0].Rows.Count > 0)
                                tot_fldwrkodc = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            TableCell tc_det_confirm = new TableCell();
                            Literal lit_det_confirm = new Literal();
                            lit_det_confirm.Text = "&nbsp;" + tot_fldwrkodc;
                            tc_det_confirm.BorderStyle = BorderStyle.Solid;
                            tc_det_confirm.BorderWidth = 1;
                            tc_det_confirm.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_confirm.Controls.Add(lit_det_confirm);
                            tr_det.Cells.Add(tc_det_confirm);


                            dsmet = dcr1.DCR_Doc_Met(drFF["sf_code"].ToString(), div_code, cmonthdoc, cyeardoc);
                            if (dsmet.Tables[0].Rows.Count > 0)
                                tot_met = dsmet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            //  itotWorkType += fldwrk_total + Convert.ToInt16(tot_met);

                            tr_det.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + dsmet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "center");

                            tr_det.Cells.Add(tc_det_FF);

                            int cmonthmiss = Convert.ToInt32(FMonth);
                            int cyearmiss = Convert.ToInt32(FYear);
                            TableCell tc_det_mis = new TableCell();
                            HyperLink lit_det_mis = new HyperLink();
                            if (tot_fldwrkodc != "0")
                            {
                                imissed_dr = Convert.ToInt16(tot_fldwrkodc) - Convert.ToInt16(dsmet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                lit_det_mis.Text = imissed_dr.ToString();
                                if (imissed_dr > 0)
                                    lit_det_mis.Attributes.Add("href", "javascript:showMissedDR('" + drFF["sf_code"].ToString() + "', '" + cmonthdoc.ToString() + "', '" + cyeardoc.ToString() + "', 1,'')");
                                lit_det_mis.Style.Add("text-decoration", "none");
                                lit_det_mis.Style.Add("color", "black");
                                lit_det_mis.Style.Add("cursor", "hand");
                                
                            }

                            else
                            {
                                lit_det_mis.Text = " - ";
                            }
                            tc_det_mis.BorderStyle = BorderStyle.Solid;
                            tc_det_mis.BorderWidth = 1;
                            tc_det_mis.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_mis.Controls.Add(lit_det_mis);
                            tr_det.Cells.Add(tc_det_mis);


                            cmonthdoc = cmonthdoc + 1;
                            if (cmonthdoc == 13)
                            {
                                cmonthdoc = 1;
                                cyeardoc = cyeardoc + 1;
                            }
                        }
                    }

                    tbl.Rows.Add(tr_det);
                }
            }

        }
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
}