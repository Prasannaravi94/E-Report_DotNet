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

public partial class MasterFiles_HolidayFixation : System.Web.UI.Page
{
    //
    #region Variables
    private string strQry = string.Empty;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dshol = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string slno;
    string str = string.Empty;
    string state_code = string.Empty;
    string[] statecd;
    string strState;
    string state_cd = string.Empty;
    string Holiday_Name = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    protected string Values;
    int time;
    string strCase = string.Empty;
    Repeater rptAmounts;
    DataSet ds = new DataSet();
    #endregion
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "HolidayList.aspx";
        slno = Request.QueryString["Sl_No"];   
        if (!Page.IsPostBack)
        {
            bindState();
            bindStatenew();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
            if (slno != "" && slno != null)
            {
                Holiday dv = new Holiday();
                dshol = dv.getHoli(div_code, slno);
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
    public void bindHoliday()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("CREATE TABLE #TestTable (Holiday_Id int,Month int,Holiday_Name VARCHAR(100), Multiple_Date int, " +
                                         "Holiday_Date VARCHAR(100), Holiday_SlNo int) " +
                                         "insert into #TestTable " +
                                         "SELECT H.Holiday_Id,H.Month,H.Holiday_Name,H.Multiple_Date, " +
                                         "convert(varchar,ISNULL(CASE WHEN YEAR(H.Fixed_Date)=" + ddlYear.Text + " THEN H.Fixed_Date ELSE NULL END, NULL),105)as Holiday_Date, H.Holiday_SlNo   " +
                                         "from Holidaylist H " +
                                         //  left outer join  Mas_Statewise_Holiday_Fixation S on " + "H.Holiday_Id=S.Holiday_Name_Sl_No 
                                         "where H.Division_Code='" + div_code + "' and Holiday_Active_Flag = 0  " +
                                         "union all " +
                                         "SELECT H.Holiday_Id,H.Month,H.Holiday_Name,H.Multiple_Date, " +
                                         "convert(varchar,ISNULL(CASE WHEN YEAR(s.Holiday_Date)=" + ddlYear.Text + " THEN s.Holiday_Date ELSE NULL END, NULL),105)as Holiday_Date, H.Holiday_SlNo   " +
                                         "from Holidaylist H left outer join  Mas_Statewise_Holiday_Fixation S on " +
                                         "H.Holiday_Id=S.Holiday_Name_Sl_No where H.Division_Code='" + div_code + "' and s.Academic_Year=" + ddlYear.Text + " and Holiday_Active_Flag = 0  " +
                                         "SELECT  Holiday_Id,Month,Holiday_Name,Multiple_Date," +
                                         "STUFF((SELECT DISTINCT ',' + Holiday_Date FROM #TestTable " +
                                         "WHERE (Holiday_Id = StudentCourses.Holiday_Id) FOR XML PATH ('')),1,1,'') AS Holiday_Date " +
                                         "FROM #TestTable StudentCourses " +
                                         "GROUP BY Holiday_Id,Month,Holiday_SlNo,Holiday_Name,Multiple_Date order by Holiday_SlNo", con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        rptName.DataSource = ds;
        rptName.DataBind();
    }
    public DataSet getState(string state_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;        
        strQry = " SELECT state_code,left(statename,12) as  statename,shortname " +
                 " FROM mas_state " +
                 " WHERE state_code in (" + state_code + ") " +
                 " ORDER BY 2";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlholiday.Visible = false;
    }
    
    public void bindState()
    {
        DB_EReporting db_ER = new DB_EReporting();
        strQry = " SELECT state_code,statename,shortname " +
                          " FROM mas_state " +
                          " WHERE state_code in (" + state_code + ") " +
                          " ORDER BY 2";
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
                    state_code = state_cd; 
                }
                i++;
            }

            dsState = getState(state_cd);
            rptYearHeader.DataSource = dsState;
            rptYearHeader.DataBind();
        }
    }
    protected void rptName_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string squery = "";
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HtmlTableCell Holiday = (HtmlTableCell)e.Item.FindControl("TDHoliday"); 
            HiddenField hdnHolidayId = (HiddenField)e.Item.FindControl("hdnHolidayId");
            TextBox txtDate = (TextBox)e.Item.FindControl("txtDate");
            Repeater rptCal = (Repeater)e.Item.FindControl("rptAmounts");
            Label lblMulti = (Label)e.Item.FindControl("lblMulti");
            Literal litName = (Literal)e.Item.FindControl("litName");
            string Color = ds.Tables[0].Rows[e.Item.ItemIndex]["Month"].ToString();

            string Clr = GetCaseColor(Color);
            if (lblMulti.Text != "0")
            {
                if (txtDate.Text != "")
                {
                    string txtDt = txtDate.Text.Substring(0, txtDate.Text.Length - 4);
                    if (txtDt == "01-01-" || txtDt == "26-01-" || txtDt == "01-05-" || txtDt == "15-08-" || txtDt == "02-10-" || txtDt == "25-12-")
                    {
                        txtDate.Text = txtDate.Text.Replace(txtDate.Text.Substring(txtDate.Text.Length - 4, 4), ddlYear.Text);
                        txtDate.Enabled = false;
                        txtDate.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if (txtDate.Text.Substring(txtDate.Text.Length - 4, 4) != ddlYear.Text)
                        {
                            txtDate.Text = "";
                        }
                    }
                }
                squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and s.Academic_Year=" + ddlYear.Text + " and state_code in ('+ CAST(h.state_code  as varchar) +')) as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
                DB_EReporting db_ER = new DB_EReporting();
                dsState = db_ER.Exec_DataSet(squery);

                rptCal.DataSource = dsState;
                rptCal.DataBind();
            }
            else
            {
                if (txtDate.Text != "")
                {
                    if (txtDate.Text.Substring(txtDate.Text.Length - 4, 4) != ddlYear.Text)
                    {
                        txtDate.Text = "";
                    }
                }
                squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and s.Academic_Year=" + ddlYear.Text + " and state_code =CAST(h.state_code  as varchar)) as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
                DB_EReporting db_ER = new DB_EReporting();
                dsState = db_ER.Exec_DataSet(squery);

                rptCal.DataSource = dsState;
                rptCal.DataBind();
            }
        }
    }
    protected void rptCal_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnHolidaySeleted = (HiddenField)e.Item.FindControl("hdnseleted");
            HiddenField hdnStateName = (HiddenField)e.Item.FindControl("hdnStateName");
            CheckBox cb = (CheckBox)e.Item.FindControl("cbHolidaySelection");

            cb.ToolTip = hdnStateName.Value;
            if (hdnHolidaySeleted.Value == "1")
            {
                cb.Checked = true;
            }
        }
    }
    //
    #region btnSave_OnClick
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            //
            #region Variable
            Repeater rptAmounts;
            Literal litHolidayname;
            TextBox txtDate;
            TextBox txtAdd;
            TextBox txtAddDate;
            CheckBox cbHolidaySelection;
            CheckBox chksta=new CheckBox();
            CheckBox chkstate2=new CheckBox();
            HiddenField hdnHolidayID;
            HiddenField hdnState_code;
            int iReturn = 0;
            string strDateValue = string.Empty;
            #endregion
            //
            #region RepeaterItem rptName
            foreach (RepeaterItem item in rptName.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    #region Variables
                    rptAmounts = (Repeater)item.FindControl("rptAmounts");
                    litHolidayname = (Literal)item.FindControl("litName");
                    txtDate = (TextBox)item.FindControl("txtDate");
                    txtAdd = (TextBox)item.FindControl("txtAdd");
                    txtAddDate = (TextBox)item.FindControl("txtAdd1");
                    hdnHolidayID = (HiddenField)item.FindControl("hdnHolidayID");
                   
                    #endregion
                    //
                    if (txtAdd.Text != "" || txtAddDate.Text != "" || txtDate.Text != "")
                    {
                        //                    
                            #region RepeaterItem rptAmounts
                            foreach (RepeaterItem checkItem in rptAmounts.Items)
                            {
                                string strStateCode = null;
                                hdnState_code = (HiddenField)checkItem.FindControl("hdnState_code");
                                cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                                Label lblMulti = (Label)item.FindControl("lblMulti");
                                chksta = (CheckBox)checkItem.FindControl("chkstate1");
                                chkstate2 = (CheckBox)checkItem.FindControl("chkstate2");

                                str = "";
                                strDateValue = "";

                                if (cbHolidaySelection.Checked == true || chksta.Checked == true || chkstate2.Checked == true)
                                {
                                    if (txtDate.Text != "" || txtAdd.Text != "" || txtAddDate.Text != "")
                                    {
                                        statecd = lblValues.Text.Split(',');
                                        strStateCode = hdnState_code.Value;
                                        str += strStateCode + (',');
                                        strDateValue += txtDate.Text + "," + txtAdd.Text + "," + txtAddDate.Text;
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The Holiday Date should not be empty');</script>");
                                        txtDate.Focus();
                                    }
                                }
                                else
                                {
                                    if (txtDate.Text != "" || txtAdd.Text != "" || txtAddDate.Text != "")
                                    {
                                        statecd = lblValues.Text.Split(',');
                                        strStateCode = hdnState_code.Value;
                                        str += strStateCode + (',');
                                        strDateValue = txtDate.Text + "," + txtAdd.Text + "," + txtAddDate.Text + (',');
                                    }
                                }

                                if (str != "" || txtDate.Text != "")
                                {
                                    string strDate = txtDate.Text.Substring(6, 4);
                                    string[] strArry;
                                    strArry = strDateValue.Split(',');
                                    foreach (string streach in strArry)
                                    {
                                        if (streach != "")
                                        {
                                            //
                                            #region TextBox 1st Date
                                            if (streach == txtDate.Text && streach != "")
                                            {
                                                iReturn = InsertHolidayListToDataBase(txtDate.Text, cbHolidaySelection, hdnHolidayID, hdnState_code, lblMulti, litHolidayname, strDate, streach);
                                            }
                                            #endregion
                                            //
                                            #region TextBox 2nd Date
                                            if (streach == txtAdd.Text && streach != "")
                                            {
                                                iReturn = InsertHolidayListToDataBase(txtAdd.Text, chksta, hdnHolidayID, hdnState_code, lblMulti, litHolidayname, strDate, streach);
                                            }
                                            #endregion
                                            //
                                            #region TextBox 3rd Date
                                            if (streach == txtAddDate.Text && streach != "")
                                            {
                                                iReturn = InsertHolidayListToDataBase(txtAddDate.Text, chkstate2, hdnHolidayID, hdnState_code, lblMulti, litHolidayname, strDate, streach);
                                            }
                                            #endregion
                                            //
                                        }
                                    }
                                    if (iReturn > 0)
                                    {
                                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Holiday Created Successfully')</script>");
                                        string qry = "Delete Mas_Statewise_Holiday_Fixation WHERE State_Code = '' OR State_Code='NULL' ";
                                        DB_EReporting db = new DB_EReporting();
                                        iReturn = db.Exec_Scalar(qry);
                                        pnlholiday.Visible = false;
                                    }
                                }
                            }
                            #endregion
                            //
                    }
                }
            }
            #endregion
            //
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    //
    #region InsertHolidayListToDataBase
    private int InsertHolidayListToDataBase(string sDate, CheckBox ckBox, HiddenField hdnHolidayID, HiddenField hdnState_code, Label lblMulti, Literal litHolidayname, string strDate, string streach)
    {
        Holiday hol = new Holiday();
        string existingholidayList = "";
        string existingHoliday = hol.getHolidayState(div_code, hdnHolidayID.Value, sDate);
        string[] arrState = existingHoliday.Split(',');
        int iReturn = -1;

        foreach (ListItem checkValue in Chkstate.Items)
        {
            if (checkValue.Selected)
            {
                foreach (string stateStr in arrState)
                {
                    if (stateStr == checkValue.Value)
                    {
                        existingholidayList += stateStr + ",";
                    }
                }
            }
        }

        strState = "";
        if (ckBox.Checked == true && sDate != "")
        {
            strState += str;
            iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingHoliday);
        }
        else if (ckBox.Checked == false && sDate != "")
        {
            existingholidayList = "";
            string strChkValue = str;
            if (strChkValue.Length>1)            
                strChkValue = strChkValue.Remove(strChkValue.Length - 1);
            string[] strExistingHoliday = existingHoliday.Split(',');
            foreach (string strExist in strExistingHoliday)
            {
                string[] arr = { strExist };
                string st = arr[0];
                if (st != hdnState_code.Value && st != "")
                    existingholidayList += st + ",";
            }
            if (existingholidayList.Length > 1)
            {
                existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
            }
            iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingholidayList);
        }
        return iReturn;
    }
    #endregion
    //
    #endregion
    //
    #region bindStatenew for Checkbox List
    public void bindStatenew()
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
            Chkstate.DataTextField = "statename";
            Chkstate.DataValueField = "state_code";
            Chkstate.DataSource = dsState;
            Chkstate.DataBind();
        }
    }
    #endregion
    //
   
    protected void btngo_onclick(object sender, EventArgs e)
    {
         string squery = "";
         System.Threading.Thread.Sleep(time);
         lblValues.Text = string.Empty;
      
         foreach (ListItem li in Chkstate.Items)
         {
             if (li.Selected)
             {
                 strState += li.Value + ",";
                 lblValues.Text += li.Value + ",";
                 dsState = getState(strState.Remove(strState.Length - 1));
                 rptYearHeader.DataSource = dsState;
                 rptYearHeader.DataBind();
             }
         }
         if (strState != "")
         {
             strState = strState.TrimEnd(',');
             ViewState["hidSelectedState"] = strState;
         }
   
        bindHoliday();
        pnlholiday.Visible = true;
        foreach (RepeaterItem ri in rptName.Items)
        {
            rptAmounts = (Repeater)ri.FindControl("rptAmounts");

            foreach (RepeaterItem checkItem in rptAmounts.Items)
            {
                Literal quantityLabel = (Literal)ri.FindControl("litName");
                TextBox partNumberLabel = (TextBox)ri.FindControl("txtDate");
                TextBox partNumberLabel1 = (TextBox)ri.FindControl("txtAdd");
                TextBox partNumberLabel2 = (TextBox)ri.FindControl("txtAdd1");
                CheckBox cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                CheckBox ChkState = (CheckBox)checkItem.FindControl("chkstate1");
                CheckBox ChkState1 = (CheckBox)checkItem.FindControl("chkstate2");
                Label lblMulti = (Label)ri.FindControl("lblMulti");
                string quantityText = quantityLabel.Text;                

                if (partNumberLabel.Text != "" || partNumberLabel1.Text != "" || partNumberLabel2.Text != "")
                {
                    if (lblMulti.Text == "0")
                    {
                        partNumberLabel1.Visible = true;
                        partNumberLabel2.Visible = true;
                        ChkState.Visible = true;
                        ChkState1.Visible = true;

                        string[] str = partNumberLabel.Text.Split(',');
                        if (checkItem.ItemIndex == 0)
                        {
                            str.Length.ToString();

                            for (int i = 0; i < str.Length; i++)
                            {
                                if (i == 0)
                                {
                                    partNumberLabel.Text = str[0];
                                }
                                if (i == 1)
                                {
                                    partNumberLabel1.Text = str[1];
                                }
                                if (i == 2)
                                {
                                    partNumberLabel2.Text = str[2];
                                }
                            }
                        }
                        ChkState.Controls.Add(new LiteralControl("<br/>"));
                        ChkState1.Controls.Add(new LiteralControl("<br/>"));
                    }

                    squery = getQuery(partNumberLabel);

                    DB_EReporting db_ER = new DB_EReporting();
                    dsState = db_ER.Exec_DataSet(squery);
                    cbHolidaySelection.Checked = false;
                    if (dsState != null)
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                        {
                            if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                            {
                                string str1 = dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString();
                                if (partNumberLabel.Text.Trim() == str1)
                                {
                                    cbHolidaySelection.Checked = true;
                                }
                            }
                        }
                    }
                    else if (dsState == null && partNumberLabel.Text.Trim() != "")
                    {
                        pnlholiday.Visible = false;
                        Server.Transfer("~/MasterFiles/HolidayList.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Multiple Holidays Selected for same State & Date (" + partNumberLabel.Text.Trim() + ")')</script>");
                        break;
                    }
                    squery = getQuery(partNumberLabel1);
                    
                    dsState = db_ER.Exec_DataSet(squery);
                    ChkState.Checked = false;
                    if (dsState != null)
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                        {
                            if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                            {
                                string str1 = dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString();
                                if (partNumberLabel1.Text.Trim() == str1)
                                {
                                    ChkState.Checked = true;
                                }
                            }
                        }
                    }
                    else if (dsState == null && partNumberLabel1.Text.Trim() != "")
                    {
                        pnlholiday.Visible = false;
                        Server.Transfer("~/MasterFiles/HolidayList.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Multiple Holiday Selected for same State & Date (" + partNumberLabel1.Text.Trim() + ")')</script>");
                        break;
                    }
                    squery = getQuery(partNumberLabel2);                    

                    dsState = db_ER.Exec_DataSet(squery);
                    ChkState1.Checked = false;
                    if (dsState != null)
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                        {
                            if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                            {
                                if (partNumberLabel2.Text.Trim() == dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString())
                                {
                                    ChkState1.Checked = true;
                                }
                            }
                        }
                    }
                    else if (dsState == null && partNumberLabel2.Text.Trim() != "")
                    {
                        pnlholiday.Visible = false;
                        Server.Transfer("~/MasterFiles/HolidayList.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Multiple Holiday Selected for same State & Date (" + partNumberLabel2.Text.Trim() + ")')</script>");
                        break;
                    }
                }
            }
        }
    }
    //
    #region getQuery
    private string getQuery(TextBox txtDate)
    {
        string qry = "";
        qry = "select state_code ,statename, " +
                                "(SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + txtDate.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%' or s.state_code = CAST(h.state_code  as varchar)) and s.Division_Code='" + div_code + "') as HolidaySeleted, " +
                                "(SELECT s.holiday_name from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + txtDate.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%' or s.state_code = CAST(h.state_code  as varchar)) and s.Division_Code='" + div_code + "') as holiday_name, " +
                                " (SELECT convert(char(10),holiday_date,105) from " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + txtDate.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%' or s.state_code = CAST(h.state_code  as varchar)) and s.Division_Code='" + div_code + "') " +
                                " as HolidayDate from mas_state h  where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
        return qry;
    }
    #endregion
    //
    //
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        Chkstate.Attributes.Add("onclick", "checkAll(this);");
    }
    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#FFFFCC";
                break;
            case "2":
                strCase = "#CCCC66";
                break;
            case "3":
                strCase = "#6699FF";
                break;
            case "4":
                strCase = "#CCFFFF";
                break;
            case "5":
                strCase = "#CCFFCC";
                break;
            case "6":
                strCase = "#F7819F";
                break;
            case "7":
                strCase = "#0B610B";
                break;
            case "8":
                strCase = "#FF4000";
                break;
            case "9":
                strCase = "#FE2E64";
                break;
            case "10":
                strCase = "#9AFE2E";
                break;
            case "11":
                strCase = "#F2F5A9";
                break;
            case "12":
                strCase = "#F5A9D0";
                break;
            default:
                strCase = "#F2F5A9";
                break;
        }
        return strCase;
    }
}