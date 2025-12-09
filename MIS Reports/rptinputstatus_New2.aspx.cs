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

public partial class MIS_Reports_rptinputstatus_New2 : System.Web.UI.Page
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


    protected void Page_Load(object sender, EventArgs e)
    {


        div_code = Session["div_code"].ToString();
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
        int tot = 0;
        SalesForce sf = new SalesForce();

        dsDr = sf.Sample_Status_New_gift(div_code, sf_code, FMonth, FYear, TMonth, TYear);

        //if (sf_code != "0")
        //{

        // dsDr = dr.getInput_Sample_Doctor(div_code, sf_code, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Gift_Code), sCurrentDate);
        // }
        //else
        //{

        //    if (Request.QueryString["totalinput"] != null)
        //    {

        //        Sf_Code_multiple = Session["Sf_Code_multiple"].ToString();
        //        dsDr = dr.Input_Total(div_code, Sf_Code_multiple, Convert.ToInt16(Year), Convert.ToInt16(Month));
        //    }

        //    else
        //    {

        //        Sf_Code_multiple = Session["Sf_Code_multiple"].ToString();
        //        dsDr = dr.Input_DrCountTotal(div_code, Sf_Code_multiple, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Gift_Code));
        //    }
        //}

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



            Label lblopening = (Label)e.Row.FindControl("lblopening");
            Label lblsamqty = (Label)e.Row.FindControl("lblsamqty");
            Label lblissued = (Label)e.Row.FindControl("lblissued");
            Label lblclosing = (Label)e.Row.FindControl("lblclosing");



            if ((lblissued.Text != "") && (lblopening.Text != "") && (lblsamqty.Text != ""))
            {
                int open1 = Convert.ToInt32(lblopening.Text);
                int samp1 = Convert.ToInt32(lblsamqty.Text);
                if ((Convert.ToInt32(lblissued.Text)) > (open1 + samp1))
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


        }
    }

}
