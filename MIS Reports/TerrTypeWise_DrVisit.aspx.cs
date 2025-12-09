using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_TerrTypeWise_DrVisit : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        hHeading.InnerText = this.Page.Title;

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

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (Session["sf_type"].ToString() == "1")
        {
            FillManagers();
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            FillManagers();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            FillManagers();
        }
        ddlFieldForce.Visible = true;
        btnGo.Enabled = true;
        FillColor();

        if (!Page.IsPostBack)
        {
       
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                if (Session["sf_type"].ToString() == "2")
                {
                    div_code = Session["div_code"].ToString();
                    UserControl_MGR_Menu c1 =
                   (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.Title = Page.Title;
                    // //c1.FindControl("btnBack").Visible = false;
                    //FillManagers();
                    FillYear();

                }

                else if (Session["sf_type"].ToString() == "1")
                {
                    div_code = Session["div_code"].ToString();
                    UserControl_MR_Menu c1 =
                   (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.Title = Page.Title;
                    // //c1.FindControl("btnBack").Visible = false;
                    //txtNew.Visible = false;
                   // FillManagers();
                    FillYear();

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
                    // //c1.FindControl("btnBack").Visible = false;
                    //FillManagers();
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
                    // //c1.FindControl("btnBack").Visible = false;

                }

                else if (Session["sf_type"].ToString() == "1")
                {
                    div_code = Session["div_code"].ToString();
                    UserControl_MR_Menu c1 =
                   (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                    Divid.Controls.Add(c1);
                    c1.Title = Page.Title;
                    // //c1.FindControl("btnBack").Visible = false;

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
                    // //c1.FindControl("btnBack").Visible = false;

                }
            }
               
        FillColor();
    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlYear.Items.Add(k.ToString());
                //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
       // dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");

        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}

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

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
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
    protected void btnGo_Click(object sender, EventArgs e)
    {
        // FillSalesForce();
        string Month = Convert.ToString(Convert.ToDateTime(txtMonthYear.Text).Month);
        string Year = Convert.ToString(Convert.ToDateTime(txtMonthYear.Text).Year);
        //string sURL = "rptDelayed_DCR_Status.aspx?cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim() + "&SF_code=" + ddlFieldForce.SelectedValue;
        string sURL = "rptDelayed_DCR_Status.aspx?cmon=" + Month + "&cyear=" + Year + "&SF_code=" + ddlFieldForce.SelectedValue;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }


}