using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_DrList_Rpt : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSecSales = null;
    DataSet dsSale = null;
    DataSet dsState = new DataSet();
    DataSet dsReport = null;
    string state_code = string.Empty;
    string sf_code = string.Empty;
    int FMonth = -1;
    int FYear = -1;
    int TMonth = -1;
    int TYear = -1;
    int stock_code = -1;
    int iDay = -1;
    DateTime SelDate;
    string sDate = string.Empty;
    string sf_name = string.Empty;
    int rpttype = -1;
    DataSet dssf = null;
    DataSet dsStock = null;
    DataSet dsRate = new DataSet();

    DataSet dsSub = null;
    string sub_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["Sf_Code"].ToString();
       

        if (!Page.IsPostBack)
        {
            FillProd();
        }
    }

    private void FillProd()
    {
        SecSale dv = new SecSale();
        DataSet dsProd = dv.Get_Dr_CRM_List_Report(sf_code, div_code);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdDrCRM.Visible = true;
            grdDrCRM.DataSource = dsProd;
            grdDrCRM.DataBind();
        }
        else
        {
            grdDrCRM.DataSource = dsProd;
            grdDrCRM.DataBind();
        }
    }
}