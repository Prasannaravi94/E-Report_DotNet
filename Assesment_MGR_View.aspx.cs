using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
using Bus_EReport;

public partial class Assesment_MGR_View : System.Web.UI.Page
{
   string div_code = string.Empty;
   string sf_type = string.Empty;

    DataSet dsAss = new DataSet();

    protected void Page_Load(object sender, EventArgs e)  
    {

        div_code = Session["division_code"].ToString();
        

        if (!Page.IsPostBack)
        {
            Asse_view();
        }
    }
    private void Asse_view()
    {
        div_code = div_code.TrimEnd(',');
        SalesForce rr = new SalesForce();
        dsAss = rr.getAssesment_MGR(div_code);
        if (dsAss.Tables[0].Rows.Count > 0)
        {
            grdAssessment.DataSource = dsAss;
            grdAssessment.DataBind();
        }
        else
        {
            grdAssessment.DataSource = dsAss;
            grdAssessment.DataBind();
        }
    }
}