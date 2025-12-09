using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_Mgrwise_Core_Doc_Map : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sLevel1_Code = string.Empty;
    string sLevel1_Name = string.Empty;
    string sLevel2_Code = string.Empty;
    string sLevel2_Name = string.Empty;
    string sLevel3_Code = string.Empty;
    string sLevel3_Name = string.Empty;
    string sf_code = string.Empty;
    DataSet dsadm = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            // FillSalesForce(div_code);
            //  FillSF_Alpha();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
               
                AdminSetup adm = new AdminSetup();
                dsadm = adm.get_core_lock(Session["sf_code"].ToString(), div_code);
                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {
                        FillSalesForce();
                        lblmgr.Visible = false;
                        ddlMGR.Visible = false;
                        pnlCore.Visible = true;
                    }
                    else if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                    {
                        lbllock.Visible = true;
                        pnlCore.Visible = false;
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
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                // FillSalesForce(div_code);
                pnlCore.Visible = true;
                FillManagers();


            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
                ViewState["sf_type"] = "";
                SalesForce sf = new SalesForce();
                dsadm = sf.getReportingTo(sf_code);

                AdminSetup adm = new AdminSetup();
                dsadm = adm.get_core_lock(Session["sf_code"].ToString(), div_code);
                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {
                        FillSalesForce();
                        lblmgr.Visible = false;
                        ddlMGR.Visible = false;
                        pnlCore.Visible = true;
                    }
                    else if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                    {
                        lbllock.Visible = true;
                        pnlCore.Visible = false;
                    }
                }
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
                div_code = Session["division_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

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
        }
    }

    private void FillSalesForce()
    {
        div_code = Session["div_code"].ToString();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (Session["sf_type"].ToString() == "2")
        {
            sf_code =  Session["sf_code"].ToString();
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            sf_code = ddlMGR.SelectedValue;
        }
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();

        }
    }



    //protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlMR.SelectedValue.ToString().Trim().Length > 0)
    //        FillDoc();
    //}
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMGR.DataTextField = "sf_name";
            ddlMGR.DataValueField = "sf_code";
            ddlMGR.DataSource = dsSalesForce;
            ddlMGR.DataBind();


        }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                sf_code = sf_code = Session["sf_code"].ToString();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                sf_code = ddlMGR.SelectedValue;
            }

            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");


            CheckBox chkLevel4 = (CheckBox)e.Row.FindControl("chkLevel4");

            AdminSetup adm = new AdminSetup();

            //bool bRet = adm.Core_Doctor_Map_RecordExist(ViewState["sLevel3_Code"].ToString().Trim(), ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            //if (bRet)
            //{
            //    chkLevel3.Checked = true;
            //}
            //else
            //{
            //    chkLevel3.Checked = false;
            //}

            bool bRet = adm.Core_Doctor_Map_RecordExist(sf_code, ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            if (bRet)
            {
                chkLevel4.Checked = true;
            }
            else
            {
                chkLevel4.Checked = false;
            }


        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        ddlMR.Enabled = false;
        ddlMGR.Enabled = false;
        FillDoc();
    }
    private void FillDoc()
    {

        gvDetails.DataSource = null;
        gvDetails.DataBind();


        if (Session["sf_type"].ToString() == "2")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            sf_code = ddlMGR.SelectedValue;
        }




        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_SlNO(ddlMR.SelectedValue.ToString().Trim());
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            gvDetails.Visible = true;
            gvDetails.DataSource = dsDoc;
            gvDetails.DataBind();

        }
        else
        {
            gvDetails.DataSource = dsDoc;
            gvDetails.DataBind();
        }
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            gvDetails.HeaderRow.Cells[6].Text = "(" + ddlMR.SelectedItem.Text + ")";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        string cur_sf_code = string.Empty;
        if (Session["sf_type"].ToString() == "2")
        {
            sf_code = sf_code = Session["sf_code"].ToString();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            sf_code = ddlMGR.SelectedValue;
        }

        string counts = string.Empty;
        SalesForce salesf = new SalesForce();
        DataSet ds = new DataSet();
        ds = salesf.Mgrcount(div_code, sf_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            counts = ds.Tables[0].Rows[0]["cnt"].ToString();

            //if (counts == "30")
            //{
                AdminSetup adm2 = new AdminSetup();
                int iReturn2 = adm2.Core_DR_Lock(sf_code, div_code, "1");

                if (iReturn2 > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Core Doctor(s) have been mapped Successfully');window.location='Mgrwise_Core_Doc_Map.aspx'</script>");

                    AdminSetup adm = new AdminSetup();
                    dsadm = adm.get_core_lock(Session["sf_code"].ToString(), div_code);
                    if (dsadm.Tables[0].Rows.Count > 0)
                    {
                        if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                        {
                            FillSalesForce();
                            lblmgr.Visible = false;
                            ddlMGR.Visible = false;
                            pnlCore.Visible = true;
                        }
                        else if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                        {
                            lbllock.Visible = true;
                            pnlCore.Visible = false;
                        }
                    }
                }
            //}

            else if (Convert.ToInt32(counts) < 30)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select 30 Core Doctor(s)');window.location='Mgrwise_Core_Doc_Map.aspx'</script>");
            }
            else if (Convert.ToInt32(counts) > 30)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Exceeds 30 Core Doctor(s)');window.location='Mgrwise_Core_Doc_Map.aspx'</script>");
            }

        }

    }
    protected void ddlMGR_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSalesForce();
        gvDetails.Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMR.Enabled = true;
        ddlMGR.Enabled = true;
        gvDetails.Visible = false;
    }
    protected void btnDraftSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        string cur_sf_code = string.Empty;
        if (Session["sf_type"].ToString() == "2")
        {
            sf_code = sf_code = Session["sf_code"].ToString();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            sf_code = ddlMGR.SelectedValue;
        }
        AdminSetup adsp = new AdminSetup();
        iReturn = adsp.Core_Doctor_Map_Delete(ddlMR.SelectedValue.ToString().Trim(), div_code, sf_code);

        if (iReturn != -1)
        {
            foreach (GridViewRow gridRow in gvDetails.Rows)
            {
                {
                    Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");

                    CheckBox chkLevel4 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel4");


                    if (chkLevel4.Checked)
                        iReturn = adsp.Core_Doctor_Map_Add(ddlMR.SelectedValue.ToString().Trim(), div_code, sf_code, lblDocCode.Text.Trim());
                }
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Core Doctor(s) have been mapped Successfully');</script>");
            }
        }
        AdminSetup adm2 = new AdminSetup();
        int iReturn2 = adm2.Core_DR_Lock(sf_code, div_code, "0");
    }
}