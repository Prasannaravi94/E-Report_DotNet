using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rpt_SecSale_StateandHQwise_Sale_Closing : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string Sf_Code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Sf_Name = string.Empty;
    string ModeOption = string.Empty;
    string OptType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Div_Code = Session["div_code"].ToString();
            Sf_Code = Request.QueryString["sfcode"].ToString();
            Sf_Name = Request.QueryString["Sf_Name"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            ModeOption = Request.QueryString["ModeOpt"].ToString();
            OptType = Request.QueryString["OptType"].ToString();

            Session["Sf_Code_1"] = Sf_Code;
            Session["FMonth"] = FMonth;
            Session["FYear"] = FYear;
            Session["TMonth"] = TMonth;
            Session["TYear"] = TYear;
            Session["Sf_Name_1"] = Sf_Name;
            Session["OptType"] = OptType;
        }
    }
}