using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using Bus_EReport.DynamicDashboard;

public partial class MasterFiles_DynamicDashboard_Dashboard : System.Web.UI.Page
{
    String SFCode = String.Empty;
    int divisionCode = 0;
    public DynamicDashboardViewModel CurrentDashboard = null;

    string sf_type = string.Empty;
    string division_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["div_code"] = 12; themis
        //Session["div_code"] = 77; //admin emcee
        if (Session["sf_code"] == null)
        {
            Response.Redirect("~/"); 
            return; 
        }
        if (Session["div_code"] == null || Session["sf_code"] == null)
        {
            if (Session["sf_type"].ToString() == "3")
            {
                string[] strDivSplit = Session["division_code"].ToString().Split(',');
                Session["div_code"] = strDivSplit[0];
            }
            else
            {
                Response.Redirect("~/");
                return;
            }
        }

        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }
        SetDivisions();
        divisionCode = Convert.ToInt32(Session["div_code"]);
        SFCode = Session["sf_code"].ToString();
        DynamicDashboardModel DynamicDashboardModel = new DynamicDashboardModel();
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                string idValue = Request.QueryString["Id"];

                int id = Convert.ToInt32(idValue);

                DataSet dashboards =DynamicDashboardModel.GetDashboards(divisionCode, id);
                if (dashboards.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = dashboards.Tables[0].Rows[0];
                    DynamicDashboardViewModel Dashboard = new DynamicDashboardViewModel();
                    Dashboard.Id = Convert.ToInt32(Row["Id"]);
                    Dashboard.Name = Row["Name"].ToString();
                    Dashboard.Module = Row["Module"].ToString();
                    Dashboard.Widgets = Row["Widgets"].ToString();
                    CurrentDashboard = Dashboard;
                }

            }

            if(CurrentDashboard == null)
            {
                DataSet dashboards = DynamicDashboardModel.GetDashboards(divisionCode, 0,"",true);
                if (dashboards.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = dashboards.Tables[0].Rows[0];
                    DynamicDashboardViewModel Dashboard = new DynamicDashboardViewModel();
                    Dashboard.Id = Convert.ToInt32(Row["Id"]);
                    Dashboard.Name = Row["Name"].ToString();
                    Dashboard.Module = Row["Module"].ToString();
                    Dashboard.Widgets = Row["Widgets"].ToString();
                    CurrentDashboard = Dashboard;
                }
            }
            if(CurrentDashboard != null)
            {
                SetSalesForce();
            }
            

        }
    }

    protected void SetSalesForce()
    {
        SalesForce salesForce = new SalesForce();
        DataSet salesForceData = salesForce.SalesForceListMgrGet(divisionCode.ToString(), SFCode);
        if (salesForceData.Tables[0].Rows.Count > 0)
        {
            SalesForceList.SelectedValue = salesForceData.Tables[0].Rows[0]["Sf_Code"].ToString();
        }
        SalesForceList.DataTextField = "Sf_Name";
        SalesForceList.DataValueField = "Sf_Code";
        SalesForceList.DataSource = salesForceData;
        SalesForceList.DataBind();
    }
    private void SetDivisions()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    DataSet dsdiv = dv.getDivisionHO(strdiv);
                    if (dsdiv.Tables[0].Rows.Count > 0)
                    {
                        ListItem liTerr = new ListItem();
                        liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        div.Items.Add(liTerr);
                    }
                }
            }
            div.SelectedValue = Session["div_code"].ToString();
        }
        else if (sf_type == "" || sf_type == null)
        {
            DataSet dsDivision = dv.getDivision_list();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                div.DataValueField = "Division_Code";
                div.DataTextField = "Division_Name";
                div.SelectedValue = Session["div_code"].ToString();

                div.DataSource = dsDivision;
                div.DataBind();
                //  btnSelect.Visible = false;
                //   lblenter.Visible = false;
            }
        }
    }
}