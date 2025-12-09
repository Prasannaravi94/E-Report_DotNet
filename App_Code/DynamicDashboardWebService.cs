using Bus_EReport;
using Bus_EReport.DynamicDashboard;
using Bus_EReport.DynamicDashboard.KPIs;
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
public class DynamicDashboardWebService : System.Web.Services.WebService
{
    String SFCode = String.Empty;
    int divisionCode = 0;
    public DynamicDashboardWebService()
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
    public DynamicDashboardFormModel SaveDashboard(DynamicDashboardFormModel FormData)
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        bool isDashboardNameUnique = DynamicDashboardModel.IsDashboardNameUnique(FormData,divisionCode);

        if (!isDashboardNameUnique)
        {
            // Add a validation error indicating the dashboard name is not unique
            FormData.ValidationErrors["dashboard_name"] = "Dashboard name already exists";
            return FormData;
        }

        FormData.Id = DynamicDashboardModel.SaveDashboard(FormData, SFCode,divisionCode);
        return FormData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardViewModel> GetDashboards()
    {
        List<DynamicDashboardViewModel> result = new List<DynamicDashboardViewModel>();
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        DataSet dashboards =DynamicDashboardModel.GetDashboards(divisionCode);
        if (dashboards.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow Row in dashboards.Tables[0].Rows)
            {
                DynamicDashboardViewModel dashboard = new DynamicDashboardViewModel();
                dashboard.Id = Convert.ToInt32(Row["Id"]);
                dashboard.Name = Row["Name"].ToString();
                dashboard.Module = Row["Module"].ToString();
                dashboard.Widgets = Row["Widgets"].ToString();
                result.Add(dashboard);
            }
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public DynamicDashboardViewModel GetDashboard(int Id)
    {
        DynamicDashboardViewModel result = new DynamicDashboardViewModel();
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        DataSet dashboards = DynamicDashboardModel.GetDashboards(Id);
        if (dashboards.Tables[0].Rows.Count > 0)
        {
            DataRow Row = dashboards.Tables[0].Rows[0];

            result.Id = Convert.ToInt32(Row["Id"]);
            result.Name = Row["Name"].ToString();
            result.Module = Row["Module"].ToString();
            result.Widgets = Row["Widgets"].ToString();
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public bool SaveWidgets(int Id, string Widgets)
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        DynamicDashboardModel.SaveWidgets(Id, Widgets);
        return true;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public WidgetDataOutputModal GetWidgetData(WidgetDataInputModal Data)
    {
        WidgetDataOutputModal WidgetData = new WidgetDataOutputModal();
        if (Data.Module == "master_kpi")
        {
            MasterKpiModel KpiModel = new MasterKpiModel();
            KpiModel.SetDivisionCode(divisionCode);
            KpiModel.SetWidgetDataInput(Data);
            WidgetData = KpiModel.getData();
        }
        else if (Data.Module == "marketing_kpi")
        {
            MarketingKpiModel KpiModel = new MarketingKpiModel();
            KpiModel.SetDivisionCode(divisionCode);
            KpiModel.SetWidgetDataInput(Data);
            WidgetData = KpiModel.getData();
        }
        else if (Data.Module == "sales_kpi")
        {
            SalesKpiModel KpiModel = new SalesKpiModel();
            KpiModel.SetDivisionCode(divisionCode);
            KpiModel.SetWidgetDataInput(Data);
            WidgetData = KpiModel.getData();
        }

        return WidgetData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public bool DeleteDashboard(int Id)
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        DynamicDashboardModel.DeleteDashboard(Id);

        return true;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetBrands()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetBrands(divisionCode));
            
        
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetDoctors()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetDoctors(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetProducts()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetProducts(divisionCode));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetGroups()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetGroups(divisionCode));


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetSubCategories()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetSubCategories(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetSpecialities()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetSpecialities(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetCategories()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetCategories(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetProductCategories()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetProductCategories(divisionCode));

    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetHQs()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetHQs(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetStates()
    {
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        return DynamicDashboardModel.DataSetToOptionsView(DynamicDashboardModel.GetStates(divisionCode));

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DynamicDashboardOptionsViewModal> GetFieldForces()
    {
        SalesForce salesForce = new SalesForce();
        DataSet salesForceData = salesForce.SalesForceListMgrGet(divisionCode.ToString(), SFCode);

        List<DynamicDashboardOptionsViewModal> dynamicDashboardOptionsViewModals = new List<DynamicDashboardOptionsViewModal>();


        if (salesForceData.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow Row in salesForceData.Tables[0].Rows)
            {
                DynamicDashboardOptionsViewModal dynamicDashboardOptionsViewModal = new DynamicDashboardOptionsViewModal();
                dynamicDashboardOptionsViewModal.Id = Row["Sf_Code"].ToString();
                dynamicDashboardOptionsViewModal.Name = Row["Sf_Name"].ToString();
                dynamicDashboardOptionsViewModal.Inactive = "0";

                dynamicDashboardOptionsViewModals.Add(dynamicDashboardOptionsViewModal);
            }
        }
        return dynamicDashboardOptionsViewModals;


    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public bool ChangeDivision(string division)
    {
        if (Session["sf_type"].ToString() == "3")
        {
            string[] strDivSplit = Session["division_code"].ToString().Split(',');
            if (strDivSplit.Contains(division))
            {
                Session["div_code"] = division;
                Division dv = new Division();
                DataSet dsdiv = dv.getDivisionHO(division);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    Session["div_name"] = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }

        return true;

    }
}