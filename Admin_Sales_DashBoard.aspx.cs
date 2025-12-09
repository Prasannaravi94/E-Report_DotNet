using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using Bus_EReport;
using System.Configuration;
using System.Web.Script.Services;
using iTextSharp.tool.xml;
using System.Web.UI.DataVisualization.Charting;
public partial class Admin_Sales_DashBoard : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsHoliday = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = new DataSet();

    static DataTable dtcopy = new DataTable();
    string strday = string.Empty;
    string strWeek = string.Empty;
    string state_code = string.Empty;

    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;
    static string FMName = string.Empty;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        menumas.FindControl("pnlHeader").Visible = false;
        div_code = Session["div_code"].ToString();
        sDivCode = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillYear();
            FillMRManagers();
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
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
                ddlFYear.Items.Add(k.ToString());
                ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                //  ddlFYear.SelectedValue = "2018";
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();

    }
    [WebMethod(EnableSession = true)]

    public static string Primary(string objData)
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
        DataTable dtcopy = new DataTable();
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
            DataTable sortedDT = new DataTable();
            //if (mode == "1")
            //{
                SqlCommand cmd = new SqlCommand("Primary_Sale_Graph_Multiple", con);
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
               
                DataView dv = dt.DefaultView;
                dv.Sort = "cnt desc";
                sortedDT = dv.ToTable();
                con.Close();
                sortedDT.Columns["Prod_Name"].ColumnName = "Label";
                sortedDT.AcceptChanges();
                sortedDT.Columns["cnt"].ColumnName = "Value";

                sortedDT.AcceptChanges();
           // }
            //else if (mode == "2")
            //{
            //    SqlCommand cmd = new SqlCommand("SecSale_Analysis_Stk_Graph", con);
            //    cmd.CommandTimeout = 50;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Div_Code", div_code);
            //    cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            //    cmd.Parameters.AddWithValue("@cMonth", smonth);
            //    cmd.Parameters.AddWithValue("@cYear", syear);
            //    con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.SelectCommand = cmd;
            //    // DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    dt.Columns.RemoveAt(5);
            //    dt.Columns.RemoveAt(4);
            //    dt.Columns.RemoveAt(2);
            //    dt.Columns.RemoveAt(1);
            //    dt.Columns.RemoveAt(0);
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = "1_0_ABT desc";
            //    sortedDT = dv.ToTable();
            //    con.Close();
            //    sortedDT.Columns["Prod_Name"].ColumnName = "Label";
            //    sortedDT.AcceptChanges();
            //    sortedDT.Columns["1_0_ABT"].ColumnName = "Value";
            //    sortedDT.AcceptChanges();

            //}


            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

 

    public static string Secondary(string objData)
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
        DataTable dtcopy = new DataTable();
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
            DataTable sortedDT = new DataTable();
            //if (mode == "1")
            //{
            SqlCommand cmd = new SqlCommand("Secondary_Sale_Graph_Multiple", con);
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

            DataView dv = dt.DefaultView;
            dv.Sort = "cnt desc";
            sortedDT = dv.ToTable();
            con.Close();
            sortedDT.Columns["Prod_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["cnt"].ColumnName = "Value";

            sortedDT.AcceptChanges();
            // }
            //else if (mode == "2")
            //{
            //    SqlCommand cmd = new SqlCommand("SecSale_Analysis_Stk_Graph", con);
            //    cmd.CommandTimeout = 50;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Div_Code", div_code);
            //    cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            //    cmd.Parameters.AddWithValue("@cMonth", smonth);
            //    cmd.Parameters.AddWithValue("@cYear", syear);
            //    con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.SelectCommand = cmd;
            //    // DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    dt.Columns.RemoveAt(5);
            //    dt.Columns.RemoveAt(4);
            //    dt.Columns.RemoveAt(2);
            //    dt.Columns.RemoveAt(1);
            //    dt.Columns.RemoveAt(0);
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = "1_0_ABT desc";
            //    sortedDT = dv.ToTable();
            //    con.Close();
            //    sortedDT.Columns["Prod_Name"].ColumnName = "Label";
            //    sortedDT.AcceptChanges();
            //    sortedDT.Columns["1_0_ABT"].ColumnName = "Value";
            //    sortedDT.AcceptChanges();

            //}


            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }
    [WebMethod]
    public static string Target_Cum(string objData)
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
        DataTable dtcopy = new DataTable();
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
            string sProc_Name = "";

            sProc_Name = "Target_Sale_Graph_Growth_Consol_Multiple";

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
            dt.Columns.Remove("sno");
            dt.Columns.Remove("sf_code");
            dt.Columns.Remove("sf_name");
            dt.Columns.Remove("sf_type");
            dt.Columns.Remove("sf_code1");
            dt.Columns.Remove("sf_cat_code");
            dt.Columns.Remove("sf_TP_Active_Flag");
            dt.Columns.Remove("SF_VacantBlock");
            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];

                    if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = 0;
                        arrData[i, j] = dt.Rows[i][j];
                    }

                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder drmet = new StringBuilder();
            StringBuilder chmet = new StringBuilder();
            StringBuilder undrmet = new StringBuilder();
            StringBuilder growth = new StringBuilder();

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': ''," +
                    "'bgcolor': 'FFFFFF'," +
                    "'showHoverEffect': '1'," +
                        "'plotgradientcolor': ''," +
                        "'plotBorderDashed':'1'," +
                        "'plotBorderDashLen':'5'," +
        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +

        "'pyaxisname': ''," +
        "'syaxisname': 'Achievement & Growth(%)'," +
           "'palettecolors': '#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                       //"'exportEnabled': '1'," +
                       // "'exportAtClient': '1'," +
                       // "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                       // "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                       //  "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0', " +
                            // "'showCanvasBorder':'1',  " +
                           //  "'canvasBorderThickness':'2'," +
        "'showborder': '0' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                        // dataset level attributes

                        "'seriesName': 'Target'," +
                        "'data': [");

            undrmet.Append("{" +
                         // dataset level attributes
                         "'seriesName': 'Primary'," +
                         "'data': [");

            chmet.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Achievement(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
        "'divlinecolor': 'CCCCCC'," +
           "'showvalues': '0'," +

        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");


            growth.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Growth(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
            "'color':'#23BFAA'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +
         "'rotateValues':'0' ," +
        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");
                    chmet.Append(",");
                    growth.Append(",");

                }


                categories.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                drmet.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                undrmet.AppendFormat("{{" +
                      // data level attributes
                      "'value': '{0}'" +
                  "}}", arrData[i, 2]);

                chmet.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);
                growth.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 4]);


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "},");
            chmet.Append("]" +
                    "},");
            growth.Append("]" +
                  "}");

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());
            jsonData.Append(chmet.ToString());
            jsonData.Append(growth.ToString());
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string Target_Cum_Sec(string objData)
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
        DataTable dtcopy = new DataTable();
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
            string sProc_Name = "";

            sProc_Name = "Target_Secondary_Graph_Growth_Consol_Multiple";

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
            dt.Columns.Remove("sno");
            dt.Columns.Remove("sf_code");
            dt.Columns.Remove("sf_name");
            dt.Columns.Remove("sf_type");
            dt.Columns.Remove("sf_code1");
            dt.Columns.Remove("sf_cat_code");
            dt.Columns.Remove("sf_TP_Active_Flag");
            dt.Columns.Remove("SF_VacantBlock");
            con.Close();

            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;


            object[,] arrData = new object[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrData[i, j] = dt.Rows[i][j];

                    if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = 0;
                        arrData[i, j] = dt.Rows[i][j];
                    }

                }
            }

            StringBuilder jsonData = new StringBuilder();
            StringBuilder categories = new StringBuilder();
            StringBuilder drmet = new StringBuilder();
            StringBuilder chmet = new StringBuilder();
            StringBuilder undrmet = new StringBuilder();
            StringBuilder growth = new StringBuilder();

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': ''," +
                    "'bgcolor': 'FFFFFF'," +
                    "'showHoverEffect': '1'," +
                        "'plotgradientcolor': ''," +
                        "'plotBorderDashed':'1'," +
                        "'plotBorderDashLen':'5'," +
        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +

        "'pyaxisname': ''," +
        "'syaxisname': 'Achievement & Growth(%)'," +
           "'palettecolors': '#49c47a,#e3a944,#d62965,#b256d1,#cfc80e,#B8860B,#BC8F8F,#C48E48,#CC00FF,#CCFFCC,#CD7054,#D0FAEE,#E7C6A5,#EE6A50'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                            //"'exportEnabled': '1'," +
                            // "'exportAtClient': '1'," +
                            // "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                            // "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                            //  "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0', " +
                // "'showCanvasBorder':'1',  " +
                //  "'canvasBorderThickness':'2'," +
                "'showborder': '0' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                        // dataset level attributes

                        "'seriesName': 'Target'," +
                        "'data': [");

            undrmet.Append("{" +
                         // dataset level attributes
                         "'seriesName': 'Secondary'," +
                         "'data': [");

            chmet.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Achievement(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
        "'divlinecolor': 'CCCCCC'," +
           "'showvalues': '0'," +

        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");


            growth.Append("{" +
                        // dataset level attributes
                        "'seriesName': 'Growth(%)'," +
                           "'renderAs': 'line'," +
                        "'anchorRadius': '4', " +
                           "'showalternatehgridcolor': '0', " +
            "'color':'#23BFAA'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +
         "'rotateValues':'0' ," +
        "'showcanvasborder': '0'," +
        "'canvasborderalpha': '0'," +
        "'canvasbordercolor': 'CCCCCC'," +
        "'canvasborderthickness': '1'," +
        "'yaxismaxvalue': '30000'," +
        "'captionpadding': '30'," +
        "'linethickness': '3'," +
        "'yaxisvaluespadding': '15'," +
        "'legendshadow': '0'," +
        "'legendborderalpha': '0'," +
        "'palettecolors': '#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78',  " +
        "'showborder': '0'," +
         "'stepSkipped': 'false', " +
                    "'appliedSmartLabel': 'true'," +

                            "'parentYAxis': 'S',   " +


                        "'data': [");

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");
                    chmet.Append(",");
                    growth.Append(",");

                }


                categories.AppendFormat("{{" +
                        // category level attributes
                        "'label': '{0}'" +
                    "}}", arrData[i, 0]);

                drmet.AppendFormat("{{" +
                        // data level attributes
                        "'value': '{0}'" +
                    "}}", arrData[i, 1]);

                undrmet.AppendFormat("{{" +
                      // data level attributes
                      "'value': '{0}'" +
                  "}}", arrData[i, 2]);

                chmet.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);
                growth.AppendFormat("{{" +
                          // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 4]);


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "},");
            chmet.Append("]" +
                    "},");
            growth.Append("]" +
                  "}");

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());
            jsonData.Append(chmet.ToString());
            jsonData.Append(growth.ToString());
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }


    protected void ddlDBMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDBMode.SelectedItem.Value == "0")
        {
            Response.Redirect("Admin_Dashboard.aspx");
        }
        if (ddlDBMode.SelectedItem.Value == "2")
        {
            Response.Redirect("Admin_Primary_Sales_DashBoard.aspx");
        }
    }
}