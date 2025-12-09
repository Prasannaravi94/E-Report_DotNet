using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;

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

        hHeading.InnerText = this.Page.Title;

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
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            ////c1.FindControl("btnBack").Visible = false;
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
                    ddlFFType.Visible = false;
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
            ////c1.FindControl("btnBack").Visible = false;
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
        setValueToChkBoxList();

        if (!Page.IsPostBack)
        {


            ddlDivision.SelectedValue = div_code;
            // menu1.FindControl("btnBack").Visible = false;
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
                //ddlFrmYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
            }
            //ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();

            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();

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
            FillCategory();
        }
        FillColor();
        Lblmain.Visible = false;
    }


    #region FillCategory
    private void FillCategory()
    {
        string sQry = "SELECT Distinct Doc_Cat_Code as Code, Doc_Cat_SName as Short_Name FROM Mas_Doctor_Category " +
            "WHERE Division_Code='" + div_code + "' AND Doc_Cat_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        chkcat.DataSource = dt;
        chkcat.DataTextField = "Short_Name";
        chkcat.DataValueField = "Code";
        chkcat.DataBind();
        setValueToChkBoxList();
    }
    #endregion
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in chkcat.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
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

        if (ddlFFType.SelectedValue.ToString() == "1")
        {

            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

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

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
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

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["sf_type"].ToString() == "admin")
        {
            if (ddlmode.SelectedValue == "5")
            {
                lblMR.Visible = false;
                ddlMR.Visible = false;
            }
            else
            {
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.UserListVacant_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
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


    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_AlphaAll(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "3" || ddlmode.SelectedValue == "9")
        {
            lbltomon.Visible = false;
            //ddlToMonth.Visible = false;
            //lblToYear.Visible = false;
            //ddlToYear.Visible = false;
            lblFrmMoth.Text = "Month-Year";
            txtToMonthYear.Visible = false;
            lblMR.Visible = true;
            ddlMR.Visible = true;
            chkcat.Visible = false;
            lblcat.Visible = false;

        }
        else if (ddlmode.SelectedValue == "4")
        {
            lbltomon.Visible = false;
            //ddlToMonth.Visible = false;
            //lblToYear.Visible = false;
            //ddlToYear.Visible = false;
            lblFrmMoth.Text = "Month-Year";
            txtToMonthYear.Visible = false;
            lblMR.Visible = true;
            ddlMR.Visible = true;
        }
        else if (ddlmode.SelectedValue == "5")
        {
            lbltomon.Visible = true;
            //ddlToMonth.Visible = true;
            //lblToYear.Visible = true;
            //ddlToYear.Visible = true;
            lblFrmMoth.Text = "From Month-Year";
            txtToMonthYear.Visible = true;
            lblMR.Visible = false;
            ddlMR.Visible = false;
        }

        else if (ddlmode.SelectedValue == "8")
        {
            lbltomon.Visible = true;
            //ddlToMonth.Visible = true;
            //lblToYear.Visible = true;
            //ddlToYear.Visible = true;
            lblFrmMoth.Text = "From Month-Year";
            txtToMonthYear.Visible = true;
            lblMR.Visible = false;
            ddlMR.Visible = false;
            SalesForce sf = new SalesForce();

            //AdminSetup adm = new AdminSetup();
            //dsSalesForce = sf.UserList_MGR_SFC(div_code, sf_code);
            //if (dsSalesForce.Tables[0].Rows.Count > 0)
            //{
            //    ddlFieldForce.DataTextField = "sf_name";
            //    ddlFieldForce.DataValueField = "sf_code";
            //    ddlFieldForce.DataSource = dsSalesForce;
            //    ddlFieldForce.DataBind();
            //}
        }
        else
        {
            lbltomon.Visible = true;
            //ddlToMonth.Visible = true;
            //lblToYear.Visible = true;
            //ddlToYear.Visible = true;
            lblFrmMoth.Text = "From Month-Year";
            txtToMonthYear.Visible = true;
            lblMR.Visible = true;
            ddlMR.Visible = true;
            chkcat.Visible = true;
            lblcat.Visible = true;
        }
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
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
        UserLogin astp = new UserLogin();
        string[] FromTxtDate = txtFromMonthYear.Text.Split('-');
        string[] ToTxtDate = txtToMonthYear.Text.Split('-');

        //int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
        //    Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
        //    toName, ddlFrmMonth.SelectedItem.ToString(), ddlFrmYear.SelectedItem.ToString(), ddlToMonth.SelectedItem.ToString(), ddlToYear.SelectedItem.ToString(), Lblmain.Text);
        int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
    Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
    toName, FromTxtDate[0].ToString().Trim(), FromTxtDate[1].ToString().Trim(), ToTxtDate[0].ToString().Trim(), ToTxtDate[1].ToString().Trim(), Lblmain.Text);
    }
}