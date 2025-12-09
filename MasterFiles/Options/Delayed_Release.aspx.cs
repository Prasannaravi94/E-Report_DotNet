using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Globalization;

public partial class MasterFiles_Options_Delayed_Release : System.Web.UI.Page
{
    DataSet dsadmin = null;
    DataSet dsadm = null;
    DataSet dsTP = null;
    DataSet dsDCR = null;
    DataSet dsState = null;
    DataSet dsDivision = null;
    DataSet dsVisit = null;
    DataSet dsDCR_New = null;
    string sState = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string[] statecd;
    string div_code = string.Empty;
    string Sf_Code = string.Empty;
    int time;
    DCR dc = new DCR();
    DataSet dsAdminSetup = null;
    DataSet dsAdminSetup2 = null;
    string LockSystem = string.Empty;
    string LockSystem2 = string.Empty;
    DataSet dsLock = null;
    string strSf_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Sf_Code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            AdminSetup ad = new AdminSetup();
            dsAdminSetup = ad.getLockSystem_AdmMR(div_code);
            if (dsAdminSetup.Tables[0].Rows.Count > 0)
            {
                LockSystem = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ViewState["LockSystemm"] = LockSystem;
            }
            AdminSetup adm = new AdminSetup();
            dsAdminSetup2 = adm.getLockSystem_AdmMGR(div_code);
            if (dsAdminSetup2.Tables[0].Rows.Count > 0)
            {
                LockSystem2 = dsAdminSetup2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ViewState["LockSystemm2"] = LockSystem2;
            }


            //if (LockSystem == "1" && LockSystem2 == "1")
            //{
            //    ddlYear.Visible = false;
            //    ddlMonth.Visible = false;
            //    lblYear.Visible = false;
            //    lblMonth.Visible = false;
            //    Span1.Visible = false;
            //    //txtNew.Visible = false;
            //}
            //else
            //{

            //    ddlYear.Visible = true;
            //    ddlMonth.Visible = true;
            //}

            SearchBy.Visible = false;
            ddlFields.Visible = false;
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            //GenerateMonth_year();

            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //TourPlan tp = new TourPlan();
            //dsTP = tp.Get_TP_Edit_Year(div_code);
            //if (dsTP.Tables[0].Rows.Count > 0)
            //{
            //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //    {
            //        ddlYear.Items.Add(k.ToString());
            //        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            //        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            //    }
            //}


            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                if (LockSystem == "1" && LockSystem2 == "1")
                {
                    ddlFieldForce.Items.Insert(0, new ListItem("---All Fieldforce---", "-1"));
                }

                else
                {

                    dsDCR = dc.get_Release_Sf(div_code, DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }
            }

            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                lnk.Enabled = false;
                lnk.Visible = false;


                if (LockSystem == "1" && LockSystem2 == "1")
                {
                    ddlFieldForce.Items.Insert(0, new ListItem("---All Fieldforce---", "-1"));

                    SalesForce sf = new SalesForce();
                    dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);
                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        //dsDCR.Tables[0].Rows[0].Delete();

                        foreach (DataRow drFF in dsDCR.Tables[0].Rows)
                        {
                            string code = drFF["sf_code"].ToString();
                            if (!code.Contains(Sf_Code))
                            {
                                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";
                            }
                        }

                        strSf_Code = strSf_Code.Substring(0, strSf_Code.Length - 1);


                        ViewState["strSf_Code"] = strSf_Code;
                    }
                }
                else
                {
                    SalesForce sf = new SalesForce();
                    dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        dsDCR.Tables[0].Rows[0].Delete();
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();


                    }

                }
            }


        }

        else
        {

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = this.Page.Title;
                //c1.FindControl("btnBack").Visible = false;
            }
            if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                      (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
            }
        }
        fillcolor();
        hHeading.InnerText = Page.Title;
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {

                if (Session["sf_type"].ToString() != "1")
                {
                    dsDCR = dc.get_Release_Sf(div_code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        //  ddlFieldForce.Items[1].Attributes.CssStyle.Add("color", "red");
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }
                else
                {
                    dsDCR = dc.get_Release_Sf_MR(Sf_Code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }

                dsDCR = dc.get_Release_Sf_Back(div_code, MonthVal.ToString(), YearVal.ToString());

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddlFieldForce1.DataTextField = "sf_name";
                    ddlFieldForce1.DataValueField = "sf_code";
                    ddlFieldForce1.DataSource = dsDCR;
                    ddlFieldForce1.DataBind();
                }
                fillcolor();
                //GetRelease();
                grdRelease.Visible = false;
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
            }

            else if (Session["sf_type"].ToString() == "2")
            {
                SalesForce sf = new SalesForce();
                dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    dsDCR.Tables[0].Rows[0].Delete();
                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsDCR;
                    ddlFieldForce.DataBind();
                }

                fillcolor();
                grdRelease.Visible = false;
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void GetRelease()
    {
        //if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        //{
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        DCR dc = new DCR();
        if (ddlFieldForce.SelectedValue == "0")
        {
            dsDCR = dc.get_Release_All_fieldforce(div_code, MonthVal.ToString(), YearVal.ToString());

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                ddlFields.SelectedValue = "StateName";
                search = ddlFields.SelectedValue.ToString();
                grdRelease.PageIndex = 0;

                if (search == "StateName")
                {
                    ddlSrc.Visible = true;
                    btnSearch.Visible = true;
                    FillState(div_code);
                    ddlSrc.Focus();
                }
                ddlSrc.Visible = true;
                SearchBy.Visible = true;
                ddlFields.Visible = true;
                grdRelease.Columns[8].Visible = false;
                grdRelease.Columns[10].Visible = true;
                btnSubmit.Visible = true;
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
        }
        else
        {
            dsDCR = dc.getReleaseDate(ddlFieldForce.SelectedValue.ToString(), MonthVal.ToString(), YearVal.ToString());
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSearch.Visible = false;
                ddlSrc.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                grdRelease.Columns[8].Visible = true;
                grdRelease.Columns[10].Visible = false;
                btnSubmit.Visible = true;
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
        }

        // }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        {
            Fill_Locksystem_sfcode();
        }

        else
        {
            GetRelease();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);
        if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        {
            foreach (GridViewRow gridRow in grdRelease.Rows)
            {
                Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
                string lblsfcode = lblsf_code.Text.ToString();
                CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
                bool bCheck = chkRelease.Checked;
                //Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
                //Label lblMode = (Label)gridRow.Cells[2].FindControl("lblMode");

                if (ddlFieldForce.SelectedValue == "-1" && (bCheck == true))
                {
                    DCR dcr = new DCR();
                    iReturn = dcr.Update_Delayed_Dates_ForAll_LockSystem(lblsf_code.Text, div_code);
                }


                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");
                    //Fill_Locksystem_sfcode();
                    grdRelease.Visible = false;
                    btnSubmit.Visible = false;

                }
            }
        }

        else
        {
            foreach (GridViewRow gridRow in grdRelease.Rows)
            {
                Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
                string lblsfcode = lblsf_code.Text.ToString();
                CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
                bool bCheck = chkRelease.Checked;
                Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
                Label lblMode = (Label)gridRow.Cells[2].FindControl("lblMode");

                if (ddlFieldForce.SelectedValue == "0" && (bCheck == true))
                {
                    DCR dcr = new DCR();
                    iReturn = dcr.Update_Delayed_Dates_ForAll(lblsf_code.Text, Convert.ToInt16(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()), div_code);
                }
                else
                {

                    if ((lblsfcode.Trim().Length > 0) && (bCheck == true))
                    {
                        DCR dcr = new DCR();
                        iReturn = dcr.Update_Delayed(lblsf_code.Text, Convert.ToDateTime(lblDate.Text), lblMode.Text, div_code);
                    }
                }
                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");
                    //GetRelease();

                }
            }
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBackColor = (Label)e.Row.FindControl("lblMode");
            if (lblBackColor.Text == "A")
            {
                string bcolor = "#ffdd99";
                e.Row.BackColor = System.Drawing.Color.FromName(bcolor);
            }
        }
        if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        {
            if (ddlFieldForce.SelectedValue == "-1")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");
                    Label lblSfName = (Label)e.Row.FindControl("lblSfName");

                    DCR dc = new DCR();
                    dsLock = dc.getlock_Delayed(lblsf_code.Text, div_code);

                    if (dsLock.Tables[0].Rows.Count > 0)
                    {
                        lblSfName.Text = lblSfName.Text + "<span style='color:magenta'> " + "" + "(Delayed)" + "" + "</span>";
                    }

                    dsState = dc.getlock_Missed(lblsf_code.Text, div_code);

                    if (dsState.Tables[0].Rows.Count > 0)
                    {
                        lblSfName.Text = lblSfName.Text + "<span style='color:#C85A17'> " + "" + " ( APP Missing )" + "" + "</span>";


                    }

                    //dsLock = dc.getAll_Lockwith_startdate(lblsf_code.Text, div_code);


                }
            }

        }

        if (ddlFieldForce.SelectedValue == "0")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");
                Label lblSfName = (Label)e.Row.FindControl("lblSfName");

                DCR dc = new DCR();
                dsLock = dc.getAllDelayed(div_code, MonthVal.ToString(), YearVal.ToString(), lblsf_code.Text);

                if (dsLock.Tables[0].Rows.Count > 0)
                {
                    lblSfName.Text = lblSfName.Text + "<span style='color:magenta'> " + "" + "(Delayed)" + "" + "</span>";
                }

                dsState = dc.getAll_Missed(div_code,  MonthVal.ToString(), YearVal.ToString(), lblsf_code.Text);

                if (dsState.Tables[0].Rows.Count > 0)
                {
                    lblSfName.Text = lblSfName.Text + "<span style='color:#C85A17'> " + "" + " ( APP Missing )" + "" + "</span>";


                }

                //dsLock = dc.getAll_Lockwith_startdate(lblsf_code.Text, div_code);


            }
        }
        if (ddlFieldForce.SelectedValue == "0")
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");
                //Label lblVisitCount = (Label)e.Row.FindControl("lblVisitCount");
                Label lblDelayed_Date = (Label)e.Row.FindControl("lblDelayed_Date");

                DCR dc = new DCR();
                dsVisit = dc.DCR_Delayed_missed_dates(lblsf_code.Text.Trim(), Convert.ToInt16(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()), div_code);

                if (dsVisit.Tables[0].Rows.Count > 0)
                {
                    string[] visit;
                    int i = 0;
                    lblDelayed_Date.Text = dsVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //visit = sReturn.Trim().Split('~');
                    //foreach (string sInd in visit)
                    //{
                    //    if (i == 0)
                    //        lblDelayed_Date.Text = sInd;
                    //    else
                    //        //lblVisitCount.Text = sInd;

                    //        i = i + 1;
                    //}
                }


            }
        }
    }
    private void fillcolor()
    {

        foreach (ListItem item in ddlFieldForce.Items)
        {
            if (item.Value == "0")
            {
                item.Attributes.Add("style", "background-color:" + "yellow"); ;
            }

        }
    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState(state_cd);
            ddlSrc.DataTextField = "statename";
            ddlSrc.DataValueField = "state_code";
            ddlSrc.DataSource = dsState;
            ddlSrc.DataBind();
        }
    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = ddlFields.SelectedValue.ToString();
        grdRelease.PageIndex = 0;

        if (search == "StateName")
        {
            ddlSrc.Visible = true;
            btnSearch.Visible = true;
            FillState(div_code);
            ddlSrc.Focus();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdRelease.PageIndex = 0;
        Search();

    }

    private void Search()
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        search = ddlFields.SelectedValue.ToString();

        if (search == "StateName")//changes in query
        {
            DCR dc = new DCR();
            dsDCR_New = dc.get_Release_All_fieldforce_Search(div_code, MonthVal.ToString(), YearVal.ToString(), ddlSrc.SelectedValue);//done by resh in query
            if (dsDCR_New.Tables[0].Rows.Count > 0)
            {
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR_New;
                grdRelease.DataBind();

            }
            else
            {
                grdRelease.DataSource = dsDCR_New;
                grdRelease.DataBind();
            }
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                if (Session["sf_type"].ToString() != "1")
                {
                    dsDCR = dc.get_Release_Sf(div_code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        // ddlFieldForce.Items[1].Attributes.CssStyle.Add("color", "red");
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }
                else
                {
                    dsDCR = dc.get_Release_Sf_MR(Sf_Code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }

                dsDCR = dc.get_Release_Sf_Back(div_code, MonthVal.ToString(), YearVal.ToString());

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddlFieldForce1.DataTextField = "sf_name";
                    ddlFieldForce1.DataValueField = "sf_code";
                    ddlFieldForce1.DataSource = dsDCR;
                    ddlFieldForce1.DataBind();
                }

                fillcolor();
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
                //GetRelease();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                SalesForce sf = new SalesForce();
                dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    dsDCR.Tables[0].Rows[0].Delete();
                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsDCR;
                    ddlFieldForce.DataBind();
                }

                fillcolor();
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        //txtNew.Visible = true;
        //lblYear.Visible = true;
        //lblMonth.Visible = true;
        //Span1.Visible = true;
        //ddlMonth.Visible = true;
        //ddlYear.Visible = true;
        ddlFieldForce1.Visible = true;
        btnGo1.Visible = true;
        ddlFieldForce.Visible = false;
        btnGo.Visible = false;
        grdRelease.Visible = false;
        btnSubmit.Visible = false;

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        {
            // ddlFieldForce1.Items.Insert(0, new ListItem("---All Fieldforce---", "-1"));
            dsDCR = dc.get_Release_Sf_Back(div_code, MonthVal.ToString(), YearVal.ToString());

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce1.DataTextField = "sf_name";
                ddlFieldForce1.DataValueField = "sf_code";
                ddlFieldForce1.DataSource = dsDCR;
                ddlFieldForce1.DataBind();
            }
        }
        else
        {
            dsDCR = dc.get_Release_Sf_Back(div_code, MonthVal.ToString(), YearVal.ToString());

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce1.DataTextField = "sf_name";
                ddlFieldForce1.DataValueField = "sf_code";
                ddlFieldForce1.DataSource = dsDCR;
                ddlFieldForce1.DataBind();
            }
        }
    }

    protected void btnGo1_Click(object sender, EventArgs e)
    {
        //if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        //{
        //    GetRelease_BackLockSystem();
        //}
        //else
        //{
        GetRelease_Back();
        //}
    }

    private void GetRelease_Back()
    {
        //if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        //{
        DCR dc = new DCR();

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        dsDCR = dc.getReleaseDate_Back(ddlFieldForce1.SelectedValue.ToString(), MonthVal.ToString(), YearVal.ToString());
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.Visible = false;
            ddlFieldForce1.Visible = true;
            btnGo.Visible = false;
            btnGo1.Visible = true;
            btnSearch.Visible = false;
            ddlSrc.Visible = false;
            SearchBy.Visible = false;
            ddlFields.Visible = false;
            grdRelease.Columns[8].Visible = true;
            grdRelease.Columns[10].Visible = false;
            btnSubmit.Visible = false;
            btnSubmit_Back.Visible = true;
            grdRelease.Visible = true;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
        // }

    }

    protected void btnSubmit_Back_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        if (ViewState["LockSystemm"].ToString() == "1" && ViewState["LockSystemm2"].ToString() == "1")
        {
            foreach (GridViewRow gridRow in grdRelease.Rows)
            {
                Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
                string lblsfcode = lblsf_code.Text.ToString();
                CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
                bool bCheck = chkRelease.Checked;
                Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
                Label lblMode = (Label)gridRow.Cells[2].FindControl("lblMode");



                if ((lblsfcode.Trim().Length > 0) && (bCheck == true))
                {
                    DCR dcr = new DCR();
                    // iReturn = dcr.Update_Delayed_back_LockSystem(lblsf_code.Text, div_code);

                    iReturn = dcr.Update_Delayed_back_totlock(lblsf_code.Text, Convert.ToDateTime(lblDate.Text), lblMode.Text);
                }

                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");
                    //GetRelease_Back();

                }
            }
        }
        else
        {
            foreach (GridViewRow gridRow in grdRelease.Rows)
            {
                Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
                string lblsfcode = lblsf_code.Text.ToString();
                CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
                bool bCheck = chkRelease.Checked;
                Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
                Label lblMode = (Label)gridRow.Cells[2].FindControl("lblMode");



                if ((lblsfcode.Trim().Length > 0) && (bCheck == true))
                {
                    DCR dcr = new DCR();
                    iReturn = dcr.Update_Delayed_back(lblsf_code.Text, Convert.ToDateTime(lblDate.Text), lblMode.Text);
                }

                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");
                    //GetRelease_Back();

                }
            }
        }
    }

    private void Fill_Locksystem_sfcode()
    {

        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            DCR dcr = new DCR();
            dsDCR = dcr.get_Release_All_fieldforce_LockSystem(div_code);

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                grdRelease.Columns[8].Visible = false;
                grdRelease.Columns[10].Visible = false;
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            DCR dcr = new DCR();
            dsDCR = dcr.get_Release_All_fieldforce_LockSystem_MGR(div_code, ViewState["strSf_Code"].ToString());

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                grdRelease.Columns[8].Visible = false;
                grdRelease.Columns[10].Visible = false;
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
        }
    }

    private void GetRelease_BackLockSystem()
    {
        DCR dc = new DCR();

        dsDCR = dc.getReleaseDate_Back_LockSystem(div_code);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.Visible = false;
            ddlFieldForce1.Visible = true;
            btnGo.Visible = false;
            btnGo1.Visible = true;
            btnSearch.Visible = false;
            ddlSrc.Visible = false;
            SearchBy.Visible = false;
            ddlFields.Visible = false;
            grdRelease.Columns[8].Visible = false;
            grdRelease.Columns[10].Visible = false;
            btnSubmit.Visible = false;
            btnSubmit_Back.Visible = true;
            grdRelease.Visible = true;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdRelease.DataSource = dsDCR;
            grdRelease.DataBind();
        }
    }

    //private void GenerateMonth_year()
    //{
    //    ddlMonth.Items.Clear();
    //    ddlYear.Items.Clear();

    //    int months = 12;

    //    for (int i = 1; i <= months; i++)
    //    {
    //        if (i == DateTime.Now.Month)
    //        {
    //            if (i == 1)
    //            {
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(12).Substring(0, 3), (12).ToString()));
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //                ddlYear.Items.Add(DateTime.Now.AddYears(-1).Year.ToString());
    //                ddlYear.Items.Add(DateTime.Now.Year.ToString());

    //            }
    //            else
    //            {
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i - 1).Substring(0, 3), (i - 1).ToString()));
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //                ddlYear.Items.Add(DateTime.Now.Year.ToString());
    //            }

    //        }
    //    }
    //    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();


    //}
    //protected void lblMonth_Click(object sender, EventArgs e)
    //{
    //    fill_monthyear();
    //}

    //private void fill_monthyear()
    //{

    //    ddlMonth.Items.Clear();
    //    ddlYear.Items.Clear();

    //    TourPlan tp = new TourPlan();
    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlYear.Items.Add(k.ToString());

    //        }

    //        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //    }

    //    int months = 12;

    //    for (int i = 1; i <= months; i++)
    //    {
    //        ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //    }

    //    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //}

    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {

                if (Session["sf_type"].ToString() != "1")
                {
                    dsDCR = dc.get_Release_Sf(div_code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        //  ddlFieldForce.Items[1].Attributes.CssStyle.Add("color", "red");
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }
                else
                {
                    dsDCR = dc.get_Release_Sf_MR(Sf_Code, MonthVal.ToString(), YearVal.ToString());

                    if (dsDCR.Tables[0].Rows.Count > 0)
                    {
                        ddlFieldForce.DataTextField = "sf_name";
                        ddlFieldForce.DataValueField = "sf_code";
                        ddlFieldForce.DataSource = dsDCR;
                        ddlFieldForce.DataBind();
                    }
                }

                dsDCR = dc.get_Release_Sf_Back(div_code, MonthVal.ToString(), YearVal.ToString());

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddlFieldForce1.DataTextField = "sf_name";
                    ddlFieldForce1.DataValueField = "sf_code";
                    ddlFieldForce1.DataSource = dsDCR;
                    ddlFieldForce1.DataBind();
                }
                fillcolor();
                //GetRelease();
                grdRelease.Visible = false;
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
            }

            else if (Session["sf_type"].ToString() == "2")
            {
                SalesForce sf = new SalesForce();
                dsDCR = sf.SalesForceListMgrGet(div_code, Sf_Code);

                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    dsDCR.Tables[0].Rows[0].Delete();
                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsDCR;
                    ddlFieldForce.DataBind();
                }

                fillcolor();
                grdRelease.Visible = false;
                grdRelease.Visible = false;
                SearchBy.Visible = false;
                ddlFields.Visible = false;
                btnSearch.Visible = false;
                btnSubmit.Visible = false;
                ddlSrc.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}