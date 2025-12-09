using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Default_admin : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;  
  
    string sf_type = string.Empty;
    string HO_ID = string.Empty;
    string division_code = string.Empty;
    string div_codeadm = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {     

        if (!Page.IsPostBack)
        {
            // menu.FindControl("btnBack").Visible = false;
        }
    }

}