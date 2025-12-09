using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.Net;
using System.Data;
using Newtonsoft.Json;

public partial class MasterFiles_LicenceKey_Maker : System.Web.UI.Page
{
	public string SrvPath;
    protected void Page_Load(object sender, EventArgs e)
    {
		SrvPath=Server.MapPath("~/Apps/");
    }
    [WebMethod]
    public static string getCompany(string Url)
    {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url+ "/Apps/ConfigAPP.json");
        httpWebRequest.Method = WebRequestMethods.Http.Get;
        httpWebRequest.Accept = "application/json; charset=utf-8";
        string file; 
        var response = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var sr = new StreamReader(response.GetResponseStream()))
        {
            file = sr.ReadToEnd();
        }
        /*var table = JsonConvert.DeserializeAnonymousType(file, new { Makes = default(DataTable) }).Makes;
        if (table.Rows.Count > 0)
        {
            
        }*/
        return file;//JsonConvert.SerializeObject(table);
    }
    [WebMethod]
    public static string getKeys(string Url,string Typ)
    {
        string FileNm = (Typ == "App") ? "ConfigAPP" : "ConfigiOS";
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + "/Apps/"+ FileNm + ".json");
        httpWebRequest.Method = WebRequestMethods.Http.Get;
        httpWebRequest.Accept = "application/json; charset=utf-8";
        string file;
        var response = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var sr = new StreamReader(response.GetResponseStream()))
        {
            file = sr.ReadToEnd();
        }
        /*var table = JsonConvert.DeserializeAnonymousType(file, new { Makes = default(DataTable) }).Makes;
        if (table.Rows.Count > 0)
        {
            
        }*/
        return file;//JsonConvert.SerializeObject(table);
    }
    [WebMethod]
    public static string svData(string data, string Typ,string path)
    {
        //var jdata = JsonConvert.DeserializeObject(data);
        //string jsonData = JsonConvert.SerializeObject(jdata, Formatting.None);
        string FileNm = (Typ == "App") ? "ConfigAPP" : "ConfigiOS";
        File.WriteAllText( path.Replace("/","\\")+ FileNm + ".json", data);
        return data;
    }

}