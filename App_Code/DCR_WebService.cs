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
using System.Globalization;

/// <summary>
/// Summary description for DCR_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DCR_WebService : System.Web.Services.WebService
{

    public DCR_WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Field_Force> GetFieldForceName()
    {
        List<Field_Force> objField = new List<Field_Force>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();
            string sf_type = HttpContext.Current.Session["sf_type"].ToString();

            SalesForce sf = new SalesForce();
            DataSet dsSalesforce = new DataSet();

            if (Session["sf_type"].ToString() == "1")
            {
                dsSalesforce = sf.SalesForceListMgrGet(div_code, sf_code);
                if (dsSalesforce.Tables[0].Rows.Count > 0)
                {
                    DataTable dtMR = dsSalesforce.Tables[0].AsEnumerable()
                                .Where(r => r.Field<string>("SF_Code") == sf_code)
                                .CopyToDataTable();
                    dsSalesforce = null;
                    dsSalesforce.Tables.Add(dtMR);
                }
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    dsSalesforce = sf.SalesForceListMgrGet(div_code, sf_code);
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsSalesforce = null;
                    dsSalesforce.Tables.Add(dt);
                }
            }
            else
            {
                dsSalesforce = sf.UserList_Hierarchy(div_code, "admin");
            }

            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSalesforce.Tables[0].Rows)
                {
                    Field_Force objFFDet = new Field_Force();
                    objFFDet.Field_Sf_Code = dr["SF_Code"].ToString();
                    objFFDet.Field_Sf_Name = dr["Sf_Name"].ToString();
                    objField.Add(objFFDet);
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
    public List<Report_DCR_Setting> GetUserSetting(string obj_Rep)
    {
        List<Report_DCR_Setting> objField = new List<Report_DCR_Setting>();
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();

            string[] arr = new string[] { };
            string sf_Code = string.Empty;
            string date = string.Empty;
            string frm_Date = string.Empty;
            string to_Date = string.Empty;

            DataSet dsRep = new DataSet();
            SqlCommand cmd = new SqlCommand("sp_Rpt_DCR_User_Setting", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Division_Code", SqlDbType.Int).Value = div_code;
            cmd.Parameters.Add("@Sf_Code", SqlDbType.VarChar).Value = sf_Code;
            cmd.Parameters.Add("@Rep", SqlDbType.VarChar).Value = obj_Rep;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            conn.Open();
            da.Fill(dsRep);
            conn.Close();

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRep.Tables[0].Rows)
                {
                    Report_DCR_Setting objFFDet = new Report_DCR_Setting();
                    objFFDet.Rep_ID = dr["Rep_ID"].ToString();
                    objFFDet.Setting_ID = dr["Setting_ID"].ToString();
                    objFFDet.Setting_Name = dr["Setting_Name"].ToString();
                    objFFDet.Default_Flag = dr["Default_Flag"].ToString();
                    objFFDet.Param_ID = dr["Param_ID"].ToString();
                    objFFDet.Parameter_Name = dr["Parameter_Name"].ToString();
                    objFFDet.Parameter_Order = dr["Parameter_Order"].ToString();
                    objFFDet.Parameter_Flag = dr["Parameter_Flag"].ToString();
                    objFFDet.Sub_Parameter_Flag = dr["Sub_Parameter_Flag"].ToString();
                    objFFDet.Sub_Param_ID = dr["Sub_Param_ID"].ToString();
                    objFFDet.Sub_Parameter_Name = dr["Sub_Parameter_Name"].ToString();
                    objFFDet.Sub_Parameter_Order = dr["Sub_Parameter_Order"].ToString();
                    objFFDet.Sub_Flag = dr["Sub_Flag"].ToString();
                    objField.Add(objFFDet);
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
    public string GetReportData(string obj_Rep)
    {
        string objField = string.Empty;
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string[] arr = new string[] { };
            string sf_Code = string.Empty;
            string date = string.Empty;
            string frm_Date = string.Empty;
            string to_Date = string.Empty;

            arr = obj_Rep.Split('^');
            sf_Code = arr[0];
            date = arr[1];
            frm_Date = DateTime.ParseExact(date.Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            to_Date = DateTime.ParseExact(date.Substring(date.Length - 10), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            DataSet dsRep = new DataSet();
            SqlCommand cmd = new SqlCommand("sp_Rpt_DCR_Analysis", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Div_Code", SqlDbType.Int).Value = div_code;
            cmd.Parameters.Add("@sf_code", SqlDbType.VarChar).Value = sf_Code;
            cmd.Parameters.Add("@from_date", SqlDbType.VarChar).Value = frm_Date;
            cmd.Parameters.Add("@to_date", SqlDbType.VarChar).Value = to_Date;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            conn.Open();
            da.Fill(dsRep);
            conn.Close();

            if (dsRep.Tables[0].Rows.Count > 0)
            {
                objField = JsonConvert.SerializeObject(dsRep.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }
}

public class Field_Force
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}

public class Report_DCR_Setting
{
    public string Rep_ID { get; set; }
    public string Setting_ID { get; set; }
    public string Setting_Name { get; set; }
    public string Param_ID { get; set; }
    public string Parameter_Name { get; set; }
    public string Parameter_Order { get; set; }
    public string Parameter_Flag { get; set; }
    public string Sub_Parameter_Flag { get; set; }
    public string Default_Flag { get; set; }
    public string Sub_Param_ID { get; set; }
    public string Sub_Parameter_Name { get; set; }
    public string Sub_Parameter_Order { get; set; }
    public string Sub_Flag { get; set; }
}