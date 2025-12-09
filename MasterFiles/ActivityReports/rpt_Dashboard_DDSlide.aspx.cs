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
    string sf_code = string.Empty;
    string sfname = string.Empty;
    string div_code = string.Empty;    
    string fDate = string.Empty;
    string tDate = string.Empty;
    string ModeName = string.Empty;
    int Mode = -1;
    string slide = string.Empty;
    int UsrLike = -2;
    string drSpcCode = string.Empty;
    string drCatCode = string.Empty;
    string drQualCode = string.Empty;
    string drClassCode = string.Empty;
    string drTrrCode = string.Empty;
    string drPrdBrd = string.Empty;
    string sCurrentDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        fDate = Request.QueryString["fDate"].ToString();
        tDate = Request.QueryString["tDate"].ToString();
        Mode = Convert.ToInt32(Request.QueryString["Mode"]);
        slide = Request.QueryString["slide"].ToString();
        UsrLike = Convert.ToInt32(Request.QueryString["UsrLike"]);
        drSpcCode = Request.QueryString["drSpcCode"].ToString();
        drCatCode = Request.QueryString["drCatCode"].ToString();
        drQualCode = Request.QueryString["drQualCode"].ToString();
        drClassCode = Request.QueryString["drClassCode"].ToString();
        drTrrCode = Request.QueryString["drTrrCode"].ToString();
        drPrdBrd = Request.QueryString["drPrdBrd"].ToString();
        //lblRegionName.Text = sfname;
        if (Mode == 1)
        {
            ModeName = " Brand Based ";
        }
        else if(Mode == 0)
        {
            ModeName = " Product Based ";
        }
        lblHead.Text = ModeName +" Listed Doctor Slide Analysis from  " + fDate + " To " + " " + tDate;
        lblFieldForce.Text = "FieldForce Name: " + sfname + "<br /><br />Slide Name: " + slide;

        if (UsrLike == -1)
        {
            FillSF();
        }
        else if (UsrLike != -1 && UsrLike != -2)
        {
            FillDrUsrLikeList();
        }
        if (UsrLike == 0)
        {
            lblFieldForce.Text = lblFieldForce.Text + " - NIL Count";
        }
        else if(UsrLike == 1)
        {
            lblFieldForce.Text = lblFieldForce.Text + " - Like Count";
        }
        else if(UsrLike == 2)
        {
            lblFieldForce.Text = lblFieldForce.Text + " - DisLike Count";
        }
    }

    private void FillSF()
    {
        Product sf = new Product();
        DataSet dsSlide = new DataSet();

        //dsSlide = sf.getSlidesListedDrs(sf_code, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, Mode, slide);
        dsSlide = sf.getSlidesListedDrsNew(sf_code, fDate.ToString(), tDate.ToString(), div_code, 1, slide);

        if (dsSlide.Tables[0].Rows.Count > 0)
        {
            grdDoctor.DataSource = dsSlide;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsSlide;
            grdDoctor.DataBind();
        }
    }
    private void FillDrUsrLikeList()
    {
        Product sf = new Product();
        DataSet dsSlide = new DataSet();

        //dsSlide = sf.getSlidesListedDrsUsrLike(sf_code, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, Mode, slide, UsrLike);
        dsSlide = sf.getSlidesListedDrsUsrLikeNew(sf_code, fDate.ToString(), tDate.ToString(), div_code, 1, slide, UsrLike);

        if (dsSlide.Tables[0].Rows.Count > 0)
        {
            grdDoctor.DataSource = dsSlide;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsSlide;
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