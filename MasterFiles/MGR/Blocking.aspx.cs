using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_AnalysisReports_Analysis_Pob_count_Periodically : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    DataSet dsSf = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  //menu1.FindControl("btnBack").Visible = false;  
            if (Session["sf_type"].ToString() == "1")
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

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers1();

                // FillColor();
             
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
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                FillManagers();

                //FillColor();
                FillSample();
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
        FillColor();
        setValueToChkBoxList();
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

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

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


        dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");


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
    
    private void FillMRManagers()
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

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        FillMRManagers1();
    //    }
    //    else if (Session["sf_type"].ToString() == "1")
    //    {
    //        SalesForce sf = new SalesForce();
    //        dsSf = sf.getReportingTo(sf_code);
    //        if (dsSf.Tables[0].Rows.Count > 0)
    //        {
    //            sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //        }
    //        if (!Page.IsPostBack)
    //        {
    //            FillMRManagers();
    //        }

    //        ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
    //        ddlFieldForce.Enabled = false;
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

    private void FillSample()
    {
        Doctor dr = new Doctor();
        DataSet dsChkinput = new DataSet();
        dsChkinput = dr.getSamplePrd_Name(div_code);
        chksample.DataSource = dsChkinput;
        chksample.DataTextField = "Product_Detail_Name";
        chksample.DataValueField = "Product_Code_SlNo";
        chksample.DataBind();
        setValueToChkBoxList();


    }
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in chksample.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }

    //protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlmode.SelectedValue == "1")
    //    {
    //        chksample.Visible = true;
    //        FillSample();
    //    }
    //    else
    //    {
    //        chksample.Visible = false;
    //    }
    //}
}