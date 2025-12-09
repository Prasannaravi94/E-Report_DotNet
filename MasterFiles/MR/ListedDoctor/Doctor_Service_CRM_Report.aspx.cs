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

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Report : System.Web.UI.Page
{

    #region "Variable Declarations"
    DataSet dsYear = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSecSale = new DataSet();
    DataSet dsSalesforce = new DataSet();
    int iErrReturn = -1;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {

           // FillMRManagers();

           // getddlSF_Code();
            FillYear();

           //FillMRManagers();

            FillManagers();
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;

        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
    }

    private void FillManagers()
    {
        DataSet dsSalesForce;
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }


    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        DataSet dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsTerritory;
            ddlFieldForce.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlFieldForce.SelectedIndex = 1;
                string sfCode = ddlFieldForce.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
            }
        }
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        DataSet  dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }

        //FillColor();
    }

    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division
            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlTYear.Items.Add(k.ToString());
                }
            }
            ddlYear.SelectedIndex = 0;
            ddlTYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }
}