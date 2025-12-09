using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
public partial class MasterFiles_Statewise_Stockist_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sf_code = Session["sf_code"].ToString();
        string div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
        }
    }

}
