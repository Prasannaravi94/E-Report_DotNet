using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_Chemist_Campaign_Chemist_SubCategory_Map : System.Web.UI.Page
{
    DataSet dsListedCHE = null;
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsCheSubCat = null;
    bool bsrch = false;
    DataSet dsCatgType = null;
    string Listed_DR_Code = string.Empty;
    string Chetype = string.Empty;
    string chkCampaign = string.Empty;
    string Che_SubCatCode = string.Empty;
    DataSet dsDR = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsChe = null;
    string sCmd = string.Empty;
    int iReturn = -1;
    int time;
    int search = 0;
    int iIndex = -1;
    DataSet dsadm = new DataSet();
    DataSet dsSalesForce = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
             //   Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                lblMR.Visible = false;
                ddlMR.Visible = false;
                ddlMR.Enabled = false;
                //AdminSetup adm = new AdminSetup();
                //dsadm = adm.get_camp_lock(sf_code, div_code);
                //if (dsadm.Tables[0].Rows.Count > 0)
                //{
                //    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                //    {
                //        //FillChe();
                //        //FillCampaign();
                //        getWorkName();
                //        btnDraft.Visible = true;
                //        btnSave.Visible = true;
                //        btnSubmit.Visible = true;
                //        lbllock.Visible = false;
                //    }
                //    else if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                //    {
                //        btnDraft.Visible = false;
                //        btnSave.Visible = false;
                //        btnSubmit.Visible = false;
                //        lbllock.Visible = false;
                //        ddlSrch.Visible = false;
                //        lblType.Visible = false;
                //        btnOk.Visible = false;
                //    }
                //}
                //else
                //{
                    //FillChe();
                    //FillCampaign();
                    getWorkName();
                    btnDraft.Visible = true;
                    btnSave.Visible = true;
                    btnSubmit.Visible = true;
                    lbllock.Visible = false;
                    Pnldiv.Visible = false;
                //}
            }
            //else if (Session["sf_type"].ToString() == "2")
            //{

            //    UserControl_MGR_Menu Usc_Menu =
            //    (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //    Divid.Controls.Add(Usc_Menu);
            //    Usc_Menu.FindControl("btnBack").Visible = false;
            //    Usc_Menu.Title = this.Page.Title;
            //    FillSalesForce();
            //    FillDoc();
            //    FillCampaign();
            //    getWorkName();


            //}
            //else
            //{
            //    UserControl_MenuUserControl Usc_Menu =
            //   (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //    Divid.Controls.Add(Usc_Menu);
            //    Usc_Menu.FindControl("btnBack").Visible = false;
            //    Usc_Menu.Title = this.Page.Title;
            //    FillSalesForce();
            //    FillDoc();
            //    FillCampaign();
            //    getWorkName();
            //}
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR1 =
                        (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
              //  Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;
                Usc_MR1.FindControl("btnBack").Visible = false;

            }
            //else if (Session["sf_type"].ToString() == "2")
            //{
            //    UserControl_MGR_Menu Usc_MR1 =
            //            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //    Divid.Controls.Add(Usc_MR1);
            //    Usc_MR1.Title = this.Page.Title;
            //    Usc_MR1.FindControl("btnBack").Visible = false;
            //}
            //else
            //{
            //    UserControl_MenuUserControl Usc_Menu =
            //   (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //    Divid.Controls.Add(Usc_Menu);

            //    Usc_Menu.FindControl("btnBack").Visible = false;
            //}
        }
        Pnldiv.Visible = false;
        btnDraft.Visible = false;
        btnSave.Visible = false;
        btnSubmit.Visible = false;
        lbllock.Visible = false;
      


    }
    private void FillSalesForce()
    {
        sf_code = Session["sf_code"].ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();

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
        ChemistCampaign LstChe = new ChemistCampaign();


        dtGrid = LstChe.getListedChemistList_DataTable_camp(sf_code);

        return dtGrid;
    }
    protected void grdChemist_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemist.DataSource = sortedView;
        grdChemist.DataBind();
    }
    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void GetCampaign()
    {
        foreach (GridViewRow row in grdChemist.Rows)
        {
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");

                if (chk.Checked == false)
                {
                    chk.Checked = false;
                    chk.Style.Add("color", "Black");
                }
            }
        }
    }
    private void FillCampaign()
    {
        foreach (GridViewRow row in grdChemist.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            TextBox txtChemists_Phone = (TextBox)row.FindControl("txtChemists_Phone");
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            string str_CateCode = "";
            ChemistCampaign LstChe = new ChemistCampaign();
            dsListedDR = LstChe.get_CampChk_New(lblcode.Text, div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                str_CateCode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            }

            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                Label SubCatCode = (Label)grid.FindControl("cbSubCat");

                string[] Salesforce;
                if (str_CateCode != "")
                {
                    iIndex = -1;
                    Salesforce = str_CateCode.Split(',');
                    foreach (string sf in Salesforce)
                    {

                        CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                        Label hf = (Label)grid.FindControl("cbSubCat");

                        if (sf == hf.Text)
                        {
                            chk.Checked = true;
                            chk.Attributes.Add("style", "Color: Red; font-weight:Bold; ");
                        }
                    }
                }
            }
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


    private void FillChe()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlMR.SelectedValue.Trim();
        }
        ChemistCampaign LstChe = new ChemistCampaign();
        dsListedCHE = LstChe.getListedChe_CampMap_New(sf_code, div_code);
        if (dsListedCHE.Tables[0].Rows.Count > 0)
        {

            grdChemist.Visible = true;
            grdChemist.DataSource = dsListedCHE;
            grdChemist.DataBind();

        }
        else
        {
            grdChemist.DataSource = dsListedCHE;
            grdChemist.DataBind();

        }
        foreach (DataRow drFF in dsListedCHE.Tables[0].Rows)
        {
            foreach (GridViewRow grid_row in grdChemist.Rows)
            {
                Label lblDrName = (Label)grid_row.FindControl("lblDrName");
                Label lblDrcode = (Label)grid_row.FindControl("lblDrcode");
                if (drFF["Chemists_Code"].ToString() == lblDrcode.Text)
                {
                    if (drFF["chm_campaign_name"].ToString().Length > 0)
                    {
                        lblDrName.ForeColor = System.Drawing.Color.White;
                        lblDrName.BackColor = System.Drawing.Color.BlueViolet;
                    }
                }
            }
        }
    }
    //protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (search == 7)
        {
            txtsearch.Visible = true;

            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = true;

        }

        if (search == 1)
        {
            ddlSrc2.Visible = false;
            FillChe();
        }

        if (search == 6)
        {
            FillTerr();
        }

    }

    private void FillTerr()
    {
        ChemistCampaign lstDR = new ChemistCampaign();
        dsDR = lstDR.FetchTerritory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ChemistCampaign LstChe = new ChemistCampaign();
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        btnSave.Visible = true;
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlMR.SelectedValue.Trim();
        }
        if (search == 1)
        {

            FillChe();
        }

        if (search == 6)
        {

            dsChe = LstChe.getListedDrforTerr_Camp_New(sf_code, ddlSrc2.SelectedValue,div_code);
            if (dsChe.Tables[0].Rows.Count > 0)
            {
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChe;
                grdChemist.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdChemist.DataSource = dsChe;
                grdChemist.DataBind();
            }
        }
        FillCampaign();
        Pnldiv.Visible = true;
        btnDraft.Visible = true;
        btnSave.Visible = true;
        btnSubmit.Visible = true;

    }
    protected void grdCampaign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("checked", Page.ClientScript.GetPostBackEventReference(sender as GridView, "Select$" + e.Row.RowIndex.ToString()));
        }

    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = "Chemist " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new ListItem(str, "6"));
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            GridView GCamp = (GridView)e.Row.FindControl("grdCampaign");

            ChemistCampaign dv = new ChemistCampaign();
            dsCheSubCat = dv.getCheSubCat(div_code);
            if (dsCheSubCat.Tables[0].Rows.Count > 0)
            {
                GCamp.Visible = true;
                GCamp.DataSource = dsCheSubCat;
                GCamp.DataBind();
            }
            else
            {
                GCamp.DataSource = dsCheSubCat;
                GCamp.DataBind();
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
            if (ddlType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByText(row["Doc_Type"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[3].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnSubmit_Click(sender, e);
    }
    //protected void chkCatName_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    string CampaignName = string.Empty;
    //    foreach (GridViewRow row in grdDoctor.Rows)
    //    {
    //        Label lblcode = (Label)row.FindControl("lblDrcode");
    //        Label lblCatName = (Label)row.FindControl("Doc_SubCatName");

    //        chkCampaign = "";
    //        CampaignName = "";
    //        GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
    //        foreach (GridViewRow grid in grdCampaign.Rows)
    //        {

    //            CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
    //            Label hf = (Label)grid.FindControl("cbSubCat");

    //            if (chk.Checked)
    //            {
                   
    //                chkCampaign = chkCampaign + hf.Text + ",";
    //                CampaignName = CampaignName + chk.Text + ",";
    //            }

    //        }
    //        lblCatName.Text = CampaignName;


    //    }
    //}
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            row.Focus();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlMR.SelectedValue.Trim();
        }
        foreach (GridViewRow row in grdChemist.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            TextBox txtChemists_Phone = (TextBox)row.FindControl("txtChemists_Phone");
            if (lblcode.Text != "-1")
            {
                Label lblSCat = (Label)row.FindControl("Che_SubCatName");
                chkCampaign = "";
                string chkCamp = "";
                GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
                foreach (GridViewRow grid in grdCampaign.Rows)
                {
                    CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                    Label hf = (Label)grid.FindControl("cbSubCat");
                    if (chk.Checked)
                    {
                        chkCampaign = chkCampaign + hf.Text + ",";
                    }

                }
                if (chkCampaign != "")
                {
                    chkCamp = chkCampaign;
                }
                else
                {
                    chkCamp = "";
                }

                ChemistCampaign lst = new ChemistCampaign();
               // int iReturn = lst.Map_Campaign(chkCampaign, lblcode.Text, div_code, sf_code);
                int iReturn = lst.Map_Campaign_New(chkCampaign, lblcode.Text, txtChemists_Phone.Text, div_code, sf_code);

                //if (iReturn > 0)
                //{
                 
                //    if (Session["sf_type"].ToString() == "1")
                //    {
                //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');window.location='Chemist-SubCategory-Map.aspx'</script>");
                //    }
                //    else
                //    {
                //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");

                //    }
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exist');</script>");
                //}
            }
        }
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');window.location='Chemist-SubCategory-Map.aspx'</script>");
        if (Session["sf_type"].ToString() == "1")
        {
        }
        else
        {
            FillChe();
            FillCampaign();
        }
        //AdminSetup adm2 = new AdminSetup();
        //int iReturn2 = adm2.Campaign_Lock(sf_code, div_code, "1");
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlMR.SelectedValue.Trim();
        }
        foreach (GridViewRow row in grdChemist.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            TextBox txtChemists_Phone = (TextBox)row.FindControl("txtChemists_Phone");
            if (lblcode.Text != "-1")
            {
                Label lblSCat = (Label)row.FindControl("Che_SubCatName");
                chkCampaign = "";
                string chkCamp = "";
                GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
                foreach (GridViewRow grid in grdCampaign.Rows)
                {
                    CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                    Label hf = (Label)grid.FindControl("cbSubCat");
                    if (chk.Checked)
                    {
                        chkCampaign = chkCampaign + hf.Text + ",";
                    }

                }
                if (chkCampaign != "")
                {
                    chkCamp = chkCampaign;
                }
                else
                {
                    chkCamp = "";
                }
         
                //ListedDR lst = new ListedDR();
                AdminSetup adm = new AdminSetup();
                //int iReturn = lst.Map_Campaign(chkCampaign, lblcode.Text, div_code, sf_code);
                ChemistCampaign lst = new ChemistCampaign();
              //  int iReturn = lst.Map_Campaign(chkCampaign, lblcode.Text, div_code, sf_code);
                int iReturn = lst.Map_Campaign_New(chkCampaign, lblcode.Text, txtChemists_Phone.Text, div_code, sf_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                }
            }
        }
        //AdminSetup adm2 = new AdminSetup();
        //int iReturn2 = adm2.Campaign_Lock(sf_code, div_code, "0");
        FillChe();
        FillCampaign();
    }
}