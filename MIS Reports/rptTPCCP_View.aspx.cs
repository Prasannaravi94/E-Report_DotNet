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


public partial class MIS_Reports_rptTPCCP_View : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsVisit = null;
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
    string sReturn = string.Empty;
    int iCount = -1;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"];
        Year = Request.QueryString["FYear"];

        sf_name = Request.QueryString["sf_name"];
        //sCurrentDate = Request.QueryString["sCurrentDate"];

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        sf_name = sf_name.TrimStart(',');

        lblccp.Text = "CCP - View of " + "<span style='color:red'>" + Year + "</span>";

        if (!Page.IsPostBack)
        {
            jan.Text = jan.Text+" "+ "<span style='color:red'>" + Year + "</span>";
            feb.Text = feb.Text + " " + "<span style='color:red'>" + Year + "</span>";
            mar.Text = mar.Text + " " + "<span style='color:red'>" + Year + "</span>";
            apr.Text = apr.Text + " " + "<span style='color:red'>" + Year + "</span>";
            may.Text = may.Text + " " + "<span style='color:red'>" + Year + "</span>";
            jun.Text = jun.Text + " " + "<span style='color:red'>" + Year + "</span>";
            jul.Text = jul.Text + " " + "<span style='color:red'>" + Year + "</span>";
            aug.Text = aug.Text + " " + "<span style='color:red'>" + Year + "</span>";
            sep.Text = sep.Text + " " + "<span style='color:red'>" + Year + "</span>";
            oct.Text = oct.Text + " " + "<span style='color:red'>" + Year + "</span>";
            nov.Text = nov.Text + " " + "<span style='color:red'>" + Year + "</span>";
            dec.Text = dec.Text + " " + "<span style='color:red'>" + Year + "</span>";

            FillDr();
            //CreateDynamicTable();

            //lblProd.Text = "ListedDr - Product Visit" + "<span style='color:red'> " + "(" + Prod_Name + ")" + "</span>" + "<br />" + " Listed Doctor Details for the Month of " + "" + strMonthName + "" + " " + Year + "";
            //lblProd.Text = "Listed Doctor - Product Map";
            //lblProd.Font.Bold = true;
        }
    }

    private void FillDr()
    {
        TP_New dr = new TP_New();
        lblname.Text = sf_name;


        dsDr = dr.CCp_View("1", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdjan.Visible = true;
            grdjan.DataSource = dsDr;
            grdjan.DataBind();
        }
      


        dsDr = dr.CCp_View("2", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdfeb.Visible = true;
            grdfeb.DataSource = dsDr;
            grdfeb.DataBind();
        }
       

        dsDr = dr.CCp_View("3", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdmar.Visible = true;
            grdmar.DataSource = dsDr;
            grdmar.DataBind();
        }
       

        dsDr = dr.CCp_View("4", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdapr.Visible = true;
            grdapr.DataSource = dsDr;
            grdapr.DataBind();
        }
      

        dsDr = dr.CCp_View("5", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdmay.Visible = true;
            grdmay.DataSource = dsDr;
            grdmay.DataBind();
        }
       

        dsDr = dr.CCp_View("6", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdjun.Visible = true;
            grdjun.DataSource = dsDr;
            grdjun.DataBind();
        }
       

        dsDr = dr.CCp_View("7", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdjul.Visible = true;
            grdjul.DataSource = dsDr;
            grdjul.DataBind();
        }
       

        dsDr = dr.CCp_View("8", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdaug.Visible = true;
            grdaug.DataSource = dsDr;
            grdaug.DataBind();
        }
       

        dsDr = dr.CCp_View("9", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdsep.Visible = true;
            grdsep.DataSource = dsDr;
            grdsep.DataBind();
        }
       

        dsDr = dr.CCp_View("10", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdoct.Visible = true;
            grdoct.DataSource = dsDr;
            grdoct.DataBind();
        }
       

        dsDr = dr.CCp_View("11", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grdnov.Visible = true;
            grdnov.DataSource = dsDr;
            grdnov.DataBind();
        }

        dsDr = dr.CCp_View("12", Year);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            grddec.Visible = true;
            grddec.DataSource = dsDr;
            grddec.DataBind();
        }
       
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // Label lblDate = (Label)e.Row.FindControl("lblDate");
            Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");
            //Label lbldays_check = (Label)e.Row.FindControl("lbldays_check");

            if (lblDay_name.Text == "Sun")
            {
                e.Row.BackColor = System.Drawing.Color.PaleGoldenrod;
                //lblDate.BackColor = System.Drawing.Color.Yellow;
                //lblDay_name.BackColor = System.Drawing.Color.Yellow;
                //lbldays_check.BackColor = System.Drawing.Color.Yellow;
            }

        }

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