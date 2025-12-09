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
using Bus_EReport;
using System.Text;
public partial class DashBoard_Marketing_SFE : System.Web.UI.Page
{
    static string sSf_Code = string.Empty;
    static string sSfName = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string Chk = string.Empty;
    static string Brand = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    static string mode = string.Empty;
    DataTable dt = new DataTable();
    DataSet dsProduct = new DataSet();
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
            sSf_Code = ddlFieldForce.SelectedValue;
            sDivCode = Session["div_code"].ToString();

            FillYear();
            FillManagers();
            FillBrand();
            sSf_Code = ddlFieldForce.SelectedValue;

        }
    }
    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.getProductBrand_UP(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlBrand.DataTextField = "Product_Brd_Name";
            ddlBrand.DataValueField = "Product_Brd_Code";
            ddlBrand.DataSource = dsProduct;
            ddlBrand.DataBind();
         
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
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
               
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserListTP_Hierarchy(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }
    [WebMethod]
    public static string Priority(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        sSfName = arr[4];
        Chk = arr[5];
        Brand = arr[6];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_Brand_Prioritywise_Brand";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);
            cmd.Parameters.AddWithValue("@Chk", Chk);
            cmd.Parameters.AddWithValue("@Brand", Brand);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
           
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
                    "'caption': 'Brand - Priority Visit'," +
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
       
           "'palettecolors': '#FFC300,#FF5733,#fe33ff'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
           //   "'labelDisplay': 'rotate'," +
                              
                       "'exportEnabled': '1'," +
                        "'exportAtClient': '1'," +
                        "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                        "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                         "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0' ," +
                             "'showCanvasBorder':'1',  " +
                             "'canvasBorderThickness':'2'," +
        "'showborder': '1' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes

                        "'seriesName': 'Drs'," +
                        "'data': [");

            undrmet.Append("{" +
                // dataset level attributes
                         "'seriesName': 'Visit'," +
                         "'data': [");


            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");
                 

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

             


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "}");
       

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());
        
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod(EnableSession = true)]

    public static string Potential(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        sSfName = arr[4];
        Chk = arr[5];
        Brand = arr[6];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dtv = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_RCPA_Brand";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);
            cmd.Parameters.AddWithValue("@Chk", Chk);
            cmd.Parameters.AddWithValue("@Brand", Brand);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dtv);
            //  dtcopy = dtD.Copy();
         

            con.Close();

            dtv.Columns["Para"].ColumnName = "Label";
            dtv.AcceptChanges();
            dtv.Columns["Cnt"].ColumnName = "Value";

            dtv.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtv);

            return jsonResult;

        }
    }
    [WebMethod]
    public static string Campaign(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        sSfName = arr[4];
        Chk = arr[5];
        Brand = arr[6];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_Campaign_drs_Brand";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);
            cmd.Parameters.AddWithValue("@Chk", Chk);
            cmd.Parameters.AddWithValue("@Brand", Brand);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

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
                    "'caption': 'Brandwise - Campaign Drs Visit'," +
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

           "'palettecolors': '#FFC300,#FF5733,#fe33ff'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
                //   "'labelDisplay': 'rotate'," +

                       "'exportEnabled': '1'," +
                        "'exportAtClient': '1'," +
                        "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                        "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                         "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0' ," +
                             "'showCanvasBorder':'1',  " +
                             "'canvasBorderThickness':'2'," +
        "'showborder': '1' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes

                        "'seriesName': 'Drs'," +
                        "'data': [");

            undrmet.Append("{" +
                // dataset level attributes
                         "'seriesName': 'Visit'," +
                         "'data': [");


            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");


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




            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "}");


            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());

            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string Sample(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        sSfName = arr[4];
        Chk = arr[5];
        Brand = arr[6];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_Sample_Input_Brand";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);
            cmd.Parameters.AddWithValue("@Chk", Chk);
            cmd.Parameters.AddWithValue("@Brand", Brand);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
      
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
                    "'caption': 'Sample & Input Issued - Brandwise'," +
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
        "'syaxisname': 'No of Drs Sample'," +
           "'palettecolors': '#FFC300,#FF5733,#fe33ff'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                       "'exportEnabled': '1'," +
                        "'exportAtClient': '1'," +
                        "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                        "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                         "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0' ," +
                             "'showCanvasBorder':'1',  " +
                             "'canvasBorderThickness':'2'," +
        "'showborder': '1' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes

                        "'seriesName': 'Drs'," +
                        "'data': [");

            undrmet.Append("{" +
                // dataset level attributes
                         "'seriesName': 'Qty'," +
                         "'data': [");

         

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");
                   

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

              


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "}");
          
            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());
          
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string Coverage(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        sSfName = arr[4];
        Chk = arr[5];
        Brand = arr[6];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_Category_drs_Brand";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMnth", smonth);
            cmd.Parameters.AddWithValue("@cYrs", syear);
            cmd.Parameters.AddWithValue("@Chk", Chk);
            cmd.Parameters.AddWithValue("@Brand", Brand);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Remove("Doc_Cat_Code");
         
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
        

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': 'Coverage - Brandwise'," +
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
        "'syaxisname': 'Coverage(%)'," +
           "'palettecolors': '#FFC300,#FF5733,#fe33ff'," +

        "'slantlabels': '0'," +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '1'," +
        "'legendborderalpha': '0'," +
              "'labelDisplay': 'rotate'," +


                     "'sYAxisMaxValue' : '30'," +
                     "'sYAxisMinValue' : '0'," +
                       "'exportEnabled': '1'," +
                        "'exportAtClient': '1'," +
                        "'exportHandler': 'http://export.api3.fusioncharts.com'," +
                        "'html5ExportHandler': 'http://export.api3.fusioncharts.com'," +
                         "'exportFileName' : 'DashBoard'," +
                            "'formatNumber': '0'," +
                        "'formatNumberScale': '0'," +
                        "'useRoundEdges': '1'," +
                            "'placeValuesInside': '0'," +
                             "'rotateValues':'0' ," +
                             "'showCanvasBorder':'1',  " +
                             "'canvasBorderThickness':'2'," +
        "'showborder': '1' " +


                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes

                        "'seriesName': 'Drs'," +
                        "'data': [");

            undrmet.Append("{" +
                // dataset level attributes
                         "'seriesName': 'Visit'," +
                         "'data': [");

            chmet.Append("{" +
                // dataset level attributes
                        "'seriesName': 'Coverage(%)'," +
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


           

            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    undrmet.Append(",");
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

                undrmet.AppendFormat("{{" +
                    // data level attributes
                      "'value': '{0}'" +
                  "}}", arrData[i, 2]);

                chmet.AppendFormat("{{" +
                    // data level attributes


                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);
              


            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");

            undrmet.Append("]" +
                   "},");
            chmet.Append("]" +
                    "}");
            

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(undrmet.ToString());
            jsonData.Append(chmet.ToString());
  
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
}