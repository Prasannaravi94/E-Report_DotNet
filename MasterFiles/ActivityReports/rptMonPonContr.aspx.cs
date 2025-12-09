using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Bus_EReport;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Data;

public partial class MasterFiles_ActivityReports_rptMonPonContr : System.Web.UI.Page
{
    string sf_code = string.Empty;
    int iFrmMnth;
    int iToMnth;
    int iFYear;
    int iTYear;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sf_code = Request.QueryString["sf_code"].ToString();
            iFrmMnth = Convert.ToInt16(Request.QueryString["FMonth"].ToString());
            iToMnth = Convert.ToInt16(Request.QueryString["TMonth"].ToString());
            iFYear = Convert.ToInt16(Request.QueryString["FYear"].ToString());
            iTYear = Convert.ToInt16(Request.QueryString["TYear"].ToString());
            if (!IsPostBack)
            {

                DateTime month = new DateTime();
                for (int i = iFrmMnth; i <= iToMnth; i++)
                {
                    DateTime NextMont = month.AddMonths(i-1);
                    ListItem list = new ListItem();
                    list.Text = NextMont.ToString("MMMM");
                    list.Value = NextMont.Month.ToString();

                    ddlMonth.Items.Add(list);
                }
                for (int i = iFYear; i <= iTYear; i++)
                {
                    DateTime NextYear = month.AddYears(i - 1);
                    ListItem list = new ListItem();
                    list.Text = NextYear.Year.ToString();
                    list.Value = NextYear.Year.ToString();

                    ddlYear.Items.Add(list);
                }


                btnSubmit_Click(sender, e);
                
            }
            lblHead.Text = "Monthly Pontential for " + ddlMonth.SelectedItem.Text +" "+ iFYear;

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Product Prd = new Product();
            DataSet ds = new DataSet();
            ds = Prd.GetMnthPonCont(sf_code, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMnthPon.DataSource = ds;
                gvMnthPon.DataBind();
            }
            else
            {
                gvMnthPon.DataSource = null;
                gvMnthPon.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
}