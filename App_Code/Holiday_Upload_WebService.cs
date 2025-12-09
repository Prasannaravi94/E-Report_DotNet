using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for Holiday_Upload_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Holiday_Upload_WebService : System.Web.Services.WebService
{

    public Holiday_Upload_WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Holiday_Excel> Holiday_Upload_Process(string objHolidayUpload)
    {
        List<Holiday_Excel> objField = new List<Holiday_Excel>();
        try
        {
            Holiday ss = new Holiday();
            DataSet ds = new DataSet();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();

            ds = ss.Holiday_Upload_Process(div_code, objHolidayUpload);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Holiday_Excel objFFDet = new Holiday_Excel();
                objFFDet.Year = dr["Year"].ToString();
                objFFDet.State_Code = dr["State_Code"].ToString();
                objFFDet.Holiday_Date = dr["Holiday_Date"].ToString();
                objFFDet.Holiday_Name = dr["Holiday_Name"].ToString();
                objFFDet.Holiday_ID = dr["Holiday_ID"].ToString();
                objFFDet.Division_Code = dr["Division_Code"].ToString();
                objField.Add(objFFDet);
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
    public string Holiday_Upload(string objHolidayUpload)
    {
        string objField = "false";
        try
        {
            Holiday ss = new Holiday();
            DataSet ds = new DataSet();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();
            int iReturn = -1;

            iReturn = ss.Holiday_Upload(div_code, objHolidayUpload);
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }
}


#region SSParam
public class Holiday_Excel
{
    public string Year { get; set; }
    public string State_Code { get; set; }
    public string Holiday_Date { get; set; }
    public string Holiday_Name { get; set; }
    public string Holiday_ID { get; set; }
    public string Division_Code { get; set; }
}
#endregion