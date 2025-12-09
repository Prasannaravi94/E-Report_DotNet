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

public partial class MIS_Reports_rpt_Quiz_Graph : System.Web.UI.Page
{
    static string sf_code = string.Empty;
    static string FMonth = string.Empty;
    static string FYear = string.Empty;
    static string TMonth = string.Empty;
    static string TYear = string.Empty;
    static string survey = string.Empty;
    string div_code = string.Empty;
    static string strFieledForceName = string.Empty;
    DataSet dsquiz = new DataSet();
   static string Head = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
          
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf=new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
           Head= "Quiz Test Result for the month of  " + strFrmMonth + " " + FYear + " ";
            dsquiz = sf.Quiz_Survey_id(div_code, sf_code, FMonth, FYear);
            if (dsquiz.Tables[0].Rows.Count > 0)
            {
                survey = dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString();
            }
        }
    }
    [WebMethod]
    public static string Quiz()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            sProc_Name = "Quiz_Graph";

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@div_Code", div_code);
            cmd.Parameters.AddWithValue("@sf_code", sf_code);
            cmd.Parameters.AddWithValue("@month", Convert.ToInt32(FMonth));
            cmd.Parameters.AddWithValue("@year", Convert.ToInt32(FYear));
            cmd.Parameters.AddWithValue("@survey_id", Convert.ToInt32(survey));
       
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
                   
                     "'caption' : '" + Head + "'  ," +
                          "'subcaption' : '" + strFieledForceName + "'," +
                    "'bgcolor': 'FFFFFF'," +
                    "'showHoverEffect': '1'," +
                        "'plotgradientcolor': ''," +
                        "'plotBorderDashed':'1'," +
                        "'plotBorderDashLen':'5'," +
        "'showalternatehgridcolor': '0'," +
        "'showplotborder': '1'," +
        "'divlinecolor': 'CCCCCC'," +
        "'showvalues': '1'," +

        "'yaxisname': 'No of Fieldforce'," +
          "'xaxisname': 'Range (%)'," +
           "'palettecolors': '#d2272d,#ea932c'," +

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

                        "'seriesName': 'I Attempt'," +
                        "'data': [");

            undrmet.Append("{" +
                // dataset level attributes
                         "'seriesName': 'II Attempt'," +
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
}