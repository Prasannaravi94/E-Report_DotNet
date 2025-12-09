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

using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Configuration;
public partial class MasterFiles_Task_Management_New_Task2 : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dssf = new DataSet();
    DataSet dsSalesForce = new DataSet();

    static string sSf_Code = string.Empty;
    static string stype = string.Empty;
    string sDivCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sSf_Code = Session["sf_code"].ToString();
        sDivCode = Session["div_code"].ToString();
        stype = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {

          
            Session["sf_code_Tem"] = null;
            SalesForce sf = new SalesForce();
            dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
             //   lblsf.Text = " ( " + dssf.Tables[0].Rows[0]["Sf_Name"].ToString() + " - " + dssf.Tables[0].Rows[0]["Sf_HQ"].ToString() + " ) ";
            }
            if (Session["sf_type"].ToString() == "1")
            {

                liassign.Visible = false;
            }

          

        }

        
    }
     [WebMethod(EnableSession = true)]

    public static string TaskDet(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();


      
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            string sProc_Name = "";

            if (sSf_Code.Contains("admin"))
            {
                sProc_Name = "Task_Self_Graph_admin";
            }
            else
            {
                sProc_Name = "Task_Self_Graph";
            }

            

            SqlCommand cmd = new SqlCommand(sProc_Name, con);


            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
          

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);
          
      
            con.Close();
            dt.Columns["Status_Name"].ColumnName = "Label";
            dt.AcceptChanges();
            dt.Columns["total"].ColumnName = "Value";

            dt.AcceptChanges();




            string jsonResult = JsonConvert.SerializeObject(dt);

            return jsonResult;

        }
    }
     [WebMethod(EnableSession = true)]

     public static string TaskDetTeam(string objData)
     {
         string div_code = HttpContext.Current.Session["div_code"].ToString();



         using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
         {
             DataTable dt = new DataTable();
             // dt.Columns.Add("Label");
             // dt.Columns.Add("Value");
             string sProc_Name = "";
             if (stype == "1")
             {
                 sProc_Name = "Task_Self_Graph";
             }
             else
             {
                 sProc_Name = "Task_Self_Graph_Team";
             }

             SqlCommand cmd = new SqlCommand(sProc_Name, con);


             cmd.CommandTimeout = 600;
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Div_Code", div_code);
             cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);


             con.Open();
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             da.SelectCommand = cmd;
             // DataTable dt = new DataTable();
             da.Fill(dt);


             con.Close();
             dt.Columns["Status_Name"].ColumnName = "Label";
             dt.AcceptChanges();
             dt.Columns["total"].ColumnName = "Value";

             dt.AcceptChanges();




             string jsonResult = JsonConvert.SerializeObject(dt);

             return jsonResult;

         }
     }

     [WebMethod(EnableSession = true)]

     public static string TaskPrior(string objData)
     {
         string div_code = HttpContext.Current.Session["div_code"].ToString();



         using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
         {
             DataTable dt = new DataTable();
             // dt.Columns.Add("Label");
             // dt.Columns.Add("Value");
             string sProc_Name = "";

             if (sSf_Code.Contains("admin"))
             {
                 sProc_Name = "Task_Self_Graph_Priority_admin";
             }
             else
             {
                 sProc_Name = "Task_Self_Graph_Priority";
             }



             SqlCommand cmd = new SqlCommand(sProc_Name, con);


             cmd.CommandTimeout = 600;
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Div_Code", div_code);
             cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);


             con.Open();
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             da.SelectCommand = cmd;
             // DataTable dt = new DataTable();
             da.Fill(dt);


             con.Close();
             dt.Columns["Priority_T"].ColumnName = "Label";
             dt.AcceptChanges();
             dt.Columns["total"].ColumnName = "Value";

             dt.AcceptChanges();




             string jsonResult = JsonConvert.SerializeObject(dt);

             return jsonResult;

         }
     }
     [WebMethod]
     [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
     public static List<values> getTaskVal()
     {
         string div_code = HttpContext.Current.Session["div_code"].ToString();

         List<values> lstTask = new List<values>();

         using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
         {
             SqlCommand cmd = new SqlCommand();

           //  cmd.CommandText = "Task_Self_Graph_Home";
             if (sSf_Code.Contains("admin"))
             {

                 cmd.CommandText = "Task_Self_Graph_Home_admin";
             }
             else
             {
                 cmd.CommandText = "Task_Self_Graph_Home";
             }


             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Div_Code", div_code);
             cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
          
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
                     cpData.Total = Convert.ToInt32(dr["Total"].ToString());
                     cpData.New = Convert.ToInt32(dr["New"].ToString());

                     cpData.Open = Convert.ToInt32(dr["Open"].ToString());
                     cpData.Completed = Convert.ToInt32(dr["Completed"].ToString());
                     cpData.Closed = Convert.ToInt32(dr["Closed"].ToString());
                     cpData.Reopen = Convert.ToInt32(dr["ReOpen"].ToString());
                     cpData.Hold = Convert.ToInt32(dr["Hold"].ToString());
                     cpData.Cancel = Convert.ToInt32(dr["Cancel"].ToString());
                     cpData.Due = Convert.ToInt32(dr["Due"].ToString());
                     lstTask.Add(cpData);
                 }
             }
             dr.Close();
             cn.Close();
         }
         return lstTask;
     }
     #region Class Values
     public class values
     {
         // public string Sf_Code { get; set; }
         public int Total { get; set; }
         public int New { get; set; }
         public int Open { get; set; }
         public int Completed { get; set; }
         public int Closed { get; set; }
         public int Reopen { get; set; }
         public int Hold { get; set; }
         public int Cancel { get; set; }
         public int Due { get; set; }
     }
     #endregion

     protected void Back_Click(object sender, EventArgs e)
     {
         if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
         {
             Server.Transfer("~/BasicMaster.aspx");
         }
         else if (Session["sf_type"].ToString() == "2") // MGR Login
         {
            Server.Transfer("~/MGR_Home.aspx");
         }
         else if (Session["sf_type"].ToString() == "1")
         {             
            Server.Transfer("~/Default_MR.aspx");
             
         }

     }
}