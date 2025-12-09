using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MIS_Reports_SalesView : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = null;
    public DataSet dsDoc; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["division_code"].ToString();
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            // c1.FindControl("btnBack").Visible = false;

            FillBillView();
        }
    }

    private void FillBillView()
    {
        ListedDR LstDoc = new ListedDR();
        div_code = div_code.TrimEnd(',');
        dsDoc = LstDoc.GetSalesViewBill(div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdSalesBillView.Visible = true;
            grdSalesBillView.DataSource = dsDoc;
            grdSalesBillView.DataBind();
        }
        else
        {
            grdSalesBillView.DataSource = dsDoc;
            grdSalesBillView.DataBind();

        }
    }
}