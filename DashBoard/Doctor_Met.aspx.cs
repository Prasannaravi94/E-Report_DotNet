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


using System.Web.Services;
public partial class DashBoard_Doctor_Met : System.Web.UI.Page
{
    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    DataSet dsProCat = new DataSet();
  

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
        
          //  FillYear();
            FillManagers();

        }
    }
    //private void FillYear()
    //{
    //    TourPlan tp = new TourPlan();
    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlFYear.Items.Add(k.ToString());
    //            ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
    //        }

    //    }
    //    ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
    //}

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
   
    [WebMethod(EnableSession = true)]

    public static string ProdCat()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


       
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsProCat = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Product dv = new Product();
            dsProCat = dv.getProCat(div_code);
            if (dsProCat.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsProCat.Tables[0].Copy();
            
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Product_Cat_Name"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["cat_count"].ColumnName = "Value";

                dt.AcceptChanges();




               
            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }

    [WebMethod(EnableSession = true)]

    public static string ProdGroup()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsProGrp = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Product dv = new Product();
            dsProGrp = dv.getProGrp(div_code);
            if (dsProGrp.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsProGrp.Tables[0].Copy();

                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Product_Grp_Name"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["Grp_count"].ColumnName = "Value";

                dt.AcceptChanges();





            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }
    [WebMethod(EnableSession = true)]

    public static string ProdBrand()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsProBrd = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Product dv = new Product();
            dsProBrd = dv.getProBrd(div_code);
            if (dsProBrd.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsProBrd.Tables[0].Copy();

                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Product_Brd_Name"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["brd_count"].ColumnName = "Value";

                dt.AcceptChanges();





            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }
    [WebMethod(EnableSession = true)]

    public static string DocCat()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsDocCat = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Doctor doc = new Doctor();
            dsDocCat = doc.getDocCat(div_code);
            if (dsDocCat.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsDocCat.Tables[0].Copy();
                dt.Columns.RemoveAt(3);
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Doc_Cat_Name"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["Cat_Count"].ColumnName = "Value";

                dt.AcceptChanges();





            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string DocSpec()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsDocSpec = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Doctor doc = new Doctor();
            dsDocSpec = doc.getDocSpe_Graph(div_code);
            if (dsDocSpec.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsDocSpec.Tables[0].Copy();
               
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Doc_Special_Name"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["Spec_Count"].ColumnName = "Value";

                dt.AcceptChanges();





            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }
    [WebMethod(EnableSession = true)]

    public static string DocCls()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            DataSet dsDocCls = new DataSet();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            Doctor doc = new Doctor();
            dsDocCls = doc.getDocCls(div_code);
            if (dsDocCls.Tables[0].Rows.Count > 0)
            {
                // DataTable dt = new DataTable();
                dt = dsDocCls.Tables[0].Copy();
               
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(0);


                dt.Columns["Doc_ClsName"].ColumnName = "Label";
                dt.AcceptChanges();
                dt.Columns["Cls_Count"].ColumnName = "Value";

                dt.AcceptChanges();





            }
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;
        }
    }

    [WebMethod(EnableSession = true)]

    public static string Doc_Camp()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();



        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "KPI_Master_Campaign_drs";


            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);


            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable dtCamp = new DataTable();
            da.Fill(dtCamp);

            dtCamp.Columns.RemoveAt(0);


            con.Close();
            dtCamp.Columns["Doc_SubCatName"].ColumnName = "Label";
            dtCamp.AcceptChanges();
            dtCamp.Columns["dr_Count"].ColumnName = "Value";

            dtCamp.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dtCamp);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string StockSt()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd;

            cmd = new SqlCommand("GetStatewise_StockistCnt", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
           
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.RemoveAt(1);

            con.Close();



            dt.Columns["Ava_Stockist"].ColumnName = "value";
            dt.AcceptChanges();
            dt.Columns["Map_Code"].ColumnName = "id";
            dt.AcceptChanges();


            //  bindState();
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
      [WebMethod(EnableSession = true)]

    public static string ssgrid()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd;

            cmd = new SqlCommand("GetStatewise_StockistCnt", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.RemoveAt(0);


            con.Close();



            dt.Columns["Ava_Stockist"].ColumnName = "value";
            dt.AcceptChanges();
            dt.Columns["State_Name"].ColumnName = "label";
            dt.AcceptChanges();


            //  bindState();
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    //[WebMethod(EnableSession=true)]

    //public static string Input(string objData)
    //{
    //    string div_code = HttpContext.Current.Session["div_code"].ToString();

     
    //    string[] arr = objData.Split('^');
    //    smonth = arr[0];
    //    syear = arr[1];
    //    sSf_Code = arr[2];
    //    using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
    //    {
    //        DataTable dt = new DataTable();
    //        // dt.Columns.Add("Label");
    //        // dt.Columns.Add("Value");
    //        SqlCommand cmd = new SqlCommand("MissedCall_Graph", con);
    //        cmd.CommandTimeout = 50;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Div_Code", div_code);
    //        cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
    //        cmd.Parameters.AddWithValue("@cMnth", smonth);
    //        cmd.Parameters.AddWithValue("@cYrs", syear);
    //        con.Open();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.SelectCommand = cmd;
    //        // DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        dt.Columns.RemoveAt(10);
    //        dt.Columns.RemoveAt(7);
    //        dt.Columns.RemoveAt(6);
    //        dt.Columns.RemoveAt(5);
    //        dt.Columns.RemoveAt(4);
    //        dt.Columns.RemoveAt(3);
    //        dt.Columns.RemoveAt(2);
    //        dt.Columns.RemoveAt(0);
    //        con.Close();
    //        dt.Columns["sf_Name"].ColumnName = "Label";
    //        dt.AcceptChanges();
    //        dt.Columns["1_0_AAT"].ColumnName = "Value";
    //        dt.Columns["1_0_ABT"].ColumnName = "Value ";
             
    //        dt.AcceptChanges();
    



    //        string jsonResult = JsonConvert.SerializeObject(dt);

    //        return jsonResult;

    //    }
    //}



    //[WebMethod]
    //public static string Input()
    //{
    //    object[,] arrData = new object[6, 3];

    //    //Store product labels in the first column.
    //    arrData[0, 0] = "Product A";
    //    arrData[1, 0] = "Product B";
    //    arrData[2, 0] = "Product C";
    //    arrData[3, 0] = "Product D";
    //    arrData[4, 0] = "Product E";
    //    arrData[5, 0] = "Product F";

    //    //Store sales data for the current year in the second column.
    //    arrData[0, 1] = 567500;
    //    arrData[1, 1] = 815300;
    //    arrData[2, 1] = 556800;
    //    arrData[3, 1] = 734500;
    //    arrData[4, 1] = 676800;
    //    arrData[5, 1] = 648500;

    //    //Store sales data for previous year in the third column.
    //    arrData[0, 2] = 367300;
    //    arrData[1, 2] = 584500;
    //    arrData[2, 2] = 754000;
    //    arrData[3, 2] = 456300;
    //    arrData[4, 2] = 754500;
    //    arrData[5, 2] = 437600;

    //    StringBuilder jsonData = new StringBuilder();
    //    StringBuilder categories = new StringBuilder();
    //    StringBuilder currentYear = new StringBuilder();
    //    StringBuilder previousYear = new StringBuilder();

    //    jsonData.Append("{" +
    //        //Initialize the chart object with the chart-level attributes..
    //        "'chart': {" +
    //            "'caption': 'Sales by Product'," +
    //            "'numberPrefix': '$'," +
    //            "'formatNumberScale': '1'," +
    //            "'placeValuesInside': '1'," +
    //            "'decimals': '0'" +
    //        "},");

    //    categories.Append("'categories': [" +
    //        "{" +
    //            "'category': [");

    //    currentYear.Append("{" +
    //        // dataset level attributes
    //                "'seriesname': 'Current Year'," +
    //                "'data': [");

    //    previousYear.Append("{" +
    //        // dataset level attributes
    //                "'seriesname': 'Previous Year'," +
    //                "'data': [");

    //    //Iterate through the data contained  in the `arrData` array.
    //    for (int i = 0; i < arrData.GetLength(0); i++)
    //    {
    //        if (i > 0)
    //        {
    //            categories.Append(",");
    //            currentYear.Append(",");
    //            previousYear.Append(",");
    //        }

    //        //Append individual category-level data to the `categories` object.

    //        categories.AppendFormat("{{" +
    //            // category level attributes
    //                "'label': '{0}'" +
    //            "}}", arrData[i, 0]);

    //        //Append current year’s sales data for each product to the `currentYear` object.

    //        currentYear.AppendFormat("{{" +
    //            // data level attributes
    //                "'value': '{0}'" +
    //            "}}", arrData[i, 1]);

    //        //Append previous year’s sales data for each product to the `currentYear` object.

    //        previousYear.AppendFormat("{{" +
    //            // data level attributes
    //                  "'value': '{0}'" +
    //              "}}", arrData[i, 2]);
    //    }

    //    categories.Append("]" +
    //            "}" +
    //        "],");

    //    //Append as strings the closing part of the array definition of the `data` object array to the `currentYear` and `previousYear` objects.

    //    currentYear.Append("]" +
    //            "},");
    //    previousYear.Append("]" +
    //            "}");

    //    //Append the complete chart data converted to a string to the `jsonData` object.

    //    jsonData.Append(categories.ToString());
    //    jsonData.Append("'dataset': [");
    //    jsonData.Append(currentYear.ToString());
    //    jsonData.Append(previousYear.ToString());
    //    jsonData.Append("]" +
    //            "}");

    //    return jsonData.ToString();
    //}
    [WebMethod]
    public static string Input(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

     
       // string[] arr = objData.;
        //smonth = arr[0];
        //syear = arr[1];
       // sSf_Code = arr[0];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd = new SqlCommand("Master_Det_Graph", con);
            cmd.CommandTimeout = 250;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", objData);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(6);          
            dt.Columns.RemoveAt(5);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);
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

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    "'caption': 'Master Details'," +
                    "'numberPrefix': ''," +
                    "'formatNumberScale': '0'," +
                    "'placeValuesInside': '0'," +
                    "'decimals': '0'," +
                    "'bgColor': '#ffffff',"+
                    "'showBorder': '1'," +
                    "'labelDisplay': 'rotate'," +
                    "'palettecolors': 'AFD8F8,A66EDD,F984A1'," +
                    "'showvalues': '0'," +
                    "'palette': '2',"+
                    "'useroundedges': '1'"+      
        
                    
                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes
                        "'seriesname': 'Doctor Cnt'," +
                        "'data': [");

            chmet.Append("{" +
                // dataset level attributes
                        "'seriesname': 'Chemist Cnt'," +
                        "'data': [");
            undrmet.Append("{" +
                // dataset level attributes
                     "'seriesname': 'Unlst Dr Cnt'," +
                     "'data': [");

          
            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    chmet.Append(",");
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

                chmet.AppendFormat("{{" +
                    // data level attributes
                          "'value': '{0}'" +
                      "}}", arrData[i, 2]);

                undrmet.AppendFormat("{{" +
                    // data level attributes
                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);
            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");
            chmet.Append("]" +
                    "},");
            undrmet.Append("]" +
                 "}");
         
            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(chmet.ToString());
            jsonData.Append(undrmet.ToString());
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
    [WebMethod]
    public static string Multi(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        // string[] arr = objData.;
        //smonth = arr[0];
        //syear = arr[1];
        // sSf_Code = arr[0];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd = new SqlCommand("Master_Det_Graph", con);
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", objData);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(6);
            dt.Columns.RemoveAt(5);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(3);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);
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

            jsonData.Append("{" +
                //Initialize the chart object with the chart-level attributes..
                "'chart': {" +
                    //"'caption': 'Master Details'," +
                    //"'numberPrefix': ''," +
                    //"'formatNumberScale': '0'," +
                    //"'placeValuesInside': '0'," +
                    //"'decimals': '0'," +
                    //"'bgColor': '#ffffff'," +
                    //"'showBorder': '1'," +
                    //"'labelDisplay': 'rotate'," +
                    //"'palettecolors': 'AFD8F8,A66EDD,F984A1'," +
                    //"'showvalues': '1'," +
                    //"'palette': '2'," +
                    //"'useroundedges': '1'," +
                    
        "'caption': 'Master Details'," +
        "'xaxisname': 'Base Level'," +
        "'yaxisname': 'Count'," +
        "'showvalues': '0'," +
            "'useroundedges': '1'," +
        "'suseroundedges': '1'," +
        "'legendborderalpha': '50',"+
        "'showborder': '0',"+
        "'bgcolor': 'FFFFFF,FFFFFF'," +
        "'plotgradientcolor': ' '," +
        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '0'," +
        "'labeldisplay': 'rotate'," +
      "'palettecolors': 'AFD8F8,A66EDD,F984A1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showcanvasborder': '0', " +
        "'canvasborderalpha': '0'," +
        "'legendshadow': '0' " +

                "},");

            categories.Append("'categories': [" +
                "{" +
                    "'category': [");

            drmet.Append("{" +
                // dataset level attributes
                 "'seriesname': 'Doctor Cnt'," +
                     "'stepSkipped': 'false'," +
                    "'appliedSmartLabel': 'true'," +
                      //  "'seriesname': 'Doctor Cnt'," +
                        "'data': [");

            chmet.Append("{" +
                // dataset level attributes
                        "'seriesname': 'Chemist Cnt'," +
                          "'stepSkipped': 'false'," +
                    "'appliedSmartLabel': 'true'," +
                        "'data': [");
            undrmet.Append("{" +
                // dataset level attributes
                     "'seriesname': 'Unlst Dr Cnt'," +
                           "'stepSkipped': 'false'," +
                    "'appliedSmartLabel': 'true'," +
                     "'data': [");


            for (int i = 0; i < arrData.GetLength(0); i++)
            {
                if (i > 0)
                {
                    categories.Append(",");
                    drmet.Append(",");
                    chmet.Append(",");
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

                chmet.AppendFormat("{{" +
                    // data level attributes
                          "'value': '{0}'" +
                      "}}", arrData[i, 2]);

                undrmet.AppendFormat("{{" +
                    // data level attributes
                          "'value': '{0}'" +
                      "}}", arrData[i, 3]);
            }

            categories.Append("]" +
                    "}" +
                "],");

            drmet.Append("]" +
                    "},");
            chmet.Append("]" +
                    "},");
            undrmet.Append("]" +
                 "}");

            jsonData.Append(categories.ToString());
            jsonData.Append("'dataset': [");
            jsonData.Append(drmet.ToString());
            jsonData.Append(chmet.ToString());
            jsonData.Append(undrmet.ToString());
            jsonData.Append("]" +
                    "}");

            return jsonData.ToString();
        }
    }
}