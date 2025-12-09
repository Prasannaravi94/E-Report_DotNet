using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Focus();
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "adminaccounts")
        {
            //  Session["id"] = txtUsername.Text;
            if (txtUsername.Text == "adminaccounts" && txtPassword.Text == "accounts" && txtsecurty.Text == "saneforce")
            {
                Session["id"] = "success";
                Response.Redirect("User_Count.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
            }
        }
    }
}