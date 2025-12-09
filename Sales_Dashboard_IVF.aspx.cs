using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Web.Configuration;
using DBase_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Sales_Dashboard_IVF : System.Web.UI.Page
{
    string[] Distinct;
    string[] strsplit;
    string sSub = string.Empty;
    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;
    static string FMName = string.Empty;
    static string spec = string.Empty;
    static string SubDiv_Code = string.Empty;
    static string MGR_Code = string.Empty;

    string sf_code = string.Empty;
    string Trans_month_year = string.Empty;
    string cs = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlDataAdapter adapt;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sDivCode = Request.QueryString["div_Code"].ToString();
        sSf_Code = Request.QueryString["sf_code"].ToString();
        smonth = Request.QueryString["Frm_Month"].ToString();
        syear = Request.QueryString["Frm_year"].ToString();
        tmonth = Request.QueryString["To_Month"].ToString();
        tyear = Request.QueryString["To_year"].ToString();

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal_YTD(List<string> lst_Input_Mn_Yr)
    {


        List<values> lstCoverage = new List<values>();
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();

            int div_cnt = 0;


            //cmd.CommandText = "Primary_YTD_All_Div";
            cmd.CommandText = "Primary_YTD_All_Div_Dash_IVF";
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@year", pData[0]);
          //  cmd.Parameters.AddWithValue("@Div_Code", sDivCode+',');
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(sDivCode + ","));
            //cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(lst_Input_Mn_Yr[6]));
            cmd.Parameters.AddWithValue("@MSf_Code", sSf_Code);
            cmd.Parameters.AddWithValue("@FMonth", smonth);
           
            cmd.Parameters.AddWithValue("@TMonth", tmonth);

            cmd.Parameters.AddWithValue("@FYear", syear);
            cmd.Parameters.AddWithValue("@TYear", tyear);


            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                values cpData = new values();
                cpData.Sf_Code = ds.Tables[0].Rows[0]["Sf_Code"].ToString();
                cpData.Target = Convert.ToDecimal(ds.Tables[0].Rows[0]["Target"].ToString());
                cpData.Sale = Convert.ToDecimal(ds.Tables[0].Rows[0]["Sale"].ToString());
                cpData.achie = Convert.ToDecimal(ds.Tables[0].Rows[0]["achie"].ToString());

                cpData.PSale = Convert.ToDecimal(ds.Tables[0].Rows[0]["PSale"].ToString());
                cpData.Growth = Convert.ToDecimal(ds.Tables[0].Rows[0]["Growth"].ToString());
                cpData.PC = Convert.ToDecimal(ds.Tables[0].Rows[0]["PC"].ToString());
                cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();


                lstCoverage.Add(cpData);
            }

            cn.Close();
        }
        return lstCoverage;
    }
    [WebMethod(EnableSession = true)]
    public static string Primary(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();

        string[] arr = objData.Split('^');
    
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();

            SqlCommand cmd = new SqlCommand();

           
                //cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div", con);
                cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div_Dash_IVF", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
        
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(sDivCode + ","));
            //cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(lst_Input_Mn_Yr[6]));
            cmd.Parameters.AddWithValue("@MSf_Code", sSf_Code);
            cmd.Parameters.AddWithValue("@FMonth", smonth);

            cmd.Parameters.AddWithValue("@TMonth", tmonth);

            cmd.Parameters.AddWithValue("@FYear", syear);
            cmd.Parameters.AddWithValue("@TYear", tyear);

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
            sortedDT.Columns["prod_code"].ColumnName = "Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Name"].ColumnName = "Division_Name";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Target_Val"].ColumnName = "Target_Val";
            sortedDT.AcceptChanges();
            sortedDT.Columns["achie"].ColumnName = "achie";
            sortedDT.AcceptChanges();

            sortedDT.Columns["Growth"].ColumnName = "Growth";
            sortedDT.AcceptChanges();
            sortedDT.Columns["PC"].ColumnName = "PC";
            sortedDT.AcceptChanges();
            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }

    public class values
    {
        public string Sf_Code { get; set; }

        public decimal Target { get; set; }
        public decimal Sale { get; set; }
        public decimal achie { get; set; }
        public decimal PSale { get; set; }
        public decimal Growth { get; set; }

        public decimal PC { get; set; }

        public string P_Sf_Code { get; set; }

        public decimal P_Target { get; set; }
        public decimal P_Sale { get; set; }
        public decimal P_achie { get; set; }
        public decimal P_PSale { get; set; }
        public decimal P_Growth { get; set; }

        public decimal P_PC { get; set; }

        public string Div_Name { get; set; }
        public string Div_Code { get; set; }

    }
}