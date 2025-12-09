using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;
using System.Globalization;

public partial class MasterFiles_HolidayFixation_Single_StateWise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsHoliday = null;
    DataSet dsHolidayM = null;
    DataSet dsHolidayN = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    DataSet dsState = null;
    string div_code = string.Empty;
    string state_cd = string.Empty;
    string[] statecd;
    string sState = string.Empty;
    string str = string.Empty;
    string sStateCode = string.Empty;
    string strState = string.Empty;
    string sDate = string.Empty;
    string sf_Code = string.Empty;
    int time;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_Code = Session["sf_code"].ToString();
        }
        string state_code = string.Empty;
        string Year = string.Empty;
        //lblSelect.Visible = true;
        divSingleSW.Visible = false;
        menu1.Title = this.Page.Title;

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillgvHoliday();
            getStateName();
            FillYear();
            lblSelect.Visible = true;
        }
    }
    private void FillControls()
    {
        Holiday hol = new Holiday();

        dsDivision = hol.get_Holidays(div_code);

        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            DropDownList ddlHolidayName;
            foreach (GridViewRow gridRowP in gvSingleSW.Rows)
            {
                TextBox year = (TextBox)gridRowP.FindControl("txtYear");
                year.Text = ddlYr.SelectedValue;

                TextBox date = (TextBox)gridRowP.FindControl("txtDate");
                string dYear = "DD-MM-" + ddlYr.SelectedValue;
                date.Text = date.Text.Replace("DD-MM-YYYY", dYear);

                DropDownList day = (DropDownList)gridRowP.FindControl("ddlDay");
                DropDownList ddlMonth = (DropDownList)gridRowP.FindControl("ddlMonth");
                day.DataSource = Enumerable.Range(1, 31).Select(x => x.ToString("D02"));
                day.DataBind();
                day.Items.Insert(0, new ListItem("---Select---", "0", true));

                ddlHolidayName = (DropDownList)gridRowP.FindControl("ddlHolidayName");
                ddlHolidayName.DataSource = dsDivision;
                ddlHolidayName.DataTextField = "Holiday_Name";
                ddlHolidayName.DataValueField = "Holiday_Id";
                ddlHolidayName.DataBind();
                ddlHolidayName.Items.Insert(0, new ListItem("---Select---", "0", true));

                if (Request.Browser.Type.Contains("Chrome"))
                {
                    ddlHolidayName.Attributes.Add("onchange", "showLoader('Search1')");
                    day.Attributes.Add("onchange", "showLoader('Search1')");
                    ddlMonth.Attributes.Add("onchange", "showLoader('Search1')");
                }
            }
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYr.Items.Add(k.ToString());
                ddlYr.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
    }
    public void FillgvHoliday()
    {
        Holiday hol = new Holiday();
        dsHoliday = null;
        dsHoliday = hol.getEmptyHoliday();
        DataTable dt = dsHoliday.Tables[0];

        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            gvSingleSW.DataSource = dsHoliday;
            gvSingleSW.DataBind();
        }

        FillControls();

        dsHolidayN = hol.getHoliday_List(ddlState.SelectedValue, div_code, ddlYr.SelectedValue);
        DataTable dtN = dsHolidayN.Tables[0];

        if (ddlYr.SelectedValue != "0" && ddlYr.SelectedValue != "")
        {
            if (dsHolidayN.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dtN.Rows.Count; i++)
                {
                    DropDownList ddlHolidayName = (DropDownList)gvSingleSW.Rows[i].Cells[1].FindControl("ddlHolidayName");
                    DropDownList ddlDay = (DropDownList)gvSingleSW.Rows[i].Cells[2].FindControl("ddlDay");
                    DropDownList ddlMonth = (DropDownList)gvSingleSW.Rows[i].Cells[2].FindControl("ddlMonth");
                    TextBox txtYear = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtYear");
                    TextBox txtDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtDate");
                    TextBox txtOldDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtOldDate");
                    Label lblMulti = (Label)gvSingleSW.Rows[i].Cells[2].FindControl("lblMulti");

                    ddlHolidayName.SelectedValue = dtN.Rows[i]["Holiday_Name_Sl_No"].ToString().Trim();
                    DateTime cDate = Convert.ToDateTime(dtN.Rows[i]["Holiday_Date"]);
                    string day = cDate.Day.ToString("D02");
                    string month = cDate.Month.ToString("D02");
                    string year = cDate.Year.ToString();
                    ddlDay.SelectedValue = day;
                    ddlMonth.SelectedValue = month;
                    txtYear.Text = dtN.Rows[i]["Academic_Year"].ToString().Trim();
                    txtDate.Text = day + "-" + month + "-" + year;
                    txtOldDate.Text = day + "-" + month + "-" + year;

                    dsHolidayM = hol.get_Holidays(div_code);
                    var results = from myRow in dsHolidayM.Tables[0].AsEnumerable()
                                  where myRow.Field<int>("Holiday_Id") == Convert.ToInt32(dtN.Rows[i]["Holiday_Name_Sl_No"])
                                  select myRow.Field<byte>("Multiple_Date");
                    var listDoc = results.ToList();

                    if (listDoc.Count > 0)
                    {
                        string Multi_Date = listDoc[0].ToString();
                        lblMulti.Text = Multi_Date;
                    }

                    if (lblMulti.Text != "0")
                    {
                        if (txtDate.Text != "")
                        {
                            string txtDt = txtDate.Text.Substring(0, txtDate.Text.Length - 4);
                            if (txtDt == "01-01-" || txtDt == "26-01-" || txtDt == "01-05-" || txtDt == "15-08-" || txtDt == "02-10-" || txtDt == "25-12-")
                            {
                                ddlDay.Enabled = false;
                                ddlMonth.Enabled = false;
                                txtYear.Enabled = false;
                            }
                            else
                            {
                                ddlDay.Enabled = true;
                                ddlMonth.Enabled = true;
                                txtYear.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        //ViewState["HolidayTable"] = dt;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        divSingleSW.Visible = true;
        lblSelect.Visible = false;
        ddlState.Enabled = false;
        ddlYr.Enabled = false;
        btnGo.Enabled = false;

        FillgvHoliday();
    }
    private void getStateName()
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
            dsState = st.getSt(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
    }
    protected void ddlHolidayName_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSingleSW.Visible = true;

        Holiday hol = new Holiday();
        string state_code = ddlState.SelectedValue;
        string year = ddlYr.SelectedValue;
        DropDownList ddlHolidayName = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlHolidayName.NamingContainer;
        DropDownList ddlDay = (DropDownList)row.FindControl("ddlDay");
        DropDownList ddlMonth = (DropDownList)row.FindControl("ddlMonth");
        TextBox txtYear = (TextBox)row.FindControl("txtYear");
        TextBox txtDate = (TextBox)row.FindControl("txtDate");
        TextBox txtOldDate = (TextBox)row.FindControl("txtOldDate");
        Label lblMulti = (Label)row.FindControl("lblMulti");
        string sHoliday = ddlHolidayName.SelectedValue;
        string key = string.Empty;

        ddlDay.Enabled = true;
        ddlMonth.Enabled = true;
        txtYear.Enabled = true;

        dsDivision = hol.getHoliday_List(state_code, div_code, year);
        DataTable dt = new DataView(dsDivision.Tables[0]).ToTable();
        DataRow[] dr = dt.Select("Holiday_Name_Sl_No =" + Convert.ToInt32(ddlHolidayName.SelectedValue));

        dsHoliday = hol.get_Holidays(div_code);
        var results = from myRow in dsHoliday.Tables[0].AsEnumerable()
                      where myRow.Field<int>("Holiday_Id") == Convert.ToInt32(ddlHolidayName.SelectedValue)
                      select myRow.Field<byte>("Multiple_Date");
        var listDoc = results.ToList();

        if (dr.Length != 0)
        {
            DataTable dt1 = dr.CopyToDataTable();

            if (dt1.Rows.Count > 0)
            {
                int rowindex;
                rowindex = row.RowIndex;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (GridViewRow gridRow in gvSingleSW.Rows)
                        {
                            DropDownList holiday = (DropDownList)gridRow.FindControl("ddlHolidayName");
                            if (holiday.SelectedValue == sHoliday)
                            {
                                key = "1";
                            }
                        }
                        if (key != "1")
                        {
                            DateTime cDate = Convert.ToDateTime(dt1.Rows[i]["Holiday_Date"]);
                            string day = cDate.Day.ToString("D02");
                            string month = cDate.Month.ToString("D02");
                            string cHdnDate = txtDate.Text.ToString();
                            txtDate.Text = txtDate.Text.Replace(cHdnDate, day + "-" + month + "-" + ddlYr.SelectedValue);
                            txtOldDate.Text = day + "-" + month + "-" + ddlYr.SelectedValue;
                            ddlDay.SelectedValue = day;
                            ddlMonth.SelectedValue = month;

                            if (listDoc.Count > 0)
                            {
                                string Multi_Date = listDoc[0].ToString();
                                lblMulti.Text = Multi_Date;
                            }

                            rowindex++;
                        }
                    }
                    else
                    {
                        ddlHolidayName = (DropDownList)gvSingleSW.Rows[rowindex].Cells[1].FindControl("ddlHolidayName");
                        ddlDay = (DropDownList)gvSingleSW.Rows[rowindex].Cells[2].FindControl("ddlDay");
                        ddlMonth = (DropDownList)gvSingleSW.Rows[rowindex].Cells[2].FindControl("ddlMonth");
                        txtYear = (TextBox)gvSingleSW.Rows[rowindex].Cells[2].FindControl("txtYear");
                        txtDate = (TextBox)gvSingleSW.Rows[rowindex].Cells[2].FindControl("txtDate");
                        txtOldDate = (TextBox)gvSingleSW.Rows[rowindex].Cells[2].FindControl("txtOldDate");

                        foreach (GridViewRow gridRow in gvSingleSW.Rows)
                        {
                            DropDownList holiday = (DropDownList)gridRow.FindControl("ddlHolidayName");
                            if (holiday.SelectedValue == sHoliday)
                            {
                                key = "1";
                            }
                        }
                        if (key != "1")
                        {
                            DropDownList pddlHolidayName = (DropDownList)gvSingleSW.Rows[rowindex - 1].Cells[1].FindControl("ddlHolidayName");

                            ddlHolidayName.SelectedValue = pddlHolidayName.SelectedValue;

                            DateTime cDate = Convert.ToDateTime(dt1.Rows[i]["Holiday_Date"]);
                            string day = cDate.Day.ToString("D02");
                            string month = cDate.Month.ToString("D02");
                            string cHdnDate = txtDate.Text.ToString();
                            txtDate.Text = txtDate.Text.Replace(cHdnDate, day + "-" + month + "-" + ddlYr.SelectedValue);
                            txtOldDate.Text = day + "-" + month + "-" + ddlYr.SelectedValue;
                            ddlDay.SelectedValue = day;
                            ddlMonth.SelectedValue = month;

                            if (listDoc.Count > 0)
                            {
                                string Multi_Date = listDoc[0].ToString();
                                lblMulti.Text = Multi_Date;
                            }
                            rowindex++;
                        }
                    }
                }
            }
        }
        else
        {
            ddlDay.SelectedValue = "0";
            ddlMonth.SelectedValue = "0";
            txtDate.Text = "DD-MM-" + ddlYr.SelectedValue;
            txtOldDate.Text = "";

            if (listDoc.Count > 0)
            {
                string Multi_Date = listDoc[0].ToString();
                lblMulti.Text = Multi_Date;
            }
        }

        if (lblMulti.Text != "0")
        {
            if (txtDate.Text != "")
            {
                string txtDt = txtDate.Text.Substring(0, txtDate.Text.Length - 4);
                if (txtDt == "01-01-" || txtDt == "26-01-" || txtDt == "01-05-" || txtDt == "15-08-" || txtDt == "02-10-" || txtDt == "25-12-")
                {
                    ddlDay.Enabled = false;
                    ddlMonth.Enabled = false;
                    txtYear.Enabled = false;
                }
                else
                {
                    ddlDay.Enabled = true;
                    ddlMonth.Enabled = true;
                    txtYear.Enabled = true;
                }
            }
        }
    }
    protected void gvSingleSW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        divSingleSW.Visible = true;

        Holiday dv = new Holiday();
        int row = e.RowIndex;
        DropDownList ddlHolidayName = (DropDownList)gvSingleSW.Rows[row].FindControl("ddlHolidayName");
        DropDownList ddlDay = (DropDownList)gvSingleSW.Rows[row].FindControl("ddlDay");
        DropDownList ddlMonth = (DropDownList)gvSingleSW.Rows[row].FindControl("ddlMonth");
        TextBox txtYear = (TextBox)gvSingleSW.Rows[row].FindControl("txtYear");
        TextBox txtDate = (TextBox)gvSingleSW.Rows[row].FindControl("txtDate");
        TextBox txtOldDate = (TextBox)gvSingleSW.Rows[row].FindControl("txtOldDate");
        Label lblMulti = (Label)gvSingleSW.Rows[row].FindControl("lblMulti");

        if (ddlDay.SelectedValue != "0" && ddlMonth.SelectedValue != "0" &&
            txtDate.Text != string.Empty && txtDate.Text != "DD-MM-" + ddlYr.SelectedItem.Text.Trim())
        {
            dsDivision = dv.getHoliday_List(ddlState.SelectedValue, div_code, ddlYr.SelectedValue);
            var results = from myRow in dsDivision.Tables[0].AsEnumerable()
                          where myRow.Field<int>("Holiday_Name_Sl_No") == Convert.ToInt32(ddlHolidayName.SelectedValue) &&
                          myRow.Field<string>("Holiday_Date") == txtDate.Text.ToString().Trim()
                          select myRow.Field<decimal>("Sl_No");
            var listSlno = results.ToList();

            if (listSlno.Count > 0)
            {
                int iReturn = dv.RecordDelete(listSlno[0].ToString(), div_code, txtDate.Text, ddlState.SelectedValue);


                if (iReturn > 0)
                {
                    string message;
                    message = "<script>alert('Record Deleted Successfully')</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
                }
                else if (iReturn == -2)
                {
                    string message;
                    message = "<script>alert('Record Exists in Division Master')</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
                }
                else
                {
                    string message;
                    message = "<script>alert('Row Deleted Successfully')</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
                }
            }
        }
        else
        {
            string message;
            message = "<script>alert('Row Deleted Successfully')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
        }
        gvSingleSW.DataSource = null;
        gvSingleSW.DataBind();
        FillgvHoliday();
        //if (ViewState["HolidayTable"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["HolidayTable"];

        //    if (dt.Rows.Count >= 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            ddlHolidayName = (DropDownList)gvSingleSW.Rows[i].Cells[1].FindControl("ddlHolidayName");
        //            txtDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtDate");
        //            txtOldDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtOldDate");

        //            dt.Rows[i]["Holiday_Name_Sl_No"] = ddlHolidayName.SelectedValue;
        //            dt.Rows[i]["Holiday_Date"] = txtDate.Text.ToString().Trim();
        //            dt.Rows[i]["oldDate"] = txtDate.Text.ToString().Trim();
        //        }
        //        if (e.RowIndex < dt.Rows.Count - 1)
        //        {
        //            dt.Rows.Remove(dt.Rows[e.RowIndex]);
        //        }
        //        ViewState["HolidayTable"] = dt;
        //        gvSingleSW.DataSource = dt;
        //        gvSingleSW.DataBind();
        //    }
        //}
        //SetPreviousData();
    }

    //private void SetPreviousData()
    //{
    //    if (ViewState["HolidayTable"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["HolidayTable"];

    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                DropDownList ddlHolidayName = (DropDownList)gvSingleSW.Rows[i].Cells[1].FindControl("ddlHolidayName");
    //                DropDownList ddlDay = (DropDownList)gvSingleSW.Rows[i].Cells[2].FindControl("ddlDay");
    //                DropDownList ddlMonth = (DropDownList)gvSingleSW.Rows[i].Cells[2].FindControl("ddlMonth");
    //                TextBox txtYear = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtYear");
    //                TextBox txtDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtDate");
    //                TextBox txtOldDate = (TextBox)gvSingleSW.Rows[i].Cells[2].FindControl("txtOldDate");
    //                Label lblMulti = (Label)gvSingleSW.Rows[i].Cells[2].FindControl("lblMulti");

    //                if (i < dt.Rows.Count)
    //                {
    //                    string date = dt.Rows[i]["Holiday_Date"].ToString().Trim();
    //                    if (!date.Contains("DD-MM-"))
    //                    {
    //                        ddlHolidayName.SelectedValue = dt.Rows[i]["Holiday_Name_Sl_No"].ToString();
    //                        txtDate.Text = dt.Rows[i]["Holiday_Date"].ToString();
    //                        DateTime cDate = Convert.ToDateTime(txtDate.Text.ToString().Trim());
    //                        string day = cDate.Day.ToString("D02");
    //                        string month = cDate.Month.ToString("D02");
    //                        string year = cDate.Year.ToString();
    //                        ddlDay.SelectedValue = day;
    //                        ddlMonth.SelectedValue = month;
    //                        txtYear.Text = year;
    //                        txtOldDate.Text = dt.Rows[i]["oldDate"].ToString();

    //                        Holiday hol = new Holiday();
    //                        dsHolidayM = hol.get_Holidays(div_code);
    //                        var results = from myRow in dsHolidayM.Tables[0].AsEnumerable()
    //                                      where myRow.Field<int>("Holiday_Id") == Convert.ToInt32(dt.Rows[i]["Holiday_Name_Sl_No"])
    //                                      select myRow.Field<byte>("Multiple_Date");
    //                        var listDoc = results.ToList();

    //                        if (listDoc.Count > 0)
    //                        {
    //                            string Multi_Date = listDoc[0].ToString();
    //                            lblMulti.Text = Multi_Date;
    //                        }

    //                        if (lblMulti.Text != "0")
    //                        {
    //                            if (txtDate.Text != "")
    //                            {
    //                                string txtDt = txtDate.Text.Substring(0, txtDate.Text.Length - 4);
    //                                if (txtDt == "01-01-" || txtDt == "26-01-" || txtDt == "01-05-" || txtDt == "15-08-" || txtDt == "02-10-" || txtDt == "25-12-")
    //                                {
    //                                    ddlDay.Enabled = false;
    //                                    ddlMonth.Enabled = false;
    //                                    txtYear.Enabled = false;
    //                                }
    //                                else
    //                                {
    //                                    ddlDay.Enabled = true;
    //                                    ddlMonth.Enabled = true;
    //                                    txtYear.Enabled = true;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        divSingleSW.Visible = true;

        System.Threading.Thread.Sleep(time);
        try
        {
            //
            #region Variable
            DropDownList ddlHolidayname;
            DropDownList ddlDay;
            DropDownList ddlMonth;
            TextBox txtYear;
            TextBox txtDate;
            TextBox txtOldDate;
            Label lblMulti;
            int iReturn = 0;
            string strDateValue = string.Empty;
            #endregion
            //

            foreach (GridViewRow gridrow in gvSingleSW.Rows)
            {
                #region Variables
                ddlHolidayname = (DropDownList)gridrow.FindControl("ddlHolidayname");
                ddlDay = (DropDownList)gridrow.FindControl("ddlDay");
                ddlMonth = (DropDownList)gridrow.FindControl("ddlMonth");
                txtYear = (TextBox)gridrow.FindControl("txtYear");
                txtDate = (TextBox)gridrow.FindControl("txtDate");
                txtOldDate = (TextBox)gridrow.FindControl("txtOldDate");
                lblMulti = (Label)gridrow.FindControl("lblMulti");
                #endregion

                if (ddlDay.SelectedValue != "0" && ddlMonth.SelectedValue != "0" && txtYear.Text != string.Empty)
                {
                    txtDate.Text = ddlDay.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + txtYear.Text;
                    sState = ddlState.SelectedItem.Text.ToString().Trim();
                    sStateCode = ddlState.SelectedValue.ToString().Trim();

                    str = "";
                    str += sStateCode + (',');
                    string strDate = txtDate.Text.Substring(6, 4);
                    string streach = txtDate.Text;

                    iReturn = InsertHolidayListToDataBase(txtDate.Text.Trim(), txtOldDate, ddlHolidayname, ddlState, lblMulti, ddlHolidayname, strDate, streach);
                }
            }
            if (iReturn > 0)
            {
                string message;
                message = "<script>alert('Holiday Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
                string qry = "Delete Mas_Statewise_Holiday_Fixation WHERE State_Code = '' OR State_Code='NULL' ";
                DB_EReporting db = new DB_EReporting();
                iReturn = db.Exec_Scalar(qry);
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    //
    #region InsertHolidayListToDataBase
    private int InsertHolidayListToDataBase(string sDate, TextBox OldDate, DropDownList HolidayID, DropDownList State_code, Label lblMulti, DropDownList Holidayname, string strDate, string streach)
    {
        Holiday hol = new Holiday();
        int iReturn = -1;

        if (OldDate.Text == sDate && sDate != string.Empty)
        {
            iReturn = 1;
        }
        else if (OldDate.Text != sDate && OldDate.Text != string.Empty)
        {
            iReturn = hol.RecordUpdate(ddlState.SelectedValue, "", Holidayname.SelectedItem.Text, sDate, div_code, HolidayID.SelectedValue, strDate, OldDate.Text);
        }
        else if (OldDate.Text == string.Empty)
        {
            iReturn = hol.Holiday_SingleRecordAdd(Convert.ToInt32(strDate), State_code.SelectedValue, streach, Holidayname.SelectedItem.Text, div_code, Convert.ToInt32(HolidayID.SelectedValue), lblMulti.Text);
        }
        return iReturn;
    }
    #endregion

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSingleSW.Visible = true;

        DropDownList ddlHolidayName = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlHolidayName.NamingContainer;
        DropDownList ddlHoliday = (DropDownList)row.FindControl("ddlHolidayName");
        DropDownList ddlDay = (DropDownList)row.FindControl("ddlDay");
        DropDownList ddlMonth = (DropDownList)row.FindControl("ddlMonth");
        TextBox txtYear = (TextBox)row.FindControl("txtYear");
        TextBox txtDate = (TextBox)row.FindControl("txtDate");

        if (ddlHoliday.SelectedValue == "0" && ddlDay.SelectedValue != "0")
        {
            string message;
            message = "<script>alert('Please select Holiday Name')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
        }
        else
        {
            if (ddlDay.SelectedValue != "0")
            {
                string cDate = txtDate.Text.ToString().Trim();
                string newDay = ddlDay.SelectedValue.ToString().Trim();
                var aStringBuilder = new StringBuilder(cDate);
                aStringBuilder.Remove(0, 2);
                aStringBuilder.Insert(0, newDay);
                cDate = aStringBuilder.ToString();
                txtDate.Text = cDate;
            }
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSingleSW.Visible = true;

        DropDownList ddlHolidayName = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlHolidayName.NamingContainer;
        DropDownList ddlHoliday = (DropDownList)row.FindControl("ddlHolidayName");
        DropDownList ddlDay = (DropDownList)row.FindControl("ddlDay");
        DropDownList ddlMonth = (DropDownList)row.FindControl("ddlMonth");
        TextBox txtYear = (TextBox)row.FindControl("txtYear");
        TextBox txtDate = (TextBox)row.FindControl("txtDate");

        if (ddlHoliday.SelectedValue == "0" && ddlMonth.SelectedValue != "0")
        {
            string message;
            message = "<script>alert('Please select Holiday Name')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
        }
        else
        {
            if (ddlMonth.SelectedValue != "0")
            {
                string cDate = txtDate.Text.ToString().Trim();
                string newMonth = ddlMonth.SelectedValue.ToString().Trim();
                var aStringBuilder = new StringBuilder(cDate);
                aStringBuilder.Remove(3, 2);
                aStringBuilder.Insert(3, newMonth);
                cDate = aStringBuilder.ToString();
                txtDate.Text = cDate;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayList.aspx");
    }
}