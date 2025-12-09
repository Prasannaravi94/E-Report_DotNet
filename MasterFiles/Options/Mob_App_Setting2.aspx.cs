using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Mob_App_Setting2 : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsAdminSetup = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if(!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            app_record();
        }
    }
    private void app_record()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_mob_app_record(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            Radio.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Radio2.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Radio3.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Radio4.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            Radio5.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Radio6.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Radio7.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            Radio8.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            Radio9.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            Radio10.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            Radio17.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            string RxQty = string.Empty;
            RxQty = txtrxqty.Text;
            string sampleqty = string.Empty;
            sampleqty = txtsampqty.Text;
            Radio11.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            Radio14.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            Radio15.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            Radio16.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
        }
       
    }
    protected void btn_save_click(object sender,EventArgs e)
    {
        AdminSetup admin = new AdminSetup();
        int iReturn = admin.RecordInsert_Mob_App2(div_code,Radio.SelectedValue, Radio2.SelectedValue, Radio3.SelectedValue, Radio4.SelectedValue, Radio5.SelectedValue,
            Radio6.SelectedValue,Radio7.SelectedValue,Radio8.SelectedValue,Radio9.SelectedValue,Radio10.SelectedValue,Radio17.SelectedValue,txtrxqty.Text.ToString(),
            txtsampqty.Text.ToString(),Radio11.SelectedValue, Radio14.SelectedValue, Radio15.SelectedValue, Radio16.SelectedValue);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
        }
    }
}