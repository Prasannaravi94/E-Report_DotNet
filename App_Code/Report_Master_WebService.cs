using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Report_Master_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Report_Master_WebService : System.Web.Services.WebService
{

    public Report_Master_WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Reports> GetReport()
    {
        List<Reports> objField = new List<Reports>();
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            DataSet dsRep = new DataSet();

            dsRep = SelectDataSet("SELECT Rep_ID,Rpt_Name, CASE WHEN Active=0 THEN 'Active' ELSE 'Deactivated' END as Status FROM Rpt_Master WHERE Division_Code='" + div_code + "'");

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRep.Tables[0].Rows)
                {
                    Reports objRep = new Reports();
                    objRep.Rep_ID = dr["Rep_ID"].ToString();
                    objRep.Rpt_Name = dr["Rpt_Name"].ToString();
                    objRep.Active = dr["Status"].ToString();
                    objField.Add(objRep);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Update_Rep_Master(string obj_Rep)
    {
        int objField = -1;
        try
        {            
            int div_code = Convert.ToInt16(HttpContext.Current.Session["div_code"]);
            string[] arr = new string[] { };
            string rep_Name = string.Empty;
            int rep_ID = -1;
            int rep_Status = -1;

            arr = obj_Rep.Split('^');
            rep_Name = arr[0];
            rep_ID = Convert.ToInt32(arr[1]);
            rep_Status = Convert.ToInt16(arr[2]);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("sp_Rep_Upd_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Div_Code", SqlDbType.TinyInt).Value = div_code;
            cmd.Parameters.Add("@Rep_ID", SqlDbType.Int).Value = rep_ID;
            cmd.Parameters.Add("@Rpt_Name", SqlDbType.VarChar).Value = rep_Name;
            cmd.Parameters.Add("@Active", SqlDbType.TinyInt).Value = rep_Status;
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            objField = Convert.ToInt32(cmd.Parameters["@result"].Value);
            conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Update_Parameter(string obj_Param)
    {
        int objField = -1;
        try
        {
            int div_code = Convert.ToInt16(HttpContext.Current.Session["div_code"]);
            string[] arr = new string[] { };
            string param_Name = string.Empty;
            int rep_ID = -1;
            int param_ID = -1;
            int param_Status = -1;

            arr = obj_Param.Split('^');
            param_Name = arr[0];
            rep_ID = Convert.ToInt32(arr[1]);
            param_ID = Convert.ToInt32(arr[2]);
            param_Status = Convert.ToInt16(arr[3]);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("sp_Param_Upd_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Div_Code", SqlDbType.TinyInt).Value = div_code;
            cmd.Parameters.Add("@Rep_ID", SqlDbType.Int).Value = rep_ID;
            cmd.Parameters.Add("@Param_ID", SqlDbType.Int).Value = param_ID;
            cmd.Parameters.Add("@Param_Name", SqlDbType.VarChar).Value = param_Name;
            cmd.Parameters.Add("@Active", SqlDbType.TinyInt).Value = param_Status;
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            objField = Convert.ToInt32(cmd.Parameters["@result"].Value);
            conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Update_Sub_Parameter(string obj_Param)
    {
        int objField = -1;
        try
        {
            int div_code = Convert.ToInt16(HttpContext.Current.Session["div_code"]);
            string[] arr = new string[] { };
            string sub_Param_Name = string.Empty;
            int rep_ID = -1;
            int param_ID = -1;
            int sub_Param_ID = -1;
            int sub_Param_Status = -1;

            arr = obj_Param.Split('^');
            sub_Param_Name = arr[0];
            rep_ID = Convert.ToInt32(arr[1]);
            param_ID = Convert.ToInt32(arr[2]);
            sub_Param_ID = Convert.ToInt32(arr[3]);
            sub_Param_Status = Convert.ToInt16(arr[4]);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("sp_Sub_Param_Upd_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Div_Code", SqlDbType.TinyInt).Value = div_code;
            cmd.Parameters.Add("@Rep_ID", SqlDbType.Int).Value = rep_ID;
            cmd.Parameters.Add("@Param_ID", SqlDbType.Int).Value = param_ID;
            cmd.Parameters.Add("@Sub_Param_ID", SqlDbType.Int).Value = sub_Param_ID;
            cmd.Parameters.Add("@Sub_Param_Name", SqlDbType.VarChar).Value = sub_Param_Name;
            cmd.Parameters.Add("@Active", SqlDbType.TinyInt).Value = sub_Param_Status;
            cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            objField = Convert.ToInt32(cmd.Parameters["@result"].Value);
            conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Parameters> GetParameter(string objRep_ID)
    {
        List<Parameters> objField = new List<Parameters>();
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            DataSet dsRep = new DataSet();

            dsRep = SelectDataSet("SELECT Param_ID,Rep_ID,Parameter_Name, CASE WHEN Active=0 THEN 'Active' ELSE 'Deactivated' END as Status FROM Rpt_Parameter WHERE Rep_ID = '" + objRep_ID + "' AND Division_Code='" + div_code + "'");

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRep.Tables[0].Rows)
                {
                    Parameters objRep = new Parameters();
                    objRep.Rep_ID = dr["Rep_ID"].ToString();
                    objRep.Param_ID = dr["Param_ID"].ToString();
                    objRep.Parameter_Name = dr["Parameter_Name"].ToString();
                    objRep.Active = dr["Status"].ToString();
                    objField.Add(objRep);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<SubParameters> GetSubParameter(string objRep_ID, string objParamID)
    {
        List<SubParameters> objField = new List<SubParameters>();
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            DataSet dsRep = new DataSet();

            dsRep = SelectDataSet("SELECT Sub_Param_ID,Param_ID,Rep_ID,Sub_Parameter_Name, CASE WHEN Active=0 THEN 'Active' ELSE 'Deactivated' END as Status FROM Rpt_Sub_Parameter WHERE Rep_ID = '" + objRep_ID + "' AND Param_ID = '" + objParamID + "' AND Division_Code='" + div_code + "'");

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRep.Tables[0].Rows)
                {
                    SubParameters objRep = new SubParameters();
                    objRep.Rep_ID = dr["Rep_ID"].ToString();
                    objRep.Param_ID = dr["Param_ID"].ToString();
                    objRep.Sub_Param_ID = dr["Sub_Param_ID"].ToString();
                    objRep.Sub_Parameter_Name = dr["Sub_Parameter_Name"].ToString();
                    objRep.Active = dr["Status"].ToString();
                    objField.Add(objRep);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    public DataSet SelectDataSet(string SQL)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = SQL;
        da.SelectCommand = cmd;
        DataSet ds = new DataSet();

        conn.Open();
        da.Fill(ds);
        conn.Close();

        return ds;
    }
}


public class Reports
{
    public string Rep_ID { get; set; }
    public string Rpt_Name { get; set; }
    public string Active { get; set; }
}

public class Parameters
{
    public string Rep_ID { get; set; }
    public string Param_ID { get; set; }
    public string Parameter_Name { get; set; }
    public string Active { get; set; }
}

public class SubParameters
{
    public string Rep_ID { get; set; }
    public string Param_ID { get; set; }
    public string Sub_Param_ID { get; set; }
    public string Sub_Parameter_Name { get; set; }
    public string Active { get; set; }
}