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
using DBase_EReport;

public partial class MasterFiles_AnalysisReports_rptAnalysis_Pob_count_Periodically2 : System.Web.UI.Page
{

 
    DataSet dsDr = null;  
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty; 
    string mode = string.Empty;
    string from_date = string.Empty;
    string to_date = string.Empty;
    string strQry = string.Empty;


    DB_EReporting db_ER = new DB_EReporting();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"];
        sf_name = Request.QueryString["sf_name"];
        mode = Request.QueryString["mode"];
        from_date = Request.QueryString["from_date"];
        to_date = Request.QueryString["to_date"];

        if (!Page.IsPostBack)
        {
            lblname.Text = sf_name;
            lblProd.Text = "POB Wise - Periodically for the period of " + from_date + " to " + to_date;
            FillDr();
        }

    }

    private void FillDr()
    {
        strQry = "EXEC POP_Peri_Sess '" + div_code + "', '" + sf_code + "', '" + from_date + "','" + to_date + "','" + mode + "'";
        dsDr = db_ER.Exec_DataSet(strQry);

        if (dsDr.Tables[0].Rows.Count > 0)
        {

            grdDr.DataSource = dsDr;
            grdDr.DataBind();
        }
        else
        {
            grdDr.DataSource = dsDr;
            grdDr.DataBind();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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
}