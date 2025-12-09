using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Globalization;
using DBase_EReport;

public partial class MasterFiles_HolidayList : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsStateH = null;
    DataSet dsHolSlno = null;
    DataSet dsTP = null;
    DataSet dsdiv = null;
    string sState = string.Empty;
    string sStateH = string.Empty;
    string div_code = string.Empty;
    string division_code = string.Empty;
    string HSlno = string.Empty;
    string sStateCode = string.Empty;
    string statecode = string.Empty;
    string Holname = string.Empty;
    string HolDate = string.Empty;
    string sf_type = string.Empty;
    string[] statecd;
    string slno;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string state_cd = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        Session["backurl"] = "HolidayList.aspx";

        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
            Filldiv();
            //FillYear(div_code);
            FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
            //FillHoliday(ddlYear.SelectedValue);                              
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    private void Filldiv()
    {
        Division dv = new Division();

        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);

                    ListItem liDiv = new ListItem();

                    liDiv.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    liDiv.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlDivision.Items.Add(liDiv);
                    ddlDDivision.Items.Add(liDiv);
                }
            }
            ddlDivision.Items.Insert(0, new ListItem("---Select---", "0", true));
            ddlDDivision.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        else if (sf_type == "" || sf_type == null)
        {
            dsDivision = dv.getDivision_list();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("---Select---", "0", true));

                ddlDDivision.DataTextField = "Division_Name";
                ddlDDivision.DataValueField = "Division_Code";
                ddlDDivision.DataSource = dsDivision;
                ddlDDivision.DataBind();
                ddlDDivision.Items.Insert(0, new ListItem("---Select---", "0", true));
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
    private void FillHoliday(string state_code, string Year)
    {
        Holiday hol = new Holiday();
        dsHoliday = hol.getHoliday_List(state_code, div_code, Year);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            grdHoliday.Visible = true;
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
        else
        {
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
        validateGridViewSameDate(grdHoliday);
    }

    private void validateGridViewSameDate(GridView grdViewHoliday)
    {
        string sDate = "", test = "";
        try
        {
            List<string> lstDate = new List<string>();
            foreach (GridViewRow grdRow in grdHoliday.Rows)
            {
                Label lblDate = (Label)grdRow.Cells[5].FindControl("lblDate");
                if (sDate != lblDate.Text)
                {
                    sDate = lblDate.Text;
                }
                else
                {
                    lstDate.Add(lblDate.Text);
                }
            }
            foreach (GridViewRow grdRow in grdHoliday.Rows)
            {
                Label lblDates = (Label)grdRow.Cells[5].FindControl("lblDate");
                foreach (string item in lstDate)
                {
                    if (item == lblDates.Text)
                    {
                        lblDates.Attributes.Add("class", "blink_me");
                        lblDates.ForeColor = System.Drawing.Color.White;
                        lblDates.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch { }
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
            dsState = st.getSt(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();

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

    //Change getHolidaylist_DataTable done by saravanan 07-08-2014
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Holiday hol = new Holiday();
        //dtGrid = hol.getHolidaylist_DataTable(div_code);
        dtGrid = hol.getHolidaylist_DataTable(div_code, ddlYear.SelectedValue, ddlState.SelectedValue);
        return dtGrid;
    }

    protected void grdHoliday_Sorting(object sender, GridViewSortEventArgs e)
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
        grdHoliday.DataSource = sortedView;
        grdHoliday.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayFixation.aspx");
    }
    protected void grdHoliday_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdHoliday.EditIndex = -1;
        //Fill the Grid
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
    }

    protected void grdHoliday_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdHoliday.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdHoliday.Rows[e.NewEditIndex].Cells[4].FindControl("txtDate");
        lbl_Old_Date_Tmp.Text = ctrl.Text;
        try
        {
            if (ctrl.Text == "01-01-2016" || ctrl.Text == "26-01-2016" || ctrl.Text == "01-05-2016"
                || ctrl.Text == "15-08-2016" || ctrl.Text == "02-10-2016" || ctrl.Text == "25-12-2016")
            {
                ctrl.Visible = false;
                grdHoliday_RowCancelingEdit(null, null);
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' This Holiday is Fixed... You cannot Change this Date!');</script>");
            }
            else
                ctrl.Focus();
        }
        finally
        {
        }
    }
    protected void grdHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblHSlno = (Label)grdHoliday.Rows[e.RowIndex].Cells[1].FindControl("lblHSlno");
        Label lblState = (Label)grdHoliday.Rows[e.RowIndex].Cells[2].FindControl("lblStateCode");
        Label lblDate = (Label)grdHoliday.Rows[e.RowIndex].Cells[7].FindControl("lblDate");
        HSlno = lblHSlno.Text;
        sStateCode = lblState.Text;
        string sHDate = lblDate.Text;
        string sSlctdState_code = ddlState.SelectedValue;

        // Delete State
        Holiday dv = new Holiday();
        int iReturn = dv.RecordDelete(HSlno, div_code, sHDate, sSlctdState_code);
        if (iReturn > 0)
        {
            //menu1.Status = "Holiday details deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Record cannot be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Exists in Division Master');</script>");
        }
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
    }
    protected void grdHoliday_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdHoliday.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
    }
    protected void grdHoliday_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHoliday.PageIndex = e.NewPageIndex;
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
        grdHoliday_RowCancelingEdit(null, null);
    }
    private void Update(int eIndex)
    {
        Label lblHSlno = (Label)grdHoliday.Rows[eIndex].Cells[1].FindControl("lblHSlno");
        HSlno = lblHSlno.Text;
        Label lblHolidayName = (Label)grdHoliday.Rows[eIndex].Cells[5].FindControl("lblHolidayName");
        Label lblHolidayNameSlNo = (Label)grdHoliday.Rows[eIndex].Cells[5].FindControl("lblHolidayNameSlNo");
        string sHolidayId = lblHolidayNameSlNo.Text;
        Holname = lblHolidayName.Text;
        Label lblStateCode = (Label)grdHoliday.Rows[eIndex].Cells[2].FindControl("lblStateCode");
        Label lblYear = (Label)grdHoliday.Rows[eIndex].Cells[1].FindControl("lblYear");
        string sYear = lblYear.Text;
        statecode = lblStateCode.Text;
        TextBox txtDate = (TextBox)grdHoliday.Rows[eIndex].Cells[6].FindControl("txtDate");
        HolDate = txtDate.Text;
        // Update Holiday
        Holiday dv = new Holiday();
        int iReturn = dv.RecordUpdate(ddlState.SelectedValue, HSlno, Holname, HolDate, div_code, sHolidayId, sYear, lbl_Old_Date_Tmp.Text);
        if (iReturn > 0)
        {
            //menu1.Status = "Holiday Details updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Holiday details already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayView.aspx");
    }
    protected void btnold_Click(object sender, EventArgs e)
    {
        Response.Redirect("HolidayFixation_old.aspx");
    }

    // New
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillHolidayYear(ddlState.SelectedValue,ddlYear.SelectedValue);
        FillHoliday(ddlState.SelectedValue, ddlYear.SelectedValue);
    }
    protected void btnCons_Click(object sender, EventArgs e)
    {
        Response.Redirect("Calendar_Consolidated.aspx");
    }
    protected void btnSingleNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayFixation_Single_StateWise.aspx");
    }
    protected void lblbtnHtransfer_Click(object sender, EventArgs e)
    {
        if (lblbtnHtransfer.CommandArgument == "Show")
        {
            divHtransfer.Visible = true;
            lblbtnHtransfer.CommandArgument = "Hide";
            divHtransfer.Focus();
        }
        else
        {
            divHtransfer.Visible = false;
            lblbtnHtransfer.CommandArgument = "Show";
        }
    }
    private void getStateName()
    {
        Holiday hol = new Holiday();
        DataTable dtStateH = new DataTable();

        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(ddlDivision.SelectedValue);
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

            DataTable dtState = new DataTable();
            State st = new State();
            dsState = st.getSt(state_cd);
            dtState = dsState.Tables[0];

            if (dtState.Rows.Count > 0)
            {
                state_cd = string.Empty;

                for (int k = 0; k < dsState.Tables[0].Rows.Count; k++)
                {
                    sState = dsState.Tables[0].Rows[k]["State_Code"].ToString();

                    dsStateH = hol.getHolidays(sState, ddlDivision.SelectedValue);
                    dtStateH = dsStateH.Tables[0];

                    if (dtStateH.Rows.Count == 0)
                    {
                        var rows = dtState.Select("state_code=" + sState);
                        foreach (var row in rows)
                            row.Delete();
                        dtState.AcceptChanges();
                    }
                }

                if (dtState.Rows.Count > 0)
                {
                    cblTState.DataTextField = "statename";
                    cblTState.DataValueField = "state_code";
                    cblTState.DataSource = dtState;
                    cblTState.DataBind();
                    foreach (ListItem li in cblTState.Items)
                    {
                        li.Attributes.Add("dvalue", li.Value);
                    }
                }
            }
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "0")
        {
            getStateName();
            ddlDDivision.Visible = true;
            lblTState.Visible = true;
            lblDState.Visible = true;
            cblTState.Visible = true;
            cblDState.Visible = true;
            btnTransfer.Visible = true;
            btnClear.Visible = true;
        }
        else
        {
            cblTState.ClearSelection();
            cblDState.ClearSelection();
            ddlDDivision.SelectedValue = "0";
            ddlDDivision.Visible = false;
            lblTState.Visible = false;
            lblDState.Visible = false;
            cblTState.Visible = false;
            cblDState.Visible = false;
            btnTransfer.Visible = false;
            btnClear.Visible = false;
        }
    }
    protected void ddlDDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblDState.ClearSelection();

        if (ddlDDivision.SelectedValue != "0" && ddlDivision.SelectedValue == "0")
        {
            string message;
            message = "<script>alert('Select Target Division')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);

            ddlDDivision.SelectedValue = "0";
        }
        else if (ddlDDivision.SelectedValue != "0" && ddlDDivision.SelectedValue == ddlDivision.SelectedValue)
        {
            string message;
            message = "<script>alert('Divisions can't be same')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);

            ddlDDivision.SelectedValue = "0";
        }
        else
        {
            btnTransfer.Visible = true;
            btnClear.Visible = true;
            lblDState.Visible = true;

            Holiday hol = new Holiday();
            DataTable dtStateH = new DataTable();

            Division dv = new Division();
            dsDivision = dv.getStatePerDivision(ddlDDivision.SelectedValue);
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

                DataTable dtState = new DataTable();
                State st = new State();
                dsState = st.getSt(state_cd);
                dtState = dsState.Tables[0];

                if (dtState.Rows.Count > 0)
                {
                    state_cd = string.Empty;

                    for (int k = 0; k < dsState.Tables[0].Rows.Count; k++)
                    {
                        sState = dsState.Tables[0].Rows[k]["State_Code"].ToString();

                        dsStateH = hol.getHolidays(sState, ddlDDivision.SelectedValue);
                        dtStateH = dsStateH.Tables[0];

                        if (dtStateH.Rows.Count > 0)
                        {
                            var rows = dtState.Select("state_code=" + sState);
                            foreach (var row in rows)
                                row.Delete();
                            dtState.AcceptChanges();
                        }
                    }

                    if (dtState.Rows.Count > 0)
                    {
                        cblDState.DataSource = dtState;
                        cblDState.DataTextField = "statename";
                        cblDState.DataValueField = "state_code";
                        cblDState.DataBind();
                        foreach (ListItem li in cblDState.Items)
                        {
                            li.Attributes.Add("dvalue", li.Value);
                        }
                    }
                }
            }
        }
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Holiday hol = new Holiday();
        DataTable dtStateT = new DataTable();

        try
        {
            int iReturn = 0;

            foreach (ListItem item in cblDState.Items)
            {
                if (item.Selected == true)
                {
                    string state = item.Value.ToString();
                    string strState = state + (',');
                    dsStateH = hol.getHolidays(state, ddlDivision.SelectedValue);
                    dtStateT = dsStateH.Tables[0];

                    if (dtStateT.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtStateT.Rows.Count; i++)
                        {
                            string Academic_Year = dtStateT.Rows[i]["Academic_Year"].ToString().Trim();
                            string Holiday_Date = dtStateT.Rows[i]["Holiday_Date"].ToString().Trim();
                            string Holiday_Name = dtStateT.Rows[i]["Holiday_Name"].ToString().Trim();
                            int aYear = Convert.ToInt32(Holiday_Date.Substring(6, 4));

                            dsHolSlno = hol.getHolidaySlno(ddlDDivision.SelectedValue, Holiday_Name);
                            DataTable dtHolSlno = new DataTable();
                            dtHolSlno = dsHolSlno.Tables[0];

                            if (dtHolSlno.Rows.Count > 0)
                            {
                                int holSlno = Convert.ToInt32(dtHolSlno.Rows[0]["Holiday_Id"].ToString());
                                string multiDate = dtHolSlno.Rows[0]["Multiple_Date"].ToString().Trim();

                                iReturn = InsertHolidayListToDataBase(aYear, holSlno, state, strState, multiDate, Holiday_Name, Holiday_Date);

                                if (iReturn > 0)
                                {
                                    string message;
                                    message = "<script>alert('Holiday Transfered Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
                                    string qry = "Delete Mas_Statewise_Holiday_Fixation WHERE State_Code = '' OR State_Code='NULL' ";
                                    DB_EReporting db = new DB_EReporting();
                                    iReturn = db.Exec_Scalar(qry);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
 
        }
    }
    #region InsertHolidayListToDataBase
    private int InsertHolidayListToDataBase(int aYear, int holSlno, string state, string strState, string multiDate, string Holiday_Name, string Holiday_Date)
    {
        Holiday hol = new Holiday();
        string existingholidayList = "";
        string existingHoliday = hol.getHolidayState(ddlDDivision.SelectedValue, holSlno.ToString(), Holiday_Date);
        string[] arrState = existingHoliday.Split(',');
        int iReturn = -1;

        foreach (string stateStr in arrState)
        {
            if (stateStr == state)
            {
                existingholidayList += stateStr + ",";
            }
        }

        iReturn = hol.RecordAdd(aYear, state, Holiday_Date, Holiday_Name, ddlDDivision.SelectedValue, holSlno, multiDate, existingHoliday);
        
        return iReturn;
    }
    #endregion
    protected void cblTState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDDivision.SelectedValue == "0")
        {
            string message;
            message = "<script>alert('Select Transfer Division')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", message, false);
            cblTState.ClearSelection();
        }
        else
        {
            string result = Request.Form["__EVENTTARGET"];
            int index1 = int.Parse(result.Substring(result.IndexOf("$") + 1));

            bool tf = cblTState.Items[index1].Selected ? true : false;
            string value = cblTState.Items[index1].Text;
            cblTStatus(tf, value);
        }
    }    
    private void cblTStatus(bool tf, string value)
    {
        foreach (ListItem item in cblDState.Items)
        {
            if (item.Text.ToString().Trim() == value)
            {
                item.Selected = tf;
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}
