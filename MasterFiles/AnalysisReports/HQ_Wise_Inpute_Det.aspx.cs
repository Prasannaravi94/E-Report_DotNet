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
using DBase_EReport;
#endregion

public partial class MasterFiles_AnalysisReports_HQ_Wise_Inpute_Det : System.Web.UI.Page
{
    #region Variables
    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    DataTable dtrowClr = null;
    int doctor_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsDoctor = null;
    List<int> iLstCat = new List<int>();
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    int mode = -1;
    string sSelectedType = "", strQry = "";
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        //
        hHeading.InnerText = this.Page.Title;

        if (Session["sf_type"].ToString() == "2")
        {
            FillMRManagers();
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            FillMRManagers();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            FillManagers();
        }
        ddlFieldForce.Visible = true;
        btnGo.Enabled = true;

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                // FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
                // FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
                // FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            //FillColor();
            BindDate();
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
                c1.Title = Page.Title;
                // //c1.FindControl("btnBack").Visible = false;
            }
            FillColor();
        }
        //
    }
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
                //ddlFYear.Items.Add(k.ToString());
                //ddlTYear.Items.Add(k.ToString());
            }

            //ddlFYear.Text = DateTime.Now.Year.ToString();
            //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlTYear.Text = DateTime.Now.Year.ToString();
            //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            
        }
    }
    #endregion
    //
    #region FillMRManagers
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        // ddlFFType.Visible = false;
        // ddlAlpha.Visible = false;
        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
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
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsTP = dsmgrsf;

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTP;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsTP;
            ddlSF.DataBind();

        }

        FillColor();

    }
    #endregion 
    //
    #region FillColor
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
    #endregion
     //
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
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

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();
    }
    #endregion  
    //
    #region ddlFFType_SelectedIndexChanged
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }
    #endregion 
    //
    #region AddHeader
    private void AddCellData(TableRow tr_header, string header_txt, int iRowSpan, int iColSpan, int iWidth, bool bStatus)
    {
        TableCell tc = new TableCell();
        tc.BorderStyle = BorderStyle.Solid;
        tc.BorderWidth = 1;
        tc.Width = iWidth;
        tc.Attributes.Add("Class", "rptCellBorder");
        tc.RowSpan = iRowSpan;
        tc.ColumnSpan = iColSpan;
        tc.VerticalAlign = VerticalAlign.Middle;
        Literal lit = new Literal();
        lit.Text = header_txt;
        tc.Font.Size = 10;
        tc.Font.Name = "calibri";
        tc.Controls.Add(lit);
        tc.Wrap = false;
        tc.Visible = bStatus;
        tr_header.VerticalAlign = VerticalAlign.Middle;
        tr_header.Cells.Add(tc);
        tr_header.Attributes.Add("style", "Background-color:#4A9586;");
    }
    #endregion
    //
    #region AddData
    private void AddCellDataValues(TableRow tr_data, string header_txt, int iRowSpan, int iColSpan, int iWidth, bool bStatus)
    {
        TableCell tcVal = new TableCell();
        tcVal.BorderStyle = BorderStyle.Solid;
        tcVal.BorderWidth = 1;
        tcVal.Attributes.Add("Class", "rptCellBorder");
        tcVal.Width = iWidth;
        tcVal.RowSpan = iRowSpan;
        tcVal.ColumnSpan = iColSpan;
        Literal lit = new Literal();
        lit.Text = header_txt;
        tcVal.Font.Size = 10;
        tcVal.Font.Name = "calibri";
        tcVal.Visible = bStatus;
        tcVal.Controls.Add(lit);
        tcVal.Wrap = false;
        tr_data.VerticalAlign = VerticalAlign.Middle;
        tr_data.Cells.Add(tcVal);
    }
    #endregion
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
}