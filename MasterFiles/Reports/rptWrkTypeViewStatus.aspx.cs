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


public partial class Reports_rptWrkTypeViewStatus : System.Web.UI.Page
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
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;

    int fldwrk_total = 0;
    int FWD_Total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string FWD = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string strChkWorktype = string.Empty;
    string strFieledForceName = string.Empty;

    string sCurrentDate = string.Empty;
    string sType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
       

        div_code = Request.QueryString["div_Code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        if (Request.QueryString["sf_name"].ToString() != "")
        {
            strFieledForceName = Request.QueryString["sf_name"].ToString();
        }
        else
        {
            strFieledForceName = Session["sf_name"].ToString();
        }

        strChkWorktype = Request.QueryString["ChkWorkType"].ToString().Replace("undefined", "");
        strChkWorktype = strChkWorktype.Replace("NaN", "");
        strChkWorktype = strChkWorktype.Remove(strChkWorktype.Length - 1);

        if (Session["sf_type"].ToString() == "1" )
        {
            
            sf_code = Session["sf_code"].ToString();
        }

        //sType = Request.QueryString["Type"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);

        lblHead.Text = "Worktype Status for the Period of " + strFrmMonth +" " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillSF();

        ExportButton();

    }

    private void ExportButton()
    {
        btnPDF.Visible = false;
    }

    

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();

        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dsSalesForce = sf.UserList_get_SelfMail(div_code, sf_code);
            }
            else
            {
                    
                DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                dsmgrsf.Tables.Add(dt);
                dsSalesForce = dsmgrsf;
            }
        }
        else if(Session["sf_type"].ToString() == "1")
        {
            div_code = Session["div_code"].ToString();
            dsSalesForce = sf.sp_UserMRLogin(div_code, sf_code);
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            div_code = Session["div_code"].ToString();
                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = sf.getUserListReportingTo(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }

        }
      

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();          
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8"); 
            //tr_header.Attributes.Add("Class", "tblCellFont");

            TableCell tc_SNo = new TableCell();
            tc_SNo.Width = 40;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.Style.Add("border-bottom", "10px solid #fff");
            tc_SNo.Attributes.Add("Class", "tr_Sno stickyFirstRow");
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);            

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);           
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Attributes.Add("Class", "tr_th stickyFirstRow");
            tc_DR_Name.Style.Add("padding", "15px 5px");                   
            tc_DR_Name.Style.Add("vertical-align", "inherit");          
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.Width = 300;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Attributes.Add("Class", "tr_th stickyFirstRow");
            tc_DR_HQ.Style.Add("padding", "15px 5px");
            tc_DR_HQ.Style.Add("vertical-align", "inherit");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.Width = 100;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Design</center>";
            tc_DR_Des.Attributes.Add("Class", "tr_th stickyFirstRow");
            tc_DR_Des.Style.Add("padding", "15px 5px");
            tc_DR_Des.Style.Add("vertical-align", "inherit");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.Width = 100;
            tc_DR_DOJ.RowSpan = 2;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>DOJ</center>";
            tc_DR_DOJ.Attributes.Add("Class", "tr_th stickyFirstRow");
            tc_DR_DOJ.Style.Add("padding", "15px 5px");
            tc_DR_DOJ.Style.Add("vertical-align", "inherit");
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tr_header.Cells.Add(tc_DR_DOJ);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);           

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);            

            DataSet dsWorkType = new DataSet();
            DCR dcr=new DCR();
            TableRow tr_lst_det = new TableRow();
            tr_lst_det.Attributes.Add("Class", "tr_th stickyFirstRow");
            tr_lst_det.Style.Add("padding", "15px 5px");
            tr_lst_det.Style.Add("vertical-align", "inherit");
            tr_lst_det.Style.Add(" background-color", "#F1F5F8");

            dsWorkType = dcr.Get_ChkWorkTypeName(strChkWorktype,div_code);
            for (int i = 0; i < dsWorkType.Tables[0].Rows.Count; i++)
            {
                int Chkmonths = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int Chkcmonth = Convert.ToInt32(FMonth);
                int Chkcyear = Convert.ToInt32(FYear);

                ViewState["months"] = Chkmonths;
                ViewState["cmonth"] = Chkcmonth;
                ViewState["cyear"] = Chkcyear;

                TableCell tc_DR_FldWrk = new TableCell();
                tc_DR_FldWrk.ColumnSpan = Chkmonths +2;                
                tc_DR_FldWrk.Width = 500;
                Literal lit_DR_FldWrk = new Literal();
                lit_DR_FldWrk.Text = "<center>"+ dsWorkType.Tables[0].Rows[i][0].ToString() +"</center>";
                tc_DR_FldWrk.Attributes.Add("Class", "tr_th stickyFirstRow");
                tc_DR_FldWrk.Style.Add("padding", "15px 5px");
                tc_DR_FldWrk.Style.Add("vertical-align", "inherit");
                tc_DR_FldWrk.Style.Add(" background-color", "#F1F5F8");
                tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                tr_header.Cells.Add(tc_DR_FldWrk);

                if (Chkmonths >= 0)
                {
                    for (int j = 1; j <= Chkmonths+1; j++)
                    {
                        TableCell tc_month = new TableCell();
                        Literal lit_month = new Literal();
                        lit_month.Text = "&nbsp;" + sf.getMonthName(Chkcmonth.ToString()).Substring(0, 3);
                        tc_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_month.Controls.Add(lit_month);
                        tc_month.Attributes.Add("Class", "tr_th stickySecondRow");
                        tc_month.Style.Add("padding", "15px 5px");
                        tc_month.Style.Add("vertical-align", "inherit");
                        tr_lst_det.Cells.Add(tc_month);
                        Chkcmonth = Chkcmonth + 1;
                        if (Chkcmonth == 13)
                        {
                            Chkcmonth = 1;
                            Chkcyear = Chkcyear + 1;
                        }
                    }

                    TableCell tc_DR_Total = new TableCell();
                    //tc_DR_Total.Width = 50;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>Total</center>";
                    tc_DR_Total.Attributes.Add("Class", "tr_th stickySecondRow");
                    tc_DR_Total.Style.Add("padding", "15px 5px");
                    tc_DR_Total.Style.Add("vertical-align", "inherit");
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tr_lst_det.Cells.Add(tc_DR_Total);
                }


               
            }

            TableCell tc_CallAvg_Lst_UnLst = new TableCell();
            tc_CallAvg_Lst_UnLst.RowSpan = 2;
            //tc_DR_Total.Width = 50;
            Literal lit_CallAvg_Lst_UnLst = new Literal();
            lit_CallAvg_Lst_UnLst.Text = "<center>Call Avg LstDr&nbsp+&nbspUnLstDr</center>";
            tc_CallAvg_Lst_UnLst.Attributes.Add("Class", "tr_th stickyFirstRow");
            tc_CallAvg_Lst_UnLst.Style.Add("padding", "15px 5px");
            tc_CallAvg_Lst_UnLst.Style.Add("vertical-align", "inherit");
            tc_CallAvg_Lst_UnLst.Style.Add("border-radius", "0px 8px 8px 0px");
            tc_CallAvg_Lst_UnLst.Controls.Add(lit_CallAvg_Lst_UnLst);
            tr_header.Cells.Add(tc_CallAvg_Lst_UnLst);

            tbl.Rows.Add(tr_header);
            

           
            tbl.Rows.Add(tr_lst_det);

            //Sub Header
            //months = Convert.ToInt16(ViewState["months"].ToString());
            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());           

            // Details Section

            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;          
            DCR dcs = new DCR();


            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Style.Add("vertical-align", "inherit");
                tc_det_SNo.Width = 40;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.Style.Add("vertical-align", "inherit");
                tc_det_doc_name.Width = 500;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_HQ.Width = 300;
                tc_det_sf_HQ.Style.Add("vertical-align", "inherit");
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "3")
                {
                    lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_sf_des.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                }
                tc_det_sf_des.Style.Add("vertical-align", "inherit");
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                TableCell tc_det_sf_DOJ = new TableCell();
                Literal lit_det_sf_DOJ = new Literal();
                lit_det_sf_DOJ.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                tc_det_sf_DOJ.Style.Add("vertical-align", "inherit");
                tc_det_sf_DOJ.Width = 100;
                tc_det_sf_DOJ.Controls.Add(lit_det_sf_DOJ);
                tr_det.Cells.Add(tc_det_sf_DOJ);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());



                tot_dr = "";
                tot_doc_met = "";
                tot_doc_calls_seen = "";

                fldwrk_total = 0;
                FWD_Total = 0;
                doctor_total = 0;
                doc_met_total = 0;
                doc_calls_seen_total = 0;
                dblCoverage = 0.00;
                dblaverage = 0.00;

                int CallAvg=0;
               
                int itotFieldWork = 0;
                for (int i = 0; i < dsWorkType.Tables[0].Rows.Count; i++)
                {
                    cmonth = Convert.ToInt16(ViewState["cmonth"]);
                    int itotWorkType = 0;
                    int iSumLeave = 0;

                    int Chkmonths = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int Chkcmonth = Convert.ToInt32(FMonth);
                    int Chkcyear = Convert.ToInt32(FYear);

                    ViewState["months"] = Chkmonths;
                    ViewState["cmonth"] = Chkcmonth;
                    ViewState["cyear"] = Chkcyear;

                    for (int j = 1; j <= Chkmonths + 1; j++)
                    {
                        tot_fldwrk = "";
                        FWD = "";
                        fldwrk_total = 0;
                        FWD_Total = 0;

                        if (dsWorkType.Tables[0].Rows[i]["worktype_name_b"].ToString() == "Field Work")
                        {
                            dsDoc = dcs.Get_CountWorkType(drFF["sf_code"].ToString(), div_code, Chkcmonth.ToString(), Chkcyear.ToString(), dsWorkType.Tables[0].Rows[i]["worktype_name_b"].ToString());
                            if (dsDoc.Tables[0].Rows.Count > 0)
                                FWD = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            FWD_Total = FWD_Total + Convert.ToInt16(FWD);
                            itotFieldWork += FWD_Total;
                        }
                        else
                        {
                            dsDoc = dcs.Get_CountWorkType(drFF["sf_code"].ToString(), div_code, Chkcmonth.ToString(), Chkcyear.ToString(), dsWorkType.Tables[0].Rows[i]["worktype_name_b"].ToString());
                        }

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);
                        itotWorkType += fldwrk_total;

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                       
                        if (itotWorkType == 0)
                        {
                            lit_det_sf_FLDWRK.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                        }
                        tc_det_sf_FLDWRK.Style.Add("vertical-align", "inherit");
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        

                        sCurrentDate = Chkcmonth + "-01-" + Chkcyear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);
                        if (dsWorkType.Tables[0].Rows[i]["worktype_name_b"].ToString() == "Field Work")
                        {
                            dsDoc = dcs.New_DCR_Visit_TotalDocCallAvg(drFF["sf_code"].ToString(), div_code, Chkcmonth, Chkcyear);
                        }
                        int istr = Convert.ToInt16(dsDoc.Tables[0].Rows[0][0].ToString());

                        CallAvg += istr;

                        Chkcmonth = Chkcmonth + 1;
                        if (Chkcmonth == 13)
                        {
                            Chkcmonth = 1;
                            Chkcyear = Chkcyear + 1;
                        }


                    }

                    int[] arrWorkType = new int[] { itotWorkType };

                    for (int W = 0; W < arrWorkType.Length; W++)
                    {
                        iSumLeave += arrWorkType[W];
                    }

                    TableCell tc_det_sf_Tot = new TableCell();
                    Literal lit_det_sf_Tot = new Literal();
                    
                    if (iSumLeave == 0)
                    {
                        lit_det_sf_Tot.Text = "&nbsp;" + "-";
                    }
                    else
                    {
                        lit_det_sf_Tot.Text = "&nbsp;" + iSumLeave;
                    }
                    //tc_det_sf_Tot.Width = 50;
                    tc_det_sf_Tot.Style.Add("vertical-align", "inherit");
                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                    tr_det.Cells.Add(tc_det_sf_Tot);



                    

                }

                decimal RoundLstCallAvg = new decimal();
                TableCell tc_Call_Avg_Lst_UnLst = new TableCell();
                Literal lit_Call_Avg_Lst_UnLst = new Literal();
                RoundLstCallAvg = 0;
                if (itotFieldWork != 0)
                {
                    dblaverage = Convert.ToDouble((Convert.ToDecimal(CallAvg) / Convert.ToDecimal(itotFieldWork)));
                    RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                }

                if (itotFieldWork != 0)
                {
                    lit_Call_Avg_Lst_UnLst.Text = RoundLstCallAvg.ToString("#.##");
                }
                else
                {
                    lit_Call_Avg_Lst_UnLst.Text = "&nbsp;" + "-";
                }

                //tc_det_sf_Tot.Width = 50;
                tc_Call_Avg_Lst_UnLst.Style.Add("vertical-align", "inherit");
                tc_Call_Avg_Lst_UnLst.HorizontalAlign = HorizontalAlign.Center;
                tc_Call_Avg_Lst_UnLst.VerticalAlign = VerticalAlign.Middle;
                tc_Call_Avg_Lst_UnLst.Controls.Add(lit_Call_Avg_Lst_UnLst);
                tr_det.Cells.Add(tc_Call_Avg_Lst_UnLst);

                tbl.Rows.Add(tr_det);
                //Session["PostTable"] = dsSalesForce;

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