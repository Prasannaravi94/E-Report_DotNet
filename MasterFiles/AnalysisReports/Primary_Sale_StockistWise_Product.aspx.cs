using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_AnalysisReports_Primary_Sale_StockistWise_Product : System.Web.UI.Page
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
            //Populate Year dropdown
            FillYear();

            //Populate MR dropdown as per sf_code
            //FillMR();

            //Populate Stockiest dropdown as per sf_code


            div_code = Session["div_code"].ToString();
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            if (Session["sf_type"].ToString() == "1")
            {
                FillMRManagers();
                FillStockiest();
            }

            else if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
                FillStockiest();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                FillMR();
                FillStockiest();
            }

        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

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
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
        }
        Lblmain.Visible = false;


    }

    //Populate the Year dropdown
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
                    //ddlFYear.Items.Add(k.ToString());
                    //ddlTYear.Items.Add(k.ToString());
                }
            }
            //ddlFYear.SelectedIndex = 0;
            //ddlTYear.SelectedIndex = 0;
            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();

        dsSalesforce = sf.UserListTP_Hierarchy_New(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
        }


        //else
        //{
        //    dsSalesforce = sf.UserListTP_Hierarchy(div_code, "admin");
        //    if (dsSalesforce.Tables[0].Rows.Count > 0)
        //    {
        //        ddlFieldForce.DataValueField = "SF_Code";
        //        ddlFieldForce.DataTextField = "Sf_Name";
        //        ddlFieldForce.DataSource = dsSalesforce;
        //        ddlFieldForce.DataBind();


        //    }
        //}

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet dsSalesForce = new DataSet();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {

            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
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




        }



    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStockiest();
    }
    //Populate the Stockiest dropdown based on sf_code
    private void FillStockiest()
    {
        try
        {
            SecSale ss = new SecSale();
            if (Session["sf_type"].ToString() == "1")
            {
                dsSecSale = ss.getStockiest_Sale(ddlFieldForce.SelectedValue.ToString(), div_code);
                if (dsSecSale.Tables[0].Rows.Count > 0)
                {
                    ddlStockiest.DataValueField = "Stockist_Code";
                    ddlStockiest.DataTextField = "Stockist_Name";
                    ddlStockiest.DataSource = dsSecSale;
                    ddlStockiest.DataBind();
                }
            }
            else
            {
                dsSecSale = ss.getStockiest_Mgr_test(div_code, ddlFieldForce.SelectedValue.ToString());
                if (dsSecSale.Tables[0].Rows.Count > 0)
                {
                    ddlStockiest.DataValueField = "Stockist_Code";
                    ddlStockiest.DataTextField = "Name";
                    ddlStockiest.DataSource = dsSecSale;
                    ddlStockiest.DataBind();
                }
            }

            ddlStockiest.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillStockiest()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }



    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    int FYear = Convert.ToInt32(ddlYear.SelectedValue);
    //    int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
    //    int FMonth = Convert.ToInt32(ddlMonth.SelectedValue);
    //    int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);

    //    if (FMonth > TMonth && FYear == TYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
    //        ddlTMonth.Focus();
    //    }
    //    else if (FYear > TYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
    //        ddlTYear.Focus();
    //    }
    //    else
    //    {
    //        if (FYear <= TYear)
    //        {
    //            if (Session["sf_type"].ToString() == "1")
    //            {

    //                btnGo.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "', '" + ddlMonth.SelectedValue.ToString() + "', '" + ddlYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlStockiest.SelectedValue.ToString() + "', '" + rdolstrpt.SelectedValue.ToString() + "' )");
    //            }
    //            else
    //            {
    //                btnGo.Attributes.Add("onclick", "javascript:showModalPopUp('" + ddlFieldForce.SelectedValue.ToString() + "', '" + ddlMonth.SelectedValue.ToString() + "', '" + ddlYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlStockiest.SelectedValue.ToString() + "', '" + rdolstrpt.SelectedValue.ToString() + "' )");
    //            }
    //        }
    //    }
    //}

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    div_code = Session["div_code"].ToString();
    //    if (div_code.Contains(','))
    //    {
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    }
    //    if (Session["sf_type"].ToString() == "1")
    //    {
    //        FillMRManagers();
    //          FillStockiest();
    //    }

    //    else if (Session["sf_type"].ToString() == "2")
    //    {
    //        FillMRManagers();
    //        FillStockiest();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillMR();
    //        FillStockiest();
    //    }
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnSubmit.Enabled = true;

    //}

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
        UserLogin astp = new UserLogin();
        string[] FromMonth = txtFromMonthYear.Text.Split('-');
        string[] ToMonth = txtToMonthYear.Text.Split('-');
        //int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
        //            Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
        //            toName, ddlFMonth.SelectedItem.ToString(), ddlFYear.SelectedItem.ToString(), ddlTMonth.SelectedItem.ToString(), ddlTYear.SelectedItem.ToString(), Lblmain.Text);
        int iRet = astp.Audit_Report_Details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(),
                    Session["div_name"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ddlFieldForce.SelectedValue.ToString(),
                    toName, FromMonth[0].ToString(), FromMonth[1].ToString(), ToMonth[0].ToString(), ToMonth[1].ToString(), Lblmain.Text);
    }
}