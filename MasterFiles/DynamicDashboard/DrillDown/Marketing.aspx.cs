using Bus_EReport.DynamicDashboard;
using Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

public partial class MasterFiles_DynamicDashboard_DrillDown_Marketing : System.Web.UI.Page
{
    int DivisionCode = 0;
    string Sfcode =string.Empty;
    public DrilldownBase Drilldown;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["div_code"] == null || Session["sf_code"] == null)
        {
            Response.Redirect("~/");
            return;
        }
        DivisionCode = Convert.ToInt32(Session["div_code"]);
        Sfcode = Session["sf_code"].ToString();

        if (Request.QueryString["widgetFilters[measureby]"] == null || Request.QueryString["widgetFilters[viewby]"] == null)
        {
            Response.Redirect("~/");
            return;
        }
        if (Request.QueryString["widgetFilters[measureby]"] == "brand_visited" && Request.QueryString["widgetFilters[viewby]"] == "priority")
        {
            Drilldown = new BrandPriorityDrillDown();
        }else if (Request.QueryString["widgetFilters[measureby]"] == "product_visited" && Request.QueryString["widgetFilters[viewby]"] == "priority")
        {
            Drilldown = new ProductPriorityDrillDown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "group_visited" && Request.QueryString["widgetFilters[viewby]"] == "priority")
        {
            Drilldown = new GroupPriorityDrillDown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "exposure")
        {
            Drilldown = new ExposureDrillDown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "sample_issued")
        {
            Drilldown = new InputOutputDrillDown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "doctor_business")
        {
            Drilldown = new DoctorBusinessDrilldown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "digital_detailing")
        {
            Drilldown = new DigitalDetailingDrilldown();
        }
        else if (Request.QueryString["widgetFilters[measureby]"] == "campaign")
        {
            Drilldown = new CampaignDrilldown();
        }
        else if (Request.QueryString["widgetFilters[viewby]"] == "potential_yield")
        {
            Drilldown = new PotentialDrillDown();
        }
        else
        {
            Response.Redirect("~/MasterFiles/DynamicDashboard/PageNotFound.aspx");
            return; 
        }
        Drilldown.Init(DivisionCode, Sfcode, Request,true);
        if (Request.QueryString["export"] != null)
        {
            DataSet records = Drilldown.Export();
            DataTable dt = records.Tables[0];
            // Set the content type and header for the response
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename="+Drilldown.FileName+".xlsx");

            // Create a new workbook
            var workbook = new XLWorkbook();

            // Add a worksheet
            var worksheet = workbook.Worksheets.Add(Drilldown.FileName);

            
            int row = 1;
            foreach (var caption in Drilldown.Captions)
            {
                worksheet.Cell(row, 1).Value = caption.Key;
                worksheet.Range(row, 2, row, 2 + (Drilldown.VisibleColumns.Count) - 1).Merge();
                worksheet.Cell(row, 2).Value = caption.Value;
                worksheet.Cell(row, 2).RichText.Bold = true;
                row++;
            }
            row++;
            worksheet.Row(row).Style.Fill.BackgroundColor = XLColor.Black;
            
            worksheet.Cell(row, 1).Value = "SL No";
            worksheet.Cell(row, 1).RichText.FontColor = XLColor.White;
            int headerCoulumnCount = 2;
            foreach (var column in Drilldown.VisibleColumns)
            {
                worksheet.Cell(row, headerCoulumnCount).Value = Drilldown.Columns[column].Label;
                worksheet.Cell(row, headerCoulumnCount).RichText.FontColor = XLColor.White;
                headerCoulumnCount++;
            }
            row++;
            int i;
            int slNo = 1;
            foreach (DataRow dr in dt.Rows)
            {
                worksheet.Cell(row, 1).Value = slNo;
                slNo++;
                int CoulumnCount = 2;
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cell(row, CoulumnCount).Value = dr[i].ToString();
                    CoulumnCount++;
                }
                row++;
            }
            // Stream the workbook to the response output
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);

                // Set the position of the stream back to the beginning
                stream.Position = 0;

                // Copy the stream to the response output
                stream.CopyTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}