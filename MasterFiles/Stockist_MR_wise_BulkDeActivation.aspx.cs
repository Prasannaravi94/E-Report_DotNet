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

public partial class MasterFiles_Stockist_MR_wise_BulkDeActivation : System.Web.UI.Page
{

    string div_code = string.Empty;
    string sfCode = string.Empty;
    string Sf_Code_1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();


        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
            Session["SF_Code_N"] = sfCode;
        }

        if (!Page.IsPostBack)
        {
            //  menu1.Title = this.Page.Title;
            //  //// menu1.FindControl("btnBack").Visible = false;
            //  getddlSF_Code();

            Session["GetCmdArgChar"] = "All";
            //  FillDoc_Alpha();

            // FillMR();

        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            // ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            // ddlFieldForce.Enabled = false;

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;



            // Session["backurl"] = "Doctor_Service_CRM_New.aspx";
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = true;
            c1.Title = this.Page.Title;
        }
    }


}

