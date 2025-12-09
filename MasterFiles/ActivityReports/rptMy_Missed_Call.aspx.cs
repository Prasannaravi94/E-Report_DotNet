using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_dcview : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string trrCode = string.Empty;

    string sCurrentDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Request.QueryString["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        trrCode = Request.QueryString["trrCode"].ToString();
        //lblRegionName.Text = sfname;

        //lblHead.Text = "Doctor Business Valuewise Entry for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        FillSF();
    }

    private void FillSF()
    {
        Doctor dc = new Doctor();
        DataSet dsDoctor = null;
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (DateTime.Now.Month == 12)
        {
            sCurrentDate = "01-01-" + (DateTime.Now.Year + 1);
        }
        else
        {
            sCurrentDate = (DateTime.Now.Month + 1) + "-01-" + DateTime.Now.Year;
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);

        dsDoctor = dc.Missed_Doc_cnt(divcode, sfCode, DateTime.Now.Month, DateTime.Now.Year, dtCurrent, "1");

        var rsLstdDr = from dr in dsDoctor.Tables[0].AsEnumerable()
                       where dr.Field<string>("territory_Name") == trrCode
                       select new
                       {
                           ListedDrCode = dr.Field<decimal>("ListedDrCode"),
                           ListedDr_Name = dr.Field<string>("ListedDr_Name"),
                           Doc_Cat_SName = dr.Field<string>("Doc_Cat_SName"),
                           Doc_Special_SName = dr.Field<string>("Doc_Special_SName"),
                           Doc_ClsSName = dr.Field<string>("Doc_ClsSName"),
                           Doc_QuaName = dr.Field<string>("Doc_QuaName"),
                           territory_Name = dr.Field<string>("territory_Name"),
                       };

        if (rsLstdDr.Any())
        {
            grdDoctor.DataSource = rsLstdDr;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = rsLstdDr;
            grdDoctor.DataBind();
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
        string attachment = "attachment; filename=DoctorBusinessValuewise_FieldForce.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}