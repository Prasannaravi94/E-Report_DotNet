using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using System.IO;
using System.Data.Sql;
public partial class MIS_Reports_ProductExpBrandWiseAnalysis : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillColor();
                FillMRManagers();
                BindDate();
                FillColor();
                FillMRManagers2();

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

                FillColor();
                FillMRManagers();
                BindDate();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                //div_code = Session["division_code"].ToString();
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

        FillColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


    }
    private void FillMRManagers2()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
        FillColor();


    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());

            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //SqlCommand cmd = new SqlCommand("Product_Exposure_Analysis", con);
        //SqlCommand cmd = new SqlCommand("Product_Exposure_Analysis_wmic", con);//with transaction id
		
		//left join by detailing table
        //SqlCommand cmd = new SqlCommand("Product_Exposure_Analysis_wmic_test", con);//with string agged for product sesc issue corrected
		//inner join by detailing table 
		SqlCommand cmd = new SqlCommand("Product_Exposure_Analysis_wmic_test_Dev", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@divcode", div_code);
        cmd.Parameters.AddWithValue("@sfcode", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@month", Convert.ToInt32(ddlFrmMonth.SelectedValue));
        cmd.Parameters.AddWithValue("@year", Convert.ToInt32(ddlFrmYear.SelectedValue));
        //cmd.Parameters.AddWithValue("@Self", 1);
        //
        //string sDate = "";
        //if (ddlFrmMonth.SelectedValue == "12")
        //    sDate = "01-01-" + (Convert.ToInt32(ddlFrmYear.SelectedValue) + 1).ToString();
        //else
        //    sDate = (Convert.ToInt32(ddlFrmMonth.SelectedValue) + 1).ToString() + "-01-" + ddlFrmYear.SelectedValue;
        ////
        //if (sDate.Substring(0, 2).Contains("-"))
        //    sDate = "0" + sDate;
        ////
        //cmd.Parameters.AddWithValue("@cDate", sDate);
        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        ds.Tables[0].Columns.Remove("Fieldforce Code");
        //ds.Tables[0].Columns.Remove("starttime");
        //ds.Tables[0].Columns.Remove("endtime");
        //ds.Tables[0].Columns.Remove("ListedDrCode");
        //ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("trans_detail_info_code");
        //ds.Tables[0].Columns.Remove("clr");
        //ds.Tables[0].Columns.Remove("trans_slno");
        //ds.Tables[0].Columns.Remove("DAY");

        con.Close();
        dt = ds.Tables[0];

        //Developed by JASMINE
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SKU_Dump.xls"));
        //Response.ContentType = "application/ms-excel";
        ////DataTable dt = BindDatatable();
        //string str = string.Empty;
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName);
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]));
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}
        //Response.End();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=SKU_Dump.csv");
        Response.Charset = "";
        Response.ContentType = "text/csv";

        using (StringWriter sw = new StringWriter())
        {

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write("\"" + dt.Columns[i].ColumnName.Replace("\"", "\"\"") + "\"");
                if (i < dt.Columns.Count - 1)
                    sw.Write(",");
            }
            sw.Write("\r\n");


            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string field = row[i].ToString().Replace("\"", "\"\"");
                    sw.Write("\"" + field + "\"");
                    if (i < dt.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.Write("\r\n");
            }

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        /*
        // ExportDataSetToExcel(ds);      
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"Input\"");

        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.Font.Name = "Verdana";
                dg.Font.Size = 10;
                dg.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                dg.HeaderStyle.BackColor = System.Drawing.Color.Chocolate;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();
            }
        } */

        //ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        ////  wbook.Worksheets.Add(dt, "Chemist");

        //var ws = wbook.Worksheets.Add(dt, "SKU");
        //ws.Row(1).InsertRowsAbove(1);
        //ws.Cell(1, 1).Value = "SKU Dump (  " + ddlFieldForce.SelectedItem.Text.Trim() + " )";
        //ws.Cell(1, 1).Style.Font.Bold = true;
        //ws.Cell(1, 1).Style.Font.FontSize = 15;
        ////  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        //ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        //ws.Range("A1:K1").Row(1).Merge();
        //// Prepare the response
        //HttpResponse httpResponse = Response;
        //httpResponse.Clear();
        //httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        ////Provide you file name here
        //httpResponse.AddHeader("content-disposition", "attachment;filename=\"SKU_Dump.xlsx\"");

        //// Flush the workbook to the Response.OutputStream
        //using (MemoryStream memoryStream = new MemoryStream())
        //{
        //    wbook.SaveAs(memoryStream);
        //    memoryStream.WriteTo(httpResponse.OutputStream);
        //    memoryStream.Close();
        //}

        //httpResponse.End();
    }
}