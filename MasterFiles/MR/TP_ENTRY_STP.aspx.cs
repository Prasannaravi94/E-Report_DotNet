using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using DBase_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class MasterFiles_MR_TP_ENTRY_STP : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsTP2 = null;
    DataSet dsTPC = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = new DataSet();
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr_Value = string.Empty;
    string TP_Terr_Name = string.Empty;
    string strddlWT = string.Empty;
    string strddlFWText = string.Empty;
    string TP_Terr1_Value = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2_Value = string.Empty;
    string TP_Terr2_Name = string.Empty;
    bool TP_Submit = false;
    bool EmptyWT = false;
    bool EmptyTerr = false;
    string wrktype_code = string.Empty;
    string ddlWwrktype_name = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    string strTPView = string.Empty;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;
    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    DataSet dsWeekoff = null;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_type = string.Empty;
    string MR_Code = string.Empty;
    string MR_Month = string.Empty;
    string MR_Year = string.Empty;
    string sQryStr = string.Empty;
    string Edit = string.Empty;
    string StrMonth = string.Empty;
    string ID = string.Empty;
    DataSet dsWorkTypeSettings = null;
    string strIndex = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsWrktype = null;
    string month = string.Empty;
    string year = string.Empty;
    DataSet dsDr = null;
    string Index = string.Empty;
    DataSet dsSTP = new DataSet();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        Edit = Request.QueryString["Edit"];
        sQryStr = Request.QueryString["refer"];
        Index = Request.QueryString["Index"];

        if (sQryStr != null && sQryStr != "")
        {
            MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Code.Length) - 1);
            MR_Month = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Month.Length) - 1);
            MR_Year = sQryStr.Trim();

            Session["sf_code_mr"] = MR_Code;

            if (sQryStr.Length > 0)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;

                btnReject.Visible = true;
                btnApprove.Visible = true;
                this.Page.Title = "TP - Approval";
                menu.Visible = false;

                lblsf_name.Visible = false;
                lblempid.Visible = false;
                lbldoj2.Visible = false;
                lblreportname.Visible = false;
                lblname.Visible = false;
                lblemp.Visible = false;
                lbldoj.Visible = false;
                lblreport.Visible = false;
            }
        }



        if (!Page.IsPostBack)
        {
          



            if (Request.QueryString["Index"] != null)
            {
                menu.Visible = false;
            }
            else
            {

                menu.Title = this.Page.Title;
            }
            // menu.FindControl("btnBack").Visible = false;

            if (Session["sf_type"].ToString() == "1")
            {
                strQry = "select trans_no from mas_stp where sf_code='" + sf_code + "' and Active_Flag=0 and Division_Code='" + div_code + "' ";
                dsSTP = db_ER.Exec_DataSet(strQry);
            }
            else
            {
                strQry = "select trans_no from mas_stp where sf_code='" + MR_Code + "' and Active_Flag=0 and Division_Code='" + div_code + "' ";
                dsSTP = db_ER.Exec_DataSet(strQry);
            }


            if (dsSTP.Tables[0].Rows.Count == 0)
            {
                lblHead.Text = "<br><br><br><br> STP Not Created yet So that Not Allowed to Enter TP ...";
                lblHead.CssClass = "blink_me";
                lblHead.ForeColor = System.Drawing.Color.Red;
                lblname.Visible = false;
                lblemp.Visible = false;
                lbldoj.Visible = false;
                lblreport.Visible = false;
                btnSave.Visible = false;
                btnSubmit.Visible = false;

            }

            else
            {


                strQry = "SELECT wrktype = STUFF(( SELECT distinct ',' + Worktype_Name_B " +
                         " FROM Mas_WorkType_BaseLevel where  division_code='" + div_code + "' and Place_Involved='Y' " +
                          " FOR XML PATH('') ), 1, 1, '') ";
                dsTPC = db_ER.Exec_DataSet(strQry);

                hdnwrktype.Value = dsTPC.Tables[0].Rows[0]["wrktype"].ToString();



                if (sQryStr != null && sQryStr != "")
                {


                    TP_New tp = new TP_New();

                    dsTP2 = tp.Tp_STP_Edit(MR_Code, MR_Month, MR_Year);

                    if (dsTP2.Tables[0].Rows.Count > 0)
                    {

                        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(MR_Month)).ToString().Substring(0, 3);

                        lblHead.Text = "TP Approval For " + "<span style='color:Red'>" + Request.QueryString["sf_name"]
                             + "</span>" + "<span style='color:Green'> " + " " + strFMonthName + " " + MR_Year + "</span>";

                        Edit = "Approval";
                        grdTP.Visible = true;
                        grdTP.DataSource = dsTP2;
                        grdTP.DataBind();
                    }
                    else
                    {
                        grdTP.DataSource = dsTP2;
                        grdTP.DataBind();
                    }


                }

                else
                {



                    strQry = " select a.Sf_Name +' - ' +sf_Designation_Short_Name+' - '+a.Sf_HQ as sf_name,sf_emp_id, CONVERT(varchar,Sf_Joining_Date,103) as Sf_Joining_Date, " +
                            " ((select Sf_Name from Mas_Salesforce_AM where Sf_Code=b.TP_AM) +' - '+ " +
                            " (select sf_Designation_Short_Name + ' - '+Sf_HQ  from Mas_Salesforce where Sf_Code=b.TP_AM)) as Reporting_to_tp,  " +
                            " (select last_tp_date from mas_salesforce_dcrtpdate where sf_code='" + sf_code + "') last_tp_date " +
                            " from Mas_Salesforce a , Mas_Salesforce_AM b where a.Sf_Code='" + sf_code + "' " +
                            " and a.Sf_Code=b.Sf_Code";

                    dsTPC = db_ER.Exec_DataSet(strQry);

                    if (dsTPC.Tables[0].Rows.Count > 0)
                    {
                        lblsf_name.Text = dsTPC.Tables[0].Rows[0]["sf_name"].ToString();
                        lblempid.Text = dsTPC.Tables[0].Rows[0]["sf_emp_id"].ToString();
                        lbldoj2.Text = dsTPC.Tables[0].Rows[0]["Sf_Joining_Date"].ToString();
                        lblreportname.Text = dsTPC.Tables[0].Rows[0]["Reporting_to_tp"].ToString();
                    }

                    if (Edit != null && Edit == "E")
                    {
                        TP_New tp = new TP_New();
                        month = Request.QueryString["month"];
                        year = Request.QueryString["year"];

                        dsTP2 = tp.Tp_STP_Edit(sf_code, month, year);
                        btnSave.Visible = false;

                        if (dsTP2.Tables[0].Rows.Count > 0)
                        {

                            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(month)).ToString().Substring(0, 3);

                            lblHead.Text = "Tour Plan - Edit " + "(<span style='font-size:11pt;color:Black;font-family:Verdana'> Before Approval </span>)" + " for the month of " + "<span style='font-size:11pt;color:Green;font-family:Verdana'>" + strFMonthName + " " + year + "</span>";
                            grdTP.Visible = true;
                            grdTP.DataSource = dsTP2;
                            grdTP.DataBind();
                        }
                        else
                        {
                            grdTP.DataSource = dsTP2;
                            grdTP.DataBind();
                        }

                    }
                    else
                    {

                        fill_tp();
                    }
                }

            }

        }

    }

    private void fill_tp()
    {
        TP_New tp = new TP_New();

        dsTP = tp.last_Tp_Datecheck(sf_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(dsTP.Tables[0].Rows[0]["month"].ToString())).ToString().Substring(0, 3);

            ViewState["month"] = dsTP.Tables[0].Rows[0]["month"].ToString();
            ViewState["year"] = dsTP.Tables[0].Rows[0]["year"].ToString();
            string month_yr = strFMonthName + " " + dsTP.Tables[0].Rows[0]["year"].ToString().ToString();

            lblHead.Text = "Your " + "<span style='color:#FF3300'>" + month_yr + "</span>" +
                                      " TP not yet approved by your manager (" + "<span style='color:Red'>" + lblreportname.Text + "</span>" + ")";
            hylEdit.Text = "Yes";
            lblLink.Text = "Before Approval by your Manager - Do you want to change your TP - ";
            tblMargin.Style.Add("margin-top", "140px");
            grdTP.Visible = false;
            tabsf.Visible = false;
            btnSubmit.Visible = false;
            btnSave.Visible = false;
        }
        else
        {

            DateTime last_tpdatee = Convert.ToDateTime(dsTPC.Tables[0].Rows[0]["last_tp_date"].ToString());

            System.Globalization.DateTimeFormatInfo mfii = new System.Globalization.DateTimeFormatInfo();
            string strFMonthNamee = mfii.GetMonthName(Convert.ToInt16(last_tpdatee.Month)).ToString().Substring(0, 3);

            dsTP = tp.last_Tp_STP_draft(sf_code, last_tpdatee.Month.ToString(), last_tpdatee.Year.ToString());

            if (dsTP.Tables[0].Rows.Count > 0)
            {

                if (dsTP.Tables[0].Rows[0]["confirmed"].ToString() == "0" && dsTP.Tables[0].Rows[0]["change_status"].ToString() == "0")
                {

                    lblHead.Text = "Tour Plan Entry for the Month of " + "<span style='color:red'>" + "" + strFMonthNamee + " " + last_tpdatee.Year + "</span>";

                    Edit = "draft";
                }

                else if (dsTP.Tables[0].Rows[0]["confirmed"].ToString() == "0" && dsTP.Tables[0].Rows[0]["change_status"].ToString() == "2")
                {
                    lblHead.Text = "Your " + "<span style='color:red'>" + "" + strFMonthNamee + " " + last_tpdatee.Year + "</span>" + " TP has been Rejected " + "<span style='color:red'>" + " ( Resubmit for Rejection )" + "</span>";
                    lblReason.Text = "Rejected Reason: " + "<span style='color:red'>" + dsTP.Tables[0].Rows[0]["Rejection_Reason"].ToString() + "</span>";
                    lblReason.Visible = true;
                    Edit = "Reject";
                    btnSave.Visible = false;
                }

                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
            }


            else
            {

                DateTime last_tpdate = Convert.ToDateTime(dsTPC.Tables[0].Rows[0]["last_tp_date"].ToString());

                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                string strFMonthName = mfi.GetMonthName(Convert.ToInt16(last_tpdate.Month)).ToString().Substring(0, 3);

                lblHead.Text = "Tour Plan Entry for the Month of " + "<span style='color:red'>" + "" + strFMonthName + " " + last_tpdate.Year + "</span>";

                strQry = "EXEC Tp_EntryNew_STPWise '" + div_code + "','" + sf_code + "'";
                dsTP = db_ER.Exec_DataSet(strQry);

                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    grdTP.Visible = true;
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }
                else
                {
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }
            }
        }
    }

    protected DataSet FillWorkType()
    {
        TP_New tp = new TP_New();
        dsWrktype = tp.FetchWorkType_New(div_code);
        return dsWrktype;
    }

    protected DataSet Fillstp()
    {
        TP_New tp = new TP_New();
        dsWrktype = tp.STP_TP_Fill();
        return dsWrktype;
    }

    protected DataSet Fillstp_drcode()
    {
        TP_New tp = new TP_New();
        dsWrktype = tp.Fillstp_drcode(sf_code,div_code);
        return dsWrktype;
    }

    protected DataSet Fillstp_chemcode()
    {
        TP_New tp = new TP_New();
        dsWrktype = tp.Fillstp_chemcode(sf_code, div_code);
        return dsWrktype;
    }

    protected DataSet FillTerritory()
    {
        TP_New tp = new TP_New();
        if (sQryStr != null && sQryStr != "")
        {
            dsTerritory = tp.FetchTerritory_new(MR_Code);
        }
        else
        {

            dsTerritory = tp.FetchTerritory_new(sf_code);
        }
        return dsTerritory;
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            // when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");

            // when mouse leaves the row, change the bg color to its original value   
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        }



        if (Edit != null && Edit == "E")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                HiddenField hdnwrktype_code = (HiddenField)e.Row.FindControl("hdnwrktype_code");
               // CheckBoxList chkterr = (CheckBoxList)e.Row.FindControl("chkterr");

                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
              //  TextBox txtterr = (TextBox)e.Row.FindControl("txtterr");

                HiddenField hdndr = (HiddenField)e.Row.FindControl("hdndr");
                HiddenField hdndr_name = (HiddenField)e.Row.FindControl("hdndr_name");

                HiddenField hdnchem = (HiddenField)e.Row.FindControl("hdnchem");
                HiddenField hdnchem_name = (HiddenField)e.Row.FindControl("hdnchem_name");

                DropDownList ddlstp = (DropDownList)e.Row.FindControl("ddlstp");


                ddlwrktype.SelectedValue = hdnwrktype_code.Value;



                //int val = hdnwrktype.Value.IndexOf(ddlwrktype.SelectedItem.Text);

                //if (val == 0)
                //{
                //    txtterr.Enabled = true;
                //}


                //string terr_code = string.Empty;
                //string terr_name = string.Empty;

                //for (int i = 0; i < chkterr.Items.Count; i++)
                //{
                //    if (hdnterr_code.Value.Contains(chkterr.Items[i].Value))
                //    {
                //        chkterr.Items[i].Selected = true;
                //        terr_name += chkterr.Items[i].Text + ",";
                //    }
                //}

                //txtterr.Text = terr_name.TrimEnd(',');

                string dr_code = dsTP2.Tables[0].Rows[e.Row.RowIndex]["dr_code"].ToString();

                string dr_name = dsTP2.Tables[0].Rows[e.Row.RowIndex]["dr_name"].ToString();


                string chem_code = dsTP2.Tables[0].Rows[e.Row.RowIndex]["chem_code"].ToString();
                string chem_name = dsTP2.Tables[0].Rows[e.Row.RowIndex]["chem_name"].ToString();

                hdndr.Value = dr_code;
                hdndr_name.Value = dr_name;

                hdnchem.Value = chem_code;
                hdnchem_name.Value = chem_name;

                ddlstp.SelectedValue = dsTP2.Tables[0].Rows[e.Row.RowIndex]["weekdays"].ToString();




                // ddlTerr.SelectedValue = hdnterr_code.Value;


                if (ddlwrktype.SelectedItem.Text == "Holiday")
                {
                    ddlstp.Enabled = false;
                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }

                if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                {
                    ddlstp.Enabled = false;

                    lblSNo.BackColor = System.Drawing.Color.Pink;
                    lblDate.BackColor = System.Drawing.Color.Pink;
                    lblDay_name.BackColor = System.Drawing.Color.Pink;
                }

                for (int i = 0; i < ddlwrktype.Items.Count; i++)
                {
                    if (ddlwrktype.Items[i].Text == "Field Work")
                    {
                        ddlwrktype.Items[i].Attributes.Add("Class", "Color");
                    }
                }
            }
        }
        else if (Edit == "draft")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                HiddenField hdnwrktype_code = (HiddenField)e.Row.FindControl("hdnwrktype_code");
                DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");



              //  CheckBoxList chkterr = (CheckBoxList)e.Row.FindControl("chkterr");

                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
              //  TextBox txtterr = (TextBox)e.Row.FindControl("txtterr");

                HiddenField hdndr = (HiddenField)e.Row.FindControl("hdndr");
                HiddenField hdndr_name = (HiddenField)e.Row.FindControl("hdndr_name");

                HiddenField hdnchem = (HiddenField)e.Row.FindControl("hdnchem");
                HiddenField hdnchem_name = (HiddenField)e.Row.FindControl("hdnchem_name");

                DropDownList ddlstp = (DropDownList)e.Row.FindControl("ddlstp");



                ddlwrktype.SelectedValue = hdnwrktype_code.Value;



                //int val = hdnwrktype.Value.IndexOf(ddlwrktype.SelectedItem.Text);

                //if (val == 0)
                //{
                //    txtterr.Enabled = true;
                //}


                //string terr_code = string.Empty;
                //string terr_name = string.Empty;

                //for (int i = 0; i < chkterr.Items.Count; i++)
                //{
                //    if (hdnterr_code.Value.Contains(chkterr.Items[i].Value))
                //    {
                //        chkterr.Items[i].Selected = true;
                //        terr_name += chkterr.Items[i].Text + ",";
                //    }
                //}

                //txtterr.Text = terr_name.TrimEnd(',');

                string dr_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_code"].ToString();

                string dr_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_name"].ToString();


                string chem_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_code"].ToString();
                string chem_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_name"].ToString();

                hdndr.Value = dr_code;
                hdndr_name.Value = dr_name;

                hdnchem.Value = chem_code;
                hdnchem_name.Value = chem_name;

                ddlstp.SelectedValue = dsTP.Tables[0].Rows[e.Row.RowIndex]["weekdays"].ToString();


                //ddlTerr.SelectedValue = hdnterr_code.Value;
                //ddlwrktype.SelectedValue = hdnwrktype_code.Value;

                if (ddlwrktype.SelectedItem.Text == "Holiday")
                {
                    ddlstp.Enabled = false;
                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }

                if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                {
                    ddlstp.Enabled = false;
                    lblSNo.BackColor = System.Drawing.Color.Pink;
                    lblDate.BackColor = System.Drawing.Color.Pink;
                    lblDay_name.BackColor = System.Drawing.Color.Pink;
                }

                for (int i = 0; i < ddlwrktype.Items.Count; i++)
                {
                    if (ddlwrktype.Items[i].Text == "Field Work")
                    {
                        ddlwrktype.Items[i].Attributes.Add("Class", "Color");
                    }
                }
            }
        }

        else if (Edit == "Reject")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                HiddenField hdnwrktype_code = (HiddenField)e.Row.FindControl("hdnwrktype_code");
                DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");



                //CheckBoxList chkterr = (CheckBoxList)e.Row.FindControl("chkterr");

                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
                //TextBox txtterr = (TextBox)e.Row.FindControl("txtterr");

                HiddenField hdndr = (HiddenField)e.Row.FindControl("hdndr");
                HiddenField hdndr_name = (HiddenField)e.Row.FindControl("hdndr_name");

                HiddenField hdnchem = (HiddenField)e.Row.FindControl("hdnchem");
                HiddenField hdnchem_name = (HiddenField)e.Row.FindControl("hdnchem_name");
                DropDownList ddlstp = (DropDownList)e.Row.FindControl("ddlstp");


                ddlwrktype.SelectedValue = hdnwrktype_code.Value;



                //int val = hdnwrktype.Value.IndexOf(ddlwrktype.SelectedItem.Text);

                //if (val == 0)
                //{
                //    txtterr.Enabled = true;
                //}


                //string terr_code = string.Empty;
                //string terr_name = string.Empty;

                //for (int i = 0; i < chkterr.Items.Count; i++)
                //{
                //    if (hdnterr_code.Value.Contains(chkterr.Items[i].Value))
                //    {
                //        chkterr.Items[i].Selected = true;
                //        terr_name += chkterr.Items[i].Text + ",";
                //    }
                //}

                //txtterr.Text = terr_name.TrimEnd(',');

                string dr_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_code"].ToString();

                string dr_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_name"].ToString();


                string chem_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_code"].ToString();
                string chem_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_name"].ToString();

                hdndr.Value = dr_code;
                hdndr_name.Value = dr_name;

                hdnchem.Value = chem_code;
                hdnchem_name.Value = chem_name;

                ddlstp.SelectedValue = dsTP.Tables[0].Rows[e.Row.RowIndex]["weekdays"].ToString();


                //ddlTerr.SelectedValue = hdnterr_code.Value;
                //ddlwrktype.SelectedValue = hdnwrktype_code.Value;

                if (ddlwrktype.SelectedItem.Text == "Holiday")
                {
                    ddlstp.Enabled = false;
                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }

                if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                {
                    ddlstp.Enabled = false;
                    lblSNo.BackColor = System.Drawing.Color.Pink;
                    lblDate.BackColor = System.Drawing.Color.Pink;
                    lblDay_name.BackColor = System.Drawing.Color.Pink;
                }

                for (int i = 0; i < ddlwrktype.Items.Count; i++)
                {
                    if (ddlwrktype.Items[i].Text == "Field Work")
                    {
                        ddlwrktype.Items[i].Attributes.Add("Class", "Color");
                    }
                }
            }
        }
        else
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");

                HiddenField hdnholiday = (HiddenField)e.Row.FindControl("hdnholiday");
                HiddenField hdnweekoff = (HiddenField)e.Row.FindControl("hdnweekoff");
                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
                //HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                //  DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
                //  DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
                TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");
                HiddenField hdnField_Work = (HiddenField)e.Row.FindControl("hdnField_Work");
                HiddenField hdnwrktype_code = (HiddenField)e.Row.FindControl("hdnwrktype_code");

                HiddenField hdnweekday = (HiddenField)e.Row.FindControl("hdnweekday");

                DropDownList ddlstp = (DropDownList)e.Row.FindControl("ddlstp");

                ddlstp.SelectedValue = hdnweekday.Value;

                ddlwrktype.SelectedValue = hdnField_Work.Value;

                if (hdnweekday.Value == "MO5")
                {
                    ddlstp.SelectedValue = "MO1";
                }

                else if (hdnweekday.Value == "TU5")
                {
                    ddlstp.SelectedValue = "TU1";
                }
                else if (hdnweekday.Value == "WE5")
                {
                    ddlstp.SelectedValue = "WE1";
                }

                else if (hdnweekday.Value == "TH5")
                {
                    ddlstp.SelectedValue = "TH1";
                }

                else if (hdnweekday.Value == "FR5")
                {
                    ddlstp.SelectedValue = "FR1";
                }

                else if (hdnweekday.Value == "SA5")
                {
                    ddlstp.SelectedValue = "SA1";
                }


                HiddenField hdndr = (HiddenField)e.Row.FindControl("hdndr");
                HiddenField hdndr_name = (HiddenField)e.Row.FindControl("hdndr_name");

                HiddenField hdnchem = (HiddenField)e.Row.FindControl("hdnchem");
                HiddenField hdnchem_name = (HiddenField)e.Row.FindControl("hdnchem_name");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");



                if (sQryStr != null && sQryStr != "")
                {



                    //TextBox txtterr = (TextBox)e.Row.FindControl("txtterr");


                    //  CheckBoxList chkterr = (CheckBoxList)e.Row.FindControl("chkterr");







                    int val = hdnwrktype.Value.IndexOf(ddlwrktype.SelectedItem.Text);

                    //if (val == 0)
                    //{
                    //    txtterr.Enabled = true;
                    //}


                    //string terr_code = string.Empty;
                    //string terr_name = string.Empty;

                    //for (int i = 0; i < chkterr.Items.Count; i++)
                    //{
                    //    if (hdnterr_code.Value.Contains(chkterr.Items[i].Value))
                    //    {
                    //        chkterr.Items[i].Selected = true;
                    //        terr_name += chkterr.Items[i].Text + ",";
                    //    }
                    //}

                    //   txtterr.Text = terr_name.TrimEnd(',');

                    string dr_code = dsTP2.Tables[0].Rows[e.Row.RowIndex]["dr_code"].ToString();

                    string dr_name = dsTP2.Tables[0].Rows[e.Row.RowIndex]["dr_name"].ToString();


                    string chem_code = dsTP2.Tables[0].Rows[e.Row.RowIndex]["chem_code"].ToString();
                    string chem_name = dsTP2.Tables[0].Rows[e.Row.RowIndex]["chem_name"].ToString();

                    hdndr.Value = dr_code;
                    hdndr_name.Value = dr_name;

                    hdnchem.Value = chem_code;
                    hdnchem_name.Value = chem_name;

                    ddlwrktype.SelectedValue = hdnwrktype_code.Value;



                    if (ddlwrktype.SelectedItem.Text == "Holiday")
                    {
                        ddlstp.Enabled = false;
                        ddlstp.SelectedValue = "0";

                        lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                        lblDate.BackColor = System.Drawing.Color.Aquamarine;
                        lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                    }

                    else if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                    {
                        ddlstp.SelectedValue = "0";
                        ddlwrktype.SelectedValue = hdnweekoff.Value;
                        ddlstp.Enabled = false;

                        txtObjective.Text = "Weekly Off";
                        // ddlTerr.SelectedValue = "0";

                        lblSNo.BackColor = System.Drawing.Color.Pink;
                        lblDate.BackColor = System.Drawing.Color.Pink;
                        lblDay_name.BackColor = System.Drawing.Color.Pink;
                    }

                }

                else
                {


                    DropDownList ddldrfill = (DropDownList)e.Row.FindControl("ddldrfill");
                    DropDownList ddlchemfill = (DropDownList)e.Row.FindControl("ddlchemfill");


                    ddlchemfill.SelectedValue = ddlstp.SelectedValue;
                    ddldrfill.SelectedValue = ddlstp.SelectedValue;

                    if (ddldrfill.SelectedValue != "")
                    {
                        string doccode = ddldrfill.SelectedItem.Text;
                        string chemcodee = ddlchemfill.SelectedItem.Text;

                        hdndr.Value = doccode;
                        hdnchem.Value = chemcodee;
                    }
                }

              


                if (hdnholiday.Value != "")
                {
                   
                    ddlstp.SelectedValue = "0";
                    ddlwrktype.SelectedValue = hdnholiday.Value;
                    ddlstp.Enabled = false;

                    //ddlTerr.SelectedValue = "0";

                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }
                else if (hdnweekoff.Value != "")
                {
                    ddlstp.SelectedValue = "0";
                    ddlwrktype.SelectedValue = hdnweekoff.Value;

                    ddlstp.Enabled = false;

                    txtObjective.Text = "Weekly Off";
                    // ddlTerr.SelectedValue = "0";

                    lblSNo.BackColor = System.Drawing.Color.Pink;
                    lblDate.BackColor = System.Drawing.Color.Pink;
                    lblDay_name.BackColor = System.Drawing.Color.Pink;
                }
               






                //string doccode = ddldrfill.SelectedItem.Text;
                //string chemcodee = ddlchemfill.SelectedItem.Text;

                //hdndr.Value = doccode;
                //hdnchem.Value = chemcodee;





                for (int i = 0; i < ddlwrktype.Items.Count; i++)
                {
                    if (ddlwrktype.Items[i].Text == "Field Work")
                    {
                        ddlwrktype.Items[i].Attributes.Add("Class", "Color");
                    }
                }

            }
        }

        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    Territory terr = new Territory();
        //    DataSet dsTerritory = new DataSet();
        //    dsTerritory = terr.getWorkAreaName(div_code);
        //    if (dsTerritory.Tables[0].Rows.Count > 0)
        //    {
        //        e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        //    }
        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        createtp("submit");

    }

    protected void hylEdit_Onclick(Object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/MR/TP_ENTRY_STP.aspx?Edit=E" + "&month=" + ViewState["month"].ToString() + "&year=" + ViewState["year"].ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        createtp("draft");
    }

    private void createtp(string mode)
    {

        TP_New tp = new TP_New();
        foreach (GridViewRow row in grdTP.Rows)
        {


            Label lblDate = (Label)row.FindControl("lblDate");
            TP_Date = lblDate.Text.ToString();
          

            DropDownList ddlwrktype = (DropDownList)row.FindControl("ddlwrktype");
            wrktype_code = ddlwrktype.SelectedValue.ToString();

            DropDownList ddlstp = (DropDownList)row.FindControl("ddlstp");




            if (wrktype_code == "0")
            {
                ddlWwrktype_name = "0";
            }
            else
            {
                ddlWwrktype_name = ddlwrktype.SelectedItem.Text;
            }


            TextBox txtObjective = (TextBox)row.FindControl("txtObjective");



            TP_Objective = txtObjective.Text.ToString().Replace("'", "");


            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);

            Label lblDay_name = (Label)row.FindControl("lblDay_name");


            HiddenField hdndr = (HiddenField)row.FindControl("hdndr");
            HiddenField hdndr_name = (HiddenField)row.FindControl("hdndr_name");

            HiddenField hdnchem = (HiddenField)row.FindControl("hdnchem");
            HiddenField hdnchem_name = (HiddenField)row.FindControl("hdnchem_name");

           

            iReturn = tp.Tp_Insert_STP(TP_Date, wrktype_code, ddlWwrktype_name, TP_Objective, Session["sf_code"].ToString(), lblsf_name.Text, div_code, mode, lblreportname.Text, hdndr.Value, hdndr_name.Value, hdnchem.Value, hdnchem_name.Value, ddlstp.SelectedValue, ddlstp.SelectedItem.Text);

            if (iReturn > 0)
            {

                if (mode == "submit")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted for Approval');window.location='TP_ENTRY_STP.aspx'</script>");
                }
                else if (mode == "draft")
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');window.location='TP_ENTRY_STP.aspx'</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Already Submitted for this month');</script>");
            }
        }
    }


    protected DataSet fill_dr()
    {
        Doctor dr = new Doctor();
        dsDr = dr.filldr_terr(sf_code);
        return dsDr;

    }








   

    [WebMethod]
    public static string GetCustomersdr(string stp_day, string day)
    {
        //string query = "SELECT Designation_Code,Designation_Short_Name  " +
        //             " FROM Mas_SF_Designation where Division_Code ='7'";]

        string sf_code = string.Empty;


        string dayshort = string.Empty;

        if (day == "Sunday")
        {
            day = "0";
            dayshort = "Sun";
        }
        else if (day == "Monday")
        {
            day = "1";
            dayshort = "Mon";
        }
        else if (day == "Tuesday")
        {

            day = "2";
            dayshort = "Tue";
        }
        else if (day == "Wednesday")
        {
            day = "3";
            dayshort = "Wed";
        }
        else if (day == "Thursday")
        {
            day = "4";
            dayshort = "Thu";
        }
        else if (day == "Friday")
        {
            day = "5";
            dayshort = "Fri";
        }
        else if (day == "Saturday")
        {
            day = "6";
            dayshort = "Sat";
        }


        string valss = string.Empty;

        string div_code = HttpContext.Current.Session["div_code"].ToString();




        if (HttpContext.Current.Session["sf_code_mr"] != null && HttpContext.Current.Session["sf_code_mr"] != "")
        {
            sf_code = HttpContext.Current.Session["sf_code_mr"].ToString();
        }

        else
        {
            sf_code = HttpContext.Current.Session["sf_code"].ToString();
        }

        if (stp_day == "0")
        {
            valss = "0";
        }
        else
        {

            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            strQry = "select dr_code from mas_stp where sf_code='" + sf_code + "' and Day_Plan_ShortName='" + stp_day + "' and active_flag=0";
            DataSet dsPatch = db_ER.Exec_DataSet(strQry);

            if (dsPatch.Tables[0].Rows.Count > 0)
            {

                if (dsPatch.Tables[0].Rows[0]["dr_code"].ToString() != "")
                {
                    string[] cust = dsPatch.Tables[0].Rows[0]["dr_code"].ToString().Split(new char[] { ',' });
                    foreach (string teri in cust)
                    {
                        // string s = string.Format("'{0}'", string.Join("','", teri));

                        valss += "'" + teri.TrimStart(' ') + "'" + ",";
                    }
                }
                else
                {
                    valss = "0";
                }
            }

            else
            {
                valss = "0";
            }
        }


        string query = "select id, ListedDrCode," +
                      " case id when 1 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold;width:5000px>'+ListedDr_Name +  '</span>' +' - '+'<span style=color:red;font-weight:bold>' + Territory_Name + '</span>' +' - ' +listeddr_visit_days " +
                      " when 2 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name+ '</span>' +' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + '</span>' +' - ' +listeddr_visit_days " +
                      " when 3 then '<span style=color:MEDIUMSLATEBLUE;font-weight:bold>'+ListedDr_Name +'</span>' + ' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>' +' - ' +listeddr_visit_days " +
                      " when 4 then '<span style=color:TEAL;font-weight:bold>'+ListedDr_Name + '</span>' + ' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' +' - ' +listeddr_visit_days" +
                      " when 5 then '<span style=color:LightCoral;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' +' - ' +listeddr_visit_days" +
                       " when 6 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 7 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 8 then '<span style=color:MEDIUMSLATEBLUE;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 9 then '<span style=color:TEAL;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 10 then '<span style=color:LightCoral;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 11 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " when 12 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +  '</span>' " +
                      " end as ListedDr_Name,Territory_Name from " +
                      "  ( select  dense_rank() over ( order by Territory_Name) as id, c.ListedDrCode, ListedDr_Name,Territory_Name, " +
                      " case listeddr_visit_days when '0' then 'Sun' " +
                      " when '1' then 'Mon' when '2' then 'Tue' when '3' then 'Wed' when '4' then 'Thu' when '5' then 'Fri' " +
                      " when '6' then 'Sat' else '' end as listeddr_visit_days from Mas_Territory_Creation p inner join Mas_ListedDr c on cast(p.Territory_Code as varchar)=CAST( c.Territory_Code  as varchar) " +
                      " where c.ListedDrCode in (" + valss.TrimEnd(',') + ") and  p.SF_Code='" + sf_code + "' and c.division_code='" + div_code + "' and ListedDr_Active_Flag=0 union all " +
                       " select '99' as id, '888888' ListedDrCode,'' ListedDr_Name,'z' Territory_Name,'' listeddr_visit_days ) yyy order by Territory_Name, " +
                       " CASE listeddr_visit_days " +
                      " WHEN '" + dayshort + "' THEN '" + day + "' else 5 END";

        SqlCommand cmd = new SqlCommand(query);
        return GetData(cmd).GetXml();
    }

    [WebMethod]
    public static string GetCustomerschem(string stp_day)
    {
        //string query = "SELECT Designation_Code,Designation_Short_Name  " +
        //             " FROM Mas_SF_Designation where Division_Code ='7'";

        string sf_code = string.Empty;

        string valss = string.Empty;

        string div_code = HttpContext.Current.Session["div_code"].ToString();

        if (HttpContext.Current.Session["sf_code_mr"] != null && HttpContext.Current.Session["sf_code_mr"] != "")
        {
            sf_code = HttpContext.Current.Session["sf_code_mr"].ToString();
        }

        else
        {
            sf_code = HttpContext.Current.Session["sf_code"].ToString();
        }

        if (stp_day == "0")
        {
            valss = "0";
        }
        else
        {

            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            strQry = "select chem_code from mas_stp where sf_code='" + sf_code + "' and Day_Plan_ShortName='" + stp_day + "' and active_flag=0";
            DataSet dsPatch = db_ER.Exec_DataSet(strQry);

            if (dsPatch.Tables[0].Rows.Count > 0)
            {

                if (dsPatch.Tables[0].Rows[0]["chem_code"].ToString() != "")
                {
                    string[] cust = dsPatch.Tables[0].Rows[0]["chem_code"].ToString().Split(new char[] { ',' });
                    foreach (string teri in cust)
                    {
                        // string s = string.Format("'{0}'", string.Join("','", teri));

                        valss += "'" + teri.TrimStart(' ') + "'" + ",";
                    }
                }
                else
                {
                    valss = "0";
                }
            }
            else
            {
                valss = "0";
            }
        }

        string query = "select id, Chem_Code," +
                    " case id when 1 then '<span style=color:olive;font-weight:bold>'+Chem_Name + '</span>' +' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + '</span>' " +
                    " when 2 then '<span style=color:orange-red;font-weight:bold>'+Chem_Name+'</span>' + ' - '+ '<span style=color:red;font-weight:bold>'+Territory_Name + '</span>' " +
                    " when 3 then '<span style=color:indigo;font-weight:bold>'+Chem_Name +'</span>' + ' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + '</span>' " +
                    " when 4 then '<span style=color:Brown;font-weight:bold>'+Chem_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>'+Territory_Name + '</span>'" +
                    " when 5 then '<span style=color:Amaranth;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                     " when 6 then '<span style=color:olive;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 7 then '<span style=color:orange;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 8 then '<span style=color:indigo;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 9 then '<span style=color:Brown;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 10 then '<span style=color:Amaranth;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 11 then '<span style=color:olive;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 12 then '<span style=color:orange;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " end as Chem_Name,Territory_Name from " +
                    "( select dense_rank() over ( order by Territory_Name) as id, c.Chemists_Code as Chem_Code, c.Chemists_Name  as Chem_Name,Territory_Name from Mas_Territory_Creation p inner join mas_chemists c on cast(p.Territory_Code as varchar)=CAST( c.Territory_Code  as varchar) " +
                    " where c.Chemists_Code in (" + valss.TrimEnd(',') + ") and  p.SF_Code='" + sf_code + "' and c.division_code='" + div_code + "' and Chemists_Active_Flag=0  union all " +
                    "  select '99' as id, '99' Chem_Code,'' Chem_Name,'z' Territory_Name ) yyy order by Territory_Name   ";


        SqlCommand cmd = new SqlCommand(query);
        return GetData(cmd).GetXml();
    }

    private static DataSet GetData(SqlCommand cmd)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;

                }
            }
        }
    }



    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Approve_tp();
    }

    private void Approve_tp()
    {
        TP_New tp = new TP_New();
        foreach (GridViewRow row in grdTP.Rows)
        {
            // DropDownList ddlwrktype = (DropDownList)row.FindControl("ddlwrktype");

            Label lblDate = (Label)row.FindControl("lblDate");
            TP_Date = lblDate.Text.ToString();
            //Label lblDay = (Label)row.FindControl("lblDay_name");
            //TP_Day = lblDay.Text.ToString();

            DropDownList ddlwrktype = (DropDownList)row.FindControl("ddlwrktype");
            wrktype_code = ddlwrktype.SelectedValue.ToString();

            DropDownList ddlstp = (DropDownList)row.FindControl("ddlstp");


            if (wrktype_code == "0")
            {
                ddlWwrktype_name = "0";
            }
            else
            {
                ddlWwrktype_name = ddlwrktype.SelectedItem.Text;
            }



            TextBox txtObjective = (TextBox)row.FindControl("txtObjective");



            TP_Objective = txtObjective.Text.ToString().Replace("'", "");

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);

            Label lblDay_name = (Label)row.FindControl("lblDay_name");


            HiddenField hdndr = (HiddenField)row.FindControl("hdndr");
            HiddenField hdndr_name = (HiddenField)row.FindControl("hdndr_name");

            HiddenField hdnchem = (HiddenField)row.FindControl("hdnchem");
            HiddenField hdnchem_name = (HiddenField)row.FindControl("hdnchem_name");

            CheckBoxList chkterr = (CheckBoxList)row.FindControl("chkterr");
          
            string Mangername = Session["sf_name"].ToString() + " - " + Session["Designation_Short_Name"].ToString() + " - " + Session["sf_hq"].ToString();

            iReturn = tp.Tp_approve_STP(TP_Date, wrktype_code, ddlWwrktype_name, TP_Objective, MR_Code, Request.QueryString["sf_name"].ToString(), div_code, Mangername, hdndr.Value, hdndr_name.Value, hdnchem.Value, hdnchem_name.Value, ddlstp.SelectedValue, ddlstp.SelectedItem.Text);

        }

        if (iReturn > 0)
        {

            int iretu = tp.TP_Apprv_terr_Update(MR_Code, MR_Month, MR_Year);
            if (iretu > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
            }

        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {

        txtReason.Visible = true;
        grdTP.Enabled = false;
        btnApprove.Visible = false;
        btnSave.Visible = false;
        btnReject.Visible = false;
        btnSubmit.Visible = false;
        btnSendBack.Visible = true;
        lblRejectReason.Visible = true;

        txtReason.Focus();
    }

    protected void btnSendBack_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            int iReturn = -1;
            TP_New tp = new TP_New();

            txtReason.Text = txtReason.Text.ToString().Replace("'", "asdf");

            string Mangername = Session["sf_name"].ToString() + " - " + Session["Designation_Short_Name"].ToString() + " - " + Session["sf_hq"].ToString();

            iReturn = tp.Reject_New(MR_Code, MR_Month, MR_Year, txtReason.Text, Mangername);

            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Rejected Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
            }
        }
    }
}




