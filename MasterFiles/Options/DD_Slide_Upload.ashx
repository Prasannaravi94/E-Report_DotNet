<%@ WebHandler Language="C#" Class="DD_Slide_Upload" %>

using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

public class DD_Slide_Upload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string objField = "false";
        string objDivCode = "";
        objDivCode = context.Session["div_code"].ToString();
        try
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                int chunk = HttpContext.Current.Request["chunk"] != null ? int.Parse(HttpContext.Current.Request["chunk"]) : 0;
                string fileName = HttpContext.Current.Request["name"] != null ? HttpContext.Current.Request["name"] : string.Empty;
                if (fileName == "")
                {
                    fileName = HttpContext.Current.Request.Files[0].FileName.ToString();
                }
                string Division_SName = HttpContext.Current.Session["Division_SName"].ToString();
                HttpPostedFile fileUpload = HttpContext.Current.Request.Files[0];

                //Regex reg = new Regex("[*'\",_&#^@]");
                //fileName = reg.Replace(fileName, "_");
                //string[] splitfileName = fileName.Split('.');

                //fileName = splitfileName[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + splitfileName[1];
                fileName = fileName.Replace(" ", "_");
                var uploadPath = context.Server.MapPath("~/EDetailing_Files/" + Division_SName + "/download/");
                if (!System.IO.Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
                {
                    var buffer = new byte[fileUpload.InputStream.Length];
                    fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                    fs.Write(buffer, 0, buffer.Length);
                    //EDetailingWebService service = new EDetailingWebService();
                    //service.Upload_Files();
                    objField = "true";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}