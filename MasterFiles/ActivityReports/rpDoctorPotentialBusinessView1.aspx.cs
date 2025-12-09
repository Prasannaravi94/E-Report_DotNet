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
using System.Web.UI.HtmlControls;
public partial class MasterFiles_ActivityReports_rpDoctorPotentialBusinessView1 : System.Web.UI.Page
{
     DataSet dsDr = null;
    DataSet dsChemists = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet st = null;
    DateTime dtCurrent;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount2 = 0;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string MR = string.Empty;
    string strFMonthName = string.Empty;
    string strTMonthName = string.Empty;
    string strMonthName = string.Empty;
    string sCmd = string.Empty;
    string tot_QtyVal = string.Empty;
    string tot_Month = string.Empty;
    string div_code = string.Empty;
    string divcode = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string state_code = string.Empty;
    string sfCode = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string Monthsub = string.Empty;
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    string basedOn = string.Empty;

    int iCount = -1;
    int doctor_total = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        divcode = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
        sfCode = Request.QueryString["sf_code"];
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Year = Request.QueryString["Year"];
        Month = Request.QueryString["Month"];
        MR = Request.QueryString["MR"].ToString();
        Prod_Name = Request.QueryString["Prod_Name"];
        Prod = Request.QueryString["Product_Code_SlNo"];
        sf_name = Request.QueryString["sf_name"];
        sCurrentDate = Request.QueryString["sCurrentDate"];
        basedOn = Request.QueryString["basedOn"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);

        sf_name = sf_name.TrimStart(',');

        if (rbtnBased.SelectedValue == "1")
        {
            LstdDrBusValue();
        }
        else if (rbtnBased.SelectedValue == "2")
        {
            FillPrdwiseLstDr();
        }
        else if (rbtnBased.SelectedValue == "3")
        {
            FillLstDrwisePrd();
        }

        if (!Page.IsPostBack)
        {
            lblProd.Text = "Listed_Doctor Potential View " + strMonthName + "_" + Year + "";
            lblProd.Font.Bold = true;
            lblname.Text = sf_name;
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
        string attachment = "attachment; filename=" + lblProd.Text + ".xls";
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

    #region LstdDrBusValue
    private void LstdDrBusValue()
    {
        tbl.Rows.Clear();

        int tot = 0;
        ListedDR dr = new ListedDR();
        ListedDR gg = new ListedDR();
        DataTable dtDr = new DataTable();

        dsDr = dr.Trans_ListedPotDr_Bus_View(sf_code, div_code, Convert.ToInt16(Month), Convert.ToInt16(Year));

        DataSet ff = new DataSet();
        ff = gg.getListedDrDiv_new(div_code, sf_code);

        dtDr = ff.Tables[0].Clone();

        foreach (DataRow drBus in dsDr.Tables[0].Rows)
        {
            var table = ff.Tables[0].AsEnumerable()
            .Where(r => r.Field<decimal>("ListedDrCode") == Convert.ToDecimal(drBus["ListedDrCode"]))
            .AsDataView().ToTable();

            foreach (DataRow row in table.Rows)
            {
                if (drBus.ItemArray[0].ToString() != row.ItemArray[0].ToString())
                {
                    DataRow dtr = dtDr.NewRow();
                    dtDr.ImportRow(row);
                }
            }
        }
        dtDr.AcceptChanges();

        if (dtDr.Rows.Count > 0)
        {
            TableRow tr_det_head = new TableRow();
            tr_det_head.BorderStyle = BorderStyle.Solid;
            tr_det_head.BorderWidth = 1;
            tr_det_head.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_det_head.Style.Add("Color", "#636d73");
            tr_det_head.Style.Add("border-bottom", "10px solid #fff");
            tr_det_head.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            //tr_det_head.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            //tr_det_head.Style.Add("Color", "White");
            //tr_det_head.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            tc_det_head_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_det_head_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_det_head_SNo.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "#";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_SNo);

            TableCell tc_det_head_docCode = new TableCell();
            tc_det_head_docCode.BorderStyle = BorderStyle.Solid;
            tc_det_head_docCode.BorderWidth = 1;
            tc_det_head_docCode.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_docCode = new Literal();
            lit_det_head_docCode.Text = "ListedDr Code";
            tc_det_head_docCode.Visible = false;
            tc_det_head_docCode.Attributes.Add("Class", "tblHead");
            tc_det_head_docCode.Controls.Add(lit_det_head_docCode);
            tc_det_head_docCode.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_docCode);

            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            tc_det_head_doc.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "Listed Doctor Name";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            tc_det_head_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "Category";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_hq);

            TableCell tc_det_head_Qua = new TableCell();
            tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua.BorderWidth = 1;
            tc_det_head_Qua.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_qua = new Literal();
            lit_det_head_qua.Text = "Speciality";
            tc_det_head_Qua.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua.Controls.Add(lit_det_head_qua);
            tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua);

            TableCell tc_det_head_Quai = new TableCell();
            tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
            tc_det_head_Quai.BorderWidth = 1;
            tc_det_head_Quai.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_quai = new Literal();
            lit_det_head_quai.Text = "Subarea";
            tc_det_head_Quai.Attributes.Add("Class", "tblHead");
            tc_det_head_Quai.Controls.Add(lit_det_head_quai);
            tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Quai);

            TableCell tc_det_head_Qua6 = new TableCell();
            tc_det_head_Qua6.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua6.BorderWidth = 1;
            tc_det_head_Qua6.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_qua6 = new Literal();
            lit_det_head_qua6.Text = "Potential Value in Rs.";
            tc_det_head_Qua6.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua6.Controls.Add(lit_det_head_qua6);
            tc_det_head_Qua6.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua6);
            tbl.Rows.Add(tr_det_head);

            iCount = 0;
            foreach (DataRow drdoctor in dtDr.Rows)
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

                TableCell tc_det_usrCode = new TableCell();
                Literal lit_det_usrCode = new Literal();
                lit_det_usrCode.Text = drdoctor["ListedDrCode"].ToString();
                tc_det_usrCode.Visible = false;
                tc_det_usrCode.BorderStyle = BorderStyle.Solid;
                tc_det_usrCode.BorderWidth = 1;
                tc_det_usrCode.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_usrCode.Attributes.Add("Class", "rptCellBorder");
                tc_det_usrCode.Controls.Add(lit_det_usrCode);
                tr_det_sno.Cells.Add(tc_det_usrCode);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();
                Literal lit_det_sfcode = new Literal();
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Doc_Cat_ShortName"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);


                TableCell tc_det_dr_name = new TableCell();
                Literal lit_det_dr_name = new Literal();
                lit_det_dr_name.Text = drdoctor["Doc_Spec_ShortName"].ToString();
                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                tc_det_dr_name.BorderWidth = 1;
                tc_det_dr_name.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                tr_det_sno.Cells.Add(tc_det_dr_name);

                TableCell tc_det_dr_hq = new TableCell();
                Literal lit_det_dr_hq = new Literal();
                lit_det_dr_hq.Text = drdoctor["Territory_Name"].ToString();
                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                tc_det_dr_hq.BorderWidth = 1;
                tc_det_dr_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                tr_det_sno.Cells.Add(tc_det_dr_hq);

                dsChemists = dr.ListedPotDrBus_value(sf_code, drdoctor["ListedDrCode"].ToString(), div_code, Convert.ToInt32(Month), Convert.ToInt32(Year), basedOn);

                TableCell tc_lst_monthw = new TableCell();
                Literal hyp_lst_monthw = new Literal();
                hyp_lst_monthw.Text = dsChemists.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tc_lst_monthw.Attributes.Add("Class", "tblRow");
                tc_lst_monthw.BorderStyle = BorderStyle.Solid;
                tc_lst_monthw.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthw.BorderWidth = 1;
                tc_lst_monthw.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_lst_monthw.Controls.Add(hyp_lst_monthw);
                tr_det_sno.Cells.Add(tc_lst_monthw);

                if (dsDr.Tables[0].Rows.Count > 0)
                    tot_dr = dsDr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                tbl.Rows.Add(tr_det_sno);
            }
            decimal iTotLstCount = 0;

            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            tc_Count_Total.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 5;
            tc_Count_Total.Style.Add("text-align", "left");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);

            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();

            foreach (DataRow drFF in dtDr.Rows)
            {
                dsChemists = dr.ListedPotDrBus_value(sf_code, drFF["ListedDrCode"].ToString(), div_code, Convert.ToInt32(Month), Convert.ToInt32(Year), basedOn);

                if (dsChemists.Tables[0].Rows.Count > 0)
                {
                    tot_dr = dsChemists.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_dr != "")
                    {
                        iTotLstCount += decimal.Parse(tot_dr);
                        hyp_month.Text = iTotLstCount.ToString();
                    }
                }
            }

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            iTotLstCount = 0;

            tbl.Rows.Add(tr_total);
        }
    }
    #endregion

    #region FillLstDrwisePrd
    private void FillLstDrwisePrd()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        ListedDR dr = new ListedDR();
        dsSalesForce = dr.rpPrdwiseDrPotBusView(sfCode, divcode, Convert.ToInt32(Year), Convert.ToInt32(Month), basedOn);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_header.Style.Add("Color", "#636d73");
            tr_header.Style.Add("border-bottom", "10px solid #fff");
            tr_header.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableRow tr_header1 = new TableRow();
            tr_header1.BorderStyle = BorderStyle.Solid;
            tr_header1.BorderWidth = 1;
            tr_header1.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_header1.Style.Add("Color", "#636d73");
            tr_header1.Style.Add("border-bottom", "10px solid #fff");
            tr_header1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableCell tc_SNo1 = new TableCell();
            tc_SNo1.BorderStyle = BorderStyle.Solid;
            tc_SNo1.BorderWidth = 1;
            tc_SNo1.Width = 50;
            tc_SNo1.RowSpan = 1;
            Literal lit_SNo1 = new Literal();
            lit_SNo1.Text = "&nbsp;";
            tc_SNo1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_SNo1.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo1.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            tc_SNo1.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo1.Controls.Add(lit_SNo1);
            tc_SNo1.Attributes.Add("Class", "rptCellBorder");
            tr_header1.Cells.Add(tc_SNo1);

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Cd1 = new TableCell();
            tc_DR_Cd1.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd1.BorderWidth = 1;
            tc_DR_Cd1.Width = 400;
            tc_DR_Cd1.RowSpan = 1;
            Literal lit_DR_Cd1 = new Literal();
            lit_DR_Cd1.Text = "&nbsp;";
            tc_DR_Cd1.Controls.Add(lit_DR_Cd1);
            tc_DR_Cd1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Cd1.Visible = false;
            tr_header1.Cells.Add(tc_DR_Cd1);

            TableCell tc_DR_Cd = new TableCell();
            tc_DR_Cd.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd.BorderWidth = 1;
            tc_DR_Cd.Width = 400;
            tc_DR_Cd.RowSpan = 1;
            Literal lit_DR_Cd = new Literal();
            lit_DR_Cd.Text = "<center>Doctor&nbspCode</center>";
            tc_DR_Cd.Controls.Add(lit_DR_Cd);
            tc_DR_Cd.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Cd.Visible = false;
            tr_header.Cells.Add(tc_DR_Cd);

            TableCell tc_DR_Cd2 = new TableCell();
            tc_DR_Cd2.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd2.BorderWidth = 1;
            tc_DR_Cd2.Width = 300;
            tc_DR_Cd2.RowSpan = 1;
            Literal lit_DR_Cd2 = new Literal();
            lit_DR_Cd2.Text = "&nbsp;";
            tc_DR_Cd2.Controls.Add(lit_DR_Cd2);
            tc_DR_Cd2.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd2.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tr_header1.Cells.Add(tc_DR_Cd2);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 300;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Doctor&nbspName</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_Pr_code1 = new TableCell();
            tc_Pr_code1.BorderStyle = BorderStyle.Solid;
            tc_Pr_code1.BorderWidth = 1;
            tc_Pr_code1.Width = 200;
            tc_Pr_code1.RowSpan = 1;
            Literal lit_Pr_Code1 = new Literal();
            lit_Pr_Code1.Text = "&nbsp;";
            tc_Pr_code1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_Pr_code1.Visible = false;
            tc_Pr_code1.Attributes.Add("Class", "rptCellBorder");
            tc_Pr_code1.Controls.Add(lit_Pr_Code1);
            tr_header1.Cells.Add(tc_Pr_code1);

            TableCell tc_Pr_code = new TableCell();
            tc_Pr_code.BorderStyle = BorderStyle.Solid;
            tc_Pr_code.BorderWidth = 1;
            tc_Pr_code.Width = 200;
            tc_Pr_code.RowSpan = 1;
            Literal lit_Pr_Code = new Literal();
            lit_Pr_Code.Text = "<center>Product&nbspCode</center>";
            tc_Pr_code.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_Pr_code.Visible = false;
            tc_Pr_code.Attributes.Add("Class", "rptCellBorder");
            tc_Pr_code.Controls.Add(lit_Pr_Code);
            tr_header.Cells.Add(tc_Pr_code);

            TableCell tc_DR_Name1 = new TableCell();
            tc_DR_Name1.BorderStyle = BorderStyle.Solid;
            tc_DR_Name1.BorderWidth = 1;
            tc_DR_Name1.Width = 400;
            tc_DR_Name1.RowSpan = 1;
            Literal lit_DR_Name1 = new Literal();
            lit_DR_Name1.Text = "&nbsp;";
            tc_DR_Name1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Name1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name1.Controls.Add(lit_DR_Name1);
            tr_header1.Cells.Add(tc_DR_Name1);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 400;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Products</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ1 = new TableCell();
            tc_DR_HQ1.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ1.BorderWidth = 1;
            tc_DR_HQ1.Width = 200;
            tc_DR_HQ1.RowSpan = 1;
            Literal lit_DR_HQ1 = new Literal();
            lit_DR_HQ1.Text = "&nbsp;";
            tc_DR_HQ1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_HQ1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ1.Controls.Add(lit_DR_HQ1);
            tr_header1.Cells.Add(tc_DR_HQ1);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>Pack</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 2;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_month = new Literal();
            Monthsub = sf.getMonthName(Month.ToString()) + "-" + Year;
            lit_month.Text = Monthsub.Substring(0, 3) + "-" + Year + "";
            tc_month.Attributes.Add("Class", "rptCellBorder");
            tc_month.BorderStyle = BorderStyle.Solid;
            tc_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_month.BorderWidth = 1;
            tc_month.HorizontalAlign = HorizontalAlign.Center;
            tc_month.Width = 300;
            tc_month.Controls.Add(lit_month);
            tr_header1.Cells.Add(tc_month);

            TableCell tc_DR_Qty = new TableCell();
            tc_DR_Qty.BorderStyle = BorderStyle.Solid;
            tc_DR_Qty.BorderWidth = 1;
            tc_DR_Qty.Width = 150;
            tc_DR_Qty.RowSpan = 1;
            Literal lit_DR_Qty = new Literal();
            lit_DR_Qty.Text = "<center>Potential Qty</center>";
            tc_DR_Qty.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Qty.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Qty.Controls.Add(lit_DR_Qty);
            tr_header.Cells.Add(tc_DR_Qty);

            TableCell tc_DR_Val = new TableCell();
            tc_DR_Val.BorderStyle = BorderStyle.Solid;
            tc_DR_Val.BorderWidth = 1;
            tc_DR_Val.Width = 150;
            tc_DR_Val.RowSpan = 1;
            Literal lit_DR_val = new Literal();
            lit_DR_val.Text = "<center>Value in Rs.</center>";
            tc_DR_Val.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Val.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Val.Controls.Add(lit_DR_val);
            tr_header.Cells.Add(tc_DR_Val);

            tbl.Rows.Add(tr_header1);
            tbl.Rows.Add(tr_header);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;

            int iCount = 0;
            decimal iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            int slno = 1;
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                int index = dsSalesForce.Tables[0].Rows.IndexOf(drFF);

                TableRow tr_det = new TableRow();
                iCount += 1;

                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                if (index == 0)
                {
                    lit_det_SNo.Text = "<center>" + slno + "</center>";
                    slno++;
                }
                else if (index > 0)
                {
                    if (drFF["ListedDrCode"].ToString() != dsSalesForce.Tables[0].Rows[index - 1]["ListedDrCode"].ToString())
                    {
                        lit_det_SNo.Text = "<center>" + slno + "</center>";
                        slno++;
                    }
                    else
                    {
                        lit_det_SNo.Text = "";
                    }
                }
                //lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_dc = new TableCell();
                Literal lit_det_cd = new Literal();
                lit_det_cd.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                tc_det_dc.BorderStyle = BorderStyle.Solid;
                tc_det_dc.BorderWidth = 1;
                tc_det_dc.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dc.Visible = false;
                tc_det_dc.Attributes.Add("Class", "rptCellBorder");
                tc_det_dc.Controls.Add(lit_det_cd);
                tr_det.Cells.Add(tc_det_dc);

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                if (index > 0)
                {
                    if (drFF["ListedDrCode"].ToString() == dsSalesForce.Tables[0].Rows[index - 1]["ListedDrCode"].ToString())
                    {
                        lit_det_usr.Text = "";
                    }
                    else
                    {
                        lit_det_usr.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                    }
                }
                //tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_code = new TableCell();
                Literal lit_det_code = new Literal();
                lit_det_code.Text = "&nbsp;" + drFF["Product_Code"].ToString();
                tc_det_code.BorderStyle = BorderStyle.Solid;
                tc_det_code.BorderWidth = 1;
                tc_det_code.Visible = false;
                tc_det_code.Attributes.Add("Class", "rptCellBorder");
                tc_det_code.Controls.Add(lit_det_code);
                tr_det.Cells.Add(tc_det_code);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["Product_Sale_Unit"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                tc_det_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_hq.HorizontalAlign = HorizontalAlign.Center;
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                TableCell tc_det_Qty = new TableCell();
                Literal lit_det_qty = new Literal();
                lit_det_qty.Text = "&nbsp;" + drFF["Potiental_Quantity"].ToString();
                tc_det_Qty.BorderStyle = BorderStyle.Solid;
                tc_det_Qty.BorderWidth = 1;
                tc_det_Qty.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_Qty.Width = 150;
                tc_det_Qty.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Qty.Attributes.Add("Class", "rptCellBorder");
                tc_det_Qty.Controls.Add(lit_det_qty);
                tr_det.Cells.Add(tc_det_Qty);

                TableCell tc_det_mnth = new TableCell();
                Literal lit_det_mnth = new Literal();
                lit_det_mnth.Text = "&nbsp;" + drFF["value"].ToString();
                tc_det_mnth.BorderStyle = BorderStyle.Solid;
                tc_det_mnth.BorderWidth = 1;
                tc_det_mnth.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_mnth.Width = 150;
                tc_det_mnth.HorizontalAlign = HorizontalAlign.Center;
                tc_det_mnth.Attributes.Add("Class", "rptCellBorder");
                tc_det_mnth.Controls.Add(lit_det_mnth);
                tr_det.Cells.Add(tc_det_mnth);

                tbl.Rows.Add(tr_det);
            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            tc_Count_Total.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 5;
            tc_Count_Total.Style.Add("text-align", "left");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);

            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                if (drFF["value"].ToString() != "")
                {
                    iTotLstCount += decimal.Parse(drFF["value"].ToString());
                    hyp_month.Text = iTotLstCount.ToString();
                }
            }

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            iTotLstCount = 0;

            tbl.Rows.Add(tr_total);
        }
    }
    #endregion
	
    #region FillPrdwiseLstDr
    private void FillPrdwiseLstDr()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        ListedDR dr = new ListedDR();
        Product objProduct = new Product();
        DataSet drDetails = new DataSet();

        st = sf.CheckStatecode(sfCode);
        if (st.Tables[0].Rows.Count > 0)
        {
            state_code = st.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        dsSalesForce = dr.rpDrwisePotPrdBusView(sfCode, divcode, Convert.ToInt32(Year), Convert.ToInt32(Month), state_code, basedOn);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_header.Style.Add("Color", "#636d73");
            tr_header.Style.Add("border-bottom", "10px solid #fff");
            tr_header.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableRow tr_header1 = new TableRow();
            tr_header1.BorderStyle = BorderStyle.Solid;
            tr_header1.BorderWidth = 1;
            tr_header1.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_header1.Style.Add("Color", "#636d73");
            tr_header1.Style.Add("border-bottom", "10px solid #fff");
            tr_header1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableCell tc_SNo1 = new TableCell();
            tc_SNo1.BorderStyle = BorderStyle.Solid;
            tc_SNo1.BorderWidth = 1;
            tc_SNo1.Width = 50;
            tc_SNo1.RowSpan = 1;
            Literal lit_SNo1 = new Literal();
            lit_SNo1.Text = "&nbsp;";
            tc_SNo1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_SNo1.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo1.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            tc_SNo1.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo1.Controls.Add(lit_SNo1);
            tc_SNo1.Attributes.Add("Class", "rptCellBorder");
            tr_header1.Cells.Add(tc_SNo1);

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_SNo.BackColor = System.Drawing.Color.FromName("#414d55");
            tc_SNo.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
            tc_SNo.Style.Add("border-radius", "8px 0 0 8px");
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Cd1 = new TableCell();
            tc_DR_Cd1.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd1.BorderWidth = 1;
            tc_DR_Cd1.Width = 400;
            tc_DR_Cd1.RowSpan = 1;
            Literal lit_DR_Cd1 = new Literal();
            lit_DR_Cd1.Text = "&nbsp;";
            tc_DR_Cd1.Controls.Add(lit_DR_Cd1);
            tc_DR_Cd1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Cd1.Visible = false;
            tr_header1.Cells.Add(tc_DR_Cd1);

            TableCell tc_DR_Cd = new TableCell();
            tc_DR_Cd.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd.BorderWidth = 1;
            tc_DR_Cd.Width = 400;
            tc_DR_Cd.RowSpan = 1;
            Literal lit_DR_Cd = new Literal();
            lit_DR_Cd.Text = "<center>Product_Detail_Code</center>";
            tc_DR_Cd.Controls.Add(lit_DR_Cd);
            tc_DR_Cd.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Cd.Visible = false;
            tr_header.Cells.Add(tc_DR_Cd);

            TableCell tc_DR_Cd2 = new TableCell();
            tc_DR_Cd2.BorderStyle = BorderStyle.Solid;
            tc_DR_Cd2.BorderWidth = 1;
            tc_DR_Cd2.Width = 300;
            tc_DR_Cd2.RowSpan = 1;
            Literal lit_DR_Cd2 = new Literal();
            lit_DR_Cd2.Text = "&nbsp;";
            tc_DR_Cd2.Controls.Add(lit_DR_Cd2);
            tc_DR_Cd2.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Cd2.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tr_header1.Cells.Add(tc_DR_Cd2);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 300;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Product</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_HQ1 = new TableCell();
            tc_DR_HQ1.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ1.BorderWidth = 1;
            tc_DR_HQ1.Width = 200;
            tc_DR_HQ1.RowSpan = 1;
            Literal lit_DR_HQ1 = new Literal();
            lit_DR_HQ1.Text = "&nbsp;";
            tc_DR_HQ1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_HQ1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ1.Controls.Add(lit_DR_HQ1);
            tr_header1.Cells.Add(tc_DR_HQ1);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>Pack</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_Pr_code1 = new TableCell();
            tc_Pr_code1.BorderStyle = BorderStyle.Solid;
            tc_Pr_code1.BorderWidth = 1;
            tc_Pr_code1.Width = 200;
            tc_Pr_code1.RowSpan = 1;
            Literal lit_Pr_Code1 = new Literal();
            lit_Pr_Code1.Text = "&nbsp;";
            tc_Pr_code1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_Pr_code1.Visible = false;
            tc_Pr_code1.Attributes.Add("Class", "rptCellBorder");
            tc_Pr_code1.Controls.Add(lit_Pr_Code1);
            tr_header1.Cells.Add(tc_Pr_code1);

            TableCell tc_Pr_code = new TableCell();
            tc_Pr_code.BorderStyle = BorderStyle.Solid;
            tc_Pr_code.BorderWidth = 1;
            tc_Pr_code.Width = 200;
            tc_Pr_code.RowSpan = 1;
            Literal lit_Pr_Code = new Literal();
            lit_Pr_Code.Text = "<center>ListedDrCode</center>";
            tc_Pr_code.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_Pr_code.Visible = false;
            tc_Pr_code.Attributes.Add("Class", "rptCellBorder");
            tc_Pr_code.Controls.Add(lit_Pr_Code);
            tr_header.Cells.Add(tc_Pr_code);

            TableCell tc_DR_Name1 = new TableCell();
            tc_DR_Name1.BorderStyle = BorderStyle.Solid;
            tc_DR_Name1.BorderWidth = 1;
            tc_DR_Name1.Width = 400;
            tc_DR_Name1.RowSpan = 1;
            Literal lit_DR_Name1 = new Literal();
            lit_DR_Name1.Text = "&nbsp;";
            tc_DR_Name1.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Name1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name1.Controls.Add(lit_DR_Name1);
            tr_header1.Cells.Add(tc_DR_Name1);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 400;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Doctor Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 2;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_month = new Literal();
            Monthsub = sf.getMonthName(Month.ToString()) + "-" + Year;
            lit_month.Text = Monthsub.Substring(0, 3) + "-" + Year + "";
            tc_month.Attributes.Add("Class", "rptCellBorder");
            tc_month.BorderStyle = BorderStyle.Solid;
            tc_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_month.BorderWidth = 1;
            tc_month.HorizontalAlign = HorizontalAlign.Center;
            tc_month.Width = 300;
            tc_month.Controls.Add(lit_month);
            tr_header1.Cells.Add(tc_month);

            TableCell tc_DR_Qty = new TableCell();
            tc_DR_Qty.BorderStyle = BorderStyle.Solid;
            tc_DR_Qty.BorderWidth = 1;
            tc_DR_Qty.Width = 150;
            tc_DR_Qty.RowSpan = 1;
            Literal lit_DR_Qty = new Literal();
            lit_DR_Qty.Text = "<center>Potential Qty</center>";
            tc_DR_Qty.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Qty.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Qty.Controls.Add(lit_DR_Qty);
            tr_header.Cells.Add(tc_DR_Qty);

            TableCell tc_DR_Val = new TableCell();
            tc_DR_Val.BorderStyle = BorderStyle.Solid;
            tc_DR_Val.BorderWidth = 1;
            tc_DR_Val.Width = 150;
            tc_DR_Val.RowSpan = 1;
            Literal lit_DR_val = new Literal();
            lit_DR_val.Text = "<center>Value in Rs.</center>";
            tc_DR_Val.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_DR_Val.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Val.Controls.Add(lit_DR_val);
            tr_header.Cells.Add(tc_DR_Val);

            tbl.Rows.Add(tr_header1);
            tbl.Rows.Add(tr_header);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;

            int iCount = 0;
            decimal iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            int slno = 1;
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                int index = dsSalesForce.Tables[0].Rows.IndexOf(drFF);

                TableRow tr_det = new TableRow();
                iCount += 1;
                //strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                if (index == 0)
                {
                    lit_det_SNo.Text = "<center>" + slno + "</center>";
                    slno++;
                }
                else if (index > 0)
                {
                    if (drFF["product_Detail_Code"].ToString() != dsSalesForce.Tables[0].Rows[index - 1]["product_Detail_Code"].ToString())
                    {
                        lit_det_SNo.Text = "<center>" + slno + "</center>";
                        slno++;
                    }
                    else
                    {
                        lit_det_SNo.Text = "";
                    }
                }
                //lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_dc = new TableCell();
                Literal lit_det_cd = new Literal();
                lit_det_cd.Text = "&nbsp;" + drFF["product_Detail_Code"].ToString();
                tc_det_dc.BorderStyle = BorderStyle.Solid;
                tc_det_dc.BorderWidth = 1;
                tc_det_dc.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dc.Visible = false;
                tc_det_dc.Attributes.Add("Class", "rptCellBorder");
                tc_det_dc.Controls.Add(lit_det_cd);
                tr_det.Cells.Add(tc_det_dc);

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["product_Detail_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                if (index > 0)
                {
                    if (drFF["product_Detail_Code"].ToString() == dsSalesForce.Tables[0].Rows[index - 1]["product_Detail_Code"].ToString())
                    {
                        lit_det_usr.Text = "";
                    }
                    else
                    {
                        lit_det_usr.Text = "&nbsp;" + drFF["product_Detail_Name"].ToString();
                    }
                }
                //tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["Product_Sale_Unit"].ToString();
                if (index > 0)
                {
                    if (drFF["product_Detail_Code"].ToString() == dsSalesForce.Tables[0].Rows[index - 1]["product_Detail_Code"].ToString())
                    {
                        lit_det_hq.Text = "";
                    }
                    else
                    {
                        lit_det_hq.Text = "&nbsp;" + drFF["Product_Sale_Unit"].ToString();
                    }
                }

                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                tc_det_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_hq.HorizontalAlign = HorizontalAlign.Center;
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Name
                TableCell tc_det_code = new TableCell();
                Literal lit_det_code = new Literal();
                lit_det_code.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                tc_det_code.BorderStyle = BorderStyle.Solid;
                tc_det_code.BorderWidth = 1;
                tc_det_code.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_code.Visible = false;
                tc_det_code.Attributes.Add("Class", "rptCellBorder");
                tc_det_code.Controls.Add(lit_det_code);
                tr_det.Cells.Add(tc_det_code);

                drDetails = dr.ViewListedDr(drFF["ListedDrCode"].ToString());

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();

                if (drDetails.Tables[0].Rows.Count > 0)
                {
                    lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString() + " - " +
                        drDetails.Tables[0].Rows[0]["Doc_Special_Name"].ToString() + " - " +
                        drDetails.Tables[0].Rows[0]["territory_Name"].ToString();
                }
                else
                {
                    lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                }
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_det_Qty = new TableCell();
                Literal lit_det_qty = new Literal();
                lit_det_qty.Text = "&nbsp;" + drFF["Potiental_Quantity"].ToString();
                tc_det_Qty.BorderStyle = BorderStyle.Solid;
                tc_det_Qty.BorderWidth = 1;
                tc_det_Qty.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_Qty.Width = 150;
                tc_det_Qty.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Qty.Attributes.Add("Class", "rptCellBorder");
                tc_det_Qty.Controls.Add(lit_det_qty);
                tr_det.Cells.Add(tc_det_Qty);

                TableCell tc_det_mnth = new TableCell();
                Literal lit_det_mnth = new Literal();
                lit_det_mnth.Text = "&nbsp;" + drFF["value"].ToString();
                tc_det_mnth.BorderStyle = BorderStyle.Solid;
                tc_det_mnth.BorderWidth = 1;
                tc_det_mnth.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_mnth.Width = 150;
                tc_det_mnth.HorizontalAlign = HorizontalAlign.Center;
                tc_det_mnth.Attributes.Add("Class", "rptCellBorder");
                tc_det_mnth.Controls.Add(lit_det_mnth);
                tr_det.Cells.Add(tc_det_mnth);

                tbl.Rows.Add(tr_det);
            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            tc_Count_Total.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 5;
            tc_Count_Total.Style.Add("text-align", "left");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);

            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                if (drFF["value"].ToString() != "")
                {
                    iTotLstCount += decimal.Parse(drFF["value"].ToString());
                    hyp_month.Text = iTotLstCount.ToString();
                }
            }

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            iTotLstCount = 0;

            tbl.Rows.Add(tr_total);
        }
    }
    #endregion

    protected void rbtnBased_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnBased.SelectedValue == "1")
        {
            LstdDrBusValue();
        }
        else if (rbtnBased.SelectedValue == "2")
        {
            FillPrdwiseLstDr();
        }
        else if (rbtnBased.SelectedValue == "3")
        {
            FillLstDrwisePrd();
        }
    }
}
