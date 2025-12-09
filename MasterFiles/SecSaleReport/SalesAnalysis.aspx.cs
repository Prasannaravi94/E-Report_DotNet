using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_SecSaleReport_SalesAnalysis : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DataSet dsSecSale = null;
    DataSet dsProduct = null;
    DataSet dsStateName = new DataSet();
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string strSt_Code = string.Empty;
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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            // menu1.FindControl("btnBack").Visible = false;
            if (Session["sf_type"].ToString() == "1")
            {
                //UserControl_MR_TP_Menu c1 =
                //    (UserControl_MR_TP_Menu)LoadControl("~/UserControl/MR_TP_Menu.ascx");
                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
                FillMRManagers();
                // ddlFieldForce.SelectedValue = sf_code;
                //   ddlFieldForce.Enabled = false;
                FillYear();
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "6")
                    {
                        item.Attributes.Add("style", "display:none;");

                    }
                }

                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.Add("style", "display:none;");
                        }

                    }
                }

                getStateName(sf_code);

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                // UserControl_MGR_TP_Menu c1 =
                //(UserControl_MGR_TP_Menu)LoadControl("~/UserControl/MGR_TP_Menu.ascx");
                UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillYear();
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "6")
                    {
                        item.Attributes.Add("style", "display:none;");

                    }
                }

                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.Add("style", "display:none;");
                        }

                    }
                }

                FillMRManagers();
                FillColor();

            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                //div_code = Session["div_code"].ToString();
                // UserControl_MenuUserControl_TP c1 =
                //(UserControl_MenuUserControl_TP)LoadControl("~/UserControl/MenuUserControl_TP.ascx");
                UserControl_MenuUserControl c1 =
                 (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                FillManagers();
                //   FillStockiest();
                FillColor();
                FillYear();
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.Add("style", "display:none;");

                        }

                    }
                }

            }


        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                //UserControl_MR_TP_Menu c1 =
                //    (UserControl_MR_TP_Menu)LoadControl("~/UserControl/MR_TP_Menu.ascx");
                UserControl_MR_Menu c1 = (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;

                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "6")
                    {
                        item.Attributes.Add("style", "display:none;");

                    }
                }
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.Add("style", "display:none;");
                        }

                    }
                }


            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                // UserControl_MGR_TP_Menu c1 =
                //(UserControl_MGR_TP_Menu)LoadControl("~/UserControl/MGR_TP_Menu.ascx");
                UserControl_MGR_Menu c1 =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "6")
                    {
                        item.Attributes.Add("style", "display:none;");

                    }
                }
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.Add("style", "display:none;");
                        }

                    }
                }

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                // UserControl_MenuUserControl_TP c1 =
                //(UserControl_MenuUserControl_TP)LoadControl("~/UserControl/MenuUserControl_TP.ascx");
                UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                foreach (ListItem item in ddltype.Items)
                {
                    if ((item.Value) == "7")
                    {
                        if (div_code != "43" && div_code != "49" && div_code != "48")
                        {
                            item.Attributes.CssStyle.Add("style", "display:none;");
                        }
                    }
                }

            }
        }
        setValueToChkBoxList();
        FillColor();
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStockiest();
        getStateName();
    }
    private void getStateName()
    {

        if (ddltype.SelectedValue == "2" || ddltype.SelectedValue == "4")
        {
            SalesForce sf = new SalesForce();
            dsStateName = sf.SalesForce_State_Get_SS(div_code, ddlFieldForce.SelectedValue);
            dsStateName.Tables[0].Rows[1].Delete();
            if (dsStateName.Tables[0].Rows.Count > 0)
            {
                ddlStateName.DataTextField = "StateName";
                ddlStateName.DataValueField = "StateName";
                ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
                ddlStateName.DataBind();

            }
        }
        else
        {
            SalesForce sf = new SalesForce();
            dsStateName = sf.SalesForce_State_Get_SS(div_code, ddlFieldForce.SelectedValue);

            if (dsStateName.Tables[0].Rows.Count > 0)
            {
                ddlStateName.DataTextField = "StateName";
                ddlStateName.DataValueField = "StateName";
                ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
                ddlStateName.DataBind();

            }
        }
    }

    private void getStateName(string sf_code)
    {

        if (ddltype.SelectedValue == "2" || ddltype.SelectedValue == "4")
        {
            SalesForce sf = new SalesForce();
            dsStateName = sf.SalesForce_State_Get_SS(div_code, sf_code);
            dsStateName.Tables[0].Rows[1].Delete();
            if (dsStateName.Tables[0].Rows.Count > 0)
            {
                ddlStateName.DataTextField = "StateName";
                ddlStateName.DataValueField = "StateName";
                ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
                ddlStateName.DataBind();

            }
        }
        else
        {
            SalesForce sf = new SalesForce();
            dsStateName = sf.SalesForce_State_Get_SS(div_code, sf_code);

            if (dsStateName.Tables[0].Rows.Count > 0)
            {
                ddlStateName.DataTextField = "StateName";
                ddlStateName.DataValueField = "StateName";
                ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
                ddlStateName.DataBind();

            }
        }
    }

    private void FillStockiest()
    {

        SecSale ss = new SecSale();
        if (Session["sf_type"].ToString() == "1")
        {
            dsSecSale = ss.getStockiest_Sale(sf_code, div_code);
            if (dsSecSale.Tables[0].Rows.Count > 0)
            {
                ddlStockiest.DataValueField = "Stockist_Code";
                ddlStockiest.DataTextField = "Stockist_Name";
                ddlStockiest.DataSource = dsSecSale;
                ddlStockiest.DataBind();
            }
            ddlStockiest.SelectedIndex = 0;
        }
        else
        {
            if (ddltype.SelectedValue == "2")
            {
                if (ddlStateName.SelectedItem.Text.Trim() == "--ALL--")
                {
                    dsSecSale = ss.getStockiest_Mgr(div_code, ddlFieldForce.SelectedValue.ToString());
                    if (dsSecSale.Tables[0].Rows.Count > 0)
                    {
                        ddlStockiest.DataValueField = "Stockist_Code";
                        ddlStockiest.DataTextField = "Name";
                        ddlStockiest.DataSource = dsSecSale;
                        ddlStockiest.DataBind();
                    }
                    ddlStockiest.SelectedIndex = 0;
                }
                else
                {
                    dsSecSale = ss.getStockiest_Statewise(div_code, ddlFieldForce.SelectedValue.ToString(), ddlStateName.SelectedItem.Text.Trim());
                    if (dsSecSale.Tables[0].Rows.Count > 0)
                    {
                        ddlStockiest.DataValueField = "Stockist_Code";
                        ddlStockiest.DataTextField = "Name";
                        ddlStockiest.DataSource = dsSecSale;
                        ddlStockiest.DataBind();
                    }
                    ddlStockiest.SelectedIndex = 0;
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
                ddlStockiest.SelectedIndex = 0;
            }
        }



    }

    private void FillYear()
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

        dsSalesForce = sf.Hirarchy_UserList_All(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //   ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

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
            ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));


            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
        FillColor();


    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
    }
    private void Fill_Product_Name()
    {
        Product pr = new Product();
        dsProduct = pr.getProduct_Sale(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlprod.DataTextField = "Product_Detail_Name";
            ddlprod.DataValueField = "Product_Detail_Code";
            ddlprod.DataSource = dsProduct;
            ddlprod.DataBind();
        }
        else
        {
            ddlprod.DataSource = dsProduct;
            ddlprod.DataBind();
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddltype.SelectedValue == "1")
        {
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lbltomon.Text = "To Month-Year";
            lbltomon.Visible = true;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = true;
            ddlStateName.Visible = true;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }

        }
        else if (ddltype.SelectedValue == "3")
        {


            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lbltomon.Text = "To Month-Year";
            lblFrmMoth.Text = "From Month-Year";
            lbltomon.Visible = true;
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = true;
            chkProduct.Visible = true;
            lblst.Visible = false;
            ddlStateName.Visible = false;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }
            FillProduct();
        }
        else if (ddltype.SelectedValue == "2")
        {
            lblStockiest.Visible = true;
            ddlStockiest.Visible = true;
            FillStockiest();
            foreach (ListItem item in ddlStockiest.Items)
            {
                if (item.Value == "-2")
                {
                    item.Attributes.Add("style", "color:Red");
                    break;
                }

            }
            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lbltomon.Text = "To Month-Year";
            lblFrmMoth.Text = "From Month-Year";
            lbltomon.Visible = true;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = true;
            ddlStateName.Visible = true;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }

        }
        else if (ddltype.SelectedValue == "4")
        {
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lbltomon.Text = "To Month-Year";
            lblFrmMoth.Text = "From Month-Year";
            lbltomon.Visible = true;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = true;
            ddlStateName.Visible = true;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            if (Session["sf_type"].ToString() == "1")
            {

                FillMRManagers();
                //ddlFieldForce.SelectedValue = sf_code;
                //ddlFieldForce.Enabled = false;

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                SalesForce sf = new SalesForce();

                dsSalesForce = sf.Hirarchy_UserList_All(div_code, sf_code);
                //  dsSalesForce.Tables[0].Rows[0].Delete();
                //                dsSalesForce.Tables[0].Rows[1].Delete();
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsSalesForce;
                    ddlFieldForce.DataBind();
                    ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));

                    ddlSF.DataTextField = "des_color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsSalesForce;
                    ddlSF.DataBind();

                }
            }
        }
        else if (ddltype.SelectedValue == "6")
        {


            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lblFrmMoth.Text = "From Month-Year";
            lbltomon.Text = "To Month-Year";
            lbltomon.Visible = true;
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = true;
            ddlStateName.Visible = true;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            //  Get_State();
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }
            ddlStateName_SelectedIndexChanged(sender, e);
        }
        else if (ddltype.SelectedValue == "7")
        {
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            //lblTMonth.Visible = false;
            //lblTYear.Visible = false;
            //ddlTMonth.Visible = false;
            //ddlTYear.Visible = false;
            txtToMonthYear.Visible = false;
            lblFrmMoth.Text = "Month - Year";
            lbltomon.Visible = false;
            lblprod.Visible = false;
            ddlprod.Visible = false;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = false;
            ddlStateName.Visible = false;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            if (Session["sf_type"].ToString() == "1")
            {

                FillMRManagers();
                //ddlFieldForce.SelectedValue = sf_code;
                //ddlFieldForce.Enabled = false;

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                SalesForce sf = new SalesForce();

                dsSalesForce = sf.Hirarchy_UserList_All(div_code, sf_code);
                //  dsSalesForce.Tables[0].Rows[0].Delete();
                //                dsSalesForce.Tables[0].Rows[1].Delete();
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsSalesForce;
                    ddlFieldForce.DataBind();
                    ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));

                    ddlSF.DataTextField = "des_color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsSalesForce;
                    ddlSF.DataBind();

                }
            }
        }
        else if (ddltype.SelectedValue == "5")
        {
            lblHQ.Visible = false;
            ddlHQ.Visible = false;
            lblStockiest.Visible = false;
            ddlStockiest.Visible = false;
            //lblTMonth.Visible = true;
            //lblTYear.Visible = true;
            //ddlTMonth.Visible = true;
            //ddlTYear.Visible = true;
            txtToMonthYear.Visible = true;
            lbltomon.Text = "To Month-Year";
            lblFrmMoth.Text = "From Month-Year";
            lbltomon.Visible = true;
            lblprod.Visible = true;
            ddlprod.Visible = true;
            lblProduct.Visible = false;
            chkProduct.Visible = false;
            lblst.Visible = true;
            ddlStateName.Visible = true;
            lblTerritory.Visible = false;
            chkHQs.Visible = false;
            Fill_Product_Name();
            if (Session["sf_type"].ToString() == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }
            foreach (ListItem item in ddlprod.Items)
            {
                if (item.Value == "-1")
                {
                    item.Attributes.Add("style", "color:Red");
                    break;
                }

            }

        }
    }
    //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //private void Get_State()
    //{
    //    Division dv = new Division();
    //    DataSet dsDivision;
    //    dsDivision = dv.getStatePerDivision(div_code);
    //    if (dsDivision.Tables[0].Rows.Count > 0)
    //    {
    //        int i = 0;
    //        state_cd = string.Empty;
    //        sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //        statecd = sState.Split(',');
    //        foreach (string st_cd in statecd)
    //        {
    //            if (i == 0)
    //            {
    //                state_cd = state_cd + st_cd;
    //            }
    //            else
    //            {
    //                if (st_cd.Trim().Length > 0)
    //                {
    //                    state_cd = state_cd + "," + st_cd;
    //                }
    //            }
    //            i++;
    //        }

    //        State st = new State();
    //        DataSet dsState;
    //        dsState = st.getStateChkBox(state_cd);
    //        ddlState.DataTextField = "statename";
    //        ddlState.DataValueField = "state_code";
    //        ddlState.DataSource = dsState;
    //        ddlState.DataBind();
    //        ddlState.Items.Insert(0, new ListItem("---Select---", "0"));

    //    }
    //}

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    div_code = Session["div_code"].ToString();
    //    if (div_code.Contains(','))
    //    {
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    }


    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        FillMRManagers();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillManagers();
    //    }
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnSubmit.Enabled = true;
    //    FillColor();
    //}

    private void FillProduct()
    {
        Product prod = new Product();
        DataSet dsChkProduct = new DataSet();
        dsChkProduct = prod.getProd(div_code);
        if (dsChkProduct.Tables[0].Rows.Count > 0)
        {
            chkProduct.DataSource = dsChkProduct;
            chkProduct.DataTextField = "Product_Detail_Name";
            chkProduct.DataValueField = "Product_Detail_Code";
            chkProduct.DataBind();
            setValueToChkBoxList();
        }

    }
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in chkProduct.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStockiest();

        DataTable dt = new DataTable();
        //  string StateName = ddlState.SelectedItem.Text;
        if (ddltype.SelectedValue == "6")
        {

            SalesForce sf = new SalesForce();
            dt = sf.SalesForce_State_Get_SS_Dt(div_code, ddlFieldForce.SelectedValue);
            //  ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
            DataTable temp = dt.DefaultView.ToTable(true, "StateName");

            if (ddlStateName.SelectedItem.Text == "--ALL--")
            {
                lblTerritory.Visible = false;
                chkHQs.Visible = false;
                //foreach (DataRow dr in temp.Rows)
                //{
                //    if (dr["StateName"].ToString().Trim() != "--ALL--")
                //    {
                //        string StCode = dr["StateName"].ToString();

                //        strSt_Code += StCode + ",";
                //    }
                //}
                //strSt_Code = strSt_Code.Substring(0, strSt_Code.Length - 1);
                //Stockist ss = new Stockist();
                //DataSet ds = ss.Get_StateWise_HQ_AllState(strSt_Code, div_code);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    chkHQs.DataTextField = "Territory";
                //    chkHQs.DataSource = ds;
                //    chkHQs.DataBind();
                //}
                //else
                //{
                //    chkHQs.DataSource = ds;
                //    chkHQs.DataBind();
                //}
            }
            else
            {
                lblTerritory.Visible = true;
                chkHQs.Visible = true;
                Stockist ss = new Stockist();
                DataSet ds = ss.Get_StateWise_HQ(ddlStateName.SelectedItem.Text, div_code);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkHQs.DataTextField = "Territory";
                    chkHQs.DataSource = ds;
                    chkHQs.DataBind();
                }
                else
                {
                    chkHQs.DataSource = ds;
                    chkHQs.DataBind();
                }
            }
        }
        else if (ddltype.SelectedValue == "4")
        {
            lblHQ.Visible = true;
            ddlHQ.Visible = true;
            if (ddlStateName.SelectedItem.Text == "--ALL--")
            {
                SalesForce sf = new SalesForce();
                dt = sf.SalesForce_State_Get_SS_Dt(div_code, ddlFieldForce.SelectedValue);
                //  ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
                DataTable temp = dt.DefaultView.ToTable(true, "StateName");
                foreach (DataRow dr in temp.Rows)
                {
                    if (dr["StateName"].ToString().Trim() != "--ALL--" && dr["StateName"].ToString().Trim() != "---Select---")
                    {
                        string StCode = dr["StateName"].ToString();

                        strSt_Code += StCode + ",";
                    }
                }
                strSt_Code = strSt_Code.Substring(0, strSt_Code.Length - 1);
                Stockist ss = new Stockist();
                DataSet ds = ss.Get_StateWise_HQ_AllState(strSt_Code, div_code);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlHQ.DataTextField = "Territory";
                    ddlHQ.DataSource = ds;
                    ddlHQ.DataBind();
                    ddlHQ.Items.Insert(0, new ListItem("--ALL--", "0"));

                }
                else
                {
                    ddlHQ.DataSource = ds;
                    ddlHQ.DataBind();
                }
            }
            else
            {

                Stockist ss = new Stockist();
                DataSet ds = ss.Get_StateWise_HQ(ddlStateName.SelectedItem.Text, div_code);



                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlHQ.DataTextField = "Territory";
                    ddlHQ.DataSource = ds;
                    ddlHQ.DataBind();
                    //ddlHQ.Items.Insert(0, new ListItem("--ALL--", "0"));

                }
                else
                {
                    ddlHQ.DataSource = ds;
                    ddlHQ.DataBind();
                }
            }
        }
    }
}