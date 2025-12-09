using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using Bus_EReport;

public partial class DashBoard_Fw_Analysis_SFE : System.Web.UI.Page
{
    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;

    static string modeName = string.Empty;
    static string FMName = string.Empty;
    static string TMName = string.Empty;
    static string type = string.Empty;
    static string[] strsplit;
    static string des_code = string.Empty;
    static string Cat_code = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    static string strFreq = string.Empty;
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    static DataSet dsdes = new DataSet();
    static DataSet dsCat = new DataSet();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!IsPostBack)
        {
            sSf_Code = Session["sf_code"].ToString();
            sDivCode = Session["div_code"].ToString();

            FillYear();
            FillManagers();

        }
    }
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.Items.Add(k.ToString());
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserListTP_Hierarchy_New(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }
    #region all
    [WebMethod(EnableSession = true)]

    public static string all(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        type = arr[10];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "0")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            if (type == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..

                    "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +

                              "'subcaption' : 'Fieldwork Days'," +
                            "'xAxisname': 'Month'," +
                            "'yAxisName': ''," +
                         "'useroundedges': '1'," +

                            "'plotFillAlpha': '80'," +
                              "'labelDisplay': 'rotate'," +
                        "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                            "'baseFontColor': '#333333'," +
                            "'baseFont': 'Helvetica Neue,Arial'," +
                            "'captionFontSize': '14'," +
                            "'subcaptionFontSize': '14'," +
                            "'subcaptionFontBold': '1'," +
                            "'showBorder': '0'," +
                            "'bgColor': '#ffffff'," +
                            "'showShadow': '1'," +
                            "'canvasBgColor': '#ffffff'," +
                            "'canvasBorderAlpha': '0'," +
                            "'divlineAlpha': '100'," +
                            "'divlineColor': '#999999'," +
                            "'divlineThickness': '1'," +
                            "'divLineIsDashed': '1'," +
                            "'divLineDashLen': '1'," +
                            "'divLineGapLen': '1'," +
                            "'usePlotGradientColor': '0'," +
                            "'showplotborder': '0'," +
                                "'showvalues': '1'," +
                            "'valueFontColor': '#000000'," +
                            "'placeValuesInside': '0'," +
                            "'showHoverEffect': '1'," +
                            "'rotateValues': '0'," +
                            "'showXAxisLine': '1'," +
                            "'xAxisLineThickness': '1'," +
                            "'xAxisLineColor': '#999999'," +
                            "'showAlternateHGridColor': '0'," +
                            "'legendBgAlpha': '0'," +
                            "'legendBorderAlpha': '0'," +
                            "'legendShadow': '0'," +
                            "'legendItemFontSize': '10'," +
                               "'formatNumber': '0'," +
                    "'formatNumberScale': '0'," +

                            "'legendItemFontColor': '#666666'" +

                    "},");
            }
            else if (type == "2")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
           "'chart': {" +
                    //   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                     "'subcaption' : 'Fieldworking Days'," +

     "'captionpadding': '20'," +
     "'numberPrefix': ''," +
     "'formatnumberscale': '1'," +

     "'labeldisplay': 'ROTATE'," +
     "'yaxisvaluespadding': '10'," +

     "'slantlabels': '1'," +
     "'animation': '1'," +
  "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
     "'divlinedashlen': '2'," +
     "'divlinedashgap': '4'," +
     "'divlineAlpha': '60'," +
     "'drawCrossLine': '1'," +
     "'crossLineColor': '#0d0d0d'," +
     "'crossLineAlpha': '100'," +
        "'formatNumber': '0'," +
           "'formatNumberScale': '0'," +
            "'showvalues': '0'," +
                   "'drawAnchors': '1'," +
                     "'anchorRadius': '6'," +
                     "'anchorBorderThickness': '2'," +
           "'anchorBorderColor': '#127fcb'," +
           "'anchorSides': '3'," +
           "'anchorBgColor': '#d3f7ff'," +

     "'crossLineAnimation': '1'" +

           "},");
            }
            else if (type == "3")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
             "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                       "'subcaption' : 'Fieldworking Days'," +

               "'linethickness': '2'," +
       "'numberPrefix': ''," +
        "'bgAlpha': '0', " +
            "'borderAlpha': '20'," +
                "'plotBorderAlpha': '10'," +

       "'formatnumberscale': '1'," +
       "'labeldisplay': 'ROTATE'," +
       "'slantlabels': '1'," +
       "'divLineAlpha': '40'," +

       "'animation': '1'," +

       "'drawCrossLine': '1'," +
       "'crossLineColor': '#0d0d0d'," +

       "'tooltipGrayOutColor': '#80bfff'," +
          "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
          "'formatNumber': '0'," +
             "'formatNumberScale': '0'," +
                "'showvalues': '0'," +
                      "'drawAnchors': '1'," +
                       "'anchorRadius': '6'," +
                       "'anchorBorderThickness': '2'," +
             "'anchorBorderColor': '#127fcb'," +
             "'anchorSides': '3'," +
             "'anchorBgColor': '#d3f7ff'," +

       "'theme': 'zune' " +

             "},");
            }
            else if (type == "4")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                  "'chart': {" +

                            "'subcaption' : 'Fieldworking Days'," +
                        "'xAxisname': 'Month'," +
                          "'yAxisName': ''," +
                  "'numberPrefix': ''," +
                  "'paletteColors': '#FF0000,#800000,#FFFF00,#9FB6CD,#6c3483,#A74CAB,#AADD00'," +
                  "'bgColor': '#ffffff'," +
                  "'legendBorderAlpha': '0'," +
                  "'legendBgAlpha': '0'," +
                  "'legendShadow': '0'," +
                  "'placevaluesInside': '1'," +
                  "'valueFontColor': '#ffffff'," +
                  "'alignCaptionWithCanvas': '1'," +
                  "'showHoverEffect':'1'," +
                  "'canvasBgColor': '#ffffff'," +
                  "'captionFontSize': '14'," +
                  "'subcaptionFontSize': '14'," +
                  "'subcaptionFontBold': '0', " +
                  "'divlineColor': '#999999'," +
                  "'divLineIsDashed': '1'," +
                  "'divLineDashLen': '1'," +
                  "'divLineGapLen': '1'," +
                  "'showAlternateHGridColor': '0'," +
                  "'toolTipColor': '#ffffff'," +
                  "'toolTipBorderThickness': '0'," +
                  "'toolTipBgColor': '#000000'," +
                  "'toolTipBgAlpha': '80'," +
                  "'toolTipBorderRadius': '2'," +
                  "'formatNumber': '0'," +
                  "'formatNumberScale': '0'," +

                  "'toolTipPadding': '5'" +

                  "},");


            }
            else if (type == "5")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
            "'chart': {" +
                      "'subcaption' : 'Fieldworking Days'," +
                    "'xAxisname': 'Month'," +

                    "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#fe33ff,#0f612c,#6c3483,#FFFF00,#AADD00'," +
                            "'valueFontColor': '#ffffff'," +

    "'useroundedges': '1'," +
    "'showvalues': '1'," +
    "'legendborderalpha': '0'," +
    "'showsum': '0'," +
    "'showalternatehgridcolor': '0'," +
    "'divlineisdashed': '1'," +
    "'showYAxisValues':'0'," +
    "'decimals': '0'," +
    "'showborder': '0'," +

                       "'formatNumber': '0'," +

            "'formatNumberScale': '0'" +

            "},");
            }
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {


                    if (mode == "0")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }

                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "0")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "0")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);
                            if (count != 0)
                            {
                                //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                                decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                                obj[c.ColumnName.ToString()] = obj

    //[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
    [c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2

     + "'" + "}";
                            }
                            else
                            {
                                obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";
                            }
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region CallAvgStack
    [WebMethod(EnableSession = true)]

    public static string CallAvgStack(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        type = arr[10];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "0")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            if (type == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..

                    "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +

                              "'subcaption' : 'Call Average'," +
                            "'xAxisname': 'Month'," +
                            "'yAxisName': ''," +
                         "'useroundedges': '1'," +

                            "'plotFillAlpha': '80'," +
                              "'labelDisplay': 'rotate'," +
                        "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                            "'baseFontColor': '#333333'," +
                            "'baseFont': 'Helvetica Neue,Arial'," +
                            "'captionFontSize': '14'," +
                            "'subcaptionFontSize': '14'," +
                            "'subcaptionFontBold': '1'," +
                            "'showBorder': '0'," +
                            "'bgColor': '#ffffff'," +
                            "'showShadow': '1'," +
                            "'canvasBgColor': '#ffffff'," +
                            "'canvasBorderAlpha': '0'," +
                            "'divlineAlpha': '100'," +
                            "'divlineColor': '#999999'," +
                            "'divlineThickness': '1'," +
                            "'divLineIsDashed': '1'," +
                            "'divLineDashLen': '1'," +
                            "'divLineGapLen': '1'," +
                            "'usePlotGradientColor': '0'," +
                            "'showplotborder': '0'," +
                                "'showvalues': '1'," +
                            "'valueFontColor': '#000000'," +
                            "'placeValuesInside': '0'," +
                            "'showHoverEffect': '1'," +
                            "'rotateValues': '0'," +
                            "'showXAxisLine': '1'," +
                            "'xAxisLineThickness': '1'," +
                            "'xAxisLineColor': '#999999'," +
                            "'showAlternateHGridColor': '0'," +
                            "'legendBgAlpha': '0'," +
                            "'legendBorderAlpha': '0'," +
                            "'legendShadow': '0'," +
                            "'legendItemFontSize': '10'," +
                               "'formatNumber': '0'," +
                    "'formatNumberScale': '0'," +

                            "'legendItemFontColor': '#666666'" +

                    "},");
            }
            else if (type == "2")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
          "'chart': {" +
                    //   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                    "'subcaption' : 'Call Average'," +

    "'captionpadding': '20'," +
    "'numberPrefix': ''," +
    "'formatnumberscale': '1'," +

    "'labeldisplay': 'ROTATE'," +
    "'yaxisvaluespadding': '10'," +

    "'slantlabels': '1'," +
    "'animation': '1'," +
 "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
    "'divlinedashlen': '2'," +
    "'divlinedashgap': '4'," +
    "'divlineAlpha': '60'," +
    "'drawCrossLine': '1'," +
    "'crossLineColor': '#0d0d0d'," +
    "'crossLineAlpha': '100'," +
       "'formatNumber': '0'," +
          "'formatNumberScale': '0'," +
           "'showvalues': '0'," +
                  "'drawAnchors': '1'," +
                    "'anchorRadius': '6'," +
                    "'anchorBorderThickness': '2'," +
          "'anchorBorderColor': '#127fcb'," +
          "'anchorSides': '3'," +
          "'anchorBgColor': '#d3f7ff'," +

    "'crossLineAnimation': '1'" +

          "},");
            }
            else if (type == "3")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
             "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                       "'subcaption' : 'Call Average'," +

               "'linethickness': '2'," +
       "'numberPrefix': ''," +
        "'bgAlpha': '0', " +
            "'borderAlpha': '20'," +
                "'plotBorderAlpha': '10'," +

       "'formatnumberscale': '1'," +
       "'labeldisplay': 'ROTATE'," +
       "'slantlabels': '1'," +
       "'divLineAlpha': '40'," +

       "'animation': '1'," +

       "'drawCrossLine': '1'," +
       "'crossLineColor': '#0d0d0d'," +

       "'tooltipGrayOutColor': '#80bfff'," +
          "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
          "'formatNumber': '0'," +
             "'formatNumberScale': '0'," +
                "'showvalues': '0'," +
                      "'drawAnchors': '1'," +
                       "'anchorRadius': '6'," +
                       "'anchorBorderThickness': '2'," +
             "'anchorBorderColor': '#127fcb'," +
             "'anchorSides': '3'," +
             "'anchorBgColor': '#d3f7ff'," +

       "'theme': 'zune' " +

             "},");
            }
            else if (type == "4")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                 "'chart': {" +

                           "'subcaption' : 'Call Average'," +
                       "'xAxisname': 'Month'," +
                         "'yAxisName': ''," +
                 "'numberPrefix': ''," +
                 "'paletteColors': '#FF0000,#800000,#FFFF00,#9FB6CD,#6c3483,#A74CAB,#AADD00'," +
                 "'bgColor': '#ffffff'," +
                 "'legendBorderAlpha': '0'," +
                 "'legendBgAlpha': '0'," +
                 "'legendShadow': '0'," +
                 "'placevaluesInside': '1'," +
                 "'valueFontColor': '#ffffff'," +
                 "'alignCaptionWithCanvas': '1'," +
                 "'showHoverEffect':'1'," +
                 "'canvasBgColor': '#ffffff'," +
                 "'captionFontSize': '14'," +
                 "'subcaptionFontSize': '14'," +
                 "'subcaptionFontBold': '0', " +
                 "'divlineColor': '#999999'," +
                 "'divLineIsDashed': '1'," +
                 "'divLineDashLen': '1'," +
                 "'divLineGapLen': '1'," +
                 "'showAlternateHGridColor': '0'," +
                 "'toolTipColor': '#ffffff'," +
                 "'toolTipBorderThickness': '0'," +
                 "'toolTipBgColor': '#000000'," +
                 "'toolTipBgAlpha': '80'," +
                 "'toolTipBorderRadius': '2'," +
                 "'formatNumber': '0'," +
                 "'formatNumberScale': '0'," +

                 "'toolTipPadding': '5'" +

                 "},");
            }
            else if (type == "5")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
            "'chart': {" +
                      "'subcaption' : 'Call Average'," +
                    "'xAxisname': 'Month'," +

                    "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#fe33ff,#0f612c,#6c3483,#FFFF00,#AADD00'," +
                            "'valueFontColor': '#ffffff'," +

    "'useroundedges': '1'," +
    "'showvalues': '1'," +
    "'legendborderalpha': '0'," +
    "'showsum': '0'," +
    "'showalternatehgridcolor': '0'," +
    "'divlineisdashed': '1'," +
    "'showYAxisValues':'0'," +
    "'decimals': '0'," +
    "'showborder': '0'," +

                       "'formatNumber': '0'," +

            "'formatNumberScale': '0'" +

            "},");
            }
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {


                    if (mode == "0")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }

                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;


                        obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region MissLine
    [WebMethod(EnableSession = true)]

    public static string MissLine(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        type = arr[10];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");


            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "0")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);



            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            if (type == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..

                    "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +

                              "'subcaption' : 'Missed Calls'," +
                            "'xAxisname': 'Month'," +
                            "'yAxisName': ''," +
                         "'useroundedges': '1'," +

                            "'plotFillAlpha': '80'," +
                              "'labelDisplay': 'rotate'," +
                        "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                            "'baseFontColor': '#333333'," +
                            "'baseFont': 'Helvetica Neue,Arial'," +
                            "'captionFontSize': '14'," +
                            "'subcaptionFontSize': '14'," +
                            "'subcaptionFontBold': '1'," +
                            "'showBorder': '0'," +
                            "'bgColor': '#ffffff'," +
                            "'showShadow': '1'," +
                            "'canvasBgColor': '#ffffff'," +
                            "'canvasBorderAlpha': '0'," +
                            "'divlineAlpha': '100'," +
                            "'divlineColor': '#999999'," +
                            "'divlineThickness': '1'," +
                            "'divLineIsDashed': '1'," +
                            "'divLineDashLen': '1'," +
                            "'divLineGapLen': '1'," +
                            "'usePlotGradientColor': '0'," +
                            "'showplotborder': '0'," +
                                "'showvalues': '1'," +
                            "'valueFontColor': '#000000'," +
                            "'placeValuesInside': '0'," +
                            "'showHoverEffect': '1'," +
                            "'rotateValues': '0'," +
                            "'showXAxisLine': '1'," +
                            "'xAxisLineThickness': '1'," +
                            "'xAxisLineColor': '#999999'," +
                            "'showAlternateHGridColor': '0'," +
                            "'legendBgAlpha': '0'," +
                            "'legendBorderAlpha': '0'," +
                            "'legendShadow': '0'," +
                            "'legendItemFontSize': '10'," +
                               "'formatNumber': '0'," +
                    "'formatNumberScale': '0'," +

                            "'legendItemFontColor': '#666666'" +

                    "},");
            }
            else if (type == "2")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
              "'chart': {" +
                    //   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                        "'subcaption' : 'Missed Calls'," +

        "'captionpadding': '20'," +
        "'numberPrefix': ''," +
        "'formatnumberscale': '1'," +

        "'labeldisplay': 'ROTATE'," +
        "'yaxisvaluespadding': '10'," +

        "'slantlabels': '1'," +
        "'animation': '1'," +
     "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
        "'divlinedashlen': '2'," +
        "'divlinedashgap': '4'," +
        "'divlineAlpha': '60'," +
        "'drawCrossLine': '1'," +
        "'crossLineColor': '#0d0d0d'," +
        "'crossLineAlpha': '100'," +
           "'formatNumber': '0'," +
              "'formatNumberScale': '0'," +
               "'showvalues': '0'," +
                      "'drawAnchors': '1'," +
                        "'anchorRadius': '6'," +
                        "'anchorBorderThickness': '2'," +
              "'anchorBorderColor': '#127fcb'," +
              "'anchorSides': '3'," +
              "'anchorBgColor': '#d3f7ff'," +

        "'crossLineAnimation': '1'" +

              "},");
            }
            else if (type == "3")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
             "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                       "'subcaption' : 'Missed Calls'," +

               "'linethickness': '2'," +
       "'numberPrefix': ''," +
        "'bgAlpha': '0', " +
            "'borderAlpha': '20'," +
                "'plotBorderAlpha': '10'," +

       "'formatnumberscale': '1'," +
       "'labeldisplay': 'ROTATE'," +
       "'slantlabels': '1'," +
       "'divLineAlpha': '40'," +

       "'animation': '1'," +

       "'drawCrossLine': '1'," +
       "'crossLineColor': '#0d0d0d'," +

       "'tooltipGrayOutColor': '#80bfff'," +
          "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
          "'formatNumber': '0'," +
             "'formatNumberScale': '0'," +
                "'showvalues': '0'," +
                      "'drawAnchors': '1'," +
                       "'anchorRadius': '6'," +
                       "'anchorBorderThickness': '2'," +
             "'anchorBorderColor': '#127fcb'," +
             "'anchorSides': '3'," +
             "'anchorBgColor': '#d3f7ff'," +

       "'theme': 'zune' " +

             "},");
            }
            else if (type == "4")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                 "'chart': {" +

                           "'subcaption' : 'Missed Calls'," +
                       "'xAxisname': 'Month'," +
                         "'yAxisName': ''," +
                 "'numberPrefix': ''," +
                 "'paletteColors': '#FF0000,#800000,#FFFF00,#9FB6CD,#6c3483,#A74CAB,#AADD00'," +
                 "'bgColor': '#ffffff'," +
                 "'legendBorderAlpha': '0'," +
                 "'legendBgAlpha': '0'," +
                 "'legendShadow': '0'," +
                 "'placevaluesInside': '1'," +
                 "'valueFontColor': '#ffffff'," +
                 "'alignCaptionWithCanvas': '1'," +
                 "'showHoverEffect':'1'," +
                 "'canvasBgColor': '#ffffff'," +
                 "'captionFontSize': '14'," +
                 "'subcaptionFontSize': '14'," +
                 "'subcaptionFontBold': '0', " +
                 "'divlineColor': '#999999'," +
                 "'divLineIsDashed': '1'," +
                 "'divLineDashLen': '1'," +
                 "'divLineGapLen': '1'," +
                 "'showAlternateHGridColor': '0'," +
                 "'toolTipColor': '#ffffff'," +
                 "'toolTipBorderThickness': '0'," +
                 "'toolTipBgColor': '#000000'," +
                 "'toolTipBgAlpha': '80'," +
                 "'toolTipBorderRadius': '2'," +
                 "'formatNumber': '0'," +
                 "'formatNumberScale': '0'," +

                 "'toolTipPadding': '5'" +

                 "},");
            }
            else if (type == "5")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
            "'chart': {" +
                      "'subcaption' : 'Missed Calls'," +
                    "'xAxisname': 'Month'," +

                    "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#fe33ff,#0f612c,#6c3483,#FFFF00,#AADD00'," +
                            "'valueFontColor': '#ffffff'," +

    "'useroundedges': '1'," +
    "'showvalues': '1'," +
    "'legendborderalpha': '0'," +
    "'showsum': '0'," +
    "'showalternatehgridcolor': '0'," +
    "'divlineisdashed': '1'," +
    "'showYAxisValues':'0'," +
    "'decimals': '0'," +
    "'showborder': '0'," +

                       "'formatNumber': '0'," +

            "'formatNumberScale': '0'" +

            "},");
            }
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    if (mode == "0")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";



                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region CovArea
    [WebMethod(EnableSession = true)]

    public static string CovArea(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        type = arr[10];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");


            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "0")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            if (type == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..

                    "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +

                              "'subcaption' : 'Coverage'," +
                            "'xAxisname': 'Month'," +
                            "'yAxisName': ''," +
                         "'useroundedges': '1'," +

                            "'plotFillAlpha': '80'," +
                              "'labelDisplay': 'rotate'," +
                        "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                            "'baseFontColor': '#333333'," +
                            "'baseFont': 'Helvetica Neue,Arial'," +
                            "'captionFontSize': '14'," +
                            "'subcaptionFontSize': '14'," +
                            "'subcaptionFontBold': '1'," +
                            "'showBorder': '0'," +
                            "'bgColor': '#ffffff'," +
                            "'showShadow': '1'," +
                            "'canvasBgColor': '#ffffff'," +
                            "'canvasBorderAlpha': '0'," +
                            "'divlineAlpha': '100'," +
                            "'divlineColor': '#999999'," +
                            "'divlineThickness': '1'," +
                            "'divLineIsDashed': '1'," +
                            "'divLineDashLen': '1'," +
                            "'divLineGapLen': '1'," +
                            "'usePlotGradientColor': '0'," +
                            "'showplotborder': '0'," +
                                "'showvalues': '1'," +
                            "'valueFontColor': '#000000'," +
                            "'placeValuesInside': '0'," +
                            "'showHoverEffect': '1'," +
                            "'rotateValues': '0'," +
                            "'showXAxisLine': '1'," +
                            "'xAxisLineThickness': '1'," +
                            "'xAxisLineColor': '#999999'," +
                            "'showAlternateHGridColor': '0'," +
                            "'legendBgAlpha': '0'," +
                            "'legendBorderAlpha': '0'," +
                            "'legendShadow': '0'," +
                            "'legendItemFontSize': '10'," +
                               "'formatNumber': '0'," +
                    "'formatNumberScale': '0'," +

                            "'legendItemFontColor': '#666666'" +

                    "},");
            }
            else if (type == "2")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
               "'chart': {" +
                    //   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                         "'subcaption' : 'Coverage'," +

         "'captionpadding': '20'," +
         "'numberPrefix': ''," +
         "'formatnumberscale': '1'," +

         "'labeldisplay': 'ROTATE'," +
         "'yaxisvaluespadding': '10'," +

         "'slantlabels': '1'," +
         "'animation': '1'," +
      "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
         "'divlinedashlen': '2'," +
         "'divlinedashgap': '4'," +
         "'divlineAlpha': '60'," +
         "'drawCrossLine': '1'," +
         "'crossLineColor': '#0d0d0d'," +
         "'crossLineAlpha': '100'," +
            "'formatNumber': '0'," +
               "'formatNumberScale': '0'," +
                "'showvalues': '0'," +
                       "'drawAnchors': '1'," +
                         "'anchorRadius': '6'," +
                         "'anchorBorderThickness': '2'," +
               "'anchorBorderColor': '#127fcb'," +
               "'anchorSides': '3'," +
               "'anchorBgColor': '#d3f7ff'," +

         "'crossLineAnimation': '1'" +

               "},");
            }
            else if (type == "3")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
             "'chart': {" +
                    //  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                       "'subcaption' : 'Coverage'," +

               "'linethickness': '2'," +
       "'numberPrefix': ''," +
        "'bgAlpha': '0', " +
            "'borderAlpha': '20'," +
                "'plotBorderAlpha': '10'," +

       "'formatnumberscale': '1'," +
       "'labeldisplay': 'ROTATE'," +
       "'slantlabels': '1'," +
       "'divLineAlpha': '40'," +

       "'animation': '1'," +

       "'drawCrossLine': '1'," +
       "'crossLineColor': '#0d0d0d'," +

       "'tooltipGrayOutColor': '#80bfff'," +
          "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
          "'formatNumber': '0'," +
             "'formatNumberScale': '0'," +
                "'showvalues': '0'," +
                      "'drawAnchors': '1'," +
                       "'anchorRadius': '6'," +
                       "'anchorBorderThickness': '2'," +
             "'anchorBorderColor': '#127fcb'," +
             "'anchorSides': '3'," +
             "'anchorBgColor': '#d3f7ff'," +

       "'theme': 'zune' " +

             "},");
            }
            else if (type == "4")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                           "'chart': {" +

                                     "'subcaption' : 'Coverage'," +
                                 "'xAxisname': 'Month'," +
                                   "'yAxisName': ''," +
                           "'numberPrefix': ''," +
                           "'paletteColors': '#FF0000,#800000,#FFFF00,#9FB6CD,#6c3483,#A74CAB,#AADD00'," +
                           "'bgColor': '#ffffff'," +
                           "'legendBorderAlpha': '0'," +
                           "'legendBgAlpha': '0'," +
                           "'legendShadow': '0'," +
                           "'placevaluesInside': '1'," +
                           "'valueFontColor': '#ffffff'," +
                           "'alignCaptionWithCanvas': '1'," +
                           "'showHoverEffect':'1'," +
                           "'canvasBgColor': '#ffffff'," +
                           "'captionFontSize': '14'," +
                           "'subcaptionFontSize': '14'," +
                           "'subcaptionFontBold': '0', " +
                           "'divlineColor': '#999999'," +
                           "'divLineIsDashed': '1'," +
                           "'divLineDashLen': '1'," +
                           "'divLineGapLen': '1'," +
                           "'showAlternateHGridColor': '0'," +
                           "'toolTipColor': '#ffffff'," +
                           "'toolTipBorderThickness': '0'," +
                           "'toolTipBgColor': '#000000'," +
                           "'toolTipBgAlpha': '80'," +
                           "'toolTipBorderRadius': '2'," +
                           "'formatNumber': '0'," +
                           "'formatNumberScale': '0'," +

                           "'toolTipPadding': '5'" +

                           "},");
            }
            else if (type == "5")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
            "'chart': {" +
                      "'subcaption' : 'Coverage'," +
                    "'xAxisname': 'Month'," +

                    "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#fe33ff,#0f612c,#6c3483,#FFFF00,#AADD00'," +
                            "'valueFontColor': '#ffffff'," +

    "'useroundedges': '1'," +
    "'showvalues': '1'," +
    "'legendborderalpha': '0'," +
    "'showsum': '0'," +
    "'showalternatehgridcolor': '0'," +
    "'divlineisdashed': '1'," +
    "'showYAxisValues':'0'," +
    "'decimals': '0'," +
    "'showborder': '0'," +

                       "'formatNumber': '0'," +

            "'formatNumberScale': '0'" +

            "},");
            }
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    if (mode == "0")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);

                SalesForce sf1 = new SalesForce();
                DataTable dtnew = new DataTable();

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;

                        //   int count = rows.Count<DataRow>();
                        // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                        //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);

                        obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";



                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion

    #region MsField
    [WebMethod(EnableSession = true)]

    public static string MsField(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1" || mode == "0")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                     "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                        "'xAxisname': 'Month'," +
                        "'yAxisName': ''," +
                     "'useroundedges': '1'," +
                        "'plotFillAlpha': '80'," +
                          "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                        "'baseFontColor': '#333333'," +
                        "'baseFont': 'Helvetica Neue,Arial'," +
                        "'captionFontSize': '14'," +
                        "'subcaptionFontSize': '14'," +
                        "'subcaptionFontBold': '1'," +
                        "'showBorder': '0'," +
                        "'bgColor': '#ffffff'," +
                        "'showShadow': '1'," +
                        "'canvasBgColor': '#ffffff'," +
                        "'canvasBorderAlpha': '0'," +
                        "'divlineAlpha': '100'," +
                        "'divlineColor': '#999999'," +
                        "'divlineThickness': '1'," +
                        "'divLineIsDashed': '1'," +
                        "'divLineDashLen': '1'," +
                        "'divLineGapLen': '1'," +
                        "'usePlotGradientColor': '0'," +
                        "'showplotborder': '0'," +
                            "'showvalues': '1'," +
                        "'valueFontColor': '#000000'," +
                        "'placeValuesInside': '0'," +
                        "'showHoverEffect': '1'," +
                        "'rotateValues': '0'," +
                        "'showXAxisLine': '1'," +
                        "'xAxisLineThickness': '1'," +
                        "'xAxisLineColor': '#999999'," +
                        "'showAlternateHGridColor': '0'," +
                        "'legendBgAlpha': '0'," +
                        "'legendBorderAlpha': '0'," +
                        "'legendShadow': '0'," +
                        "'legendItemFontSize': '10'," +
                           "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                      "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

                        "'legendItemFontColor': '#666666'" +
                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {


                    if (mode == "0")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "0")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "0")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2

 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region Multi
    [WebMethod(EnableSession = true)]

    public static string Input(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            else if (mode == "5")
            {
                sProc_Name = "Freq_Cat_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            if (mode != "5")
            {
                dt.Columns.RemoveAt(0);
                strFreq = "Days";
            }
            else
            {
                strFreq = "Percentage";
            }
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                     "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                        "'xAxisname': 'Month'," +

                        "'yAxisName': '" + strFreq + "'," +

                     "'useroundedges': '1'," +
                        "'plotFillAlpha': '80'," +
                          "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                        "'baseFontColor': '#333333'," +
                        "'baseFont': 'Helvetica Neue,Arial'," +
                        "'captionFontSize': '14'," +
                        "'subcaptionFontSize': '14'," +
                        "'subcaptionFontBold': '1'," +
                        "'showBorder': '0'," +
                        "'bgColor': '#ffffff'," +
                        "'showShadow': '1'," +
                        "'canvasBgColor': '#ffffff'," +
                        "'canvasBorderAlpha': '0'," +
                        "'divlineAlpha': '100'," +
                        "'divlineColor': '#999999'," +
                        "'divlineThickness': '1'," +
                        "'divLineIsDashed': '1'," +
                        "'divLineDashLen': '1'," +
                        "'divLineGapLen': '1'," +
                        "'usePlotGradientColor': '0'," +
                        "'showplotborder': '0'," +
                            "'showvalues': '1'," +
                        "'valueFontColor': '#000000'," +
                        "'placeValuesInside': '0'," +
                        "'showHoverEffect': '1'," +
                        "'rotateValues': '0'," +
                        "'showXAxisLine': '1'," +
                        "'xAxisLineThickness': '1'," +
                        "'xAxisLineColor': '#999999'," +
                        "'showAlternateHGridColor': '0'," +
                        "'legendBgAlpha': '0'," +
                        "'legendBorderAlpha': '0'," +
                        "'legendShadow': '0'," +
                        "'legendItemFontSize': '10'," +
                           "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                      "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

                        "'legendItemFontColor': '#666666'" +
                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");
            Doctor dc = new Doctor();
            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {


                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                    else if (mode == "5")
                    {
                        Cat_code = c.ColumnName.Split('_').First();
                        dsCat = dc.getDocCat_Visit_Name(div_code, Cat_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsCat.Tables[0].Rows[0]["Doc_Cat_SName"] + "'," +
                                "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2


 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region Bar
    [WebMethod(EnableSession = true)]

    public static string Bar(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd;

            cmd = new SqlCommand("SecSale_Analysis_Stk_Graph", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            dt.Columns["Prod_code"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["1_0_ABT"].ColumnName = "Value";
            dt.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    #endregion
    #region Area
    [WebMethod(EnableSession = true)]

    public static string Area(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");


            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            else if (mode == "5")
            {
                sProc_Name = "Freq_Cat_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            if (mode != "5")
            {
                dt.Columns.RemoveAt(0);
            }
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +

          "'captionpadding': '20'," +
          "'numberPrefix': ''," +
          "'formatnumberscale': '1'," +

          "'labeldisplay': 'ROTATE'," +
          "'yaxisvaluespadding': '10'," +

          "'slantlabels': '1'," +
          "'animation': '1'," +
       "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
          "'divlinedashlen': '2'," +
          "'divlinedashgap': '4'," +
          "'divlineAlpha': '60'," +
          "'drawCrossLine': '1'," +
          "'crossLineColor': '#0d0d0d'," +
          "'crossLineAlpha': '100'," +
             "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                 "'showvalues': '0'," +
                        "'drawAnchors': '1'," +
                          "'anchorRadius': '6'," +
                          "'anchorBorderThickness': '2'," +
                "'anchorBorderColor': '#127fcb'," +
                "'anchorSides': '3'," +
                "'anchorBgColor': '#d3f7ff'," +
                     "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
          "'crossLineAnimation': '1'" +

                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            Doctor dc = new Doctor();
            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                    else if (mode == "5")
                    {
                        Cat_code = c.ColumnName.Split('_').First();
                        dsCat = dc.getDocCat_Visit_Name(div_code, Cat_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsCat.Tables[0].Rows[0]["Doc_Cat_SName"] + "'," +
                                "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);

                SalesForce sf1 = new SalesForce();
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;

                        //   int count = rows.Count<DataRow>();
                        // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                        //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2


 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region Line
    [WebMethod(EnableSession = true)]

    public static string Line(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");


            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            else if (mode == "5")
            {
                sProc_Name = "Freq_Cat_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);



            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            if (mode != "5")
            {
                dt.Columns.RemoveAt(0);
            }
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                //     "'captionFontSize': '14'," +
                //"'subcaptionFontSize': '14'," +
                //"'subcaptionFontBold': '0',"+
                //"'paletteColors': '#0075c2,#1aaf5d'," +
                //"'bgcolor': '#ffffff'," +
                //"'showBorder': '0'," +
                //"'showShadow': '0'," +
                //"'showCanvasBorder': '0'," +
                //"'usePlotGradientColor': '0'," +
                //"'legendBorderAlpha': '0'," +
                //"'legendShadow': '0'," +
                //"'showAxisLines': '0'," +
                //"'showAlternateHGridColor': '0'," +
                //"'divlineThickness': '1'," +
                //"'divLineIsDashed': '1'," +
                //"'divLineDashLen': '1'," +
                //"'divLineGapLen': '1'," +
                //"'xAxisName': 'Month'," +
                //"'showValues': '0' " +
                //----------
                  "'linethickness': '2'," +
          "'numberPrefix': ''," +
           "'bgAlpha': '0', " +
               "'borderAlpha': '20'," +
                   "'plotBorderAlpha': '10'," +

          "'formatnumberscale': '1'," +
          "'labeldisplay': 'ROTATE'," +
          "'slantlabels': '1'," +
          "'divLineAlpha': '40'," +

          "'animation': '1'," +

          "'drawCrossLine': '1'," +
          "'crossLineColor': '#0d0d0d'," +

          "'tooltipGrayOutColor': '#80bfff'," +
             "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
             "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                   "'showvalues': '0'," +
                         "'drawAnchors': '1'," +
                          "'anchorRadius': '6'," +
                          "'anchorBorderThickness': '2'," +
                "'anchorBorderColor': '#127fcb'," +
                "'anchorSides': '3'," +
                "'anchorBgColor': '#d3f7ff'," +
                      "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
          "'theme': 'zune' " +

                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            Doctor dc = new Doctor();
            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                    else if (mode == "5")
                    {
                        Cat_code = c.ColumnName.Split('_').First();
                        dsCat = dc.getDocCat_Visit_Name(div_code, Cat_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsCat.Tables[0].Rows[0]["Doc_Cat_SName"] + "'," +
                                "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;

                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)

[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2
 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion

    #region MBar
    [WebMethod(EnableSession = true)]

    public static string MBar(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            //SqlCommand cmd;

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            else if (mode == "5")
            {
                sProc_Name = "Freq_Cat_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);



            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            if (mode != "5")
            {
                dt.Columns.RemoveAt(0);
            }
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                   "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                      "'xAxisname': 'Month'," +
                        "'yAxisName': ''," +
                "'numberPrefix': ''," +
                "'paletteColors': '#FF0000,#800000,#FFFF00,#9FB6CD,#6c3483,#A74CAB,#AADD00'," +
                "'bgColor': '#ffffff'," +
                "'legendBorderAlpha': '0'," +
                "'legendBgAlpha': '0'," +
                "'legendShadow': '0'," +
                "'placevaluesInside': '1'," +
                "'valueFontColor': '#ffffff'," +
                "'alignCaptionWithCanvas': '1'," +
                "'showHoverEffect':'1'," +
                "'canvasBgColor': '#ffffff'," +
                "'captionFontSize': '14'," +
                "'subcaptionFontSize': '14'," +
                "'subcaptionFontBold': '0', " +
                "'divlineColor': '#999999'," +
                "'divLineIsDashed': '1'," +
                "'divLineDashLen': '1'," +
                "'divLineGapLen': '1'," +
                "'showAlternateHGridColor': '0'," +
                "'toolTipColor': '#ffffff'," +
                "'toolTipBorderThickness': '0'," +
                "'toolTipBgColor': '#000000'," +
                "'toolTipBgAlpha': '80'," +
                "'toolTipBorderRadius': '2'," +
                "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                      "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
                "'toolTipPadding': '5'" +

                "},");


            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");
            Doctor dc = new Doctor();
            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                    else if (mode == "5")
                    {
                        Cat_code = c.ColumnName.Split('_').First();
                        dsCat = dc.getDocCat_Visit_Name(div_code, Cat_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsCat.Tables[0].Rows[0]["Doc_Cat_SName"] + "'," +
                                "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);


                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2

 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion

    #region Drag
    [WebMethod(EnableSession = true)]

    public static string Drag(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd;

            SalesForce sf = new SalesForce();
            cmd = new SqlCommand("Desigwise_FW_Graph", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +

                       "'yAxisname': ''," +
                         "'captionFontSize': '14'," +
                "'subcaptionFontSize': '14', " +
                "'subcaptionFontBold': '0'," +

               "'showValues': '0'," +
                     "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
                "'xaxisname': 'Month'" +

                //"'usePlotGradientColor': '0'," +
                //"'bgColor' : '#ffffff'," +
                //"'palettecolors': '#6baa01, #d35400'," +
                //"'showBorder' : '0', " +
                //"'showPlotBorder': '0'," +
                //"'showValues': '0'," +                 
                //"'showShadow' : '0'," +
                //"'showAlternateHGridColor' : '0'," +
                //"'showCanvasBorder': '0'," +
                //"'showXAxisLine': '1'," +

                //"'drawverticaljoints': '1'," +
                //"'useforwardsteps': '0'," +
                //"'xAxisLineThickness': '1'," +
                //"'xAxisLineColor': '#999999'," +
                //"'canvasBgColor' : '#ffffff'," +
                //"'divlineAlpha' : '100'," +
                //"'divlineColor' : '#999999',"+
                //"'divlineThickness' : '1'," +
                //"'divLineIsDashed' : '1'," +
                //"'divLineDashLen' : '1'," +
                //"'divLineGapLen' : '1'," +
                //"'legendBorderAlpha': '0', " +
                //"'legendShadow': '0'," +
                //"'toolTipColor': '#ffffff'," +
                //"'toolTipBorderThickness': '0'," +
                //"'toolTipBgColor': '#000000'," +
                //"'toolTipBgAlpha': '80'," +
                //"'toolTipBorderRadius': '2'," +
                //"'toolTipPadding': '5'" +


                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    des_code = c.ColumnName.Split('_').First();

                    dsdes = des.getDesig_graph(des_code, div_code);



                    obj.Add(c.ColumnName.ToString(), "{" +
                        // dataset level attributes

                            "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                            "'data': [");
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);

                SalesForce sf1 = new SalesForce();
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con1 = new SqlConnection(strConn);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dtnew = new DataTable();
                da1.Fill(dtnew);

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;

                        int count = rows.Count<DataRow>();
                        // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                        //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                        decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                        // cnt = cnt / count;



                        obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)

+ "'" + "}";
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion

    #region Stacked
    [WebMethod(EnableSession = true)]

    public static string Stacked(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            else if (mode == "5")
            {
                sProc_Name = "Freq_Cat_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            if (mode != "5")
            {
                dt.Columns.RemoveAt(0);
            }
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                  "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                        "'xAxisname': 'Month'," +

                        "'labelDisplay': 'rotate'," +
                        "'palettecolors': '#FF0000,#800000,#fe33ff,#0f612c,#6c3483,#FFFF00,#AADD00'," +
                                "'valueFontColor': '#ffffff'," +

        "'useroundedges': '1'," +
        "'showvalues': '1'," +
        "'legendborderalpha': '0'," +
        "'showsum': '0'," +
        "'showalternatehgridcolor': '0'," +
        "'divlineisdashed': '1'," +
        "'showYAxisValues':'0'," +
        "'decimals': '0'," +
        "'showborder': '0'," +

                           "'formatNumber': '0'," +
                                 "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
                "'formatNumberScale': '0'" +

                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");
            Doctor dc = new Doctor();
            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {


                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'seriesname': 'MR'," +
                                  "'data': [");

                    }
                    else if (mode == "5")
                    {
                        Cat_code = c.ColumnName.Split('_').First();
                        dsCat = dc.getDocCat_Visit_Name(div_code, Cat_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsCat.Tables[0].Rows[0]["Doc_Cat_SName"] + "'," +
                                "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)
[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2

 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";


                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName != "R_BSN" && c.ColumnName != "mnth")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
    #region Pie
    [WebMethod(EnableSession = true)]

    public static string Pie(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "Desigwise_CallAVG_Graph";

            }
            else if (mode == "3")
            {
                sProc_Name = "Desigwise_Cov_Graph";

            }
            else if (mode == "4")
            {
                sProc_Name = "Desigwise_Miss_Graph";

            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                     "'caption' : 'Fieldworking Days'," +
                         "'captionFontSize': '14'," +
                "'subcaptionFontSize': '14'," +
                "'baseFontColor' : '#333333'," +
                "'baseFont' : 'Helvetica Neue,Arial',    " +
                "'basefontsize': '9'," +
                "'subcaptionFontBold': '0'," +
                "'bgColor' : '#ffffff'," +
                "'canvasBgColor' : '#ffffff'," +
                "'showBorder' : '0'," +
                "'showShadow' : '0'," +
                "'showCanvasBorder': '0'," +
                "'pieFillAlpha': '60'," +
                "'pieBorderThickness': '2'," +
                "'hoverFillColor': '#cccccc'," +
                "'pieBorderColor': '#ffffff', " +
                "'useHoverColor': '1'," +
                "'showValuesInTooltip': '1'," +
                "'showPercentInTooltip': '0'," +

                      "'plotTooltext': 'label, value, percentValue'," +

                           "'formatNumber': '0'," +
                                 "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'exportFileName' : 'DashBoard'," +
                "'formatNumberScale': '0'" +

                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'category': [" +
                "{" +


                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH")
                {


                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'label': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }
                    else if (mode == "3" || mode == "4")
                    {
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                  "'label': 'MR'," +
                                  "'data': [");

                    }
                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH")
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");
                    currentYear.Append(",");
                    previousYear.Append(",");
                    upcomingYear.Append(",");
                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH")
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)

[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2
 + "'" + "}";
                        }
                        else
                        {
                            obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";
                        }
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }
            currentYear.Append("]" +
                    "},");
            previousYear.Append("]" +
                    "},");
            upcomingYear.Append("]" +
                    "}");



            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion


    //Single Monthwise
    [WebMethod(EnableSession = true)]

    public static string SingleFW(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "FW_Single_Graph";
            }
            else if (mode == "2")
            {
                sProc_Name = "CallAvg_Single_Graph";
            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);

            con.Close();
            dt.Columns["sf_name"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["1_TTL"].ColumnName = "Value";

            dt.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    [WebMethod]
    public static string Freq(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "FW_Single_Graph_Freq";
            }
            else if (mode == "2")
            {
                sProc_Name = "CallAvg_Single_Graph_Norms";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);

            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder drmet = new StringBuilder();
            StringBuilder chmet = new StringBuilder();
            StringBuilder undrmet = new StringBuilder();
            if (mode == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                    "'chart': {" +
                        "'caption': 'Fieldwork Days'," +
                        "'bgcolor': 'FFFFFF'," +
                        "'plotgradientcolor': ''," +
            "'showalternatehgridcolor': '0'," +
            "'showplotborder': '0'," +
            "'divlinecolor': 'CCCCCC'," +
            "'showvalues': '1'," +
            "'showcanvasborder': '0'," +
            "'pyaxisname': 'Fieldwork Days'," +
            "'syaxisname': 'FW Norms'," +
               "'palettecolors': '#6c3483,#fe33ff,#AADD00'," +

            "'slantlabels': '0'," +
            "'canvasborderalpha': '0'," +
            "'legendshadow': '1'," +
            "'legendborderalpha': '0'," +
                  "'labelDisplay': 'rotate'," +
                    "'useroundedges': '1'," +

                         "'sYAxisMaxValue' : '30'," +
                         "'sYAxisMinValue' : '0'," +
                           "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

            "'showborder': '0' " +


                    "},");

                categories.Append("'categories': [" +
                    "{" +
                        "'category': [");

                drmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'Days'," +
                            "'data': [");

                chmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'FW Norms'," +
                              "'renderAs': 'line'," +
                          "'showValues': '0'," +
                         "'color': '#FF0000', " +

                            "'parentYAxis': 'S',   " +


                            "'data': [");


            }
            else
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
        "'chart': {" +
            "'caption': 'Call Average'," +
            "'bgcolor': 'FFFFFF'," +
            "'plotgradientcolor': ''," +
"'showalternatehgridcolor': '0'," +
"'showplotborder': '0'," +
"'divlinecolor': 'CCCCCC'," +
"'showvalues': '1'," +
"'showcanvasborder': '0'," +
"'pyaxisname': 'Call Average'," +
"'syaxisname': 'Average Norms'," +
   "'palettecolors': '#6c3483,#fe33ff,#AADD00'," +

"'slantlabels': '0'," +
"'canvasborderalpha': '0'," +
"'legendshadow': '1'," +
"'legendborderalpha': '0'," +
      "'labelDisplay': 'rotate'," +
        "'useroundedges': '1'," +

             "'sYAxisMaxValue' : '15'," +
             "'sYAxisMinValue' : '0'," +
               "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

"'showborder': '0' " +


        "},");

                categories.Append("'categories': [" +
                    "{" +
                        "'category': [");

                drmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'Average'," +
                            "'data': [");

                chmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'Average Norms'," +
                              "'renderAs': 'line'," +
                          "'showValues': '0'," +
                         "'color': '#FF0000', " +

                            "'parentYAxis': 'S',   " +


                            "'data': [");
            }
            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    chmet.Append(",");

                }


                categories.AppendFormat("{{" +
                    // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                drmet.AppendFormat("{{" +
                    // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                chmet.AppendFormat("{{" +
                    // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 2]);


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");
            chmet.Append("]" +
                    "}");

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(chmet.ToString());

            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string FreqArea(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "FW_Single_Graph_Freq";
            }
            else if (mode == "2")
            {
                sProc_Name = "CallAvg_Single_Graph_Norms";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);

            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder drmet = new StringBuilder();
            StringBuilder chmet = new StringBuilder();
            StringBuilder undrmet = new StringBuilder();
            if (mode == "1")
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
                    "'chart': {" +
                        "'caption': 'Fieldwork Days'," +
                        "'bgcolor': 'FFFFFF'," +
                        "'plotgradientcolor': ''," +
            "'showalternatehgridcolor': '0'," +
            "'showplotborder': '0'," +
            "'divlinecolor': 'CCCCCC'," +
            "'showvalues': '1'," +
            "'showcanvasborder': '0'," +
            "'pyaxisname': 'Fieldwork Days'," +
            "'syaxisname': 'FW Norms'," +
               "'palettecolors': '#6c3483,#fe33ff,#AADD00'," +

            "'slantlabels': '0'," +
            "'canvasborderalpha': '0'," +
            "'legendshadow': '1'," +
            "'legendborderalpha': '0'," +
                  "'labelDisplay': 'rotate'," +
                    "'useroundedges': '1'," +

                         "'sYAxisMaxValue' : '30'," +
                         "'sYAxisMinValue' : '0'," +
                           "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

            "'showborder': '0' " +


                    "},");

                categories.Append("'categories': [" +
                    "{" +
                        "'category': [");

                drmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'Days'," +
                            "'data': [");

                chmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'FW Norms'," +
                              "'renderAs': 'area'," +
                          "'showValues': '0'," +
                         "'color': '#FF0000', " +

                            "'parentYAxis': 'S',   " +


                            "'data': [");


            }
            else
            {
                jsonData.Append("{" +
                    //Initialize the chart object with the chart-level attributes..
        "'chart': {" +
            "'caption': 'Call Average'," +
            "'bgcolor': 'FFFFFF'," +
            "'plotgradientcolor': ''," +
"'showalternatehgridcolor': '0'," +
"'showplotborder': '0'," +
"'divlinecolor': 'CCCCCC'," +
"'showvalues': '1'," +
"'showcanvasborder': '0'," +
"'pyaxisname': 'Fieldwork Days'," +
"'syaxisname': 'FW Norms'," +
   "'palettecolors': '#6c3483,#fe33ff,#AADD00'," +

"'slantlabels': '0'," +
"'canvasborderalpha': '0'," +
"'legendshadow': '1'," +
"'legendborderalpha': '0'," +
      "'labelDisplay': 'rotate'," +
        "'useroundedges': '1'," +

             "'sYAxisMaxValue' : '15'," +
             "'sYAxisMinValue' : '0'," +
               "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

"'showborder': '0' " +


        "},");

                categories.Append("'categories': [" +
                    "{" +
                        "'category': [");

                drmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'Days'," +
                            "'data': [");

                chmet.Append("{" +
                    // dataset level attributes
                            "'seriesName': 'FW Norms'," +
                              "'renderAs': 'area'," +
                          "'showValues': '0'," +
                         "'color': '#FF0000', " +

                            "'parentYAxis': 'S',   " +


                            "'data': [");
            }
            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    chmet.Append(",");

                }


                categories.AppendFormat("{{" +
                    // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                drmet.AppendFormat("{{" +
                    // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                chmet.AppendFormat("{{" +
                    // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 2]);


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");
            chmet.Append("]" +
                    "}");

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(chmet.ToString());

            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }


    #region MultiFreq
    [WebMethod(EnableSession = true)]

    public static string MultiFreq(string objData)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];
        sSfName = arr[6];
        modeName = arr[7];
        FMName = arr[8];
        TMName = arr[9];
        int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(smonth);
        int cyear = Convert.ToInt32(syear);
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

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");

            SalesForce sf = new SalesForce();
            string sProc_Name = "";
            if (mode == "1")
            {
                sProc_Name = "Desigwise_FW_Graph_Norms";
            }
            else if (mode == "2")
            {
                sProc_Name = "CallAvg_Single_Graph";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(0);
            //dt.Columns.RemoveAt(2);
            //dt.Columns.RemoveAt(3);
            //dt.Columns.RemoveAt(1);
            //dt.Columns.RemoveAt(1);
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    arrData[i, k] = dt.Rows[i][k];


                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder currentYear = new StringBuilder();
            StringBuilder previousYear = new StringBuilder();
            StringBuilder upcomingYear = new StringBuilder();
            Designation des = new Designation();
            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                     "'caption' : '" + sSfName + ' ' + '(' + FMName + syear + ' ' + '-' + ' ' + TMName + tyear + ')' + "'  ," +
                          "'subcaption' : '" + modeName + "'," +
                        "'xAxisname': 'Month'," +
                        "'yAxisName': 'Days'," +
                     "'useroundedges': '1'," +
                        "'plotFillAlpha': '80'," +
                          "'labelDisplay': 'rotate'," +
                    "'palettecolors': '#FF0000,#800000,#FFFF00,#0f612c,#6c3483,#fe33ff,#AADD00'," +
                        "'baseFontColor': '#333333'," +
                        "'baseFont': 'Helvetica Neue,Arial'," +
                        "'captionFontSize': '14'," +
                        "'subcaptionFontSize': '14'," +
                        "'subcaptionFontBold': '1'," +
                        "'showBorder': '0'," +
                        "'bgColor': '#ffffff'," +
                        "'showShadow': '1'," +
                        "'canvasBgColor': '#ffffff'," +
                        "'canvasBorderAlpha': '0'," +
                        "'divlineAlpha': '100'," +
                        "'divlineColor': '#999999'," +
                        "'divlineThickness': '1'," +
                        "'divLineIsDashed': '1'," +
                        "'divLineDashLen': '1'," +
                        "'divLineGapLen': '1'," +
                        "'usePlotGradientColor': '0'," +
                        "'showplotborder': '0'," +
                            "'showvalues': '1'," +
                        "'valueFontColor': '#000000'," +
                        "'placeValuesInside': '0'," +
                        "'showHoverEffect': '1'," +
                        "'rotateValues': '0'," +
                        "'showXAxisLine': '1'," +
                        "'xAxisLineThickness': '1'," +
                        "'xAxisLineColor': '#999999'," +
                        "'showAlternateHGridColor': '0'," +
                        "'legendBgAlpha': '0'," +
                        "'legendBorderAlpha': '0'," +
                        "'legendShadow': '0'," +
                        "'legendItemFontSize': '10'," +
                           "'formatNumber': '0'," +
                "'formatNumberScale': '0'," +
                      "'exportEnabled': '1'," +
                            "'exportAtClient': '1'," +
                            "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                             "'exportFileName' : 'DashBoard'," +

                        "'legendItemFontColor': '#666666'" +
                "},");
            Dictionary<string, string> obj = new Dictionary<string, string>();


            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            foreach (DataColumn c in dt.Columns)
            {
                // if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("CTTL"))
                if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("ATTL"))
                {


                    if (mode == "1" || mode == "2")
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        obj.Add(c.ColumnName.ToString(), "{" +
                            // dataset level attributes

                                "'seriesname': '" + dsdes.Tables[0].Rows[0]["Designation_Short_Name"] + "'," +
                                "'data': [");
                    }

                }
            }


            int j = 1;
            //Iterate through the data contained  in the `arrData` array.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("ATTL"))
                        {
                            obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + ",";
                        }
                    }
                    categories.Append(",");

                }
                dt.Rows[i][0] = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["MNTH"].ToString()).Substring(0, 3) +
           " - " + dtMnYr.Rows[Convert.ToInt32(dt.Rows[i][0]) - 1]["YR"].ToString();
                categories.AppendFormat("{{" +
                    // category level attributes
                    "'label': '{0}'" +
                "}}", dt.Rows[i][0]);
                DataTable dtnew = new DataTable();
                if (mode == "1")
                {
                    SalesForce sf1 = new SalesForce();
                    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                    SqlConnection con1 = new SqlConnection(strConn);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("EXEC ViewDetails_DrsTmp_Graph " + div_code + ",'" + sSf_Code + "'", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                    da1.Fill(dtnew);
                }
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("ATTL"))
                    {
                        des_code = c.ColumnName.Split('_').First();
                        var rows = from row in dtnew.AsEnumerable()
                                   where row.Field<string>("designation_code") == des_code
                                   select row;


                        // cnt = cnt / count;

                        if (mode == "1")
                        {
                            int count = rows.Count<DataRow>();
                            // var dataVal = dt.AsEnumerable().Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //  dt.Rows[i] = Convert.float(dt.Rows[i] / count);
                            decimal cnt = Convert.ToDecimal(dt.Rows[i][des_code + "_ATTL"]) / count;
                            obj[c.ColumnName.ToString()] = obj

//[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt, 1)

[c.ColumnName.ToString()] + "{" + "'value': '" + System.Math.Round(cnt * 2, MidpointRounding.ToEven) / 2
 + "'" + "}";
                        }

                    }
                    else if (c.ColumnName.ToUpper() != "MONTH" && c.ColumnName.Contains("BTTL"))
                    {
                        des_code = c.ColumnName.Split('_').First();

                        dsdes = des.getDesig_graph(des_code, div_code);
                        string str = dsdes.Tables[0].Rows[0]["Norms_FW"].ToString();
                        dt.Rows[i][c.ColumnName.ToUpper()] = str;

                        // obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()].ToString() + "'" + "}";
                        obj[c.ColumnName.ToString()] = obj

[c.ColumnName.ToString()] + "{" + "'value': '" + dt.Rows[i][c.ColumnName.ToUpper()]


+ "'" + "}";
                    }
                }
                j += 1;
            }

            categories.Append("]" +
                    "}" +
                "],");

            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    obj[c.ColumnName.ToString()] = obj[c.ColumnName.ToString()] +

"]" + "},";
                }
            }




            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName.ToUpper() != "MONTH")
                {
                    jsonData.Append(obj[c.ColumnName.ToString()]);
                }
            }
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }

    }
    #endregion
}