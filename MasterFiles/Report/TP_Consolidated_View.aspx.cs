//
#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
#endregion
//
//
#region Class Reports_TP_Status_Report
public partial class Reports_TP_Status_Report : System.Web.UI.Page
{
    //
    #region Variables
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsState = null;
    DataSet dsSalesForce = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
                //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                DateTime FromMonth = DateTime.Now;
                txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            }
            //
            #region type
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers();
                FillColor();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                FillManagers();
                FillColor();
            }
            ddlDesig.Load += new EventHandler(ddlFieldForce_SelectedIndexChanged);
            #endregion
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
            }
            FillColor();
        }
        if (ddlDesig.Items.Count == 0)
            btnSubmit.Visible = false;
        else
            btnSubmit.Visible = true;
    }
    //
    #endregion
    //
    #region FillManagers
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlFFType.DataTextField = "des_color";
            ddlFFType.DataValueField = "sf_code";
            ddlFFType.DataSource = dsSalesForce;
            ddlFFType.DataBind();
        }
        //FillColor();
    }
    #endregion
    //
    #region FillColor
    private void FillColor()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlFFType.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;
        }
    }
    #endregion
    //
    #region FillMRManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlFFType.DataTextField = "des_color";
            ddlFFType.DataValueField = "sf_code";
            ddlFFType.DataSource = dsSalesForce;
            ddlFFType.DataBind();
        }
        FillColor();
    }
    #endregion
    //
    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("EXEC ViewDetails_DrsTmp " + div_code + ",'" + ddlFieldForce.SelectedValue.ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("sf_Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

        ddlDesig.DataTextField = "Desg";
        ddlDesig.DataValueField = "Desg";
        ddlDesig.DataSource = dataVal;
        ddlDesig.DataBind();
        foreach (ListItem item in ddlDesig.Items)
        {
            if (item.Text.ToLower() == "admin")
            {
                ddlDesig.Items.Remove(item);
                break;
            }
        }
        FillColor();
        if (ddlDesig.Items.Count == 0)
            btnSubmit.Visible = false;
        else
            btnSubmit.Visible = true;
    }
    #endregion
    //
}
//
#endregion
//