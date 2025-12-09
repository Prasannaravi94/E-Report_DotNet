using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class TargetFixationView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    DataSet dsTP = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                this.FillMasterList();
                FillYear();
            }

            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                this.FillMasterList();
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
                // c1.FindControl("btnBack").Visible = false;
                this.Fillteam();
                FillYear();
            }

        }

        else
        {

            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "2")
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
        }
    }

    private void FillMasterList()
    {
        //dsSalesForce = sf.UserList_getMR(div_code, sfCode);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlFieldForce.DataTextField = "sf_name";
        //    ddlFieldForce.DataValueField = "sf_code";
        //    ddlFieldForce.DataSource = dsSalesForce;
        //    ddlFieldForce.DataBind();
        //    ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        //}

        //if (Session["sf_type"].ToString() == "1")
        //{
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
        //}

       // else //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{
        //    dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sfCode);
        //}
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
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
                //ddlFromYear.Items.Add(k.ToString());
                //ddlToYear.Items.Add(k.ToString());
                //ddlFromYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        //ddlFromMonth.SelectedValue = DateTime.Now.Month.ToString();
        //ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
        DateTime FromMonth = DateTime.Now;
        DateTime ToMonth = DateTime.Now;
        txtFromMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        txtToMonthYear.Value = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
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

    private void Fillteam()
    {
        dsSalesForce = sf.Hierarchy_Team(div_code, sfCode);

        //if (sf_type == "3")
        //{
        //    dsSalesForce.Tables[0].Rows[0].Delete();
        //    dsSalesForce.Tables[0].Rows[1].Delete();
        //}
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }


        FillColor();
    }
}