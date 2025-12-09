<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.Script.Services;
using System.Data;
using System.IO;

public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        HttpFileCollection files = context.Request.Files;
        Product prd = new Product();
        DataSet ds = new DataSet();
        string Div_Code = "";
        string Div_Name = "";

        
        
        Div_Code = context.Session["div_code"].ToString();
        Div_Name = context.Session["Division_SName"].ToString();
        
        
        var folder = HttpContext.Current.Server.MapPath("~/Edetailing_files/"+ Div_Name +"/download");
        if (!Directory.Exists(folder))
           {
                Directory.CreateDirectory(folder);
           }

        ds = prd.GetFileFolderPath(Div_Code);
        string strAlrFile = "";
        
        for (int i = 0; i < files.Count; i++)
        {
            HttpPostedFile file = files[i];
            string filePath = context.Server.MapPath("~/Edetailing_files/"+ Div_Name +"/download/" + file.FileName);
              if (System.IO.File.Exists(filePath))
              {
                  strAlrFile += file.FileName +',';
              }
              else
              {
                  string[] strSplitName = file.FileName.Split('.');
                  //string fname = context.Server.MapPath("~/Edetailing_files/KS/download/" + strSplitName[0] + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "." + strSplitName[1]);
                  string fname = context.Server.MapPath("~/Edetailing_files/" + Div_Name + "/download/" + file.FileName);
                  file.SaveAs(fname);
              }
        }

        if (strAlrFile.Length > 0)
        {           
            context.Response.Write("File "+ strAlrFile + "This are files are already Exit");
        }
        else
        {
            context.Response.Write("File Uploaded Successfully");
        }
       
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}