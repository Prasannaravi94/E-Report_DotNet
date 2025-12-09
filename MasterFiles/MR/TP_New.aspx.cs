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

public partial class MasterFiles_MR_TP_New : System.Web.UI.Page
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
    DataSet dsWrktype=null;
    string month = string.Empty;
    string year = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        Edit = Request.QueryString["Edit"];

        if (!Page.IsPostBack)
        {
          
            menu.Title = this.Page.Title;
            // menu.FindControl("btnBack").Visible = false;

          //  lblHead.Text = "Tour Plan Entry - For the Month of";

            strQry = " select a.Sf_Name +' - ' +sf_Designation_Short_Name+' - '+a.Sf_HQ as sf_name,sf_emp_id, CONVERT(varchar,Sf_Joining_Date,103) as Sf_Joining_Date, " +
                    " ((select Sf_Name from Mas_Salesforce_AM where Sf_Code=b.TP_AM) +' - '+ " +
                    " (select sf_Designation_Short_Name + ' - '+Sf_HQ  from Mas_Salesforce where Sf_Code=b.TP_AM)) as Reporting_to_tp,  " +
                    " (select last_tp_date from mas_salesforce_dcrtpdate where sf_code='"+sf_code+"') last_tp_date "+
                    " from Mas_Salesforce a , Mas_Salesforce_AM b where a.Sf_Code='"+sf_code+"' " +
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

                dsTP2 = tp.Tp_Newfill_Edit(sf_code,month,year);

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
            string month_yr =strFMonthName +" " +dsTP.Tables[0].Rows[0]["year"].ToString().ToString();

            lblHead.Text = "Your " + "<span style='color:#FF3300'>" + month_yr + "</span>" +
                                      " TP not yet approved by your manager (" + "<span style='color:Green'>" + lblreportname.Text + "</span>" + ")";
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

            lblHead.Text = "Tour Plan Entry for the Month of " + "<span style='color:red'>" + "" + strFMonthNamee + " " + last_tpdatee.Year + "</span>";

             dsTP = tp.last_Tp_Datecheck_draft(sf_code, last_tpdatee.Month.ToString(), last_tpdatee.Year.ToString());

             if (dsTP.Tables[0].Rows.Count > 0)
             {
                 Edit = "draft";
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

                 strQry = "EXEC Tp_EntryNew '" + div_code + "','" + sf_code + "'";
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

    protected DataSet FillTerritory()
    {
        TP_New tp = new TP_New();

        dsTerritory = tp.FetchTerritory(sf_code);
        return dsTerritory;
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       

        if (Edit != null && Edit == "E")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                HiddenField hdnwrktype_code = (HiddenField)e.Row.FindControl("hdnwrktype_code");
                DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");

                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");

              


                ddlTerr.SelectedValue = hdnterr_code.Value;
                ddlwrktype.SelectedValue = hdnwrktype_code.Value;

                if (ddlwrktype.SelectedItem.Text == "Holiday")
                {
                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }

                if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                {

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

                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");


                ddlTerr.SelectedValue = hdnterr_code.Value;
                ddlwrktype.SelectedValue = hdnwrktype_code.Value;

                if (ddlwrktype.SelectedItem.Text == "Holiday")
                {
                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }

                if (ddlwrktype.SelectedItem.Text == "Weekly Off")
                {

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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            //    Label lblDate = (Label)e.Row.FindControl("lblDate");
            //    Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");

            //    HiddenField hdnholiday = (HiddenField)e.Row.FindControl("hdnholiday");
            //    HiddenField hdnweekoff = (HiddenField)e.Row.FindControl("hdnweekoff");
            //    HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
            //    DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
            //    DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
            //    TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");
            //    HiddenField hdnField_Work = (HiddenField)e.Row.FindControl("hdnField_Work");

            //    ddlTerr.SelectedValue = hdnterr_code.Value;


            //    if (ddlTerr.SelectedValue != "0")
            //    {

            //        ddlwrktype.SelectedValue = hdnField_Work.Value;
            //    }


            //    if (hdnholiday.Value != "")
            //    {

            //        ddlwrktype.SelectedValue = hdnholiday.Value;

            //        ddlTerr.SelectedValue = "0";

            //        lblSNo.BackColor = System.Drawing.Color.Aquamarine;
            //        lblDate.BackColor = System.Drawing.Color.Aquamarine;
            //        lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
            //    }
            //    else if (hdnweekoff.Value != "")
            //    {

            //        ddlwrktype.SelectedValue = hdnweekoff.Value;

            //        txtObjective.Text = "Weekly Off";
            //        ddlTerr.SelectedValue = "0";

            //        lblSNo.BackColor = System.Drawing.Color.Pink;
            //        lblDate.BackColor = System.Drawing.Color.Pink;
            //        lblDay_name.BackColor = System.Drawing.Color.Pink;
            //    }





            //    for (int i = 0; i < ddlwrktype.Items.Count; i++)
            //    {
            //        if (ddlwrktype.Items[i].Text == "Field Work")
            //        {
            //            ddlwrktype.Items[i].Attributes.Add("Class", "Color");
            //        }
            //    }

            //}


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                Label lblDay_name = (Label)e.Row.FindControl("lblDay_name");

                HiddenField hdnholiday = (HiddenField)e.Row.FindControl("hdnholiday");
                HiddenField hdnweekoff = (HiddenField)e.Row.FindControl("hdnweekoff");
                HiddenField hdnterr_code = (HiddenField)e.Row.FindControl("hdnterr_code");
                DropDownList ddlwrktype = (DropDownList)e.Row.FindControl("ddlwrktype");
                DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
                TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");
                HiddenField hdnField_Work = (HiddenField)e.Row.FindControl("hdnField_Work");

                ddlTerr.SelectedValue = hdnterr_code.Value;


                if (ddlTerr.SelectedValue != "0")
                {

                    ddlwrktype.SelectedValue = hdnField_Work.Value;
                }


                if (hdnholiday.Value != "")
                {

                    ddlwrktype.SelectedValue = hdnholiday.Value;

                    ddlTerr.SelectedValue = "0";

                    lblSNo.BackColor = System.Drawing.Color.Aquamarine;
                    lblDate.BackColor = System.Drawing.Color.Aquamarine;
                    lblDay_name.BackColor = System.Drawing.Color.Aquamarine;
                }
                else if (hdnweekoff.Value != "")
                {

                    ddlwrktype.SelectedValue = hdnweekoff.Value;

                    txtObjective.Text = "Weekly Off";
                    ddlTerr.SelectedValue = "0";

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

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        createtp("submit");

    }

    protected void hylEdit_Onclick(Object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/MR/TP_New.aspx?Edit=E" + "&month=" + ViewState["month"].ToString() + "&year=" + ViewState["year"].ToString());
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
            // DropDownList ddlwrktype = (DropDownList)row.FindControl("ddlwrktype");

            Label lblDate = (Label)row.FindControl("lblDate");
            TP_Date = lblDate.Text.ToString();
            //Label lblDay = (Label)row.FindControl("lblDay_name");
            //TP_Day = lblDay.Text.ToString();

            DropDownList ddlwrktype = (DropDownList)row.FindControl("ddlwrktype");
            wrktype_code = ddlwrktype.SelectedValue.ToString();

            DropDownList ddlTerritory_Type = (DropDownList)row.FindControl("ddlTerr");
            TP_Terr_Value = ddlTerritory_Type.SelectedValue.ToString();

            if (wrktype_code == "0")
            {
                ddlWwrktype_name = "0";
            }
            else
            {
                ddlWwrktype_name = ddlwrktype.SelectedItem.Text;
            }

            if (TP_Terr_Value == "0")
            {
                TP_Terr_Name = "0";
            }
            else
            {
                TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;
            }

            TextBox txtObjective = (TextBox)row.FindControl("txtObjective");



            TP_Objective = txtObjective.Text.ToString().Replace("'", "asdf");

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);

            iReturn = tp.Tp_Insert_Record(TP_Date, TP_Terr_Name, TP_Terr_Value, wrktype_code, ddlWwrktype_name, TP_Objective, Session["sf_code"].ToString(), Session["sf_name"].ToString(), div_code,mode);

            if (iReturn > 0)
            {

                if (mode == "submit")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted for Approval');window.location='TP_New.aspx'</script>");
                }
                else if (mode == "draft")
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');window.location='TP_New.aspx'</script>");
                }
            }
        }
    }

}