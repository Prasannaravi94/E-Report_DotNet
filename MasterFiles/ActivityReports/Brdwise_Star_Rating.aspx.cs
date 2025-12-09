using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Bus_EReport;

public partial class MasterFiles_DDProductBrand_Detail : System.Web.UI.Page
{
    #region "Declaration"
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    DataSet dsListedDR = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string drSpcCode = string.Empty;
    string drCatCode = string.Empty;
    string drQualCode = string.Empty;
    string drClassCode = string.Empty;
    string drTrrCode = string.Empty;
    string filter = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    int search = 0;
    int time;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Label lblHeading;
    string strMultiDiv = string.Empty;

    #endregion

    #region PageEvents
    protected void Page_Init(object sender, EventArgs e)
    {
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Header.DataBind();

        //if (!Page.IsPostBack)
        //{
        //    //  Menu1.Title = Page.Title;
        //    //  Menu1.FindControl("btnBack").Visible = false;  
        //      FillManagers();
        //      FillYear();
        //}
       

        //sf_code = Session["sf_code"].ToString();
        //if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        //{
        //    sf_code = Session["sf_code"].ToString();
        //}


        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  Menu1.FindControl("btnBack").Visible = false;  

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillManagers();
              
               
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                SalesForce sf = new SalesForce();
                //dsSf = sf.getReportingTo(sf_code);
                //if (dsSf.Tables[0].Rows.Count > 0)
                //{
                //    sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //}

                if (!Page.IsPostBack)
                {
                    FillManagers();
                }
                ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
                ddlFieldForce.Enabled = false;
                // FillColor();
               // BindDate();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
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

                //FillColor();
                //BindDate();
            }

        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
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
                sf_code = Session["sf_code"].ToString();
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
        //  FillColor();
        Lblmain.Visible = false;

        FillYear();













        //if (!Page.IsPostBack)
        //{
        //    //ServerStartTime = DateTime.Now;
        //    //base.OnPreInit(e);

        //    if (Session["sf_type"].ToString() == "2")
        //    {
        //        //Filldiv();
        //        DataSet dsdiv = new DataSet();
        //        Product prd = new Product();
        //        dsdiv = prd.getMultiDivsf_Name(sf_code);
        //        if (dsdiv.Tables[0].Rows.Count > 0)
        //        {
        //            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
        //            {
        //                strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
        //                ddlDivision.Visible = true;
        //                lblDivision.Visible = true;
        //                getDivision();
        //                Session["MultiDivision"] = ddlDivision.SelectedValue;
        //            }
        //            else
        //            {
        //                Session["MultiDivision"] = "";
        //                ddlDivision.Visible = false;
        //                lblDivision.Visible = false;
        //                //FillProd();
        //            }
        //            FillManagers();
        //        }
        //    }    


        //    if (Session["sf_type"].ToString() == "1")
        //    {
        //        div_code = Session["div_code"].ToString();
        //        UserControl_MR_Menu c1 =
        //       (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";

        //        FillManagers();
        //        ddlFieldForce.SelectedValue = sf_code;
        //        ddlFieldForce.Enabled = false;
        //    }
        //    else if (Session["sf_type"].ToString() == "2")
        //    {
        //        div_code = Session["div_code"].ToString();
        //        UserControl_MGR_Menu c1 =
        //       (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";

        //        FillManagers();
        //        ddlFieldForce.SelectedIndex = 1;
        //    }
        //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //    {
        //        div_code = Session["div_code"].ToString();
        //        if (div_code.Contains(','))
        //        {
        //            div_code = div_code.Remove(div_code.Length - 1);
        //        }
        //        UserControl_MenuUserControl c1 =
        //       (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";

        //        if (div_code.Contains(','))
        //        {
        //            div_code = div_code.Remove(div_code.Length - 1);
        //        }

        //        FillManagers();
        //        ddlFieldForce.SelectedIndex = 1;
        //    }

        //    ddlFieldForce.SelectedValue = sf_code;

        //    cFMnth = (DateTime.Now.Month - 2).ToString().Trim();
        //    cFYear = DateTime.Now.Year.ToString().Trim();
        //    cTMnth = DateTime.Now.Month.ToString().Trim();
        //    cTYear = DateTime.Now.Year.ToString().Trim();

        //    TourPlan tp = new TourPlan();
        //    dsTP = tp.Get_TP_Edit_Year(div_code);
        //    if (dsTP.Tables[0].Rows.Count > 0)
        //    {
        //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
        //        {
        //            ddlFYear.Items.Add(k.ToString());
        //            ddlFYear.SelectedValue = cFYear;
        //        }
        //    }
        //    ddlFMonth.SelectedValue = cFMnth;
        //}
        //else
        //{
        //    if (Session["sf_type"].ToString() == "1")
        //    {
        //        div_code = Session["div_code"].ToString();
        //        UserControl_MR_Menu c1 =
        //       (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";
        //    }
        //    else if (Session["sf_type"].ToString() == "2")
        //    {
        //        sf_code = ddlFieldForce.SelectedValue;
        //        div_code = Session["div_code"].ToString();
        //        UserControl_MGR_Menu c1 =
        //       (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";
        //    }
        //    else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //    {
        //        sf_code = ddlFieldForce.SelectedValue;
        //        div_code = Session["div_code"].ToString();
        //        if (div_code.Contains(','))
        //        {
        //            div_code = div_code.Remove(div_code.Length - 1);
        //        }
        //        UserControl_MenuUserControl c1 =
        //       (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        //        ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
        //        c1.Controls.Remove(mpe);
        //        ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
        //        c1.Controls.Remove(tsm);
        //        Divid.Controls.Add(c1);
        //        c1.Title = Page.Title;
        //        c1.FindControl("btnBack").Visible = false;
        //        lblHeading = (Label)c1.FindControl("lblHeading");
        //        lblHeading.Text = "Digital Detailing Listed-Doctorwise Product/Brand Analysis";
        //    }
        //}
    }

    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
    }

    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
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
    #endregion

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        
        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");


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

    //#region FillManagers
    //private void FillManagers()
    //{

    //    if (Session["sf_type"].ToString() == "2")
    //    {
    //        if (Session["MultiDivision"] != "")
    //        {
    //            div_code = ddlDivision.SelectedValue.ToString();
    //        }
    //        else
    //        {
    //            div_code = Session["div_code"].ToString();
    //        }
    //    }
        
    //    SalesForce sf = new SalesForce();

    //    if (sf_type == "1" || sf_type == "MR")
    //    {
    //        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin");
    //    }
    //    else
    //    {
    //        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
    //    }

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();
    //        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
    //    }
    //}
    //#endregion

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlFYear.Items.Add(k.ToString().Trim());
                //ddlFYear.SelectedValue = DateTime.Now.Year.ToString().Trim();
            }
        }
        DateTime FromMonth = DateTime.Now;
        txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();        
    }

    #region ClickEvents

    #endregion


    //gowsi

    string selected = string.Empty;
    DataSet dsProd = null;
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSelected();
    }
    private void FillSelected()
    {
        selectedmode.Visible = true;
        Product pro = new Product();
        DataTable dt = new DataTable();
        selected = ddlmode.SelectedValue;
      
        if (selected != string.Empty)
        {
            if (selected == "1")
            {
                selectedmode.Text="Brand";
                dsProd = pro.getBrdBySelectedState(selected, div_code);
                string[] selectedColumns = new[] { "Product_Brd_Name", "Product_Brd_Code" };
                dt = new DataView(dsProd.Tables[0]).ToTable(true, selectedColumns);

                if (dt.Rows.Count > 0)
                {
                    var result = from data in dt.AsEnumerable()
                                 select new
                                 {
                                     Ch_Name = data.Field<string>("Product_Brd_Name"),
                                     Ch_Code = data.Field<decimal>("Product_Brd_Code")
                                 };
                    var listOfGrades = result.ToList();
                    cblProductList.Visible = true;
                    cblProductList.DataSource = listOfGrades;
                    cblProductList.DataTextField = "Ch_Name";
                    cblProductList.DataValueField = "Ch_Code";
                    cblProductList.DataBind();
                    foreach (ListItem li in cblProductList.Items)
                    {
                        li.Attributes.Add("dvalue", li.Value);
                    }
                }
            }
            if (selected == "2")
            {
                selectedmode.Text = "Category";
                dsProd = pro.getCatBySelectedState(selected, div_code);
                string[] selectedColumns = new[] { "Product_Cat_Name", "Product_Cat_Code" };
                dt = new DataView(dsProd.Tables[0]).ToTable(true, selectedColumns);

                if (dt.Rows.Count > 0)
                {
                    var result = from data in dt.AsEnumerable()
                                 select new
                                 {
                                     Ch_Name = data.Field<string>("Product_Cat_Name"),
                                     Ch_Code = data.Field<decimal>("Product_Cat_Code")
                                 };
                    var listOfGrades = result.ToList();
                    cblProductList.Visible = true;
                    cblProductList.DataSource = listOfGrades;
                    cblProductList.DataTextField = "Ch_Name";
                    cblProductList.DataValueField = "Ch_Code";
                    cblProductList.DataBind();
                    foreach (ListItem li in cblProductList.Items)
                    {
                        li.Attributes.Add("dvalue", li.Value);
                    }
                }
            }
            if (selected == "3")
            {
                selectedmode.Text = "Group";
                dsProd = pro.getGrpBySelectedState(selected, div_code);
                string[] selectedColumns = new[] { "Product_Grp_Name", "Product_Grp_Code" };
                dt = new DataView(dsProd.Tables[0]).ToTable(true, selectedColumns);

                if (dt.Rows.Count > 0)
                {
                    var result = from data in dt.AsEnumerable()
                                 select new
                                 {
                                     Ch_Name = data.Field<string>("Product_Grp_Name"),
                                     Ch_Code = data.Field<decimal>("Product_Grp_Code")
                                 };
                    var listOfGrades = result.ToList();
                    cblProductList.Visible = true;
                    cblProductList.DataSource = listOfGrades;
                    cblProductList.DataTextField = "Ch_Name";
                    cblProductList.DataValueField = "Ch_Code";
                    cblProductList.DataBind();
                    foreach (ListItem li in cblProductList.Items)
                    {
                        li.Attributes.Add("dvalue", li.Value);
                    }
                }
            }
        }
        else
        {
            cblProductList.ClearSelection();
            selectedmode.Visible = false;
        }
    }
    //protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SalesForce sf = new SalesForce();
    //    string sReport = ddlmode.SelectedValue.ToString();
    //    dsSalesForce = sf.SalesForceList_MR_ALL(div_code, sReport);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        selectedmode.Text = ddlmode.Text;
    //        //ddlSF.DataTextField = "des_color";
    //        //ddlSF.DataValueField = "sf_code";
    //        //ddlSF.DataSource = dsSalesForce;
    //        //ddlSF.DataBind();
    //    }
    //}
}