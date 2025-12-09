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
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;


public partial class MIS_Reports_rptsamplestatus_New_New : System.Web.UI.Page
{

    int cmonth = -1;
    int cyear = -1;
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    string sPending = string.Empty;
    string dcrdays = string.Empty;
    string SF_code = string.Empty;
    int tot_days = -1;
    string div_code = string.Empty;
    DateTime ldcrdate;
    int count = 0;
    DataTable dtrowClr = null;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string sf_name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        SF_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();


        if (Request.QueryString["Dashboard"] != null)//Dashboard
        {
            pnlbutton.Visible = false;
        }
        //lblname.Text = sf_name;


        if (!Page.IsPostBack)
        {
            FillDespatch_status();

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            lblname.Text = sf_name;

            lblProd.Text = "Sample Despatch Status for the Month of  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

            linkDetail.Attributes.Add("href", "javascript:showModalPopUp('" + SF_code + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + sf_name + "', '" + div_code + "')");
        }

    }
    private void FillDespatch_status()
    {
        //SalesForce sf = new SalesForce();
        //////dsSalesForce = sf.sample_Des_New(div_code, SF_code, FMonth, FYear, TMonth, TYear);//[sample_Des_New_Modify_old] proc changed old proc//js
        //dsSalesForce = sf.sample_Des_New_Modify(div_code, SF_code, FMonth, FYear, TMonth, TYear, "1", "0", "");
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{

        //    grdDespatch.DataSource = dsSalesForce;
        //    grdDespatch.DataBind();
        //}
        //else
        //{
        //    grdDespatch.DataSource = dsSalesForce;
        //    grdDespatch.DataBind();
        //}
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        con.Open();
        //SqlCommand cmd = new SqlCommand("sample_Des_New_Modify_zero", con);
        //SqlCommand cmd = new SqlCommand("sample_Des_New_Modify", con);
        SqlCommand cmd = new SqlCommand("sample_Des_New_Modify_Job", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_Code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", SF_code);
        cmd.Parameters.AddWithValue("@month", FMonth);
        cmd.Parameters.AddWithValue("@Year", FYear);
        cmd.Parameters.AddWithValue("@to_month", TMonth);
        cmd.Parameters.AddWithValue("@to_year", TYear);
        cmd.Parameters.AddWithValue("@Mode", "1");
        cmd.Parameters.AddWithValue("@Prod_Mode", "0");
        cmd.Parameters.AddWithValue("@Prod_Code", "");
        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();

        if (ds.Tables[0].Rows.Count > 0)
        {

            grdDespatch.DataSource = ds;
            grdDespatch.DataBind();
        }
        else
        {
            grdDespatch.DataSource = ds;
            grdDespatch.DataBind();
        }
    }

    protected void grdDr_RowDataBoud(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblprod_name = (Label)e.Row.FindControl("lblprod_name");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblopening = (Label)e.Row.FindControl("lblopening");
            Label lblDes = (Label)e.Row.FindControl("lblDes");
            Label lblissued = (Label)e.Row.FindControl("lblissued");
            Label lblclosing = (Label)e.Row.FindControl("lblclosing");


            if (lblprod_name.Text == "Total")
            {
                lblprod_name.Font.Bold = true;
                lblprod_name.ForeColor = System.Drawing.Color.Red;
                lblSNo.Text = "";
                lblopening.Font.Bold = true;
                lblDes.Font.Bold = true;
                lblissued.Font.Bold = true;
                lblclosing.Font.Bold = true;
                lblprod_name.Attributes.Add("align", "right");

            }
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
    protected void linkDetail_Click(object sender, EventArgs e)
    {

        //string sURL = string.Empty;
        //sURL = "rptsamplestatus_New2_New.aspx?sfcode=" + SF_code + "&FMonth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + TMonth + "&TYear=" + TYear + "&sf_name=" + sf_name;

        //string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        linkDetail.Attributes.Add("href", "javascript:showModalPopUp('" + SF_code + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + sf_name + "', '" + div_code + "')");

    }

    protected void chklst_CheckedChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        if (chklst.Checked == true)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sample_Des_New_Modify_zero", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_Code", div_code);
            cmd.Parameters.AddWithValue("@sf_code", SF_code);
            cmd.Parameters.AddWithValue("@month", FMonth);
            cmd.Parameters.AddWithValue("@Year", FYear);
            cmd.Parameters.AddWithValue("@to_month", TMonth);
            cmd.Parameters.AddWithValue("@to_year", TYear);
            cmd.Parameters.AddWithValue("@Mode", "1");
            cmd.Parameters.AddWithValue("@Prod_Mode", "0");
            cmd.Parameters.AddWithValue("@Prod_Code", "");
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
        }
        else if (chklst.Checked == false)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sample_Des_New_Modify", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_Code", div_code);
            cmd.Parameters.AddWithValue("@sf_code", SF_code);
            cmd.Parameters.AddWithValue("@month", FMonth);
            cmd.Parameters.AddWithValue("@Year", FYear);
            cmd.Parameters.AddWithValue("@to_month", TMonth);
            cmd.Parameters.AddWithValue("@to_year", TYear);
            cmd.Parameters.AddWithValue("@Mode", "1");
            cmd.Parameters.AddWithValue("@Prod_Mode", "0");
            cmd.Parameters.AddWithValue("@Prod_Code", "");
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
        }
        //dsSalesForce = sf.sample_Des_New_Modify(div_code, SF_code, FMonth, FYear, TMonth, TYear, "1", "0", "");
        if (ds.Tables[0].Rows.Count > 0)
        {

            grdDespatch.DataSource = ds;
            grdDespatch.DataBind();
        }
        else
        {
            grdDespatch.DataSource = ds;
            grdDespatch.DataBind();
        }

       // chklst.Visible = false;
    }
}