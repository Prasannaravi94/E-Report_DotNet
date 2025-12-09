using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.Transfer("Index.aspx?d=2");
        /*
        string roseline="temp";
        HttpContext _context = HttpContext.Current;
        _context.Items.Add("name", roseline);
        _context.Items.Add("password", "pass@123");
        Server.Transfer("Destination.aspx");

        HttpContext _contexts = HttpContext.Current;
        Response.Write(_contexts.Items["name"]);
        Response.Write(_contexts.Items["password"]);
        */
    }
}