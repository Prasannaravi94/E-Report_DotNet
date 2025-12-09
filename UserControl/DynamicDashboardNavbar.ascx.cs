using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using Bus_EReport.DynamicDashboard;
public partial class UserControl_DynamicDashboardNavbar : System.Web.UI.UserControl
{
    String SFCode = String.Empty;
    int divisionCode = 0;
    public DataSet MasterKpiDashboards { get; set; }
    Uri currentUrl = HttpContext.Current.Request.Url;
    protected void Page_Load(object sender, EventArgs e)
    {
        divisionCode = Convert.ToInt32(Session["div_code"]);
        SFCode = Session["sf_code"].ToString();

        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();

        if (!IsPostBack)
        {
            DataSet MasterKpiDashboards = DynamicDashboardModel.GetDashboards(divisionCode, 0, "master_kpi");
            MasterKpiLinks.DataSource = ConvertDataSetToLinkData(MasterKpiDashboards);
            MasterKpiLinks.DataBind();

            DataSet MarKetingKpiDashboards = DynamicDashboardModel.GetDashboards(divisionCode, 0, "marketing_kpi");
            MarketingKpiLinks.DataSource = ConvertDataSetToLinkData(MarKetingKpiDashboards);
            MarketingKpiLinks.DataBind();

            DataSet SalesKpiDashboards = DynamicDashboardModel.GetDashboards(divisionCode, 0, "sales_kpi");
            SalesKpiLinks.DataSource = ConvertDataSetToLinkData(SalesKpiDashboards);
            SalesKpiLinks.DataBind();
        }

    }
    public class LinkData
    {
        public string LinkUrl { get; set; }
        public string LinkText { get; set; }
        public string LinkId { get; set; }
    }

    private List<LinkData> ConvertDataSetToLinkData(DataSet dataSet)
    {
        List<LinkData> links = new List<LinkData>();

        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            string dashboardId = row["Id"].ToString(); // Replace with the actual column name
            string dashboardName = row["Name"].ToString(); // Replace with the actual column name
            string baseUrl = currentUrl.GetLeftPart(UriPartial.Authority);
            string linkUrl = baseUrl + "/MasterFiles/DynamicDashboard/Dashboard.aspx?id=" + dashboardId;
            string linkText = dashboardName;

            links.Add(new LinkData { LinkUrl = linkUrl, LinkText = linkText, LinkId =dashboardId });
        }

        return links;
    }
}