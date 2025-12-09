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

public partial class Reports_CallAverage : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    DataSet dsSalesForce = new DataSet();
    DataSet dsdiv = new DataSet();


    DataSet dsTP = new DataSet();
    DataSet dsmgrsf = new DataSet();
    SalesForce sf = new SalesForce();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sf_code = Session["sf_code"].ToString();
            sf_type = Session["sf_type"].ToString();
            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }

            if (!Page.IsPostBack)
            {
                Filldiv();
                //Menu1.Title = Page.Title;
                //Menu1.FindControl("btnBack").Visible = false;
                //string str = DateTime.Now.AddMonths(-1).Month.ToString();
                //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                //ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
                //ddlFrmMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
                //ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
                //  ddlDivision.SelectedIndex = 1;               
                ddlOption.SelectedIndex = 1;
                ddlOption_SelectedIndexChanged1(sender, e);
                ddlDivision_SelectedIndexChanged(sender, e);
                ddlFieldForce.SelectedIndex = 1;

                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
                {
                    UserControl_pnlMenu c1 =
               (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                    Divid.Controls.Add(c1);
                    c1.Title = Page.Title;
                    FillManagers();
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    UserControl_MGR_Menu c1 =
                   (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.Title = Page.Title;
                    // //c1.FindControl("btnBack").Visible = false;
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                //else if (Session["sf_type"].ToString() == "2")
                //{
                //    DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                //    if (DsAudit.Tables[0].Rows.Count > 0)
                //    {
                //        FillMGRLogin();
                //    }
                //    else
                //    {
                //        DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                //        dsmgrsf.Tables.Add(dt);
                //        dsTP = dsmgrsf;

                //        ddlFieldForce.DataTextField = "sf_name";
                //        ddlFieldForce.DataValueField = "sf_code";
                //        ddlFieldForce.DataSource = dsTP;
                //        ddlFieldForce.DataBind();

                //        ddlSF.DataTextField = "desig_Color";
                //        ddlSF.DataValueField = "sf_code";
                //        ddlSF.DataSource = dsTP;
                //        ddlSF.DataBind();

                //    }

                //    FillColor();
                //}
                else if (Session["sf_type"].ToString() == "1")
                {
                    ddlDivision.SelectedValue = Session["division_code"].ToString();
                    //linkcheck.Visible = false;
                    FillManagers();
                    // btnSubmit.Enabled = true;
                    UserControl_MR_Menu c1 =
                   (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.FindControl("btnBack").Visible = false;
                    c1.Title = Page.Title;
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                    ddlFieldForce.Visible = false;
                    lblFilter.Visible = false;
                    chkMgr.Visible = false;
                }

            }
            else
            {
                if (Session["sf_type"].ToString() == "1")
                {
                    UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.FindControl("btnBack").Visible = false;
                    c1.Title = Page.Title;
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                    ddlFieldForce.Visible = false;
                    lblFilter.Visible = false;
                    chkMgr.Visible = false;
                }
                else if (Session["sf_type"].ToString() == "")
                {
                    UserControl_pnlMenu c1 =
                   (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                    Divid.Controls.Add(c1);
                    //c1.FindControl("btnBack").Visible = false;
                    //c1.Title = Page.Title;
                }
                else if (Session["sf_type"].ToString() == "3")
                {
                    UserControl_pnlMenu c1 =
                        (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                    Divid.Controls.Add(c1);
                    //c1.FindControl("btnBack").Visible = false;
                    //c1.Title = Page.Title;
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    UserControl_MGR_Menu c1 =
                    (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    //c1.FindControl("btnBack").Visible = false;
                    //c1.Title = Page.Title;
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;

                }
            }
            setValueToChkBoxList();
            FillColor();
            Lblmain.Visible = false;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "') </script>");
        }
    }
    #region FillSpeciality
    private void FillSpeciality()
    {
        string sQry = "SELECT Distinct Doc_Special_Code as Code, Doc_Special_SName as Short_Name FROM Mas_Doctor_Speciality " +
            "WHERE Division_Code='" + ddlDivision.SelectedValue + "' AND Doc_Special_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();

    }
    #endregion

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }

            }

        }
        else if (sf_type == "2")
        {
            DataSet dsD  = new DataSet();

            dsD = dv.getDivision_Name(Session["div_code"].ToString());
            if (dsD.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsD;
                ddlDivision.DataBind();
            }
        }
        else
        {
            DataSet dsDivision = new DataSet();

            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }

    private void FillMGRLogin()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "2")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 1)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
        }
        else
        {

            dsSalesForce = sf.sp_UserMGRLogin(div_code, sf_code);

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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getUserList_Reporting(ddlDivision.SelectedValue.ToString());
        if (ddlOption.Text == "MonthWise" || ddlOption.Text.Trim() == "Date Wise(for App Approval)" || ddlOption.Text.Trim() == "Date Wise")
        {
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlOption.Text == "Periodically")
        {
            // dsSalesForce = sf.sp_UserList_NameHierarchy(ddlDivision.SelectedValue.ToString(), "admin");
            dsSalesForce = sf.UserList_Hierarchy_New(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlOption.Text == "Periodically All Field Force")
        {
            dsSalesForce = sf.UserList_Hierarchy_New(ddlDivision.SelectedValue.ToString(), "admin");
            // dsSalesForce = sf.sp_UserList_NameHierarchy(ddlDivision.SelectedValue.ToString(), "admin");
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
        FillColor();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        // ddlFFType.Visible = false;
        // ddlAlpha.Visible = false;
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

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
        FillColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillSpeciality();
            setValueToChkBoxList();
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();

            dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue.ToString());
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlFrmYear.Items.Add(k.ToString());
                    //ddlToYear.Items.Add(k.ToString());
                }
                //ddlYear.Text = DateTime.Now.Year.ToString();
                //ddlFrmYear.Text = DateTime.Now.Year.ToString();
                //ddlToYear.Text = DateTime.Now.Year.ToString();

                DateTime MonthValNow = DateTime.Now;
                txtMonthYear.Text = MonthValNow.ToString("MMM") + "-" + DateTime.Now.Year.ToString();

                //from Month-year to Month-year
                DateTime FromMonth = DateTime.Now;
                DateTime ToMonth = DateTime.Now;
                txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
                txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            }
            if (sf_type == "2" || sf_type == "1")
            {
                FillMRManagers();
            }
            else
            {

                FillManagers();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        int FromMonthVal = Convert.ToInt32(Convert.ToDateTime(txtFromMonthYear.Text).Month);
        int FromYearVal = Convert.ToInt32(Convert.ToDateTime(txtFromMonthYear.Text).Year);
        int ToMonthVal = Convert.ToInt32(Convert.ToDateTime(txtToMonthYear.Text).Month);
        int ToYearVal = Convert.ToInt32(Convert.ToDateTime(txtToMonthYear.Text).Year);

        try
        {
            string sURL = "";

            if (ddlOption.Text == "MonthWise")
            {
                getcountspeciality();
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + MonthVal.ToString() + "&cur_year=" + YearVal.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&cbtxt=" + "&Mode=" + ddlOption.Text + "";
            }
            else if (ddlOption.Text == "Periodically")
            {
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&frm_month=" + FromMonthVal.ToString() + "&frm_year=" + FromYearVal.ToString()
                      + "&To_Month=" + ToMonthVal.ToString() + "&To_Year=" + ToYearVal.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                      + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&Mode=" + ddlOption.Text + "";
            }
            else if (ddlOption.Text == "Periodically All Field Force")
            {
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&frm_month=" + FromMonthVal.ToString() + "&frm_year=" + FromYearVal.ToString()
                      + "&To_Month=" + ToMonthVal.ToString() + "&To_Year=" + ToYearVal.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                      + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&Mode=" + ddlOption.Text + "";
            }

            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOption_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "1")
            {

                FillManagers();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMGRLogin();
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

                }

                FillColor();
            }
            if (ddlOption.Text == "MonthWise")
            {
                cbSpeciality.Visible = true;
                pnlMonthly.Visible = true;
                pnlPeriodically.Visible = false;
                pnlDateWise.Visible = false;
                chkMgr.Visible = false;

                FillSpeciality();
                lblMode.Text = "Select Speciality : ";
                btnSubmit.Visible = true;
            }
            else if (ddlOption.Text == "Periodically")
            {
                cbSpeciality.Visible = false;
                pnlMonthly.Visible = false;
                pnlPeriodically.Visible = true;
                pnlDateWise.Visible = false;
                chkMgr.Visible = false;
                lblMode.Visible = false;
            }
            else if (ddlOption.Text == "Periodically All Field Force")
            {
                cbSpeciality.Visible = false;
                pnlMonthly.Visible = false;
                pnlPeriodically.Visible = true;
                pnlDateWise.Visible = false;
                chkMgr.Visible = true;
                lblMode.Visible = false;
                if (Session["sf_type"].ToString() == "1")
                {
                    chkMgr.Visible = false;
                }
            }
            else if (ddlOption.Text == "Date Wise" || ddlOption.Text == "Date Wise(for App Approval)")
            {
                cbSpeciality.Visible = false;
                pnlMonthly.Visible = false;
                pnlPeriodically.Visible = false;
                pnlDateWise.Visible = true;
                chkMgr.Visible = false;
                lblMode.Visible = false;
            }




            //if (Session["sf_type"].ToString() == "2")
            //{
            //    FillMGRLogin();
            //}
            //else
            //{
            //    FillManagers();
            //}
        }
        catch (Exception ex)
        {

        }
    }

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    if (Session["sf_type"].ToString() == "1")
    //    {
    //        FillManagers();
    //    }

    //    else if (Session["sf_type"].ToString() == "2")
    //    {

    //        DataSet dsTP = new DataSet();
    //        DataSet dsmgrsf = new DataSet();
    //        SalesForce sf = new SalesForce();
    //        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
    //        if (DsAudit.Tables[0].Rows.Count > 0)
    //        {
    //            FillMGRLogin();
    //        }
    //        else
    //        {
    //            DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
    //            dsmgrsf.Tables.Add(dt);
    //            dsTP = dsmgrsf;

    //            ddlFieldForce.DataTextField = "sf_name";
    //            ddlFieldForce.DataValueField = "sf_code";
    //            ddlFieldForce.DataSource = dsTP;
    //            ddlFieldForce.DataBind();

    //            ddlSF.DataTextField = "desig_Color";
    //            ddlSF.DataValueField = "sf_code";
    //            ddlSF.DataSource = dsTP;
    //            ddlSF.DataBind();

    //        }

    //        FillColor();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillManagers();
    //    }
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnSubmit.Enabled = true;

    //}

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {

        string toName = string.Empty;
        string sf_name = string.Empty;
        string[] FromMonthYearVal = txtFromMonthYear.Text.Split('-');
        string[] ToMonthYearVal = txtToMonthYear.Text.Split('-');
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "")
        {

            if (ddlFieldForce.SelectedValue.ToString().Contains("MGR"))
            {
                toName = ddlFieldForce.SelectedItem.ToString() + "   (All MR)";
            }
            else
            {
                toName = ddlFieldForce.SelectedItem.ToString();
            }
            UserLogin astp = new UserLogin();



            int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), ddlDivision.SelectedValue.ToString(),
                        ddlDivision.SelectedItem.ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
                       toName, FromMonthYearVal[0].ToString(), FromMonthYearVal[1].ToString(), ToMonthYearVal[0].ToString(), ToMonthYearVal[1].ToString(), Lblmain.Text);
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            UserLogin astp = new UserLogin();
            int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), ddlDivision.SelectedValue.ToString(),
                        ddlDivision.SelectedItem.ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), Session["sf_code"].ToString(),
                        Session["sf_name"].ToString(), FromMonthYearVal[0].ToString(), FromMonthYearVal[1].ToString(), ToMonthYearVal[0].ToString(), ToMonthYearVal[1].ToString(), Lblmain.Text);
        }


    }
    #region setValueToChkBoxList
    private void setValueToChkBoxList()
    {
        try
        {

            foreach (ListItem item in cbSpeciality.Items)
            {
                item.Attributes.Add("cbValue", item.Value);

            }
        }
        catch (Exception)
        {
        }
    }
    #endregion
    private void getcountspeciality()
    {
        int scount = 0;
        foreach (ListItem item in cbSpeciality.Items)
        {
            if (item.Selected)
            {
                scount++;
            }
        }

    }
}
