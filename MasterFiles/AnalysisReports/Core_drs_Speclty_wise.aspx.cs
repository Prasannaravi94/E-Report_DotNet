using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_AnalysisReports_Core_drs_Speclty_wise : System.Web.UI.Page
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
    DataSet dsSf = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  Menu1.FindControl("btnBack").Visible = false;  
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                SalesForce sf = new SalesForce();
                 dsSf = sf.getReportingTo(sf_code);
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (!Page.IsPostBack)
                {
                    FillMRManagers();
                }

                ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
                ddlFieldForce.Enabled = false;

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers1();

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
            FillSpeciality();
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
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
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
        }
        FillColor();
        setValueToChkBoxList();
    }

    #region FillSpeciality
    private void FillSpeciality()
    {
        string sQry = "SELECT Distinct Doc_Special_Code as Code,Doc_Special_SName as Short_Name FROM Mas_Doctor_Speciality " +
            "WHERE Division_Code='" + div_code + "' AND Doc_Special_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    #endregion

    #region setValueToChkBoxList
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in cbSpeciality.Items)
            {
                item.Attributes.Add("cbValue",  item.Value);
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion
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

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        ddlFieldForce.Items.RemoveAt(1);

    }
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


    }

     private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            //ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

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

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
}