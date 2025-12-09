using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

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
          
         

        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
      (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            //btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Reactivation";
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
            btnBack.Visible = true;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;'>For " + Session["sfName"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:#696D6E;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:#696D6E;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Reactivation";
            }
            ViewTerritory();
        }
    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_React(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
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
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
        
    //    Server.Transfer("Territory.aspx");
    //}
    protected void grdTerr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            Territory_Code = Convert.ToInt32(e.CommandArgument);
            Territory terr = new Territory();
            int iReturn = terr.Reactivate_Terr(Territory_Code,div_code);
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
                ViewTerritory();
            }
            else
            {
                // menu1.Status ="Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Reactivate.\');", true);
            }

            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory.aspx");
    }
}