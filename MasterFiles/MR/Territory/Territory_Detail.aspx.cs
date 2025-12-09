using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Territory_Detail : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Alias_Name = string.Empty;
    string terr_code = string.Empty;
    string Territory_Code = string.Empty;
    int iReturn = -1;
    string hill = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       // sf_code = Session["sf_code"].ToString();
        heading.InnerText = this.Page.Title;
      
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();

                terr_code = Convert.ToString(Request.QueryString["Territory_Code"]);
                ViewTerritory();
            }
            else
            {
                if (Session["sf_code_Temp"] != null)
                {
                    sf_code = Session["sf_code_Temp"].ToString();

                }
                terr_code = Convert.ToString(Request.QueryString["Territory_Code"]);
                ViewTerritory();
            }
            Session["backurl"] = "Territory.aspx";

            // ViewTerritory();
            //  menu1.Title = this.Page.Title;


        }
        if (Session["sf_type"].ToString() == "1")
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            //  sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;

            //ViewTerritory();
        }
        else
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            //   sf_code = Session["sf_code"].ToString();
            UserControl_MenuUserControl c1 =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            //  ViewTerritory();
            //menu1.Visible = false;
            Session["backurl"] = "Territory.aspx";

        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Alias_Name = txtAlName.Text.ToString();
        string Territory_SName = txtSName.Text.ToString();
        string Fare = txtFare.Text.ToString();
        string Allowance = txtAdditional.Text;
         Territory Terr = new Territory();
         if (rdoYes.Checked == true)
         {
             hill = "Y";
         }
         else if (rdoNo.Checked == true)
         {
             hill = "N";
         }
        //int iReturn = Terr.getTerritory_Al(terr_code, Alias_Name, Territory_SName);
        int iReturn = Terr.getTerritory_Hill_Metro_Terr(terr_code, Alias_Name, Territory_SName, hill, Fare, ddlMetro.SelectedValue.Trim(), Allowance);//, ddlTerr_Visit.SelectedValue.Trim());
            if (iReturn > 0)
            {      
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added Successfully');</script>");
            }
        
    }
    private void ViewTerritory()
    {
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Det(sf_code, terr_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblName.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblType.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtSName.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtAlName.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            if (dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "Y")
            {
                rdoYes.Checked = true;
            }
            else if (dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "N")
            {
                rdoNo.Checked = true;
            }
            txtFare.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            ddlMetro.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            txtAdditional.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
           // ddlTerr_Visit.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Territory.aspx");
    }
}