using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class Ho_Id_Pwd_Chg : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;
    string old_pwd = string.Empty;
    int iRet = -1;
    string Ho_Id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Ho_Id = Session["HO_ID"].ToString();
        if (!Page.IsPostBack)
        {         
            txtOldPwd.Focus();
        
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        AdminSetup sf = new AdminSetup();
        dsSalesForce = sf.getHo_Password(Ho_Id);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            old_pwd = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        if (old_pwd.ToLower().Trim() == txtOldPwd.Text.ToLower().Trim())
        {

            if (old_pwd.ToLower().Trim() != txtNewPwd.Text.ToLower().Trim())
            {
                if (txtNewPwd.Text.Trim() != "")
                {
                    if (txtNewPwd.Text.ToLower().Trim() == txtConfirmPwd.Text.ToLower().Trim())
                    {
                        iRet = sf.Update_Ho_Password(Ho_Id, txtConfirmPwd.Text.Trim());
                        if (iRet > 0)
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password has been updated successfully');window.location='Index.aspx'</script>");
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Password and Confirm Password does not match. Please try again');</script>");
                        txtNewPwd.Focus();
                    }
                }
                else
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter New Password');</script>");
                    txtNewPwd.Focus();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Do not Enter Old Password');</script>");
            }
        }
        else
        {
            //menu1.Status = "Invalid Old Password";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Old Password');</script>");
            txtOldPwd.Focus();
        }
        
    }
}