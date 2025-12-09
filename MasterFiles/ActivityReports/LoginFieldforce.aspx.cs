using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ActivityReports_LoginFieldforce : System.Web.UI.Page
{
    string div_code = string.Empty;
    string divcode = string.Empty;
    string SfName = string.Empty;
    DataSet dsLogin = null;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        // SfName = Session["Sf_Name"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            // FillManagers();
            //  ddlFieldForce.SelectedIndex = 1;
            FillManagers();
        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.LoginFieldforce(divcode, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
          
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        UserLogin ul = new UserLogin();
        dsLogin = ul.Process_LoginAll(ddlFieldForce.SelectedValue.ToString());
        if (dsLogin.Tables[0].Rows.Count > 0)
        {
            
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            // Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Session["sf_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Session["sf_type"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Session["Designation_Short_Name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Session["Sf_HQ"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            if (Session["sf_type"].ToString() == "1")
            {
                //   Server.Transfer("Default_MR.aspx");
                Response.Redirect("~/Default_MR.aspx");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                Response.Redirect("~/Default_MGR.aspx");
            }
        }
    }

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    FillManagers();
    //    //ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    //txtNew.Visible = true;
    //    btnsubmit.Enabled = true;

    //}
}