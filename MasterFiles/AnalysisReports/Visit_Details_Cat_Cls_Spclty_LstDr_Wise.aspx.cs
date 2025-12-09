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
using System.Drawing.Imaging;
using DBase_EReport;
#endregion
//
#region Class MasterFiles_AnalysisReports_Coverage_Analysis
//
public partial class MasterFiles_AnalysisReports_Coverage_Analysis : System.Web.UI.Page
{
    //
    #region Variables
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string XlsDown = string.Empty;
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (chkD.Checked)
        { chkdate.Visible = true; lbldate.Visible = true; lblFrmMoth.Text = "Month-Year"; }
        else { chkdate.Visible = false;lbldate.Visible = false; lblFrmMoth.Text = "From Month-Year"; }
        
        
        hHeading.InnerText = this.Page.Title;

        
        ddlFieldForce.Visible = true;
        btnSubmit.Enabled = true;

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
            } 
            FillColor();
            BindDate();
            Filldays();
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
                // //c1.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
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
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
            }
            FillColor();
        }
        setValueToChkBoxList();
  
    }
    #endregion
    //
    #region FillSpeciality
    private void FillSpeciality()
    {
        string sQry = "SELECT Distinct Doc_Special_Code as Code, Doc_Special_SName as Short_Name FROM Mas_Doctor_Speciality "+
            "WHERE Division_Code='"+div_code+"' AND Doc_Special_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    #endregion
    //
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
           // ddlAlpha.Visible = false;
        dsSalesForce = sf.Hirarchy_UserList_All(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "2")
        //{
        //    dsSalesForce = sf.UserList_HQ(div_code, "admin");
        //}

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0,new ListItem("---Select---", "0"));

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();
    }
    #endregion
    //
    #region FillSF_Alpha
    //private void FillSF_Alpha()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlAlpha.DataTextField = "sf_name";
    //        ddlAlpha.DataValueField = "val";
    //        ddlAlpha.DataSource = dsSalesForce;
    //        ddlAlpha.DataBind();
    //        ddlAlpha.SelectedIndex = 0;
    //    }
    //}
    #endregion
    //
    #region FillColor
    private void FillColor()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;
        }
        //ddlFieldForce.Items.Remove("admin");
    }
    #endregion
    //
    #region ddlFFType_SelectedIndexChanged
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillManagers();
        FillColor();
    }
    #endregion
    //
    #region ddlAlpha_SelectedIndexChanged
    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();
    //    }
    //}
    #endregion
    //
    #region BindDate
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlYear.Items.Add(k.ToString());
                //ddlYearTo.Items.Add(k.ToString());
            }

            //ddlYear.Text = DateTime.Now.Year.ToString(); 
            //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlYearTo.Text = DateTime.Now.Year.ToString();
            //ddlMonthTo.SelectedValue = DateTime.Now.Month.ToString();
            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Value = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }
    #endregion
    //
    #region btnClear_click
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    //private void setChkBoxList()
    //{
    //    try
    //    {

    //        foreach (ListItem item in chkdate.Items)
    //        {
    //            item.Attributes.Add("cbValue2", item.Value.Trim());
    //        }

    //    }
    //    catch (Exception)
    //    {
    //    }
    //}
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdate.Items.Clear();
        Filldays();
        setValueToChkBoxList();
        //setChkBoxList();
    }
    #endregion
    //
    #region FillMRManagers
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
       // ddlFFType.Visible = false;
       // ddlAlpha.Visible = false;
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
    #endregion
    //
    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindBaseLevelDropdown();
    }
    #endregion
    //
    //
    #region BindBaseLevelDropdown
    private void BindBaseLevelDropdown()
    {
        SalesForce sf = new SalesForce();
        if (ViewState["sf_type"].ToString() == "admin")
        {
            DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
                dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    ddlBaseLvl.Visible = false;
                    ddlBaseLvl.DataTextField = "sf_name";
                    ddlBaseLvl.DataValueField = "sf_code";
                    ddlBaseLvl.DataSource = dsSalesForce;
                    ddlBaseLvl.DataBind();
                    ddlBaseLvl.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            else
            {
                DataTable dt = sf.getAuditManagerTeam(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
                DataSet dsmgrsf = new DataSet();
                dsmgrsf.Tables.Add(dt);
                dsmgrsf.Tables[0].Rows[0].Delete();
                DataSet dsTP = dsmgrsf;

                ddlBaseLvl.Visible = false;
                ddlBaseLvl.DataTextField = "sf_name";
                ddlBaseLvl.DataValueField = "sf_code";
                ddlBaseLvl.DataSource = dsTP;
                ddlBaseLvl.DataBind();
                ddlBaseLvl.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        DataSet dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            if (ViewState["sf_type"].ToString() != "admin")
                sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        if ((ViewState["sf_type"].ToString() == "admin" && ddlBaseLvl.SelectedIndex > 0) || (sf_type == "1"))
        {
            //ddlMode.SelectedIndex = 1;
            //ddlMode.Enabled = false;
        }
        else
        {
            //ddlMode.SelectedIndex = 0;
            //ddlMode.Enabled = true;
        }
    }
    #endregion
    //
    #region ddlMode_SelectedIndexChanged
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMode.SelectedIndex != 0)
        {
            btnSubmit.Visible = false;
            if (ddlMode.SelectedValue == "1")
            {
                FillCategory();
                lblMode.Text = "Select Category : ";
                btnSubmit.Visible = true;
                lnkExcel.Visible = false;
                chkD.Visible = true;
               // lblDt.Visible = true;
                //lbldate.Visible = false;
                chkdate.Visible = false;

            }
            else if (ddlMode.SelectedValue == "2")
            {
                FillSpeciality();
                lblMode.Text = "Select Speciality : ";
                btnSubmit.Visible = true;
                lnkExcel.Visible = false;
            }
            else if(ddlMode.SelectedValue=="3")
            {
                FillClass();
                lblMode.Text = "Select Class : ";
                btnSubmit.Visible = true;
                lnkExcel.Visible = false;
                chkD.Visible = false;
            }
            else if (ddlMode.SelectedValue == "5")
            {
                FillCampaign();
                lblMode.Text = "Select Campaign : ";
                btnSubmit.Visible = true;
                lnkExcel.Visible = true;
                chkD.Visible = false;
            }
            else
            {                
                lblMode.Visible=false;
                cbSpeciality.Items.Clear();
                btnSubmit.Visible = true;
                lnkExcel.Visible = false;
            }
            if (ddlMode.SelectedIndex!=4)
            {  
                lblMode.Visible = true;
                btnSubmit.Visible = true;
               
            }
        }
        else
        {
            lblMode.Visible = false;
            cbSpeciality.Items.Clear();
            btnSubmit.Visible = false;
            lnkExcel.Visible = false;
        }
    }
    #endregion
    //
    #region FillCampaign
    private void FillCampaign()
    {
        string sQry = " SELECT Distinct Doc_SubCatCode as Code,Doc_SubCatSName as Short_Name FROM  Mas_Doc_SubCategory " +
                          " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + div_code + "' " +
                          " ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    #endregion
    //
    #region FillCategory
    private void FillCategory()
    {
        string sQry = "SELECT Distinct Doc_Cat_Code as Code, Doc_Cat_SName as Short_Name FROM Mas_Doctor_Category " +
            "WHERE Division_Code='" + div_code + "' AND Doc_Cat_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    #endregion
    //
    #region FillClass()
    private void FillClass()
    {
        string sQry = "SELECT Distinct Doc_ClsCode as Code, Doc_ClsSName as Short_Name FROM Mas_Doc_Class " +
            "WHERE Division_Code='" + div_code + "' AND Doc_Cls_ActiveFlag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    #endregion
    //   
    #region setValueToChkBoxList
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in cbSpeciality.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region setValueToChkBoxList
    private void setValueToChkBoxListDate()
    {
        try
        {
            foreach (ListItem item in chkdate.Items)
            {
                item.Attributes.Add("cbValueDate", item.Value.Trim());
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion
    //
    private void Filldays()
    {
        string[] spFromMonthYear = txtFromMonthYear.Value.Split('-');
        string[] spToMonthYear = txtToMonthYear.Value.Split('-');
        int FMonth = DateTime.ParseExact(spFromMonthYear[0].ToString(), "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
        int FYear = Convert.ToInt32(spFromMonthYear[1]);
        int to_days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));


        for (int i = 1; i <= to_days; i++)
        {

            chkdate.Items.Add("   " + i.ToString());

        }


    }
    protected void chkD_CheckedChanged(object sender, EventArgs e)
    {
        if (chkD.Checked)
        {
            lbltomon.Visible = false;
            txtToMonthYear.Visible = false;
           // lbldate.Visible = true;
            chkdate.Visible = true;
            foreach (ListItem item in ddlMode.Items)
            {
                if ((item.Value) == "3" || (item.Value) == "4")
                {
                    item.Attributes.Add("style", "display:none;");

                }
            }
            setValueToChkBoxListDate();
        }
        else
        {
            chkdate.ClearSelection();
            lbltomon.Visible = true;
            txtToMonthYear.Visible = true;
            lbldate.Visible = false;
            chkdate.Visible = false;
        }
    }
    protected void lnkExcel_click(object sender, EventArgs e)
    {
        string[] spFromMonthYear = txtFromMonthYear.Value.Split('-');
        string[] spToMonthYear = txtToMonthYear.Value.Split('-');
        int FMonth = DateTime.ParseExact(spFromMonthYear[0].ToString(), "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
        int FYear = Convert.ToInt32(spFromMonthYear[1]);
        int TMonth = DateTime.ParseExact(spToMonthYear[0].ToString(), "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
        int TYear = Convert.ToInt32(spToMonthYear[1]);
        HiddenField cbValue;
        HiddenField cbTxt;
        string host = Request.Url.Host;
        int port = Request.Url.Port;
        
       // string cbValue = "";
       // cbValue = cbva.Value.ToString();
       // string cbTxt = "";
       //  cbTxt = cbtx.Value.ToString();
       //foreach (ListItem item in cbSpeciality.Items)
       //{
       //    item.Attributes.Add("cbValue", item.Value);
       //}
        ifmRep.Attributes.Add("src", "https://" + host + ":" + port + "/MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_Report.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&FMonth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + TMonth + "&TYear=" + TYear + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&cbVal=" + cbva.Value.ToString() + "&cbTxt=" + cbtx.Value.ToString() + "&cMode=" + ddlMode.SelectedValue + "&div_code=" + div_code + "&XlsDown=0" );

        
    }
    #region btnSubmit Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (ddlMode.SelectedIndex != 0)
        //{
        //    string sCbVal = "",sUrl="",sWindow;
        //    foreach (ListItem item in cbSpeciality.Items)
        //    {
        //        if (item.Selected)
        //        {                    
        //            sCbVal += item.Text + ",";
        //        }
        //    }
        //    if (ddlMode.SelectedIndex==4)
        //    {
        //        DateTime sFrom = Convert.ToDateTime("01-"+ddlMonth.SelectedValue +"-"+ ddlYearTo.SelectedItem.Text);
        //        DateTime sTo = Convert.ToDateTime("01-"+ddlMonthTo.SelectedValue + "-" + ddlYear.SelectedItem.Text);
        //        sCbVal = "";

        //        while (sFrom<=sTo)
        //        {
        //            sCbVal += sFrom.ToString("MM_yyyy")+",";
        //            sFrom = sFrom.AddMonths(1);
        //        }
                
        //        sUrl = "ViewDetails_ListedDr_Report.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&FMonth=" + ddlMonth.SelectedValue + "" +
        //            "&FYear=" + ddlYear.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&cbVal=" + sCbVal + "";
        //    }
        //    sWindow = "window.open('" + sUrl + "', 'popup_window', 'width=900,height=600,left=100,top=0,resizable=yes,toolbar=no,scrollbars=yes,location=no,statusbar=no,menubar=no,addressbar=no');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", sWindow, true);            
        //}
    }
    #endregion
    // 

}
#endregion
//