using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using DBase_EReport;

public partial class MasterFiles_MR_Territory_Territory_Bulk_Deactivation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    int Territory_Code = 0;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    string txtSlNo = string.Empty;
    //  string Territory_Code = string.Empty;
    string lblTerritory_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
             //   c1.FindControl("btnBack").Visible = false;
                ViewTerritory();

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
             //   c1.FindControl("btnBack").Visible = false;

                FillColor();
                FillMRManagers();
              

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
              //  c1.FindControl("btnBack").Visible = false;

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
             //   c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
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


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
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
    protected void btnGO_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sf_code = ddlFieldForce.SelectedValue.ToString();
            Session["sf_code_Temp"] = sf_code;
            ViewTerritory();
           

        }
        catch (Exception ex)
        {

        }
    }
     

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_BulkDeact( div_code, sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdTerr.Visible = false;
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();
        }
        else
        {
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Server.Transfer("Territory.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        Territory terr = new Territory();

        foreach (GridViewRow gridRow in grdTerr.Rows)
        {
            Label lblTerritory_Code = (Label)gridRow.Cells[0].FindControl("lblTerritory_Code");
            string lblterrcode = lblTerritory_Code.Text.ToString();
            //CheckBox chkdeactivate = (CheckBox)gridRow.Cells[1].FindControl("chkdeactivate");
            //bool bCheck = chkdeactivate.Checked;
            //if (bCheck == true)
            //{
                iReturn = terr.DeActivate(lblTerritory_Code.Text);

                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated successfully');</script>");
                btnSubmit.Visible = false;
                    ViewTerritory();
                }
                else
                {
                    // menu1.Status ="Unable to Deactivate";
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
                }
                //ViewTerritory();
            //}
            //else { ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Please select the checkbox\');", true); }
        }
    }

    protected void grdTerr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Territory_Code = Convert.ToInt32(e.CommandArgument);
            Territory terr = new Territory();
            int iReturn = terr.DeActivate(Territory_Code.ToString());
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
                ViewTerritory();
            }
            else
            {
                // menu1.Status ="Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }


        }
    }
}