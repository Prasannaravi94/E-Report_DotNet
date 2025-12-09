using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_DashBoard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsale_Click(object sender, EventArgs e)
    {
        Response.Redirect("SaleAnalysis_Graph.aspx");
    }
    protected void btnSfe_Click(object sender, EventArgs e)
    {
        Response.Redirect("Fw_Analysis_SFE.aspx");
    }
    protected void btnmaster_Click(object sender, EventArgs e)
    {
        Response.Redirect("Doctor_Met.aspx");
    }
    protected void btnmar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Marketing_SFE.aspx");
    }
}