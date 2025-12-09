using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Car_Service : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsTP = null;
    public DataSet dsDoc; 
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillDoc();

        }
    }

    private void FillDoc()
    {

        ListedDR LstDoc = new ListedDR();
        div_code = div_code.TrimEnd(',');
        dsDoc = LstDoc.GetTrans(div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            grdCarservice.Visible = true;

            grdCarservice.DataSource = dsDoc;
            grdCarservice.DataBind();
        }
        else
        {
           
            grdCarservice.DataSource = dsDoc;
            grdCarservice.DataBind();

        }
    }
}