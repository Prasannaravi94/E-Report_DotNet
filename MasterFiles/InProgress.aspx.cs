using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_InProgress : System.Web.UI.Page
{
    #region Declaration
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cMnth = string.Empty;
    string cYear = string.Empty;
    #endregion

    #region Page_Life_Cycle
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        div_code = Request.QueryString["div_Code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        sf_type = Request.QueryString["sf_type"].ToString();
        cMnth = Request.QueryString["cMnth"].ToString();
        cYear = Request.QueryString["cYr"].ToString();
    } 
    #endregion

    #region lblbtnback_Click
    protected void lblbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion
}