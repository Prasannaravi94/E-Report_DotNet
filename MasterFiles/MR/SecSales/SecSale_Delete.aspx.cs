using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;


public partial class MasterFiles_MGR_SecSales_SecSale_Delete : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string MgrRefer = string.Empty;

    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
        
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            // Session["backurl"] = "~/MasterFiles/MGR/MGR_Index.aspx";
            // Session["sf_code"] = sf_code;

            //  Session["Sf_Mgr"] = sf_code;

           

            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {

                if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
                {
                    sf_code = Session["sf_code"].ToString();

                    if (sf_code.Contains("MR"))
                    {
                        SecSale ss = new SecSale();
                        DataSet dsMGR = ss.GetSecSale_MGR(sf_code);

                        if (dsMGR.Tables[0].Rows.Count > 0)
                        {
                            string ReportingTo = dsMGR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            sf_code = ReportingTo;
                            Session["Sf_Mgr"] = sf_code;
                        }
                    }
                    else
                    {
                        Session["Sf_Mgr"] = sf_code;
                    }

                }
                if (Request.QueryString["refer"] != null && Request.QueryString["refer"] != "")
                {
                    MgrRefer = Request.QueryString["refer"];
                    string[] Data = MgrRefer.Split('-');
                    // Session["sf_code"] = Data[0];
                    //sf_code = Data[0];

                }

            }
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            sf_code = Session["sf_code"].ToString();

            
        }

        hHeading.InnerText = Page.Title;
    }
}