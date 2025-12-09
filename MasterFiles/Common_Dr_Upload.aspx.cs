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

public partial class MasterFiles_Common_Dr_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
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
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
        }
    
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Common_Dr.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Common_Dr.xls");
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}