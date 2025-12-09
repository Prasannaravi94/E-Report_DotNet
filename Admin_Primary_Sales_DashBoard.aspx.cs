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
public partial class Admin_Primary_Sales_DashBoard : System.Web.UI.Page
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
            FillProd();
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
    private void FillProd()
    {
        Product prod = new Product();
        DataSet dsprod = new DataSet();
        dsprod = prod.getProd(div_code);
        if (dsprod.Tables[0].Rows.Count > 0)
        {
            ddlsearch.DataTextField = "Product_Detail_Name";
            ddlsearch.DataValueField = "Product_Detail_Code";
            ddlsearch.DataSource = dsprod;
            ddlsearch.DataBind();
            ddlsearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--ALL--", "0"));
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
              
                //  ddlFYear.SelectedValue = "2018";
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();

       

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal(List<string> lst_Input_Mn_Yr)
    {
        int months;
        int cmonth;
        int cyear;
        if (Convert.ToInt32(lst_Input_Mn_Yr[0]) < 4)
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1) - Convert.ToInt32(lst_Input_Mn_Yr[1])) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1;
        }
        else
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1])) - Convert.ToInt32(lst_Input_Mn_Yr[1])) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]);

        }
        List<values> lstCoverage = new List<values>();
      
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

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            if (lst_Input_Mn_Yr[3] == "0")
            {
                cmd.CommandText = "Primary_CYS";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
                cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            }
            else
            {
                cmd.CommandText = "Primary_CYS_Prod";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
                cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
                cmd.Parameters.AddWithValue("@Prod", lst_Input_Mn_Yr[3]);
            }
         
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.Sf_Code = dr["Sf_Code"].ToString();

                    cpData.Target = Convert.ToDecimal(dr["Target"].ToString());

                    cpData.Sale = Convert.ToDecimal(dr["Sale"].ToString());
                    cpData.achie = Convert.ToDecimal(dr["achie"].ToString());
                    cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());
                    cpData.PC = Convert.ToDecimal(dr["PC"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    #region Class Values
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
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getSecondary_Sale(List<string> lst_Input_Mn_Yr)
    {

        List<values> lstCoverage = new List<values>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            if (lst_Input_Mn_Yr[3] == "0")
            {
                cmd.CommandText = "Secondary_CYS";

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
                cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            }
            else
            {
                cmd.CommandText = "Secondary_CYS_Prod";

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
                cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
                cmd.Parameters.AddWithValue("@Prod", lst_Input_Mn_Yr[3]);
            }
            //string cDate = "";
            //if (lst_Input_Mn_Yr[0].ToString() == "12")
            //    cDate = "01-01-" + (Convert.ToInt32(lst_Input_Mn_Yr[1].ToString()) + 1).ToString();
            //else
            //    cDate = lst_Input_Mn_Yr[0].ToString() + "-01-" + lst_Input_Mn_Yr[1].ToString();
            //cmd.Parameters.AddWithValue("@cDate", cDate);
            //  cmd.Parameters.AddWithValue("@cDate", Convert.ToInt32(lst_Input_Mn_Yr[3]));
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.P_Sf_Code = dr["Sf_Code"].ToString();

                    cpData.P_Target = Convert.ToDecimal(dr["Target"].ToString());

                    cpData.P_Sale = Convert.ToDecimal(dr["Sale"].ToString());
                    cpData.P_achie = Convert.ToDecimal(dr["achie"].ToString());
                    cpData.P_PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    cpData.P_Growth = Convert.ToDecimal(dr["Growth"].ToString());
                    cpData.P_PC = Convert.ToDecimal(dr["PC"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal_YTD(List<string> lst_Input_Mn_Yr)
    {
        int months;
        int cmonth;
        int cyear;
        if (Convert.ToInt32(lst_Input_Mn_Yr[0]) < 4)
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1])) - (Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1;
        }
        else
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1])) - Convert.ToInt32(lst_Input_Mn_Yr[1])) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]);

        }
        List<values> lstCoverage = new List<values>();

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

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            if (lst_Input_Mn_Yr[3] == "0")
            {
                cmd.CommandText = "Primary_YTD";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            }
            else
            {
                cmd.CommandText = "Primary_YTD_Prod";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Prod", lst_Input_Mn_Yr[3]);
            }

            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.Sf_Code = dr["Sf_Code"].ToString();

                    cpData.Target = Convert.ToDecimal(dr["Target"].ToString());

                    cpData.Sale = Convert.ToDecimal(dr["Sale"].ToString());
                    cpData.achie = Convert.ToDecimal(dr["achie"].ToString());
                    cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());
                    cpData.PC = Convert.ToDecimal(dr["PC"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getSecondary_Sale_YTD(List<string> lst_Input_Mn_Yr)
    {
        int months;
        int cmonth;
        int cyear;
        if (Convert.ToInt32(lst_Input_Mn_Yr[0]) < 4)
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1])) - (Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]) - 1;
        }
        else
        {
            smonth = "4";
            tmonth = lst_Input_Mn_Yr[0];
            months = ((Convert.ToInt32(lst_Input_Mn_Yr[1])) - Convert.ToInt32(lst_Input_Mn_Yr[1])) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            cmonth = Convert.ToInt32(smonth);
            cyear = Convert.ToInt32(lst_Input_Mn_Yr[1]);

        }
        List<values> lstCoverage = new List<values>();

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

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            if (lst_Input_Mn_Yr[3] == "0")
            {
                cmd.CommandText = "Secondary_YTD";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            }
            else
            {
                cmd.CommandText = "Secondary_YTD_Prod";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDivCode));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Prod", lst_Input_Mn_Yr[3]);
            }

            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.Sf_Code = dr["Sf_Code"].ToString();

                    cpData.Target = Convert.ToDecimal(dr["Target"].ToString());

                    cpData.Sale = Convert.ToDecimal(dr["Sale"].ToString());
                    cpData.achie = Convert.ToDecimal(dr["achie"].ToString());
                    cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());
                    cpData.PC = Convert.ToDecimal(dr["PC"].ToString());
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }

    protected void ddlDBMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDBMode.SelectedItem.Value == "0")
        {
            Response.Redirect("Admin_Dashboard.aspx");
        }
        if (ddlDBMode.SelectedItem.Value == "1")
        {
            Response.Redirect("Admin_Sales_DashBoard.aspx");
        }
    }
}