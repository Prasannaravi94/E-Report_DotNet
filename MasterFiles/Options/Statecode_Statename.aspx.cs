using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_Statecode_Statename : System.Web.UI.Page
{
    string div_code = string.Empty;
    System.Data.DataSet dsSalesForce = new System.Data.DataSet();
    string strSf_Code = string.Empty;
    DataTable dtListed = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;
    string sf_type = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        FillSpl();
    }
    private void FillSpl()
    {

        Holiday holi = new Holiday();
        dsListedDR = holi.Holi_State(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdsaname.DataSource = dsListedDR;
            grdsaname.DataBind();

        }
        else
        {
            grdsaname.DataSource = dsListedDR;
            grdsaname.DataBind();

        }
    }
}