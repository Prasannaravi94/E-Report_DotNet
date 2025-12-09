using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_Joinee_KitView : System.Web.UI.Page
{
   string div_code = string.Empty;
   string sf_type = string.Empty;

    DataSet dsrep = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["division_code"].ToString();
        

        if (!Page.IsPostBack)
        {
            Joinview();
        }
    }
    private void Joinview()
    {
        div_code = div_code.TrimEnd(',');
        Rep rr = new Rep();
        dsrep = rr.gettraineekit(div_code);
        if (dsrep.Tables[0].Rows.Count > 0)
        {
            grdtrainee.DataSource = dsrep;
            grdtrainee.DataBind();
        }
        else
        {
            grdtrainee.DataSource = dsrep;
            grdtrainee.DataBind();
        }
    }
}