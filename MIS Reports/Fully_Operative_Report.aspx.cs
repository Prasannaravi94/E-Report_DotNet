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

#region Class MIS_Reports_Visit_Details_BasedonVisit
public partial class MIS_Reports_Fully_Operative_Report : System.Web.UI.Page
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
    string sSelectedType = "",strQry = "";
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
                ddlType.Items.FindByValue("2").Enabled = false;
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

                //DropDownList approveItem = ddlType.Items.FindByValue("Line Manager HQ wise");
                //approveItem.Enabled = false;
                ddlType.Items.FindByValue("2").Enabled = false;
              
                //approveItem.Attributes.Add("class", "border");
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
            txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
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
    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
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
                     lblMR.Visible = false;
                     ddlMR.Visible = false;
                     ddlMR.DataTextField = "sf_name";
                     ddlMR.DataValueField = "sf_code";
                     ddlMR.DataSource = dsSalesForce;
                     ddlMR.DataBind();
                     ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
                 }
             }
             else
             {
                 // Fetch Managers Audit Team
                 DataTable dt = sf.getAuditManagerTeam(div_code, ddlFieldForce.SelectedValue.ToString(), 0);

                 dsmgrsf.Tables.Add(dt);
                 dsmgrsf.Tables[0].Rows[0].Delete();
                 dsTP = dsmgrsf;

                 lblMR.Visible = true;
                 ddlMR.Visible = true;

                 ddlMR.DataTextField = "sf_name";
                 ddlMR.DataValueField = "sf_code";
                 ddlMR.DataSource = dsTP;
                 ddlMR.DataBind();
                 ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
             }
         }
         dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());
         if (dsSf.Tables[0].Rows.Count > 0)
         {
             if (ViewState["sf_type"].ToString() != "admin")
                 sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
         }

         if ((ViewState["sf_type"].ToString() == "admin" && ddlMR.SelectedIndex > 0) || (sf_type == "1"))
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
    #region ddlMR_SelectedIndexChanged
    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMR.SelectedIndex > 0)
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
    #region ddlAlpha_SelectedIndexChanged
    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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
    #region ddlFFType_SelectedIndexChanged
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }
    #endregion
    //
    #region btnGo_Click
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    sSelectedType = ddlType.SelectedItem.Text;
    //    if (sSelectedType == "Category")
    //    {
    //        strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
    //             " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
    //             " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
    //             " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
    //             " ORDER BY c.Doc_Cat_SName";
    //    }
    //    else if (sSelectedType == "Speciality")
    //    {
    //        strQry = " SELECT c.Doc_Special_Code,c.Doc_Special_SName AS ShortName,c.Doc_Special_Name,case isnull(c.No_of_visit,'') " +
    //             " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
    //             " (select count(d.Doc_special_code) from Mas_ListedDr d where d.Doc_special_code = c.Doc_Special_Code) as Cat_Count" +
    //             " FROM  Mas_Doctor_Speciality c WHERE c.Doc_Special_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
    //             " ORDER BY c.Doc_Special_SName";
    //    }
    //    else if (sSelectedType == "Class")
    //    {
    //        strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName AS ShortName,c.Doc_ClsName,case isnull(c.No_of_visit,'') " +
    //             " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit, " +
    //             " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cat_Count" +
    //             " FROM  Mas_Doc_Class c WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code= '" + div_code + "' " +
    //             " ORDER BY c.Doc_ClsSName";
    //    }
    //    else if (sSelectedType == "Campaign")
    //    {

    //        strQry = " SELECT c.Doc_SubCatCode,c.Doc_SubCatSName AS ShortName,c.Doc_SubCatName,case isnull(c.No_Visit,'') " +
    //             " when '' then 1 when 0 then 1 else c.No_Visit end as No_of_visit, " +
    //             " (select count(d.Doc_SubCatCode) from Mas_ListedDr d where d.Doc_SubCatCode like CONCAT((CAST(c.Doc_SubCatCode as varchar(50)) + ','), '%')) as Cat_Count" +
    //             " FROM  Mas_Doc_SubCategory c WHERE c.Doc_SubCat_ActiveFlag=0 AND c.Division_Code= " + Convert.ToInt32(div_code) + " " +
    //             " ORDER BY c.Doc_SubCatSName";
    //    }
    //    DB_EReporting db = new DB_EReporting();
    //    dsDoctor = db.Exec_DataSet(strQry);
    //    int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
    //    int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
    //    int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
    //    int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
    //    if (FMonth > TMonth && FYear == TYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
    //        ddlTMonth.Focus();
    //    }
    //    else if (FYear > TYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
    //        ddlTYear.Focus();
    //    }
    //    else
    //    {
    //        if (FYear <= TYear)
    //        {
    //            if (ddlType.SelectedValue.ToString() == "1")
    //            {
    //                FillCatg();
    //            }
    //            else if (ddlType.SelectedValue.ToString() == "2")
    //            {
    //                FillSpec();
    //            }
    //            else if (ddlType.SelectedValue.ToString() == "3")
    //            {
    //                FillClass();
    //            }
    //            else if (ddlType.SelectedValue.ToString() == "4")
    //            {
    //                FillCamp();
    //            }
    //        }
    //    }
    //}
    //

    #region FillCatg
    //private void FillCatg()
    //{
    //    int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //    int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());
      
    //    int iMn=0,iYr=0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement=true;        
    //    dtMnYr.Columns.Add("MNTH",typeof(int));
    //    dtMnYr.Columns.Add("YR",typeof(int));
    //    //
    //    while (months>=0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null,iMn,iYr);
    //        months--; cmonth++;
    //    }
    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
    //    SqlCommand cmd = new SqlCommand("visit_fixation_Cat", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    dsts.Tables[0].Columns.RemoveAt(6);
    //    dsts.Tables[0].Columns.RemoveAt(5);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    GrdFixation.DataSource = dsts;
    //    GrdFixation.DataBind();
    //}
    #endregion
    //
    #region dynamic table
    #region AddHeader
    private void AddCellData(TableRow tr_header,string header_txt, int iRowSpan,int iColSpan , int iWidth, bool bStatus)
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
    #region FillCatgG
    //private void FillCatgG()
    //{
    //    tbl.Rows.Clear();
    //    doctor_total = 0;
        
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
        
    //    /*if (ddlMode.SelectedValue.ToString() == "1")
    //    {
    //        if (ddlMR.SelectedIndex > 0)
    //        {
    //            mode = 1;
    //            dsSalesForce = dcc.SF_Self_Report(div_code, ddlMR.SelectedValue.ToString());
    //        }
    //        else
    //        {
    //            mode = 1;
    //            dsSalesForce = dcc.SF_Self_Report(div_code, ddlFieldForce.SelectedValue.ToString());
    //        }
    //    }
    //    else
    //    {*/
    //         mode = 2;
    //         DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue.ToString());
    //         if (DsAudit.Tables[0].Rows.Count > 0)
    //         {
    //             dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, ddlFieldForce.SelectedValue.ToString());
    //         }
    //         else
    //         {
    //             DataTable dt = sf.getAuditManagerTeam_GetMGR(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
    //             dsmgrsf.Tables.Add(dt);
    //             dsmgrsf.Tables[0].Rows[0].Delete();
    //             dsSalesForce = dsmgrsf;
    //         }
    //    //}

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        //
    //        //GrdFixation.DataSource = dsSalesForce;
    //        //GrdFixation.DataBind();
    //        //
    //        #region header
    //        TableRow tr_header = new TableRow();
    //        tr_header.BorderStyle = BorderStyle.Solid;
    //        tr_header.BorderWidth = 1;
    //        AddCellData(tr_header, "#", 3, 0, 50,true);
            
    //        if (Session["sf_type"].ToString() == "1")
    //        {
    //            //tr_header.Attributes.Add("Class", "MRBackColor");
    //            tr_header.Attributes.Add("style", "background-color:#4A9586;");
    //        }
    //        else if (Session["sf_type"].ToString() == "2")
    //        {
    //            //tr_header.Attributes.Add("Class", "MGRBackColor");
    //            tr_header.Attributes.Add("style", "background-color:#4A9586;");
    //        }
    //        else
    //        {
    //            //tr_header.Attributes.Add("Class", "Backcolor");
    //            tr_header.Attributes.Add("style", "background-color:#4A9586;");
    //        }

    //        AddCellData(tr_header, "<center>SF Code</center>", 3, 0, 400, false);
    //        AddCellData(tr_header, "<center>Field Force</center>", 3, 0, 900, true);
    //        AddCellData(tr_header, "<center>HQ</center>", 3, 0, 500, true);
    //        AddCellData(tr_header, "<center>Designation</center>", 3, 0, 500, true);
            
    //        int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //        int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //        int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

    //        ViewState["months"] = months;
    //        ViewState["cmonth"] = cmonth;
    //        ViewState["cyear"] = cyear;

    //        Doctor dr = new Doctor();
    //        dsDoctor = dr.getDocCat_Visit(div_code);

    //        if (months >= 0)
    //        {
    //            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //            {
    //                MonColspan = MonColspan + Convert.ToInt16(dataRow["No_of_Visit"].ToString())+2;
    //            }

    //            for (int j = 1; j <= months + 1; j++)
    //            {
    //                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //                AddCellData(tr_header, "<center>" + sTxt + "</center>", 0, MonColspan, 25, true);
                   
    //                cmonth = cmonth + 1;
    //                if (cmonth == 13)
    //                {
    //                    cmonth = 1;
    //                    cyear = cyear + 1;
    //                }
    //            }
    //        }
    //        tbl.Rows.Add(tr_header);

    //        months = Convert.ToInt16(ViewState["months"].ToString());
    //        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //        if (months >= 0)
    //        {
    //            TableRow tr_lst_det = new TableRow();

    //            for (int j = 1; j <= months + 1; j++)
    //            {
    //                //AddCellDataValues(tr_lst_det, "<center>Total Drs</center>",2,0,200,true);
                    
    //                if (dsDoctor.Tables[0].Rows.Count > 0)
    //                {
    //                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                    {
    //                        string sTxt = "<center>" + dataRow["Doc_Cat_SName"].ToString() + '(' + dataRow["No_of_visit"].ToString() + ')' + "</center>";
    //                        AddCellDataValues(tr_lst_det,sTxt, 0, Convert.ToInt16(dataRow["No_of_Visit"].ToString())+2, 30, true);
    //                    }

    //                    //AddCellDataValues(tr_lst_det, "<center>Missed</center>", 2, 0, 200, true);                        
    //                }

    //                cmonth = cmonth + 1;
    //                if (cmonth == 13)
    //                {
    //                    cmonth = 1;
    //                    cyear = cyear + 1;
    //                }
    //            }

    //            if (Session["sf_type"].ToString() == "1")
    //            {
    //                tr_lst_det.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_lst_det.Attributes.Add("Class", "MRBackColor");
    //            }
    //            else if (Session["sf_type"].ToString() == "2")
    //            {
    //                tr_lst_det.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_lst_det.Attributes.Add("Class", "MGRBackColor");
    //            }
    //            else
    //            {
    //                tr_lst_det.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_lst_det.Attributes.Add("Class", "Backcolor");
    //            }
    //            tbl.Rows.Add(tr_lst_det);
    //        }

    //        if (months >= 0)
    //        {
    //            TableRow tr_catg = new TableRow();

    //            if (Session["sf_type"].ToString() == "1")
    //            {
    //                tr_catg.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_catg.Attributes.Add("Class", "MRBackColor");
    //            }
    //            else if (Session["sf_type"].ToString() == "2")
    //            {
    //                tr_catg.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_catg.Attributes.Add("Class", "MGRBackColor");
    //            }
    //            else
    //            {
    //                tr_catg.Attributes.Add("style", "background-color:#4A9586;");
    //                //tr_catg.Attributes.Add("Class", "Backcolor");
    //            }

    //            for (int j = 1; j <= (months + 1); j++)
    //            {
    //                if (dsDoctor.Tables[0].Rows.Count > 0)
    //                {
    //                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                    {
    //                        if (Convert.ToInt16(dataRow["No_of_Visit"].ToString()) > 0)
    //                        {
    //                            AddCellDataValues(tr_catg, "<center>Total Drs</center>", 0, 0, 200, true);
    //                            int nvisit = Convert.ToInt16(dataRow["No_of_Visit"].ToString());
    //                            for (int i = 1; i <= nvisit; i++)
    //                            {
    //                                AddCellDataValues(tr_catg, "<center>" + i + " V" + "</center>", 0, 0, 30, true);                                    
    //                            }
    //                            AddCellDataValues(tr_catg, "<center>Missed</center>", 0, 0, 200, true);
    //                        }
    //                    }
    //                    tbl.Rows.Add(tr_catg);
    //                }
    //            }
    //        }
    //        #endregion
    //        //
    //        #region itemval
    //        // Details Section
    //        string sURL = string.Empty;
    //        int iCount = 0;
    //        int iCnt = 0;
    //        int tot_met = 0;
    //        int tot_miss = 0;

    //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //        {
    //            ListedDR lstDR = new ListedDR();
    //            iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

    //            TableRow tr_det = new TableRow();
    //            //tr_det.Attributes.Add("Class", "tblCellFont");
    //            tr_det.BackColor = System.Drawing.Color.White;
    //            iCount += 1;

    //            AddCellDataValues(tr_det, "<center>" + iCount.ToString() + "</center>", 0, 0, 50, true);
    //            AddCellDataValues(tr_det, "&nbsp;" + drFF["sf_code"].ToString(), 0, 0, 45, false);

    //            TableCell tc_det_doc_name = new TableCell();               
    //            HyperLink lit_det_doc_name = new HyperLink();
    //            lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
    //            tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
    //            if( (drFF["SF_Type"].ToString() == "2") && mode == 2)
    //            {
    //                lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + ddlFMonth.SelectedValue.ToString() + "', '" + ddlFYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlType.SelectedValue.ToString() + "')");
    //            }
    //            tc_det_doc_name.BorderStyle = BorderStyle.Solid;
    //            tc_det_doc_name.BorderWidth = 1;
    //            tc_det_doc_name.Width = 900;
    //            tc_det_doc_name.Style.Add("font-family", "Calibri");
    //            tc_det_doc_name.Style.Add("font-size", "10pt");
    //            tc_det_doc_name.Controls.Add(lit_det_doc_name);
    //            tr_det.Cells.Add(tc_det_doc_name);

    //            AddCellDataValues(tr_det, "&nbsp;" + drFF["Sf_HQ"].ToString(), 0, 0, 50, true);
    //            AddCellDataValues(tr_det, "&nbsp;" + drFF["Designation_Short_Name"].ToString(), 0, 0, 50, true);
                
    //            months = Convert.ToInt16(ViewState["months"].ToString());
    //            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            if (months >= 0)
    //            {
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    tot_met = 0;
    //                    tot_miss = 0;
    //                    doctor_total = 0;
    //                    FWD_total = 0;

    //                    if (cmonth == 12)
    //                    {
    //                        sCurrentDate = "01-01-" + (cyear + 1);
    //                    }
    //                    else
    //                    {
    //                        sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //                    }

    //                    dtCurrent = Convert.ToDateTime(sCurrentDate);

    //                    if (dsDoctor.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {                              

    //                            dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

    //                            if (dsDoc.Tables[0].Rows.Count > 0)
    //                                tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //                            doctor_total = doctor_total + Convert.ToInt16(tot_dr);
    //                        }

    //                        AddCellDataValues(tr_det, "&nbsp;" + doctor_total.ToString(), 0, 0, 50, true);                            

    //                        DCR dc = new DCR();
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {
    //                            if (Convert.ToInt16(dataRow["No_of_Visit"].ToString()) > 0)
    //                            {
    //                                int nvisit = Convert.ToInt16(dataRow["No_of_Visit"].ToString());
    //                                for (int i = 1; i <= nvisit; i++)
    //                                {
    //                                    tot_dcr_dr = "";

    //                                    if (dataRow["No_of_Visit"].ToString() == Convert.ToString(i))
    //                                    {
    //                                        dsDCR = dc.Catg_Visit_Report_noofvisit(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()), i);
    //                                    }
    //                                    else
    //                                    {
    //                                        dsDCR = dc.DCR_VisitDR_Catg_NoofVisit_Not_Equal(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()), i);
    //                                    }

    //                                    if (dsDCR.Tables[0].Rows.Count > 0)
    //                                    {
    //                                        tot_dcr_dr = dsDCR.Tables[0].Rows.Count.ToString();
    //                                    }
    //                                    TableCell tc_lst_month = new TableCell();
    //                                    HyperLink hyp_lst_month = new HyperLink();
    //                                    if (tot_dcr_dr != "0" && tot_dcr_dr != "")
    //                                    {
    //                                        tot_met = tot_met + Convert.ToInt16(tot_dcr_dr);
    //                                        hyp_lst_month.Text = tot_dcr_dr;
    //                                        hyp_lst_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "','" + i.ToString() + "')");
    //                                    }
    //                                    else
    //                                    {
    //                                        hyp_lst_month.Text = "-";
    //                                    }
    //                                    tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                                    tc_lst_month.BorderWidth = 1;
    //                                    tc_lst_month.Style.Add("font-family", "Calibri");
    //                                    tc_lst_month.Style.Add("font-size", "10pt");
    //                                    tc_lst_month.Width = 200;
    //                                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                                    tc_lst_month.VerticalAlign = VerticalAlign.Middle;
    //                                    tc_lst_month.Controls.Add(hyp_lst_month);
    //                                    tr_det.Cells.Add(tc_lst_month);
    //                                }
    //                            }
    //                        }

    //                        TableCell tc_det_sf_tot_met= new TableCell();
    //                        HyperLink hy_det_sf_tot_met = new HyperLink();
                          
    //                        if (tot_met > 0)
    //                        {
    //                            hy_det_sf_tot_met.Text = "&nbsp;" + tot_met.ToString();
    //                            hy_det_sf_tot_met.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','' )");
    //                        }
    //                        else
    //                        {
    //                            hy_det_sf_tot_met.Text = "-";
    //                        }
    //                        tc_det_sf_tot_met.BorderStyle = BorderStyle.Solid;
    //                        tc_det_sf_tot_met.BorderWidth = 1;
    //                        tc_det_sf_tot_met.HorizontalAlign = HorizontalAlign.Center;
    //                        tc_det_sf_tot_met.VerticalAlign = VerticalAlign.Middle;
    //                        tc_det_sf_tot_met.Controls.Add(hy_det_sf_tot_met);
    //                        tr_det.Cells.Add(tc_det_sf_tot_met);

    //                        cmonth = cmonth + 1;
    //                        if (cmonth == 13)
    //                        {
    //                            cmonth = 1;
    //                            cyear = cyear + 1;
    //                        }
    //                    }
    //                }
    //                tbl.Rows.Add(tr_det);
    //            }
    //        }
    //        #endregion
    //        //
    //    }
    //}
    #endregion
    #endregion
    //
    #region FillSpec
    //private void FillSpec()
    //{
    //    int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //    int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

    //    int iMn = 0, iYr = 0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtMnYr.Columns.Add("MNTH", typeof(int));
    //    dtMnYr.Columns.Add("YR", typeof(int));
    //    //
    //    while (months >= 0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null, iMn, iYr);
    //        months--; cmonth++;
    //    }
    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
    //    SqlCommand cmd = new SqlCommand("visit_fixation_Spclty", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    dsts.Tables[0].Columns.RemoveAt(6);
    //    dsts.Tables[0].Columns.RemoveAt(5);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    GrdFixation.DataSource = dsts;
    //    GrdFixation.DataBind();
    //}
    #endregion
    //
    #region FillClass
    //private void FillClass()
    //{
    //    int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //    int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

    //    int iMn = 0, iYr = 0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtMnYr.Columns.Add("MNTH", typeof(int));
    //    dtMnYr.Columns.Add("YR", typeof(int));
    //    //
    //    while (months >= 0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null, iMn, iYr);
    //        months--; cmonth++;
    //    }
    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
    //    SqlCommand cmd = new SqlCommand("visit_fixation_Class", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    dsts.Tables[0].Columns.RemoveAt(6);
    //    dsts.Tables[0].Columns.RemoveAt(5);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    GrdFixation.DataSource = dsts;
    //    GrdFixation.DataBind();
    //}
    #endregion
    //
    #region FillCamp
    //private void FillCamp()
    //{
    //    int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //    int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

    //    int iMn = 0, iYr = 0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtMnYr.Columns.Add("MNTH", typeof(int));
    //    dtMnYr.Columns.Add("YR", typeof(int));
    //    //
    //    while (months >= 0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null, iMn, iYr);
    //        months--; cmonth++;
    //    }
    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    //string sqry = "EXEC visit_fixation_Camp " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
    //    SqlCommand cmd = new SqlCommand("visit_fixation_Camp", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    dsts.Tables[0].Columns.RemoveAt(6);
    //    dsts.Tables[0].Columns.RemoveAt(5);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    GrdFixation.DataSource = dsts;
    //    GrdFixation.DataBind();
    //}
    #endregion
    //
    #region GrdFixation_RowDataBound
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            
            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {/*
                int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
                int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());
                string dttme = "";
                if (months == 12)
                    dttme = "01-01-" + (cyear + 1).ToString();
                else
                    dttme = (months + 1).ToString() + "-01-" + (cyear).ToString();

                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);

                SqlCommand cmd = new SqlCommand("VisitDetail_LstDr_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@div_code", div_code);
                cmd.Parameters.AddWithValue("@sf_code", dtrowClr.Rows[indx][1].ToString());
                cmd.Parameters.AddWithValue("@cdate", dttme);
                cmd.CommandTimeout = 100;
                string txtc = dtrowClr.Rows[indx][1].ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dst = new DataTable();
                da.Fill(dst);
                e.Row.Cells[4].Text = dst.Rows[0][0].ToString();
              */
            }

            for (int i = 4, j=0; i < e.Row.Cells.Count; i++)
            {                
                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {
                    HyperLink hLnk = new HyperLink();
                    hLnk.Text=e.Row.Cells[i].Text;
                    hLnk.NavigateUrl="#";
                    hLnk.ForeColor = System.Drawing.Color.Black;
                    hLnk.Font.Underline = false;
                    hLnk.ToolTip = "Click to View Details";
                    e.Row.Cells[i].Controls.Add(hLnk);
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    int ist = iLstCat[j];
                    int iMax = (e.Row.Cells[i - ist].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - ist].Text);
                    int iMin = (e.Row.Cells[i - 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                    e.Row.Cells[i].Text = (iMax - iMin).ToString();
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    j++;
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
        }
    }
    #endregion
    //
    #region GrdFixation_RowCreated
    //protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        //
    //        #region Object
    //        //Creating a gridview object            
    //        GridView objGridView = (GridView)sender;

    //        //Creating a gridview row object
    //        GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        //Creating a table cell object
    //        TableCell objtablecell = new TableCell();

    //        GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell objtablecell2 = new TableCell();
    //        GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell objtablecell3 = new TableCell();
    //        #endregion
    //        //
    //        #region Merge cells

    //        AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#4A9586", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#4A9586", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#4A9586", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#4A9586", true);

    //        int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //        int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
    //        int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

    //        SalesForce sf = new SalesForce();
                      
    //        for (int i = 0; i <= months; i++)
    //        {
    //            //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
    //            int iColSpan = 0;
    //            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
    //            {
    //                int iCnt = 0;
    //                iColSpan += Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 2;
    //                iCnt = Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 2;
    //                iLstCat.Add(iCnt-1);
    //                AddMergedCells(objgridviewrow2, objtablecell2, 0, iCnt, dtRow["ShortName"] + "(" + dtRow["No_of_visit"].ToString() + ")", "#4A9586", false);

    //                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "TDrs", "#4A9586", false);
    //                for (int j = 0; j < iCnt - 2; j++)
    //                {
    //                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, (j + 1).ToString() + " V", "#4A9586", false);
    //                }
    //                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Miss", "#4A9586", false);
    //            }
    //            string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //            AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#4A9586", true);
    //            cmonth = cmonth + 1;
    //            if (cmonth == 13)
    //            {
    //                cmonth = 1;
    //                cyear = cyear + 1;
    //            }
    //        }

    //        //Lastly add the gridrow object to the gridview object at the 0th position
    //        //Because, the header row position is 0.   
    //        objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
    //        objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
    //        objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
    //        //
    //        #endregion
    //        //
    //    }
    //}
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
    #endregion
    //
    #endregion
    //  

}
#endregion