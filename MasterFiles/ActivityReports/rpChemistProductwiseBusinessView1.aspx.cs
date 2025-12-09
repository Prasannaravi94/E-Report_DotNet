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
public partial class MIS_Reports_dcview1 : System.Web.UI.Page
{
    DataSet dsDr = null;
    DataSet dsChemists = null;
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
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    int iCount = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
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

        if (MR == "1")
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            System.Globalization.DateTimeFormatInfo mfi1 = new System.Globalization.DateTimeFormatInfo();
            strTMonthName = mfi1.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        }
        else
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        }
        sf_name = sf_name.TrimStart(',');

        CreateDynamicTable();

        if (!Page.IsPostBack)
        {
            if (MR == "1")
            {
                lblProd.Text = "Chemist_Productwise_Business_Entry_View_for_the_Period_of_" + strFMonthName + "_" + FYear + "_" + "-" + strTMonthName + "_" + TYear + "";
            }
            else
            {
                lblProd.Text = "Chemist_Productwise_Business_Entry_View_for_the_Period_of_" + strMonthName + "_" + Year + "";
            }
            lblProd.Font.Bold = true;
            lblname.Text = sf_name;
            //CreateDynamicTable();
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

    private void CreateDynamicTable()
    {
        int tot = 0;
        Chemist dr = new Chemist();
        Chemist chem = new Chemist();

        if (MR == "1")
        {
            dsDr = dr.Trans_ChPr_Bus_ViewMR(sf_code, div_code, Convert.ToInt16(FYear), Convert.ToInt16(FMonth), Convert.ToInt16(TYear), Convert.ToInt16(TMonth));
        }
        else
        {
            dsDr = dr.Trans_ChPr_Bus_View(sf_code, div_code, Convert.ToInt16(Month), Convert.ToInt16(Year));
        }

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_det_head = new TableRow();
            tr_det_head.BorderStyle = BorderStyle.Solid;
            tr_det_head.BorderWidth = 1;
            tr_det_head.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            tr_det_head.Style.Add("Color", "Black");
            tr_det_head.BorderColor = System.Drawing.Color.FromName("#DCE2E8");

            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            tc_det_head_SNo.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            tc_det_head_doc.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Chemists Code</b>";
            tc_det_head_doc.Visible = false;
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            tc_det_head_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "<b>Chemists Name</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_hq);

            TableCell tc_det_head_Quai = new TableCell();
            tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
            tc_det_head_Quai.BorderWidth = 1;
            tc_det_head_Quai.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_quai = new Literal();
            lit_det_head_quai.Text = "<b>Territory</b>";
            tc_det_head_Quai.Attributes.Add("Class", "tblHead");
            tc_det_head_Quai.Controls.Add(lit_det_head_quai);
            tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Quai);

            TableCell tc_det_head_Qua6 = new TableCell();
            tc_det_head_Qua6.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua6.BorderWidth = 1;
            tc_det_head_Qua6.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
            Literal lit_det_head_qua6 = new Literal();
            lit_det_head_qua6.Text = "<b>Business Value</b>";
            tc_det_head_Qua6.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua6.Controls.Add(lit_det_head_qua6);
            tc_det_head_Qua6.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua6);
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

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = drdoctor["Chemists_Code"].ToString();
                tc_det_usr.Visible = false;
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();
                Literal lit_det_sfcode = new Literal();
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Chemists_Name"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);

                TableCell tc_det_dr_hq = new TableCell();
                Literal lit_det_dr_hq = new Literal();
                lit_det_dr_hq.Text = drdoctor["Territory_Name"].ToString();
                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                tc_det_dr_hq.BorderWidth = 1;
                tc_det_dr_hq.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                tr_det_sno.Cells.Add(tc_det_dr_hq);

                if (MR == "1")
                {
                    dsChemists = dr.ChPrBus_valueMR(sf_code, drdoctor["Chemists_Code"].ToString(), div_code, Convert.ToInt16(FYear), Convert.ToInt16(FMonth), Convert.ToInt16(TYear), Convert.ToInt16(TMonth));
                }
                else
                {
                    dsChemists = chem.ChPrBus_value(sf_code, drdoctor["Chemists_Code"].ToString(), div_code, Convert.ToInt32(Month), Convert.ToInt32(Year));
                }

                TableCell tc_lst_monthw = new TableCell();
                Literal hyp_lst_monthw = new Literal();
                hyp_lst_monthw.Text = dsChemists.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tc_lst_monthw.Attributes.Add("Class", "tblRow");
                tc_lst_monthw.BorderStyle = BorderStyle.Solid;
                tc_lst_monthw.BorderWidth = 1;
                tc_lst_monthw.BorderColor = System.Drawing.Color.FromName("#DCE2E8");
                tc_lst_monthw.Controls.Add(hyp_lst_monthw);
                tr_det_sno.Cells.Add(tc_lst_monthw);

                if (dsDr.Tables[0].Rows.Count > 0)
                    tot_dr = dsDr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                tbl.Rows.Add(tr_det_sno);
            }
        }
    }
}
