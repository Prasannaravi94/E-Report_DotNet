using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_AnalysisReports_Listeddr_View_Period : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
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
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
            foreach (ListItem item in ddlmode.Items)
            {
                if ((item.Value) == "8")
                {
                    item.Attributes.Add("style", "display:none;");

                }
            }
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsTP;
                    ddlFieldForce.DataBind();

                    ddlSF.DataTextField = "desig_Color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsTP;
                    ddlSF.DataBind();
                   // ddlFFType.Visible = false;
                }
            }
            foreach (ListItem item in ddlmode.Items)
            {
                if ((item.Value) == "8")
                {
                    item.Attributes.Add("style", "display:none;");

                }
            }
            // lblDivision.Visible = false;
            // ddlDivision.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                Filldiv();             
                FillManagers();
            }

            if (Session["div_code"] != null)
            {
                lblDivision.Visible = false;
                ddlDivision.Visible = false;
            }
        }

        if (!Page.IsPostBack)
        {
            ddlDivision.SelectedValue = div_code;
            // //// menu1.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFrmYear.Items.Add(k.ToString());

                    ddlToYear.Items.Add(k.ToString());
                    
                }
                ddlFrmYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
            }
            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();

            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;
                    getDivision();
                }
                else
                {
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                }
            }
        }
       // FillColor();
    }
      private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        //ddlFFType.Visible = false;
       // ddlAlpha.Visible = false;
        AdminSetup adm = new AdminSetup();
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();


        }
    }

    private void Filldiv()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }
    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_code = ddlDivision.SelectedValue.ToString();
        FillMRManagers();
        FillColor();
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void chkVacant_CheckedChanged(object sender, EventArgs e)
    {
        
        FillManagers();
        FillColor();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
           
        //        ddlAlpha.Visible = false;
        //        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
           
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "2")
        //{
        //    dsSalesForce = sf.UserList_HQ(div_code, "admin");
        //}
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
    private void FillManagers_II_Level()
    {
        SalesForce sf = new SalesForce();
        //dsSf = sf.getSfCode_Second_Level(div_code);
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSf;
            ddlFieldForce.DataBind();

        }

    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["sf_type"].ToString() == "admin")
        {
            if (ddlmode.SelectedValue != "8")
            {
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.SalesForceList_New_GetMr(div_code, ddlFieldForce.SelectedValue.ToString());
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    lblMR.Visible = true;
                    ddlMR.Visible = true;
                    ddlMR.DataTextField = "sf_name";
                    ddlMR.DataValueField = "sf_code";
                    ddlMR.DataSource = dsSalesForce;
                    ddlMR.DataBind();
                    ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
        }
    }
   

   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "3")
        {
            lbltomon.Visible = false;
            ddlToMonth.Visible = false;
            lblToYear.Visible = false;
            ddlToYear.Visible = false;
            if (ViewState["sf_type"].ToString() == "admin")
            {
                lblMR.Visible = true;
                ddlMR.Visible = true;
                FillManagers();
                FillColor();
            }
            else
            {
                lblMR.Visible = false;
                ddlMR.Visible = false;
                FillMRManagers();
            }

        }
        else if (ddlmode.SelectedValue == "4")
        {
            lbltomon.Visible = false;
            ddlToMonth.Visible = false;
            lblToYear.Visible = false;
            ddlToYear.Visible = false;
            if (ViewState["sf_type"].ToString() == "admin")
            {
                lblMR.Visible = true;
                ddlMR.Visible = true;
                FillManagers();
                FillColor();
            }
            else
            {
                lblMR.Visible = false;
                ddlMR.Visible = false;
                FillMRManagers();
            }
        }
        else if (ddlmode.SelectedValue == "8")
        {

            lbltomon.Visible = true;
            ddlToMonth.Visible = true;
            lblToYear.Visible = true;
            ddlToYear.Visible = true;
            lblMR.Visible = false;
            ddlMR.Visible = false;
            FillManagers_II_Level();
        }
        else
        {
            lbltomon.Visible = true;
            ddlToMonth.Visible = true;
            lblToYear.Visible = true;
            ddlToYear.Visible = true;
            if (ViewState["sf_type"].ToString() == "admin")
            {
                lblMR.Visible = true;
                ddlMR.Visible = true;
                FillManagers();
                FillColor();
            }
            else
            {
                lblMR.Visible = false;
                ddlMR.Visible = false;
                FillMRManagers();
            }
            FillColor();
        }
    }
}