using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_AnalysisReports_Discipline_Coverage_Report : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    int mode = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                FillMRManagers();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
           
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
        }
        else
        {
            ViewState["sf_type"] = "admin";
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                FillManagers();
            }
        }


        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlFYear.Items.Add(k.ToString());
                    //ddlTYear.Items.Add(k.ToString());
                    //ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                    //ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

            //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();

            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
           
        }
        FillColor();

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
      
         DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
         if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
         {
             dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
             if (dsSalesForce.Tables[0].Rows.Count > 0)
             {
                 ddlFieldForce.DataTextField = "sf_name";
                 ddlFieldForce.DataValueField = "sf_code";
                 ddlFieldForce.DataSource = dsSalesForce;
                 ddlFieldForce.DataBind();

                 ddlSF.DataTextField = "Desig_Color";
                 ddlSF.DataValueField = "sf_code";
                 ddlSF.DataSource = dsSalesForce;
                 ddlSF.DataBind();


             }
         }
         else
         {
             // Fetch Managers Audit Team
             DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
             dsmgrsf.Tables.Add(dt);
             dsTP = dsmgrsf;

             ddlFieldForce.DataTextField = "sf_name";
             ddlFieldForce.DataValueField = "sf_code";
             ddlFieldForce.DataSource = dsTP;
             ddlFieldForce.DataBind();

             ddlSF.DataTextField = "Des_Color";
             ddlSF.DataValueField = "sf_code";
             ddlSF.DataSource = dsTP;
             ddlSF.DataBind();

         }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
   

  
    private void FillManagers()
    {
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

   

}