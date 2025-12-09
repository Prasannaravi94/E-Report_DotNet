using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_MR_Order_Booking_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
           
            //ServerStartTime = DateTime.Now;
            //base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            //FillMR();
            //FillYear();
            FillMRfor_mr();
            getstockist();

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

                //FillMRfor_mr();
                FillYear();

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
               

                FillYear();
                //FillMRfor_mr();

            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

                // FillMRfor_mr();
                FillYear();

            }



        }

        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }


        }
        FillColor();
    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
        }
    }

    private void FillMRManagers()
    {
       
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        dsSalesForce = sf.UserListTP_Hierarchy(div_code, sf_code);

  
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

    private void FillMRfor_mr()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesforce;
            ddlSF.DataBind();

        }
        FillColor();
    }

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        FillMRfor_mr();
    //    }

    //    if (Session["sf_type"].ToString() == "1")
    //    {
    //        FillMRfor_mr();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillMRfor_mr();
    //    }
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnGo.Enabled = true;

    //}

    private void getstockist()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;

        DataSet dsStock = null;

        strQry = " EXEC getstockist_mgr_all '" + div_code + "', '" + ddlFieldForce.SelectedValue + "' ";
        dsStock = db_ER.Exec_DataSet(strQry);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            ddlstockist.DataValueField = "Stockist_Code";
            ddlstockist.DataTextField = "Stockist_Name";
            ddlstockist.DataSource = dsStock;
            ddlstockist.DataBind();
        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstockist();
    }
}