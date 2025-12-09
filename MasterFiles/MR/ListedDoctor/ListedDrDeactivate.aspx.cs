using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_ListedDrDeactivate : System.Web.UI.Page
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
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR =
             (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;

                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                           "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                            "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
                btnBack.Visible = true;
                Usc_MR.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                sf_code = Session["sf_code"].ToString();
                if (Session["sf_code_Temp"].ToString() != "")
                {
                    sfCode = Session["sf_code_Temp"].ToString();
                }
                UserControl_MGR_Menu Usc_MGR =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MGR);
                Usc_MGR.Title = this.Page.Title;

                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                           "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                            "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
                btnBack.Visible = true;
                Usc_MGR.FindControl("btnBack").Visible = false;
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                if (Session["sf_code_Temp"].ToString() != "")
                {
                    sfCode = Session["sf_code_Temp"].ToString();
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //Divid.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;

                Session["backurl"] = "LstDoctorList.aspx";
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;'>For " + Session["sfName"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:#696D6E;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:#696D6E;'>  " + Session["sf_HQ"] + "</span>" + " )";
            }
            if (!Page.IsPostBack)
            {
                FillDoc();
                //menu1.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                getWorkName();
            }
            // getVisit();
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = "Doctor " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new ListItem(str, "6", true));
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
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDrdeativate_MR(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            btnSave.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
    }
    // Sorting 
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
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        ListedDR LstDoc = new ListedDR();
        dtGrid = LstDoc.getListedDoctorListNew_DT(sfCode);
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (txtsearch.Text != "")
        {
            dtGrid = LstDoc.getListedDrforName_Datatable(sfCode, txtsearch.Text);
        }
        else if (ddlSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
            }
            else if (search == 2)
            {
                dtGrid = LstDoc.getListedDrforSpl_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 3)
            {
                dtGrid = LstDoc.getListedDrforCat_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 4)
            {
                dtGrid = LstDoc.getListedDrforQual_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 5)
            {
                dtGrid = LstDoc.getListedDrforClass_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 6)
            {
                dtGrid = LstDoc.getListedDrforTerr_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
        }
        return dtGrid;

    }
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdDoctor.DataSource = sortedView;
        grdDoctor.DataBind();

    }
    //Changes done by Priya
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (search == 7)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillDoc();
        }
        if (search == 2)
        {
            FillSpl();
        }
        if (search == 3)
        {
            FillCat();
        }
        if (search == 4)
        {
            FillQualification();
        }
        if (search == 5)
        {
            FillClass();
        }
        if (search == 6)
        {
            FillTerritory();
        }
    }
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_SName";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_SName";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_QuaName";
            ddlSrc2.DataValueField = "Doc_QuaCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsSName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    //protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        ListedDR LstDoc = new ListedDR();
        if (search == 1)
        {
            FillDoc();
        }
        if (search == 2)
        {
            dsDoc = LstDoc.getListedDrforSpl(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 3)
        {
            dsDoc = LstDoc.getListedDrforCat(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = LstDoc.getListedDrforQual(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = LstDoc.getListedDrforClass(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 6)
        {
            dsDoc = LstDoc.getListedDrforTerr(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 7)
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                btnSave.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        //End 

    }
    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.ClassName='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.ClassName='Normal'");
            // e.Row.Cells[1].Attributes.Add("colspan", "1");

        }
    }

    private void getVisit()
    {
        //string Camp = "CAMPAIGN";
        //string Core = "CORE";
        //string Camp_Core = "CORE/CAMP";
        //string CRM = "CRM";

        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.FindControl("chkListedDR");
            CheckBox chkAll = (CheckBox)gridRow.FindControl("chkAll");
            CheckBox checklist = (CheckBox)gridRow.FindControl("chkListedDR");
            Image imgCross = (Image)gridRow.FindControl("imgCross");
            Image imgCross1 = (Image)gridRow.FindControl("imgCross1");
            Image imgCross2 = (Image)gridRow.FindControl("imgCross2");
            Image imgCross3 = (Image)gridRow.FindControl("imgCross3");
            Label lblVisitdate = (Label)gridRow.FindControl("lblVisit");
            Label lblDocCode = (Label)gridRow.FindControl("lblDocCode");
            Label lblSNo = (Label)gridRow.FindControl("lblSNo");

            //ListedDR LstDoc = new ListedDR();
            //dsSalesForce = LstDoc.Deact_Visitdr(sfCode, lblDocCode.Text);

            //ListedDR LstDocCamp = new ListedDR();
            //dsSalesForcecamp = LstDocCamp.Deact_VisitdrCamp(sfCode, lblDocCode.Text, div_code);

            //ListedDR LstDocCore = new ListedDR();
            //dsSalesForcecore = LstDocCore.Deact_VisitdrCore(sfCode, lblDocCode.Text, div_code);

            //ListedDR LstDocCore_Camp = new ListedDR();
            //dsSalesForcecore_camp = LstDocCore_Camp.Deact_VisitdrCore_Camp(sfCode, lblDocCode.Text, div_code);


            //ListedDR LstDocCRM = new ListedDR();
            //dsSalesForcecrm = LstDocCRM.Deact_VisitdrCRM(sfCode, lblDocCode.Text, div_code);

            //if (dsSalesForcecamp != null)
            //{
            //    foreach (DataRow drDoc in dsSalesForcecamp.Tables[0].Rows)
            //    {
            //        gridRow.Enabled = false;
            //        gridRow.BackColor = System.Drawing.Color.White;
            //        imgCross2.Visible = true;
            //        checklist.Visible = false;

            //        lblVisitdate.Text = Camp;
            //        lblVisitdate.ForeColor = System.Drawing.Color.DarkGreen;
            //        lblVisitdate.Font.Bold = true;


            //    }
            //}
            //if (dsSalesForcecore != null)
            //{
            //    foreach (DataRow drDoc in dsSalesForcecore.Tables[0].Rows)
            //    {
            //        gridRow.Enabled = false;
            //        gridRow.BackColor = System.Drawing.Color.White;
            //        imgCross1.Visible = true;
            //        checklist.Visible = false;

            //        lblVisitdate.Text = Core;
            //        lblVisitdate.ForeColor = System.Drawing.Color.DeepPink;
            //        lblVisitdate.Font.Bold = true;


            //    }
            //}
            //if (dsSalesForcecore_camp != null)
            //{
            //    foreach (DataRow drDoc in dsSalesForcecore_camp.Tables[0].Rows)
            //    {
            //        gridRow.Enabled = false;
            //        gridRow.BackColor = System.Drawing.Color.White;
            //        imgCross2.Visible = true;
            //        imgCross1.Visible = false;
            //        checklist.Visible = false;

            //        lblVisitdate.Text = Camp_Core;
            //        lblVisitdate.ForeColor = System.Drawing.Color.LimeGreen;
            //        lblVisitdate.Font.Bold = true;
            //    }
            //}
            //if (dsSalesForcecrm != null)
            //{
            //    foreach (DataRow drcrm in dsSalesForcecrm.Tables[0].Rows)
            //    {
            //        gridRow.Enabled = false;
            //        gridRow.BackColor = System.Drawing.Color.White;
            //        imgCross3.Visible = true;
            //        imgCross2.Visible = false;
            //        imgCross1.Visible = false;
            //        checklist.Visible = false;

            //        lblVisitdate.Text = CRM;
            //        lblVisitdate.ForeColor = System.Drawing.Color.Orange;
            //        lblVisitdate.Font.Bold = true;
            //    }
            //}


            //if (dsSalesForce != null)
            //{
            //    foreach (DataRow drDoc in dsSalesForce.Tables[0].Rows)
            //    {
            //        gridRow.Enabled = false;
            //        gridRow.BackColor = System.Drawing.Color.White;
            //        imgCross.Visible = true;
            //        imgCross2.Visible = false;
            //        imgCross1.Visible = false;
            //        checklist.Visible = false;

            //        lblVisitdate.Text = dsSalesForce.Tables[0].Rows[0]["Activity_Date"].ToString();

            //    }

            //}
        }
    }

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;

            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkListedDR");
            CheckBox chkAll = (CheckBox)e.Row.FindControl("chkAll");
            Image imgCross = (Image)e.Row.FindControl("imgCross");
            Image imgCross1 = (Image)e.Row.FindControl("imgCross1");
            Image imgCross2 = (Image)e.Row.FindControl("imgCross2");
            Image imgCross3 = (Image)e.Row.FindControl("imgCross3");
            //  Panel pnlImage = (Panel)e.Row.FindControl("pnlImage");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblVisitdate = (Label)e.Row.FindControl("lblVisit");
            // Label lblcommon = (Label)e.Row.FindControl("lblcom");
            ListedDR LstDocCamp = new ListedDR();
            dsSalesForcecamp = LstDocCamp.Deact_VisitdrCamp(sfCode, lblDocCode.Text, div_code);

            ListedDR LstDocCore = new ListedDR();
            dsSalesForcecore = LstDocCore.Deact_VisitdrCore(sfCode, lblDocCode.Text, div_code);

            ListedDR LstDocCore_Camp = new ListedDR();
            dsSalesForcecore_camp = LstDocCore_Camp.Deact_VisitdrCore_Camp(sfCode, lblDocCode.Text, div_code);

            ListedDR LstDocCRM = new ListedDR();
            dsSalesForcecrm = LstDocCRM.Deact_VisitdrCRM(sfCode, lblDocCode.Text, div_code);

            //if (dsSalesForcecamp.Tables[0].Rows.Count > 0)
            //{
            //    imgCross2.Visible = true;
            //    chkSelect.Visible = false;
            //    imgCross3.Visible = false;
            //    imgCross1.Visible = false;
            //    imgCross.Visible = false;
            //    lblVisitdate.Text = "CAMPAIGN";
            //    lblVisitdate.ForeColor = System.Drawing.Color.DarkGreen;
            //}
            //if (dsSalesForcecore.Tables[0].Rows.Count > 0)
            //{
            //    imgCross1.Visible = true;
            //    chkSelect.Visible = false;
            //    imgCross.Visible = false;
            //    imgCross2.Visible = false;
            //    imgCross3.Visible = false;
            //    lblVisitdate.Text = "CORE";
            //    lblVisitdate.ForeColor = System.Drawing.Color.DeepPink;
            //}

            //if (dsSalesForcecore_camp.Tables[0].Rows.Count > 0)
            //{
            //    imgCross2.Visible = true;
            //    chkSelect.Visible = false;
            //    imgCross1.Visible = false;
            //    imgCross.Visible = false;
            //    imgCross3.Visible = false;
            //    lblVisitdate.Text = "CORE & CAMP";
            //    lblVisitdate.ForeColor = System.Drawing.Color.LimeGreen;
            //}
            //if (dsSalesForcecrm.Tables[0].Rows.Count > 0)
            //{
            //    imgCross2.Visible = false;
            //    chkSelect.Visible = false;
            //    imgCross1.Visible = false;
            //    imgCross.Visible = false;
            //    imgCross3.Visible = true;
            //    lblVisitdate.Text = "CRM";
            //    lblVisitdate.ForeColor = System.Drawing.Color.Orange;
            //}


            if (lblVisitdate.Text.Contains("/"))
            {
                imgCross.Visible = true;
                imgCross2.Visible = false;
                imgCross1.Visible = false;
                chkSelect.Visible = false;
                imgCross3.Visible = false;
            }





            //if (lblcommon.Text != "")
            //{
            //    imgCross.Visible = true;
            //    chkSelect.Visible = false;
            //    //  e.Row.BackColor = System.Drawing.Color.Gray;
            //    e.Row.ToolTip = "Common Doctos(s) are not Possible to Deactivate";
            //    // e.Row.Attributes.Add("class", "border");
            //    e.Row.Font.Strikeout = true;

            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //    e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                LinkButton LnkHeaderText = new LinkButton();
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("LstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();
            int iflag = -1;

            if (Session["sf_type"].ToString() == "1")
            {
                ListedDR ListDrDeact = new ListedDR();
                dsadm = ListDrDeact.getDoc_Deact_Needed(div_code);
                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {
                        iflag = 1;
                    }
                    else
                    {
                        iflag = 3;
                    }
                }
            }
            else
            {
                iflag = 1;
            }
            if ((ListedDR.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Listed Doctor
                ListedDR lstDR = new ListedDR();
                iReturn = lstDR.DeActivate_Dr_Div(ListedDR, iflag, div_code);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
            //if (iflag == 1)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            //    FillDoc();
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivation Approval sent for the Manager');</script>");
            //    FillDoc();
            //}
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Listed Doctor De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            FillDoc();
        }

    }
}