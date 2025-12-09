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

public partial class MasterFiles_Reports_rptVacantBlockId : System.Web.UI.Page
{
    string div_code = string.Empty;
    string div_name = string.Empty;
    string division_code = string.Empty;
    DataSet dsVac = new DataSet();
    DataSet dsBlock = new DataSet();
    DataSet dsvacant = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"];
        div_name = Request.QueryString["div_name"];
        //division_code = Session["division_code"].ToString();

        if (!Page.IsPostBack)
        {
            lbldiv.Text = div_name;
            fillHold();
            fillblock();
            fillvacant();
        }
    }

    private void fillHold()
    {
        SalesForce sf = new SalesForce();
        dsVac =sf.getvacant_id_only(div_code);

        if (dsVac.Tables[0].Rows.Count > 0)
        {
            grdvac.Visible = true;
            grdvac.DataSource = dsVac;
            grdvac.DataBind();
        }
        else
        {
            grdvac.DataSource = dsVac;
            grdvac.DataBind();
        }

    }

    private void fillblock()
    {
        SalesForce sf = new SalesForce();
        dsBlock =sf.getblock_id_only(div_code);

        if (dsBlock.Tables[0].Rows.Count > 0)
        {
            grdblock.Visible = true;
            grdblock.DataSource = dsBlock;
            grdblock.DataBind();
        }
        else
        {
            grdblock.DataSource = dsBlock;
            grdblock.DataBind();
        }
    }
    private void fillvacant()
    {
        SalesForce sf = new SalesForce();
        dsvacant = sf.getvacant(div_code);


        if (dsvacant.Tables[0].Rows.Count > 0)
        {
            grdvacant.Visible = true;
            grdvacant.DataSource = dsvacant;
            grdvacant.DataBind();
        }
        else
        {
            grdvacant.DataSource = dsvacant;
            grdvacant.DataBind();
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void grdvac_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            Label lblflag = (Label)e.Row.FindControl("lblflag");

            if (lblflag.Text == "0")
            {
                lblstatus.Text = "Still in Hold";
            }
            else if (lblflag.Text == "1")
            {
                lblstatus.Text = "Hold Released";
            }
        }
    }

    protected void grdblock_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus2 = (Label)e.Row.FindControl("lblstatus2");
            Label lblflag2 = (Label)e.Row.FindControl("lblflag2");

            if (lblflag2.Text == "0")
            {
                lblstatus2.Text = "Still in Block";
            }
            else if (lblflag2.Text == "1")
            {
                lblstatus2.Text = "Block Released";
            }
        }
    }
    protected void grdvacant_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatusv = (Label)e.Row.FindControl("lblstatusv");
            Label lblflagv = (Label)e.Row.FindControl("lblflagv");
            if (lblflagv.Text == "0")
            {
                lblstatusv.Text = "Still in Vacant";
            }
            else if (lblflagv.Text == "1")
            {
                lblstatusv.Text = "Vacant";
            }

        }

    }
}