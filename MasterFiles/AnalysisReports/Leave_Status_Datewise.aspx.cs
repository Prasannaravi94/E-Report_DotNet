using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_AnalysisReports_Leave_Status_Datewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    DataSet dsSalesForce = null;
    DataSet dsSalesForcecamp = null;
    DataSet dsSalesForcecore = null;
    DataSet dsSalesForcecore_camp = null;
    DataSet dsSalesForcecrm = null;
    DataSet dsadm = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Activity_Date = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    int iCnt = -1;
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int i = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRfor_mr();
            //   FillYear();
            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }


    private void FillMRfor_mr()
    {
        // Territory terr = new Territory();
        //DataSet dsSalesforce = terr.getSFCode(div_code);
        SalesForce sf = new SalesForce();
        DataSet dsSalesforce = sf.Hierarchy_Team(div_code, "admin");
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
        }
        // FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

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
}

