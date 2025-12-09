using Bus_EReport;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_MR_Territory_Territory_Reactivation : System.Web.UI.Page
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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            //menu1.Title = this.Page.Title;
            Session["backurl"] = "Territory.aspx";
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR =
          (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                btnBack.Visible = false;
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                    "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Deactivation";
                }
                ViewTerritory();
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                if (Session["sf_code_Temp"] != null)
                {
                    sf_code = Session["sf_code_Temp"].ToString();
                }
                UserControl_MenuUserControl c1 =
          (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Session["backurl"] = "Territory.aspx";
                Divid.Controls.Add(c1);
                c1.Title = this.Page.Title;
                btnBack.Visible = false;
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                      "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                       "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Deactivation";
                }
                ViewTerritory();
            }


        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR = (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
            }
            else {
                if (Session["sf_code_Temp"] != null)
                {
                    sf_code = Session["sf_code_Temp"].ToString();
                }
                UserControl_MenuUserControl c1 =
              (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = this.Page.Title;
            }
        }

    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Deact(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdTerr.Visible = true;
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
            CheckBox chkdeactivate = (CheckBox)gridRow.Cells[1].FindControl("chkdeactivate");
            bool bCheck = chkdeactivate.Checked;
            if (bCheck == true)
            {
                iReturn = terr.DeActivate(lblTerritory_Code.Text);

                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                   
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated successfully');</script>");
                    ViewTerritory();
                }
                else
                {
                    // menu1.Status ="Unable to Deactivate";
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
                }
                //ViewTerritory();
            }
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