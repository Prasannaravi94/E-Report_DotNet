using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class MasterFiles_DynamicDashboard_Dashboard : System.Web.UI.MasterPage
{
    String SFCode = String.Empty;
    int divisionCode = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["div_code"] == null || Session["sf_code"] == null)
        {
            Response.Redirect("~/"); // Redirect to default.aspx
            return; // Exit the Page_Load method
        }

        divisionCode = Convert.ToInt32(Session["div_code"]);
        SFCode = Session["sf_code"].ToString();

        if (!IsPostBack)
        {
            var navigationBarWidget = LoadControl("~/UserControl/DynamicDashboardNavbar.ascx");
            DynamicDashboardNavbar.Controls.Add(navigationBarWidget);
        }

    }
}
