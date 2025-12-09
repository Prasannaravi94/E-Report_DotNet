using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Bus_EReport;
using System.IO;

public partial class MasterFiles_DD_Slide_Upload : System.Web.UI.Page
{
    #region "Declaration"
    Division dv = new Division();
    DataSet dsDv = new DataSet();
    DataSet dsImg = new DataSet();
    string divcode = string.Empty;
    string sf_code = string.Empty;
    string[] statecd;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
        }

        dsDv = dv.getDivision(divcode);
        Session["Division_SName"] = dsDv.Tables[0].Rows[0]["Division_SName"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin_Dashboard.aspx");
    }
}
