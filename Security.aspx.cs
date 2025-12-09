using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string st = Request.Url.Host;

        HttpCookie Cookie = new HttpCookie("Username");
        Cookie.Value = st;
        Cookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(Cookie);
        Application["Username"] = st;
        //// ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert('" + st + "');", true);
        Server.Transfer("Index.aspx");//?d='"+ st+"'");
        Response.Redirect("Index.aspx");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "noBack();", true);
    }
}