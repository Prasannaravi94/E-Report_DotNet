using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using DBase_EReport;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Password : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string sfCode = string.Empty;
    string EX_Distance = string.Empty;
    string OS_Distance = string.Empty;
    string OSEX_Distance = string.Empty;
    string terrName_From = string.Empty;
    string terrName_To = string.Empty;
    DataSet dsTerritory = null;
    string terr_Name = string.Empty;
    DataSet dsTerr1 = null;
    DataSet dsTerr2 = null;
    string Fromterr_Sf_Code = string.Empty;
    string Toterr_Sf_Code = string.Empty;
    DataSet dsDCR = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        //if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        //{
        //    sfCode = Session["sf_code"].ToString();
        //}
        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            Usc_MR.FindControl("btnBack").Visible = false;
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";
            //btnBack.Visible = true;



        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sfCode = Session["sf_code"].ToString();
            UserControl_MGR_Menu c1 =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            //btnBack.Visible = true;
            c1.Title = this.Page.Title;
            //   Session["backurl"] = "LstDoctorList.aspx";
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";

        }
        else
        {
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }

        if (!Page.IsPostBack)
        {
            //btnclear.Visible = false;
          

        }
      
    }
}