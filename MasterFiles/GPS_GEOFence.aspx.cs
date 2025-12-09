using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;


public partial class GPS_GEOFence : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    int check = 0;
    int geo_code = 0;
    int geo_fencing = 0;
    private int Fencingstock;
    private int Fencingche;
    private int chkfencingche;
    private int chkfencingstock;
    private int DigitalOffline;
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        hHeading.InnerText = this.Page.Title;

        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
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
                FillMRManagers1();


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
        pnlGeoFence.Visible = false;
        //btnGo.Visible = false;
        btnUpdate.Visible = false;
        btnCancel.Visible = false;
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


    //protected void linkcheck_Click(object sender, EventArgs e)
    //{
    //    div_code = Session["div_code"].ToString();
    //    if (div_code.Contains(','))
    //    {
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    }

    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        // FillMRManagers1();
    //    }
    //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
    //    {
    //        FillMRManagers1();
    //    }

    //    pnlGeoFence.Visible = true;
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnGo.Visible = true;

    //    FillColor();
    //}

    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
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

    //protected void rdotagg_CheckedChanged(object sender, EventArgs e)
    //{
    //    Response.Redirect("../MasterFiles/Options/Geo_drs_deletion.aspx");
    //}

    //protected void rdoFence_CheckedChanged(object sender, EventArgs e)
    //{
    //    pnlGeoFence.Visible = true;


    //}
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        int iReturn = -1;

        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();
            CheckBox chkfencing = (CheckBox)gridRow.Cells[3].FindControl("chkfencing");
            CheckBox GeoFencingche = (CheckBox)gridRow.Cells[8].FindControl("chkfencingche");
            CheckBox GeoFencingstock = (CheckBox)gridRow.Cells[9].FindControl("chkfencingstock");
            CheckBox DigitalOfflineMode = (CheckBox)gridRow.Cells[11].FindControl("chkOfflineMode");
            AdminSetup ad = new AdminSetup();
            if (chkId.Checked)
            {

                geo_code = 0;
            }
            else
            {
                geo_code = 1;
            }

            if (chkfencing.Checked)
            {
                geo_fencing = 1;
            }
            else
            {
                geo_fencing = 0;
            }
            if (GeoFencingche.Checked)
            {
                Fencingche = 1;
            }
            else
            {
                Fencingche = 0;
            }

            if (GeoFencingstock.Checked)
            {
                Fencingstock = 1;
            }
            else
            {
                Fencingstock = 0;
            }
            if (DigitalOfflineMode.Checked)
            {
                DigitalOffline = 1;
            }
            else
            {
                DigitalOffline = 0;
            }
            
            iReturn = ad.RecordUpdate_geosf_code(sf_Code, geo_code, geo_fencing, Fencingche, Fencingstock, DigitalOffline);



        }

        if (iReturn != -1)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
            pnlGeoFence.Visible = true;
            btnGo.Visible = true;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;

        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {

            pnlGeoFence.Visible = true;
            FillSalesForce();
        }
    }

    private void FillSalesForce()
    {
        AdminSetup adm = new AdminSetup();
        dsSalesForce = adm.get_Managers(ddlFieldForce.SelectedValue.ToString(), div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            grdgps.Visible = true;
            grdgps.DataSource = dsSalesForce;
            grdgps.DataBind();
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            btnGo.Visible = true;

        }
        else
        {

            grdgps.Visible = true;
            grdgps.DataSource = null;
            grdgps.DataBind();
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnGo.Visible = true;


        }
    }
    protected void grdgps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();
            CheckBox chkfencing = (CheckBox)gridRow.Cells[0].FindControl("chkfencing");


            Label lblGeoNeed = (Label)gridRow.Cells[6].FindControl("lblGeoNeed");
            string GeoNeed = lblGeoNeed.Text.ToString();
            Label lblGeoFence = (Label)gridRow.Cells[7].FindControl("lblGeoFence");
            string GeoFence = lblGeoFence.Text.ToString();

            CheckBox chkfencingche = (CheckBox)gridRow.Cells[8].FindControl("chkfencingche");
            CheckBox chkfencingstock = (CheckBox)gridRow.Cells[9].FindControl("chkfencingstock");
            CheckBox chkOfflineMode = (CheckBox)gridRow.Cells[12].FindControl("chkOfflineMode");
            

            Label lblGeoChe = (Label)gridRow.Cells[10].FindControl("lblGeoChe");
            string GeoChe = lblGeoChe.Text.ToString();
            Label lblGeostck = (Label)gridRow.Cells[11].FindControl("lblGeostck");
            string Geostck = lblGeostck.Text.ToString();
            Label lbldigital_offline = (Label)gridRow.Cells[13].FindControl("lbldigital_offline");
            string digital_offline = lbldigital_offline.Text.ToString();



            if (sf_Code != "")
            {
                if (GeoNeed == "0")
                {
                    chkId.Checked = true;
                }
                else
                {
                    chkId.Checked = false;
                }


                if (GeoFence == "1")
                {
                    chkfencing.Checked = true;

                }
                else
                {
                    chkfencing.Checked = false;
                }
                if (GeoChe == "1")
                {
                    chkfencingche.Checked = true;

                }
                else
                {
                    chkfencingche.Checked = false;
                }
                if (Geostck == "1")
                {
                    chkfencingstock.Checked = true;

                }
                else
                {
                    chkfencingstock.Checked = false;
                }
                if (digital_offline == "1")
                {
                    chkOfflineMode.Checked = true;

                }
                else
                {
                    chkOfflineMode.Checked = false;
                }
            }
        }
    }

    protected void rdoFenceTagg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoFenceTagg.SelectedValue == "1")
        {
            pnlGeoFence.Visible = true;


        }
        if (rdoFenceTagg.SelectedValue == "2")
        {


            Response.Redirect("../MasterFiles/Options/Geo_drs_deletion.aspx");
        }
    }
}

