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
using System.Globalization;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_Reports_rptDCRViewDeletedDetails : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Day = string.Empty;
    string StrDate = string.Empty;
    string strProduct_Detail = string.Empty;
    string strSubArea = string.Empty;
    string strProduct_Gift = string.Empty;
    string strActivity_Remark = string.Empty;
    string strCall_Remark = string.Empty;
    string strUnProduct_Detail = string.Empty;
    string strUnProduct_Gift = string.Empty;

    string Sf_HQ = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    string sDCR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strDateMatch = string.Empty;
        string strMonth = string.Empty;
        string str = string.Empty;
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["Month"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["Year"].ToString());
        Day = Request.QueryString["Day"].ToString();
        if (Day.Length != 2)
        {
            strDateMatch = "0" + Day;
        }
        else
        {
            strDateMatch = Day;
        }

        if (cmonth.ToString().Length != 2)
        {
            strMonth = "0" + cmonth;
        }
        else
        {
            strMonth = Convert.ToString(cmonth);
        }

        StrDate = strDateMatch + "/" + strMonth + "/" + cyear;

        //CultureInfo english = new CultureInfo("en-US");

        str = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(DateTime.Parse(StrDate).DayOfWeek);

        lblHead.Text = "<b>Daily Call Report - " + "<span style='color:#0077FF'>" + StrDate + " - " + str + "</span>" + "</b>";


        //CreateDynamicTable(cmonth, cyear, sf_code);
        DCR dc = new DCR();

        DCR dcsf = new DCR();
        dssf = dcsf.getSfName_HQ(sf_code);

        if (dssf.Tables[0].Rows.Count > 0)
        {
            Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }

        lblFieldForceName.Text = "<b>Field Force Name - </b>" + Sf_Name.ToString();

        lblHQ.Text = "<b>Head Quarters - </b>" + Sf_HQ.ToString();

        dsdoc = dc.get_TransDeletedRecord(sf_code, StrDate); //1-DCRMainTrans
        grdDCRTrans.DataSource = dsdoc;
        grdDCRTrans.DataBind();

        dsdoc = dc.get_LstTransDeletedRecord(sf_code, StrDate); //2-Listed Doctor
        grdLstDr.DataSource = dsdoc;
        grdLstDr.DataBind();

        dsdoc = dc.get_CshTransDeletedRecord(sf_code, StrDate); //3-Chemists
        GVChemist.DataSource = dsdoc;
        GVChemist.DataBind();

        dsdoc = dc.get_UnLstTransDeletedRecord(sf_code, StrDate); //4-UnListed Doctor
        GVUnlstDr.DataSource = dsdoc;
        GVUnlstDr.DataBind();



        lblSubArea.Text = "<b>Sub Area Worked - </b>" + Session["strSubArea"];



    }




    protected void btnExcel_Click(object sender, EventArgs e)
    {


        string attachment = "attachment; filename=Export.xls";
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}