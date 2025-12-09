using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Bus_EReport;

public partial class MasterFiles_ActivityReports_Dashboard_DDSlide : System.Web.UI.Page
{
    #region Declaration
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
    string drPrdBrd = string.Empty;
    string filter = string.Empty;
    string Slide = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    int search = 0;
    int time;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Label lblHeading;
    #endregion

    #region Page_lifecycle
    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        drSpcCode = "NULL";
        drCatCode = "NULL";
        drQualCode = "NULL";
        drClassCode = "NULL";
        drTrrCode = "NULL";
        drPrdBrd = "NULL";
        filter = " Filter: ALL";

        sf_code = Session["sf_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            //ServerStartTime = DateTime.Now;
            //base.OnPreInit(e);

            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";

                FillMasterList();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";

                FillMasterList();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                Fillteam();
            }

            ddlFieldForce.SelectedValue = sf_code;

            cFMnth = DateTime.Now.Month.ToString().Trim();
            cFYear = DateTime.Now.Year.ToString().Trim();
            cTMnth = DateTime.Now.Month.ToString().Trim();
            cTYear = DateTime.Now.Year.ToString().Trim();

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = cFYear;

                    ddlTYear.Items.Add(k.ToString());
                    ddlTYear.SelectedValue = cTYear;
                }
            }
            ddlFMonth.SelectedValue = cFMnth;
            ddlTMonth.SelectedValue = cTMnth;

            FillReport();
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                sf_code = ddlFieldForce.SelectedValue;
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                sf_code = ddlFieldForce.SelectedValue;
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                lblHeading = (Label)c1.FindControl("lblHeading");
                lblHeading.Text = "Listed Doctor Slide Analysis";
            }
        }
    }
    //protected override void OnLoadComplete(EventArgs e)
    //{
    //    ServerEndTime = DateTime.Now;
    //    TrackPageTime();//It will give you page load time 
    //}
    //public void TrackPageTime()
    //{
    //    TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
    //    time = serverTimeDiff.Minutes;
    //}
    #endregion

    #region Fillteam
    private void Fillteam()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.Hierarchy_Team(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    #endregion

    #region FillMasterList
    private void FillMasterList()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    #endregion

    public static Array GetMonths(DateTime date1, DateTime date2)
    {
        //Note - You may change the format of date as required.  
        return GetDates(date1, date2).Select(x => x.ToString("MMMM yyyy")).ToArray();
    }

    public static IEnumerable<DateTime> GetDates(DateTime date1, DateTime date2)
    {
        while (date1 <= date2)
        {
            yield return date1;
            date1 = date1.AddMonths(1);
        }
        if (date1 > date2 && date1.Month == date2.Month)
        {
            // Include the last month  
            yield return date1;
        }
    }

    #region FillReport
    private void FillReport()
    {
        cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
        ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

        int days = DateTime.DaysInMonth(Convert.ToInt32(ctyear), Convert.ToInt32(ctmonth));

        string fDate = cfmonth + "/01/" + cfyear;
        string tDate = ctmonth + "/" + days + "/" + ctyear;

        //DateTime fDate = DateTime.ParseExact(cfyear + "-" + cfmonth.ToString("D2") + "-01", "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);
        //DateTime tDate = DateTime.ParseExact(ctyear + "-" + ctmonth.ToString("D2") + "-01", "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);

        //Array dates = GetMonths(fDate, tDate);
        //string[] strDate = dates.OfType<object>().Select(o => o.ToString()).ToArray();
        //string strDates = (new JavaScriptSerializer()).Serialize(strDate);

        string monthName = new DateTime(cfyear, cfmonth, 1).ToString("MMM", CultureInfo.InvariantCulture);

        int months = (Convert.ToInt32(ctyear) - Convert.ToInt32(cfyear)) * 12 + Convert.ToInt32(ctmonth) - Convert.ToInt32(cfmonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(cfmonth);
        int cyear = Convert.ToInt32(cfyear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        Product sf = new Product();
        DataSet dsSlide = new DataSet();
        DataSet dsSlideListedDr = new DataSet();
        DataSet dsSlideTime = new DataSet();
        DataSet dsSlideusrLike = new DataSet();

        mode = Convert.ToInt32(rblBasedOn.SelectedValue);

        dsSlide = sf.getSlides(ddlFieldForce.SelectedValue, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, mode);

        string strSlide = string.Empty;
        string strListedDr = string.Empty;
        string strSlideTime = string.Empty;
        string strSlideUsrLike = string.Empty;
        string strSlideUsrDisLike = string.Empty;
        string strSlideUsrNil = string.Empty;

        if (dsSlide.Tables[0].Rows.Count > 0)
        {
            string[] arrSlide = new string[dsSlide.Tables[0].Rows.Count];
            string[] arrListedDr = new string[dsSlide.Tables[0].Rows.Count];
            string[] arrSlideTime = new string[dsSlide.Tables[0].Rows.Count];
            string[] arrSlideUsrLike = new string[dsSlide.Tables[0].Rows.Count];
            string[] arrSlideUsrDisLike = new string[dsSlide.Tables[0].Rows.Count];
            string[] arrSlideUsrNil = new string[dsSlide.Tables[0].Rows.Count];

            dsSlideListedDr = sf.getSlideListedDr(ddlFieldForce.SelectedValue, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, mode);
            dsSlideTime = sf.getSlideTime(ddlFieldForce.SelectedValue, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, mode);
            dsSlideusrLike = sf.getSlideUsrLike(ddlFieldForce.SelectedValue, fDate.ToString(), tDate.ToString(), drSpcCode, drCatCode, drQualCode, drClassCode, drTrrCode, drPrdBrd, div_code, mode);

            int i = 0;
            foreach (DataRow slide in dsSlide.Tables[0].Rows)
            {
                arrSlide[i] = dsSlide.Tables[0].Rows[i]["SlideName"].ToString();

                var rsMSL_code = from row in dsSlideListedDr.Tables[0].AsEnumerable()
                                 where row.Field<string>("SlideName") == slide["SlideName"].ToString()
                                 select new
                                 {
                                     MSL_code = row.Field<string>("MSL_code")
                                 };

                if (rsMSL_code.Any())
                {
                    if (i < dsSlide.Tables[0].Rows.Count)
                    {
                        arrListedDr[i] = rsMSL_code.Count().ToString();
                    }
                }
                else
                {
                    arrListedDr[i] = "0";
                }

                var rsSlideTime = from row in dsSlideTime.Tables[0].AsEnumerable()
                                  where row.Field<string>("SlideName") == slide["SlideName"].ToString()
                                  select new
                                  {
                                      SlideTime = row.Field<int>("Time")
                                  };

                if (rsSlideTime.Any())
                {
                    if (i < dsSlide.Tables[0].Rows.Count)
                    {
                        TimeSpan t = TimeSpan.FromSeconds((double)(new decimal(rsSlideTime.Sum(item => item.SlideTime))));
                        //if (t.Minutes <= 0)
                        //{
                        //    arrSlideTime[i] = t.Seconds.ToString() + "s";
                        //}
                        //else
                        //{
                        //arrSlideTime[i] = t.Minutes.ToString();
                        //}
                        arrSlideTime[i] = (t.Minutes + "." + t.Seconds);
                        //arrSlideTime[i] = (t.ToString().Substring(4)).Replace(":", ".");
                    }
                }
                else
                {
                    arrSlideTime[i] = "0";
                }

                var rsSlideUsrLike = from row in dsSlideusrLike.Tables[0].AsEnumerable()
                                     where row.Field<string>("SlideName") == slide["SlideName"].ToString()
                                     && row.Field<byte>("usrLike") == 1
                                     select new
                                     {
                                         SlideUsrLike = row.Field<byte>("usrLike")
                                     };

                var rsSlideUsrDisLike = from row in dsSlideusrLike.Tables[0].AsEnumerable()
                                        where row.Field<string>("SlideName") == slide["SlideName"].ToString()
                                        && row.Field<byte>("usrLike") == 2
                                        select new
                                        {
                                            SlideUsrLike = row.Field<byte>("usrLike")
                                        };

                var rsSlideUsrNil = from row in dsSlideusrLike.Tables[0].AsEnumerable()
                                    where row.Field<string>("SlideName") == slide["SlideName"].ToString()
                                    && row.Field<byte>("usrLike") == 0
                                    select new
                                    {
                                        SlideUsrLike = row.Field<byte>("usrLike")
                                    };

                if (rsSlideUsrLike.Any())
                {
                    if (i < dsSlide.Tables[0].Rows.Count)
                    {
                        arrSlideUsrLike[i] = rsSlideUsrLike.Count().ToString();
                    }
                }
                else
                {
                    arrSlideUsrLike[i] = "0";
                }

                if (rsSlideUsrDisLike.Any())
                {
                    if (i < dsSlide.Tables[0].Rows.Count)
                    {
                        arrSlideUsrDisLike[i] = rsSlideUsrDisLike.Count().ToString();
                    }
                }
                else
                {
                    arrSlideUsrDisLike[i] = "0";
                }

                if (rsSlideUsrNil.Any())
                {
                    if (i < dsSlide.Tables[0].Rows.Count)
                    {
                        arrSlideUsrNil[i] = rsSlideUsrNil.Count().ToString();
                    }
                }
                else
                {
                    arrSlideUsrNil[i] = "0";
                }
                i++;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Slides", typeof(string));
            dt.Columns.Add("ListedDrs", typeof(int));
            dt.Columns.Add("SlideTimes", typeof(string));
            dt.Columns.Add("SlideUsrLikes", typeof(string));
            dt.Columns.Add("SlideUsrDisLikes", typeof(string));
            dt.Columns.Add("SlideUsrNils", typeof(string));

            int iSlide = arrSlide.Count();
            for (int j = 0; j < arrSlide.Count(); j++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[j]["Slides"] = arrSlide[j];
                dt.Rows[j]["ListedDrs"] = Convert.ToInt32(arrListedDr[j]);
                dt.Rows[j]["SlideTimes"] = arrSlideTime[j];
                dt.Rows[j]["SlideUsrLikes"] = arrSlideUsrLike[j];
                dt.Rows[j]["SlideUsrDisLikes"] = arrSlideUsrDisLike[j];
                dt.Rows[j]["SlideUsrNils"] = arrSlideUsrNil[j];
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "ListedDrs desc";
            DataTable srtDT = dv.ToTable();

            //for (int j = 0; j < dsSlide.Tables[0].Rows.Count; j++)
            //{
            //    arrSlide[j] = dsSlide.Tables[0].Rows[j]["SlideName"].ToString();
            //}

            arrSlide = srtDT.AsEnumerable().Select(r => r.Field<string>("Slides")).ToArray();
            arrListedDr = srtDT.AsEnumerable().Select(r => Convert.ToString(r.Field<int>("ListedDrs"))).ToArray();
            arrSlideTime = srtDT.AsEnumerable().Select(r => r.Field<string>("SlideTimes")).ToArray();
            arrSlideUsrLike = srtDT.AsEnumerable().Select(r => r.Field<string>("SlideUsrLikes")).ToArray();
            arrSlideUsrDisLike = srtDT.AsEnumerable().Select(r => r.Field<string>("SlideUsrDisLikes")).ToArray();
            arrSlideUsrNil = srtDT.AsEnumerable().Select(r => r.Field<string>("SlideUsrNils")).ToArray();

            strSlide = (new JavaScriptSerializer()).Serialize(arrSlide);
            strListedDr = (new JavaScriptSerializer()).Serialize(arrListedDr);
            strListedDr = strListedDr.Replace("null", "0");
            strSlideTime = (new JavaScriptSerializer()).Serialize(arrSlideTime);
            strSlideTime = strSlideTime.Replace("null", "0");
            strSlideUsrLike = (new JavaScriptSerializer()).Serialize(arrSlideUsrLike);
            strSlideUsrDisLike = (new JavaScriptSerializer()).Serialize(arrSlideUsrDisLike);
            strSlideUsrNil = (new JavaScriptSerializer()).Serialize(arrSlideUsrNil);
        }
        else
        {

        }

        string uri = string.Empty;
        uri = "/MasterFiles/ActivityReports/rpt_Dashboard_DDSlide.aspx";
        #region HighChart_Multi
        string script = "<script type=text/javascript src=https://code.highcharts.com/stock/highstock.js></script><script src=https://code.highcharts.com/modules/series-label.js></script><script src=https://code.highcharts.com/modules/exporting.js></script><script src=https://code.highcharts.com/modules/export-data.js></script><script type=text/javascript> Highcharts.chart('highcontainer', { " +
        "	chart: {	" +
        "     zoomType: 'xy'	" +
        " },	" +
        " title: {	" +
        "     text: 'Digital Detailing - Product Slide Analysis'	" +
        " },	" +
        " subtitle: {	" +
        "     text: 'FieldForce: " + ddlFieldForce.SelectedItem.Text + "<br />   From: " + ddlFMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + "\u00a0 \u00a0 To: " + ddlTMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + "\u00a0 \u00a0 \u00a0" + filter + "' " +
        " },	" +
        " xAxis: [{	" +
        "     categories:" + strSlide + ",	" +
        "     min: 0,	" +
        "     max: 15,	" +
        "     crosshair: true	" +
        " }], " +
        " scrollbar: {" +
        "     enabled: true, barBackgroundColor: 'gray'," +
        "     barBorderRadius: 7," +
        "     barBorderWidth: 0," +
        "     buttonBackgroundColor: 'gray'," +
        "     buttonBorderWidth: 0," +
        "     buttonBorderRadius: 7," +
        "     trackBackgroundColor: 'none'," +
        "     trackBorderWidth: 1," +
        "     trackBorderRadius: 8," +
        "     trackBorderColor: '#CCC'" +
        " },	" +
        " yAxis: [{	" +
        "     title: {	" +
        "         text: 'Slide Time Duration (min)',	" +
        "         style: {	" +
        "             color: '#333333'	" +
        "         }	" +
        "     },	" +
        "     labels: {	" +
        "         format: '{value} min',	" +
        "         style: {	" +
        "             color: '#333333'	" +
        "         }	" +
        "     },	" +
        "     opposite: true	" +
        " }, {	" +
        "     gridLineWidth: 0,	" +
        "     title: {	" +
        "         text: 'Total Listed Doctors',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[1]	" +
        "         }	" +
        "     },	" +
        "     labels: {	" +
        "         format: '{value}',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[1]	" +
        "         }	" +
        "     }	" +
        " }], " +
        " tooltip: {	" +
        "     enabled: false	" +
        " },	" +
        " credits:	" +
        " {	" +
        "     enabled: false	" +
        " },	" +
        " plotOptions: {	" +
        "     column: {	" +
        "         dataLabels: {	" +
        "             enabled: true,	" +
        "             crop: false,	" +
        "             overflow: 'none'	" +
        "         }	" +
        "     }	" +
        " },	" +
        " legend: {	" +
        "    align: 'center'," +
        "    verticalAlign: 'bottom'," +
        "    x: 0," +
        "    y: 0" +
        " },	" +
        " series: [{	" +
        "     name: 'Slides',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	cursor: 'pointer', events: { click: function(event) { var hdnSlide=document.getElementById('hdnSlide'); hdnSlide.value = event.point.category; window.open('" + uri + "?sf_code=" + ddlFieldForce.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&div_code=" + div_code + "&fDate=" + fDate.ToString() + "&tDate=" + tDate.ToString() + "&Mode=" + mode + "&UsrLike=-1&slide='+hdnSlide.value+'&drSpcCode=" + drSpcCode + "&drCatCode=" + drCatCode + "&drQualCode=" + drQualCode + "&drClassCode=" + drClassCode + "&drTrrCode=" + drTrrCode + "&drPrdBrd=" + drPrdBrd + "', 'ModalPopUp', 'toolbar=no', 'scrollbars=yes', 'location=no', 'statusbar=no', 'menubar=no', 'addressbar=no', 'resizable=yes', 'width=900','height=600', 'left = 0','top=0'); } }," +
        "     data: " + strListedDr.Replace('"', ' ').Trim() + " " +
        " }, {	" +
        "     name: 'Time',	" +
        "     type: 'column',	" +
        "     data: " + strSlideTime.Replace('"', ' ').Trim() + " " +
        " }]	" +
        " });</script>	";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart", script, false);
        #endregion

        #region HighChart_Stack_Bar
        string script1 = "<script type=text/javascript src=https://code.highcharts.com/stock/highstock.js></script><script src=https://code.highcharts.com/modules/series-label.js></script><script src=https://code.highcharts.com/modules/exporting.js></script><script src=https://code.highcharts.com/modules/export-data.js></script><script type=text/javascript>	Highcharts.chart('highcontainer1', {	" +
        "	    chart: {	" +
        "	        type: 'bar', zoomType: 'xy'	" +
        "	    },	" +
        " title: {	" +
        "     text: 'Digital Detailing - Product Slide Analysis'	" +
        " },	" +
        " subtitle: {	" +
        "     text: 'FieldForce: " + ddlFieldForce.SelectedItem.Text + "<br />   From: " + ddlFMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + "\u00a0 \u00a0 To: " + ddlTMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + "\u00a0 \u00a0 \u00a0" + filter + "' " +
        " },	" +
        " tooltip: {	" +
        "     enabled: false	" +
        " },	" +
        " credits:	" +
        " {	" +
        "     enabled: false	" +
        " },	" +
        "	    xAxis: {	" +
        "	        categories: " + strSlide + ", min: 0, max:10	" +
        "	    },	" +
        "	    yAxis: {	" +
        "	        min: 0, minTickInterval: 1, stackLabels: {            enabled: true,style: {fontWeight: 'bold',color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'}},	" +
        "	        title: {	" +
        "	            text: 'Total'	" +
        "	        }	" +
        "	    },	" +
        "	    legend: {	" +
        "	        reversed: true	" +
        "	    },	" +
        "	    plotOptions: {	" +
        "	        series: {	" +
        "	            stacking: 'normal', dataLabels: { enabled: true, color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'black'}	" +
        "	        }	" +
        "	    },	" +
        " scrollbar:	" +
        " {	" +
        "     enabled: true	" +
        " },	" +
        "	    series: [{	" +
        "	        name: 'Likes', cursor: 'pointer', events: { click: function(event) { var hdnSlide=document.getElementById('hdnSlide'); hdnSlide.value = event.point.category; window.open('" + uri + "?sf_code=" + ddlFieldForce.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&div_code=" + div_code + "&fDate=" + fDate.ToString() + "&tDate=" + tDate.ToString() + "&Mode=" + mode + "&UsrLike=1&slide='+hdnSlide.value+'&drSpcCode=" + drSpcCode + "&drCatCode=" + drCatCode + "&drQualCode=" + drQualCode + "&drClassCode=" + drClassCode + "&drTrrCode=" + drTrrCode + "&drPrdBrd=" + drPrdBrd + "', 'ModalPopUp', 'toolbar=no', 'scrollbars=yes', 'location=no', 'statusbar=no', 'menubar=no', 'addressbar=no', 'resizable=yes', 'width=900','height=600', 'left = 0','top=0'); } },	" +
        "	        data: " + strSlideUsrLike.Replace('"', ' ').Trim() + "	" +
        "	    }, {	" +
        "	        name: 'Dislikes', legendColor: Highcharts.getOptions().colors[3], color: Highcharts.getOptions().colors[3], cursor: 'pointer', events: { click: function(event) { var hdnSlide=document.getElementById('hdnSlide'); hdnSlide.value = event.point.category; window.open('" + uri + "?sf_code=" + ddlFieldForce.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&div_code=" + div_code + "&fDate=" + fDate.ToString() + "&tDate=" + tDate.ToString() + "&Mode=" + mode + "&UsrLike=2&slide='+hdnSlide.value+'&drSpcCode=" + drSpcCode + "&drCatCode=" + drCatCode + "&drQualCode=" + drQualCode + "&drClassCode=" + drClassCode + "&drTrrCode=" + drTrrCode + "&drPrdBrd=" + drPrdBrd + "', 'ModalPopUp', 'toolbar=no', 'scrollbars=yes', 'location=no', 'statusbar=no', 'menubar=no', 'addressbar=no', 'resizable=yes', 'width=900','height=600', 'left = 0','top=0'); } },	" +
        "	        data: " + strSlideUsrDisLike.Replace('"', ' ').Trim() + "	" +
        "	    }, {	" +
        "	        name: 'NIL', legendColor: Highcharts.getOptions().colors[2], color: Highcharts.getOptions().colors[2], cursor: 'pointer', events: { click: function(event) { var hdnSlide=document.getElementById('hdnSlide'); hdnSlide.value = event.point.category; window.open('" + uri + "?sf_code=" + ddlFieldForce.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&div_code=" + div_code + "&fDate=" + fDate.ToString() + "&tDate=" + tDate.ToString() + "&Mode=" + mode + "&UsrLike=0&slide='+hdnSlide.value+'&drSpcCode=" + drSpcCode + "&drCatCode=" + drCatCode + "&drQualCode=" + drQualCode + "&drClassCode=" + drClassCode + "&drTrrCode=" + drTrrCode + "&drPrdBrd=" + drPrdBrd + "', 'ModalPopUp', 'toolbar=no', 'scrollbars=yes', 'location=no', 'statusbar=no', 'menubar=no', 'addressbar=no', 'resizable=yes', 'width=900','height=600', 'left = 0','top=0'); } },	" +
        "	        data: " + strSlideUsrNil.Replace('"', ' ').Trim() + "	" +
        "	    }]	" +
        "	});	</script>	";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart1", script1, false);
        #endregion
    }
    #endregion

    #region rowRepeaterP_ItemBound
    protected void rowRepeaterP_ItemBound(object sender, RepeaterItemEventArgs e)
    {

    }
    #endregion

    #region ddlSrch_SelectedIndexChanged
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (search == 8)
        {
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
        }
        else
        {
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            ddlSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillReport();
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
        if (search == 7)
        {
            if (ddlFieldForce.SelectedValue != "0" && ddlFieldForce.SelectedValue != "admin")
            {
                FillProduct(ddlFieldForce.SelectedValue);
            }
            else if (ddlFieldForce.SelectedValue == "0" || ddlFieldForce.SelectedValue == "admin")
            {
                FillProductAdmin(div_code);
            }
        }
    }
    #endregion

    #region Fill_ddlSrch
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(ddlFieldForce.SelectedValue);
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
        dsListedDR = lstDR.FetchSpeciality(ddlFieldForce.SelectedValue);
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
        dsListedDR = lstDR.FetchQualification(ddlFieldForce.SelectedValue);
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
        dsListedDR = lstDR.FetchClass(ddlFieldForce.SelectedValue);
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
        dsListedDR = lstDR.FetchTerritory(ddlFieldForce.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }
    }
    private void FillProduct(string pSf_Code)
    {
        DCR sf = new DCR();
        DataSet dsProduct = new DataSet();
        dsProduct = sf.getAppProd(pSf_Code);

        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataSource = dsProduct;
            ddlSrc2.DataTextField = "name";
            ddlSrc2.DataValueField = "id";
            ddlSrc2.DataBind();
            ddlSrc2.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            ddlSrc2.DataSource = dsProduct;
            ddlSrc2.DataTextField = "name";
            ddlSrc2.DataValueField = "id";
            ddlSrc2.DataBind();
            ddlSrc2.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    private void FillProductAdmin(string div_code)
    {
        DCR sf = new DCR();
        DataSet dsProduct = new DataSet();
        dsProduct = sf.getAppProdAdmin(div_code);

        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataSource = dsProduct;
            ddlSrc2.DataTextField = "name";
            ddlSrc2.DataValueField = "id";
            ddlSrc2.DataBind();
            ddlSrc2.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            ddlSrc2.DataSource = dsProduct;
            ddlSrc2.DataTextField = "name";
            ddlSrc2.DataValueField = "id";
            ddlSrc2.DataBind();
            ddlSrc2.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    #endregion

    #region Click_Events
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0"
            && ddlFieldForce.SelectedValue != ""
            && ddlFMonth.SelectedValue != "0"
            && ddlTMonth.SelectedValue != "0")
        {
            FillReport();
            //cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
            //cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
            //ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
            //ctyear = Convert.ToInt32(ddlTYear.SelectedValue);
        }

        //var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        //nameValues.Set("div_Code", div_code);
        //nameValues.Set("sfcode", ddlFieldForce.SelectedValue);
        //nameValues.Set("sf_type", sf_type);
        //nameValues.Set("cFMnth", cfmonth.ToString());
        //nameValues.Set("cFYear", cfyear.ToString());
        //nameValues.Set("cTMnth", ctmonth.ToString());
        //nameValues.Set("cTYr", ctyear.ToString());
        //string url = Request.Url.AbsolutePath;
        //Response.Redirect(url + "?" + nameValues);
    }

    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (search == 1)
        {
            drSpcCode = "NULL";
            drCatCode = "NULL";
            drQualCode = "NULL";
            drClassCode = "NULL";
            drTrrCode = "NULL";
            drPrdBrd = "NULL";

            filter = " Filter: ALL";
        }
        if (search == 2)
        {
            drSpcCode = ddlSrc2.SelectedValue;
            drCatCode = "NULL";
            drQualCode = "NULL";
            drClassCode = "NULL";
            drTrrCode = "NULL";
            drPrdBrd = "NULL";

            filter = " Filter: Speciality: " + ddlSrc2.SelectedItem.Text;
        }
        if (search == 3)
        {
            drSpcCode = "NULL";
            drCatCode = ddlSrc2.SelectedValue;
            drQualCode = "NULL";
            drClassCode = "NULL";
            drTrrCode = "NULL";
            drPrdBrd = "NULL";

            filter = " Filter: Category: " + ddlSrc2.SelectedItem.Text;
        }
        if (search == 4)
        {
            drSpcCode = "NULL";
            drCatCode = "NULL";
            drQualCode = ddlSrc2.SelectedValue;
            drClassCode = "NULL";
            drTrrCode = "NULL";
            drPrdBrd = "NULL";

            filter = " Filter: Qualification: " + ddlSrc2.SelectedItem.Text;
        }
        if (search == 5)
        {
            drSpcCode = "NULL";
            drCatCode = "NULL";
            drQualCode = "NULL";
            drClassCode = ddlSrc2.SelectedValue;
            drTrrCode = "NULL";
            drPrdBrd = "NULL";

            filter = " Filter: Class: " + ddlSrc2.SelectedItem.Text;
        }
        if (search == 6)
        {
            drSpcCode = "NULL";
            drCatCode = "NULL";
            drQualCode = "NULL";
            drClassCode = "NULL";
            drTrrCode = ddlSrc2.SelectedValue;
            drPrdBrd = "NULL";

            filter = " Filter: Territory: " + ddlSrc2.SelectedItem.Text;
        }
        if (search == 7)
        {
            drSpcCode = "NULL";
            drCatCode = "NULL";
            drQualCode = "NULL";
            drClassCode = "NULL";
            drTrrCode = "NULL";
            drPrdBrd = ddlSrc2.SelectedValue;

            filter = " Filter: Territory: " + ddlSrc2.SelectedItem.Text;
        }

        FillReport();
    }
    #endregion
}