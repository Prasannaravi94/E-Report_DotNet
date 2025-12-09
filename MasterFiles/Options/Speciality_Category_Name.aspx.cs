using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
public partial class MasterFiles_Options_Speciality_Category_Name : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string strSf_Code = string.Empty;
    DataTable dtListed = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
           
            FillSpl();
            FillCat();
            FillCls();
        }
     // hHeading.InnerText = Page.Title;

    }

    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Speciality_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptSpec.DataSource = dsListedDR;
            rptSpec.DataBind();
        }
        else
        {
            rptSpec.DataSource = dsListedDR;
            rptSpec.DataBind();
        }

    }
    private void FillCls()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Class_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptcls.DataSource = dsListedDR;
            rptcls.DataBind();
        }
        else
        {
            rptcls.DataSource = dsListedDR;
            rptcls.DataBind();
        }

    }

    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Category_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptCat.DataSource = dsListedDR;
            rptCat.DataBind();
        }

    }
}