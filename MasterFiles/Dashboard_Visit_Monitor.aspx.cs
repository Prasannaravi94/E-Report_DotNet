using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MasterFiles_Dashboard_Visit_Monitor : System.Web.UI.Page
{

    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cMnth = string.Empty;
    string cYear = string.Empty;
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        //Session["div_code"] = Request.QueryString["div"].ToString();
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();
        }
        catch
        {
            sf_code = Request.QueryString["sfcode"].ToString();
            div_code = Request.QueryString["div_Code"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
            cMnth = Request.QueryString["cMnth"].ToString();
            cYear = Request.QueryString["cYr"].ToString();
        }

        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = cYear;
                }
            }
            ddlMonth.SelectedValue = cMnth;

            FillReport();
        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    #region FillManagers
    private void FillManagers()
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

    #region FillReport
    private void FillReport()
    {
        int cmonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int cyear = Convert.ToInt32(ddlYear.SelectedValue);
        string monthName = new DateTime(cyear, cmonth, 1).ToString("MMM", CultureInfo.InvariantCulture);

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "AdmDash_Coverage_Analysis";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@cMnth", cmonth);
        cmd.Parameters.AddWithValue("@cYrs", cyear);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();

        var sumTotal_Listed_Drs = (Object)null;
        var sumDoctors_Met = (Object)null;
        var sumDoctors_Calls_Seen = (Object)null;
        var sumListed_Drs_Missed = (Object)null;
        var sumNo_Of_Field_Wrk_Days = (Object)null;
        var sumCall_Average = (Object)null;
        var sumCount = (Object)null;
        var sumCoverage_Per = (Object)null;
        double tCall_Average = 0.00;
        double tCoverage_Per = 0.00;

        if (ddlFieldForce.SelectedValue.Contains("MR") || ddlFieldForce.SelectedValue.Contains("MGR"))
        {
            sumTotal_Listed_Drs = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToInt32(s["Total_Listed_Drs"]));
            sumDoctors_Met = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue
                && r.Field<string>("Doctors_Met") != "-")
                .Sum(s => Convert.ToInt32(s["Doctors_Met"]));
            sumDoctors_Calls_Seen = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue
                && r.Field<string>("Doctors_Calls_Seen") != "-")
                .Sum(s => Convert.ToInt32(s["Doctors_Calls_Seen"]));
            sumListed_Drs_Missed = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue)
                .Sum(s => Convert.ToInt32(s["Listed_Drs_Missed"]));
            sumNo_Of_Field_Wrk_Days = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue)
                .Sum(s => Convert.ToInt32(s["No_Of_Field_Wrk_Days"]));
            sumCall_Average = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue)
                .Sum(s => Convert.ToDouble(s["Call_Average"]));
            sumCoverage_Per = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("sf_code") == ddlFieldForce.SelectedValue)
                .Sum(s => Convert.ToDouble(s["Coverage_Per"]));

            tCall_Average = Math.Round(Convert.ToDouble(sumCall_Average));
            tCoverage_Per = Math.Round(Convert.ToDouble(sumCoverage_Per));
        }
        else if (ddlFieldForce.SelectedValue.Contains("admin"))
        {
            sumTotal_Listed_Drs = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToInt32(s["Total_Listed_Drs"]));
            sumDoctors_Met = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-"
                && r.Field<string>("Doctors_Met") != "-")
                .Sum(s => Convert.ToInt32(s["Doctors_Met"]));
            sumDoctors_Calls_Seen = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-"
                && r.Field<string>("Doctors_Calls_Seen") != "-")
                .Sum(s => Convert.ToInt32(s["Doctors_Calls_Seen"]));
            sumListed_Drs_Missed = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToInt32(s["Listed_Drs_Missed"]));
            sumNo_Of_Field_Wrk_Days = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToInt32(s["No_Of_Field_Wrk_Days"]));
            sumCall_Average = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToDouble(s["Call_Average"]));
            sumCoverage_Per = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Sum(s => Convert.ToDouble(s["Coverage_Per"]));
            sumCount = dsts.Tables[0].AsEnumerable()
                .Where(r => r.Field<string>("Total_Listed_Drs") != "-")
                .Count();

            tCall_Average = Math.Round(Convert.ToDouble(sumCall_Average) / Convert.ToInt32(sumCount));
            tCoverage_Per = Math.Round(Convert.ToDouble(sumCoverage_Per) / Convert.ToInt32(sumCount));
        }

        hdnTotal_Listed_Drs.Value = sumTotal_Listed_Drs.ToString();
        hdnDoctors_Met.Value = sumDoctors_Met.ToString();
        hdnDoctors_Calls_Seen.Value = sumDoctors_Calls_Seen.ToString();
        hdnListed_Drs_Missed.Value = sumListed_Drs_Missed.ToString();
        hdnNo_Of_Field_Wrk_Days.Value = sumNo_Of_Field_Wrk_Days.ToString();
        hdnCall_Average.Value = sumCall_Average.ToString();
        hdnCoverage_Per.Value = sumCoverage_Per.ToString();

        #region Pie

        //string script = "function donutChart() { " +
        //    " window.donutChart = Morris.Donut({ " +
        //    "     element: 'donut-chart', " +
        //    "     data: [ " +
        //    "       { label: 'Doctors Met', value: " + hdnDoctors_Met.Value + " }, " +
        //    "       { label: 'Doctors Calls Seen', value: " + hdnDoctors_Calls_Seen.Value + " }, " +
        //    "       { label: 'Missed Doctor Calls', value: " + hdnListed_Drs_Missed.Value + " } " +
        //    "     ], " +
        //    "     resize: true, " +
        //    "     redraw: true " +
        //    " }); " +
        //" }";
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart", script, true);

        //dvLD.Attributes.Add("class", "c100 p100 center");
        //lblLD.Text = hdnTotal_Listed_Drs.Value.ToString();

        //dvCAV.Attributes.Add("class", "c100 p" + Convert.ToInt32(tCall_Average) + " center");
        //lblCAV.Text = tCall_Average.ToString("0.##") + "%";

        //dvCOV.Attributes.Add("class", "c100 p" + Convert.ToInt32(tCoverage_Per) + " center");
        //lblCOV.Text = tCoverage_Per.ToString("0.##") + "%"; 
        #endregion

        #region HighChart
        //string script = "<script src=https://code.highcharts.com/highcharts.js></script><script src=https://code.highcharts.com/modules/exporting.js></script><script src=https://code.highcharts.com/modules/export-data.js></script><script type=text/javascript> Highcharts.chart('highcontainer', { " +
        //    " chart: { " +
        //    "     type: 'column' " +
        //    " }, " +
        //    " title: { " +
        //    "     text: 'Coverage Analysis' " +
        //    " }, " +
        //    " subtitle: { " +
        //    "     text: 'FieldForce: " + ddlFieldForce.SelectedItem.Text + " - No.of Field Work Days: " + hdnNo_Of_Field_Wrk_Days.Value + " - Call Average: " + tCall_Average.ToString("0.##") + "  - Coverage (%): " + tCoverage_Per.ToString("0.##") + "' " +
        //    " }, " +
        //    " xAxis: { " +
        //    "     categories: [ " +
        //    "         '" + monthName + " " + ddlYear.SelectedValue + "' " +
        //    "    ], " +
        //    "     crosshair: true " +
        //    " }, " +
        //    " yAxis: { " +
        //    "     min: 0, " +
        //    "     title: { " +
        //    "         text: 'Listed Doctors Count' " +
        //    "     } " +
        //    " }, " +
        //    " tooltip: { " +
        //    "     headerFormat: '<span style=font-size:10px>{point.key}</span><table>', " +
        //    "     pointFormat: '<tr><td style=color:{series.color};padding:0>{series.name}: </td>' + " +
        //    "         '<td style=padding:0><b>{point.y:.1f}</b></td></tr>', " +
        //    "     footerFormat: '</table>', " +
        //    "     shared: true, " +
        //    "     useHTML: true " +
        //    " }, " +
        //    " plotOptions: { " +
        //    "     column: { " +
        //    "         pointPadding: 0.2, " +
        //    "         borderWidth: 0 " +
        //    "     } " +
        //    " }, " +
        //    " credits: { " +
        //    "     enabled: false " +
        //    " }, " +
        //    " series: [{ " +
        //    "     name: 'Total Listed Doctors', " +
        //    "     data: [" + hdnTotal_Listed_Drs.Value + "] " +
        //    " }, { " +
        //    "     name: 'Doctors Met', " +
        //    "     data: [" + hdnDoctors_Met.Value + "] " +
        //    " }, { " +
        //    "     name: 'Doctors Seen', " +
        //    "     data: [" + hdnDoctors_Calls_Seen.Value + "] " +
        //    " }, { " +
        //    "      name: 'Missed Doctor Calls', " +
        //    "     data: [" + hdnListed_Drs_Missed.Value + "] " +
        //    " }] " +
        //" });</script>";
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart", script, false);
        #endregion

        #region HighChart_Multi
        string script = "<script src=https://code.highcharts.com/highcharts.js></script><script src=https://code.highcharts.com/modules/series-label.js></script><script src=https://code.highcharts.com/modules/exporting.js></script><script src=https://code.highcharts.com/modules/export-data.js></script><script type=text/javascript> Highcharts.chart('highcontainer', { " +
        "	chart: {	" +
        "     zoomType: 'xy'	" +
        " },	" +
        " title: {	" +
        "     text: 'Visit Monitor'	" +
        " },	" +
        " subtitle: {	" +
        "     text: 'FieldForce: " + ddlFieldForce.SelectedItem.Text + " - No.of Field Work Days: " + hdnNo_Of_Field_Wrk_Days.Value + "' " +
        " },	" +
        " xAxis: [{	" +
        "     categories: ['" + monthName + " " + ddlYear.SelectedValue + "'],	" +
        "     crosshair: true	" +
        " }],	" +
        " yAxis: [{	" +
        "     labels: {	" +
        "         format: '{value}',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[5]	" +
        "         }	" +
        "     },	" +
        "     title: {	" +
        "         text: 'Call Average',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[4]	" +
        "         }	" +
        "     },	" +
        "     opposite: true	" +
        " }, {	" +
        "     gridLineWidth: 0,	" +
        "     title: {	" +
        "         text: 'Listed Doctors Count',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[0]	" +
        "         }	" +
        "     },	" +
        "     labels: {	" +
        "         format: '{value}',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[0]	" +
        "         }	" +
        "     }	" +
        " }, {	" +
        "     gridLineWidth: 0,	" +
        "     title: {	" +
        "         text: 'Coverage (%)',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[1]	" +
        "         }	" +
        "     },	" +
        "     labels: {	" +
        "         format: '{value} %',	" +
        "         style: {	" +
        "             color: Highcharts.getOptions().colors[1]	" +
        "         }	" +
        "     },	" +
        "     opposite: true	" +
        " }],	" +
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
        "     name: 'Total Listed Doctors',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: [" + hdnTotal_Listed_Drs.Value + "]	" +
        " }, {	" +
        "     name: 'Doctors Met',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: [" + hdnDoctors_Met.Value + "]	" +
        " }, {	" +
        "     name: 'Doctors Seen',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: [" + hdnDoctors_Calls_Seen.Value + "]	" +
        " }, {	" +
        "     name: 'Missed Doctor Calls',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: [" + hdnListed_Drs_Missed.Value + "]	" +
        " }, {	" +
        "     name: 'Call Average',	" +
        "     type: 'column',	" +
        "     data: [" + tCall_Average.ToString("0.##") + "]	" +
        " }, {	" +
        "     name: 'Coverage',	" +
        "     type: 'column',	" +
        "     yAxis: 2,	" +
        "     data: [" + tCoverage_Per.ToString("0.##") + "]	" +
        " }]	" +

        " });</script>	";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart", script, false);
        #endregion
    }
    #endregion

    #region GrdFixation
    protected void GrdFixation_DataBound(object sender, EventArgs e)
    {
        if (GrdFixation.Rows.Count > 0)
        {
            GrdFixation.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    #endregion

    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0"
            && ddlFieldForce.SelectedValue != ""
            && ddlMonth.SelectedValue != "0")
        {
            FillReport();
        }
    }
    #endregion
}