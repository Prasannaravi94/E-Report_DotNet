using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Globalization;

public partial class MasterFiles_AnalysisReports_Missed_Call : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSf = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  Menu1.FindControl("btnBack").Visible = false;  

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers1();

                FillColor();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
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
                // FillColor();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                FillManagers();

                FillColor();
                BindDate();
            }

        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }
        //  FillColor();
        Lblmain.Visible = false;
    }
    //private void FillMRManagers1()
    //{
    //    SalesForce sf = new SalesForce();

    //    dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        //ddlSF.DataTextField = "Desig_Color";
    //        //ddlSF.DataValueField = "sf_code";
    //        //ddlSF.DataSource = dsSalesForce;
    //        //ddlSF.DataBind();

    //        //ddlSF.DataTextField = "des_color";
    //        //ddlSF.DataValueField = "sf_code";
    //        //ddlSF.DataSource = dsSalesForce;
    //        //ddlSF.DataBind();
    //    }
    //    FillColor();


    //}
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

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

            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();
        }
        FillColor();


    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");


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
        FillColor();
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
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlFrmYear.Items.Add(k.ToString());
                //ddlToYear.Items.Add(k.ToString());
            }

            //ddlFrmYear.Text = DateTime.Now.Year.ToString();
            //ddlToYear.Text = DateTime.Now.Year.ToString();
            
            //ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }
    private void FillSalesForce()
    {

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "0" || ddlmode.SelectedValue == "2")
        {
            //ddlToMonth.Visible = true;
            //ddlToYear.Visible = true;
            //lblToYear.Visible = true;
            lbltomon.Visible = true;
            txtToMonthYear.Visible = true;
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers1();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }
        }
        else
        {
            //ddlToMonth.Visible = false;
           // ddlToYear.Visible = false;
            //lblToYear.Visible = false;
            lbltomon.Visible = false;
            txtToMonthYear.Visible = false;
            FillSalesForce();

        }

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
            DataSet dsmgrsf = new DataSet();
            DataSet dsTP = new DataSet();
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
        FillColor();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string toName = string.Empty;
        if (ddlFieldForce.SelectedValue.ToString().Contains("MGR"))
        {
            toName = ddlFieldForce.SelectedItem.ToString() + "   (All MR)";
        }
        else
        {
            toName = ddlFieldForce.SelectedItem.ToString();
        }
        string[] FromTxtDate=txtFromMonthYear.Text.Split('-');
        string[] ToTxtDate = txtToMonthYear.Text.Split('-');

        UserLogin astp = new UserLogin();
        //int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
        //Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
        //toName, ddlFrmMonth.SelectedItem.ToString(), ddlFrmYear.SelectedItem.ToString(), ddlToMonth.SelectedItem.ToString(), ddlToYear.SelectedItem.ToString(), Lblmain.Text);
        int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
        Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
        toName, FromTxtDate[0].ToString().Trim(), FromTxtDate[1].ToString().Trim(), ToTxtDate[0].ToString().Trim(), ToTxtDate[1].ToString().Trim(), Lblmain.Text);
    }
}