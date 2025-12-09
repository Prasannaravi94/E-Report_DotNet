using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;


public partial class MasterFiles_Reports_VacantBlockId : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = new DataSet();
    string sf_type = string.Empty;
    string div_code = string.Empty;
    DataSet dsdiv = null;
    DataSet dsDivision = null;

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

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_pnlMenu c1 =
                (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                Filldiv();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_pnlMenu c1 =
            (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
            }
        }
    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}