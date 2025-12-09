using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Common_Doctors_Unique_dr_app_admin : System.Web.UI.Page
{
    DataSet dsUnique = new DataSet();
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
         sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menumas.Title = this.Page.Title;
            // menumas.FindControl("btnBack").Visible = false;
            FillUnique();
        }
    }
    private void FillUnique()
    {
        grdUnique.DataSource = null;
        grdUnique.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsUnique = LstDoc.getListedDr_Unique_admin_cnt(5, div_code);
        if (dsUnique.Tables[0].Rows.Count > 0)
        {
            grdUnique.Visible = true;
            grdUnique.DataSource = dsUnique;
            grdUnique.DataBind();

        }
        else
        {
            grdUnique.DataSource = dsUnique;
            grdUnique.DataBind();
        }
    }

}