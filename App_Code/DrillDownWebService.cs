using Bus_EReport;
using Bus_EReport.DynamicDashboard;
using Bus_EReport.DynamicDashboard.DrillDown;
using Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi;
using Bus_EReport.DynamicDashboard.DrillDown.SalesKpi;
using Bus_EReport.DynamicDashboard.KPIs;
using DBase_EReport;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for DynamicDashboardWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class DrillDownWebService : System.Web.Services.WebService
{
    String SFCode = String.Empty;
    int divisionCode = 0;
    public DrilldownBase Drilldown;
    public DrillDownWebService()
    {

        if (Session["div_code"] == null || Session["sf_code"] == null)
        {
            HttpContext.Current.Response.StatusCode = 401;
        }

        divisionCode = Convert.ToInt32(Session["div_code"]);
        SFCode = Session["sf_code"].ToString();
        
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Marketing(int Start, int End,Dictionary<string, Dictionary<string, string>> Filters)
    {
        if (Filters["widgetFilters"]["measureby"] == "brand_visited" && Filters["widgetFilters"]["viewby"] == "priority")
        {
            Drilldown = new BrandPriorityDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "product_visited" && Filters["widgetFilters"]["viewby"] == "priority")
        {
            Drilldown = new ProductPriorityDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "group_visited" && Filters["widgetFilters"]["viewby"] == "priority")
        {
            Drilldown = new GroupPriorityDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "exposure")
        {
            Drilldown = new ExposureDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "sample_issued")
        {
            Drilldown = new InputOutputDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "doctor_business")
        {
            Drilldown = new DoctorBusinessDrilldown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "digital_detailing")
        {
            Drilldown = new DigitalDetailingDrilldown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "campaign")
        {
            Drilldown = new CampaignDrilldown();
        }
        else if (Filters["widgetFilters"]["viewby"] == "potential_yield")
        {
            Drilldown = new PotentialDrillDown();
        }
        else
        {
            return null;
        }
        Drilldown.Filters =Filters;
        Drilldown.Init(divisionCode, SFCode);
        Drilldown.SetPagination(Start, End);
        Drilldown.SetVisibleColumns();
        return Drilldown.GetRecords();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Sales(int Start, int End, Dictionary<string, Dictionary<string, string>> Filters)
    {
        if (Filters["widgetFilters"]["measureby"] == "primary_sales")
        {
            Drilldown = new PrimarySalesDrillDown();
        }
        else if(Filters["widgetFilters"]["measureby"] == "secondary_sales")
        {
            Drilldown = new SecondarySalesDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "target_primary_sales")
        {
            Drilldown = new TargetPrimarySalesDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "target_secondary_sales")
        {
            Drilldown = new TargetSecondarySalesDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "primary_secondary_sales")
        {
            Drilldown = new PrimarySecondarySalesDrillDown();
        }
        else if (Filters["widgetFilters"]["measureby"] == "target_primary_secondary_sales")
        {
            Drilldown = new TargetPrimarySecondarySalesDrillDown();
        }

        else
        {
            return null;
        }
        Drilldown.Filters = Filters;
        Drilldown.Init(divisionCode, SFCode);
        Drilldown.SetPagination(Start, End);
        Drilldown.SetVisibleColumns();
        return Drilldown.GetRecords();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void saveColumns(string Name,List<string>Values)
    {
        DB_EReporting db_ER = new DB_EReporting();

        var strQry = "INSERT INTO Dynamic_Dashboard_Preferences (DivisionCode,Sfcode,Type,Name,Value) VALUES(" + divisionCode + ", '" + SFCode + "', 'column','" + Name + "','"+ JsonConvert.SerializeObject(Values) + "'); ";
        try
        {
            db_ER.Exec_Scalar("DELETE Dynamic_Dashboard_Preferences WHERE DivisionCode="+divisionCode+ " AND Sfcode='"+SFCode+ "' AND Type='column' AND Name='" + Name + "'");
            db_ER.Exec_Scalar(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}