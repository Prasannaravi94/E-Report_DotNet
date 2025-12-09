using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MasterFiles_Dashboard_TargetvsSales : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();
        }
        catch
        {
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
            cFMnth = Request.QueryString["cMnth"].ToString();
            cFYear = Request.QueryString["cYr"].ToString();
            cTMnth = Request.QueryString["cMnth"].ToString();
            cTYear = Request.QueryString["cYr"].ToString();
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
                FillMasterList();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillMasterList();
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                Fillteam();
            }

            ddlFieldForce.SelectedValue = sf_code;

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

    #region GetMonths / GetDates
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
    #endregion

    #region FillReport
    private void FillReport()
    {
        cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
        ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

        DateTime fDate = DateTime.ParseExact(cfyear + "-" + cfmonth.ToString("D2") + "-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
        DateTime tDate = DateTime.ParseExact(ctyear + "-" + ctmonth.ToString("D2") + "-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

        Array dates = GetMonths(fDate, tDate);
        string[] strDate = dates.OfType<object>().Select(o => o.ToString()).ToArray();
        string strDates = (new JavaScriptSerializer()).Serialize(strDate);

        string monthName = new DateTime(cfyear, cfmonth, 1).ToString("MMM", CultureInfo.InvariantCulture);

        int months = (Convert.ToInt32(ctyear) - Convert.ToInt32(cFYear)) * 12 + Convert.ToInt32(ctmonth) - Convert.ToInt32(cfmonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(cfmonth);
        int cyear = Convert.ToInt32(cFYear);

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

        SalesForce sf = new SalesForce();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "TargetVsSales_Dash";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);

        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns.RemoveAt(0);

        DataRow lastRow = dsts.Tables[0].Rows[dsts.Tables[0].Rows.Count - 1];
        DataTable dtGraphVal = new DataTable();
        dtGraphVal = dsts.Tables[0].Clone();
        dtGraphVal.ImportRow(lastRow);

        months = (Convert.ToInt32(ctyear) - Convert.ToInt32(cFYear)) * 12 + Convert.ToInt32(ctmonth) - Convert.ToInt32(cfmonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        cmonth = Convert.ToInt32(cfmonth);
        cyear = Convert.ToInt32(cFYear);

        double[] trgtVal = new double[months + 1];
        double[] salVal = new double[months + 1];
        double[] perVal = new double[months + 1];
        int target = 4;
        int sales = 6;
        int per = 8;
        int index = (months + 1) * 6;

        for (int i = 0; i <= months; i++)
        {
            if (i == 0)
            {
                trgtVal[i] = lastRow.Field<double>(lastRow.Table.Columns[target].ToString());
                salVal[i] = lastRow.Field<double>(lastRow.Table.Columns[sales].ToString());

                if (lastRow.Field<double>(lastRow.Table.Columns[target].ToString()) != 0
                    && lastRow.Field<double>(lastRow.Table.Columns[sales].ToString()) != 0)
                {
                    double tPer = (lastRow.Field<double>(lastRow.Table.Columns[sales].ToString())
                       / lastRow.Field<double>(lastRow.Table.Columns[target].ToString())) * 100;

                    if (tPer > 0)
                    {
                        perVal[i] = Math.Round(tPer, 2);
                    }
                    else
                    {
                        perVal[i] = 0;
                    }
                }
                else
                {
                    perVal[i] = 0;
                }
                target = target + 6;
                sales = sales + 6;
                per = per + 6;
            }
            else if (target < index)
            {
                trgtVal[i] = lastRow.Field<double>(lastRow.Table.Columns[target].ToString());
                salVal[i] = lastRow.Field<double>(lastRow.Table.Columns[sales].ToString());

                if (lastRow.Field<double>(lastRow.Table.Columns[target].ToString()) != 0
                    && lastRow.Field<double>(lastRow.Table.Columns[sales].ToString()) != 0)
                {
                    double tPer = (lastRow.Field<double>(lastRow.Table.Columns[sales].ToString())
                       / lastRow.Field<double>(lastRow.Table.Columns[target].ToString())) * 100;

                    if (tPer > 0)
                    {
                        perVal[i] = Math.Round(tPer, 2);
                    }
                    else
                    {
                        perVal[i] = 0;
                    }
                }
                else
                {
                    perVal[i] = 0;
                }
                target = target + 6;
                sales = sales + 6;
                per = per + 6;
            }
        }

        string trgtVales = (new JavaScriptSerializer()).Serialize(trgtVal);
        string salValues = (new JavaScriptSerializer()).Serialize(salVal);
        string perValues = (new JavaScriptSerializer()).Serialize(perVal);

        #region HighChart_Multi
        string script = "<script src=https://code.highcharts.com/highcharts.js></script><script src=https://code.highcharts.com/modules/series-label.js></script><script src=https://code.highcharts.com/modules/exporting.js></script><script src=https://code.highcharts.com/modules/export-data.js></script><script type=text/javascript> Highcharts.chart('highcontainer', { " +
        "	chart: {	" +
        "     zoomType: 'xy'	" +
        " },	" +
        " title: {	" +
        "     text: 'Target vs Sales'	" +
        " },	" +
        " subtitle: {	" +
        "     text: 'FieldForce: " + ddlFieldForce.SelectedItem.Text + "' " +
        " },	" +
        " xAxis: [{	" +
        "     categories:" + strDates + ",	" +
        "     crosshair: true	" +
        " }],	" +
        " yAxis: [{	" +
        "     title: {	" +
        "         text: 'Achieve %',	" +
        "         style: {	" +
        "             color: '#27ae60'	" +
        "         }	" +
        "     },	" +
        "     labels: {	" +
        "         format: '{value} %',	" +
        "         style: {	" +
        "             color: '#27ae60'	" +
        "         }	" +
        "     },	" +
        "     opposite: true	" +
        " }, {	" +
        "     gridLineWidth: 0,	" +
        "     title: {	" +
        "         text: 'Target vs Sales',	" +
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
        " }], " +
        " tooltip: {	" +
        "     enabled: false	" +
        " },	" +
        " credits:	" +
        " {	" +
        "     enabled: false	" +
        " },	" +
        " plotOptions: {	" +
        " series: {" +
        " allowPointSelect: true," +
        " point: {" +
        " events: {" +
        " select: function () {" +
        " var text = this.category + ': ' + this.y," +
        " chart = this.series.chart;" +
        " if (!chart.lbl) {" +
        " chart.lbl = chart.renderer.label(text, 100, 70)" +
        " .attr({" +
        " padding: 10," +
        " r: 5," +
        " fill: Highcharts.getOptions().colors[1]," +
        " zIndex: 5" +
        " })" +
        " .css({" +
        " color: '#FFFFFF'" +
        " })" +
        ".add();" +
        " } else {" +
        " chart.lbl.attr({" +
        " text: text" +
        " });" +
        " } " +
        " }" +
        " }}}," +
            //" spline: {	" +
            //"   dataLabels: {	" +
            //"       enabled: true	" +
            //"       },	" +
            //"       enableMouseTracking: false	" +
            //" }," +

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
        "     name: 'Target',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: " + trgtVales + " " +
        " }, {	" +
        "     name: 'Sales',	" +
        "     type: 'column',	" +
        "     yAxis: 1,	" +
        "     data: " + salValues + " " +
        " }, {	" +
        "     name: 'Achieve %',	" +
        "     type: 'spline',	" +
        "     color: '#27ae60',	" +
        "     data: " + perValues + ",	" +
        "     tooltip: { valueSuffix: ' %'}	" +
        " }]	" +

        " });</script>	";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "chart", script, false);

        SalesForce dv = new SalesForce();
        DataSet dsSF = new DataSet();

        dsSF = dv.Allfieldforce_withvacant_2(div_code, ddlFieldForce.SelectedValue);

        if (dsSF.Tables[0].Rows.Count > 0)
        {
            rowRepeaterP.DataSource = dsSF;
            rowRepeaterP.DataBind();
        }
        else
        {
            rowRepeaterP.DataSource = dsSF;
            rowRepeaterP.DataBind();
        }

        #endregion
    }
    #endregion

    #region rowRepeaterP_ItemBound
    protected void rowRepeaterP_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
        ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

        int months1 = (Convert.ToInt32(ctyear) - Convert.ToInt32(cfyear)) * 12 + Convert.ToInt32(cTMnth) - Convert.ToInt32(cFMnth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth1 = Convert.ToInt32(cfmonth);
        int cyear1 = Convert.ToInt32(cfyear);

        SalesForce sf = new SalesForce();
        DataSet dsMonth = new DataSet();
        dsMonth.Tables.Add(new DataTable());
        dsMonth.Tables[0].Columns.Add("Period", typeof(System.String));

        DataSet dsMonthVal = new DataSet();
        dsMonthVal.Tables.Add(new DataTable());
        dsMonthVal.Tables[0].Columns.Add("Target_Qty", typeof(System.Double));
        dsMonthVal.Tables[0].Columns.Add("Target_Val", typeof(System.Double));
        dsMonthVal.Tables[0].Columns.Add("Sale_Qty", typeof(System.Double));
        dsMonthVal.Tables[0].Columns.Add("Sale_Val", typeof(System.Double));
        dsMonthVal.Tables[0].Columns.Add("Achieve_Qty", typeof(System.Double));
        dsMonthVal.Tables[0].Columns.Add("Achieve_Val", typeof(System.Double));

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months1 >= 0)
        {
            if (cmonth1 == 13)
            {
                cmonth1 = 01; iMn = cmonth1; cyear1 = cyear1 + 1; iYr = cyear1;
            }
            else
            {
                iMn = cmonth1; iYr = cyear1;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months1--; cmonth1++;
        }

        if (e.Item.ItemType == ListItemType.Header)
        {
            months1 = (Convert.ToInt32(ctyear) - Convert.ToInt32(cfyear)) * 12 + Convert.ToInt32(cTMnth) - Convert.ToInt32(cFMnth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth1 = Convert.ToInt32(cfmonth);
            cyear1 = Convert.ToInt32(cfyear);

            if (months1 >= 0)
            {
                for (int j = 1; j <= months1 + 1; j++)
                {
                    string s = sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " " + cyear1;
                    DataRow row = dsMonth.Tables[0].NewRow();
                    dsMonth.Tables[0].Rows.Add(row);
                    dsMonth.Tables[0].Rows[j - 1][0] = s;

                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }
            }

            Repeater headerRepeater1 = e.Item.FindControl("headerRepeater1") as Repeater;
            headerRepeater1.DataSource = dsMonth;
            headerRepeater1.DataBind();

            Repeater headerRepeater2 = e.Item.FindControl("headerRepeater2") as Repeater;
            headerRepeater2.DataSource = dsMonth;
            headerRepeater2.DataBind();

            Repeater headerRepeater3 = e.Item.FindControl("headerRepeater3") as Repeater;
            headerRepeater3.DataSource = dsMonth;
            headerRepeater3.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblsf_Code = e.Item.FindControl("lblsf_Code") as Label;
            string isf_Code = lblsf_Code.Text;

            months1 = (Convert.ToInt32(cTYear) - Convert.ToInt32(cFYear)) * 12 + Convert.ToInt32(cTMnth) - Convert.ToInt32(cFMnth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth1 = Convert.ToInt32(cFMnth);
            cyear1 = Convert.ToInt32(cFYear);

            DB_EReporting db = new DB_EReporting();
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            string sProc_Name = "";
            sProc_Name = "TargetVsSales_Dash";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", isf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);

            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(4);
            dsts.Tables[0].Columns.RemoveAt(3);
            dsts.Tables[0].Columns.RemoveAt(2);
            dsts.Tables[0].Columns.RemoveAt(1);
            dsts.Tables[0].Columns.RemoveAt(0);

            int index = (months1 + 1) * 6;
            int targetQty = 0;
            int targetVal = 1;
            int saleQty = 2;
            int salVal = 3;
            int perQty = 4;
            int perVal = 5;

            for (int i = 0; i <= months1; i++)
            {
                DataRow row = dsMonthVal.Tables[0].NewRow();
                dsMonthVal.Tables[0].Rows.Add(row);

                if (i == 0 && index == dsts.Tables[0].Columns.Count)
                {
                    row["Target_Qty"] = dsts.Tables[0].Rows[0][i + targetQty].ToString();
                    row["Target_Val"] = dsts.Tables[0].Rows[0][i + targetVal].ToString();
                    row["Sale_Qty"] = dsts.Tables[0].Rows[0][i + saleQty].ToString();
                    row["Sale_Val"] = dsts.Tables[0].Rows[0][i + salVal].ToString();

                    if (row.Field<double>(row.Table.Columns["Target_Qty"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Target_Val"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Sale_Qty"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Sale_Val"].ToString()) != 0)
                    {
                        double tPerQty = (row.Field<double>(row.Table.Columns["Sale_Qty"].ToString())
                           / row.Field<double>(row.Table.Columns["Target_Qty"].ToString())) * 100;
                        double tPerVal = (row.Field<double>(row.Table.Columns["Sale_Val"].ToString())
                           / row.Field<double>(row.Table.Columns["Target_Val"].ToString())) * 100;

                        if (tPerQty > 0)
                        {
                            row["Achieve_Qty"] = Math.Round(tPerQty, 2);
                            row["Achieve_Val"] = Math.Round(tPerVal, 2);
                        }
                        else
                        {
                            row["Achieve_Qty"] = 0;
                            row["Achieve_Val"] = 0;
                        }
                    }
                    else
                    {
                        row["Achieve_Qty"] = 0;
                        row["Achieve_Val"] = 0;
                    }

                    //row["Achieve_Qty"] = dsts.Tables[0].Rows[0][i + perQty].ToString();
                    //row["Achieve_Val"] = dsts.Tables[0].Rows[0][i + perVal].ToString();

                    targetQty = targetQty + 5;
                    targetVal = targetVal + 5;
                    saleQty = saleQty + 5;
                    salVal = salVal + 5;
                    perQty = perQty + 5;
                    perVal = perVal + 5;
                }
                else if (targetQty < index && index == dsts.Tables[0].Columns.Count)
                {
                    row["Target_Qty"] = dsts.Tables[0].Rows[0][i + targetQty].ToString();
                    row["Target_Val"] = dsts.Tables[0].Rows[0][i + targetVal].ToString();
                    row["Sale_Qty"] = dsts.Tables[0].Rows[0][i + saleQty].ToString();
                    row["Sale_Val"] = dsts.Tables[0].Rows[0][i + salVal].ToString();

                    if (row.Field<double>(row.Table.Columns["Target_Qty"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Target_Val"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Sale_Qty"].ToString()) != 0
                    && row.Field<double>(row.Table.Columns["Sale_Val"].ToString()) != 0)
                    {
                        double tPerQty = (row.Field<double>(row.Table.Columns["Sale_Qty"].ToString())
                           / row.Field<double>(row.Table.Columns["Target_Qty"].ToString())) * 100;
                        double tPerVal = (row.Field<double>(row.Table.Columns["Sale_Val"].ToString())
                           / row.Field<double>(row.Table.Columns["Target_Val"].ToString())) * 100;

                        if (tPerQty > 0)
                        {
                            row["Achieve_Qty"] = Math.Round(tPerQty, 2);
                            row["Achieve_Val"] = Math.Round(tPerVal, 2);
                        }
                        else
                        {
                            row["Achieve_Qty"] = 0;
                            row["Achieve_Val"] = 0;
                        }
                    }
                    else
                    {
                        row["Achieve_Qty"] = 0;
                        row["Achieve_Val"] = 0;
                    }

                    //row["Achieve_Qty"] = dsts.Tables[0].Rows[0][i + perQty].ToString();
                    //row["Achieve_Val"] = dsts.Tables[0].Rows[0][i + perVal].ToString();

                    targetQty = targetQty + 5;
                    targetVal = targetVal + 5;
                    saleQty = saleQty + 5;
                    salVal = salVal + 5;
                    perQty = perQty + 5;
                    perVal = perVal + 5;
                }
            }

            Repeater columnRepeater = e.Item.FindControl("columnRepeater") as Repeater;
            columnRepeater.DataSource = dsMonthVal;
            columnRepeater.DataBind();
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
            && ddlFMonth.SelectedValue != "0"
            && ddlTMonth.SelectedValue != "0")
        {
            FillReport();
        }

        //var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        //nameValues.Set("div_Code", div_code);
        //nameValues.Set("sfcode", sf_code);
        //nameValues.Set("sf_type", sf_type);
        //nameValues.Set("cFMnth", cfmonth.ToString());
        //nameValues.Set("cFYear", cfyear.ToString());
        //nameValues.Set("cTMnth", ctmonth.ToString());
        //nameValues.Set("cTYr", ctyear.ToString());
        //string url = Request.Url.AbsolutePath;
        //Response.Redirect(url + "?" + nameValues);
    }
    #endregion
}