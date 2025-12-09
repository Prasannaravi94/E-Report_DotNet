using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Feed_Back_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsDivision = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();

        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {

            FillDivision();
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
         


        }
    }
    private void FillDivision()
    {
        AdminSetup dv = new AdminSetup();
       

            dsDivision = dv.GetFeedback_All();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                grdDivision.Visible = true;
                grdDivision.DataSource = dsDivision;
                grdDivision.DataBind();
            }
            else
            {
                grdDivision.DataSource = dsDivision;
                grdDivision.DataBind();
            }
            
        

    }

}