using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI.DataVisualization.Charting;


public partial class MasterFiles_User_List : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsUserList = null;
    DataSet dsDivision = null;
    DataSet dsSalesForce = null;
    DataSet dsAT = null;
    DataSet dsATM = null;
    DataSet dsDiv = null;
    DataSet dsSal = null;

    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string sf_type = string.Empty;
    SalesForce sf = new SalesForce();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    string strMultiDiv = string.Empty;
    string sf_code = string.Empty;
    string bcolor = string.Empty;
    DataTable dt = new DataTable();
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                btnExcelSubmit.Visible = true;
                Filldiv();
                FillManagers();
                //  ddlDivision.SelectedIndex = 1;
                ddlDivision_SelectedIndexChanged(sender, e);
                ddlFieldForce.SelectedIndex = 1;
                btnGo.Focus();
                btnmgrgo.Visible = false;
                FillSub_Division();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                lblsubdiv.Visible = false;
                ddlsubdiv.Visible = false;
                Product prd = new Product();
                DataSet dsdiv = new DataSet();
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                        ddlDivision.Visible = true;
                        lblDivision.Visible = true;
                        btnGo.Visible = true;
                        getDivision();
                    }
                    else
                    {

                        ddlDivision.Visible = false;
                        lblDivision.Visible = false;
                        btnGo.Visible = false;
                        btnmgrgo.Visible = true;
                        BindUserList();
                    }
                }
            }
        }

        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_TP_Menu c1 =
           (UserControl_MGR_TP_Menu)LoadControl("~/UserControl/MGR_TP_Menu.ascx");
            Divid.Controls.Add(c1);
            //c1.Title = Page.Title;
            //// c1.FindControl("btnBack").Visible = false;
            lblFilter.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            grdSalesForce.Columns[8].Visible = false;
            grdSalesForce.Columns[10].Visible = false;
        }
        else if (Session["sf_type"].ToString() == "")
        {
            UserControl_pnlMenu_TP c1 =
           (UserControl_pnlMenu_TP)LoadControl("~/UserControl/pnlMenu_TP.ascx");
            Divid.Controls.Add(c1);
            //c1.Title = Page.Title;
            //// c1.FindControl("btnBack").Visible = false;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu_TP c1 =
           (UserControl_pnlMenu_TP)LoadControl("~/UserControl/pnlMenu_TP.ascx");
            Divid.Controls.Add(c1);
            //c1.Title = Page.Title;
            //// c1.FindControl("btnBack").Visible = false;
        }
        FillColor();

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

    private void BindUserList()
    {

        dsdiv = prd.getMultiDivsf_Name(sf_code);

        string strVacant = "1";
        if (chkVacant.Checked == true)
        {
            strVacant = "0";
        }
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }
        }
        DataTable dtUserList = new DataTable();
        if (chkVacant.Checked == true)
        {
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dtUserList = sf.getUserListReportingToNew(div_code, sf_code, 0, Session["sf_type"].ToString());
            }
            else
            {
                // Fetch Managers Audit Team
                dtUserList = ds.getAuditManagerTeam_User(div_code, sf_code, 0);
                //  dsmgrsf.Tables.Add(dt);
                //  dsSalesForce = dsmgrsf;
            }
        }
        else
        {
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dtUserList = sf.getUserListReportingToAllNew(div_code, sf_code, 0, Session["sf_type"].ToString());
            }
            else
            {
                dtUserList = ds.getAuditManagerTeam_UserAll(div_code, sf_code, 0);
            }
        }

        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();

        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ToolTip = (e.Row.DataItem as DataRowView)["sf_code"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code1 = (Label)e.Row.FindControl("lblSF_Code");
            LinkButton Tp_Active_flag = (LinkButton)e.Row.FindControl("lblS");
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                if (ddlsubdiv.SelectedValue != "0")
                {


                    //if (lblSF_Code1.Text.Contains("MR"))
                    //{
                    SalesForce sal = new SalesForce();
                    dsSal = sal.checkMr_subdiv(ddlDivision.SelectedValue, lblSF_Code1.Text, ddlsubdiv.SelectedValue);

                    if (dsSal.Tables[0].Rows.Count > 0)
                    {


                    }
                    else
                    {

                        e.Row.Visible = false;
                    }

                }
            }

            //if (chkVacant.Checked == false)
            //{
            //    if (lblSF_Code1.Text.Contains("MGR") && Tp_Active_flag.Text.Contains("Vacant"))
            //    {
            //        e.Row.Visible = false;
            //    }
            //}

            if (chkBase.Checked == false)
            {
                if (lblSF_Code1.Text.Contains("MR") && Tp_Active_flag.Text.Contains("Vacant"))
                {
                    e.Row.Visible = false;
                }
            }

            Label lblBackColor = (Label)e.Row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            e.Row.BackColor = System.Drawing.Color.FromName(bcolor);

            LinkButton lblS = (LinkButton)e.Row.FindControl("lblS");
            if (lblS.Text == "Vacant")
            {
                lblS.ForeColor = System.Drawing.Color.Red;
                //lblS.Style.Add("font-size", "12pt");
                lblS.Style.Add("font-weight", "Bold");
            }

            if (lblS.Text == "Hold")
            {
                lblS.ForeColor = System.Drawing.Color.Red;
                //lblS.Style.Add("font-size", "12pt");
                lblS.Style.Add("font-weight", "Bold");

                // string url = "User_List.aspx?sf_code=" + sf_code;

                //  lblS.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");




                //  lblS.Attributes.Add("href", "javascript:showModalPopUp('" + "" + "', '" + "" + "', '" + "" + "', '" + "" + "','" + "" + "','" + "" + "', '" + "" + "','" + "" + "')");



                // HyperLink hyhold = new HyperLink();

                //e.Row.Cells[11].Controls.Add(hyhold);
            }
            if (lblS.Text == "Blocked")
            {
                lblS.ForeColor = System.Drawing.Color.Red;
                //lblS.Style.Add("font-size", "12pt");
                lblS.Style.Add("font-weight", "Bold");
            }
            // Added by Sridevi - To Highlight Managers in UserList - 8-Nov-15
            Label lblSFType = (Label)e.Row.FindControl("lblSFType");
            if (lblSFType.Text == "2") // Manager
            {
                //e.Row.Style.Add("font-size", "16pt");
                e.Row.Style.Add("font-weight", "Bold");
            }
            // Ends Here  
            HyperLink lblDrsCnt = (HyperLink)e.Row.FindControl("lblDrsCnt");
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            ListedDR lstdr = new ListedDR();
            DataSet dsdr = new DataSet();
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
            }
            else
            {
                div_code = ddlDivision.SelectedValue;
            }
            if (chkdoctor.Checked == false)
            {
                dsdr = lstdr.getListDr_CountNew(lblSF_Code.Text, div_code);
                grdSalesForce.Columns[1].Visible = true;
                grdSalesForce.Columns[5].Visible = true;

                if (dsdr.Tables[0].Rows.Count > 0)
                {
                    lblDrsCnt.Text = dsdr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (lblDrsCnt.Text == "0")
                {
                    lblDrsCnt.Text = "***";
                    lblDrsCnt.Style.Add("pointer-events", "none;");
                    lblDrsCnt.Style.Add("cursor", "default");
                }
            }
            else
            {
                grdSalesForce.Columns[1].Visible = false;
                grdSalesForce.Columns[5].Visible = false;
            }

        }
        FillAuditTeam();
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }

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


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            //ddlFieldForce.Items[j].Selected = true;

            //if (ColorItems.Text == "Level1")
            //    //ColorItems.Attributes.Add("style", "background-color: Wheat");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            //if (ColorItems.Text == "Level2")
            //    //ColorItems.Attributes.Add("style", "background-color: Blue");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            //if (ColorItems.Text == "Level3")
            //    //ColorItems.Attributes.Add("style", "background-color: Cyan");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            //if (ColorItems.Text == "Level4")
            //    //ColorItems.Attributes.Add("style", "background-color: Lavendar");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;

        }
    }

    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
        FillAuditTeam();
    }
    private void FillAuditTeam()
    {
        // To show  audit team.
        SalesForce sf = new SalesForce();
        dsAT = sf.getAuditTeam(ddlDivision.SelectedValue.ToString());
        if (dsAT.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drFF in dsAT.Tables[0].Rows)
            {
                foreach (GridViewRow grid_row in grdSalesForce.Rows)
                {

                    //string AuditMgr = dsATM.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //string[] Audit;
                    //Audit = AuditMgr.Split(',');
                    //foreach (string Au_cd in Audit)
                    //{
                    Label lblsfCode = (Label)grid_row.FindControl("lblSF_Code");
                    Label lblFieldForce = (Label)grid_row.FindControl("lblFieldForce");
                    if (drFF["sf_code"].ToString() == lblsfCode.Text)
                    {
                        // grid_row.BackColor = System.Drawing.Color.Yellow;
                        if (drFF["Audit_team"].ToString().Length > 0)
                        {
                            lblFieldForce.ForeColor = System.Drawing.Color.White;
                            lblFieldForce.BackColor = System.Drawing.Color.Green;
                        }
                        // lblFieldForce.Style.Add("font-size", "12pt");
                        //  lblFieldForce.Style.Add("font-weight", "Bold");
                    }
                    // }
                }
            }
        }
    }
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    private void FillUserList()
    {

        string sMgr = "admin";
        SalesForce sf = new SalesForce();
        string strVacant = "1";
        if (chkVacant.Checked == true)
        {
            strVacant = "0";
        }
        if (ddlFieldForce.SelectedIndex > 0)
        {
            sMgr = ddlFieldForce.SelectedValue;
        }

        //// Commented the below code //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15
        ////  dsUserList = sf.UserList_Self(ddlDivision.SelectedValue, sMgr);
        //dsUserList = sf.UserList_Self_Vacant(ddlDivision.SelectedValue, sMgr, strVacant);
        //if (dsUserList.Tables[0].Rows.Count > 0)
        //{
        //    grdSalesForce.Visible = true;
        //    grdSalesForce.DataSource = dsUserList;
        //    grdSalesForce.DataBind();
        //}
        //else
        //{
        //    grdSalesForce.DataSource = dsUserList;
        //    grdSalesForce.DataBind();
        //}
        //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15

        DataTable dtUserList = new DataTable();
        DataSet ds = new DataSet();
        if (chkVacant.Checked == true)
        {
            dtUserList = sf.getUserListReportingToNew(ddlDivision.SelectedValue, sMgr, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
            //ds = sf.Hierarchy_Team(ddlDivision.SelectedValue, sMgr);
            //dtUserList.Merge(ds.Tables[0]);
            //ViewState["dtUserList"] = dtUserList;
        }
        else
        {
            dtUserList = sf.getUserListReportingToAllNew(ddlDivision.SelectedValue, sMgr, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        }

        if (dtUserList.Rows.Count > 0)
        {
            //dtUserList = (DataTable)ViewState["dtUserList"];
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedIndex == 0)
        {
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        }
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
            FillColor();
            FillgridColor();
        }


    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnlprint.Visible = true;
        System.Threading.Thread.Sleep(time);
        if (Session["sf_type"].ToString() == "2")
        {
            BindUserList();
        }
        else
        {
            FillUserList();
        }
        FillgridColor();
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
        FillgridColor();
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillgridColor();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdSalesForce.Visible = false;
        pnlprint.Visible = false;
        FillManagers();
        FillColor();
        FillgridColor();
        FillSub_Division();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=UserList.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        grdSalesForce.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(grdSalesForce);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "UserList";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        grdSalesForce.HeaderRow.Style.Add("font-size", "10px");
        grdSalesForce.Style.Add("text-decoration", "none");
        grdSalesForce.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        grdSalesForce.Style.Add("font-size", "8px");

        grdSalesForce.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(grdSalesForce);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        //  Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);

        Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);

        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void btnmgrgo_Click(object sender, EventArgs e)
    {
        btnGo_Click(sender, e);
    }

    private void FillSub_Division()
    {
        SalesForce sf = new SalesForce();

        dsDiv = sf.getsubdiv_userlist(ddlDivision.SelectedValue);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {
            ddlsubdiv.DataTextField = "subdivision_name";
            ddlsubdiv.DataValueField = "subdivision_code";
            ddlsubdiv.DataSource = dsDiv;
            ddlsubdiv.DataBind();
        }
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


    protected void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Sort"))
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

            DataView sortedView = new DataView((DataTable)ViewState["GridView"]);
            sortedView.Sort = e.CommandArgument + " " + sortingDirection;
            grdSalesForce.DataSource = sortedView;
            grdSalesForce.DataBind();
        }
    }
    protected void btnExcelSubmit_Click(object sender, EventArgs e)
    {

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //userlst_Dump_test2 crt wrk
        SqlCommand cmd = new SqlCommand("userlst_Dump_test4", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", ddlDivision.SelectedValue);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);



        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        // ds.Tables[0].Columns.Remove("ListedDrCode");
        ds.Tables[0].Columns.Remove("Sf_Code");


        dt = ds.Tables[0];
        con.Close();


        //Dev  by js
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "UserListdump.xls"));
        Response.ContentType = "application/ms-excel";
        //DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {

            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();


    }
}