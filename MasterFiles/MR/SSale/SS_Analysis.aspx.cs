using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
public partial class MasterFiles_MR_SSale_SS_Analysis : System.Web.UI.Page
{
   
        DataSet dsYear = null;
        string sf_code = string.Empty;
        string div_code = string.Empty;
        DataSet dsState = new DataSet();
        DataSet dsSecSale = new DataSet();
        DataSet dsSalesforce = new DataSet();
        int iErrReturn = -1;
   
        protected void Page_Init(object sender, EventArgs e)
        {
            div_code = Session["div_code"].ToString();
           
        }
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
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
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
                    ddlFYear.Items.Add(k.ToString());
                    ddlTYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
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
   
            dsSalesforce = sf.UserListTP_Hierarchy(div_code, sf_code);
            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataValueField = "SF_Code";
                ddlFieldForce.DataTextField = "Sf_Name";
                ddlFieldForce.DataSource = dsSalesforce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "desig_color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesforce;
                ddlSF.DataBind();

            }

            FillColor();
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


            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();



        }

        FillColor();

    }
   
    //Populate the Stockiest dropdown based on sf_code
   



    protected void linkcheck_Click(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (Session["sf_type"].ToString() == "1")
        {
            FillMRManagers();
            
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            FillMRManagers();
         
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            FillMRManagers();
            
        }
        ddlFieldForce.Visible = true;
        linkcheck.Visible = false;
       // txtNew.Visible = true;
        btnSubmit.Enabled = true;
       
    }
}