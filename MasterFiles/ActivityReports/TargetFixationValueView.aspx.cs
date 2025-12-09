using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_doctorbusview : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int time;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

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
                // c1.FindControl("btnBack").Visible = false;

                FillMRManagers1();
                FillYear();
                FillColor();
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

                FillManagers();
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
                lblFF.Visible = false;
                ddlFieldForce.Visible = false;
                ddlSF.Visible = false;
                //txtNew.Visible = false;

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
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                lblFF.Visible = false;
                ddlFieldForce.Visible = false;
                ddlSF.Visible = false;
                //txtNew.Visible = false;

                FillYear();
            }

        }
        FillColor();
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time 
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
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
        txtFromMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        txtToMonthYear.Value = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
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
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void txtNew_TextChanged(object sender, EventArgs e)
    //{
    //    ddlFieldForce_SelectedIndexChanged(sender, e);
    //}
    protected void btnGo_Click(object sender, EventArgs e)
    {

        //int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
        //int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
        //int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        //int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        //if (FMonth > TMonth && TYear == FYear)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
        //    ddlTMonth.Focus();
        //}
        //else if (FYear > TYear)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
        //    ddlTYear.Focus();
        //}
        //else
        //{

        //    if (FYear <= TYear)
        //    {
        //        string sURL = string.Empty;
        //        if (ddlFieldForce.SelectedIndex > 0)
        //        {

        //            sURL = "rptTargetFixationValueView.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString();

        //        }
        //        string newWin = "popUpObj = window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0'); popUpObj.focus(); $(popUpObj.document.body).ready(function () { $(popUpObj.document.body).append('<div><p style=color:blue;margin-top:10%;margin-left:40%;>Loading Please Wait....</p></div><div id=preload> <img src=https://s4.postimg.org/j1heaetwt/loading16.gif style=width:310px;height:300px;position:fixed;top:20%;left:30%; alt= /></div>');}); ";
        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //}
    }
}


