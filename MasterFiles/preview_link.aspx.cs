using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class preview_link : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            BindDate();
            fillmsg();
            
        }
    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();

        }
    }
    protected void btnlink_goclick(object sender, EventArgs e)
    {
        fillmsg();
        
    }
    private void fillmsg()
    {
        SalesForce saln = new SalesForce();

        dsSalesForce = saln.getnotifymsg(div_code, ddlYear.SelectedValue);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdnotifymsg.Visible = true;
            grdnotifymsg.DataSource = dsSalesForce;
            grdnotifymsg.DataBind();
        }
        else
        {
            grdnotifymsg.DataSource = dsSalesForce;
            grdnotifymsg.DataBind();
        }
        
    }

}