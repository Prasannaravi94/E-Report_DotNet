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

public partial class DashBoard_SaleAnalysis_Graph : System.Web.UI.Page
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
    static string mode = string.Empty;
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
    [WebMethod(EnableSession=true)]

    public static string Input(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

     
        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            SqlCommand cmd = new SqlCommand("SecSale_Analysis_Stk_Graph", con);
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
            cmd.Parameters.AddWithValue("@cMonth", smonth);
            cmd.Parameters.AddWithValue("@cYear", syear);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.RemoveAt(5);
            dt.Columns.RemoveAt(4);
            dt.Columns.RemoveAt(2);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(0);
            con.Close();
            dt.Columns["Prod_Name"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["1_0_ABT"].ColumnName = "Value";
            dt.AcceptChanges();
    



            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    [WebMethod(EnableSession = true)]

    public static string Map(string objData)
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
            SqlCommand cmd;

            cmd = new SqlCommand("Statewise_Graph", con);

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


            dt.Columns.RemoveAt(0);

            con.Close();


            dt.Columns["1_0_ACT"].ColumnName = "value";
            dt.AcceptChanges();
            dt.Columns["state"].ColumnName = "id";
            dt.AcceptChanges();


            //  bindState();
            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }

    [WebMethod(EnableSession = true)]

    public static string ssgrid(string objData)
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
            SqlCommand cmd;

            cmd = new SqlCommand("Statewise_Graph_Grid", con);

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
            dt.Columns.RemoveAt(0);

            con.Close();
            dt.Columns["1_0_ACT"].ColumnName = "value";
            dt.AcceptChanges();
            dt.Columns["state"].ColumnName = "label";

            dt.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
    
}