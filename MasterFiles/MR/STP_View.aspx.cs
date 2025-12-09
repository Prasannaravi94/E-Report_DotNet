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


public partial class MasterFiles_MR_STP_View : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string day = string.Empty;
    string sf_name = string.Empty;
    DataTable dsDr = new DataTable();
    DataTable dataTable = new DataTable();
    DataTable  dsDr2 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sf_name = Session["sf_name"].ToString();
      

        lblccp.Text = "STP - View ";
        lblname.Text = sf_name;

        if (!Page.IsPostBack)
        {
            


            FillDr();

        }

    }

    private void FillDr()
    {
        TP_New dr = new TP_New();
        lblname.Text = sf_name;


        dsDr = dr.STP_DR_VIEW(sf_code, div_code);

        if (dsDr.Rows.Count > 0)
        {
         

          dataTable = GetFilteredTable(
           dsDr, "day_plan_name='Monday 1' ");
           grddr_1.Visible = true;
           grddr_1.DataSource = dataTable;
           grddr_1.DataBind();

           dataTable = GetFilteredTable(
         dsDr, "day_plan_name='Monday 2' ");
           grddr_2.Visible = true;
           grddr_2.DataSource = dataTable;
           grddr_2.DataBind();

           dataTable = GetFilteredTable(
        dsDr, "day_plan_name='Monday 3' ");
           grddr_3.Visible = true;
           grddr_3.DataSource = dataTable;
           grddr_3.DataBind();

           dataTable = GetFilteredTable(
     dsDr, "day_plan_name='Monday 4' ");
           grddr_4.Visible = true;
           grddr_4.DataSource = dataTable;
           grddr_4.DataBind();
        

         dataTable = GetFilteredTable(
     dsDr, "day_plan_name='Tuesday 1' ");
           grddr_t1.Visible = true;
           grddr_t1.DataSource = dataTable;
           grddr_t1.DataBind();

           dataTable = GetFilteredTable(
 dsDr, "day_plan_name='Tuesday 2' ");
           grddr_t2.Visible = true;
           grddr_t2.DataSource = dataTable;
           grddr_t2.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Tuesday 3' ");
           grddr_t3.Visible = true;
           grddr_t3.DataSource = dataTable;
           grddr_t3.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Tuesday 4' ");
           grddr_t4.Visible = true;
           grddr_t4.DataSource = dataTable;
           grddr_t4.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Wednesday 1' ");
           grddr_w1.Visible = true;
           grddr_w1.DataSource = dataTable;
           grddr_w1.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Wednesday 2' ");
           grddr_w2.Visible = true;
           grddr_w2.DataSource = dataTable;
           grddr_w2.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Wednesday 3' ");
           grddr_w3.Visible = true;
           grddr_w3.DataSource = dataTable;
           grddr_w3.DataBind();


           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Wednesday 4' ");
           grddr_w4.Visible = true;
           grddr_w4.DataSource = dataTable;
           grddr_w4.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Thursday 1' ");
           grddr_th1.Visible = true;
           grddr_th1.DataSource = dataTable;
           grddr_th1.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Thursday 2' ");
           grddr_th2.Visible = true;
           grddr_th2.DataSource = dataTable;
           grddr_th2.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Thursday 3' ");
           grddr_th3.Visible = true;
           grddr_th3.DataSource = dataTable;
           grddr_th3.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Thursday 4' ");
           grddr_th4.Visible = true;
           grddr_th4.DataSource = dataTable;
           grddr_th4.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Friday 1' ");
           grddr_f1.Visible = true;
           grddr_f1.DataSource = dataTable;
           grddr_f1.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Friday 2' ");
           grddr_f2.Visible = true;
           grddr_f2.DataSource = dataTable;
           grddr_f2.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Friday 3' ");
           grddr_f3.Visible = true;
           grddr_f3.DataSource = dataTable;
           grddr_f3.DataBind();


           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Friday 4' ");
           grddr_f4.Visible = true;
           grddr_f4.DataSource = dataTable;
           grddr_f4.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Saturday 1' ");
           grddr_sa1.Visible = true;
           grddr_sa1.DataSource = dataTable;
           grddr_sa1.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Saturday 2' ");
           grddr_sa2.Visible = true;
           grddr_sa2.DataSource = dataTable;
           grddr_sa2.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Saturday 3' ");
           grddr_sa3.Visible = true;
           grddr_sa3.DataSource = dataTable;
           grddr_sa3.DataBind();

           dataTable = GetFilteredTable(
dsDr, "day_plan_name='Saturday 4' ");
           grddr_sa4.Visible = true;
           grddr_sa4.DataSource = dataTable;
           grddr_sa4.DataBind();

        }




       dsDr2 = dr.STP_CHEM_VIEW(sf_code, div_code);
      if (dsDr2.Rows.Count > 0)
        {
            dataTable = GetFilteredTable(
         dsDr2, "day_plan_name='Monday 1' ");
            grdchem_1.Visible = true;
            grdchem_1.DataSource = dataTable;
            grdchem_1.DataBind();

            dataTable = GetFilteredTable(
          dsDr2, "day_plan_name='Monday 2' ");
            grdchem_2.Visible = true;
            grdchem_2.DataSource = dataTable;
            grdchem_2.DataBind();

            dataTable = GetFilteredTable(
         dsDr2, "day_plan_name='Monday 3' ");
            grdchem_3.Visible = true;
            grdchem_3.DataSource = dataTable;
            grdchem_3.DataBind();

            dataTable = GetFilteredTable(
      dsDr2, "day_plan_name='Monday 4' ");
            grdchem_4.Visible = true;
            grdchem_4.DataSource = dataTable;
            grdchem_4.DataBind();


            dataTable = GetFilteredTable(
        dsDr2, "day_plan_name='Tuesday 1' ");
            grdchem_t1.Visible = true;
            grdchem_t1.DataSource = dataTable;
            grdchem_t1.DataBind();

            dataTable = GetFilteredTable(
  dsDr2, "day_plan_name='Tuesday 2' ");
            grdchem_t2.Visible = true;
            grdchem_t2.DataSource = dataTable;
            grdchem_t2.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Tuesday 3' ");
            grdchem_t3.Visible = true;
            grdchem_t3.DataSource = dataTable;
            grdchem_t3.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Tuesday 4' ");
            grdchem_t4.Visible = true;
            grdchem_t4.DataSource = dataTable;
            grdchem_t4.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Wednesday 1' ");
            grdchem_w1.Visible = true;
            grdchem_w1.DataSource = dataTable;
            grdchem_w1.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Wednesday 2' ");
            grdchem_w2.Visible = true;
            grdchem_w2.DataSource = dataTable;
            grdchem_w2.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Wednesday 3' ");
            grdchem_w3.Visible = true;
            grdchem_w3.DataSource = dataTable;
            grdchem_w3.DataBind();


            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Wednesday 4' ");
            grdchem_w4.Visible = true;
            grdchem_w4.DataSource = dataTable;
            grdchem_w4.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Thursday 1' ");
            grdchem_th1.Visible = true;
            grdchem_th1.DataSource = dataTable;
            grdchem_th1.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Thursday 2' ");
            grdchem_th2.Visible = true;
            grdchem_th2.DataSource = dataTable;
            grdchem_th2.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Thursday 3' ");
            grdchem_th3.Visible = true;
            grdchem_th3.DataSource = dataTable;
            grdchem_th3.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Thursday 4' ");
            grdchem_th4.Visible = true;
            grdchem_th4.DataSource = dataTable;
            grdchem_th4.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Friday 1' ");
            grdchem_f1.Visible = true;
            grdchem_f1.DataSource = dataTable;
            grdchem_f1.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Friday 2' ");
            grdchem_f2.Visible = true;
            grdchem_f2.DataSource = dataTable;
            grdchem_f2.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Friday 3' ");
            grdchem_f3.Visible = true;
            grdchem_f3.DataSource = dataTable;
            grdchem_f3.DataBind();


            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Friday 4' ");
            grdchem_f4.Visible = true;
            grdchem_f4.DataSource = dataTable;
            grdchem_f4.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Saturday 1' ");
            grdchem_sa1.Visible = true;
            grdchem_sa1.DataSource = dataTable;
            grdchem_sa1.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Saturday 2' ");
            grdchem_sa2.Visible = true;
            grdchem_sa2.DataSource = dataTable;
            grdchem_sa2.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Saturday 3' ");
            grdchem_sa3.Visible = true;
            grdchem_sa3.DataSource = dataTable;
            grdchem_sa3.DataBind();

            dataTable = GetFilteredTable(
 dsDr2, "day_plan_name='Saturday 4' ");
            grdchem_sa4.Visible = true;
            grdchem_sa4.DataSource = dataTable;
            grdchem_sa4.DataBind();
        }

      
    }

    public static DataTable GetFilteredTable(
  DataTable sourceTable, string selectFilter)
    {
        var filteredTable = sourceTable.Clone();
        var rows = sourceTable.Select(selectFilter);
        foreach (DataRow row in rows)
        {
            filteredTable.ImportRow(row);
        }
        return filteredTable;
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

}