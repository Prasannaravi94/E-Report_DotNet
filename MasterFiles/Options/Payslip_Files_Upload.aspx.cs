using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
public partial class MasterFiles_Options_Payslip_Files_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
        hHeading.InnerText = Page.Title;
    }
    protected void btnExtractZipFiles_Click(object sender, EventArgs e)
    {
        if (fileUploadZipFiles.HasFile)
        {
            string uploadedZipFile = Path.GetFileName(fileUploadZipFiles.PostedFile.FileName);
            string zipFileLocation = Server.MapPath("~/MasterFiles/Options/ZipFiles/" + uploadedZipFile);
            fileUploadZipFiles.SaveAs(zipFileLocation);
            
            ZipFile zipFileToExtract = ZipFile.Read(zipFileLocation);
            zipFileToExtract.ExtractAll(Server.MapPath("~/MasterFiles/Options/PaySlip_Files/"), ExtractExistingFileAction.OverwriteSilently);
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Files Uploaded Successfully');</script>");
         
        }

    } 
}