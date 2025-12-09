using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_Options_SetupScreen : System.Web.UI.Page
{
    DataSet dsadmin = null;
    DataSet dsadm = null;

    int iDOB_DOW = 0;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getHome_Dash_Display(div_code);
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                string strAdd = dsadmin.Tables[0].Rows[0]["DOB_DOW"].ToString();
                if (strAdd == "1")
                {
                    chkNew.Items[0].Selected = true;
                }
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cur_sf_code = string.Empty;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (chkNew.Items[0].Selected)
        {
            iDOB_DOW = 1;
        }
        else
        {
            iDOB_DOW = 0;
        }

        AdminSetup admin = new AdminSetup();

        int iReturn = admin.Home_Dash_Display(iDOB_DOW, div_code);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
        }
    }
}