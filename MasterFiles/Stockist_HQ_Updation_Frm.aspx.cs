using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;

public partial class MasterFiles_Stockist_HQ_Updation_Frm : System.Web.UI.Page
{
    #region "Declaration"
    string divcode = string.Empty;

    string SF_Name = string.Empty;
    string SF_Code = string.Empty;
    string SF_Type = string.Empty;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
           // ServerStartTime = DateTime.Now;
           // base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            FillManagers();
            FillColor();
            //btnAdd_Hq.Visible = false;
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    private void FillManagers()
    {
        DataSet dsSalesForce;
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(divcode, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stock_HQ_Bulk_Edit.aspx");
    }
}

