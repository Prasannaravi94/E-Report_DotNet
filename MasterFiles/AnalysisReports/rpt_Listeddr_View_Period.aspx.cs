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

public partial class MasterFiles_AnalysisReports_rpt_Listeddr_View_Period : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    string SfNameM = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
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
        lblHead.Text = "Listed Doctor Visit - Periodically " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        CreateDynamicTable();
         //}

    }
    private void CreateDynamicTable()
    {
           ListedDR dc = new ListedDR();
           dsDoctor = dc.getListedDr_new_Cnt(sf_code, div_code);

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>S.No</center>";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 80;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Listed Doctor Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("font-size", "10pt");
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);

            TableCell tc_DR_Terr = new TableCell();
            tc_DR_Terr.BorderStyle = BorderStyle.Solid;
            tc_DR_Terr.BorderWidth = 1;
            tc_DR_Terr.Width = 180;
            tc_DR_Terr.RowSpan = 2;
            Literal lit_DR_Terr = new Literal();
            lit_DR_Terr.Text = "<center>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</center>";
            tc_DR_Terr.BorderColor = System.Drawing.Color.Black;
            tc_DR_Terr.Style.Add("font-family", "Calibri");
            tc_DR_Terr.Style.Add("font-size", "10pt");
            tc_DR_Terr.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Terr.Controls.Add(lit_DR_Terr);
            tr_header.Cells.Add(tc_DR_Terr);


            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 120;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>Qualification</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Style.Add("font-family", "Calibri");
            tc_DR_HQ.Style.Add("font-size", "10pt");
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 90;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Category</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Style.Add("font-family", "Calibri");
            tc_DR_Des.Style.Add("font-size", "10pt");
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_DR_Spe = new TableCell();
            tc_DR_Spe.BorderStyle = BorderStyle.Solid;
            tc_DR_Spe.BorderWidth = 1;
            tc_DR_Spe.Width = 90;
            tc_DR_Spe.RowSpan = 2;
            Literal lit_DR_Spe = new Literal();
            lit_DR_Spe.Text = "<center>Specialty</center>";
            tc_DR_Spe.BorderColor = System.Drawing.Color.Black;
            tc_DR_Spe.Style.Add("font-family", "Calibri");
            tc_DR_Spe.Style.Add("font-size", "10pt");
            tc_DR_Spe.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Spe.Controls.Add(lit_DR_Spe);
            tr_header.Cells.Add(tc_DR_Spe);

            TableCell tc_DR_Class = new TableCell();
            tc_DR_Class.BorderStyle = BorderStyle.Solid;
            tc_DR_Class.BorderWidth = 1;
            tc_DR_Class.Width = 80;
            tc_DR_Class.RowSpan = 2;
            Literal lit_DR_Class = new Literal();
            lit_DR_Class.Text = "<center>Class</center>";
            tc_DR_Class.BorderColor = System.Drawing.Color.Black;
            tc_DR_Class.Style.Add("font-family", "Calibri");
            tc_DR_Class.Style.Add("font-size", "10pt");
            tc_DR_Class.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Class.Controls.Add(lit_DR_Class);
            tr_header.Cells.Add(tc_DR_Class);


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
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                    tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    tc_month.Attributes.Add("Class", "tr_det_head");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.ColumnSpan = 2;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                   // tc_month.Width = 300;
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
                    lit_lst_month.Text = "Count";

                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                
                    //  tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
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
                    lit_msd_month.Text = "Date";
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
            foreach (DataRow drFF in dsDoctor.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.Style.Add("text-align", "left");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                //tc_det_usr.Style.Add("font-family", "Calibri");
                //tc_det_usr.Style.Add("font-size", "10pt");
                tc_det_usr.Style.Add("text-align", "left");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_det_terr = new TableCell();
                Literal lit_det_terr = new Literal();
                lit_det_terr.Text = "&nbsp;" + drFF["territory_Name"].ToString();
                tc_det_terr.BorderStyle = BorderStyle.Solid;
                tc_det_terr.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                tc_det_terr.Style.Add("text-align", "left");
                tc_det_terr.Attributes.Add("Class", "rptCellBorder");
                tc_det_terr.Controls.Add(lit_det_terr);
                tr_det.Cells.Add(tc_det_terr);


                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["Doc_Qua_Name"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                tc_det_hq.Style.Add("text-align", "left");
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);


                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["Doc_Cat_ShortName"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                //tc_det_Designation.Style.Add("font-family", "Calibri");
                //tc_det_Designation.Style.Add("font-size", "10pt");
                tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                TableCell tc_det_Spec = new TableCell();
                Literal lit_det_Spec = new Literal();
                lit_det_Spec.Text = "&nbsp;" + drFF["Doc_Spec_ShortName"].ToString();
                tc_det_Spec.BorderStyle = BorderStyle.Solid;
                tc_det_Spec.BorderWidth = 1;
                //tc_det_Spec.Style.Add("font-family", "Calibri");
                //tc_det_Spec.Style.Add("font-size", "10pt");
                tc_det_Spec.Style.Add("text-align", "left");
                tc_det_Spec.Controls.Add(lit_det_Spec);
                tc_det_Spec.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Spec);

                TableCell tc_det_Class = new TableCell();
                Literal lit_det_Class = new Literal();
                lit_det_Class.Text = "&nbsp;" + drFF["Doc_Class_ShortName"].ToString();
                tc_det_Class.BorderStyle = BorderStyle.Solid;
                tc_det_Class.BorderWidth = 1;
                //tc_det_Class.Style.Add("font-family", "Calibri");
                //tc_det_Class.Style.Add("font-size", "10pt");
                tc_det_Class.Style.Add("text-align", "left");
                tc_det_Class.Controls.Add(lit_det_Class);
                tc_det_Class.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Class);

                tbl.Rows.Add(tr_det);
                int tot_cnt = 0;

                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                ViewState["months"] = months1;
                ViewState["cmonth"] = cmonth1;
                ViewState["cyear"] = cyear1;
                if (months1 >= 0)
                {

                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        TableRow tr_det1 = new TableRow();
                        TableCell tc_det_cnt = new TableCell();
                        Literal lit_det_cnt = new Literal();
                        DataSet dscnt = new DataSet();
                        DCR dcrdr = new DCR();
                        dscnt = dcrdr.getvisit_drcnt(sf_code, cmonth1, cyear1, drFF["ListedDrCode"].ToString());
                        if (dscnt.Tables[0].Rows.Count > 0)
                            tot_cnt = Convert.ToInt32(dscnt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                        lit_det_cnt.Text = "" + tot_cnt;
                        if (tot_cnt == 0)
                        {
                            lit_det_cnt.Text = " - ";
                        }
                        tc_det_cnt.BorderStyle = BorderStyle.Solid;
                        tc_det_cnt.BorderWidth = 1;
                        //tc_det_Class.Style.Add("font-family", "Calibri");
                        //tc_det_Class.Style.Add("font-size", "10pt");
                        tc_det_cnt.Style.Add("text-align", "left");
                        tc_det_cnt.BorderColor = System.Drawing.Color.Black;
                        tc_det_cnt.Controls.Add(lit_det_cnt);
                        tc_det_cnt.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_det_cnt);

                        TableCell tc_det_date = new TableCell();
                        Literal lit_det_date = new Literal();
                        DataSet dsdate = new DataSet();
                        string sActive_date = string.Empty;
                        dsdate = dcrdr.getvisit_drdate(sf_code, cmonth1, cyear1, drFF["ListedDrCode"].ToString());
                        if (dsdate.Tables[0].Rows.Count > 0)
                            foreach (DataRow drSF in dsdate.Tables[0].Rows)
                            {
                                sActive_date = sActive_date + drSF["Activity_Date"].ToString() + " , ";
                            }
                        //if (sActive_date.Length > 0)
                        //    sActive_date = sActive_date.Substring(0, sActive_date.Length - 2);
                        lit_det_date.Text = "" + sActive_date;
                        if (sActive_date == "")
                        {
                            lit_det_date.Text = " - ";
                        }
                        tc_det_date.BorderStyle = BorderStyle.Solid;
                        tc_det_date.BorderWidth = 1;
                        tc_det_date.Style.Add("color", "Red");
                        tc_det_date.Style.Add("text-align", "left");
                        tc_det_date.BorderColor = System.Drawing.Color.Black;
                        tc_det_date.Controls.Add(lit_det_date);
                        tc_det_date.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_det_date);
                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
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