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

public partial class MIS_Reports_SampleGift_details_Exel : System.Web.UI.Page
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
                FillMRManagers1();

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
                c1.FindControl("btnBack").Visible = false;

                FillColor();
                FillMRManagers();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                //ddlFFType.Visible = false;

                FillMRManagers1();
                FillColor();
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
                c1.FindControl("btnBack").Visible = false;

            }
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

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

        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
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
        //SqlCommand cmd = new SqlCommand("Input_GrandTotal_exel_test", con);

        //without job
        //SqlCommand cmd = new SqlCommand("Input_GrandTotal_exel", con);
        //with job
        SqlCommand cmd = new SqlCommand("Input_GrandTotal_exel_Using_Job", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@cmonth", Convert.ToInt32(ddlFrmMonth.SelectedValue));
        cmd.Parameters.AddWithValue("@cyear", Convert.ToInt32(ddlFrmYear.SelectedValue));
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
       ds.Tables[0].Columns.Remove("Rate");
        //ds.Tables[0].Columns.Remove("ListedDrCode");
        ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("trans_detail_info_code");
        //ds.Tables[0].Columns.Remove("clr");
        //ds.Tables[0].Columns.Remove("trans_slno");
        //ds.Tables[0].Columns.Remove("DAY");

        con.Close();
        dt = ds.Tables[0];



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

        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        //  wbook.Worksheets.Add(dt, "Chemist");

        var ws = wbook.Worksheets.Add(dt, "Input");
        ws.Row(1).InsertRowsAbove(1);
        ws.Cell(1, 1).Value = "Input Dump (  " + ddlFieldForce.SelectedItem.Text.Trim() + " )";
        ws.Cell(1, 1).Style.Font.Bold = true;
        ws.Cell(1, 1).Style.Font.FontSize = 15;
        //  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        ws.Range("A1:K1").Row(1).Merge();
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"Input_Dump.xlsx\"");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    }


}