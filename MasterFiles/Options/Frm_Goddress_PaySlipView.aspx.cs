using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using Bus_EReport;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class MasterFiles_AnalysisReports_Frm_Goddress_PaySlipView : System.Web.UI.Page
{
    string SfCode = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string div_code = Session["div_code"].ToString();

        if (!IsPostBack)
        {

            // SfCode = HttpUtility.UrlDecode(Decrypt(Request.QueryString["SF_Code"]));

            SfCode = (Request.QueryString["SF_Code"]);
            Month = (Request.QueryString["Month"]);
            Year = (Request.QueryString["Year"]);

            Session["SF_Code"] = SfCode;
            Session["Month"] = Month;
            Session["Year"] = Year;
        }
    }

}

