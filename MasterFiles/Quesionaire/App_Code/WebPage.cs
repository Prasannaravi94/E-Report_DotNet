using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for WebPage
/// </summary>

public class WebPage : System.Web.UI.Page
{
    public WebPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void DisplayMessage(string message)
    {
        string script = GetAlert(message);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }
    private string GetAlert(string message)
    {
        return string.Format("alert(\"{0}\");", message);
    }
    public void DisplayMessageAddRedirect(string message, string pageName)
    {
        string script = GetAlert(message);
        script += string.Format("location.href='{0}';", pageName);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }

}
