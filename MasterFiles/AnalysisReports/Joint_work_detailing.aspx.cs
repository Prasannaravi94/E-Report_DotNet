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
using DBase_EReport;
using ClosedXML;

public partial class MasterFiles_AnalysisReports_Joint_work_detailing : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string woslidefwd = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
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

                //FillColor();
                // FillMRManagers1();
                BindDate();
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
                //c1.Title = Page.Title;
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
                c1.FindControl("btnBack").Visible = false;

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
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "desig_color";
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
        SqlCommand cmd = new SqlCommand("SP_FFSPLITDAY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@month", Convert.ToInt32(ddlFrmMonth.SelectedValue.Trim()));
        cmd.Parameters.AddWithValue("@year", Convert.ToInt32(ddlFrmYear.SelectedValue.Trim()));


        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("Desig_Color");
        ds.Tables[0].Columns.Remove("sf_code");
        ds.Tables[0].Columns.Remove("sf_code1");

        dt = ds.Tables[0];
        con.Close();
        int countRow = dt.Rows.Count;
        int countCol = dt.Columns.Count;

        for (int iCol = 0; iCol < countRow; iCol++)
        {
            if (Convert.ToString(ds.Tables[0].Rows[iCol]["1_AFT"]) != "-")
            {
                woslidefwd = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[iCol]["1_AEU"]) - Convert.ToInt32(ds.Tables[0].Rows[iCol]["1_AFT"]));
                dt.Rows[iCol]["1_AGT"] = woslidefwd;
            }
            else
            {
                woslidefwd =  ds.Tables[0].Rows[iCol]["1_AEU"].ToString() ;
                dt.Rows[iCol]["1_AGT"] = woslidefwd;
            }
        }
        woslidefwd = string.Empty;

        for (int iCol = 0; iCol < countCol; iCol++)
        {
            DataColumn col = dt.Columns[iCol];

            if (col.ColumnName == "1_0AA")
            {
                dt.Columns[iCol].ColumnName = "NO: of days in Month";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_AEU")
            {
                dt.Columns[iCol].ColumnName = "NO: of field work days";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_ABT")
            {
                dt.Columns[iCol].ColumnName = "Holiday";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_ACT")
            {
                dt.Columns[iCol].ColumnName = "Sunday";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_ADT")
            {
                dt.Columns[iCol].ColumnName = "Leave";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_AET")
            {
                dt.Columns[iCol].ColumnName = "Non-field work days";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_AFT")
            {
                dt.Columns[iCol].ColumnName = "Slide field work days";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_AGT")
            {
                dt.Columns[iCol].ColumnName = "W/O Slide field work days";
                dt.AcceptChanges();
            }
            else if (col.ColumnName == "1_AHT")
            {
                dt.Columns[iCol].ColumnName = "Pending days";
                dt.AcceptChanges();
            }
        }


        //for (int iCol = 0; iCol < countRow; iCol++)
        //{
        //    if (Convert.ToInt32(ds.Tables[0].Rows[iCol]["1_AFT"]) != 0)
        //    {
        //        woslidefwd = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[iCol]["1_AAT"]) - Convert.ToInt32(ds.Tables[0].Rows[iCol]["1_AFT"]));
        //        dt.Rows[iCol]["1_AGT"] = woslidefwd;
        //    }
        //}


        /**   
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"Dump\"");

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
                dg.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();
            }

        }
    **/
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        var ws = wbook.Worksheets.Add(dt, "ATTENDANCE");
        ws.Row(1).InsertRowsAbove(1);
        ws.Cell(1, 1).Value = "SLIDE WISE - ATTENDANCE OF  -  (" + ddlFieldForce.SelectedItem.Text.Trim() + ") FOR THE MONTH OF " + ddlFrmMonth.SelectedItem.Text + "  -  " + ddlFrmYear.SelectedItem.Text + "";
        ws.Cell(1, 1).Style.Font.Bold = true;
        ws.Cell(1, 1).Style.Font.FontSize = 15;
        //  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        ws.Range("A1:S1").Row(1).Merge();
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"JW_Attendance.xlsx\"");

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