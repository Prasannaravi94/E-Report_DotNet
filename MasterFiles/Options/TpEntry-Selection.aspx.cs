using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class TpEntry_Selection : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsAdminSetup = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
           
            Entrymethod();
        }
    }

    private void Entrymethod()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_TpEntry(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
           
            Radio1.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            
        }
    }

   
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        AdminSetup admin = new AdminSetup();
        int iReturn1 = admin.TpEntry(div_code,Radio1.SelectedValue);
        if (iReturn1 > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
        }
    }



    protected void BtnClear_Click(object sender, EventArgs e)
    {
        
        Radio1.ClearSelection();
      
    }

    
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/MasterFiles/Options/AdminSetup.aspx");
    }
}