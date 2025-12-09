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

public partial class MIS_Reports_rptinputstatus_New2_New : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsDoc = null;
    DataSet dsdoc1 = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string a = "-";
    string is_dr = string.Empty;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string MultiProd_Code = string.Empty;

    int iCount = -1;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string strQry = string.Empty;


    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsSF = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sf_name = Request.QueryString["sfname"].ToString();


        sf_name = sf_name.TrimStart(',');


        if (!Page.IsPostBack)
        {
            FillPrd();
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblProd.Text = "Input Despatch Status of  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

            lblProd.Font.Bold = true;
            if (sf_code != "0")
            {
                lblname.Text = sf_name;
            }
            else
            {
                lblfieldname.Visible = false;
            }
        }
    }

    private void FillPrd()
    {
        //strQry = "EXEC Input_Des_New_Zoom '" + div_code + "', '" + sf_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + " ";
        strQry = "EXEC Input_Des_New_Modify_zero_Using_Job '" + div_code + "', '" + sf_code + "', " + FMonth + "," + FYear + "," + TMonth + "," + TYear + ", '" + "2" + "', '" + "0" + "', '" + "" + "' ";
        dsDr = db_ER.Exec_DataSet(strQry);




        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdprd.Visible = true;
            grdprd.DataSource = dsDr;
            grdprd.DataBind();
        }
        else
        {
            grdprd.DataSource = dsDr;
            grdprd.DataBind();
        }
        //}
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
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void grdDr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblGift_Name = (Label)e.Row.FindControl("lblGift_Name");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblopening = (Label)e.Row.FindControl("lblopening");
            Label lblDes = (Label)e.Row.FindControl("lblsamqty");
            Label lblissued = (Label)e.Row.FindControl("lblissued");
            Label lblclosing = (Label)e.Row.FindControl("lblclosing");


            if (lblGift_Name.Text == "Total")
            {
                lblGift_Name.Font.Bold = true;
                lblGift_Name.ForeColor = System.Drawing.Color.Red;
                lblSNo.Text = "";
                lblopening.Font.Bold = true;
                lblDes.Font.Bold = true;
                lblissued.Font.Bold = true;
                lblclosing.Font.Bold = true;
                lblGift_Name.Attributes.Add("align", "right");

            }
        }

    }
}


